Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices
Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.GBFF.Keywords.FEATURES
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.InteropService
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Programs
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models

Public Class FormAnnotation

    Dim filepath As String
    Dim proj As GenBankProject

    Private Sub FormAnnotation_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        Workbench.SetFormActiveTitle(TabText)
    End Sub

    Private Sub FormAnnotation_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Workbench.RestoreFormTitle()
    End Sub

    Public Function LoadProject(filepath As String) As FormAnnotation
        Me.filepath = filepath
        Me.proj = GenBankProject.Load(filepath)

        Return Me
    End Function

    Public Function LoadModel(filepath As String) As FormAnnotation
        Dim genbank = GBFF.File.Load(filepath)
        Dim nucl = genbank _
            .EnumerateGeneFeatures(ORF:=False) _
            .GroupBy(Function(a) a.Query(FeatureQualifiers.locus_tag)) _
            .ToDictionary(Function(a) a.Key,
                          Function(a)
                              Return a.First.SequenceData
                          End Function)
        Dim prot = genbank _
            .ExportProteins_Short(True) _
            .ToDictionary(Function(a) a.Title,
                          Function(a)
                              Return a.SequenceData
                          End Function)
        Dim proj As New GenBankProject With {
            .taxonomy = genbank.Source.GetTaxonomy,
            .nt = genbank.Origin.SequenceData,
            .genes = nucl,
            .proteins = prot,
            .gene_table = genbank.ExportGeneFeatures
        }

        Me.proj = proj
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

        Call ApplyVsTheme(ToolStrip1)
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

    Private Async Sub EnzymeAnnotationCmd_Run() Handles EnzymeAnnotationCmd.Run
        Dim tempfile As String = TempFileSystem.GetAppSysTempFile(".fasta", sessionID:=App.PID, prefix:="enzyme_blast")
        Dim tempOutfile As String = tempfile.ChangeSuffix("txt")

        Using s As Stream = tempfile.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False)
            Call proj.DumpProteinFasta(s)
        End Using

        Dim blastp As New BLASTPlus(Workbench.Settings.ncbi_blast) With {.NumThreads = 8}
        Dim enzyme_db As String = $"{App.HOME}/data/ec_numbers.fasta"

        Await Task.Run(Sub() blastp.FormatDb(enzyme_db, dbType:=blastp.MolTypeProtein).Run())
        Await Task.Run(Sub() blastp.Blastp(tempfile, enzyme_db, tempOutfile, e:=0.01).Run())

        proj.enzyme_hits = BlastpOutputReader _
            .RunParser(tempOutfile) _
            .ExportHistResult _
            .ToArray
    End Sub
End Class