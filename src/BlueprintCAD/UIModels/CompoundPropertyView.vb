Imports System.ComponentModel
Imports Galaxy.Data
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Namespace UIData

    Public Class CompoundPropertyView

        Public ReadOnly Property id As String
        Public ReadOnly Property name As String

        <TypeConverter(GetType(DefaultExpandedArray)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public ReadOnly Property referenceID As String()

        <TypeConverter(GetType(DefaultExpandedArray)), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public ReadOnly Property db_xrefs As String()

        Sub New()
        End Sub

        Sub New(compound As Compound)
            id = compound.ID
            name = compound.name
            referenceID = compound.referenceIds
            db_xrefs = compound.db_xrefs
        End Sub

    End Class
End Namespace