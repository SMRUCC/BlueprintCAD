Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.application.json
Imports Microsoft.VisualBasic.MIME.application.json.Javascript
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model

Public Class RegistryUrl

    ReadOnly server As String

#Region "in-memory cache data"
    ReadOnly cachedOperon As WebJSON.Operon()
    ReadOnly cachedReactions As Dictionary(Of String, WebJSON.Reaction())
    ReadOnly cachedMolecules As Dictionary(Of String, WebJSON.Molecule)
    ReadOnly cachedExpansion As Dictionary(Of String, WebJSON.Reaction())
#End Region

    Public Const defaultServer As String = "http://biocad.innovation.ac.cn"

    Sub New(Optional server As String = defaultServer, Optional cache_dir As String = Nothing)
        Me.server = Strings.Trim(server).TrimEnd("/"c)

        If Not cache_dir.StringEmpty() Then
            Call "start to load local cahced database files".info

            Dim network = $"{cache_dir}/metabolic_network.jsonl".LoadJSONL(Of WebJSON.Reaction).ToArray

            cachedOperon = $"{cache_dir}/all_operons.json".LoadJsonFile(Of WebJSON.Operon())(throwEx:=False)
            cachedMolecules = $"{cache_dir}/molecules.jsonl".LoadJSONL(Of WebJSON.Molecule).ToDictionary(Function(m) m.id)
            cachedReactions = (From rxn In network Where Not rxn.law.IsNullOrEmpty) _
                .Select(Function(r) r.law.Select(Function(ec) (ec.ec_number, r))) _
                .IteratesALL _
                .GroupBy(Function(r) r.ec_number) _
                .ToDictionary(Function(r) r.Key,
                              Function(r)
                                  Return r.Select(Function(i) i.r).ToArray
                              End Function)
            cachedExpansion = (From rxn In network Where rxn.law.IsNullOrEmpty) _
                .Select(Function(r)
                            Return r.left.JoinIterates(r.right).Select(Function(c) (c.molecule_id, r))
                        End Function) _
                .IteratesALL _
                .GroupBy(Function(c) c.molecule_id) _
                .ToDictionary(Function(c) c.Key.ToString,
                              Function(c)
                                  Return c.Select(Function(i) i.r).GroupBy(Function(r) r.guid).Select(Function(r) r.First).ToArray
                              End Function)

            If cachedOperon Is Nothing Then cachedOperon = {}

            Call "load cached database from a given cache dir:".info
            Call $" * {cachedOperon.Length} known operons".info
            Call $" * {cachedReactions.Count} known enzyme reaction network".info
            Call $" * {cachedExpansion.Count} reaction network expansions".info
            Call $" * {cachedMolecules.Count} associated metabolites".info
        End If
    End Sub

    Public Function GetAllKnownOperons() As WebJSON.Operon()
        If cachedOperon Is Nothing Then
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
            ' just read cache data for local test
            ' not used cache dir for save web request data
            Return cachedOperon.ToArray
        End If
    End Function

    Public Function Ping() As Boolean
        Dim url As String = $"{server}/ping/"
        Dim test As String = url.GET(timeoutSec:=1, echo:=False)
        Return Not test Is Nothing
    End Function

    Public Function GetMoleculeDataById(id As UInteger) As WebJSON.Molecule
        If cachedMolecules Is Nothing Then
            Dim url As String = $"{server}/registry/molecule/?id={id}"
            Dim key As String = $"+{id}"

            Static cache As New Dictionary(Of String, WebJSON.Molecule)

            Return cache.ComputeIfAbsent(key, Function() GetMoleculeData(url))
        Else
            Return cachedMolecules.TryGetValue(id.ToString, [default]:=cachedMolecules.TryGetValue("BioCAD" & id.ToString.PadLeft(11, "0"c)))
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
        If cachedReactions Is Nothing Then
            Dim url As String = $"{server}/registry/reaction_network/?ec_number={ec_number}&simple={simple.ToString.ToLower}"
            Dim key As String = $"{ec_number}:{simple.ToString.ToLower}"

            Static cache As New Dictionary(Of String, Dictionary(Of String, WebJSON.Reaction))

            Return cache.ComputeIfAbsent(key, Function() GetReactionList(url))
        Else
            Dim list = cachedReactions.TryGetValue(ec_number)

            If Not list Is Nothing Then
                Return list.ToDictionary(Function(r) r.guid)
            Else
                Return Nothing
            End If
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="registry_id">id of the molecule</param>
    ''' <returns></returns>
    Public Function ExpandNetworkByCompound(registry_id As String) As Dictionary(Of String, WebJSON.Reaction)
        If cachedExpansion Is Nothing Then
            Dim url As String = $"{server}/registry/expand_network/?cid={registry_id}"
            Dim key As String = registry_id

            Static cache As New Dictionary(Of String, Dictionary(Of String, WebJSON.Reaction))

            Return cache.ComputeIfAbsent(key, Function() GetReactionList(url))
        Else
            Dim list = cachedExpansion.TryGetValue(registry_id)

            If Not list Is Nothing Then
                Return list.ToDictionary(Function(r) r.guid)
            Else
                Return Nothing
            End If
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
