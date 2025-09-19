<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
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
        PanelBase = New Panel()
        StatusStrip1 = New StatusStrip()
        SuspendLayout()
        ' 
        ' PanelBase
        ' 
        PanelBase.Location = New Point(0, 392)
        PanelBase.Name = "PanelBase"
        PanelBase.Size = New Size(1260, 299)
        PanelBase.TabIndex = 0
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Location = New Point(0, 691)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1260, 22)
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' FormMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1260, 713)
        Controls.Add(PanelBase)
        Controls.Add(StatusStrip1)
        Name = "FormMain"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PanelBase As Panel
    Friend WithEvents StatusStrip1 As StatusStrip
End Class
