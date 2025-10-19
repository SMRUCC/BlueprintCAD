Imports Microsoft.VisualBasic.ApplicationServices.Zip
Imports Microsoft.VisualBasic.MIME.application.json
Imports SMRUCC.genomics.Metagenomics

Public Class GenBankProject

    Public Property taxonomy As Taxonomy

    Public Sub SaveZip(filepath As String)
        Using zip As New ZipStream(filepath)
            Call zip.WriteText(taxonomy.GetJson, "/source.json")
        End Using
    End Sub
End Class
