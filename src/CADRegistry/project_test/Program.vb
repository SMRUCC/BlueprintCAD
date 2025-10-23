Imports CADRegistry
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline

Module Program
    Sub Main(args As String())
        Dim proj = ProjectCreator.FromGenBank(GBFF.File.Load("G:\BlueprintCAD\demo\Escherichia coli str. K-12 substr. MG1655.gbff"))

        proj.enzyme_hits = BlastpOutputReader _
            .RunParser("G:\BlueprintCAD\demo\tmp\Sophia\19032\enzyme_blast650548.txt") _
            .ExportHistResult _
            .ToArray

        proj.SaveZip("G:\BlueprintCAD\demo\ECOLI.gcproj")

        Pause()
    End Sub
End Module
