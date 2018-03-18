Imports System.Drawing
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper

Module test

    Sub Main()
        Dim a = (New Point(10, 10), New Point(20, 20))
        Dim b = (New Point(0, 0), New Point(25, 20))
        Dim c = (New Point(15, 10), New Point(25, 25))
        Dim d = (a.Item1, b.Item1)
        Dim e = (c.Item1, b.Item1)
        Dim size = New Size(25, 25)
        Dim anchors = {a, b, c, d, e}

        Dim population As Population(Of Routes) = New Routes(anchors, size).InitialPopulation(100)
        Dim fitness As Fitness(Of Routes) = New Fitness
        Dim ga As New GeneticAlgorithm(Of Routes)(population, fitness)
        '   Dim out As New List(Of outPrint)

        ga.AddDefaultListener '(Sub(x) Call out.Add(x))
        ga.Evolve(5000)
        '   out.SaveTo("./outPrint.csv")

        Pause()
    End Sub
End Module
