Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CompoundSearchIndex

    ReadOnly compounds As Compound()
    ReadOnly qgram As QGramIndex

    Sub New(cell As VirtualCell, qgram As Integer)
        Dim offset As Integer = 0

        Me.qgram = New QGramIndex(qgram)
        Me.compounds = cell.metabolismStructure.compounds _
            .SafeQuery _
            .ToArray

        For Each compound As Compound In compounds
            Call Me.qgram.AddString(compound.name, offset)

            For Each id As String In compound.db_xrefs.SafeQuery
                Call Me.qgram.AddString(id, offset)
            Next

            offset += 1
        Next
    End Sub

    Public Iterator Function Search(text As String, Optional top As Integer = 9) As IEnumerable(Of Compound)
        Dim index = qgram.FindSimilar(text, 0.6) _
            .OrderByDescending(Function(a) a.similarity) _
            .ToArray
        Dim hits = index.Select(Function(i) (i.similarity, i.index, compounds(i.index))) _
            .GroupBy(Function(a) a.index) _
            .OrderByDescending(Function(a) a.Max(Function(c) c.similarity)) _
            .Take(top) _
            .ToArray

        For Each hitResult In hits
            Yield hitResult.First.Item3
        Next
    End Function
End Class
