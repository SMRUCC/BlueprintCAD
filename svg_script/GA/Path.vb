Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.Models
Imports Microsoft.VisualBasic.Math.LinearAlgebra

''' <summary>
''' 两个锚点之间的连接路线
''' </summary>
Public Class Path

    ''' <summary>
    ''' 假设所有的路径点都是必须要存在于大于等于零的可见区域
    ''' 所以在这两个向量之中使用-1来填充未被使用的区域
    ''' </summary>
    Public X, Y As Vector
    ''' <summary>
    ''' 路径的固定的首尾两个锚点
    ''' </summary>
    Public A, B As PointF

    Public Const NULL# = -1

    Public ReadOnly Property IsFullStack As Boolean
        Get
            Return Tail = X.Length - 1
        End Get
    End Property

    ''' <summary>
    ''' 返回路径点最后一个元素的顶点编号
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Tail As Integer
        Get
            For i As Integer = 0 To X.Length - 1
                If X(i) = NULL AndAlso Y(i) = NULL Then
                    ' 这个路径点是空的，那么上一个路径点一定不是空的
                    ' 返回上一个路径点作为尾巴
                    Return i - 1
                End If
            Next

            Return X.Length - 1
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="maxStack%">最大的拐点数量</param>
    Sub New(Optional maxStack% = 10)
        X = New Vector(NULL, maxStack)
        Y = New Vector(NULL, maxStack)
    End Sub

    ''' <summary>
    ''' 向最末尾添加一个路径点，如果还有剩余路径点可以被使用的话
    ''' </summary>
    ''' <param name="point"></param>
    ''' 
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Sub Append(point As PointF)
        Call Append(point.X, point.Y)
    End Sub

    Sub Append(x#, y#)
        If Not IsFullStack Then
            With Tail
                Me.X(.ByRef) = x
                Me.Y(.ByRef) = y
            End With
        End If
    End Sub

    ''' <summary>
    ''' 同样，如果还有剩余的空格才会被添加
    ''' </summary>
    ''' <param name="point"></param>
    ''' <param name="index%"></param>
    Sub Insert(point As PointF, index%)
        If Not IsFullStack Then
            Dim afterX = X.Skip(index).ToArray
            Dim afterY = Y.Skip(index).ToArray

            For i As Integer = 0 To afterX.Length - 1
                X(i + index + 1) = afterX(i)
                Y(i + index + 1) = afterY(i)
            Next

            X(index) = point.X
            Y(index) = point.Y
        End If
    End Sub

    ''' <summary>
    ''' 删除编号为<paramref name="index"/>的路径点
    ''' </summary>
    ''' <param name="index">
    ''' 删除掉这个点之后，后面的元素都会往前移
    ''' </param>
    Sub Delete(index As Integer)
        Dim afterX = X.Skip(index).ToArray
        Dim afterY = Y.Skip(index).ToArray

        For i As Integer = 0 To afterX.Length - 1
            X(i + index) = afterX(i)
            Y(i + index) = afterY(i)
        Next

        Call Append(NULL, NULL)
    End Sub

End Class
