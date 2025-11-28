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
    Dim nodeId As String

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
            Dim message As String = e.WebMessageAsJson

            If String.IsNullOrEmpty(message) Then
                Return
            End If

            Dim messageObject = message.LoadJSON(Of Dictionary(Of String, String))

            ' 检查消息类型
            If messageObject("type") = "nodeClicked" Then
                nodeId = messageObject.TryGetValue("nodeId")

                If nodeId Is Nothing Then
                    Return
                Else
                    Dim target = explorer.GetNodeName(nodeId)

                    Call explorer.ShowNode(nodeId)

                    If target.type Is Nothing Then
                        ToolStripStatusLabel3.Text = "<Nothing>"
                        nodeId = Nothing
                    Else
                        ToolStripStatusLabel3.Text = $"<{target.type}> {target.name}"
                    End If
                End If
            End If
        Catch ex As Exception
            ' 处理 JSON 解析或其他错误
            Call CommonRuntime.Warning($"处理Web消息时出错: {ex.Message}")
        End Try
    End Sub

    Private Async Sub ToolStripStatusLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel2.Click
        If Not nodeId Is Nothing Then
            Await explorer.viewGraph(nodeId)
        End If
    End Sub
End Class