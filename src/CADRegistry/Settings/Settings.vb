Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Serialization.JSON

Public Class Settings

    Public Property ncbi_blast As String
    Public Property registry_server As String
    Public Property cache_dir As String
    Public Property n_threads As Integer = 16
    ''' <summary>
    ''' the directory of the ncbi local blast database files
    ''' </summary>
    ''' <returns></returns>
    Public Property blastdb As String

    Shared ReadOnly defaultConfig As String = App.ProductProgramData & "/settings.json"

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Shared Function Load() As Settings
        Return Load(defaultConfig)
    End Function

    Public Shared Function Load(configfile As String) As Settings
        Dim config As Settings = configfile.LoadJsonFile(Of Settings)(throwEx:=False)

        If config Is Nothing Then
            config = New Settings With {.registry_server = RegistryUrl.defaultServer}
        End If
        If config.registry_server.StringEmpty(, True) Then
            config.registry_server = RegistryUrl.defaultServer
        End If
        If config.blastdb.StringEmpty(, True) Then
            config.blastdb = $"{App.HOME}/data/blastdb/"
        End If

        Return config
    End Function

    Public Sub Save()
        Call Save(defaultConfig)
    End Sub

    Public Function Save(file As String) As Boolean
        Try
            Call Me.GetJson.SaveTo(file)
        Catch ex As Exception
            Call App.LogException(ex)
            Return False
        End Try

        Return True
    End Function

End Class
