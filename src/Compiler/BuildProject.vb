Imports System.IO
Imports CADRegistry
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.Analysis.SequenceTools.SequencePatterns
Imports SMRUCC.genomics.Assembly.NCBI.CDD
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

    Sub New(proj As GenBankProject, settings As Settings)
        Dim blast_threads As Integer = settings.n_threads

        Me.proj = proj
        Me.settings = settings
        Me.localblast = New BLASTPlus(settings.ncbi_blast) With {.NumThreads = blast_threads}
        Me.server = New RegistryUrl(settings.registry_server, cache_dir:=settings.cache_dir)

        prot_data = TempFileSystem.GetAppSysTempFile(".fasta", prefix:=$"prot_{App.PID}")
        gene_data = TempFileSystem.GetAppSysTempFile(".fasta", prefix:=$"nucl_{App.PID}")

        Using s As Stream = prot_data.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpProteinFasta(s)
        End Using
        Using s As Stream = gene_data.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpGeneFasta(s)
        End Using

        Call localblast.FormatDb(prot_data, dbType:="prot")
        Call localblast.FormatDb(gene_data, dbType:="nucl")
    End Sub

    Public Sub EnzymeAnnotation()

    End Sub

    Public Sub BuildTRN()

    End Sub

    Public Sub BuildOperonClusters()
        Dim knownOperons = server.GetAllKnownOperons.ToDictionary(Function(a) a.cluster_id)
    End Sub

    Public Sub CreateModelProject(proj As GenBankProject, settings As Settings, skipTRN As Boolean, outproj As String)

        Dim enzyme_db As String = $"{settings.blastdb}/ec_numbers.fasta"
        Dim transporter_db As String = $"{settings.blastdb}/subcellular.fasta"
        Dim tf_db As String = $"{settings.blastdb}/TF.fasta"

        ' ------- TFBS sites --------
        Dim motif_db As String = $"{settings.blastdb}/RegPrecise.dat"
        Dim tss = proj.tss_upstream _
            .Select(Function(seq)
                        Return New FastaSeq({seq.Key}, seq.Value)
                    End Function) _
            .ToArray

        If Not skipTRN Then
            proj.tfbs_hits = CADRegistry.MotifDatabase _
                .OpenReadOnly(motif_db.OpenReadonly) _
                .ScanSites(tss,
                           n_threads:=blast_threads,
                           workflowMode:=True)
        End If

        ' ----- enzyme hits ------

        Dim tempOutfile As String = tempfile.ChangeSuffix(".txt")



        localblast.Blastp(tempfile, enzyme_db, tempOutfile, e:=0.01).Run()

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

        ' -------- transcript factors --------
        tempOutfile = TempFileSystem.GetAppSysTempFile(".txt", sessionID:=App.PID, prefix:="tf_blast")

        localblast.Blastp(tempfile, tf_db, tempOutfile, e:=0.01).Run()

        proj.tf_hits = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHitsResult _
            .ToArray
        proj.transcript_factors = proj.tf_hits _
            .Select(Function(hits) hits.AssignTFFamilyHit()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToArray

        ' ------ membrane transporter -------
        tempOutfile = TempFileSystem.GetAppSysTempFile(".txt", sessionID:=App.PID, prefix:="transporter_blast")

        localblast.Blastp(tempfile, transporter_db, tempOutfile, e:=0.01).Run()

        proj.transporter = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHitsResult(grepName:=Function(name) name.GetTagValue("|")) _
            .ToArray
        proj.membrane_proteins = proj.transporter _
            .Select(Function(hits) RankTerm.RankTopTerm(hits)) _
            .IteratesALL _
            .ToArray

        ' ------ operon, conserved gene clusters -------

        tempOutfile = tempfile.ChangeSuffix(".txt")



        Dim operon_db As String = $"{settings.blastdb}/operon.fasta"

        localblast.Blastn(tempfile, operon_db, tempOutfile, e:=0.01).Run()

        proj.operon_hits = OperonAnnotator.ParseBlastn(tempOutfile).ToArray
        proj.operons = OperonAnnotator.AnnotateOperons(proj.gene_table, proj.operon_hits, knownOperons).ToArray

        Call proj.SaveZip(outproj)
    End Sub
End Class
