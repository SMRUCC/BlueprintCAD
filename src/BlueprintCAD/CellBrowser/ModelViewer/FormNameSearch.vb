Imports System.Runtime.CompilerServices
Imports Galaxy.Data.TableSheet
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class FormNameSearch

    Private Sub FormNameSearch_Load(sender As Object, e As EventArgs) Handles Me.Load
        loader = New GridLoaderHandler(AdvancedDataGridView1, AdvancedDataGridViewSearchToolBar1)
    End Sub

    Dim loader As GridLoaderHandler

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Sub ShowReactionTable(links As IEnumerable(Of Reaction))
        Call loader.LoadTable(Sub(tbl) Call CellViewer.ShowReactionTable(tbl, links))
    End Sub
End Class