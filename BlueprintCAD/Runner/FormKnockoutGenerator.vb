Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Definitions
Imports VirtualCellHost

Public Class FormKnockoutGenerator

    ''' <summary>
    ''' current selected cell model
    ''' </summary>
    Dim cell As VirtualCell
    Dim wizard As Wizard

    Private Function LoadModelFiles()
        Dim cell As VirtualCell
        Dim compounds_id As New List(Of String)
        Dim copy As New Dictionary(Of String, Integer)

        For Each file As ModelFile In wizard.models.Values
            cell = file.model
            compounds_id.AddRange(cell.metabolismStructure.compounds.Keys)
            copy.Add(cell.cellular_id, 1000)
            ListBox1.Items.Add(cell)
        Next

        If ListBox1.Items.Count > 0 Then
            ListBox1.SelectedIndex = 0
        End If

        wizard.config.mapping = Definition.MetaCyc(compounds_id.Distinct, Double.NaN)
        wizard.config.copy_number = copy

        Return Me
    End Function

    Public Function LoadWizard(wizard As Wizard) As FormKnockoutGenerator
        Me.wizard = wizard
        Me.LoadModelFiles()

        Return Me
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim knockouts As New List(Of String)

        For i As Integer = 0 To ListBox4.Items.Count - 1
            Call knockouts.Add(DirectCast(ListBox4.Items(i), KnockoutGene).gene.locus_tag)
        Next

        wizard.config.knockouts = knockouts.Distinct.ToArray

        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex = -1 Then
            Return
        End If

        cell = ListBox1.SelectedItem

        ListBox2.Items.Clear()

        If cell.genome IsNot Nothing AndAlso Not cell.genome.replicons.IsNullOrEmpty Then
            For Each rep As replicon In cell.genome.replicons
                For Each gene As gene In rep.GetGeneList
                    Call ListBox2.Items.Add(gene)
                Next
            Next
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        Dim gene As New KnockoutGene With {
            .genome = cell.cellular_id,
            .gene = ListBox2.SelectedItem
        }

        Call ViewMetabolicNetwork(gene)
    End Sub

    Private Sub ListBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox4.SelectedIndexChanged
        If ListBox4.SelectedIndex < 0 Then
            Return
        End If

        Dim gene As KnockoutGene = ListBox4.SelectedItem

        Call ViewMetabolicNetwork(gene)
    End Sub

    Private Sub ViewMetabolicNetwork(gene As KnockoutGene)
        Dim cell As VirtualCell = wizard.models(gene.genome).model
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

    Private Sub AddToKnockoutListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToKnockoutListToolStripMenuItem.Click
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        Dim gene As New KnockoutGene With {
            .genome = cell.cellular_id,
            .gene = ListBox2.SelectedItem
        }

        Call ListBox4.Items.Add(gene)
    End Sub
End Class

Public Class KnockoutGene

    Public Property genome As String
    Public Property gene As gene

    Public Overrides Function ToString() As String
        Return $"[{genome}] {gene}"
    End Function

End Class