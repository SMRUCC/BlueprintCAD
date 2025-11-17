Imports Galaxy.ExcelPad
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.Data.Framework.IO
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D.Colors
Imports RibbonLib.Controls.Events

Module FileHandler

    Public Sub GlobalOpenFile(sender As Object, e As ExecuteEventArgs)
        Using file As New OpenFileDialog With {
            .Filter = {
                "NCBI GenBank Assembly(*.gb;*.gbff;*.gbk)|*.gb;*.gbff;*.gbk",
                "Excel Table File(*.csv)|*.csv"
            }.JoinBy("|")
        }
            If file.ShowDialog = DialogResult.OK Then
                Call GlobalOpenFile(file.FileName)
            End If
        End Using
    End Sub

    Private Sub GlobalOpenFile(filepath As String)
        Select Case filepath.ExtensionSuffix
            Case "csv" : Call OpenCSVTableFile(filepath)
            Case "gb", "gbff", "gbk" : Call OpenGenBankFile(filepath)
            Case Else

        End Select
    End Sub

    Private Sub OpenCSVTableFile(filepath As String)
        With CommonRuntime.ShowDocument(Of FormExcelPad)(, filepath.FileName)
            Call .SetCanvas(Workbench.chartPad, Designer.GetColors("paper").Select(Function(c) c.ToHtmlColor))
            Call ProgressSpinner.DoLoading(
                Sub()
                    Dim df As DataFrameResolver = DataFrameResolver.Load(filepath)

                    Call .LoadTable(Sub(tbl)
                                        For Each col In df.MeasureTypeSchema.Headers
                                            Call tbl.Columns.Add(col.Name, col.Value)
                                        Next

                                        Do While df.Read
                                            Call tbl.Rows.Add(df.GetRowData)
                                        Loop
                                    End Sub)
                End Sub)
        End With
    End Sub

    Private Sub OpenGenBankFile(filepath As String)

    End Sub
End Module
