Imports Galaxy.Workbench
Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Definitions
Imports VirtualCellHost

Public Class FormConfigGenerator : Implements IDataContainer, IWizardUI

    Dim wizardConfig As Wizard

    Public ReadOnly Property Title As String Implements IWizardUI.Title
        Get
            Return "Basic Configuration"
        End Get
    End Property

    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        wizardConfig = DirectCast(data, Wizard)
    End Sub

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizardConfig
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using file As New OpenFileDialog With {
            .Multiselect = True,
            .Filter = "Virtual Cell Model File(*.xml)|*.xml"
        }
            If file.ShowDialog = DialogResult.OK Then
                wizardConfig.SetModelFiles(file.FileNames)
                DataGridView1.Rows.Clear()

                For Each model In wizardConfig.models.Values
                    Dim tax = model.model.taxonomy

                    Call DataGridView1.Rows.Add(If(tax?.scientificName, "no name"),
                                                model.model.cellular_id,
                                                model.filepath)
                Next

                Call DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        End Using
    End Sub

    Public Function OK() As Boolean Implements IWizardUI.OK
        If Not wizardConfig.GetModelFiles.Any Then
            Call MessageBox.Show("No virtual cell model was selected, select one or more virtual cell model from file selection.",
                                 "No model data",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning)
            Return False
        End If

        Using file As New SaveFileDialog With {.Filter = "Config JSON File(*.json)|*.json"}
            If file.ShowDialog = DialogResult.OK Then
                Dim config As New Config With {
                    .debug = False,
                    .models = wizardConfig.GetModelFiles.ToArray,
                    .tqdm_progress = True,
                    .iterations = NumericUpDown1.Value,
                    .resolution = NumericUpDown2.Value,
                    .kinetics = New FluxBaseline With {
                        .boost = NumericUpDown3.Value
                    }
                }

                wizardConfig.configFile = file.FileName
                wizardConfig.config = config

                Return True
            Else
                Return False
            End If
        End Using
    End Function
End Class