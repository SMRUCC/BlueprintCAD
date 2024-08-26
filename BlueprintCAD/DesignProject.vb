Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports TabularEdge = Microsoft.VisualBasic.Data.visualize.Network.FileStream.NetworkEdge
Imports TabularNode = Microsoft.VisualBasic.Data.visualize.Network.FileStream.Node

Public Class DesignProject

    Public Property Canvas As Integer()
    Public Property ViewBox As Integer()
    Public Property BackColor As String
    Public Property id As String
    Public Property name As String

    Public Property nodes As TabularNode()
    Public Property links As TabularEdge()

    Public Sub ApplyConfig(pad As GraphPad)
        If Canvas.TryCount >= 2 Then
            pad.Canvas = New Size(Canvas(0), Canvas(1))
        Else
            pad.Canvas = New Size(5000, 5000)
        End If
        If ViewBox.TryCount >= 2 Then
            pad.ViewPosition = New Point(ViewBox(0), ViewBox(1))
        End If

        Call pad.SetGraphModel(GetGraphModel)
        Call pad.Rendering()
    End Sub

    Public Function GetGraphModel() As NetworkGraph
        Dim data As New NetworkTables(nodes, links)
        Dim g As NetworkGraph = data.CreateGraph

        g.id = id
        g.name = name

        Return g
    End Function

End Class
