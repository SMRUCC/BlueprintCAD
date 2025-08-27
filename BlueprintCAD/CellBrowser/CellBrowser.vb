Imports System.IO
Imports SMRUCC.genomics.GCModeller.ModellingEngine.IO

Public Class CellBrowser

    Dim vcellPack As Raw.Reader

    Private Sub OpenVirtualCellDataFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenVirtualCellDataFileToolStripMenuItem.Click
        Using file As New OpenFileDialog With {.Filter = "Virtual Cell Data Pack(*.vcellPack)|*.vcellPack"}
            If file.ShowDialog = DialogResult.OK Then
                If vcellPack IsNot Nothing Then
                    Call vcellPack.Dispose()
                End If

                vcellPack = New Raw.Reader(file.FileName.Open(FileMode.Open, doClear:=False, [readOnly]:=True))
                Text = $"VirtualCell Browser [{file.FileName}]"
            End If
        End Using
    End Sub

    Private Sub CellBrowser_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If vcellPack IsNot Nothing Then
            Call vcellPack.Dispose()
        End If
    End Sub
End Class