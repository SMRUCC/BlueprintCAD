Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
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
                        hit.tag = .Value.Split("|"c).First
                        hit.hitName = .Name
                    End With
                Next
            End If

            Yield hits
        Next
    End Function

    ' --- 2. 核心逻辑模块 ---

    ''' <summary>
    ''' 基于BLASTN比对结果和已知Operon信息，对基因组进行Operon注释。
    ''' </summary>
    ''' <param name="allGenes">基因组中所有基因的数组。</param>
    ''' <param name="blastResults">所有基因的BLASTN比对结果数组。</param>
    ''' <param name="knownOperonsDict">包含所有已知Operon信息的字典。</param>
    ''' <returns>一个包含所有注释到的Operon的列表。</returns>
    Public Iterator Function AnnotateOperons(
    allGenes As GeneTable(),
    blastResults As HitCollection(),
    knownOperonsDict As Dictionary(Of String, WebJSON.Operon)
) As IEnumerable(Of AnnotatedOperon)

        ' --- 步骤 1: 为每个基因投票，确定其最可能的Operon ID ---

        ' 创建一个从基因locus_id到其注释的OperonID的映射
        Dim geneToOperonMap As New Dictionary(Of String, String)

        ' 将blastResults转换为字典，方便通过QueryName查找
        Dim blastDict As Dictionary(Of String, HitCollection) = blastResults.ToDictionary(Function(hc) hc.QueryName)

        For Each gene As GeneTable In allGenes
            If blastDict.ContainsKey(gene.locus_id) Then
                Dim hc As HitCollection = blastDict(gene.locus_id)
                If hc.hits IsNot Nothing AndAlso hc.hits.Length > 0 Then
                    ' 按tag分组并计算总分
                    Dim operonScores = hc.hits.GroupBy(Function(h) h.tag) _
                                          .ToDictionary(
                                              Function(g) g.Key,
                                              Function(g) g.Sum(Function(h) h.score * h.identities * (1 - h.gaps))
                                          )

                    ' 找到得分最高的Operon ID
                    If operonScores.Any() Then
                        Dim bestOperonId = operonScores.OrderByDescending(Function(kvp) kvp.Value).First().Key
                        geneToOperonMap(gene.locus_id) = bestOperonId
                    End If
                End If
            End If
        Next

        ' --- 步骤 2: 在基因组上寻找连续的、具有相同Operon ID的基因区块 ---
        ' 按链方向和位置对基因进行排序，这是识别相邻基因的关键
        Dim sortedGenes = allGenes.OrderBy(Function(g) g.strand) _
                             .ThenBy(Function(g) If(g.strand = "+", g.left, g.right)) _
                             .ToList()

        Dim i As Integer = 0
        While i < sortedGenes.Count
            Dim currentGene = sortedGenes(i)

            ' 检查当前基因是否被注释到了某个Operon
            If geneToOperonMap.ContainsKey(currentGene.locus_id) Then
                Dim currentOperonId = geneToOperonMap(currentGene.locus_id)
                Dim operonBlock As New List(Of GeneTable) From {currentGene}

                ' 向后查找连续的、具有相同Operon ID的基因
                i += 1
                While i < sortedGenes.Count AndAlso geneToOperonMap.ContainsKey(sortedGenes(i).locus_id) _
                  AndAlso geneToOperonMap(sortedGenes(i).locus_id) = currentOperonId
                    operonBlock.Add(sortedGenes(i))
                    i += 1
                End While

                ' --- 步骤 3: 对找到的基因区块进行分类（保守、插入、缺失） ---
                If knownOperonsDict.ContainsKey(currentOperonId) Then
                    Yield ClassifyOperonBlock(operonBlock, currentOperonId, knownOperonsDict(currentOperonId), blastDict)
                End If
            Else
                ' 当前基因不属于任何Operon，继续处理下一个
                i += 1
            End If
        End While
    End Function

    ''' <summary>
    ''' 辅助函数，用于对一个连续的基因区块进行Operon类型分类。
    ''' </summary>
    Private Function ClassifyOperonBlock(block As List(Of GeneTable), operonId As String, knownOperon As WebJSON.Operon, blastDict As Dictionary(Of String, HitCollection)) As AnnotatedOperon
        ' 1. 准备基础数据集
        ' 目标区块中所有基因的locus_id
        Dim blockLocusIds = block.Select(Function(g) g.locus_id).ToHashSet()
        ' 参考Operon中所有成员基因的ID (hitName)
        Dim knownHitNames = knownOperon.members.ToHashSet()

        ' 2. 找出目标区块中，实际匹配到参考Operon成员的基因 (hitName)
        ' 使用SelectMany来“扁平化”查询，即对每个基因的每个hit都进行检查
        Dim matchedHitNames = blockLocusIds _
        .SelectMany(Function(locusId)
                        ' 如果blastDict中没有这个基因的记录，返回一个空的Hit集合
                        If Not blastDict.ContainsKey(locusId) Then Return {}
                        ' 否则，返回该基因的所有Hit
                        Return blastDict(locusId).hits
                    End Function) _
        .Where(Function(hit) hit.tag = operonId AndAlso knownHitNames.Contains(hit.hitName)) _
        .Select(Function(hit) hit.hitName) _
        .ToHashSet()

        ' 3. 识别缺失的基因
        ' 缺失的基因 = 参考Operon中的所有成员 - 实际被匹配到的成员
        Dim missingGeneIds = knownHitNames.Except(matchedHitNames).ToList()

        ' 4. 识别插入的基因
        ' 插入的基因 = 区块中的基因 - 其BLAST结果能匹配到当前Operon的基因
        ' 一个基因是插入的，如果它没有任何BLAST结果，或者它的所有BLAST结果都指向了别的Operon
        Dim insertedLocusIds = blockLocusIds _
        .Where(Function(locusId)
                   ' 条件1: 基因没有BLAST结果
                   If Not blastDict.ContainsKey(locusId) Then Return True
                   ' 条件2: 基因有BLAST结果，但没有任何一个hit的tag是当前Operon的ID
                   Return Not blastDict(locusId).hits.Any(Function(hit) hit.tag = operonId)
               End Function) _
        .ToList()

        ' 5. 根据插入和缺失情况确定Operon类型
        Dim opType As OperonType
        If insertedLocusIds.Any() Then
            ' 只要有插入，就优先标记为插入突变
            opType = OperonType.Insertion
        ElseIf missingGeneIds.Any() Then
            ' 没有插入但有缺失，标记为缺失突变
            opType = OperonType.Deletion
        Else
            ' 两者都没有，是保守的
            opType = OperonType.Conserved
        End If

        ' 6. 构建并返回最终的注释结果
        Return New AnnotatedOperon With {
        .OperonID = operonId,
        .Type = opType,
        .Genes = block.Select(Function(gene) gene.locus_id).ToArray, ' 保留完整的GeneTable对象列表
        .KnownGeneIds = knownHitNames.ToArray,
        .InsertedGeneIds = insertedLocusIds.ToArray, ' 插入的是目标基因组的locus_id
        .MissingGeneIds = missingGeneIds.ToArray    ' 缺失的是参考数据库的hitName
    }
    End Function
End Module