Imports BlueprintCAD.UIData
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class FormSetupCellularContents : Implements IDataContainer, IWizardUI

    Public ReadOnly Property Title As String Implements IWizardUI.Title
        Get
            Return "Cellular Contents"
        End Get
    End Property

    Dim wizard As Wizard
    Dim sel As CompoundContentData
    Dim data As Dictionary(Of String, CompoundContentData())
    Dim selCellKey As String
    Dim search As QGramIndex

    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        wizard = DirectCast(data, Wizard)

        For Each model As String In wizard.models.Keys
            Call ListBox1.Items.Add(model)
        Next
    End Sub

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizard
    End Function

    Public Function OK() As Boolean Implements IWizardUI.OK

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
        Dim model As VirtualCell = wizard.models(key).model
        Dim compounds As CompoundContentData() = data.ComputeIfAbsent(
            key, lazyValue:=Function(id)
                                Return model.metabolismStructure.compounds _
                                    .Select(Function(a)
                                                Return New CompoundContentData With {
                                                    .content = 0,
                                                    .id = a.ID,
                                                    .name = a.name
                                                }
                                            End Function) _
                                    .ToArray
                            End Function)

        Call ListBox2.Items.Clear()

        search = New QGramIndex(q:=3)

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
        Dim find = search.FindSimilar(str, 0.3).OrderByDescending(Function(a) a.similarity).Take(30).ToArray
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
End Class
