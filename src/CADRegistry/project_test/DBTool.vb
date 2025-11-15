Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices.Terminal.ProgressBar.Tqdm
Imports SMRUCC.genomics.Analysis.SequenceTools.SequencePatterns
Imports SMRUCC.genomics.Interops.NBCR.MEME_Suite.DocumentFormat.XmlOutput.MEME

Public Module DBTool

    Sub CreateMotifDb()
        Using s As Stream = "G:\BlueprintCAD\App\net8.0-windows\data\RegPrecise.dat".Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False),
            db As New PWMDatabase(s, is_readonly:=False)

            Call "create motif database...".info

            For Each dir As String In TqdmWrapper.Wrap("M:\motifs".ListDirectory.ToArray)
                Dim name As String = dir.BaseName.Replace("_motif_results", "")
                Dim file As String = $"{dir}/meme.xml"

                If file.FileLength <= 0 Then
                    Continue For
                End If

                Dim scan As MEMEXml = MEMEXml.LoadDocument(file)
                Dim pwm As Probability() = scan.GetMotifs.ToArray

                Call db.AddPWM(name, pwm)
            Next
        End Using
    End Sub
End Module
