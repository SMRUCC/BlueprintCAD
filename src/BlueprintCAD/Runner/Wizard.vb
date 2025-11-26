Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.MIME.application.json
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports VirtualCellHost

''' <summary>
''' the wizard data
''' </summary>
Public Class Wizard

    Public Property models As New Dictionary(Of String, ModelFile)
    Public Property config As Config
    Public Property configFile As String

    ''' <summary>
    ''' save config data to config file location
    ''' </summary>
    Public Sub Save()
        config.mapping.status(config.mapping.CultureMedium) = config.cultureMedium
        config.GetJson.SaveTo(configFile)
    End Sub

    ''' <summary>
    ''' load model files
    ''' </summary>
    ''' <param name="files"></param>
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