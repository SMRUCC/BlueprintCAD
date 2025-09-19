Imports WeifenLuo.WinFormsUI.Docking

Public Module Workbench

    Public ReadOnly Property AppHost As FormMain

    Public Sub SetHost(appHost As FormMain)
        _AppHost = appHost
    End Sub

    Friend Sub LogText(v As String)

    End Sub

    Friend Sub Warning(v As String)

    End Sub

    Public Function OpenDocument(Of T As {New, DocumentWindow})(Optional title As String = Nothing) As T
        Dim docPage As New T

        If Not title.StringEmpty(, True) Then
            docPage.TabText = title
            docPage.Text = title
        Else
            docPage.TabText = title
        End If

        docPage.Show(AppHost.DockPanel)
        docPage.DockState = DockState.Document

        Return docPage
    End Function
End Module
