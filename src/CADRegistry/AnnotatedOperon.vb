Public Class AnnotatedOperon
    ''' <summary>
    ''' 被注释的Operon的Tag (来自Hit.tag)
    ''' </summary>
    Public Property OperonTag As String

    ''' <summary>
    ''' 组成此Operon的基因的locus_id列表
    ''' </summary>
    Public Property GeneIds As List(Of String)

    ''' <summary>
    ''' Operon所在的链
    ''' </summary>
    Public Property Strand As String

    ''' <summary>
    ''' Operon在基因组上的最左侧位置，用于排序
    ''' </summary>
    Public Property LeftmostPosition As Integer

    ''' <summary>
    ''' 根据算法，任何连续的比对链都是一个有效的注释。
    ''' 此处可以标记为True，或者根据更复杂的规则（如比对成员数量）来判断。
    ''' </summary>
    Public Property IsConserved As Boolean = True
End Class