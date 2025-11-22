Imports System.ComponentModel
Imports BlueprintCAD.My
Imports BlueprintCAD.RibbonLib.Controls
Imports CADRegistry
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging
Imports Microsoft.VisualStudio.WinForms.Docking
Imports RibbonLib
Imports ThemeVS2015

Public Class FormMain : Implements AppHost

    Friend WithEvents m_dockPanel As New DockPanel
    Friend WithEvents m_ribbon As New Ribbon
    Friend WithEvents m_ribbonItems As RibbonItems

    Dim vS2015LightTheme1 As New VS2015LightTheme
    Dim vsToolStripExtender1 As New VisualStudioToolStripExtender

    ReadOnly _toolStripProfessionalRenderer As New ToolStripProfessionalRenderer()

    Public Event ResizeForm As AppHost.ResizeFormEventHandler Implements AppHost.ResizeForm
    Public Event CloseWorkbench As AppHost.CloseWorkbenchEventHandler Implements AppHost.CloseWorkbench

    Public ReadOnly Property DockPanel As DockPanel
        Get
            Return m_dockPanel
        End Get
    End Property

    Public ReadOnly Property ActiveDocument As Form Implements AppHost.ActiveDocument
        Get
            Return m_dockPanel.ActiveDocument
        End Get
    End Property

    Private ReadOnly Property AppHost_ClientRectangle As Rectangle Implements AppHost.ClientRectangle
        Get
            Return New Rectangle(Location, Size)
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
        Call Timer1.Start()
        Call Workbench.InitializeWindows()
        Call CommonRuntime.GetOutputWindow.AddLog(Now, "workbench startup", "ready!")
    End Sub

    Public Function GetDesktopLocation() As Point Implements AppHost.GetDesktopLocation
        Return Location
    End Function

    Public Function GetClientSize() As Size Implements AppHost.GetClientSize
        Return Size
    End Function

    Public Sub StatusMessage(msg As String, Optional icon As Image = Nothing) Implements AppHost.StatusMessage
        Call Invoke(Sub() ToolStripStatusLabel1.Text = msg)
        Call Invoke(Sub() ToolStripStatusLabel1.Image = If(icon, Icons8.Information))
    End Sub

    Public Function GetDocuments() As IEnumerable(Of Form) Implements AppHost.GetDocuments
        Return m_dockPanel.Documents.Select(Function(d) DirectCast(d, Form))
    End Function

    Public Function GetDockPanel() As Control Implements AppHost.GetDockPanel
        Return m_dockPanel
    End Function

    Public Sub SetWorkbenchVisible(visible As Boolean) Implements AppHost.SetWorkbenchVisible

    End Sub

    Public Sub SetWindowState(stat As FormWindowState) Implements AppHost.SetWindowState
        WindowState = stat
    End Sub

    Public Function GetWindowState() As FormWindowState Implements AppHost.GetWindowState
        Return WindowState
    End Function

    Public Sub SetTitle(title As String) Implements AppHost.SetTitle
        Call Workbench.SetFormActiveTitle(title)
    End Sub

    Public Sub Warning(msg As String) Implements AppHost.Warning

    End Sub

    Public Sub LogText(text As String) Implements AppHost.LogText

    End Sub

    Public Sub ShowProperties(obj As Object) Implements AppHost.ShowProperties

    End Sub

    Private Sub FormMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Call CommonRuntime.SaveUISettings()
    End Sub

    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Await CheckServerConnectivity()
    End Sub

    Private Async Function CheckServerConnectivity() As Task
        If Not Workbench.Settings.registry_server.StringEmpty(, True) Then
            Dim test As New RegistryUrl(Workbench.Settings.registry_server)

            If Await Task.Run(Function() test.Ping()) Then
                If Workbench.ServerConnection = False Then
                    Call CommonRuntime.GetOutputWindow.AddLog(Now, "database connection", "registry database server is online!", MSG_TYPES.FINEST)
                End If

                ToolStripStatusLabel2.Text = "Connected To Server"
                ToolStripStatusLabel2.Image = My.Resources.Icons.icons8_wifi_96
                Workbench.SetConnection(True)
            Else
                If Workbench.ServerConnection = True Then
                    Call CommonRuntime.GetOutputWindow.AddLog(Now, "database connection", "registry database server is offline, part of software function and virtual cell model compiler will not working!", MSG_TYPES.WRN)
                    Call CommonRuntime.Warning("registry database server is offline, part of software function and virtual cell model compiler will not working!")
                End If

                ToolStripStatusLabel2.Text = "Server Offline"
                ToolStripStatusLabel2.Image = My.Resources.Icons.icons8_offline_96
                Workbench.SetConnection(False)
            End If
        End If
    End Function

    Private Async Sub ToolStripStatusLabel2_Click() Handles ToolStripStatusLabel2.Click
        Await CheckServerConnectivity()
    End Sub
End Class