Imports CADRegistry
Imports Microsoft.VisualBasic.CommandLine
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus

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

    ' ./target.gbff --out gcmodeller.gcproj --config settings.json
    Public Function CompileGenbankFile(file As String, args As CommandLine) As Integer
        Dim proj = ProjectCreator.FromGenBank(GBFF.File.Load(file))
        Dim settings As  
        Dim server As New RegistryUrl()
        Dim knownOperons = server.GetAllKnownOperons.ToDictionary(Function(a) a.cluster_id)

        proj.operon_hits = OperonAnnotator.ParseBlastn("G:\BlueprintCAD\demo\tmp\Sophia\41948\operon_blast869219.txt").ToArray
        proj.operons = OperonAnnotator.AnnotateOperons(proj.gene_table, proj.operon_hits, knownOperons).ToArray

        proj.enzyme_hits = BlastpOutputReader _
            .RunParser("G:\BlueprintCAD\demo\tmp\Sophia\19032\enzyme_blast650548.txt") _
            .ExportHistResult _
            .ToArray
        proj.ec_numbers = proj.enzyme_hits _
            .Select(Function(hits) hits.AssignECNumber()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToDictionary(Function(a) a.gene_id)

        proj.SaveZip("G:\BlueprintCAD\demo\ECOLI.gcproj")

        Pause()
    End Function
End Module
