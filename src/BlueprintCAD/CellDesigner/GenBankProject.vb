Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices.Zip
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.ComponentModel.Annotation
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models
Imports SMRUCC.genomics.Metagenomics
Imports SMRUCC.genomics.SequenceModel
Imports SMRUCC.genomics.SequenceModel.FASTA

Public Class GenBankProject

    Public Property taxonomy As Taxonomy
    Public Property nt As String
    Public Property genes As Dictionary(Of String, String)
    Public Property proteins As Dictionary(Of String, String)
    Public Property gene_table As GeneTable()
    Public Property enzyme_hits As HitCollection()

    Public Sub DumpProteinFasta(s As Stream)
        Call FASTA.StreamWriter.WriteList(proteins, s)
    End Sub

    Public Sub SaveZip(filepath As String)
        Using zip As New ZipStream(filepath.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False))
            Call zip.WriteText(taxonomy.GetJson, "/source.json")
            Call zip.WriteText(nt, "/source.txt")
            Call FASTA.StreamWriter.WriteList(genes, zip.OpenFile("/genes.txt", access:=FileAccess.Write))
            Call DumpProteinFasta(zip.OpenFile("/proteins.txt", access:=FileAccess.Write))
            Call zip.WriteLines(gene_table.SafeQuery.Select(Function(a) a.GetJson), "/genes.jsonl")
            Call zip.WriteLines(enzyme_hits.SafeQuery.Select(Function(q) q.GetJson), "/localblast/enzyme_hits.jsonl")
        End Using
    End Sub

    Public Shared Function Load(filepath As String) As GenBankProject
        Using s As Stream = filepath.Open(FileMode.Open, doClear:=False, [readOnly]:=True)
            Return Load(s)
        End Using
    End Function

    Public Shared Function Load(s As Stream) As GenBankProject
        Using zip As New ZipStream(s, is_readonly:=True)
            Dim source_json As String = zip.ReadAllText("/source.json")
            Dim source_nt As String = zip.ReadAllText("/source.txt")
            Dim nucl_fasta As FastaSeq() = FastaFile.DocParser(zip.ReadLines("/genes.txt")).ToArray
            Dim prot_fasta As FastaSeq() = FastaFile.DocParser(zip.ReadLines("/proteins.txt")).ToArray
            Dim genes As GeneTable() = zip _
                .ReadLines("/genes.jsonl") _
                .SafeQuery _
                .Select(Function(line) line.LoadJSON(Of GeneTable)) _
                .Where(Function(line) Not line Is Nothing) _
                .ToArray
            Dim enzyme_hits As HitCollection() = zip _
                .ReadLines("/localblast/enzyme_hits.jsonl") _
                .SafeQuery _
                .Select(Function(line) line.LoadJSON(Of HitCollection)(throwEx:=False)) _
                .Where(Function(line) Not line Is Nothing) _
                .ToArray

            Return New GenBankProject With {
                .enzyme_hits = enzyme_hits,
                .nt = source_nt,
                .taxonomy = source_json.LoadJSON(Of Taxonomy)(throwEx:=False),
                .gene_table = genes,
                .genes = nucl_fasta.ToDictionary(Function(a) a.Title, Function(a) a.SequenceData),
                .proteins = prot_fasta.ToDictionary(Function(a) a.Title, Function(a) a.SequenceData)
            }
        End Using
    End Function
End Class
