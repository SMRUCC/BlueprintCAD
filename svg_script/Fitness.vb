Imports System.Drawing
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF

Public Class Fitness : Implements Fitness(Of Routes)

    Public Function Calculate(chromosome As Routes) As Double Implements Fitness(Of Routes).Calculate
        ' 路径应该尽量短
        ' 路径间的交叉应该尽量少
        ' 斜线应该尽量少
        Dim X = chromosome.X.Split(Function(v) v = -1.0R)
        Dim Y = chromosome.Y.Split(Function(v) v = -1.0R)

        Dim length = Aggregate line In X Into Sum(line.Length)
        Dim hypotenuse%

        ' 使用滑窗，计算前后两个节点之间是否存在斜线，存在斜线则加1
        For index As Integer = 0 To X.Length - 1
            Dim linesX = X(index).SlideWindows(2).ToArray
            Dim linesY = Y(index).SlideWindows(2).ToArray

            For i As Integer = 0 To linesX.Length - 1
                Dim a As New PointF(linesX(i)(0), linesY(i)(0))
                Dim b As New PointF(linesX(i)(1), linesY(i)(1))
                Dim angle = Math.Abs(a.CalculateAngle(b))

                If angle Mod 45 = 0R Then
                    hypotenuse += 0
                ElseIf angle Mod 30 = 0R Then
                    hypotenuse += 0
                Else
                    hypotenuse += angle / Math.PI
                End If
            Next
        Next

        Dim fitness# = length * hypotenuse
        Return fitness
    End Function
End Class
