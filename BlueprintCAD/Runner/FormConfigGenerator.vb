Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Definitions
Imports VirtualCellHost

Public Class FormConfigGenerator

    Friend ReadOnly wizardConfig As New Wizard

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using file As New OpenFileDialog With {
            .Multiselect = True,
            .Filter = "Virtual Cell Model File(*.xml)|*.xml"
        }
            If file.ShowDialog = DialogResult.OK Then
                wizardConfig.SetModelFiles(file.FileNames)
                ListBox1.Items.Clear()
                ListBox1.Items.AddRange(file.FileNames)
            End If
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using file As New SaveFileDialog With {.Filter = "Config JSON File(*.json)|*.json"}
            If file.ShowDialog = DialogResult.OK Then
                Dim config As New Config With {
                    .debug = False,
                    .models = wizardConfig.GetModelFiles.ToArray,
                    .tqdm_progress = True,
                    .iterations = NumericUpDown1.Value,
                    .resolution = NumericUpDown2.Value,
                    .kinetics = New FluxBaseline
                }

                wizardConfig.configFile = file.FileName
                wizardConfig.config = config

                Me.DialogResult = DialogResult.OK
            End If
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub
End Class