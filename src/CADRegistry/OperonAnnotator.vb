Imports Microsoft.VisualBasic.Language
Imports SMRUCC.genomics.ComponentModel.Annotation
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.BLASTOutput.BlastPlus
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Pipeline
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models

Public Module OperonAnnotator

    Public Iterator Function ParseBlastn(blastn As String) As IEnumerable(Of HitCollection)
        For Each hits As HitCollection In BlastnOutputReader.RunParser(blastn).ExportHistResult
            If Not hits.hits.IsNullOrEmpty Then
                For Each hit As Hit In hits.hits
                    With hit.hitName.GetTagValue("|")
                        hit.tag = .Name
                        hit.hitName = .Value
                    End With
                Next
            End If

            Yield hits
        Next
    End Function

    ''' <summary>
    ''' 主函数：基于BLASTN结果注释基因组中的Operon。
    ''' </summary>
    ''' <param name="allGenes">目标基因组中的所有基因。</param>
    ''' <param name="allBlastResults">所有基因的BLASTN比对结果。</param>
    ''' <returns>一个包含所有已注释Operon的列表。</returns>
    Public Function AnnotateOperons(allGenes As GeneTable(), allBlastResults As HitCollection()) As AnnotatedOperon()
        ' 1. 预处理：创建一个快速查找BLAST结果的字典
        Dim blastLookup As New Dictionary(Of String, HitCollection)()
        For Each hc In allBlastResults
            If Not blastLookup.ContainsKey(hc.QueryName) Then
                blastLookup.Add(hc.QueryName, hc)
            End If
        Next

        ' 2. 按链分组并排序基因
        Dim forwardStrandGenes = allGenes.Where(Function(g) g.strand.Equals("+", StringComparison.OrdinalIgnoreCase)).OrderBy(Function(g) g.left).ToArray
        Dim reverseStrandGenes = allGenes.Where(Function(g) g.strand.Equals("-", StringComparison.OrdinalIgnoreCase)).OrderBy(Function(g) g.left).ToArray

        ' 3. 分别处理每条链上的基因
        Dim finalAnnotations As New List(Of AnnotatedOperon)()
        finalAnnotations.AddRange(ProcessGeneStrand(forwardStrandGenes, "+", blastLookup))
        finalAnnotations.AddRange(ProcessGeneStrand(reverseStrandGenes, "-", blastLookup))

        ' 4. 对最终结果进行排序，方便查看
        Return finalAnnotations _
            .OrderBy(Function(o) o.Strand) _
            .ThenBy(Function(o) o.LeftmostPosition) _
            .ToArray
    End Function

    ''' <summary>
    ''' 处理单条链上的基因，识别Operon。
    ''' </summary>
    Private Iterator Function ProcessGeneStrand(sortedGenes As GeneTable(), strandName As String, blastLookup As Dictionary(Of String, HitCollection)) As IEnumerable(Of AnnotatedOperon)
        Dim i As Integer = 0

        While i < sortedGenes.Length
            Dim currentGene = sortedGenes(i)

            ' 获取当前基因的最佳比对Hit
            Dim bestHit As Hit = GetBestHitForGene(currentGene.locus_id, blastLookup)

            ' 如果没有比对结果，则跳过此基因，检查下一个
            If bestHit Is Nothing Then
                i += 1
                Continue While
            End If

            ' 如果有比对结果，开始一个潜在的Operon链
            Dim currentOperonTag As String = bestHit.tag
            Dim clusterGenes As New List(Of String) From {currentGene.locus_id}
            Dim j As Integer = i + 1

            ' 向后查找所有连续的、比对到同一个Operon的基因
            While j < sortedGenes.Length
                Dim nextGene = sortedGenes(j)
                Dim nextBestHit As Hit = GetBestHitForGene(nextGene.locus_id, blastLookup)

                ' 如果下一个基因的最佳比对也属于同一个Operon，则将其加入链
                If nextBestHit IsNot Nothing AndAlso nextBestHit.tag = currentOperonTag Then
                    clusterGenes.Add(nextGene.locus_id)
                    j += 1
                Else
                    ' 链中断，退出循环
                    Exit While
                End If
            End While

            ' 将找到的连续基因链记录为一个Operon
            ' 即使只有一个基因，也认为是一个（残缺的）Operon
            Dim newOperon As New AnnotatedOperon With {
                .OperonTag = currentOperonTag,
                .GeneIds = clusterGenes.ToArray,
                .Strand = strandName,
                .LeftmostPosition = sortedGenes(i).left
            }

            Yield newOperon

            ' 将主索引i移动到已处理链的末尾，继续搜索
            i = j
        End While
    End Function

    ''' <summary>
    ''' 辅助函数：根据基因ID查找其最佳比对Hit。
    ''' 最佳定义为evalue最小，如果evalue相同则score最高。
    ''' </summary>
    Private Function GetBestHitForGene(locusId As String, blastLookup As Dictionary(Of String, HitCollection)) As Hit
        If Not blastLookup.ContainsKey(locusId) Then
            Return Nothing
        End If

        Dim hc As HitCollection = blastLookup(locusId)
        If hc.hits Is Nothing OrElse hc.hits.Length = 0 Then
            Return Nothing
        End If

        ' 按evalue升序，score降序排序，取第一个
        Dim bestHit = hc.hits.OrderBy(Function(h) h.evalue).ThenByDescending(Function(h) h.score).FirstOrDefault()
        Return bestHit
    End Function
End Module