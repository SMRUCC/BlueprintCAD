Imports Galaxy.Workbench
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Definitions

Public Class FormKnockoutGenerator : Implements IDataContainer

    ''' <summary>
    ''' current selected cell model
    ''' </summary>
    Dim cell As VirtualCell

    Dim wizardConfig As New Wizard

    Public Sub SetData(data As Object) Implements IDataContainer.SetData
        wizardConfig = DirectCast(data, Wizard)
        LoadModelFiles()
    End Sub

    Public Function GetData() As Object Implements IDataContainer.GetData
        Return wizardConfig
    End Function

    Private Function LoadModelFiles()
        Dim cell As VirtualCell
        Dim compounds_id As New List(Of String)
        Dim copy As New Dictionary(Of String, Integer)

        For Each file As ModelFile In wizardConfig.models.Values
            cell = file.model
            compounds_id.AddRange(cell.metabolismStructure.compounds.Keys)
            copy.Add(cell.cellular_id, 1000)
            ListBox1.Items.Add(cell)
        Next

        If ListBox1.Items.Count > 0 Then
            ListBox1.SelectedIndex = 0
        End If

        wizardConfig.config.mapping = Definition.MetaCyc(compounds_id.Distinct, Double.NaN)
        wizardConfig.config.copy_number = copy

        Return Me
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MessageBox.Show("Whether to create an independent mutant virtual cell model or directly perform gene knockout experimental settings on the original model. Creating an independent mutant virtual cell model will allow simultaneous analysis of both the wild-type and the mutant model after gene knockout in the experiment. Whereas directly performing gene knockout experimental settings on the original virtual cell model will directly remove the expression associations of the knocked-out gene from the original model, enabling computational analysis of the mutant. 

Selecting 【Yes】 will cause the program to create an independent mutant virtual cell model; 
selecting 【No】 will perform the knockout experiment on the original model.",
                           "Virtual Cell Knockout Model",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Information) = DialogResult.Yes Then

            ' create mutant for each gene knockout
            Dim newModels As Dictionary(Of String, ModelFile) = wizardConfig.models

            For i As Integer = 0 To ListBox4.Items.Count - 1
                Dim knockout = DirectCast(ListBox4.Items(i), KnockoutGene)
                Dim modelFile As ModelFile = wizardConfig.models(knockout.genome)
                Dim model As VirtualCell = modelFile.model
                Dim knockout_id As String = $"{model.cellular_id}-{knockout.gene.locus_tag}"
                Dim saveModel As String = $"{modelFile.filepath.ParentPath}/knockouts/{knockout_id}.xml"

                model = model.DeleteMutation({knockout.gene.locus_tag})

                Call model.GetXml.SaveTo(saveModel)
                Call newModels.Add(knockout_id, New ModelFile With {
                    .model = model,
                    .filepath = saveModel
                })
            Next

            wizardConfig.config.models = newModels.Values _
                .Select(Function(c) c.filepath) _
                .ToArray
        Else
            Dim knockouts As New List(Of String)

            For i As Integer = 0 To ListBox4.Items.Count - 1
                Call knockouts.Add(DirectCast(ListBox4.Items(i), KnockoutGene).gene.locus_tag)
            Next

            wizardConfig.config.knockouts = knockouts.Distinct.ToArray
        End If

        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex = -1 Then
            Return
        End If

        cell = ListBox1.SelectedItem

        ListBox2.Items.Clear()

        If cell.genome IsNot Nothing AndAlso Not cell.genome.replicons.IsNullOrEmpty Then
            For Each rep As replicon In cell.genome.replicons
                For Each gene As gene In rep.GetGeneList
                    Call ListBox2.Items.Add(gene)
                Next
            Next
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        Dim gene As New KnockoutGene With {
            .genome = cell.cellular_id,
            .gene = ListBox2.SelectedItem
        }

        Call ViewMetabolicNetwork(gene)
    End Sub

    Private Sub ListBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox4.SelectedIndexChanged
        If ListBox4.SelectedIndex < 0 Then
            Return
        End If

        Dim gene As KnockoutGene = ListBox4.SelectedItem

        Call ViewMetabolicNetwork(gene)
    End Sub

    Private Sub ViewMetabolicNetwork(gene As KnockoutGene)
        Dim cell As VirtualCell = wizardConfig.models(gene.genome).model
        Dim proteins As String() = gene.gene.protein_id
        Dim visited As New Index(Of String)
        Dim proteinList As protein() = proteins _
            .SafeQuery _
            .Select(Function(id)
                        Return protein.ProteinRoutine(cell.genome.proteins, id, visited)
                    End Function) _
            .IteratesALL _
            .Distinct _
            .ToArray

        Call ListBox3.Items.Clear()

        For Each impact As Reaction In proteinList _
            .Select(Function(prot)
                        Return cell.metabolismStructure.GetImpactedMetabolicNetwork(prot.protein_id)
                    End Function) _
            .IteratesALL _
            .Distinct

            Call ListBox3.Items.Add(impact)
        Next
    End Sub

    Private Sub AddToKnockoutListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToKnockoutListToolStripMenuItem.Click
        If ListBox2.SelectedIndex < 0 Then
            Return
        End If

        Dim gene As New KnockoutGene With {
            .genome = cell.cellular_id,
            .gene = ListBox2.SelectedItem
        }

        Call ListBox4.Items.Add(gene)
    End Sub

    Private Sub FormKnockoutGenerator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Rtf = "{\rtf1\adeflang1025\ansi\ansicpg936\uc1\adeff0\deff0\stshfdbch31505\stshfloch31506\stshfhich31506\stshfbi0\deflang1033\deflangfe2052\themelang1033\themelangfe2052\themelangcs0{\fonttbl{\f0\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}
{\f13\fbidi \fnil\fcharset134\fprq2{\*\panose 02010600030101010101}\'cb\'ce\'cc\'e5;}{\f34\fbidi \froman\fcharset0\fprq2{\*\panose 02040503050406030204}Cambria Math;}
{\f36\fbidi \fnil\fcharset134\fprq2{\*\panose 02010600030101010101}\'b5\'c8\'cf\'df{\*\falt DengXian};}{\f44\fbidi \froman\fcharset0\fprq2{\*\panose 00000000000000000000}Cambria;}
{\f45\fbidi \fnil\fcharset134\fprq2{\*\panose 00000000000000000000}@\'cb\'ce\'cc\'e5{\*\falt @SimSun};}{\f46\fbidi \fnil\fcharset134\fprq2{\*\panose 00000000000000000000}@\'b5\'c8\'cf\'df{\*\falt @DengXian};}
{\flomajor\f31500\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\fdbmajor\f31501\fbidi \fnil\fcharset134\fprq2{\*\panose 02010600030101010101}\'b5\'c8\'cf\'df Light{\*\falt DengXian Light};}
{\fhimajor\f31502\fbidi \fnil\fcharset134\fprq2{\*\panose 02010600030101010101}\'b5\'c8\'cf\'df Light{\*\falt DengXian Light};}{\fbimajor\f31503\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}
{\flominor\f31504\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\fdbminor\f31505\fbidi \fnil\fcharset134\fprq2{\*\panose 02010600030101010101}\'b5\'c8\'cf\'df{\*\falt DengXian};}
{\fhiminor\f31506\fbidi \fnil\fcharset134\fprq2{\*\panose 02010600030101010101}\'b5\'c8\'cf\'df{\*\falt DengXian};}{\fbiminor\f31507\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}
{\f57\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}{\f58\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr;}{\f60\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}{\f61\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}
{\f62\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\f63\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}{\f64\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}
{\f65\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}{\f189\fbidi \fnil\fcharset0\fprq2 \'cb\'ce\'cc\'e5 Western;}{\f397\fbidi \froman\fcharset238\fprq2 Cambria Math CE;}{\f398\fbidi \froman\fcharset204\fprq2 Cambria Math Cyr;}
{\f400\fbidi \froman\fcharset161\fprq2 Cambria Math Greek;}{\f401\fbidi \froman\fcharset162\fprq2 Cambria Math Tur;}{\f404\fbidi \froman\fcharset186\fprq2 Cambria Math Baltic;}{\f405\fbidi \froman\fcharset163\fprq2 Cambria Math (Vietnamese);}
{\f419\fbidi \fnil\fcharset0\fprq2 \'b5\'c8\'cf\'df Western{\*\falt DengXian};}{\f417\fbidi \fnil\fcharset238\fprq2 \'b5\'c8\'cf\'df CE{\*\falt DengXian};}{\f418\fbidi \fnil\fcharset204\fprq2 \'b5\'c8\'cf\'df Cyr{\*\falt DengXian};}
{\f420\fbidi \fnil\fcharset161\fprq2 \'b5\'c8\'cf\'df Greek{\*\falt DengXian};}{\f497\fbidi \froman\fcharset238\fprq2 Cambria CE;}{\f498\fbidi \froman\fcharset204\fprq2 Cambria Cyr;}{\f500\fbidi \froman\fcharset161\fprq2 Cambria Greek;}
{\f501\fbidi \froman\fcharset162\fprq2 Cambria Tur;}{\f504\fbidi \froman\fcharset186\fprq2 Cambria Baltic;}{\f505\fbidi \froman\fcharset163\fprq2 Cambria (Vietnamese);}{\f509\fbidi \fnil\fcharset0\fprq2 @\'cb\'ce\'cc\'e5 Western{\*\falt @SimSun};}
{\f519\fbidi \fnil\fcharset0\fprq2 @\'b5\'c8\'cf\'df Western{\*\falt @DengXian};}{\f517\fbidi \fnil\fcharset238\fprq2 @\'b5\'c8\'cf\'df CE{\*\falt @DengXian};}{\f518\fbidi \fnil\fcharset204\fprq2 @\'b5\'c8\'cf\'df Cyr{\*\falt @DengXian};}
{\f520\fbidi \fnil\fcharset161\fprq2 @\'b5\'c8\'cf\'df Greek{\*\falt @DengXian};}{\flomajor\f31508\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}{\flomajor\f31509\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr;}
{\flomajor\f31511\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}{\flomajor\f31512\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}{\flomajor\f31513\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}
{\flomajor\f31514\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}{\flomajor\f31515\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}{\flomajor\f31516\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}
{\fdbmajor\f31520\fbidi \fnil\fcharset0\fprq2 \'b5\'c8\'cf\'df Light Western{\*\falt DengXian Light};}{\fdbmajor\f31518\fbidi \fnil\fcharset238\fprq2 \'b5\'c8\'cf\'df Light CE{\*\falt DengXian Light};}
{\fdbmajor\f31519\fbidi \fnil\fcharset204\fprq2 \'b5\'c8\'cf\'df Light Cyr{\*\falt DengXian Light};}{\fdbmajor\f31521\fbidi \fnil\fcharset161\fprq2 \'b5\'c8\'cf\'df Light Greek{\*\falt DengXian Light};}
{\fhimajor\f31530\fbidi \fnil\fcharset0\fprq2 \'b5\'c8\'cf\'df Light Western{\*\falt DengXian Light};}{\fhimajor\f31528\fbidi \fnil\fcharset238\fprq2 \'b5\'c8\'cf\'df Light CE{\*\falt DengXian Light};}
{\fhimajor\f31529\fbidi \fnil\fcharset204\fprq2 \'b5\'c8\'cf\'df Light Cyr{\*\falt DengXian Light};}{\fhimajor\f31531\fbidi \fnil\fcharset161\fprq2 \'b5\'c8\'cf\'df Light Greek{\*\falt DengXian Light};}
{\fbimajor\f31538\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}{\fbimajor\f31539\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr;}{\fbimajor\f31541\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}
{\fbimajor\f31542\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}{\fbimajor\f31543\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\fbimajor\f31544\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}
{\fbimajor\f31545\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}{\fbimajor\f31546\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}{\flominor\f31548\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}
{\flominor\f31549\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr;}{\flominor\f31551\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}{\flominor\f31552\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}
{\flominor\f31553\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\flominor\f31554\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}{\flominor\f31555\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}
{\flominor\f31556\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}{\fdbminor\f31560\fbidi \fnil\fcharset0\fprq2 \'b5\'c8\'cf\'df Western{\*\falt DengXian};}
{\fdbminor\f31558\fbidi \fnil\fcharset238\fprq2 \'b5\'c8\'cf\'df CE{\*\falt DengXian};}{\fdbminor\f31559\fbidi \fnil\fcharset204\fprq2 \'b5\'c8\'cf\'df Cyr{\*\falt DengXian};}
{\fdbminor\f31561\fbidi \fnil\fcharset161\fprq2 \'b5\'c8\'cf\'df Greek{\*\falt DengXian};}{\fhiminor\f31570\fbidi \fnil\fcharset0\fprq2 \'b5\'c8\'cf\'df Western{\*\falt DengXian};}
{\fhiminor\f31568\fbidi \fnil\fcharset238\fprq2 \'b5\'c8\'cf\'df CE{\*\falt DengXian};}{\fhiminor\f31569\fbidi \fnil\fcharset204\fprq2 \'b5\'c8\'cf\'df Cyr{\*\falt DengXian};}
{\fhiminor\f31571\fbidi \fnil\fcharset161\fprq2 \'b5\'c8\'cf\'df Greek{\*\falt DengXian};}{\fbiminor\f31578\fbidi \froman\fcharset238\fprq2 Times New Roman CE;}{\fbiminor\f31579\fbidi \froman\fcharset204\fprq2 Times New Roman Cyr;}
{\fbiminor\f31581\fbidi \froman\fcharset161\fprq2 Times New Roman Greek;}{\fbiminor\f31582\fbidi \froman\fcharset162\fprq2 Times New Roman Tur;}{\fbiminor\f31583\fbidi \froman\fcharset177\fprq2 Times New Roman (Hebrew);}
{\fbiminor\f31584\fbidi \froman\fcharset178\fprq2 Times New Roman (Arabic);}{\fbiminor\f31585\fbidi \froman\fcharset186\fprq2 Times New Roman Baltic;}{\fbiminor\f31586\fbidi \froman\fcharset163\fprq2 Times New Roman (Vietnamese);}}
{\colortbl;\red0\green0\blue0;\red0\green0\blue255;\red0\green255\blue255;\red0\green255\blue0;\red255\green0\blue255;\red255\green0\blue0;\red255\green255\blue0;\red255\green255\blue255;\red0\green0\blue128;\red0\green128\blue128;\red0\green128\blue0;
\red128\green0\blue128;\red128\green0\blue0;\red128\green128\blue0;\red128\green128\blue128;\red192\green192\blue192;\red0\green0\blue0;\red0\green0\blue0;\red0\green77\blue187;\ctextone\ctint242\cshade255\red13\green13\blue13;}{\*\defchp 
\fs21\kerning2\loch\af31506\hich\af31506\dbch\af31505 }{\*\defpap \ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 }\noqfpromote {\stylesheet{
\qj \li0\ri0\nowidctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af0\afs22\alang1025 \ltrch\fcs0 \fs21\lang1033\langfe2052\kerning2\loch\f31506\hich\af31506\dbch\af31505\cgrid\langnp1033\langfenp2052 
\snext0 \sqformat \spriority0 Normal;}{\*\cs10 \additive \ssemihidden \sunhideused \spriority1 Default Paragraph Font;}{\*
\ts11\tsrowd\trftsWidthB3\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\trcbpat1\trcfpat1\tblind0\tblindtype3\tsvertalt\tsbrdrt\tsbrdrl\tsbrdrb\tsbrdrr\tsbrdrdgl\tsbrdrdgr\tsbrdrh\tsbrdrv 
\ql \li0\ri0\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af0\afs21\alang1025 \ltrch\fcs0 \fs21\lang1033\langfe2052\kerning2\loch\f31506\hich\af31506\dbch\af31505\cgrid\langnp1033\langfenp2052 
\snext11 \ssemihidden \sunhideused Normal Table;}{\s15\qc \li0\ri0\nowidctlpar\brdrb\brdrs\brdrw15\brsp20 \tqc\tx4153\tqr\tx8306\wrapdefault\aspalpha\aspnum\faauto\nosnaplinegrid\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af0\afs18\alang1025 \ltrch\fcs0 
\fs18\lang1033\langfe2052\kerning2\loch\f31506\hich\af31506\dbch\af31505\cgrid\langnp1033\langfenp2052 \sbasedon0 \snext15 \slink16 \sunhideused \styrsid4216103 header;}{\*\cs16 \additive \rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \fs18 
\sbasedon10 \slink15 \slocked \styrsid4216103 Header Char;}{\s17\ql \li0\ri0\nowidctlpar\tqc\tx4153\tqr\tx8306\wrapdefault\aspalpha\aspnum\faauto\nosnaplinegrid\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af0\afs18\alang1025 \ltrch\fcs0 
\fs18\lang1033\langfe2052\kerning2\loch\f31506\hich\af31506\dbch\af31505\cgrid\langnp1033\langfenp2052 \sbasedon0 \snext17 \slink18 \sunhideused \styrsid4216103 footer;}{\*\cs18 \additive \rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \fs18 
\sbasedon10 \slink17 \slocked \styrsid4216103 Footer Char;}{\s19\ql \li0\ri0\sb100\sa100\sbauto1\saauto1\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \rtlch\fcs1 \af13\afs24\alang1025 \ltrch\fcs0 
\fs24\lang1033\langfe2052\loch\f13\hich\af13\dbch\af13\cgrid\langnp1033\langfenp2052 \sbasedon0 \snext19 \sunhideused \styrsid4216103 Normal (Web);}}{\*\listtable{\list\listtemplateid1436025094\listsimple{\listlevel\levelnfc1\levelnfcn1\leveljc0\leveljcn0
\levelfollow0\levelstartat1\levelold\levelspace0\levelindent0{\leveltext\'02\'00.;}{\levelnumbers\'01;}\rtlch\fcs1 \af0 \ltrch\fcs0 \f44\fbias0 }{\listname ;}\listid1873422489}}{\*\listoverridetable{\listoverride\listid1873422489\listoverridecount0\ls1}}
{\*\pgptbl {\pgp\ipgp0\itap0\li0\ri0\sb0\sa0}}{\*\rsidtbl \rsid2577415\rsid4216103\rsid8011912\rsid12348761}{\mmathPr\mmathFont34\mbrkBin0\mbrkBinSub0\msmallFrac0\mdispDef1\mlMargin0\mrMargin0\mdefJc1\mwrapIndent1440\mintLim0\mnaryLim1}{\info
{\operator xie guigang}{\creatim\yr2025\mo9\dy28\hr23\min5}{\revtim\yr2025\mo9\dy28\hr23\min6}{\version3}{\edmins1}{\nofpages1}{\nofwords153}{\nofchars876}{\nofcharsws1027}{\vern31}}{\*\xmlnstbl {\xmlns1 http://schemas.microsoft.com/office/word/2003/wordm
l}}\paperw12240\paperh15840\margl1800\margr1800\margt1440\margb1440\gutter0\ltrsect 
\ftnbj\aenddoc\trackmoves0\trackformatting1\donotembedsysfont0\relyonvml0\donotembedlingdata1\grfdocevents0\validatexml0\showplaceholdtext0\ignoremixedcontent0\saveinvalidxml0\showxmlerrors0\horzdoc\dghspace120\dgvspace120\dghorigin1701\dgvorigin1984
\dghshow0\dgvshow3\jcompress\viewkind1\viewscale100\rsidroot4216103 \nouicompat \fet0{\*\wgrffmtfilter 2450}\nofeaturethrottle1\ilfomacatclnup0{\*\ftnsep \ltrpar \pard\plain \ltrpar
\qj \li0\ri0\nowidctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid4216103 \rtlch\fcs1 \af0\afs22\alang1025 \ltrch\fcs0 \fs21\lang1033\langfe2052\kerning2\loch\af31506\hich\af31506\dbch\af31505\cgrid\langnp1033\langfenp2052 {
\rtlch\fcs1 \af0 \ltrch\fcs0 \insrsid12348761 \chftnsep 
\par }}{\*\ftnsepc \ltrpar \pard\plain \ltrpar\qj \li0\ri0\nowidctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid4216103 \rtlch\fcs1 \af0\afs22\alang1025 \ltrch\fcs0 
\fs21\lang1033\langfe2052\kerning2\loch\af31506\hich\af31506\dbch\af31505\cgrid\langnp1033\langfenp2052 {\rtlch\fcs1 \af0 \ltrch\fcs0 \insrsid12348761 \chftnsepc 
\par }}{\*\aftnsep \ltrpar \pard\plain \ltrpar\qj \li0\ri0\nowidctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid4216103 \rtlch\fcs1 \af0\afs22\alang1025 \ltrch\fcs0 
\fs21\lang1033\langfe2052\kerning2\loch\af31506\hich\af31506\dbch\af31505\cgrid\langnp1033\langfenp2052 {\rtlch\fcs1 \af0 \ltrch\fcs0 \insrsid12348761 \chftnsep 
\par }}{\*\aftnsepc \ltrpar \pard\plain \ltrpar\qj \li0\ri0\nowidctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid4216103 \rtlch\fcs1 \af0\afs22\alang1025 \ltrch\fcs0 
\fs21\lang1033\langfe2052\kerning2\loch\af31506\hich\af31506\dbch\af31505\cgrid\langnp1033\langfenp2052 {\rtlch\fcs1 \af0 \ltrch\fcs0 \insrsid12348761 \chftnsepc 
\par }}\ltrpar \sectd \ltrsect\linex0\sectdefaultcl\sftnbj {\*\pnseclvl1\pnucrm\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl2\pnucltr\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl3\pndec\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl4
\pnlcltr\pnstart1\pnindent720\pnhang {\pntxta )}}{\*\pnseclvl5\pndec\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl6\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl7\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}
{\pntxta )}}{\*\pnseclvl8\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl9\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\pntext\pard\plain\ltrpar \rtlch\fcs1 \af0\afs22 \ltrch\fcs0 
\fs22\lang9\langfe2052\loch\af44\hich\af44\dbch\af31505\langnp9\insrsid8011912 \hich\af44\dbch\af31505\loch\f44 I.\tab}\pard\plain \ltrpar\ql \fi-360\li720\ri0\sa200\sl276\slmult1\nowidctlpar\wrapdefault{\*\pn \pnlvlbody\ilvl0\ls1\pnrnot0
\pnucrm\pnf44\pnstart1 {\pntxta .}}\faauto\ls1\rin0\lin720\itap0\pararsid4216103 \rtlch\fcs1 \af0\afs22\alang1025 \ltrch\fcs0 \fs21\lang1033\langfe2052\kerning2\loch\af31506\hich\af31506\dbch\af31505\cgrid\langnp1033\langfenp2052 {\rtlch\fcs1 \af44 
\ltrch\fcs0 \f44\fs22\lang9\langfe2052\kerning0\langnp9\insrsid8011912 \hich\af44\dbch\af31505\loch\f44 First, }{\rtlch\fcs1 \ab\af44 \ltrch\fcs0 \b\f44\fs22\cf19\lang9\langfe2052\kerning0\langnp9\insrsid8011912 \hich\af44\dbch\af31505\loch\f44 
select the target genome model}{\rtlch\fcs1 \af44 \ltrch\fcs0 \f44\fs22\lang9\langfe2052\kerning0\langnp9\insrsid8011912 \hich\af44\dbch\af31505\loch\f44 . The program will then display a list of all genes in the selected genome. 
\par {\pntext\pard\plain\ltrpar \rtlch\fcs1 \af0\afs22 \ltrch\fcs0 \fs22\lang9\langfe2052\loch\af44\hich\af44\dbch\af31505\langnp9\insrsid8011912 \hich\af44\dbch\af31505\loch\f44 II.\tab}}\pard \ltrpar\ql \fi-360\li720\ri0\sa200\sl276\slmult1
\nowidctlpar\wrapdefault{\*\pn \pnlvlbody\ilvl0\ls1\pnrnot0\pnucrm\pnf44\pnstart1 {\pntxta .}}\faauto\ls1\rin0\lin720\itap0\pararsid4216103 {\rtlch\fcs1 \af44 \ltrch\fcs0 \f44\fs22\lang9\langfe2052\kerning0\langnp9\insrsid8011912 
\hich\af44\dbch\af31505\loch\f44 Click on a specific gene, and the affected biochemical reaction network will be listed below to assist in the preliminary assessment of the gene knockout effect. 
\par {\pntext\pard\plain\ltrpar \rtlch\fcs1 \af0\afs22 \ltrch\fcs0 \fs22\lang9\langfe2052\loch\af44\hich\af44\dbch\af31505\langnp9\insrsid8011912 \hich\af44\dbch\af31505\loch\f44 III.\tab}}\pard \ltrpar\ql \fi-360\li720\ri0\sa200\sl276\slmult1
\nowidctlpar\wrapdefault{\*\pn \pnlvlbody\ilvl0\ls1\pnrnot0\pnucrm\pnf44\pnstart1 {\pntxta .}}\faauto\ls1\rin0\lin720\itap0\pararsid4216103 {\rtlch\fcs1 \af44 \ltrch\fcs0 \f44\fs22\lang9\langfe2052\kerning0\langnp9\insrsid8011912 
\hich\af44\dbch\af31505\loch\f44 After \hich\af44\dbch\af31505\loch\f44 selecting a gene, use the ""}{\rtlch\fcs1 \ab\af44 \ltrch\fcs0 \b\f44\fs22\cf19\lang9\langfe2052\kerning0\langnp9\insrsid8011912 \hich\af44\dbch\af31505\loch\f44 
Add to Gene Knockout List}{\rtlch\fcs1 \af44 \ltrch\fcs0 \f44\fs22\lang9\langfe2052\kerning0\langnp9\insrsid8011912 \hich\af44\dbch\af31505\loch\f44 
"" option from the right-click context menu to add the selected gene to the knockout list for subsequent modification of the virtual cell model.
\par }\pard\plain \ltrpar\s19\ql \li0\ri0\sb100\sa100\sbauto1\saauto1\widctlpar\wrapdefault\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0\pararsid2577415 \rtlch\fcs1 \af13\afs24\alang1025 \ltrch\fcs0 
\fs24\lang1033\langfe2052\loch\af13\hich\af13\dbch\af13\cgrid\langnp1033\langfenp2052 {\rtlch\fcs1 \af13 \ltrch\fcs0 \f34\cf20\insrsid4216103\charrsid4216103 \hich\af34\dbch\af13\loch\f34 Whether to create an independent mutant virtual cell model
\hich\af34\dbch\af13\loch\f34 
 or directly perform gene knockout experimental settings on the original model. Creating an independent mutant virtual cell model will allow simultaneous analysis of both the wild-type and the mutant model after gene knockout in the experiment. Whereas di
\hich\af34\dbch\af13\loch\f34 r\hich\af34\dbch\af13\loch\f34 
ectly performing gene knockout experimental settings on the original virtual cell model will directly remove the expression associations of the knocked-out gene from the original model, enabling computational analysis of the mutant. }{\rtlch\fcs1 \af44 
\ltrch\fcs0 \f44\fs22\insrsid4216103\charrsid4216103 
\par }{\*\themedata 504b030414000600080000002100e9de0fbfff0000001c020000130000005b436f6e74656e745f54797065735d2e786d6cac91cb4ec3301045f748fc83e52d4a
9cb2400825e982c78ec7a27cc0c8992416c9d8b2a755fbf74cd25442a820166c2cd933f79e3be372bd1f07b5c3989ca74aaff2422b24eb1b475da5df374fd9ad
5689811a183c61a50f98f4babebc2837878049899a52a57be670674cb23d8e90721f90a4d2fa3802cb35762680fd800ecd7551dc18eb899138e3c943d7e503b6
b01d583deee5f99824e290b4ba3f364eac4a430883b3c092d4eca8f946c916422ecab927f52ea42b89a1cd59c254f919b0e85e6535d135a8de20f20b8c12c3b0
0c895fcf6720192de6bf3b9e89ecdbd6596cbcdd8eb28e7c365ecc4ec1ff1460f53fe813d3cc7f5b7f020000ffff0300504b030414000600080000002100a5d6
a7e7c0000000360100000b0000005f72656c732f2e72656c73848fcf6ac3300c87ef85bd83d17d51d2c31825762fa590432fa37d00e1287f68221bdb1bebdb4f
c7060abb0884a4eff7a93dfeae8bf9e194e720169aaa06c3e2433fcb68e1763dbf7f82c985a4a725085b787086a37bdbb55fbc50d1a33ccd311ba548b6309512
0f88d94fbc52ae4264d1c910d24a45db3462247fa791715fd71f989e19e0364cd3f51652d73760ae8fa8c9ffb3c330cc9e4fc17faf2ce545046e37944c69e462
a1a82fe353bd90a865aad41ed0b5b8f9d6fd010000ffff0300504b0304140006000800000021006b799616830000008a0000001c0000007468656d652f746865
6d652f7468656d654d616e616765722e786d6c0ccc4d0ac3201040e17da17790d93763bb284562b2cbaebbf600439c1a41c7a0d29fdbd7e5e38337cedf14d59b
4b0d592c9c070d8a65cd2e88b7f07c2ca71ba8da481cc52c6ce1c715e6e97818c9b48d13df49c873517d23d59085adb5dd20d6b52bd521ef2cdd5eb9246a3d8b
4757e8d3f729e245eb2b260a0238fd010000ffff0300504b0304140006000800000021007fdac6cd91070000c7200000160000007468656d652f7468656d652f
7468656d65312e786d6cec59cd8b1bc915bf07f23f347d97f5d5ad8fc1f2a24fcfda33b6b164873dd648a5eef2547789aad28cc56208de532e81c0ee924316f6
b68765c94216b2e4923fc660936cfe88bcaa6e755749a5f5cce080093382a1bbf47baf7ef5deabf79eaaee7ef232a1de05e682b0b4e7d7efd47c0fa773b62069
d4f39fcd26958eef0989d205a22cc53d7f8385ffc9bddffee62e3a92314eb007f2a938423d3f96727554ad8a390c237187ad700adf2d194f9084571e55171c5d
82de84561bb55aab9a2092fa5e8a1250fb78b92473eccd944affde56f998c26b2a851a98533e55aab125a1b18bf3ba42888d1852ee5d20daf3619e05bb9ce197
d2f7281212bee8f935fde757efddada2a35c88ca03b286dc44ffe572b9c0e2bca1e7e4d1593169108441ab5fe8d7002af771e3f6b8356e15fa3400cde7b0d28c
8badb3dd180639d600658f0edda3f6a859b7f086fee61ee77ea83e165e8332fdc11e7e321982152dbc0665f8700f1f0eba8391ad5f83327c6b0fdfaef54741db
d2af413125e9f91eba16b69ac3ed6a0bc892d16327bc1b06937623575ea2201a8ae852532c592a0fc55a825e303e018002522449eac9cd0a2fd11ca2f8dd0f7f
78f78f7f7a27248a21ee56286502466b8ddaa4d684ffea13e827ed5074849121ac680111b137a4e87862cec94af6fc07a0d537206f7ffef9cdeb9fdebcfefb9b
2fbe78f3faaff9dc5a9525778cd2c894fbe5bb3ffde79bdf7bfffedbb7bf7cf95536f52e5e98786b694ef5b0e2d2126fbffef1dd4f3fbefdf31ffff5fd970eed
7d8ece4cf88c2458788ff0a5f79425b040c704f88c5f4f621623624af4d348a014a9591cfac732b6d08f368822076e806d3b3ee790695cc0fbeb1716e169ccd7
9238343e8c130b78ca181d30eeb4c243359761e6d93a8ddc93f3b5897b8ad0856bee214a2d2f8fd72b48b1c4a57218638be6138a5289229c62e9a9efd839c68e
d57d468865d75332e74cb0a5f43e23de0011a74966e4cc8aa652e89824e0978d8b20f8dbb2cde9736fc0a86bd5237c6123616f20ea203fc3d432e37db4962871
a99ca1849a063f413276919c6ef8dcc48d85044f4798326fbcc042b8641e7358afe1f48708929bd3eda77493d8482ec9b94be70962cc448ed8f93046c9ca859d
923436b19f8a730851e43d61d2053f65f60e51efe007941e74f773822d77bf3f1b3c830c6b522a03447db3e60e5fdec7cc8adfe9862e1176a59a3e4fac14dbe7
c4191d83756485f609c6145da205c6deb34f1d0c066c65d9bc24fd2086ac728c5d81f500d9b1aade532ca05552bdcd7e9e3c21c20ad9298ed8013ea79b9dc4b3
416982f821cd8fc0eba6cdc7671c36a383c2633a3f37818f08b480102f4ea33c16a0c308ee835a9fc4c82a60ea5db8e375c32dff5d658fc1be7c61d1b8c2be04
197c6d1948eca6ccafda6686a8354119303344bc1357ba0511cbfda5882aae5a6ced945bda9bb67403344756cf9390f47d0dd04eeb13feef5a1f6830defee51b
470c7e9876c7add8ca55d76c740ee592e39df6e6106eb7a91932be201f7f4f3342ebf4098632b29fb06e5b9adb96c6ffbf6f690eede7db46e650bb71dbc8f8d0
60dc3632f9d1ca876964cade05da1a75de911df3e8439fe4e099cf92503a951b8a4f843ef611f0736631814125a7cf3b717106b88ae151953998c0c2451c6919
8f33f93b22e3698c56703854f7959248e4aa23e1ad988033233decd4adf0749d9cb24576d459afab63cdacb20a24cbf15a588cc33195ccd0ad76797c57a8d76c
237dccba25a064af43c298cc26d17490686f079591f4a12e18cd4142afec83b0e83a587494faadabf65800b5c22bf07bdb835fe93d3f0c400484e0380e7af385
f253e6eaad77b5333fa4a70f19d38a0068b0b711507abaabb81e5c9e5a5d166a57f0b445c208379b84b68c6ef0440cbf82f3e854a357a1715d5f774b975af494
29f47c105a258d76e7d758dcd4d720b79b1b686a660a9a7a973dbfd50c2164e668d5f39770640c8fc90a6247a89f5c884670ef32973cdbf037c92c2b2ee40889
3833b84e3a59364888c4dca324e9f96af9851b68aa7388e6566f4042f868c97521ad7c6ce4c0e9b693f17289e7d274bb31a22c9dbd4286cf7285f35b2d7e73b0
92646b70f7345e5c7a6774cd9f2208b1b05d57065c10015707f5cc9a0b02576145222be36fa730e569d7bc8bd231948d23ba8a515e51cc649ec1752a2fe8e8b7
c206c65bbe6630a86192bc109e45aac09a46b5aa695135320e07abeefb8594e58ca459d64c2baba8aae9ce62d60cdb32b063cb9b157983d5d6c490d3cc0a9fa5
eedd94dbdde6ba9d3ea1a81260f0c27e8eaa7b858260502b27b3a829c6fb6958e5ec7cd4ae1ddb05be87da558a8491f55b5bb53b762b6a84733a18bc51e507b9
dda885a1e5b6afd496d677e6e6b5363b7b01c963045dee9a4aa15d0907bb1c414334d53d499636608bbc94f9d680276fcd49cfffbc16f68361231c566a9d705c
099a41add209fbcd4a3f0c9bf57158af8d068d575058649cd4c3ecbe7e02f7177493dfdaebf1bd9bfb647b457367ce922ad337f3554d5cdfdcd71b876fee3d02
49e7f35663d26d7607ad4ab7d99f5482d1a053e90e5b83caa8356c8f26a361d8e94e5ef9de850607fde630688d3b95567d38ac04ad9aa2dfe956da41a3d10fda
fdce38e8bfcadb185879963e725b807935af7bff050000ffff0300504b0304140006000800000021000dd1909fb60000001b010000270000007468656d652f74
68656d652f5f72656c732f7468656d654d616e616765722e786d6c2e72656c73848f4d0ac2301484f78277086f6fd3ba109126dd88d0add40384e4350d363f24
51eced0dae2c082e8761be9969bb979dc9136332de3168aa1a083ae995719ac16db8ec8e4052164e89d93b64b060828e6f37ed1567914b284d262452282e3198
720e274a939cd08a54f980ae38a38f56e422a3a641c8bbd048f7757da0f19b017cc524bd62107bd5001996509affb3fd381a89672f1f165dfe514173d9850528
a2c6cce0239baa4c04ca5bbabac4df000000ffff0300504b01022d0014000600080000002100e9de0fbfff0000001c0200001300000000000000000000000000
000000005b436f6e74656e745f54797065735d2e786d6c504b01022d0014000600080000002100a5d6a7e7c0000000360100000b000000000000000000000000
00300100005f72656c732f2e72656c73504b01022d00140006000800000021006b799616830000008a0000001c00000000000000000000000000190200007468
656d652f7468656d652f7468656d654d616e616765722e786d6c504b01022d00140006000800000021007fdac6cd91070000c720000016000000000000000000
00000000d60200007468656d652f7468656d652f7468656d65312e786d6c504b01022d00140006000800000021000dd1909fb60000001b010000270000000000
00000000000000009b0a00007468656d652f7468656d652f5f72656c732f7468656d654d616e616765722e786d6c2e72656c73504b050600000000050005005d010000960b00000000}
{\*\colorschememapping 3c3f786d6c2076657273696f6e3d22312e302220656e636f64696e673d225554462d3822207374616e64616c6f6e653d22796573223f3e0d0a3c613a636c724d
617020786d6c6e733a613d22687474703a2f2f736368656d61732e6f70656e786d6c666f726d6174732e6f72672f64726177696e676d6c2f323030362f6d6169
6e22206267313d226c743122207478313d22646b3122206267323d226c743222207478323d22646b322220616363656e74313d22616363656e74312220616363
656e74323d22616363656e74322220616363656e74333d22616363656e74332220616363656e74343d22616363656e74342220616363656e74353d22616363656e74352220616363656e74363d22616363656e74362220686c696e6b3d22686c696e6b2220666f6c486c696e6b3d22666f6c486c696e6b222f3e}
{\*\latentstyles\lsdstimax376\lsdlockeddef0\lsdsemihiddendef0\lsdunhideuseddef0\lsdqformatdef0\lsdprioritydef99{\lsdlockedexcept \lsdqformat1 \lsdpriority0 \lsdlocked0 Normal;\lsdqformat1 \lsdpriority9 \lsdlocked0 heading 1;
\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 2;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 3;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 4;
\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 5;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 6;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 7;
\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 8;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority9 \lsdlocked0 heading 9;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 1;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 5;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 6;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 7;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 8;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index 9;
\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 1;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 2;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 3;
\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 4;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 5;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 6;
\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 7;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 8;\lsdsemihidden1 \lsdunhideused1 \lsdpriority39 \lsdlocked0 toc 9;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Normal Indent;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 footnote text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 annotation text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 header;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 footer;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 index heading;\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority35 \lsdlocked0 caption;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 table of figures;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 envelope address;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 envelope return;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 footnote reference;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 annotation reference;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 line number;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 page number;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 endnote reference;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 endnote text;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 table of authorities;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 macro;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 toa heading;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List 3;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List 5;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet 3;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Bullet 5;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number 3;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number 4;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Number 5;\lsdqformat1 \lsdpriority10 \lsdlocked0 Title;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Closing;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Signature;\lsdsemihidden1 \lsdunhideused1 \lsdpriority1 \lsdlocked0 Default Paragraph Font;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text Indent;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue 4;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 List Continue 5;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Message Header;\lsdqformat1 \lsdpriority11 \lsdlocked0 Subtitle;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Salutation;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Date;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text First Indent;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text First Indent 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Note Heading;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text Indent 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Body Text Indent 3;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Block Text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Hyperlink;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 FollowedHyperlink;\lsdqformat1 \lsdpriority22 \lsdlocked0 Strong;
\lsdqformat1 \lsdpriority20 \lsdlocked0 Emphasis;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Document Map;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Plain Text;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 E-mail Signature;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Top of Form;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Bottom of Form;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Normal (Web);\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Acronym;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Address;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Cite;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Code;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Definition;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Keyboard;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Preformatted;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Sample;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Typewriter;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 HTML Variable;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 annotation subject;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 No List;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Outline List 1;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Outline List 2;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Outline List 3;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Balloon Text;\lsdpriority39 \lsdlocked0 Table Grid;
\lsdsemihidden1 \lsdlocked0 Placeholder Text;\lsdqformat1 \lsdpriority1 \lsdlocked0 No Spacing;\lsdpriority60 \lsdlocked0 Light Shading;\lsdpriority61 \lsdlocked0 Light List;\lsdpriority62 \lsdlocked0 Light Grid;
\lsdpriority63 \lsdlocked0 Medium Shading 1;\lsdpriority64 \lsdlocked0 Medium Shading 2;\lsdpriority65 \lsdlocked0 Medium List 1;\lsdpriority66 \lsdlocked0 Medium List 2;\lsdpriority67 \lsdlocked0 Medium Grid 1;\lsdpriority68 \lsdlocked0 Medium Grid 2;
\lsdpriority69 \lsdlocked0 Medium Grid 3;\lsdpriority70 \lsdlocked0 Dark List;\lsdpriority71 \lsdlocked0 Colorful Shading;\lsdpriority72 \lsdlocked0 Colorful List;\lsdpriority73 \lsdlocked0 Colorful Grid;\lsdpriority60 \lsdlocked0 Light Shading Accent 1;
\lsdpriority61 \lsdlocked0 Light List Accent 1;\lsdpriority62 \lsdlocked0 Light Grid Accent 1;\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 1;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 1;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 1;
\lsdsemihidden1 \lsdlocked0 Revision;\lsdqformat1 \lsdpriority34 \lsdlocked0 List Paragraph;\lsdqformat1 \lsdpriority29 \lsdlocked0 Quote;\lsdqformat1 \lsdpriority30 \lsdlocked0 Intense Quote;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 1;
\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 1;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 1;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 1;\lsdpriority70 \lsdlocked0 Dark List Accent 1;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 1;
\lsdpriority72 \lsdlocked0 Colorful List Accent 1;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 1;\lsdpriority60 \lsdlocked0 Light Shading Accent 2;\lsdpriority61 \lsdlocked0 Light List Accent 2;\lsdpriority62 \lsdlocked0 Light Grid Accent 2;
\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 2;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 2;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 2;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 2;
\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 2;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 2;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 2;\lsdpriority70 \lsdlocked0 Dark List Accent 2;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 2;
\lsdpriority72 \lsdlocked0 Colorful List Accent 2;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 2;\lsdpriority60 \lsdlocked0 Light Shading Accent 3;\lsdpriority61 \lsdlocked0 Light List Accent 3;\lsdpriority62 \lsdlocked0 Light Grid Accent 3;
\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 3;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 3;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 3;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 3;
\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 3;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 3;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 3;\lsdpriority70 \lsdlocked0 Dark List Accent 3;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 3;
\lsdpriority72 \lsdlocked0 Colorful List Accent 3;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 3;\lsdpriority60 \lsdlocked0 Light Shading Accent 4;\lsdpriority61 \lsdlocked0 Light List Accent 4;\lsdpriority62 \lsdlocked0 Light Grid Accent 4;
\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 4;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 4;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 4;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 4;
\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 4;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 4;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 4;\lsdpriority70 \lsdlocked0 Dark List Accent 4;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 4;
\lsdpriority72 \lsdlocked0 Colorful List Accent 4;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 4;\lsdpriority60 \lsdlocked0 Light Shading Accent 5;\lsdpriority61 \lsdlocked0 Light List Accent 5;\lsdpriority62 \lsdlocked0 Light Grid Accent 5;
\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 5;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 5;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 5;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 5;
\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 5;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 5;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 5;\lsdpriority70 \lsdlocked0 Dark List Accent 5;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 5;
\lsdpriority72 \lsdlocked0 Colorful List Accent 5;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 5;\lsdpriority60 \lsdlocked0 Light Shading Accent 6;\lsdpriority61 \lsdlocked0 Light List Accent 6;\lsdpriority62 \lsdlocked0 Light Grid Accent 6;
\lsdpriority63 \lsdlocked0 Medium Shading 1 Accent 6;\lsdpriority64 \lsdlocked0 Medium Shading 2 Accent 6;\lsdpriority65 \lsdlocked0 Medium List 1 Accent 6;\lsdpriority66 \lsdlocked0 Medium List 2 Accent 6;
\lsdpriority67 \lsdlocked0 Medium Grid 1 Accent 6;\lsdpriority68 \lsdlocked0 Medium Grid 2 Accent 6;\lsdpriority69 \lsdlocked0 Medium Grid 3 Accent 6;\lsdpriority70 \lsdlocked0 Dark List Accent 6;\lsdpriority71 \lsdlocked0 Colorful Shading Accent 6;
\lsdpriority72 \lsdlocked0 Colorful List Accent 6;\lsdpriority73 \lsdlocked0 Colorful Grid Accent 6;\lsdqformat1 \lsdpriority19 \lsdlocked0 Subtle Emphasis;\lsdqformat1 \lsdpriority21 \lsdlocked0 Intense Emphasis;
\lsdqformat1 \lsdpriority31 \lsdlocked0 Subtle Reference;\lsdqformat1 \lsdpriority32 \lsdlocked0 Intense Reference;\lsdqformat1 \lsdpriority33 \lsdlocked0 Book Title;\lsdsemihidden1 \lsdunhideused1 \lsdpriority37 \lsdlocked0 Bibliography;
\lsdsemihidden1 \lsdunhideused1 \lsdqformat1 \lsdpriority39 \lsdlocked0 TOC Heading;\lsdpriority41 \lsdlocked0 Plain Table 1;\lsdpriority42 \lsdlocked0 Plain Table 2;\lsdpriority43 \lsdlocked0 Plain Table 3;\lsdpriority44 \lsdlocked0 Plain Table 4;
\lsdpriority45 \lsdlocked0 Plain Table 5;\lsdpriority40 \lsdlocked0 Grid Table Light;\lsdpriority46 \lsdlocked0 Grid Table 1 Light;\lsdpriority47 \lsdlocked0 Grid Table 2;\lsdpriority48 \lsdlocked0 Grid Table 3;\lsdpriority49 \lsdlocked0 Grid Table 4;
\lsdpriority50 \lsdlocked0 Grid Table 5 Dark;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 1;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 1;
\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 1;\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 1;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 1;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 1;
\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 1;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 2;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 2;\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 2;
\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 2;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 2;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 2;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 2;
\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 3;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 3;\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 3;\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 3;
\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 3;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 3;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 3;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 4;
\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 4;\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 4;\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 4;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 4;
\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 4;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 4;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 5;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 5;
\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 5;\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 5;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 5;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 5;
\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 5;\lsdpriority46 \lsdlocked0 Grid Table 1 Light Accent 6;\lsdpriority47 \lsdlocked0 Grid Table 2 Accent 6;\lsdpriority48 \lsdlocked0 Grid Table 3 Accent 6;
\lsdpriority49 \lsdlocked0 Grid Table 4 Accent 6;\lsdpriority50 \lsdlocked0 Grid Table 5 Dark Accent 6;\lsdpriority51 \lsdlocked0 Grid Table 6 Colorful Accent 6;\lsdpriority52 \lsdlocked0 Grid Table 7 Colorful Accent 6;
\lsdpriority46 \lsdlocked0 List Table 1 Light;\lsdpriority47 \lsdlocked0 List Table 2;\lsdpriority48 \lsdlocked0 List Table 3;\lsdpriority49 \lsdlocked0 List Table 4;\lsdpriority50 \lsdlocked0 List Table 5 Dark;
\lsdpriority51 \lsdlocked0 List Table 6 Colorful;\lsdpriority52 \lsdlocked0 List Table 7 Colorful;\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 1;\lsdpriority47 \lsdlocked0 List Table 2 Accent 1;\lsdpriority48 \lsdlocked0 List Table 3 Accent 1;
\lsdpriority49 \lsdlocked0 List Table 4 Accent 1;\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 1;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 1;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 1;
\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 2;\lsdpriority47 \lsdlocked0 List Table 2 Accent 2;\lsdpriority48 \lsdlocked0 List Table 3 Accent 2;\lsdpriority49 \lsdlocked0 List Table 4 Accent 2;
\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 2;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 2;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 2;\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 3;
\lsdpriority47 \lsdlocked0 List Table 2 Accent 3;\lsdpriority48 \lsdlocked0 List Table 3 Accent 3;\lsdpriority49 \lsdlocked0 List Table 4 Accent 3;\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 3;
\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 3;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 3;\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 4;\lsdpriority47 \lsdlocked0 List Table 2 Accent 4;
\lsdpriority48 \lsdlocked0 List Table 3 Accent 4;\lsdpriority49 \lsdlocked0 List Table 4 Accent 4;\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 4;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 4;
\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 4;\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 5;\lsdpriority47 \lsdlocked0 List Table 2 Accent 5;\lsdpriority48 \lsdlocked0 List Table 3 Accent 5;
\lsdpriority49 \lsdlocked0 List Table 4 Accent 5;\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 5;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 5;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 5;
\lsdpriority46 \lsdlocked0 List Table 1 Light Accent 6;\lsdpriority47 \lsdlocked0 List Table 2 Accent 6;\lsdpriority48 \lsdlocked0 List Table 3 Accent 6;\lsdpriority49 \lsdlocked0 List Table 4 Accent 6;
\lsdpriority50 \lsdlocked0 List Table 5 Dark Accent 6;\lsdpriority51 \lsdlocked0 List Table 6 Colorful Accent 6;\lsdpriority52 \lsdlocked0 List Table 7 Colorful Accent 6;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Mention;
\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Smart Hyperlink;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Hashtag;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Unresolved Mention;\lsdsemihidden1 \lsdunhideused1 \lsdlocked0 Smart Link;}}{\*\datastore 01050000
02000000180000004d73786d6c322e534158584d4c5265616465722e362e3000000000000000000000060000
d0cf11e0a1b11ae1000000000000000000000000000000003e000300feff090006000000000000000000000001000000010000000000000000100000feffffff00000000feffffff0000000000000000ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
fffffffffffffffffdfffffffeffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff
ffffffffffffffffffffffffffffffff52006f006f007400200045006e00740072007900000000000000000000000000000000000000000000000000000000000000000000000000000000000000000016000500ffffffffffffffffffffffff0c6ad98892f1d411a65f0040963251e50000000000000000000000004021
007f8930dc01feffffff00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000ffffffffffffffffffffffff00000000000000000000000000000000000000000000000000000000
00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000ffffffffffffffffffffffff0000000000000000000000000000000000000000000000000000
000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000ffffffffffffffffffffffff000000000000000000000000000000000000000000000000
0000000000000000000000000000000000000000000000000105000000000000}}"
    End Sub
End Class

Public Class KnockoutGene

    Public Property genome As String
    Public Property gene As gene

    Public Overrides Function ToString() As String
        Return $"[{genome}] {gene}"
    End Function

End Class