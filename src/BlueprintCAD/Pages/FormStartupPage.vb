Imports System.Runtime.InteropServices
Imports BlueprintCAD.My
Imports Galaxy.Workbench
Imports Microsoft.Web.WebView2.Core

Public Class FormStartupPage

    Private Async Sub FormStartupPage_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await WebViewLoader.Init(WebView21, enableDevTool:=True)
    End Sub

    Private Sub WebView21_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles WebView21.CoreWebView2InitializationCompleted
        Call WebView21.CoreWebView2.AddHostObjectToScript("host", New HostObject)
        Call WebView21.CoreWebView2.Navigate($"http://localhost:{AppEnvironment.WebPort}/index.html")
    End Sub

    <ComVisible(True)>
    Public Class HostObject

        Public Sub RunModel()
            Call MyApplication.RunVirtualCell(Nothing, Nothing)
        End Sub
    End Class
End Class