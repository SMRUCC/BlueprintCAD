Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CellViewer

    Dim cell As VirtualCell
    Dim filepath As String

    Private Sub CellViewer_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub CellViewer_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub CellViewer_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Workbench.RestoreFormTitle()
    End Sub

    Private Sub CellViewer_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        If cell IsNot Nothing Then
            Workbench.SetFormActiveTitle($"Inspect: {cell.cellular_id}")
        End If
    End Sub

    Public Function LoadModel(file As String) As CellViewer
        filepath = file
        cell = file.LoadXml(Of VirtualCell)

        Call CellViewer_Activated(Nothing, Nothing)

        Return Me
    End Function

End Class