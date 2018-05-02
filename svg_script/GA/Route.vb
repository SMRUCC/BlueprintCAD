Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports GCModeller.CAD.GA.Models
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.GAF.Helper
Imports Microsoft.VisualBasic.MachineLearning.Darwinism.Models
Imports Microsoft.VisualBasic.Math
Imports Microsoft.VisualBasic.Math.LinearAlgebra
Imports Microsoft.VisualBasic.Serialization.JSON

Namespace GA

    ''' <summary>
    ''' 网格之中的A到B点的连接路径集合，这个相当于一个染色体，即一个可能的最优解决方案
    ''' </summary>
    Public Class Routes : Implements Chromosome(Of Routes)

        ''' <summary>
        ''' 节点之间的连接使用-1来进行分割，在这里规定坐标点必须要大于等于零
        ''' 例如第一个被-1分割的块为第一对连接点之间的连接路线
        ''' </summary>
        Public ReadOnly Path As Path()
        Public ReadOnly Size As Size

        ReadOnly random As New Random(Now.Millisecond)
        ReadOnly minSize%

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="anchors"></param>
        ''' <param name="size"></param>
        ''' <param name="maxStackSize%"></param>
        ''' <param name="minSize%">一条路径最短需要有多少个节点构成</param>
        Sub New(anchors As (a As Point, b As Point)(), size As Size, Optional maxStackSize% = 10, Optional minSize% = 3)
            Me.Path = anchors _
                .Select(Function(a)
                            Return a.Allocate(minSize, random)
                        End Function) _
                .ToArray
            Me.Size = size
            Me.minSize = minSize
        End Sub

        Sub New(path As IEnumerable(Of Path), size As Size, minSize%)
            Me.Path = path.ToArray
            Me.Size = size
            Me.minSize = minSize
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function Copy() As Routes
            Return New Routes(Path.Select(Function(p) p.Copy), Size, minSize)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="another"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 在交叉的时候要特别注意不可以将首尾元素的锚点给修改了
        ''' </remarks>
        Public Function Crossover(another As Routes) As IEnumerable(Of Routes) Implements Chromosome(Of Routes).Crossover
            ' 目前不进行杂交
            Return {Copy(), another.Copy}
        End Function

        ''' <summary>
        ''' 可能发生的情况：
        ''' 
        ''' + 节点的坐标发生变化
        ''' + 增加一个节点坐标
        ''' + 减少一个节点坐标
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' 在突变的时候要特别注意不可以将首尾的锚点给修改了
        ''' </remarks>
        Public Function Mutate() As Routes Implements Chromosome(Of Routes).Mutate
            Dim i% = random.Next(Path.Length)
            Dim pathCopy = Path.Select(Function(p) p.Copy).ToArray
            pathCopy(i) = pathCopy(i).MovePoint(random)
            Return New Routes(pathCopy, Size, minSize)
        End Function

        Public Overrides Function ToString() As String
            Dim shrinks = Path.Select(Function(p) p.Shrink).IteratesALL.ToArray
            Dim X = shrinks.X.Select(Function(n) CInt(n)).AsList
            Dim Y = shrinks.Y.Select(Function(n) CInt(n)).ToArray

            Return (X + Y).GetJson
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Narrowing Operator CType(routes As Routes) As String
            Return routes.ToString
        End Operator
    End Class

End Namespace

