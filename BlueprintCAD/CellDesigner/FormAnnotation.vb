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
End Class