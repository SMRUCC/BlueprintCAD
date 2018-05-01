Imports System.Drawing
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper
Imports SMRUCC.genomics.Assembly.KEGG.WebServices
Imports SMRUCC.genomics.Data.KEGG.Metabolism

Module test

    Sub randomTest()

        With New Random

            For i As Integer = 0 To 100
                Call .NextDouble.__DEBUG_ECHO
            Next

            Call "==============================================".__INFO_ECHO

            ' 尽量不要创建新的random对象
            For i As Integer = 0 To 100
                Call New Random().NextDouble.__DEBUG_ECHO
            Next

            Pause()
        End With
    End Sub

    Sub runTest()
        Dim links As ReactionLink = ReactionLink.FromRepository("C:\Users\Evia\source\repos\GCModeller-CAD-blueprint\KGML\br08201")
        Dim engine = KEGGImports.ImportsMap("C:\Users\Evia\source\repos\GCModeller-CAD-blueprint\KGML\maps\Metabolism\Carbohydrate metabolism\map00020.XML".LoadXml(Of Map), links, 10)
        Dim best As Routes = engine.DoAutoLayout(500, 1000)

        Call best.Draw(engine.blocks,).AsGDIImage.SaveAs("./test_run.png")

        Pause()
    End Sub

    Sub Main()

        Call runTest()
        Call randomTest()

        '  Call helperTest()

        Dim a = (New Point(100, 100), New Point(2000, 2000))
        Dim b = (New Point(500, 985), New Point(2500, 2000))
        Dim c = (New Point(1500, 100), New Point(2500, 1850))
        Dim d = (a.Item1, b.Item1)
        Dim e = (c.Item1, b.Item1)
        Dim f = (New Point(500, 2100), New Point(900, 1500))
        Dim g = (New Point(300, 1800), New Point(600, 1400))
        Dim h = (a.Item1, g.Item1)
        Dim i = (c.Item2, f.Item2)

        Dim size = New Size(2600, 2500)
        Dim anchors = {a, b, c, d, e, f, g, h, i}

        Dim links As ReactionLink = ReactionLink.FromRepository("C:\Users\Evia\source\repos\GCModeller-CAD-blueprint\KGML\br08201")
        Dim blocks = KEGGImports.ImportsMap("C:\Users\Evia\source\repos\GCModeller-CAD-blueprint\KGML\Metabolism\Carbohydrate metabolism\map00020.XML".LoadXml(Of Map), links, 10)

        Dim population As Population(Of Routes) = New Routes(anchors, size).InitialPopulation(10000)
        Dim fitness As Fitness(Of Routes) = New Fitness With {
            .blocks = New Block() {
                New rect With {.rectangle = New Rectangle(800, 1000, 100, 200)},
                New rect With {.rectangle = New Rectangle(1750, 1700, 300, 100)},
                New rect With {.rectangle = New Rectangle(1400, 1000, 300, 100)}
            }.AsList + blocks.blocks
        }
        Dim ga As New GeneticAlgorithm(Of Routes)(population, fitness)
        '   Dim out As New List(Of outPrint)

        ga.AddDefaultListener '(Sub(x) Call out.Add(x))
        ga.Evolve(100)
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
