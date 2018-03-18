Imports System.Drawing
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper

Module test

    Sub Main()
        Dim a = (New Point(100, 100), New Point(2000, 2000))
        Dim b = (New Point(500, 985), New Point(2500, 2000))
        Dim c = (New Point(1500, 100), New Point(2500, 1850))
        Dim d = (a.Item1, b.Item1)
        Dim e = (c.Item1, b.Item1)
        Dim size = New Size(2600, 2500)
        Dim anchors = {a, b, c, d, e}

        Dim population As Population(Of Routes) = New Routes(anchors, size).InitialPopulation(1000)
        Dim fitness As Fitness(Of Routes) = New Fitness
        Dim ga As New GeneticAlgorithm(Of Routes)(population, fitness)
        '   Dim out As New List(Of outPrint)

        ga.AddDefaultListener '(Sub(x) Call out.Add(x))
        ga.Evolve(550)
        '   out.SaveTo("./outPrint.csv")

        Dim solution = ga.Best

        Call solution.Draw().Save("./test_print.png")

        Pause()
    End Sub
End Module
