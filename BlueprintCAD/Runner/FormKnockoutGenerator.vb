Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports VirtualCellHost

Public Class FormKnockoutGenerator

    Dim file As String
    Dim config As Config
    Dim models As New Dictionary(Of String, VirtualCell)

    Public Function LoadModelFiles(files As IEnumerable(Of String)) As FormKnockoutGenerator
        Dim cell As VirtualCell

        For Each file As String In files
            cell = file.LoadXml(Of VirtualCell)
            models(cell.cellular_id) = cell
            ListBox1.Items.Add(cell)
        Next

        Return Me
    End Function

    Public Function LoadConfig(file As String) As FormKnockoutGenerator
        Me.file = file
        Me.config = file.LoadJsonFile(Of Config)
        Return Me
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex = -1 Then
            Return
        End If

        Dim cell As VirtualCell = ListBox1.SelectedItem

        ListBox2.Items.Clear()

        If cell.genome IsNot Nothing AndAlso Not cell.genome.replicons.IsNullOrEmpty Then
            For Each rep As replicon In cell.genome.replicons
                For Each gene As gene In rep.GetGeneList
                    Call ListBox2.Items.Add(gene)
                Next
            Next
        End If
    End Sub
End Class