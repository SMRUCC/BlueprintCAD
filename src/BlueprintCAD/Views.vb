Imports System.Runtime.CompilerServices
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core
Imports SMRUCC.genomics.GCModeller.ModellingEngine.IO

Module Views

    <Extension>
    Public Function MakeToString(rxn As FluxEdge, symbols As Dictionary(Of String, CompoundInfo)) As String
        Dim left = rxn.left.Select(Function(v) symbols.GetNameText(v.mass_id)).JoinBy(" + ")
        Dim right = rxn.right.Select(Function(v) symbols.GetNameText(v.mass_id)).JoinBy(" + ")

        Return left & " = " & right
    End Function

    <Extension>
    Public Function GetNameText(symbols As Dictionary(Of String, CompoundInfo), id As String) As String
        Dim c As CompoundInfo = Nothing

        If symbols.TryGetValue(id, c) Then
            Return c.name
        Else
            Return id
        End If
    End Function
End Module
