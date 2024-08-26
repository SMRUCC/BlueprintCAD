Imports Microsoft.VisualBasic.Data.visualize.Network.Graph

Public Class FormNavigator

    Public Property Pad As GraphPad

    Public Sub SetNodeTarget(node As Node)
        PropertyGrid1.SelectedObject = node.data
        PropertyGrid1.Refresh()
    End Sub
End Class