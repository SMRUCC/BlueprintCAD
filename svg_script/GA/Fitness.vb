Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF

Public Class Fitness : Implements Fitness(Of Routes)

    Public blocks As Block()

    Public Function Calculate(chromosome As Routes) As Double Implements Fitness(Of Routes).Calculate
        ' 路径应该尽量短
        ' 路径间的交叉应该尽量少
        ' 斜线应该尽量少

        Dim hypotenuse%
        Dim length%
        Dim pathLength As New List(Of Double)

        ' 使用滑窗，计算前后两个节点之间是否存在斜线，存在斜线则加1
        For Each routeState As PathProperty In chromosome.Path.Select(Function(p) New PathProperty(p))
            Dim path As PointF() = routeState.Path
            Dim i% = 0

            length += routeState.Length

            For Each window As SlideWindow(Of PointF) In path.SlideWindows(2)
                Dim a = window(0), b = window(1)
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

                If i > 1 AndAlso i <= path.Length - 2 Then
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
