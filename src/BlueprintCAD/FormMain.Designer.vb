<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        PanelBase = New Panel()
        StatusStrip1 = New StatusStrip()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        ToolStripStatusLabel3 = New ToolStripStatusLabel()
        ToolStripStatusLabel2 = New ToolStripStatusLabel()
        Timer1 = New Timer(components)
        Ribbon1 = New RibbonLib.Ribbon()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelBase
        ' 
        PanelBase.Dock = DockStyle.Fill
        PanelBase.Location = New Point(0, 116)
        PanelBase.Name = "PanelBase"
        PanelBase.Size = New Size(946, 452)
        PanelBase.TabIndex = 0
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripStatusLabel1, ToolStripStatusLabel3, ToolStripStatusLabel2})
        StatusStrip1.Location = New Point(0, 568)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(946, 22)
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.Image = CType(resources.GetObject("ToolStripStatusLabel1.Image"), Image)
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(64, 17)
        ToolStripStatusLabel1.Text = "Ready!"
        ' 
        ' ToolStripStatusLabel3
        ' 
        ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        ToolStripStatusLabel3.Size = New Size(721, 17)
        ToolStripStatusLabel3.Spring = True
        ' 
        ' ToolStripStatusLabel2
        ' 
        ToolStripStatusLabel2.Image = CType(resources.GetObject("ToolStripStatusLabel2.Image"), Image)
        ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        ToolStripStatusLabel2.Size = New Size(146, 17)
        ToolStripStatusLabel2.Text = "Connected To Server"
        ToolStripStatusLabel2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        Timer1.Interval = 1000
        ' 
        ' Ribbon1
        ' 
        Ribbon1.Location = New Point(0, 0)
        Ribbon1.Name = "Ribbon1"
        Ribbon1.ResourceIdentifier = Nothing
        Ribbon1.ResourceName = "BlueprintCAD.RibbonMarkup.ribbon"
        Ribbon1.ShortcutTableResourceName = Nothing
        Ribbon1.Size = New Size(946, 116)
        Ribbon1.TabIndex = 2
        ' 
        ' FormMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(946, 590)
        Controls.Add(PanelBase)
        Controls.Add(Ribbon1)
        Controls.Add(StatusStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "FormMain"
        Text = "Sophia VirtualCell Workshop"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PanelBase As Panel
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Ribbon1 As RibbonLib.Ribbon
End Class
