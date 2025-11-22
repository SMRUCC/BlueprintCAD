Imports Microsoft.VisualBasic.Windows.Forms

Public Class FormConsoleHost

    Dim WithEvents console As New ConsoleControl
    Dim commandQueue As New Queue(Of (cmdl$, args$))
    Dim final_cmd As Action

    Private Sub FormConsoleHost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controls.Add(console)
        console.Dock = DockStyle.Fill
        console.ReadOnly = True
        console.IsInputEnabled = False
        console.Font = New Font("Consolas", 10)
    End Sub

    Public Sub Run(cmd As String, args As String)
        Call console.StartProcess(cmd, args)
    End Sub

    Private Sub console_ProcessExisted() Handles console.ProcessExisted
        If commandQueue.Count > 0 Then
            With commandQueue.Dequeue
                Call console.StartProcess(.cmdl, .args)
            End With
        Else
            If final_cmd IsNot Nothing Then
                Call final_cmd()
            End If
        End If
    End Sub

    Public Sub Final(act As Action)
        final_cmd = act
    End Sub

    Public Sub LogText(text As String, color As Color)
        Call console.WriteOutput(text, color)
    End Sub

    Public Function NextCommand(cmd As String, args As String) As FormConsoleHost
        commandQueue.Enqueue((cmd, args))
        Return Me
    End Function
End Class