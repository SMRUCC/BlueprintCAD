Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports GCModeller.CAD.Canvas
Imports GCModeller.CAD.GA
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Driver
Imports Microsoft.VisualBasic.MIME.Markup.HTML.CSS

Public Module DirectDraw

    <Extension>
    Public Function Visualize(layout As AutoLayout, Optional bg$ = "white", Optional compoundLabelFontCSS$ = CSSFont.Win7Normal) As GraphicsData
        Dim compoundLabel As Font = CSSFont.TryParse(compoundLabelFontCSS).GDIObject
        Dim plotInternal =
            Sub(ByRef g As IGraphics, region As GraphicsRegion)
                For Each block As Block In layout.blocks
                    If TypeOf block Is Circle Then
                        With DirectCast(block, Circle)
                            Call g.DrawCircle(.center, .radius, Brushes.Black)
                            Call g.DrawString(.Name, compoundLabel, Brushes.Black, .center)
                        End With
                    End If

                    For Each anchor In layout.anchors
                        Dim a = anchor.a
                        Dim b = anchor.b

                        If Math.Abs(a.Y - b.Y) <= 10 Then
                            ' 两个点是位于同一个水平面的
                            Dim AB As DoubleRange = {a.X, b.X}

                            ' 再查看路径上是否存在其他的连接点
                            If layout.blocks.Where(Function(ap)
                                                       ' X位于AB之间，并且Y等于a或者b点
                                                       Return AB.IsInside(ap.Location.X) AndAlso Math.Abs(ap.Location.Y - a.Y) <= 10
                                                   End Function).Count >= 3 Then

                                ' 路径中间存在其他的点，则越过这个点
                                Dim c As New PointF(a.X, a.Y - 100)
                                Dim d As New PointF(b.X, c.Y)

                                Call g.DrawLine(Pens.Black, a, c)
                                Call g.DrawLine(Pens.Black, c, d)
                                Call g.DrawLine(Pens.Black, d, b)
                            Else
                                ' 直接绘制
                                Call g.DrawLine(Pens.Black, a, New PointF(b.X, a.Y))
                            End If

                        ElseIf Math.Abs(a.X - b.X) <= 10 Then
                            ' 两个点是位于同一个垂直方向的
                            Dim AB As DoubleRange = {a.Y, b.Y}

                            ' 再查看路径上是否存在其他的连接点
                            If layout.blocks.Where(Function(ap)
                                                       ' Y位于AB之间，并且X等于a或者b点
                                                       Return AB.IsInside(ap.Location.Y) AndAlso Math.Abs(ap.Location.X - a.X) <= 10
                                                   End Function).Count >= 3 Then
                                Dim c As New PointF(a.X + 100, a.Y)
                                Dim d As New PointF(c.X, b.Y)

                                Call g.DrawLine(Pens.Black, a, c)
                                Call g.DrawLine(Pens.Black, c, d)
                                Call g.DrawLine(Pens.Black, d, b)

                            Else
                                ' 直接绘制
                                Call g.DrawLine(Pens.Black, a, New PointF(a.X, b.Y))
                            End If

                        Else
                                ' 两个点之间会存在一个转弯
                                Dim c As New PointF(a.X, b.Y)

                            Call g.DrawLine(Pens.Black, a, c)
                            Call g.DrawLine(Pens.Black, c, b)

                        End If
                    Next
                Next
            End Sub

        Return g.GraphicsPlots(
            layout.size, New Padding,
            bg,
            plotInternal
        )
    End Function
End Module
