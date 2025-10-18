Imports BlueprintCAD.RibbonLib.Controls
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.Drawing

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

    Public ReadOnly Property Settings As Settings

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

        _Settings = Settings.Load
    End Sub
End Module
