<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CellBrowser
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
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        OpenVirtualCellDataFileToolStripMenuItem = New ToolStripMenuItem()
        CloseFileToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        ExitToolStripMenuItem = New ToolStripMenuItem()
        BrowserToolStripMenuItem = New ToolStripMenuItem()
        ResetNetworkTableToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem3 = New ToolStripSeparator()
        ViewMassActivityLoadsToolStripMenuItem = New ToolStripMenuItem()
        ExportMoleculeExpressionToolStripMenuItem = New ToolStripMenuItem()
        ExportFluxomicsToolStripMenuItem = New ToolStripMenuItem()
        ViewerToolStripMenuItem = New ToolStripMenuItem()
        ExportPlotMatrixToolStripMenuItem = New ToolStripMenuItem()
        PlotView1 = New ggviewer.PlotView()
        GroupBox2 = New GroupBox()
        SplitContainer2 = New SplitContainer()
        TreeView1 = New TreeView()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        FilterNetworkToolStripMenuItem = New ToolStripMenuItem()
        FilterSubstrateNetworkToolStripMenuItem = New ToolStripMenuItem()
        FilterProductNetworkToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        CopyNameToolStripMenuItem = New ToolStripMenuItem()
        DataGridView1 = New DataGridView()
        Column1 = New DataGridViewLinkColumn()
        Column2 = New DataGridViewLinkColumn()
        ContextMenuStrip2 = New ContextMenuStrip(components)
        ViewFluxDynamicsToolStripMenuItem = New ToolStripMenuItem()
        SplitContainer1 = New SplitContainer()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        SplitContainer3 = New SplitContainer()
        CheckedListBox1 = New CheckedListBox()
        TabPage2 = New TabPage()
        DataGridView2 = New DataGridView()
        BindingSource1 = New BindingSource(components)
        MenuStrip1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(SplitContainer2, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer2.Panel1.SuspendLayout()
        SplitContainer2.Panel2.SuspendLayout()
        SplitContainer2.SuspendLayout()
        ContextMenuStrip1.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        ContextMenuStrip2.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(SplitContainer3, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer3.Panel1.SuspendLayout()
        SplitContainer3.Panel2.SuspendLayout()
        SplitContainer3.SuspendLayout()
        TabPage2.SuspendLayout()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        CType(BindingSource1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, BrowserToolStripMenuItem, ViewerToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(1524, 24)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OpenVirtualCellDataFileToolStripMenuItem, CloseFileToolStripMenuItem, ToolStripMenuItem1, ExitToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(37, 20)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' OpenVirtualCellDataFileToolStripMenuItem
        ' 
        OpenVirtualCellDataFileToolStripMenuItem.Name = "OpenVirtualCellDataFileToolStripMenuItem"
        OpenVirtualCellDataFileToolStripMenuItem.Size = New Size(208, 22)
        OpenVirtualCellDataFileToolStripMenuItem.Text = "Open VirtualCell Data File"
        ' 
        ' CloseFileToolStripMenuItem
        ' 
        CloseFileToolStripMenuItem.Name = "CloseFileToolStripMenuItem"
        CloseFileToolStripMenuItem.Size = New Size(208, 22)
        CloseFileToolStripMenuItem.Text = "Close File"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(205, 6)
        ' 
        ' ExitToolStripMenuItem
        ' 
        ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        ExitToolStripMenuItem.Size = New Size(208, 22)
        ExitToolStripMenuItem.Text = "Exit"
        ' 
        ' BrowserToolStripMenuItem
        ' 
        BrowserToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ResetNetworkTableToolStripMenuItem, ToolStripMenuItem3, ViewMassActivityLoadsToolStripMenuItem, ExportMoleculeExpressionToolStripMenuItem, ExportFluxomicsToolStripMenuItem})
        BrowserToolStripMenuItem.Name = "BrowserToolStripMenuItem"
        BrowserToolStripMenuItem.Size = New Size(61, 20)
        BrowserToolStripMenuItem.Text = "Browser"
        ' 
        ' ResetNetworkTableToolStripMenuItem
        ' 
        ResetNetworkTableToolStripMenuItem.Name = "ResetNetworkTableToolStripMenuItem"
        ResetNetworkTableToolStripMenuItem.Size = New Size(219, 22)
        ResetNetworkTableToolStripMenuItem.Text = "Reset Network Table"
        ' 
        ' ToolStripMenuItem3
        ' 
        ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        ToolStripMenuItem3.Size = New Size(216, 6)
        ' 
        ' ViewMassActivityLoadsToolStripMenuItem
        ' 
        ViewMassActivityLoadsToolStripMenuItem.Name = "ViewMassActivityLoadsToolStripMenuItem"
        ViewMassActivityLoadsToolStripMenuItem.Size = New Size(219, 22)
        ViewMassActivityLoadsToolStripMenuItem.Text = "View Mass Activity Loads"
        ' 
        ' ExportMoleculeExpressionToolStripMenuItem
        ' 
        ExportMoleculeExpressionToolStripMenuItem.Name = "ExportMoleculeExpressionToolStripMenuItem"
        ExportMoleculeExpressionToolStripMenuItem.Size = New Size(219, 22)
        ExportMoleculeExpressionToolStripMenuItem.Text = "Export Molecule Expression"
        ' 
        ' ExportFluxomicsToolStripMenuItem
        ' 
        ExportFluxomicsToolStripMenuItem.Name = "ExportFluxomicsToolStripMenuItem"
        ExportFluxomicsToolStripMenuItem.Size = New Size(219, 22)
        ExportFluxomicsToolStripMenuItem.Text = "Export Fluxomics"
        ' 
        ' ViewerToolStripMenuItem
        ' 
        ViewerToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ExportPlotMatrixToolStripMenuItem})
        ViewerToolStripMenuItem.Name = "ViewerToolStripMenuItem"
        ViewerToolStripMenuItem.Size = New Size(54, 20)
        ViewerToolStripMenuItem.Text = "Viewer"
        ' 
        ' ExportPlotMatrixToolStripMenuItem
        ' 
        ExportPlotMatrixToolStripMenuItem.Name = "ExportPlotMatrixToolStripMenuItem"
        ExportPlotMatrixToolStripMenuItem.Size = New Size(169, 22)
        ExportPlotMatrixToolStripMenuItem.Text = "Export Plot Matrix"
        ' 
        ' PlotView1
        ' 
        PlotView1.BackColor = Color.SkyBlue
        PlotView1.Debug = True
        PlotView1.Dock = DockStyle.Fill
        PlotView1.ggplot = Nothing
        PlotView1.Location = New Point(0, 0)
        PlotView1.Name = "PlotView1"
        PlotView1.ScaleFactor = 1.25F
        PlotView1.Size = New Size(1167, 572)
        PlotView1.TabIndex = 1
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(SplitContainer2)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(0, 0)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(1524, 308)
        GroupBox2.TabIndex = 3
        GroupBox2.TabStop = False
        GroupBox2.Text = "Browser"
        ' 
        ' SplitContainer2
        ' 
        SplitContainer2.Dock = DockStyle.Fill
        SplitContainer2.FixedPanel = FixedPanel.Panel1
        SplitContainer2.IsSplitterFixed = True
        SplitContainer2.Location = New Point(3, 19)
        SplitContainer2.Name = "SplitContainer2"
        ' 
        ' SplitContainer2.Panel1
        ' 
        SplitContainer2.Panel1.Controls.Add(TreeView1)
        ' 
        ' SplitContainer2.Panel2
        ' 
        SplitContainer2.Panel2.Controls.Add(DataGridView1)
        SplitContainer2.Size = New Size(1518, 286)
        SplitContainer2.SplitterDistance = 344
        SplitContainer2.TabIndex = 0
        ' 
        ' TreeView1
        ' 
        TreeView1.ContextMenuStrip = ContextMenuStrip1
        TreeView1.Dock = DockStyle.Fill
        TreeView1.Location = New Point(0, 0)
        TreeView1.Name = "TreeView1"
        TreeView1.Size = New Size(344, 286)
        TreeView1.TabIndex = 0
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {FilterNetworkToolStripMenuItem, FilterSubstrateNetworkToolStripMenuItem, FilterProductNetworkToolStripMenuItem, ToolStripMenuItem2, CopyNameToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(201, 98)
        ' 
        ' FilterNetworkToolStripMenuItem
        ' 
        FilterNetworkToolStripMenuItem.Name = "FilterNetworkToolStripMenuItem"
        FilterNetworkToolStripMenuItem.Size = New Size(200, 22)
        FilterNetworkToolStripMenuItem.Text = "Filter Network"
        ' 
        ' FilterSubstrateNetworkToolStripMenuItem
        ' 
        FilterSubstrateNetworkToolStripMenuItem.Name = "FilterSubstrateNetworkToolStripMenuItem"
        FilterSubstrateNetworkToolStripMenuItem.Size = New Size(200, 22)
        FilterSubstrateNetworkToolStripMenuItem.Text = "Filter Substrate Network"
        ' 
        ' FilterProductNetworkToolStripMenuItem
        ' 
        FilterProductNetworkToolStripMenuItem.Name = "FilterProductNetworkToolStripMenuItem"
        FilterProductNetworkToolStripMenuItem.Size = New Size(200, 22)
        FilterProductNetworkToolStripMenuItem.Text = "Filter Product Network"
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(197, 6)
        ' 
        ' CopyNameToolStripMenuItem
        ' 
        CopyNameToolStripMenuItem.Name = "CopyNameToolStripMenuItem"
        CopyNameToolStripMenuItem.Size = New Size(200, 22)
        CopyNameToolStripMenuItem.Text = "Copy Name"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.BackgroundColor = Color.WhiteSmoke
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2})
        DataGridView1.ContextMenuStrip = ContextMenuStrip2
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(0, 0)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(1170, 286)
        DataGridView1.TabIndex = 0
        ' 
        ' Column1
        ' 
        Column1.HeaderText = "Flux Edge ID"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        Column1.Resizable = DataGridViewTriState.True
        Column1.SortMode = DataGridViewColumnSortMode.Automatic
        Column1.Width = 200
        ' 
        ' Column2
        ' 
        Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Column2.HeaderText = "Network"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' ContextMenuStrip2
        ' 
        ContextMenuStrip2.Items.AddRange(New ToolStripItem() {ViewFluxDynamicsToolStripMenuItem})
        ContextMenuStrip2.Name = "ContextMenuStrip2"
        ContextMenuStrip2.Size = New Size(180, 26)
        ' 
        ' ViewFluxDynamicsToolStripMenuItem
        ' 
        ViewFluxDynamicsToolStripMenuItem.Name = "ViewFluxDynamicsToolStripMenuItem"
        ViewFluxDynamicsToolStripMenuItem.Size = New Size(179, 22)
        ViewFluxDynamicsToolStripMenuItem.Text = "View Flux Dynamics"
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(0, 24)
        SplitContainer1.Name = "SplitContainer1"
        SplitContainer1.Orientation = Orientation.Horizontal
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(TabControl1)
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(GroupBox2)
        SplitContainer1.Size = New Size(1524, 918)
        SplitContainer1.SplitterDistance = 606
        SplitContainer1.TabIndex = 4
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Location = New Point(0, 0)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1524, 606)
        TabControl1.TabIndex = 0
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(SplitContainer3)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(1516, 578)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Dynamics Plot"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' SplitContainer3
        ' 
        SplitContainer3.Dock = DockStyle.Fill
        SplitContainer3.FixedPanel = FixedPanel.Panel1
        SplitContainer3.Location = New Point(3, 3)
        SplitContainer3.Name = "SplitContainer3"
        ' 
        ' SplitContainer3.Panel1
        ' 
        SplitContainer3.Panel1.Controls.Add(CheckedListBox1)
        ' 
        ' SplitContainer3.Panel2
        ' 
        SplitContainer3.Panel2.Controls.Add(PlotView1)
        SplitContainer3.Size = New Size(1510, 572)
        SplitContainer3.SplitterDistance = 339
        SplitContainer3.TabIndex = 2
        ' 
        ' CheckedListBox1
        ' 
        CheckedListBox1.Dock = DockStyle.Fill
        CheckedListBox1.FormattingEnabled = True
        CheckedListBox1.Location = New Point(0, 0)
        CheckedListBox1.Name = "CheckedListBox1"
        CheckedListBox1.Size = New Size(339, 572)
        CheckedListBox1.TabIndex = 0
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(DataGridView2)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(1516, 578)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Matrix"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' DataGridView2
        ' 
        DataGridView2.AllowUserToAddRows = False
        DataGridView2.AllowUserToDeleteRows = False
        DataGridView2.BackgroundColor = Color.Gainsboro
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Dock = DockStyle.Fill
        DataGridView2.Location = New Point(3, 3)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.ReadOnly = True
        DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView2.Size = New Size(1510, 572)
        DataGridView2.TabIndex = 0
        ' 
        ' CellBrowser
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1524, 942)
        Controls.Add(SplitContainer1)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "CellBrowser"
        Text = "Cell Browser"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        SplitContainer2.Panel1.ResumeLayout(False)
        SplitContainer2.Panel2.ResumeLayout(False)
        CType(SplitContainer2, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer2.ResumeLayout(False)
        ContextMenuStrip1.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ContextMenuStrip2.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        SplitContainer3.Panel1.ResumeLayout(False)
        SplitContainer3.Panel2.ResumeLayout(False)
        CType(SplitContainer3, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer3.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        CType(BindingSource1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenVirtualCellDataFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PlotView1 As ggviewer.PlotView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Column1 As DataGridViewLinkColumn
    Friend WithEvents Column2 As DataGridViewLinkColumn
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents BrowserToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ResetNetworkTableToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents FilterNetworkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents ViewFluxDynamicsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportPlotMatrixToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewMassActivityLoadsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents CopyNameToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FilterSubstrateNetworkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FilterProductNetworkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripSeparator
    Friend WithEvents ExportMoleculeExpressionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportFluxomicsToolStripMenuItem As ToolStripMenuItem
End Class
