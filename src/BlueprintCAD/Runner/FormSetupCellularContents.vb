Imports BlueprintCAD.UIData
Imports Galaxy.Workbench

Public Class FormSetupCellularContents : Implements IDataContainer, IWizardUI

    Public ReadOnly Property Title As String Implements IWizardUI.Title
        Get
            Return "Cellular Contents"
        End Get
    End Property

    Dim wizard As Wizard
    Dim sel As CompoundContentData
    Dim data As Dictionary(Of String, CompoundContentData())

    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        wizard = DirectCast(data, Wizard)
    End Sub

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizard
    End Function

    Public Function OK() As Boolean Implements IWizardUI.OK

    End Function
End Class
