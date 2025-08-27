Imports System.IO
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.DataStorage.HDSPack
Imports Microsoft.VisualBasic.DataStorage.HDSPack.FileSystem
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core
Imports SMRUCC.genomics.GCModeller.ModellingEngine.IO

Public Class CellBrowser

    Dim vcellPack As Raw.Reader
    Dim network As Dictionary(Of String, FluxEdge)

    Private Sub OpenVirtualCellDataFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenVirtualCellDataFileToolStripMenuItem.Click
        Using file As New OpenFileDialog With {.Filter = "Virtual Cell Data Pack(*.vcellPack)|*.vcellPack"}
            If file.ShowDialog = DialogResult.OK Then
                If vcellPack IsNot Nothing Then
                    Call vcellPack.Dispose()
                End If

                vcellPack = New Raw.Reader(file.FileName.Open(FileMode.Open, doClear:=False, [readOnly]:=True))
                Text = $"VirtualCell Browser [{file.FileName}]"
                network = FormBuzyLoader.Loading(Function(println) LoadNetwork(println))
            End If
        End Using
    End Sub

    Private Function LoadNetwork(println As Action(Of String)) As Dictionary(Of String, FluxEdge)
        Dim dataRoot As StreamPack = vcellPack.GetStream
        Dim dir As StreamGroup = dataRoot.OpenFolder("/cellular_graph/")
        Dim index As New Dictionary(Of String, FluxEdge)

        Call println("Loading network from the virtual cell data pack...")

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
End Class