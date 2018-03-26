Imports System.Drawing
Imports Microsoft.VisualBasic.Imaging.Math2D

''' <summary>
''' The routes blocker
''' </summary>
Public MustInherit Class Block

    ''' <summary>
    ''' The center location of this block object.
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride ReadOnly Property Location As PointF

    Public MustOverride Function Intersect(a As PointF, b As PointF) As Boolean

End Class

Public Class rect : Inherits Block

    Public ReadOnly rectangle As RectangleF

    Public Overrides ReadOnly Property Location As PointF
        Get
            Return rectangle.Centre
        End Get
    End Property

    Public Overrides Function Intersect(a As PointF, b As PointF) As Boolean
        '  Return rectangle.inter
    End Function
End Class

Public Class Circle : Inherits Block

    Public Property center As PointF
    Public Property radius As Single

    Public Overrides ReadOnly Property Location As PointF
        Get
            Return center
        End Get
    End Property

    Public Overrides Function Intersect(a As PointF, b As PointF) As Boolean
        Throw New NotImplementedException()
    End Function
End Class