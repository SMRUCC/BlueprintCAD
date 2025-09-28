Imports Galaxy.Workbench.CommonDialogs

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormKnockoutGenerator
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
        Label1 = New Label()
        ListBox1 = New ListBox()
        Label2 = New Label()
        ListBox2 = New ListBox()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        AddToKnockoutListToolStripMenuItem = New ToolStripMenuItem()
        Button1 = New Button()
        Button2 = New Button()
        Label3 = New Label()
        ListBox4 = New ListBox()
        ListBox3 = New ListBox()
        Label4 = New Label()
        GroupBox1 = New GroupBox()
        Label5 = New Label()
        RichTextBox1 = New RichTextBox()
        ContextMenuStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 21)
        Label1.Name = "Label1"
        Label1.Size = New Size(110, 15)
        Label1.TabIndex = 0
        Label1.Text = "Select a Cell Model:"
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(9, 40)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(221, 184)
        ListBox1.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(256, 19)
        Label2.Name = "Label2"
        Label2.Size = New Size(103, 15)
        Label2.TabIndex = 2
        Label2.Text = "Genes in Genome:"
        ' 
        ' ListBox2
        ' 
        ListBox2.ContextMenuStrip = ContextMenuStrip1
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(256, 40)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(256, 184)
        ListBox2.TabIndex = 3
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {AddToKnockoutListToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(187, 26)
        ' 
        ' AddToKnockoutListToolStripMenuItem
        ' 
        AddToKnockoutListToolStripMenuItem.Name = "AddToKnockoutListToolStripMenuItem"
        AddToKnockoutListToolStripMenuItem.Size = New Size(186, 22)
        AddToKnockoutListToolStripMenuItem.Text = "Add To Knockout List"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(749, 606)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 4
        Button1.Text = "OK"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(657, 606)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 5
        Button2.Text = "Cancel"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(9, 231)
        Label3.Name = "Label3"
        Label3.Size = New Size(151, 15)
        Label3.TabIndex = 6
        Label3.Text = "Metabolic Network Impact:"
        ' 
        ' ListBox4
        ' 
        ListBox4.FormattingEnabled = True
        ListBox4.ItemHeight = 15
        ListBox4.Location = New Point(553, 40)
        ListBox4.Name = "ListBox4"
        ListBox4.Size = New Size(262, 184)
        ListBox4.TabIndex = 8
        ' 
        ' ListBox3
        ' 
        ListBox3.FormattingEnabled = True
        ListBox3.ItemHeight = 15
        ListBox3.Location = New Point(10, 254)
        ListBox3.Name = "ListBox3"
        ListBox3.Size = New Size(805, 124)
        ListBox3.TabIndex = 9
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(553, 19)
        Label4.Name = "Label4"
        Label4.Size = New Size(117, 15)
        Label4.TabIndex = 10
        Label4.Text = "Genes Knockout List:"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(ListBox1)
        GroupBox1.Controls.Add(ListBox3)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(ListBox4)
        GroupBox1.Controls.Add(ListBox2)
        GroupBox1.Location = New Point(9, 9)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(826, 387)
        GroupBox1.TabIndex = 11
        GroupBox1.TabStop = False
        GroupBox1.Text = "Add Genes To Knockout"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(517, 124)
        Label5.Name = "Label5"
        Label5.Size = New Size(31, 15)
        Label5.TabIndex = 11
        Label5.Text = ">>>"
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.BackColor = Color.White
        RichTextBox1.BorderStyle = BorderStyle.None
        RichTextBox1.Location = New Point(9, 402)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.ReadOnly = True
        RichTextBox1.Size = New Size(826, 198)
        RichTextBox1.TabIndex = 12
        RichTextBox1.Text = ""
        ' 
        ' FormKnockoutGenerator
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(846, 643)
        Controls.Add(RichTextBox1)
        Controls.Add(GroupBox1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Name = "FormKnockoutGenerator"
        Text = "Config Gene Knockout List"
        ContextMenuStrip1.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents ListBox4 As ListBox
    Friend WithEvents ListBox3 As ListBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents AddToKnockoutListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
End Class
