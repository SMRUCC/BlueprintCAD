Public Module Workbench

    Public ReadOnly Property AppHost As FormMain

    Public Sub SetHost(appHost As FormMain)
        _AppHost = appHost
    End Sub

    Friend Sub LogText(v As String)

    End Sub

    Friend Sub Warning(v As String)

    End Sub
End Module
