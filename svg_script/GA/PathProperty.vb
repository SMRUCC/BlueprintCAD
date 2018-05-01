Imports System.Drawing
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.base
Imports Microsoft.VisualBasic.Imaging.Math2D

''' <summary>
''' 用于<see cref="Fitness"/>计算的一些路径的统计属性
''' </summary>
Public Class PathProperty

    ''' <summary>
    ''' 路径之中的节点数量
    ''' </summary>
    ''' <returns></returns>
    Public Property Length As Integer
    Public Property TotalLength As Double
    Public Property AverageLength As Double
    Public Property MaxDelta As Double
    Public Property MinDelta As Double

    ''' <summary>
    ''' 经过压缩合并之后的路径对象
    ''' </summary>
    ''' <returns></returns>
    Public Property Path As PointF()

    Sub New(path As Path)
        Me.Path = path.Shrink.ToArray

        ' 进行路径属性的计算
        doStat()
    End Sub

    Private Sub doStat()
        Dim dist = Path.SlideWindows(2) _
                       .Select(Function(a, b) a.Distance(b)) _
                       .ToArray

        Length = Path.Length
        TotalLength = dist.Sum
        AverageLength = dist.Average
        MaxDelta = dist.Max
        MinDelta = dist.Min
    End Sub
End Class
