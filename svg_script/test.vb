Imports System.Drawing
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper

Module test

    Sub Main()

        '  Call helperTest()

        Dim a = (New Point(100, 100), New Point(2000, 2000))
        Dim b = (New Point(500, 985), New Point(2500, 2000))
        Dim c = (New Point(1500, 100), New Point(2500, 1850))
        Dim d = (a.Item1, b.Item1)
        Dim e = (c.Item1, b.Item1)
        Dim size = New Size(2600, 2500)
        Dim anchors = {a, b, c, d, e}

        Dim population As Population(Of Routes) = New Routes(anchors, size).InitialPopulation(750)
        Dim fitness As Fitness(Of Routes) = New Fitness With {
            .blocks = {
                New rect With {.rectangle = New Rectangle(800, 1000, 100, 200)},
                New rect With {.rectangle = New Rectangle(1750, 1700, 300, 100)},
                New rect With {.rectangle = New Rectangle(1400, 1000, 300, 100)}
            }
        }
        Dim ga As New GeneticAlgorithm(Of Routes)(population, fitness)
        '   Dim out As New List(Of outPrint)

        ga.AddDefaultListener '(Sub(x) Call out.Add(x))
        ga.Evolve(2000)
        '   out.SaveTo("./outPrint.csv")

        Dim solution = ga.Best

        Call solution.Draw(DirectCast(fitness, Fitness).blocks).Save("./test_print.png")

        Pause()
    End Sub

    Sub helperTest()

        Dim ZERO = (New Point(1, 0), New Point(10, 0))

        Dim d45 = (New Point(0, 0), New Point(5, 5))

        Call GeomTransform.CalculateAngle(ZERO.Item1, ZERO.Item2).__DEBUG_ECHO
        Call GeomTransform.CalculateAngle(d45.Item1, d45.Item2).__DEBUG_ECHO

        Pause()
    End Sub
End Module
