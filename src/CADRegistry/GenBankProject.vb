Imports System.IO
Imports SMRUCC.genomics.ComponentModel.Annotation
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models
Imports SMRUCC.genomics.Metagenomics
Imports SMRUCC.genomics.SequenceModel

Public Class GenBankProject

    Public Property taxonomy As Taxonomy
    Public Property nt As String
    Public Property genes As Dictionary(Of String, String)
    Public Property proteins As Dictionary(Of String, String)
    Public Property tss_upstream As Dictionary(Of String, String)
    Public Property gene_table As GeneTable()
    Public Property enzyme_hits As HitCollection()
    Public Property ec_numbers As Dictionary(Of String, ECNumberAnnotation)

    Public Sub DumpProteinFasta(s As Stream)
        Call FASTA.StreamWriter.WriteList(proteins, s)
    End Sub

    Public Sub DumpGeneFasta(s As Stream)
        Call FASTA.StreamWriter.WriteList(genes, s)
    End Sub

    Public Sub DumpTSSUpstreamFasta(s As Stream)
        Call FASTA.StreamWriter.WriteList(tss_upstream, s)
    End Sub

End Class
