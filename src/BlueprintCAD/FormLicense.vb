Imports System.Text

Public Class FormLicense

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub FormLicense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Rtf = Encoding.UTF8.GetString(My.Resources.HelpDocs.License)
    End Sub
End Class