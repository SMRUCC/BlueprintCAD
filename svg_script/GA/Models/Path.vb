Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper
Imports Microsoft.VisualBasic.Math.LinearAlgebra

Namespace GA.Models

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
        Public ReadOnly A, B As PointF

        Public Const NULL# = -1

        Public ReadOnly Property IsFullStack As Boolean
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return TailIndex = X.Length - 1
            End Get
        End Property

        Public ReadOnly Property MaxStackSize As Integer
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return X.Dim
            End Get
        End Property

        ''' <summary>
        ''' 返回路径点最后一个元素的顶点编号
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property TailIndex As Integer
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
        ''' 在当前的这个路径之中的节点的数量
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Length As Integer
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return TailIndex + 1
            End Get
        End Property

        Default Public ReadOnly Property Point(i As Integer) As PointF
            Get
                Return New PointF With {
                    .X = X(i),
                    .Y = Y(i)
                }
            End Get
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="maxStack%">最大的拐点数量</param>
        Sub New(anchor As (A As Point, B As Point), Optional maxStack% = 10)
            X = New Vector(NULL, maxStack)
            Y = New Vector(NULL, maxStack)
            A = anchor.A
            B = anchor.B
        End Sub

        Sub New(anchorA As PointF, anchorB As PointF, X As Vector, Y As Vector)
            Me.A = anchorA
            Me.B = anchorB
            Me.X = X
            Me.Y = Y
        End Sub

        Public Function GetX() As Double()
            Return X.Take(TailIndex + 1).ToArray
        End Function

        Public Function GetY() As Double()
            Return Y.Take(TailIndex + 1).ToArray
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function Copy() As Path
            Return New Path(A, B, X.ToArray, Y.ToArray)
        End Function

#Region "Mutations"
        Sub Mutate(random As Random)
            Dim index% = random.Next(TailIndex + 1)

            ' X Y 必须要同时突变？
            X.Array.Mutate(random, index)
            Y.Array.Mutate(random, index)
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
                With TailIndex + 1
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
                Dim X = GetX()
                Dim Y = GetY()

                Call X.InsertAt(point.X, index)
                Call Y.InsertAt(point.Y, index)

                For i As Integer = 0 To X.Length - 1
                    Me.X(i) = X(i)
                    Me.Y(i) = Y(i)
                Next
            End If
        End Sub

        ''' <summary>
        ''' 删除编号为<paramref name="index"/>的路径点
        ''' </summary>
        ''' <param name="index">
        ''' 删除掉这个点之后，后面的元素都会往前移
        ''' </param>
        Sub Delete(index As Integer)
            Dim X = GetX()
            Dim Y = GetY()

            Call X.Delete(index)
            Call Y.Delete(index)

            For i As Integer = 0 To X.Length - 1
                Me.X(i) = X(i)
                Me.Y(i) = Y(i)
            Next

            For i As Integer = X.Length To Me.X.Length - 1
                Me.X(i) = NULL
                Me.Y(i) = NULL
            Next
        End Sub
#End Region

#Region "Fitness"

        ''' <summary>
        ''' 将一些线段进行合并简化计算的模型
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' 主要是合并相同方向的相邻线段
        ''' </remarks>
        Public Iterator Function Shrink() As IEnumerable(Of PointF)
            Dim A As PointF = Me.A
            Dim B As PointF = New PointF(X(Scan0), Y(Scan0))
            Dim C As PointF
            Dim lastIndex% = TailIndex

            Yield A

            For i As Integer = 1 To lastIndex + 1
                If i = lastIndex + 1 Then
                    ' lastIndex + 1 实际上已经超过了范围了
                    ' 在这里将锚点B也放出去
                    C = Me.B
                Else
                    C = New PointF(X(i), Y(i))
                End If

                If Math.Abs(GeomTransform.CalculateAngle(A, B) - GeomTransform.CalculateAngle(B, C)) <= 0.00001 Then
                    ' 认为两条线段的角度是一样的，可以进行合并
                    ' 在本次循环之中不将中点B抛出，就可以产生合并的效果
                    ' Do Nothing
                Else
                    ' 两条线段不在同一个角度，则不可以进行合并
                    ' 将中点B抛出即可不进行合并
                    Yield B
                End If

                B = C
            Next

            ' 最后一个点是锚点B，必须要和锚点A一样被抛出
            Yield C
        End Function
#End Region

    End Class

End Namespace
