Imports CellBuilder

Public Module DataRegistry

    Public Function OpenRegistry(serverUrl As String) As IDataRegistry
        If serverUrl.ToLower.StartsWith("http://") OrElse serverUrl.ToLower.StartsWith("https://") Then
            Return New RegistryUrl(serverUrl)
        Else
            Return New RegistryUrl(RegistryUrl.defaultServer, serverUrl)
        End If
    End Function
End Module
