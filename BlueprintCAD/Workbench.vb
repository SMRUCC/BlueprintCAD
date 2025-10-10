Imports BlueprintCAD.RibbonLib.Controls
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.Drawing
Imports Microsoft.VisualStudio.WinForms.Docking

Public Module Workbench

    Public ReadOnly Property AppHost As FormMain
        Get
            Return DirectCast(CommonRuntime.AppHost, FormMain)
        End Get
    End Property

    Friend ReadOnly Property Ribbon As RibbonItems
        Get
            Return AppHost.m_ribbonItems
        End Get
    End Property

    Sub New()
        Call SkiaDriver.Register()
    End Sub

    Dim title As String = "Sophia VirtualCell Workshop"

    Public Sub SetFormActiveTitle(subtitle As String)
        title = AppHost.Text
        AppHost.Text = $"Sophia VirtualCell Workshop [{subtitle}]"
    End Sub

    Public Sub RestoreFormTitle()
        AppHost.Text = title
    End Sub

    Public Sub SetHost(appHost As FormMain)
        CommonRuntime.Hook(appHost)
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
