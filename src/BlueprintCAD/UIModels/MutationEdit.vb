Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Namespace UIData

    Public Class MutationEdit

        Public Property gene As gene
        Public Property knockout As Boolean = False
        Public Property expression_level As Double = 1

        Public Overrides Function ToString() As String
            If knockout Then
                Return $"Knockout [{gene}]"
            ElseIf expression_level > 1 Then
                Return $"Overexpress [{gene}] = {expression_level}"
            Else
                Return $"Suppressexpress [{gene}] = {expression_level}"
            End If
        End Function

    End Class
End Namespace