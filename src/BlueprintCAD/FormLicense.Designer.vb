Imports Galaxy.Workbench.CommonDialogs

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormLicense
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLicense))
        PictureBox1 = New PictureBox()
        RichTextBox1 = New RichTextBox()
        Button1 = New Button()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), Image)
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox1.Dock = DockStyle.Left
        PictureBox1.Location = New Point(0, 0)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(170, 522)
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.BackColor = Color.White
        RichTextBox1.BorderStyle = BorderStyle.None
        RichTextBox1.Location = New Point(178, 12)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.Size = New Size(608, 473)
        RichTextBox1.TabIndex = 1
        RichTextBox1.Text = ""
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(696, 491)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 2
        Button1.Text = "OK"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' FormLicense
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(794, 522)
        Controls.Add(Button1)
        Controls.Add(RichTextBox1)
        Controls.Add(PictureBox1)
        Name = "FormLicense"
        Text = "Software License"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Button1 As Button
End Class
