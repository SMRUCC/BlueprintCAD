Imports Microsoft.VisualBasic.Serialization.JSON
Imports VirtualCellHost

Public Class FormConfigGenerator

    Dim modelFiles As String()

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using file As New OpenFileDialog With {
            .Multiselect = True,
            .Filter = "Virtual Cell Model File(*.xml)|*.xml"
        }
            If file.ShowDialog = DialogResult.OK Then
                modelFiles = file.FileNames
                ListBox1.Items.Clear()
                ListBox1.Items.AddRange(modelFiles)
            End If
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using file As New SaveFileDialog With {.Filter = "Config JSON File(*.json)|*.json"}
            If file.ShowDialog = DialogResult.OK Then
                Dim config As New Config With {
                    .debug = False,
                    .models = modelFiles,
                    .tqdm_progress = True
                }

                Call config.GetJson.SaveTo(file.FileName)
            End If
        End Using
    End Sub
End Class