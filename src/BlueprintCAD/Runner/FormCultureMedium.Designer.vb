Imports Galaxy.Workbench.CommonDialogs

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormCultureMedium
    Inherits UserControl

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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCultureMedium))
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
        GroupBox2 = New GroupBox()
        DataGridView1 = New DataGridView()
        Column2 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Button3 = New Button()
        TextBox1 = New TextBox()
        Label4 = New Label()
        RichTextBox1 = New RichTextBox()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        Label5 = New Label()
        ComboBox1 = New ComboBox()
        PictureBox1 = New PictureBox()
        TabPage2 = New TabPage()
        ContextMenuStrip1.SuspendLayout()
        ContextMenuStrip2.SuspendLayout()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox2.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        TabPage2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ListBox1
        ' 
        ListBox1.ContextMenuStrip = ContextMenuStrip1
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(6, 25)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(347, 244)
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
        Label1.Location = New Point(3, 3)
        Label1.Name = "Label1"
        Label1.Size = New Size(149, 15)
        Label1.TabIndex = 1
        Label1.Text = "Compound && Metabolites:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(396, 7)
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
        ListBox2.Location = New Point(396, 25)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(334, 244)
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
        Label3.Location = New Point(520, 279)
        Label3.Name = "Label3"
        Label3.Size = New Size(80, 15)
        Label3.TabIndex = 5
        Label3.Text = "Content Data:"
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        NumericUpDown1.Location = New Point(606, 275)
        NumericUpDown1.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(124, 23)
        NumericUpDown1.TabIndex = 6
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(DataGridView1)
        GroupBox2.Location = New Point(6, 304)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(727, 164)
        GroupBox2.TabIndex = 10
        GroupBox2.TabStop = False
        GroupBox2.Text = "Associated Membrane Transportation"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column2, Column4})
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Window
        DataGridViewCellStyle1.Font = New Font("Cambria", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.False
        DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(3, 19)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Size = New Size(721, 142)
        DataGridView1.TabIndex = 0
        ' 
        ' Column2
        ' 
        Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Column2.HeaderText = "name"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' Column4
        ' 
        Column4.HeaderText = "ec_number"
        Column4.Name = "Column4"
        Column4.ReadOnly = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(282, 275)
        Button3.Name = "Button3"
        Button3.Size = New Size(71, 23)
        Button3.TabIndex = 9
        Button3.Text = "Search"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(6, 275)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(270, 23)
        TextBox1.TabIndex = 8
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(359, 143)
        Label4.Name = "Label4"
        Label4.Size = New Size(31, 15)
        Label4.TabIndex = 7
        Label4.Text = ">>>"
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.BackColor = Color.WhiteSmoke
        RichTextBox1.BorderStyle = BorderStyle.None
        RichTextBox1.Dock = DockStyle.Fill
        RichTextBox1.Location = New Point(3, 3)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(733, 468)
        RichTextBox1.TabIndex = 10
        RichTextBox1.Text = ""
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Location = New Point(0, 0)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(747, 502)
        TabControl1.TabIndex = 11
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(Label5)
        TabPage1.Controls.Add(ComboBox1)
        TabPage1.Controls.Add(PictureBox1)
        TabPage1.Controls.Add(GroupBox2)
        TabPage1.Controls.Add(ListBox1)
        TabPage1.Controls.Add(Button3)
        TabPage1.Controls.Add(Label3)
        TabPage1.Controls.Add(TextBox1)
        TabPage1.Controls.Add(NumericUpDown1)
        TabPage1.Controls.Add(Label4)
        TabPage1.Controls.Add(ListBox2)
        TabPage1.Controls.Add(Label1)
        TabPage1.Controls.Add(Label2)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(739, 474)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Set Culture Medium"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(535, 7)
        Label5.Name = "Label5"
        Label5.Size = New Size(36, 15)
        Label5.TabIndex = 13
        Label5.Text = "Load:"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(577, 4)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(153, 23)
        ComboBox1.TabIndex = 12
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), Image)
        PictureBox1.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox1.Cursor = Cursors.Hand
        PictureBox1.Location = New Point(499, 6)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(16, 16)
        PictureBox1.TabIndex = 11
        PictureBox1.TabStop = False
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(RichTextBox1)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(739, 474)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Help"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' FormCultureMedium
        ' 
        Controls.Add(TabControl1)
        Name = "FormCultureMedium"
        Size = New Size(747, 502)
        ContextMenuStrip1.ResumeLayout(False)
        ContextMenuStrip2.ResumeLayout(False)
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox2.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        TabPage2.ResumeLayout(False)
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
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents RemovesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label4 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBox1 As ComboBox
End Class
