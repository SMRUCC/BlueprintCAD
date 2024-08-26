Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Imaging

Public Class FormNavigator

    Public Property Pad As GraphPad

    Dim viewbox As Rectangle

    Public Sub SetView(pos As Point)
        viewbox = New Rectangle(pos, viewbox.Size)
        RefreshViewBox()
    End Sub

    Public Sub SetView(size As Size)
        viewbox = New Rectangle(viewbox.Location, size)
        RefreshViewBox()
    End Sub

    Private Sub RefreshViewBox()
        If viewbox.Width > 0 AndAlso viewbox.Height > 0 AndAlso Pad.Canvas.Width > 0 AndAlso Pad.Canvas.Height > 0 Then
            Dim viewSize As New Size(
                viewbox.Width / Pad.Canvas.Width * PictureBox1.Width,
                viewbox.Height / Pad.Canvas.Height * PictureBox1.Height
            )
            Dim viewPos As New Point(
                viewbox.Left / Pad.Canvas.Width * PictureBox1.Width,
                viewbox.Top / Pad.Canvas.Height * PictureBox1.Height
            )

            Using g As Graphics2D = PictureBox1.Size.CreateGDIDevice(Pad.BackColor)
                g.DrawRectangle(Pens.Black, New Rectangle(New Point, PictureBox1.Size))
                g.DrawRectangle(Pens.Red, New Rectangle(viewPos, viewSize))

                PictureBox1.BackgroundImage = g.ImageResource
            End Using
        End If
    End Sub

    Public Sub SetNodeTarget(node As Node)
        PropertyGrid1.SelectedObject = node.data
        PropertyGrid1.Refresh()
    End Sub
End Class