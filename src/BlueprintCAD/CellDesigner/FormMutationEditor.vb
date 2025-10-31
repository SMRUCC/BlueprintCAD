Imports Galaxy.Workbench
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class FormMutationEditor

    Dim modifiedFile As String
    Dim model As VirtualCell

    Public Function LoadModel(file As String) As FormMutationEditor
        Call ProgressSpinner.DoLoading(Sub() model = file.LoadXml(Of VirtualCell))

        For Each gene As gene In model.genome.GetAllGenes
            Call ListBox1.Items.Add(gene)
        Next

        Return Me
    End Function

    Protected Overrides Sub SaveDocument()
        If modifiedFile.StringEmpty Then
            Using file As New SaveFileDialog With {.Filter = "GCModeller Virtual Cell Model File(*.xml)|*.xml"}
                If file.ShowDialog = DialogResult.OK Then
                    modifiedFile = file.FileName
                End If
            End Using
        End If

        If Not modifiedFile.StringEmpty Then
            Call model.GetXml.SaveTo(modifiedFile)
        End If
    End Sub

End Class