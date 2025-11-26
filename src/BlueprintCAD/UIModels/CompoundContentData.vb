Namespace UIData

    Public Class CompoundContentData

        Public Property id As String
        Public Property content As Double
        Public Property name As String

        Public Overrides Function ToString() As String
            Return $"{If(name, id)} ({content} g/L)"
        End Function

    End Class
End Namespace