Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CellViewer

    Dim cell As VirtualCell

    Public Function LoadModel(file As String) As CellViewer
        cell = file.LoadXml(Of VirtualCell)
        Return Me
    End Function



End Class