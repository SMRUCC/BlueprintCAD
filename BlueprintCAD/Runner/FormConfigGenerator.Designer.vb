<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConfigGenerator
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
        Button1 = New Button()
        ListBox1 = New ListBox()
        Button2 = New Button()
        Button3 = New Button()
        Label1 = New Label()
        Label2 = New Label()
        NumericUpDown1 = New NumericUpDown()
        Label3 = New Label()
        NumericUpDown2 = New NumericUpDown()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(511, 42)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 0
        Button1.Text = "..."
        Button1.UseVisualStyleBackColor = True
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(45, 42)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(449, 94)
        ListBox1.TabIndex = 1
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(511, 298)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 2
        Button2.Text = "Start"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(419, 298)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 23)
        Button3.TabIndex = 3
        Button3.Text = "Cancel"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(45, 22)
        Label1.Name = "Label1"
        Label1.Size = New Size(113, 15)
        Label1.TabIndex = 4
        Label1.Text = "Genome Model List:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(45, 173)
        Label2.Name = "Label2"
        Label2.Size = New Size(88, 15)
        Label2.TabIndex = 5
        Label2.Text = "Time Iterations:"
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        NumericUpDown1.Location = New Point(139, 171)
        NumericUpDown1.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        NumericUpDown1.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(120, 23)
        NumericUpDown1.TabIndex = 6
        NumericUpDown1.Value = New Decimal(New Integer() {100, 0, 0, 0})
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(67, 218)
        Label3.Name = "Label3"
        Label3.Size = New Size(66, 15)
        Label3.TabIndex = 7
        Label3.Text = "Resolution:"
        ' 
        ' NumericUpDown2
        ' 
        NumericUpDown2.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        NumericUpDown2.Location = New Point(139, 216)
        NumericUpDown2.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        NumericUpDown2.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        NumericUpDown2.Name = "NumericUpDown2"
        NumericUpDown2.Size = New Size(120, 23)
        NumericUpDown2.TabIndex = 8
        NumericUpDown2.Value = New Decimal(New Integer() {100, 0, 0, 0})
        ' 
        ' FormConfigGenerator
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(605, 337)
        Controls.Add(NumericUpDown2)
        Controls.Add(Label3)
        Controls.Add(NumericUpDown1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(ListBox1)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormConfigGenerator"
        StartPosition = FormStartPosition.CenterParent
        Text = "Runner Wizard"
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents NumericUpDown2 As NumericUpDown
End Class
