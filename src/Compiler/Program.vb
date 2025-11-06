Imports System.IO
Imports CADRegistry
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.CommandLine
Imports SMRUCC.genomics.Assembly.NCBI.CDD
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Programs
Imports System.IO
Imports CADRegistry
Imports Galaxy.Data.TableSheet
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Unit
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualStudio.WinForms.Docking
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Programs
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models

Module Program

    Public Function Main(args As String()) As Integer
        Return GetType(Program).RunCLI(App.CommandLine, executeFile:=AddressOf RunCompiler)
    End Function

    Private Function RunCompiler(file As String, args As CommandLine) As Integer
        If file.ExtensionSuffix("gcproj") Then
            Return CompileProjectFile(file, args)
        Else
            Return CompileGenbankFile(file, args)
        End If
    End Function

    ' ./target.gcproj --out model.xml --server "http://biocad.innovation.ac.cn" --name XXXX 
    Public Function CompileProjectFile(file As String, args As CommandLine) As Integer
        Dim proj As GenBankProject = ProjectIO.Load(file)
        Dim serverUrl As String = args("--server") Or RegistryUrl.defaultServer
        Dim savefile As String = args("--out") Or file.ChangeSuffix("xml")
        Dim compiler As New Compiler(proj, serverUrl)
        Dim model As VirtualCell = compiler.Compile(args)

        Return model _
            .GetXml _
            .SaveTo(savefile) _
            .CLICode
    End Function

    ' ./target.gbff --out gcmodeller.gcproj --config settings.json --num_threads 8 --workdir workspace_dir
    Public Function CompileGenbankFile(file As String, args As CommandLine) As Integer
        Dim proj = ProjectCreator.FromGenBank(GBFF.File.Load(file))
        Dim settings As Settings = Settings.Load(args("--config"))
        Dim outproj As String = args("--out")
        Dim server As New RegistryUrl(settings.registry_server)
        Dim blast_threads As Integer = args("--num_threads") Or 8
        Dim knownOperons = server.GetAllKnownOperons.ToDictionary(Function(a) a.cluster_id)
        Dim localblast As New BLASTPlus(settings.ncbi_blast) With {.NumThreads = blast_threads}
        Dim enzyme_db As String = $"{settings.blastdb}/ec_numbers.fasta"
        Dim workdir As String = args("--workdir")

        If Not workdir.StringEmpty(, True) Then
            Call App.SetSystemTemp(workdir)
        End If

        Dim tempfile As String = TempFileSystem.GetAppSysTempFile(".fasta", prefix:=$"enzyme_number_{App.PID}")
        Dim tempOutfile As String = tempfile.ChangeSuffix(".txt")

        Using s As Stream = tempfile.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpProteinFasta(s)
        End Using

        localblast.Blastp(tempfile, enzyme_db, tempOutfile, e:=0.01).Run()

        proj.enzyme_hits = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHistResult _
            .ToArray
        proj.ec_numbers = proj.enzyme_hits _
            .Select(Function(hits) hits.AssignECNumber()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToDictionary(Function(a) a.gene_id)

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

        Return 0
    End Function
End Module
