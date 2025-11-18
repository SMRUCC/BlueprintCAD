Public Class FormInputName

    Public ReadOnly Property GetTextData As String
        Get
            Return Strings.Trim(TextBox1.Text)
        End Get
    End Property

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If GetTextData = "" Then
            MessageBox.Show("The required data text value input should not be empty!", "No data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        DialogResult = DialogResult.OK
    End Sub
End Class