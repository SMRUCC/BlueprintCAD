Imports System.Drawing
Imports System.Runtime.CompilerServices

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
        <Extension>
        Public Function Allocate(anchor As (A As Point, B As Point), minSize%, rand As Random) As Path

        End Function

    End Module
End Namespace

