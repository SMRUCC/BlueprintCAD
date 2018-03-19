Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
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

    ReadOnly random As New Random

    Sub New(anchors As (a As Point, b As Point)(),
            size As Size,
            Optional x As Vector = Nothing,
            Optional y As Vector = Nothing)

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

        X = New Vector(Anchors.Length * 3)
        Y = New Vector(X.Length)

        For Each line In Anchors
            X(++i) = line.a.X
            Y(++j) = line.a.Y
            X(++i) = line.b.X
            Y(++j) = line.b.Y
            X(++i) = -1
            Y(++j) = -1
        Next
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="another"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 在交叉的时候要特别注意不可以将首尾元素的锚点给修改了
    ''' </remarks>
    Public Function Crossover(another As Routes) As IEnumerable(Of Routes) Implements Chromosome(Of Routes).Crossover
        Dim thisX = X.Split(Function(v) v = -1.0R), thisY = Y.Split(Function(v) v = -1.0R)
        Dim otherX = another.X.Split(Function(v) v = -1.0R), otherY = another.Y.Split(Function(v) v = -1.0R)

        With random
            Dim d%

            For i As Integer = 0 To Anchors.Length - 1
                d = thisX(i).Length - otherY(i).Length

                ' 因为在突变过程之中可能会增减网格节点，所以可能会出现长度不一致的情况
                ' 如果长度不一致的话，需要对最短的向量进行补齐

                ' 2018-3-18
                ' 因为必须要保持首尾元素不变，所以在这里补齐的时候fill最后一个元素
                If d > 0 Then
                    ' 补齐other
                    otherX(i) = otherX(i).Fill(otherX(i).Last, d)
                    otherY(i) = otherY(i).Fill(otherY(i).Last, d)
                ElseIf d < 0 Then
                    ' 补齐this
                    d = -d

                    thisX(i) = thisX(i).Fill(thisX(i).Last, d)
                    thisY(i) = thisY(i).Fill(thisY(i).Last, d)
                End If

                Call .Crossover(thisX(i), otherX(i))
                Call .Crossover(thisY(i), otherY(i))

                Call thisX(i).Add(-1)
                Call thisY(i).Add(-1)
                Call otherX(i).Add(-1)
                Call otherY(i).Add(-1)
            Next
        End With

        Return {
            New Routes(Anchors, Size, thisX.IteratesALL.AsVector, thisY.IteratesALL.AsVector),
            New Routes(Anchors, Size, otherX.IteratesALL.AsVector, otherY.IteratesALL.AsVector)
        }
    End Function

    ''' <summary>
    ''' 可能发生的情况：
    ''' 
    ''' + 节点的坐标发生变化
    ''' + 增加一个节点坐标
    ''' + 减少一个节点坐标
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' 在突变的时候要特别注意不可以将首尾的锚点给修改了
    ''' </remarks>
    Public Function Mutate() As Routes Implements Chromosome(Of Routes).Mutate
        Dim X = Me.X.Split(Function(v) v = -1.0R)
        Dim Y = Me.Y.Split(Function(v) v = -1.0R)
        Dim dx, dy As New List(Of Double)

        With random
            Dim px, py As Double()

            For i As Integer = 0 To Anchors.Length - 1
                px = X(i)
                py = Y(i)

                If px.Length = 2 Then
                    ' 只有首尾两个元素，必须要向中间插入一个元素
                    px.InsertAt(.Next(Size.Width), 1)
                    py.InsertAt(.Next(Size.Height), 1)
                Else
                    Select Case .NextDouble
                        Case <= 0.3
                            px.Mutate(.ByRef)
                            py.Mutate(.ByRef)
                        Case <= 0.6
                            Dim index = .Next(px.Length)

                            px.InsertAt(.Next(Size.Width), index)
                            py.InsertAt(.Next(Size.Height), index)
                        Case Else
                            Dim index = .Next(px.Length)

                            Call px.Delete(index)
                            Call py.Delete(index)
                    End Select
                End If

                dx.AddRange(px)
                dy.AddRange(py)
                dx.Add(-1)
                dy.Add(-1)
            Next
        End With

        Return New Routes(Anchors, Size, dx.AsVector, dy.AsVector)
    End Function

    Public Overrides Function ToString() As String
        Dim X = Me.X.ToString
        Dim Y = Me.Y.ToString

        Return $"[{X} | {Y}]"
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Shared Narrowing Operator CType(routes As Routes) As String
        Return routes.ToString
    End Operator
End Class
