Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2

Namespace UIData

    Public Class ReactionPropertyView

        Public ReadOnly Property id As String
        Public ReadOnly Property name As String
        Public ReadOnly Property note As String
        Public ReadOnly Property is_enzymatic As Boolean
        Public ReadOnly Property ec_number As String()
        Public ReadOnly Property compartment As String()
        Public Property substrate As String()
        Public Property product As String()

        Sub New()
        End Sub

        Sub New(rxn As Reaction, list As Dictionary(Of String, Compound))
            id = rxn.ID
            name = rxn.name
            note = rxn.note
            is_enzymatic = rxn.is_enzymatic
            ec_number = rxn.ec_number
            compartment = rxn.compartment
            substrate = rxn.substrate _
                .Select(Function(c)
                            If list.ContainsKey(c.compound) Then
                                Return list(c.compound).name
                            Else
                                Return c
                            End If
                        End Function) _
                .ToArray
            product = rxn.product _
                .Select(Function(c)
                            If list.ContainsKey(c.compound) Then
                                Return list(c.compound).name
                            Else
                                Return c
                            End If
                        End Function) _
                .ToArray
        End Sub

    End Class
End Namespace