Imports System.IO
Imports ggplot
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.Framework
Imports Microsoft.VisualBasic.Data.Framework.IO
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
    Dim fluxLines As New Dictionary(Of String, Double())
    Dim plotMatrix As New Dictionary(Of String, FeatureVector)

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
                        Call println("loading molecule list ui... [metabolite tree]")
                        Call Me.Invoke(Sub() LoadTree(println))
                        Call println("loading molecule list ui... [metabolite matrix]")
                        Call LoadMatrix()
                        Call println("loading molecule list ui... [metabolite star links]")
                        Call Me.Invoke(Sub() LoadNodeStar())
                        Call println("load flux dynamics data into memory...")
                        Call LoadFluxData()
                    End Sub)
            End If
        End Using
    End Sub

    Private Sub LoadFluxData()
        For Each fluxSet In moleculeSet.Where(Function(a) a.Key.EndsWith("-Flux"))
            For Each compart_id As String In vcellPack.comparts
                For Each fluxId As String In fluxSet.Value
                    fluxId = fluxId & "@" & compart_id

                    If moleculeLines.ContainsKey(fluxId) Then
                        fluxLines(fluxId) = moleculeLines(fluxId)
                        moleculeLines.Remove(fluxId)
                    End If
                Next
            Next
        Next
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

    Private Sub LoadTree(println As Action(Of String))
        For Each molSet In moleculeSet.Where(Function(a) Not a.Key.EndsWith("-Flux"))
            Dim root = TreeView1.Nodes.Add(molSet.Key)

            For Each id As String In molSet.Value
                Call root.Nodes.Add(id)
            Next

            Call Application.DoEvents()
            Call println($"loading molecule list ui... [metabolite tree -> {molSet.Key}]")
        Next
    End Sub

    ''' <summary>
    ''' metabolite + flux
    ''' </summary>
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
            Call Application.DoEvents()
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

            Call Application.DoEvents()
        Next

        Call println("Loading flux data into table UI...")
        Call Me.Invoke(Sub() LoadUI(index.Select(Function(a) New NamedValue(Of FluxEdge)(a.Key, a.Value))))

        Return index
    End Function

    Private Sub ResetNetworkUI()
        If network Is Nothing Then
            Return
        End If

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
            Try
                PlotView1.ScaleFactor = 1.25
                PlotView1.PlotPadding = plot.ggplotTheme.padding
                PlotView1.ggplot = plot
            Catch ex As Exception

            End Try
        End If
    End Function

    Private Function CreatePlotMatrix(idset As IEnumerable(Of String)) As Dictionary(Of String, FeatureVector)
        Call plotMatrix.Clear()

        For Each id As String In idset
            If moleculeLines.ContainsKey(id) Then
                plotMatrix(id) = New FeatureVector(id, moleculeLines(id))
            End If
        Next

        Return plotMatrix
    End Function

    Private Async Function CreateGgplot(idset As IEnumerable(Of String)) As Task(Of ggplot.ggplot)
        Return Await CreatePlot(CreatePlotMatrix(idset))
    End Function

    Private Async Function CreatePlot(matrix As Dictionary(Of String, FeatureVector)) As Task(Of ggplot.ggplot)
        Dim cols As New List(Of (name$, expr As Double()))

        If matrix.IsNullOrEmpty Then
            Return Nothing
        End If

        Call CheckedListBox1.Items.Clear()

        For Each col As FeatureVector In matrix.Values
            Call cols.Add((col.name, col.TryCast(Of Double)))
            Call CheckedListBox1.Items.Add(col.name)
            Call CheckedListBox1.SetItemChecked(CheckedListBox1.Items.Count - 1, True)
        Next

        Call DataGridView2.Rows.Clear()
        Call DataGridView2.Columns.Clear()
        Call DataGridView2.Columns.Add("Time", "Time")

        For Each col In cols
            Call DataGridView2.Columns.Add(col.name, col.name)
        Next

        For i As Integer = 0 To timePoints.Length - 1
            Dim v As Object() = New Object(cols.Count) {}

            v(0) = timePoints(i)

            For j As Integer = 0 To cols.Count - 1
                v(j + 1) = cols(j).expr(i)
            Next

            Call DataGridView2.Rows.Add(v)
        Next

        Call DataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit)

        Return Await CreateGgplot(matrix)
    End Function

    Private Async Function CreateGgplot(matrix As Dictionary(Of String, FeatureVector)) As Task(Of ggplot.ggplot)
        If matrix.Count = 0 Then
            Return Nothing
        End If

        Return Await Task.Run(
            Function()
                Dim time As New List(Of Double)
                Dim expression As New List(Of Double)
                Dim names As New List(Of String)

                For Each col As FeatureVector In matrix.Values
                    Call time.AddRange(timePoints)
                    Call expression.AddRange(col.TryCast(Of Double))
                    Call names.AddRange(col.name.Repeats(timePoints.Length))
                Next

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
                    padding:="padding: 5% 20% 10% 10%;")

                plot += geom_point(size:=6, color:="paper")

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

    Private Sub SplitContainer2_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer2.SplitterMoved

    End Sub

    Private Async Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        Await RefreshPlot()
    End Sub

    Private Async Function RefreshPlot() As Task
        Dim matrix As New Dictionary(Of String, FeatureVector)
        Dim id As String

        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemChecked(i) Then
                id = CheckedListBox1.Items(i).ToString
                matrix(id) = plotMatrix(id)
            End If
        Next

        Dim plot As ggplot.ggplot = Await CreateGgplot(matrix)

        If Not plot Is Nothing Then
            Try
                PlotView1.ScaleFactor = 1.25
                PlotView1.PlotPadding = plot.ggplotTheme.padding
                PlotView1.ggplot = plot
            Catch ex As Exception

            End Try
        End If
    End Function

    Private Async Sub CheckedListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseDoubleClick
        Await RefreshPlot()
    End Sub

    Private Async Sub ViewFluxDynamicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewFluxDynamicsToolStripMenuItem.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim id As String = CStr(DataGridView1.SelectedRows(0).Cells(0).Value)
        Dim data As New Dictionary(Of String, FeatureVector)

        For Each compart_id As String In vcellPack.comparts
            compart_id = id & "@" & compart_id

            If fluxLines.ContainsKey(compart_id) Then
                Call data.Add(compart_id, New FeatureVector(compart_id, fluxLines(compart_id)))
            End If
        Next

        Dim plot As ggplot.ggplot = Await CreatePlot(matrix:=data)

        If Not plot Is Nothing Then
            Try
                PlotView1.ScaleFactor = 1.25
                PlotView1.PlotPadding = plot.ggplotTheme.padding
                PlotView1.ggplot = plot
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub ExportPlotMatrixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportPlotMatrixToolStripMenuItem.Click
        Using file As New SaveFileDialog With {.Filter = "Excel Table(*.csv)|*.csv"}
            If file.ShowDialog = DialogResult.OK Then
                Using s As StreamWriter = file.FileName.OpenWriter
                    Dim row As New RowObject

                    For i As Integer = 0 To DataGridView2.Columns.Count - 1
                        Call row.Add(DataGridView2.Columns(i).HeaderText)
                    Next

                    Call s.WriteLine(row.AsLine)

                    For i As Integer = 0 To DataGridView2.Rows.Count - 1
                        Dim r = DataGridView2.Rows(i)

                        Call row.Clear()

                        For offset As Integer = 0 To DataGridView2.Columns.Count - 1
                            Call row.Add(CStr(r.Cells(offset).Value))
                        Next

                        Call s.WriteLine(row.AsLine)
                    Next
                End Using
            End If
        End Using
    End Sub

    Private Async Sub ViewMassActivityLoadsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewMassActivityLoadsToolStripMenuItem.Click
        Dim loads = vcellPack.ActivityLoads.ToArray
        Dim idset = Await Task.Run(Function() loads.Select(Function(ti) ti.Keys).IteratesALL.Distinct.ToArray)
        Dim matrix = idset _
            .ToDictionary(Function(id) id,
                          Function(id)
                              Return New FeatureVector(id, loads.Select(Function(ti) ti(id)))
                          End Function)

        Dim plot As ggplot.ggplot = Await CreatePlot(matrix:=matrix)

        If Not plot Is Nothing Then
            Try
                PlotView1.ScaleFactor = 1.25
                PlotView1.PlotPadding = plot.ggplotTheme.padding
                PlotView1.ggplot = plot
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class