Imports System.IO
Imports ggplot
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.Framework
Imports Microsoft.VisualBasic.Data.Framework.IO
Imports Microsoft.VisualBasic.Drawing
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.Runtime
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.Analysis.HTS.DataFrame
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core
Imports SMRUCC.genomics.GCModeller.ModellingEngine.IO
Imports std = System.Math

Public Class CellBrowser

    Dim vcellPack As VCellMatrixReader
    Dim network As Dictionary(Of String, FluxEdge)
    Dim nodeLinks As Dictionary(Of String, FluxEdge())
    Dim timePoints As Double()
    Dim moleculeSet As (compartment_id As String, modules As NamedCollection(Of String)())()
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

                vcellPack = New VCellMatrixReader(file.FileName.Open(FileMode.Open, doClear:=False, [readOnly]:=True))
                Text = $"VirtualCell Browser [{file.FileName}]"
                network = FormBuzyLoader.Loading(Function(println) LoadNetwork(println))
                timePoints = Enumerable.Range(0, vcellPack.totalPoints).AsDouble
                moleculeSet = vcellPack.GetCellularMolecules.ToArray

                Call FormBuzyLoader.Loading(
                    Sub(println)
                        Call println("loading molecule list ui... [metabolite tree]")
                        Call Me.Invoke(Sub() LoadTree(println))
                        Call println("loading molecule list ui... [metabolite matrix]")
                        Call println("loading molecule list ui... [metabolite star links]")
                        Call Me.Invoke(Sub() LoadNodeStar())
                        Call println("load flux dynamics data into memory...")
                    End Sub)

                Call ToolStripComboBox1.Items.Clear()
                Call ToolStripComboBox1.Items.Add("*")

                For Each item In vcellPack.fluxSet
                    Call ToolStripComboBox1.Items.Add(item)
                Next
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

    ''' <summary>
    ''' load the molecule file tree
    ''' </summary>
    ''' <param name="println"></param>
    Private Sub LoadTree(println As Action(Of String))
        For Each molSet In vcellPack.ReadMoleculeTree
            Dim root = TreeView1.Nodes.Add(molSet.Key)

            For Each id As String In molSet.Value
                Call root.Nodes.Add(id)
            Next

            Call Application.DoEvents()
            Call println($"loading molecule list ui... [metabolite tree -> {molSet.Key}]")
        Next
    End Sub

    Private Function LoadNetwork(println As Action(Of String)) As Dictionary(Of String, FluxEdge)
        Dim index As Dictionary(Of String, FluxEdge) = vcellPack.network

        Call println("Loading network from the virtual cell data pack.")
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
            Dim flux As FluxEdge = edge.Value
            Dim forward As VariableFactor() = flux.regulation.Where(Function(m) m.factor > 0).ToArray
            Dim reverse As VariableFactor() = flux.regulation.Where(Function(m) m.factor < 0).ToArray

            offset = DataGridView1.Rows.Add(flux, forward.JoinBy("; "), reverse.JoinBy("; "))
            DataGridView1.Rows(offset).Tag = flux
            DataGridView1.Rows(offset).HeaderCell.Value = flux.id
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
        Dim edge As FluxEdge = row.Tag

        TextBox1.Text = edge.GetJson(indent:=True)

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
                Call Message(ex.Message)
            End Try
        End If
    End Function

    Private Function CreatePlotMatrix(idset As IEnumerable(Of String)) As Dictionary(Of String, FeatureVector)
        Call plotMatrix.Clear()

        For Each id As String In idset.Where(Function(mol) vcellPack.CheckSymbol(mol))
            Dim vec As Double() = vcellPack.GetExpression(id)
            Dim col As New FeatureVector(id, vec)

            plotMatrix(id) = col
        Next

        Return plotMatrix
    End Function

    Private Async Function CreateGgplot(idset As IEnumerable(Of String)) As Task(Of ggplot.ggplot)
        Return Await CreatePlot(CreatePlotMatrix(idset))
    End Function

    ''' <summary>
    ''' Create ggplot chart and render dataframe into data grid table view
    ''' </summary>
    ''' <param name="matrix"></param>
    ''' <returns></returns>
    Private Async Function CreatePlot(matrix As Dictionary(Of String, FeatureVector)) As Task(Of ggplot.ggplot)
        Dim cols As New List(Of (name$, expr As Double()))

        If matrix.IsNullOrEmpty Then
            Return Nothing
        Else
            plotMatrix = matrix
        End If

        Call CheckedListBox1.Items.Clear()

        For Each col As FeatureVector In matrix.Values
            Call cols.Add((col.name, col.TryCast(Of Double)))
            Call CheckedListBox1.Items.Add(col.name)
            Call CheckedListBox1.SetItemChecked(CheckedListBox1.Items.Count - 1, True)
        Next

        Call DataGridView2.Rows.Clear()
        Call DataGridView2.Columns.Clear()

        For Each col In cols
            Call DataGridView2.Columns.Add(col.name, col.name)
        Next

        For i As Integer = 0 To timePoints.Length - 1
            Dim v As Object() = New Object(cols.Count - 1) {}

            For j As Integer = 0 To cols.Count - 1
                v(j) = cols(j).expr(i).ToString("G4")
            Next

            Dim offset As Integer = DataGridView2.Rows.Add(v)

            DataGridView2.Rows(offset).HeaderCell.Value = timePoints(i)
        Next

        Call DataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit)

        Return Await CreateGgplot(matrix)
    End Function

    Private Async Function CreateGgplot(matrix As Dictionary(Of String, FeatureVector)) As Task(Of ggplot.ggplot)
        Dim logscale As Boolean = ExpressionValueLogScaleToolStripMenuItem.Checked

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

                Dim exprVec As FeatureVector

                If logscale Then
                    exprVec = New FeatureVector("expression", expression.Select(Function(xi) std.Log(xi + 1)))
                Else
                    exprVec = New FeatureVector("expression", expression)
                End If

                ' loadd all compound data
                Dim lines As New DataFrame With {
                    .features = New Dictionary(Of String, FeatureVector) From {
                        {"time", New FeatureVector("time", time)},
                        {"expression", exprVec},
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

        If node Is Nothing OrElse node.Nodes.Count > 0 Then
            Return
        End If

        Dim node_id As String() = vcellPack.compartmentIds _
            .Select(Function(cid) node.Text & "@" & cid) _
            .ToArray

        Await RefreshPlot(node_id)
    End Sub

    Private Sub ResetNetworkTableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetNetworkTableToolStripMenuItem.Click
        FormBuzyLoader.Loading(Sub(println) Me.Invoke(Sub() ResetNetworkUI()))
    End Sub

    Private Sub CopyNameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyNameToolStripMenuItem.Click
        If TreeView1.SelectedNode Is Nothing Then
            Return
        End If

        Dim node As TreeNode = TreeView1.SelectedNode
        Dim node_id As String = node.Text

        Call Clipboard.SetText(node_id)
    End Sub

    Private Sub FilterSubstrateNetworkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FilterSubstrateNetworkToolStripMenuItem.Click
        If TreeView1.SelectedNode Is Nothing Then
            Return
        End If

        Dim node As TreeNode = TreeView1.SelectedNode
        Dim node_id As String() = vcellPack.compartmentIds _
            .Select(Function(cid) node.Text & "@" & cid) _
            .ToArray
        Dim idset As Index(Of String) = node_id
        Dim edges As FluxEdge() = node_id.Select(Function(id) nodeLinks.TryGetValue(id)).IteratesALL.Where(Function(f) f.left.Any(Function(v) idset(v.id) > -1)).ToArray

        FormBuzyLoader.Loading(
            Sub(println)
                Call Me.Invoke(Sub() Call LoadUI(edges.Select(Function(a) New NamedValue(Of FluxEdge)(a.id, a))))
            End Sub)
    End Sub

    Private Sub FilterProductNetworkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FilterProductNetworkToolStripMenuItem.Click
        If TreeView1.SelectedNode Is Nothing Then
            Return
        End If

        Dim node As TreeNode = TreeView1.SelectedNode
        Dim node_id As String() = vcellPack.compartmentIds _
            .Select(Function(cid) node.Text & "@" & cid) _
            .ToArray
        Dim idset As Index(Of String) = node_id
        Dim edges As FluxEdge() = node_id.Select(Function(id) nodeLinks.TryGetValue(id)).IteratesALL.Where(Function(f) f.right.Any(Function(v) idset(v.id) > -1)).ToArray

        FormBuzyLoader.Loading(
            Sub(println)
                Call Me.Invoke(Sub() Call LoadUI(edges.Select(Function(a) New NamedValue(Of FluxEdge)(a.id, a))))
            End Sub)
    End Sub

    Private Sub FilterNetworkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FilterNetworkToolStripMenuItem.Click
        If TreeView1.SelectedNode Is Nothing Then
            Return
        End If

        Dim node As TreeNode = TreeView1.SelectedNode
        Dim node_id As String() = vcellPack.compartmentIds _
            .Select(Function(cid) node.Text & "@" & cid) _
            .ToArray
        Dim edges As FluxEdge() = node_id _
            .Select(Function(id) nodeLinks.TryGetValue(id)) _
            .IteratesALL _
            .ToArray

        FormBuzyLoader.Loading(
            Sub(println)
                Call Me.Invoke(Sub() Call LoadUI(edges.Select(Function(a) New NamedValue(Of FluxEdge)(a.id, a))))
            End Sub)
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Dim item As Object = ToolStripComboBox1.SelectedItem

        If TypeOf item Is String AndAlso CStr(item) = "*" Then
            ' display all
            network = FormBuzyLoader.Loading(Function(println) LoadNetwork(println))
        Else
            ' display a specific module
            Dim fluxIdSet As String() = CType(item, NamedCollection(Of String))
            Dim edges As FluxEdge() = fluxIdSet _
               .Select(Function(id) network.TryGetValue(id)) _
               .Where(Function(f) Not f Is Nothing) _
               .ToArray

            FormBuzyLoader.Loading(
                Sub(println)
                    Call Me.Invoke(Sub() Call LoadUI(edges.Select(Function(a) New NamedValue(Of FluxEdge)(a.id, a))))
                End Sub)
        End If
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
                Call Message(ex.Message)
            End Try
        End If
    End Function

    Private Async Sub CheckedListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseDoubleClick
        Await RefreshPlot()
    End Sub

    Private Sub ExportMoleculeExpressionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportMoleculeExpressionToolStripMenuItem.Click
        Using file As New SaveFileDialog With {.Filter = "GCModeller Matrix(*.mat)|*.mat|Excel Table(*.csv)|*.csv"}
            If file.ShowDialog = DialogResult.OK Then
                Dim matrix As New Matrix With {
                    .tag = "Molecule Expression",
                    .sampleID = timePoints _
                        .AsCharacter _
                        .ToArray
                }

                Call FormBuzyLoader.Loading(
                    Sub(println)
                        Call println("Loading the molecule expression data...")

                        matrix.expression = vcellPack _
                            .GetCellularMolecules _
                            .Select(Function(n) n.Item2) _
                            .IteratesALL _
                            .Select(Function(m) m.AsEnumerable) _
                            .IteratesALL _
                            .Distinct _
                            .Select(Function(id)
                                        If vcellPack.CheckSymbol(id) Then
                                            Return New DataFrameRow With {
                                                .geneID = id,
                                                .experiments = vcellPack.GetExpression(id)
                                            }
                                        Else
                                            Call println($"Missing molecule epxression data of {id}")
                                            Return Nothing
                                        End If
                                    End Function) _
                            .ToArray
                    End Sub)
                Call FormBuzyLoader.Loading(
                    Sub(println)
                        Call println("Save molecule expression data matrix...")

                        If file.FileName.ExtensionSuffix("csv") Then
                            Call matrix.SaveMatrix(file.FileName, "molecule ID")
                        Else
                            Using s As Stream = file.OpenFile
                                Call matrix.Save(s)
                            End Using
                        End If
                    End Sub)
            End If
        End Using
    End Sub

    Private Sub ExportFluxomicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportFluxomicsToolStripMenuItem.Click
        Using file As New SaveFileDialog With {.Filter = "GCModeller Matrix(*.mat)|*.mat|Excel Table(*.csv)|*.csv"}
            If file.ShowDialog = DialogResult.OK Then
                Dim matrix As New Matrix With {
                    .tag = "Fluxomics",
                    .sampleID = timePoints _
                        .AsCharacter _
                        .ToArray
                }

                Call FormBuzyLoader.Loading(
                    Sub(println)
                        Call println("Loading flux data...")

                        matrix.expression = network.Keys _
                            .Select(Function(flux_id)
                                        If vcellPack.FluxExpressionExists(flux_id) Then
                                            Return New DataFrameRow With {
                                                .geneID = flux_id,
                                                .experiments = vcellPack.GetFluxExpression(flux_id)
                                            }
                                        Else
                                            Call println($"missing flux expression data of {flux_id}!")
                                            Return Nothing
                                        End If
                                    End Function) _
                            .Where(Function(f) f IsNot Nothing) _
                            .ToArray
                    End Sub)
                Call FormBuzyLoader.Loading(
                    Sub(println)
                        Call println("Save flux data matrix...")

                        If file.FileName.ExtensionSuffix("csv") Then
                            Call matrix.SaveMatrix(file.FileName, "flux ID")
                        Else
                            Using s As Stream = file.OpenFile
                                Call matrix.Save(s)
                            End Using
                        End If
                    End Sub)
            End If
        End Using
    End Sub

    Private Async Sub ViewFluxDynamicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewFluxDynamicsToolStripMenuItem.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim id As String = CStr(DataGridView1.SelectedRows(0).Cells(0).Value)
        Dim vec As Double() = vcellPack.GetFluxExpression(id)

        If vec Is Nothing Then
            Return
        End If

        Dim data As New Dictionary(Of String, FeatureVector) From {
            {id, New FeatureVector(id, vec)}
        }

        Dim plot As ggplot.ggplot = Await CreatePlot(matrix:=data)

        If Not plot Is Nothing Then
            Try
                PlotView1.ScaleFactor = 1.25
                PlotView1.PlotPadding = plot.ggplotTheme.padding
                PlotView1.ggplot = plot
            Catch ex As Exception
                Call Message(ex.Message)
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

                    Call row.Add("#Time")

                    For i As Integer = 0 To DataGridView2.Columns.Count - 1
                        Call row.Add(DataGridView2.Columns(i).HeaderText)
                    Next

                    Call s.WriteLine(row.AsLine)

                    For i As Integer = 0 To DataGridView2.Rows.Count - 1
                        Dim r = DataGridView2.Rows(i)

                        Call row.Clear()
                        Call row.Add(r.HeaderCell.Value.ToString)

                        For offset As Integer = 0 To DataGridView2.Columns.Count - 1
                            Call row.Add(CStr(r.Cells(offset).Value))
                        Next

                        Call s.WriteLine(row.AsLine)
                    Next
                End Using
            End If
        End Using
    End Sub

    Public Sub Message(str As String)
        Call Me.Invoke(Sub() ToolStripStatusLabel1.Text = str)
    End Sub

    Private Async Sub ViewMassActivityLoadsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewMassActivityLoadsToolStripMenuItem.Click
        Dim loads = Await Task.Run(Function() vcellPack.ActivityLoads)
        Dim matrix = loads _
            .ToDictionary(Function(id) id.Key,
                          Function(id)
                              Return New FeatureVector(id.Key, id.Value)
                          End Function)

        Dim plot As ggplot.ggplot = Await CreatePlot(matrix:=matrix)

        If Not plot Is Nothing Then
            Try
                PlotView1.ScaleFactor = 1.25
                PlotView1.PlotPadding = plot.ggplotTheme.padding
                PlotView1.ggplot = plot
            Catch ex As Exception
                Call Message(ex.Message)
            End Try
        End If
    End Sub

    Private Sub CloseFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseFileToolStripMenuItem.Click
        If Not vcellPack Is Nothing Then
            Try
                Call vcellPack.Dispose()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub CheckAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckAllToolStripMenuItem.Click
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            Call CheckedListBox1.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub UnCheckAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnCheckAllToolStripMenuItem.Click
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            Call CheckedListBox1.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub ExpressionValueLogScaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpressionValueLogScaleToolStripMenuItem.Click

    End Sub

    Private Sub ToolStripComboBox1_Click(sender As Object, e As EventArgs) Handles ToolStripComboBox1.Click

    End Sub
End Class