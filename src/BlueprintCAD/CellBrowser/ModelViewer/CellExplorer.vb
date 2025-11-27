Imports System.ComponentModel
Imports Galaxy.Workbench
Imports Microsoft.VisualStudio.WinForms.Docking
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CellExplorer

    Dim model As VirtualCell

    Public Sub LoadModel(model As VirtualCell)
        Me.model = model
        ProgressSpinner.DoLoading(Sub() Call LoadCellComponents())
    End Sub

    Private Sub LoadCellComponents()

    End Sub

    Private Sub CellExplorer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        DockState = DockState.DockLeftAutoHide
    End Sub
End Class