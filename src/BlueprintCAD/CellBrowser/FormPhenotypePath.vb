Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core

Public Class FormPhenotypePath

    Dim g As NetworkGraph
    Dim vertex As Node()
    Dim qgram As QGramIndex

    Public Function LoadNetwork(network As Dictionary(Of String, FluxEdge)) As FormPhenotypePath
        g = New NetworkGraph

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
        Next

        Dim offset As Integer

        qgram = New QGramIndex(6)
        vertex = g.vertex.ToArray

        For Each v As Node In vertex
            Call ListBox1.Items.Add(v.label)
            Call ListBox2.Items.Add(v.label)
            Call qgram.AddString(v.label, offset)

            offset += 1
        Next

        Return Me
    End Function
End Class