Imports System.ComponentModel
Imports BlueprintCAD.UIData
Imports Galaxy.Data.JSON
Imports Galaxy.Data.JSON.Models
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
Imports Microsoft.VisualBasic.Data.visualize.Network
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Data.visualize.Network.Layouts
Imports Microsoft.VisualBasic.Imaging.SVG.PathHelper
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualStudio.WinForms.Docking
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CellExplorer

    Dim model As VirtualCell
    Dim WithEvents viewer As JsonViewer
    Dim compoundIndex As New List(Of Compound)
    Dim links As New Dictionary(Of String, List(Of Reaction))
    Dim compounds As New Dictionary(Of String, Compound)
    Dim rxnList As New Dictionary(Of String, Reaction)
    Dim web As CellViewer
    Dim qgram As QGramIndex

    Public Sub LoadModel(model As VirtualCell, web As CellViewer)
        Me.model = model
        Me.web = web
        Me.qgram = New QGramIndex(3)

        ProgressSpinner.DoLoading(Sub() Call LoadCellComponents(), host:=Me)
    End Sub

    Private Sub LoadCellComponents()
        Dim tree As New JsonObject With {.Id = model.cellular_id}
        Dim metabolites As New JsonObject With {
            .Id = "metabolites",
            .JsonType = JsonType.Array,
            .Parent = tree
        }

        If Not model.metabolismStructure?.compounds Is Nothing Then
            Dim offset As i32 = 1

            For Each meta As Compound In model.metabolismStructure.compounds
                Dim obj As New JsonObject With {
                    .Id = $"[{++offset}]",
                    .JsonType = JsonType.Value,
                    .Parent = metabolites,
                    .Value = meta
                }
                Dim index As Integer = CInt(offset) - 2

                metabolites.Fields.Add(obj)
                compounds(meta.ID) = meta
                compoundIndex.Add(meta)

                Call qgram.AddString(meta.ID, index:=index)
                Call qgram.AddString(meta.name, index:=index)
                Call qgram.AddString(meta.formula, index:=index)

                For Each id As String In meta.db_xrefs.SafeQuery
                    Call qgram.AddString(id, index:=index)
                Next
            Next
        End If

        For Each rxn As Reaction In model.metabolismStructure.reactions.AsEnumerable
            For Each cpd As CompoundFactor In rxn.substrate.JoinIterates(rxn.product)
                If Not links.ContainsKey(cpd.compound) Then
                    Call links.Add(cpd.compound, New List(Of Reaction))
                End If

                Call links(cpd.compound).Add(rxn)
            Next

            rxnList(rxn.ID) = rxn
        Next

        Call tree.Fields.Add(metabolites)

        viewer.Tag = model.cellular_id
        viewer.Render(New JsonObjectTree(tree))
    End Sub

    Private Sub CellExplorer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        DockState = DockState.DockLeftAutoHide
    End Sub

    Private Sub CellExplorer_Load(sender As Object, e As EventArgs) Handles Me.Load
        TabText = "Cell Explorer"

        viewer = New JsonViewer
        viewer.Dock = DockStyle.Fill
        viewer.AddContextMenuItem("View In Registry", "view_registry")
        viewer.AddContextMenuItem("Add Ignores", "add_ignores")

        Call Panel1.Controls.Add(viewer)

        ApplyVsTheme(ToolStrip1, viewer.GetContextMenu)
    End Sub

    Private Sub viewer_FindAction(node As JsonViewerTreeNode, text As String) Handles viewer.FindAction
        Dim val As Object = DirectCast(node.Tag, JsonObject).Value

    End Sub

    ''' <summary>
    ''' Click on the custom menu item of the json viewer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="node"></param>
    Private Sub viewer_MenuAction(sender As ToolStripMenuItem, node As JsonObject) Handles viewer.MenuAction
        Dim compound As Compound = TryCast(node.Value, Compound)

        If compound IsNot Nothing Then
            Select Case sender.Name
                Case "view_registry"
                    Call Tools.OpenUrlWithDefaultBrowser($"http://biocad.innovation.ac.cn/molecule/{compound.ID}/")
                Case "add_ignores"
                    If MessageBox.Show($"Make general compound ignores of this metabolite({compound}) used in network graph view and path finding?",
                                       "Make General Compound Ignores",
                                       MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Warning) = DialogResult.OK Then

                        Workbench.Settings.ignores = Workbench.Settings.ignores _
                            .JoinIterates({compound.ID}) _
                            .Where(Function(str)
                                       Return Not str.StringEmpty(, True)
                                   End Function) _
                            .Distinct _
                            .ToArray
                        Workbench.Settings.Save()
                    End If
            End Select
        End If
    End Sub

    Private Sub viewer_Visit(node As JsonViewerTreeNode) Handles viewer.Visit
        Dim json As JsonObject = TryCast(node.Tag, JsonObject)

        If json Is Nothing Then
            Return
        End If

        If TypeOf json.Value Is Compound Then
            Call ShowNode(DirectCast(json.Value, Compound).ID)
        End If
    End Sub

    Public Function GetNodeName(id As String) As (type$, name$)
        If compounds.ContainsKey(id) Then
            Return ("Metabolite", compounds(id).name)
        ElseIf rxnList.ContainsKey(id) Then
            Return ("Metabolic Reaction", rxnList(id).name)
        Else
            Return Nothing
        End If
    End Function

    Public Sub ShowNode(id As String)
        If compounds.ContainsKey(id) Then
            Call CommonRuntime.GetPropertyWindow.SetObject(New CompoundPropertyView(compounds(id)), NameOf(CompoundPropertyView.db_xrefs), NameOf(CompoundPropertyView.referenceID))
        ElseIf rxnList.ContainsKey(id) Then
            Call CommonRuntime.GetPropertyWindow.SetObject(New ReactionPropertyView(rxnList(id), compounds))
        End If
    End Sub

    ''' <summary>
    ''' view graph
    ''' </summary>
    ''' <param name="node"></param>
    Private Async Sub viewer_ViewAction(node As JsonViewerTreeNode) Handles viewer.ViewAction
        Dim json As JsonObject = TryCast(node.Tag, JsonObject)

        If json Is Nothing Then
            Return
        End If

        If TypeOf json.Value Is Compound Then
            Await viewGraph(DirectCast(json.Value, Compound).ID)
        End If
    End Sub

    Public Async Function viewGraph(metaID As String()) As Task
        Dim links As New List(Of Reaction)

        For Each id As String In metaID
            If Me.links.ContainsKey(id) Then
                Call links.AddRange(Me.links(id))
            End If
        Next

        Call web.ViewGraph(Await Task.Run(Function() BuildGraph(links)))
        Call web.ShowReactionTable(links)
    End Function

    Public Async Function viewGraph(metaID As String) As Task
        If links.ContainsKey(metaID) Then
            Dim links = Me.links(metaID)
            Dim sigma = Await Task.Run(Function() BuildGraph(links))

            Call web.ShowReactionTable(links)
            Call web.ViewGraph(sigma)
        End If
    End Function

    Private Function BuildGraph(links As IEnumerable(Of Reaction)) As graphology.graph
        Dim g As New NetworkGraph
        Dim class_metab As Microsoft.VisualBasic.Imaging.SolidBrush = Microsoft.VisualBasic.Imaging.Brushes.Red
        Dim class_rxn As Microsoft.VisualBasic.Imaging.SolidBrush = Microsoft.VisualBasic.Imaging.Brushes.Blue
        Dim line As New Microsoft.VisualBasic.Imaging.Pen(Color.LightGray, 3)
        Dim ignores As Index(Of String) = Workbench.Settings.ignores.Indexing

        For Each rxn As Reaction In links
            If g.GetElementByID(rxn.ID) Is Nothing Then
                Call g.CreateNode(rxn.ID, New NodeData With {
                    .label = rxn.name,
                    .color = class_rxn
                })
            End If

            Dim rxnNode = g.GetElementByID(rxn.ID)

            For Each left As CompoundFactor In rxn.substrate
                If left.compound Like ignores Then
                    Continue For
                End If

                If g.GetElementByID(left.compound) Is Nothing Then
                    Dim metadata = compounds.TryGetValue(left.compound)

                    Call g.CreateNode(left.compound, New NodeData With {
                        .label = If(metadata Is Nothing, left.compound, metadata.name),
                        .color = class_metab
                    })
                End If

                Dim v = g.GetElementByID(left.compound)

                If Not (g.ExistEdge(v.label, rxnNode.label) OrElse g.ExistEdge(rxnNode.label, v.label)) Then
                    Call g.CreateEdge(v, rxnNode, left.factor, New EdgeData With {.style = line})
                End If
            Next
            For Each right As CompoundFactor In rxn.product
                If right.compound Like ignores Then
                    Continue For
                End If

                If g.GetElementByID(right.compound) Is Nothing Then
                    Dim metadata = compounds.TryGetValue(right.compound)

                    Call g.CreateNode(right.compound, New NodeData With {
                        .label = If(metadata Is Nothing, right.compound, metadata.name),
                        .color = class_metab
                    })
                End If

                Dim u = g.GetElementByID(right.compound)

                If Not (g.ExistEdge(u.label, rxnNode.label) OrElse g.ExistEdge(rxnNode.label, u.label)) Then
                    Call g.CreateEdge(rxnNode, u, right.factor, New EdgeData With {.style = line})
                End If
            Next
        Next

        Call g.ApplyAnalysis
        Call g.doRandomLayout
        Call g.UsingDegreeAsRadius(computeDegree:=False).ScaleRadius(New DoubleRange(9, 20))

        Return g.AsGraphology
    End Function

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Call web.OpenRouter()
    End Sub

    ''' <summary>
    ''' Search name
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If ToolStripSpringTextBox1.Text.StringEmpty(, True) Then
            Return
        End If

        Dim find As Compound() = qgram _
            .FindSimilar(ToolStripSpringTextBox1.Text) _
            .OrderByDescending(Function(a) a.similarity) _
            .Select(Function(r) compoundIndex(r.index)) _
            .GroupBy(Function(a) a.ID) _
            .Select(Function(g) g.First) _
            .Take(30) _
            .ToArray

        Call CommonRuntime.ShowDocument(Of FormNameSearch)(
            status:=DockState.Document,
            title:=$"Search Metabolites of {ToolStripSpringTextBox1.Text}").SetData(web, find)
    End Sub
End Class