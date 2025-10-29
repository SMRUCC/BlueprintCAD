Imports Microsoft.VisualBasic.CommandLine

Module Program

    Public Function Main(args As String()) As Integer
        Return GetType(Program).RunCLI(App.CommandLine, executeFile:=AddressOf CompileProjectFile)
    End Function

    Public Function CompileProjectFile(file As String, args As CommandLine) As Integer

    End Function
End Module
