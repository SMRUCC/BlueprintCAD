Imports System.ComponentModel
Imports Galaxy.Data.JSON
Imports Galaxy.Data.JSON.Models
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualStudio.WinForms.Docking
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CellExplorer

    Dim model As VirtualCell
    Dim WithEvents viewer As JsonViewer

    Public Sub LoadModel(model As VirtualCell)
        Me.model = model
        ProgressSpinner.DoLoading(Sub() Call LoadCellComponents(), host:=Me)
    End Sub

    Private Sub LoadCellComponents()
        Dim tree As New JsonObject With {.Id = model.cellular_id}
        Dim metabolites As New JsonObject With {
            .Id = "metabolites",
            .JsonType = JsonType.Array,
            .Parent = tree
        }

        If Not model.metabolismStructure?.compounds Is Nothing Then
            Dim offset As i32 = 1

            For Each meta As Compound In model.metabolismStructure.compounds
                Dim obj As New JsonObject With {
                    .Id = $"[{++offset}]",
                    .JsonType = JsonType.Value,
                    .Parent = metabolites,
                    .Value = meta
                }

                Call metabolites.Fields.Add(obj)
            Next
        End If

        Call tree.Fields.Add(metabolites)

        viewer.Tag = model.cellular_id
        viewer.Render(New JsonObjectTree(tree))
    End Sub

    Private Sub CellExplorer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        DockState = DockState.DockLeftAutoHide
    End Sub

    Private Sub CellExplorer_Load(sender As Object, e As EventArgs) Handles Me.Load
        TabText = "Cell Explorer"

        viewer = New JsonViewer
        viewer.Dock = DockStyle.Fill

        Panel1.Controls.Add(viewer)

        ApplyVsTheme(ToolStrip1, viewer.GetContextMenu)
    End Sub
End Class