Imports BlueprintCAD.RibbonLib.Controls
Imports Microsoft.VisualBasic.Drawing
Imports WeifenLuo.WinFormsUI.Docking

Public Module Workbench

    Public ReadOnly Property AppHost As FormMain

    Friend ReadOnly Property Ribbon As RibbonItems
        Get
            Return AppHost.m_ribbonItems
        End Get
    End Property

    Sub New()
        Call SkiaDriver.Register()
    End Sub

    Public Sub SetHost(appHost As FormMain)
        _AppHost = appHost
    End Sub

    Friend Sub LogText(v As String)

    End Sub

    Friend Sub Warning(v As String)

    End Sub

    Public Function OpenDocument(Of T As {New, DocumentWindow})(Optional title As String = Nothing) As T
        Dim docPage As New T

        If Not title.StringEmpty(, True) Then
            docPage.TabText = title
            docPage.Text = title
        Else
            docPage.TabText = title
        End If

        docPage.Show(AppHost.DockPanel)
        docPage.DockState = DockState.Document

        Return docPage
    End Function
End Module
