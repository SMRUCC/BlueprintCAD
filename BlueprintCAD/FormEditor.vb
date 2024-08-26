Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging

Public Class FormEditor

    Private Sub GraphPad1_PrintMessage(msg As String, level As MSG_TYPES) Handles GraphPad1.PrintMessage
        ToolStripStatusLabel1.Text = $"[{level.Description}] " & msg
    End Sub

    Private Sub FormEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim nav As New FormNavigator

        Call nav.Show()

        GraphPad1.BackColor = Color.LightBlue
        GraphPad1.Canvas = New Size(5000, 5000)
        GraphPad1.Hook(nav)
    End Sub
End Class
