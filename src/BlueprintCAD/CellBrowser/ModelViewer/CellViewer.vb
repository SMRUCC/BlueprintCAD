Imports BlueprintCAD.UIData
Imports Galaxy.Workbench
Imports Galaxy.Workbench.CommonDialogs
Imports Microsoft.VisualBasic.Data.visualize.Network
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualStudio.WinForms.Docking
Imports Microsoft.Web.WebView2.Core
Imports RibbonLib.Interop
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core
Imports SMRUCC.genomics.GCModeller.ModellingEngine.IO

Public Class CellViewer

    Dim cell As VirtualCell
    Dim filepath As String
    Dim explorer As CellExplorer
    Dim nodeId As String

    Shared ReadOnly inspector As New RibbonEventBinding(Ribbon.ButtonPathwayRouter)

    Dim network As Dictionary(Of String, FluxEdge)
    Dim symbols As Dictionary(Of String, CompoundInfo)
    Dim cache_graph As NetworkGraph

    Private Sub OpenRouter()
        If cell Is Nothing Then
            Return
        End If

        If network Is Nothing Then
            ' make cache
            Call ProgressSpinner.DoLoading(Sub() Call CacheCellNetwork(), host:=Me)
        End If

        Call InputDialog.Input(
            setConfig:=Sub(router)
                           Dim plotNodes As NodeView() = router.PlotNodes
                           Dim nodeId As String() = plotNodes _
                               .Select(Function(n) n.id) _
                               .ToArray

                           Call explorer.viewGraph(nodeId).Wait()
                       End Sub,
            config:=New FormPhenotypePath().LoadNetwork(network, symbols, {"WATER", "CO2"}, cache_graph))
    End Sub

    Private Sub CacheCellNetwork()
        symbols = New Dictionary(Of String, CompoundInfo)
        network = New Dictionary(Of String, FluxEdge)

        For Each meta In cell.metabolismStructure.compounds
            symbols(meta.ID) = New CompoundInfo With {
                .id = meta.ID,
                .name = meta.name,
                .db_xrefs = meta.db_xrefs
            }
        Next
        For Each gene In cell.genome.GetAllGenes
            symbols(gene.locus_tag) = New CompoundInfo With {
                .id = gene.locus_tag,
                .name = gene.product
            }
        Next

        For Each rxn In cell.metabolismStructure.reactions.AsEnumerable
            network(rxn.ID) = New FluxEdge With {
                .id = rxn.ID,
                .name = rxn.name,
                .left = rxn.substrate _
                    .Select(Function(a)
                                Return New VariableFactor With {
                                    .id = a.compound,
                                    .factor = a.factor,
                                    .compartment_id = a.compartment
                                }
                            End Function) _
                    .ToArray,
                .right = rxn.product _
                    .Select(Function(a)
                                Return New VariableFactor With {
                                    .id = a.compound,
                                    .factor = a.factor,
                                    .compartment_id = a.compartment
                                }
                            End Function) _
                    .ToArray
            }
        Next
    End Sub

    Private Async Sub CellViewer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await WebViewLoader.Init(WebView21)

        explorer = New CellExplorer
        explorer.Show(Workbench.AppHost.GetDockPanel, dockState:=DockState.DockLeft)

        Ribbon.GroupModelInspecter.ContextAvailable = ContextAvailability.Active
    End Sub

    Private Sub CellViewer_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Ribbon.GroupModelInspecter.ContextAvailable = ContextAvailability.Active
        inspector.evt = AddressOf OpenRouter
    End Sub

    Private Sub CellViewer_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Workbench.RestoreFormTitle()
        Ribbon.GroupModelInspecter.ContextAvailable = ContextAvailability.NotAvailable
        inspector.ClearHook()
    End Sub

    Private Sub CellViewer_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        If cell IsNot Nothing Then
            Workbench.SetFormActiveTitle($"Inspect: {cell.cellular_id}")
        End If
    End Sub

    Public Function LoadModel(file As String) As CellViewer
        filepath = file

        Call ProgressSpinner.DoLoading(Sub() cell = file.LoadXml(Of VirtualCell))
        Call explorer.LoadModel(cell, Me)
        Call CellViewer_Activated(Nothing, Nothing)

        Return Me
    End Function

    Public Sub ViewGraph(g As graphology.graph)
        Call WebView21.CoreWebView2.PostWebMessageAsJson(g.GetJson(simpleDict:=True))
    End Sub

    Private Sub WebView21_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles WebView21.CoreWebView2InitializationCompleted
        Call WebView21.CoreWebView2.Navigate($"http://localhost:{WebPort}/cell_graph.html")
    End Sub

    Private Sub WebView2Control_WebMessageReceived(sender As Object, e As CoreWebView2WebMessageReceivedEventArgs) Handles WebView21.WebMessageReceived
        Try
            ' 获取消息字符串
            Dim message As String = e.WebMessageAsJson

            If String.IsNullOrEmpty(message) Then
                Return
            End If

            Dim messageObject = message.LoadJSON(Of Dictionary(Of String, String))

            ' 检查消息类型
            If messageObject("type") = "nodeClicked" Then
                nodeId = messageObject.TryGetValue("nodeId")

                If nodeId Is Nothing Then
                    Return
                Else
                    Dim target = explorer.GetNodeName(nodeId)

                    Call explorer.ShowNode(nodeId)

                    If target.type Is Nothing Then
                        ToolStripStatusLabel3.Text = "<Nothing>"
                        nodeId = Nothing
                    Else
                        ToolStripStatusLabel3.Text = $"<{target.type}> {target.name}"
                    End If
                End If
            End If
        Catch ex As Exception
            ' 处理 JSON 解析或其他错误
            Call CommonRuntime.Warning($"处理Web消息时出错: {ex.Message}")
        End Try
    End Sub

    Private Async Sub ToolStripStatusLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel2.Click
        If Not nodeId Is Nothing Then
            Await explorer.viewGraph(nodeId)
        End If
    End Sub
End Class