Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Definitions

Public Class FormKnockoutGenerator : Implements IDataContainer

    ''' <summary>
    ''' current selected cell model
    ''' </summary>
    Dim cell As VirtualCell

    Dim wizardConfig As New Wizard

    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        wizardConfig = DirectCast(data, Wizard)
        LoadModelFiles()
    End Sub

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizardConfig
    End Function

    Private Function LoadModelFiles()
        Dim cell As VirtualCell
        Dim compounds_id As New List(Of String)
        Dim copy As New Dictionary(Of String, Integer)

        For Each file As ModelFile In wizardConfig.models.Values
            cell = file.model
            compounds_id.AddRange(cell.metabolismStructure.compounds.Keys)
            copy.Add(cell.cellular_id, 1000)
            ListBox1.Items.Add(cell)
        Next

        If ListBox1.Items.Count > 0 Then
            ListBox1.SelectedIndex = 0
        End If

        wizardConfig.config.mapping = Definition.MetaCyc(compounds_id.Distinct, Double.NaN)
        wizardConfig.config.copy_number = copy

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

        wizardConfig.config.knockouts = knockouts.Distinct.ToArray

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

    Private Sub FormKnockoutGenerator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Rtf = "{\rtf1\ansi\ansicpg936\deff0\nouicompat\deflang1033\deflangfe2052{\fonttbl{\f0\fnil\fcharset0 Cambria;}}
{\colortbl ;\red0\green77\blue187;}
{\*\generator Riched20 10.0.20348}\viewkind4\uc1 
\pard 
{\pntext\f0 I.\tab}{\*\pn\pnlvlbody\pnf0\pnindent0\pnstart1\pnucrm{\pntxta.}}
\fi-360\li720\sa200\sl276\slmult1\f0\fs22\lang9 First, \cf1\b select the target genome model\cf0\b0 . The program will then display a list of all genes in the selected genome. \par
{\pntext\f0 II.\tab}Click on a specific gene, and the affected biochemical reaction network will be listed below to assist in the preliminary assessment of the gene knockout effect. \par
{\pntext\f0 III.\tab}After selecting a gene, use the ""\cf1\b Add to Gene Knockout List\cf0\b0 "" option from the right-click context menu to add the selected gene to the knockout list for subsequent modification of the virtual cell model.\par
}"
    End Sub
End Class

Public Class KnockoutGene

    Public Property genome As String
    Public Property gene As gene

    Public Overrides Function ToString() As String
        Return $"[{genome}] {gene}"
    End Function

End Class