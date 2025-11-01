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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAnnotation))
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        SplitContainer1 = New SplitContainer()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        EnzymeAnnotationCmd = New AnnotationItem()
        OperonAnnotationCmd = New AnnotationItem()
        SplitContainer2 = New SplitContainer()
        TabControl2 = New TabControl()
        TabPage3 = New TabPage()
        DataGridView1 = New AdvancedDataGridView()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        ViewNetworkToolStripMenuItem = New ToolStripMenuItem()
        ToolStrip2 = New AdvancedDataGridViewSearchToolBar()
        TabPage4 = New TabPage()
        AdvancedDataGridView2 = New AdvancedDataGridView()
        AdvancedDataGridViewSearchToolBar2 = New AdvancedDataGridViewSearchToolBar()
        TabPage5 = New TabPage()
        AdvancedDataGridView1 = New AdvancedDataGridView()
        AdvancedDataGridViewSearchToolBar1 = New AdvancedDataGridViewSearchToolBar()
        TabPage2 = New TabPage()
        Button1 = New Button()
        TextBox1 = New TextBox()
        Label1 = New Label()
        ToolStrip1 = New ToolStrip()
        ToolStripButton1 = New ToolStripButton()
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
        CType(AdvancedDataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        TabPage2.SuspendLayout()
        ToolStrip1.SuspendLayout()
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
        TabControl1.Size = New Size(1354, 718)
        TabControl1.TabIndex = 1
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(SplitContainer1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(1346, 690)
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
        SplitContainer1.Size = New Size(1340, 684)
        SplitContainer1.SplitterDistance = 194
        SplitContainer1.TabIndex = 2
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.AutoScroll = True
        FlowLayoutPanel1.BackColor = Color.White
        FlowLayoutPanel1.Controls.Add(EnzymeAnnotationCmd)
        FlowLayoutPanel1.Controls.Add(OperonAnnotationCmd)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown
        FlowLayoutPanel1.Location = New Point(0, 0)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(1340, 194)
        FlowLayoutPanel1.TabIndex = 1
        ' 
        ' EnzymeAnnotationCmd
        ' 
        EnzymeAnnotationCmd.BackColor = Color.WhiteSmoke
        EnzymeAnnotationCmd.Location = New Point(3, 3)
        EnzymeAnnotationCmd.Name = "EnzymeAnnotationCmd"
        EnzymeAnnotationCmd.Running = False
        EnzymeAnnotationCmd.Size = New Size(552, 71)
        EnzymeAnnotationCmd.TabIndex = 0
        ' 
        ' OperonAnnotationCmd
        ' 
        OperonAnnotationCmd.BackColor = Color.WhiteSmoke
        OperonAnnotationCmd.Location = New Point(3, 80)
        OperonAnnotationCmd.Name = "OperonAnnotationCmd"
        OperonAnnotationCmd.Running = False
        OperonAnnotationCmd.Size = New Size(552, 71)
        OperonAnnotationCmd.TabIndex = 1
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
        SplitContainer2.Size = New Size(1340, 486)
        SplitContainer2.SplitterDistance = 737
        SplitContainer2.TabIndex = 1
        ' 
        ' TabControl2
        ' 
        TabControl2.Controls.Add(TabPage3)
        TabControl2.Controls.Add(TabPage4)
        TabControl2.Controls.Add(TabPage5)
        TabControl2.Dock = DockStyle.Fill
        TabControl2.Location = New Point(0, 0)
        TabControl2.Name = "TabControl2"
        TabControl2.SelectedIndex = 0
        TabControl2.Size = New Size(737, 486)
        TabControl2.TabIndex = 0
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(DataGridView1)
        TabPage3.Controls.Add(ToolStrip2)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(729, 458)
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
        DataGridView1.Size = New Size(723, 425)
        DataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        DataGridView1.TabIndex = 0
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {ViewNetworkToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(148, 26)
        ' 
        ' ViewNetworkToolStripMenuItem
        ' 
        ViewNetworkToolStripMenuItem.Name = "ViewNetworkToolStripMenuItem"
        ViewNetworkToolStripMenuItem.Size = New Size(147, 22)
        ViewNetworkToolStripMenuItem.Text = "View Network"
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
        ToolStrip2.Size = New Size(723, 27)
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
        TabPage4.Size = New Size(729, 458)
        TabPage4.TabIndex = 1
        TabPage4.Text = "Transcript Units"
        TabPage4.UseVisualStyleBackColor = True
        ' 
        ' AdvancedDataGridView2
        ' 
        AdvancedDataGridView2.AllowUserToAddRows = False
        AdvancedDataGridView2.BackgroundColor = Color.WhiteSmoke
        AdvancedDataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
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
        AdvancedDataGridView2.Size = New Size(723, 425)
        AdvancedDataGridView2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView2.TabIndex = 2
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
        AdvancedDataGridViewSearchToolBar2.Size = New Size(723, 27)
        AdvancedDataGridViewSearchToolBar2.TabIndex = 3
        AdvancedDataGridViewSearchToolBar2.Text = "AdvancedDataGridViewSearchToolBar2"
        ' 
        ' TabPage5
        ' 
        TabPage5.Location = New Point(4, 24)
        TabPage5.Name = "TabPage5"
        TabPage5.Size = New Size(729, 458)
        TabPage5.TabIndex = 2
        TabPage5.Text = "Transcript Factor"
        TabPage5.UseVisualStyleBackColor = True
        ' 
        ' AdvancedDataGridView1
        ' 
        AdvancedDataGridView1.AllowUserToAddRows = False
        AdvancedDataGridView1.BackgroundColor = Color.WhiteSmoke
        AdvancedDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.Window
        DataGridViewCellStyle3.Font = New Font("Cambria", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        AdvancedDataGridView1.DefaultCellStyle = DataGridViewCellStyle3
        AdvancedDataGridView1.Dock = DockStyle.Fill
        AdvancedDataGridView1.FilterAndSortEnabled = True
        AdvancedDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView1.Location = New Point(0, 27)
        AdvancedDataGridView1.MaxFilterButtonImageHeight = 23
        AdvancedDataGridView1.Name = "AdvancedDataGridView1"
        AdvancedDataGridView1.ReadOnly = True
        AdvancedDataGridView1.RightToLeft = RightToLeft.No
        AdvancedDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        AdvancedDataGridView1.Size = New Size(599, 459)
        AdvancedDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView1.TabIndex = 2
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
        AdvancedDataGridViewSearchToolBar1.Size = New Size(599, 27)
        AdvancedDataGridViewSearchToolBar1.TabIndex = 3
        AdvancedDataGridViewSearchToolBar1.Text = "AdvancedDataGridViewSearchToolBar1"
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(Button1)
        TabPage2.Controls.Add(TextBox1)
        TabPage2.Controls.Add(Label1)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(1346, 690)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Settings"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(1009, 20)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 2
        Button1.Text = "..."
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(146, 20)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(847, 23)
        TextBox1.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(27, 23)
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
        ToolStrip1.Size = New Size(1354, 25)
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
        ' FormAnnotation
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1354, 743)
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
        CType(AdvancedDataGridView1, ComponentModel.ISupportInitialize).EndInit()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
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
End Class
