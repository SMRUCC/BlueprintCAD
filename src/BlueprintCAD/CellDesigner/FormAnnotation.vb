Imports System.IO
Imports System.Text
Imports CADRegistry
Imports Galaxy.Data.TableSheet
Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Unit
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Drawing
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Driver
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualStudio.WinForms.Docking
Imports SMRUCC.genomics.Analysis.SequenceTools.SequencePatterns
Imports SMRUCC.genomics.Analysis.SequenceTools.SequencePatterns.SequenceLogo
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Application.BBH
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Programs
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models
Imports SMRUCC.genomics.SequenceModel.FASTA
Imports SMRUCC.genomics.Visualize.ChromosomeMap
Imports SMRUCC.genomics.Visualize.ChromosomeMap.Configuration
Imports SMRUCC.genomics.Visualize.ChromosomeMap.DrawingModels

Public Class FormAnnotation

    Dim filepath As String
    Dim proj As GenBankProject
    Dim metadata As GenbankProperties

    Dim enzymeLoader As GridLoaderHandler
    Dim blastLoader As GridLoaderHandler
    Dim tfbsLoader As GridLoaderHandler
    Dim tfListLoader As GridLoaderHandler
    Dim transportLoader As GridLoaderHandler

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
                                        Dim println As Action(Of String) = bar.Echo

                                        Call println("Load enzyme annotation result...")
                                        Call Me.Invoke(Sub() enzymeLoader.LoadTable(AddressOf LoadEnzymeHits))
                                        Call println("Load conserved gene clusters annotation result...")
                                        Call Me.Invoke(Sub() operonLoader.LoadTable(AddressOf LoadOperonHits))
                                        Call println("Load TF binding sites annotation result...")
                                        Call Me.Invoke(Sub() tfbsLoader.LoadTable(AddressOf LoadTFBSList))
                                        Call println("Load transcript factors annotation result...")
                                        Call Me.Invoke(Sub() tfListLoader.LoadTable(AddressOf LoadTFHits))
                                        Call println("Load membrane transporter annotation result...")
                                        Call Me.Invoke(Sub() transportLoader.LoadTable(AddressOf loadTransporter))
                                    End Sub, info:="Load annotation table data into workspace viewer...")

        metadata = New GenbankProperties(proj)
        CommonRuntime.GetPropertyWindow.SetObject(metadata)
        CommonRuntime.GetOutputWindow.AddLog(Now, "open annotation project", "open gcmodeller virtualcell annotation project file success!", MSG_TYPES.INF)

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

    Dim pwm As Dictionary(Of String, Probability())

    Private Async Sub FormAnnotation_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = Workbench.Settings.ncbi_blast
        TextBox2.Text = Workbench.Settings.registry_server
        enzymeLoader = New GridLoaderHandler(DataGridView1, ToolStrip2)
        blastLoader = New GridLoaderHandler(AdvancedDataGridView1, AdvancedDataGridViewSearchToolBar1)
        operonLoader = New GridLoaderHandler(AdvancedDataGridView2, AdvancedDataGridViewSearchToolBar2)
        tfbsLoader = New GridLoaderHandler(AdvancedDataGridView3, AdvancedDataGridViewSearchToolBar3)
        tfListLoader = New GridLoaderHandler(AdvancedDataGridView4, AdvancedDataGridViewSearchToolBar4)
        transportLoader = New GridLoaderHandler(AdvancedDataGridView5, AdvancedDataGridViewSearchToolBar5)

        EnzymeAnnotationCmd.Text = "Enzyme Number Annotation"
        TFAnnotationCmd.Text = "Transcript Factor Annotation"
        OperonAnnotationCmd.Text = "Operon Cluster Annotation"
        TFBSAnnotationCmd.Text = "TF Binding Site Annotation"
        TransporterAnnotationCmd.Text = "Membrane Transporter Annotation"

        RichTextBox1.Rtf = Encoding.UTF8.GetString(My.Resources.HelpDocs.ModelAnnotation)

        Call ApplyVsTheme(ToolStrip1,
                          ToolStrip2,
                          AdvancedDataGridViewSearchToolBar1,
                          AdvancedDataGridViewSearchToolBar2,
                          AdvancedDataGridViewSearchToolBar3,
                          AdvancedDataGridViewSearchToolBar4,
                          AdvancedDataGridViewSearchToolBar5,
                          ContextMenuStrip1)

        pwm = Await Task.Run(Function() MotifDatabase.LoadMotifs(OpenMotifDatabaseFile))
    End Sub

    Private Function OpenMotifDatabaseFile() As Stream
        Return $"{App.HOME}/data/RegPrecise.dat".Open(FileMode.Open, doClear:=False, [readOnly]:=True)
    End Function

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Not Workbench.ServerConnection Then
            Call MessageBox.Show("You are disconnected from the registry server, there are no data source for the virtual cell model compiler for build the model file!",
                                 "Disconnected",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning)
            Return
        End If

        Using file As New SaveFileDialog With {.Filter = "GCModeller VirtualCell Model File(*.xml)|*.xml"}
            If file.ShowDialog = DialogResult.OK Then
                ' 保存工程项目
                Call SaveDocument()

                If filepath.StringEmpty(, True) Then
                    Return
                End If

                ' run compile of the virtual cell model
                Dim saveResult As String = file.FileName
                Dim gcc As String = $"{App.HOME}/Compiler.exe"
                Dim server As String = Workbench.Settings.registry_server
                Dim args As String = $"{filepath.CLIPath} --out {saveResult.CLIPath} --server ""{server}"" --name {saveResult.BaseName}  /@set tqdm=false"
                Dim proc As FormConsoleHost = CommonRuntime.ShowDocument(Of FormConsoleHost)(DockState.Document, "Build Virtual Cell...")

                Call proc.LogText("run virtualcell compiler " & args, Color.Red)
                Call CommonRuntime.StatusMessage("run virtualcell compiler " & args)
                Call proc.Run(gcc, args)

                If saveResult.FileLength > ByteSize.KB Then
                    Call CommonRuntime.GetOutputWindow.AddLog(Now, "compile virtualcell", "build virtualcell model file success!", MSG_TYPES.FINEST)
                    Call MessageBox.Show("Build Virtual Cell Model Success!", "Build Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End Using
    End Sub

    Private Sub FlowLayoutPanel1_SizeChanged(sender As Object, e As EventArgs) Handles FlowLayoutPanel1.SizeChanged
        For Each item As Control In FlowLayoutPanel1.Controls
            item.Width = FlowLayoutPanel1.Width - 15
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
            Call ProgressSpinner.DoLoading(Sub() proj.SaveZip(filepath))
        End If
    End Sub

    Private Sub LoadEnzymeHits(tbl As DataTable)
        Dim hits As Integer = 0

        Call tbl.Columns.Add("gene", GetType(String))
        Call tbl.Columns.Add("supports", GetType(Integer))
        Call tbl.Columns.Add("ec_number", GetType(String))
        Call tbl.Columns.Add("score", GetType(Double))
        Call tbl.Columns.Add("sources", GetType(String))

        If proj Is Nothing Then
            Return
        End If

        For Each enzyme As HitCollection In proj.enzyme_hits.SafeQuery
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

        If Not metadata Is Nothing Then
            metadata.enzymes = proj.ec_numbers.Count
        End If

        EnzymeAnnotationCmd.Running = False

        If Not proj.enzyme_hits.IsNullOrEmpty Then
            Call EnzymeAnnotationCmd.SetStatusIcon(SuccessIcon)
            Call EnzymeAnnotationCmd.SetStatusText($"Found {proj.enzyme_hits.Length} enzyme number hits.")
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

        If proj Is Nothing Then
            Return
        End If

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

        If Not metadata Is Nothing Then
            metadata.operons = proj.operons.Length
        End If

        OperonAnnotationCmd.Running = False

        If Not proj.enzyme_hits.IsNullOrEmpty Then
            Call OperonAnnotationCmd.SetStatusIcon(SuccessIcon)
            Call OperonAnnotationCmd.SetStatusText($"{proj.operons.Length} operons was annotated.")
        End If
    End Sub

    Private Async Sub TFAnnotationCmd_Run() Handles TFAnnotationCmd.Run
        Dim tempfile As String = TempFileSystem.GetAppSysTempFile(".fasta", sessionID:=App.PID, prefix:="tf_blast")
        Dim tempOutfile As String = tempfile.ChangeSuffix("txt")

        If TFAnnotationCmd.Running Then
            Return
        Else
            TFAnnotationCmd.Running = True
            TFAnnotationCmd.SetStatusText("Running the annotation...")
            TFAnnotationCmd.SetStatusIcon(ProcessIcon)
        End If

        Using s As Stream = tempfile.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpProteinFasta(s)
        End Using

        Dim blastp As New BLASTPlus(Workbench.Settings.ncbi_blast) With {.NumThreads = 12}
        Dim tf_db As String = $"{App.HOME}/data/TF.fasta"

        ' Await Task.Run(Sub() blastp.FormatDb(enzyme_db, dbType:=blastp.MolTypeProtein).Run())
        Await Task.Run(Sub() blastp.Blastp(tempfile, tf_db, tempOutfile, e:=0.01).Run())

        proj.tf_hits = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHitsResult _
            .ToArray
        proj.transcript_factors = proj.tf_hits _
            .Select(Function(hits) hits.AssignTFFamilyHit()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToArray

        Call tfListLoader.LoadTable(AddressOf LoadTFHits)
    End Sub

    Private Sub LoadTFHits(tbl As DataTable)
        Call tbl.Columns.Add("gene_id", GetType(String))
        Call tbl.Columns.Add("tf family", GetType(String))
        Call tbl.Columns.Add("hit", GetType(String))
        Call tbl.Columns.Add("supports", GetType(Integer))
        Call tbl.Columns.Add("identities", GetType(Double))
        Call tbl.Columns.Add("positive", GetType(Double))
        Call tbl.Columns.Add("score", GetType(Double))
        Call tbl.Columns.Add("e-value", GetType(Double))

        If proj Is Nothing Then
            Return
        End If

        For Each tf As BestHit In proj.transcript_factors
            Call tbl.Rows.Add(tf.QueryName,
                              tf.HitName,
                              tf.description,
                              tf.hit_length,
                              tf.identities,
                              tf.positive,
                              tf.score,
                              tf.evalue)
        Next

        If Not metadata Is Nothing Then
            metadata.transcript_factors = proj.transcript_factors.Length
        End If

        TFAnnotationCmd.Running = False

        If Not proj.transcript_factors.IsNullOrEmpty Then
            Call TFAnnotationCmd.SetStatusIcon(SuccessIcon)
            Call TFAnnotationCmd.SetStatusText($"{proj.transcript_factors.Length} transcript factors was annotated.")
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
            EnzymeAnnotationCmd.SetStatusIcon(ProcessIcon)
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
            .ExportHitsResult _
            .ToArray
        proj.ec_numbers = proj.enzyme_hits _
            .Select(Function(hits) hits.AssignECNumber()) _
            .Where(Function(ec) Not ec Is Nothing) _
            .ToDictionary(Function(a) a.gene_id)

        Call enzymeLoader.LoadTable(AddressOf LoadEnzymeHits)
    End Sub

    Private Async Sub TransporterAnnotationCmd_Run() Handles TransporterAnnotationCmd.Run
        Dim tempfile As String = TempFileSystem.GetAppSysTempFile(".fasta", sessionID:=App.PID, prefix:="transporter_blast")
        Dim tempOutfile As String = tempfile.ChangeSuffix("txt")

        If TransporterAnnotationCmd.Running Then
            Return
        Else
            TransporterAnnotationCmd.Running = True
            TransporterAnnotationCmd.SetStatusText("Running the annotation...")
            TransporterAnnotationCmd.SetStatusIcon(ProcessIcon)
        End If

        Using s As Stream = tempfile.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpProteinFasta(s)
        End Using

        Dim blastp As New BLASTPlus(Workbench.Settings.ncbi_blast) With {.NumThreads = Workbench.Settings.n_threads}
        Dim enzyme_db As String = $"{App.HOME}/data/Membrane.fasta"

        ' Await Task.Run(Sub() blastp.FormatDb(enzyme_db, dbType:=blastp.MolTypeProtein).Run())
        Await Task.Run(Sub() blastp.Blastp(tempfile, enzyme_db, tempOutfile, e:=0.01).Run())

        proj.transporter = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHitsResult(grepName:=Function(name) name.GetTagValue("|")) _
            .ToArray
        proj.membrane_proteins = proj.transporter _
            .Select(Function(hits) RankTerm.RankTopTerm(hits)) _
            .IteratesALL _
            .ToArray

        Call transportLoader.LoadTable(AddressOf loadTransporter)
    End Sub

    Private Sub loadTransporter(tbl As DataTable)
        Dim transporters As IGrouping(Of String, RankTerm)() = proj.membrane_proteins _
            .SafeQuery _
            .GroupBy(Function(a) a.queryName) _
            .ToArray

        Call tbl.Columns.Add("gene_id", GetType(String))
        Call tbl.Columns.Add("subcellular location", GetType(String))
        Call tbl.Columns.Add("supports", GetType(Integer))
        Call tbl.Columns.Add("score", GetType(Double))
        Call tbl.Columns.Add("sources", GetType(String))

        For Each group As IGrouping(Of String, RankTerm) In transporters
            For Each term As RankTerm In group
                Call tbl.Rows.Add(group.Key, term.term, term.source.Length, term.score, term.source.JoinBy(", "))
            Next
        Next

        Dim n As Integer = transporters.Length

        If Not metadata Is Nothing Then
            metadata.transporter = n
        End If

        TransporterAnnotationCmd.Running = False

        If Not proj.membrane_proteins.IsNullOrEmpty Then
            Call TransporterAnnotationCmd.SetStatusIcon(SuccessIcon)
            Call TransporterAnnotationCmd.SetStatusText($"{n} membrane transporter was annotated.")
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row = DataGridView1.SelectedRows(0)
        Dim gene_id = CStr(row.Cells(0).Value)
        Dim hits = proj.enzyme_hits.KeyItem(gene_id)

        viewDetails = Nothing

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

        If Not Workbench.ServerConnection Then
            MessageBox.Show("Sorry, no server connection for request operon dataset.", "No Server Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        If OperonAnnotationCmd.Running Then
            Return
        Else
            OperonAnnotationCmd.Running = True
            OperonAnnotationCmd.SetStatusText("Search for conserved gene clusters...")
            OperonAnnotationCmd.SetStatusIcon(ProcessIcon)
        End If

        Using s As Stream = tempfile.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpGeneFasta(s)
        End Using

        Dim blastp As New BLASTPlus(Workbench.Settings.ncbi_blast) With {.NumThreads = 12}
        Dim operon_db As String = $"{App.HOME}/data/operon.fasta"
        Dim knownOperons = Await Task.Run(Function() Workbench.CADRegistry.GetAllKnownOperons.ToDictionary(Function(a) a.cluster_id))

        Await Task.Run(Sub() blastp.FormatDb(operon_db, dbType:=blastp.MolTypeNucleotide).Run())
        Await Task.Run(Sub() blastp.Blastn(tempfile, operon_db, tempOutfile, e:=0.01).Run())

        proj.operon_hits = Await Task.Run(Function() OperonAnnotator.ParseBlastn(tempOutfile).ToArray)
        proj.operons = OperonAnnotator.AnnotateOperons(proj.gene_table, proj.operon_hits, knownOperons).ToArray

        Call operonLoader.LoadTable(AddressOf LoadOperonHits)
    End Sub

    Private Async Sub TFBSAnnotationCmd_Run() Handles TFBSAnnotationCmd.Run
        Dim tss = proj.tss_upstream _
            .Select(Function(seq)
                        Return New FastaSeq({seq.Key}, seq.Value)
                    End Function) _
            .ToArray
        Dim tfbsList As Dictionary(Of String, MotifMatch()) = Nothing

        If TFBSAnnotationCmd.Running Then
            Return
        Else
            TFBSAnnotationCmd.Running = True
            TFBSAnnotationCmd.SetStatusText("Running the annotation...")
            TFBSAnnotationCmd.SetStatusIcon(ProcessIcon)
        End If

        Await Task.Run(Sub()
                           tfbsList = MotifDatabase _
                               .OpenReadOnly(OpenMotifDatabaseFile) _
                               .ScanSites(tss,
                                          n_threads:=App.CPUCoreNumbers,
                                          progress:=AddressOf TFBSAnnotationCmd.SetStatusText)
                       End Sub)

        proj.tfbs_hits = tfbsList

        tfbsLoader.LoadTable(AddressOf LoadTFBSList)
    End Sub

    Dim motif_identities_filter As Double = 0.9

    Private Sub LoadTFBSList(tbl As DataTable)
        Dim hits As Integer = 0

        Call tbl.Columns.Add("gene_id", GetType(String))
        Call tbl.Columns.Add("tfbs number", GetType(Integer))
        Call tbl.Columns.Add("family number", GetType(Integer))
        Call tbl.Columns.Add("top family", GetType(String))

        If proj Is Nothing Then
            Return
        End If

        For Each site As KeyValuePair(Of String, MotifMatch()) In proj.tfbs_hits
            Dim familyList = site.Value _
                .Where(Function(a) a.identities > motif_identities_filter) _
                .GroupBy(Function(a) a.seeds(0)) _
                .ToArray
            Dim topFamily = familyList.OrderByDescending(Function(a) a.Count).FirstOrDefault

            Call tbl.Rows.Add(site.Key,
                              familyList.Select(Function(a) a.Count).Sum,
                              familyList.Length,
                              If(topFamily Is Nothing, "-", topFamily.Key))
        Next

        Dim nsites As Integer = proj.tfbs_hits.Values.Sum(Function(a) a.Length)

        If Not metadata Is Nothing Then
            metadata.tfbs = nsites
        End If

        TFBSAnnotationCmd.Running = False

        If Not proj.tfbs_hits.IsNullOrEmpty Then
            Call TFBSAnnotationCmd.SetStatusIcon(SuccessIcon)
            Call TFBSAnnotationCmd.SetStatusText($"{nsites} motif site was found.")
        End If
    End Sub

    Private ReadOnly SuccessIcon As System.Drawing.Image = DirectCast(My.Resources.Icons.ResourceManager.GetObject("icons8-done-144"), System.Drawing.Image)
    Private ReadOnly ProcessIcon As System.Drawing.Image = DirectCast(My.Resources.Icons.ResourceManager.GetObject("icons8-workflow-96"), System.Drawing.Image)

    Private Sub LoadGeneTFBSList(tbl As DataTable, hits As MotifMatch())
        Call tbl.Columns.Add("title", GetType(String))
        Call tbl.Columns.Add("segment", GetType(String))
        Call tbl.Columns.Add("motif", GetType(String))
        Call tbl.Columns.Add("start", GetType(Integer))
        Call tbl.Columns.Add("ends", GetType(Integer))
        Call tbl.Columns.Add("identities", GetType(Double))
        Call tbl.Columns.Add("score1", GetType(Double))
        Call tbl.Columns.Add("score2", GetType(Double))
        Call tbl.Columns.Add("pvalue", GetType(Double))
        Call tbl.Columns.Add("source", GetType(String))

        If proj Is Nothing Then
            Return
        End If

        For Each site As MotifMatch In hits.Where(Function(a) a.identities > motif_identities_filter)
            Call tbl.Rows.Add(site.title,      ' 0
                              site.segment,    ' 1  
                              site.motif,      ' 2
                              site.start,      ' 3
                              site.ends,       ' 4
                              site.identities, ' 5
                              site.score1,     ' 6
                              site.score2,     ' 7
                              site.pvalue,     ' 8
                              site.seeds(0))   ' 9
        Next
    End Sub

    Private Sub viewMotifSite(row As DataGridViewRow)
        Dim family As String = CStr(row.Cells(9).Value)
        Dim model As Probability() = pwm(family)
        Dim logo As Image = model(0).CreateModel.DrawFrequency(family, driver:=Drivers.GDI).AsGDIImage

        PictureBox1.BackgroundImage = logo.CTypeGdiImage
    End Sub

    Private Sub ViewEnzymeInRegistryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewEnzymeInRegistryToolStripMenuItem.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row = DataGridView1.SelectedRows(0)
        Dim ec_number = CStr(row.Cells(2).Value)

        If ec_number <> "-" Then
            Call Tools.OpenUrlWithDefaultBrowser($"{Workbench.Settings.registry_server}/enzyme/{ec_number}")
        End If
    End Sub

    Private Sub ViewGeneInRegistryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewGeneInRegistryToolStripMenuItem.Click
        If AdvancedDataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim sel As DataGridViewRow = AdvancedDataGridView1.SelectedRows(0)
        Dim registry_id = CStr(sel.Cells(1).Value)

        Call Tools.OpenUrlWithDefaultBrowser($"{Workbench.Settings.registry_server}/molecule/{registry_id}")
    End Sub

    Private Sub ViewOperonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewOperonToolStripMenuItem.Click
        If AdvancedDataGridView2.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row = AdvancedDataGridView2.SelectedRows(0)
        Dim operon_id As String = CStr(row.Cells(0).Value)
        Dim url As String = $"http://biocad.innovation.ac.cn/operon/{operon_id}/"

        Call Tools.OpenUrlWithDefaultBrowser(url)
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Workbench.Settings.registry_server = Strings.Trim(TextBox2.Text)
        Workbench.Settings.Save()
    End Sub

    ''' <summary>
    ''' view motif site details
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AdvancedDataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles AdvancedDataGridView3.CellContentClick
        If AdvancedDataGridView3.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row As DataGridViewRow = AdvancedDataGridView3.SelectedRows(0)
        Dim gene_id = CStr(row.Cells(0).Value)
        Dim hits As MotifMatch() = proj.tfbs_hits.TryGetValue(gene_id)

        viewDetails = AddressOf viewMotifSite

        If hits Is Nothing Then
            blastLoader.ClearData()
        Else
            blastLoader.LoadTable(
                Sub(tbl)
                    Call LoadGeneTFBSList(tbl, hits)
                End Sub)
        End If
    End Sub

    Dim viewDetails As Action(Of DataGridViewRow)

    Private Sub AdvancedDataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles AdvancedDataGridView1.CellContentClick
        If AdvancedDataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row As DataGridViewRow = AdvancedDataGridView1.SelectedRows(0)

        If Not viewDetails Is Nothing Then
            Call viewDetails(row)
        End If
    End Sub

    Private Sub AdvancedDataGridView4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles AdvancedDataGridView4.CellContentClick
        If AdvancedDataGridView4.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row As DataGridViewRow = AdvancedDataGridView4.SelectedRows(0)
        Dim gene_id = CStr(row.Cells(0).Value)
        Dim hits As HitCollection = proj.tf_hits.KeyItem(gene_id)

        viewDetails = Nothing

        If hits Is Nothing Then
            blastLoader.ClearData()
        Else
            blastLoader.LoadTable(
                Sub(tbl)
                    tbl.Columns.Add("TF family", GetType(String))
                    tbl.Columns.Add("source", GetType(String))
                    tbl.Columns.Add("identities", GetType(Double))
                    tbl.Columns.Add("positive", GetType(Double))
                    tbl.Columns.Add("gaps", GetType(Double))
                    tbl.Columns.Add("score", GetType(Double))
                    tbl.Columns.Add("e-value", GetType(Double))

                    For Each hit As Hit In hits.AsEnumerable
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

    Private Sub TransporterAnnotationCmd_SelectAnnotationTabPage() Handles TransporterAnnotationCmd.SelectAnnotationTabPage
        TabControl2.SelectedTab = TabPage7
    End Sub

    Private Sub TFBSAnnotationCmd_SelectAnnotationTabPage() Handles TFBSAnnotationCmd.SelectAnnotationTabPage
        TabControl2.SelectedTab = TabPage6
    End Sub

    Private Sub TFAnnotationCmd_SelectAnnotationTabPage() Handles TFAnnotationCmd.SelectAnnotationTabPage
        TabControl2.SelectedTab = TabPage5
    End Sub

    Private Sub OperonAnnotationCmd_SelectAnnotationTabPage() Handles OperonAnnotationCmd.SelectAnnotationTabPage
        TabControl2.SelectedTab = TabPage4
    End Sub

    Private Sub EnzymeAnnotationCmd_SelectAnnotationTabPage() Handles EnzymeAnnotationCmd.SelectAnnotationTabPage
        TabControl2.SelectedTab = TabPage3
    End Sub

    Private Sub AdvancedDataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles AdvancedDataGridView2.CellContentClick
        If AdvancedDataGridView2.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row As DataGridViewRow = AdvancedDataGridView2.SelectedRows(0)
        Dim members As Index(Of String) = CStr(row.Cells(4).Value).StringSplit("\s*,\s*").Indexing
        Dim genes As SegmentObject() = proj.gene_table _
            .Where(Function(g) g.locus_id Like members) _
            .OrderBy(Function(g) g.Location.left) _
            .Select(Function(gene)
                        Return New SegmentObject With {
                            .Location = gene.Location,
                            .CommonName = gene.commonName,
                            .Direction = gene.Location.Strand,
                            .Height = 85,
                            .Left = gene.Location.left,
                            .Right = gene.Location.right,
                            .Product = gene.function,
                            .LocusTag = gene.locus_id,
                            .Color = Brushes.Gray
                        }
                    End Function) _
            .ToArray
        Dim map As New ChromesomeDrawingModel With {
            .GeneObjects = genes,
            .Configuration = New DataReader,
            .Size = New DoubleRange(genes.Select(Function(g) {g.Left, g.Right}).IteratesALL).Length * 1.5
        }
        Dim draw As System.Drawing.Image = RegionMap _
            .PlotRegion(map, "1000,500", geneShapeHeight:=200, driver:=Drivers.GDI) _
            .AsGDIImage _
            .CTypeGdiImage

        PictureBox1.BackgroundImage = draw
    End Sub
End Class