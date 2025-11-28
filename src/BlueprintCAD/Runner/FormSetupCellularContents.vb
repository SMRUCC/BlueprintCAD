Imports BlueprintCAD.UIData
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports randf = Microsoft.VisualBasic.Math.RandomExtensions

Public Class FormSetupCellularContents : Implements IDataContainer, IWizardUI

    Public ReadOnly Property Title As String Implements IWizardUI.Title
        Get
            Return "Cellular Contents"
        End Get
    End Property

    Dim wizard As Wizard
    Dim sel As CompoundContentData
    Dim data As New Dictionary(Of String, CompoundContentData())
    Dim selCellKey As String
    Dim search As QGramIndex

    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        wizard = DirectCast(data, Wizard)
        ListBox1.Items.Clear()

        For Each model_id As String In wizard.models.Keys
            Dim copyNumber As Double = wizard.config.copy_number(model_id)
            Dim model = wizard.models(model_id).model

            If Not Me.data.ContainsKey(model_id) Then
                Dim compounds As CompoundContentData() = model.metabolismStructure.compounds _
                    .Select(Function(a)
                                Return New CompoundContentData With {
                                    .content = copyNumber,
                                    .id = a.ID,
                                    .name = a.name
                                }
                            End Function) _
                    .OrderBy(Function(a) a.name) _
                    .ToArray

                Call Me.data.Add(model_id, compounds)
            End If

            Call ListBox1.Items.Add(model_id)
        Next
    End Sub

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizard
    End Function

    Public Function OK() As Boolean Implements IWizardUI.OK
        For Each name As String In wizard.models.Keys
            wizard.config.mapping.status(name) = data(name).ToDictionary(Function(c) c.id, Function(c) c.content)
        Next

        Return True
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex < 0 Then
            Return
        End If

        Dim key As String = ListBox1.SelectedItem.ToString

        selCellKey = key
        refreshCompoundList(key)
    End Sub

    Private Sub refreshCompoundList(key As String)
        Dim compounds = data(key)

        Call ListBox2.Items.Clear()

        search = New QGramIndex(q:=6)

        For Each item As CompoundContentData In compounds
            Call search.AddString(item.name)
            Call ListBox2.Items.Add(item)
        Next
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        sel = ListBox2.SelectedItem
        TextBox2.Text = sel.content
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.StringEmpty AndAlso Not selCellKey.StringEmpty Then
            Call refreshCompoundList(selCellKey)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If selCellKey.StringEmpty OrElse TextBox1.Text.StringEmpty Then
            Return
        End If

        Dim str As String = Strings.Trim(TextBox1.Text)
        Dim find = search.FindSimilar(str, 0).OrderByDescending(Function(a) a.similarity).Take(30).ToArray
        Dim data = Me.data(selCellKey)

        Call ListBox2.Items.Clear()

        For Each hit As FindResult In find
            Call ListBox2.Items.Add(data(hit.index))
        Next
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If sel Is Nothing Then
            Return
        Else
            sel.content = Val(Strings.Trim(TextBox2.Text))
        End If
    End Sub

    Private Sub MakeRandomToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MakeRandomToolStripMenuItem.Click
        If ListBox1.SelectedIndex < 0 Then
            Return
        End If

        Dim cell_id As String = ListBox1.SelectedItem.ToString
        Dim list = data(cell_id)
        Dim upper As Double = wizard.config.copy_number(cell_id)

        For Each item As CompoundContentData In list
            item.content = randf.NextDouble * upper
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim data As Double = Val(Strings.Trim(TextBox2.Text))

        For Each item As CompoundContentData In ListBox2.Items
            item.content = data
        Next
    End Sub
End Class
