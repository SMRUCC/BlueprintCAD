Imports System.Threading
Imports CADRegistry
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Math.Scripting.MathExpression
Imports Microsoft.VisualBasic.Text.Xml.Models
Imports SMRUCC.genomics.ComponentModel.Annotation
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.CompilerServices
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model.Cellular
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model.Cellular.Vector
Imports SMRUCC.genomics.SequenceModel.NucleotideModels.Translation

Public Class Compiler : Inherits Compiler(Of VirtualCell)

    ReadOnly proj As GenBankProject
    ReadOnly registry As RegistryUrl

    Sub New(proj As GenBankProject, Optional serverUrl As String = RegistryUrl.defaultServer)
        Me.proj = proj
        Me.registry = New RegistryUrl(serverUrl)
    End Sub

    Protected Overrides Function PreCompile(args As CommandLine) As Integer
        m_compiledModel = New VirtualCell With {
            .taxonomy = proj.taxonomy,
            .properties = New SMRUCC.genomics.GCModeller.CompilerServices.[Property],
            .cellular_id = args("--name") Or ("cell" & Now.ToShortDateString)
        }

        Return 0
    End Function

    Protected Overrides Function CompileImpl(args As CommandLine) As Integer
        m_compiledModel.genome = BuildGenome()
        m_compiledModel.metabolismStructure = CreateMetabolismNetwork(m_compiledModel.genome.replicons.Select(Function(r) r.GetGeneList).IteratesALL.GroupBy(Function(a) a.locus_tag).ToDictionary(Function(a) a.Key, Function(a) a.First))

        Return 0
    End Function

    Private Function CreateMetabolismNetwork(genes As Dictionary(Of String, gene)) As MetabolismStructure
        Dim enzymes As Dictionary(Of String, ECNumberAnnotation) = proj.ec_numbers
        Dim network As New Dictionary(Of String, WebJSON.Reaction)
        Dim ec_numbers As New Dictionary(Of String, List(Of String))
        Dim enzymeModels As New List(Of Enzyme)

        For Each enzyme As ECNumberAnnotation In enzymes.Values
            Dim ec_number As String = enzyme.EC
            Dim list = registry.GetAssociatedReactions(enzyme.EC, simple:=False)

            If list Is Nothing Then
                Continue For
            Else
                Dim model As New Enzyme With {
                    .ECNumber = enzyme.EC,
                    .proteinID = If(genes.ContainsKey(enzyme.gene_id), genes(enzyme.gene_id).protein_id.ElementAtOrDefault(0, enzyme.gene_id), enzyme.gene_id),
                    .catalysis = list.Values _
                         .Select(Function(reaction)
                                     Return reaction.law _
                                        .SafeQuery _
                                        .Select(Function(law)
                                                    Dim pars = law.params.Keys.ToArray
                                                    Dim args As KineticsParameter() = law.params _
                                                        .Select(Function(a)
                                                                    If a.Value.IsNumeric Then
                                                                        Return New KineticsParameter With {
                                                                            .name = a.Key,
                                                                            .value = Val(a.Value),
                                                                            .isModifier = False
                                                                        }
                                                                    Else
                                                                        Return New KineticsParameter With {
                                                                            .name = a.Key,
                                                                            .value = 0,
                                                                            .isModifier = False,
                                                                            .target = a.Value
                                                                        }
                                                                    End If
                                                                End Function) _
                                                        .ToArray

                                                    Return New Catalysis With {
                                                        .reaction = reaction.guid,
                                                        .temperature = 36,
                                                        .PH = 7.0,
                                                        .formula = New FunctionElement With {
                                                            .lambda = law.lambda,
                                                            .name = enzyme.EC,
                                                            .parameters = pars
                                                        },
                                                        .parameter = args
                                                    }
                                                End Function)
                                 End Function) _
                         .IteratesALL _
                         .ToArray
                }

                Call enzymeModels.Add(model)
            End If

            Call network.AddRange(list.Where(Function(r) r.Value.left.JoinIterates(r.Value.right).All(Function(a) a.molecule_id > 0)), replaceDuplicated:=True)

            For Each guid As String In list.Keys
                If Not ec_numbers.ContainsKey(guid) Then
                    Call ec_numbers.Add(guid, New List(Of String))
                End If

                ec_numbers(guid).Add(ec_number)
            Next
        Next

        Dim compounds_id As UInteger() = network.Values _
            .Select(Function(r) r.left.JoinIterates(r.right)) _
            .IteratesALL _
            .GroupBy(Function(a) a.molecule_id) _
            .Keys
        Dim metadata As WebJSON.Molecule() = compounds_id _
            .Select(Function(id) registry.GetMoleculeDataById(id)) _
            .Where(Function(c) Not c Is Nothing) _
            .ToArray

        Return New MetabolismStructure With {
            .compounds = metadata _
                .Select(Function(c)
                            Dim biocyc_id As WebJSON.DBXref() = c.db_xrefs _
                                .SafeQuery _
                                .Where(Function(r) r.dbname = "MetaCyc") _
                                .ToArray

                            Return New Compound With {
                                .formula = c.formula,
                                .ID = FormatCompoundId(c.id),
                                .name = c.name,
                                .referenceIds = biocyc_id _
                                    .Select(Function(xi) xi.xref_id) _
                                    .ToArray
                            }
                        End Function) _
                .ToArray,
            .reactions = New ReactionGroup With {
                .enzymatic = network.Values _
                    .Select(Function(a)
                                Return New Reaction With {
                                    .ID = a.guid,
                                    .ec_number = ec_numbers(a.guid).Distinct.ToArray,
                                    .bounds = {5, 5},
                                    .is_enzymatic = True,
                                    .name = a.name,
                                    .note = a.reaction,
                                    .substrate = a.left _
                                        .Select(Function(c)
                                                    Return New CompoundFactor With {
                                                        .factor = c.factor,
                                                        .compound = FormatCompoundId(c.molecule_id)
                                                    }
                                                End Function) _
                                        .ToArray,
                                    .product = a.right _
                                        .Select(Function(c)
                                                    Return New CompoundFactor With {
                                                        .factor = c.factor,
                                                        .compound = FormatCompoundId(c.molecule_id)
                                                    }
                                                End Function) _
                                        .ToArray
                                }
                            End Function) _
                    .ToArray
            },
            .enzymes = enzymeModels.ToArray
        }
    End Function

    Private Iterator Function GeneObjects(rnas As List(Of RNA)) As IEnumerable(Of gene)
        Dim nt As Dictionary(Of String, String) = proj.genes
        Dim RNA As RNA

        For Each gene As GeneTable In proj.gene_table
            Dim nt_seq As String = nt(gene.locus_id)
            Dim bases As NumericVector = RNAComposition.FromNtSequence(nt_seq, gene.locus_id & "_rna").CreateVector
            Dim residues As NumericVector = Nothing
            Dim gene_type As RNATypes

            If Not gene.Translation.StringEmpty Then
                residues = ProteinComposition.FromRefSeq(gene.Translation, If(gene.ProteinId, gene.locus_id & "_protein")).CreateVector
                gene_type = RNATypes.mRNA
            Else
                Select Case gene.type
                    Case "CDS"
                        Dim trans As String = TranslationTable.Translate(nt_seq)

                        residues = ProteinComposition.FromRefSeq(trans, If(gene.ProteinId, gene.locus_id & "_protein")).CreateVector
                        gene_type = RNATypes.mRNA
                    Case "rRNA"
                        Dim rRNA = gene.commonName.Split(" "c, "-"c).First.ToLower

                        gene_type = RNATypes.ribosomalRNA
                        RNA = New RNA With {
                            .gene = gene.locus_id,
                            .id = gene.commonName,
                            .note = gene.commonName,
                            .type = gene_type,
                            .val = rRNA
                        }
                        rnas.Add(RNA)
                    Case "tRNA"
                        gene_type = RNATypes.tRNA
                        RNA = New RNA With {
                            .gene = gene.locus_id,
                            .id = gene.commonName,
                            .note = gene.commonName,
                            .type = gene_type,
                            .val = gene.commonName.Split("-"c).Last
                        }
                        rnas.Add(RNA)
                    Case Else
                        gene_type = RNATypes.micsRNA
                End Select
            End If

            Yield New gene With {
                .locus_tag = gene.locus_id,
                .left = gene.left,
                .right = gene.right,
                .strand = gene.strand,
                .product = {gene.function},
                .type = gene_type,
                .amino_acid = residues,
                .nucleotide_base = bases,
                .protein_id = If(residues Is Nothing, Nothing, {residues.name})
            }
        Next
    End Function

    Private Function BuildGenome() As Genome
        Dim RNAs As New List(Of RNA)
        Dim geneSet = GeneObjects(RNAs).GroupBy(Function(a) a.locus_tag).ToDictionary(Function(a) a.Key, Function(a) a.First)
        Dim operons As List(Of TranscriptUnit) = proj.operons _
            .SafeQuery _
            .Select(Function(op)
                        Return New TranscriptUnit With {
                            .id = op.OperonID,
                            .name = op.name,
                            .note = op.Type.Description,
                            .genes = op.Genes _
                                .Select(Function(gene_id)
                                            Return geneSet(gene_id)
                                        End Function) _
                                .ToArray
                        }
                    End Function) _
            .AsList
        Dim operon_genes As Index(Of String) = operons _
            .Select(Function(op) op.genes) _
            .IteratesALL _
            .Select(Function(gene) gene.locus_tag) _
            .Indexing

        For Each gene As gene In geneSet.Values
            If Not gene.locus_tag Like operon_genes Then
                Call operons.Add(New TranscriptUnit(gene))
            End If
        Next

        Dim genomics As New replicon With {
            .genomeName = proj.taxonomy.scientificName,
            .isPlasmid = False,
            .operons = operons.ToArray,
            .RNAs = RNAs.ToArray
        }

        Return New Genome With {
            .replicons = {genomics}
        }
    End Function

    Private Function FormatCompoundId(id As UInteger) As String
        Return "BioCAD" & id.ToString.PadLeft(11, "0"c)
    End Function
End Class
