<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CellBrowser : Inherits DocumentWindow

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CellBrowser))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
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
        ExpressionValueLogScaleToolStripMenuItem = New ToolStripMenuItem()
        PlotView1 = New ggviewer.PlotView()
        GroupBox2 = New GroupBox()
        SplitContainer2 = New SplitContainer()
        TreeView1 = New TreeView()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        FilterNetworkToolStripMenuItem = New ToolStripMenuItem()
        FilterSubstrateNetworkToolStripMenuItem = New ToolStripMenuItem()
        FilterProductNetworkToolStripMenuItem = New ToolStripMenuItem()
        FilterRegulationToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        CopyNameToolStripMenuItem = New ToolStripMenuItem()
        DataGridView1 = New DataGridView()
        Column2 = New DataGridViewLinkColumn()
        Column3 = New DataGridViewLinkColumn()
        Column4 = New DataGridViewLinkColumn()
        ContextMenuStrip2 = New ContextMenuStrip(components)
        ViewFluxDynamicsToolStripMenuItem = New ToolStripMenuItem()
        ToolStrip1 = New ToolStrip()
        ToolStripLabel1 = New ToolStripLabel()
        ToolStripComboBox1 = New ToolStripComboBox()
        SplitContainer1 = New SplitContainer()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        SplitContainer3 = New SplitContainer()
        CheckedListBox1 = New CheckedListBox()
        ContextMenuStrip3 = New ContextMenuStrip(components)
        UnCheckAllToolStripMenuItem = New ToolStripMenuItem()
        CheckAllToolStripMenuItem = New ToolStripMenuItem()
        TabPage2 = New TabPage()
        DataGridView2 = New DataGridView()
        TabPage3 = New TabPage()
        TextBox1 = New TextBox()
        BindingSource1 = New BindingSource(components)
        StatusStrip1 = New StatusStrip()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        ToolStripMenuItem4 = New ToolStripSeparator()
        PhenotypeAnalysisToolStripMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(SplitContainer2, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer2.Panel1.SuspendLayout()
        SplitContainer2.Panel2.SuspendLayout()
        SplitContainer2.SuspendLayout()
        ContextMenuStrip1.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        ContextMenuStrip2.SuspendLayout()
        ToolStrip1.SuspendLayout()
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
        ContextMenuStrip3.SuspendLayout()
        TabPage2.SuspendLayout()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        CType(BindingSource1, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
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
        OpenVirtualCellDataFileToolStripMenuItem.Image = CType(resources.GetObject("OpenVirtualCellDataFileToolStripMenuItem.Image"), Image)
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
        ExitToolStripMenuItem.Image = CType(resources.GetObject("ExitToolStripMenuItem.Image"), Image)
        ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        ExitToolStripMenuItem.Size = New Size(208, 22)
        ExitToolStripMenuItem.Text = "Exit"
        ' 
        ' BrowserToolStripMenuItem
        ' 
        BrowserToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ResetNetworkTableToolStripMenuItem, ToolStripMenuItem3, ViewMassActivityLoadsToolStripMenuItem, ExportMoleculeExpressionToolStripMenuItem, ExportFluxomicsToolStripMenuItem, ToolStripMenuItem4, PhenotypeAnalysisToolStripMenuItem})
        BrowserToolStripMenuItem.Name = "BrowserToolStripMenuItem"
        BrowserToolStripMenuItem.Size = New Size(61, 20)
        BrowserToolStripMenuItem.Text = "Browser"
        ' 
        ' ResetNetworkTableToolStripMenuItem
        ' 
        ResetNetworkTableToolStripMenuItem.Image = CType(resources.GetObject("ResetNetworkTableToolStripMenuItem.Image"), Image)
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
        ViewerToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ExportPlotMatrixToolStripMenuItem, ExpressionValueLogScaleToolStripMenuItem})
        ViewerToolStripMenuItem.Name = "ViewerToolStripMenuItem"
        ViewerToolStripMenuItem.Size = New Size(54, 20)
        ViewerToolStripMenuItem.Text = "Viewer"
        ' 
        ' ExportPlotMatrixToolStripMenuItem
        ' 
        ExportPlotMatrixToolStripMenuItem.Image = CType(resources.GetObject("ExportPlotMatrixToolStripMenuItem.Image"), Image)
        ExportPlotMatrixToolStripMenuItem.Name = "ExportPlotMatrixToolStripMenuItem"
        ExportPlotMatrixToolStripMenuItem.Size = New Size(214, 22)
        ExportPlotMatrixToolStripMenuItem.Text = "Export Plot Matrix"
        ' 
        ' ExpressionValueLogScaleToolStripMenuItem
        ' 
        ExpressionValueLogScaleToolStripMenuItem.CheckOnClick = True
        ExpressionValueLogScaleToolStripMenuItem.Image = CType(resources.GetObject("ExpressionValueLogScaleToolStripMenuItem.Image"), Image)
        ExpressionValueLogScaleToolStripMenuItem.Name = "ExpressionValueLogScaleToolStripMenuItem"
        ExpressionValueLogScaleToolStripMenuItem.Size = New Size(214, 22)
        ExpressionValueLogScaleToolStripMenuItem.Text = "Expression Value Log Scale"
        ' 
        ' PlotView1
        ' 
        PlotView1.BackColor = Color.SkyBlue
        PlotView1.Debug = True
        PlotView1.Dock = DockStyle.Fill
        PlotView1.Dpi = 120
        PlotView1.ggplot = Nothing
        PlotView1.Location = New Point(0, 0)
        PlotView1.Name = "PlotView1"
        PlotView1.ScaleFactor = 1.25F
        PlotView1.Size = New Size(1167, 557)
        PlotView1.TabIndex = 1
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(SplitContainer2)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(0, 0)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(1524, 301)
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
        SplitContainer2.Panel2.Controls.Add(ToolStrip1)
        SplitContainer2.Size = New Size(1518, 279)
        SplitContainer2.SplitterDistance = 344
        SplitContainer2.TabIndex = 0
        ' 
        ' TreeView1
        ' 
        TreeView1.ContextMenuStrip = ContextMenuStrip1
        TreeView1.Dock = DockStyle.Fill
        TreeView1.Location = New Point(0, 0)
        TreeView1.Name = "TreeView1"
        TreeView1.Size = New Size(344, 279)
        TreeView1.TabIndex = 0
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {FilterNetworkToolStripMenuItem, FilterSubstrateNetworkToolStripMenuItem, FilterProductNetworkToolStripMenuItem, FilterRegulationToolStripMenuItem, ToolStripMenuItem2, CopyNameToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(201, 120)
        ' 
        ' FilterNetworkToolStripMenuItem
        ' 
        FilterNetworkToolStripMenuItem.Image = CType(resources.GetObject("FilterNetworkToolStripMenuItem.Image"), Image)
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
        ' FilterRegulationToolStripMenuItem
        ' 
        FilterRegulationToolStripMenuItem.Image = CType(resources.GetObject("FilterRegulationToolStripMenuItem.Image"), Image)
        FilterRegulationToolStripMenuItem.Name = "FilterRegulationToolStripMenuItem"
        FilterRegulationToolStripMenuItem.Size = New Size(200, 22)
        FilterRegulationToolStripMenuItem.Text = "Filter Regulation"
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
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllHeaders
        DataGridView1.BackgroundColor = Color.WhiteSmoke
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column2, Column3, Column4})
        DataGridView1.ContextMenuStrip = ContextMenuStrip2
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(0, 25)
        DataGridView1.MultiSelect = False
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Control
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(1170, 254)
        DataGridView1.TabIndex = 0
        ' 
        ' Column2
        ' 
        Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Column2.HeaderText = "Network"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' Column3
        ' 
        Column3.HeaderText = "Forward Regulation"
        Column3.Name = "Column3"
        Column3.ReadOnly = True
        Column3.Resizable = DataGridViewTriState.True
        Column3.SortMode = DataGridViewColumnSortMode.Automatic
        Column3.Width = 250
        ' 
        ' Column4
        ' 
        Column4.HeaderText = "Reverse Regulation"
        Column4.Name = "Column4"
        Column4.ReadOnly = True
        Column4.Resizable = DataGridViewTriState.True
        Column4.SortMode = DataGridViewColumnSortMode.Automatic
        Column4.Width = 250
        ' 
        ' ContextMenuStrip2
        ' 
        ContextMenuStrip2.Items.AddRange(New ToolStripItem() {ViewFluxDynamicsToolStripMenuItem})
        ContextMenuStrip2.Name = "ContextMenuStrip2"
        ContextMenuStrip2.Size = New Size(180, 26)
        ' 
        ' ViewFluxDynamicsToolStripMenuItem
        ' 
        ViewFluxDynamicsToolStripMenuItem.Image = CType(resources.GetObject("ViewFluxDynamicsToolStripMenuItem.Image"), Image)
        ViewFluxDynamicsToolStripMenuItem.Name = "ViewFluxDynamicsToolStripMenuItem"
        ViewFluxDynamicsToolStripMenuItem.Size = New Size(179, 22)
        ViewFluxDynamicsToolStripMenuItem.Text = "View Flux Dynamics"
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripLabel1, ToolStripComboBox1})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1170, 25)
        ToolStrip1.TabIndex = 1
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripLabel1
        ' 
        ToolStripLabel1.Name = "ToolStripLabel1"
        ToolStripLabel1.Size = New Size(133, 22)
        ToolStripLabel1.Text = "Select Network Module:"
        ' 
        ' ToolStripComboBox1
        ' 
        ToolStripComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ToolStripComboBox1.Name = "ToolStripComboBox1"
        ToolStripComboBox1.Size = New Size(300, 25)
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
        SplitContainer1.Size = New Size(1524, 896)
        SplitContainer1.SplitterDistance = 591
        SplitContainer1.TabIndex = 4
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Location = New Point(0, 0)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1524, 591)
        TabControl1.TabIndex = 0
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(SplitContainer3)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(1516, 563)
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
        SplitContainer3.Size = New Size(1510, 557)
        SplitContainer3.SplitterDistance = 339
        SplitContainer3.TabIndex = 2
        ' 
        ' CheckedListBox1
        ' 
        CheckedListBox1.ContextMenuStrip = ContextMenuStrip3
        CheckedListBox1.Dock = DockStyle.Fill
        CheckedListBox1.FormattingEnabled = True
        CheckedListBox1.Location = New Point(0, 0)
        CheckedListBox1.Name = "CheckedListBox1"
        CheckedListBox1.Size = New Size(339, 557)
        CheckedListBox1.TabIndex = 0
        ' 
        ' ContextMenuStrip3
        ' 
        ContextMenuStrip3.Items.AddRange(New ToolStripItem() {UnCheckAllToolStripMenuItem, CheckAllToolStripMenuItem})
        ContextMenuStrip3.Name = "ContextMenuStrip3"
        ContextMenuStrip3.Size = New Size(140, 48)
        ' 
        ' UnCheckAllToolStripMenuItem
        ' 
        UnCheckAllToolStripMenuItem.Name = "UnCheckAllToolStripMenuItem"
        UnCheckAllToolStripMenuItem.Size = New Size(139, 22)
        UnCheckAllToolStripMenuItem.Text = "UnCheck All"
        ' 
        ' CheckAllToolStripMenuItem
        ' 
        CheckAllToolStripMenuItem.Name = "CheckAllToolStripMenuItem"
        CheckAllToolStripMenuItem.Size = New Size(139, 22)
        CheckAllToolStripMenuItem.Text = "Check All"
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(DataGridView2)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(1516, 563)
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
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Cambria", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        DataGridView2.DefaultCellStyle = DataGridViewCellStyle2
        DataGridView2.Dock = DockStyle.Fill
        DataGridView2.Location = New Point(3, 3)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.ReadOnly = True
        DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView2.Size = New Size(1510, 557)
        DataGridView2.TabIndex = 0
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(TextBox1)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(1516, 563)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Flux Model"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.BorderStyle = BorderStyle.None
        TextBox1.Dock = DockStyle.Fill
        TextBox1.Location = New Point(3, 3)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ScrollBars = ScrollBars.Both
        TextBox1.Size = New Size(1510, 557)
        TextBox1.TabIndex = 0
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripStatusLabel1})
        StatusStrip1.Location = New Point(0, 920)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1524, 22)
        StatusStrip1.TabIndex = 5
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(42, 17)
        ToolStripStatusLabel1.Text = "Ready!"
        ' 
        ' ToolStripMenuItem4
        ' 
        ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        ToolStripMenuItem4.Size = New Size(216, 6)
        ' 
        ' PhenotypeAnalysisToolStripMenuItem
        ' 
        PhenotypeAnalysisToolStripMenuItem.Name = "PhenotypeAnalysisToolStripMenuItem"
        PhenotypeAnalysisToolStripMenuItem.Size = New Size(219, 22)
        PhenotypeAnalysisToolStripMenuItem.Text = "Phenotype Analysis"
        ' 
        ' CellBrowser
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1524, 942)
        Controls.Add(SplitContainer1)
        Controls.Add(MenuStrip1)
        Controls.Add(StatusStrip1)
        MainMenuStrip = MenuStrip1
        Name = "CellBrowser"
        Text = "Cell Browser"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        SplitContainer2.Panel1.ResumeLayout(False)
        SplitContainer2.Panel2.ResumeLayout(False)
        SplitContainer2.Panel2.PerformLayout()
        CType(SplitContainer2, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer2.ResumeLayout(False)
        ContextMenuStrip1.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ContextMenuStrip2.ResumeLayout(False)
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
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
        ContextMenuStrip3.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        CType(BindingSource1, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
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
    Friend WithEvents ContextMenuStrip3 As ContextMenuStrip
    Friend WithEvents UnCheckAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ExpressionValueLogScaleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents Column2 As DataGridViewLinkColumn
    Friend WithEvents Column3 As DataGridViewLinkColumn
    Friend WithEvents Column4 As DataGridViewLinkColumn
    Friend WithEvents FilterRegulationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripSeparator
    Friend WithEvents PhenotypeAnalysisToolStripMenuItem As ToolStripMenuItem
End Class
