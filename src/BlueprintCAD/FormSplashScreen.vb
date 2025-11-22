Imports Galaxy.Workbench

Public Class FormSplashScreen

    Private Sub FormSplashScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        Dim gfx = e.Graphics
        Dim pen As Pen = Pens.LightGray

        Call gfx.DrawRectangle(pen, New RectangleF(1, 1, Panel1.Width - 2, Panel1.Height - 2))
    End Sub

    Private Sub FormSplashScreen_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Close()
    End Sub

    Private Sub FormSplashScreen_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Close()
    End Sub

    Private Sub PictureBox1_LostFocus(sender As Object, e As EventArgs) Handles PictureBox1.LostFocus
        Close()
    End Sub

    Private Async Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Await Task.Run(Sub() Tools.OpenUrlWithDefaultBrowser("https://gcmodeller.org/"))
    End Sub
End Class