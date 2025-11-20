Imports System.Text
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Definitions

Public Class FormCultureMedium : Implements IDataContainer

    Dim wizardConfig As New Wizard
    Dim search As CompoundSearchIndex
    Dim membraneTransports As Dictionary(Of String, String())
    Dim allReactions As Dictionary(Of String, Reaction)

    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        Dim compounds_id As New List(Of String)
        Dim compoundSet = DirectCast(data, Wizard).models.Values _
            .Select(Function(c) c.model.metabolismStructure.compounds) _
            .ToArray
        Dim allCompounds = compoundSet _
            .IteratesALL _
            .GroupBy(Function(c) c.ID) _
            .Select(Function(c) c.First) _
            .ToArray

        allReactions = DirectCast(data, Wizard).models.Values _
            .Select(Function(c) c.model.metabolismStructure.reactions.AsEnumerable) _
            .IteratesALL _
            .GroupBy(Function(r) r.ID) _
            .ToDictionary(Function(r) r.Key,
                          Function(r)
                              Return r.First
                          End Function)

        wizardConfig = DirectCast(data, Wizard)
        search = New CompoundSearchIndex(FilterMembraneTransportMetabolites(allCompounds), 3)

        For Each compound As Compound In search.AsEnumerable
            Call compounds_id.Add(compound.ID)
            Call ListBox1.Items.Add(New IDDisplay With {.id = compound.ID, .name = compound.name})
        Next

        wizardConfig.config.mapping = Definition.MetaCyc(compounds_id.Distinct, Double.NaN)
    End Sub

    Private Iterator Function FilterMembraneTransportMetabolites(allCompounds As Compound()) As IEnumerable(Of Compound)
        Dim transports As String() = wizardConfig.models.Values _
            .Select(Function(c) c.model.metabolismStructure.reactions.transportation) _
            .IteratesALL _
            .Distinct _
            .ToArray
        Dim transportReactions = transports _
            .Where(Function(id) allReactions.ContainsKey(id)) _
            .Select(Function(id) allReactions(id)) _
            .ToArray
        Dim compoundSet As Index(Of String) = transportReactions _
            .Select(Function(r) r.substrate.JoinIterates(r.product)) _
            .IteratesALL _
            .Select(Function(c) c.compound) _
            .Indexing

        For Each compound As Compound In allCompounds
            If compound.ID Like compoundSet Then
                Yield compound
            End If
        Next
    End Function

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizardConfig
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim data As New Dictionary(Of String, Double)

        For Each compound As CultureMediumCompound In ListBox2.Items
            data(compound.id) = compound.content
        Next

        wizardConfig.config.cultureMedium = data

        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub AddToDCultureMediumToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToDCultureMediumToolStripMenuItem.Click
        If ListBox1.SelectedIndex < 0 Then
            Return
        End If

        Dim id As IDDisplay = DirectCast(ListBox1.SelectedItem, IDDisplay)
        Dim data As New CultureMediumCompound With {
            .id = id.id,
            .content = 1,
            .name = id.name
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
        If Not compound Is Nothing Then
            compound.content = NumericUpDown1.Value
        End If
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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.StringEmpty Then
            Call ListBox1.Items.Clear()

            For Each item In search.AsEnumerable
                Call ListBox1.Items.Add(New IDDisplay With {
                    .id = item.ID,
                    .name = item.name
                })
            Next
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim text As String = Strings.Trim(TextBox1.Text)
        Dim find = search.Search(text).ToArray

        Call ListBox1.Items.Clear()

        For Each item In find
            Call ListBox1.Items.Add(New IDDisplay With {
                .id = item.ID,
                .name = item.name
            })
        Next
    End Sub

    Private Sub FormCultureMedium_Load(sender As Object, e As EventArgs) Handles Me.Load
        RichTextBox1.Rtf = Encoding.UTF8.GetString(My.Resources.HelpDocs.SetupCultureMedium)
    End Sub
End Class

Public Class CultureMediumCompound

    Public Property id As String
    Public Property content As Double
    Public Property name As String

    Public Overrides Function ToString() As String
        Return $"{If(name, id)} ({content} mg/ml)"
    End Function

End Class