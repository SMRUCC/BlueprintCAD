Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Math
Imports Microsoft.VisualBasic.Math.LinearAlgebra

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

        ''' <summary>
        ''' 移动路径之中的某一个节点，然后会连带的也移动相邻的节点
        ''' </summary>
        ''' <param name="path"></param>
        ''' <param name="rand"></param>
        ''' <returns></returns>
        <Extension>
        Public Function MovePoint(path As Path, rand As Random) As Path
            Dim i% = rand.Next(1, path.Length - 2)
            Dim routeX As Vector = path.X.ToArray
            Dim routeY As Vector = path.Y.ToArray
            Dim point As New PointF With {
                .X = routeX(i),
                .Y = routeY(i)
            }

            ' 需要一起被移动的节点
            Dim j%

            If rand.NextDouble < 0.5 Then
                ' 移动X的时候，与其垂直的节点也需要移动X
                ' 相邻的节点应该是X相同Y不同

                If routeX(i - 1) = point.X Then
                    j = i - 1
                Else
                    j = i + 1
                End If

                ' i移动X之后需要调整j的x一致
                Dim dx% = rand.NextDouble * (path.B.X - path.A.X) * If(rand.NextDouble > 0.5, 1, -1)
                routeX(i) += dx
                routeX(j) = routeX(i)

            Else
                ' 移动Y的时候，与其水平的节点也需要移动Y
                ' 相邻的节点应该是X不同Y相同

                If routeY(i - 1) = point.Y Then
                    j = i - 1
                Else
                    j = i + 1
                End If

                ' i移动Y之后需要调整j的y一致
                Dim dy% = rand.NextDouble * (path.B.Y - path.A.Y) * If(rand.NextDouble > 0.5, 1, -1)
                routeY(i) += dy
                routeY(j) = routeY(i)

            End If

            ' 返回新的突变之后的路径
            Return New Path(path.A, path.B, routeX, routeY)
        End Function


    End Module
End Namespace

