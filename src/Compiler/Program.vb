Imports System.ComponentModel
Imports CADRegistry
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Serialization.JSON
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
        Call Console.WriteLine("<target_file.gcproj/gbff> --out <model.xml/project.gcproj> [--server <database server/local cache dir>] [--skip-TRN] [--name <virtual cell name>] [--config <settings.json>] [--num_threads 8] [--workdir tempdir]")
        Call Console.WriteLine("")
        Call Console.WriteLine("step 1: build annotation project from genbank file")
        Call Console.WriteLine("--------------------------------------------------")
        Call Console.WriteLine("target.gbff --config settings.json [--out annotation.gcproj] [--skip-TRN] [--num_threads 8] [--workdir workspace_dir] [--enable-blast-cache]")
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
        Call Console.WriteLine("  --skip-TRN:    [optional] skip of scan the TFBS site for create transcript regulation network.")
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

    <Usage("./target.gcproj --out model.xml --server ""http://biocad.innovation.ac.cn"" --name XXXX")>
    Public Function CompileProjectFile(file As String, args As CommandLine) As Integer
        Call Banner.Print(App.StdOut)

        Dim name As String = args("--name") Or file.BaseName
        Dim proj As GenBankProject = ProjectIO.Load(file)
        Dim serverUrl As String = args("--server") Or RegistryUrl.defaultServer
        Dim savefile As String = args("--out") Or file.ChangeSuffix("xml")
        Dim compiler As New Compiler(proj, serverUrl, name)
        Dim model As VirtualCell = compiler.Compile(args)

        Return model _
            .GetXml _
            .SaveTo(savefile) _
            .CLICode
    End Function

    <Usage("./target.gbff --out gcmodeller.gcproj --config settings.json [--num_threads 8 --skip-TRN --workdir workspace_dir --enable-blast-cache]")>
    Public Function CompileGenbankFile(file As String, args As CommandLine) As Integer
        Dim proj = ProjectCreator.FromGenBank(GBFF.File.LoadDatabase(file))
        Dim settings As Settings = Settings.Load(args.Required("--config", "Missing the required configuration file, `--config` argument must be specificed!"))
        Dim outproj As String = args("--out")
        Dim n_threads = args("--num_threads") Or -1
        Dim workdir As String = args("--workdir")
        Dim skipTRN As Boolean = args("--skip-TRN")
        Dim enableBlastCache As Boolean = args("--enable-blast-cache")

        If n_threads > 0 Then
            settings.n_threads = n_threads
        End If
        If Not workdir.StringEmpty(, True) Then
            Call App.SetSystemTemp(workdir)
        End If

        Call BuildProject.CreateModelProject(
            proj, settings, skipTRN,
            outproj:=outproj,
            enableBlastCache:=enableBlastCache)

        Return 0
    End Function

    <ExportAPI("--batch-build")>
    <Description("compile the GCModeller virtual cell model file from annotation project file in batch mode")>
    <Usage("--batch-build --dir <project_dir> [--server ""http://biocad.innovation.ac.cn"" --outdir <output_models_dir>]")>
    Public Function BatchBuild(args As CommandLine) As Integer
        Dim dir As String = args.Required("--dir", "a directory path that contains the genbank assembly for the genome for make virtual cell model is required!")
        Dim serverUrl As String = args("--server") Or RegistryUrl.defaultServer
        Dim outdir As String = args("--outdir") Or dir & "/models"

        For Each projFile As String In dir.ListFiles("*.gcproj")
            Dim outModel As String = $"{outdir}/{projFile.BaseName}.xml"

            Try
                Call New Compiler(ProjectIO.Load(projFile), serverUrl) _
                    .Compile(args) _
                    .GetXml _
                    .SaveTo(outModel)
            Catch ex As Exception
                Call App.LogException(ex)
            End Try
        Next

        Return 0
    End Function

    <ExportAPI("--batch-make")>
    <Description("compile the GCModeller virtual cell model file from genbank assembly file in batch mode")>
    <Usage("--batch-make --dir <genbank_gbff_dir> --config <settings.json> [--num_threads <8> --skip-TRN --workdir <workspace_dir> --enable-blast-cache --server ""http://biocad.innovation.ac.cn"" --outdir <output_models_dir>]")>
    Public Function BatchMake(args As CommandLine) As Integer
        Dim dir As String = args.Required("--dir", "a directory path that contains the genbank assembly for the genome for make virtual cell model is required!")
        Dim config As String = args.Required("--config", "the configuration json file path for config the compiler is required!")
        Dim n_threads As Integer = args("--num_threads") Or -1
        Dim workdir As String = args("--workdir")
        Dim skipTRN As Boolean = args("--skip-TRN")
        Dim enableBlastCache As Boolean = args("--enable-blast-cache")
        Dim serverUrl As String = args("--server") Or RegistryUrl.defaultServer
        Dim outdir As String = args("--outdir") Or dir & "/models"
        Dim settings As Settings = Settings.Load(config)
        Dim gbffs As String() = dir.ListFiles("*.gbff").ToArray

        If n_threads > 0 Then
            settings.n_threads = n_threads
        End If
        If Not workdir.StringEmpty(, True) Then
            Call App.SetSystemTemp(workdir)
        End If

        For Each asm As String In gbffs
            Dim proj As GenBankProject
            Dim asm_name As String = asm.BaseName
            Dim outProj As String = $"{outdir}/{asm_name}.gcproj"
            Dim outModel As String = $"{outdir}/{asm_name}.xml"

            Try
                proj = ProjectCreator.FromGenBank(GBFF.File.LoadDatabase(asm))
            Catch ex As Exception
                Call App.LogException(ex)
                Continue For
            End Try

            Try
                Call BuildProject.CreateModelProject(
                    proj, settings, skipTRN,
                    outproj:=outProj,
                    enableBlastCache:=enableBlastCache)
            Catch ex As Exception
                Call App.LogException(ex)
            End Try

            If outProj.FileExists Then
                Try
                    Dim compiler As New Compiler(ProjectIO.Load(outProj), serverUrl)
                    Dim model As VirtualCell = compiler.Compile(args)

                    Call model.GetXml.SaveTo(outModel)
                Catch ex As Exception
                    Call App.LogException(ex)
                End Try
            End If
        Next

        Return 0
    End Function

    <ExportAPI("--config")>
    <Usage("--config --ini <settings.json> [--ncbi_blast <blastn_bin directory> 
                                            --server <server url> 
                                            --cache_dir <localdb cache_dir> 
                                            --n_threads <number_parallel_threads> 
                                            --blastdb <directory_of_fasta>]")>
    <Description("Make configuration settings.json file")>
    <Argument("--ini", False, CLITypes.File, Description:="The settings json file path.")>
    <Argument("--ncbi_blast", True, CLITypes.File, Description:="A directory path to the ncbi localblast bin directory, this directory path should contains the blastp/blastn program files.")>
    <Argument("--server", True, CLITypes.String, Description:="A url to the registry server, usually be the 'http://biocad.innovation.ac.cn'")>
    <Argument("--cache_dir", True, CLITypes.File, Description:="A local directory path that contains the registry database cached json data files.")>
    <Argument("--n_threads", True, CLITypes.Integer, Description:="Number of the parallel threads for run the localblast search and motif finding workflow.")>
    <Argument("--blastdb", True, CLITypes.File, Description:="A directory path to the localblast database fasta files.")>
    Public Function MakeConfig(args As CommandLine) As Integer
        Dim inifile As String = args.Required("--ini", "Missing the required configuration file, `--ini` argument must be specificed!")
        Dim settings As Settings = Settings.Load(inifile)
        Dim ncbi_blast As String = args("--ncbi_blast")
        Dim server_url As String = args("--server")
        Dim cache_dir As String = args("--cache_dir")
        Dim num_threads As Integer = args("--n_threads")
        Dim blastdb As String = args("--blastdb")

        If ncbi_blast <> "" Then settings.ncbi_blast = ncbi_blast
        If server_url <> "" Then settings.registry_server = server_url
        If cache_dir <> "" Then settings.cache_dir = cache_dir
        If blastdb <> "" Then settings.blastdb = blastdb
        If num_threads > 0 Then settings.n_threads = num_threads

        Call Console.WriteLine(settings.GetJson(indent:=True))

        Return settings.Save(inifile).CLICode
    End Function
End Module
