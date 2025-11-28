Imports System.Windows.Controls
Imports BlueprintCAD.UIData
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Data.GraphTheory.Analysis.Dijkstra
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream.Generic
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Language
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core
Imports SMRUCC.genomics.GCModeller.ModellingEngine.IO

Public Class FormPhenotypePath

    Dim g As NetworkGraph
    Dim vertex As Node()
    Dim qgram As QGramIndex
    Dim view As NodeView()

    Public Function LoadNetwork(network As Dictionary(Of String, FluxEdge),
                                symbols As Dictionary(Of String, CompoundInfo),
                                ByRef g As NetworkGraph) As FormPhenotypePath
        If g Is Nothing Then
            g = TaskProgress.LoadData(streamLoad:=Function(bar As ITaskProgress)
                                                      Return CreateNetwork(network, symbols, bar)
                                                  End Function,
                                      title:="Initialize Data",
                                      info:="Build cellular network graph...",
                                      canbeCancel:=True)
        End If

        Me.g = g
        Me.vertex = g.vertex.ToArray
        Me.view = vertex.AsParallel _
            .Where(Function(v) v.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE) <> "BiologicalProcess") _
            .Select(Function(v)
                        Return New NodeView With {
                            .id = v.label,
                            .name = v.data.label,
                            .type = v.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE),
                            .compartment = v.data("location")
                        }
                    End Function) _
            .ToArray

        Return Me
    End Function

    Private Shared Function CreateNetwork(network As Dictionary(Of String, FluxEdge), symbols As Dictionary(Of String, CompoundInfo), bar As ITaskProgress) As NetworkGraph
        Dim g As New NetworkGraph
        Dim n As Integer = network.Values.Count
        Dim d As Integer = n / 80
        Dim tick As i32 = 0

        If d < 1 Then
            d = 1
        End If

        Call bar.SetProgressMode()

        For Each rxn As FluxEdge In network.Values
            Dim rxnNode As Node = g.GetElementByID(rxn.id)

            If rxnNode Is Nothing Then
                g.CreateNode(rxn.id, New NodeData With {
                    .Properties = New Dictionary(Of String, String) From {
                        {NamesOf.REFLECTION_ID_MAPPING_NODETYPE, "BiologicalProcess"},
                        {"location", "?"}
                    }
                })
                rxnNode = g.GetElementByID(rxn.id)
            End If

            For Each left As VariableFactor In rxn.left
                Dim v As Node = g.GetElementByID(left.id)

                If v Is Nothing Then
                    Dim info As CompoundInfo = symbols.TryGetValue(left.id)
                    Dim loc As String = If(left.compartment_id, "Unknown")

                    g.CreateNode(left.id, New NodeData With {
                        .Properties = New Dictionary(Of String, String) From {
                            {NamesOf.REFLECTION_ID_MAPPING_NODETYPE, "Molecule Entity"},
                            {"location", loc}
                        },
                        .label = If(info Is Nothing, left.id, info.name)
                    })
                    v = g.GetElementByID(left.id)
                End If

                If g.ExistEdge(rxnNode.label, v.label) OrElse g.ExistEdge(v.label, rxnNode.label) Then
                Else
                    g.CreateEdge(v, rxnNode, 1, New EdgeData With {
                        .Properties = New Dictionary(Of String, String) From {
                            {NamesOf.REFLECTION_ID_MAPPING_INTERACTION_TYPE, "Substrate"}
                        }
                    })
                End If
            Next
            For Each right As VariableFactor In rxn.right
                Dim u As Node = g.GetElementByID(right.id)

                If u Is Nothing Then
                    Dim info As CompoundInfo = symbols.TryGetValue(right.id)
                    Dim loc As String = If(right.compartment_id, "Unknown")

                    g.CreateNode(right.id, New NodeData With {
                        .Properties = New Dictionary(Of String, String) From {
                            {NamesOf.REFLECTION_ID_MAPPING_NODETYPE, "Molecule Entity"},
                            {"location", loc}
                        },
                        .label = If(info Is Nothing, right.id, info.name)
                    })
                    u = g.GetElementByID(right.id)
                End If

                If g.ExistEdge(rxnNode.label, u.label) OrElse g.ExistEdge(u.label, rxnNode.label) Then
                Else
                    g.CreateEdge(rxnNode, u, 1, New EdgeData With {
                        .Properties = New Dictionary(Of String, String) From {
                            {NamesOf.REFLECTION_ID_MAPPING_INTERACTION_TYPE, "Product"}
                        }
                    })
                End If
            Next

            If (++tick) Mod d = 0 Then
                Call bar.SetProgress(CInt(tick / n * 100))
                Call bar.SetInfo($"Build cellular network graph... {rxn.id}")
            End If
        Next

        Call bar.TaskFinish()

        Return g
    End Function

    Private Sub FormPhenotypePath_Load(sender As Object, e As EventArgs) Handles Me.Load
        qgram = New QGramIndex(6)

        Call ProgressSpinner.DoLoading(
            Sub()
                For Each v As NodeView In view
                    Call ListBox1.Items.Add(v)
                    Call ListBox2.Items.Add(v)
                    Call qgram.AddString(v.name)
                Next
            End Sub, host:=Me)
    End Sub

    Dim from As NodeView = Nothing
    Dim target As NodeView = Nothing

    Private Sub SetAsFromNodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetAsFromNodeToolStripMenuItem.Click
        If ListBox1.SelectedIndex < 0 Then
            Return
        End If

        from = ListBox1.SelectedItem
        Label3.Text = from.ToString
    End Sub

    Private Sub SetAsTargetNodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetAsTargetNodeToolStripMenuItem.Click
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        target = ListBox2.SelectedItem
        Label7.Text = target.ToString
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.StringEmpty Then
            Call ListBox1.Items.Clear()

            For Each item In view
                Call ListBox1.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text.StringEmpty Then
            Call ListBox2.Items.Clear()

            For Each item In view
                Call ListBox2.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text.StringEmpty Then
            Return
        End If

        Dim find = qgram.FindSimilar(TextBox1.Text).OrderByDescending(Function(a) a.similarity).Take(30).ToArray

        Call ListBox1.Items.Clear()

        For Each item In find
            Call ListBox2.Items.Add(view(item.index))
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text.StringEmpty Then
            Return
        End If

        Dim find = qgram.FindSimilar(TextBox2.Text).OrderByDescending(Function(a) a.similarity).Take(30).ToArray

        Call ListBox2.Items.Clear()

        For Each item In find
            Call ListBox2.Items.Add(view(item.index))
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If from Is Nothing OrElse target Is Nothing Then
            Call MessageBox.Show("Missing the from node to target node to find the phenotype pathway!",
                                 "Invalid Config",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning)
            Return
        End If

        Dim router As New DijkstraRouter(g, undirected:=False)
        Dim u = router.GetLocation(from.id)
        Dim v = router.GetLocation(target.id)
        Dim pathway As Route = router.CalculateMinCost(u, v)

        If Not pathway Is Nothing Then

        End If
    End Sub
End Class