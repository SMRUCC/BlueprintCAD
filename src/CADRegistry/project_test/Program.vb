Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline

Module Program
    Sub Main(args As String())
        Dim enzyme_hits = BlastpOutputReader _
            .RunParser("G:\BlueprintCAD\demo\tmp\Sophia\19032\enzyme_blast650548.txt") _
            .ExportHistResult _
            .ToArray

        Pause()
    End Sub
End Module
