Imports System.Runtime.CompilerServices
Imports Galaxy.Data.TableSheet
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class FormNameSearch

    Private Sub FormNameSearch_Load(sender As Object, e As EventArgs) Handles Me.Load
        loader = New GridLoaderHandler(AdvancedDataGridView1, AdvancedDataGridViewSearchToolBar1)
    End Sub

    Dim loader As GridLoaderHandler
    Dim web As CellViewer
    Dim compounds As Compound()

    Public Sub SetData(web As CellViewer, compounds As IEnumerable(Of Compound))
        Me.web = web
        Me.compounds = compounds.ToArray

        For Each meta As Compound In Me.compounds
            Call DataGridView1.Rows.Add(meta.ID, meta.name, meta.formula, meta.db_xrefs.JoinBy("; "))
        Next
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Sub ShowReactionTable(links As IEnumerable(Of Reaction))
        Call loader.LoadTable(Sub(tbl) Call CellViewer.ShowReactionTable(tbl, links))
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

    End Sub
End Class