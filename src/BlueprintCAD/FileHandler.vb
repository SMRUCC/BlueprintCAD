Imports RibbonLib.Controls.Events

Module FileHandler

    Public Sub GlobalOpenFile(sender As Object, e As ExecuteEventArgs)
        Using file As New OpenFileDialog With {
            .Filter = {
                "NCBI GenBank Assembly(*.gb;*.gbff;*.gbk)|*.gb;*.gbff;*.gbk"
            }.JoinBy("|")
        }
            If file.ShowDialog = DialogResult.OK Then

            End If
        End Using
    End Sub

End Module
