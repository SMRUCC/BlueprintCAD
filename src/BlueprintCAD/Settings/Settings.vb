Imports CADRegistry
Imports Microsoft.VisualBasic.Serialization.JSON

Public Class Settings

    Public Property ncbi_blast As String
    Public Property registry_server As String

    Shared ReadOnly defaultConfig As String = App.ProductProgramData & "/settings.json"

    Public Shared Function Load() As Settings
        Dim config As Settings = defaultConfig.LoadJsonFile(Of Settings)(throwEx:=False)

        If config Is Nothing Then
            config = New Settings With {.registry_server = RegistryUrl.defaultServer}
        End If
        If config.registry_server.StringEmpty(, True) Then
            config.registry_server = RegistryUrl.defaultServer
        End If

        Return config
    End Function

    Public Sub Save()
        Try
            Call Me.GetJson.SaveTo(defaultConfig)
        Catch ex As Exception
            Call App.LogException(ex)
        End Try
    End Sub

End Class
