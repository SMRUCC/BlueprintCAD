Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging
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

    Public Sub Hook(nav As FormNavigator)
        Me.nav = nav
        Me.nav.Pad = Me
    End Sub

    Private Sub AddNodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNodeToolStripMenuItem.Click
        Dim x, y As Integer

        Call GetCanvasXY(x, y)
        Call g.CreateNode(New NodeData() With {
            .initialPostion = New FDGVector2(x, y),
            .size = {10},
            .color = Brushes.Red
        })
        Reload()
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
            Dim q = g.vertex _
                .AsParallel _
                .Where(Function(v)
                           Return v.data.CheckInside(rect)
                       End Function) _
                .ToArray
            Dim pos As PointF

            For Each node As Node In q
                pos = New PointF(node.data.initialPostion.x - ViewPosition.X, node.data.initialPostion.y - ViewPosition.Y)
                view.DrawCircle(pos, node.data.size(0), node.data.color)
            Next

            Return view.ImageResource
        End Using
    End Function

    Private Sub Reload()
        PictureBox1.BackgroundImage = RenderView()
    End Sub

    Private Sub GraphPad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.Dock = DockStyle.Fill
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub

    Private Sub GraphPad_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        PictureBox1.BackgroundImage = RenderView()
    End Sub

    Public Property DampingFactor As Double = 35

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        Dim movX = e.X - offsetX
        Dim movY = e.Y - offsetY

        If dragNode IsNot Nothing Then
            ' just move the node 
            RaiseEvent PrintMessage($"Move graph node by delta({movX},{movY})", MSG_TYPES.DEBUG)

            dragNode.data.initialPostion.x += movX
            dragNode.data.initialPostion.y += movY

            Reload()
        ElseIf dragCanvas Then
            ' move the current canvas view
            RaiseEvent PrintMessage($"Move canvas viewbox by delta({movX},{movY})", MSG_TYPES.DEBUG)

            ViewPosition = New Point(ViewPosition.X + movX / DampingFactor, ViewPosition.Y + movY / DampingFactor)
            Reload()
        End If
    End Sub

    Dim dragNode As Node
    Dim dragCanvas As Boolean = False
    Dim offsetX, offsetY As Integer

    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick
        clickPos = PictureBox1.PointToClient(Cursor.Position)
        dragNode = Nothing
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        clickPos = PictureBox1.PointToClient(Cursor.Position)
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        If e.Button = MouseButtons.Left Then
            dragCanvas = False
            RaiseEvent PrintMessage("Release the mouse drag handler", MSG_TYPES.DEBUG)
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        ' check of the node position
        ' inside node - select node
        ' outside node - do nothing
        Dim x, y As Integer

        Call GetCanvasXY(x, y)

        If e.Button = MouseButtons.Left Then
            ' start drag a node
            ' find a node
            Dim q = g.vertex _
                .Where(Function(v)
                           Dim vpos = v.data.initialPostion
                           Dim r = v.data.size(0)

                           Return vpos.x - r < x AndAlso vpos.x + r > x AndAlso vpos.y - r < y AndAlso vpos.y + r > y
                       End Function) _
                .FirstOrDefault

            If Not q Is Nothing Then
                dragNode = q

                If Not nav Is Nothing Then
                    Call nav.SetNodeTarget(dragNode)
                End If

                RaiseEvent PrintMessage($"Select Node {dragNode.ToString} at ({x},{y})", MSG_TYPES.DEBUG)
            Else
                dragCanvas = True
                offsetX = e.X
                offsetY = e.Y
                RaiseEvent PrintMessage($"Click on canvas at ({x},{y})", MSG_TYPES.DEBUG)
            End If
        Else
            ' do nothing for context menu
        End If
    End Sub
End Class
