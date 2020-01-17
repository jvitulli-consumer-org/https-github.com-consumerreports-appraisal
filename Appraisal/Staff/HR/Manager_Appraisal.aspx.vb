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
Public Class HR_Appraisal_Review
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
    Dim Msg, Subj, x, x1 As String
    Dim EMPLID As String


    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer
    Dim KeyTaskRows1, KeyTaskRows2, KeyTaskRows3, KeyTaskRows4, KeyTaskRows5, KeyTaskRows6, KeyTaskRows7, KeyTaskRows8, KeyTaskRows9, KeyTaskRows10, KeyTaskRows11, KeyTaskRows12, KeyTaskRows13, KeyTaskRows14 As Integer

    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Len(Session("YEAR")) = 0 Then Response.Redirect("../../Default.aspx")
        If Session("YEAR") > 2015 Then Response.Redirect("Manager_Appraisal1.aspx")

        lblEMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), _
                         "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")
        lblYEAR.Text = Session("YEAR")

        SetLevel_Approval()
        DisplayData()
        ShowHideGoalPanel()
        ShowHideFutureGoalPanel()
        DiscussionComments()


    End Sub

    Protected Sub SetLevel_Approval()
        '--Employee information---
        SQL1 = "/*3*/select (case when New_Employee=1 then 'SHORT' else 'FULL' end)Elig_Review,(select First+' '+last from id_tbl where emplid=cc.emplid)EMPLOYEE_NAME,(select email from id_tbl where emplid=cc.emplid) EMPLOYEE_EMAIL,"
        SQL1 &= " (select jobtitle1 from id_tbl where emplid=cc.emplid)JOBTITLE,(select deptid1 from id_tbl where emplid=cc.emplid)DEPTID,(select departname from id_tbl where emplid=cc.emplid)DEPTNAME,(select convert(char(10),hire_date,101) from id_tbl "
        SQL1 &= " where emplid=cc.emplid)Hired,Left(convert(decimal,datediff(day,(select convert(char(10),hire_date,101) from id_tbl where emplid=cc.emplid),GetDate()))/365.25,5)YearsCU,Perf_Year CY,Perf_Year-1 PY,Perf_Year+1 NY,BEN_ID,0 EMPL_Supervision,"
        SQL1 &= " SUP_EMPLID FIRST_SUP_EMPLID,(select first+' '+last from id_tbl where emplid=cc.SUP_EMPLID)FIRST_SUP_NAME,(select jobtitle1 from id_tbl where emplid=cc.emplid)FIRST_SUP_EMAIL,(select emplid from id_tbl where emplid=cc.up_mgt_emplid)"
        SQL1 &= " UP_MGT_ID,(select first+' '+last from id_tbl where emplid=cc.up_mgt_emplid)UP_MGT_NAME,(select email from id_tbl where emplid=cc.up_mgt_emplid)UP_MGT_EMAIL,(select emplid from id_tbl where emplid=cc.hr_emplid)HR_Generalist,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=cc.hr_emplid)HR_Generalist_Name,(select email from id_tbl where emplid=cc.hr_emplid)HR_Generalist_Email,CC.* from(/*2*/select EMPLID,EMPLOYEE_NAME,SAP,EMPLOYEE_EMAIL,JOBTITLE,DEPTID,"
        SQL1 &= " DEPTNAME, Hired, EMPL_Supervision, YearsCU, BEN_ID,(case when EMPLID=6193 then 6193 else FIRST_SUP_EMPLID end)FIRST_SUP_EMPLID,(case when EMPLID=6193 then 'Marta Tellado' else FIRST_SUP_NAME end)FIRST_SUP_NAME, (case when EMPLID=6193 "
        SQL1 &= " then 'Marta.Tellado@consumer.org' else FIRST_SUP_EMAIL end)FIRST_SUP_EMAIL,(case when EMPLID=6193 or FIRST_SUP_EMPLID=6193 then 6193 when FIRST_SUP_EMPLID not in(6193) and UP_MGT_ID=6193 then FIRST_SUP_EMPLID else UP_MGT_ID end)UP_MGT_ID,"
        SQL1 &= " (case when FIRST_SUP_EMPLID=6193 or EMPLID=6193 then 'Marta Tellado' when FIRST_SUP_EMPLID not in(6193) and UP_MGT_ID=6193 then FIRST_SUP_NAME else UP_MGT_NAME end)UP_MGT_NAME, (case when FIRST_SUP_EMPLID=6193 or EMPLID=6193 then "
        SQL1 &= " 'Marta.Tellado@consumer.org' when FIRST_SUP_EMPLID not in(6193) and UP_MGT_ID=6193 then FIRST_SUP_EMAIL else UP_MGT_EMAIL end) UP_MGT_Email,(case when FIRST_SUP_EMPLID=6193 or EMPLID=6193 then 6785 else HR_Generalist end)HR_Generalist,"
        SQL1 &= " (case when FIRST_SUP_EMPLID=6193 or EMPLID=6193 then 'Lisa Cribari' else HR_Generalist_Name end) HR_Generalist_Name,(case when FIRST_SUP_EMPLID=6193 or EMPLID=6193 then 'Rafael.Perez@consumer.org' else HR_Generalist_Email end)"
        SQL1 &= " HR_Generalist_Email from(/*1*/select EMPLID,(FIRST_NAME+' '+LAST_NAME)EMPLOYEE_NAME,SAL_ADMIN_PLAN SAP, EMAIL EMPLOYEE_EMAIL,JOBTITLE,DEPTID,DEPTNAME,Hire_date Hired, (case when A.emplid in (5529,1241,6129,6235,1189,5315,6057,6203,1683,"
        SQL1 &= " 6094,1167) then 1 else (select count(*) from ps_employees where supervisor_id=A.emplid) end) EMPL_Supervision ,Left(convert(decimal,datediff(day,Hire_date,GetDate()))/365.25,5)YearsCU,BEN_ID,SUPERVISOR_EMPLID FIRST_SUP_EMPLID,(select "
        SQL1 &= " First_Name+' '+Last_Name from HR_PDS_DATA_tbl where emplid=A.SUPERVISOR_EMPLID)FIRST_SUP_NAME,(select Email from HR_PDS_DATA_tbl where emplid=A.SUPERVISOR_EMPLID) FIRST_SUP_EMAIL,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where "
        SQL1 &= " EMPLID=A.SUPERVISOR_EMPLID)UP_MGT_ID,(select First_Name+' '+Last_Name from HR_PDS_DATA_tbl where EMPLID in (select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where EMPLID=A.SUPERVISOR_EMPLID))UP_MGT_NAME,(select Email from HR_PDS_DATA_tbl "
        SQL1 &= " where EMPLID in (select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where EMPLID=A.SUPERVISOR_EMPLID))UP_MGT_Email, /*Generalist*/HR_Generalist,(select First_Name+' '+Last_Name from HR_PDS_DATA_tbl where emplid=A.HR_Generalist)"
        SQL1 &= " HR_Generalist_Name,(select Email from HR_PDS_DATA_tbl where emplid=A.HR_Generalist)HR_Generalist_Email from HR_PDS_DATA_tbl A /*1END*/)AA)BB RIGHT JOIN (select * from ME_Appraisal_Master_tbl)CC ON CC.emplid=BB.emplid "
        SQL1 &= " where CC.emplid = " & lblEMPLID.Text & " And Perf_Year = " & lblYEAR.Text & ""
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)


        '--Manager Information--
        lblEMPLOYEE_NAME.Text = DT1.Rows(0)("EMPLOYEE_NAME").ToString
        lblEMPLOYEE_TITLE.Text = DT1.Rows(0)("jobtitle").ToString
        lblEMPLOYEE_DEPT.Text = DT1.Rows(0)("Deptname").ToString
        lblEMPLOYEE_HIRE.Text = DT1.Rows(0)("Hired").ToString
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
        LblMgr_NAME.Text = DT1.Rows(0)("First_Sup_Name").ToString
        '--Second Supervisor--
        lblUP_MGT_EMAIL.Text = DT1.Rows(0)("UP_MGT_EMAIL").ToString
        lblUP_MGT_NAME.Text = DT1.Rows(0)("UP_MGT_NAME").ToString
        If DT1.Rows(0)("SUP_EMPLID").ToString = DT1.Rows(0)("UP_MGT_ID").ToString Then
            lblMGR_UP_NAME.Text = "n/a"
        Else
            lblMGR_UP_NAME.Text = DT1.Rows(0)("UP_MGT_NAME").ToString
        End If
        LblEligible_Full_Review.Text = DT1.Rows(0)("Elig_Review").ToString
        LblDEPTID.Text = DT1.Rows(0)("deptid").ToString
        '--Generalist--
        lblGENERALIST_NAME.Text = DT1.Rows(0)("HR_Generalist_Name").ToString
        LblSTATUS.Text = DT1.Rows(0)("Process_Flag").ToString
        '---Employee Information--
        lblEMPL_Supervision.Text = DT1.Rows(0)("EMPL_Supervision").ToString


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

        '--Approval Date--
        If CDbl(DT1.Rows(0)("Process_Flag").ToString) = 1 Then
            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Sent").ToString
            LblSec_Mgr_Appr.Text = "Waiting Approval"
            LblSec_Mgr_Appr.ForeColor = Drawing.Color.Blue
            LblSec_Mgr_Appr.Font.Bold = True

        ElseIf CDbl(DT1.Rows(0)("Process_Flag").ToString) = 2 Then
            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Sent").ToString

            If DT1.Rows(0)("SUP_EMPLID").ToString = DT1.Rows(0)("UP_MGT_ID").ToString Then
                LblSec_Mgr_Appr.Text = "n/a"
            Else
                LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
            End If

            LblHR_Appr.Text = "Waiting Approval"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True
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
        ElseIf CDbl(DT1.Rows(0)("Process_Flag").ToString) = 5 Then
            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Sent").ToString

            If DT1.Rows(0)("SUP_EMPLID").ToString = DT1.Rows(0)("UP_MGT_ID").ToString Then
                LblSec_Mgr_Appr.Text = "n/a"
            Else
                LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
            End If

            LblHR_Appr.Text = DT1.Rows(0)("Date_HR_Approved").ToString
            LblEMP_Appr.Text = DT1.Rows(0)("Date_Employee_Esign").ToString
        End If

        LocalClass.CloseSQLServerConnection()

    End Sub


    Protected Sub DiscussionComments()
        SQL = "select top 1 *,(select First+' '+Last+''''+'s' from id_tbl where emplid=Rej_Emplid)Rej_Name from ME_Appraisal_Discussion_tbl where MGR_EMPLID=" & LblFirst_SUP_EMPLID.Text & " "
        SQL &= " and emplid=" & lblEMPLID.Text & " and Perf_Year=" & Session("YEAR") & " order by Datetime desc"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        If DT.Rows.Count > 0 Then
            If CDbl(LblFirst_SUP_EMPLID.Text) = CDbl(DT.Rows(0)("REJ_EMPLID").ToString) Then
            ElseIf Len(DT.Rows(0)("Comments").ToString) > 0 Then
                Discuss.Text = "<font color=black><b>" & DT.Rows(0)("Rej_Name").ToString & " Discussion Comments:</b></font>   " & Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<p>")
                Discuss.ForeColor = Drawing.Color.Blue
                Discuss.BorderStyle = BorderStyle.Ridge
                Discuss.BorderColor = Drawing.Color.LightGray
                Discuss.BorderWidth = 1
            End If
        End If


    End Sub


    Protected Sub ShowHideGoalPanel()
        'DisplayData()
        SQL6 = "select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5 from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then"
        SQL6 &= " count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,(case when IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5"
        SQL6 &= " from ME_Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " group by IndexID)A"
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        'If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_Goal2.Visible = True Else Panel_Goal2.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_Goal3.Visible = True Else Panel_Goal3.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_Goal4.Visible = True Else Panel_Goal4.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_Goal5.Visible = True Else Panel_Goal5.Visible = False
        LocalClass.CloseSQLServerConnection()

        DisplayData()
    End Sub
    Protected Sub ShowHideFutureGoalPanel()
        'DisplayData()
        SQL6 = "select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10"
        SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,(case when"
        SQL6 &= " IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*)"
        SQL6 &= " else 0 end)F_IND7,(case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
        SQL6 &= " from ME_Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " group by IndexID)A"
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_FutureGoal2.Visible = True Else Panel_FutureGoal2.Visible = False
        If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_FutureGoal3.Visible = True Else Panel_FutureGoal3.Visible = False
        If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_FutureGoal4.Visible = True Else Panel_FutureGoal4.Visible = False
        If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_FutureGoal5.Visible = True Else Panel_FutureGoal5.Visible = False
        If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel_FutureGoal6.Visible = True Else Panel_FutureGoal6.Visible = False
        If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel_FutureGoal7.Visible = True Else Panel_FutureGoal7.Visible = False
        If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel_FutureGoal8.Visible = True Else Panel_FutureGoal8.Visible = False
        If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel_FutureGoal9.Visible = True Else Panel_FutureGoal9.Visible = False
        If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel_FutureGoal10.Visible = True Else Panel_FutureGoal10.Visible = False

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
        'Goals_2.Text = Replace(Replace(DT.Rows(0)("Accomp2").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        'Goals_3.Text = Replace(Replace(DT.Rows(0)("Accomp3").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        'Goals_4.Text = Replace(Replace(DT.Rows(0)("Accomp4").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        'Goals_5.Text = Replace(Replace(DT.Rows(0)("Accomp5").ToString, Chr(13), "<p>"), Chr(10), "<p>")

        '--Dispaly data from Future Goals table
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

   
End Class