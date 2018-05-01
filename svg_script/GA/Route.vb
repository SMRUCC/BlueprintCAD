Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports GCModeller.CAD.GA.Models
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.Models
Imports Microsoft.VisualBasic.Math
Imports Microsoft.VisualBasic.Math.LinearAlgebra
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace GA

    ''' <summary>
    ''' 网格之中的A到B点的连接路径集合，这个相当于一个染色体，即一个可能的最优解决方案
    ''' </summary>
    Public Class Routes : Implements Chromosome(Of Routes)

        ''' <summary>
        ''' 节点之间的连接使用-1来进行分割，在这里规定坐标点必须要大于等于零
        ''' 例如第一个被-1分割的块为第一对连接点之间的连接路线
        ''' </summary>
        Public ReadOnly Path As Path()
        Public ReadOnly Size As Size

        ReadOnly random As New Random(Now.Millisecond)
        ReadOnly minSize%

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="anchors"></param>
        ''' <param name="size"></param>
        ''' <param name="maxStackSize%"></param>
        ''' <param name="minSize%">一条路径最短需要有多少个节点构成</param>
        Sub New(anchors As (a As Point, b As Point)(), size As Size, Optional maxStackSize% = 10, Optional minSize% = 3)
            Me.Path = anchors.Select(Function(a) New Path(a, maxStackSize)).ToArray
            Me.Size = size
            Me.minSize = minSize
        End Sub

        Sub New(path As IEnumerable(Of Path), size As Size, minSize%)
            Me.Path = path.ToArray
            Me.Size = size
            Me.minSize = minSize
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
            Dim d%
            Dim thisX#(), thisY#()
            Dim otherX#(), otherY#()
            Dim out1 As New List(Of Path)
            Dim out2 As New List(Of Path)
            Dim A, B As PointF

            With random

                For i As Integer = 0 To Path.Length - 1
                    With Path(i)
                        thisX = .X.ToArray
                        thisY = .Y.ToArray

                        ' 因为都是同一个对象，所以this和other都是用同一组锚点
                        A = .A
                        B = .B
                    End With
                    With another.Path(i)
                        otherX = .X.ToArray
                        otherY = .Y.ToArray
                    End With

                    d = thisX.Length - otherY.Length

                    ' 因为在突变过程之中可能会增减网格节点，所以可能会出现长度不一致的情况
                    ' 如果长度不一致的话，需要对最短的向量进行补齐

                    ' 2018-3-18
                    ' 因为必须要保持首尾元素不变，所以在这里补齐的时候fill最后一个元素
                    If d > 0 Then
                        ' 补齐other
                        otherX = otherX.Fill(otherX.Last, d)
                        otherY = otherY.Fill(otherY.Last, d)
                    ElseIf d < 0 Then
                        ' 补齐this
                        d = -d

                        thisX = thisX.Fill(thisX.Last, d)
                        thisY = thisY.Fill(thisY.Last, d)
                    Else
                        ' d = 0 
                        ' 二者是等长的，不需要做额外的处理
                        ' Do Nothing
                    End If

                    Call .Crossover(thisX, otherX)
                    Call .Crossover(thisY, otherY)

                    out1 += New Path(A, B, thisX.AsVector, thisY.AsVector)
                    out2 += New Path(A, B, otherX.AsVector, otherY.AsVector)
                Next
            End With

            Return {
                New Routes(out1, Size, minSize),
                New Routes(out2, Size, minSize)
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
            Dim out As New List(Of Path)

            With random

                For Each path As Path In Me.Path _
                .Select(Function(p)
                            ' 在这里应该都是使用的copy方法，
                            ' 否则会将原来的数据对象也给修改掉了
                            Return New Path(p.A, p.B, p.X.AsVector, p.Y.AsVector)
                        End Function)

                    Dim randX = .Next(Size.Width)
                    Dim randY = .Next(Size.Height)

                    If path.Length < minSize Then
                        Select Case .NextDouble
                            Case >= 0.5
                                Call path.Append(randX, randY)
                            Case Else
                                Call path.Insert(New PointF(randX, randY), .Next(path.Length))
                        End Select
                    Else
                        Select Case .NextDouble
                            Case <= 0.2
                                Call path.Append(randX, randY)
                            Case <= 0.35
                                Call path.Delete(.Next(path.Length))
                            Case <= 0.5
                                Call path.Insert(New PointF(randX, randY), .Next(path.Length))
                            Case Else
                                Call path.Mutate(.ByRef)
                        End Select
                    End If

                    out += path
                Next

            End With

            Return New Routes(out, Size, minSize)
        End Function

        Public Overrides Function ToString() As String
            Dim shrinks = Path.Select(Function(p) p.Shrink).IteratesALL.ToArray
            Dim X = shrinks.X.Select(Function(n) CInt(n)).AsList
            Dim Y = shrinks.Y.Select(Function(n) CInt(n)).ToArray

            Return (X + Y).GetJson
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Narrowing Operator CType(routes As Routes) As String
            Return routes.ToString
        End Operator
    End Class

End Namespace

