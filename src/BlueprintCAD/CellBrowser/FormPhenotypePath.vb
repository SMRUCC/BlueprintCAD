Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Language
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core

Public Class FormPhenotypePath

    Dim g As NetworkGraph
    Dim vertex As Node()
    Dim qgram As QGramIndex

    Public Function LoadNetwork(network As Dictionary(Of String, FluxEdge), ByRef g As NetworkGraph) As FormPhenotypePath
        Dim offset As Integer

        If g Is Nothing Then
            g = TaskProgress.LoadData(streamLoad:=Function(bar As ITaskProgress)
                                                      Return CreateNetwork(network, bar)
                                                  End Function,
                                      title:="Initialize Data",
                                      info:="Build cellular network graph...",
                                      canbeCancel:=True)
        End If

        Me.g = g
        Me.qgram = New QGramIndex(6)
        Me.vertex = g.vertex.ToArray

        For Each v As Node In vertex
            Call ListBox1.Items.Add(v.label)
            Call ListBox2.Items.Add(v.label)
            Call qgram.AddString(v.label, offset)

            offset += 1
        Next

        Return Me
    End Function

    Private Shared Function CreateNetwork(network As Dictionary(Of String, FluxEdge), bar As ITaskProgress) As NetworkGraph
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
                g.CreateNode(rxn.id)
                rxnNode = g.GetElementByID(rxn.id)
            End If

            For Each left As VariableFactor In rxn.left
                Dim v As Node = g.GetElementByID(left.id)

                If v Is Nothing Then
                    g.CreateNode(left.id)
                    v = g.GetElementByID(left.id)
                End If

                If g.ExistEdge(rxnNode.label, v.label) OrElse g.ExistEdge(v.label, rxnNode.label) Then
                Else
                    g.CreateEdge(v, rxnNode)
                End If
            Next
            For Each right As VariableFactor In rxn.right
                Dim u As Node = g.GetElementByID(right.id)

                If u Is Nothing Then
                    g.CreateNode(right.id)
                    u = g.GetElementByID(right.id)
                End If

                If g.ExistEdge(rxnNode.label, u.label) OrElse g.ExistEdge(u.label, rxnNode.label) Then
                Else
                    g.CreateEdge(rxnNode, u)
                End If
            Next

            If (++tick) Mod d = 0 Then
                Call bar.SetProgress(CInt(tick / n * 100), $"Build cellular network graph... {rxn.id}")
            End If
        Next

        Return g
    End Function
End Class