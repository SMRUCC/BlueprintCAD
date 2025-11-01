Imports System.IO
Imports CADRegistry
Imports Galaxy.Data.TableSheet
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Programs
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models

Public Class FormAnnotation

    Dim filepath As String
    Dim proj As GenBankProject

    Dim enzymeLoader As GridLoaderHandler
    Dim blastLoader As GridLoaderHandler

    Private Sub FormAnnotation_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        Workbench.SetFormActiveTitle(TabText)
    End Sub

    Private Sub FormAnnotation_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Workbench.RestoreFormTitle()
    End Sub

    Public Function LoadProject(filepath As String) As FormAnnotation
        Me.filepath = filepath

        Call ProgressSpinner.DoLoading(Sub() Me.proj = ProjectIO.Load(filepath))
        Call TaskProgress.RunAction(Sub(bar As ITaskProgress)
                                        Call Me.Invoke(Sub() enzymeLoader.LoadTable(AddressOf LoadEnzymeHits))
                                        Call Me.Invoke(Sub() operonLoader.LoadTable(AddressOf LoadOperonHits))
                                    End Sub, info:="Load annotation table data into workspace viewer...")

        Return Me
    End Function

    Public Function LoadModel(filepath As String) As FormAnnotation
        Call ProgressSpinner.DoLoading(Sub() Me.proj = ProjectCreator.FromGenBank(GBFF.File.Load(filepath)))

        Me.filepath = Nothing

        Return Me
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Workbench.Settings.ncbi_blast = Strings.Trim(TextBox1.Text)
        Workbench.Settings.Save()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using dir As New FolderBrowserDialog With {.InitialDirectory = Workbench.Settings.ncbi_blast}
            If dir.ShowDialog = DialogResult.OK Then
                TextBox1.Text = dir.SelectedPath

                Workbench.Settings.ncbi_blast = dir.SelectedPath
                Workbench.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub FormAnnotation_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = Workbench.Settings.ncbi_blast
        enzymeLoader = New GridLoaderHandler(DataGridView1, ToolStrip2)
        blastLoader = New GridLoaderHandler(AdvancedDataGridView1, AdvancedDataGridViewSearchToolBar1)
        operonLoader = New GridLoaderHandler(AdvancedDataGridView2, AdvancedDataGridViewSearchToolBar2)

        Call ApplyVsTheme(ToolStrip1,
                          ToolStrip2,
                          AdvancedDataGridViewSearchToolBar1,
                          ContextMenuStrip1)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Using file As New SaveFileDialog With {.Filter = "GCModeller VirtualCell Model File(*.xml)|*.xml"}
            If file.ShowDialog = DialogResult.OK Then

            End If
        End Using
    End Sub

    Private Sub FlowLayoutPanel1_SizeChanged(sender As Object, e As EventArgs) Handles FlowLayoutPanel1.SizeChanged
        For Each item As Control In FlowLayoutPanel1.Controls
            item.Width = FlowLayoutPanel1.Width - 10
        Next
    End Sub

    Protected Overrides Sub SaveDocument()
        If filepath.StringEmpty Then
            Using file As New SaveFileDialog With {.Filter = "Annotation Project(*.gcproj)|*.gcproj"}
                If file.ShowDialog = DialogResult.OK Then
                    filepath = file.FileName
                End If
            End Using
        End If
        If Not filepath.StringEmpty Then
            Call proj.SaveZip(filepath)
        End If
    End Sub

    Private Sub LoadEnzymeHits(tbl As DataTable)
        Dim hits As Integer = 0

        Call tbl.Columns.Add("gene", GetType(String))
        Call tbl.Columns.Add("supports", GetType(Integer))
        Call tbl.Columns.Add("ec_number", GetType(String))
        Call tbl.Columns.Add("score", GetType(Double))
        Call tbl.Columns.Add("sources", GetType(String))

        For Each enzyme As HitCollection In proj.enzyme_hits
            Dim supports As Integer = 0
            Dim annotation As String = "-"
            Dim score As Double = 0
            Dim source_id As String = "-"

            If proj.ec_numbers.ContainsKey(enzyme.QueryName) Then
                Dim result As ECNumberAnnotation = proj.ec_numbers(enzyme.QueryName)

                supports = result.SourceIDs.Length
                annotation = result.EC
                score = result.Score
                source_id = result.SourceIDs.JoinBy(", ")
            End If

            Call tbl.Rows.Add(
                enzyme.QueryName,
                supports,
                annotation,
                score,
                source_id
            )
        Next

        EnzymeAnnotationCmd.Running = False

        If Not proj.enzyme_hits.IsNullOrEmpty Then
            Call EnzymeAnnotationCmd.SetStatusIcon(DirectCast(My.Resources.Icons.ResourceManager.GetObject("icons8-done-144"), Image))
            Call EnzymeAnnotationCmd.SetStatusText($"{hits}/{proj.enzyme_hits.Length} enzyme number hits.")
        End If
    End Sub

    Private Sub LoadOperonHits(tbl As DataTable)
        Dim hits As Integer = 0

        Call tbl.Columns.Add("operonID", GetType(String))
        Call tbl.Columns.Add("name", GetType(String))
        Call tbl.Columns.Add("type", GetType(String))
        Call tbl.Columns.Add("size", GetType(Integer))
        Call tbl.Columns.Add("genes", GetType(String))
        Call tbl.Columns.Add("known_genes", GetType(String))
        Call tbl.Columns.Add("inserted", GetType(String))
        Call tbl.Columns.Add("deleted", GetType(String))

        For Each operon As AnnotatedOperon In proj.operons
            Call tbl.Rows.Add(operon.OperonID,
                              operon.name,
                              operon.Type.ToString,
                              operon.Genes.TryCount,
                              operon.Genes.JoinBy(", "),
                              operon.KnownGeneIds.JoinBy(", "),
                              operon.InsertedGeneIds.JoinBy(", "),
                              operon.MissingGeneIds.JoinBy(", "))
        Next

        OperonAnnotationCmd.Running = False

        If Not proj.enzyme_hits.IsNullOrEmpty Then
            Call OperonAnnotationCmd.SetStatusIcon(DirectCast(My.Resources.Icons.ResourceManager.GetObject("icons8-done-144"), Image))
            Call OperonAnnotationCmd.SetStatusText($"{hits}/{proj.operons.Length} operons annotated.")
        End If
    End Sub

    Private Async Sub EnzymeAnnotationCmd_Run() Handles EnzymeAnnotationCmd.Run
        Dim tempfile As String = TempFileSystem.GetAppSysTempFile(".fasta", sessionID:=App.PID, prefix:="enzyme_blast")
        Dim tempOutfile As String = tempfile.ChangeSuffix("txt")

        If EnzymeAnnotationCmd.Running Then
            Return
        Else
            EnzymeAnnotationCmd.Running = True
            EnzymeAnnotationCmd.SetStatusText("Running the annotation...")
            EnzymeAnnotationCmd.SetStatusIcon(DirectCast(My.Resources.Icons.ResourceManager.GetObject("icons8-workflow-96"), Image))
        End If

        Using s As Stream = tempfile.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpProteinFasta(s)
        End Using

        Dim blastp As New BLASTPlus(Workbench.Settings.ncbi_blast) With {.NumThreads = 12}
        Dim enzyme_db As String = $"{App.HOME}/data/ec_numbers.fasta"

        ' Await Task.Run(Sub() blastp.FormatDb(enzyme_db, dbType:=blastp.MolTypeProtein).Run())
        Await Task.Run(Sub() blastp.Blastp(tempfile, enzyme_db, tempOutfile, e:=0.01).Run())

        proj.enzyme_hits = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHistResult _
            .ToArray
        proj.ec_numbers = proj.enzyme_hits _
            .Select(Function(hits) hits.AssignECNumber()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToDictionary(Function(a) a.gene_id)

        Call enzymeLoader.LoadTable(AddressOf LoadEnzymeHits)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row = DataGridView1.SelectedRows(0)
        Dim gene_id = CStr(row.Cells(0).Value)
        Dim hits = proj.enzyme_hits.KeyItem(gene_id)

        If hits Is Nothing Then
            blastLoader.ClearData()
        Else
            blastLoader.LoadTable(
                Sub(tbl)
                    tbl.Columns.Add("ec_number", GetType(String))
                    tbl.Columns.Add("registry_id", GetType(String))
                    tbl.Columns.Add("identities", GetType(Double))
                    tbl.Columns.Add("positive", GetType(Double))
                    tbl.Columns.Add("gaps", GetType(Double))
                    tbl.Columns.Add("score", GetType(Double))
                    tbl.Columns.Add("e-value", GetType(Double))

                    For Each hit In hits.AsEnumerable
                        Dim annotation = hit.hitName.Split("|"c)

                        tbl.Rows.Add(annotation(0), annotation(1),
                                          hit.identities,
                                          hit.positive,
                                          hit.gaps,
                                          hit.score,
                                          hit.evalue)
                    Next
                End Sub)
        End If
    End Sub

    Shared cache As New Dictionary(Of String, WebJSON.Reaction())
    Shared cacheMols As New Dictionary(Of String, WebJSON.Molecule)

    Private Async Sub ViewNetworkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewNetworkToolStripMenuItem.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim geneRow As DataGridViewRow = DataGridView1.SelectedRows(0)
        Dim ec_number As String = CStr(geneRow.Cells(2).Value)
        Dim network = Await Task.Run(Function()
                                         Return cache.ComputeIfAbsent(ec_number, Function(any)
                                                                                     Dim listSet = Workbench.CADRegistry.GetAssociatedReactions(ec_number, simple:=True)

                                                                                     If listSet Is Nothing Then
                                                                                         Return {}
                                                                                     Else
                                                                                         Return listSet.Values.ToArray
                                                                                     End If
                                                                                 End Function)
                                     End Function)
        Dim g As New NetworkGraph
        Dim editor As FormEditor = CommonRuntime.ShowDocument(Of FormEditor)(, $"Network of enzyme {ec_number}")
        Dim v As Node

        For Each reaction As WebJSON.Reaction In network
            Dim u = g.CreateNode(reaction.guid, New NodeData With {.label = reaction.name, .origID = reaction.name, .size = {12}})

            For Each mol As WebJSON.Substrate In reaction.left
                Dim data = Await Task.Run(Function() cacheMols.ComputeIfAbsent(mol.molecule_id, Function() Workbench.CADRegistry.GetMoleculeDataById(mol.molecule_id)))
                Dim cad_id As String = mol.molecule_id.ToString

                If cad_id = "0" Then
                    Continue For
                End If

                If g.GetElementByID(cad_id) Is Nothing Then
                    v = g.CreateNode(cad_id, New NodeData With {.label = data.name, .origID = data.name, .size = {6}})
                Else
                    v = g.GetElementByID(cad_id)
                End If

                g.CreateEdge(v, u)
            Next

            For Each mol As WebJSON.Substrate In reaction.right
                Dim data = Await Task.Run(Function() cacheMols.ComputeIfAbsent(mol.molecule_id, Function() Workbench.CADRegistry.GetMoleculeDataById(mol.molecule_id)))
                Dim cad_id As String = mol.molecule_id.ToString

                If cad_id = "0" Then
                    Continue For
                End If

                If g.GetElementByID(cad_id) Is Nothing Then
                    v = g.CreateNode(cad_id, New NodeData With {.label = data.name, .origID = data.name, .size = {6}})
                Else
                    v = g.GetElementByID(cad_id)
                End If

                g.CreateEdge(u, v)
            Next
        Next

        Await editor.LoadModel(g)
    End Sub

    Dim operonLoader As GridLoaderHandler

    Private Async Sub OperonAnnotationCmd_Run() Handles OperonAnnotationCmd.Run
        Dim tempfile As String = TempFileSystem.GetAppSysTempFile(".fasta", sessionID:=App.PID, prefix:="operon_blast")
        Dim tempOutfile As String = tempfile.ChangeSuffix("txt")

        If EnzymeAnnotationCmd.Running Then
            Return
        Else
            EnzymeAnnotationCmd.Running = True
            EnzymeAnnotationCmd.SetStatusText("Running the annotation...")
            EnzymeAnnotationCmd.SetStatusIcon(DirectCast(My.Resources.Icons.ResourceManager.GetObject("icons8-workflow-96"), Image))
        End If

        Using s As Stream = tempfile.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpGeneFasta(s)
        End Using

        Dim blastp As New BLASTPlus(Workbench.Settings.ncbi_blast) With {.NumThreads = 12}
        Dim operon_db As String = $"{App.HOME}/data/operon.fasta"
        Dim knownOperons = Await Task.Run(Function() Workbench.CADRegistry.GetAllKnownOperons.ToDictionary(Function(a) a.cluster_id))

        Await Task.Run(Sub() blastp.FormatDb(operon_db, dbType:=blastp.MolTypeNucleotide).Run())
        Await Task.Run(Sub() blastp.Blastn(tempfile, operon_db, tempOutfile, e:=0.01).Run())

        proj.operon_hits = Await Task.Run(Function() BlastnOutputReader _
            .RunParser(tempOutfile) _
            .ExportHistResult _
            .ToArray)
        proj.operons = OperonAnnotator.AnnotateOperons(proj.gene_table, proj.operon_hits, knownOperons).ToArray

        Call operonLoader.LoadTable(AddressOf LoadOperonHits)
    End Sub

    Private Sub ViewEnzymeInRegistryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewEnzymeInRegistryToolStripMenuItem.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row = DataGridView1.SelectedRows(0)
        Dim ec_number = CStr(row.Cells(2).Value)

        If ec_number <> "-" Then
            Call Tools.OpenUrlWithDefaultBrowser($"http://biocad.innovation.ac.cn/enzyme/{ec_number}")
        End If
    End Sub
End Class