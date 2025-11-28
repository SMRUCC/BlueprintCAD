Imports Galaxy.Workbench.CommonDialogs

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPhenotypePath
    Inherits InputDialog

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        ListBox1 = New ListBox()
        ListBox2 = New ListBox()
        Label1 = New Label()
        Label2 = New Label()
        DataGridView1 = New DataGridView()
        Label3 = New Label()
        Label4 = New Label()
        TextBox1 = New TextBox()
        Button1 = New Button()
        Button2 = New Button()
        TextBox2 = New TextBox()
        Label5 = New Label()
        Button3 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(27, 135)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(243, 274)
        ListBox1.TabIndex = 0
        ' 
        ' ListBox2
        ' 
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(429, 135)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(246, 274)
        ListBox2.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(322, 243)
        Label1.Name = "Label1"
        Label1.Size = New Size(20, 15)
        Label1.TabIndex = 2
        Label1.Text = "->"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(36, 34)
        Label2.Name = "Label2"
        Label2.Size = New Size(234, 15)
        Label2.TabIndex = 3
        Label2.Text = "Find Pathway From Node To Another Node"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(36, 555)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(683, 171)
        DataGridView1.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(179, 504)
        Label3.Name = "Label3"
        Label3.Size = New Size(93, 15)
        Label3.TabIndex = 5
        Label3.Text = "Pathway Details:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(37, 91)
        Label4.Name = "Label4"
        Label4.Size = New Size(45, 15)
        Label4.TabIndex = 6
        Label4.Text = "Search:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(88, 91)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(99, 23)
        TextBox1.TabIndex = 7
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(209, 91)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 8
        Button1.Text = "Match"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(600, 91)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 11
        Button2.Text = "Match"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(472, 87)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(99, 23)
        TextBox2.TabIndex = 10
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(421, 87)
        Label5.Name = "Label5"
        Label5.Size = New Size(45, 15)
        Label5.TabIndex = 9
        Label5.Text = "Search:"
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(162, 445)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 23)
        Button3.TabIndex = 12
        Button3.Text = "Find Pathway"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' FormPhenotypePath
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(826, 758)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(TextBox2)
        Controls.Add(Label5)
        Controls.Add(Button1)
        Controls.Add(TextBox1)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(DataGridView1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(ListBox2)
        Controls.Add(ListBox1)
        Name = "FormPhenotypePath"
        Text = "Phenotype Pathway"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Button3 As Button
End Class
