Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports GCModeller.CAD.Canvas
Imports GCModeller.CAD.GA
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
                            ' 直接绘制
                            Call g.DrawLine(Pens.Black, a, New PointF(b.X, a.Y))
                        ElseIf Math.Abs(a.X - b.X) <= 10 Then
                            ' 两个点是位于同一个垂直方向的
                            ' 直接绘制
                            Call g.DrawLine(Pens.Black, a, New PointF(a.X, b.Y))
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
