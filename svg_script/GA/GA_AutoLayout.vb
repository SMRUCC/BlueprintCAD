Imports System.Drawing
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.Language.Default
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper
Imports Microsoft.VisualBasic.Scripting.Runtime

Public Structure GA_AutoLayout

    Dim anchors As (a As Point, b As Point)()
    Dim blocks As Block()
    Dim size As Size
    Dim stackSize As (minSize%, maxStackSize%)

    Shared ReadOnly defaultStackSize As New DefaultValue(Of (Integer, Integer)) With {
        .Value = (3, 20),
        .assert = Function(size)
                      With DirectCast(size, (Integer, Integer))
                          Return .Item1 = 0 AndAlso .Item2 = 0
                      End With
                  End Function
    }

    Public Function DoAutoLayout(Optional populationSize% = 1000, Optional runs% = 5000) As Routes
        Dim population As Population(Of Routes) = New Routes(anchors, size).InitialPopulation(populationSize)
        Dim fitness As New Fitness With {
            .blocks = blocks
        }
        Dim ga As New GeneticAlgorithm(Of Routes)(population, fitness)

        ga.AddDefaultListener(Sub(best)
                                  Call Console.WriteLine(best.ToString)
                                  Call ga.Best.Draw(fitness.blocks) _
                                              .AsGDIImage _
                                              .SaveAs($"./Debug/{best.iter}.png")
                              End Sub)
        ga.Evolve(runs)

        Dim solution As Routes = ga.Best
        Return solution
    End Function

    Public Shared Operator *(layout As GA_AutoLayout, scaleFactor#) As GA_AutoLayout
        Dim size As Size = layout.size.Scale(scaleFactor)
        Dim shapes = layout.blocks _
                           .Select(Function(b) b.Location) _
                           .ToArray
        Dim offset As PointF = shapes.CentralOffset(size)

        shapes = shapes _
            .Select(Function(p) p.OffSet2D(offset)) _
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
                       .ToArray
        offset = shapes.CentralOffset(size)
        shapes = shapes _
            .Select(Function(p) p.OffSet2D(offset)) _
            .Enlarge(scaleFactor)

        Return New GA_AutoLayout With {
            .blocks = blocks,
            .size = size,
            .anchors = shapes.SlideWindows(2) _
                             .Select(Of (Point, Point))(Function(a, b) (a, b)) _
                             .ToArray,
            .stackSize = layout.stackSize
        }
    End Operator
End Structure
