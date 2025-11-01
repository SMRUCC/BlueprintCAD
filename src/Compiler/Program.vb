Imports CADRegistry
Imports Microsoft.VisualBasic.CommandLine
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Module Program

    Public Function Main(args As String()) As Integer
        Return GetType(Program).RunCLI(App.CommandLine, executeFile:=AddressOf CompileProjectFile)
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
End Module
