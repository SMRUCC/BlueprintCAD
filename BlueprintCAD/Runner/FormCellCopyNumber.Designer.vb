Imports Galaxy.Workbench.CommonDialogs

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormCellCopyNumber
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
        Button1 = New Button()
        Button2 = New Button()
        Label1 = New Label()
        ListBox1 = New ListBox()
        Label2 = New Label()
        NumericUpDown1 = New NumericUpDown()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(671, 392)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 0
        Button1.Text = "OK"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(564, 391)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 1
        Button2.Text = "Cancel"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(57, 38)
        Label1.Name = "Label1"
        Label1.Size = New Size(69, 15)
        Label1.TabIndex = 2
        Label1.Text = "Cell Clones:"
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(57, 77)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(321, 259)
        ListBox1.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(443, 77)
        Label2.Name = "Label2"
        Label2.Size = New Size(90, 15)
        Label2.TabIndex = 4
        Label2.Text = "Copy Numbers:"
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        NumericUpDown1.Location = New Point(443, 122)
        NumericUpDown1.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(196, 23)
        NumericUpDown1.TabIndex = 5
        ' 
        ' FormCellCopyNumber
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(NumericUpDown1)
        Controls.Add(Label2)
        Controls.Add(ListBox1)
        Controls.Add(Label1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Name = "FormCellCopyNumber"
        Text = "Set Cell Copy Numbers"
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
End Class
