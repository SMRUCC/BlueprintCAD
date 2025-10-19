Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging
Imports Microsoft.VisualBasic.Serialization.JSON

Public Class FormEditor

    Private Sub GraphPad1_PrintMessage(msg As String, level As MSG_TYPES) Handles GraphPad1.PrintMessage
        ToolStripStatusLabel1.Text = $"[{level.Description}] " & msg
    End Sub

    Private Sub FormEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim nav As New FormNavigator

        Call nav.Show()

        GraphPad1.BackColor = Color.AliceBlue
        GraphPad1.Canvas = New Size(5000, 5000)
        GraphPad1.Hook(nav)
        GraphPad1.Rendering()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        App.Exit(0)
    End Sub

    Private Sub SaveModelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveModelToolStripMenuItem.Click
        Using file As New SaveFileDialog With {.Filter = "Graph Model(*.gjson)|*.gjson"}
            If file.ShowDialog = DialogResult.OK Then
                Call GraphPad1.GetProject.GetJson.SaveTo(file.FileName)
            End If
        End Using
    End Sub

    Private Sub LoadModelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadModelToolStripMenuItem.Click
        Using file As New OpenFileDialog With {.Filter = "Graph Model(*.gjson)|*.gjson"}
            If file.ShowDialog = DialogResult.OK Then
                Dim model As DesignProject = file.FileName.ReadAllText.LoadJSON(Of DesignProject)

                Call model.ApplyConfig(GraphPad1)
            End If
        End Using
    End Sub
End Class
