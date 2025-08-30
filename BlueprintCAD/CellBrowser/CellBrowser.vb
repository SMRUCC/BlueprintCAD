Imports System.IO
Imports ggplot
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.Framework
Imports Microsoft.VisualBasic.DataStorage.HDSPack
Imports Microsoft.VisualBasic.DataStorage.HDSPack.FileSystem
Imports Microsoft.VisualBasic.Drawing
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core
Imports SMRUCC.genomics.GCModeller.ModellingEngine.IO

Public Class CellBrowser

    Dim vcellPack As Raw.Reader
    Dim network As Dictionary(Of String, FluxEdge)
    Dim nodeLinks As Dictionary(Of String, FluxEdge())
    Dim timePoints As Double()
    Dim moleculeSet As Dictionary(Of String, String())
    Dim moleculeLines As New Dictionary(Of String, Double())

    Shared Sub New()
        Call SkiaDriver.Register()
    End Sub

    Private Sub OpenVirtualCellDataFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenVirtualCellDataFileToolStripMenuItem.Click
        Using file As New OpenFileDialog With {.Filter = "Virtual Cell Data Pack(*.vcellPack)|*.vcellPack"}
            If file.ShowDialog = DialogResult.OK Then
                If vcellPack IsNot Nothing Then
                    Call vcellPack.Dispose()
                End If

                vcellPack = New Raw.Reader(file.FileName.Open(FileMode.Open, doClear:=False, [readOnly]:=True))
                Text = $"VirtualCell Browser [{file.FileName}]"
                network = FormBuzyLoader.Loading(Function(println) LoadNetwork(println))
                timePoints = vcellPack.AllTimePoints.ToArray
                moleculeSet = vcellPack.GetMoleculeIdList

                Call FormBuzyLoader.Loading(
                    Sub(println)
                        Call println("loading molecule list ui...")
                        Call Me.Invoke(Sub() LoadTree())
                        Call Me.Invoke(Sub() LoadMatrix())
                        Call Me.Invoke(Sub() LoadNodeStar())
                    End Sub)
            End If
        End Using
    End Sub

    Private Sub LoadNodeStar()
        nodeLinks = network.Values _
            .Select(Function(n)
                        Return n.FactorIds.Select(Function(id) (id, n))
                    End Function) _
            .IteratesALL _
            .GroupBy(Function(n) n.id) _
            .ToDictionary(Function(n) n.Key,
                          Function(n)
                              Return n _
                                 .Select(Function(a) a.Item2) _
                                 .ToArray
                          End Function)
    End Sub

    Private Sub LoadTree()
        For Each molSet In moleculeSet.Where(Function(a) Not a.Key.EndsWith("-Flux"))
            Dim root = TreeView1.Nodes.Add(molSet.Key)

            For Each id As String In molSet.Value
                Call root.Nodes.Add(id)
            Next
        Next
    End Sub

    Private Sub LoadMatrix()
        Dim times As New List(Of (Double, Dictionary(Of String, Double)))

        For Each ti As Double In timePoints
            Dim data As New Dictionary(Of String, Double)

            For Each mod_id As String In moleculeSet.Keys
                Dim vec = vcellPack.Read(ti, mod_id)

                For Each item As KeyValuePair(Of String, Double) In vec
                    data(item.Key) = item.Value
                Next
            Next

            Call times.Add((ti, data))
        Next

        Dim mols As String() = times _
            .Select(Function(a) a.Item2.Keys) _
            .IteratesALL _
            .Distinct _
            .ToArray

        For Each id As String In mols
            Call moleculeLines.Add(id, times.Select(Function(ti) ti.Item2(id)).ToArray)
        Next
    End Sub

    Private Function LoadNetwork(println As Action(Of String)) As Dictionary(Of String, FluxEdge)
        Dim dataRoot As StreamPack = vcellPack.GetStream
        Dim dir As StreamGroup = dataRoot.OpenFolder("/cellular_graph/")
        Dim index As New Dictionary(Of String, FluxEdge)

        Call println("Loading network from the virtual cell data pack...")
        Call vcellPack.LoadIndex()

        For Each block As StreamBlock In dir.ListFiles(recursive:=False).OfType(Of StreamBlock)
            Dim key As String = block.fileName.BaseName

            index(key) = dataRoot.ReadText(block).LoadJSON(Of FluxEdge)
            index(key).id = key
        Next

        Call Me.Invoke(Sub() LoadUI(index.Select(Function(a) New NamedValue(Of FluxEdge)(a.Key, a.Value))))

        Return index
    End Function

    Private Sub ResetNetworkUI()
        Call LoadUI(network.Select(Function(a) New NamedValue(Of FluxEdge)(a.Key, a.Value)))
    End Sub

    Private Sub LoadUI(network As IEnumerable(Of NamedValue(Of FluxEdge)))
        Dim offset As Integer

        Call DataGridView1.Rows.Clear()

        For Each edge As NamedValue(Of FluxEdge) In network
            offset = DataGridView1.Rows.Add(edge.Name, edge.Value.ToString)
            DataGridView1.Rows(offset).Tag = edge.Value
        Next

        Call DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub CellBrowser_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If vcellPack IsNot Nothing Then
            Call vcellPack.Dispose()
        End If
    End Sub

    Private Async Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row = DataGridView1.SelectedRows(0)
        Dim link = CStr(row.Cells(0).Value)
        Dim edge As FluxEdge = row.Tag

        Await RefreshPlot(edge.FactorIds.Distinct)
    End Sub

    Private Async Function RefreshPlot(idset As IEnumerable(Of String)) As Task
        Dim plot As ggplot.ggplot = Await CreateGgplot(idset)

        If Not plot Is Nothing Then
            PlotView1.ScaleFactor = 1.25
            PlotView1.PlotPadding = plot.ggplotTheme.padding
            PlotView1.ggplot = plot
        End If
    End Function

    Private Async Function CreateGgplot(idset As IEnumerable(Of String)) As Task(Of ggplot.ggplot)
        Return Await Task.Run(
            Function()
                Dim time As New List(Of Double)
                Dim expression As New List(Of Double)
                Dim names As New List(Of String)

                For Each id As String In idset
                    If moleculeLines.ContainsKey(id) Then
                        Call time.AddRange(timePoints)
                        Call expression.AddRange(moleculeLines(id))
                        Call names.AddRange(id.Repeats(timePoints.Length))
                    End If
                Next

                If Not time.Any Then
                    Return Nothing
                End If

                ' loadd all compound data
                Dim lines As New DataFrame With {
                    .features = New Dictionary(Of String, FeatureVector) From {
                        {"time", New FeatureVector("time", time)},
                        {"expression", New FeatureVector("expression", expression)},
                        {"names", New FeatureVector("names", names)}
                    }
                }
                Dim plot As ggplot.ggplot = ggplotFunction.ggplot(
                    data:=lines,
                    mapping:=aes("time", "expression", color:="names"),
                    colorSet:="paper",
                    padding:="padding: 5% 10% 10% 10%;")

                plot += geom_point(size:=6)

                Return plot
            End Function)
    End Function

    Private Async Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim node = e.Node

        If node Is Nothing Then
            Return
        End If

        Dim node_id As String() = vcellPack.comparts _
            .SafeQuery _
            .Select(Function(cid) node.Text & "@" & cid) _
            .ToArray

        Await RefreshPlot(node_id)
    End Sub

    Private Sub ResetNetworkTableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetNetworkTableToolStripMenuItem.Click
        FormBuzyLoader.Loading(Sub(println) Me.Invoke(Sub() ResetNetworkUI()))
    End Sub

    Private Sub FilterNetworkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FilterNetworkToolStripMenuItem.Click
        If TreeView1.SelectedNode Is Nothing Then
            Return
        End If

        Dim node As TreeNode = TreeView1.SelectedNode
        Dim node_id As String() = vcellPack.comparts _
            .SafeQuery _
            .Select(Function(cid) node.Text & "@" & cid) _
            .ToArray
        Dim edges As FluxEdge() = node_id.Select(Function(id) nodeLinks.TryGetValue(id)).IteratesALL.ToArray

        FormBuzyLoader.Loading(
            Sub(println)
                Call Me.Invoke(Sub() Call LoadUI(edges.Select(Function(a) New NamedValue(Of FluxEdge)(a.id, a))))
            End Sub)
    End Sub
End Class