Imports CADRegistry
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline

Module Program

    Sub Main(args As String())
        DBTool.CreateMotifDb()

    End Sub

    Sub testCreateProject()
        Dim proj = ProjectCreator.FromGenBank(GBFF.File.Load("G:\BlueprintCAD\demo\Escherichia coli str. K-12 substr. MG1655.gbff"))
        Dim server As New RegistryUrl()
        Dim knownOperons = server.GetAllKnownOperons.ToDictionary(Function(a) a.cluster_id)

        proj.operon_hits = OperonAnnotator.ParseBlastn("G:\BlueprintCAD\demo\tmp\Sophia\41948\operon_blast869219.txt").ToArray
        proj.operons = OperonAnnotator.AnnotateOperons(proj.gene_table, proj.operon_hits, knownOperons).ToArray

        proj.enzyme_hits = BlastpOutputReader _
            .RunParser("G:\BlueprintCAD\demo\tmp\Sophia\19032\enzyme_blast650548.txt") _
            .ExportHitsResult _
            .ToArray
        proj.ec_numbers = proj.enzyme_hits _
            .Select(Function(hits) hits.AssignECNumber()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToDictionary(Function(a) a.gene_id)

        proj.SaveZip("G:\BlueprintCAD\demo\ECOLI.gcproj")

        Pause()
    End Sub
End Module
