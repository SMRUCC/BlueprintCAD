Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CompoundSearchIndex : Implements Enumeration(Of Compound)

    ReadOnly compounds As Compound()
    ReadOnly qgram As QGramIndex

    Sub New(compounds As IEnumerable(Of Compound), qgram As Integer)
        Dim offset As Integer = 0

        Me.qgram = New QGramIndex(qgram)
        Me.compounds = compounds _
            .SafeQuery _
            .ToArray

        For Each compound As Compound In Me.compounds
            Call Me.qgram.AddString(compound.name, offset)

            For Each id As String In compound.db_xrefs.SafeQuery
                Call Me.qgram.AddString(id, offset)
            Next

            offset += 1
        Next
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Sub New(cell As VirtualCell, qgram As Integer)
        Call Me.New(cell.metabolismStructure.compounds, qgram)
    End Sub

    Public Iterator Function Search(text As String, Optional top As Integer = 30) As IEnumerable(Of Compound)
        Dim index = qgram.FindSimilar(text, 0) _
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

    Public Iterator Function GenericEnumerator() As IEnumerator(Of Compound) Implements Enumeration(Of Compound).GenericEnumerator
        For Each c As Compound In compounds
            Yield c
        Next
    End Function
End Class
