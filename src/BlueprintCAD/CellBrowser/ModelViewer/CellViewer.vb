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

    Public Sub ViewGraph(g As graphology.graph)
        Call WebView21.CoreWebView2.PostWebMessageAsJson(g.GetJson(simpleDict:=True))
    End Sub

    Private Sub WebView21_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles WebView21.CoreWebView2InitializationCompleted
        Call WebView21.CoreWebView2.Navigate($"http://localhost:{WebPort}/cell_graph.html")
    End Sub

    Private Sub WebView2Control_WebMessageReceived(sender As Object, e As CoreWebView2WebMessageReceivedEventArgs) Handles WebView21.WebMessageReceived
        Try
            ' 获取消息字符串
            Dim message As String = e.TryGetWebMessageAsString()

            If String.IsNullOrEmpty(message) Then
                Return
            End If

            Dim messageObject = message.LoadJSON(Of Dictionary(Of String, String))

            ' 检查消息类型
            If messageObject("type") = "nodeClicked" Then
                Dim nodeId As String = messageObject("nodeId")?.ToString()
                Dim nodeLabel As String = messageObject("nodeLabel")?.ToString()

                ' 现在您可以使用 nodeId 和 nodeLabel 了
                ' 例如，在标题栏显示，或者弹出一个 MessageBox
                Me.Text = $"点击了节点: {nodeLabel} (ID: {nodeId})"
                MessageBox.Show($"您点击了节点！{Environment.NewLine}ID: {nodeId}{Environment.NewLine}标签: {nodeLabel}", "节点点击事件", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ' 处理 JSON 解析或其他错误
            Call CommonRuntime.Warning($"处理Web消息时出错: {ex.Message}")
        End Try
    End Sub
End Class