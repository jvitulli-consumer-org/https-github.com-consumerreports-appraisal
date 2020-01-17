Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Imports System.Configuration
Imports System.Web.Configuration
Public Class Print_Goals
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8, SQL9, SQL10, SQL1_1, SQL2_1, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6, DT7, DT8, DT9, DT10, DT1_1 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim Msg, Msg1, Subj, x, x1 As String
    Dim EMPLID As String

    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer
    Dim KeyTaskRows1, KeyTaskRows2, KeyTaskRows3, KeyTaskRows4, KeyTaskRows5, KeyTaskRows6, KeyTaskRows7, KeyTaskRows8, KeyTaskRows9, KeyTaskRows10, KeyTaskRows11, KeyTaskRows12, KeyTaskRows13, KeyTaskRows14 As Integer

    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Trim(Request.QueryString("Token").ToString) = 2016 Then
            DisplayData()
        Else
            'Response.Write(Trim(Request.QueryString("Token").ToString)) : Response.End()
            DisplayData_New()
        End If

    End Sub

    Protected Sub DisplayData()
        Dim a As Integer

        SQL = "select (select First+' '+Last from id_tbl where emplid=A.emplid)EMP_Name,(select First+' '+Last from id_tbl where emplid=A.SUP_EMPLID)SUP_Name,(select First+' '+Last from id_tbl where emplid=A.UP_MGT_EMPLID)UP_MGT_Name,"
        SQL &= " (select First+' '+Last from id_tbl where emplid=A.HR_EMPLID)HR_Name,(select Last from id_tbl where emplid=A.emplid)EMP_LastName,(select convert(char(10),Hire_date,101) from id_tbl where emplid=A.emplid)Hired,"
        SQL &= " * from (select * from Guild_Appraisal_FUTUREGOAL_MASTER_tbl)A LEFT JOIN (/*1.Milestones*/"
        SQL &= " select emplid,Max(Goals1)Goals1,Max(Milestones1)Milestones1,Max(TargetDate1)TargetDate1,Max(Goals2)Goals2,Max(Milestones2)Milestones2,Max(TargetDate2)TargetDate2,Max(Goals3)Goals3,Max(Milestones3)Milestones3,"
        SQL &= " Max(TargetDate3)TargetDate3,Max(Goals4)Goals4,Max(Milestones4)Milestones4,Max(TargetDate4)TargetDate4,Max(Goals5)Goals5,Max(Milestones5)Milestones5,Max(TargetDate5)TargetDate5,Max(Goals6)Goals6,"
        SQL &= " Max(Milestones6)Milestones6,Max(TargetDate6)TargetDate6,Max(Goals7)Goals7,Max(Milestones7)Milestones7,Max(TargetDate7)TargetDate7,Max(Goals8)Goals8,Max(Milestones8)Milestones8,Max(TargetDate8)TargetDate8,"
        SQL &= " Max(Goals9)Goals9,Max(Milestones9)Milestones9,Max(TargetDate9)TargetDate9,Max(Goals10)Goals10,Max(Milestones10)Milestones10,Max(TargetDate10)TargetDate10 from(select emplid,"
        SQL &= " (case when IndexID=1 then Rtrim(Ltrim(Goals)) else '' end)Goals1,(case when IndexID=1 then Rtrim(Ltrim(Milestones)) else '' end)Milestones1,(case when IndexID=1 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate1,"
        SQL &= " (case when IndexID=2 then Rtrim(Ltrim(Goals)) else '' end)Goals2,(case when IndexID=2 then Rtrim(Ltrim(Milestones)) else '' end)Milestones2,(case when IndexID=2 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate2,"
        SQL &= " (case when IndexID=3 then Rtrim(Ltrim(Goals)) else '' end)Goals3,(case when IndexID=3 then Rtrim(Ltrim(Milestones)) else '' end)Milestones3,(case when IndexID=3 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate3,"
        SQL &= " (case when IndexID=4 then Rtrim(Ltrim(Goals)) else '' end)Goals4,(case when IndexID=4 then Rtrim(Ltrim(Milestones)) else '' end)Milestones4,(case when IndexID=4 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate4,"
        SQL &= " (case when IndexID=5 then Rtrim(Ltrim(Goals)) else '' end)Goals5,(case when IndexID=5 then Rtrim(Ltrim(Milestones)) else '' end)Milestones5,(case when IndexID=5 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate5,"
        SQL &= " (case when IndexID=6 then Rtrim(Ltrim(Goals)) else '' end)Goals6,(case when IndexID=6 then Rtrim(Ltrim(Milestones)) else '' end)Milestones6,(case when IndexID=6 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate6,"
        SQL &= " (case when IndexID=7 then Rtrim(Ltrim(Goals)) else '' end)Goals7,(case when IndexID=7 then Rtrim(Ltrim(Milestones)) else '' end)Milestones7,(case when IndexID=7 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate7,"
        SQL &= " (case when IndexID=8 then Rtrim(Ltrim(Goals)) else '' end)Goals8,(case when IndexID=8 then Rtrim(Ltrim(Milestones)) else '' end)Milestones8,(case when IndexID=8 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate8,"
        SQL &= " (case when IndexID=9 then Rtrim(Ltrim(Goals)) else '' end)Goals9,(case when IndexID=9 then Rtrim(Ltrim(Milestones)) else '' end)Milestones9,(case when IndexID=9 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate9,"
        SQL &= " (case when IndexID=10 then Rtrim(Ltrim(Goals)) else '' end)Goals10,(case when IndexID=10 then Rtrim(Ltrim(Milestones)) else '' end)Milestones10,(case when IndexID=10 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate10"
        SQL &= " from GUILD_Appraisal_FUTUREGOAL_TBL)B group by emplid/*1END*/)C ON A.emplid=C.emplid LEFT JOIN (/*2.Key Task Description*/"
        SQL &= " select emplid,Max(Task1)Task1,Max(Task2)Task2,Max(Task3)Task3,Max(Task4)Task4,Max(Task5)Task5,Max(Task6)Task6,Max(Task7)Task7,Max(Task8)Task8,Max(Task9)Task9,Max(Task10)Task10,Max(Task11)Task11,Max(Task12)Task12,"
        SQL &= " Max(Task13)Task13,Max(Task14)Task14,Max(Task15)Task15,Max(Task16)Task16,Max(Task17)Task17,Max(Task18)Task18,Max(Task19)Task19,Max(Task20)Task20"
        SQL &= " from(select emplid,(case when  IndexID=1 then Rtrim(Ltrim(Task)) else '' end)Task1,(case when  IndexID=2 then Rtrim(Ltrim(Task)) else '' end)Task2,(case when  IndexID=3 then Rtrim(Ltrim(Task)) else '' end)Task3,"
        SQL &= " (case when  IndexID=4 then Rtrim(Ltrim(Task)) else '' end)Task4,(case when  IndexID=5 then Rtrim(Ltrim(Task)) else '' end)Task5,(case when  IndexID=6 then Rtrim(Ltrim(Task)) else '' end)Task6,"
        SQL &= " (case when  IndexID=7 then Rtrim(Ltrim(Task)) else '' end)Task7,(case when  IndexID=8 then Rtrim(Ltrim(Task)) else '' end)Task8,(case when  IndexID=9 then Rtrim(Ltrim(Task)) else '' end)Task9,"
        SQL &= " (case when  IndexID=10 then Rtrim(Ltrim(Task)) else '' end)Task10,(case when  IndexID=11 then Rtrim(Ltrim(Task)) else '' end)Task11,(case when  IndexID=12 then Rtrim(Ltrim(Task)) else '' end)Task12,"
        SQL &= " (case when  IndexID=13 then Rtrim(Ltrim(Task)) else '' end)Task13,(case when  IndexID=14 then Rtrim(Ltrim(Task)) else '' end)Task14,(case when  IndexID=15 then Rtrim(Ltrim(Task)) else '' end)Task15,"
        SQL &= " (case when  IndexID=16 then Rtrim(Ltrim(Task)) else '' end)Task16,(case when  IndexID=17 then Rtrim(Ltrim(Task)) else '' end)Task17,(case when  IndexID=18 then Rtrim(Ltrim(Task)) else '' end)Task18,"
        SQL &= " (case when  IndexID=19 then Rtrim(Ltrim(Task)) else '' end)Task19,(case when  IndexID=20 then Rtrim(Ltrim(Task)) else '' end)Task20 "
        SQL &= " from GUILD_Appraisal_FUTURETASK_TBL)A group by emplid/*2END*/)D ON A.emplid=D.emplid LEFT JOIN"
        SQL &= " (/*3.Previous Goals*/ select emplid,Max(Goals1)PRE_Goals1,Max(Milestones1)PRE_Milestones1,Max(TargetDate1)PRE_TargetDate1,Max(Goals2)PRE_Goals2,Max(Milestones2)PRE_Milestones2,Max(TargetDate2)PRE_TargetDate2,"
        SQL &= " Max(Goals3)PRE_Goals3,Max(Milestones3)PRE_Milestones3,Max(TargetDate3)PRE_TargetDate3,Max(Goals4)PRE_Goals4,Max(Milestones4)PRE_Milestones4,Max(TargetDate4)PRE_TargetDate4,Max(Goals5)Goals5,"
        SQL &= " Max(Milestones5)PRE_Milestones5,Max(TargetDate5)PRE_TargetDate5,Max(Goals6)PRE_Goals6,Max(Milestones6)PRE_Milestones6,Max(TargetDate6)PRE_TargetDate6,Max(Goals7)PRE_Goals7,Max(Milestones7)PRE_Milestones7,"
        SQL &= " Max(TargetDate7)PRE_TargetDate7,Max(Goals8)PRE_Goals8,Max(Milestones8)PRE_Milestones8,Max(TargetDate8)PRE_TargetDate8,Max(Goals9)PRE_Goals9,Max(Milestones9)PRE_Milestones9,Max(TargetDate9)PRE_TargetDate9,"
        SQL &= " Max(Goals10)PRE_Goals10,Max(Milestones10)PRE_Milestones10,Max(TargetDate10)PRE_TargetDate10 from(select emplid,"
        SQL &= " (case when IndexID=1 then Rtrim(Ltrim(Goals)) else '' end)Goals1,(case when IndexID=1 then Rtrim(Ltrim(Milestones)) else '' end)Milestones1,(case when IndexID=1 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate1,"
        SQL &= " (case when IndexID=2 then Rtrim(Ltrim(Goals)) else '' end)Goals2,(case when IndexID=2 then Rtrim(Ltrim(Milestones)) else '' end)Milestones2,(case when IndexID=2 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate2,"
        SQL &= " (case when IndexID=3 then Rtrim(Ltrim(Goals)) else '' end)Goals3,(case when IndexID=3 then Rtrim(Ltrim(Milestones)) else '' end)Milestones3,(case when IndexID=3 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate3,"
        SQL &= " (case when IndexID=4 then Rtrim(Ltrim(Goals)) else '' end)Goals4,(case when IndexID=4 then Rtrim(Ltrim(Milestones)) else '' end)Milestones4,(case when IndexID=4 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate4,"
        SQL &= " (case when IndexID=5 then Rtrim(Ltrim(Goals)) else '' end)Goals5,(case when IndexID=5 then Rtrim(Ltrim(Milestones)) else '' end)Milestones5,(case when IndexID=5 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate5,"
        SQL &= " (case when IndexID=6 then Rtrim(Ltrim(Goals)) else '' end)Goals6,(case when IndexID=6 then Rtrim(Ltrim(Milestones)) else '' end)Milestones6,(case when IndexID=6 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate6,"
        SQL &= " (case when IndexID=7 then Rtrim(Ltrim(Goals)) else '' end)Goals7,(case when IndexID=7 then Rtrim(Ltrim(Milestones)) else '' end)Milestones7,(case when IndexID=7 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate7,"
        SQL &= " (case when IndexID=8 then Rtrim(Ltrim(Goals)) else '' end)Goals8,(case when IndexID=8 then Rtrim(Ltrim(Milestones)) else '' end)Milestones8,(case when IndexID=8 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate8,"
        SQL &= " (case when IndexID=9 then Rtrim(Ltrim(Goals)) else '' end)Goals9,(case when IndexID=9 then Rtrim(Ltrim(Milestones)) else '' end)Milestones9,(case when IndexID=9 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate9,"
        SQL &= " (case when IndexID=10 then Rtrim(Ltrim(Goals)) else '' end)Goals10,(case when IndexID=10 then Rtrim(Ltrim(Milestones)) else '' end)Milestones10,(case when IndexID=10 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate10"
        SQL &= " from GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL)B group by emplid/*3END*/)E ON A.emplid=E.emplid order by EMP_LastName"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        For i = 0 To DT.Rows.Count - 1

            Response.Write("<table border=0 class=StyleBreak style=width:100%><tr><td style=width:50px;>&nbsp;</td>")
            Response.Write("<td>")
            Response.Write("<table border=0 style=width:100%>")
            Response.Write("<tr><td style=text-align:center;><img alt=../../../images/CR_logo.png src=../../../images/CR_logo.png style=width:380px; height:60px runat=server/></td></tr>")
            Response.Write("<tr><td class=Style1><u>Goal-Setting Form - 10/01/15 - 05/31/16</u></td></tr>")
            Response.Write("<tr><td>")

            Response.Write("<table border=0 style=width:100%>")

            Response.Write("<tr><td class=style4><b>Name:</b></td><td class=style7>" & DT.Rows(i)("EMP_Name").ToString & "</td>")
            Response.Write("<td class=style12><b>Manager Name:</b></td><td class=style8>" & DT.Rows(i)("SUP_Name").ToString & "</td>")
            Response.Write("<td style=text-align:right; font-family:Calibri;>&nbsp; Approved:&nbsp;</td>")

            '1--Approved by Manager--
            If CDbl(DT.Rows(i)("Process_Flag").ToString) >= 1 Then
                Response.Write("<td>" & DT.Rows(i)("Date_Submitted_To_UP_MGT").ToString & "</td>")
            Else
                Response.Write("<td>&nbsp;</td>")
            End If
            Response.Write("</tr>")
            '--1END--

            Response.Write("<tr><td class=style4><b>Title:</b></td><td class=style7>" & DT.Rows(i)("title").ToString & "</td>")
            Response.Write("<td class=style12><b>2nd Level Manager Name:&nbsp;</b></td><td class=style8>" & DT.Rows(i)("up_mgt_name").ToString & "</td>")

            Response.Write("<td style=text-align:right; width:100px; font-family:Calibri;>Approved:&nbsp;</td>")
            '2--Approved by Second Manager--
            If CDbl(DT.Rows(i)("Process_Flag").ToString) >= 2 Then
                Response.Write("<td>" & DT.Rows(i)("Date_Submitted_To_HR").ToString & "</td>")
            Else
                Response.Write("<td>&nbsp;</td>")
            End If
            Response.Write("</tr>")
            '--2END--

            Response.Write("<tr><td class=style4><b>Department:</b></td><td class=style7>" & DT.Rows(i)("Department").ToString & "</td>")
            Response.Write("<td class=style12><b>Human Resources Generalist:&nbsp;</b></td><td class=style8>" & DT.Rows(i)("HR_Name").ToString & "</td>")

            Response.Write("<td style=text-align:right; font-family:Calibri;>Approved:&nbsp;</td>")
            '3.--Reviewed by HR-- 
            If CDbl(DT.Rows(i)("Process_Flag").ToString) >= 3 Then
                Response.Write("<td>" & DT.Rows(i)("Date_HR_Approved").ToString & "</td>")
            Else
                Response.Write("<td>&nbsp;</td>")
            End If
            Response.Write("</tr>")
            '--3END--


            Response.Write("<tr><td class=style4><b>Hire Date:</b></td><td class=style7 colspan=4>" & DT.Rows(i)("Hired").ToString & "</td><td>&nbsp;</td></tr>")

            Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")
            Response.Write("<tr><td colspan=6><font size=4px color=#00AE4D><b><u>Rating Criteria:</u></b></font></td></tr>")
            Response.Write("<tr><td colspan=6> Enter in the Key Task and description information in the space provided below. Key Task information should be extracted from employee's job description.</td></tr>")
            Response.Write("<tr><td colspan=6><table width=100% border=1 cellpadding=0 cellspacing=0 >")
            If Len(DT.Rows(i)("Task1").ToString) > 2 Then Response.Write("<tr><td class=style15>1) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task1").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task2").ToString) > 2 Then Response.Write("<tr><td class=style15>2) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task2").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task3").ToString) > 2 Then Response.Write("<tr><td class=style15>3) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task3").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task4").ToString) > 2 Then Response.Write("<tr><td class=style15>4) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task4").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task5").ToString) > 2 Then Response.Write("<tr><td class=style15>5) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task5").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task6").ToString) > 2 Then Response.Write("<tr><td class=style15>6) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task6").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task7").ToString) > 2 Then Response.Write("<tr><td class=style15>7) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task7").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task8").ToString) > 2 Then Response.Write("<tr><td class=style15>8) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task8").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task9").ToString) > 2 Then Response.Write("<tr><td class=style15>9) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task9").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task10").ToString) > 2 Then Response.Write("<tr><td class=style15>10) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task10").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task11").ToString) > 2 Then Response.Write("<tr><td class=style15>11) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task11").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task12").ToString) > 2 Then Response.Write("<tr><td class=style15>12) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task12").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task13").ToString) > 2 Then Response.Write("<tr><td class=style15>13) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task13").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task14").ToString) > 2 Then Response.Write("<tr><td class=style15>14) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task14").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task15").ToString) > 2 Then Response.Write("<tr><td class=style15>15) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task15").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task16").ToString) > 2 Then Response.Write("<tr><td class=style15>16) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task16").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task17").ToString) > 2 Then Response.Write("<tr><td class=style15>17) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task17").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task18").ToString) > 2 Then Response.Write("<tr><td class=style15>18) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task18").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task19").ToString) > 2 Then Response.Write("<tr><td class=style15>19) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task19").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task20").ToString) > 2 Then Response.Write("<tr><td class=style15>20) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task20").ToString & "</td></tr>")

            Response.Write("</table></td></tr>")

            Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")
            Response.Write("<tr><td colspan=6><font size=4px color=#00AE4D><b><u>SMART Goals:</u></b></font></td></tr>")
            Response.Write("<tr><td colspan=6> Enter SMART Goals (<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to focus employees on achieving important, tangible outcomes that contribute to the achievement of CR&#39;s Enterprise Goals. Summarize the goals agreed to with the employee.")

            'Response.Write(" Please click <font color=blue><u>here</u></font> for a SMART goal example. </td></tr>")

            Response.Write("<tr><td colspan=6><table width=100% border=1 cellpadding=0 cellspacing=0>")

            Response.Write("<tr><td width=45% align=center><b>Goals</b></td><td width=45% align=center><b>Success Measures or Milestones</b></td><td width=10% align=center><b>Target<br/>Completion Date</b></td></tr>")

            If Len(DT.Rows(i)("Goals1").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals1").ToString & "</td><td>" & DT.Rows(i)("Milestones1").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate1").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals2").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals2").ToString & "</td><td>" & DT.Rows(i)("Milestones2").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate2").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals3").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals3").ToString & "</td><td>" & DT.Rows(i)("Milestones3").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate3").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals4").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals4").ToString & "</td><td>" & DT.Rows(i)("Milestones4").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate4").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals5").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals5").ToString & "</td><td>" & DT.Rows(i)("Milestones5").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate5").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals6").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals6").ToString & "</td><td>" & DT.Rows(i)("Milestones6").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate6").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals7").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals7").ToString & "</td><td>" & DT.Rows(i)("Milestones7").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate7").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals8").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals8").ToString & "</td><td>" & DT.Rows(i)("Milestones8").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate8").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals9").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals9").ToString & "</td><td>" & DT.Rows(i)("Milestones9").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate9").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals10").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals10").ToString & "</td><td>" & DT.Rows(i)("Milestones10").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate10").ToString & "</td></tr>")
            Response.Write("</table></td></tr>")

            '--Guild Confirmed 
            If CDbl(DT.Rows(i)("Process_Flag").ToString) = 5 Then
                Response.Write("<tr><td colspan=6 style=color:#00AE4D;font-family:Calibri;font-size:12pt;font-weight:bold;text-align:center;>Reviewed and Confirmed on " & DT.Rows(i)("Date_Guild_Reviewed").ToString & "</td></tr>")
            End If

            Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")


            SQL1 = "select * from(select (select last+' '+first from id_tbl where emplid=A.sup_emplid)Created,* from(select distinct sup_emplid,emplid EMPLID_History,Goals Goals_History,Milestones Milestones_History,TargetDate "
            SQL1 &= " TargetDate_History,Max(Guild_Approved)Guild_Approved from GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL A group by sup_emplid,emplid,Goals,Milestones,TargetDate)A where Guild_Approved not in (select distinct "
            SQL1 &= " Date_Guild_Reviewed from GUILD_Appraisal_FutureGoal_Master_TBL where emplid=a.EMPLID_History))B where EMPLID_History=" & DT.Rows(i)("EMPLID").ToString
            'Response.Write(SQL1)
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            SQL2 = "select count(*)CNT_History from(select distinct emplid EMPLID_History,Goals Goals_History,Milestones Milestones_History,TargetDate TargetDate_History,"
            SQL2 &= " Max(Guild_Approved)Guild_Approved from GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL A group by sup_emplid,emplid,Goals,Milestones,TargetDate)A where "
            SQL2 &= " Guild_Approved not in (select distinct Date_Guild_Reviewed from GUILD_Appraisal_FutureGoal_Master_TBL where emplid=a.EMPLID_History)"
            SQL2 &= " and EMPLID_History=" & DT.Rows(i)("EMPLID").ToString
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)

            If DT2.Rows(0)("CNT_History").ToString > 0 Then
                Response.Write("<tr><td colspan=6 align=center><font size=4px color=#00AE4D><b><u>Previous Goals record</u></b></font></td></tr>")
                Response.Write("<tr><td colspan=6><table width=100% border=1 cellpadding=0 cellspacing=0>")

                Response.Write("<tr><td align=center><b>Reviewed Date</b></td><td align=center><b>Goals</b></td><td align=center><b>Key Results</b></td><td align=center><b>Target<br/>Completion Date</b></td><td align=center><b>Manager</b></td></tr>")

                For a = 0 To DT1.Rows.Count - 1
                    Response.Write("<tr><td nowrap=nowrap>&nbsp;" & DT1.Rows(a)("Guild_Approved").ToString & "&nbsp;</td>")
                    Response.Write("<td>&nbsp;" & DT1.Rows(a)("Goals_History").ToString & "&nbsp;</td>")
                    Response.Write("<td>&nbsp;" & DT1.Rows(a)("Milestones_History").ToString & "&nbsp;</td>")
                    Response.Write("<td>&nbsp;" & DT1.Rows(a)("TargetDate_History").ToString & "&nbsp;</td>")
                    Response.Write("<td nowrap=nowrap>&nbsp;" & DT1.Rows(a)("Created").ToString & "&nbsp;</td></tr>")
                Next
                Response.Write("</table></td></tr>")
            End If

            Response.Write("<tr><td colspan=6><hr></td></tr>")

            Response.Write("<td colspan=6>&nbsp;</td></tr></table>")

            Response.Write("</td></tr></table>")




            Response.Write("")
            Response.Write("")
            Response.Write("</td>")
            Response.Write("")


            Response.Write("<td style=width:50px;>&nbsp;</td></tr></table>")
        Next



        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub DisplayData_New()
        Dim a As Integer

        SQL = "select A.*,(select First+' '+Last from id_tbl where emplid=A.emplid)EMP_Name,,(select First+' '+Last from id_tbl where emplid=A.SUP_EMPLID)SUP_Name,(select First+' '+Last from id_tbl where emplid=A.UP_MGT_EMPLID)UP_MGT_Name,"""
        SQL &= " (select First+' '+Last from id_tbl where emplid=A.HR_EMPLID)HR_Name,(select Last from id_tbl where emplid=A.emplid)EMP_LastName,(select convert(char(10),Hire_date,101) from id_tbl where emplid=A.emplid)Hired,"
        SQL &= " IsNull(Goals1,'')Goals1,IsNull(Milestones1,'')Milestones1,IsNull(TargetDate1,'')TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2,"
        SQL &= " IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4,"
        SQL &= " IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6,"
        SQL &= " IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,IsNull(TargetDate8,'')TargetDate8,"
        SQL &= " IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10"
        SQL &= " from (select emplid,mgt_emplid,(select first+' '+last from id_tbl where emplid=hr_emplid)HR_Name,hr_emplid,"
        SQL &= " IsNull(Comments,'')Comments from Appraisal_FutureGoals_Master_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " )A "
        SQL &= " Left Join (select MGT_Emplid,emplid,Rtrim(Ltrim(Goals))Goals1,Rtrim(Ltrim(Milestones))Milestones1,Rtrim(Ltrim(TargetDate))TargetDate1 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & "  and IndexID=1)A1 on a.emplid=a1.emplid"
        SQL &= " Left Join (select emplid,Rtrim(Ltrim(Goals))Goals2,Rtrim(Ltrim(Milestones))Milestones2,Rtrim(Ltrim(TargetDate))TargetDate2 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " and IndexID=2)B on a.emplid=b.emplid"
        SQL &= " Left Join (select emplid,Rtrim(Ltrim(Goals))Goals3,Rtrim(Ltrim(Milestones))Milestones3,Rtrim(Ltrim(TargetDate))TargetDate3 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " and IndexID=3)C on a.emplid=c.emplid"
        SQL &= " Left Join (select emplid,Rtrim(Ltrim(Goals))Goals4,Rtrim(Ltrim(Milestones))Milestones4,Rtrim(Ltrim(TargetDate))TargetDate4 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " and IndexID=4)D on a.emplid=d.emplid"
        SQL &= " Left Join (select emplid,Rtrim(Ltrim(Goals))Goals5,Rtrim(Ltrim(Milestones))Milestones5,Rtrim(Ltrim(TargetDate))TargetDate5 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " and IndexID=5)E on a.emplid=e.emplid"
        SQL &= " Left Join (select emplid,Rtrim(Ltrim(Goals))Goals6,Rtrim(Ltrim(Milestones))Milestones6,Rtrim(Ltrim(TargetDate))TargetDate6 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " and IndexID=6)F on a.emplid=f.emplid"
        SQL &= " Left Join (select emplid,Rtrim(Ltrim(Goals))Goals7,Rtrim(Ltrim(Milestones))Milestones7,Rtrim(Ltrim(TargetDate))TargetDate7 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " and IndexID=7)G on a.emplid=g.emplid"
        SQL &= " Left Join (select emplid,Rtrim(Ltrim(Goals))Goals8,Rtrim(Ltrim(Milestones))Milestones8,Rtrim(Ltrim(TargetDate))TargetDate8 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " and IndexID=8)H on a.emplid=h.emplid"
        SQL &= " Left Join (select emplid,Rtrim(Ltrim(Goals))Goals9,Rtrim(Ltrim(Milestones))Milestones9,Rtrim(Ltrim(TargetDate))TargetDate9 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " and IndexID=9)N on a.emplid=n.emplid"
        SQL &= " Left Join (select emplid,Rtrim(Ltrim(Goals))Goals10,Rtrim(Ltrim(Milestones))Milestones10,Rtrim(Ltrim(TargetDate))TargetDate10 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & Trim(Request.QueryString("Token").ToString) & " and IndexID =10)K on A.emplid=K.emplid"

        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        For i = 0 To DT.Rows.Count - 1

            Response.Write("<table border=0 class=StyleBreak style=width:100%><tr><td style=width:50px;>&nbsp;</td>")
            Response.Write("<td>")
            Response.Write("<table border=0 style=width:100%>")
            Response.Write("<tr><td style=text-align:center;><img alt=../../../images/CR_logo.png src=../../../images/CR_logo.png style=width:380px; height:60px runat=server/></td></tr>")
            Response.Write("<tr><td class=Style1><u>Goal-Setting Form - 10/01/15 - 05/31/16</u></td></tr>")
            Response.Write("<tr><td>")

            Response.Write("<table border=0 style=width:100%>")

            Response.Write("<tr><td class=style4><b>Name:</b></td><td class=style7>" & DT.Rows(i)("EMP_Name").ToString & "</td>")
            Response.Write("<td class=style12><b>Manager Name:</b></td><td class=style8>" & DT.Rows(i)("SUP_Name").ToString & "</td>")
            Response.Write("<td style=text-align:right; font-family:Calibri;>&nbsp; Approved:&nbsp;</td>")

            '1--Approved by Manager--
            If CDbl(DT.Rows(i)("Process_Flag").ToString) >= 1 Then
                Response.Write("<td>" & DT.Rows(i)("Date_Submitted_To_UP_MGT").ToString & "</td>")
            Else
                Response.Write("<td>&nbsp;</td>")
            End If
            Response.Write("</tr>")
            '--1END--

            Response.Write("<tr><td class=style4><b>Title:</b></td><td class=style7>" & DT.Rows(i)("title").ToString & "</td>")
            Response.Write("<td class=style12><b>2nd Level Manager Name:&nbsp;</b></td><td class=style8>" & DT.Rows(i)("up_mgt_name").ToString & "</td>")

            Response.Write("<td style=text-align:right; width:100px; font-family:Calibri;>Approved:&nbsp;</td>")
            '2--Approved by Second Manager--
            If CDbl(DT.Rows(i)("Process_Flag").ToString) >= 2 Then
                Response.Write("<td>" & DT.Rows(i)("Date_Submitted_To_HR").ToString & "</td>")
            Else
                Response.Write("<td>&nbsp;</td>")
            End If
            Response.Write("</tr>")
            '--2END--

            Response.Write("<tr><td class=style4><b>Department:</b></td><td class=style7>" & DT.Rows(i)("Department").ToString & "</td>")
            Response.Write("<td class=style12><b>Human Resources Generalist:&nbsp;</b></td><td class=style8>" & DT.Rows(i)("HR_Name").ToString & "</td>")

            Response.Write("<td style=text-align:right; font-family:Calibri;>Approved:&nbsp;</td>")
            '3.--Reviewed by HR-- 
            If CDbl(DT.Rows(i)("Process_Flag").ToString) >= 3 Then
                Response.Write("<td>" & DT.Rows(i)("Date_HR_Approved").ToString & "</td>")
            Else
                Response.Write("<td>&nbsp;</td>")
            End If
            Response.Write("</tr>")
            '--3END--


            Response.Write("<tr><td class=style4><b>Hire Date:</b></td><td class=style7 colspan=4>" & DT.Rows(i)("Hired").ToString & "</td><td>&nbsp;</td></tr>")

            Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")
            Response.Write("<tr><td colspan=6><font size=4px color=#00AE4D><b><u>Rating Criteria:</u></b></font></td></tr>")
            Response.Write("<tr><td colspan=6> Enter in the Key Task and description information in the space provided below. Key Task information should be extracted from employee's job description.</td></tr>")
            Response.Write("<tr><td colspan=6><table width=100% border=1 cellpadding=0 cellspacing=0 >")
            If Len(DT.Rows(i)("Task1").ToString) > 2 Then Response.Write("<tr><td class=style15>1) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task1").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task2").ToString) > 2 Then Response.Write("<tr><td class=style15>2) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task2").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task3").ToString) > 2 Then Response.Write("<tr><td class=style15>3) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task3").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task4").ToString) > 2 Then Response.Write("<tr><td class=style15>4) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task4").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task5").ToString) > 2 Then Response.Write("<tr><td class=style15>5) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task5").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task6").ToString) > 2 Then Response.Write("<tr><td class=style15>6) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task6").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task7").ToString) > 2 Then Response.Write("<tr><td class=style15>7) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task7").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task8").ToString) > 2 Then Response.Write("<tr><td class=style15>8) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task8").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task9").ToString) > 2 Then Response.Write("<tr><td class=style15>9) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task9").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task10").ToString) > 2 Then Response.Write("<tr><td class=style15>10) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task10").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task11").ToString) > 2 Then Response.Write("<tr><td class=style15>11) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task11").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task12").ToString) > 2 Then Response.Write("<tr><td class=style15>12) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task12").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task13").ToString) > 2 Then Response.Write("<tr><td class=style15>13) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task13").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task14").ToString) > 2 Then Response.Write("<tr><td class=style15>14) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task14").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task15").ToString) > 2 Then Response.Write("<tr><td class=style15>15) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task15").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task16").ToString) > 2 Then Response.Write("<tr><td class=style15>16) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task16").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task17").ToString) > 2 Then Response.Write("<tr><td class=style15>17) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task17").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task18").ToString) > 2 Then Response.Write("<tr><td class=style15>18) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task18").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task19").ToString) > 2 Then Response.Write("<tr><td class=style15>19) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task19").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Task20").ToString) > 2 Then Response.Write("<tr><td class=style15>20) Key Task Description:</td><td colspan=5>" & DT.Rows(i)("Task20").ToString & "</td></tr>")

            Response.Write("</table></td></tr>")

            Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")
            Response.Write("<tr><td colspan=6><font size=4px color=#00AE4D><b><u>SMART Goals:</u></b></font></td></tr>")
            Response.Write("<tr><td colspan=6> Enter SMART Goals (<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to focus employees on achieving important, tangible outcomes that contribute to the achievement of CR&#39;s Enterprise Goals. Summarize the goals agreed to with the employee.")
            If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 > "17" Then
                Response.Write(" Please click <font color=blue><u>here</u></font> for a SMART goal example.")
            End If

            Response.Write("<tr><td colspan=6><table width=100% border=1 cellpadding=0 cellspacing=0>")

            'Response.Write("<tr><td width=45% align=center><b>Goals</b></td><td width=45% align=center><b>Key Results</b></td><td width=10% align=center><b>Target<br/>Completion Date</b></td></tr>")
            If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 = "17" Then
                Response.Write("<td class=style10 width=45%><b>Goals</b></td>")
                Response.Write("<td class=style10 width:40%;><b>Success Measures or Milestones</b></td>")
                Response.Write("<td class=style10><b>Target<br>Completion Date</b></td></tr>")
            Else
                Response.Write("<td class=style10 width=45%><b>Goals</b><div style=font-size:small; font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td>")
                Response.Write("<td class=style10 width:40%;><b>Key Results</b><div style=font-size:small;font-weight:lighter;font-style:italic>How will I know that I’ve accomplished each goal?</div> </td>")
                Response.Write("<td class=style10><b>Target<br>Completion Date</b><div style=font-size:small; font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td></tr>")
            End If


            If Len(DT.Rows(i)("Goals1").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals1").ToString & "</td><td>" & DT.Rows(i)("Milestones1").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate1").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals2").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals2").ToString & "</td><td>" & DT.Rows(i)("Milestones2").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate2").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals3").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals3").ToString & "</td><td>" & DT.Rows(i)("Milestones3").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate3").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals4").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals4").ToString & "</td><td>" & DT.Rows(i)("Milestones4").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate4").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals5").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals5").ToString & "</td><td>" & DT.Rows(i)("Milestones5").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate5").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals6").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals6").ToString & "</td><td>" & DT.Rows(i)("Milestones6").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate6").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals7").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals7").ToString & "</td><td>" & DT.Rows(i)("Milestones7").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate7").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals8").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals8").ToString & "</td><td>" & DT.Rows(i)("Milestones8").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate8").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals9").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals9").ToString & "</td><td>" & DT.Rows(i)("Milestones9").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate9").ToString & "</td></tr>")
            If Len(DT.Rows(i)("Goals10").ToString) > 2 Then Response.Write("<tr><td>" & DT.Rows(i)("Goals10").ToString & "</td><td>" & DT.Rows(i)("Milestones10").ToString & "</td><td width=10% align=center>" & DT.Rows(i)("TargetDate10").ToString & "</td></tr>")
            Response.Write("</table></td></tr>")

            '--Guild Confirmed 
            If CDbl(DT.Rows(i)("Process_Flag").ToString) = 5 Then
                Response.Write("<tr><td colspan=6 style=color:#00AE4D;font-family:Calibri;font-size:12pt;font-weight:bold;text-align:center;>Reviewed and Confirmed on " & DT.Rows(i)("Date_Guild_Reviewed").ToString & "</td></tr>")
            End If

            Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")


            SQL1 = "select * from(select (select last+' '+first from id_tbl where emplid=A.sup_emplid)Created,* from(select distinct sup_emplid,emplid EMPLID_History,Goals Goals_History,Milestones Milestones_History,TargetDate "
            SQL1 &= " TargetDate_History,Max(Guild_Approved)Guild_Approved from GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL A group by sup_emplid,emplid,Goals,Milestones,TargetDate)A where Guild_Approved not in (select distinct "
            SQL1 &= " Date_Guild_Reviewed from GUILD_Appraisal_FutureGoal_Master_TBL where emplid=a.EMPLID_History))B where EMPLID_History=" & DT.Rows(i)("EMPLID").ToString
            'Response.Write(SQL1)
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            SQL2 = "select count(*)CNT_History from(select distinct emplid EMPLID_History,Goals Goals_History,Milestones Milestones_History,TargetDate TargetDate_History,"
            SQL2 &= " Max(Guild_Approved)Guild_Approved from GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL A group by sup_emplid,emplid,Goals,Milestones,TargetDate)A where "
            SQL2 &= " Guild_Approved not in (select distinct Date_Guild_Reviewed from GUILD_Appraisal_FutureGoal_Master_TBL where emplid=a.EMPLID_History)"
            SQL2 &= " and EMPLID_History=" & DT.Rows(i)("EMPLID").ToString
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)

            If DT2.Rows(0)("CNT_History").ToString > 0 Then
                Response.Write("<tr><td colspan=6 align=center><font size=4px color=#00AE4D><b><u>Previous Goals record</u></b></font></td></tr>")
                Response.Write("<tr><td colspan=6><table width=100% border=1 cellpadding=0 cellspacing=0>")

                Response.Write("<tr><td align=center><b>Reviewed Date</b></td><td align=center><b>Goals</b></td><td align=center><b>Key Results</b></td><td align=center><b>Target<br/>Completion Date</b></td><td align=center><b>Manager</b></td></tr>")

                For a = 0 To DT1.Rows.Count - 1
                    Response.Write("<tr><td nowrap=nowrap>&nbsp;" & DT1.Rows(a)("Guild_Approved").ToString & "&nbsp;</td>")
                    Response.Write("<td>&nbsp;" & DT1.Rows(a)("Goals_History").ToString & "&nbsp;</td>")
                    Response.Write("<td>&nbsp;" & DT1.Rows(a)("Milestones_History").ToString & "&nbsp;</td>")
                    Response.Write("<td>&nbsp;" & DT1.Rows(a)("TargetDate_History").ToString & "&nbsp;</td>")
                    Response.Write("<td nowrap=nowrap>&nbsp;" & DT1.Rows(a)("Created").ToString & "&nbsp;</td></tr>")
                Next
                Response.Write("</table></td></tr>")
            End If

            Response.Write("<tr><td colspan=6><hr></td></tr>")

            Response.Write("<td colspan=6>&nbsp;</td></tr></table>")

            Response.Write("</td></tr></table>")

            Response.Write("")
            Response.Write("")
            Response.Write("</td>")
            Response.Write("")

            Response.Write("<td style=width:50px;>&nbsp;</td></tr></table>")
        Next


        LocalClass.CloseSQLServerConnection()



    End Sub
End Class