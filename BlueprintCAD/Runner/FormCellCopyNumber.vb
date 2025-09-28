Imports Galaxy.Workbench

Public Class FormCellCopyNumber : Implements IDataContainer

    Dim copyNum As New Dictionary(Of String, Integer)
    Dim model As String
    Dim wizardConfig As New Wizard

    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        wizardConfig = DirectCast(data, Wizard)

        For Each model As String In wizardConfig.models.Keys
            Call ListBox1.Items.Add(model)
            Call copyNum.Add(model, 1000)
        Next
    End Sub

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizardConfig
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        wizardConfig.config.copy_number = copyNum

        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex < 0 Then
            Return
        Else
            model = CStr(ListBox1.SelectedItem)
        End If

        NumericUpDown1.Value = copyNum(model)
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If model.StringEmpty() Then
            Return
        Else
            copyNum(model) = NumericUpDown1.Value
        End If
    End Sub
End Class