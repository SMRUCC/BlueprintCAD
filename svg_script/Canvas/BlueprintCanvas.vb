Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports GCModeller.CAD.GA
Imports GCModeller.CAD.GA.Models
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Driver
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.Markup.HTML.CSS

Public Module BlueprintCanvas

    <Extension>
    Public Function Draw(solution As Routes, blocks As IEnumerable(Of Block), Optional bg$ = "white", Optional connectorStyle$ = Stroke.StrongHighlightStroke) As GraphicsData
        Dim linePen As Pen = Stroke.TryParse(connectorStyle).GDIObject
        Dim r! = 5
        Dim plotInternal =
            Sub(ByRef g As IGraphics, region As GraphicsRegion)

                For Each anchor In solution.Anchors
                    Call g.FillCircles(Brushes.Blue, {anchor.A}, r)
                    Call g.FillCircles(Brushes.Blue, {anchor.B}, r)
                Next

                For Each connection In solution.PopulateRoutes
                    For Each line In connection.SlideWindows(2)
                        Dim a = line(0), b = line(1)
                        Call g.DrawLine(linePen, a, b)
                    Next
                Next

                For Each block As Block In blocks.SafeQuery
                    Select Case block.GetType
                        Case GetType(rect)

                            Call g.FillRectangle(Brushes.Black, DirectCast(block, rect).rectangle)

                        Case GetType(Circle)

                            With DirectCast(block, Circle)
                                Call g.DrawCircle(.center, .radius, Brushes.Black)
                            End With

                        Case Else

                            Throw New NotImplementedException(block.GetType.FullName)

                    End Select
                Next
            End Sub

        Return g.GraphicsPlots(solution.Size, New Padding(0), bg, plotInternal)
    End Function

    <Extension>
    Public Iterator Function PopulateRoutes(solution As Routes) As IEnumerable(Of PointF())
        For Each path As Path In solution.Path
            Yield path.Shrink.ToArray
        Next
    End Function

    <Extension>
    Public Iterator Function Anchors(solution As Routes) As IEnumerable(Of (A As PointF, B As PointF))
        For Each path As Path In solution.Path
            Yield (path.A, path.B)
        Next
    End Function
End Module
