Public Class FormSetupKinetics

    Dim wizard As Wizard

    Public Function LoadConfig(wizard As Wizard) As FormSetupKinetics
        Me.wizard = wizard
        Return Me
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.DialogResult = DialogResult.OK
    End Sub
End Class