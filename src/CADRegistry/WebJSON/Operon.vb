﻿Imports Microsoft.VisualBasic.MIME.application.json

Namespace WebJSON

    Public Class Operon

        Public Property cluster_id As String
        Public Property members As String()

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

    End Class
End Namespace