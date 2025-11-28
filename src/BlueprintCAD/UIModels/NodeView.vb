Namespace UIData

    Public Class NodeView

        Public Property id As String
        Public Property name As String
        Public Property type As String
        Public Property compartment As String

        Public Overrides Function ToString() As String
            Return $"[{type}] {name}@{compartment}"
        End Function

    End Class
End Namespace