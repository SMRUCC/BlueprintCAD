Imports Galaxy.Workbench

Public Class FormCellCopyNumber : Implements IDataContainer, IWizardUI

    Dim copyNum As New Dictionary(Of String, Integer)
    Dim model As String
    Dim wizardConfig As New Wizard

    Public ReadOnly Property Title As String Implements IWizardUI.Title
        Get
            Return "Cell Copy Numbers"
        End Get
    End Property

    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        wizardConfig = DirectCast(data, Wizard)
        ListBox1.Items.Clear()

        For Each model As String In wizardConfig.models.Keys
            Call ListBox1.Items.Add(model)

            If Not copyNum.ContainsKey(model) Then
                Call copyNum.Add(model, 1000)
            End If
        Next
    End Sub

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizardConfig
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex < 0 Then
            Return
        Else
            model = CStr(ListBox1.SelectedItem)
        End If

        NumericUpDown1.Value = copyNum(model)
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If model.StringEmpty() Then
            Return
        Else
            copyNum(model) = NumericUpDown1.Value
        End If
    End Sub

    Private Sub FormCellCopyNumber_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Rtf = "{\rtf1\ansi\ansicpg936\deff0\nouicompat\deflang1033\deflangfe2052{\fonttbl{\f0\fnil\fcharset0 Cambria;}}
{\colortbl ;\red0\green77\blue187;}
{\*\generator Riched20 10.0.20348}\viewkind4\uc1 
\pard\sa200\sl276\slmult1\b\f0\fs22\lang9 Copy number\cf1\u8203? \b0 refers to the quantity of a specific gene, plasmid, or genomic region present within a single cell's genome . \ul It is a fundamental concept in genetics and molecular biology, describing how many copies of a particular DNA sequence exist.\par
}"
    End Sub

    Public Function OK() As Boolean Implements IWizardUI.OK
        wizardConfig.config.copy_number = copyNum
        Return True
    End Function
End Class