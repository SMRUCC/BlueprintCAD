Imports BlueprintCAD.My
Imports BlueprintCAD.RibbonLib.Controls
Imports Galaxy.Workbench
Imports RibbonLib
Imports WeifenLuo.WinFormsUI.Docking

Public Class FormMain : Implements AppHost

    Friend WithEvents m_dockPanel As New DockPanel
    Friend WithEvents m_ribbon As New Ribbon
    Friend WithEvents m_ribbonItems As RibbonItems

    Dim vS2015LightTheme1 As New VS2015LightTheme
    Dim vsToolStripExtender1 As New VisualStudioToolStripExtender

    ReadOnly _toolStripProfessionalRenderer As New ToolStripProfessionalRenderer()

    Public Event ResizeForm As AppHost.ResizeFormEventHandler Implements AppHost.ResizeForm

    Public ReadOnly Property DockPanel As DockPanel
        Get
            Return m_dockPanel
        End Get
    End Property

    Private Sub initializeVSPanel()
        m_dockPanel.ShowDocumentIcon = True

        Me.m_dockPanel.Dock = DockStyle.Fill
        Me.m_dockPanel.DockBackColor = Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.m_dockPanel.DockBottomPortion = 150.0R
        Me.m_dockPanel.DockLeftPortion = 200.0R
        Me.m_dockPanel.DockRightPortion = 200.0R
        Me.m_dockPanel.DockTopPortion = 150.0R
        Me.m_dockPanel.Font = New Font("Tahoma", 11.0!, FontStyle.Regular, GraphicsUnit.World, CType(0, Byte))

        Me.m_dockPanel.Name = "dockPanel"
        Me.m_dockPanel.RightToLeftLayout = True
        Me.m_dockPanel.ShowAutoHideContentOnHover = False

        Me.m_dockPanel.TabIndex = 0

        Call SetSchema(Nothing, Nothing)
    End Sub

    Private Sub SetSchema(sender As Object, e As EventArgs)
        m_dockPanel.Theme = vS2015LightTheme1
        EnableVSRenderer(VisualStudioToolStripExtender.VsVersion.Vs2015, vS2015LightTheme1)

        If m_dockPanel.Theme.ColorPalette IsNot Nothing Then
            StatusStrip1.BackColor = m_dockPanel.Theme.ColorPalette.MainWindowStatusBarDefault.Background
        End If
    End Sub

    Private Sub EnableVSRenderer(version As VisualStudioToolStripExtender.VsVersion, theme As ThemeBase)
        vsToolStripExtender1.SetStyle(StatusStrip1, version, theme)
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Workbench.SetHost(appHost:=Me)

        m_ribbon.Height = 60
        m_ribbon.Dock = DockStyle.Top
        m_ribbon.ResourceName = "BlueprintCAD.RibbonMarkup.ribbon"
        m_ribbonItems = New RibbonItems(m_ribbon)

        Call Controls.Add(m_ribbon)
        Call PanelBase.Controls.Add(Me.m_dockPanel)
        Call initializeVSPanel()
        Call MyApplication.SetRibbonEvents()
    End Sub

    Public Function GetDesktopLocation() As Point Implements AppHost.GetDesktopLocation
        Return Location
    End Function

    Public Function GetClientSize() As Size Implements AppHost.GetClientSize
        Return Size
    End Function

    Public Sub StatusMessage(msg As String, Optional icon As Image = Nothing) Implements AppHost.StatusMessage
        ToolStripStatusLabel1.Text = msg
        ToolStripStatusLabel1.Image = icon
    End Sub
End Class