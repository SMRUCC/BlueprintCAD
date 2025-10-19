Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.GBFF.Keywords.FEATURES

Public Class FormAnnotation

    Dim filepath As String
    Dim proj As GenBankProject

    Private Sub FormAnnotation_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        Workbench.SetFormActiveTitle(TabText)
    End Sub

    Private Sub FormAnnotation_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Workbench.RestoreFormTitle()
    End Sub

    Public Function LoadModel(filepath As String) As FormAnnotation
        Dim genbank = GBFF.File.Load(filepath)
        Dim nucl = genbank _
            .EnumerateGeneFeatures _
            .ToDictionary(Function(a) a.Query(FeatureQualifiers.locus_tag),
                            Function(a)
                                Return a.SequenceData
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
End Class