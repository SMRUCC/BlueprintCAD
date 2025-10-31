Imports System.Windows.Controls
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class FormMutationEditor

    Dim modifiedFile As String
    Dim model As VirtualCell
    Dim updated As VirtualCell

    Public Function LoadModel(file As String) As FormMutationEditor
        Call ProgressSpinner.DoLoading(Sub() model = file.LoadXml(Of VirtualCell))

        For Each gene As gene In model.genome.GetAllGenes
            Call ListBox1.Items.Add(gene)
        Next

        Return Me
    End Function

    Protected Overrides Sub SaveDocument()
        If modifiedFile.StringEmpty Then
            Using file As New SaveFileDialog With {
                .Filter = "GCModeller Virtual Cell Model File(*.xml)|*.xml"
            }
                If file.ShowDialog = DialogResult.OK Then
                    modifiedFile = file.FileName
                End If
            End Using
        End If

        If Not modifiedFile.StringEmpty Then
            Call updated.GetXml.SaveTo(modifiedFile)
        End If
    End Sub

    Dim target As MutationEdit

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex > -1 Then
            target = New MutationEdit With {.gene = ListBox1.SelectedItem}
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If Not target Is Nothing Then
            target.knockout = CheckBox1.Checked
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call ListBox2.Items.Add(target)
    End Sub

    Private Sub ViewMetabolicNetwork(gene As KnockoutGene)
        Dim cell As VirtualCell = wizardConfig.models(gene.genome).model
        Dim proteins As String() = gene.gene.protein_id
        Dim visited As New Index(Of String)
        Dim proteinList As protein() = proteins _
            .SafeQuery _
            .Select(Function(id)
                        Return protein.ProteinRoutine(cell.genome.proteins, id, visited)
                    End Function) _
            .IteratesALL _
            .Distinct _
            .ToArray

        Call ListBox3.Items.Clear()

        For Each impact As Reaction In proteinList _
            .Select(Function(prot)
                        Return cell.metabolismStructure.GetImpactedMetabolicNetwork(prot.protein_id)
                    End Function) _
            .IteratesALL _
            .Distinct

            Call ListBox3.Items.Add(impact)
        Next
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim knockouts As New List(Of String)

        For Each edit As MutationEdit In ListBox2.Items
            If edit.knockout Then
                Call knockouts.Add(edit.gene.locus_tag)
            End If
        Next

        updated = model.DeleteMutation(knockouts.ToArray)
    End Sub
End Class

Public Class MutationEdit

    Public Property gene As gene
    Public Property knockout As Boolean = False
    Public Property expression_level As Double = 1

End Class