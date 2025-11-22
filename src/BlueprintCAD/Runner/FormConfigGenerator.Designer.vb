Imports Galaxy.Workbench.CommonDialogs

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormConfigGenerator
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
        Button1 = New Button()
        Label1 = New Label()
        Label2 = New Label()
        NumericUpDown1 = New NumericUpDown()
        Label3 = New Label()
        NumericUpDown2 = New NumericUpDown()
        GroupBox1 = New GroupBox()
        DataGridView1 = New DataGridView()
        GroupBox2 = New GroupBox()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Label4 = New Label()
        NumericUpDown3 = New NumericUpDown()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox2.SuspendLayout()
        CType(NumericUpDown3, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(637, 45)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 0
        Button1.Text = "..."
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(113, 15)
        Label1.TabIndex = 4
        Label1.Text = "Genome Model List:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(16, 254)
        Label2.Name = "Label2"
        Label2.Size = New Size(88, 15)
        Label2.TabIndex = 5
        Label2.Text = "Time Iterations:"
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        NumericUpDown1.Location = New Point(110, 252)
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
        Label3.Location = New Point(38, 285)
        Label3.Name = "Label3"
        Label3.Size = New Size(66, 15)
        Label3.TabIndex = 7
        Label3.Text = "Resolution:"
        ' 
        ' NumericUpDown2
        ' 
        NumericUpDown2.Increment = New Decimal(New Integer() {50, 0, 0, 0})
        NumericUpDown2.Location = New Point(110, 283)
        NumericUpDown2.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        NumericUpDown2.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        NumericUpDown2.Name = "NumericUpDown2"
        NumericUpDown2.Size = New Size(120, 23)
        NumericUpDown2.TabIndex = 8
        NumericUpDown2.Value = New Decimal(New Integer() {100, 0, 0, 0})
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(DataGridView1)
        GroupBox1.Controls.Add(NumericUpDown2)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(NumericUpDown1)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Location = New Point(9, 9)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(727, 320)
        GroupBox1.TabIndex = 9
        GroupBox1.TabStop = False
        GroupBox1.Text = "Basic Experiment Setup"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.BackgroundColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2, Column3})
        DataGridView1.Location = New Point(9, 45)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(622, 192)
        DataGridView1.TabIndex = 9
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(NumericUpDown3)
        GroupBox2.Controls.Add(Label4)
        GroupBox2.Location = New Point(9, 335)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(727, 164)
        GroupBox2.TabIndex = 10
        GroupBox2.TabStop = False
        GroupBox2.Text = "Basic Dynamics Parameters"
        ' 
        ' Column1
        ' 
        Column1.HeaderText = "organism name"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        ' 
        ' Column2
        ' 
        Column2.HeaderText = "cell name"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' Column3
        ' 
        Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Column3.HeaderText = "model file"
        Column3.Name = "Column3"
        Column3.ReadOnly = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(28, 29)
        Label4.Name = "Label4"
        Label4.Size = New Size(76, 15)
        Label4.TabIndex = 0
        Label4.Text = "Boost Factor:"
        ' 
        ' NumericUpDown3
        ' 
        NumericUpDown3.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        NumericUpDown3.Location = New Point(110, 27)
        NumericUpDown3.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        NumericUpDown3.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown3.Name = "NumericUpDown3"
        NumericUpDown3.Size = New Size(120, 23)
        NumericUpDown3.TabIndex = 1
        NumericUpDown3.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' FormConfigGenerator
        ' 
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Name = "FormConfigGenerator"
        Size = New Size(747, 502)
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(NumericUpDown3, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents NumericUpDown3 As NumericUpDown
    Friend WithEvents Label4 As Label
End Class
