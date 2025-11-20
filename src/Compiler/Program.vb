Imports CADRegistry
Imports Microsoft.VisualBasic.CommandLine
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Module Program

    Public Function Main(args As String()) As Integer
        Return GetType(Program).RunCLI(App.CommandLine,
                                       executeFile:=AddressOf RunCompiler,
                                       executeEmpty:=AddressOf ShowHelp)
    End Function

    Private Function ShowHelp() As Integer
        Call Console.WriteLine()
        Call Console.WriteLine("The GCModeller Virtual Cell Compiler")
        Call Console.WriteLine()
        Call Console.WriteLine("commandline usage:")
        Call Console.WriteLine("<target_file.gcproj/gbff> --out <model.xml/project.gcproj> [--server <database server/local cache dir>] [--name <virtual cell name>] [--config <settings.json>] [--num_threads 8] [--workdir tempdir]")
        Call Console.WriteLine("")
        Call Console.WriteLine("step 1: build annotation project from genbank file")
        Call Console.WriteLine("--------------------------------------------------")
        Call Console.WriteLine("target.gbff --config settings.json [--out annotation.gcproj] [--num_threads 8] [--workdir workspace_dir]")
        Call Console.WriteLine()
        Call Console.WriteLine("input file:      genbank database file of a specific bacterial genome, this genbank file should")
        Call Console.WriteLine("                 contains the features of gene, CDS, tRNA, rRNA and whole genomics sequence.")
        Call Console.WriteLine("  --config:      the config json file for settings the annotation workflow, includes ncbi local ")
        Call Console.WriteLine("                 blast+ location and the database location.")
        Call Console.WriteLine("  --out:         [optional] the output file path, this command create an annotation project file")
        Call Console.WriteLine("                 output that wait for the virtual cell model compile.")
        Call Console.WriteLine("  --num_threads: [optional] number of the threads that used for run the localblast+ alignment ")
        Call Console.WriteLine("                 search for run the annotation workflow, default use 8 CPU threads.")
        Call Console.WriteLine("  --workdir:     [optional] set the temp workdir for run the annotation workflow.")
        Call Console.WriteLine()
        Call Console.WriteLine("step2: compile virtual cell model from annotation project file")
        Call Console.WriteLine("--------------------------------------------------------------")
        Call Console.WriteLine("target.gcproj [--out model.xml] [--server ""http://biocad.innovation.ac.cn""] [--name XXXX]")
        Call Console.WriteLine()
        Call Console.WriteLine("input file:      the gcmodeller annotation project file that produced from the step 1. a target ")
        Call Console.WriteLine("                 virtual cell model will be linked and compiled based on the annotation result ")
        Call Console.WriteLine("                 of the cellular components in this annotation project file.")
        Call Console.WriteLine("  --out:         [optional] the output file path of the compiled virtual cell model file. default ")
        Call Console.WriteLine("                 is in the same folder location with the input file.")
        Call Console.WriteLine("  --server:      [optional] config the http web server name to the database server, default is ")
        Call Console.WriteLine("                 http://biocad.innovation.ac.cn. this parameter also could be a local filesystem ")
        Call Console.WriteLine("                 location that contains the local cahced data files of the database.")
        Call Console.WriteLine("  --name:        [optional] set the cell model identifier, example as use `ECOLI` simplify for the ")
        Call Console.WriteLine("                 genome name of `Escherichia coli`. default use the file name of the input project ")
        Call Console.WriteLine("                 file its basename as the cell model name. ")

        Return 0
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

        Call Banner.Print(App.StdOut)

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
        Dim settings As Settings = Settings.Load(args.Required("--config", "Missing the required configuration file, `--config` argument must be specificed!"))
        Dim outproj As String = args("--out")
        Dim n_threads = args("--num_threads") Or -1
        Dim workdir As String = args("--workdir")

        If n_threads > 0 Then
            settings.n_threads = n_threads
        End If
        If Not workdir.StringEmpty(, True) Then
            Call App.SetSystemTemp(workdir)
        End If

        Call BuildProject.CreateModelProject(proj, settings, outproj)

        Return 0
    End Function
End Module
