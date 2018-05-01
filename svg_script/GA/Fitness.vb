Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF

Namespace GA

    Public Class Fitness : Implements Fitness(Of Routes)

        Public blocks As Block()

        Public Function Calculate(chromosome As Routes) As Double Implements Fitness(Of Routes).Calculate
            ' 路径应该尽量短
            ' 路径间的交叉应该尽量少
            ' 斜线应该尽量少

            Dim hypotenuse%
            Dim length%
            Dim pathLength As New List(Of Double)
            Dim pathList As New List(Of Line())

            ' 使用滑窗，计算前后两个节点之间是否存在斜线，存在斜线则加1
            For Each routeState As PathProperty In chromosome.Path.Select(Function(p) New PathProperty(p))
                Dim path As PointF() = routeState.Path
                Dim i% = 0

                length += routeState.Length
                pathList += path.SlideWindows(2) _
                            .Select(Of Line)(Function(a, b) New Line(a, b)) _
                            .ToArray

                For Each line As Line In pathList.Last
                    Dim a = line.P1, b = line.P2
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

                    Dim mod90 = angle Mod 90

                    If mod90 = 0R Then
                        hypotenuse += 0
                    Else
                        ' 线条应该尽量是垂直或者水平的
                        hypotenuse += Math.Max(90 - mod90, mod90)
                    End If

                    pathLength += a.Distance(b)

                    If i > 1 AndAlso i <= path.Length - 2 Then
                        For Each block In blocks.SafeQuery
                            If block.Intersect(a, b) Then
                                pathLength += 2000
                            End If
                        Next
                    End If
                Next
            Next

            ' 因为在计算交点的fitness的时候使用的是乘法
            ' 所以假若没有任何交点的话，可能会使fitness的一部分为零
            ' 在这里从1开始
            Dim in% = 1

            ' 查看是否存在交点
            For i As Integer = 0 To pathList.Count - 1
                Dim q = pathList(i)

                For j As Integer = i To pathList.Count - 1
                    Dim s = pathList(j)

                    For Each line1 In q
                        For Each line2 In s
                            If line1.IntersectionOf(line2) Then
                                [in] += 1
                            End If
                        Next
                    Next
                Next
            Next

            Dim fitness# = length * hypotenuse + pathLength.Sum * [in]
            Return fitness
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Private Shared Function assertDoubleEquals(a#, b#) As Boolean
            Return Math.Abs(a - b) > 0.00001
        End Function
    End Class

End Namespace

