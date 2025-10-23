Namespace WebJSON

    Public Class Reaction
        Public Property name As String
        Public Property reaction As String
        Public Property left As Substrate()
        Public Property right As Substrate()
        Public Property law As LawData()
    End Class

    Public Class Substrate
        Public Property molecule_id As UInteger
        Public Property factor As Double
    End Class

    Public Class LawData
        Public Property params As Dictionary(Of String, String)
        Public Property lambda As String
        Public Property metabolite_id As String
    End Class
End Namespace