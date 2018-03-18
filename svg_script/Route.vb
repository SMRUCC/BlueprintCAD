Imports System.Drawing
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper
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
    ''' 一个populate之中的所有的route的锚点都应该是一样的
    ''' 即，这个域的值在一个种群内都是一致的
    ''' </summary>
    Public ReadOnly Anchors As (a As Point, b As Point)()
    Public ReadOnly Size As Size

    Sub New(anchors As (a As Point, b As Point)(), size As Size, x As Vector, y As Vector)
        Me.Anchors = anchors
        Me.Size = size

        If x Is Nothing OrElse y Is Nothing Then
            Call allocate()
        Else
            Me.X = x
            Me.Y = y
        End If
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

    ''' <summary>
    ''' 可能发生的情况：
    ''' 
    ''' + 节点的坐标发生变化
    ''' + 增加一个节点坐标
    ''' + 减少一个节点坐标
    ''' </summary>
    ''' <returns></returns>
    Public Function Mutate() As Routes Implements Chromosome(Of Routes).Mutate
        Dim X = Me.X.Split(Function(v) v = -1.0R)
        Dim Y = Me.Y.Split(Function(v) v = -1.0R)
        Dim dx, dy As New List(Of Double)

        With New Random
            Dim px, py As Double()

            For i As Integer = 0 To Anchors.Length - 1
                px = X(i)
                py = Y(i)

                Select Case .NextDouble
                    Case <= 0.3
                        px.Mutate(.ByRef)
                        py.Mutate(.ByRef)
                    Case <= 0.6
                        px.Add(.Next(Size.Width))
                        py.Add(.Next(Size.Height))
                    Case Else
                        Dim index = .Next(px.Length)

                        Call px.Delete(index)
                        Call py.Delete(index)
                End Select

                dx.AddRange(px)
                dy.AddRange(py)
                dx.Add(-1)
                dy.Add(-1)
            Next
        End With

        Return New Routes(Anchors, Size, dx.AsVector, dy.AsVector)
    End Function
End Class
