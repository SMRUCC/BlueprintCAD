Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports VirtualCellHost

Public Class FormCultureMedium

    Public ReadOnly Property config As Config
    Public ReadOnly Property models As VirtualCell()

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="config"></param>
    ''' <param name="models"></param>
    ''' <returns></returns>
    Public Function SetConfigAndModels(config As Config, models As VirtualCell()) As FormCultureMedium
        _config = config
        _models = models

        Return Me
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Me.DialogResult = DialogResult.OK
    End Sub
End Class

Public Class CultureMediumCompound

    Public Property id As String
    Public Property content As Double

    Public Overrides Function ToString() As String
        Return $"{id} ({content} mg/ml)"
    End Function

End Class