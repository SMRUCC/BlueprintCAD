Imports System.IO
Imports System.Runtime.CompilerServices
Imports SMRUCC.genomics.Analysis.SequenceTools.SequencePatterns
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

    ''' <summary>
    ''' ec number blastp hits
    ''' </summary>
    ''' <returns></returns>
    Public Property enzyme_hits As HitCollection()
    ''' <summary>
    ''' operon gene blastn hits
    ''' </summary>
    ''' <returns></returns>
    Public Property operon_hits As HitCollection()

    Public Property operons As AnnotatedOperon()
    Public Property ec_numbers As Dictionary(Of String, ECNumberAnnotation)
    Public Property tfbs_hits As MotifMatch()

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Sub DumpProteinFasta(s As Stream)
        Call FASTA.StreamWriter.WriteList(proteins, s)
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Sub DumpGeneFasta(s As Stream)
        Call FASTA.StreamWriter.WriteList(genes, s)
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Sub DumpTSSUpstreamFasta(s As Stream)
        Call FASTA.StreamWriter.WriteList(tss_upstream, s)
    End Sub

End Class
