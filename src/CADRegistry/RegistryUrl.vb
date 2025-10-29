Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.MIME.application.json.Javascript

Public Class RegistryUrl

    ReadOnly server As String

    Public Const defaultServer As String = "http://biocad.innovation.ac.cn"

    Sub New(server As String)
        Me.server = Strings.Trim(server).TrimEnd("/"c)
    End Sub

    Public Function GetMoleculeDataById(id As UInteger) As WebJSON.Molecule
        Dim url As String = $"{server}/registry/molecule/?id={id}"
        Dim json_str As String = url.GET
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
        Dim json_str As String = url.GET
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
