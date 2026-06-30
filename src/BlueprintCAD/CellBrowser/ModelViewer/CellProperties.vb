Imports Galaxy.Data.TableSheet
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CellProperties

    Dim loader As GridLoaderHandler

    Public Sub ShowModelProperties(cell As VirtualCell)
        Dim transports = cell.metabolismStructure.reactions.transportation.Keys.Indexing
        Dim inputs As New List(Of String)
        Dim outputs As New List(Of String)
        Dim compoundIndex = cell.metabolismStructure.compounds.ToDictionary(Function(a) a.ID)

        For Each rxn As Reaction In cell.metabolismStructure.reactions.AsEnumerable
            Call inputs.AddRange(From m In rxn.substrate Select m.compound)
            Call outputs.AddRange(From n In rxn.product Select n.compound)
        Next
        For Each id As String In cell.traits.phenotype.SafeQuery
            Call ListBox1.Items.Add(id)
        Next

        Dim compounds = inputs _
            .Select(Function(id) (id, "I")) _
            .JoinIterates(outputs.Select(Function(id) (id, "O"))) _
            .GroupBy(Function(i) i.id) _
            .Where(Function(a) compoundIndex.ContainsKey(a.Key)) _
            .Select(Function(i)
                        Return (compoundIndex(i.Key), i.Select(Function(a) a.Item2).Distinct.OrderBy(Function(chr) chr).JoinBy("/"))
                    End Function) _
            .ToArray

        Call loader.LoadTable(
            Sub(tbl)
                Call tbl.Columns.Add("Cell I/O", GetType(String))
                Call tbl.Columns.Add("ID", GetType(String))
                Call tbl.Columns.Add("name", GetType(String))
                Call tbl.Columns.Add("formula", GetType(String))
                Call tbl.Columns.Add("db_xrefs", GetType(String))

                For Each c As (meta As Compound, io As String) In compounds
                    Dim meta = c.meta

                    Call tbl.Rows.Add(c.io, meta.ID, meta.name, meta.formula, meta.db_xrefs.JoinBy("; "))
                Next
            End Sub)
    End Sub

    Private Sub CellProperties_Load(sender As Object, e As EventArgs) Handles Me.Load
        loader = New GridLoaderHandler(AdvancedDataGridView1, AdvancedDataGridViewSearchToolBar1)
    End Sub
End Class