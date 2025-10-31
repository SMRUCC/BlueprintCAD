Imports SMRUCC.genomics.Assembly.NCBI.GenBank
Imports SMRUCC.genomics.Assembly.NCBI.GenBank.GBFF.Keywords.FEATURES
Imports SMRUCC.genomics.ComponentModel.Annotation
Imports SMRUCC.genomics.ContextModel.Promoter
Imports SMRUCC.genomics.SequenceModel.FASTA

Public Module ProjectCreator

    Public Function FromGenBank(genbank As GBFF.File) As GenBankProject
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
        Dim genes As GeneTable() = genbank.EnumerateGeneFeatures.ExportTable().ToArray
        Dim nt As New FastaSeq({"nt"}, genbank.Origin.SequenceData)
        Dim tss As Dictionary(Of String, String) = genes _
            .Select(Function(gene)
                        Return (gene.locus_id, gene.GetUpstreamSeq(nt, 150))
                    End Function) _
            .GroupBy(Function(a) a.locus_id) _
            .ToDictionary(Function(a) a.Key,
                          Function(a)
                              Return a.First.Item2.SequenceData
                          End Function)
        Dim proj As New GenBankProject With {
            .taxonomy = genbank.Source.GetTaxonomy,
            .nt = nt.SequenceData,
            .genes = nucl,
            .proteins = prot,
            .gene_table = genes,
            .tss_upstream = tss
        }

        Return proj
    End Function
End Module
