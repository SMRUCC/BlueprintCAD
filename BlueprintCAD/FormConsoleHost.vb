Imports Microsoft.VisualBasic.Windows.Forms

Public Class FormConsoleHost

    Dim WithEvents console As New ConsoleControl
    Dim commandQueue As New Queue(Of (cmdl$, args$))

    Private Sub FormConsoleHost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controls.Add(console)
        console.Dock = DockStyle.Fill
        console.ReadOnly = True
        console.IsInputEnabled = False
    End Sub

    Public Sub Run(cmd As String, args As String)
        Call console.StartProcess(cmd, args)
    End Sub

    Private Sub console_ProcessExisted() Handles console.ProcessExisted
        If commandQueue.Count > 0 Then
            With commandQueue.Dequeue
                Call console.StartProcess(.cmdl, .args)
            End With
        End If
    End Sub

    Public Function NextCommand(cmd As String, args As String) As FormConsoleHost
        commandQueue.Enqueue((cmd, args))
        Return Me
    End Function
End Class