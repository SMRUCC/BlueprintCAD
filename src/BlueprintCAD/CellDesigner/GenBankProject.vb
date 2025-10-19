Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices.Zip
Imports Microsoft.VisualBasic.MIME.application.json
Imports SMRUCC.genomics.ComponentModel.Annotation
Imports SMRUCC.genomics.Metagenomics
Imports SMRUCC.genomics.SequenceModel

Public Class GenBankProject

    Public Property taxonomy As Taxonomy
    Public Property nt As String
    Public Property genes As Dictionary(Of String, String)
    Public Property proteins As Dictionary(Of String, String)
    Public Property gene_table As GeneTable()

    Public Sub SaveZip(filepath As String)
        Using zip As New ZipStream(filepath)
            Call zip.WriteText(taxonomy.GetJson, "/source.json")
            Call zip.WriteText(nt, "/source.txt")
            Call FASTA.StreamWriter.WriteList(genes, zip.OpenFile("/genes.txt", access:=FileAccess.Write))
            Call FASTA.StreamWriter.WriteList(proteins, zip.OpenFile("/proteins.txt", access:=FileAccess.Write))
            Call zip.WriteText(gene_table.GetJson, "/genes.json")
        End Using
    End Sub
End Class
