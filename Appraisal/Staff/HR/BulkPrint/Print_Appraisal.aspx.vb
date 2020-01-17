Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Public Class Print_Appraisal
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim Msg, Msg1, Msg2, Msg3, Msg4 As String
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView

    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer


    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Write(Request.QueryString("Token") & "<br> LEN " & Len(Request.QueryString("Token")))
        'Response.Write(Session("Year")) : Response.End()

        If Session("NETID") = "" Then
            Response.Redirect("default.aspx")
        End If

        SQL1 = "select emplid from id_tbl where netid='" & Trim(Session("NETID")) & "' "
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        Session("EMPLID") = DT1.Rows(0)("emplid").ToString
        'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8) & "<br>" & Session("NAME") & " --  " & Session("EMPLID")) : Response.End()
        If Left(Request.QueryString("Token"), 3) = "GLD" Then
            If Session("YEAR") < 2016 Then
                Guild_Appraisal()
            ElseIf Session("YEAR") = 2016 Or Session("YEAR") = 2017 Then
                Guild_Appraisal_New()
            Else
                'Response.Write("<br/><br/><br/><br/><br/><br/><center><img src=../../../images/UnderDevelopment.png />")
                Guild_Appraisal_18Forwad()
            End If

        ElseIf Left(Request.QueryString("Token"), 3) = "MGT" Then
            If Session("YEAR") < 2016 Then
                Manager_Appraisal()
            ElseIf Session("YEAR") = 2016 Or Session("YEAR") = 2017 Then
                Manager_Appraisal_New()
            Else
                'Response.Write("<br/><br/><br/><br/><br/><br/><center><img src=../../../images/UnderDevelopment.png />")
                Manager_Appraisal_18Forwad()
            End If


        ElseIf Left(Request.QueryString("Token"), 3) = "IND" Then
            If Session("YEAR") < 2016 Then
                Individual_Appraisal()
            ElseIf Session("YEAR") = 2016 Or Session("YEAR") = 2017 Then
                Individual_Appraisal_New()
            Else
                Individual_Appraisal_18Forwad()
                'Response.Write("<br/><br/><br/><br/><br/><br/><center><img src=../../../images/UnderDevelopment.png />")
            End If


        ElseIf Left(Request.QueryString("Token"), 3) = "TER" Then
            If Session("YEAR") < 2016 Then
                Terminated_Appraisal()
            ElseIf Session("YEAR") = 2016 Or Session("YEAR") = 2017 Then
                Terminated_Appraisal_New()
            Else
                Response.Write("<br/><br/><br/><br/><br/><br/><center><img src=../../../images/UnderDevelopment.png />")
            End If


        End If

            LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Guild_Appraisal()

        'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8)) : Response.End()
        '--Guild information---
        SQL1 = "select (select last from id_tbl where emplid=a.emplid)LastName, * from Guild_Appraisal_Print_tbl A "

        If Len(Mid(Request.QueryString("Token"), 4, 8)) = 4 Then
            SQL1 = SQL1 & " where HR_EMPLID =" & Mid(Request.QueryString("Token"), 4, 8)
        ElseIf Len(Mid(Request.QueryString("Token"), 4, 8)) = 7 Then
            SQL1 = SQL1 & "  where deptid1 =" & Mid(Request.QueryString("Token"), 4, 11)
        ElseIf Len(Mid(Request.QueryString("Token"), 11, 15)) = 4 Then
            SQL1 = SQL1 & "  where SUP_EMPLID =" & Mid(Request.QueryString("Token"), 11, 15)
        End If
        SQL1 = SQL1 & "  order by LastName"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        '---Effectiveness Factor---
        '1.  Autonomy
        '2.  Critical Judgment
        '3.  Dependability
        '4.  Flexibility
        '5.  Follow Through
        '6.  Quantity
        '7.  Self Motivation
        '8.  Style & Use of language
        '9.  Teamwork
        '10. Timeliness
        '11. Trust, Civility & Respect

        For i = 0 To DT1.Rows.Count - 1

            Response.Write("<table border=0 class=StyleBreak width=100%>")
            Response.Write("<tr><td width=15%>&nbsp;</td><td style= text-align:center><img src=../../../images/CR_logo.png style=width:380px; height:50px/></td><td width=15%>&nbsp;</td></tr>")
            Response.Write("<tr><td style=Style3>&nbsp;</td><td class=Style1>Performance Appraisal</td><td>&nbsp;</td></tr>")
            Response.Write("<tr><td style=Style3>&nbsp;</td><td class=Style2> October 1, 2014&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;September 30, 2015</td><td>&nbsp;</td></tr>")
            Response.Write("<tr><td>&nbsp;</td><td>")

            Response.Write("<center><table border=0 style=Style0><tr><td width=3%>&nbsp;&nbsp;</td>")
            '--Column #1-2
            Response.Write("<td width=8%>Name:</td><td width=25%>" & DT1.Rows(i)("Guild_Name").ToString & "</td>")
            Response.Write("<td width=18%>Manager Name:</td><td>" & DT1.Rows(i)("SUP_NAME").ToString & "</td>")
            Response.Write("<td>Approved:&nbsp;&nbsp;" & DT1.Rows(i)("Mgr_Apr").ToString & "</td></tr>")
            Response.Write("<tr><td width=3%>&nbsp;&nbsp;</td>")
            '--Column #1-2
            Response.Write("<td>Title:</td><td>" & DT1.Rows(i)("jobtitle1").ToString & "</td>")
            Response.Write("<td nowrap=nowrap>2nd Level Manager Name:&nbsp;</td><td width=15%>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td>")
            Response.Write("<td>Approved:&nbsp;&nbsp;" & DT1.Rows(i)("UP_Mgt_Apr").ToString & "</td></tr>")
            Response.Write("<tr><td width=3%>&nbsp;&nbsp;</td>")
            '--Column #1-2
            Response.Write("<td>Department:</td><td>" & DT1.Rows(i)("Departname").ToString & "</td>")
            Response.Write("<td>HR Generalist:</td><td>" & DT1.Rows(i)("HR_NAME").ToString & "</td><td>Approved:&nbsp;&nbsp;" & DT1.Rows(i)("HR_Apr").ToString & "</td></tr>")
            Response.Write("<tr><td width=3%>&nbsp;&nbsp;</td>")
            '--Column #1-2
            Response.Write("<td>Hire Date:</td><td>" & DT1.Rows(i)("Hired").ToString & "</td>")
            Response.Write("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>")
            Response.Write("<tr><td colspan=6>&nbsp;</td></tr></table><td style=Style3>&nbsp;</td>")
            Response.Write("<tr><td style=Style3>&nbsp;</td><td style=Style4><font color=red><b><u>Key Tasks:</u></b></font>&nbsp;<i>Enter in the Key Task and description information in the space provided below. Key Task information should be extracted from employee's job description. Rate each key task using the effectiveness factor that best relates to the duties of each individual key task. The effectiveness factors may be rated by entering a check mark.</i></td><td>&nbsp;</td></tr>")
            Response.Write("<tr><td style=Style3>&nbsp;</td><td style=Style4>")

            '--Start Tasks#1 table
            Response.Write("<table width=100% border=0 style=font-family:Calibri>")
            Response.Write("<tr><td width=3%></td><td><font color=red><b><u>1) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
            '--Grid
            Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
            Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
            Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

            Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
            'E_Aut_1 H_Aut_1 M_Aut_1 N_Aut_1 U_Aut_1 
            If DT1.Rows(i)("E_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
            'E_Cri_1 H_Cri_1 M_Cri_1 N_Cri_1 U_Cri_1  
            If DT1.Rows(i)("E_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
            'E_Dep_1 H_Dep_1 M_Dep_1 N_Dep_1 U_Dep_1 
            If DT1.Rows(i)("E_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
            'E_Fle_1 H_Fle_1 M_Fle_1 N_Fle_1 U_Fle_1 
            If DT1.Rows(i)("E_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
            'E_Fol_1 H_Fol_1 M_Fol_1 N_Fol_1 U_Fol_1 
            If DT1.Rows(i)("E_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
            'E_Qua_1 H_Qua_1 M_Qua_1 N_Qua_1 U_Qua_1 
            If DT1.Rows(i)("E_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
            'E_Sel_1 H_Sel_1 M_Sel_1 N_Sel_1 U_Sel_1 
            If DT1.Rows(i)("E_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
            'E_Sty_1 H_Sty_1 M_Sty_1 N_Sty_1 U_Sty_1 
            If DT1.Rows(i)("E_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
            'E_Tea_1 H_Tea_1 M_Tea_1 N_Tea_1 U_Tea_1 
            If DT1.Rows(i)("E_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
            'E_Tim_1 H_Tim_1 M_Tim_1 N_Tim_1 U_Tim_1 
            If DT1.Rows(i)("E_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
            'E_Tru_1 H_Tru_1 M_Tru_1 N_Tru_1 U_Tru_1 
            If DT1.Rows(i)("E_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("H_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("M_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("N_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
            If DT1.Rows(i)("U_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
            Response.Write("</table></td></tr>")
            '--End Grid
            Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating1").ToString) & "</font></b></td></tr>")
            Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
            Response.Write("</table>")
            '--End  Tasks#1 table


            '--Start Tasks#2 table
            If CDbl(Len(DT1.Rows(i)("TaskDesc2").ToString)) > 0 Then
                Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                Response.Write("<tr><td width=3%></td><td><font color=red><b><u>2) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                '--Grid
                Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                'E_Aut_2 H_Aut_2 M_Aut_2 N_Aut_2 U_Aut_2 
                If DT1.Rows(i)("E_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                'E_Cri_2 H_Cri_2 M_Cri_2 N_Cri_2 U_Cri_2  
                If DT1.Rows(i)("E_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                'E_Dep_2 H_Dep_2 M_Dep_2 N_Dep_2 U_Dep_2 
                If DT1.Rows(i)("E_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                'E_Fle_2 H_Fle_2 M_Fle_2 N_Fle_2 U_Fle_2 
                If DT1.Rows(i)("E_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                'E_Fol_2 H_Fol_2 M_Fol_2 N_Fol_2 U_Fol_2 
                If DT1.Rows(i)("E_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                'E_Qua_2 H_Qua_2 M_Qua_2 N_Qua_2 U_Qua_2 
                If DT1.Rows(i)("E_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                'E_Sel_2 H_Sel_2 M_Sel_2 N_Sel_2 U_Sel_2 
                If DT1.Rows(i)("E_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                'E_Sty_2 H_Sty_2 M_Sty_2 N_Sty_2 U_Sty_2 
                If DT1.Rows(i)("E_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                'E_Tea_2 H_Tea_2 M_Tea_2 N_Tea_2 U_Tea_2 
                If DT1.Rows(i)("E_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                'E_Tim_2 H_Tim_2 M_Tim_2 N_Tim_2 U_Tim_2 
                If DT1.Rows(i)("E_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                'E_Tru_2 H_Tru_2 M_Tru_2 N_Tru_2 U_Tru_2 
                If DT1.Rows(i)("E_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("</table></td></tr>")
                '--End Grid
                Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating2").ToString) & "</font></b></td></tr>")
                Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                Response.Write("</table>")
                '--End  Tasks#2 table
            End If

            '--Start Tasks#3 table
            If CDbl(Len(DT1.Rows(i)("TaskDesc3").ToString)) > 0 Then
                Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                Response.Write("<tr><td width=3%></td><td><font color=red><b><u>3) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                '--Grid
                Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                'E_Aut_3 H_Aut_3 M_Aut_3 N_Aut_3 U_Aut_3 
                If DT1.Rows(i)("E_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                'E_Cri_3 H_Cri_3 M_Cri_3 N_Cri_3 U_Cri_3  
                If DT1.Rows(i)("E_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                'E_Dep_3 H_Dep_3 M_Dep_3 N_Dep_3 U_Dep_3 
                If DT1.Rows(i)("E_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                'E_Fle_3 H_Fle_3 M_Fle_3 N_Fle_3 U_Fle_3 
                If DT1.Rows(i)("E_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                'E_Fol_3 H_Fol_3 M_Fol_3 N_Fol_3 U_Fol_3 
                If DT1.Rows(i)("E_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                'E_Qua_3 H_Qua_3 M_Qua_3 N_Qua_3 U_Qua_3 
                If DT1.Rows(i)("E_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                'E_Sel_3 H_Sel_3 M_Sel_3 N_Sel_3 U_Sel_3 
                If DT1.Rows(i)("E_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                'E_Sty_3 H_Sty_3 M_Sty_3 N_Sty_3 U_Sty_3 
                If DT1.Rows(i)("E_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                'E_Tea_3 H_Tea_3 M_Tea_3 N_Tea_3 U_Tea_3 
                If DT1.Rows(i)("E_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                'E_Tim_3 H_Tim_3 M_Tim_3 N_Tim_3 U_Tim_3 
                If DT1.Rows(i)("E_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                'E_Tru_3 H_Tru_3 M_Tru_3 N_Tru_3 U_Tru_3 
                If DT1.Rows(i)("E_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("</table></td></tr>")
                '--End Grid
                Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating3").ToString) & "</font></b></td></tr>")
                Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                Response.Write("</table>")
                '--End  Tasks#3 table
            End If

            '--Start Tasks#4 table
            If CDbl(Len(DT1.Rows(i)("TaskDesc4").ToString)) > 0 Then
                Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                Response.Write("<tr><td width=3%></td><td><font color=red><b><u>4) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                '--Grid
                Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                'E_Aut_4 H_Aut_4 M_Aut_4 N_Aut_4 U_Aut_4 
                If DT1.Rows(i)("E_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                'E_Cri_4 H_Cri_4 M_Cri_4 N_Cri_4 U_Cri_4  
                If DT1.Rows(i)("E_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                'E_Dep_4 H_Dep_4 M_Dep_4 N_Dep_4 U_Dep_4 
                If DT1.Rows(i)("E_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                'E_Fle_4 H_Fle_4 M_Fle_4 N_Fle_4 U_Fle_4 
                If DT1.Rows(i)("E_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                'E_Fol_4 H_Fol_4 M_Fol_4 N_Fol_4 U_Fol_4 
                If DT1.Rows(i)("E_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                'E_Qua_4 H_Qua_4 M_Qua_4 N_Qua_4 U_Qua_4 
                If DT1.Rows(i)("E_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                'E_Sel_4 H_Sel_4 M_Sel_4 N_Sel_4 U_Sel_4 
                If DT1.Rows(i)("E_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                'E_Sty_4 H_Sty_4 M_Sty_4 N_Sty_4 U_Sty_4 
                If DT1.Rows(i)("E_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                'E_Tea_4 H_Tea_4 M_Tea_4 N_Tea_4 U_Tea_4 
                If DT1.Rows(i)("E_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                'E_Tim_4 H_Tim_4 M_Tim_4 N_Tim_4 U_Tim_4 
                If DT1.Rows(i)("E_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                'E_Tru_4 H_Tru_4 M_Tru_4 N_Tru_4 U_Tru_4 
                If DT1.Rows(i)("E_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If

                Response.Write("</table></td></tr>")
                '--End Grid
                Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating4").ToString) & "</font></b></td></tr>")
                Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                Response.Write("</table>")
                '--End  Tasks#4 table
            End If

            '--Start Tasks#5 table
            If CDbl(Len(DT1.Rows(i)("TaskDesc5").ToString)) > 0 Then
                Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                Response.Write("<tr><td width=3%></td><td><font color=red><b><u>5) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                '--Grid
                Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                'E_Aut_5 H_Aut_5 M_Aut_5 N_Aut_5 U_Aut_5 
                If DT1.Rows(i)("E_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                'E_Cri_5 H_Cri_5 M_Cri_5 N_Cri_5 U_Cri_5  
                If DT1.Rows(i)("E_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                'E_Dep_5 H_Dep_5 M_Dep_5 N_Dep_5 U_Dep_5 
                If DT1.Rows(i)("E_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                'E_Fle_5 H_Fle_5 M_Fle_5 N_Fle_5 U_Fle_5 
                If DT1.Rows(i)("E_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                'E_Fol_5 H_Fol_5 M_Fol_5 N_Fol_5 U_Fol_5 
                If DT1.Rows(i)("E_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                'E_Qua_5 H_Qua_5 M_Qua_5 N_Qua_5 U_Qua_5 
                If DT1.Rows(i)("E_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                'E_Sel_5 H_Sel_5 M_Sel_5 N_Sel_5 U_Sel_5 
                If DT1.Rows(i)("E_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                'E_Sty_5 H_Sty_5 M_Sty_5 N_Sty_5 U_Sty_5 
                If DT1.Rows(i)("E_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                'E_Tea_5 H_Tea_5 M_Tea_5 N_Tea_5 U_Tea_5 
                If DT1.Rows(i)("E_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                'E_Tim_5 H_Tim_5 M_Tim_5 N_Tim_5 U_Tim_5 
                If DT1.Rows(i)("E_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                'E_Tru_5 H_Tru_5 M_Tru_5 N_Tru_5 U_Tru_5 
                If DT1.Rows(i)("E_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("</table></td></tr>")
                '--End Grid
                Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating5").ToString) & "</font></b></td></tr>")
                Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                Response.Write("</table>")
                '--End  Tasks#5 table
            End If

            '--Start Tasks#6 table
            If CDbl(Len(DT1.Rows(i)("TaskDesc6").ToString)) > 0 Then
                Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                Response.Write("<tr><td width=3%></td><td><font color=red><b><u>6) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                '--Grid
                Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                'E_Aut_6 H_Aut_6 M_Aut_6 N_Aut_6 U_Aut_6 
                If DT1.Rows(i)("E_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                'E_Cri_6 H_Cri_6 M_Cri_6 N_Cri_6 U_Cri_6  
                If DT1.Rows(i)("E_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                'E_Dep_6 H_Dep_6 M_Dep_6 N_Dep_6 U_Dep_6 
                If DT1.Rows(i)("E_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                'E_Fle_6 H_Fle_6 M_Fle_6 N_Fle_6 U_Fle_6 
                If DT1.Rows(i)("E_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                'E_Fol_6 H_Fol_6 M_Fol_6 N_Fol_6 U_Fol_6 
                If DT1.Rows(i)("E_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                'E_Qua_6 H_Qua_6 M_Qua_6 N_Qua_6 U_Qua_6 
                If DT1.Rows(i)("E_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                'E_Sel_6 H_Sel_6 M_Sel_6 N_Sel_6 U_Sel_6 
                If DT1.Rows(i)("E_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                'E_Sty_6 H_Sty_6 M_Sty_6 N_Sty_6 U_Sty_6 
                If DT1.Rows(i)("E_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                'E_Tea_6 H_Tea_6 M_Tea_6 N_Tea_6 U_Tea_6 
                If DT1.Rows(i)("E_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                'E_Tim_6 H_Tim_6 M_Tim_6 N_Tim_6 U_Tim_6 
                If DT1.Rows(i)("E_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                'E_Tru_6 H_Tru_6 M_Tru_6 N_Tru_6 U_Tru_6 
                If DT1.Rows(i)("E_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If

                Response.Write("</table></td></tr>")
                '--End Grid
                Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating6").ToString) & "</font></b></td></tr>")
                Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                Response.Write("</table>")
                '--End  Tasks#6 table
            End If
            '--Start Tasks#7 table
            If CDbl(Len(DT1.Rows(i)("TaskDesc7").ToString)) > 0 Then
                Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                Response.Write("<tr><td width=3%></td><td><font color=red><b><u>7) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                '--Grid
                Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                'E_Aut_7 H_Aut_7 M_Aut_7 N_Aut_7 U_Aut_7 
                If DT1.Rows(i)("E_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                'E_Cri_7 H_Cri_7 M_Cri_7 N_Cri_7 U_Cri_7  
                If DT1.Rows(i)("E_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                'E_Dep_7 H_Dep_7 M_Dep_7 N_Dep_7 U_Dep_7 
                If DT1.Rows(i)("E_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                'E_Fle_7 H_Fle_7 M_Fle_7 N_Fle_7 U_Fle_7 
                If DT1.Rows(i)("E_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                'E_Fol_7 H_Fol_7 M_Fol_7 N_Fol_7 U_Fol_7 
                If DT1.Rows(i)("E_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                'E_Qua_7 H_Qua_7 M_Qua_7 N_Qua_7 U_Qua_7 
                If DT1.Rows(i)("E_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                'E_Sel_7 H_Sel_7 M_Sel_7 N_Sel_7 U_Sel_7 
                If DT1.Rows(i)("E_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                'E_Sty_7 H_Sty_7 M_Sty_7 N_Sty_7 U_Sty_7 
                If DT1.Rows(i)("E_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                'E_Tea_7 H_Tea_7 M_Tea_7 N_Tea_7 U_Tea_7 
                If DT1.Rows(i)("E_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                'E_Tim_7 H_Tim_7 M_Tim_7 N_Tim_7 U_Tim_7 
                If DT1.Rows(i)("E_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                'E_Tru_7 H_Tru_7 M_Tru_7 N_Tru_7 U_Tru_7 
                If DT1.Rows(i)("E_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("</table></td></tr>")
                '--End Grid
                Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating7").ToString) & "</font></b></td></tr>")
                Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                Response.Write("</table>")
                '--End  Tasks#7 table
            End If

            '--Start Tasks#8 table
            If CDbl(Len(DT1.Rows(i)("TaskDesc8").ToString)) > 0 Then
                Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                Response.Write("<tr><td width=3%></td><td><font color=red><b><u>8) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                '--Grid
                Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                'E_Aut_8 H_Aut_8 M_Aut_8 N_Aut_8 U_Aut_8 
                If DT1.Rows(i)("E_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                'E_Cri_8 H_Cri_8 M_Cri_8 N_Cri_8 U_Cri_8  
                If DT1.Rows(i)("E_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                'E_Dep_8 H_Dep_8 M_Dep_8 N_Dep_8 U_Dep_8 
                If DT1.Rows(i)("E_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                'E_Fle_8 H_Fle_8 M_Fle_8 N_Fle_8 U_Fle_8 
                If DT1.Rows(i)("E_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                'E_Fol_8 H_Fol_8 M_Fol_8 N_Fol_8 U_Fol_8 
                If DT1.Rows(i)("E_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                'E_Qua_8 H_Qua_8 M_Qua_8 N_Qua_8 U_Qua_8 
                If DT1.Rows(i)("E_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                'E_Sel_8 H_Sel_8 M_Sel_8 N_Sel_8 U_Sel_8 
                If DT1.Rows(i)("E_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                'E_Sty_8 H_Sty_8 M_Sty_8 N_Sty_8 U_Sty_8 
                If DT1.Rows(i)("E_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                'E_Tea_8 H_Tea_8 M_Tea_8 N_Tea_8 U_Tea_8 
                If DT1.Rows(i)("E_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                'E_Tim_8 H_Tim_8 M_Tim_8 N_Tim_8 U_Tim_8 
                If DT1.Rows(i)("E_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                'E_Tru_8 H_Tru_8 M_Tru_8 N_Tru_8 U_Tru_8 
                If DT1.Rows(i)("E_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("</table></td></tr>")
                '--End Grid
                Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating8").ToString) & "</font></b></td></tr>")
                Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                Response.Write("</table>")
                '--End  Tasks#8 table
            End If

            '--Start Tasks#9 table
            If CDbl(Len(DT1.Rows(i)("TaskDesc9").ToString)) > 0 Then
                Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                Response.Write("<tr><td width=3%></td><td><font color=red><b><u>9) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                '--Grid
                Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                'E_Aut_9 H_Aut_9 M_Aut_9 N_Aut_9 U_Aut_9 
                If DT1.Rows(i)("E_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                'E_Cri_9 H_Cri_9 M_Cri_9 N_Cri_9 U_Cri_9  
                If DT1.Rows(i)("E_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                'E_Dep_9 H_Dep_9 M_Dep_9 N_Dep_9 U_Dep_9 
                If DT1.Rows(i)("E_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                'E_Fle_9 H_Fle_9 M_Fle_9 N_Fle_9 U_Fle_9 
                If DT1.Rows(i)("E_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                'E_Fol_9 H_Fol_9 M_Fol_9 N_Fol_9 U_Fol_9 
                If DT1.Rows(i)("E_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                'E_Qua_9 H_Qua_9 M_Qua_9 N_Qua_9 U_Qua_9 
                If DT1.Rows(i)("E_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                'E_Sel_9 H_Sel_9 M_Sel_9 N_Sel_9 U_Sel_9 
                If DT1.Rows(i)("E_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                'E_Sty_9 H_Sty_9 M_Sty_9 N_Sty_9 U_Sty_9 
                If DT1.Rows(i)("E_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                'E_Tea_9 H_Tea_9 M_Tea_9 N_Tea_9 U_Tea_9 
                If DT1.Rows(i)("E_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                'E_Tim_9 H_Tim_9 M_Tim_9 N_Tim_9 U_Tim_9 
                If DT1.Rows(i)("E_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                'E_Tru_9 H_Tru_9 M_Tru_9 N_Tru_9 U_Tru_9 
                If DT1.Rows(i)("E_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If

                Response.Write("</table></td></tr>")
                '--End Grid
                Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating9").ToString) & "</font></b></td></tr>")
                Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                Response.Write("</table>")
                '--End  Tasks#9 table
            End If

            '--Start Tasks#10 table
            If CDbl(Len(DT1.Rows(i)("TaskDesc10").ToString)) > 0 Then
                Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                Response.Write("<tr><td width=3%></td><td><font color=red><b><u>10) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                '--Grid
                Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                'E_Aut_10 H_Aut_10 M_Aut_10 N_Aut_10 U_Aut_10 
                If DT1.Rows(i)("E_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                'E_Cri_10 H_Cri_10 M_Cri_10 N_Cri_10 U_Cri_10  
                If DT1.Rows(i)("E_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                'E_Dep_10 H_Dep_10 M_Dep_10 N_Dep_10 U_Dep_10 
                If DT1.Rows(i)("E_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                'E_Fle_10 H_Fle_10 M_Fle_10 N_Fle_10 U_Fle_10 
                If DT1.Rows(i)("E_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                'E_Fol_10 H_Fol_10 M_Fol_10 N_Fol_10 U_Fol_10 
                If DT1.Rows(i)("E_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                'E_Qua_10 H_Qua_10 M_Qua_10 N_Qua_10 U_Qua_10 
                If DT1.Rows(i)("E_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                'E_Sel_10 H_Sel_10 M_Sel_10 N_Sel_10 U_Sel_10 
                If DT1.Rows(i)("E_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                'E_Sty_10 H_Sty_10 M_Sty_10 N_Sty_10 U_Sty_10 
                If DT1.Rows(i)("E_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                'E_Tea_10 H_Tea_10 M_Tea_10 N_Tea_10 U_Tea_10 
                If DT1.Rows(i)("E_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                'E_Tim_10 H_Tim_10 M_Tim_10 N_Tim_10 U_Tim_10 
                If DT1.Rows(i)("E_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                'E_Tru_10 H_Tru_10 M_Tru_10 N_Tru_10 U_Tru_10 
                If DT1.Rows(i)("E_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("H_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("M_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("N_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                If DT1.Rows(i)("U_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                Response.Write("</table></td></tr>")
                '--End Grid
                Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating10").ToString) & "</font></b></td></tr>")
                Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Comments10").ToString & "</td></tr>")
                Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                Response.Write("</table></td>")
                '--End  Tasks#10 table
            End If

            Response.Write("<tr><td style=height:1px;>&nbsp;&nbsp;</td><td></td><td>&nbsp;&nbsp;</td></tr>")

            Response.Write("<tr><td>&nbsp;&nbsp;</td><td class=Style1><u>Overall Summary</u></td><td>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td>&nbsp;&nbsp;</td><td><font color=red><b><u>Overall Appraisal Rating:</u></font><font color=blue> " & getRating(DT1.Rows(i)("Overall_Rating").ToString) & "</td><td>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td>&nbsp;&nbsp;</td><td><hr color=gray></td><td>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td>&nbsp;&nbsp;</td><td><font color=red><b><u>Manager's Overall Peformance Comments:</u></b></font> " & Replace(Replace(DT1.Rows(i)("MGR_comments").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td><td>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td>&nbsp;&nbsp;</td><td><hr color=gray></td><td>&nbsp;&nbsp;</td></tr>")

            Response.Write("<tr><td>&nbsp;&nbsp;</td><td></td><td>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td>&nbsp;&nbsp;</td><td align=center><font color=red><b>" & DT1.Rows(i)("Guild_Name").ToString & " has submitted appraisal on " & DT1.Rows(i)("Esign").ToString & "</b></font></td><td>&nbsp;&nbsp;</td></tr>")

            Response.Write("</td><td>&nbsp;</td></tr></table>")

        Next

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Manager_Appraisal()
        'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8) & "<br>" & Session("NAME") & " --  " & Session("EMPLID")) : Response.End()

        If Session("EMPLID") = 6785 Or Session("EMPLID") = 6250 Or Session("EMPLID") = 6671 Then
            SQL = "select distinct last LastName,(select count(*)CNT from ps_employees where supervisor_id=A.emplid)EMPL_Supervision,last+' '+first Name, A.*,B.Accomplishment,deptid1,Departname,jobtitle1,convert(char(10),hire_date,101)Hired,"
            SQL = SQL & " (select First+' '+Last from id_tbl where emplid=Sup_EMPLID)SUP_NAME,IsNULL((select First+' '+Last from id_tbl where emplid=UP_MGT_EMPLID),'N/A')UP_MGT_NAME,IsNULL((select First+' '+Last from id_tbl where emplid=HR_EMPLID),'N/A')HR_Name,"
            SQL = SQL & " (Lead_Change+Inspire_Risk+Leverage_External+Communicate_Impact+Lead_Urgency+Promote_Collaboration+Confront_Challenges+Make_Balance+Build_Trust+Learn_Continuously)Addendum from "
            SQL = SQL & " (select * from ME_Appraisal_Master_tbl)A,(select * from ME_Appraisal_Accomplishments_TBL where perf_year=2015)B,id_tbl C where a.emplid=b.emplid and a.emplid=c.emplid and New_Employee=0"
            If Len(Mid(Request.QueryString("Token"), 4, 8)) = 4 Then
                SQL = SQL & " and HR_EMPLID =" & Mid(Request.QueryString("Token"), 4, 8) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 4, 8)) = 7 Then
                SQL = SQL & " and deptid1 =" & Mid(Request.QueryString("Token"), 4, 11) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 11, 15)) = 4 Then
                SQL = SQL & " and SUP_EMPLID =" & Mid(Request.QueryString("Token"), 11, 15)
            End If
            SQL = SQL & "  order by LastName"
        Else
            SQL = "select distinct last LastName,(select count(*)CNT from ps_employees where supervisor_id=A.emplid)EMPL_Supervision,last+' '+first Name, A.*,B.Accomplishment,deptid1,Departname,jobtitle1,convert(char(10),hire_date,101)Hired,"
            SQL = SQL & " (select First+' '+Last from id_tbl where emplid=Sup_EMPLID)SUP_NAME,IsNULL((select First+' '+Last from id_tbl where emplid=UP_MGT_EMPLID),'N/A')UP_MGT_NAME,IsNULL((select First+' '+Last from id_tbl where emplid=HR_EMPLID),'N/A')HR_Name,"
            SQL = SQL & " (Lead_Change+Inspire_Risk+Leverage_External+Communicate_Impact+Lead_Urgency+Promote_Collaboration+Confront_Challenges+Make_Balance+Build_Trust+Learn_Continuously)Addendum from "
            SQL = SQL & " (select * from ME_Appraisal_Master_tbl)A,(select * from ME_Appraisal_Accomplishments_TBL where perf_year=2015)B,id_tbl C where a.emplid=b.emplid and a.emplid=c.emplid and New_Employee=0 and deptid1 not in (9009120)"
            If Len(Mid(Request.QueryString("Token"), 4, 8)) = 4 Then
                SQL = SQL & " and HR_EMPLID =" & Mid(Request.QueryString("Token"), 4, 8) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 4, 8)) = 7 Then
                SQL = SQL & " and deptid1 =" & Mid(Request.QueryString("Token"), 4, 11) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 11, 15)) = 4 Then
                SQL = SQL & " and SUP_EMPLID =" & Mid(Request.QueryString("Token"), 11, 15)
            End If
            SQL = SQL & "  order by LastName"
        End If
        'Response.Write(SQL) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL)

        For i = 0 To DT1.Rows.Count - 1

            Response.Write("<table border=0 class=StyleBreak width=100%>")
            Response.Write("<tr><td width=10%>&nbsp;</td><td style= text-align:center><img src=../../../images/CR_logo.png style=width:380px; height:50px/></td><td width=10%>&nbsp;</td></tr>")
            Response.Write("<tr><td style=Style3>&nbsp;</td><td align=center style=color: #00ae4d;font-family:Calibri;font-size:24px;font-weight:bold;><u>FY2015 PERFORMANCE APPRAISAL</u></td><td>&nbsp;</td></tr>")
            Response.Write("<tr><td style=Style3>&nbsp;</td><td class=Style2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;</td></tr>")
            Response.Write("<tr><td>&nbsp;</td><td>")

            Response.Write("<table border=0 width=100% class=Style4>")
            '--Row 1--
            Response.Write("<tr><td width=8%><b>Name:</b></td><td width=30%>" & DT1.Rows(i)("Name").ToString & "</td><td width=18%>&nbsp;</td><td>&nbsp;</td><td><b>E-Signed:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Date_Employee_Esign").ToString & "</td></tr>")

            '--Row 2--
            Response.Write("<tr><td><b>Title:</td><td>" & DT1.Rows(i)("jobtitle1").ToString & "</td>")
            Response.Write("<td width=18%><b>Manager Name:</b></td><td>" & DT1.Rows(i)("SUP_NAME").ToString & "</td><td><b>Approved:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Date_Sent").ToString & "</td></tr>")

            '--Row 3--
            Response.Write("<tr><td><b>Department:</b></td><td>" & DT1.Rows(i)("Departname").ToString & "</td>")
            Response.Write("<td nowrap=nowrap><b>2nd Level Manager Name:&nbsp;<b></td><td width=15%>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td><td><b>Approved:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Date_Submitted_To_HR").ToString & "</td></tr>")

            '--Row 4--
            Response.Write("<tr><td><b>Hire Date:</b></td><td>" & DT1.Rows(i)("Hired").ToString & "</td>")
            Response.Write("<td><b>HR Generalist:</b></td><td>" & DT1.Rows(i)("HR_NAME").ToString & "</td><td><b>Approved:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Date_HR_Approved").ToString & "</td></tr>")

            Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")

            Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Accomplishments:</b></u></font> (Summarize accomplishments vs the goals/expectations established for FY15)</td></tr>")
            Response.Write("<tr><td colspan=6><table border=0 cellpadding=0 cellspacing=0 width=100% class=Style7><tr><td>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr></table></td></tr>")

            Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Strengths:</b></u></font> (Comment on key strengths and highlight areas performed well this year)</td></tr>")
            Response.Write("<tr><td colspan=6><table border=0 cellpadding=0 cellspacing=0 width=100% class=Style7><tr><td>" & Replace(Replace(DT1.Rows(i)("Strenghts").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr></table></td></tr>")

            Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Development Areas:</b></u></font> (Comment on areas of performance that require futher development or improvement. Cite examples of areas of performance that could have been better this year)</td></tr>")
            Response.Write("<tr><td colspan=6><table border=0 cellpadding=0 cellspacing=0 width=100% class=Style7><tr><td>" & Replace(Replace(DT1.Rows(i)("DevelopmentAreas").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr></table></td></tr>")


            Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Overall Performance Rating:</b></u></font> (Check the box that most appropriately describes the individual's overall performance)</td></tr>")

            Response.Write("<tr><td colspan=6>")
            Response.Write("<table border=1 cellpadding=0 cellspacing=0 width=100% align=center class=Style7>")

            Response.Write("<tr style=background-color:#E7E8E3;><td align=center><b>Underperforming</b></td><td align=center><b>Developing/Improving Contributor</b></td><td align=center><b>Solid Contributor</b></td>")
            Response.Write("<td align=center><b>Very Strong Contributor</b></td><td align=center><b>Distinguished Contributor</b></td></tr>")

            Response.Write("<tr>")
            If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")

            Response.Write("</tr></table></td></tr>")

            Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Overall Summary:</b></u></font> (Comment on overall performance in FY15)</td></tr>")
            Response.Write("<tr><td colspan=6><table border=0 cellpadding=0 cellspacing=0 width=100% class=Style7><tr><td>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr></table></td></tr>")
            Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")


            If CDbl(DT1.Rows(i)("EMPL_Supervision").ToString) = 0 And CDbl(DT1.Rows(i)("Addendum").ToString) > 0 Then
                'EMPL_Supervision>0 and Addendum>0

                Response.Write("<tr><td colspan=6 align=center style=color: #00ae4d;font-family:Calibri;font-size:24px;font-weight:bold;><u>Addendum - FY16 Leadership Competencies</u></td></tr>")
                Response.Write("<tr><td colspan=6>Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies. Use this assessment to provide feedback and guide your direct report on areas to focus on in FY16 for further growth and development.</td></tr>")

                Response.Write("<tr><td colspan=6><table border=1 width=100%  cellpadding=0 cellspacing=0>")
                Response.Write("<tr align=center><td width=55%><table width=100% border=0><tr><td width=40%></td><td style=font-family:Calibri;font-size:18px;font-weight:bold;>LEADING ONESELF</td></tr></table></td>")
                Response.Write("<td width=15% style=background-color:#E7E8E3;><b>Needs<br>Development/Improvement</b></td><td width=15% style=background-color:#E7E8E3;><b>Proficient</b></td><td width=15% style=background-color:#E7E8E3;><b>Excels</b></td></tr>")

                Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Make Balanced Decisions</td></tr></table></td>")
                If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;background-color:#E7E8E3;><tr><td width=45%></td><td>Build Trust</td></tr></table></td>")
                If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Learn Continuously</td></tr></table></td>")
                If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td colspan=4><table width=55%><tr><td width=40%></td><td style=font-family:Calibri;font-size:18px;font-weight:bold;>LEADING OTHERS</td></tr></table></td></tr>")
                Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Lead with Urgency & Purpose</td></tr></table></td>")
                If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Promote Collaboration & Accountability</td></tr></table></td>")
                If DT1.Rows(i)("Promote_Collaboration").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Promote_Collaboration").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Promote_Collaboration").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Confront Challenges</td></tr></table></td>")
                If DT1.Rows(i)("Confront_Challenges").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Confront_Challenges").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Confront_Challenges").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td colspan=4><table width=55%><tr><td width=40%></td><td style=font-family:Calibri;font-size:18px;font-weight:bold;>LEADING THE ORGANIZATION</td></tr></table></td></tr>")
                Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Lead Change</td></tr></table></td>")
                If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr align=center><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Inspire Risk Taking & innovation</td></tr></table></td>")
                If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Leverage External Perspective</td></tr></table></td>")
                If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Communicate for Impact</td></tr></table></td>")
                If DT1.Rows(i)("Communicate_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Communicate_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Communicate_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("</table></td></tr>")

            End If

            Response.Write("<tr><td colspan=6>&nbsp;</td></tr></table>")

            Response.Write("<td style=Style3>&nbsp;</td></td><td>&nbsp;</td></tr></table>")

        Next

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Guild_Appraisal_New()
        'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8)) : Response.End()
        '--Guild information---
        SQL1 = " select *,convert(char(10),Hire_Date,101)Hired from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,(select last from id_tbl where emplid=a.emplid)LastName,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.mgt_emplid)MGT_Name,(select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B  "
        SQL1 &= " where A.emplid=B.emplid and SAP=14 and A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & ") AA JOiN (select emplid,Goals1,Milestones1,TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,"
        SQL1 &= " IsNull(TargetDate2,'')TargetDate2,IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,"
        SQL1 &= " IsNull(TargetDate4,'')TargetDate4,IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,"
        SQL1 &= " IsNull(TargetDate6,'')TargetDate6,IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,"
        SQL1 &= " IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,"
        SQL1 &= " IsNull(TargetDate10,'')TargetDate10 from(select A8.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,"
        SQL1 &= " Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9,Goals10,Milestones10,TargetDate10"
        SQL1 &= " from(select A7.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,"
        SQL1 &= " Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9 from(select A6.Emplid,Goals1,Milestones1,TargetDate1,"
        SQL1 &= " Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,"
        SQL1 &= " Goals8,Milestones8,TargetDate8 from(select A5.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,"
        SQL1 &= " Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7 from(select A4.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,"
        SQL1 &= " Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6 from(select A3.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,"
        SQL1 &= " TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5 from(select A2.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,"
        SQL1 &= " Milestones4,TargetDate4 from(select A1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3 from(select AC1.Emplid,Goals1,Milestones1,TargetDate1,"
        SQL1 &= " Goals2,Milestones2,TargetDate2 from (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoals_tbl where IndexID=1 and Perf_Year=" & Session("YEAR") + 1 & "  and Appr_Goals=0)Ac1 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoals_tbl where IndexID=2 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac2 ON Ac1.emplid=Ac2.emplid)A1 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoals_tbl where IndexID=3 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac3 ON A1.emplid=Ac3.emplid)A2 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoals_tbl where IndexID=4 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac4 ON A2.emplid=Ac4.emplid )A3 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoals_tbl where IndexID=5 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac5 ON A3.emplid=Ac5.emplid)A4 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoals_tbl where IndexID=6 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac6 ON A4.emplid=Ac6.emplid)A5 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoals_tbl where IndexID=7 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac7 ON A5.emplid=Ac7.emplid )A6 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoals_tbl where IndexID=8 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac8 ON A6.emplid=Ac8.emplid)A7 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoals_tbl where IndexID=9 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac9 ON A7.emplid=Ac9.emplid)A8 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoals_tbl where IndexID=10 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac10"
        SQL1 &= " ON A8.emplid=Ac10.emplid)A10 )BB ON aa.emplid=bb.emplid "

        If Len(Mid(Request.QueryString("Token"), 4, 8)) = 4 Then
            SQL1 = SQL1 & " and HR_EMPLID =" & Mid(Request.QueryString("Token"), 4, 8) & ""
        ElseIf Len(Mid(Request.QueryString("Token"), 4, 8)) = 7 Then
            SQL1 = SQL1 & " and DEPTID =" & Mid(Request.QueryString("Token"), 4, 11) & ""
        ElseIf Len(Mid(Request.QueryString("Token"), 11, 15)) = 4 Then
            SQL1 = SQL1 & " and MGT_EMPLID =" & Mid(Request.QueryString("Token"), 11, 15) & ""
        End If
        SQL1 = SQL1 & " order by LastName"
        'Response.Write(SQL1) : Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<table border=0 class=StyleBreak width=100%>")
            Response.Write("<tr><td class=auto-style1><img src=../../../images/CR_logo.png /></td></tr>")
            Response.Write("<tr><td class=auto-style1><u>Performance Appraisal (FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & ")</u></td></tr>")
            Response.Write("<tr><td class=auto-style1>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td >")
            Response.Write("<table style=width:100%>")
            Response.Write(" <tr><td style=width:7%; font-family: Calibri;><b>Name:</b></td>")
            Response.Write("<td style=width:30%;>" & DT1.Rows(i)("Name").ToString & "</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td style=width:5%; font-family: Calibri;>E-Signed:&nbsp;&nbsp;</td>")

            If DT1.Rows(i)("Process_Flag").ToString = 5 Then
                Response.Write("<td style=width:20%;>" & DT1.Rows(i)("DateEmpl_Appr").ToString & "</td></tr>")
            ElseIf DT1.Rows(i)("Process_Flag").ToString = 4 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            Else
                Response.Write("<td style=width:20%;></td></tr>")
            End If

            Response.Write("<tr><td><b>Title:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Jobtitle").ToString & "</td>")
            Response.Write("<td style=width:12%; font-family: Calibri;><b>Manager Name:&nbsp;</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("MGT_Name").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 1 Then
                Response.Write("<td>" & DT1.Rows(i)("DateSub_UP_MGT").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If


            Response.Write("<tr><td style=font-family: Calibri;><b>Department:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Department").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;><b>Former Manager:&nbsp;</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td>")
            'Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")
            Response.Write("<td></td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 1 Then
                'Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                Response.Write("<td></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 2 Then
                'Response.Write("<td>" & DT1.Rows(i)("DateSub_HR").ToString & "</td></tr>")
                Response.Write("<td></td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("<tr><td style=font-family: Calibri;><b>Hire Date:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Hired").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;><b>Human Resources Generalist:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("HR_NAME").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 2 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 3 Then
                Response.Write("<td>" & DT1.Rows(i)("DateHR_Appr").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")
            '--1. Accomplishments--
            Response.Write("<table border=0><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class=style7A>(Summarize accomplishments vs the goals/expectations established)</span></td></tr></table>")
            Response.Write("</td></tr><tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--1END Accomplishments--
            Response.Write("</td></tr>")
            '--2. Strengths-- 
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class=style7A>(Comment on key strengths and highlight areas performed well)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Strengths").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--2. END Strengths-- 
            Response.Write("</td></tr>")
            '--3. Development Areas--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;<span class=style7A>(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Area").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray /></td></tr></table>")
            '--3. END Development--  
            Response.Write("</td></tr>")
            '--4. Overall Performance--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class=style7A> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>")
            Response.Write("<tr><td>")
            Response.Write("<table border=1 style=border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width=100%>")
            Response.Write("<tr><td class=style9><b>Unsatisfactory</b></td>")
            Response.Write("<td class=style9><b>Developing/Improving Contributor</b></td>")
            Response.Write("<td class=style9><b>Solid Contributor</b></td>")
            Response.Write("<td class=style9><b>Very Strong Contributor</b></td>")
            Response.Write("<td class=style9><b>Distinguished Contributor</b></td></tr><tr>")
            If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            Response.Write("</tr></table>")
            Response.Write("</td></tr></table>")
            '--4. END Overall Performance--  
            Response.Write("</td></tr>")
            '--5. Overall Summary Waiting approval--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class=style7A>(Comment on overall performance)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr /></td></tr>")
            Response.Write("</table>")
            '--5. END Overall Performance--    
            Response.Write("</td></tr>")
            '--6. Development Plan--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            'Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Plan:</u>&nbsp;<span class=style7A>(Based on development area, summarize a plan for professional and performance development)</span></td></tr>")
            'Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Objective").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td ><hr /></td></tr>")
            Response.Write("</table>")
            Response.Write("</td></tr>")
            '--6. END Development Plan--
            Response.Write("<tr><td>")
            '--Divider--
            Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            Response.Write("<tr><td>&nbsp;</td></tr>")
            '--END Divider--
            Response.Write("<tr><td>")
            '--Addendum --  
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style1><u>Addendum:&nbsp;Leadership Competencies</u></td></tr><tr><td></td></tr>")
            Response.Write("<tr><td class=style7>Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies. Use this as a guide to provide feedback and guide your direct report on areas to focus on for further growth and development.")
            Response.Write("<table border=1 style=border-spacing:0; border-collapse:collapse; width=100%>")
            Response.Write("<tr><td class=style9>&nbsp;</td>")
            Response.Write("<td class=style9><b>Needs Development/ Improvement</b></td>")
            Response.Write("<td class=style9><b>Proficient</b></td>")
            Response.Write("<td class=style9><b>Excels</b></td></tr>")

            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Oneself</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Make Balanced Decisions</font></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;>Build Trust</td>")
            If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;>Learn Continuously</td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Other</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead with Urgency & Purpose</font></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Promote Collaboration & Accountability</font></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Confront Challenges</font></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading the Organization</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead Change</font></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Inspire Risk Taking & Innovation</font></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Leverage External Perspective</font></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Communicate for Impact</font></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("</table>")
            Response.Write("</td></tr>")
            '--END Addendum --
            Response.Write("<tr><td>")
            '--Divider--
            Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            '--END Divider--
            Response.Write("<tr><td>&nbsp;</td></tr>")
            '--
            Response.Write("<tr><td class=auto-style1><u>FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & " Goal-Setting Form")
            Response.Write("(06/01/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & " - 05/31/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & ")</u></td></tr>")
            '--Smart Goal-Setting Form
            Response.Write("<tr><td>&nbsp;</td></tr>")
            Response.Write("<tr><td><font size=medium font-family=Calibri>Enter SMART Goals")
            Response.Write("(<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to ")
            Response.Write("focus employees on achieving important, tangible outcomes that contribute to the achievement of department and strategic goals. Summarize the goals agreed to with the employee.")
            If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 > "17" Then
                Response.Write(" Please click <font color=blue><u>here</u></font> for a SMART goal example.")
            End If

            Response.Write("</td></tr>")
            Response.Write("<tr><td>")

            Response.Write("<table border=1 style=border-collapse:collapse; border-spacing:0; width=100%>")
            Response.Write("<tr><td class=style10 width=3%;></td>")

            If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 = "17" Then
                Response.Write("<td class=style10 width=45%><b>Goals</b></td>")
                Response.Write("<td class=style10 width:40%;><b>Success Measures or Milestones</b></td>")
                Response.Write("<td class=style10><b>Target<br>Completion Date</b></td></tr>")
            Else
                Response.Write("<td class=style10 width=45%><b>Goals</b><div style=font-size:small;font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td>")
                Response.Write("<td class=style10 width:40%;><b>Key Results</b><div style=font-size:small;font-weight:lighter;font-style:italic>How will I know that I’ve accomplished each goal?</div> </td>")
                Response.Write("<td class=style10><b>Target<br>Completion Date</b><div style=font-size:small;font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td></tr>")
            End If

            Response.Write("<tr><td valign=top align=center><b>1)</b></td>")

            Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
            Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
            Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")

            If Len(DT1.Rows(i)("Goals2").ToString) + Len(DT1.Rows(i)("Milestones2").ToString) + Len(DT1.Rows(i)("TargetDate2").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>2)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals3").ToString) + Len(DT1.Rows(i)("Milestones3").ToString) + Len(DT1.Rows(i)("TargetDate3").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>3)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals4").ToString) + Len(DT1.Rows(i)("Milestones4").ToString) + Len(DT1.Rows(i)("TargetDate4").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>4)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals5").ToString) + Len(DT1.Rows(i)("Milestones5").ToString) + Len(DT1.Rows(i)("TargetDate5").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>5)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals6").ToString) + Len(DT1.Rows(i)("Milestones6").ToString) + Len(DT1.Rows(i)("TargetDate6").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>6)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals7").ToString) + Len(DT1.Rows(i)("Milestones7").ToString) + Len(DT1.Rows(i)("TargetDate7").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>7)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals8").ToString) + Len(DT1.Rows(i)("Milestones8").ToString) + Len(DT1.Rows(i)("TargetDate8").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>8)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals9").ToString) + Len(DT1.Rows(i)("Milestones9").ToString) + Len(DT1.Rows(i)("TargetDate9").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>9)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals10").ToString) + Len(DT1.Rows(i)("Milestones10").ToString) + Len(DT1.Rows(i)("TargetDate10").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>10)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If
            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("</table>")

        Next

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Manager_Appraisal_New()
        'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8)) : Response.End()

        If Session("EMPLID") = 6785 Or Session("EMPLID") = 6250 Or Session("EMPLID") = 6671 Then
            '--Managers information---
            SQL1 = " select *,convert(char(10),Hire_Date,101)Hired from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,(select last from id_tbl where emplid=a.emplid)LastName,"
            SQL1 &= " (select first+' '+last from id_tbl where emplid=a.mgt_emplid)MGT_Name,(select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,"
            SQL1 &= " (select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B  "
            SQL1 &= " where A.emplid=B.emplid and SAP not in (14) and A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & ") AA JOiN (select emplid,Goals1,Milestones1,TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,"
            SQL1 &= " IsNull(TargetDate2,'')TargetDate2,IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,"
            SQL1 &= " IsNull(TargetDate4,'')TargetDate4,IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,"
            SQL1 &= " IsNull(TargetDate6,'')TargetDate6,IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,"
            SQL1 &= " IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,"
            SQL1 &= " IsNull(TargetDate10,'')TargetDate10 from(select A8.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,"
            SQL1 &= " Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9,Goals10,Milestones10,TargetDate10"
            SQL1 &= " from(select A7.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,"
            SQL1 &= " Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9 from(select A6.Emplid,Goals1,Milestones1,TargetDate1,"
            SQL1 &= " Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,"
            SQL1 &= " Goals8,Milestones8,TargetDate8 from(select A5.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,"
            SQL1 &= " Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7 from(select A4.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,"
            SQL1 &= " Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6 from(select A3.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,"
            SQL1 &= " TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5 from(select A2.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,"
            SQL1 &= " Milestones4,TargetDate4 from(select A1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3 from(select AC1.Emplid,Goals1,Milestones1,TargetDate1,"
            SQL1 &= " Goals2,Milestones2,TargetDate2 from (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoals_tbl where IndexID=1 and Perf_Year=" & Session("YEAR") + 1 & "  and Appr_Goals=0)Ac1 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoals_tbl where IndexID=2 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac2 ON Ac1.emplid=Ac2.emplid)A1 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoals_tbl where IndexID=3 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac3 ON A1.emplid=Ac3.emplid)A2 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoals_tbl where IndexID=4 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac4 ON A2.emplid=Ac4.emplid )A3 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoals_tbl where IndexID=5 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac5 ON A3.emplid=Ac5.emplid)A4 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoals_tbl where IndexID=6 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac6 ON A4.emplid=Ac6.emplid)A5 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoals_tbl where IndexID=7 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac7 ON A5.emplid=Ac7.emplid )A6 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoals_tbl where IndexID=8 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac8 ON A6.emplid=Ac8.emplid)A7 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoals_tbl where IndexID=9 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac9 ON A7.emplid=Ac9.emplid)A8 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoals_tbl where IndexID=10 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac10"
            SQL1 &= " ON A8.emplid=Ac10.emplid)A10 )BB ON aa.emplid=bb.emplid"
            If Len(Mid(Request.QueryString("Token"), 4, 8)) = 4 Then
                SQL1 = SQL1 & " and HR_EMPLID =" & Mid(Request.QueryString("Token"), 4, 8) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 4, 8)) = 7 Then
                SQL1 = SQL1 & " and DEPTID =" & Mid(Request.QueryString("Token"), 4, 11) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 11, 15)) = 4 Then
                SQL1 = SQL1 & " and MGT_EMPLID =" & Mid(Request.QueryString("Token"), 11, 15) & ""
            End If
            SQL1 = SQL1 & " order by LastName"
        Else
            '--Managers information---
            SQL1 = " select '1'LAM, *,convert(char(10),Hire_Date,101)Hired from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,(select last from id_tbl where emplid=a.emplid)LastName,"
            SQL1 &= " (select first+' '+last from id_tbl where emplid=a.mgt_emplid)MGT_Name,(select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,"
            SQL1 &= " (select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B  "
            SQL1 &= " where A.emplid=B.emplid and SAP not in (14) and A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & ") AA JOiN (select emplid,Goals1,Milestones1,TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,"
            SQL1 &= " IsNull(TargetDate2,'')TargetDate2,IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,"
            SQL1 &= " IsNull(TargetDate4,'')TargetDate4,IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,"
            SQL1 &= " IsNull(TargetDate6,'')TargetDate6,IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,"
            SQL1 &= " IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,"
            SQL1 &= " IsNull(TargetDate10,'')TargetDate10 from(select A8.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,"
            SQL1 &= " Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9,Goals10,Milestones10,TargetDate10"
            SQL1 &= " from(select A7.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,"
            SQL1 &= " Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9 from(select A6.Emplid,Goals1,Milestones1,TargetDate1,"
            SQL1 &= " Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,"
            SQL1 &= " Goals8,Milestones8,TargetDate8 from(select A5.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,"
            SQL1 &= " Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7 from(select A4.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,"
            SQL1 &= " Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6 from(select A3.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,"
            SQL1 &= " TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5 from(select A2.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,"
            SQL1 &= " Milestones4,TargetDate4 from(select A1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3 from(select AC1.Emplid,Goals1,Milestones1,TargetDate1,"
            SQL1 &= " Goals2,Milestones2,TargetDate2 from (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoals_tbl where IndexID=1 and Perf_Year=" & Session("YEAR") + 1 & "  and Appr_Goals=0)Ac1 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoals_tbl where IndexID=2 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac2 ON Ac1.emplid=Ac2.emplid)A1 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoals_tbl where IndexID=3 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac3 ON A1.emplid=Ac3.emplid)A2 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoals_tbl where IndexID=4 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac4 ON A2.emplid=Ac4.emplid )A3 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoals_tbl where IndexID=5 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac5 ON A3.emplid=Ac5.emplid)A4 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoals_tbl where IndexID=6 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac6 ON A4.emplid=Ac6.emplid)A5 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoals_tbl where IndexID=7 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac7 ON A5.emplid=Ac7.emplid )A6 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoals_tbl where IndexID=8 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac8 ON A6.emplid=Ac8.emplid)A7 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoals_tbl where IndexID=9 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac9 ON A7.emplid=Ac9.emplid)A8 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoals_tbl where IndexID=10 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac10"
            SQL1 &= " ON A8.emplid=Ac10.emplid)A10 )BB ON aa.emplid=bb.emplid and aa.hr_emplid not in (6785) "
            If Len(Mid(Request.QueryString("Token"), 4, 8)) = 4 Then
                SQL1 = SQL1 & " and HR_EMPLID =" & Mid(Request.QueryString("Token"), 4, 8) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 4, 8)) = 7 Then
                SQL1 = SQL1 & " and DEPTID =" & Mid(Request.QueryString("Token"), 4, 11) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 11, 15)) = 4 Then
                SQL1 = SQL1 & " and MGT_EMPLID =" & Mid(Request.QueryString("Token"), 11, 15) & ""
            End If
            SQL1 = SQL1 & " order by LastName"

        End If
        'Response.Write(SQL1) : Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<table border=0 class=StyleBreak width=100%>")
            Response.Write("<tr><td class=auto-style1><img src=../../../images/CR_logo.png /></td></tr>")
            Response.Write("<tr><td class=auto-style1><u>Performance Appraisal (FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & ")</u></td></tr>")
            Response.Write("<tr><td class=auto-style1>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td >")
            Response.Write("<table style=width:100%>")
            Response.Write(" <tr><td style=width:7%; font-family: Calibri;><b>Name:</b></td>")
            Response.Write("<td style=width:30%;>" & DT1.Rows(i)("Name").ToString & "</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td style=width:5%; font-family: Calibri;>E-Signed:&nbsp;&nbsp;</td>")

            If DT1.Rows(i)("Process_Flag").ToString = 5 Then
                Response.Write("<td style=width:20%;>" & DT1.Rows(i)("DateEmpl_Appr").ToString & "</td></tr>")
            ElseIf DT1.Rows(i)("Process_Flag").ToString = 4 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            Else
                Response.Write("<td style=width:20%;></td></tr>")
            End If
            Response.Write("<tr><td><b>Title:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Jobtitle").ToString & "</td>")
            Response.Write("<td style=width:12%; font-family: Calibri;><b>Manager Name:&nbsp;</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("MGT_Name").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 1 Then
                Response.Write("<td>" & DT1.Rows(i)("DateSub_UP_MGT").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If
            Response.Write("<tr><td style=font-family: Calibri;><b>Department:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Department").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;><b>Former Manager:&nbsp;</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td>")
            'Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")
            Response.Write("<td></td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 1 Then
                'Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                Response.Write("<td></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 2 Then
                'Response.Write("<td>" & DT1.Rows(i)("DateSub_HR").ToString & "</td></tr>")
                Response.Write("<td></td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If
            Response.Write("<tr><td style=font-family: Calibri;><b>Hire Date:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Hired").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;><b>Human Resources Generalist:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("HR_NAME").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 2 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 3 Then
                Response.Write("<td>" & DT1.Rows(i)("DateHR_Appr").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")
            '--1. Accomplishments--
            Response.Write("<table border=0><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class=style7A>(Summarize accomplishments vs the goals/expectations established)</span></td></tr></table>")
            Response.Write("</td></tr><tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--1END Accomplishments--
            Response.Write("</td></tr>")

            Response.Write("<tr><td>")

            '--Leadership Competencies: --  
            Response.Write("<table border=0><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Leadership Competencies:</u>&nbsp;<span class=style7A>(Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies)</span></td></tr></table>")
            Response.Write("<table border=1 style=border-spacing:0; border-collapse:collapse; width=100%>")
            Response.Write("<tr><td class=style9>&nbsp;</td>")
            Response.Write("<td class=style9><b>Needs Development/ Improvement</b></td>")
            Response.Write("<td class=style9><b>Proficient</b></td>")
            Response.Write("<td class=style9><b>Excels</b></td></tr>")

            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Oneself</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Make Balanced Decisions</font></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;>Build Trust</td>")
            If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;>Learn Continuously</td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Other</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead with Urgency & Purpose</font></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Promote Collaboration & Accountability</font></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Confront Challenges</font></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading the Organization</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead Change</font></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Inspire Risk Taking & Innovation</font></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Leverage External Perspective</font></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Communicate for Impact</font></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            'Response.Write("</table>")
            'Response.Write("</td></tr>")
            Response.Write("</table>")
            Response.Write("</td></tr>")
            '--END Leadership Competencies: --

            '--2. Strengths-- 
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class=style7A>(Comment on key strengths and highlight areas performed well)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Strengths").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--2. END Strengths-- 
            Response.Write("</td></tr>")
            '--3. Development Areas--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;<span class=style7A>(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Area").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray /></td></tr></table>")
            '--3. END Development--  
            Response.Write("</td></tr>")
            '--4. Overall Performance--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class=style7A> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>")
            Response.Write("<tr><td>")
            Response.Write("<table border=1 style=border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width=100%>")
            Response.Write("<tr><td class=style9><b>Unsatisfactory</b></td>")
            Response.Write("<td class=style9><b>Developing/Improving Contributor</b></td>")
            Response.Write("<td class=style9><b>Solid Contributor</b></td>")
            Response.Write("<td class=style9><b>Very Strong Contributor</b></td>")
            Response.Write("<td class=style9><b>Distinguished Contributor</b></td></tr><tr>")
            If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            Response.Write("</tr></table>")
            Response.Write("</td></tr></table>")
            '--4. END Overall Performance--  
            Response.Write("</td></tr>")
            '--5. Overall Summary Waiting approval--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class=style7A>(Comment on overall performance)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr /></td></tr>")
            Response.Write("</table>")
            '--5. END Overall Performance--    
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")

            '--Divider--
            Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            '--END Divider--
            Response.Write("<tr><td>&nbsp;</td></tr>")
            '--
            Response.Write("<tr><td class=auto-style1><u>FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & " Goal-Setting Form")
            Response.Write("(06/01/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & " - 05/31/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & ")</u></td></tr>")

            '--Smart Goal-Setting Form
            Response.Write("<tr><td>&nbsp;</td></tr>")
            Response.Write("<tr><td><font size=medium font-family=Calibri>Enter SMART Goals")
            Response.Write("(<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to ")
            Response.Write("focus employees on achieving important, tangible outcomes that contribute to the achievement of department and strategic goals. Summarize the goals agreed to with the employee.")
            If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 > "17" Then
                Response.Write(" Please click <font color=blue><u>here</u></font> for a SMART goal example.")
            End If
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")

            Response.Write("<table border=1 style=border-collapse:collapse; border-spacing:0; width=100%>")
            Response.Write("<tr><td class=style10 width=3%;></td>")

            If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 = "17" Then
                Response.Write("<td class=style10 width=45%><b>Goals</b></td>")
                Response.Write("<td class=style10 width:40%;><b>Success Measures or Milestones</b></td>")
                Response.Write("<td class=style10><b>Target<br>Completion Date</b></td></tr>")
            Else
                Response.Write("<td class=style10 width=45%><b>Goals</b><div style=font-size:small;font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td>")
                Response.Write("<td class=style10 width:40%;><b>Key Results</b><div style=font-size:small;font-weight:lighter;font-style:italic>How will I know that I’ve accomplished each goal?</div> </td>")
                Response.Write("<td class=style10><b>Target<br>Completion Date</b><div style=font-size:small;font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td></tr>")
            End If

            Response.Write("<tr><td valign=top align=center><b>1)</b></td>")

            Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
            Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
            Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")

            If Len(DT1.Rows(i)("Goals2").ToString) + Len(DT1.Rows(i)("Milestones2").ToString) + Len(DT1.Rows(i)("TargetDate2").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>2)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals3").ToString) + Len(DT1.Rows(i)("Milestones3").ToString) + Len(DT1.Rows(i)("TargetDate3").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>3)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals4").ToString) + Len(DT1.Rows(i)("Milestones4").ToString) + Len(DT1.Rows(i)("TargetDate4").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>4)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals5").ToString) + Len(DT1.Rows(i)("Milestones5").ToString) + Len(DT1.Rows(i)("TargetDate5").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>5)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals6").ToString) + Len(DT1.Rows(i)("Milestones6").ToString) + Len(DT1.Rows(i)("TargetDate6").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>6)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals7").ToString) + Len(DT1.Rows(i)("Milestones7").ToString) + Len(DT1.Rows(i)("TargetDate7").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>7)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals8").ToString) + Len(DT1.Rows(i)("Milestones8").ToString) + Len(DT1.Rows(i)("TargetDate8").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>8)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals9").ToString) + Len(DT1.Rows(i)("Milestones9").ToString) + Len(DT1.Rows(i)("TargetDate9").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>9)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals10").ToString) + Len(DT1.Rows(i)("Milestones10").ToString) + Len(DT1.Rows(i)("TargetDate10").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>10)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If
            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("</table>")

        Next

        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub Individual_Appraisal()

        If Session("Year") = 2014 Then
            Response.Redirect("..\Guild_Appraisal.aspx?Token=" & Mid(Request.QueryString("Token"), 5, 9))
        Else
            SQL = "select * from(select count(*)CNT_GLD from Guild_Appraisal_MASTER_tbl A where Perf_Year=2015 and emplid=" & Mid(Request.QueryString("Token"), 5, 9) & ")A,"
            SQL &= " (select count(*)CNT_MGT  from ME_Appraisal_Master_TBL A where Perf_Year=2015 and emplid=" & Mid(Request.QueryString("Token"), 5, 9) & ")B"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            'Response.Write(SQL)
            If DT.Rows(0)("CNT_GLD").ToString = 1 Then
                'Response.Write("Guild's appraisal for 2015")
                'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8)) : Response.End()
                '--Guild information---
                SQL1 = "select (select last from id_tbl where emplid=a.emplid)LastName, * from Guild_Appraisal_Print_tbl A where emplid=" & Mid(Request.QueryString("Token"), 5, 9) & "   "
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                '---Effectiveness Factor---
                '1.  Autonomy
                '2.  Critical Judgment
                '3.  Dependability
                '4.  Flexibility
                '5.  Follow Through
                '6.  Quantity
                '7.  Self Motivation
                '8.  Style & Use of language
                '9.  Teamwork
                '10. Timeliness
                '11. Trust, Civility & Respect

                For i = 0 To DT1.Rows.Count - 1

                    Response.Write("<table border=0 class=StyleBreak width=100%>")
                    Response.Write("<tr><td width=15%>&nbsp;</td><td style= text-align:center><img src=../../../images/CR_logo.png style=width:380px; height:50px/></td><td width=15%>&nbsp;</td></tr>")
                    Response.Write("<tr><td style=Style3>&nbsp;</td><td class=Style1>Performance Appraisal</td><td>&nbsp;</td></tr>")
                    Response.Write("<tr><td style=Style3>&nbsp;</td><td class=Style2> October 1, 2014&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;September 30, 2015</td><td>&nbsp;</td></tr>")
                    Response.Write("<tr><td>&nbsp;</td><td>")

                    Response.Write("<center><table border=0 style=Style0><tr><td width=3%>&nbsp;&nbsp;</td>")
                    '--Column #1-2
                    Response.Write("<td width=8%>Name:</td><td width=25%>" & DT1.Rows(i)("Guild_Name").ToString & "</td>")
                    Response.Write("<td width=18%>Manager Name:</td><td>" & DT1.Rows(i)("SUP_NAME").ToString & "</td>")
                    Response.Write("<td>Approved:&nbsp;&nbsp;" & DT1.Rows(i)("Mgr_Apr").ToString & "</td></tr>")
                    Response.Write("<tr><td width=3%>&nbsp;&nbsp;</td>")
                    '--Column #1-2
                    Response.Write("<td>Title:</td><td>" & DT1.Rows(i)("jobtitle1").ToString & "</td>")
                    Response.Write("<td nowrap=nowrap>2nd Level Manager Name:&nbsp;</td><td width=15%>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td>")
                    Response.Write("<td>Approved:&nbsp;&nbsp;" & DT1.Rows(i)("UP_Mgt_Apr").ToString & "</td></tr>")
                    Response.Write("<tr><td width=3%>&nbsp;&nbsp;</td>")
                    '--Column #1-2
                    Response.Write("<td>Department:</td><td>" & DT1.Rows(i)("Departname").ToString & "</td>")
                    Response.Write("<td>HR Generalist:</td><td>" & DT1.Rows(i)("HR_NAME").ToString & "</td><td>Approved:&nbsp;&nbsp;" & DT1.Rows(i)("HR_Apr").ToString & "</td></tr>")
                    Response.Write("<tr><td width=3%>&nbsp;&nbsp;</td>")
                    '--Column #1-2
                    Response.Write("<td>Hire Date:</td><td>" & DT1.Rows(i)("Hired").ToString & "</td>")
                    Response.Write("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>")
                    Response.Write("<tr><td colspan=6>&nbsp;</td></tr></table><td style=Style3>&nbsp;</td>")
                    Response.Write("<tr><td style=Style3>&nbsp;</td><td style=Style4><font color=red><b><u>Key Tasks:</u></b></font>&nbsp;<i>Enter in the Key Task and description information in the space provided below. Key Task information should be extracted from employee's job description. Rate each key task using the effectiveness factor that best relates to the duties of each individual key task. The effectiveness factors may be rated by entering a check mark.</i></td><td>&nbsp;</td></tr>")
                    Response.Write("<tr><td style=Style3>&nbsp;</td><td style=Style4>")

                    '--Start Tasks#1 table
                    Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                    Response.Write("<tr><td width=3%></td><td><font color=red><b><u>1) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                    Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                    '--Grid
                    Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                    Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                    Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                    Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                    'E_Aut_1 H_Aut_1 M_Aut_1 N_Aut_1 U_Aut_1 
                    If DT1.Rows(i)("E_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Aut_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                    'E_Cri_1 H_Cri_1 M_Cri_1 N_Cri_1 U_Cri_1  
                    If DT1.Rows(i)("E_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Cri_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                    'E_Dep_1 H_Dep_1 M_Dep_1 N_Dep_1 U_Dep_1 
                    If DT1.Rows(i)("E_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Dep_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                    'E_Fle_1 H_Fle_1 M_Fle_1 N_Fle_1 U_Fle_1 
                    If DT1.Rows(i)("E_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Fle_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                    'E_Fol_1 H_Fol_1 M_Fol_1 N_Fol_1 U_Fol_1 
                    If DT1.Rows(i)("E_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Fol_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                    'E_Qua_1 H_Qua_1 M_Qua_1 N_Qua_1 U_Qua_1 
                    If DT1.Rows(i)("E_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Qua_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                    'E_Sel_1 H_Sel_1 M_Sel_1 N_Sel_1 U_Sel_1 
                    If DT1.Rows(i)("E_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Sel_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                    'E_Sty_1 H_Sty_1 M_Sty_1 N_Sty_1 U_Sty_1 
                    If DT1.Rows(i)("E_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Sty_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                    'E_Tea_1 H_Tea_1 M_Tea_1 N_Tea_1 U_Tea_1 
                    If DT1.Rows(i)("E_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Tea_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                    'E_Tim_1 H_Tim_1 M_Tim_1 N_Tim_1 U_Tim_1 
                    If DT1.Rows(i)("E_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Tim_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                    'E_Tru_1 H_Tru_1 M_Tru_1 N_Tru_1 U_Tru_1 
                    If DT1.Rows(i)("E_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("H_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("M_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("N_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                    If DT1.Rows(i)("U_Tru_1").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                    Response.Write("</table></td></tr>")
                    '--End Grid
                    Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating1").ToString) & "</font></b></td></tr>")
                    Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                    Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                    Response.Write("</table>")
                    '--End  Tasks#1 table

                    '--Start Tasks#2 table
                    If CDbl(Len(DT1.Rows(i)("TaskDesc2").ToString)) > 0 Then
                        Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                        Response.Write("<tr><td width=3%></td><td><font color=red><b><u>2) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                        '--Grid
                        Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                        Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                        Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                        Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                        'E_Aut_2 H_Aut_2 M_Aut_2 N_Aut_2 U_Aut_2 
                        If DT1.Rows(i)("E_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Aut_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                        'E_Cri_2 H_Cri_2 M_Cri_2 N_Cri_2 U_Cri_2  
                        If DT1.Rows(i)("E_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Cri_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                        'E_Dep_2 H_Dep_2 M_Dep_2 N_Dep_2 U_Dep_2 
                        If DT1.Rows(i)("E_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Dep_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                        'E_Fle_2 H_Fle_2 M_Fle_2 N_Fle_2 U_Fle_2 
                        If DT1.Rows(i)("E_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fle_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                        'E_Fol_2 H_Fol_2 M_Fol_2 N_Fol_2 U_Fol_2 
                        If DT1.Rows(i)("E_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fol_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                        'E_Qua_2 H_Qua_2 M_Qua_2 N_Qua_2 U_Qua_2 
                        If DT1.Rows(i)("E_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Qua_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                        'E_Sel_2 H_Sel_2 M_Sel_2 N_Sel_2 U_Sel_2 
                        If DT1.Rows(i)("E_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sel_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                        'E_Sty_2 H_Sty_2 M_Sty_2 N_Sty_2 U_Sty_2 
                        If DT1.Rows(i)("E_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sty_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                        'E_Tea_2 H_Tea_2 M_Tea_2 N_Tea_2 U_Tea_2 
                        If DT1.Rows(i)("E_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tea_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                        'E_Tim_2 H_Tim_2 M_Tim_2 N_Tim_2 U_Tim_2 
                        If DT1.Rows(i)("E_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tim_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                        'E_Tru_2 H_Tru_2 M_Tru_2 N_Tru_2 U_Tru_2 
                        If DT1.Rows(i)("E_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tru_2").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("</table></td></tr>")
                        '--End Grid
                        Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating2").ToString) & "</font></b></td></tr>")
                        Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                        Response.Write("</table>")
                        '--End  Tasks#2 table
                    End If

                    '--Start Tasks#3 table
                    If CDbl(Len(DT1.Rows(i)("TaskDesc3").ToString)) > 0 Then
                        Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                        Response.Write("<tr><td width=3%></td><td><font color=red><b><u>3) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                        '--Grid
                        Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                        Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                        Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                        Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                        'E_Aut_3 H_Aut_3 M_Aut_3 N_Aut_3 U_Aut_3 
                        If DT1.Rows(i)("E_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Aut_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                        'E_Cri_3 H_Cri_3 M_Cri_3 N_Cri_3 U_Cri_3  
                        If DT1.Rows(i)("E_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Cri_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                        'E_Dep_3 H_Dep_3 M_Dep_3 N_Dep_3 U_Dep_3 
                        If DT1.Rows(i)("E_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Dep_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                        'E_Fle_3 H_Fle_3 M_Fle_3 N_Fle_3 U_Fle_3 
                        If DT1.Rows(i)("E_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fle_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                        'E_Fol_3 H_Fol_3 M_Fol_3 N_Fol_3 U_Fol_3 
                        If DT1.Rows(i)("E_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fol_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                        'E_Qua_3 H_Qua_3 M_Qua_3 N_Qua_3 U_Qua_3 
                        If DT1.Rows(i)("E_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Qua_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                        'E_Sel_3 H_Sel_3 M_Sel_3 N_Sel_3 U_Sel_3 
                        If DT1.Rows(i)("E_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sel_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                        'E_Sty_3 H_Sty_3 M_Sty_3 N_Sty_3 U_Sty_3 
                        If DT1.Rows(i)("E_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sty_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                        'E_Tea_3 H_Tea_3 M_Tea_3 N_Tea_3 U_Tea_3 
                        If DT1.Rows(i)("E_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tea_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                        'E_Tim_3 H_Tim_3 M_Tim_3 N_Tim_3 U_Tim_3 
                        If DT1.Rows(i)("E_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tim_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                        'E_Tru_3 H_Tru_3 M_Tru_3 N_Tru_3 U_Tru_3 
                        If DT1.Rows(i)("E_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tru_3").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("</table></td></tr>")
                        '--End Grid
                        Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating3").ToString) & "</font></b></td></tr>")
                        Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                        Response.Write("</table>")
                        '--End  Tasks#3 table
                    End If

                    '--Start Tasks#4 table
                    If CDbl(Len(DT1.Rows(i)("TaskDesc4").ToString)) > 0 Then
                        Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                        Response.Write("<tr><td width=3%></td><td><font color=red><b><u>4) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                        '--Grid
                        Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                        Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                        Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                        Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                        'E_Aut_4 H_Aut_4 M_Aut_4 N_Aut_4 U_Aut_4 
                        If DT1.Rows(i)("E_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Aut_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                        'E_Cri_4 H_Cri_4 M_Cri_4 N_Cri_4 U_Cri_4  
                        If DT1.Rows(i)("E_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Cri_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                        'E_Dep_4 H_Dep_4 M_Dep_4 N_Dep_4 U_Dep_4 
                        If DT1.Rows(i)("E_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Dep_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                        'E_Fle_4 H_Fle_4 M_Fle_4 N_Fle_4 U_Fle_4 
                        If DT1.Rows(i)("E_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fle_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                        'E_Fol_4 H_Fol_4 M_Fol_4 N_Fol_4 U_Fol_4 
                        If DT1.Rows(i)("E_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fol_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                        'E_Qua_4 H_Qua_4 M_Qua_4 N_Qua_4 U_Qua_4 
                        If DT1.Rows(i)("E_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Qua_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                        'E_Sel_4 H_Sel_4 M_Sel_4 N_Sel_4 U_Sel_4 
                        If DT1.Rows(i)("E_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sel_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                        'E_Sty_4 H_Sty_4 M_Sty_4 N_Sty_4 U_Sty_4 
                        If DT1.Rows(i)("E_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sty_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                        'E_Tea_4 H_Tea_4 M_Tea_4 N_Tea_4 U_Tea_4 
                        If DT1.Rows(i)("E_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tea_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                        'E_Tim_4 H_Tim_4 M_Tim_4 N_Tim_4 U_Tim_4 
                        If DT1.Rows(i)("E_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tim_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                        'E_Tru_4 H_Tru_4 M_Tru_4 N_Tru_4 U_Tru_4 
                        If DT1.Rows(i)("E_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tru_4").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If

                        Response.Write("</table></td></tr>")
                        '--End Grid
                        Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating4").ToString) & "</font></b></td></tr>")
                        Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                        Response.Write("</table>")
                        '--End  Tasks#4 table
                    End If

                    '--Start Tasks#5 table
                    If CDbl(Len(DT1.Rows(i)("TaskDesc5").ToString)) > 0 Then
                        Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                        Response.Write("<tr><td width=3%></td><td><font color=red><b><u>5) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                        '--Grid
                        Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                        Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                        Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                        Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                        'E_Aut_5 H_Aut_5 M_Aut_5 N_Aut_5 U_Aut_5 
                        If DT1.Rows(i)("E_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Aut_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                        'E_Cri_5 H_Cri_5 M_Cri_5 N_Cri_5 U_Cri_5  
                        If DT1.Rows(i)("E_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Cri_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                        'E_Dep_5 H_Dep_5 M_Dep_5 N_Dep_5 U_Dep_5 
                        If DT1.Rows(i)("E_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Dep_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                        'E_Fle_5 H_Fle_5 M_Fle_5 N_Fle_5 U_Fle_5 
                        If DT1.Rows(i)("E_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fle_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                        'E_Fol_5 H_Fol_5 M_Fol_5 N_Fol_5 U_Fol_5 
                        If DT1.Rows(i)("E_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fol_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                        'E_Qua_5 H_Qua_5 M_Qua_5 N_Qua_5 U_Qua_5 
                        If DT1.Rows(i)("E_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Qua_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                        'E_Sel_5 H_Sel_5 M_Sel_5 N_Sel_5 U_Sel_5 
                        If DT1.Rows(i)("E_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sel_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                        'E_Sty_5 H_Sty_5 M_Sty_5 N_Sty_5 U_Sty_5 
                        If DT1.Rows(i)("E_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sty_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                        'E_Tea_5 H_Tea_5 M_Tea_5 N_Tea_5 U_Tea_5 
                        If DT1.Rows(i)("E_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tea_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                        'E_Tim_5 H_Tim_5 M_Tim_5 N_Tim_5 U_Tim_5 
                        If DT1.Rows(i)("E_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tim_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                        'E_Tru_5 H_Tru_5 M_Tru_5 N_Tru_5 U_Tru_5 
                        If DT1.Rows(i)("E_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tru_5").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("</table></td></tr>")
                        '--End Grid
                        Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating5").ToString) & "</font></b></td></tr>")
                        Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                        Response.Write("</table>")
                        '--End  Tasks#5 table
                    End If

                    '--Start Tasks#6 table
                    If CDbl(Len(DT1.Rows(i)("TaskDesc6").ToString)) > 0 Then
                        Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                        Response.Write("<tr><td width=3%></td><td><font color=red><b><u>6) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                        '--Grid
                        Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                        Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                        Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                        Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                        'E_Aut_6 H_Aut_6 M_Aut_6 N_Aut_6 U_Aut_6 
                        If DT1.Rows(i)("E_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Aut_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                        'E_Cri_6 H_Cri_6 M_Cri_6 N_Cri_6 U_Cri_6  
                        If DT1.Rows(i)("E_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Cri_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                        'E_Dep_6 H_Dep_6 M_Dep_6 N_Dep_6 U_Dep_6 
                        If DT1.Rows(i)("E_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Dep_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                        'E_Fle_6 H_Fle_6 M_Fle_6 N_Fle_6 U_Fle_6 
                        If DT1.Rows(i)("E_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fle_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                        'E_Fol_6 H_Fol_6 M_Fol_6 N_Fol_6 U_Fol_6 
                        If DT1.Rows(i)("E_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fol_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                        'E_Qua_6 H_Qua_6 M_Qua_6 N_Qua_6 U_Qua_6 
                        If DT1.Rows(i)("E_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Qua_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                        'E_Sel_6 H_Sel_6 M_Sel_6 N_Sel_6 U_Sel_6 
                        If DT1.Rows(i)("E_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sel_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                        'E_Sty_6 H_Sty_6 M_Sty_6 N_Sty_6 U_Sty_6 
                        If DT1.Rows(i)("E_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sty_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                        'E_Tea_6 H_Tea_6 M_Tea_6 N_Tea_6 U_Tea_6 
                        If DT1.Rows(i)("E_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tea_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                        'E_Tim_6 H_Tim_6 M_Tim_6 N_Tim_6 U_Tim_6 
                        If DT1.Rows(i)("E_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tim_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                        'E_Tru_6 H_Tru_6 M_Tru_6 N_Tru_6 U_Tru_6 
                        If DT1.Rows(i)("E_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tru_6").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If

                        Response.Write("</table></td></tr>")
                        '--End Grid
                        Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating6").ToString) & "</font></b></td></tr>")
                        Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                        Response.Write("</table>")
                        '--End  Tasks#6 table
                    End If
                    '--Start Tasks#7 table
                    If CDbl(Len(DT1.Rows(i)("TaskDesc7").ToString)) > 0 Then
                        Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                        Response.Write("<tr><td width=3%></td><td><font color=red><b><u>7) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                        '--Grid
                        Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                        Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                        Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                        Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                        'E_Aut_7 H_Aut_7 M_Aut_7 N_Aut_7 U_Aut_7 
                        If DT1.Rows(i)("E_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Aut_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                        'E_Cri_7 H_Cri_7 M_Cri_7 N_Cri_7 U_Cri_7  
                        If DT1.Rows(i)("E_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Cri_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                        'E_Dep_7 H_Dep_7 M_Dep_7 N_Dep_7 U_Dep_7 
                        If DT1.Rows(i)("E_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Dep_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                        'E_Fle_7 H_Fle_7 M_Fle_7 N_Fle_7 U_Fle_7 
                        If DT1.Rows(i)("E_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fle_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                        'E_Fol_7 H_Fol_7 M_Fol_7 N_Fol_7 U_Fol_7 
                        If DT1.Rows(i)("E_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fol_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                        'E_Qua_7 H_Qua_7 M_Qua_7 N_Qua_7 U_Qua_7 
                        If DT1.Rows(i)("E_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Qua_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                        'E_Sel_7 H_Sel_7 M_Sel_7 N_Sel_7 U_Sel_7 
                        If DT1.Rows(i)("E_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sel_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                        'E_Sty_7 H_Sty_7 M_Sty_7 N_Sty_7 U_Sty_7 
                        If DT1.Rows(i)("E_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sty_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                        'E_Tea_7 H_Tea_7 M_Tea_7 N_Tea_7 U_Tea_7 
                        If DT1.Rows(i)("E_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tea_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                        'E_Tim_7 H_Tim_7 M_Tim_7 N_Tim_7 U_Tim_7 
                        If DT1.Rows(i)("E_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tim_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                        'E_Tru_7 H_Tru_7 M_Tru_7 N_Tru_7 U_Tru_7 
                        If DT1.Rows(i)("E_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tru_7").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("</table></td></tr>")
                        '--End Grid
                        Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating7").ToString) & "</font></b></td></tr>")
                        Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                        Response.Write("</table>")
                        '--End  Tasks#7 table
                    End If

                    '--Start Tasks#8 table
                    If CDbl(Len(DT1.Rows(i)("TaskDesc8").ToString)) > 0 Then
                        Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                        Response.Write("<tr><td width=3%></td><td><font color=red><b><u>8) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                        '--Grid
                        Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                        Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                        Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                        Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                        'E_Aut_8 H_Aut_8 M_Aut_8 N_Aut_8 U_Aut_8 
                        If DT1.Rows(i)("E_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Aut_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                        'E_Cri_8 H_Cri_8 M_Cri_8 N_Cri_8 U_Cri_8  
                        If DT1.Rows(i)("E_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Cri_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                        'E_Dep_8 H_Dep_8 M_Dep_8 N_Dep_8 U_Dep_8 
                        If DT1.Rows(i)("E_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Dep_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                        'E_Fle_8 H_Fle_8 M_Fle_8 N_Fle_8 U_Fle_8 
                        If DT1.Rows(i)("E_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fle_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                        'E_Fol_8 H_Fol_8 M_Fol_8 N_Fol_8 U_Fol_8 
                        If DT1.Rows(i)("E_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fol_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                        'E_Qua_8 H_Qua_8 M_Qua_8 N_Qua_8 U_Qua_8 
                        If DT1.Rows(i)("E_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Qua_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                        'E_Sel_8 H_Sel_8 M_Sel_8 N_Sel_8 U_Sel_8 
                        If DT1.Rows(i)("E_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sel_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                        'E_Sty_8 H_Sty_8 M_Sty_8 N_Sty_8 U_Sty_8 
                        If DT1.Rows(i)("E_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sty_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                        'E_Tea_8 H_Tea_8 M_Tea_8 N_Tea_8 U_Tea_8 
                        If DT1.Rows(i)("E_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tea_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                        'E_Tim_8 H_Tim_8 M_Tim_8 N_Tim_8 U_Tim_8 
                        If DT1.Rows(i)("E_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tim_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                        'E_Tru_8 H_Tru_8 M_Tru_8 N_Tru_8 U_Tru_8 
                        If DT1.Rows(i)("E_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tru_8").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("</table></td></tr>")
                        '--End Grid
                        Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating8").ToString) & "</font></b></td></tr>")
                        Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                        Response.Write("</table>")
                        '--End  Tasks#8 table
                    End If

                    '--Start Tasks#9 table
                    If CDbl(Len(DT1.Rows(i)("TaskDesc9").ToString)) > 0 Then
                        Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                        Response.Write("<tr><td width=3%></td><td><font color=red><b><u>9) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                        '--Grid
                        Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                        Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                        Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                        Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                        'E_Aut_9 H_Aut_9 M_Aut_9 N_Aut_9 U_Aut_9 
                        If DT1.Rows(i)("E_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Aut_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                        'E_Cri_9 H_Cri_9 M_Cri_9 N_Cri_9 U_Cri_9  
                        If DT1.Rows(i)("E_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Cri_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                        'E_Dep_9 H_Dep_9 M_Dep_9 N_Dep_9 U_Dep_9 
                        If DT1.Rows(i)("E_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Dep_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                        'E_Fle_9 H_Fle_9 M_Fle_9 N_Fle_9 U_Fle_9 
                        If DT1.Rows(i)("E_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fle_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                        'E_Fol_9 H_Fol_9 M_Fol_9 N_Fol_9 U_Fol_9 
                        If DT1.Rows(i)("E_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fol_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                        'E_Qua_9 H_Qua_9 M_Qua_9 N_Qua_9 U_Qua_9 
                        If DT1.Rows(i)("E_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Qua_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                        'E_Sel_9 H_Sel_9 M_Sel_9 N_Sel_9 U_Sel_9 
                        If DT1.Rows(i)("E_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sel_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                        'E_Sty_9 H_Sty_9 M_Sty_9 N_Sty_9 U_Sty_9 
                        If DT1.Rows(i)("E_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sty_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                        'E_Tea_9 H_Tea_9 M_Tea_9 N_Tea_9 U_Tea_9 
                        If DT1.Rows(i)("E_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tea_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                        'E_Tim_9 H_Tim_9 M_Tim_9 N_Tim_9 U_Tim_9 
                        If DT1.Rows(i)("E_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tim_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                        'E_Tru_9 H_Tru_9 M_Tru_9 N_Tru_9 U_Tru_9 
                        If DT1.Rows(i)("E_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tru_9").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If

                        Response.Write("</table></td></tr>")
                        '--End Grid
                        Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating9").ToString) & "</font></b></td></tr>")
                        Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & Replace(Replace(DT1.Rows(i)("Comments9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                        Response.Write("</table>")
                        '--End  Tasks#9 table
                    End If

                    '--Start Tasks#10 table
                    If CDbl(Len(DT1.Rows(i)("TaskDesc10").ToString)) > 0 Then
                        Response.Write("<table width=100% border=0 style=font-family:Calibri>")
                        Response.Write("<tr><td width=3%></td><td><font color=red><b><u>10) Key Task Description:</u></b></font>&nbsp;&nbsp" & Replace(Replace(DT1.Rows(i)("TaskDesc10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                        Response.Write("<tr><td width=3%></td><td class=Style5>&nbsp;&nbsp;</td></tr><tr><td width=3%></td><td>")
                        '--Grid
                        Response.Write("<table width=100% border=1 cellpadding=1 cellspacing=0 style=font-family:Calibri>")
                        Response.Write("<tr style=background-color:lightgrey><td><b>Effectiveness Factor</td><td><b>Description</b></td><td width=10% align=center><b>Exceptional</b></td><td width=10% align=center><b>High</b></td>")
                        Response.Write("<td width=10% align=center><b>Meets</td><td width=10% align=center><b>Needs Improvement</td width=10%><td align=center><b>Unsatisfactory<b></td></tr>")

                        Response.Write("<tr><td>Autonomy</td><td>Works Independently</td>")
                        'E_Aut_10 H_Aut_10 M_Aut_10 N_Aut_10 U_Aut_10 
                        If DT1.Rows(i)("E_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Aut_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled ></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Critical Judgment</td><td>Makes sound judgment</td>")
                        'E_Cri_10 H_Cri_10 M_Cri_10 N_Cri_10 U_Cri_10  
                        If DT1.Rows(i)("E_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Cri_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Dependability</td><td>Can be relied upon</td>")
                        'E_Dep_10 H_Dep_10 M_Dep_10 N_Dep_10 U_Dep_10 
                        If DT1.Rows(i)("E_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Dep_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Flexibility</td><td>Adapts to changing priorities</td>")
                        'E_Fle_10 H_Fle_10 M_Fle_10 N_Fle_10 U_Fle_10 
                        If DT1.Rows(i)("E_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fle_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Follow Through</td><td>Accomplishes task objectives</td>")
                        'E_Fol_10 H_Fol_10 M_Fol_10 N_Fol_10 U_Fol_10 
                        If DT1.Rows(i)("E_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Fol_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Quantity</td><td>Handles large work load</td>")
                        'E_Qua_10 H_Qua_10 M_Qua_10 N_Qua_10 U_Qua_10 
                        If DT1.Rows(i)("E_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Qua_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Self Motivation</td><td>Takes initiative</td>")
                        'E_Sel_10 H_Sel_10 M_Sel_10 N_Sel_10 U_Sel_10 
                        If DT1.Rows(i)("E_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sel_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Style & Use of language</td><td>Able to write clearly and concisely</td>")
                        'E_Sty_10 H_Sty_10 M_Sty_10 N_Sty_10 U_Sty_10 
                        If DT1.Rows(i)("E_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Sty_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Teamwork</td><td>Listens, cooperates</td>")
                        'E_Tea_10 H_Tea_10 M_Tea_10 N_Tea_10 U_Tea_10 
                        If DT1.Rows(i)("E_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tea_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Timeliness</td><td>Completes work in a timely manner</td>")
                        'E_Tim_10 H_Tim_10 M_Tim_10 N_Tim_10 U_Tim_10 
                        If DT1.Rows(i)("E_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tim_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("<tr><td>Trust, Civility & Respect</td><td>Treats all colleagues with civility & respect</td>")
                        'E_Tru_10 H_Tru_10 M_Tru_10 N_Tru_10 U_Tru_10 
                        If DT1.Rows(i)("E_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("H_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("M_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("N_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td>") Else Response.Write("<td align=center><input type=checkbox disabled></td>") 'End If
                        If DT1.Rows(i)("U_Tru_10").ToString = 1 Then Response.Write("<td align=center><input type=checkbox checked disabled></td></tr>") Else Response.Write("<td align=center><input type=checkbox disabled></td></tr>") 'End If
                        Response.Write("</table></td></tr>")
                        '--End Grid
                        Response.Write("<tr><td></td><td><b>Overall Task Rating:&nbsp;&nbsp;<font color=blue>" & getRating(DT1.Rows(i)("Rating10").ToString) & "</font></b></td></tr>")
                        Response.Write("<tr><td></td><td><b>Comments:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Comments10").ToString & "</td></tr>")
                        Response.Write("<tr><td></td><td><hr color=gray></td></tr>")
                        Response.Write("</table></td>")
                        '--End  Tasks#10 table
                    End If

                    Response.Write("<tr><td style=height:1px;>&nbsp;&nbsp;</td><td></td><td>&nbsp;&nbsp;</td></tr>")
                    Response.Write("<tr><td>&nbsp;&nbsp;</td><td class=Style1><u>Overall Summary</u></td><td>&nbsp;&nbsp;</td></tr>")
                    Response.Write("<tr><td>&nbsp;&nbsp;</td><td><font color=red><b><u>Overall Appraisal Rating:</u></font><font color=blue> " & getRating(DT1.Rows(i)("Overall_Rating").ToString) & "</td><td>&nbsp;&nbsp;</td></tr>")
                    Response.Write("<tr><td>&nbsp;&nbsp;</td><td><hr color=gray></td><td>&nbsp;&nbsp;</td></tr>")
                    Response.Write("<tr><td>&nbsp;&nbsp;</td><td><font color=red><b><u>Manager's Overall Peformance Comments:</u></b></font> " & Replace(Replace(DT1.Rows(i)("MGR_comments").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td><td>&nbsp;&nbsp;</td></tr>")
                    Response.Write("<tr><td>&nbsp;&nbsp;</td><td><hr color=gray></td><td>&nbsp;&nbsp;</td></tr>")

                    Response.Write("<tr><td>&nbsp;&nbsp;</td><td></td><td>&nbsp;&nbsp;</td></tr>")
                    Response.Write("<tr><td>&nbsp;&nbsp;</td><td align=center><font color=red><b>" & DT1.Rows(i)("Guild_Name").ToString & " has submitted appraisal on " & DT1.Rows(i)("Esign").ToString & "</b></font></td><td>&nbsp;&nbsp;</td></tr>")

                    Response.Write("</td><td>&nbsp;</td></tr></table>")

                Next

                LocalClass.CloseSQLServerConnection()


            Else
                'Response.Write("Manager's appraisal for 2015")
                'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8) & "<br>" & Session("NAME") & " --  " & Session("EMPLID")) : Response.End()
                SQL = "select distinct last LastName,(select count(*)CNT from ps_employees where supervisor_id=A.emplid)EMPL_Supervision,last+' '+first Name, A.*,B.Accomplishment,deptid1,Departname,jobtitle1,convert(char(10),hire_date,101)Hired,"
                SQL = SQL & " (select First+' '+Last from id_tbl where emplid=Sup_EMPLID)SUP_NAME,IsNULL((select First+' '+Last from id_tbl where emplid=UP_MGT_EMPLID),'N/A')UP_MGT_NAME,IsNULL((select First+' '+Last from id_tbl where emplid=HR_EMPLID),'N/A')HR_Name,"
                SQL = SQL & " (Lead_Change+Inspire_Risk+Leverage_External+Communicate_Impact+Lead_Urgency+Promote_Collaboration+Confront_Challenges+Make_Balance+Build_Trust+Learn_Continuously)Addendum from (select * from ME_Appraisal_Master_tbl)A,"
                SQL = SQL & " (select * from ME_Appraisal_Accomplishments_TBL where perf_year=2015)B,id_tbl C where a.emplid=b.emplid and a.emplid=c.emplid and New_Employee=0 and a.emplid =" & Mid(Request.QueryString("Token"), 5, 9) & ""
                DT1 = LocalClass.ExecuteSQLDataSet(SQL)

                For i = 0 To DT1.Rows.Count - 1

                    Response.Write("<table border=0 class=StyleBreak width=100%>")
                    Response.Write("<tr><td width=10%>&nbsp;</td><td style= text-align:center><img src=../../../images/CR_logo.png style=width:380px; height:50px/></td><td width=10%>&nbsp;</td></tr>")
                    Response.Write("<tr><td style=Style3>&nbsp;</td><td align=center style=color: #00ae4d;font-family:Calibri;font-size:24px;font-weight:bold;><u>FY2015 PERFORMANCE APPRAISAL</u></td><td>&nbsp;</td></tr>")
                    Response.Write("<tr><td style=Style3>&nbsp;</td><td class=Style2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>&nbsp;</td></tr>")
                    Response.Write("<tr><td>&nbsp;</td><td>")

                    Response.Write("<table border=0 width=100% class=Style4>")
                    '--Row 1--
                    Response.Write("<tr><td width=8%><b>Name:</b></td><td width=30%>" & DT1.Rows(i)("Name").ToString & "</td><td width=18%>&nbsp;</td><td>&nbsp;</td><td><b>E-Signed:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Date_Employee_Esign").ToString & "</td></tr>")

                    '--Row 2--
                    Response.Write("<tr><td><b>Title:</td><td>" & DT1.Rows(i)("jobtitle1").ToString & "</td>")
                    Response.Write("<td width=18%><b>Manager Name:</b></td><td>" & DT1.Rows(i)("SUP_NAME").ToString & "</td><td><b>Approved:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Date_Sent").ToString & "</td></tr>")

                    '--Row 3--
                    Response.Write("<tr><td><b>Department:</b></td><td>" & DT1.Rows(i)("Departname").ToString & "</td>")
                    Response.Write("<td nowrap=nowrap><b>Former Manager:&nbsp;<b></td><td width=15%>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td><td><b>Approved:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Date_Submitted_To_HR").ToString & "</td></tr>")

                    '--Row 4--
                    Response.Write("<tr><td><b>Hire Date:</b></td><td>" & DT1.Rows(i)("Hired").ToString & "</td>")
                    Response.Write("<td><b>HR Generalist:</b></td><td>" & DT1.Rows(i)("HR_NAME").ToString & "</td><td><b>Approved:</b>&nbsp;&nbsp;" & DT1.Rows(i)("Date_HR_Approved").ToString & "</td></tr>")

                    Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")

                    Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Accomplishments:</b></u></font> (Summarize accomplishments vs the goals/expectations established for FY15)</td></tr>")
                    Response.Write("<tr><td colspan=6><table border=0 cellpadding=0 cellspacing=0 width=100% class=Style7><tr><td>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr></table></td></tr>")

                    Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Strengths:</b></u></font> (Comment on key strengths and highlight areas performed well this year)</td></tr>")
                    Response.Write("<tr><td colspan=6><table border=0 cellpadding=0 cellspacing=0 width=100% class=Style7><tr><td>" & Replace(Replace(DT1.Rows(i)("Strenghts").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr></table></td></tr>")

                    Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Development Areas:</b></u></font> (Comment on areas of performance that require futher development or improvement. Cite examples of areas of performance that could have been better this year)</td></tr>")
                    Response.Write("<tr><td colspan=6><table border=0 cellpadding=0 cellspacing=0 width=100% class=Style7><tr><td>" & Replace(Replace(DT1.Rows(i)("DevelopmentAreas").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr></table></td></tr>")


                    Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Overall Performance Rating:</b></u></font> (Check the box that most appropriately describes the individual's overall performance)</td></tr>")

                    Response.Write("<tr><td colspan=6>")
                    Response.Write("<table border=1 cellpadding=0 cellspacing=0 width=100% align=center class=Style7>")

                    Response.Write("<tr style=background-color:#E7E8E3;><td align=center><b>Underperforming</b></td><td align=center><b>Developing/Improving Contributor</b></td><td align=center><b>Solid Contributor</b></td>")
                    Response.Write("<td align=center><b>Very Strong Contributor</b></td><td align=center><b>Distinguished Contributor</b></td></tr>")

                    Response.Write("<tr>")
                    If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                    If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                    If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                    If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                    If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")

                    Response.Write("</tr></table></td></tr>")

                    Response.Write("<tr><td colspan=6><font size=4px color=red><u><b>Overall Summary:</b></u></font> (Comment on overall performance in FY15)</td></tr>")
                    Response.Write("<tr><td colspan=6><table border=0 cellpadding=0 cellspacing=0 width=100% class=Style7><tr><td>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr></table></td></tr>")
                    Response.Write("<tr><td colspan=6>&nbsp;</td></tr>")


                    If CDbl(DT1.Rows(i)("EMPL_Supervision").ToString) = 0 And CDbl(DT1.Rows(i)("Addendum").ToString) > 0 Then
                        'EMPL_Supervision>0 and Addendum>0

                        Response.Write("<tr><td colspan=6 align=center style=color: #00ae4d;font-family:Calibri;font-size:24px;font-weight:bold;><u>Addendum - FY16 Leadership Competencies</u></td></tr>")
                        Response.Write("<tr><td colspan=6>Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies. Use this assessment to provide feedback and guide your direct report on areas to focus on in FY16 for further growth and development.</td></tr>")

                        Response.Write("<tr><td colspan=6><table border=1 width=100%  cellpadding=0 cellspacing=0>")
                        Response.Write("<tr align=center><td width=55%><table width=100% border=0><tr><td width=40%></td><td style=font-family:Calibri;font-size:18px;font-weight:bold;>LEADING ONESELF</td></tr></table></td>")
                        Response.Write("<td width=15% style=background-color:#E7E8E3;><b>Needs<br>Development/Improvement</b></td><td width=15% style=background-color:#E7E8E3;><b>Proficient</b></td><td width=15% style=background-color:#E7E8E3;><b>Excels</b></td></tr>")

                        Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Make Balanced Decisions</td></tr></table></td>")
                        If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;background-color:#E7E8E3;><tr><td width=45%></td><td>Build Trust</td></tr></table></td>")
                        If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Learn Continuously</td></tr></table></td>")
                        If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("<tr><td colspan=4><table width=55%><tr><td width=40%></td><td style=font-family:Calibri;font-size:18px;font-weight:bold;>LEADING OTHERS</td></tr></table></td></tr>")
                        Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Lead with Urgency & Purpose</td></tr></table></td>")
                        If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Promote Collaboration & Accountability</td></tr></table></td>")
                        If DT1.Rows(i)("Promote_Collaboration").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Promote_Collaboration").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Promote_Collaboration").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Confront Challenges</td></tr></table></td>")
                        If DT1.Rows(i)("Confront_Challenges").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Confront_Challenges").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Confront_Challenges").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("<tr><td colspan=4><table width=55%><tr><td width=40%></td><td style=font-family:Calibri;font-size:18px;font-weight:bold;>LEADING THE ORGANIZATION</td></tr></table></td></tr>")
                        Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Lead Change</td></tr></table></td>")
                        If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("<tr align=center><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Inspire Risk Taking & innovation</td></tr></table></td>")
                        If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Leverage External Perspective</td></tr></table></td>")
                        If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("<tr><td width=55%><table width=100% style=background-color:#E7E8E3;font-family:Calibri;font-size:18px;font-weight:bold;><tr><td width=45%></td><td>Communicate for Impact</td></tr></table></td>")
                        If DT1.Rows(i)("Communicate_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Communicate_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio disabled checked></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                        If DT1.Rows(i)("Communicate_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio disabled checked></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                        Response.Write("</table></td></tr>")

                    End If
                    Response.Write("<tr><td colspan=6>&nbsp;</td></tr></table>")
                    Response.Write("<td style=Style3>&nbsp;</td></td><td>&nbsp;</td></tr></table>")

                Next
                LocalClass.CloseSQLServerConnection()
            End If
        End If


    End Sub
    Protected Sub Individual_Appraisal_New()
        'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8)) : Response.End()
        '--Managers information---
        SQL1 = " select *,convert(char(10),Hire_Date,101)Hired from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,(select last from id_tbl where emplid=a.emplid)LastName,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.mgt_emplid)MGT_Name,(select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B where A.emplid=B.emplid and"
        SQL1 &= " A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & ") AA JOiN (select emplid,Goals1,Milestones1,TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,"
        SQL1 &= " IsNull(TargetDate2,'')TargetDate2,IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,"
        SQL1 &= " IsNull(TargetDate4,'')TargetDate4,IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,"
        SQL1 &= " IsNull(TargetDate6,'')TargetDate6,IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,"
        SQL1 &= " IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,"
        SQL1 &= " IsNull(TargetDate10,'')TargetDate10 from(select A8.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,"
        SQL1 &= " Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9,Goals10,Milestones10,TargetDate10"
        SQL1 &= " from(select A7.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,"
        SQL1 &= " Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9 from(select A6.Emplid,Goals1,Milestones1,TargetDate1,"
        SQL1 &= " Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,"
        SQL1 &= " Goals8,Milestones8,TargetDate8 from(select A5.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,"
        SQL1 &= " Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7 from(select A4.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,"
        SQL1 &= " Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6 from(select A3.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,"
        SQL1 &= " TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5 from(select A2.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,"
        SQL1 &= " Milestones4,TargetDate4 from(select A1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3 from(select AC1.Emplid,Goals1,Milestones1,TargetDate1,"
        SQL1 &= " Goals2,Milestones2,TargetDate2 from (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoals_tbl where IndexID=1 and Perf_Year=" & Session("YEAR") + 1 & "  and Appr_Goals=0)Ac1 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoals_tbl where IndexID=2 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac2 ON Ac1.emplid=Ac2.emplid)A1 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoals_tbl where IndexID=3 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac3 ON A1.emplid=Ac3.emplid)A2 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoals_tbl where IndexID=4 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac4 ON A2.emplid=Ac4.emplid )A3 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoals_tbl where IndexID=5 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac5 ON A3.emplid=Ac5.emplid)A4 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoals_tbl where IndexID=6 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac6 ON A4.emplid=Ac6.emplid)A5 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoals_tbl where IndexID=7 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac7 ON A5.emplid=Ac7.emplid )A6 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoals_tbl where IndexID=8 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac8 ON A6.emplid=Ac8.emplid)A7 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoals_tbl where IndexID=9 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac9 ON A7.emplid=Ac9.emplid)A8 "
        SQL1 &= " LEFT JOIN (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoals_tbl where IndexID=10 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac10"
        SQL1 &= " ON A8.emplid=Ac10.emplid)A10 )BB ON aa.emplid=bb.emplid and aa.emplid=" & Mid(Request.QueryString("Token"), 4, 8) & ""
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<table border=0 class=StyleBreak width=100%>")
            Response.Write("<tr><td class=auto-style1><img src=../../../images/CR_logo.png /></td></tr>")
            Response.Write("<tr><td class=auto-style1><u>Performance Appraisal (FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & ")</u></td></tr>")
            Response.Write("<tr><td class=auto-style1>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td >")
            Response.Write("<table style=width:100%>")
            Response.Write(" <tr><td style=width:7%; font-family: Calibri;><b>Name:</b></td>")
            Response.Write("<td style=width:30%;>" & DT1.Rows(i)("Name").ToString & "</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td style=width:5%; font-family: Calibri;>E-Signed:&nbsp;&nbsp;</td>")

            If DT1.Rows(i)("Process_Flag").ToString = 5 Then
                Response.Write("<td style=width:20%;>" & DT1.Rows(i)("DateEmpl_Appr").ToString & "</td></tr>")
            ElseIf DT1.Rows(i)("Process_Flag").ToString = 4 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            Else
                Response.Write("<td style=width:20%;></td></tr>")
            End If
            Response.Write("<tr><td><b>Title:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Jobtitle").ToString & "</td>")
            Response.Write("<td style=width:12%; font-family: Calibri;><b>Manager Name:&nbsp;</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("MGT_Name").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 1 Then
                Response.Write("<td>" & DT1.Rows(i)("DateSub_UP_MGT").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If
            Response.Write("<tr><td style=font-family: Calibri;><b>Department:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Department").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;><b>Former Manager:&nbsp;</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td>")
            'Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")
            Response.Write("<td></td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 1 Then
                'Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                Response.Write("<td></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 2 Then
                'Response.Write("<td>" & DT1.Rows(i)("DateSub_HR").ToString & "</td></tr>")
                Response.Write("<td></td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If
            Response.Write("<tr><td style=font-family: Calibri;><b>Hire Date:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Hired").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;><b>Human Resources Generalist:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("HR_NAME").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 2 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 3 Then
                Response.Write("<td>" & DT1.Rows(i)("DateHR_Appr").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")
            '--1. Accomplishments--
            Response.Write("<table border=0><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class=style7A>(Summarize accomplishments vs the goals/expectations established)</span></td></tr></table>")
            Response.Write("</td></tr><tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--1END Accomplishments--
            Response.Write("</td></tr>")

            Response.Write("<tr><td>")

            '--Leadership Competencies: --  
            Response.Write("<table border=0><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Leadership Competencies:</u>&nbsp;<span class=style7A>(Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies)</span></td></tr></table>")
            Response.Write("<table border=1 style=border-spacing:0; border-collapse:collapse; width=100%>")
            Response.Write("<tr><td class=style9>&nbsp;</td>")
            Response.Write("<td class=style9><b>Needs Development/ Improvement</b></td>")
            Response.Write("<td class=style9><b>Proficient</b></td>")
            Response.Write("<td class=style9><b>Excels</b></td></tr>")

            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Oneself</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Make Balanced Decisions</font></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;>Build Trust</td>")
            If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;>Learn Continuously</td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Other</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead with Urgency & Purpose</font></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Promote Collaboration & Accountability</font></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Confront Challenges</font></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading the Organization</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead Change</font></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Inspire Risk Taking & Innovation</font></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Leverage External Perspective</font></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            Response.Write("<tr><td style=height:33px; font-size:large;><font>Communicate for Impact</font></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

            'Response.Write("</table>")
            'Response.Write("</td></tr>")
            Response.Write("</table>")
            Response.Write("</td></tr>")
            '--END Leadership Competencies: --

            '--2. Strengths-- 
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class=style7A>(Comment on key strengths and highlight areas performed well)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Strengths").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--2. END Strengths-- 
            Response.Write("</td></tr>")
            '--3. Development Areas--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;<span class=style7A>(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Area").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray /></td></tr></table>")
            '--3. END Development--  
            Response.Write("</td></tr>")
            '--4. Overall Performance--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class=style7A> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>")
            Response.Write("<tr><td>")
            Response.Write("<table border=1 style=border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width=100%>")
            Response.Write("<tr><td class=style9><b>Unsatisfactory</b></td>")
            Response.Write("<td class=style9><b>Developing/Improving Contributor</b></td>")
            Response.Write("<td class=style9><b>Solid Contributor</b></td>")
            Response.Write("<td class=style9><b>Very Strong Contributor</b></td>")
            Response.Write("<td class=style9><b>Distinguished Contributor</b></td></tr><tr>")
            If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            Response.Write("</tr></table>")
            Response.Write("</td></tr></table>")
            '--4. END Overall Performance--  
            Response.Write("</td></tr>")
            '--5. Overall Summary Waiting approval--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class=style7A>(Comment on overall performance)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td><hr /></td></tr>")
            Response.Write("</table>")
            '--5. END Overall Performance--    
            Response.Write("</td></tr>")
            '--6. Development Plan--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            'Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Plan:</u>&nbsp;<span class=style7A>(Based on development area, summarize a plan for professional and performance development)</span></td></tr>")
            'Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Objective").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            Response.Write("<tr><td ><hr /></td></tr>")
            Response.Write("</table>")
            Response.Write("</td></tr>")
            '--6. END Development Plan--
            Response.Write("<tr><td>")
            '--Divider--
            Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            Response.Write("<tr><td>&nbsp;</td></tr>")
            '--END Divider--
            Response.Write("<tr><td>")

            '--Divider--
            Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            '--END Divider--
            Response.Write("<tr><td>&nbsp;</td></tr>")
            '--
            Response.Write("<tr><td class=auto-style1><u>FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & " Goal-Setting Form")
            Response.Write("(06/01/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & " - 05/31/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & ")</u></td></tr>")

            '--Smart Goal-Setting Form
            Response.Write("<tr><td>&nbsp;</td></tr>")
            Response.Write("<tr><td><font size=medium font-family=Calibri>Enter SMART Goals")
            Response.Write("(<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to ")
            Response.Write("focus employees on achieving important, tangible outcomes that contribute to the achievement of department and strategic goals. Summarize the goals agreed to with the employee.")
            If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 > "17" Then
                Response.Write(" Please click <font color=blue><u>here</u></font> for a SMART goal example.")
            End If
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")

            Response.Write("<table border=1 style=border-collapse:collapse; border-spacing:0; width=100%>")
            Response.Write("<tr><td class=style10 width=3%;></td>")

            If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 = "17" Then
                Response.Write("<td class=style10 width=45%><b>Goals</b></td>")
                Response.Write("<td class=style10 width:40%;><b>Success Measures or Milestones</b></td>")
                Response.Write("<td class=style10><b>Target<br>Completion Date</b></td></tr>")
            Else
                Response.Write("<td class=style10 width=45%><b>Goals</b><div style=font-size:small; font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td>")
                Response.Write("<td class=style10 width:40%;><b>Key Results</b><div style=font-size:small;font-weight:lighter;font-style:italic>How will I know that I’ve accomplished each goal?</div> </td>")
                Response.Write("<td class=style10><b>Target<br>Completion Date</b><div style=font-size:small; font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td></tr>")
            End If

            Response.Write("<tr><td valign=top align=center><b>1)</b></td>")

            Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
            Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
            Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")

            If Len(DT1.Rows(i)("Goals2").ToString) + Len(DT1.Rows(i)("Milestones2").ToString) + Len(DT1.Rows(i)("TargetDate2").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>2)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals3").ToString) + Len(DT1.Rows(i)("Milestones3").ToString) + Len(DT1.Rows(i)("TargetDate3").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>3)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals4").ToString) + Len(DT1.Rows(i)("Milestones4").ToString) + Len(DT1.Rows(i)("TargetDate4").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>4)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals5").ToString) + Len(DT1.Rows(i)("Milestones5").ToString) + Len(DT1.Rows(i)("TargetDate5").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>5)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals6").ToString) + Len(DT1.Rows(i)("Milestones6").ToString) + Len(DT1.Rows(i)("TargetDate6").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>6)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals7").ToString) + Len(DT1.Rows(i)("Milestones7").ToString) + Len(DT1.Rows(i)("TargetDate7").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>7)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals8").ToString) + Len(DT1.Rows(i)("Milestones8").ToString) + Len(DT1.Rows(i)("TargetDate8").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>8)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals9").ToString) + Len(DT1.Rows(i)("Milestones9").ToString) + Len(DT1.Rows(i)("TargetDate9").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>9)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If

            If Len(DT1.Rows(i)("Goals10").ToString) + Len(DT1.Rows(i)("Milestones10").ToString) + Len(DT1.Rows(i)("TargetDate10").ToString) > 5 Then
                Response.Write("<tr><td valign=top align=center><b>10)</b></td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
            End If
            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("</table>")

        Next

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Terminated_Appraisal()
        If Session("EMPLID") = 6785 Or Session("EMPLID") = 6250 Or Session("EMPLID") = 6671 Then
        Else
        End If
    End Sub
    Protected Sub Terminated_Appraisal_New()

        If Session("EMPLID") = 6785 Or Session("EMPLID") = 6250 Or Session("EMPLID") = 6671 Then
            SQL1 = " select (case when SAP=14 then 1 else SAP end)OrderBy,*,convert(char(10),Hire_Date,101)Hired from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,(select last from id_tbl where emplid=a.emplid)LastName,"
            SQL1 &= " (select first+' '+last from id_tbl where emplid=a.mgt_emplid)MGT_Name,(select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,"
            SQL1 &= " (select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B  "
            SQL1 &= " where A.emplid=B.emplid /*and SAP14*/ and A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & ") AA JOiN (select emplid,Goals1,Milestones1,TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,"
            SQL1 &= " IsNull(TargetDate2,'')TargetDate2,IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,"
            SQL1 &= " IsNull(TargetDate4,'')TargetDate4,IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,"
            SQL1 &= " IsNull(TargetDate6,'')TargetDate6,IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,"
            SQL1 &= " IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,"
            SQL1 &= " IsNull(TargetDate10,'')TargetDate10 from(select A8.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,"
            SQL1 &= " Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9,Goals10,Milestones10,TargetDate10"
            SQL1 &= " from(select A7.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,"
            SQL1 &= " Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9 from(select A6.Emplid,Goals1,Milestones1,TargetDate1,"
            SQL1 &= " Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,"
            SQL1 &= " Goals8,Milestones8,TargetDate8 from(select A5.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,"
            SQL1 &= " Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7 from(select A4.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,"
            SQL1 &= " Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6 from(select A3.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,"
            SQL1 &= " TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5 from(select A2.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,"
            SQL1 &= " Milestones4,TargetDate4 from(select A1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3 from(select AC1.Emplid,Goals1,Milestones1,TargetDate1,"
            SQL1 &= " Goals2,Milestones2,TargetDate2 from (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoals_tbl where IndexID=1 and Perf_Year=" & Session("YEAR") + 1 & "  and Appr_Goals=0)Ac1 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoals_tbl where IndexID=2 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac2 ON Ac1.emplid=Ac2.emplid)A1 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoals_tbl where IndexID=3 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac3 ON A1.emplid=Ac3.emplid)A2 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoals_tbl where IndexID=4 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac4 ON A2.emplid=Ac4.emplid )A3 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoals_tbl where IndexID=5 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac5 ON A3.emplid=Ac5.emplid)A4 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoals_tbl where IndexID=6 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac6 ON A4.emplid=Ac6.emplid)A5 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoals_tbl where IndexID=7 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac7 ON A5.emplid=Ac7.emplid )A6 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoals_tbl where IndexID=8 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac8 ON A6.emplid=Ac8.emplid)A7 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoals_tbl where IndexID=9 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac9 ON A7.emplid=Ac9.emplid)A8 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoals_tbl where IndexID=10 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac10"
            SQL1 &= " ON A8.emplid=Ac10.emplid)A10 )BB ON aa.emplid=bb.emplid where  aa.emplid in (select emplid from id_tbl where status='I') order by OrderBy,LastName"
        Else
            SQL1 = " select (case when SAP=14 then 1 else SAP end)OrderBy,*,convert(char(10),Hire_Date,101)Hired from("

            'SQL1 &= " select (select first+' '+last from id_tbl where emplid=a.emplid)Name,(select last from id_tbl where emplid=a.emplid)LastName,"
            'SQL1 &= " (select first+' '+last from id_tbl where emplid=a.mgt_emplid)MGT_Name,(select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,"
            'SQL1 &= " (select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B  "
            'SQL1 &= " where A.emplid=B.emplid /*and SAP14*/ and A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & ""

            SQL1 &= " select (case when D.NEW_mgt_emplid is null or D.NEW_mgt_emplid='' then UP_MGT_Name else  (select first+' '+last from id_tbl where emplid=Rtrim(Ltrim(D.NEW_mgt_emplid))) end)UP_MGT_Name_New,Isnull(D.NEW_mgt_emplid,0)NEW_mgt_emplid,"
            SQL1 &= " C.* from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,(select last from id_tbl where emplid=a.emplid)LastName,(select first+' '+last from id_tbl where emplid=a.mgt_emplid)MGT_Name,"
            SQL1 &= " (select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment "
            SQL1 &= " from Appraisal_Master_tbl A JOIN Appraisal_Accomplishments_tbl B ON A.emplid=B.emplid where A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & ")C LEFT JOIN"
            SQL1 &= " (select A.EMPLID,mgt_emplid NEW_mgt_emplid from (select * from Appraisal_MasterHistory_tbl where perf_year=" & Session("YEAR") & ")A JOIN (select emplid,Max(DateTime)DateTime "
            SQL1 &= " from Appraisal_MasterHistory_tbl where perf_year=" & Session("YEAR") & " and login_emplid=0 group by emplid)B ON a.emplid=b.emplid and a.DateTime=b.DateTime)D ON C.emplid=D.emplid"

            SQL1 &= " ) AA JOiN (select emplid,Goals1,Milestones1,TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,"
            SQL1 &= " IsNull(TargetDate2,'')TargetDate2,IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,"
            SQL1 &= " IsNull(TargetDate4,'')TargetDate4,IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,"
            SQL1 &= " IsNull(TargetDate6,'')TargetDate6,IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,"
            SQL1 &= " IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,"
            SQL1 &= " IsNull(TargetDate10,'')TargetDate10 from(select A8.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,"
            SQL1 &= " Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9,Goals10,Milestones10,TargetDate10"
            SQL1 &= " from(select A7.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,"
            SQL1 &= " Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9 from(select A6.Emplid,Goals1,Milestones1,TargetDate1,"
            SQL1 &= " Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,"
            SQL1 &= " Goals8,Milestones8,TargetDate8 from(select A5.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,"
            SQL1 &= " Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7 from(select A4.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,"
            SQL1 &= " Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6 from(select A3.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,"
            SQL1 &= " TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5 from(select A2.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,"
            SQL1 &= " Milestones4,TargetDate4 from(select A1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3 from(select AC1.Emplid,Goals1,Milestones1,TargetDate1,"
            SQL1 &= " Goals2,Milestones2,TargetDate2 from (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoals_tbl where IndexID=1 and Perf_Year=" & Session("YEAR") + 1 & "  and Appr_Goals=0)Ac1 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoals_tbl where IndexID=2 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac2 ON Ac1.emplid=Ac2.emplid)A1 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoals_tbl where IndexID=3 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac3 ON A1.emplid=Ac3.emplid)A2 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoals_tbl where IndexID=4 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac4 ON A2.emplid=Ac4.emplid )A3 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoals_tbl where IndexID=5 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac5 ON A3.emplid=Ac5.emplid)A4 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoals_tbl where IndexID=6 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac6 ON A4.emplid=Ac6.emplid)A5 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoals_tbl where IndexID=7 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac7 ON A5.emplid=Ac7.emplid )A6 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoals_tbl where IndexID=8 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac8 ON A6.emplid=Ac8.emplid)A7 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoals_tbl where IndexID=9 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac9 ON A7.emplid=Ac9.emplid)A8 "
            SQL1 &= " LEFT JOIN (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoals_tbl where IndexID=10 and Perf_Year=" & Session("YEAR") + 1 & " and Appr_Goals=0)Ac10"
            SQL1 &= " ON A8.emplid=Ac10.emplid)A10 )BB ON aa.emplid=bb.emplid where  aa.emplid in (select emplid from id_tbl where status='I') and deptid not in (9009120) order by OrderBy,LastName"
        End If
        'Response.Write(SQL1) : Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        For i = 0 To DT1.Rows.Count - 1
            '------------------------GUILD FORM--------------------------
            If DT1.Rows(i)("SAP").ToString = 14 Then

                Response.Write("<table border=0 class=StyleBreak width=100%>")
                Response.Write("<tr><td class=auto-style1><img src=../../../images/CR_logo.png /></td></tr>")
                Response.Write("<tr><td class=auto-style1><u>Performance Appraisal (FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & ")</u></td></tr>")
                Response.Write("<tr><td class=auto-style1>&nbsp;&nbsp;</td></tr>")
                Response.Write("<tr><td >")
                Response.Write("<table style=width:100%>")
                Response.Write(" <tr><td style=width:7%; font-family: Calibri;><b>Name:</b></td>")
                Response.Write("<td style=width:30%;>" & DT1.Rows(i)("Name").ToString & "</td>")
                Response.Write("<td>&nbsp;&nbsp;</td>")
                Response.Write("<td>&nbsp;&nbsp;</td>")
                Response.Write("<td style=width:5%; font-family: Calibri;>E-Signed:&nbsp;&nbsp;</td>")

                If DT1.Rows(i)("Process_Flag").ToString = 5 Then
                    Response.Write("<td style=width:20%;>" & DT1.Rows(i)("DateEmpl_Appr").ToString & "</td></tr>")
                ElseIf DT1.Rows(i)("Process_Flag").ToString = 4 Then
                    Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                Else
                    Response.Write("<td style=width:20%;></td></tr>")
                End If

                Response.Write("<tr><td><b>Title:</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("Jobtitle").ToString & "</td>")
                Response.Write("<td style=width:12%; font-family: Calibri;><b>Manager Name:&nbsp;</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("MGT_Name").ToString & "</td>")
                Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

                If CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 1 Then
                    Response.Write("<td>" & DT1.Rows(i)("DateSub_UP_MGT").ToString & "</td></tr>")
                Else
                    Response.Write("<td></td></tr>")
                End If

                Response.Write("<tr><td style=font-family: Calibri;><b>Department:</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("Department").ToString & "</td>")

                If Session("YEAR") < 2016 Then
                    Response.Write("<td style=font-family: Calibri;><b>2nd Level Manager Name:&nbsp;</b></td>")
                    Response.Write("<td>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td>")
                Else
                    If CDbl(DT1.Rows(i)("New_Mgt_Emplid").ToString) = 0 Then
                        Response.Write("<td style=font-family: Calibri;><b>2nd Level Manager Name:&nbsp;</b></td>")
                        Response.Write("<td>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td>")
                    Else
                        Response.Write("<td style=font-family: Calibri;><b>Former Manager:&nbsp;</b></td>")
                        Response.Write("<td>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td>")
                    End If
                End If


                'Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")
                Response.Write("<td></td>")

                If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 1 Then
                    'Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                    Response.Write("<td></td></tr>")
                ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 2 Then
                    'Response.Write("<td>" & DT1.Rows(i)("DateSub_HR").ToString & "</td></tr>")
                    Response.Write("<td></td></tr>")
                Else
                    Response.Write("<td></td></tr>")
                End If

                Response.Write("<tr><td style=font-family: Calibri;><b>Hire Date:</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("Hired").ToString & "</td>")
                Response.Write("<td style=font-family: Calibri;><b>Human Resources Generalist:</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("HR_NAME").ToString & "</td>")
                Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

                If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 2 Then
                    Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 3 Then
                    Response.Write("<td>" & DT1.Rows(i)("DateHR_Appr").ToString & "</td></tr>")
                Else
                    Response.Write("<td></td></tr>")
                End If

                Response.Write("</table>")
                Response.Write("</td></tr>")
                Response.Write("<tr><td>")

                '--1. Accomplishments--
                Response.Write("<table border=0><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class=style7A>(Summarize accomplishments vs the goals/expectations established)</span></td></tr></table>")
                Response.Write("</td></tr><tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr color=gray></td></tr></table>")
                '--1END Accomplishments--
                Response.Write("</td></tr>")

                '--2. Strengths-- 
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class=style7A>(Comment on key strengths and highlight areas performed well)</span></td></tr>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Strengths").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr color=gray></td></tr></table>")
                '--2. END Strengths-- 
                Response.Write("</td></tr>")
                '--3. Development Areas--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;<span class=style7A>(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Area").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr color=gray /></td></tr></table>")
                '--3. END Development--  
                Response.Write("</td></tr>")
                '--4. Overall Performance--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class=style7A> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>")
                Response.Write("<tr><td>")
                Response.Write("<table border=1 style=border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width=100%>")
                Response.Write("<tr><td class=style9><b>Unsatisfactory</b></td>")
                Response.Write("<td class=style9><b>Developing/Improving Contributor</b></td>")
                Response.Write("<td class=style9><b>Solid Contributor</b></td>")
                Response.Write("<td class=style9><b>Very Strong Contributor</b></td>")
                Response.Write("<td class=style9><b>Distinguished Contributor</b></td></tr><tr>")
                If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                Response.Write("</tr></table>")
                Response.Write("</td></tr></table>")
                '--4. END Overall Performance--  
                Response.Write("</td></tr>")
                '--5. Overall Summary Waiting approval--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class=style7A>(Comment on overall performance)</span></td></tr>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr /></td></tr>")
                Response.Write("</table>")
                '--5. END Overall Performance--    
                Response.Write("</td></tr>")
                '--6. Development Plan--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                'Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Plan:</u>&nbsp;<span class=style7A>(Based on development area, summarize a plan for professional and performance development)</span></td></tr>")
                'Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Objective").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td ><hr /></td></tr>")
                Response.Write("</table>")
                Response.Write("</td></tr>")
                '--6. END Development Plan--
                Response.Write("<tr><td>")
                '--Divider--
                Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
                Response.Write("</td></tr>")
                Response.Write("<tr><td>&nbsp;</td></tr>")
                '--END Divider--
                Response.Write("<tr><td>")
                '--Addendum --  
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style1><u>Addendum:&nbsp;Leadership Competencies</u></td></tr><tr><td></td></tr>")
                Response.Write("<tr><td class=style7>Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies. Use this as a guide to provide feedback and guide your direct report on areas to focus on for further growth and development.")
                Response.Write("<table border=1 style=border-spacing:0; border-collapse:collapse; width=100%>")
                Response.Write("<tr><td class=style9>&nbsp;</td>")
                Response.Write("<td class=style9><b>Needs Development/ Improvement</b></td>")
                Response.Write("<td class=style9><b>Proficient</b></td>")
                Response.Write("<td class=style9><b>Excels</b></td></tr>")

                Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Oneself</b></span></td></tr>")
                Response.Write("<tr><td style=height:33px; font-size:large;><font>Make Balanced Decisions</font></td>")
                If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;>Build Trust</td>")
                If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;>Learn Continuously</td>")
                If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Other</b></span></td></tr>")
                Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead with Urgency & Purpose</font></td>")
                If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Promote Collaboration & Accountability</font></td>")
                If DT1.Rows(i)("Promote_Collab").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Promote_Collab").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Promote_Collab").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Confront Challenges</font></td>")
                If DT1.Rows(i)("Confront_Challenge").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Confront_Challenge").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Confront_Challenge").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading the Organization</b></span></td></tr>")
                Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead Change</font></td>")
                If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Inspire Risk Taking & Innovation</font></td>")
                If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Leverage External Perspective</font></td>")
                If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Communicate for Impact</font></td>")
                If DT1.Rows(i)("Communic_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Communic_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Communic_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("</table>")
                Response.Write("</td></tr>")
                Response.Write("</table>")
                Response.Write("</td></tr>")
                '--END Addendum --
                '--2. Strengths-- 
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class=style7A>(Comment on key strengths and highlight areas performed well)</span></td></tr>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Strengths").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr color=gray></td></tr></table>")
                '--2. END Strengths-- 
                Response.Write("</td></tr>")
                '--3. Development Areas--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;<span class=style7A>(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Area").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr color=gray /></td></tr></table>")
                '--3. END Development--  
                Response.Write("</td></tr>")
                '--4. Overall Performance--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class=style7A> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>")
                Response.Write("<tr><td>")
                Response.Write("<table border=1 style=border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width=100%>")
                Response.Write("<tr><td class=style9><b>Unsatisfactory</b></td>")
                Response.Write("<td class=style9><b>Developing/Improving Contributor</b></td>")
                Response.Write("<td class=style9><b>Solid Contributor</b></td>")
                Response.Write("<td class=style9><b>Very Strong Contributor</b></td>")
                Response.Write("<td class=style9><b>Distinguished Contributor</b></td></tr><tr>")
                If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                Response.Write("</tr></table>")
                Response.Write("</td></tr></table>")
                '--4. END Overall Performance--  
                Response.Write("</td></tr>")
                '--5. Overall Summary Waiting approval--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class=style7A>(Comment on overall performance)</span></td></tr>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr /></td></tr>")
                Response.Write("</table>")
                '--5. END Overall Performance--    
                Response.Write("</td></tr>")
                '--6. Development Plan--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                'Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Plan:</u>&nbsp;<span class=style7A>(Based on development area, summarize a plan for professional and performance development)</span></td></tr>")
                'Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Objective").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td ><hr /></td></tr>")
                Response.Write("</table>")
                Response.Write("</td></tr>")
                '--6. END Development Plan--
                Response.Write("<tr><td>")
                '--Divider--
                Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
                Response.Write("</td></tr>")
                Response.Write("<tr><td>&nbsp;</td></tr>")
                '--END Divider--
                Response.Write("<tr><td>&nbsp;</td></tr>")

                '--Smart Goal-Setting Start here-- 
                Response.Write("<tr><td class=auto-style1><u>FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & " Goal-Setting Form")
                Response.Write("(06/01/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & " - 05/31/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & ")</u></td></tr>")
                '--Smart Goal-Setting Form
                Response.Write("<tr><td>&nbsp;</td></tr>")
                Response.Write("<tr><td><font size=medium font-family=Calibri>Enter SMART Goals")
                Response.Write("(<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to ")
                Response.Write("focus employees on achieving important, tangible outcomes that contribute to the achievement of department and strategic goals. Summarize the goals agreed to with the employee.")
                If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 > "17" Then
                    Response.Write(" Please click <font color=blue><u>here</u></font> for a SMART goal example.")
                End If
                Response.Write("</td></tr>")
                Response.Write("<tr><td>")

                Response.Write("<table border=1 style=border-collapse:collapse; border-spacing:0; width=100%>")
                Response.Write("<tr><td class=style10 width=3%;></td>")

                If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 = "17" Then
                    Response.Write("<td class=style10 width=45%><b>Goals</b></td>")
                    Response.Write("<td class=style10 width:40%;><b>Success Measures or Milestones</b></td>")
                    Response.Write("<td class=style10><b>Target<br>Completion Date</b></td></tr>")
                Else
                    Response.Write("<td class=style10 width=45%><b>Goals</b><div style=font-size:small; font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td>")
                    Response.Write("<td class=style10 width:40%;><b>Key Results</b><div style=font-size:small;font-weight:lighter;font-style:italic>How will I know that I’ve accomplished each goal?</div> </td>")
                    Response.Write("<td class=style10><b>Target<br>Completion Date</b><div style=font-size:small; font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td></tr>")
                End If

                Response.Write("<tr><td valign=top align=center><b>1)</b></td>")

                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")

                If Len(DT1.Rows(i)("Goals2").ToString) + Len(DT1.Rows(i)("Milestones2").ToString) + Len(DT1.Rows(i)("TargetDate2").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>2)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals3").ToString) + Len(DT1.Rows(i)("Milestones3").ToString) + Len(DT1.Rows(i)("TargetDate3").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>3)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals4").ToString) + Len(DT1.Rows(i)("Milestones4").ToString) + Len(DT1.Rows(i)("TargetDate4").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>4)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals5").ToString) + Len(DT1.Rows(i)("Milestones5").ToString) + Len(DT1.Rows(i)("TargetDate5").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>5)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals6").ToString) + Len(DT1.Rows(i)("Milestones6").ToString) + Len(DT1.Rows(i)("TargetDate6").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>6)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals7").ToString) + Len(DT1.Rows(i)("Milestones7").ToString) + Len(DT1.Rows(i)("TargetDate7").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>7)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals8").ToString) + Len(DT1.Rows(i)("Milestones8").ToString) + Len(DT1.Rows(i)("TargetDate8").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>8)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals9").ToString) + Len(DT1.Rows(i)("Milestones9").ToString) + Len(DT1.Rows(i)("TargetDate9").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>9)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals10").ToString) + Len(DT1.Rows(i)("Milestones10").ToString) + Len(DT1.Rows(i)("TargetDate10").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>10)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If
                Response.Write("</table>")
                Response.Write("</td></tr>")
                Response.Write("</table>")


                '--------------------------------------------MANAGER FORM--------------------------
            Else
                'Response.Write("<hr />")
                'Response.Write("<center><font color=red><b>MANAGER FORM</center>")
                'Response.Write("<hr />")

                Response.Write("<table border=0 class=StyleBreak width=100%>")
                Response.Write("<tr><td class=auto-style1><img src=../../../images/CR_logo.png /></td></tr>")
                Response.Write("<tr><td class=auto-style1><u>Performance Appraisal (FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & ")</u></td></tr>")
                Response.Write("<tr><td class=auto-style1>&nbsp;&nbsp;</td></tr>")
                Response.Write("<tr><td >")
                Response.Write("<table style=width:100%>")
                Response.Write(" <tr><td style=width:7%; font-family: Calibri;><b>Name:</b></td>")
                Response.Write("<td style=width:30%;>" & DT1.Rows(i)("Name").ToString & "</td>")
                Response.Write("<td>&nbsp;&nbsp;</td>")
                Response.Write("<td>&nbsp;&nbsp;</td>")
                Response.Write("<td style=width:5%; font-family: Calibri;>E-Signed:&nbsp;&nbsp;</td>")

                If DT1.Rows(i)("Process_Flag").ToString = 5 Then
                    Response.Write("<td style=width:20%;>" & DT1.Rows(i)("DateEmpl_Appr").ToString & "</td></tr>")
                ElseIf DT1.Rows(i)("Process_Flag").ToString = 4 Then
                    Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                Else
                    Response.Write("<td style=width:20%;></td></tr>")
                End If
                Response.Write("<tr><td><b>Title:</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("Jobtitle").ToString & "</td>")
                Response.Write("<td style=width:12%; font-family: Calibri;><b>Manager Name:&nbsp;</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("MGT_Name").ToString & "</td>")
                Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

                If CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 1 Then
                    Response.Write("<td>" & DT1.Rows(i)("DateSub_UP_MGT").ToString & "</td></tr>")
                Else
                    Response.Write("<td></td></tr>")
                End If
                Response.Write("<tr><td style=font-family: Calibri;><b>Department:</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("Department").ToString & "</td>")

                If Session("YEAR") < 2016 Then
                    Response.Write("<td style=font-family: Calibri;><b>2nd Level Manager Name:&nbsp;</b></td>")
                    Response.Write("<td>" & DT1.Rows(i)("UP_MGT_NAME").ToString & "</td>")
                Else
                    If Len(CDbl(DT1.Rows(i)("New_Mgt_Emplid").ToString)) = 1 Then
                        Response.Write("<td style=font-family: Calibri;><b>2nd Level Manager Name:&nbsp;</b></td>")
                        Response.Write("<td>" & DT1.Rows(i)("UP_MGT_NAME_NEW").ToString & "</td>")
                    Else
                        Response.Write("<td style=font-family: Calibri;><b>Former Manager:&nbsp;</b></td>")
                        Response.Write("<td>" & DT1.Rows(i)("UP_MGT_NAME_NEW").ToString & "</td>")
                    End If
                End If

                'Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")
                Response.Write("<td></td>")

                If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 1 Then
                    'Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                    Response.Write("<td></td></tr>")
                ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 2 Then
                    'Response.Write("<td>" & DT1.Rows(i)("DateSub_HR").ToString & "</td></tr>")
                    Response.Write("<td></td></tr>")
                Else
                    Response.Write("<td></td></tr>")
                End If
                Response.Write("<tr><td style=font-family: Calibri;><b>Hire Date:</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("Hired").ToString & "</td>")
                Response.Write("<td style=font-family: Calibri;><b>Human Resources Generalist:</b></td>")
                Response.Write("<td>" & DT1.Rows(i)("HR_NAME").ToString & "</td>")
                Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

                If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 2 Then
                    Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 3 Then
                    Response.Write("<td>" & DT1.Rows(i)("DateHR_Appr").ToString & "</td></tr>")
                Else
                    Response.Write("<td></td></tr>")
                End If

                Response.Write("</table>")
                Response.Write("</td></tr>")
                Response.Write("<tr><td>")
                '--1. Accomplishments--
                Response.Write("<table border=0><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class=style7A>(Summarize accomplishments vs the goals/expectations established)</span></td></tr></table>")
                Response.Write("</td></tr><tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr color=gray></td></tr></table>")
                '--1END Accomplishments--
                Response.Write("</td></tr>")

                Response.Write("<tr><td>")

                '--Leadership Competencies: --  
                Response.Write("<table border=0><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Leadership Competencies:</u>&nbsp;<span class=style7A>(Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies)</span></td></tr></table>")
                Response.Write("<table border=1 style=border-spacing:0; border-collapse:collapse; width=100%>")
                Response.Write("<tr><td class=style9>&nbsp;</td>")
                Response.Write("<td class=style9><b>Needs Development/ Improvement</b></td>")
                Response.Write("<td class=style9><b>Proficient</b></td>")
                Response.Write("<td class=style9><b>Excels</b></td></tr>")

                Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Oneself</b></span></td></tr>")
                Response.Write("<tr><td style=height:33px; font-size:large;><font>Make Balanced Decisions</font></td>")
                If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;>Build Trust</td>")
                If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;>Learn Continuously</td>")
                If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Other</b></span></td></tr>")
                Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead with Urgency & Purpose</font></td>")
                If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Promote Collaboration & Accountability</font></td>")
                If DT1.Rows(i)("Promote_Collab").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Promote_Collab").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Promote_Collab").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Confront Challenges</font></td>")
                If DT1.Rows(i)("Confront_Challenge").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Confront_Challenge").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Confront_Challenge").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading the Organization</b></span></td></tr>")
                Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead Change</font></td>")
                If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Inspire Risk Taking & Innovation</font></td>")
                If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Leverage External Perspective</font></td>")
                If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                Response.Write("<tr><td style=height:33px; font-size:large;><font>Communicate for Impact</font></td>")
                If DT1.Rows(i)("Communic_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Communic_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Communic_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")

                'Response.Write("</table>")
                'Response.Write("</td></tr>")
                Response.Write("</table>")
                Response.Write("</td></tr>")
                '--END Leadership Competencies: --

                '--2. Strengths-- 
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class=style7A>(Comment on key strengths and highlight areas performed well)</span></td></tr>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Strengths").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr color=gray></td></tr></table>")
                '--2. END Strengths-- 
                Response.Write("</td></tr>")
                '--3. Development Areas--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;<span class=style7A>(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Area").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr color=gray /></td></tr></table>")
                '--3. END Development--  
                Response.Write("</td></tr>")
                '--4. Overall Performance--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class=style7A> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>")
                Response.Write("<tr><td>")
                Response.Write("<table border=1 style=border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width=100%>")
                Response.Write("<tr><td class=style9><b>Unsatisfactory</b></td>")
                Response.Write("<td class=style9><b>Developing/Improving Contributor</b></td>")
                Response.Write("<td class=style9><b>Solid Contributor</b></td>")
                Response.Write("<td class=style9><b>Very Strong Contributor</b></td>")
                Response.Write("<td class=style9><b>Distinguished Contributor</b></td></tr><tr>")
                If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
                Response.Write("</tr></table>")
                Response.Write("</td></tr></table>")
                '--4. END Overall Performance--  
                Response.Write("</td></tr>")
                '--5. Overall Summary Waiting approval--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class=style7A>(Comment on overall performance)</span></td></tr>")
                Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td><hr /></td></tr>")
                Response.Write("</table>")
                '--5. END Overall Performance--    
                Response.Write("</td></tr>")
                '--6. Development Plan--
                Response.Write("<tr><td>")
                Response.Write("<table border=0 width=100%>")
                'Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Plan:</u>&nbsp;<span class=style7A>(Based on development area, summarize a plan for professional and performance development)</span></td></tr>")
                'Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Objective").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                Response.Write("<tr><td ><hr /></td></tr>")
                Response.Write("</table>")
                Response.Write("</td></tr>")
                '--6. END Development Plan--
                Response.Write("<tr><td>")
                '--Divider--
                Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
                Response.Write("</td></tr>")
                Response.Write("<tr><td>&nbsp;</td></tr>")
                '--END Divider--
                Response.Write("<tr><td>")

                '--Divider--
                Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
                Response.Write("</td></tr>")
                '--END Divider--
                Response.Write("<tr><td>&nbsp;</td></tr>")
                '--
                Response.Write("<tr><td class=auto-style1><u>FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & " Goal-Setting Form")
                Response.Write("(06/01/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & " - 05/31/" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 & ")</u></td></tr>")

                '--Smart Goal-Setting Form
                Response.Write("<tr><td>&nbsp;</td></tr>")
                Response.Write("<tr><td><font size=medium font-family=Calibri>Enter SMART Goals")
                Response.Write("(<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to ")
                Response.Write("focus employees on achieving important, tangible outcomes that contribute to the achievement of department and strategic goals. Summarize the goals agreed to with the employee.")
                If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 > "17" Then
                    Response.Write(" Please click <font color=blue><u>here</u></font> for a SMART goal example.")
                End If
                Response.Write("</td></tr>")
                Response.Write("<tr><td>")

                Response.Write("<table border=1 style=border-collapse:collapse; border-spacing:0; width=100%>")
                Response.Write("<tr><td class=style10 width=3%;></td>")
                If Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) + 1 = "17" Then
                    Response.Write("<td class=style10 width=45%><b>Goals</b></td>")
                    Response.Write("<td class=style10 width:40%;><b>Success Measures or Milestones</b></td>")
                    Response.Write("<td class=style10><b>Target<br>Completion Date</b></td></tr>")
                Else
                    Response.Write("<td class=style10 width=45%><b>Goals</b><div style=font-size:small;font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td>")
                    Response.Write("<td class=style10 width:40%;><b>Key Results</b><div style=font-size:small;font-weight:lighter;font-style:italic>How will I know that I’ve accomplished each goal?</div> </td>")
                    Response.Write("<td class=style10><b>Target<br>Completion Date</b><div style=font-size:small; font-weight:lighter;font-style:italic>By when do I need to accomplish each goal?</div></td></tr>")
                End If

                Response.Write("<tr><td valign=top align=center><b>1)</b></td>")

                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate1").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")

                If Len(DT1.Rows(i)("Goals2").ToString) + Len(DT1.Rows(i)("Milestones2").ToString) + Len(DT1.Rows(i)("TargetDate2").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>2)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate2").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals3").ToString) + Len(DT1.Rows(i)("Milestones3").ToString) + Len(DT1.Rows(i)("TargetDate3").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>3)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate3").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals4").ToString) + Len(DT1.Rows(i)("Milestones4").ToString) + Len(DT1.Rows(i)("TargetDate4").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>4)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate4").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals5").ToString) + Len(DT1.Rows(i)("Milestones5").ToString) + Len(DT1.Rows(i)("TargetDate5").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>5)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate5").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals6").ToString) + Len(DT1.Rows(i)("Milestones6").ToString) + Len(DT1.Rows(i)("TargetDate6").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>6)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate6").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals7").ToString) + Len(DT1.Rows(i)("Milestones7").ToString) + Len(DT1.Rows(i)("TargetDate7").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>7)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate7").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals8").ToString) + Len(DT1.Rows(i)("Milestones8").ToString) + Len(DT1.Rows(i)("TargetDate8").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>8)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate8").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals9").ToString) + Len(DT1.Rows(i)("Milestones9").ToString) + Len(DT1.Rows(i)("TargetDate9").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>9)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate9").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If

                If Len(DT1.Rows(i)("Goals10").ToString) + Len(DT1.Rows(i)("Milestones10").ToString) + Len(DT1.Rows(i)("TargetDate10").ToString) > 5 Then
                    Response.Write("<tr><td valign=top align=center><b>10)</b></td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Goals10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Milestones10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td>")
                    Response.Write("<td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("TargetDate10").ToString, Chr(13), "<p>"), Chr(10), "<p>") & "</td></tr>")
                End If
                Response.Write("</table>")
                Response.Write("</td></tr>")
                Response.Write("</table>")


            End If

        Next

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub Individual_Appraisal_18Forwad()
        'SQL1 = " select *,convert(char(10),Hire_Date,101)Hired from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,(select last from id_tbl where emplid=a.emplid)LastName,"
        'SQL1 &= " (select first+' '+last from id_tbl where emplid=a.mgt_emplid)MGT_Name,(select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,"
        'SQL1 &= " (select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B  "
        'SQL1 &= " where A.emplid=B.emplid and A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & ") AA where aa.emplid=" & Mid(Request.QueryString("Token"), 4, 8) & ""

        SQL1 = " select (case when Process_Flag<5 then MGT_Name else (select B.First+' '+B.Last MGT_Name from Appraisal_Master_Esign_tbl A JOIN id_tbl B ON A.MGT_Emplid=B.emplid "
        SQL1 &= " where A.emplid=D.emplid and Perf_Year=D.Perf_Year) end)Esign_MGT_Name, * from( "

        SQL1 &= " select IsNull(Collab_MGT_EMPLID,'')Collab_MGT_EMPLID,IsNull(Collab_MGT,'')Collab_MGT,BB.* from(select *,convert(char(10),Hire_Date,101)Hired from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,"
        SQL1 &= " (select last from id_tbl where emplid=a.emplid)LastName,(select first+' '+last from id_tbl where emplid in (select SUPERVISOR_ID from ps_employees where emplid=a.emplid))MGT_Name,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment "
        SQL1 &= " from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B  where A.emplid=B.emplid and A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & ") "
        SQL1 &= " AA)BB LEFT JOIN (select * from(select A.EMPLID,Employee,Current_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=A.Current_MGT_EMPLID)Current_MGT_Name,"
        SQL1 &= " B.MGT_EMPLID Collab_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=B.MGT_EMPLID)Collab_MGT from(select emplid,(First_name+' '+Last_name)Employee,"
        SQL1 &= " SUPERVISOR_EMPLID Current_MGT_EMPLID from HR_PDS_DATA_tbl)A JOIN Appraisal_MasterHistory_tbl B ON  A.emplid=B.emplid "
        SQL1 &= " where Perf_Year=" & Session("YEAR") & " and LOGIN_EMPLID=0 )C where Collab_MGT_EMPLID<>Current_MGT_EMPLID)CC ON BB.emplid=CC.emplid where BB.emplid=" & Mid(Request.QueryString("Token"), 4, 8) & " "

        SQL1 &= " )D"

        'Response.Write(SQL1) : Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<table border=0 class=StyleBreak width=100%>")
            Response.Write("<tr><td class=auto-style1><img src=../../../images/CR_logo.png /></td></tr>")
            Response.Write("<tr><td class=auto-style1><u>Performance Appraisal (FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & ")</u></td></tr>")
            Response.Write("<tr><td class=auto-style1>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td >")
            Response.Write("<table style=width:100%>")
            Response.Write(" <tr><td style=width:7%; font-family: Calibri;><b>Name:</b></td>")
            Response.Write("<td style=width:30%;>" & DT1.Rows(i)("Name").ToString & "</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td style=width:5%; font-family: Calibri;>E-Signed:&nbsp;&nbsp;</td>")

            If DT1.Rows(i)("Process_Flag").ToString = 5 Then
                Response.Write("<td style=width:20%;>" & DT1.Rows(i)("DateEmpl_Appr").ToString & "</td></tr>")
            ElseIf DT1.Rows(i)("Process_Flag").ToString = 4 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            Else
                Response.Write("<td style=width:20%;></td></tr>")
            End If

            Response.Write("<tr><td><b>Title:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Jobtitle").ToString & "</td>")
            Response.Write("<td style=width:12%; font-family: Calibri;><b>Manager Name:&nbsp;</b></td>")

            Response.Write("<td>" & DT1.Rows(i)("Esign_MGT_Name").ToString & "</td>")

            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 1 Then
                Response.Write("<td>" & DT1.Rows(i)("DateSub_HR").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If


            Response.Write("<tr><td style=font-family: Calibri;><b>Department:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Department").ToString & "</td>")

            If Len(DT1.Rows(i)("Collab_MGT").ToString) = 0 Then
                Response.Write("<td style=font-family: Calibri;><b>&nbsp;</b></td>")
            Else
                Response.Write("<td style=font-family: Calibri;><b>Former Manager:&nbsp;</b></td>")
            End If
            Response.Write("<td>" & DT1.Rows(i)("Collab_MGT").ToString & "</td>")
            'Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")
            Response.Write("<td></td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 1 Then
                'Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                Response.Write("<td></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 2 Then
                'Response.Write("<td>" & DT1.Rows(i)("DateSub_HR").ToString & "</td></tr>")
                Response.Write("<td></td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("<tr><td style=font-family: Calibri;><b>Hire Date:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Hired").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;><b>Human Resources Generalist:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("HR_NAME").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 2 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 3 Then
                Response.Write("<td>" & DT1.Rows(i)("DateHR_Appr").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")
            '--1. Accomplishments--
            Response.Write("<table border=0 width=100%><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class=style7A>(Summarize accomplishments vs the goals/expectations established)</span></td></tr></table>")
            Response.Write("</td></tr><tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--1END Accomplishments--
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")

            '--Addendum --  
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Competencies:</u>&nbsp;<span class=style7A>(Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Competencies.)</span></td></tr></table>")

            Response.Write("<table border=1 style=border-spacing:0; border-collapse:collapse; width=100%>")
            Response.Write("<tr><td class=style9>&nbsp;</td>")
            Response.Write("<td class=style9><b>Needs Development/ Improvement</b></td>")
            Response.Write("<td class=style9><b>Proficient</b></td>")
            Response.Write("<td class=style9><b>Excels</b></td></tr>")
            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Oneself</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Make Balanced Decisions</font></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;>Build Trust</td>")
            If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;>Learn Continuously</td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Other</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead with Urgency & Purpose</font></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Promote Collaboration & Accountability</font></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Confront Challenges</font></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading the Organization</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead Change</font></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Inspire Risk Taking & Innovation</font></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Leverage External Perspective</font></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Communicate for Impact</font></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("</table>")
            Response.Write("</td></tr>")

            '--END Addendum --
            Response.Write("<tr><td>")
            '--Divider--
            Response.Write("<table width=100% style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; ><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            '--END Divider--

            '--2. Strengths-- 
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class=style7A>(Comment on key strengths and highlight areas performed well)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Strengths").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--2. END Strengths-- 
            Response.Write("</td></tr>")
            '--3. Development Areas--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;<span class=style7A>(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Area").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray /></td></tr></table>")
            '--3. END Development--  
            Response.Write("</td></tr>")
            '--4. Overall Performance--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class=style7A> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>")
            Response.Write("<tr><td>")
            Response.Write("<table border=1 style=border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width=100%>")
            Response.Write("<tr><td class=style9><b>Unsatisfactory</b></td>")
            Response.Write("<td class=style9><b>Developing/Improving Contributor</b></td>")
            Response.Write("<td class=style9><b>Solid Contributor</b></td>")
            Response.Write("<td class=style9><b>Very Strong Contributor</b></td>")
            Response.Write("<td class=style9><b>Distinguished Contributor</b></td></tr><tr>")
            If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            Response.Write("</tr></table>")
            Response.Write("</td></tr></table>")
            '--4. END Overall Performance--  
            Response.Write("</td></tr>")
            '--5. Overall Summary Waiting approval--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class=style7A>(Comment on overall performance)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr /></td></tr>")
            Response.Write("</table>")
            '--5. END Overall Performance--    
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")
            '--Divider--
            Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            Response.Write("<tr><td>&nbsp;</td></tr>")
            '--END Divider--

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("</table>")

        Next

        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub Guild_Appraisal_18Forwad()
        'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8)) : Response.End()
        '--Guild information---

        SQL1 = " select (case when Process_Flag<5 then MGT_Name else (select B.First+' '+B.Last MGT_Name from Appraisal_Master_Esign_tbl A JOIN id_tbl B ON A.MGT_Emplid=B.emplid "
        SQL1 &= " where A.emplid=D.emplid and Perf_Year=D.Perf_Year) end)Esign_MGT_Name, * from( "

        SQL1 &= "select IsNull(Collab_MGT_EMPLID,'')Collab_MGT_EMPLID,IsNull(Collab_MGT,'')Collab_MGT,BB.* from/*3*/(/*2*/select *,convert(char(10),Hire_Date,101)Hired from(/*1*/select (select first+' '+last from id_tbl "
        SQL1 &= " where emplid=a.emplid)Name, (select last from id_tbl where emplid=a.emplid)LastName,(select first+' '+last from id_tbl where emplid in (select SUPERVISOR_ID from ps_employees where emplid=a.emplid))MGT_Name, "
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B "
        SQL1 &= " where A.emplid = B.emplid And SAP = 14 And A.Perf_Year = B.Perf_Year And A.emplid in (select emplid from ps_employees) and A.Perf_Year=" & Session("YEAR") & "/*1END*/)AA/*2END*/)/*3END*/BB LEFT JOIN "
        SQL1 &= " /*4*/(select * from(select A.EMPLID,Employee,Current_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=A.Current_MGT_EMPLID)Current_MGT_Name,B.MGT_EMPLID Collab_MGT_EMPLID,(select first+' '+last from id_tbl"
        SQL1 &= " where emplid=B.MGT_EMPLID)Collab_MGT from(select emplid,(First_name+' '+Last_name)Employee, SUPERVISOR_EMPLID Current_MGT_EMPLID from HR_PDS_DATA_tbl)A JOIN Appraisal_MasterHistory_tbl B ON A.emplid=B.emplid where "
        SQL1 &= " Perf_Year=" & Session("YEAR") & " and LOGIN_EMPLID=0 )C where Collab_MGT_EMPLID<>Current_MGT_EMPLID )/*4END*/CC ON BB.emplid=CC.emplid"

        SQL1 &= " )D"

        If Len(Mid(Request.QueryString("Token"), 4, 8)) = 4 Then
            SQL1 = SQL1 & " where HR_EMPLID =" & Mid(Request.QueryString("Token"), 4, 8) & ""
        ElseIf Len(Mid(Request.QueryString("Token"), 4, 8)) = 7 Then
            SQL1 = SQL1 & " where DEPTID =" & Mid(Request.QueryString("Token"), 4, 11) & ""
        ElseIf Len(Mid(Request.QueryString("Token"), 11, 15)) = 4 Then
            SQL1 = SQL1 & " where MGT_EMPLID =" & Mid(Request.QueryString("Token"), 11, 15) & ""
        End If
        SQL1 = SQL1 & " order by LastName"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<table border=0 class=StyleBreak width=100%>")
            Response.Write("<tr><td class=auto-style1><img src=../../../images/CR_logo.png /></td></tr>")
            Response.Write("<tr><td class=auto-style1><u>Performance Appraisal (FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & ")</u></td></tr>")
            Response.Write("<tr><td class=auto-style1>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td >")
            Response.Write("<table style=width:100%>")
            Response.Write(" <tr><td style=width:7%; font-family: Calibri;><b>Name:</b></td>")
            Response.Write("<td style=width:30%;>" & DT1.Rows(i)("Name").ToString & "</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td style=width:5%; font-family: Calibri;>E-Signed:&nbsp;&nbsp;</td>")

            If DT1.Rows(i)("Process_Flag").ToString = 5 Then
                Response.Write("<td style=width:20%;>" & DT1.Rows(i)("DateEmpl_Appr").ToString & "</td></tr>")
            ElseIf DT1.Rows(i)("Process_Flag").ToString = 4 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            Else
                Response.Write("<td style=width:20%;></td></tr>")
            End If

            Response.Write("<tr><td><b>Title:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Jobtitle").ToString & "</td>")
            Response.Write("<td style=width:12%; font-family: Calibri;><b>Manager Name:&nbsp;</b></td>")

            Response.Write("<td>" & DT1.Rows(i)("Esign_MGT_Name").ToString & "</td>")

            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 1 Then
                Response.Write("<td>" & DT1.Rows(i)("DateSub_UP_MGT").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If
            Response.Write("<tr><td style=font-family: Calibri;><b>Department:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Department").ToString & "</td>")

            If Len(DT1.Rows(i)("Collab_MGT").ToString) = 0 Then
                Response.Write("<td style=font-family: Calibri;><b>&nbsp;</b></td>")
            Else
                Response.Write("<td style=font-family: Calibri;><b>Former Manager:&nbsp;</b></td>")
            End If
            Response.Write("<td>" & DT1.Rows(i)("Collab_MGT").ToString & "</td>")

            'Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")
            Response.Write("<td></td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 1 Then
                'Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                Response.Write("<td></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 2 Then
                'Response.Write("<td>" & DT1.Rows(i)("DateSub_HR").ToString & "</td></tr>")
                Response.Write("<td></td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("<tr><td style=font-family: Calibri;><b>Hire Date:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Hired").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;><b>Human Resources Generalist:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("HR_NAME").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 2 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 3 Then
                Response.Write("<td>" & DT1.Rows(i)("DateHR_Appr").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")
            '--1. Accomplishments--
            Response.Write("<table border=0 width=100%><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class=style7A>(Summarize accomplishments vs the goals/expectations established)</span></td></tr></table>")
            Response.Write("</td></tr><tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--1END Accomplishments--
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")

            '--Addendum --  
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Competencies:</u>&nbsp;<span class=style7A>(Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Competencies.)</span></td></tr></table>")

            Response.Write("<table border=1 style=border-spacing:0; border-collapse:collapse; width=100%>")
            Response.Write("<tr><td class=style9>&nbsp;</td>")
            Response.Write("<td class=style9><b>Needs Development/ Improvement</b></td>")
            Response.Write("<td class=style9><b>Proficient</b></td>")
            Response.Write("<td class=style9><b>Excels</b></td></tr>")
            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Oneself</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Make Balanced Decisions</font></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;>Build Trust</td>")
            If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;>Learn Continuously</td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Other</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead with Urgency & Purpose</font></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Promote Collaboration & Accountability</font></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Confront Challenges</font></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading the Organization</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead Change</font></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Inspire Risk Taking & Innovation</font></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Leverage External Perspective</font></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Communicate for Impact</font></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("</table>")
            Response.Write("</td></tr>")

            '--END Addendum --
            Response.Write("<tr><td>")
            '--Divider--
            Response.Write("<table width=100% style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; ><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            '--END Divider--

            '--2. Strengths-- 
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class=style7A>(Comment on key strengths and highlight areas performed well)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Strengths").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--2. END Strengths-- 
            Response.Write("</td></tr>")
            '--3. Development Areas--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;<span class=style7A>(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Area").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray /></td></tr></table>")
            '--3. END Development--  
            Response.Write("</td></tr>")
            '--4. Overall Performance--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class=style7A> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>")
            Response.Write("<tr><td>")
            Response.Write("<table border=1 style=border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width=100%>")
            Response.Write("<tr><td class=style9><b>Unsatisfactory</b></td>")
            Response.Write("<td class=style9><b>Developing/Improving Contributor</b></td>")
            Response.Write("<td class=style9><b>Solid Contributor</b></td>")
            Response.Write("<td class=style9><b>Very Strong Contributor</b></td>")
            Response.Write("<td class=style9><b>Distinguished Contributor</b></td></tr><tr>")
            If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            Response.Write("</tr></table>")
            Response.Write("</td></tr></table>")
            '--4. END Overall Performance--  
            Response.Write("</td></tr>")
            '--5. Overall Summary Waiting approval--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class=style7A>(Comment on overall performance)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr /></td></tr>")
            Response.Write("</table>")
            '--5. END Overall Performance--    
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")
            '--Divider--
            Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            '--END Divider--
            If CDbl(Len(DT1.Rows(i)("DateEmpl_Refused").ToString)) > 5 Then
                Response.Write("<tr><td><center><font color=red><b>" & DT1.Rows(i)("Comments").ToString & " </b></font></center></td></tr>")

            ElseIf CDbl(Len(DT1.Rows(i)("DateEmpl_Refused").ToString)) < 5 And Len(DT1.Rows(i)("Comments").ToString) > 3 Then
                Response.Write("<tr><td><b>Employee's Comments:</b><br />" & Replace(Replace(DT1.Rows(i)("Comments").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            End If

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("</table>")

        Next

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub Manager_Appraisal_18Forwad()
        'Response.Write(Len(Mid(Request.QueryString("Token"), 4, 8)) & "<br>" & Mid(Request.QueryString("Token"), 4, 8)) : Response.End()

        If Session("EMPLID") = 6785 Or Session("EMPLID") = 6250 Or Session("EMPLID") = 6671 Then
            '--Manager information---

            SQL1 = " select (case when Process_Flag<5 then MGT_Name else (select B.First+' '+B.Last MGT_Name from Appraisal_Master_Esign_tbl A JOIN id_tbl B ON A.MGT_Emplid=B.emplid "
            SQL1 &= " where A.emplid=D.emplid and Perf_Year=D.Perf_Year) end)Esign_MGT_Name, * from( "

            SQL1 &= " select IsNull(Collab_MGT_EMPLID,'')Collab_MGT_EMPLID,IsNull(Collab_MGT,'')Collab_MGT,BB.* from(select *,convert(char(10),Hire_Date,101)Hired from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,"
            SQL1 &= " (select last from id_tbl where emplid=a.emplid)LastName,(select first+' '+last from id_tbl where emplid in (select SUPERVISOR_ID from ps_employees where emplid=a.emplid))MGT_Name,"
            SQL1 &= " (select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment "
            SQL1 &= " from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B  where A.emplid=B.emplid and SAP<>14 and A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & " and A.emplid in (select emplid from ps_employees)) "
            SQL1 &= " AA)BB LEFT JOIN (select * from(select A.EMPLID,Employee,Current_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=A.Current_MGT_EMPLID)Current_MGT_Name,"
            SQL1 &= " B.MGT_EMPLID Collab_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=B.MGT_EMPLID)Collab_MGT from(select emplid,(First_name+' '+Last_name)Employee,"
            SQL1 &= " SUPERVISOR_EMPLID Current_MGT_EMPLID from HR_PDS_DATA_tbl)A JOIN Appraisal_MasterHistory_tbl B ON  A.emplid=B.emplid "
            SQL1 &= " where Perf_Year=" & Session("YEAR") & " and LOGIN_EMPLID=0 )C where Collab_MGT_EMPLID<>Current_MGT_EMPLID)CC ON BB.emplid=CC.emplid"

            SQL1 &= " )D"

            If Len(Mid(Request.QueryString("Token"), 4, 8)) = 4 Then
                SQL1 = SQL1 & " where HR_EMPLID =" & Mid(Request.QueryString("Token"), 4, 8) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 4, 8)) = 7 Then
                SQL1 = SQL1 & " where DEPTID =" & Mid(Request.QueryString("Token"), 4, 11) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 11, 15)) = 4 Then
                SQL1 = SQL1 & " where MGT_EMPLID =" & Mid(Request.QueryString("Token"), 11, 15) & ""
            End If
            SQL1 = SQL1 & " order by LastName"
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        Else
            '--Manager information---
            SQL1 = " select (case when Process_Flag<5 then MGT_Name else (select B.First+' '+B.Last MGT_Name from Appraisal_Master_Esign_tbl A JOIN id_tbl B ON A.MGT_Emplid=B.emplid "
            SQL1 &= " where A.emplid=D.emplid and Perf_Year=D.Perf_Year) end)Esign_MGT_Name, * from( "

            SQL1 &= " select IsNull(Collab_MGT_EMPLID,'')Collab_MGT_EMPLID,IsNull(Collab_MGT,'')Collab_MGT,BB.* from(select *,convert(char(10),Hire_Date,101)Hired from(select (select first+' '+last from id_tbl where emplid=a.emplid)Name,"
            SQL1 &= " (select last from id_tbl where emplid=a.emplid)LastName,(select first+' '+last from id_tbl where emplid in (select SUPERVISOR_ID from ps_employees where emplid=a.emplid))MGT_Name,"
            SQL1 &= " (select first+' '+last from id_tbl where emplid=a.up_mgt_emplid)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.hr_emplid)HR_Name,A.*,Accomplishment "
            SQL1 &= " from Appraisal_Master_tbl A,Appraisal_Accomplishments_tbl B  where A.emplid=B.emplid and SAP<>14 and A.Perf_Year=B.Perf_Year and A.Perf_Year=" & Session("YEAR") & " and deptid<>9009120 and A.emplid in  "
            SQL1 &= " (select emplid from ps_employees)) AA)BB LEFT JOIN (select * from(select A.EMPLID,Employee,Current_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=A.Current_MGT_EMPLID)Current_MGT_Name,"
            SQL1 &= " B.MGT_EMPLID Collab_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=B.MGT_EMPLID)Collab_MGT from(select emplid,(First_name+' '+Last_name)Employee,"
            SQL1 &= " SUPERVISOR_EMPLID Current_MGT_EMPLID from HR_PDS_DATA_tbl)A JOIN Appraisal_MasterHistory_tbl B ON  A.emplid=B.emplid "
            SQL1 &= " where Perf_Year=" & Session("YEAR") & " and LOGIN_EMPLID=0 )C where Collab_MGT_EMPLID<>Current_MGT_EMPLID)CC ON BB.emplid=CC.emplid"

            SQL1 &= " )D"

            If Len(Mid(Request.QueryString("Token"), 4, 8)) = 4 Then
                SQL1 = SQL1 & " where HR_EMPLID =" & Mid(Request.QueryString("Token"), 4, 8) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 4, 8)) = 7 Then
                SQL1 = SQL1 & " where DEPTID =" & Mid(Request.QueryString("Token"), 4, 11) & ""
            ElseIf Len(Mid(Request.QueryString("Token"), 11, 15)) = 4 Then
                SQL1 = SQL1 & " where MGT_EMPLID =" & Mid(Request.QueryString("Token"), 11, 15) & ""
            End If
            SQL1 = SQL1 & " order by LastName"
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        End If

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<table border=0 class=StyleBreak width=100%>")
            Response.Write("<tr><td class=auto-style1><img src=../../../images/CR_logo.png /></td></tr>")
            Response.Write("<tr><td class=auto-style1><u>Performance Appraisal (FY" & Right(Trim(DT1.Rows(i)("Perf_Year").ToString), 2) & ")</u></td></tr>")
            Response.Write("<tr><td class=auto-style1>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td >")
            Response.Write("<table style=width:100%>")
            Response.Write(" <tr><td style=width:7%; font-family: Calibri;><b>Name:</b></td>")
            Response.Write("<td style=width:30%;>" & DT1.Rows(i)("Name").ToString & "</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td>&nbsp;&nbsp;</td>")
            Response.Write("<td style=width:5%; font-family: Calibri;>E-Signed:&nbsp;&nbsp;</td>")

            If DT1.Rows(i)("Process_Flag").ToString = 5 Then
                Response.Write("<td style=width:20%;>" & DT1.Rows(i)("DateEmpl_Appr").ToString & "</td></tr>")
            ElseIf DT1.Rows(i)("Process_Flag").ToString = 4 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            Else
                Response.Write("<td style=width:20%;></td></tr>")
            End If

            Response.Write("<tr><td><b>Title:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Jobtitle").ToString & "</td>")
            Response.Write("<td style=width:12%; font-family: Calibri;><b>Manager Name:&nbsp;</b></td>")

            Response.Write("<td>" & DT1.Rows(i)("Esign_MGT_Name").ToString & "</td>")

            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 1 Then
                Response.Write("<td>" & DT1.Rows(i)("DateSub_UP_MGT").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If
            Response.Write("<tr><td style=font-family: Calibri;><b>Department:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Department").ToString & "</td>")

            If Len(DT1.Rows(i)("Collab_MGT").ToString) = 0 Then
                Response.Write("<td style=font-family: Calibri;><b>&nbsp;</b></td>")
            Else
                Response.Write("<td style=font-family: Calibri;><b>Former Manager:&nbsp;</b></td>")
            End If
            Response.Write("<td>" & DT1.Rows(i)("Collab_MGT").ToString & "</td>")

            'Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")
            Response.Write("<td></td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 1 Then
                'Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
                Response.Write("<td></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 2 Then
                'Response.Write("<td>" & DT1.Rows(i)("DateSub_HR").ToString & "</td></tr>")
                Response.Write("<td></td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("<tr><td style=font-family: Calibri;><b>Hire Date:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("Hired").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;><b>Human Resources Generalist:</b></td>")
            Response.Write("<td>" & DT1.Rows(i)("HR_NAME").ToString & "</td>")
            Response.Write("<td style=font-family: Calibri;>Approved:&nbsp;&nbsp;</td>")

            If CDbl(DT1.Rows(i)("Process_Flag").ToString) = 2 Then
                Response.Write("<td><font color=blue><b>Waiting Approval</b></font></td></tr>")
            ElseIf CDbl(DT1.Rows(i)("Process_Flag").ToString) >= 3 Then
                Response.Write("<td>" & DT1.Rows(i)("DateHR_Appr").ToString & "</td></tr>")
            Else
                Response.Write("<td></td></tr>")
            End If

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")
            '--1. Accomplishments--
            Response.Write("<table border=0 width=100%><tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class=style7A>(Summarize accomplishments vs the goals/expectations established)</span></td></tr></table>")
            Response.Write("</td></tr><tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Accomplishment").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--1END Accomplishments--
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")

            '--Addendum --  
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Competencies:</u>&nbsp;<span class=style7A>(Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Competencies.)</span></td></tr></table>")

            Response.Write("<table border=1 style=border-spacing:0; border-collapse:collapse; width=100%>")
            Response.Write("<tr><td class=style9>&nbsp;</td>")
            Response.Write("<td class=style9><b>Needs Development/ Improvement</b></td>")
            Response.Write("<td class=style9><b>Proficient</b></td>")
            Response.Write("<td class=style9><b>Excels</b></td></tr>")
            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Oneself</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Make Balanced Decisions</font></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Make_Balance").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;>Build Trust</td>")
            If DT1.Rows(i)("Build_Trust").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Build_Trust").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;>Learn Continuously</td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Learn_Continuously").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading Other</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead with Urgency & Purpose</font></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Urgency").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Promote Collaboration & Accountability</font></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Promote_Collab").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Confront Challenges</font></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Confront_Challenge").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td height=30px bgcolor=silver colspan=4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span><font size=large font-family=Calibri color=white><b>Leading the Organization</b></span></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Lead Change</font></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Lead_Change").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Inspire Risk Taking & Innovation</font></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Inspire_Risk").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Leverage External Perspective</font></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Leverage_External").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("<tr><td style=height:33px; font-size:large;><font>Communicate for Impact</font></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 1 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 2 Then Response.Write("<td align=center><input type=radio checked disabled></td>") Else Response.Write("<td align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Communic_Impact").ToString = 3 Then Response.Write("<td align=center><input type=radio checked disabled></td></tr>") Else Response.Write("<td align=center><input type=radio disabled></td></tr>")
            Response.Write("</table>")
            Response.Write("</td></tr>")

            '--END Addendum --
            Response.Write("<tr><td>")
            '--Divider--
            Response.Write("<table width=100% style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; ><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            '--END Divider--

            '--2. Strengths-- 
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class=style7A>(Comment on key strengths and highlight areas performed well)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Strengths").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray></td></tr></table>")
            '--2. END Strengths-- 
            Response.Write("</td></tr>")
            '--3. Development Areas--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;<span class=style7A>(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Development_Area").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr color=gray /></td></tr></table>")
            '--3. END Development--  
            Response.Write("</td></tr>")
            '--4. Overall Performance--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class=style7A> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>")
            Response.Write("<tr><td>")
            Response.Write("<table border=1 style=border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width=100%>")
            Response.Write("<tr><td class=style9><b>Unsatisfactory</b></td>")
            Response.Write("<td class=style9><b>Developing/Improving Contributor</b></td>")
            Response.Write("<td class=style9><b>Solid Contributor</b></td>")
            Response.Write("<td class=style9><b>Very Strong Contributor</b></td>")
            Response.Write("<td class=style9><b>Distinguished Contributor</b></td></tr><tr>")
            If DT1.Rows(i)("Overall_Rating").ToString = 1 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 2 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 3 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 4 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            If DT1.Rows(i)("Overall_Rating").ToString = 5 Then Response.Write("<td width=20%; align=center><input type=radio checked disabled></td>") Else Response.Write("<td width=20%; align=center><input type=radio disabled></td>")
            Response.Write("</tr></table>")
            Response.Write("</td></tr></table>")
            '--4. END Overall Performance--  
            Response.Write("</td></tr>")
            '--5. Overall Summary Waiting approval--
            Response.Write("<tr><td>")
            Response.Write("<table border=0 width=100%>")
            Response.Write("<tr><td class=style8>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class=style7A>(Comment on overall performance)</span></td></tr>")
            Response.Write("<tr><td style=vertical-align:top; font-family:Calibri;>" & Replace(Replace(DT1.Rows(i)("Overall_Summary").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            Response.Write("<tr><td><hr /></td></tr>")
            Response.Write("</table>")
            '--5. END Overall Performance--    
            Response.Write("</td></tr>")
            Response.Write("<tr><td>")
            '--Divider--
            Response.Write("<table style=border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width=100%><tr><td></td></tr></table>")
            Response.Write("</td></tr>")
            '--END Divider--
            If CDbl(Len(DT1.Rows(i)("DateEmpl_Refused").ToString)) > 5 Then
                Response.Write("<tr><td><center><font color=red><b>" & DT1.Rows(i)("Comments").ToString & " </b></font></center></td></tr>")
            ElseIf CDbl(Len(DT1.Rows(i)("DateEmpl_Refused").ToString)) < 5 And Len(DT1.Rows(i)("Comments").ToString) > 3 Then
                Response.Write("<tr><td><b>Employee's Comments:</b><br />" & Replace(Replace(DT1.Rows(i)("Comments").ToString, Chr(13), "<span>"), Chr(10), "<br>") & "</td></tr>")
            End If

            Response.Write("</table>")
            Response.Write("</td></tr>")
            Response.Write("</table>")
        Next


        LocalClass.CloseSQLServerConnection()

    End Sub


    Public Function getRating(ByVal Rating As Integer) As String
        If Rating = 1 Then Return "Exceptional"
        If Rating = 2 Then Return "High"
        If Rating = 3 Then Return "Meets"
        If Rating = 4 Then Return "Needs Improvement"
        If Rating = 5 Then Return "Unsatisfactory"

        Return "Not Rate"

    End Function

    Public Function getName(ByVal emplid As String)
        SQL = "select IsNull(First+' '+Last,'N,/A')Name from id_tbl where emplid= " & emplid
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        Return DT.Rows(0)("Name").ToString

    End Function

End Class