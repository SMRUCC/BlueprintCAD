Imports System.Drawing

Public Structure GA_AutoLayout

    Dim anchors As (a As Point, b As Point)()
    Dim blocks As Block()
    Dim size As SizeF

    Public Function DoAutoLayout(Optional runs% = 5000) As Routes

    End Function

    Public Shared Operator *(layout As GA_AutoLayout, scale#) As GA_AutoLayout

    End Operator
End Structure
