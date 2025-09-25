Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq

Public Class FormCultureMedium

    Dim wizard As Wizard

    Public Function SetConfigAndModels(wizard As Wizard) As FormCultureMedium
        For Each compound As String In wizard.models.Values.Select(Function(c) c.model.metabolismStructure.compounds).IteratesALL.Keys.Distinct
            Call ListBox1.Items.Add(compound)
        Next

        Return Me
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim data As New Dictionary(Of String, Double)

        For Each compound As CultureMediumCompound In ListBox2.Items
            data(compound.id) = compound.content
        Next

        wizard.config.cultureMedium = data

        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub AddToDCultureMediumToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToDCultureMediumToolStripMenuItem.Click
        If ListBox1.SelectedIndex < 0 Then
            Return
        End If

        Dim id As String = CStr(ListBox1.SelectedItem)
        Dim data As New CultureMediumCompound With {
            .id = id,
            .content = 1
        }

        Call ListBox2.Items.Add(data)
    End Sub

    Dim compound As CultureMediumCompound

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        compound = ListBox2.SelectedItem
        NumericUpDown1.Value = compound.content
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        compound.content = NumericUpDown1.Value
    End Sub

    Private Sub RemovesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemovesToolStripMenuItem.Click
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        Call ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        Call ListBox2.Items.Clear()
    End Sub
End Class

Public Class CultureMediumCompound

    Public Property id As String
    Public Property content As Double

    Public Overrides Function ToString() As String
        Return $"{id} ({content} mg/ml)"
    End Function

End Class