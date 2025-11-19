Imports Galaxy.Data.TableSheet
Imports Galaxy.Workbench.DockDocument

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormAnnotation
    Inherits DocumentWindow

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAnnotation))
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        SplitContainer1 = New SplitContainer()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        EnzymeAnnotationCmd = New AnnotationItem()
        OperonAnnotationCmd = New AnnotationItem()
        TFAnnotationCmd = New AnnotationItem()
        TFBSAnnotationCmd = New AnnotationItem()
        TransporterAnnotationCmd = New AnnotationItem()
        SplitContainer2 = New SplitContainer()
        TabControl2 = New TabControl()
        TabPage3 = New TabPage()
        DataGridView1 = New AdvancedDataGridView()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        ViewNetworkToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        ViewEnzymeInRegistryToolStripMenuItem = New ToolStripMenuItem()
        ToolStrip2 = New AdvancedDataGridViewSearchToolBar()
        TabPage4 = New TabPage()
        AdvancedDataGridView2 = New AdvancedDataGridView()
        ContextMenuStrip3 = New ContextMenuStrip(components)
        ViewOperonToolStripMenuItem = New ToolStripMenuItem()
        AdvancedDataGridViewSearchToolBar2 = New AdvancedDataGridViewSearchToolBar()
        TabPage5 = New TabPage()
        AdvancedDataGridView4 = New AdvancedDataGridView()
        AdvancedDataGridViewSearchToolBar4 = New AdvancedDataGridViewSearchToolBar()
        TabPage6 = New TabPage()
        AdvancedDataGridView3 = New AdvancedDataGridView()
        AdvancedDataGridViewSearchToolBar3 = New AdvancedDataGridViewSearchToolBar()
        TabPage7 = New TabPage()
        AdvancedDataGridView5 = New AdvancedDataGridView()
        AdvancedDataGridViewSearchToolBar5 = New AdvancedDataGridViewSearchToolBar()
        AdvancedDataGridView1 = New AdvancedDataGridView()
        ContextMenuStrip2 = New ContextMenuStrip(components)
        ViewGeneInRegistryToolStripMenuItem = New ToolStripMenuItem()
        AdvancedDataGridViewSearchToolBar1 = New AdvancedDataGridViewSearchToolBar()
        PictureBox1 = New PictureBox()
        TabPage2 = New TabPage()
        TextBox2 = New TextBox()
        Label2 = New Label()
        Button1 = New Button()
        TextBox1 = New TextBox()
        Label1 = New Label()
        ToolStrip1 = New ToolStrip()
        ToolStripButton1 = New ToolStripButton()
        GroupBox1 = New GroupBox()
        GroupBox2 = New GroupBox()
        Panel1 = New Panel()
        RichTextBox1 = New RichTextBox()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        CType(SplitContainer2, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer2.Panel1.SuspendLayout()
        SplitContainer2.Panel2.SuspendLayout()
        SplitContainer2.SuspendLayout()
        TabControl2.SuspendLayout()
        TabPage3.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        ContextMenuStrip1.SuspendLayout()
        TabPage4.SuspendLayout()
        CType(AdvancedDataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        ContextMenuStrip3.SuspendLayout()
        TabPage5.SuspendLayout()
        CType(AdvancedDataGridView4, ComponentModel.ISupportInitialize).BeginInit()
        TabPage6.SuspendLayout()
        CType(AdvancedDataGridView3, ComponentModel.ISupportInitialize).BeginInit()
        TabPage7.SuspendLayout()
        CType(AdvancedDataGridView5, ComponentModel.ISupportInitialize).BeginInit()
        CType(AdvancedDataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        ContextMenuStrip2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        TabPage2.SuspendLayout()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Location = New Point(0, 25)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1428, 882)
        TabControl1.TabIndex = 1
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(SplitContainer1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(1420, 854)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Annotation"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.FixedPanel = FixedPanel.Panel1
        SplitContainer1.Location = New Point(3, 3)
        SplitContainer1.Name = "SplitContainer1"
        SplitContainer1.Orientation = Orientation.Horizontal
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(FlowLayoutPanel1)
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(SplitContainer2)
        SplitContainer1.Size = New Size(1414, 848)
        SplitContainer1.SplitterDistance = 194
        SplitContainer1.TabIndex = 2
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.AutoScroll = True
        FlowLayoutPanel1.BackColor = Color.White
        FlowLayoutPanel1.Controls.Add(EnzymeAnnotationCmd)
        FlowLayoutPanel1.Controls.Add(OperonAnnotationCmd)
        FlowLayoutPanel1.Controls.Add(TFAnnotationCmd)
        FlowLayoutPanel1.Controls.Add(TFBSAnnotationCmd)
        FlowLayoutPanel1.Controls.Add(TransporterAnnotationCmd)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown
        FlowLayoutPanel1.Location = New Point(0, 0)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(1414, 194)
        FlowLayoutPanel1.TabIndex = 1
        FlowLayoutPanel1.WrapContents = False
        ' 
        ' EnzymeAnnotationCmd
        ' 
        EnzymeAnnotationCmd.BackColor = Color.WhiteSmoke
        EnzymeAnnotationCmd.Location = New Point(3, 3)
        EnzymeAnnotationCmd.Name = "EnzymeAnnotationCmd"
        EnzymeAnnotationCmd.Running = False
        EnzymeAnnotationCmd.Size = New Size(552, 41)
        EnzymeAnnotationCmd.TabIndex = 0
        ' 
        ' OperonAnnotationCmd
        ' 
        OperonAnnotationCmd.BackColor = Color.WhiteSmoke
        OperonAnnotationCmd.Location = New Point(3, 50)
        OperonAnnotationCmd.Name = "OperonAnnotationCmd"
        OperonAnnotationCmd.Running = False
        OperonAnnotationCmd.Size = New Size(552, 41)
        OperonAnnotationCmd.TabIndex = 1
        ' 
        ' TFAnnotationCmd
        ' 
        TFAnnotationCmd.BackColor = Color.WhiteSmoke
        TFAnnotationCmd.Location = New Point(3, 97)
        TFAnnotationCmd.Name = "TFAnnotationCmd"
        TFAnnotationCmd.Running = False
        TFAnnotationCmd.Size = New Size(552, 41)
        TFAnnotationCmd.TabIndex = 2
        ' 
        ' TFBSAnnotationCmd
        ' 
        TFBSAnnotationCmd.BackColor = Color.WhiteSmoke
        TFBSAnnotationCmd.Location = New Point(3, 144)
        TFBSAnnotationCmd.Name = "TFBSAnnotationCmd"
        TFBSAnnotationCmd.Running = False
        TFBSAnnotationCmd.Size = New Size(552, 41)
        TFBSAnnotationCmd.TabIndex = 3
        ' 
        ' TransporterAnnotationCmd
        ' 
        TransporterAnnotationCmd.BackColor = Color.WhiteSmoke
        TransporterAnnotationCmd.Location = New Point(3, 191)
        TransporterAnnotationCmd.Name = "TransporterAnnotationCmd"
        TransporterAnnotationCmd.Running = False
        TransporterAnnotationCmd.Size = New Size(552, 41)
        TransporterAnnotationCmd.TabIndex = 4
        ' 
        ' SplitContainer2
        ' 
        SplitContainer2.Dock = DockStyle.Fill
        SplitContainer2.Location = New Point(0, 0)
        SplitContainer2.Name = "SplitContainer2"
        ' 
        ' SplitContainer2.Panel1
        ' 
        SplitContainer2.Panel1.Controls.Add(TabControl2)
        ' 
        ' SplitContainer2.Panel2
        ' 
        SplitContainer2.Panel2.Controls.Add(AdvancedDataGridView1)
        SplitContainer2.Panel2.Controls.Add(AdvancedDataGridViewSearchToolBar1)
        SplitContainer2.Panel2.Controls.Add(PictureBox1)
        SplitContainer2.Size = New Size(1414, 650)
        SplitContainer2.SplitterDistance = 777
        SplitContainer2.TabIndex = 1
        ' 
        ' TabControl2
        ' 
        TabControl2.Controls.Add(TabPage3)
        TabControl2.Controls.Add(TabPage4)
        TabControl2.Controls.Add(TabPage5)
        TabControl2.Controls.Add(TabPage6)
        TabControl2.Controls.Add(TabPage7)
        TabControl2.Dock = DockStyle.Fill
        TabControl2.Location = New Point(0, 0)
        TabControl2.Name = "TabControl2"
        TabControl2.SelectedIndex = 0
        TabControl2.Size = New Size(777, 650)
        TabControl2.TabIndex = 0
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(DataGridView1)
        TabPage3.Controls.Add(ToolStrip2)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(769, 622)
        TabPage3.TabIndex = 0
        TabPage3.Text = "Enzyme"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.BackgroundColor = Color.WhiteSmoke
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.ContextMenuStrip = ContextMenuStrip1
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Window
        DataGridViewCellStyle1.Font = New Font("Cambria", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.FilterAndSortEnabled = True
        DataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        DataGridView1.Location = New Point(3, 30)
        DataGridView1.MaxFilterButtonImageHeight = 23
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.RightToLeft = RightToLeft.No
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(763, 589)
        DataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        DataGridView1.TabIndex = 0
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {ViewNetworkToolStripMenuItem, ToolStripMenuItem1, ViewEnzymeInRegistryToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(202, 54)
        ' 
        ' ViewNetworkToolStripMenuItem
        ' 
        ViewNetworkToolStripMenuItem.Name = "ViewNetworkToolStripMenuItem"
        ViewNetworkToolStripMenuItem.Size = New Size(201, 22)
        ViewNetworkToolStripMenuItem.Text = "View Network"
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(198, 6)
        ' 
        ' ViewEnzymeInRegistryToolStripMenuItem
        ' 
        ViewEnzymeInRegistryToolStripMenuItem.Name = "ViewEnzymeInRegistryToolStripMenuItem"
        ViewEnzymeInRegistryToolStripMenuItem.Size = New Size(201, 22)
        ViewEnzymeInRegistryToolStripMenuItem.Text = "View Enzyme In Registry"
        ' 
        ' ToolStrip2
        ' 
        ToolStrip2.AllowMerge = False
        ToolStrip2.GripStyle = ToolStripGripStyle.Hidden
        ToolStrip2.Location = New Point(3, 3)
        ToolStrip2.MaximumSize = New Size(0, 27)
        ToolStrip2.MinimumSize = New Size(0, 27)
        ToolStrip2.Name = "ToolStrip2"
        ToolStrip2.RenderMode = ToolStripRenderMode.Professional
        ToolStrip2.Size = New Size(763, 27)
        ToolStrip2.TabIndex = 1
        ToolStrip2.Text = "ToolStrip2"
        ' 
        ' TabPage4
        ' 
        TabPage4.Controls.Add(AdvancedDataGridView2)
        TabPage4.Controls.Add(AdvancedDataGridViewSearchToolBar2)
        TabPage4.Location = New Point(4, 24)
        TabPage4.Name = "TabPage4"
        TabPage4.Padding = New Padding(3)
        TabPage4.Size = New Size(769, 622)
        TabPage4.TabIndex = 1
        TabPage4.Text = "Transcript Units"
        TabPage4.UseVisualStyleBackColor = True
        ' 
        ' AdvancedDataGridView2
        ' 
        AdvancedDataGridView2.AllowUserToAddRows = False
        AdvancedDataGridView2.BackgroundColor = Color.WhiteSmoke
        AdvancedDataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        AdvancedDataGridView2.ContextMenuStrip = ContextMenuStrip3
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Cambria", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        AdvancedDataGridView2.DefaultCellStyle = DataGridViewCellStyle2
        AdvancedDataGridView2.Dock = DockStyle.Fill
        AdvancedDataGridView2.FilterAndSortEnabled = True
        AdvancedDataGridView2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView2.Location = New Point(3, 30)
        AdvancedDataGridView2.MaxFilterButtonImageHeight = 23
        AdvancedDataGridView2.Name = "AdvancedDataGridView2"
        AdvancedDataGridView2.ReadOnly = True
        AdvancedDataGridView2.RightToLeft = RightToLeft.No
        AdvancedDataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        AdvancedDataGridView2.Size = New Size(763, 589)
        AdvancedDataGridView2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView2.TabIndex = 2
        ' 
        ' ContextMenuStrip3
        ' 
        ContextMenuStrip3.Items.AddRange(New ToolStripItem() {ViewOperonToolStripMenuItem})
        ContextMenuStrip3.Name = "ContextMenuStrip3"
        ContextMenuStrip3.Size = New Size(182, 26)
        ' 
        ' ViewOperonToolStripMenuItem
        ' 
        ViewOperonToolStripMenuItem.Name = "ViewOperonToolStripMenuItem"
        ViewOperonToolStripMenuItem.Size = New Size(181, 22)
        ViewOperonToolStripMenuItem.Text = "View Source Operon"
        ' 
        ' AdvancedDataGridViewSearchToolBar2
        ' 
        AdvancedDataGridViewSearchToolBar2.AllowMerge = False
        AdvancedDataGridViewSearchToolBar2.GripStyle = ToolStripGripStyle.Hidden
        AdvancedDataGridViewSearchToolBar2.Location = New Point(3, 3)
        AdvancedDataGridViewSearchToolBar2.MaximumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar2.MinimumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar2.Name = "AdvancedDataGridViewSearchToolBar2"
        AdvancedDataGridViewSearchToolBar2.RenderMode = ToolStripRenderMode.Professional
        AdvancedDataGridViewSearchToolBar2.Size = New Size(763, 27)
        AdvancedDataGridViewSearchToolBar2.TabIndex = 3
        AdvancedDataGridViewSearchToolBar2.Text = "AdvancedDataGridViewSearchToolBar2"
        ' 
        ' TabPage5
        ' 
        TabPage5.Controls.Add(AdvancedDataGridView4)
        TabPage5.Controls.Add(AdvancedDataGridViewSearchToolBar4)
        TabPage5.Location = New Point(4, 24)
        TabPage5.Name = "TabPage5"
        TabPage5.Size = New Size(769, 622)
        TabPage5.TabIndex = 2
        TabPage5.Text = "Transcript Factor"
        TabPage5.UseVisualStyleBackColor = True
        ' 
        ' AdvancedDataGridView4
        ' 
        AdvancedDataGridView4.AllowUserToAddRows = False
        AdvancedDataGridView4.BackgroundColor = Color.WhiteSmoke
        AdvancedDataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        AdvancedDataGridView4.ContextMenuStrip = ContextMenuStrip1
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.Window
        DataGridViewCellStyle3.Font = New Font("Cambria", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        AdvancedDataGridView4.DefaultCellStyle = DataGridViewCellStyle3
        AdvancedDataGridView4.Dock = DockStyle.Fill
        AdvancedDataGridView4.FilterAndSortEnabled = True
        AdvancedDataGridView4.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView4.Location = New Point(0, 27)
        AdvancedDataGridView4.MaxFilterButtonImageHeight = 23
        AdvancedDataGridView4.Name = "AdvancedDataGridView4"
        AdvancedDataGridView4.ReadOnly = True
        AdvancedDataGridView4.RightToLeft = RightToLeft.No
        AdvancedDataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        AdvancedDataGridView4.Size = New Size(769, 595)
        AdvancedDataGridView4.SortStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView4.TabIndex = 2
        ' 
        ' AdvancedDataGridViewSearchToolBar4
        ' 
        AdvancedDataGridViewSearchToolBar4.AllowMerge = False
        AdvancedDataGridViewSearchToolBar4.GripStyle = ToolStripGripStyle.Hidden
        AdvancedDataGridViewSearchToolBar4.Location = New Point(0, 0)
        AdvancedDataGridViewSearchToolBar4.MaximumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar4.MinimumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar4.Name = "AdvancedDataGridViewSearchToolBar4"
        AdvancedDataGridViewSearchToolBar4.RenderMode = ToolStripRenderMode.Professional
        AdvancedDataGridViewSearchToolBar4.Size = New Size(769, 27)
        AdvancedDataGridViewSearchToolBar4.TabIndex = 3
        AdvancedDataGridViewSearchToolBar4.Text = "AdvancedDataGridViewSearchToolBar4"
        ' 
        ' TabPage6
        ' 
        TabPage6.Controls.Add(AdvancedDataGridView3)
        TabPage6.Controls.Add(AdvancedDataGridViewSearchToolBar3)
        TabPage6.Location = New Point(4, 24)
        TabPage6.Name = "TabPage6"
        TabPage6.Padding = New Padding(3)
        TabPage6.Size = New Size(769, 622)
        TabPage6.TabIndex = 3
        TabPage6.Text = "TFBS Annotation"
        TabPage6.UseVisualStyleBackColor = True
        ' 
        ' AdvancedDataGridView3
        ' 
        AdvancedDataGridView3.AllowUserToAddRows = False
        AdvancedDataGridView3.BackgroundColor = Color.WhiteSmoke
        AdvancedDataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        AdvancedDataGridView3.ContextMenuStrip = ContextMenuStrip3
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.Window
        DataGridViewCellStyle4.Font = New Font("Cambria", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        AdvancedDataGridView3.DefaultCellStyle = DataGridViewCellStyle4
        AdvancedDataGridView3.Dock = DockStyle.Fill
        AdvancedDataGridView3.FilterAndSortEnabled = True
        AdvancedDataGridView3.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView3.Location = New Point(3, 30)
        AdvancedDataGridView3.MaxFilterButtonImageHeight = 23
        AdvancedDataGridView3.Name = "AdvancedDataGridView3"
        AdvancedDataGridView3.ReadOnly = True
        AdvancedDataGridView3.RightToLeft = RightToLeft.No
        AdvancedDataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        AdvancedDataGridView3.Size = New Size(763, 589)
        AdvancedDataGridView3.SortStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView3.TabIndex = 4
        ' 
        ' AdvancedDataGridViewSearchToolBar3
        ' 
        AdvancedDataGridViewSearchToolBar3.AllowMerge = False
        AdvancedDataGridViewSearchToolBar3.GripStyle = ToolStripGripStyle.Hidden
        AdvancedDataGridViewSearchToolBar3.Location = New Point(3, 3)
        AdvancedDataGridViewSearchToolBar3.MaximumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar3.MinimumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar3.Name = "AdvancedDataGridViewSearchToolBar3"
        AdvancedDataGridViewSearchToolBar3.RenderMode = ToolStripRenderMode.Professional
        AdvancedDataGridViewSearchToolBar3.Size = New Size(763, 27)
        AdvancedDataGridViewSearchToolBar3.TabIndex = 5
        AdvancedDataGridViewSearchToolBar3.Text = "AdvancedDataGridViewSearchToolBar3"
        ' 
        ' TabPage7
        ' 
        TabPage7.Controls.Add(AdvancedDataGridView5)
        TabPage7.Controls.Add(AdvancedDataGridViewSearchToolBar5)
        TabPage7.Location = New Point(4, 24)
        TabPage7.Name = "TabPage7"
        TabPage7.Padding = New Padding(3)
        TabPage7.Size = New Size(769, 622)
        TabPage7.TabIndex = 4
        TabPage7.Text = "Membrane Transporter"
        TabPage7.UseVisualStyleBackColor = True
        ' 
        ' AdvancedDataGridView5
        ' 
        AdvancedDataGridView5.AllowUserToAddRows = False
        AdvancedDataGridView5.BackgroundColor = Color.WhiteSmoke
        AdvancedDataGridView5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        AdvancedDataGridView5.ContextMenuStrip = ContextMenuStrip3
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Window
        DataGridViewCellStyle5.Font = New Font("Cambria", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        AdvancedDataGridView5.DefaultCellStyle = DataGridViewCellStyle5
        AdvancedDataGridView5.Dock = DockStyle.Fill
        AdvancedDataGridView5.FilterAndSortEnabled = True
        AdvancedDataGridView5.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView5.Location = New Point(3, 30)
        AdvancedDataGridView5.MaxFilterButtonImageHeight = 23
        AdvancedDataGridView5.Name = "AdvancedDataGridView5"
        AdvancedDataGridView5.ReadOnly = True
        AdvancedDataGridView5.RightToLeft = RightToLeft.No
        AdvancedDataGridView5.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        AdvancedDataGridView5.Size = New Size(763, 589)
        AdvancedDataGridView5.SortStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView5.TabIndex = 6
        ' 
        ' AdvancedDataGridViewSearchToolBar5
        ' 
        AdvancedDataGridViewSearchToolBar5.AllowMerge = False
        AdvancedDataGridViewSearchToolBar5.GripStyle = ToolStripGripStyle.Hidden
        AdvancedDataGridViewSearchToolBar5.Location = New Point(3, 3)
        AdvancedDataGridViewSearchToolBar5.MaximumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar5.MinimumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar5.Name = "AdvancedDataGridViewSearchToolBar5"
        AdvancedDataGridViewSearchToolBar5.RenderMode = ToolStripRenderMode.Professional
        AdvancedDataGridViewSearchToolBar5.Size = New Size(763, 27)
        AdvancedDataGridViewSearchToolBar5.TabIndex = 7
        AdvancedDataGridViewSearchToolBar5.Text = "AdvancedDataGridViewSearchToolBar5"
        ' 
        ' AdvancedDataGridView1
        ' 
        AdvancedDataGridView1.AllowUserToAddRows = False
        AdvancedDataGridView1.BackgroundColor = Color.WhiteSmoke
        AdvancedDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        AdvancedDataGridView1.ContextMenuStrip = ContextMenuStrip2
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = SystemColors.Window
        DataGridViewCellStyle6.Font = New Font("Cambria", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.False
        AdvancedDataGridView1.DefaultCellStyle = DataGridViewCellStyle6
        AdvancedDataGridView1.Dock = DockStyle.Fill
        AdvancedDataGridView1.FilterAndSortEnabled = True
        AdvancedDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView1.Location = New Point(0, 27)
        AdvancedDataGridView1.MaxFilterButtonImageHeight = 23
        AdvancedDataGridView1.Name = "AdvancedDataGridView1"
        AdvancedDataGridView1.ReadOnly = True
        AdvancedDataGridView1.RightToLeft = RightToLeft.No
        AdvancedDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        AdvancedDataGridView1.Size = New Size(633, 381)
        AdvancedDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView1.TabIndex = 2
        ' 
        ' ContextMenuStrip2
        ' 
        ContextMenuStrip2.Items.AddRange(New ToolStripItem() {ViewGeneInRegistryToolStripMenuItem})
        ContextMenuStrip2.Name = "ContextMenuStrip2"
        ContextMenuStrip2.Size = New Size(188, 26)
        ' 
        ' ViewGeneInRegistryToolStripMenuItem
        ' 
        ViewGeneInRegistryToolStripMenuItem.Name = "ViewGeneInRegistryToolStripMenuItem"
        ViewGeneInRegistryToolStripMenuItem.Size = New Size(187, 22)
        ViewGeneInRegistryToolStripMenuItem.Text = "View Gene In Registry"
        ' 
        ' AdvancedDataGridViewSearchToolBar1
        ' 
        AdvancedDataGridViewSearchToolBar1.AllowMerge = False
        AdvancedDataGridViewSearchToolBar1.GripStyle = ToolStripGripStyle.Hidden
        AdvancedDataGridViewSearchToolBar1.Location = New Point(0, 0)
        AdvancedDataGridViewSearchToolBar1.MaximumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar1.MinimumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar1.Name = "AdvancedDataGridViewSearchToolBar1"
        AdvancedDataGridViewSearchToolBar1.RenderMode = ToolStripRenderMode.Professional
        AdvancedDataGridViewSearchToolBar1.Size = New Size(633, 27)
        AdvancedDataGridViewSearchToolBar1.TabIndex = 3
        AdvancedDataGridViewSearchToolBar1.Text = "AdvancedDataGridViewSearchToolBar1"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.White
        PictureBox1.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox1.Dock = DockStyle.Bottom
        PictureBox1.Location = New Point(0, 408)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(633, 242)
        PictureBox1.TabIndex = 4
        PictureBox1.TabStop = False
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(RichTextBox1)
        TabPage2.Controls.Add(Panel1)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(1420, 854)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Settings"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(15, 116)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(660, 23)
        TextBox2.TabIndex = 4
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(15, 98)
        Label2.Name = "Label2"
        Label2.Size = New Size(61, 15)
        Label2.TabIndex = 3
        Label2.Text = "Set Server:"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(681, 63)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 2
        Button1.Text = "..."
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(15, 63)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(660, 23)
        TextBox1.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(15, 41)
        Label1.Name = "Label1"
        Label1.Size = New Size(92, 15)
        Label1.TabIndex = 0
        Label1.Text = "Set NCBI Blast+:"
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton1})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1428, 25)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), Image)
        ToolStripButton1.ImageTransparentColor = Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(23, 22)
        ToolStripButton1.Text = "Build Model"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Location = New Point(13, 268)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(774, 474)
        GroupBox1.TabIndex = 5
        GroupBox1.TabStop = False
        GroupBox1.Text = "Cellular Component Annotation Search"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Location = New Point(13, 14)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(774, 236)
        GroupBox2.TabIndex = 6
        GroupBox2.TabStop = False
        GroupBox2.Text = "Edit Model Metadata"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(GroupBox2)
        Panel1.Controls.Add(GroupBox1)
        Panel1.Dock = DockStyle.Left
        Panel1.Location = New Point(3, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(803, 848)
        Panel1.TabIndex = 7
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.BorderStyle = BorderStyle.None
        RichTextBox1.Dock = DockStyle.Fill
        RichTextBox1.Location = New Point(806, 3)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(611, 848)
        RichTextBox1.TabIndex = 8
        RichTextBox1.Text = ""
        ' 
        ' FormAnnotation
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1428, 907)
        Controls.Add(TabControl1)
        Controls.Add(ToolStrip1)
        DockAreas = Microsoft.VisualStudio.WinForms.Docking.DockAreas.Float Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockLeft Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockRight Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockTop Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockBottom Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.Document
        DoubleBuffered = True
        Name = "FormAnnotation"
        ShowHint = Microsoft.VisualStudio.WinForms.Docking.DockState.Unknown
        TabPageContextMenuStrip = DockContextMenuStrip1
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        FlowLayoutPanel1.ResumeLayout(False)
        SplitContainer2.Panel1.ResumeLayout(False)
        SplitContainer2.Panel2.ResumeLayout(False)
        SplitContainer2.Panel2.PerformLayout()
        CType(SplitContainer2, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer2.ResumeLayout(False)
        TabControl2.ResumeLayout(False)
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ContextMenuStrip1.ResumeLayout(False)
        TabPage4.ResumeLayout(False)
        TabPage4.PerformLayout()
        CType(AdvancedDataGridView2, ComponentModel.ISupportInitialize).EndInit()
        ContextMenuStrip3.ResumeLayout(False)
        TabPage5.ResumeLayout(False)
        TabPage5.PerformLayout()
        CType(AdvancedDataGridView4, ComponentModel.ISupportInitialize).EndInit()
        TabPage6.ResumeLayout(False)
        TabPage6.PerformLayout()
        CType(AdvancedDataGridView3, ComponentModel.ISupportInitialize).EndInit()
        TabPage7.ResumeLayout(False)
        TabPage7.PerformLayout()
        CType(AdvancedDataGridView5, ComponentModel.ISupportInitialize).EndInit()
        CType(AdvancedDataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ContextMenuStrip2.ResumeLayout(False)
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        TabPage2.ResumeLayout(False)
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        Panel1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents DataGridView1 As AdvancedDataGridView
    Friend WithEvents ToolStrip2 As AdvancedDataGridViewSearchToolBar
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents EnzymeAnnotationCmd As AnnotationItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents AdvancedDataGridView1 As AdvancedDataGridView
    Friend WithEvents AdvancedDataGridViewSearchToolBar1 As AdvancedDataGridViewSearchToolBar
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ViewNetworkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OperonAnnotationCmd As AnnotationItem
    Friend WithEvents AdvancedDataGridView2 As AdvancedDataGridView
    Friend WithEvents AdvancedDataGridViewSearchToolBar2 As AdvancedDataGridViewSearchToolBar
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ViewEnzymeInRegistryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents ViewGeneInRegistryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip3 As ContextMenuStrip
    Friend WithEvents ViewOperonToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents TFAnnotationCmd As AnnotationItem
    Friend WithEvents TFBSAnnotationCmd As AnnotationItem
    Friend WithEvents AdvancedDataGridView3 As AdvancedDataGridView
    Friend WithEvents AdvancedDataGridViewSearchToolBar3 As AdvancedDataGridViewSearchToolBar
    Friend WithEvents AdvancedDataGridView4 As AdvancedDataGridView
    Friend WithEvents AdvancedDataGridViewSearchToolBar4 As AdvancedDataGridViewSearchToolBar
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents AdvancedDataGridView5 As AdvancedDataGridView
    Friend WithEvents AdvancedDataGridViewSearchToolBar5 As AdvancedDataGridViewSearchToolBar
    Friend WithEvents TransporterAnnotationCmd As AnnotationItem
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
End Class
