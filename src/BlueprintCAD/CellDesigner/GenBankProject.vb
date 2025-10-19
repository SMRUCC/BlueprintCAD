Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices.Zip
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application.json
Imports SMRUCC.genomics.ComponentModel.Annotation
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models
Imports SMRUCC.genomics.Metagenomics
Imports SMRUCC.genomics.SequenceModel

Public Class GenBankProject

    Public Property taxonomy As Taxonomy
    Public Property nt As String
    Public Property genes As Dictionary(Of String, String)
    Public Property proteins As Dictionary(Of String, String)
    Public Property gene_table As GeneTable()
    Public Property enzyme_hits As HitCollection()

    Public Sub DumpProteinFasta(s As Stream)
        Call FASTA.StreamWriter.WriteList(proteins, s)
    End Sub

    Public Sub SaveZip(filepath As String)
        Using zip As New ZipStream(filepath)
            Call zip.WriteText(taxonomy.GetJson, "/source.json")
            Call zip.WriteText(nt, "/source.txt")
            Call FASTA.StreamWriter.WriteList(genes, zip.OpenFile("/genes.txt", access:=FileAccess.Write))
            Call DumpProteinFasta(zip.OpenFile("/proteins.txt", access:=FileAccess.Write))
            Call zip.WriteLines(gene_table.SafeQuery.Select(Function(a) a.GetJson), "/genes.jsonl")
            Call zip.WriteLines(enzyme_hits.SafeQuery.Select(Function(q) q.GetJson), "/localblast/enzyme_hits.jsonl")
        End Using
    End Sub
End Class
