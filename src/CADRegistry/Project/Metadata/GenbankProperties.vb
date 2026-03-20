Imports CellBuilder

Public Class GenbankProperties

    Public Property organism_name As String = "unknown"
    Public Property size As Dictionary(Of String, String)
    Public Property genes As Integer
    Public Property proteins As Integer
    Public Property enzymes As Integer
    Public Property operons As Integer
    Public Property transcript_factors As Integer
    Public Property tfbs As Integer
    Public Property transporter As Integer

    Sub New()
    End Sub

    Sub New(proj As GenBankProject)
        If proj IsNot Nothing Then
            Dim annoSet As AnnotationSet = proj.annotations

            If Not proj.taxonomy Is Nothing Then
                organism_name = proj.taxonomy.scientificName
            End If

            genes = proj.gene_table.TryCount
            proteins = proj.proteins.TryCount
            operons = annoSet.operons.TryCount
            transcript_factors = annoSet.transcript_factors.TryCount
            enzymes = annoSet.ec_numbers.TryCount
            tfbs = annoSet.tfbs_hits.TryCount
            transporter = annoSet.transporter.TryCount
            size = proj.nt _
                .ToDictionary(Function(a) a.Key,
                              Function(a)
                                  Return StringFormats.Lanudry(a.Value)
                              End Function)
        End If
    End Sub

End Class
