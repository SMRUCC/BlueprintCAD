Imports Galaxy.Workbench.DockDocument

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormFBATool : Inherits DocumentWindow

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormFBATool))
        ListBox2 = New ListBox()
        ContextMenuStrip2 = New ContextMenuStrip(components)
        RemoveToolStripMenuItem = New ToolStripMenuItem()
        ComboBox1 = New ComboBox()
        Label3 = New Label()
        Label2 = New Label()
        Button1 = New Button()
        TextBox1 = New TextBox()
        Label1 = New Label()
        ListBox1 = New ListBox()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        AddToObjectiveListToolStripMenuItem = New ToolStripMenuItem()
        ToolStrip1 = New ToolStrip()
        ToolStripButton1 = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        ToolStripLabel1 = New ToolStripLabel()
        ToolStripTextBox1 = New ToolStripTextBox()
        ToolStripButton2 = New ToolStripButton()
        Panel1 = New Panel()
        DataGridView1 = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Panel2 = New Panel()
        AddAllOfThemToolStripMenuItem = New ToolStripMenuItem()
        ContextMenuStrip2.SuspendLayout()
        ContextMenuStrip1.SuspendLayout()
        ToolStrip1.SuspendLayout()
        Panel1.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ListBox2
        ' 
        ListBox2.ContextMenuStrip = ContextMenuStrip2
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(564, 68)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(499, 319)
        ListBox2.TabIndex = 7
        ' 
        ' ContextMenuStrip2
        ' 
        ContextMenuStrip2.Items.AddRange(New ToolStripItem() {RemoveToolStripMenuItem})
        ContextMenuStrip2.Name = "ContextMenuStrip2"
        ContextMenuStrip2.Size = New Size(118, 26)
        ' 
        ' RemoveToolStripMenuItem
        ' 
        RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        RemoveToolStripMenuItem.Size = New Size(117, 22)
        RemoveToolStripMenuItem.Text = "Remove"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Maximize", "Minimize"})
        ComboBox1.Location = New Point(649, 42)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(161, 23)
        ComboBox1.TabIndex = 6
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(564, 46)
        Label3.Name = "Label3"
        Label3.Size = New Size(79, 15)
        Label3.TabIndex = 5
        Label3.Text = "Optimization:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(564, 15)
        Label2.Name = "Label2"
        Label2.Size = New Size(81, 15)
        Label2.TabIndex = 4
        Label2.Text = "Objective List:"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(200, 38)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 3
        Button1.Text = "Search"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(19, 39)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(175, 23)
        TextBox1.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(19, 15)
        Label1.Name = "Label1"
        Label1.Size = New Size(141, 15)
        Label1.TabIndex = 1
        Label1.Text = "Select Target Metabolites:"
        ' 
        ' ListBox1
        ' 
        ListBox1.ContextMenuStrip = ContextMenuStrip1
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(19, 68)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(503, 319)
        ListBox1.TabIndex = 0
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {AddToObjectiveListToolStripMenuItem, AddAllOfThemToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(186, 70)
        ' 
        ' AddToObjectiveListToolStripMenuItem
        ' 
        AddToObjectiveListToolStripMenuItem.Name = "AddToObjectiveListToolStripMenuItem"
        AddToObjectiveListToolStripMenuItem.Size = New Size(185, 22)
        AddToObjectiveListToolStripMenuItem.Text = "Add To Objective List"
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton1, ToolStripSeparator1, ToolStripLabel1, ToolStripTextBox1, ToolStripButton2})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1075, 25)
        ToolStrip1.TabIndex = 3
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), Image)
        ToolStripButton1.ImageTransparentColor = Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(23, 22)
        ToolStripButton1.Text = "ToolStripButton1"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 25)
        ' 
        ' ToolStripLabel1
        ' 
        ToolStripLabel1.Name = "ToolStripLabel1"
        ToolStripLabel1.Size = New Size(160, 22)
        ToolStripLabel1.Text = "FBA Output(Objective Mass):"
        ' 
        ' ToolStripTextBox1
        ' 
        ToolStripTextBox1.Name = "ToolStripTextBox1"
        ToolStripTextBox1.ReadOnly = True
        ToolStripTextBox1.Size = New Size(125, 25)
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), Image)
        ToolStripButton2.ImageTransparentColor = Color.Magenta
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(23, 22)
        ToolStripButton2.Text = "Export FBA Flux Table"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(DataGridView1)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 430)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1075, 468)
        Panel1.TabIndex = 8
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.BackgroundColor = Color.WhiteSmoke
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2})
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(0, 0)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.Size = New Size(1075, 468)
        DataGridView1.TabIndex = 3
        ' 
        ' Column1
        ' 
        Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Column1.HeaderText = "Metabolic Reaction"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        ' 
        ' Column2
        ' 
        Column2.HeaderText = "Flux"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(Label1)
        Panel2.Controls.Add(ListBox1)
        Panel2.Controls.Add(ListBox2)
        Panel2.Controls.Add(TextBox1)
        Panel2.Controls.Add(ComboBox1)
        Panel2.Controls.Add(Button1)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(Label2)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 25)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1075, 405)
        Panel2.TabIndex = 9
        ' 
        ' AddAllOfThemToolStripMenuItem
        ' 
        AddAllOfThemToolStripMenuItem.Name = "AddAllOfThemToolStripMenuItem"
        AddAllOfThemToolStripMenuItem.Size = New Size(185, 22)
        AddAllOfThemToolStripMenuItem.Text = "Add All of Them"
        ' 
        ' FormFBATool
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1075, 898)
        Controls.Add(Panel1)
        Controls.Add(Panel2)
        Controls.Add(ToolStrip1)
        DockAreas = Microsoft.VisualStudio.WinForms.Docking.DockAreas.Float Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockLeft Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockRight Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockTop Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockBottom Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.Document
        DoubleBuffered = True
        Name = "FormFBATool"
        ShowHint = Microsoft.VisualStudio.WinForms.Docking.DockState.Unknown
        TabPageContextMenuStrip = DockContextMenuStrip1
        Text = "FBA Analysis Tool"
        ContextMenuStrip2.ResumeLayout(False)
        ContextMenuStrip1.ResumeLayout(False)
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        Panel1.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AddToObjectiveListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents AddAllOfThemToolStripMenuItem As ToolStripMenuItem
End Class
