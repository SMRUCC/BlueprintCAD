Imports BlueprintCAD.UIData
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Data.GraphTheory
Imports Microsoft.VisualBasic.Data.GraphTheory.Analysis.Dijkstra
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream.Generic
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.Office.Excel.XLSX.Writer
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core
Imports SMRUCC.genomics.GCModeller.ModellingEngine.IO

Public Class FormPhenotypePath

    Dim g As NetworkGraph
    Dim qgram As QGramIndex
    Dim view As NodeView()

    Public Shared ReadOnly ignores As String() = {
        "BioCAD00001853702", ' H+
        "BioCAD00000244943", ' Water
        "BioCAD00018534519",' Fe2+
        "BioCAD00018439229",' CO
        "BioCAD00018439154",' CO2
        "BioCAD00018492418",' CCl4
        "BioCAD00018480327" ' O2
    }

    Public Function LoadNetwork(network As Dictionary(Of String, FluxEdge),
                                symbols As Dictionary(Of String, CompoundInfo),
                                ignores As Index(Of String),
                                ByRef g As NetworkGraph) As FormPhenotypePath
        If g Is Nothing Then
            g = TaskProgress.LoadData(streamLoad:=Function(bar As ITaskProgress)
                                                      Return CreateNetwork(network, symbols, ignores, bar)
                                                  End Function,
                                      title:="Initialize Data",
                                      info:="Build cellular network graph...",
                                      canbeCancel:=True)
        End If

        Me.g = g
        Me.view = g.vertex _
            .AsParallel _
            .Where(Function(v) v.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE) <> "BiologicalProcess") _
            .Select(Function(v)
                        Return New NodeView With {
                            .id = v.label,
                            .name = v.data.label,
                            .type = v.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE),
                            .compartment = v.data("location")
                        }
                    End Function) _
            .OrderBy(Function(a) a.name) _
            .ToArray

        Return Me
    End Function

    Private Shared Function CreateNetwork(network As Dictionary(Of String, FluxEdge),
                                          symbols As Dictionary(Of String, CompoundInfo),
                                          ignores As Index(Of String),
                                          bar As ITaskProgress) As NetworkGraph
        Dim g As New NetworkGraph
        Dim n As Integer = network.Values.Count
        Dim d As Integer = n / 80
        Dim tick As i32 = 0

        If d < 1 Then
            d = 1
        End If

        Call bar.SetProgressMode()

        For Each rxn As FluxEdge In network.Values
            Dim rxnNode As Microsoft.VisualBasic.Data.visualize.Network.Graph.Node = g.GetElementByID(rxn.id)

            If rxnNode Is Nothing Then
                g.CreateNode(rxn.id, New NodeData With {
                    .Properties = New Dictionary(Of String, String) From {
                        {NamesOf.REFLECTION_ID_MAPPING_NODETYPE, "BiologicalProcess"},
                        {"location", "?"}
                    },
                    .label = If(rxn.name, rxn.id)
                })
                rxnNode = g.GetElementByID(rxn.id)
            End If

            For Each left As VariableFactor In rxn.left
                Dim v As Microsoft.VisualBasic.Data.visualize.Network.Graph.Node = g.GetElementByID(left.id)

                If left.mass_id Like ignores Then
                    Continue For
                End If

                If v Is Nothing Then
                    Dim info As CompoundInfo = symbols.TryGetValue(left.mass_id)
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
                Dim u As Microsoft.VisualBasic.Data.visualize.Network.Graph.Node = g.GetElementByID(right.id)

                If right.mass_id Like ignores Then
                    Continue For
                End If

                If u Is Nothing Then
                    Dim info As CompoundInfo = symbols.TryGetValue(right.mass_id)
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
                Call bar.SetInfo($"Build cellular network graph... {If(rxn.name, rxn.id)}")
            End If
        Next

        Call bar.TaskFinish()

        Return g
    End Function

    Private Sub FormPhenotypePath_Load(sender As Object, e As EventArgs) Handles Me.Load
        qgram = New QGramIndex(6)

        Call ProgressSpinner.DoLoading(
            Sub()
                Dim i As Integer = 0

                For Each v As NodeView In view
                    Call ListBox1.Items.Add(v)
                    Call ListBox2.Items.Add(v)
                    ' 20251205
                    ' skip of some empty name for deal with the unexpected offset error
                    Call qgram.AddString(v.name, index:=i)

                    i += 1
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
            Call ListBox1.Items.Add(view(item.index))
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

    Dim pathway As Route

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If from Is Nothing OrElse target Is Nothing Then
            Call MessageBox.Show("Missing the from node to target node to find the phenotype pathway!",
                                 "Invalid Config",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning)
            Return
        End If

        Dim router As DijkstraRouter = DijkstraRouter.FromNetwork(g, undirected:=False)
        Dim u = router.GetLocation(from.id)
        Dim v = router.GetLocation(target.id)

        Call ProgressSpinner.DoLoading(Sub() pathway = router.CalculateMinCost(u, v))
        Call DataGridView1.Rows.Clear()

        If Not pathway Is Nothing Then
            For Each link As VertexEdge In pathway.AsEnumerable
                Dim edge As Edge = g.GetEdge(link.U, link.V)

                Call DataGridView1.Rows.Add(edge.U.data.label, edge.U.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE),
                                            edge.V.data.label, edge.V.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE),
                                            edge.data(NamesOf.REFLECTION_ID_MAPPING_INTERACTION_TYPE))
            Next

            If pathway.Count = 0 Then
                Label8.Text = "<No Pathway Route Result>"
            Else
                Label8.Text = $"Found a shortest pathway route with {pathway.Count} steps!"
            End If
        Else
            Label8.Text = "<No Pathway Route Result>"
        End If

        Call MessageBox.Show($"Search pathway route from {from} to {target} done!" &
                             vbCrLf &
                             vbCrLf &
                             Label8.Text, "Job Done", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Not pathway.AsEnumerable.Any Then
            Call MessageBox.Show("No available pathway router, please make the pathway search at first!",
                                 "No data",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning)
            Return
        End If

        Using file As New SaveFileDialog With {.Filter = "Excel Table(*.xlsx)|*.xlsx"}
            If file.ShowDialog = DialogResult.OK Then
                Dim wb As New Workbook("Pathway")
                Dim sheet = wb.CurrentWorksheet
                Dim nodes As New Dictionary(Of String, NodeView)
                Dim viewIndex = view.ToDictionary(Function(v) v.id)

                Call sheet.AddDataRow("fromNode", "toNode", "edgeType")

                For Each link As VertexEdge In pathway.AsEnumerable
                    Dim edge As Edge = g.GetEdge(link.U, link.V)

                    nodes(edge.U.label) = viewIndex(edge.U.label)
                    nodes(edge.V.label) = viewIndex(edge.V.label)

                    Call sheet.AddDataRow(edge.U.data.label,
                                          edge.V.data.label,
                                          edge.data(NamesOf.REFLECTION_ID_MAPPING_INTERACTION_TYPE))
                Next

                Call wb.AddWorksheet("Molecule Information")

                sheet = wb.CurrentWorksheet
                sheet.AddDataRow("ID", "Name", "Type", "Location")

                For Each vertex As NodeView In nodes.Values
                    Call sheet.AddDataRow(vertex.id,
                                          vertex.name,
                                          vertex.type,
                                          vertex.compartment)
                Next

                Call wb.SaveAs(file.FileName)
            End If
        End Using
    End Sub

    Public ReadOnly Property PlotNodes As NodeView()

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If Not pathway.AsEnumerable.Any Then
            Call MessageBox.Show("No available pathway router, please make the pathway search at first!",
                                 "No data",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning)
            Return
        End If

        ' view removes biological process nodes
        Dim viewIndex = view.ToDictionary(Function(v) v.id)
        Dim nodes As New Dictionary(Of String, NodeView)

        For Each link As VertexEdge In pathway.AsEnumerable
            Dim edge As Edge = g.GetEdge(link.U, link.V)

            If viewIndex.ContainsKey(edge.U.label) Then nodes(edge.U.label) = viewIndex(edge.U.label)
            If viewIndex.ContainsKey(edge.V.label) Then nodes(edge.V.label) = viewIndex(edge.V.label)
        Next

        Me._PlotNodes = nodes.Values.ToArray
        Me.DialogResult = DialogResult.OK
    End Sub
End Class