Imports BlueprintCAD.RibbonLib.Controls
Imports Microsoft.VisualBasic.ApplicationServices
Imports RibbonLib.Controls.Events

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    ' **NEW** ApplyApplicationDefaults: Raised when the application queries default values to be set for the application.

    ' Example:
    ' Private Sub MyApplication_ApplyApplicationDefaults(sender As Object, e As ApplyApplicationDefaultsEventArgs) Handles Me.ApplyApplicationDefaults
    '
    '   ' Setting the application-wide default Font:
    '   e.Font = New Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular)
    '
    '   ' Setting the HighDpiMode for the Application:
    '   e.HighDpiMode = HighDpiMode.PerMonitorV2
    '
    '   ' If a splash dialog is used, this sets the minimum display time:
    '   e.MinimumSplashScreenDisplayTime = 4000
    ' End Sub

    Partial Friend Class MyApplication

        Public Shared Sub SetRibbonEvents()
            Dim ribbon As RibbonItems = Workbench.AppHost.m_ribbonItems

            AddHandler ribbon.ButtonOpenVirtualCellPackFile.ExecuteEvent, AddressOf OpenVirtualCellPackFile
            AddHandler ribbon.ButtonExit.ExecuteEvent, AddressOf Close
            AddHandler ribbon.ButtonRun.ExecuteEvent, AddressOf RunVirtualCell
        End Sub

        Private Shared Sub Close(sender As Object, e As ExecuteEventArgs)
            Try
                Call AppHost.Close()
                Call App.Exit()
            Catch ex As Exception
            End Try
        End Sub

        Public Shared Sub OpenVirtualCellPackFile(sender As Object, e As ExecuteEventArgs)
            Using file As New OpenFileDialog With {.Filter = "Virtual Cell Data Pack(*.vcellPack)|*.vcellPack"}
                If file.ShowDialog = DialogResult.OK Then
                    Call Workbench.OpenDocument(Of CellBrowser)() _
                        .OpenVirtualCellDataFile(file.FileName)
                End If
            End Using
        End Sub

        Public Shared Sub RunVirtualCell(sender As Object, e As ExecuteEventArgs)
            Dim step1 As New FormConfigGenerator()

            If step1.ShowDialog() = DialogResult.OK Then
                Dim step2 = New FormKnockoutGenerator() _
                    .LoadConfig(step1.configFile) _
                    .LoadModelFiles(step1.modelFiles)

                If step2.ShowDialog = DialogResult.OK Then
                    Dim step3 = New FormCultureMedium().SetConfigAndModels(step2.config, step1.configFile, step2.modelList)

                    If step3.ShowDialog = DialogResult.OK Then
                        ' run the virtual cell simulation
                        Dim vc As String = $"{App.HOME}/VirtualCell.exe"
                        Dim args As String = $"--run {step1.configFile.CLIPath}"
                        Dim proc As New Process With {
                            .StartInfo = New ProcessStartInfo With {
                                .FileName = vc,
                                .Arguments = args,
                                .UseShellExecute = True,
                                .CreateNoWindow = False,
                                .RedirectStandardOutput = False,
                                .RedirectStandardError = False
                            }
                        }

                        Call proc.Start()
                    End If
                End If
            End If
        End Sub
    End Class
End Namespace
