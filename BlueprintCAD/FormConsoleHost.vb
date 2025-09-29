Imports Microsoft.VisualBasic.Windows.Forms

Public Class FormConsoleHost

    Dim WithEvents console As New ConsoleControl

    Private Sub FormConsoleHost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controls.Add(console)
        console.Dock = DockStyle.Fill
        console.ReadOnly = True
    End Sub

    Public Sub Run(cmd As String, args As String)
        Call console.StartProcess(cmd, args)
    End Sub
End Class