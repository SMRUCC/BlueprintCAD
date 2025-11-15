Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.Interops.NCBI.Extensions.LocalBLAST.Application.BBH
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models

Public Module Annotation

    <Extension>
    Public Function AssignECNumber(enzymeHits As HitCollection) As ECNumberAnnotation
        Dim enzymes = enzymeHits.AsEnumerable.GroupBy(Function(a) a.hitName.Split("|"c).First).ToArray
        Dim enzyme_scores As ECNumberAnnotation() = enzymes _
            .Select(Function(a)
                        Dim ec_number = a.Key
                        Dim total As Double = Aggregate hit As Hit In a Into Sum(hit.score * hit.identities * hit.positive)
                        Dim source_id As String() = a.Select(Function(prot) prot.hitName.GetTagValue("|"c).Value).Distinct.ToArray

                        Return New ECNumberAnnotation With {
                            .EC = ec_number,
                            .Score = total,
                            .SourceIDs = source_id,
                            .gene_id = enzymeHits.QueryName
                        }
                    End Function) _
            .ToArray

        If enzyme_scores.Length = 0 Then
            Return Nothing
        ElseIf enzyme_scores.Length = 1 Then
            Return enzyme_scores(0)
        Else
            Return enzyme_scores _
                .OrderByDescending(Function(a) a.Score) _
                .First
        End If
    End Function

    <Extension>
    Public Function AssignTFFamilyHit(tfhits As HitCollection) As BestHit

    End Function

End Module

Public Class ECNumberAnnotation

    Public Property gene_id As String
    Public Property EC As String
    Public Property Score As Double
    Public Property SourceIDs As String()

End Class