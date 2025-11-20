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

Module BuildProject

    Public Sub CreateModelProject(proj As GenBankProject, settings As Settings, skipTRN As Boolean, outproj As String)
        Dim server As New RegistryUrl(settings.registry_server, cache_dir:=settings.cache_dir)
        Dim blast_threads As Integer = settings.n_threads
        Dim knownOperons = server.GetAllKnownOperons.ToDictionary(Function(a) a.cluster_id)
        Dim localblast As New BLASTPlus(settings.ncbi_blast) With {.NumThreads = blast_threads}
        Dim enzyme_db As String = $"{settings.blastdb}/ec_numbers.fasta"
        Dim transporter_db As String = $"{App.HOME}/data/Membrane.fasta"
        Dim tf_db As String = $"{App.HOME}/data/TF.fasta"

        ' ------- TFBS sites --------
        Dim pwm = MotifDatabase.OpenReadOnly($"{settings.blastdb}/RegPrecise.dat".Open(FileMode.Open, doClear:=False, [readOnly]:=True))
        Dim tss = proj.tss_upstream _
            .Select(Function(seq)
                        Return New FastaSeq({seq.Key}, seq.Value)
                    End Function) _
            .ToArray

        If Not skipTRN Then
            proj.tfbs_hits = pwm.ScanSites(tss, blast_threads, workflowMode:=True)
        End If

        ' ----- enzyme hits ------
        Dim tempfile As String = TempFileSystem.GetAppSysTempFile(".fasta", prefix:=$"enzyme_number_{App.PID}")
        Dim tempOutfile As String = tempfile.ChangeSuffix(".txt")

        Using s As Stream = tempfile.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpProteinFasta(s)
        End Using

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
        tempfile = TempFileSystem.GetAppSysTempFile(".fasta", prefix:=$"operon_{App.PID}")
        tempOutfile = tempfile.ChangeSuffix(".txt")

        Using s As Stream = tempfile.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpGeneFasta(s)
        End Using

        Dim operon_db As String = $"{settings.blastdb}/operon.fasta"

        localblast.Blastn(tempfile, operon_db, tempOutfile, e:=0.01).Run()

        proj.operon_hits = OperonAnnotator.ParseBlastn(tempOutfile).ToArray
        proj.operons = OperonAnnotator.AnnotateOperons(proj.gene_table, proj.operon_hits, knownOperons).ToArray

        Call proj.SaveZip(outproj)
    End Sub
End Module
