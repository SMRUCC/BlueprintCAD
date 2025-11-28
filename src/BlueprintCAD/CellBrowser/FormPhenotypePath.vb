Imports BlueprintCAD.UIData
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
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
        Dim offset As Integer

        If g Is Nothing Then
            g = TaskProgress.LoadData(streamLoad:=Function(bar As ITaskProgress)
                                                      Return CreateNetwork(network, symbols, bar)
                                                  End Function,
                                      title:="Initialize Data",
                                      info:="Build cellular network graph...",
                                      canbeCancel:=True)
        End If

        Me.g = g
        Me.qgram = New QGramIndex(6)
        Me.vertex = g.vertex.ToArray
        Me.view = New NodeView(vertex.Length - 1) {}

        For Each v As Node In vertex
            view(offset) = New NodeView With {
                .id = v.label,
                .name = v.data.label,
                .type = v.data(NamesOf.REFLECTION_ID_MAPPING_NODETYPE),
                .compartment = v.data("location")
            }

            Call ListBox1.Items.Add(view(offset))
            Call ListBox2.Items.Add(view(offset))
            Call qgram.AddString(view(offset).name, offset)

            offset += 1
        Next

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
End Class