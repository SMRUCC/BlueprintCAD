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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAnnotation))
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        TabControl2 = New TabControl()
        TabPage3 = New TabPage()
        DataGridView1 = New AdvancedDataGridView()
        ToolStrip2 = New AdvancedDataGridViewSearchToolBar()
        TabPage4 = New TabPage()
        TabPage5 = New TabPage()
        TabPage2 = New TabPage()
        Button1 = New Button()
        TextBox1 = New TextBox()
        Label1 = New Label()
        ToolStrip1 = New ToolStrip()
        ToolStripButton1 = New ToolStripButton()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        TabControl2.SuspendLayout()
        TabPage3.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
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
        TabPage1.Controls.Add(TabControl2)
        TabPage1.Location = New Point(4, 26)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(1346, 688)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Annotation"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' TabControl2
        ' 
        TabControl2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TabControl2.Controls.Add(TabPage3)
        TabControl2.Controls.Add(TabPage4)
        TabControl2.Controls.Add(TabPage5)
        TabControl2.Location = New Point(8, 231)
        TabControl2.Name = "TabControl2"
        TabControl2.SelectedIndex = 0
        TabControl2.Size = New Size(1330, 449)
        TabControl2.TabIndex = 0
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(DataGridView1)
        TabPage3.Controls.Add(ToolStrip2)
        TabPage3.Location = New Point(4, 26)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(1322, 419)
        TabPage3.TabIndex = 0
        TabPage3.Text = "Enzyme"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.BackgroundColor = Color.WhiteSmoke
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.FilterAndSortEnabled = True
        DataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        DataGridView1.Location = New Point(3, 30)
        DataGridView1.MaxFilterButtonImageHeight = 23
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RightToLeft = RightToLeft.No
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(1316, 386)
        DataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        DataGridView1.TabIndex = 0
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
        ToolStrip2.Size = New Size(1316, 27)
        ToolStrip2.TabIndex = 1
        ToolStrip2.Text = "ToolStrip2"
        ' 
        ' TabPage4
        ' 
        TabPage4.Location = New Point(4, 26)
        TabPage4.Name = "TabPage4"
        TabPage4.Padding = New Padding(3)
        TabPage4.Size = New Size(1322, 419)
        TabPage4.TabIndex = 1
        TabPage4.Text = "Transcript Units"
        TabPage4.UseVisualStyleBackColor = True
        ' 
        ' TabPage5
        ' 
        TabPage5.Location = New Point(4, 26)
        TabPage5.Name = "TabPage5"
        TabPage5.Size = New Size(1322, 419)
        TabPage5.TabIndex = 2
        TabPage5.Text = "Transcript Factor"
        TabPage5.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(Button1)
        TabPage2.Controls.Add(TextBox1)
        TabPage2.Controls.Add(Label1)
        TabPage2.Location = New Point(4, 26)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(1346, 688)
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
        Label1.Size = New Size(104, 17)
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
        AutoScaleDimensions = New SizeF(7F, 17F)
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
        TabControl2.ResumeLayout(False)
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
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
End Class
