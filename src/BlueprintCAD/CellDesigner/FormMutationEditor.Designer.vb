Imports Galaxy.Workbench.DockDocument

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMutationEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMutationEditor))
        Label1 = New Label()
        TextBox1 = New TextBox()
        Button1 = New Button()
        DataGridView1 = New DataGridView()
        Label2 = New Label()
        ListBox1 = New ListBox()
        ListBox2 = New ListBox()
        Label3 = New Label()
        NumericUpDown1 = New NumericUpDown()
        CheckBox1 = New CheckBox()
        Button2 = New Button()
        ToolStrip1 = New ToolStrip()
        ToolStripButton1 = New ToolStripButton()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(31, 35)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 1
        Label1.Text = "Search:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(97, 35)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(237, 23)
        TextBox1.TabIndex = 2
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(356, 35)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 3
        Button1.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(31, 477)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(1150, 332)
        DataGridView1.TabIndex = 4
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(741, 32)
        Label2.Name = "Label2"
        Label2.Size = New Size(59, 15)
        Label2.TabIndex = 5
        Label2.Text = "Mutation:"
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(42, 80)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(371, 349)
        ListBox1.TabIndex = 6
        ' 
        ' ListBox2
        ' 
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(726, 80)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(365, 334)
        ListBox2.TabIndex = 7
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(458, 118)
        Label3.Name = "Label3"
        Label3.Size = New Size(147, 15)
        Label3.TabIndex = 8
        Label3.Text = "Adjust of Expression Level:"
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        NumericUpDown1.Location = New Point(467, 155)
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(120, 23)
        NumericUpDown1.TabIndex = 9
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(477, 220)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(80, 19)
        CheckBox1.TabIndex = 10
        CheckBox1.Text = "Knock out"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(620, 155)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 11
        Button2.Text = ">>>"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton1})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1210, 25)
        ToolStrip1.TabIndex = 12
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), Image)
        ToolStripButton1.ImageTransparentColor = Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(23, 22)
        ToolStripButton1.Text = "Create Modified Model"
        ' 
        ' FormMutationEditor
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1210, 845)
        Controls.Add(ToolStrip1)
        Controls.Add(Button2)
        Controls.Add(CheckBox1)
        Controls.Add(NumericUpDown1)
        Controls.Add(Label3)
        Controls.Add(ListBox2)
        Controls.Add(ListBox1)
        Controls.Add(Label2)
        Controls.Add(DataGridView1)
        Controls.Add(Button1)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        DockAreas = Microsoft.VisualStudio.WinForms.Docking.DockAreas.Float Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockLeft Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockRight Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockTop Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.DockBottom Or Microsoft.VisualStudio.WinForms.Docking.DockAreas.Document
        DoubleBuffered = True
        Name = "FormMutationEditor"
        ShowHint = Microsoft.VisualStudio.WinForms.Docking.DockState.Unknown
        TabPageContextMenuStrip = DockContextMenuStrip1
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Label3 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Button2 As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
End Class
