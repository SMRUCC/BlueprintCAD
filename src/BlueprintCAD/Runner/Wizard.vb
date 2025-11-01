Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.MIME.application.json
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports VirtualCellHost

Public Class Wizard

    Public Property models As New Dictionary(Of String, ModelFile)
    Public Property config As Config
    Public Property configFile As String

    ''' <summary>
    ''' save config data to config file location
    ''' </summary>
    Public Sub Save()
        Call config.GetJson.SaveTo(configFile)
    End Sub

    Public Sub SetModelFiles(files As IEnumerable(Of String))
        For Each file As String In files
            Dim model As VirtualCell = file.LoadXml(Of VirtualCell)
            Dim data As New ModelFile With {
                .filepath = file,
                .model = model
            }

            models(model.cellular_id) = data
        Next
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function GetModelFiles() As IEnumerable(Of String)
        Return models.Values.Select(Function(m) m.filepath)
    End Function

End Class

Public Class ModelFile

    Public Property filepath As String
    Public Property model As VirtualCell

End Class