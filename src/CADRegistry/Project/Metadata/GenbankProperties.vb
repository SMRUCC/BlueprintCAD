Public Class GenbankProperties

    Public Property organism_name As String = "unknown"
    Public Property size As String
    Public Property genes As Integer
    Public Property proteins As Integer
    Public Property enzymes As Integer
    Public Property operons As Integer
    Public Property transcript_factors As Integer
    Public Property tfbs As Integer

    Sub New()
    End Sub

    Sub New(proj As GenBankProject)
        If proj IsNot Nothing Then
            If Not proj.taxonomy Is Nothing Then
                organism_name = proj.taxonomy.scientificName
            End If

            size = StringFormats.Lanudry(proj.nt.Length)
            genes = proj.gene_table.TryCount
            proteins = proj.proteins.TryCount
            operons = proj.operons.TryCount
            transcript_factors = proj.transcript_factors.TryCount
            enzymes = proj.ec_numbers.TryCount
            tfbs = proj.tfbs_hits.TryCount
        End If
    End Sub

End Class
