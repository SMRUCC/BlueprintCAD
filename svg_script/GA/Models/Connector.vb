Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Math

Namespace GA.Models

    ''' <summary>
    ''' 这个模块主要是计算突变之后的整条路径的整体布局的变化情况
    ''' </summary>
    Public Module ConnectorEffects

        ''' <summary>
        ''' 分配最小的节点数量构建一条新的路径
        ''' </summary>
        ''' <param name="anchor"></param>
        ''' <param name="minSize%">最少的节点数量</param>
        ''' <param name="rand"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 路径规定只有90度夹角
        ''' </remarks>
        <Extension>
        Public Function Allocate(anchor As (A As Point, B As Point), minSize%, rand As Random) As Path
            Dim points As New List(Of PointF)
            Dim previous As PointF = anchor.A.PointF
            Dim X, Y As Integer
            Dim dx = anchor.B.X - anchor.A.X
            Dim dy = anchor.B.Y - anchor.A.Y

            For i As Integer = 1 To minSize - 1
                Select Case rand.NextDouble
                    Case < 0.5
                        X = previous.X + dx * rand.NextDouble
                        Y = previous.Y
                    Case Else
                        X = previous.X
                        Y = previous.Y + dy * rand.NextDouble
                End Select

                points += New PointF(X, Y)
                previous = points.Last
            Next

            ' 最后一个连接点
            If rand.NextDouble < 0.5 Then
                X = previous.X
                Y = anchor.B.Y
            Else
                X = anchor.B.X
                Y = previous.Y
            End If

            points += New PointF(X, Y)

            Return New Path(anchor.A, anchor.B, points.X, points.Y)
        End Function

    End Module
End Namespace

