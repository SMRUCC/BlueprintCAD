Imports BlueprintCAD.UIData
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Math.LinearAlgebra.LinearProgramming
Imports SMRUCC.genomics.Analysis.FBA.Core
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model.Cellular

Public Class FormFBATool

    Dim model As CellularModule
    Dim search As CompoundSearchIndex

    Public Function LoadModel(cell As VirtualCell) As FormFBATool
        search = New CompoundSearchIndex(cell.metabolismStructure.compounds.OrderBy(Function(c) c.name), 6)
        model = ProgressSpinner.LoadData(Function() cell.CreateModel)
        ComboBox1.SelectedIndex = 0

        Call refreshList()

        Return Me
    End Function

    Private Sub refreshList()
        ListBox1.Items.Clear()

        For Each compound As Compound In search.AsEnumerable
            Call ListBox1.Items.Add(New IDDisplay With {.id = compound.ID, .name = compound.name})
        Next
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Strings.Trim(TextBox1.Text).Length = 0 Then
            Call refreshList()
        End If
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim text As String = Strings.Trim(TextBox1.Text)

        If text = "" Then
            Call refreshList()
        Else
            Dim find = Await Task.Run(Function() search.Search(text).ToArray)

            Call ListBox1.Items.Clear()

            For Each item In find.OrderBy(Function(a) a.name)
                Call ListBox1.Items.Add(New IDDisplay With {
                    .id = item.ID,
                    .name = item.name
                })
            Next
        End If
    End Sub

    Private Sub AddToObjectiveListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToObjectiveListToolStripMenuItem.Click
        If ListBox1.SelectedIndex < 0 Then
            Return
        End If

        Dim id As IDDisplay = ListBox1.SelectedItem

        Call ListBox2.Items.Add(id)
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        Call ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
    End Sub

    Private Iterator Function getTargets() As IEnumerable(Of String)
        For i As Integer = 0 To ListBox2.Items.Count - 1
            Yield DirectCast(ListBox2.Items(i), IDDisplay).id
        Next
    End Function

    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim engine As New LinearProgrammingEngine()
        Dim matrix As Matrix = Await Task.Run(Function() engine.CreateMatrix(model, getTargets.ToArray))
        Dim opt As OptimizationType = CType(ComboBox1.SelectedIndex, OptimizationType)
        Dim result As LPPSolution = Await Task.Run(Function() engine.Run(matrix, opt))

        TextBox2.Text = result.ObjectiveFunctionValue
        ListBox3.Items.Clear()

        For Each compound As NamedValue(Of Double) In result.GetSolution()
            Call ListBox3.Items.Add(New CompoundContentData With {
                .content = compound.Value,
                .id = compound.Name
            })
        Next

        MessageBox.Show("Run FBA task finished!", "Task Finished", MessageBoxButtons.OK, MessageBoxIcon.Information)

        TabControl1.SelectedTab = TabPage2
    End Sub
End Class