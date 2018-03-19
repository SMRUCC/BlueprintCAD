Imports System.Drawing
Imports Microsoft.VisualBasic.Imaging.Math2D

''' <summary>
''' The routes blocker
''' </summary>
Public MustInherit Class Block

    Public MustOverride ReadOnly Property Location As PointF

End Class

Public Class rect : Inherits Block

    Public ReadOnly rectangle As RectangleF

    Public Overrides ReadOnly Property Location As PointF
        Get
            Return rectangle.Centre
        End Get
    End Property

End Class

Public Class Circle : Inherits Block

    Public Property center As PointF
    Public Property radius As Single

    Public Overrides ReadOnly Property Location As PointF
        Get
            Return center
        End Get
    End Property

End Class