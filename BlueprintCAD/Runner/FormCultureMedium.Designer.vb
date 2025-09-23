<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCultureMedium
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
        ContextMenuStrip1.SuspendLayout()
        ContextMenuStrip2.SuspendLayout()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ListBox1
        ' 
        ListBox1.ContextMenuStrip = ContextMenuStrip1
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(25, 44)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(232, 364)
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
        Label1.Location = New Point(25, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(149, 15)
        Label1.TabIndex = 1
        Label1.Text = "Compound && Metabolites:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(370, 19)
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
        ListBox2.Location = New Point(358, 53)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(232, 319)
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
        Label3.Location = New Point(627, 48)
        Label3.Name = "Label3"
        Label3.Size = New Size(35, 15)
        Label3.TabIndex = 5
        Label3.Text = "Value"
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        NumericUpDown1.Location = New Point(627, 86)
        NumericUpDown1.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(120, 23)
        NumericUpDown1.TabIndex = 6
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(690, 409)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 7
        Button1.Text = "OK"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(555, 409)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 8
        Button2.Text = "Cancel"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' FormCultureMedium
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(NumericUpDown1)
        Controls.Add(Label3)
        Controls.Add(ListBox2)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(ListBox1)
        Name = "FormCultureMedium"
        Text = "Form1"
        ContextMenuStrip1.ResumeLayout(False)
        ContextMenuStrip2.ResumeLayout(False)
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
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
End Class
