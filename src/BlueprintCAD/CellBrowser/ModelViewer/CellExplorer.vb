Imports System.ComponentModel
Imports BlueprintCAD.UIData
Imports Galaxy.Data.JSON
Imports Galaxy.Data.JSON.Models
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
Imports Microsoft.VisualBasic.Data.visualize.Network
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Data.visualize.Network.Layouts
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualStudio.WinForms.Docking
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CellExplorer

    Dim model As VirtualCell
    Dim WithEvents viewer As JsonViewer
    Dim edges
    Dim links As New Dictionary(Of String, List(Of Reaction))
    Dim compounds As New Dictionary(Of String, Compound)
    Dim web As CellViewer

    Public Sub LoadModel(model As VirtualCell, web As CellViewer)
        Me.model = model
        Me.web = web

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

                compounds(meta.ID) = meta
                metabolites.Fields.Add(obj)
            Next
        End If

        For Each rxn As Reaction In model.metabolismStructure.reactions.AsEnumerable
            For Each cpd As CompoundFactor In rxn.substrate.JoinIterates(rxn.product)
                If Not links.ContainsKey(cpd.compound) Then
                    Call links.Add(cpd.compound, New List(Of Reaction))
                End If

                Call links(cpd.compound).Add(rxn)
            Next
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

        Panel1.Controls.Add(viewer)

        ApplyVsTheme(ToolStrip1, viewer.GetContextMenu)
    End Sub

    Private Sub viewer_FindAction(node As JsonViewerTreeNode, text As String) Handles viewer.FindAction
        Dim val As Object = DirectCast(node.Tag, JsonObject).Value

    End Sub

    Private Sub viewer_MenuAction(sender As ToolStripMenuItem, node As JsonObject) Handles viewer.MenuAction
        Dim compound As Compound = TryCast(node.Value, Compound)

        If compound IsNot Nothing Then
            Dim url = $"http://biocad.innovation.ac.cn/molecule/{compound.ID}/"
            Call Tools.OpenUrlWithDefaultBrowser(url)
        End If
    End Sub

    Private Sub viewer_Visit(node As JsonViewerTreeNode) Handles viewer.Visit
        Dim json As JsonObject = TryCast(node.Tag, JsonObject)

        If json Is Nothing Then
            Return
        End If

        If TypeOf json.Value Is Compound Then
            Call CommonRuntime.GetPropertyWindow.SetObject(New CompoundPropertyView(json.Value), NameOf(CompoundPropertyView.db_xrefs), NameOf(CompoundPropertyView.referenceID))
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
            Dim meta As Compound = DirectCast(json.Value, Compound)

            If links.ContainsKey(meta.ID) Then
                Dim links = Me.links(meta.ID)
                Dim sigma = Await Task.Run(Function() BuildGraph(links))

                Call web.ViewGraph(sigma)
            End If
        End If
    End Sub

    Private Function BuildGraph(links As IEnumerable(Of Reaction)) As graphology.graph
        Dim g As New NetworkGraph
        Dim class_metab As Microsoft.VisualBasic.Imaging.SolidBrush = Microsoft.VisualBasic.Imaging.Brushes.Red
        Dim class_rxn As Microsoft.VisualBasic.Imaging.SolidBrush = Microsoft.VisualBasic.Imaging.Brushes.Blue

        For Each rxn As Reaction In links
            If g.GetElementByID(rxn.ID) Is Nothing Then
                Call g.CreateNode(rxn.ID, New NodeData With {
                    .label = rxn.name,
                    .color = class_rxn
                })
            End If

            Dim rxnNode = g.GetElementByID(rxn.ID)

            For Each left As CompoundFactor In rxn.substrate
                If g.GetElementByID(left.compound) Is Nothing Then
                    Dim metadata = compounds.TryGetValue(left.compound)

                    Call g.CreateNode(left.compound, New NodeData With {
                        .label = If(metadata Is Nothing, left.compound, metadata.name),
                        .color = class_metab
                    })
                End If

                Call g.CreateEdge(g.GetElementByID(left.compound), rxnNode, left.factor)
            Next
            For Each right As CompoundFactor In rxn.product
                If g.GetElementByID(right.compound) Is Nothing Then
                    Dim metadata = compounds.TryGetValue(right.compound)

                    Call g.CreateNode(right.compound, New NodeData With {
                        .label = If(metadata Is Nothing, right.compound, metadata.name),
                        .color = class_metab
                    })
                End If

                Call g.CreateEdge(rxnNode, g.GetElementByID(right.compound), right.factor)
            Next
        Next

        Call g.ApplyAnalysis
        Call g.doRandomLayout
        Call g.UsingDegreeAsRadius(computeDegree:=False).ScaleRadius(New DoubleRange(5, 20))

        Return g.AsGraphology
    End Function
End Class