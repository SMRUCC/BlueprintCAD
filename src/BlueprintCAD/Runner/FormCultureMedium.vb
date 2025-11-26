Imports System.Text
Imports BlueprintCAD.UIData
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Definitions

Public Class FormCultureMedium : Implements IDataContainer, IWizardUI

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
        search = New CompoundSearchIndex(FilterMembraneTransportMetabolites(allCompounds).OrderBy(Function(c) c.name), 3)
        ListBox1.Items.Clear()

        For Each compound As Compound In search.AsEnumerable
            Call compounds_id.Add(compound.ID)
            Call ListBox1.Items.Add(New IDDisplay With {.id = compound.ID, .name = compound.name})
        Next

        If wizardConfig.config.mapping Is Nothing Then
            wizardConfig.config.mapping = Definition.MetaCyc((From c As Compound In allCompounds Select c.ID), Double.NaN)
        End If
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
            .JoinIterates(allReactions.Values.Where(Function(r) r.CheckTransportation)) _
            .ToArray
        Dim compoundSet As Dictionary(Of String, String()) = transportReactions _
            .Select(Function(r) r.substrate.JoinIterates(r.product).Select(Function(c) (c.compound, r.ID))) _
            .IteratesALL _
            .GroupBy(Function(c) c.compound) _
            .ToDictionary(Function(c) c.Key,
                          Function(c)
                              Return c.Select(Function(a) a.ID).Distinct.ToArray
                          End Function)

        membraneTransports = compoundSet

        For Each compound As Compound In allCompounds
            If compoundSet.ContainsKey(compound.ID) Then
                Yield compound
            End If
        Next
    End Function

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizardConfig
    End Function

    Private Sub AddToDCultureMediumToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToDCultureMediumToolStripMenuItem.Click
        If ListBox1.SelectedIndex < 0 Then
            Return
        End If

        Dim id As IDDisplay = DirectCast(ListBox1.SelectedItem, IDDisplay)
        Dim data As New CompoundContentData With {
            .id = id.id,
            .content = 1,
            .name = id.name
        }

        Call ListBox2.Items.Add(data)
    End Sub

    Dim compound As CompoundContentData
    Public ReadOnly Property Title As String Implements IWizardUI.Title
        Get
            Return "Culture Medium Compounds"
        End Get
    End Property
    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        compound = ListBox2.SelectedItem
        NumericUpDown1.Value = compound.content

        Call showTransportNetwork(compound.id)
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
            Call reloadAll()
        End If
    End Sub

    Private Sub reloadAll()
        Call ListBox1.Items.Clear()

        For Each item In search.AsEnumerable
            Call ListBox1.Items.Add(New IDDisplay With {
                .id = item.ID,
                .name = item.name
            })
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim text As String = Strings.Trim(TextBox1.Text)

        If text = "" Then
            Call reloadAll()
        Else
            Dim find = search.Search(text).ToArray

            Call ListBox1.Items.Clear()

            For Each item In find.OrderBy(Function(a) a.name)
                Call ListBox1.Items.Add(New IDDisplay With {
                    .id = item.ID,
                    .name = item.name
                })
            Next
        End If
    End Sub

    Private Sub FormCultureMedium_Load(sender As Object, e As EventArgs) Handles Me.Load
        RichTextBox1.Rtf = Encoding.UTF8.GetString(My.Resources.HelpDocs.SetupCultureMedium)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim id As IDDisplay = DirectCast(ListBox1.SelectedItem, IDDisplay)

        Call showTransportNetwork(id.id)
    End Sub

    Private Sub showTransportNetwork(cid As String)
        Dim reaction_ids As String() = membraneTransports.TryGetValue(cid)

        Call DataGridView1.Rows.Clear()

        If Not reaction_ids.IsNullOrEmpty Then
            For Each rid As String In reaction_ids
                If allReactions.ContainsKey(rid) Then
                    Dim model As Reaction = allReactions(rid)

                    Call DataGridView1.Rows.Add(model.name, model.ec_number.JoinBy(", "))
                End If
            Next
        End If
    End Sub

    Public Function OK() As Boolean Implements IWizardUI.OK
        Dim data As New Dictionary(Of String, Double)

        For Each compound As CompoundContentData In ListBox2.Items
            data(compound.id) = compound.content
        Next

        wizardConfig.config.cultureMedium = data

        Return True
    End Function
End Class

