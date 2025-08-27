Imports System.IO
Imports ggplot
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.Framework
Imports Microsoft.VisualBasic.DataStorage.HDSPack
Imports Microsoft.VisualBasic.DataStorage.HDSPack.FileSystem
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core
Imports SMRUCC.genomics.GCModeller.ModellingEngine.IO

Public Class CellBrowser

    Dim vcellPack As Raw.Reader
    Dim network As Dictionary(Of String, FluxEdge)
    Dim timePoints As Double()
    Dim moleculeSet As Dictionary(Of String, String())
    Dim moleculeLines As New Dictionary(Of String, Double())

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
                    End Sub)
            End If
        End Using
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
            index(block.fileName.BaseName) = dataRoot.ReadText(block).LoadJSON(Of FluxEdge)
        Next

        Call Me.Invoke(Sub() LoadUI(index.Select(Function(a) New NamedValue(Of FluxEdge)(a.Key, a.Value))))

        Return index
    End Function

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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row = DataGridView1.SelectedRows(0)
        Dim link = CStr(row.Cells(0).Value)
        Dim edge As FluxEdge = row.Tag
        Dim time As New List(Of Double)
        Dim expression As New List(Of Double)
        Dim names As New List(Of String)

        For Each id As String In edge.FactorIds.Distinct
            Call time.AddRange(timePoints)
            Call expression.AddRange(moleculeLines(id))
            Call names.AddRange(id.Repeats(timePoints.Length))
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
            padding:="padding: 5% 10% 10% 10%;")

        plot += geom_point(size:=6)

        PlotView1.ScaleFactor = 1.25
        PlotView1.PlotPadding = plot.ggplotTheme.padding
        PlotView1.ggplot = plot
    End Sub
End Class