Imports CellBuilder
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.MIME.application.json.Javascript
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model

Public Class RegistryUrl

    ReadOnly server As String
    ReadOnly cache As New DataRepository

    Public Const defaultServer As String = "http://biocad.innovation.ac.cn"

    Sub New(Optional server As String = defaultServer, Optional cache_dir As String = Nothing)
        Me.server = Strings.Trim(server).TrimEnd("/"c)

        If Not cache_dir.StringEmpty() Then
            cache = New DataRepository(cache_dir)
        End If
    End Sub

    Public Function GetAllKnownOperons() As WebJSON.Operon()
        If Not cache.HasOperonDataCache Then
            Dim url As String = $"{server}/registry/known_operons/"
            Dim json_str As String = url.GET
            Dim json As JsonObject = JsonParser.Parse(json_str)
            Dim code As Integer = DirectCast(json!code, JsonValue)

            If code <> 0 Then
                Return Nothing
            Else
                Return json!info.CreateObject(Of WebJSON.Operon())
            End If
        Else
            Return cache.GetAllKnownOperons
        End If
    End Function

    Public Function Ping() As Boolean
        Dim url As String = $"{server}/ping/"
        Dim test As String = url.GET(timeoutSec:=1, echo:=False)
        Return Not test Is Nothing
    End Function

    Public Function GetMoleculeDataById(id As UInteger) As WebJSON.Molecule
        If Not cache.HasMoleculeDataCache Then
            Dim url As String = $"{server}/registry/molecule/?id={id}"
            Dim key As String = $"+{id}"

            Static cache As New Dictionary(Of String, WebJSON.Molecule)

            Return cache.ComputeIfAbsent(key, Function() GetMoleculeData(url))
        Else
            Return cache.GetMoleculeDataByID(id)
        End If
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
        If Not cache.HasReactionDataCache Then
            Dim url As String = $"{server}/registry/reaction_network/?ec_number={ec_number}&simple={simple.ToString.ToLower}"
            Dim key As String = $"{ec_number}:{simple.ToString.ToLower}"

            Static cache As New Dictionary(Of String, Dictionary(Of String, WebJSON.Reaction))

            Return cache.ComputeIfAbsent(key, Function() GetReactionList(url))
        Else
            Return cache.GetAssociatedReactions(ec_number, simple)
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="registry_id">id of the molecule</param>
    ''' <returns></returns>
    Public Function ExpandNetworkByCompound(registry_id As String) As Dictionary(Of String, WebJSON.Reaction)
        If Not cache.HasExpansionNetworkDataCache Then
            Dim url As String = $"{server}/registry/expand_network/?cid={registry_id}"
            Dim key As String = registry_id

            Static cache As New Dictionary(Of String, Dictionary(Of String, WebJSON.Reaction))

            Return cache.ComputeIfAbsent(key, Function() GetReactionList(url))
        Else
            Return cache.ExpandNetworkByCompound(registry_id)
        End If
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
