Imports CADRegistry
Imports Microsoft.VisualBasic.CommandLine
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.CompilerServices

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
            .properties = New [Property]
        }

        Return 0
    End Function

    Protected Overrides Function CompileImpl(args As CommandLine) As Integer
        Dim enzymes As Dictionary(Of String, ECNumberAnnotation) = proj.ec_numbers
        Dim network As New List(Of WebJSON.Reaction)

        For Each enzyme As ECNumberAnnotation In enzymes.Values
            Call network.AddRange(registry.GetAssociatedReactions(enzyme.EC, simple:=False).Values)
        Next


    End Function
End Class
