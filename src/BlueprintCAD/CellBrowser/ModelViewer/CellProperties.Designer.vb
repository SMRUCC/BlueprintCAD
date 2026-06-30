Imports Galaxy.Workbench.DockDocument

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CellProperties
    Inherits DocumentWindow

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
        GroupBox1 = New GroupBox()
        GroupBox2 = New GroupBox()
        ListBox1 = New ListBox()
        GroupBox3 = New GroupBox()
        AdvancedDataGridView1 = New Galaxy.Data.TableSheet.AdvancedDataGridView()
        AdvancedDataGridViewSearchToolBar1 = New Galaxy.Data.TableSheet.AdvancedDataGridViewSearchToolBar()
        SplitContainer1 = New SplitContainer()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        CopyIDToolStripMenuItem = New ToolStripMenuItem()
        ViewToolStripMenuItem = New ToolStripMenuItem()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        CType(AdvancedDataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        ContextMenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Dock = DockStyle.Left
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(412, 395)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "Cell Metadata"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(ListBox1)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(412, 0)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(488, 395)
        GroupBox2.TabIndex = 2
        GroupBox2.TabStop = False
        GroupBox2.Text = "Cell Phenotype Traits"
        ' 
        ' ListBox1
        ' 
        ListBox1.Dock = DockStyle.Fill
        ListBox1.FormattingEnabled = True
        ListBox1.Location = New Point(3, 19)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(482, 373)
        ListBox1.TabIndex = 0
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(AdvancedDataGridView1)
        GroupBox3.Controls.Add(AdvancedDataGridViewSearchToolBar1)
        GroupBox3.Dock = DockStyle.Fill
        GroupBox3.Location = New Point(0, 0)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(900, 392)
        GroupBox3.TabIndex = 3
        GroupBox3.TabStop = False
        GroupBox3.Text = "Cell Environment I/O"
        ' 
        ' AdvancedDataGridView1
        ' 
        AdvancedDataGridView1.AllowUserToAddRows = False
        AdvancedDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        AdvancedDataGridView1.ContextMenuStrip = ContextMenuStrip1
        AdvancedDataGridView1.Dock = DockStyle.Fill
        AdvancedDataGridView1.FilterAndSortEnabled = True
        AdvancedDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView1.Location = New Point(3, 46)
        AdvancedDataGridView1.MaxFilterButtonImageHeight = 23
        AdvancedDataGridView1.Name = "AdvancedDataGridView1"
        AdvancedDataGridView1.ReadOnly = True
        AdvancedDataGridView1.RightToLeft = RightToLeft.No
        AdvancedDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        AdvancedDataGridView1.Size = New Size(894, 343)
        AdvancedDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        AdvancedDataGridView1.TabIndex = 1
        ' 
        ' AdvancedDataGridViewSearchToolBar1
        ' 
        AdvancedDataGridViewSearchToolBar1.AllowMerge = False
        AdvancedDataGridViewSearchToolBar1.GripStyle = ToolStripGripStyle.Hidden
        AdvancedDataGridViewSearchToolBar1.Location = New Point(3, 19)
        AdvancedDataGridViewSearchToolBar1.MaximumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar1.MinimumSize = New Size(0, 27)
        AdvancedDataGridViewSearchToolBar1.Name = "AdvancedDataGridViewSearchToolBar1"
        AdvancedDataGridViewSearchToolBar1.RenderMode = ToolStripRenderMode.Professional
        AdvancedDataGridViewSearchToolBar1.Size = New Size(894, 27)
        AdvancedDataGridViewSearchToolBar1.TabIndex = 0
        AdvancedDataGridViewSearchToolBar1.Text = "AdvancedDataGridViewSearchToolBar1"
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(0, 0)
        SplitContainer1.Name = "SplitContainer1"
        SplitContainer1.Orientation = Orientation.Horizontal
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.Controls.Add(GroupBox2)
        SplitContainer1.Panel1.Controls.Add(GroupBox1)
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(GroupBox3)
        SplitContainer1.Size = New Size(900, 791)
        SplitContainer1.SplitterDistance = 395
        SplitContainer1.TabIndex = 4
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {CopyIDToolStripMenuItem, ViewToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(181, 70)
        ' 
        ' CopyIDToolStripMenuItem
        ' 
        CopyIDToolStripMenuItem.Name = "CopyIDToolStripMenuItem"
        CopyIDToolStripMenuItem.Size = New Size(180, 22)
        CopyIDToolStripMenuItem.Text = "Copy ID"
        ' 
        ' ViewToolStripMenuItem
        ' 
        ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        ViewToolStripMenuItem.Size = New Size(180, 22)
        ViewToolStripMenuItem.Text = "View"
        ' 
        ' CellProperties
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(900, 791)
        Controls.Add(SplitContainer1)
        DockAreas = Microsoft.VisualStudio.WinForms.Docking.DockAreas.Float Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockLeft Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockRight Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockTop Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockBottom Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.Document
        DoubleBuffered = True
        Name = "CellProperties"
        ShowHint = Microsoft.VisualStudio.WinForms.Docking.DockState.Unknown
        TabPageContextMenuStrip = DockContextMenuStrip1
        GroupBox2.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        CType(AdvancedDataGridView1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        ContextMenuStrip1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents AdvancedDataGridView1 As Galaxy.Data.TableSheet.AdvancedDataGridView
    Friend WithEvents AdvancedDataGridViewSearchToolBar1 As Galaxy.Data.TableSheet.AdvancedDataGridViewSearchToolBar
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CopyIDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
End Class
