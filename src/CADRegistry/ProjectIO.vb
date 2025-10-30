Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Zip
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.ComponentModel.Annotation
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models
Imports SMRUCC.genomics.Metagenomics
Imports SMRUCC.genomics.SequenceModel.FASTA

Public Module ProjectIO

    Public Function Load(filepath As String) As GenBankProject
        Using s As Stream = filepath.Open(FileMode.Open, doClear:=False, [readOnly]:=True)
            Return Load(s)
        End Using
    End Function

    Public Function Load(s As Stream) As GenBankProject
        Using zip As New ZipStream(s, is_readonly:=True)
            Dim source_json As String = zip.ReadAllText("/source.json")
            Dim source_nt As String = zip.ReadAllText("/source.txt")
            Dim nucl_fasta As FastaSeq() = FastaFile.DocParser(zip.ReadLines("/genes.txt")).ToArray
            Dim prot_fasta As FastaSeq() = FastaFile.DocParser(zip.ReadLines("/proteins.txt")).ToArray
            Dim tss_fasta As FastaSeq() = FastaFile.DocParser(zip.ReadLines("/tss_upstream.txt")).ToArray
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
            Dim operon_hits As HitCollection() = zip _
                .ReadLines("/localblast/operon_hits.jsonl") _
                .SafeQuery _
                .Select(Function(line) line.LoadJSON(Of HitCollection)(throwEx:=False)) _
                .Where(Function(line) Not line Is Nothing) _
                .ToArray
            Dim operons As AnnotatedOperon() = zip _
                .ReadLines("/localblast/operons.jsonl") _
                .SafeQuery _
                .Select(Function(line) line.LoadJSON(Of AnnotatedOperon)(throwEx:=False)) _
                .Where(Function(line) Not line Is Nothing) _
                .ToArray
            Dim ec_numbers As Dictionary(Of String, ECNumberAnnotation) = zip _
                .ReadLines("/localblast/ec_numbers.jsonl") _
                .SafeQuery _
                .Select(Function(line) line.LoadJSON(Of ECNumberAnnotation)(throwEx:=False)) _
                .Where(Function(line) Not line Is Nothing) _
                .ToDictionary(Function(e) e.gene_id)

            Return New GenBankProject With {
                .enzyme_hits = enzyme_hits,
                .nt = source_nt,
                .taxonomy = source_json.LoadJSON(Of Taxonomy)(throwEx:=False),
                .gene_table = genes,
                .genes = nucl_fasta.ToDictionary(Function(a) a.Title, Function(a) a.SequenceData),
                .proteins = prot_fasta.ToDictionary(Function(a) a.Title, Function(a) a.SequenceData),
                .tss_upstream = tss_fasta.ToDictionary(Function(a) a.Title, Function(a) a.SequenceData),
                .ec_numbers = ec_numbers,
                .operon_hits = operon_hits,
                .operons = operons
            }
        End Using
    End Function

    <Extension>
    Public Sub SaveZip(proj As GenBankProject, filepath As String)
        Using zip As New ZipStream(filepath.Open(FileMode.OpenOrCreate, doClear:=True, [readOnly]:=False))
            Call zip.WriteText(proj.taxonomy.GetJson, "/source.json")
            Call zip.WriteText(proj.nt, "/source.txt")

            Call proj.DumpGeneFasta(zip.OpenFile("/genes.txt", access:=FileAccess.Write))
            Call proj.DumpProteinFasta(zip.OpenFile("/proteins.txt", access:=FileAccess.Write))
            Call proj.DumpTSSUpstreamFasta(zip.OpenFile("/tss_upstream.txt", access:=FileAccess.Write))

            Call zip.WriteLines(proj.gene_table.SafeQuery.Select(Function(a) a.GetJson), "/genes.jsonl")
            Call zip.WriteLines(proj.enzyme_hits.SafeQuery.Select(Function(q) q.GetJson), "/localblast/enzyme_hits.jsonl")
            Call zip.WriteLines(proj.operon_hits.SafeQuery.Select(Function(q) q.GetJson), "/localblast/operon_hits.jsonl")
            Call zip.WriteLines(proj.ec_numbers.SafeQuery.Select(Function(e) e.Value.GetJson), "/localblast/ec_numbers.jsonl")
            Call zip.WriteLines(proj.operons.SafeQuery.Select(Function(e) e.GetJson), "/localblast/operons.jsonl")
        End Using
    End Sub
End Module
