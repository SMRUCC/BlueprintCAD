Imports System.Drawing
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.Runtime

Public Structure GA_AutoLayout

    Dim anchors As (a As Point, b As Point)()
    Dim blocks As Block()
    Dim size As SizeF

    Public Function DoAutoLayout(Optional runs% = 5000) As Routes

    End Function

    Public Shared Operator *(layout As GA_AutoLayout, scaleFactor#) As GA_AutoLayout
        Dim size As SizeF = layout.size.Scale(scaleFactor)
        Dim shapes = layout.blocks _
                           .Select(Function(b) b.Location) _
                           .Enlarge(scaleFactor)
        Dim blocks = layout.blocks _
                           .Select(Function(b, i)
                                       Dim location As Point = shapes(i)

                                       Select Case b.GetType
                                           Case GetType(Circle)
                                               Return New Circle With {
                                                   .center = location,
                                                   .ID = b.ID,
                                                   .radius = DirectCast(b, Circle).radius * scaleFactor
                                               }.AsBaseType(Of Block)
                                           Case Else
                                               Return b
                                       End Select
                                   End Function) _
                           .ToArray

        shapes = layout.anchors _
                       .Select(Function(a) {a.a, a.b}) _
                       .IteratesALL _
                       .Enlarge(scaleFactor)

        Return New GA_AutoLayout With {
            .blocks = blocks,
            .size = size,
            .anchors = shapes.SlideWindows(2) _
                             .Select(Of (Point, Point))(Function(a, b) (a, b)) _
                             .ToArray
        }
    End Operator
End Structure
