Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.Data.visualize.Network
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualStudio.WinForms.Docking
Imports Microsoft.Web.WebView2.Core
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CellViewer

    Dim cell As VirtualCell
    Dim filepath As String
    Dim explorer As CellExplorer

    Private Async Sub CellViewer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await WebViewLoader.Init(WebView21)

        explorer = New CellExplorer
        explorer.Show(Workbench.AppHost.GetDockPanel, dockState:=DockState.DockLeft)
    End Sub

    Private Sub CellViewer_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub CellViewer_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Workbench.RestoreFormTitle()
    End Sub

    Private Sub CellViewer_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        If cell IsNot Nothing Then
            Workbench.SetFormActiveTitle($"Inspect: {cell.cellular_id}")
        End If
    End Sub

    Public Function LoadModel(file As String) As CellViewer
        filepath = file

        Call ProgressSpinner.DoLoading(Sub() cell = file.LoadXml(Of VirtualCell))
        Call explorer.LoadModel(cell, Me)
        Call CellViewer_Activated(Nothing, Nothing)

        Return Me
    End Function

    Private Sub WebView21_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs)
        WebView21.CoreWebView2.Navigate($"http://localhost:{WebPort}/cell_graph.html")
    End Sub

    Public Sub ViewGraph(g As graphology.graph)
        Call WebView21.CoreWebView2.PostWebMessageAsJson(g.GetJson(simpleDict:=True))
    End Sub
End Class