Imports Galaxy.Workbench.CommonDialogs

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPhenotypePath
    Inherits InputDialog

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
        ListBox1 = New ListBox()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        SetAsFromNodeToolStripMenuItem = New ToolStripMenuItem()
        ListBox2 = New ListBox()
        ContextMenuStrip2 = New ContextMenuStrip(components)
        SetAsTargetNodeToolStripMenuItem = New ToolStripMenuItem()
        Label1 = New Label()
        DataGridView1 = New DataGridView()
        Label4 = New Label()
        TextBox1 = New TextBox()
        Button1 = New Button()
        Button2 = New Button()
        TextBox2 = New TextBox()
        Label5 = New Label()
        Button3 = New Button()
        ToolStrip1 = New ToolStrip()
        ToolStripLabel1 = New ToolStripLabel()
        GroupBox1 = New GroupBox()
        Label7 = New Label()
        Label6 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        GroupBox2 = New GroupBox()
        SplitContainer1 = New SplitContainer()
        ContextMenuStrip1.SuspendLayout()
        ContextMenuStrip2.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ListBox1
        ' 
        ListBox1.ContextMenuStrip = ContextMenuStrip1
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(6, 53)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(485, 274)
        ListBox1.TabIndex = 0
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {SetAsFromNodeToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(170, 26)
        ' 
        ' SetAsFromNodeToolStripMenuItem
        ' 
        SetAsFromNodeToolStripMenuItem.Name = "SetAsFromNodeToolStripMenuItem"
        SetAsFromNodeToolStripMenuItem.Size = New Size(169, 22)
        SetAsFromNodeToolStripMenuItem.Text = "Set As From Node"
        ' 
        ' ListBox2
        ' 
        ListBox2.ContextMenuStrip = ContextMenuStrip2
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(523, 54)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(514, 274)
        ListBox2.TabIndex = 1
        ' 
        ' ContextMenuStrip2
        ' 
        ContextMenuStrip2.Items.AddRange(New ToolStripItem() {SetAsTargetNodeToolStripMenuItem})
        ContextMenuStrip2.Name = "ContextMenuStrip2"
        ContextMenuStrip2.Size = New Size(174, 26)
        ' 
        ' SetAsTargetNodeToolStripMenuItem
        ' 
        SetAsTargetNodeToolStripMenuItem.Name = "SetAsTargetNodeToolStripMenuItem"
        SetAsTargetNodeToolStripMenuItem.Size = New Size(173, 22)
        SetAsTargetNodeToolStripMenuItem.Text = "Set As Target Node"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(497, 189)
        Label1.Name = "Label1"
        Label1.Size = New Size(20, 15)
        Label1.TabIndex = 2
        Label1.Text = "->"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(3, 19)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(1037, 235)
        DataGridView1.TabIndex = 4
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(12, 29)
        Label4.Name = "Label4"
        Label4.Size = New Size(70, 15)
        Label4.TabIndex = 6
        Label4.Text = "From Node:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(88, 26)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(322, 23)
        TextBox1.TabIndex = 7
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(416, 26)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 8
        Button1.Text = "Search"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(962, 26)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 11
        Button2.Text = "Search"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(603, 26)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(353, 23)
        TextBox2.TabIndex = 10
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(523, 30)
        Label5.Name = "Label5"
        Label5.Size = New Size(74, 15)
        Label5.TabIndex = 9
        Label5.Text = "Target Node:"
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(34, 384)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 23)
        Button3.TabIndex = 12
        Button3.Text = "Find Pathway"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripLabel1})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.RenderMode = ToolStripRenderMode.System
        ToolStrip1.Size = New Size(1043, 25)
        ToolStrip1.TabIndex = 13
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripLabel1
        ' 
        ToolStripLabel1.BackColor = SystemColors.Control
        ToolStripLabel1.Name = "ToolStripLabel1"
        ToolStripLabel1.Size = New Size(234, 22)
        ToolStripLabel1.Text = "Find Pathway From Node To Another Node"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(ListBox1)
        GroupBox1.Controls.Add(Button3)
        GroupBox1.Controls.Add(ListBox2)
        GroupBox1.Controls.Add(Button2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(1043, 419)
        GroupBox1.TabIndex = 14
        GroupBox1.TabStop = False
        GroupBox1.Text = "Set Targets"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(94, 359)
        Label7.Name = "Label7"
        Label7.Size = New Size(74, 15)
        Label7.TabIndex = 16
        Label7.Text = "Not Selected"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(33, 359)
        Label6.Name = "Label6"
        Label6.Size = New Size(55, 15)
        Label6.TabIndex = 15
        Label6.Text = "Travel To:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(78, 337)
        Label3.Name = "Label3"
        Label3.Size = New Size(74, 15)
        Label3.TabIndex = 14
        Label3.Text = "Not Selected"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(34, 337)
        Label2.Name = "Label2"
        Label2.Size = New Size(38, 15)
        Label2.TabIndex = 13
        Label2.Text = "From:"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(DataGridView1)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(0, 0)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(1043, 257)
        GroupBox2.TabIndex = 15
        GroupBox2.TabStop = False
        GroupBox2.Text = "Pathway Details"
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(0, 25)
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
        SplitContainer1.Size = New Size(1043, 680)
        SplitContainer1.SplitterDistance = 419
        SplitContainer1.TabIndex = 16
        ' 
        ' FormPhenotypePath
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1043, 705)
        Controls.Add(SplitContainer1)
        Controls.Add(ToolStrip1)
        Name = "FormPhenotypePath"
        Text = "Phenotype Pathway"
        ContextMenuStrip1.ResumeLayout(False)
        ContextMenuStrip2.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SetAsFromNodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents SetAsTargetNodeToolStripMenuItem As ToolStripMenuItem
End Class
