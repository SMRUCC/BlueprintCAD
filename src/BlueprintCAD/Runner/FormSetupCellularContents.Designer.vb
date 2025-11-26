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
        GroupBox1 = New GroupBox()
        ListBox1 = New ListBox()
        GroupBox2 = New GroupBox()
        ListBox2 = New ListBox()
        TextBox2 = New TextBox()
        Label2 = New Label()
        Button1 = New Button()
        TextBox1 = New TextBox()
        Label1 = New Label()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(ListBox1)
        GroupBox1.Location = New Point(3, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(174, 496)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Cell Colony"
        ' 
        ' ListBox1
        ' 
        ListBox1.Dock = DockStyle.Fill
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(3, 19)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(168, 474)
        ListBox1.TabIndex = 0
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(ListBox2)
        GroupBox2.Controls.Add(TextBox2)
        GroupBox2.Controls.Add(Label2)
        GroupBox2.Controls.Add(Button1)
        GroupBox2.Controls.Add(TextBox1)
        GroupBox2.Controls.Add(Label1)
        GroupBox2.Location = New Point(183, 3)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(561, 493)
        GroupBox2.TabIndex = 1
        GroupBox2.TabStop = False
        GroupBox2.Text = "Set Content Data"
        ' 
        ' ListBox2
        ' 
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(6, 27)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(549, 424)
        ListBox2.TabIndex = 6
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(436, 463)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(119, 23)
        TextBox2.TabIndex = 5
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(377, 466)
        Label2.Name = "Label2"
        Label2.Size = New Size(53, 15)
        Label2.TabIndex = 4
        Label2.Text = "Content:"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(276, 462)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 3
        Button1.Text = "Search"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(57, 463)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(213, 23)
        TextBox1.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 466)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 1
        Label1.Text = "Search:"
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
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
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

End Class
