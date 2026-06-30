Imports System.Xml.Serialization
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Public Class CellData

    Public Property CellModelId As String

    ''' <summary>
    ''' 1. 界
    ''' </summary>
    Public Property kingdom As String

    ''' <summary>
    ''' 2. 门
    ''' </summary>
    Public Property phylum As String
    ''' <summary>
    ''' 3A. 纲
    ''' </summary>
    Public Property [class] As String
    ''' <summary>
    ''' 4B. 目
    ''' </summary>
    Public Property order As String
    ''' <summary>
    ''' 5C. 科
    ''' </summary>
    Public Property family As String
    ''' <summary>
    ''' 6D. 属
    ''' </summary>
    Public Property genus As String
    ''' <summary>
    ''' 7E. 种
    ''' </summary>
    Public Property species As String

    <XmlAttribute> Public Property ncbi_taxid As UInteger

    Public Property traits As String()

    Sub New()
    End Sub

    Sub New(cell As VirtualCell)
        If Not cell Is Nothing Then
            CellModelId = cell.cellular_id
            kingdom = cell.taxonomy?.kingdom
            phylum = cell.taxonomy?.phylum
            [class] = cell.taxonomy?.class
            order = cell.taxonomy?.order
            family = cell.taxonomy?.family
            genus = cell.taxonomy?.genus
            species = cell.taxonomy?.species
            ncbi_taxid = cell.taxonomy?.ncbi_taxid
            traits = cell.traits?.phenotype
        End If
    End Sub

End Class
