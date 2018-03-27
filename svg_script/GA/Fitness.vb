Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF

Public Class Fitness : Implements Fitness(Of Routes)

    Public blocks As Block()
    Public minNodes% = 5

    Public Function Calculate(chromosome As Routes) As Double Implements Fitness(Of Routes).Calculate
        ' 路径应该尽量短
        ' 路径间的交叉应该尽量少
        ' 斜线应该尽量少
        Dim X = chromosome.X.Split(Function(v) v = -1.0R)
        Dim Y = chromosome.Y.Split(Function(v) v = -1.0R)

        Dim length = Aggregate line In X Into Sum(line.Length)
        Dim hypotenuse%
        Dim anchors = chromosome.Anchors
        Dim pathLength As New List(Of Double)

        ' 使用滑窗，计算前后两个节点之间是否存在斜线，存在斜线则加1
        For index As Integer = 0 To anchors.Length - 1
            Dim linesX = X(index).SlideWindows(2).ToArray
            Dim linesY = Y(index).SlideWindows(2).ToArray
            Dim anchor = anchors(index)

            ' 如果首尾锚点已经发生了变动
            ' 则这个解决方案肯定不可以被采用了
            If assertDoubleEquals(X(index)(0), anchor.a.X) OrElse
               assertDoubleEquals(Y(index)(0), anchor.a.Y) OrElse
               assertDoubleEquals(X(index)(X(index).Length - 1), anchor.b.X) OrElse
               assertDoubleEquals(Y(index)(Y(index).Length - 1), anchor.b.Y) Then

                hypotenuse += 10000
                pathLength += 10000
                Continue For
            ElseIf linesX.Length <= minNodes Then
                '  pathLength += 200000
            End If

            For i As Integer = 0 To linesX.Length - 1
                Dim a As New PointF(linesX(i)(0), linesY(i)(0))
                Dim b As New PointF(linesX(i)(1), linesY(i)(1))
                Dim angle = Math.Abs(a.CalculateAngle(b))

                If a.X < 0 Then
                    hypotenuse += -a.X
                End If
                If a.Y < 0 Then
                    hypotenuse += -a.Y
                End If
                If b.X < 0 Then
                    hypotenuse += -b.X
                End If
                If b.Y < 0 Then
                    hypotenuse += -b.Y
                End If

                If angle Mod 45 = 0R Then
                    hypotenuse += 0
                Else
                    hypotenuse += angle
                End If

                pathLength += a.Distance(b)

                If i > 1 AndAlso i < linesX.Length - 2 Then
                    For Each block In blocks.SafeQuery
                        If block.Intersect(a, b) Then
                            pathLength += 2000
                        End If
                    Next
                End If
            Next
        Next

        Dim fitness# = length * hypotenuse + pathLength.Sum
        Return fitness
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Private Shared Function assertDoubleEquals(a#, b#) As Boolean
        Return Math.Abs(a - b) > 0.00001
    End Function
End Class
