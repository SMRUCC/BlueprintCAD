<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSplashScreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSplashScreen))
        Panel1 = New Panel()
        Label1 = New Label()
        PictureBox1 = New PictureBox()
        Label2 = New Label()
        Label3 = New Label()
        Panel2 = New Panel()
        Label4 = New Label()
        Panel1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.White
        Panel1.BackgroundImageLayout = ImageLayout.Zoom
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(Panel2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(671, 428)
        Panel1.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(322, 201)
        Label1.Name = "Label1"
        Label1.Size = New Size(168, 15)
        Label1.TabIndex = 0
        Label1.Text = "Virtual Cell Engine Powered By"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), Image)
        PictureBox1.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox1.Location = New Point(496, 201)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(149, 190)
        PictureBox1.TabIndex = 1
        PictureBox1.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(473, 394)
        Label2.Name = "Label2"
        Label2.Size = New Size(172, 15)
        Label2.TabIndex = 2
        Label2.Text = "https://gcmodeller.org/  ·  2025"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Cambria", 72F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.White
        Label3.Location = New Point(17, 15)
        Label3.Name = "Label3"
        Label3.Size = New Size(342, 112)
        Label3.TabIndex = 3
        Label3.Text = "Sophia"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(136), CByte(165), CByte(200))
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(Label3)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(671, 188)
        Panel2.TabIndex = 4
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(31, 143)
        Label4.Name = "Label4"
        Label4.Size = New Size(237, 32)
        Label4.TabIndex = 4
        Label4.Text = "VirtualCell Workshop"
        ' 
        ' FormSplashScreen
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(671, 428)
        Controls.Add(Panel1)
        FormBorderStyle = FormBorderStyle.None
        Name = "FormSplashScreen"
        StartPosition = FormStartPosition.CenterScreen
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label4 As Label
End Class
