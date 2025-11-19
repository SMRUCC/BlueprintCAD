Public Structure IDDisplay

    Dim id As String
    Dim name As String

    Public Overrides Function ToString() As String
        Return If(name, id)
    End Function

End Structure
