Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class AnnotationItem

    Public Event Run()
    Public Event SelectAnnotationTabPage()

    Public Property Running As Boolean
        Get
            Return Not Button1.Enabled
        End Get
        Set(value As Boolean)
            Button1.Enabled = Not value

            If Running Then
                Button1.Text = "Running"
            Else
                Button1.Text = "Run"
            End If
        End Set
    End Property

    <Browsable(True)>
    Public Overrides Property Text As String
        Get
            Return Label1.Text
        End Get
        Set(value As String)
            Label1.Text = value
            MyBase.Text = value
        End Set
    End Property

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Sub SetStatusIcon(icon As Image)
        PictureBox1.BackgroundImage = icon
    End Sub

    ''' <summary>
    ''' thread safe invoke of set the label text
    ''' </summary>
    ''' <param name="txt"></param>
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Sub SetStatusText(txt As String)
        Call Me.Invoke(Sub() LinkLabel1.Text = txt)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RaiseEvent Run()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        RaiseEvent SelectAnnotationTabPage()
    End Sub
End Class
