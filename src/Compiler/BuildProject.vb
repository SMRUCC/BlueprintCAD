Imports System.IO
Imports CADRegistry
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.Analysis.SequenceTools.SequencePatterns
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Programs
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline
Imports SMRUCC.genomics.SequenceModel.FASTA
Imports TRNScanner

Public Class BuildProject

    ReadOnly proj As GenBankProject
    ReadOnly settings As Settings
    ReadOnly localblast As BLASTPlus
    ReadOnly server As RegistryUrl

    ReadOnly gene_data As String
    ReadOnly prot_data As String
    ReadOnly enableBlastCache As Boolean = False
    ReadOnly session_hashcode As String

    Sub New(proj As GenBankProject, settings As Settings, Optional enableBlastCache As Boolean = False)
        Dim blast_threads As Integer = settings.n_threads

        Me.enableBlastCache = enableBlastCache
        Me.proj = proj
        Me.settings = settings
        Me.localblast = New BLASTPlus(settings.ncbi_blast) With {.NumThreads = blast_threads}
        Me.server = New RegistryUrl(settings.registry_server, cache_dir:=settings.cache_dir)
        Me.session_hashcode = proj.ComputeHashCode

        Call $"hashcode of this genomics data is {session_hashcode}".debug

        prot_data = TempFileSystem.GetAppSysTempFile(".fasta", prefix:=$"prot_{session_hashcode}")
        gene_data = TempFileSystem.GetAppSysTempFile(".fasta", prefix:=$"nucl_{session_hashcode}")

        Using s As Stream = prot_data.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpProteinFasta(s)
        End Using
        Using s As Stream = gene_data.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpGeneFasta(s)
        End Using

        Call localblast.FormatDb(prot_data, dbType:="prot").Run()
        Call localblast.FormatDb(gene_data, dbType:="nucl").Run()
    End Sub

    Public Sub LocationAnnotation()
        Dim transporter_db As String = $"{settings.blastdb}/subcellular.fasta"
        Dim tempOutfile = TempFileSystem.GetAppSysTempFile(".txt", sessionID:=session_hashcode, prefix:=$"subcellular_")

        If enableBlastCache Then
            tempOutfile = App.SysTemp & $"/{session_hashcode}/subcellular_blastp.txt"
        End If
        If tempOutfile.FileExists AndAlso enableBlastCache Then
            ' do nothing
            Call $"use the cached [{tempOutfile}] result file for sub-cellular location annotation.".info
        Else
            ' run blast search
            Call localblast.Blastp(prot_data, transporter_db, tempOutfile, e:="1e-5").Run()
        End If

        proj.transporter = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHitsResult(grepName:=Function(name) name.GetTagValue("|")) _
            .ToArray
        proj.membrane_proteins = proj.transporter _
            .Select(Function(hits) RankTerm.RankTopTerm(hits)) _
            .IteratesALL _
            .ToArray
    End Sub

    Public Sub EnzymeAnnotation()
        Dim tempOutfile As String = TempFileSystem.GetAppSysTempFile(".txt", prefix:=$"ec_number_{session_hashcode}")
        Dim enzyme_db As String = $"{settings.blastdb}/ec_numbers.fasta"

        If enableBlastCache Then
            tempOutfile = App.SysTemp & $"/{session_hashcode}/ec_numbers_blastp.txt"
        End If
        If tempOutfile.FileExists AndAlso enableBlastCache Then
            ' do nothing
            Call $"use the cached [{tempOutfile}] result file for enzyme EC_number annotation.".info
        Else
            ' run blast search
            Call localblast.Blastp(prot_data, enzyme_db, tempOutfile, e:="1e-5").Run()
        End If

        proj.enzyme_hits = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHitsResult _
            .ToArray
        proj.ec_numbers = proj.enzyme_hits _
            .Select(Function(hits) hits.AssignECNumber()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToDictionary(Function(a)
                              Return a.gene_id
                          End Function)
    End Sub

    Public Sub TFAnnotation()
        Dim tempOutfile = TempFileSystem.GetAppSysTempFile(".txt", sessionID:=App.PID, prefix:=$"tf_blast_{App.PID}")
        Dim tf_db As String = $"{settings.blastdb}/TF.fasta"

        If enableBlastCache Then
            tempOutfile = App.SysTemp & $"/{session_hashcode}/TF_blastp.txt"
        End If
        If tempOutfile.FileExists AndAlso enableBlastCache Then
            ' do nothing
            Call $"use the cached [{tempOutfile}] result file for transcript factors annotation.".info
        Else
            ' run blast search
            Call localblast.Blastp(prot_data, tf_db, tempOutfile, e:="1e-5").Run()
        End If

        proj.tf_hits = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHitsResult _
            .ToArray
        proj.transcript_factors = proj.tf_hits _
            .Select(Function(hits) hits.AssignTFFamilyHit()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToArray
    End Sub

    Public Sub BuildTRN()
        Dim motif_db As String = $"{settings.blastdb}/RegPrecise.dat"
        Dim tss As FastaSeq() = proj.tss_upstream _
            .Select(Function(seq)
                        Return New FastaSeq({seq.Key}, seq.Value)
                    End Function) _
            .ToArray

        If motif_db.FileExists Then
            proj.tfbs_hits = CADRegistry.MotifDatabase _
                .OpenReadOnly(motif_db.OpenReadonly) _
                .ScanSites(tss,
                           n_threads:=settings.n_threads,
                           workflowMode:=True)
        Else
            proj.tfbs_hits = New Dictionary(Of String, MotifMatch())
        End If
    End Sub

    Public Sub BuildOperonClusters()
        Dim knownOperons = server.GetAllKnownOperons.ToDictionary(Function(a) a.cluster_id)
        Dim tempOutfile = TempFileSystem.GetAppSysTempFile(".txt", prefix:=$"operon_{App.PID}")
        Dim operon_db As String = $"{settings.blastdb}/operon.fasta"

        If enableBlastCache Then
            tempOutfile = App.SysTemp & $"/{session_hashcode}/operon_blastn.txt"
        End If
        If tempOutfile.FileExists AndAlso enableBlastCache Then
            ' do nothing
            Call $"use the cached [{tempOutfile}] result file for gene operon clusters annotation.".info
        Else
            ' run blast search
            Call localblast.Blastn(gene_data, operon_db, tempOutfile, e:="1e-5").Run()
        End If

        proj.operon_hits = OperonAnnotator.ParseBlastn(tempOutfile).ToArray
        proj.operons = OperonAnnotator.AnnotateOperons(proj.gene_table, proj.operon_hits, knownOperons).ToArray
    End Sub

    Public Shared Sub CreateModelProject(proj As GenBankProject, settings As Settings, skipTRN As Boolean, outproj As String, Optional enableBlastCache As Boolean = False)
        Dim worker As New BuildProject(proj, settings, enableBlastCache)

        ' ------- TFBS sites --------
        If Not skipTRN Then
            Call worker.BuildTRN()
        End If

        ' ----- enzyme hits ------
        Call worker.EnzymeAnnotation()
        ' -------- transcript factors --------
        Call worker.TFAnnotation()
        ' ------ membrane transporter -------
        Call worker.LocationAnnotation()
        ' ------ operon, conserved gene clusters -------
        Call worker.BuildOperonClusters()

        Call proj.SaveZip(outproj)
    End Sub
End Class
