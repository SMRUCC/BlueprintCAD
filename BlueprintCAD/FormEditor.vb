Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging

Public Class FormEditor

    Private Sub GraphPad1_PrintMessage(msg As String, level As MSG_TYPES) Handles GraphPad1.PrintMessage
        ToolStripStatusLabel1.Text = $"[{level.Description}] " & msg
    End Sub
End Class
