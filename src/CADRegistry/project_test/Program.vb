Imports CADRegistry
Imports CellBuilder
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline
Imports SMRUCC.genomics.Model.OperonMapper

Module Program

    Sub Main(args As String())
        DBTool.CreateMotifDb()

    End Sub

    Sub testCreateProject()
        Dim proj = New ProjectCreator().FromGenBank(GBFF.File.LoadDatabase("G:\BlueprintCAD\demo\Escherichia coli str. K-12 substr. MG1655.gbff"))
        Dim server As New RegistryUrl()
        Dim knownOperons = server.GetAllKnownOperons.ToDictionary(Function(a) a.cluster_id, Function(a) New ODBOperon(a.cluster_id, a.name, a.members))
        Dim annoSet As AnnotationSet = proj.annotations

        annoSet.operon_hits = OperonAnnotator.ParseBlastn("G:\BlueprintCAD\demo\tmp\Sophia\41948\operon_blast869219.txt").ToArray
        annoSet.operons = OperonAnnotator.AnnotateOperons(proj.gene_table, annoSet.operon_hits, knownOperons).ToArray

        annoSet.enzyme_hits = BlastpOutputReader _
            .RunParser("G:\BlueprintCAD\demo\tmp\Sophia\19032\enzyme_blast650548.txt") _
            .ExportHitsResult _
            .ToArray
        annoSet.ec_numbers = annoSet.enzyme_hits _
            .Select(Function(hits) hits.AssignECNumber()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToDictionary(Function(a) a.gene_id)

        proj.SaveZip("G:\BlueprintCAD\demo\ECOLI.gcproj")

        Pause()
    End Sub
End Module
