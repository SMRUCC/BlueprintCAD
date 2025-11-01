Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.MIME.application.json.Javascript

Public Class RegistryUrl

    ReadOnly server As String

    Public Const defaultServer As String = "http://biocad.innovation.ac.cn"

    Sub New(Optional server As String = defaultServer)
        Me.server = Strings.Trim(server).TrimEnd("/"c)
    End Sub

    Public Function GetAllKnownOperons() As WebJSON.Operon()
        Dim url As String = $"{server}/registry/known_operons/"
        Dim json_str As String = url.GET
        Dim json As JsonObject = JsonParser.Parse(json_str)
        Dim code As Integer = DirectCast(json!code, JsonValue)

        If code <> 0 Then
            Return Nothing
        Else
            Return json!info.CreateObject(Of WebJSON.Operon())
        End If
    End Function

    Public Function Ping() As Boolean
        Dim url As String = $"{server}/ping/"
        Dim test As String = url.GET(timeoutSec:=1, echo:=False)
        Return Not test Is Nothing
    End Function

    Public Function GetMoleculeDataById(id As UInteger) As WebJSON.Molecule
        Dim url As String = $"{server}/registry/molecule/?id={id}"
        Dim key As String = $"+{id}"

        Static cache As New Dictionary(Of String, WebJSON.Molecule)

        Return cache.ComputeIfAbsent(key, Function() GetMoleculeData(url))
    End Function

    Private Shared Function GetMoleculeData(url As String) As WebJSON.Molecule
        Dim json_str As String = url.GET(echo:=False)
        Dim json As JsonObject = JsonParser.Parse(json_str)
        Dim code As Integer = DirectCast(json!code, JsonValue)

        If code <> 0 Then
            Return Nothing
        Else
            Return json!info.CreateObject(Of WebJSON.Molecule)
        End If
    End Function

    Public Function GetAssociatedReactions(ec_number As String, Optional simple As Boolean = False) As Dictionary(Of String, WebJSON.Reaction)
        Dim url As String = $"{server}/registry/reaction_network/?ec_number={ec_number}&simple={simple.ToString.ToLower}"
        Dim key As String = $"{ec_number}:{simple.ToString.ToLower}"

        Static cache As New Dictionary(Of String, Dictionary(Of String, WebJSON.Reaction))

        Return cache.ComputeIfAbsent(key, Function() GetReactionList(url))
    End Function

    Private Shared Function GetReactionList(url As String) As Dictionary(Of String, WebJSON.Reaction)
        Dim json_str As String = url.GET(echo:=False)
        Dim json As JsonObject = JsonParser.Parse(json_str)

        If json Is Nothing Then
            Return Nothing
        End If

        Dim code As Integer = DirectCast(json!code, JsonValue)

        If code <> 0 Then
            Return Nothing
        Else
            Return json!info.CreateObject(Of Dictionary(Of String, WebJSON.Reaction))
        End If
    End Function

End Class
