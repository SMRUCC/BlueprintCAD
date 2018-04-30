Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.Runtime
Imports SMRUCC.genomics.Assembly.KEGG.WebServices
Imports SMRUCC.genomics.Data.KEGG.Metabolism

''' <summary>
''' Imports KEGG pathway reference map data.
''' </summary>
Module KEGGImports

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="map">
    ''' + rectangle: KO(gene)
    ''' + circle: compound
    ''' </param>
    ''' <returns></returns>
    <Extension>
    Public Function ImportsMap(map As Map, links As ReactionLink, Optional scale# = 1.25) As GA_AutoLayout
        Dim size As Size = map.GetImage.Size
        ' blocks
        ' 主要解决的关系是代谢物之间的关系连接的布局信息
        Dim blocks As Block() = map _
            .Areas _
            .Select(Function(a)

                        ' 基因或者代谢物
                        Select Case a.shape
                            'Case "rect"
                            '    block = New rect With {
                            '        .rectangle = New Rectangle With {
                            '            .X = a.Rectangle.X,
                            '            .Y = a.Rectangle.Y,
                            '            .Width = a.Rectangle.Width,
                            '            .Height = a.Rectangle.Height
                            '        }
                            '    }
                            Case "circle"
                                Return a.IDVector _
                                        .Select(Function(id)
                                                    Return New Circle With {
                                                        .center = a.Rectangle.Centre.ToPoint,
                                                        .radius = a.Rectangle.Width / 2,
                                                        .ID = id
                                                    }.AsBaseType(Of Block)
                                                End Function)
                            Case Else
                                ' Throw New NotImplementedException(a.GetJson)
                                Return Nothing
                        End Select
                    End Function) _
            .IteratesALL _
            .Where(Function(b) b.ID.IsPattern("C\d+")) _
            .ToArray

        ' 将当前的这个map之中所有的代谢物拿出来
        Dim anchors As New Dictionary(Of String, (Point, Point))
        Dim compounds$() = map.Areas.Select(Function(a) a.IDVector).IteratesALL.ToArray
        Dim posTable = blocks.ToDictionary

        For Each id1 As String In compounds
            For Each id2 As String In compounds.Where(Function(id) id <> id1)
                Dim uid = {id1, id2}.OrderBy(Function(s) s).JoinBy("-")

                If Not anchors.ContainsKey(uid) AndAlso Not links.PopulateConversionLinks(id1, id2).IsNullOrEmpty Then
                    anchors(uid) = (posTable(id1).Location, posTable(id2).Location)
                End If
            Next
        Next

        Return New GA_AutoLayout With {
            .blocks = blocks,
            .size = size,
            .anchors = anchors.Values.ToArray
        } * scale
    End Function
End Module
