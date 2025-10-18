Imports SMRUCC.genomics.Assembly.NCBI.GenBank

Public Class FormAnnotation

    Dim filepath As String
    Dim genbank As GBFF.File

    Private Sub FormAnnotation_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        Workbench.SetFormActiveTitle(TabText)
    End Sub

    Private Sub FormAnnotation_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Workbench.RestoreFormTitle()
    End Sub

    Public Function LoadModel(filepath As String) As FormAnnotation
        genbank = GBFF.File.Load(filepath)
        Return Me
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Workbench.Settings.ncbi_blast = Strings.Trim(TextBox1.Text)
        Workbench.Settings.Save()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using dir As New FolderBrowserDialog With {.InitialDirectory = Workbench.Settings.ncbi_blast}
            If dir.ShowDialog = DialogResult.OK Then
                TextBox1.Text = dir.SelectedPath

                Workbench.Settings.ncbi_blast = dir.SelectedPath
                Workbench.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub FormAnnotation_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = Workbench.Settings.ncbi_blast
    End Sub
End Class