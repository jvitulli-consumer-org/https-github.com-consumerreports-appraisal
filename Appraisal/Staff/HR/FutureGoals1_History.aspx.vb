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
Public Class FutureGoals1_History
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8, SQL9, SQL10, SQL11, SQL14, SQL1_1, SQL2_1, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6, DT7, DT8, DT9, DT10, DT11, DT14, DT1_1 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim Msg, Msg1, Subj, x, x1 As String
    Dim EMPLID As String

    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer

    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.AddHeader("Refresh", "840")

        lblEMPLID.Text = Request.QueryString("Token")
        lblLogin_EMPLID.Text = Trim(Session("MGT_EMPLID"))

        lblYear.Text = Trim(Session("Year"))

        'lblWindowBatch.Text = Mid(Request.QueryString("Token"), 9)
        'lblDataBaseBatch.Text = Session("Window_batch")

        SetLevel_Approval()
        Goals_Log()

    End Sub

    Protected Sub SetLevel_Approval()

        SQL1 = "select * from(select Goals,Milestones,TargetDate from Appraisal_FutureGoals_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & " and IndexID=1)A,"
        SQL1 &= " (select (select count(*) from Appraisal_FutureGoals_Master_tbl A where mgt_emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & ")Manager,*,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.emplid)Empl_Name,(select first+' '+last from id_tbl where emplid=a.MGT_EMPLID)MGT_Name,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.HR_EMPLID)HR_Name,"
        SQL1 &= " (select convert(char(10),hire_Date,101) from id_tbl where emplid=a.emplid)Empl_Hired,(select email from id_tbl where emplid=a.emplid)Empl_Email,"
        SQL1 &= " (select email from id_tbl where emplid=a.MGT_EMPLID)MGT_Email,(select email from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Email,"
        SQL1 &= " (select email from id_tbl where emplid=a.HR_EMPLID)HR_Email "
        SQL1 &= " from Appraisal_FutureGoals_Master_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & ")B,"
        If Len(lblLogin_EMPLID.Text) = 0 Then
            SQL1 &= " (select 0 Login_Mgt_Emp)C"
        Else
            SQL1 &= " (select " & Trim(lblEMPLID.Text) & " Login_Mgt_Emp)C"
        End If
        'Response.Write(SQL1) : Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        lblEmpl_NAME.Text = DT1.Rows(0)("Empl_Name").ToString
        lblEmpl_TITLE.Text = DT1.Rows(0)("jobtitle").ToString
        lblEmpl_DEPT.Text = DT1.Rows(0)("Department").ToString
        lblEmpl_HIRE.Text = DT1.Rows(0)("Empl_Hired").ToString
        lblEmpl_EMAIL.Text = DT1.Rows(0)("empl_email").ToString
        lblFlag.Text = DT1.Rows(0)("Process_Flag").ToString
        '--First Supervisor--
        lblFirst_SUP_EMPLID.Text = DT1.Rows(0)("MGT_emplid").ToString
        LblMGT_NAME.Text = DT1.Rows(0)("MGT_Name").ToString
        lblFirst_SUP_EMAIL.Text = DT1.Rows(0)("MGT_Email").ToString
        '--Second Supervisor--
        lblUP_MGT_EMPLID.Text = DT1.Rows(0)("UP_MGT_emplid").ToString
        lblUP_MGT_NAME.Text = DT1.Rows(0)("UP_MGT_Name").ToString
        lblUP_MGT_EMAIL.Text = DT1.Rows(0)("UP_MGT_email").ToString

        '--HR Generalist--

        lblHR_EMPLID.Text = DT1.Rows(0)("HR_EMPLID").ToString
        lblHR_EMAIL.Text = DT1.Rows(0)("HR_email").ToString
        '--Goal Years--
        Goal_Year.Text = Trim(Right(lblYear.Text, 2))
        Goal_Year1.Text = Trim(Right(lblYear.Text - 1, 2))
        Goal_Year2.Text = Trim(Right(lblYear.Text, 2))
        LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString

        LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_Empl").ToString


        Session("Goal_Year") = Trim(Right(lblYear.Text, 2))
        Session("Goal_Year1") = Trim(Right(lblYear.Text - 1, 2))
        Session("Goal_Year2") = Trim(Right(lblYear.Text, 2))
        Session("EMP_Appr") = DT1.Rows(0)("DateEmpl_Appr").ToString

        If CDbl(lblFlag.Text) = 5 Then
            Session("MGT_Appr") = DT1.Rows(0)("DateSUB_Empl").ToString
        Else
            Session("MGT_Appr") = ""
        End If


    End Sub

    Sub Goals_Log()

        Dim j As Integer

        Response.Write("<table class=Style0 border=0>")
        Response.Write("<tr><td class=style6>&nbsp;&nbsp;</td>")
        Response.Write("<td class=Style1><img alt=../../images/CR_logo.png src=../../images/CR_logo.png style=width: 380px; height:60px /></td>")
        Response.Write("<td class=style6>&nbsp;&nbsp;</td></tr>")
        Response.Write("<tr><td class=style6>&nbsp;&nbsp;</td>")
        Response.Write("<td class=Style1><u>FY" & Session("Goal_Year") & " ")
        Response.Write("Goal-Setting Form (06/01/" & Session("Goal_Year1") & " - 05/31/" & Session("Goal_Year2") & ")</u></td>")
        Response.Write("<td class=style6>&nbsp;&nbsp;</td></tr>")
        Response.Write("<tr><td class=style6>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td class=style6>&nbsp;&nbsp;</td></tr>")
        Response.Write("<tr><td class=style6>&nbsp;&nbsp;</td>")
        Response.Write("<td><table style=width:100% border=0 class=TextBox_StyleSheet>")
        Response.Write("<tr><td class=auto-style2><b>Name:</b></td>")
        Response.Write("<td class=style2>" & lblEmpl_NAME.Text & "</td>")
        Response.Write("<td class=style4>&nbsp;&nbsp;</td>")
        Response.Write("<td class=style2>&nbsp;&nbsp;</td>")

        If lblYear.Text < 2020 Then
            Response.Write("<td class=style5>E-Signed:&nbsp;&nbsp;</td>")
        Else
            Response.Write("<td class=style5>Status:&nbsp;&nbsp;</td>")
        End If

        If lblYear.Text < 2020 Then
            Response.Write("<td>" & Session("EMP_Appr") & "</td></tr>")
        Else
            If CDbl(lblFlag.Text) = 0 Then
                Response.Write("<td><font color=blue>Create Goals</font></td></tr>")
                'LblMGT_Appr.Text = ""
            ElseIf CDbl(lblFlag.Text) = 1 Then
                Response.Write("<td><font color=blue>Edit Goals</font></td></tr>")
                'LblEMP_Appr.Text = "<font color=blue>Edit Goals</font>"
                'LblMGT_Appr.Text = ""
            ElseIf CDbl(lblFlag.Text) = 2 Then
                Response.Write("<td><font color=blue>Sent to Manager</font></td></tr>")
                'LblEMP_Appr.Text = "<font color=blue>Sent to Manager</font>"
                'LblMGT_Appr.Text = ""
            ElseIf CDbl(lblFlag.Text) = 3 Then
                Response.Write("<td><font color=blue>Revised Goals</font></td></tr>")
                'LblEMP_Appr.Text = "<font color=blue>Revised Goals</font>"
            ElseIf CDbl(lblFlag.Text) = 5 Then
                Response.Write("<td><font color=blue>Approved</font></td></tr>")
                'LblEMP_Appr.Text = "<font color=blue>Approved</font>"
                'LblMGT_Appr.Text = DT.Rows(0)("DateEmpl_Appr").ToString
            End If

        End If



        Response.Write("<tr><td class=auto-style2><b>Title:</b></td>")
        Response.Write("<td>" & lblEmpl_TITLE.Text & "</td>")
        Response.Write("<td><b>Manager Name:&nbsp;</b></td>")
        Response.Write("<td>" & LblMGT_NAME.Text & "</td>")
        Response.Write("<td class=style5>Approved:&nbsp;&nbsp;</td>")
        Response.Write("<td>" & Session("MGT_Appr") & "</td></tr>")
        Response.Write("<tr><td class=auto-style2><b>Department:</b></td>")
        Response.Write("<td>" & lblEmpl_DEPT.Text & "</td>")
        Response.Write("<td><!--<b>2nd Level Manager Name:&nbsp;</b>--></td>")
        Response.Write("<td>&nbsp;&nbsp;</td>")
        Response.Write("<td class=style5>&nbsp;&nbsp;</td>")
        Response.Write("<td>&nbsp;</td></tr>")
        Response.Write("<tr><td class=auto-style2><b>Hire Date:</b></td>")
        Response.Write("<td>" & lblEmpl_HIRE.Text & "</td>")
        Response.Write("<td><!--<b>Human Resources Generalist:</b>-->&nbsp;&nbsp;</td>")
        Response.Write("<td><asp:label id=lblHR_NAME runat=server Font-Names=Calibri Visible=false/></td>")
        Response.Write("<td class=style5>&nbsp;</td>")
        Response.Write("<td>&nbsp;</td></tr></table></td><td>&nbsp;</td></tr>")
        Response.Write("<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>")
        Response.Write("<tr><td>&nbsp;</td><td style=text-align:center;font-size:x-large>Prior Goal History</td><td>&nbsp;</td></tr>")

        Response.Write("<tr><td>&nbsp;</td><td>")

        SQL = "select distinct DateEmpl_Appr DEAppr,(select last+' '+first from id_tbl where emplid=Recall_EMPLID)Created FROM Appraisal_FutureGoal_Recall_tbl "
        SQL &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & "  and Len(DateEmpl_Appr)>5 and Len(Recall_Date)>5 order by DateEmpl_Appr desc"
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        For i = 0 To DT.Rows.Count - 1


            SQL1 = "select distinct IndexID,DateEmpl_Appr,(select last+' '+first from id_tbl where emplid=Recall_EMPLID)Created,Recall_Emplid,emplid,"
            SQL1 &= "Replace(Replace(Goals,Char(13),'<span>'), Char(10),'<br>')Goals,Replace(Replace(Milestones,Char(13),'<span>'),Char(10),'<br>')Milestones,"
            SQL1 &= "Replace(Replace(TargetDate,Char(13), '<span>'), Char(10), '<br>')TargetDate,"
            SQL1 &= "(select Max(DateEmpl_Appr)Recall_Date from Appraisal_FutureGoal_Recall_tbl where Rtrim(Ltrim(Goals))=Rtrim(Ltrim(A.Goals)) and "
            SQL1 &= "Rtrim(Ltrim(Milestones))=Rtrim(Ltrim(A.Milestones)) and  Rtrim(Ltrim(TargetDate))=Rtrim(Ltrim(A.TargetDate)))Recall_Date "
            SQL1 &= "from Appraisal_FutureGoal_Recall_tbl A where DateEmpl_Appr='" & DT.Rows(i)("DEAppr").ToString & "' and emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " "
            SQL1 &= "and Len(DateEmpl_Appr)>5 and Len(Recall_Date)>5 order by DateEmpl_Appr desc,IndexID"
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            Response.Write("<br/><br/>")
            Response.Write("<table border=0 style=border-spacing:0; border-collapse:collapse; width=100%>")

            Response.Write("<tr><td colspan=4>")

            Response.Write("<table border=0><tr>")
            Response.Write("<td style=width:50%><b>Reviewed and E-signed Date</b>&nbsp;&nbsp;" & DT.Rows(i)("DEAppr").ToString & "</td>")
            Response.Write("<td style=text-align:right; width:50%><b>Created by&nbsp;&nbsp;</b>" & DT.Rows(i)("Created").ToString & "&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>")
            Response.Write("</table>")

            Response.Write("</td></tr>")

            Response.Write("<tr style=background-color:LightGray>")
            Response.Write("<td style=width:3%;></td>")

            Response.Write("<td style=text-align:center;font-size:large;font-weight:bold;width:42%;font-family:Calibri;background-color:lightgray;>Goal")
            Response.Write("<div style=font-size:small; font-weight:lighter;font-style:italic;word-wrap:normal;word-break:normal;>")
            Response.Write("What do I need to accomplish in order to support my department’s goals and CR’s goals?</div><br /></td>")

            Response.Write("<td style=text-align:center;font-size:large;font-weight:bold;width:40%;font-family:Calibri;background-color:lightgray;>Result")
            Response.Write("<div style=font-size:small; font-weight:lighter;font-style:italic;word-wrap:normal;word-break:normal;>")
            Response.Write("How will I know that I’ve accomplished each goal?</td>")

            Response.Write("<td style=text-align:center;font-size:large;font-weight:bold;width:15%;font-family:Calibri;background-color:lightgray;>Target Completion Date")
            Response.Write("<div style=font-size:small; font-weight:lighter;font-style:italic;word-wrap:normal;word-break:normal;>")
            Response.Write("When will I <br />accomplish this goal?</td>")

            Response.Write("</tr></table>")

            Response.Write("<table border=1 style=border-spacing:0; border-collapse:collapse; border-color:gray; width=100%>")

            For j = 0 To DT1.Rows.Count - 1

                Response.Write("<tr><td style=width:3%;vertical-align:top>" & DT1.Rows(j)("IndexID").ToString & ")</td>")
                Response.Write("<td style=width:42%;vertical-align:top>" & DT1.Rows(j)("Goals").ToString & "</td>")
                Response.Write("<td style=width:40%;vertical-align:top>" & DT1.Rows(j)("Milestones").ToString & "</td>")
                Response.Write("<td style=width:15%;vertical-align:top>" & DT1.Rows(j)("TargetDate").ToString & "</td></tr>")
            Next
            Response.Write("</table>")

        Next

        Response.Write("<td>&nbsp;</td></tr></table>")

    End Sub

End Class