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
Public Class Manager_Waiting_Approval
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
    Dim Msg, Msg1, Msq2, Msg3, Subj, x, x1 As String
    Dim EMPLID As String


    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer
    Dim KeyTaskRows1, KeyTaskRows2, KeyTaskRows3, KeyTaskRows4, KeyTaskRows5, KeyTaskRows6, KeyTaskRows7, KeyTaskRows8, KeyTaskRows9, KeyTaskRows10, KeyTaskRows11, KeyTaskRows12, KeyTaskRows13, KeyTaskRows14 As Integer

    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Write(Session("YEAR") & "<br>" & Len(Session("MGT_EMPLID")) & "<br>" & Request.QueryString("Token")) : Response.End()

        If (Len(Session("YEAR")) + Len(Session("MGT_EMPLID"))) = 0 Then Response.Redirect("../../Default.aspx")

        lblEMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")
        LblLogin.Text = Session("MGT_EMPLID")
        lblYEAR.Text = Session("YEAR")

        '--Suspended all appraisals and redirect to print/view page
        SQL1 = "select /*Max(CNT_Susp)CNT_Susp,*/Max(Suspend)Suspend from(select CNT_Susp,Suspend from("
        SQL1 &= "select Top 1 ''CNT_Susp,* from ME_Appraisal_HR_SUSPEND_tbl order by Time_Stampt desc)A"
        SQL1 &= " UNION select count(*)CNT_Susp,'' Suspend from ME_Appraisal_HR_SUSPEND_tbl)AA"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        If CDbl(DT1.Rows(0)("Suspend").ToString) = 1 Then
            x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblEMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
            Response.Redirect("Appraisal_Review.aspx?Token=" & x)
        End If
        LocalClass.CloseSQLServerConnection()

        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display")
            SaveFutureData()
        Else
            'Response.Write("<br>2. else Save Data before Display")
            'DisplayFutureData()
        End If


        SetLevel_Approval()
        DisplayData()
        DisplayFutureData()

        ShowHideGoalPanel()
        ShowHideFutureGoalPanel()

        If LblLogin.Text = LblFirst_SUP_EMPLID.Text And Session("Process_Flag") > 4 Then
            'SaveFutureGoal.Visible = True
            Panel_FutureGoal_Dispaly.Visible = True

            Panel_FutureGoal_EditALL.Visible = False
        Else
            Panel_FutureGoal_EditALL.Visible = False
            'SaveFutureGoal.Visible = False
            Panel_FutureGoal_Dispaly.Visible = True
        End If

    End Sub
    Protected Sub SetLevel_Approval()
        '--Employee information---
        SQL1 = "/*3*/select /*BB.EMPLID,*/(select deptid from ps_employees where emplid=HR_Generalist)HR_DEPID,(case when New_Employee=1 then 'SHORT' else 'FULL' end)Elig_Review,EMPLOYEE_NAME,/*SAP,*/EMPLOYEE_EMAIL,JOBTITLE,DEPTID,"
        SQL1 &= " DEPTNAME,Hired,YearsCU,Perf_Year CY,Perf_Year-1 PY,Perf_Year+1 NY,BEN_ID,EMPL_Supervision,FIRST_SUP_EMPLID,FIRST_SUP_NAME,FIRST_SUP_EMAIL,UP_MGT_ID,UP_MGT_NAME,UP_MGT_EMAIL,HR_Generalist,"
        SQL1 &= " HR_Generalist_Name,HR_Generalist_Email,CC.* from(/*2*/select EMPLID,EMPLOYEE_NAME,SAP,EMPLOYEE_EMAIL,JOBTITLE,DEPTID,DEPTNAME,Hired,EMPL_Supervision,YearsCU,BEN_ID,/*1st Supervisor*/"
        SQL1 &= " (case when EMPLID=6193 then 6193 else FIRST_SUP_EMPLID end)FIRST_SUP_EMPLID,(case when EMPLID=6193 then 'Marta Tellado' else FIRST_SUP_NAME end)FIRST_SUP_NAME,"
        SQL1 &= " (case when EMPLID=6193 then 'Marta.Tellado@consumer.org' else FIRST_SUP_EMAIL end)FIRST_SUP_EMAIL,/*2nd Supervisor*/(case when EMPLID=6193 or FIRST_SUP_EMPLID=6193 then 6193 when FIRST_SUP_EMPLID not in(6193) and UP_MGT_ID=6193 "
        SQL1 &= " then FIRST_SUP_EMPLID else UP_MGT_ID end)UP_MGT_ID,(case when FIRST_SUP_EMPLID=6193 or EMPLID=6193 then 'Marta Tellado' when FIRST_SUP_EMPLID not in(6193) and UP_MGT_ID=6193 then FIRST_SUP_NAME else UP_MGT_NAME end)UP_MGT_NAME,"
        SQL1 &= " (case when FIRST_SUP_EMPLID=6193 or EMPLID=6193 then 'Marta.Tellado@consumer.org' when FIRST_SUP_EMPLID not in(6193) and UP_MGT_ID=6193 then FIRST_SUP_EMAIL else UP_MGT_EMAIL end) UP_MGT_Email,/*Generalist*/(case when "
        SQL1 &= " FIRST_SUP_EMPLID=6193 or EMPLID=6193 then 6785 else HR_Generalist end)HR_Generalist,(case when FIRST_SUP_EMPLID=6193 or EMPLID=6193 then 'Lisa Cribari' else HR_Generalist_Name end) HR_Generalist_Name,(case when "
        SQL1 &= " FIRST_SUP_EMPLID=6193 or EMPLID=6193 then 'Rafael.Perez@consumer.org' else HR_Generalist_Email end) HR_Generalist_Email from(/*1*/select /*Employee*/EMPLID,(FIRST_NAME+' '+LAST_NAME)EMPLOYEE_NAME,SAL_ADMIN_PLAN SAP,"
        SQL1 &= " EMAIL EMPLOYEE_EMAIL,JOBTITLE,DEPTID,DEPTNAME,Hire_date Hired,"
        '--Ticket #12847 Restrict managers from filling out the Competency Grid for these employees.  
        SQL1 &= " (case when A.emplid in (5529,1241,6129,6235,1189,5315,6057,6203,1683,6094,1167) then 1 else "
        SQL1 &= " (select count(*) from ps_employees where supervisor_id=A.emplid)  end) EMPL_Supervision"
        '---END Ticket #12847---------------------------------------------------------------------------------
        SQL1 &= " ,Left(convert(decimal,datediff(day,Hire_date,GetDate()))/365.25,5)YearsCU,BEN_ID,"
        SQL1 &= " /*1st Supervisor*/SUPERVISOR_EMPLID FIRST_SUP_EMPLID,(select First_Name+' '+Last_Name from HR_PDS_DATA_tbl where emplid=A.SUPERVISOR_EMPLID)FIRST_SUP_NAME,(select Email from HR_PDS_DATA_tbl where emplid=A.SUPERVISOR_EMPLID)"
        SQL1 &= " FIRST_SUP_EMAIL,/*2nd Supervisor*/(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where EMPLID=A.SUPERVISOR_EMPLID)UP_MGT_ID,(select First_Name+' '+Last_Name from HR_PDS_DATA_tbl where EMPLID in (select SUPERVISOR_EMPLID "
        SQL1 &= " from HR_PDS_DATA_tbl where EMPLID=A.SUPERVISOR_EMPLID))UP_MGT_NAME,(select Email from HR_PDS_DATA_tbl where EMPLID  in (select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where EMPLID=A.SUPERVISOR_EMPLID))UP_MGT_Email,"
        SQL1 &= " /*Generalist*/HR_Generalist,(select First_Name+' '+Last_Name from HR_PDS_DATA_tbl where emplid=A.HR_Generalist)HR_Generalist_Name,(select Email from HR_PDS_DATA_tbl where emplid=A.HR_Generalist)HR_Generalist_Email"
        SQL1 &= "  from HR_PDS_DATA_tbl A /*1END*/)AA)BB,(select * from ME_Appraisal_Master_tbl)CC where BB.emplid=CC.emplid And CC.emplid = " & lblEMPLID.Text & " And Perf_Year = " & lblYEAR.Text & ""
        'Response.Write(SQL1) : Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Session("Process_Flag") = CDbl(DT1.Rows(0)("Process_Flag").ToString)

        '--Manager Information--
        lblEMPLOYEE_NAME.Text = DT1.Rows(0)("EMPLOYEE_NAME").ToString
        lblEMPLOYEE_TITLE.Text = DT1.Rows(0)("jobtitle").ToString
        lblEMPLOYEE_EMAIL.Text = DT1.Rows(0)("EMPLOYEE_EMAIL").ToString
        lblEMPLOYEE_DEPT.Text = DT1.Rows(0)("Deptname").ToString
        lblEMPLOYEE_HIRE.Text = DT1.Rows(0)("Hired").ToString
        LblDEPTID.Text = DT1.Rows(0)("deptid").ToString

        '--Years--
        LblCur_Year.Text = DT1.Rows(0)("CY").ToString
        LblCur_Year1.Text = Right(Trim(DT1.Rows(0)("CY").ToString), 2)
        LblCur_Year2.Text = Right(Trim(DT1.Rows(0)("CY").ToString), 2)
        LblNext_Year.Text = Right(Trim(DT1.Rows(0)("NY").ToString), 2)
        LblNext_Year1.Text = Right(Trim(DT1.Rows(0)("NY").ToString), 2)
        LblNext_Year2.Text = Right(Trim(DT1.Rows(0)("NY").ToString), 2)
        LblNext_Year3.Text = Right(Trim(DT1.Rows(0)("NY").ToString), 2)
        '--First Supervisor--
        lblFirst_SUP_NAME.Text = DT1.Rows(0)("First_Sup_Name").ToString
        LblFirst_SUP_EMPLID.Text = DT1.Rows(0)("SUP_EMPLID").ToString
        lblFirst_SUP_EMAIL.Text = DT1.Rows(0)("First_SUP_Email").ToString
        LblMgr_NAME.Text = DT1.Rows(0)("First_Sup_Name").ToString

        '--Second Supervisor--
        lblUP_MGT_EMAIL.Text = DT1.Rows(0)("UP_MGT_EMAIL").ToString
        lblUP_MGT_NAME.Text = DT1.Rows(0)("UP_MGT_NAME").ToString

        If DT1.Rows(0)("SUP_EMPLID").ToString = DT1.Rows(0)("UP_MGT_ID").ToString Then
            lblMGR_UP_NAME.Text = "n/a"
        Else
            lblMGR_UP_NAME.Text = DT1.Rows(0)("UP_MGT_NAME").ToString
        End If
        lblUP_MGT_EMPLID.Text = DT1.Rows(0)("UP_MGT_EMPLID").ToString
        LblEligible_Full_Review.Text = DT1.Rows(0)("Elig_Review").ToString
        '---Employee Information--
        lblEMPL_Supervision.Text = DT1.Rows(0)("EMPL_Supervision").ToString
        '--Generalist--
        lblGENERALIST_NAME.Text = DT1.Rows(0)("HR_Generalist_Name").ToString
        lblGENERALIST_EMPLID.Text = DT1.Rows(0)("HR_Generalist").ToString
        lblGENERALIST_EMAIL.Text = DT1.Rows(0)("HR_Generalist_email").ToString
        lblHR_DEPTID.Text = DT1.Rows(0)("HR_DEPID").ToString

        '--Approval Date--
        If CDbl(DT1.Rows(0)("Process_Flag").ToString) = 1 Then
            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Sent").ToString
            LblSec_Mgr_Appr.Text = "Waiting Approval"
            LblSec_Mgr_Appr.ForeColor = Drawing.Color.Blue
            LblSec_Mgr_Appr.Font.Bold = True
            '--Send to Generalist
            '--Secod manager same as Generalist--  
            If Trim(lblUP_MGT_EMPLID.Text) = lblGENERALIST_EMPLID.Text Then
                HR_Generalist.Visible = False
                Generalist_Appr.Text = "Approve " & lblEMPLOYEE_NAME.Text & "'s Apprail"
                Generalist_Appr.Visible = True
            Else
                HR_Generalist.Text = "Submit for review to " & lblGENERALIST_NAME.Text & " "
                Generalist_Appr.Visible = False
            End If

            Discuss.Text = "Send back to " & lblFirst_SUP_NAME.Text & " for revision"
            Disc_Com.Text = "Send suggested revisions to " & lblFirst_SUP_NAME.Text & ""


            If LblLogin.Text = LblFirst_SUP_EMPLID.Text Then
                DiscussionPanel.Visible = False
                EditRecords.Visible = False
                SendToEmp.Visible = False
            End If



        ElseIf CDbl(DT1.Rows(0)("Process_Flag").ToString) = 2 Then
            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Sent").ToString
            If DT1.Rows(0)("SUP_EMPLID").ToString = DT1.Rows(0)("UP_MGT_ID").ToString Then
                LblSec_Mgr_Appr.Text = "n/a"
            Else
                LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
            End If

            If LblLogin.Text = LblFirst_SUP_EMPLID.Text Then

                DiscussionPanel.Visible = True
                EditRecords.Visible = False
                SendToEmp.Visible = False
            End If

            If lblEMPLID.Text = 6785 Then
                LblHR_Appr.Text = "n/a"
                lblGENERALIST_NAME.Text = "n/a"
            Else

                LblHR_Appr.Text = "Waiting Approval"
                LblHR_Appr.ForeColor = Drawing.Color.Blue
                LblHR_Appr.Font.Bold = True
                '--Show Generalist Approval
                If LblLogin.Text = 1631 Or LblLogin.Text = 3474 Or LblLogin.Text = 4835 Or LblLogin.Text = 6194 Or LblLogin.Text = 6196 Or LblLogin.Text = 2714 Or LblLogin.Text = 6217 Or LblLogin.Text = 3097 Or LblLogin.Text = 1091 Then
                    DiscussionPanel.Visible = False
                Else
                    HR_Generalist.Visible = False
                    Generalist_Appr.Visible = True
                    Generalist_Appr.Text = "Approve " & lblEMPLOYEE_NAME.Text & "'s Appraisal"
                    Discuss.Text = "Send back to " & lblFirst_SUP_NAME.Text & " for revision"
                    Disc_Com.Text = "Send suggested revisions to " & lblFirst_SUP_NAME.Text & ""
                    'Response.Write(LblLogin.Text)
                End If

            End If

        ElseIf CDbl(DT1.Rows(0)("Process_Flag").ToString) = 3 Then
                LblFirst_Mgr_Appr.Text = "Not Sent to Employee"
                LblFirst_Mgr_Appr.ForeColor = Drawing.Color.Blue
                LblFirst_Mgr_Appr.Font.Bold = True

                If DT1.Rows(0)("SUP_EMPLID").ToString = DT1.Rows(0)("UP_MGT_ID").ToString Then
                    LblSec_Mgr_Appr.Text = "n/a"
                Else
                    LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
                End If

                LblHR_Appr.Text = DT1.Rows(0)("Date_HR_Approved").ToString
                '--Send to Employee
                DiscussionPanel.Visible = False
                EditRecords.Visible = True
                SendToEmp.Visible = True
                SendToEmp.Text = "Send to " & lblEMPLOYEE_NAME.Text

        ElseIf CDbl(DT1.Rows(0)("Process_Flag").ToString) = 4 Then
                LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Sent").ToString

                If DT1.Rows(0)("SUP_EMPLID").ToString = DT1.Rows(0)("UP_MGT_ID").ToString Then
                    LblSec_Mgr_Appr.Text = "n/a"
                Else
                    LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
                End If

                LblHR_Appr.Text = DT1.Rows(0)("Date_HR_Approved").ToString
                LblEMP_Appr.Text = "Waiting E Signature"
                LblEMP_Appr.ForeColor = Drawing.Color.Blue
                LblEMP_Appr.Font.Bold = True
                '--Waitng Employee's response
                DiscussionPanel.Visible = False
                EditRecords.Visible = True
                SendToEmp.Visible = False

                If LblFirst_SUP_EMPLID.Text = DT1.Rows(0)("SUP_EMPLID").ToString Then
                    lblSentToEmployee.Text = "Sent to Employee on " & DT1.Rows(0)("Date_Sent_Employee").ToString
                End If


        ElseIf CDbl(DT1.Rows(0)("Process_Flag").ToString) = 5 Then

            If lblEMPLID.Text = 6785 Then
                lblGENERALIST_NAME.Text = "n/a"
                LblHR_Appr.Text = "n/a"
                LblEMP_Appr.Text = "n/a"
                DiscussionPanel.Visible = False
                EditRecords.Visible = False
                SendToEmp.Visible = False
            Else
                LblHR_Appr.Text = DT1.Rows(0)("Date_HR_Approved").ToString
                LblEMP_Appr.Text = DT1.Rows(0)("Date_Employee_Esign").ToString
                '--Employee E-sign          
                DiscussionPanel.Visible = False
                EditRecords.Visible = False
                SendToEmp.Visible = False
            End If

                LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Sent").ToString

                If DT1.Rows(0)("SUP_EMPLID").ToString = DT1.Rows(0)("UP_MGT_ID").ToString Then
                    LblSec_Mgr_Appr.Text = "n/a"
                Else
                    LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
                End If

        End If

        '--Show/Hide tables in Page #3 depend of manage 
        If CDbl(DT1.Rows(0)("EMPL_Supervision").ToString) = 0 Then
            No_Employees.Visible = True
            Manage_Employees.Visible = False
        Else
            No_Employees.Visible = False
            '--Only for this year 04/08/2015--M.Z.
            'Manage_Employees.Visible = True   
            '--Remove next year--M.Z.
            Manage_Employees.Visible = False
            Page3.Visible = False
        End If

        '--Show / Hide New Employees appraisal
        If LblEligible_Full_Review.Text = "SHORT" Then
            Panel_Goal1.Visible = False
            Panel_Strengths.Visible = False
            Panel_Development.Visible = False
            Panel_OveallRating.Visible = False
            Panel_Summary.Visible = False
            Page3.Visible = False
            No_Employees.Visible = False
        ElseIf LblEligible_Full_Review.Text = "FULL" And CDbl(DT1.Rows(0)("EMPL_Supervision").ToString) = 0 Then
            Panel_Goal1.Visible = True
            Panel_Strengths.Visible = True
            Panel_Development.Visible = True
            Panel_OveallRating.Visible = True
            Panel_Summary.Visible = True
            Page3.Visible = True
            'No_Employees.Visible = True
        End If

        Discuss.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';")
        Discuss.Attributes.Add("onmouseout", "this.style.color='#000000'")
        EditRecords.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';")
        EditRecords.Attributes.Add("onmouseout", "this.style.color='#000000'")
        SendToEmp.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';")
        SendToEmp.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Generalist_Appr.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';")
        Generalist_Appr.Attributes.Add("onmouseout", "this.style.color='#000000'")
        HR_Generalist.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';")
        HR_Generalist.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Print_Preview.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';")
        Print_Preview.Attributes.Add("onmouseout", "this.style.color='#666666'")

        ' Hide all controls 
        BtnFuture_Goal.Visible = False : Discuss.Visible = False : HR_Generalist.Visible = False : Generalist_Appr.Visible = False : EditRecords.Visible = False
        SendToEmp.Visible = False : SaveFutureGoal.Visible = False : Print_Preview.Visible = False : Disc_Com.Visible = False : DiscussionComments.Visible = False
        Delete_FutureGoal2.Visible = False : Delete_FutureGoal3.Visible = False : Delete_FutureGoal4.Visible = False : Delete_FutureGoal5.Visible = False
        Delete_FutureGoal6.Visible = False : Delete_FutureGoal7.Visible = False : Delete_FutureGoal8.Visible = False : Delete_FutureGoal9.Visible = False
        Delete_FutureGoal10.Visible = False




    End Sub
    Protected Sub ShowHideGoalPanel()
        'DisplayData()
        SQL6 = "select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5 from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then"
        SQL6 &= " count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,(case when IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5"
        SQL6 &= " from ME_Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " group by IndexID)A"
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_Goal2.Visible = True Else Panel_Goal2.Visible = False
        If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_Goal3.Visible = True Else Panel_Goal3.Visible = False
        If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_Goal4.Visible = True Else Panel_Goal4.Visible = False
        If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_Goal5.Visible = True Else Panel_Goal5.Visible = False
        LocalClass.CloseSQLServerConnection()

        DisplayData()
    End Sub
    Protected Sub Delete_FutureGoal2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal2.Click
        SQL = "delete ME_Appraisal_FutureGoals_TBL where IndexID=2 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update ME_Appraisal_FutureGoals_TBL Set IndexID=IndexID-1 where IndexID>2 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal3.Click
        SQL = "delete ME_Appraisal_FutureGoals_TBL where IndexID=3 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update ME_Appraisal_FutureGoals_TBL Set IndexID=IndexID-1 where IndexID>3 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal4.Click
        SQL = "delete ME_Appraisal_FutureGoals_TBL where IndexID=4 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update ME_Appraisal_FutureGoals_TBL Set IndexID=IndexID-1 where IndexID>4 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal5.Click
        SQL = "delete ME_Appraisal_FutureGoals_TBL where IndexID=5 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update ME_Appraisal_FutureGoals_TBL Set IndexID=IndexID-1 where IndexID>5 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal6.Click
        SQL = "delete ME_Appraisal_FutureGoals_TBL where IndexID=6 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update ME_Appraisal_FutureGoals_TBL Set IndexID=IndexID-1 where IndexID>6 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal7.Click
        SQL = "delete ME_Appraisal_FutureGoals_TBL where IndexID=7 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update ME_Appraisal_FutureGoals_TBL Set IndexID=IndexID-1 where IndexID>7 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal8.Click
        SQL = "delete ME_Appraisal_FutureGoals_TBL where IndexID=8 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update ME_Appraisal_FutureGoals_TBL Set IndexID=IndexID-1 where IndexID>8 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal9.Click
        SQL = "delete ME_Appraisal_FutureGoals_TBL where IndexID=9 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update ME_Appraisal_FutureGoals_TBL Set IndexID=IndexID-1 where IndexID>9 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal10.Click
        SQL = "delete ME_Appraisal_FutureGoals_TBL where IndexID=10 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update ME_Appraisal_FutureGoals_TBL Set IndexID=IndexID-1 where IndexID>10 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub ShowHideFutureGoalPanel()

        SQL6 = "select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10"
        SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,(case when"
        SQL6 &= " IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*)"
        SQL6 &= " else 0 end)F_IND7,(case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
        SQL6 &= " from ME_Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " group by IndexID)A"
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)

        If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_FutureGoal2.Visible = True : Panel_FutureGoal_Edit2.Visible = True Else Panel_FutureGoal2.Visible = False : Panel_FutureGoal_Edit2.Visible = False
        If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_FutureGoal3.Visible = True : Panel_FutureGoal_Edit3.Visible = True Else Panel_FutureGoal3.Visible = False : Panel_FutureGoal_Edit3.Visible = False
        If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_FutureGoal4.Visible = True : Panel_FutureGoal_Edit4.Visible = True Else Panel_FutureGoal4.Visible = False : Panel_FutureGoal_Edit4.Visible = False
        If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_FutureGoal5.Visible = True : Panel_FutureGoal_Edit5.Visible = True Else Panel_FutureGoal5.Visible = False : Panel_FutureGoal_Edit5.Visible = False
        If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel_FutureGoal6.Visible = True : Panel_FutureGoal_Edit6.Visible = True Else Panel_FutureGoal6.Visible = False : Panel_FutureGoal_Edit6.Visible = False
        If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel_FutureGoal7.Visible = True : Panel_FutureGoal_Edit7.Visible = True Else Panel_FutureGoal7.Visible = False : Panel_FutureGoal_Edit7.Visible = False
        If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel_FutureGoal8.Visible = True : Panel_FutureGoal_Edit8.Visible = True Else Panel_FutureGoal8.Visible = False : Panel_FutureGoal_Edit8.Visible = False
        If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel_FutureGoal9.Visible = True : Panel_FutureGoal_Edit9.Visible = True Else Panel_FutureGoal9.Visible = False : Panel_FutureGoal_Edit9.Visible = False
        If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel_FutureGoal10.Visible = True : Panel_FutureGoal_Edit10.Visible = True Else Panel_FutureGoal10.Visible = False : Panel_FutureGoal_Edit10.Visible = False

        LocalClass.CloseSQLServerConnection()

        DisplayData()


    End Sub
    Protected Sub DisplayData()
        '--Master table
        SQL = "select * from(select * from ME_Appraisal_Master_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A,"
        '--Accomplishments table 
        SQL &= "(select Max(Accomp1)Accomp1, Max(Accomp2)Accomp2,Max(Accomp3)Accomp3,Max(Accomp4)Accomp4,Max(Accomp5)Accomp5 from("
        SQL &= "select (case when IndexID=1 then Rtrim(Ltrim(Accomplishment)) else '' end)Accomp1,(case when IndexID=2 then Rtrim(Ltrim(Accomplishment)) else '' end)Accomp2,"
        SQL &= "(case when IndexID=3 then Rtrim(Ltrim(Accomplishment)) else '' end)Accomp3,(case when IndexID=4 then Rtrim(Ltrim(Accomplishment)) else '' end)Accomp4,"
        SQL &= "(case when IndexID=5 then Rtrim(Ltrim(Accomplishment)) else '' end)Accomp5 from ME_Appraisal_Accomplishments_TBL where  Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A )B,"
        '--Future Goals table
        SQL &= "(select Max(Goals1)Goals1,Max(Milestones1)Milestones1,Max(TargetDate1)TargetDate1,Max(Goals2)Goals2,Max(Milestones2)Milestones2,Max(TargetDate2)TargetDate2,Max(Goals3)Goals3,Max(Milestones3)Milestones3,"
        SQL &= "Max(TargetDate3)TargetDate3,Max(Goals4)Goals4,Max(Milestones4)Milestones4,Max(TargetDate4)TargetDate4,Max(Goals5)Goals5,Max(Milestones5)Milestones5,Max(TargetDate5)TargetDate5,Max(Goals6)Goals6,"
        SQL &= "Max(Milestones6)Milestones6,Max(TargetDate6)TargetDate6,Max(Goals7)Goals7,Max(Milestones7)Milestones7,Max(TargetDate7)TargetDate7,Max(Goals8)Goals8,Max(Milestones8)Milestones8,Max(TargetDate8)TargetDate8,"
        SQL &= "Max(Goals9)Goals9,Max(Milestones9)Milestones9,Max(TargetDate9)TargetDate9,Max(Goals10)Goals10,Max(Milestones10)Milestones10,Max(TargetDate10)TargetDate10 from(select "
        SQL &= "(case when IndexID=1 then Rtrim(Ltrim(Goals)) else '' end)Goals1,(case when IndexID=1 then Rtrim(Ltrim(Milestones)) else '' end)Milestones1,(case when IndexID=1 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate1,"
        SQL &= "(case when IndexID=2 then Rtrim(Ltrim(Goals)) else '' end)Goals2,(case when IndexID=2 then Rtrim(Ltrim(Milestones)) else '' end)Milestones2,(case when IndexID=2 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate2,"
        SQL &= "(case when IndexID=3 then Rtrim(Ltrim(Goals)) else '' end)Goals3,(case when IndexID=3 then Rtrim(Ltrim(Milestones)) else '' end)Milestones3,(case when IndexID=3 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate3,"
        SQL &= "(case when IndexID=4 then Rtrim(Ltrim(Goals)) else '' end)Goals4,(case when IndexID=4 then Rtrim(Ltrim(Milestones)) else '' end)Milestones4,(case when IndexID=4 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate4,"
        SQL &= "(case when IndexID=5 then Rtrim(Ltrim(Goals)) else '' end)Goals5,(case when IndexID=5 then Rtrim(Ltrim(Milestones)) else '' end)Milestones5,(case when IndexID=5 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate5,"
        SQL &= "(case when IndexID=6 then Rtrim(Ltrim(Goals)) else '' end)Goals6,(case when IndexID=6 then Rtrim(Ltrim(Milestones)) else '' end)Milestones6,(case when IndexID=6 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate6,"
        SQL &= "(case when IndexID=7 then Rtrim(Ltrim(Goals)) else '' end)Goals7,(case when IndexID=7 then Rtrim(Ltrim(Milestones)) else '' end)Milestones7,(case when IndexID=7 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate7,"
        SQL &= "(case when IndexID=8 then Rtrim(Ltrim(Goals)) else '' end)Goals8,(case when IndexID=8 then Rtrim(Ltrim(Milestones)) else '' end)Milestones8,(case when IndexID=8 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate8,"
        SQL &= "(case when IndexID=9 then Rtrim(Ltrim(Goals)) else '' end)Goals9,(case when IndexID=9 then Rtrim(Ltrim(Milestones)) else '' end)Milestones9,(case when IndexID=9 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate9,"
        SQL &= "(case when IndexID=10 then Rtrim(Ltrim(Goals)) else '' end)Goals10,(case when IndexID=10 then Rtrim(Ltrim(Milestones)) else '' end)Milestones10,(case when IndexID=10 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate10"
        SQL &= " from ME_Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A  )C"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '---Oveall Performance Rating
        If DT.Rows(0)("Overall_rating").ToString = 1 Then rbBelow.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 2 Then rbNeed.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 3 Then rbMeet.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 4 Then rbExceed.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 5 Then rbDisting.Checked = True

        'Response.Write(CDbl(lblEMPL_Supervision.Text)) : Response.End()
        If CDbl(lblEMPL_Supervision.Text) > 0 Then
            '--Lead Change
            If DT.Rows(0)("Lead_Change").ToString = 1 Then rbLead_Need.Checked = True
            If DT.Rows(0)("Lead_Change").ToString = 2 Then rbLead_Prof.Checked = True
            If DT.Rows(0)("Lead_Change").ToString = 3 Then rbLead_Exce.Checked = True
            '--Translate Vision into Action
            If DT.Rows(0)("Translate_Vision").ToString = 1 Then rbTran_Need.Checked = True
            If DT.Rows(0)("Translate_Vision").ToString = 2 Then rbTran_Prof.Checked = True
            If DT.Rows(0)("Translate_Vision").ToString = 3 Then rbTran_Exce.Checked = True
            '---Inspire Risk Taking & innovation
            If DT.Rows(0)("Inspire_Risk").ToString = 1 Then rbInsp_Need.Checked = True
            If DT.Rows(0)("Inspire_Risk").ToString = 2 Then rbInsp_Prof.Checked = True
            If DT.Rows(0)("Inspire_Risk").ToString = 3 Then rbInsp_Exce.Checked = True
            '---Leverage External Perspective
            If DT.Rows(0)("Leverage_External").ToString = 1 Then rbLeve_Need.Checked = True
            If DT.Rows(0)("Leverage_External").ToString = 2 Then rbLeve_Prof.Checked = True
            If DT.Rows(0)("Leverage_External").ToString = 3 Then rbLeve_Exce.Checked = True
            '---Communicate for Impact
            If DT.Rows(0)("Communicate_Impact").ToString = 1 Then rbComm_Need.Checked = True
            If DT.Rows(0)("Communicate_Impact").ToString = 2 Then rbComm_Prof.Checked = True
            If DT.Rows(0)("Communicate_Impact").ToString = 3 Then rbComm_Exce.Checked = True
            '---Lead with Urgency & Purpose
            If DT.Rows(0)("Lead_Urgency").ToString = 1 Then rbLead2_Need.Checked = True
            If DT.Rows(0)("Lead_Urgency").ToString = 2 Then rbLead2_Prof.Checked = True
            If DT.Rows(0)("Lead_Urgency").ToString = 3 Then rbLead2_Exce.Checked = True
            '---Promote Collaboration & Accountability
            If DT.Rows(0)("Promote_Collaboration").ToString = 1 Then rbProm_Need.Checked = True
            If DT.Rows(0)("Promote_Collaboration").ToString = 2 Then rbProm_Prof.Checked = True
            If DT.Rows(0)("Promote_Collaboration").ToString = 3 Then rbProm_Exce.Checked = True
            '---Build & Manage High Performing Teams
            If DT.Rows(0)("Build_Manage").ToString = 1 Then rbBuild_Need.Checked = True
            If DT.Rows(0)("Build_Manage").ToString = 2 Then rbBuild_Prof.Checked = True
            If DT.Rows(0)("Build_Manage").ToString = 3 Then rbBuild_Exce.Checked = True
            '---Confront Challenges
            If DT.Rows(0)("Confront_Challenges").ToString = 1 Then rbConf_Need.Checked = True
            If DT.Rows(0)("Confront_Challenges").ToString = 2 Then rbConf_Prof.Checked = True
            If DT.Rows(0)("Confront_Challenges").ToString = 3 Then rbConf_Exce.Checked = True
            '---Make Balanced Decisions
            If DT.Rows(0)("Make_Balance").ToString = 1 Then rbMake_Need.Checked = True
            If DT.Rows(0)("Make_Balance").ToString = 2 Then rbMake_Prof.Checked = True
            If DT.Rows(0)("Make_Balance").ToString = 3 Then rbMake_Exce.Checked = True
            '---Build Trust
            If DT.Rows(0)("Build_Trust").ToString = 1 Then rbBuild2_Need.Checked = True
            If DT.Rows(0)("Build_Trust").ToString = 2 Then rbBuild2_Prof.Checked = True
            If DT.Rows(0)("Build_Trust").ToString = 3 Then rbBuild2_Exce.Checked = True
            '---Learn Continuously
            If DT.Rows(0)("Learn_Continuously").ToString = 1 Then rbLearn_Need.Checked = True
            If DT.Rows(0)("Learn_Continuously").ToString = 2 Then rbLearn_Prof.Checked = True
            If DT.Rows(0)("Learn_Continuously").ToString = 3 Then rbLearn_Exce.Checked = True
        Else
            'Response.Write("No Supervision") : Response.End()
            '--Lead Change
            If DT.Rows(0)("Lead_Change").ToString = 1 Then rbLead_Need1.Checked = True
            If DT.Rows(0)("Lead_Change").ToString = 2 Then rbLead_Prof1.Checked = True
            If DT.Rows(0)("Lead_Change").ToString = 3 Then rbLead_Exce1.Checked = True
            '---Inspire Risk Taking & innovation
            If DT.Rows(0)("Inspire_Risk").ToString = 1 Then rbInsp_Need1.Checked = True
            If DT.Rows(0)("Inspire_Risk").ToString = 2 Then rbInsp_Prof1.Checked = True
            If DT.Rows(0)("Inspire_Risk").ToString = 3 Then rbInsp_Exce1.Checked = True
            '---Leverage External Perspective
            If DT.Rows(0)("Leverage_External").ToString = 1 Then rbLeve_Need1.Checked = True
            If DT.Rows(0)("Leverage_External").ToString = 2 Then rbLeve_Prof1.Checked = True
            If DT.Rows(0)("Leverage_External").ToString = 3 Then rbLeve_Exce1.Checked = True
            '---Communicate for Impact
            If DT.Rows(0)("Communicate_Impact").ToString = 1 Then rbComm_Need1.Checked = True
            If DT.Rows(0)("Communicate_Impact").ToString = 2 Then rbComm_Prof1.Checked = True
            If DT.Rows(0)("Communicate_Impact").ToString = 3 Then rbComm_Exce1.Checked = True
            '---Lead with Urgency & Purpose
            If DT.Rows(0)("Lead_Urgency").ToString = 1 Then rbLead2_Need1.Checked = True
            If DT.Rows(0)("Lead_Urgency").ToString = 2 Then rbLead2_Prof1.Checked = True
            If DT.Rows(0)("Lead_Urgency").ToString = 3 Then rbLead2_Exce1.Checked = True
            '---Promote Collaboration & Accountability
            If DT.Rows(0)("Promote_Collaboration").ToString = 1 Then rbProm_Need1.Checked = True
            If DT.Rows(0)("Promote_Collaboration").ToString = 2 Then rbProm_Prof1.Checked = True
            If DT.Rows(0)("Promote_Collaboration").ToString = 3 Then rbProm_Exce1.Checked = True
            '---Confront Challenges
            If DT.Rows(0)("Confront_Challenges").ToString = 1 Then rbConf_Need1.Checked = True
            If DT.Rows(0)("Confront_Challenges").ToString = 2 Then rbConf_Prof1.Checked = True
            If DT.Rows(0)("Confront_Challenges").ToString = 3 Then rbConf_Exce1.Checked = True
            '---Make Balanced Decisions
            If DT.Rows(0)("Make_Balance").ToString = 1 Then rbMake_Need1.Checked = True
            If DT.Rows(0)("Make_Balance").ToString = 2 Then rbMake_Prof1.Checked = True
            If DT.Rows(0)("Make_Balance").ToString = 3 Then rbMake_Exce1.Checked = True
            '---Build Trust
            If DT.Rows(0)("Build_Trust").ToString = 1 Then rbBuild2_Need1.Checked = True
            If DT.Rows(0)("Build_Trust").ToString = 2 Then rbBuild2_Prof1.Checked = True
            If DT.Rows(0)("Build_Trust").ToString = 3 Then rbBuild2_Exce1.Checked = True
            '---Learn Continuously
            If DT.Rows(0)("Learn_Continuously").ToString = 1 Then rbLearn_Need1.Checked = True
            If DT.Rows(0)("Learn_Continuously").ToString = 2 Then rbLearn_Prof1.Checked = True
            If DT.Rows(0)("Learn_Continuously").ToString = 3 Then rbLearn_Exce1.Checked = True
        End If

        Strengths.Text = Replace(Replace(DT.Rows(0)("Strenghts").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        Development.Text = Replace(Replace(DT.Rows(0)("DevelopmentAreas").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        OverAll_Sum.Text = Replace(Replace(DT.Rows(0)("OverAll_Summary").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        '---Display  data from Accomplishments table
        Goals_1.Text = Replace(Replace(DT.Rows(0)("Accomp1").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        Goals_2.Text = Replace(Replace(DT.Rows(0)("Accomp2").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        Goals_3.Text = Replace(Replace(DT.Rows(0)("Accomp3").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        Goals_4.Text = Replace(Replace(DT.Rows(0)("Accomp4").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        Goals_5.Text = Replace(Replace(DT.Rows(0)("Accomp5").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        '--Dispaly data from Future Goals table for View 
        FUT_Goal_1.Text = Replace(Replace(DT.Rows(0)("Goals1").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_1.Text = Replace(Replace(DT.Rows(0)("Milestones1").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_1.Text = Replace(Replace(DT.Rows(0)("TargetDate1").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Goal_2.Text = Replace(Replace(DT.Rows(0)("Goals2").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_2.Text = Replace(Replace(DT.Rows(0)("Milestones2").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_2.Text = Replace(Replace(DT.Rows(0)("TargetDate2").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Goal_3.Text = Replace(Replace(DT.Rows(0)("Goals3").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_3.Text = Replace(Replace(DT.Rows(0)("Milestones3").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_3.Text = Replace(Replace(DT.Rows(0)("TargetDate3").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Goal_4.Text = Replace(Replace(DT.Rows(0)("Goals4").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_4.Text = Replace(Replace(DT.Rows(0)("Milestones4").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_4.Text = Replace(Replace(DT.Rows(0)("TargetDate4").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Goal_5.Text = Replace(Replace(DT.Rows(0)("Goals5").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_5.Text = Replace(Replace(DT.Rows(0)("Milestones5").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_5.Text = Replace(Replace(DT.Rows(0)("TargetDate5").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Goal_6.Text = Replace(Replace(DT.Rows(0)("Goals6").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_6.Text = Replace(Replace(DT.Rows(0)("Milestones6").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_6.Text = Replace(Replace(DT.Rows(0)("TargetDate6").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Goal_7.Text = Replace(Replace(DT.Rows(0)("Goals7").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_7.Text = Replace(Replace(DT.Rows(0)("Milestones7").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_7.Text = Replace(Replace(DT.Rows(0)("TargetDate7").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Goal_8.Text = Replace(Replace(DT.Rows(0)("Goals8").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_8.Text = Replace(Replace(DT.Rows(0)("Milestones8").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_8.Text = Replace(Replace(DT.Rows(0)("TargetDate8").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Goal_9.Text = Replace(Replace(DT.Rows(0)("Goals9").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_9.Text = Replace(Replace(DT.Rows(0)("Milestones9").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_9.Text = Replace(Replace(DT.Rows(0)("TargetDate9").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Goal_10.Text = Replace(Replace(DT.Rows(0)("Goals10").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Succ_10.Text = Replace(Replace(DT.Rows(0)("Milestones10").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        FUT_Date_10.Text = Replace(Replace(DT.Rows(0)("TargetDate10").ToString, Chr(13), "<p>"), Chr(10), "<p>")

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Discuss_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Discuss.Click

        If Len(DiscussionComments.Text) < 5 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please type discussion comments'); </script>")
        Else

            SQL = "update ME_Appraisal_MASTER_tbl set Process_Flag=0,UP_MGT_EMPLID='',HR_EMPLID='',Date_Submitted_To_HR=NULL,Date_HR_Approved=NULL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text

            SQL &= " insert into ME_Appraisal_Discussion_tbl (EMPLID,Perf_Year,MGR_EMPLID,REJ_EMPLID,DateTime,Comments)"
            SQL &= " values(" & lblEMPLID.Text & ",'" & lblYEAR.Text & "'," & LblFirst_SUP_EMPLID.Text & "," & Session("MGT_EMPLID") & ",'" & Now & "','" & Replace(DiscussionComments.Text, "'", "`") & "')"
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            SQL1 = "select first+' '+last Name from id_tbl where emplid=" & LblLogin.Text
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)


            Msg = DT1.Rows(0)("Name").ToString & " has rejected " & lblEMPLOYEE_NAME.Text & "'s Performance Appraisal for the following reason:<br><br>"
            Msg &= Replace(DiscussionComments.Text, "'", "`") & " <br><br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/ME_Performance_Appraisal/Default.aspx for full details "

            Msg1 = "The Manager/Exempt Performance Appraisal you had sent to " & lblFirst_SUP_NAME.Text & " regarding employee " & lblEMPLOYEE_NAME.Text & " reads as follows:<br>"
            Msg1 &= "<b> " & Replace(DiscussionComments.Text, "'", "`") & " "

            '--Production email
            'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Manager/Exempt Performance Appraisal was Rejected by " & Session("NAME"), Msg)



            If CDbl(lblGENERALIST_EMPLID.Text) = CDbl(LblLogin.Text) Then
                'LocalClass.SendMail(lblGENERALIST_EMAIL.Text, "Manager/Exempt Performance Appraisal - Revise comments", Msg1)

                '("EMargalit@consumer.org", "Manager/Exempt Performance Appraisal - Revise comments", Msg1)
            End If


            If LblLogin.Text = lblGENERALIST_EMPLID.Text And lblHR_DEPTID.Text = 9009120 Then
                Response.Redirect("Admin_Default.aspx")
            Else
                Response.Redirect("Manager_Default.aspx")
            End If

        End If

        LocalClass.CloseSQLServerConnection()


    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click

        SQL = "select count(*)CNT_MGT from ME_Appraisal_MASTER_tbl where SUP_EMPLID=" & LblLogin.Text & " or UP_MGT_EMPLID=" & LblLogin.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(LblLogin.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        If CDbl(DT.Rows(0)("CNT_MGT").ToString) = 0 Then
            Response.Redirect("..\..\Default_HR.aspx?Token=" & x)
        Else
            Response.Redirect("..\..\Default_Manager.aspx?Token=" & x)
        End If
    End Sub
    Protected Sub Print_Preview_Click(sender As Object, e As EventArgs) Handles Print_Preview.Click

        x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblEMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        Response.Redirect("Appraisal_Review.aspx?Token=" & x)

    End Sub
    Protected Sub HR_Generalist_Click(sender As Object, e As EventArgs) Handles HR_Generalist.Click
        SQL1 = "update ME_Appraisal_Master_tbl Set Date_Submitted_To_HR='" & Now & "',Process_Flag=2,HR_EMPLID=" & lblGENERALIST_EMPLID.Text & " "
        SQL1 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblEMPLOYEE_NAME.Text & "'s Performance Appraisal has been completed by " & lblFirst_SUP_NAME.Text & " and approved by " & lblUP_MGT_NAME.Text & "<br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/ME_Performance_Appraisal/Default.aspx for full details."

        '--Production email
        'LocalClass.SendMail(lblGENERALIST_EMAIL.Text, "Manager/Exempt Performance Appraisal was sent to you for Approval by " & Session("NAME"), Msg)

        '("EMargalit@consumer.org", "Manager/Exempt Performance Appraisal was sent to you for Approval by " & Session("NAME"), Msg)

        If LblLogin.Text = lblGENERALIST_EMPLID.Text And lblHR_DEPTID.Text = 9009120 Then
            Response.Redirect("Admin_Default.aspx")
        Else
            Response.Redirect("Manager_Default.aspx")
        End If

    End Sub
    Protected Sub Generalist_Appr_Click(sender As Object, e As EventArgs) Handles Generalist_Appr.Click
        SQL1 = "update ME_Appraisal_Master_tbl Set Date_HR_Approved='" & Now & "',Process_Flag=3 "
        If Trim(lblUP_MGT_EMPLID.Text) = Trim(lblGENERALIST_EMPLID.Text) Then
            SQL1 &= " ,HR_EMPLID= " & lblUP_MGT_EMPLID.Text & ", Date_Submitted_To_HR= '" & Now & "'"
        End If

        SQL1 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text

        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblEMPLOYEE_NAME.Text & "'s Performance Appraisal has been Approved by " & lblGENERALIST_NAME.Text & "<br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/ME_Performance_Appraisal/Default.aspx for full details."

        '--Production email
        'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Manager/Exempt Performance Appraisal was Reviewed by HR", Msg)

        Response.Redirect("Admin_Default.aspx")

    End Sub
    Protected Sub EditRecords_Click(sender As Object, e As EventArgs) Handles EditRecords.Click
        SQL = " update ME_Appraisal_MASTER_tbl set Process_Flag=0,UP_MGT_EMPLID='',HR_EMPLID='',Date_Submitted_To_HR=NULL,Date_HR_Approved=NULL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        SQL &= " Insert into ME_Appraisal_Discussion_tbl values('" & lblEMPLID.Text & "','" & lblYEAR.Text & "','" & LblFirst_SUP_EMPLID.Text & "','" & LblFirst_SUP_EMPLID.Text & "','" & Now & "','Edit')"
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        'Response.Redirect("Manager_Performance.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub SendToEmp_Click(sender As Object, e As EventArgs) Handles SendToEmp.Click

        SQL = "update ME_Appraisal_MASTER_tbl set Process_Flag=4, Date_Sent_Employee='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        Msg = "Your performance appraisal has been completed by your manager and is ready for your review.<br><br>"
        Msg &= "<b>NOTE: Please do not electronically sign and submit the review until you have had the Appraisal/Ratings Criteria discussion with your manager."
        Msg &= "Your electronic signature only confirms that you have had the discussion, not that you necessarily agree with the contents of the appraisal.</b><br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/ME_Performance_Appraisal/Default.aspx for full details."

        '--Production email 
        'LocalClass.SendMail(lblEMPLOYEE_EMAIL.Text, "Your Performance Appraisal is ready for your review", Msg)

        '("EMargalit@consumer.org", "Your Performance Appraisal is ready for your review", Msg)

        If LblLogin.Text = lblGENERALIST_EMPLID.Text And lblHR_DEPTID.Text = 9009120 Then
            Response.Redirect("Admin_Default.aspx")
        Else
            Response.Redirect("Manager_Default.aspx")
        End If

    End Sub
    Protected Sub BtnFuture_Goal_Click(sender As Object, e As EventArgs) Handles BtnFuture_Goal.Click
        SQL = "select count(*)CNT_FUT from ME_Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " "
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If CDbl(DT.Rows(0)("CNT_FUT").ToString) = 0 Then
            SQL3 = " insert into ME_Appraisal_FutureGoals_TBL (EMPLID,Perf_Year,Future_Year,IndexID,Goals,Milestones,TargetDate) values(" & lblEMPLID.Text & "," & lblYEAR.Text & "," & lblYEAR.Text + 1 & ",1,'','','')"
            'Response.Write("Zero " & SQL3) : Response.End()
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()
        Else
            SQL2 = "select Max(IndexId)+1 NewIndexID from ME_Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " "
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
            SQL4 &= " insert into ME_Appraisal_FutureGoals_TBL (EMPLID,Perf_Year,Future_Year,IndexID,Goals,Milestones,TargetDate) values"
            SQL4 &= " (" & lblEMPLID.Text & ", " & lblYEAR.Text & " ," & lblYEAR.Text + 1 & "," & DT2.Rows(0)("NewIndexID").ToString & ",'','','')"
            'Response.Write(SQL2 & "<br>" & SQL4) : Response.End()
            DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
            LocalClass.CloseSQLServerConnection()

            SQL3 = "select Max(IndexId) NextIndexID from ME_Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " "
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()

            If CDbl(DT3.Rows(0)("NextIndexID").ToString) > 10 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('A maximum of 10 task has been exceeded.'); </script>")
                SQL2 &= "delete ME_Appraisal_FutureGoals_TBL where IndexID>10"
                DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
                LocalClass.CloseSQLServerConnection()
            Else
                ShowHideFutureGoalPanel()

            End If

        End If
    End Sub

    Protected Sub SaveFutureData()

        '--Update data from Future Goals table
        SQL1 = " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit1.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit1.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit1.Text), "'", "`") & "' where IndexId=1 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit2.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit2.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit2.Text), "'", "`") & "' where IndexId=2 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit3.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit3.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit3.Text), "'", "`") & "' where IndexId=3 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit4.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit4.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit4.Text), "'", "`") & "' where IndexId=4 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit5.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit5.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit5.Text), "'", "`") & "' where IndexId=5 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit6.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit6.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit6.Text), "'", "`") & "' where IndexId=6 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit7.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit7.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit7.Text), "'", "`") & "' where IndexId=7 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit8.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit8.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit8.Text), "'", "`") & "' where IndexId=8 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit9.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit9.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit9.Text), "'", "`") & "' where IndexId=9 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update ME_Appraisal_FutureGoals_TBL Set Goals='" & Replace(Trim(FUT_Goal_Edit10.Text), "'", "`") & "', Milestones='" & Replace(Trim(FUT_Succ_Edit10.Text), "'", "`") & "',TargetDate='" & Replace(Trim(FUT_Date_Edit10.Text), "'", "`") & "' where IndexId=10 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        'Response.Write(SQL1) ': Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        DisplayFutureData()

    End Sub

    Protected Sub DisplayFutureData()
        '--Future Goals table
        SQL5 = "select Max(Goals1)Goals1,Max(Milestones1)Milestones1,Max(TargetDate1)TargetDate1,Max(Goals2)Goals2,Max(Milestones2)Milestones2,Max(TargetDate2)TargetDate2,Max(Goals3)Goals3,Max(Milestones3)Milestones3,"
        SQL5 &= "Max(TargetDate3)TargetDate3,Max(Goals4)Goals4,Max(Milestones4)Milestones4,Max(TargetDate4)TargetDate4,Max(Goals5)Goals5,Max(Milestones5)Milestones5,Max(TargetDate5)TargetDate5,Max(Goals6)Goals6,"
        SQL5 &= "Max(Milestones6)Milestones6,Max(TargetDate6)TargetDate6,Max(Goals7)Goals7,Max(Milestones7)Milestones7,Max(TargetDate7)TargetDate7,Max(Goals8)Goals8,Max(Milestones8)Milestones8,Max(TargetDate8)TargetDate8,"
        SQL5 &= "Max(Goals9)Goals9,Max(Milestones9)Milestones9,Max(TargetDate9)TargetDate9,Max(Goals10)Goals10,Max(Milestones10)Milestones10,Max(TargetDate10)TargetDate10 from(select "
        SQL5 &= "(case when IndexID=1 then Rtrim(Ltrim(Goals)) else '' end)Goals1,(case when IndexID=1 then Rtrim(Ltrim(Milestones)) else '' end)Milestones1,(case when IndexID=1 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate1,"
        SQL5 &= "(case when IndexID=2 then Rtrim(Ltrim(Goals)) else '' end)Goals2,(case when IndexID=2 then Rtrim(Ltrim(Milestones)) else '' end)Milestones2,(case when IndexID=2 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate2,"
        SQL5 &= "(case when IndexID=3 then Rtrim(Ltrim(Goals)) else '' end)Goals3,(case when IndexID=3 then Rtrim(Ltrim(Milestones)) else '' end)Milestones3,(case when IndexID=3 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate3,"
        SQL5 &= "(case when IndexID=4 then Rtrim(Ltrim(Goals)) else '' end)Goals4,(case when IndexID=4 then Rtrim(Ltrim(Milestones)) else '' end)Milestones4,(case when IndexID=4 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate4,"
        SQL5 &= "(case when IndexID=5 then Rtrim(Ltrim(Goals)) else '' end)Goals5,(case when IndexID=5 then Rtrim(Ltrim(Milestones)) else '' end)Milestones5,(case when IndexID=5 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate5,"
        SQL5 &= "(case when IndexID=6 then Rtrim(Ltrim(Goals)) else '' end)Goals6,(case when IndexID=6 then Rtrim(Ltrim(Milestones)) else '' end)Milestones6,(case when IndexID=6 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate6,"
        SQL5 &= "(case when IndexID=7 then Rtrim(Ltrim(Goals)) else '' end)Goals7,(case when IndexID=7 then Rtrim(Ltrim(Milestones)) else '' end)Milestones7,(case when IndexID=7 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate7,"
        SQL5 &= "(case when IndexID=8 then Rtrim(Ltrim(Goals)) else '' end)Goals8,(case when IndexID=8 then Rtrim(Ltrim(Milestones)) else '' end)Milestones8,(case when IndexID=8 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate8,"
        SQL5 &= "(case when IndexID=9 then Rtrim(Ltrim(Goals)) else '' end)Goals9,(case when IndexID=9 then Rtrim(Ltrim(Milestones)) else '' end)Milestones9,(case when IndexID=9 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate9,"
        SQL5 &= "(case when IndexID=10 then Rtrim(Ltrim(Goals)) else '' end)Goals10,(case when IndexID=10 then Rtrim(Ltrim(Milestones)) else '' end)Milestones10,(case when IndexID=10 then Rtrim(Ltrim(TargetDate)) else '' end)TargetDate10"
        SQL5 &= " from ME_Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A "
        DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
        'Response.Write(SQL5) : Response.End()

        '--Dispaly data from Future Goals table for Edit
        FUT_Goal_Edit1.Text = DT5.Rows(0)("Goals1").ToString : FUT_Succ_Edit1.Text = DT5.Rows(0)("Milestones1").ToString : FUT_Date_Edit1.Text = DT5.Rows(0)("TargetDate1").ToString
        FUT_Goal_Edit2.Text = DT5.Rows(0)("Goals2").ToString : FUT_Succ_Edit2.Text = DT5.Rows(0)("Milestones2").ToString : FUT_Date_Edit2.Text = DT5.Rows(0)("TargetDate2").ToString
        FUT_Goal_Edit3.Text = DT5.Rows(0)("Goals3").ToString : FUT_Succ_Edit3.Text = DT5.Rows(0)("Milestones3").ToString : FUT_Date_Edit3.Text = DT5.Rows(0)("TargetDate3").ToString
        FUT_Goal_Edit4.Text = DT5.Rows(0)("Goals4").ToString : FUT_Succ_Edit4.Text = DT5.Rows(0)("Milestones4").ToString : FUT_Date_Edit4.Text = DT5.Rows(0)("TargetDate4").ToString
        FUT_Goal_Edit5.Text = DT5.Rows(0)("Goals5").ToString : FUT_Succ_Edit5.Text = DT5.Rows(0)("Milestones5").ToString : FUT_Date_Edit5.Text = DT5.Rows(0)("TargetDate5").ToString
        FUT_Goal_Edit6.Text = DT5.Rows(0)("Goals6").ToString : FUT_Succ_Edit6.Text = DT5.Rows(0)("Milestones6").ToString : FUT_Date_Edit6.Text = DT5.Rows(0)("TargetDate6").ToString
        FUT_Goal_Edit7.Text = DT5.Rows(0)("Goals7").ToString : FUT_Succ_Edit7.Text = DT5.Rows(0)("Milestones7").ToString : FUT_Date_Edit7.Text = DT5.Rows(0)("TargetDate7").ToString
        FUT_Goal_Edit8.Text = DT5.Rows(0)("Goals8").ToString : FUT_Succ_Edit8.Text = DT5.Rows(0)("Milestones8").ToString : FUT_Date_Edit8.Text = DT5.Rows(0)("TargetDate8").ToString
        FUT_Goal_Edit9.Text = DT5.Rows(0)("Goals9").ToString : FUT_Succ_Edit9.Text = DT5.Rows(0)("Milestones9").ToString : FUT_Date_Edit9.Text = DT5.Rows(0)("TargetDate9").ToString
        FUT_Goal_Edit10.Text = DT5.Rows(0)("Goals10").ToString : FUT_Succ_Edit10.Text = DT5.Rows(0)("Milestones10").ToString : FUT_Date_Edit10.Text = DT5.Rows(0)("TargetDate10").ToString

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub SaveFutureGoal_Click(sender As Object, e As EventArgs) Handles SaveFutureGoal.Click
        SaveFutureData()
        'Send email to employee
        Msg = lblEMPLOYEE_NAME.Text & "'s Performance Appraisal has been updated by " & lblFirst_SUP_NAME.Text & "<br><br>"
        Msg &= "Please click on the link http://crnet/ME_Performance_Appraisal/Default.aspx for full details."
        '--Production Email

        'LocalClass.SendMail(lblEMPLOYEE_EMAIL.Text, "Future Goal has been added", Msg)

        '("EMargalit@consumer.org", "Future Goal in your Appraisal has been updated", Msg)

    End Sub
End Class