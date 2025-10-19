Public Class RegistryUrl

    ReadOnly server As String

    Sub New(server As String)
        Me.server = Strings.Trim(server).TrimEnd("/"c)
    End Sub

End Class
