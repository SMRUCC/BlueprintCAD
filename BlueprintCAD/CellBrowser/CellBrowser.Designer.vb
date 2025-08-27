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
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        OpenVirtualCellDataFileToolStripMenuItem = New ToolStripMenuItem()
        PlotView1 = New ggviewer.PlotView()
        GroupBox1 = New GroupBox()
        GroupBox2 = New GroupBox()
        SplitContainer2 = New SplitContainer()
        TreeView1 = New TreeView()
        DataGridView1 = New DataGridView()
        Column1 = New DataGridViewLinkColumn()
        Column2 = New DataGridViewLinkColumn()
        SplitContainer1 = New SplitContainer()
        MenuStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(SplitContainer2, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer2.Panel1.SuspendLayout()
        SplitContainer2.Panel2.SuspendLayout()
        SplitContainer2.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(1183, 24)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {OpenVirtualCellDataFileToolStripMenuItem})
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
        ' PlotView1
        ' 
        PlotView1.BackColor = Color.SkyBlue
        PlotView1.Debug = True
        PlotView1.Dock = DockStyle.Fill
        PlotView1.ggplot = Nothing
        PlotView1.Location = New Point(3, 19)
        PlotView1.Name = "PlotView1"
        PlotView1.ScaleFactor = 1.25F
        PlotView1.Size = New Size(1177, 528)
        PlotView1.TabIndex = 1
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(PlotView1)
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(1183, 550)
        GroupBox1.TabIndex = 2
        GroupBox1.TabStop = False
        GroupBox1.Text = "Dynamics Plot"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(SplitContainer2)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(0, 0)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(1183, 278)
        GroupBox2.TabIndex = 3
        GroupBox2.TabStop = False
        GroupBox2.Text = "Browser"
        ' 
        ' SplitContainer2
        ' 
        SplitContainer2.Dock = DockStyle.Fill
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
        SplitContainer2.Size = New Size(1177, 256)
        SplitContainer2.SplitterDistance = 344
        SplitContainer2.TabIndex = 0
        ' 
        ' TreeView1
        ' 
        TreeView1.Dock = DockStyle.Fill
        TreeView1.Location = New Point(0, 0)
        TreeView1.Name = "TreeView1"
        TreeView1.Size = New Size(344, 256)
        TreeView1.TabIndex = 0
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.BackgroundColor = Color.WhiteSmoke
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2})
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(0, 0)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(829, 256)
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
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(0, 24)
        SplitContainer1.Name = "SplitContainer1"
        SplitContainer1.Orientation = Orientation.Horizontal
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(GroupBox1)
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(GroupBox2)
        SplitContainer1.Size = New Size(1183, 832)
        SplitContainer1.SplitterDistance = 550
        SplitContainer1.TabIndex = 4
        ' 
        ' CellBrowser
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1183, 856)
        Controls.Add(SplitContainer1)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "CellBrowser"
        Text = "Cell Browser"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        SplitContainer2.Panel1.ResumeLayout(False)
        SplitContainer2.Panel2.ResumeLayout(False)
        CType(SplitContainer2, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer2.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenVirtualCellDataFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PlotView1 As ggviewer.PlotView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Column1 As DataGridViewLinkColumn
    Friend WithEvents Column2 As DataGridViewLinkColumn
End Class
