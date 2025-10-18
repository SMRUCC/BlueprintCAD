Imports SMRUCC.genomics.Assembly.NCBI.GenBank

Public Class FormAnnotation

    Dim filepath As String
    Dim genbank As GBFF.File

    Public Function LoadModel(filepath As String) As FormAnnotation
        genbank = GBFF.File.Load(filepath)
        Return Me
    End Function
End Class