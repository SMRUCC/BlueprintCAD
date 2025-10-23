Imports System.IO
Imports CADRegistry
Imports Galaxy.Data.TableSheet
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.GBFF.Keywords.FEATURES
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
        Me.proj = ProjectIO.Load(filepath)

        Call enzymeLoader.LoadTable(AddressOf LoadEnzymeHits)

        Return Me
    End Function

    Public Function LoadModel(filepath As String) As FormAnnotation
        Me.proj = ProjectCreator.FromGenBank(GBFF.File.Load(filepath))
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

        Call ApplyVsTheme(ToolStrip1, ToolStrip2, AdvancedDataGridViewSearchToolBar1)
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
        Call tbl.Columns.Add("sources", GetType(Double))

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

        Call enzymeLoader.LoadTable(AddressOf LoadEnzymeHits)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If DataGridView1.SelectedRows.Count = 0 Then
            Return
        End If

        Dim row = DataGridView1.SelectedRows(0)
        Dim gene_id As String = CStr(row.Cells(0).Value)
        Dim hits As HitCollection = proj.enzyme_hits.KeyItem(gene_id)

        If hits Is Nothing Then
            Call blastLoader.ClearData()
        Else
            Call blastLoader.LoadTable(
                Sub(tbl)
                    Call tbl.Columns.Add("ec_number", GetType(String))
                    Call tbl.Columns.Add("registry_id", GetType(String))
                    Call tbl.Columns.Add("identities", GetType(Double))
                    Call tbl.Columns.Add("positive", GetType(Double))
                    Call tbl.Columns.Add("gaps", GetType(Double))
                    Call tbl.Columns.Add("score", GetType(Double))
                    Call tbl.Columns.Add("e-value", GetType(Double))

                    For Each hit As Hit In hits.AsEnumerable
                        Dim annotation As String() = hit.hitName.Split("|"c)

                        Call tbl.Rows.Add(annotation(0), annotation(1), hit.identities, hit.positive, hit.gaps, hit.score, hit.evalue)
                    Next
                End Sub)
        End If
    End Sub
End Class