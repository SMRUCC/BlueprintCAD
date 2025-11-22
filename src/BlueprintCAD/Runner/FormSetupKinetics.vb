Imports Galaxy.Workbench

Public Class FormSetupKinetics : Implements IDataContainer, IWizardUI

    Dim wizard As Wizard
    Public ReadOnly Property Title As String Implements IWizardUI.Title
        Get
            Return Text
        End Get
    End Property
    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        wizard = data
    End Sub

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizard
    End Function

    Public Function OK() As Boolean Implements IWizardUI.OK
        Return True
    End Function
End Class