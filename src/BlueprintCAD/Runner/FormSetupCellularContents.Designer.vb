<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSetupCellularContents
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        ListBox1 = New ListBox()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        MakeRandomToolStripMenuItem = New ToolStripMenuItem()
        GroupBox2 = New GroupBox()
        ListBox2 = New ListBox()
        Panel1 = New Panel()
        Button2 = New Button()
        TextBox2 = New TextBox()
        Label1 = New Label()
        TextBox1 = New TextBox()
        Label2 = New Label()
        Button1 = New Button()
        GroupBox1.SuspendLayout()
        ContextMenuStrip1.SuspendLayout()
        GroupBox2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(ListBox1)
        GroupBox1.Dock = DockStyle.Left
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(168, 502)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Cell Colony"
        ' 
        ' ListBox1
        ' 
        ListBox1.ContextMenuStrip = ContextMenuStrip1
        ListBox1.Dock = DockStyle.Fill
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(3, 19)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(162, 480)
        ListBox1.TabIndex = 0
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {MakeRandomToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(152, 26)
        ' 
        ' MakeRandomToolStripMenuItem
        ' 
        MakeRandomToolStripMenuItem.Name = "MakeRandomToolStripMenuItem"
        MakeRandomToolStripMenuItem.Size = New Size(151, 22)
        MakeRandomToolStripMenuItem.Text = "Make Random"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(ListBox2)
        GroupBox2.Controls.Add(Panel1)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(168, 0)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(579, 502)
        GroupBox2.TabIndex = 1
        GroupBox2.TabStop = False
        GroupBox2.Text = "Set Content Data"
        ' 
        ' ListBox2
        ' 
        ListBox2.Dock = DockStyle.Fill
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(3, 19)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(573, 443)
        ListBox2.TabIndex = 6
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Button2)
        Panel1.Controls.Add(TextBox2)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(TextBox1)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Button1)
        Panel1.Dock = DockStyle.Bottom
        Panel1.Location = New Point(3, 462)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(573, 37)
        Panel1.TabIndex = 7
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(479, 5)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 6
        Button2.Text = "Apply To All"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(399, 6)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(74, 23)
        TextBox2.TabIndex = 5
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(5, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 1
        Label1.Text = "Search:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(56, 6)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(146, 23)
        TextBox1.TabIndex = 2
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(340, 9)
        Label2.Name = "Label2"
        Label2.Size = New Size(53, 15)
        Label2.TabIndex = 4
        Label2.Text = "Content:"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(208, 5)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 3
        Button1.Text = "Search"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' FormSetupCellularContents
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Name = "FormSetupCellularContents"
        Size = New Size(747, 502)
        GroupBox1.ResumeLayout(False)
        ContextMenuStrip1.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents MakeRandomToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button2 As Button

End Class
