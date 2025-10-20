Public Class AnnotationItem

    Public Event Run()

    Public Property Running As Boolean
        Get
            Return Not Button1.Enabled
        End Get
        Set(value As Boolean)
            Button1.Enabled = Not value
        End Set
    End Property

    Public Sub SetStatusIcon(icon As Image)
        PictureBox1.BackgroundImage = icon
    End Sub

    Public Sub SetStatusText(txt As String)
        LinkLabel1.Text = txt
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RaiseEvent Run()
    End Sub
End Class
