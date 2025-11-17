Imports Galaxy.ExcelPad
Imports Microsoft.VisualStudio.WinForms.Docking

Public Class FormChartPad : Implements IChartPad

    Public Sub ShowPage() Implements IChartPad.ShowPage
        Me.DockState = DockState.Document
    End Sub

    Public Function GetChartPadCanvas() As ChartPad Implements IChartPad.GetChartPadCanvas
        Return PlotView1
    End Function
End Class