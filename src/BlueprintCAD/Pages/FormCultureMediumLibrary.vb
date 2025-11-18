Imports CADRegistry
Imports Galaxy.Data.TableSheet
Imports Galaxy.Workbench.CommonDialogs
Imports Microsoft.VisualBasic.Data.Framework
Imports Microsoft.VisualBasic.MIME.Office.Excel
Imports SMRUCC.genomics.Assembly.MetaCyc.File.DataFiles

Public Class FormCultureMediumLibrary

    Dim edit As String
    Dim loader As GridLoaderHandler

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Call InputDialog.Input(Of FormInputName)(
            Sub(input)
                ToolStripComboBox1.Items.Add(input.GetTextData)
                ToolStripComboBox1.SelectedItem = input.GetTextData
                edit = input.GetTextData
                DataGridView1.Rows.Clear()
            End Sub)
    End Sub

    Private Sub FormCultureMediumLibrary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ApplyVsTheme(ToolStrip2)
    End Sub

    Protected Overrides Sub SaveDocument()
        Dim compounds As FormulaCompound() = ExportTable.ToArray
        Dim savefile As String = $"{App.ProductProgramData}/cultureMediu/{edit.NormalizePathString(False, replacement:="-")}.csv"

        Call compounds.SaveTo(savefile)
    End Sub

    Public Iterator Function ExportTable() As IEnumerable(Of FormulaCompound)
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Dim row As DataGridViewRow = DataGridView1.Rows(i)
            Dim met As New FormulaCompound With {
                .registry_id = CStr(row.HeaderCell.Value),
                .Name = CStr(row.Cells(0).Value),
                .formula = CStr(row.Cells(1).Value),
                .cas_id = CStr(row.Cells(2).Value),
                .kegg_id = CStr(row.Cells(3).Value),
                .biocyc_id = CStr(row.Cells(4).Value),
                .value = CDbl(row.Cells(5).Value)
            }

            Yield met
        Next
    End Function

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        If ToolStripComboBox1.SelectedIndex < 0 Then
            Return
        Else
            Call DataGridView1.Rows.Clear()
        End If

        Dim name As String = ToolStripComboBox1.SelectedItem.ToString
        Dim savefile As String = $"{App.ProductProgramData}/cultureMedium/{name.NormalizePathString(False, replacement:="-")}.csv"
        Dim data As FormulaCompound() = savefile.LoadCsv(Of FormulaCompound)().ToArray

        edit = name
        ImportsTable(data)
    End Sub

    Private Sub ImportsTable(data As IEnumerable(Of FormulaCompound))
        For Each met As FormulaCompound In data
            Dim offset = DataGridView1.Rows.Add(
                met.name, met.formula, met.cas_id, met.kegg_id, met.biocyc_id, met.value
            )

            DataGridView1.Rows(offset).HeaderCell.Value = met.registry_id
        Next

        DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If ToolStripComboBox1.SelectedIndex < 0 Then
            Return
        ElseIf MessageBox.Show($"Going to delete the culture medium formula: {ToolStripComboBox1.SelectedItem}?",
                               "Confirmed Action",
                               MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Warning) = DialogResult.OK Then

            Dim savefile As String = $"{App.ProductProgramData}/cultureMediu/{ToolStripComboBox1.SelectedItem.ToString.NormalizePathString(False, replacement:="-")}.csv"

            Call savefile.DeleteFile
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        Using file As New OpenFileDialog With {.Filter = "Excel Table(*.csv)|*.csv"}
            If file.ShowDialog = DialogResult.OK Then
                Dim data As FormulaCompound() = file.FileName.LoadCsv(Of FormulaCompound)().ToArray

                If data.Any AndAlso DataGridView1.Rows.Count > 0 Then
                    If MessageBox.Show("We found that there are some existed data inside the editor table, going to replace[YES] the data or merge[NO] with it?",
                                       "Check Operation",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Information) = DialogResult.Yes Then

                        Call DataGridView1.Rows.Clear()
                        Call ImportsTable(data)
                    Else
                        Call ImportsTable(data)
                    End If
                Else
                    Call DataGridView1.Rows.Clear()
                    Call ImportsTable(data)
                End If
            End If
        End Using
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Using file As New SaveFileDialog With {.Filter = "Excel Table(*.xlsx;*.csv)|*.xlsx;*.csv"}
            If file.ShowDialog = DialogResult.OK Then
                If file.FileName.ExtensionSuffix("csv") Then
                    Call ExportTable.SaveTo(file.FileName)
                Else
                    Call XLSX.SaveToExcel(ExportTable, file.FileName)
                End If
            End If
        End Using
    End Sub
End Class