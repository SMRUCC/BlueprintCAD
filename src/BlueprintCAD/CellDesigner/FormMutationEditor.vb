Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
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
            UpdateTargetView()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If Not target Is Nothing Then
            target.knockout = CheckBox1.Checked
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each item As MutationEdit In ListBox2.Items
            ' avoid duplicated item
            If item.gene.locus_tag = target.gene.locus_tag Then
                Return
            End If
        Next

        Call ListBox2.Items.Add(target)
    End Sub

    Private Sub ViewMetabolicNetwork(gene As MutationEdit)
        Dim cell As VirtualCell = model
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

        If cell.genome.proteins.IsNullOrEmpty Then
            Call CommonRuntime.Warning("No protein model data for inspect of the impact metabolic network.")
        End If

        Call DataGridView1.Rows.Clear()

        For Each impact As Reaction In proteinList _
            .Select(Function(prot)
                        Return cell.metabolismStructure.GetImpactedMetabolicNetwork(prot.protein_id)
                    End Function) _
            .IteratesALL _
            .Distinct

            Dim offset = DataGridView1.Rows.Add(impact.name,
                                                impact.equation,
                                                impact.substrate.Select(Function(a) a.compound).JoinBy(", "),
                                                impact.product.Select(Function(a) a.compound).JoinBy(", "))

            DataGridView1.Rows(offset).HeaderCell.Value = impact.ID
        Next
    End Sub

    ''' <summary>
    ''' create updated model and then save the model file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim knockouts As New List(Of String)

        For Each edit As MutationEdit In ListBox2.Items
            If edit.knockout Then
                Call knockouts.Add(edit.gene.locus_tag)
            Else
                edit.gene.expression_level = edit.expression_level
            End If
        Next

        updated = model.DeleteMutation(knockouts.ToArray)

        Call SaveDocument()
    End Sub

    Private Sub FormMutationEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call ApplyVsTheme(ToolStrip1)
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If Not target Is Nothing Then
            target.expression_level = NumericUpDown1.Value
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex > -1 Then
            target = ListBox2.SelectedItem
            UpdateTargetView()
        End If
    End Sub

    Private Sub UpdateTargetView()
        NumericUpDown1.Value = target.expression_level
        CheckBox1.Checked = target.knockout
        ViewMetabolicNetwork(target)
    End Sub
End Class

Public Class MutationEdit

    Public Property gene As gene
    Public Property knockout As Boolean = False
    Public Property expression_level As Double = 1

    Public Overrides Function ToString() As String
        If knockout Then
            Return $"Knockout [{gene}]"
        ElseIf expression_level > 1 Then
            Return $"Overexpress [{gene}] = {expression_level}"
        Else
            Return $"Suppressexpress [{gene}] = {expression_level}"
        End If
    End Function

End Class