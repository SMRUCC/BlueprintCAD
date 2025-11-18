Imports Galaxy.Data.TableSheet
Imports Galaxy.Workbench.CommonDialogs

Public Class FormCultureMediumLibrary

    Dim edit As String

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Call InputDialog.Input(Of FormInputName)(
            Sub(input)
                ToolStripComboBox1.Items.Add(input.GetTextData)
                edit = input.GetTextData

            End Sub)
    End Sub

    Private Sub FormCultureMediumLibrary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ApplyVsTheme(ToolStrip1, ToolStrip2)
    End Sub
End Class