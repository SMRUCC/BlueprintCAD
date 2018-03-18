Imports System.Drawing
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.Models
Imports Microsoft.VisualBasic.Math.LinearAlgebra

''' <summary>
''' 网格之中的A到B点的连接路径集合，这个相当于一个染色体，即一个可能的最优解决方案
''' </summary>
Public Class Routes : Implements Chromosome(Of Routes)

    ''' <summary>
    ''' 节点之间的连接使用-1来进行分割，在这里规定坐标点必须要大于等于零
    ''' 例如第一个被-1分割的块为第一对连接点之间的连接路线
    ''' </summary>
    Public X, Y As Vector
    ''' <summary>
    ''' 节点对之间的坐标
    ''' </summary>
    Public ReadOnly Anchors As (a As PointF, b As PointF)()

    Sub New(anchors As IEnumerable(Of (a As PointF, b As PointF)))
        Me.Anchors = anchors.ToArray
        Me.allocate()
    End Sub

    Private Sub allocate()
        Dim i As int = 0, j As int = 0

        X = New Vector(Anchors.Length * 3 - 1)
        Y = New Vector(X.Length - 1)

        For Each line In Anchors
            X(++i) = line.a.X
            Y(++j) = line.a.Y
            X(++i) = line.b.X
            Y(++j) = line.b.Y
            X(++i) = -1
            Y(++j) = -1
        Next
    End Sub

    Public Function Crossover(another As Routes) As IList(Of Routes) Implements Chromosome(Of Routes).Crossover

    End Function

    Public Function Mutate() As Routes Implements Chromosome(Of Routes).Mutate

    End Function
End Class
