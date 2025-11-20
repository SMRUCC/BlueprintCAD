Imports Galaxy.Workbench.CommonDialogs

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormCultureMedium
    Inherits InputDialog

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
        ListBox1 = New ListBox()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        AddToDCultureMediumToolStripMenuItem = New ToolStripMenuItem()
        Label1 = New Label()
        Label2 = New Label()
        ListBox2 = New ListBox()
        ContextMenuStrip2 = New ContextMenuStrip(components)
        RemovesToolStripMenuItem = New ToolStripMenuItem()
        ClearToolStripMenuItem = New ToolStripMenuItem()
        Label3 = New Label()
        NumericUpDown1 = New NumericUpDown()
        Button1 = New Button()
        Button2 = New Button()
        GroupBox1 = New GroupBox()
        Button3 = New Button()
        TextBox1 = New TextBox()
        Label4 = New Label()
        GroupBox2 = New GroupBox()
        RichTextBox1 = New RichTextBox()
        DataGridView1 = New DataGridView()
        ContextMenuStrip1.SuspendLayout()
        ContextMenuStrip2.SuspendLayout()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ListBox1
        ' 
        ListBox1.ContextMenuStrip = ContextMenuStrip1
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(9, 43)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(283, 259)
        ListBox1.TabIndex = 0
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {AddToDCultureMediumToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(199, 26)
        ' 
        ' AddToDCultureMediumToolStripMenuItem
        ' 
        AddToDCultureMediumToolStripMenuItem.Name = "AddToDCultureMediumToolStripMenuItem"
        AddToDCultureMediumToolStripMenuItem.Size = New Size(198, 22)
        AddToDCultureMediumToolStripMenuItem.Text = "Add To CultureMedium"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 21)
        Label1.Name = "Label1"
        Label1.Size = New Size(149, 15)
        Label1.TabIndex = 1
        Label1.Text = "Compound && Metabolites:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(335, 21)
        Label2.Name = "Label2"
        Label2.Size = New Size(97, 15)
        Label2.TabIndex = 3
        Label2.Text = "Culture Medium:"
        ' 
        ' ListBox2
        ' 
        ListBox2.ContextMenuStrip = ContextMenuStrip2
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(335, 43)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(332, 259)
        ListBox2.TabIndex = 4
        ' 
        ' ContextMenuStrip2
        ' 
        ContextMenuStrip2.Items.AddRange(New ToolStripItem() {RemovesToolStripMenuItem, ClearToolStripMenuItem})
        ContextMenuStrip2.Name = "ContextMenuStrip2"
        ContextMenuStrip2.Size = New Size(123, 48)
        ' 
        ' RemovesToolStripMenuItem
        ' 
        RemovesToolStripMenuItem.Name = "RemovesToolStripMenuItem"
        RemovesToolStripMenuItem.Size = New Size(122, 22)
        RemovesToolStripMenuItem.Text = "Removes"
        ' 
        ' ClearToolStripMenuItem
        ' 
        ClearToolStripMenuItem.Name = "ClearToolStripMenuItem"
        ClearToolStripMenuItem.Size = New Size(122, 22)
        ClearToolStripMenuItem.Text = "Clear"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(446, 315)
        Label3.Name = "Label3"
        Label3.Size = New Size(80, 15)
        Label3.TabIndex = 5
        Label3.Text = "Content Data:"
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        NumericUpDown1.Location = New Point(543, 311)
        NumericUpDown1.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(124, 23)
        NumericUpDown1.TabIndex = 6
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(604, 605)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 7
        Button1.Text = "OK"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(514, 605)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 8
        Button2.Text = "Cancel"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(GroupBox2)
        GroupBox1.Controls.Add(Button3)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(ListBox1)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(ListBox2)
        GroupBox1.Controls.Add(NumericUpDown1)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Location = New Point(12, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(681, 587)
        GroupBox1.TabIndex = 9
        GroupBox1.TabStop = False
        GroupBox1.Text = "Add Compound To Culture Medium"
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(217, 311)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 23)
        Button3.TabIndex = 9
        Button3.Text = "Search"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(9, 311)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(203, 23)
        TextBox1.TabIndex = 8
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(298, 168)
        Label4.Name = "Label4"
        Label4.Size = New Size(31, 15)
        Label4.TabIndex = 7
        Label4.Text = ">>>"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(DataGridView1)
        GroupBox2.Location = New Point(9, 340)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(658, 241)
        GroupBox2.TabIndex = 10
        GroupBox2.TabStop = False
        GroupBox2.Text = "Associated Membrane Transportation"
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.BorderStyle = BorderStyle.FixedSingle
        RichTextBox1.Location = New Point(702, 21)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(354, 576)
        RichTextBox1.TabIndex = 10
        RichTextBox1.Text = ""
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(3, 19)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(652, 219)
        DataGridView1.TabIndex = 0
        ' 
        ' FormCultureMedium
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1065, 640)
        Controls.Add(RichTextBox1)
        Controls.Add(GroupBox1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "FormCultureMedium"
        Text = "Setup Culture Medium"
        ContextMenuStrip1.ResumeLayout(False)
        ContextMenuStrip2.ResumeLayout(False)
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AddToDCultureMediumToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents RemovesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents DataGridView1 As DataGridView
End Class
