<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormEditor
    Inherits System.Windows.Forms.Form

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
        GraphPad1 = New GraphPad()
        SuspendLayout()
        ' 
        ' GraphPad1
        ' 
        GraphPad1.Dock = DockStyle.Fill
        GraphPad1.Location = New Point(0, 0)
        GraphPad1.Name = "GraphPad1"
        GraphPad1.Size = New Size(800, 450)
        GraphPad1.TabIndex = 0
        ' 
        ' FormEditor
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(GraphPad1)
        Name = "FormEditor"
        Text = "Form1"
        ResumeLayout(False)
    End Sub

    Friend WithEvents GraphPad1 As GraphPad

End Class
