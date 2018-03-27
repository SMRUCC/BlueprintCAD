Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Math2D
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.Assembly.KEGG.WebServices

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
    Public Function ImportsMap(map As Map) As GA_AutoLayout
        ' blocks
        Dim blocks As Block() = map _
            .Areas _
            .Select(Function(a)
                        Dim block As Block

                        Select Case a.shape
                            Case "rect"
                                block = New rect With {
                                    .rectangle = New Rectangle With {
                                        .X = a.Rectangle.X,
                                        .Y = a.Rectangle.Y,
                                        .Width = a.Rectangle.Width,
                                        .Height = a.Rectangle.Height
                                    }
                                }
                            Case "circle"
                                block = New Circle With {
                                    .center = a.Rectangle.Centre.ToPoint,
                                    .radius = a.Rectangle.Width / 2
                                }
                            Case Else
                                Throw New NotImplementedException(a.GetJson)
                        End Select

                        Return block
                    End Function) _
            .ToArray

        Return New GA_AutoLayout With {
            .blocks = blocks
        }
    End Function
End Module
