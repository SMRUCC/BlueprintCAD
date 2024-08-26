Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Data.visualize.Network.Layouts
Imports Microsoft.VisualBasic.Imaging

Public Class GraphPad

    Public Property Canvas As New Size(5000, 5000)
    Public Property ViewPosition As Point

    Dim g As New NetworkGraph
    Dim clickPos As Point
    Dim nav As FormNavigator

    Public Event PrintMessage(msg As String, level As MSG_TYPES)

    Public Sub SetGraphModel(g As NetworkGraph)
        Me.g = g
    End Sub

    Public Function GetProject() As DesignProject
        Dim tabular As NetworkTables = g.Tabular()

        Return New DesignProject With {
            .BackColor = PictureBox1.BackColor.ToHtmlColor,
            .Canvas = {Canvas.Width, Canvas.Height},
            .id = g.id,
            .name = g.name,
            .ViewBox = {ViewPosition.X, ViewPosition.Y},
            .nodes = tabular.nodes,
            .links = tabular.edges
        }
    End Function

    Public Sub Hook(nav As FormNavigator)
        Me.nav = nav
        Me.nav.Pad = Me
        Me.nav.SetView(ViewPosition)
        Me.nav.SetView(PictureBox1.Size)
    End Sub

    Private Sub AddNodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNodeToolStripMenuItem.Click
        Dim x, y As Integer

        Call GetCanvasXY(x, y)
        Call g.CreateNode(New NodeData() With {
            .initialPostion = New FDGVector2(x, y),
            .size = {10},
            .color = Brushes.Red
        })
        Rendering()
    End Sub

    Private Sub GetCanvasXY(<Out> ByRef x As Integer, <Out> ByRef y As Integer)
        ' needs to translate to the canvas position
        x = clickPos.X + ViewPosition.X
        y = clickPos.Y + ViewPosition.Y
    End Sub

    Private Function RenderView() As Image
        Dim sz = PictureBox1.Size

        If sz.Width <= 0 OrElse sz.Height <= 0 Then
            Return New Bitmap(1, 1)
        End If

        Using view As Graphics2D = PictureBox1.Size.CreateGDIDevice(BackColor)
            ' find all nodes insdie current view
            Dim rect As New Rectangle(ViewPosition, PictureBox1.Size)
            Dim nodeInBox = g.vertex _
                .AsParallel _
                .Where(Function(v)
                           Return v.data.CheckInside(rect)
                       End Function) _
                .ToArray
            Dim pos As PointF
            Dim nodeIndex = nodeInBox.Select(Function(vi) vi.ID.ToString).Indexing

            For Each edge In g.graphEdges
                If edge.V.ID.ToString Like nodeIndex OrElse edge.U.ID.ToString Like nodeIndex Then
                    Dim a As New PointF(edge.U.data.initialPostion.x - ViewPosition.X, edge.U.data.initialPostion.y - ViewPosition.Y)
                    Dim b As New PointF(edge.V.data.initialPostion.x - ViewPosition.X, edge.V.data.initialPostion.y - ViewPosition.Y)

                    view.DrawLine(edge.data.style, a, b)
                End If
            Next

            For Each node As Node In nodeInBox
                pos = New PointF(node.data.initialPostion.x - ViewPosition.X, node.data.initialPostion.y - ViewPosition.Y)
                view.DrawCircle(pos, node.data.size(0), node.data.color)
            Next

            Return view.ImageResource
        End Using
    End Function

    Public Sub Rendering()
        PictureBox1.BackgroundImage = RenderView()
    End Sub

    Private Sub GraphPad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.Dock = DockStyle.Fill
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub

    Private Sub GraphPad_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        PictureBox1.BackgroundImage = RenderView()

        If Not nav Is Nothing Then
            nav.SetView(PictureBox1.Size)
        End If
    End Sub

    ' Public Property DampingFactor As Double = 35

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        Dim movX = e.X - offsetX
        Dim movY = e.Y - offsetY

        If dragNode IsNot Nothing Then
            ' just move the node 
            RaiseEvent PrintMessage($"Move graph node by delta({movX},{movY})", MSG_TYPES.DEBUG)

            Dim x = x0 + movX '/ DampingFactor
            Dim y = y0 + movY ' / DampingFactor

            If x < 0 OrElse y < 0 Then
                Return
            End If
            If x >= Canvas.Width OrElse y >= Canvas.Height Then
                Return
            End If

            dragNode.data.initialPostion.x = x
            dragNode.data.initialPostion.y = y

            Rendering()
        ElseIf dragCanvas Then
            ' move the current canvas view
            RaiseEvent PrintMessage($"Move canvas viewbox by delta({movX},{movY})", MSG_TYPES.DEBUG)

            Dim x = x0 + movX '/ DampingFactor
            Dim y = y0 + movY '/ DampingFactor

            If x < 0 OrElse y < 0 Then
                Return
            End If
            If x + PictureBox1.Width >= Canvas.Width OrElse y + PictureBox1.Height >= Canvas.Height Then
                Return
            End If

            ViewPosition = New Point(x, y)

            If Not nav Is Nothing Then
                nav.SetView(ViewPosition)
            End If

            Rendering()
        ElseIf addLink AndAlso Not U Is Nothing Then

        Else
            ' show mouse location, for debug
            Dim x, y As Integer

            Call GetCanvasXY(x, y)

            Dim q = SelectNode(x, y)

            If q Is Nothing Then
                RaiseEvent PrintMessage($"Mouse location on graph canvas: ({x},{y})", MSG_TYPES.DEBUG)
            Else
                RaiseEvent PrintMessage($"Mouse hover on node {q.ToString}, left click for drag the node", MSG_TYPES.DEBUG)
            End If
        End If
    End Sub

    Dim dragNode As Node
    Dim dragCanvas As Boolean = False
    Dim offsetX, offsetY As Integer
    Dim x0, y0 As Integer

    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick
        clickPos = PictureBox1.PointToClient(Cursor.Position)
        dragNode = Nothing

        If e.Button = MouseButtons.Left Then
            If addLink AndAlso Not U Is Nothing Then
                Dim x, y As Integer

                Call GetCanvasXY(x, y)

                Dim V = SelectNode(x, y)

                If Not V Is Nothing Then
                    RaiseEvent PrintMessage($"Create a new link from {U} to {V}", MSG_TYPES.INF)

                    Call g.CreateEdge(U, V, 1, New EdgeData With {.style = Pens.Green})
                    Call Rendering()
                End If
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        clickPos = PictureBox1.PointToClient(Cursor.Position)
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        If e.Button = MouseButtons.Left Then
            dragCanvas = False
            dragNode = Nothing

            RaiseEvent PrintMessage("Release the mouse drag handler", MSG_TYPES.DEBUG)
        End If
    End Sub

    Private Function SelectNode(x As Integer, y As Integer) As Node
        Return g.vertex _
            .Where(Function(v)
                       Dim vpos = v.data.initialPostion
                       Dim r = v.data.size(0)

                       Return vpos.x - r < x AndAlso vpos.x + r > x AndAlso vpos.y - r < y AndAlso vpos.y + r > y
                   End Function) _
            .FirstOrDefault
    End Function

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        ' check of the node position
        ' inside node - select node
        ' outside node - do nothing
        Dim x, y As Integer

        Call GetCanvasXY(x, y)

        offsetX = e.X
        offsetY = e.Y

        If e.Button = MouseButtons.Left Then
            ' start drag a node
            ' find a node
            Dim q As Node = SelectNode(x, y)

            If Not q Is Nothing Then
                dragNode = q
                x0 = q.data.initialPostion.x
                y0 = q.data.initialPostion.y

                If Not nav Is Nothing Then
                    Call nav.SetNodeTarget(dragNode)
                End If

                RaiseEvent PrintMessage($"Select Node {dragNode.ToString} at ({x},{y})", MSG_TYPES.DEBUG)
            Else
                dragCanvas = True
                x0 = ViewPosition.X
                y0 = ViewPosition.Y

                RaiseEvent PrintMessage($"Click on canvas at ({x},{y})", MSG_TYPES.DEBUG)
            End If
        Else
            ' do nothing for context menu
        End If
    End Sub

    Dim addLink As Boolean = False
    Dim U As Node

    Private Sub AddLinkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddLinkToolStripMenuItem.Click
        Dim x, y As Integer

        Call GetCanvasXY(x, y)

        U = SelectNode(x, y)

        If U Is Nothing Then
            RaiseEvent PrintMessage("No graph node could be selected!", MSG_TYPES.ERR)
        Else
            addLink = True
        End If
    End Sub
End Class
