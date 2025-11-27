Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Net.Tcp

Module AppEnvironment

    Dim http As Process

    Public ReadOnly Property WebRoot As String
        Get
            Dim dev As String = $"{App.HOME}/../../pages/index.html"
            Dim prod As String = $"{App.HOME}/resources/web/index.html"

            If prod.FileExists Then
                Return prod.ParentPath
            Else
                Return dev.ParentPath
            End If
        End Get
    End Property

    Public ReadOnly Property WebPort As Integer

    Public Sub Init()
        _WebPort = TCPExtensions.GetFirstAvailablePort(-1)
        http = Process.Start(New ProcessStartInfo With {
            .CreateNoWindow = True,
            .FileName = $"{App.HOME}/rstudio/bin/Rserve.exe",
            .Arguments = $"--listen /wwwroot {WebRoot.CLIPath} --parent {App.PID} /port {WebPort}"
        })
    End Sub

End Module
