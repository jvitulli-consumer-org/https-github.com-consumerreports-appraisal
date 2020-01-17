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
Public Class Guild_FutureGoal
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

        lblEMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")

        If Session("NETID") = "" Then Response.Redirect("..\..\default.aspx")

        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 1080)) + 20)

        If Session("NETID") = "" Then Response.Redirect("..\..\default.aspx")


        SetLevel_Approval()

        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display") ': Response.End()
            SaveData()
            DisplayData()
        Else
            'Response.Write("<br>2. else Save Data before Display") ': Response.End()
            'DisplayData()
        End If
        ShowHideFutureGoalPanel()
        ShowHideFutureTaskPanel()
        DisplayData()
        Goals_Log()

        '--Process Flag---
        '  0--First Manager 
        '  1--Second Manager
        '  2--HR Generalist
        '  3--First Manager - Reviewed by HR
        '  4--Guild Waiting for review
        '  5--Guild Reviewed and confirmed

        '-------TABLES USED THIS FORM-------------
        '1. Guild_Appraisal_FUTUREGOAL_MASTER_tbl
        '2. GUILD_Appraisal_FUTUREGOAL_TBL
        '3. GUILD_Appraisal_FUTURETASK_TBL
        '4. GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL

        'CHR(10) -- Line feed        replace to <br>
        'CHR(13) -- Carriage return  replace to <span>      
        Sub_Empl.Visible = False
    End Sub

    Protected Sub SetLevel_Approval()
        SQL1 = "select *,(select first+' '+last from id_tbl where emplid=a.emplid)employee_name,(select first+' '+last from id_tbl where emplid=a.sup_emplid)sup_name,"
        SQL1 &= " IsNull((select first+' '+last from id_tbl where emplid=a.up_mgt_emplid),'N/A')up_mgt_name,(select first+' '+last from id_tbl where emplid=a.hr_emplid)hr_name,"
        SQL1 &= " (select convert(char(10),hire_date,101) from id_tbl where emplid=a.emplid)hire_date,(select email from id_tbl where emplid=a.sup_emplid)sup_email,"
        SQL1 &= " (select email from id_tbl where emplid=a.emplid)empl_email,(select email from id_tbl where emplid=a.up_mgt_emplid)up_mgt_email,(select email from id_tbl where emplid=a.hr_emplid)hr_email"
        SQL1 &= " from Guild_Appraisal_FUTUREGOAL_MASTER_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year=2016"
        'Response.Write(SQL1) ': Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()
        lblEMPLOYEE_NAME.Text = DT1.Rows(0)("EMPLOYEE_NAME").ToString
        lblEMPLOYEE_TITLE.Text = DT1.Rows(0)("title").ToString
        lblEMPLOYEE_DEPT.Text = DT1.Rows(0)("Department").ToString
        lblEMPLOYEE_HIRE.Text = DT1.Rows(0)("hire_date").ToString
        lblGUILD_EMAIL.Text = DT1.Rows(0)("empl_email").ToString
        lblFlag.Text = DT1.Rows(0)("Process_Flag").ToString
        '--First Supervisor--
        lblFirst_SUP_EMPLID.Text = DT1.Rows(0)("sup_emplid").ToString
        LblMgr_NAME.Text = DT1.Rows(0)("sup_name").ToString
        lblFirst_SUP_EMAIL.Text = DT1.Rows(0)("sup_email").ToString
        '--Second Supervisor--
        lblUP_MGT_EMPLID.Text = DT1.Rows(0)("up_mgt_emplid").ToString
        lblMGR_UP_NAME.Text = DT1.Rows(0)("up_mgt_name").ToString
        lblUP_MGT_EMAIL.Text = DT1.Rows(0)("up_mgt_email").ToString

        '--HR Generalist--
        lblGENERALIST_NAME.Text = DT1.Rows(0)("hr_name").ToString
        lblHR_EMPLID.Text = DT1.Rows(0)("hr_EMPLID").ToString
        lblHR_EMAIL.Text = DT1.Rows(0)("hr_email").ToString

        If Len(Session("EMPLID_LOGON")) = 0 Then lblLogin_EMPLID.Text = Session("MGT_EMPLID") Else lblLogin_EMPLID.Text = Session("EMPLID_LOGON")
        'Response.Write("Employee  " & Session("EMPLID_LOGON") & "<br> Manager " & Session("MGT_EMPLID") & "<br> Login EMPLID " & lblLogin_EMPLID.Text)

        If DateTime.Parse(DT1.Rows(0)("Date_Submitted_to_Guild").ToString) > DateTime.Parse(DT1.Rows(0)("Date_Guild_Reviewed").ToString) Then
            Session("GoalUpdated") = 1
        Else
            Session("GoalUpdated") = 0
        End If
        'Response.Write("GoalUpdated   " & Session("GoalUpdated"))

        If lblFlag.Text = 0 Then
            Goal_Setting_Edit.Visible = True : Goal_Setting_Edit1.Visible = True : Future_Tasks.Visible = True
            Goal_Setting_Review.Visible = False : Goal_Discussion.Visible = False
            BtnSubmit_UpperMgr.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : BtnSubmit_UpperMgr.Attributes.Add("onmouseout", "this.style.color='#000000'")
            btnSave.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 1 Then

            Generalist.Visible = True : Approve.Visible = False
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Future_Tasks.Visible = False : Future_Tasks_Review.Visible = True
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = True

            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_to_up_mgt").ToString
            LblSec_Mgr_Appr.Text = "Waiting Approval"
            LblSec_Mgr_Appr.ForeColor = Drawing.Color.Blue
            Generalist.Text = "Submit for review to " & lblGENERALIST_NAME.Text
            Generalist.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Generalist.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 2 Then
            Generalist.Visible = False : Approve.Visible = True
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Future_Tasks.Visible = False : Future_Tasks_Review.Visible = True
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = True

            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_to_up_mgt").ToString
            LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
            LblHR_Appr.Text = "Waiting Approval"
            LblHR_Appr.ForeColor = Drawing.Color.Blue

            Approve.Text = "Approve"
            Approve.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Approve.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 3 Then
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Future_Tasks.Visible = False : Future_Tasks_Review.Visible = True
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False

            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_to_up_mgt").ToString
            LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
            LblHR_Appr.Text = DT1.Rows(0)("Date_HR_Approved").ToString
            Sub_Empl.Visible = True
            Sub_Empl.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Sub_Empl.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 4 And Session("SAP") <> "GLD" Then
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Future_Tasks.Visible = False : Future_Tasks_Review.Visible = True
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False
            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_to_up_mgt").ToString
            LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
            LblHR_Appr.Text = DT1.Rows(0)("Date_HR_Approved").ToString
            Sub_Empl.Visible = False
            Sub_Empl_Review.Visible = True
            Sub_Empl_Review.Text = "Submitted for Review on " & DT1.Rows(0)("Date_Submitted_To_Guild").ToString


        ElseIf lblFlag.Text = 4 And Session("SAP") = "GLD" Then
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Future_Tasks.Visible = False : Future_Tasks_Review.Visible = True
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False
            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_to_up_mgt").ToString
            LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
            LblHR_Appr.Text = DT1.Rows(0)("Date_HR_Approved").ToString
            Sub_Empl.Visible = False
            Sub_Empl_Review.Visible = True
            Sub_Empl_Review.Text = "Submitted for Review on " & DT1.Rows(0)("Date_Submitted_To_Guild").ToString
            Empl_Review.Visible = False
            Empl_Review.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Empl_Review.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 5 And Session("SAP") = "GLD" Then
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Future_Tasks.Visible = False : Future_Tasks_Review.Visible = True
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False
            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_to_up_mgt").ToString
            LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
            LblHR_Appr.Text = DT1.Rows(0)("Date_HR_Approved").ToString
            Sub_Empl.Visible = False
            Sub_Empl_Review.Visible = True

            If Session("GoalUpdated") = 1 Then
                Empl_Review.Visible = False
            Else
                Empl_Review.Visible = False
                Sub_Empl_Review.Text = "Reviewed and Confirmed on " & DT1.Rows(0)("Date_Guild_Reviewed").ToString
            End If


        ElseIf lblFlag.Text = 5 And Session("SAP") <> "GLD" Then

            LblFirst_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_to_up_mgt").ToString
            LblSec_Mgr_Appr.Text = DT1.Rows(0)("Date_Submitted_To_HR").ToString
            LblHR_Appr.Text = DT1.Rows(0)("Date_HR_Approved").ToString
            Sub_Empl.Visible = False
            Sub_Empl_Review.Visible = True

            'Future_Tasks_Review.Visible = True

            Empl_Review.Visible = False
            BtnSubmit_UpperMgr.Visible = False

            If Session("GoalUpdated") = 1 Then
                Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Future_Tasks.Visible = False
                Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = True : Future_Tasks_Review.Visible = True
                btnSave1.Visible = False
                Sub_Empl_Review.Text = "Waiting Confirmation from " & lblEMPLOYEE_NAME.Text
                Sub_Empl_Review.Font.Size = 16
                Panel_Goals_Log.Visible = False

            Else
                Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Goal_Setting_Edit.Visible = False : Future_Tasks.Visible = False
                Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False : Future_Tasks_Review.Visible = True
                btnSave1.Visible = True
                Sub_Empl_Review.Text = "<br>" & lblEMPLOYEE_NAME.Text & " Reviewed and Confirmed on " & DT1.Rows(0)("Date_Guild_Reviewed").ToString & ". "
                '<br>If changes need to be made to the goals, please make edits on the form, and used the save and submit button above."
            End If
            btnSave1.Text = "Save and Submit for review to " & lblEMPLOYEE_NAME.Text
            btnSave1.Visible = False
            btnSave.Visible = False
        End If

        Discuss.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Discuss.Attributes.Add("onmouseout", "this.style.color='#000000'")
        'Disc_Com.Text = "Send suggested revisions to " & LblMgr_NAME.Text & ""
        Discuss.Text = "Send back to " & LblMgr_NAME.Text & " for revision"

        If (lblLogin_EMPLID.Text = lblFirst_SUP_EMPLID.Text) Then
            Goal_Discussion.Visible = False
        End If

        '--Hide all editable controls
        btnSave1.Visible = False : btnSave.Visible = False : BtnSubmit_UpperMgr.Visible = False : BtnFuture_Task.Enabled = False : BtnFuture_Goal.Enabled = False

        Fut_KeyTask1.Enabled = False : Fut_KeyTask2.Enabled = False : Fut_KeyTask3.Enabled = False : Fut_KeyTask4.Enabled = False : Fut_KeyTask5.Enabled = False
        Fut_KeyTask6.Enabled = False : Fut_KeyTask7.Enabled = False : Fut_KeyTask8.Enabled = False : Fut_KeyTask9.Enabled = False : Fut_KeyTask10.Enabled = False
        Fut_KeyTask11.Enabled = False : Fut_KeyTask12.Enabled = False : Fut_KeyTask13.Enabled = False : Fut_KeyTask14.Enabled = False : Fut_KeyTask15.Enabled = False
        Fut_KeyTask16.Enabled = False : Fut_KeyTask17.Enabled = False : Fut_KeyTask18.Enabled = False : Fut_KeyTask19.Enabled = False : Fut_KeyTask20.Enabled = False
        FUT_Goal_Edit1.Enabled = False : FUT_Succ_Edit1.Enabled = False : FUT_Date_Edit1.Enabled = False
        FUT_Goal_Edit2.Enabled = False : FUT_Succ_Edit2.Enabled = False : FUT_Date_Edit2.Enabled = False
        FUT_Goal_Edit3.Enabled = False : FUT_Succ_Edit3.Enabled = False : FUT_Date_Edit3.Enabled = False
        FUT_Goal_Edit4.Enabled = False : FUT_Succ_Edit4.Enabled = False : FUT_Date_Edit4.Enabled = False
        FUT_Goal_Edit5.Enabled = False : FUT_Succ_Edit5.Enabled = False : FUT_Date_Edit5.Enabled = False
        FUT_Goal_Edit6.Enabled = False : FUT_Succ_Edit6.Enabled = False : FUT_Date_Edit6.Enabled = False
        FUT_Goal_Edit7.Enabled = False : FUT_Succ_Edit7.Enabled = False : FUT_Date_Edit7.Enabled = False
        FUT_Goal_Edit8.Enabled = False : FUT_Succ_Edit8.Enabled = False : FUT_Date_Edit8.Enabled = False
        FUT_Goal_Edit9.Enabled = False : FUT_Succ_Edit9.Enabled = False : FUT_Date_Edit9.Enabled = False
        FUT_Goal_Edit10.Enabled = False : FUT_Succ_Edit10.Enabled = False : FUT_Date_Edit10.Enabled = False


        BtnSubmit_UpperMgr.Visible = False : Generalist.Visible = False : btnSave1.Visible = False : btnSave.Visible = False : Discuss.Visible = False : Approve.Visible = False
        DiscussionComments.Visible = False


    End Sub

    Protected Sub ShowHideFutureGoalPanel()
        'DisplayData()
        SQL6 = "select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10"
        SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,(case when"
        SQL6 &= " IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*)"
        SQL6 &= " else 0 end)F_IND7,(case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
        SQL6 &= " from GUILD_Appraisal_FUTUREGOAL_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=2016 group by IndexID)A"
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_FutureGoal_Edit2.Visible = True Else Panel_FutureGoal_Edit2.Visible = False
        If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_FutureGoal_Edit3.Visible = True Else Panel_FutureGoal_Edit3.Visible = False
        If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_FutureGoal_Edit4.Visible = True Else Panel_FutureGoal_Edit4.Visible = False
        If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_FutureGoal_Edit5.Visible = True Else Panel_FutureGoal_Edit5.Visible = False
        If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel_FutureGoal_Edit6.Visible = True Else Panel_FutureGoal_Edit6.Visible = False
        If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel_FutureGoal_Edit7.Visible = True Else Panel_FutureGoal_Edit7.Visible = False
        If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel_FutureGoal_Edit8.Visible = True Else Panel_FutureGoal_Edit8.Visible = False
        If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel_FutureGoal_Edit9.Visible = True Else Panel_FutureGoal_Edit9.Visible = False
        If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel_FutureGoal_Edit10.Visible = True Else Panel_FutureGoal_Edit10.Visible = False

        LocalClass.CloseSQLServerConnection()
        DisplayData()
    End Sub

    Protected Sub BtnFuture_Goal_Click(sender As Object, e As EventArgs) Handles BtnFuture_Goal.Click
        SQL = "select count(*)CNT_FUT from GUILD_Appraisal_FUTUREGOAL_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        'EMPLID,Perf_Year,Future_Year,IndexID,Goals,Milestones,TargetDate

        If CDbl(DT.Rows(0)("CNT_FUT").ToString) = 0 Then
            SQL3 = " insert into GUILD_Appraisal_FUTUREGOAL_TBL (EMPLID,Perf_Year,IndexID,Goals,Milestones,TargetDate) values(" & lblEMPLID.Text & ",'2016',1,'','','')"
            'Response.Write("Zero " & SQL3) : Response.End()
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()
        Else
            SQL2 = "select Max(IndexId)+1 NewIndexID from GUILD_Appraisal_FUTUREGOAL_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
            SQL4 &= " insert into GUILD_Appraisal_FUTUREGOAL_TBL (EMPLID,Perf_Year,IndexID,Goals,Milestones,TargetDate) values(" & lblEMPLID.Text & ", '2016'," & DT2.Rows(0)("NewIndexID").ToString & ",'','','')"
            'Response.Write(SQL2 & "<br>" & SQL4) : Response.End()
            DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
            LocalClass.CloseSQLServerConnection()

            SQL3 = "select Max(IndexId) NextIndexID from GUILD_Appraisal_FUTUREGOAL_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()

            If CDbl(DT3.Rows(0)("NextIndexID").ToString) > 10 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('A maximum of 10 task has been exceeded.'); </script>")
                SQL2 &= "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID>10"
                DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
                LocalClass.CloseSQLServerConnection()
            Else
                ShowHideFutureGoalPanel()

            End If
        End If

    End Sub

    Protected Sub Delete_FutureGoal2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal2.Click
        SQL = "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID=2 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTUREGOAL_TBL Set IndexID=IndexID-1 where IndexID>2 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal3.Click
        SQL = "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID=3 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTUREGOAL_TBL Set IndexID=IndexID-1 where IndexID>3 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal4.Click
        SQL = "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID=4 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTUREGOAL_TBL Set IndexID=IndexID-1 where IndexID>4 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal5.Click
        SQL = "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID=5 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTUREGOAL_TBL Set IndexID=IndexID-1 where IndexID>5 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal6.Click
        SQL = "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID=6 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTUREGOAL_TBL Set IndexID=IndexID-1 where IndexID>6 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal7.Click
        SQL = "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID=7 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTUREGOAL_TBL Set IndexID=IndexID-1 where IndexID>7 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal8.Click
        SQL = "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID=8 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTUREGOAL_TBL Set IndexID=IndexID-1 where IndexID>8 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal9.Click
        SQL = "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID=9 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTUREGOAL_TBL Set IndexID=IndexID-1 where IndexID>9 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub
    Protected Sub Delete_FutureGoal10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Delete_FutureGoal10.Click
        SQL = "delete GUILD_Appraisal_FUTUREGOAL_TBL where IndexID=10 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTUREGOAL_TBL Set IndexID=IndexID-1 where IndexID>10 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureGoalPanel()
    End Sub

    Protected Sub BtnSubmit_UpperMgr_Click(sender As Object, e As EventArgs) Handles BtnSubmit_UpperMgr.Click
        SaveData()
        AllRules()
    End Sub

    Protected Sub AllRules()

        Dim Flag As Integer = 0

        SQL = " select * from (select * from"
        SQL &= "(select count(*)Fut1 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=1)F,"
        SQL &= "(select count(*)Fut2 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=2)G,"
        SQL &= "(select count(*)Fut3 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=3)H,"
        SQL &= "(select count(*)Fut4 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=4)J,"
        SQL &= "(select count(*)Fut5 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=5)K,"
        SQL &= "(select count(*)Fut6 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=6)K2,"
        SQL &= "(select count(*)Fut7 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=7)K3,"
        SQL &= "(select count(*)Fut8 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=8)K4,"
        SQL &= "(select count(*)Fut9 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=9)K5,"
        SQL &= "(select count(*)Fut10 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=10)K6)AA,"
        SQL &= "(select * from "
        SQL &= "(select count(*)FutTask1 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=1)A1,"
        SQL &= "(select count(*)FutTask2 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=2)A2,"
        SQL &= "(select count(*)FutTask3 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=3)A3,"
        SQL &= "(select count(*)FutTask4 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=4)A4,"
        SQL &= "(select count(*)FutTask5 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=5)A5,"
        SQL &= "(select count(*)FutTask6 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=6)A6,"
        SQL &= "(select count(*)FutTask7 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=7)A7,"
        SQL &= "(select count(*)FutTask8 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=8)A8,"
        SQL &= "(select count(*)FutTask9 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=9)A9,"
        SQL &= "(select count(*)FutTask10 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=10)A10,"
        SQL &= "(select count(*)FutTask11 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=11)A11,"
        SQL &= "(select count(*)FutTask12 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=12)A12,"
        SQL &= "(select count(*)FutTask13 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=13)A13,"
        SQL &= "(select count(*)FutTask14 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=14)A14,"
        SQL &= "(select count(*)FutTask15 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=15)A15,"
        SQL &= "(select count(*)FutTask16 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=16)A16,"
        SQL &= "(select count(*)FutTask17 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=17)A17,"
        SQL &= "(select count(*)FutTask18 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=18)A18,"
        SQL &= "(select count(*)FutTask19 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=19)A19,"
        SQL &= "(select count(*)FutTask20 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=20)A20)BB"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '---1. check FUTURE GOALS table Index=1---
        If CDbl(DT.Rows(0)("Fut1").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals1,len(Milestones)Milestones1,len(TargetDate)TargetDate1 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals1").ToString) < 2 Then
                FUT_Goal_Edit1.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit1.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones1").ToString) < 2 Then
                FUT_Succ_Edit1.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit1.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate1").ToString) < 2 Then
                FUT_Date_Edit1.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit1.BackColor = Drawing.Color.White
            End If
        End If
        '---2. check FUTURE GOALS table Index=2---
        If CDbl(DT.Rows(0)("Fut2").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals2,len(Milestones)Milestones2,len(TargetDate)TargetDate2 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals2").ToString) < 2 Then
                FUT_Goal_Edit2.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit2.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones2").ToString) < 2 Then
                FUT_Succ_Edit2.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit2.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate2").ToString) < 2 Then
                FUT_Date_Edit2.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit2.BackColor = Drawing.Color.White
            End If
        End If
        '---3. check FUTURE GOALS table Index=3---
        If CDbl(DT.Rows(0)("Fut3").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals3,len(Milestones)Milestones3,len(TargetDate)TargetDate3 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals3").ToString) < 2 Then
                FUT_Goal_Edit3.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit3.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones3").ToString) < 2 Then
                FUT_Succ_Edit3.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit3.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate3").ToString) < 2 Then
                FUT_Date_Edit3.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit3.BackColor = Drawing.Color.White
            End If
        End If
        '---4. check FUTURE GOALS table Index=4---
        If CDbl(DT.Rows(0)("Fut4").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals4,len(Milestones)Milestones4,len(TargetDate)TargetDate4 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals4").ToString) < 2 Then
                FUT_Goal_Edit4.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit4.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones4").ToString) < 2 Then
                FUT_Succ_Edit4.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit4.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate4").ToString) < 2 Then
                FUT_Date_Edit4.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit4.BackColor = Drawing.Color.White
            End If
        End If
        '---5. check FUTURE GOALS table Index=5---
        If CDbl(DT.Rows(0)("Fut5").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals5,len(Milestones)Milestones5,len(TargetDate)TargetDate5 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals5").ToString) < 2 Then
                FUT_Goal_Edit5.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit5.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones5").ToString) < 2 Then
                FUT_Succ_Edit5.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit5.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate5").ToString) < 2 Then
                FUT_Date_Edit5.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit5.BackColor = Drawing.Color.White
            End If
        End If
        '---6. check FUTURE GOALS table Index=6---
        If CDbl(DT.Rows(0)("Fut6").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals6,len(Milestones)Milestones6,len(TargetDate)TargetDate6 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals6").ToString) < 2 Then
                FUT_Goal_Edit6.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit6.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones6").ToString) < 2 Then
                FUT_Succ_Edit6.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit6.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate6").ToString) < 2 Then
                FUT_Date_Edit6.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit6.BackColor = Drawing.Color.White
            End If
        End If
        '---7. check FUTURE GOALS table Index=7---
        If CDbl(DT.Rows(0)("Fut7").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals7,len(Milestones)Milestones7,len(TargetDate)TargetDate7 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals7").ToString) < 2 Then
                FUT_Goal_Edit7.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit7.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones7").ToString) < 2 Then
                FUT_Succ_Edit7.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit7.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate7").ToString) < 2 Then
                FUT_Date_Edit7.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit7.BackColor = Drawing.Color.White
            End If
        End If
        '---8. check FUTURE GOALS table Index=8---
        If CDbl(DT.Rows(0)("Fut8").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals8,len(Milestones)Milestones8,len(TargetDate)TargetDate8 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals8").ToString) < 2 Then
                FUT_Goal_Edit8.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit8.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones8").ToString) < 2 Then
                FUT_Succ_Edit8.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit8.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate8").ToString) < 2 Then
                FUT_Date_Edit8.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit8.BackColor = Drawing.Color.White
            End If
        End If
        '---9. check FUTURE GOALS table Index=9---
        If CDbl(DT.Rows(0)("Fut9").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals9,len(Milestones)Milestones9,len(TargetDate)TargetDate9 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals9").ToString) < 2 Then
                FUT_Goal_Edit9.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit9.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones9").ToString) < 2 Then
                FUT_Succ_Edit9.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit9.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate9").ToString) < 2 Then
                FUT_Date_Edit9.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit9.BackColor = Drawing.Color.White
            End If
        End If
        '---10. check FUTURE GOALS table Index=10---
        If CDbl(DT.Rows(0)("Fut10").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals10,len(Milestones)Milestones10,len(TargetDate)TargetDate10 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=10"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals10").ToString) < 2 Then
                FUT_Goal_Edit10.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit10.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones10").ToString) < 2 Then
                FUT_Succ_Edit10.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit10.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate10").ToString) < 2 Then
                FUT_Date_Edit10.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit10.BackColor = Drawing.Color.White
            End If
        End If

        '---1. check FUTURE Task Index=1---
        If CDbl(DT.Rows(0)("FutTask1").ToString) = 1 Then
            SQL5 = "select len(Task)Task1 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task1").ToString) < 2 Then
                Fut_KeyTask1.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask1.BackColor = Drawing.Color.White
            End If
        End If

        '---2. check FUTURE Task Index=2---
        If CDbl(DT.Rows(0)("FutTask2").ToString) = 1 Then
            SQL5 = "select len(Task)Task2 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task2").ToString) < 2 Then
                Fut_KeyTask2.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask2.BackColor = Drawing.Color.White
            End If
        End If

        '---3. check FUTURE Task Index=3---
        If CDbl(DT.Rows(0)("FutTask3").ToString) = 1 Then
            SQL5 = "select len(Task)Task3 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task3").ToString) < 2 Then
                Fut_KeyTask3.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask3.BackColor = Drawing.Color.White
            End If
        End If

        '---4. check FUTURE Task Index=4---
        If CDbl(DT.Rows(0)("FutTask4").ToString) = 1 Then
            SQL5 = "select len(Task)Task4 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task4").ToString) < 2 Then
                Fut_KeyTask4.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask4.BackColor = Drawing.Color.White
            End If
        End If

        '---5. check FUTURE Task Index=5---
        If CDbl(DT.Rows(0)("FutTask5").ToString) = 1 Then
            SQL5 = "select len(Task)Task5 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task5").ToString) < 2 Then
                Fut_KeyTask5.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask5.BackColor = Drawing.Color.White
            End If
        End If

        '---6. check FUTURE Task Index=6---
        If CDbl(DT.Rows(0)("FutTask6").ToString) = 1 Then
            SQL5 = "select len(Task)Task6 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task6").ToString) < 2 Then
                Fut_KeyTask6.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask6.BackColor = Drawing.Color.White
            End If
        End If

        '---7. check FUTURE Task Index=7---
        If CDbl(DT.Rows(0)("FutTask7").ToString) = 1 Then
            SQL5 = "select len(Task)Task7 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task7").ToString) < 2 Then
                Fut_KeyTask7.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask7.BackColor = Drawing.Color.White
            End If
        End If

        '---8. check FUTURE Task Index=8---
        If CDbl(DT.Rows(0)("FutTask8").ToString) = 1 Then
            SQL5 = "select len(Task)Task8 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task8").ToString) < 2 Then
                Fut_KeyTask8.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask8.BackColor = Drawing.Color.White
            End If
        End If

        '---9. check FUTURE Task Index=9---
        If CDbl(DT.Rows(0)("FutTask9").ToString) = 1 Then
            SQL5 = "select len(Task)Task9 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task9").ToString) < 2 Then
                Fut_KeyTask9.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask9.BackColor = Drawing.Color.White
            End If
        End If

        '---10. check FUTURE Task Index=10---
        If CDbl(DT.Rows(0)("FutTask10").ToString) = 1 Then
            SQL5 = "select len(Task)Task10 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=10"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task10").ToString) < 2 Then
                Fut_KeyTask10.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask10.BackColor = Drawing.Color.White
            End If
        End If

        '---11. check FUTURE Task Index=11---
        If CDbl(DT.Rows(0)("FutTask11").ToString) = 1 Then
            SQL5 = "select len(Task)Task11 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=11"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task11").ToString) < 2 Then
                Fut_KeyTask11.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask11.BackColor = Drawing.Color.White
            End If
        End If

        '---12. check FUTURE Task Index=12---
        If CDbl(DT.Rows(0)("FutTask12").ToString) = 1 Then
            SQL5 = "select len(Task)Task12 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=12"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task12").ToString) < 2 Then
                Fut_KeyTask12.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask12.BackColor = Drawing.Color.White
            End If
        End If

        '---13. check FUTURE Task Index=13---
        If CDbl(DT.Rows(0)("FutTask13").ToString) = 1 Then
            SQL5 = "select len(Task)Task13 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=13"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task13").ToString) < 2 Then
                Fut_KeyTask13.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask13.BackColor = Drawing.Color.White
            End If
        End If

        '---14. check FUTURE Task Index=14---
        If CDbl(DT.Rows(0)("FutTask14").ToString) = 1 Then
            SQL5 = "select len(Task)Task14 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=14"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task14").ToString) < 2 Then
                Fut_KeyTask14.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask14.BackColor = Drawing.Color.White
            End If
        End If

        '---15. check FUTURE Task Index=15---
        If CDbl(DT.Rows(0)("FutTask15").ToString) = 1 Then
            SQL5 = "select len(Task)Task15 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=15"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task15").ToString) < 2 Then
                Fut_KeyTask15.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask15.BackColor = Drawing.Color.White
            End If
        End If

        '---16. check FUTURE Task Index=16---
        If CDbl(DT.Rows(0)("FutTask16").ToString) = 1 Then
            SQL5 = "select len(Task)Task16 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=16"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task16").ToString) < 2 Then
                Fut_KeyTask16.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask16.BackColor = Drawing.Color.White
            End If
        End If

        '---17. check FUTURE Task Index=17---
        If CDbl(DT.Rows(0)("FutTask17").ToString) = 1 Then
            SQL5 = "select len(Task)Task17 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=17"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task17").ToString) < 2 Then
                Fut_KeyTask17.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask17.BackColor = Drawing.Color.White
            End If
        End If

        '---18. check FUTURE Task Index=18---
        If CDbl(DT.Rows(0)("FutTask18").ToString) = 1 Then
            SQL5 = "select len(Task)Task18 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=18"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task18").ToString) < 2 Then
                Fut_KeyTask18.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask18.BackColor = Drawing.Color.White
            End If
        End If

        '---19. check FUTURE Task Index=19---
        If CDbl(DT.Rows(0)("FutTask19").ToString) = 1 Then
            SQL5 = "select len(Task)Task19 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=19"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task19").ToString) < 2 Then
                Fut_KeyTask19.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask19.BackColor = Drawing.Color.White
            End If
        End If

        '---20. check FUTURE Task Index=20---
        If CDbl(DT.Rows(0)("FutTask20").ToString) = 1 Then
            SQL5 = "select len(Task)Task20 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=20"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task20").ToString) < 2 Then
                Fut_KeyTask20.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask20.BackColor = Drawing.Color.White
            End If
        End If

        If Flag = 0 Then
            SendToUpperManager()
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('Please fill yellow highlighted fields');</script>")
            Return
        End If

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub SaveData()
        '--Update data from Future Goals table
        SQL7 = " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit1.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit1.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit1.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=1 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit2.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit2.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit2.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=2 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit3.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit3.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit3.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=3 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit4.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit4.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit4.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=4 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit5.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit5.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit5.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=5 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit6.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit6.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit6.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=6 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit7.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit7.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit7.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=7 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit8.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit8.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit8.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=8 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit9.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit9.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit9.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=9 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTUREGOAL_TBL Set Goals='" & Replace(Replace(Replace(Trim(FUT_Goal_Edit10.Text), "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Trim(FUT_Succ_Edit10.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Trim(FUT_Date_Edit10.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=10 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "

        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask1.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=1 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask2.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=2 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask3.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=3 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask4.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=4 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask5.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=5 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask6.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=6 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask7.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=7 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask8.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=8 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask9.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=9 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask10.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=10 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask11.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=11 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask12.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=12 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask13.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=13 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask14.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=14 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask15.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=15 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask16.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=16 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask17.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=17 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask18.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=18 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask19.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=19 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update GUILD_Appraisal_FUTURETASK_TBL Set Task='" & Replace(Replace(Replace(Trim(Fut_KeyTask20.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=20 and Perf_Year = 2016 and EMPLID = " & lblEMPLID.Text & " "
        'Response.Write(SQL7) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL7)
        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Protected Sub DisplayData()
        '--Future Goals table
        SQL = "select * from(select *,(select DiscussionComments from GUILD_Appraisal_FUTUREGOAL_Master_TBL where emplid=" & lblEMPLID.Text & ")DiscussionComments from"
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
        SQL &= " from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & ")A )C)AA,"
        SQL &= " (select Max(Task1)Task1,Max(Task2)Task2,Max(Task3)Task3,Max(Task4)Task4,Max(Task5)Task5,Max(Task6)Task6,Max(Task7)Task7,Max(Task8)Task8,Max(Task9)Task9,Max(Task10)Task10,Max(Task11)Task11,Max(Task12)Task12,"
        SQL &= " Max(Task13)Task13,Max(Task14)Task14,Max(Task15)Task15,Max(Task16)Task16,Max(Task17)Task17,Max(Task18)Task18,Max(Task19)Task19,Max(Task20)Task20"
        SQL &= " from(select (case when  IndexID=1 then Rtrim(Ltrim(Task)) else '' end)Task1,(case when  IndexID=2 then Rtrim(Ltrim(Task)) else '' end)Task2,(case when  IndexID=3 then Rtrim(Ltrim(Task)) else '' end)Task3,  "
        SQL &= " (case when  IndexID=4 then Rtrim(Ltrim(Task)) else '' end)Task4,(case when  IndexID=5 then Rtrim(Ltrim(Task)) else '' end)Task5,(case when  IndexID=6 then Rtrim(Ltrim(Task)) else '' end)Task6,"
        SQL &= " (case when  IndexID=7 then Rtrim(Ltrim(Task)) else '' end)Task7,(case when  IndexID=8 then Rtrim(Ltrim(Task)) else '' end)Task8,(case when  IndexID=9 then Rtrim(Ltrim(Task)) else '' end)Task9,"
        SQL &= " (case when  IndexID=10 then Rtrim(Ltrim(Task)) else '' end)Task10,(case when  IndexID=11 then Rtrim(Ltrim(Task)) else '' end)Task11,(case when  IndexID=12 then Rtrim(Ltrim(Task)) else '' end)Task12,"
        SQL &= " (case when  IndexID=13 then Rtrim(Ltrim(Task)) else '' end)Task13,(case when  IndexID=14 then Rtrim(Ltrim(Task)) else '' end)Task14,(case when  IndexID=15 then Rtrim(Ltrim(Task)) else '' end)Task15,"
        SQL &= " (case when  IndexID=16 then Rtrim(Ltrim(Task)) else '' end)Task16,(case when  IndexID=17 then Rtrim(Ltrim(Task)) else '' end)Task17,(case when  IndexID=18 then Rtrim(Ltrim(Task)) else '' end)Task18,"
        SQL &= " (case when  IndexID=19 then Rtrim(Ltrim(Task)) else '' end)Task19,(case when  IndexID=20 then Rtrim(Ltrim(Task)) else '' end)Task20 "
        SQL &= " from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & ")A)BB"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '--Dispaly Data on TextBoxs
        FUT_Goal_Edit1.Text = DT.Rows(0)("Goals1").ToString : FUT_Succ_Edit1.Text = DT.Rows(0)("Milestones1").ToString : FUT_Date_Edit1.Text = DT.Rows(0)("TargetDate1").ToString
        FUT_Goal_Edit2.Text = DT.Rows(0)("Goals2").ToString : FUT_Succ_Edit2.Text = DT.Rows(0)("Milestones2").ToString : FUT_Date_Edit2.Text = DT.Rows(0)("TargetDate2").ToString
        FUT_Goal_Edit3.Text = DT.Rows(0)("Goals3").ToString : FUT_Succ_Edit3.Text = DT.Rows(0)("Milestones3").ToString : FUT_Date_Edit3.Text = DT.Rows(0)("TargetDate3").ToString
        FUT_Goal_Edit4.Text = DT.Rows(0)("Goals4").ToString : FUT_Succ_Edit4.Text = DT.Rows(0)("Milestones4").ToString : FUT_Date_Edit4.Text = DT.Rows(0)("TargetDate4").ToString
        FUT_Goal_Edit5.Text = DT.Rows(0)("Goals5").ToString : FUT_Succ_Edit5.Text = DT.Rows(0)("Milestones5").ToString : FUT_Date_Edit5.Text = DT.Rows(0)("TargetDate5").ToString
        FUT_Goal_Edit6.Text = DT.Rows(0)("Goals6").ToString : FUT_Succ_Edit6.Text = DT.Rows(0)("Milestones6").ToString : FUT_Date_Edit6.Text = DT.Rows(0)("TargetDate6").ToString
        FUT_Goal_Edit7.Text = DT.Rows(0)("Goals7").ToString : FUT_Succ_Edit7.Text = DT.Rows(0)("Milestones7").ToString : FUT_Date_Edit7.Text = DT.Rows(0)("TargetDate7").ToString
        FUT_Goal_Edit8.Text = DT.Rows(0)("Goals8").ToString : FUT_Succ_Edit8.Text = DT.Rows(0)("Milestones8").ToString : FUT_Date_Edit8.Text = DT.Rows(0)("TargetDate8").ToString
        FUT_Goal_Edit9.Text = DT.Rows(0)("Goals9").ToString : FUT_Succ_Edit9.Text = DT.Rows(0)("Milestones9").ToString : FUT_Date_Edit9.Text = DT.Rows(0)("TargetDate9").ToString
        FUT_Goal_Edit10.Text = DT.Rows(0)("Goals10").ToString : FUT_Succ_Edit10.Text = DT.Rows(0)("Milestones10").ToString : FUT_Date_Edit10.Text = DT.Rows(0)("TargetDate10").ToString

        Fut_KeyTask1.Text = DT.Rows(0)("Task1").ToString : Fut_KeyTask2.Text = DT.Rows(0)("Task2").ToString : Fut_KeyTask3.Text = DT.Rows(0)("Task3").ToString
        Fut_KeyTask4.Text = DT.Rows(0)("Task4").ToString : Fut_KeyTask5.Text = DT.Rows(0)("Task5").ToString : Fut_KeyTask6.Text = DT.Rows(0)("Task6").ToString
        Fut_KeyTask7.Text = DT.Rows(0)("Task7").ToString : Fut_KeyTask8.Text = DT.Rows(0)("Task8").ToString : Fut_KeyTask9.Text = DT.Rows(0)("Task9").ToString
        Fut_KeyTask10.Text = DT.Rows(0)("Task10").ToString : Fut_KeyTask11.Text = DT.Rows(0)("Task11").ToString : Fut_KeyTask12.Text = DT.Rows(0)("Task12").ToString
        Fut_KeyTask13.Text = DT.Rows(0)("Task13").ToString : Fut_KeyTask14.Text = DT.Rows(0)("Task14").ToString : Fut_KeyTask15.Text = DT.Rows(0)("Task15").ToString
        Fut_KeyTask16.Text = DT.Rows(0)("Task16").ToString : Fut_KeyTask17.Text = DT.Rows(0)("Task17").ToString : Fut_KeyTask18.Text = DT.Rows(0)("Task18").ToString
        Fut_KeyTask19.Text = DT.Rows(0)("Task19").ToString : Fut_KeyTask20.Text = DT.Rows(0)("Task20").ToString


        '--Dispaly Data on Labels
        FUT_Goal_Edit11.Text = Replace(Replace(DT.Rows(0)("Goals1").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit11.Text = Replace(Replace(DT.Rows(0)("Milestones1").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit11.Text = Replace(Replace(DT.Rows(0)("TargetDate1").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit12.Text = Replace(Replace(DT.Rows(0)("Goals2").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit12.Text = Replace(Replace(DT.Rows(0)("Milestones2").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit12.Text = Replace(Replace(DT.Rows(0)("TargetDate2").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit13.Text = Replace(Replace(DT.Rows(0)("Goals3").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit13.Text = Replace(Replace(DT.Rows(0)("Milestones3").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit13.Text = Replace(Replace(DT.Rows(0)("TargetDate3").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit14.Text = Replace(Replace(DT.Rows(0)("Goals4").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit14.Text = Replace(Replace(DT.Rows(0)("Milestones4").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit14.Text = Replace(Replace(DT.Rows(0)("TargetDate4").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit15.Text = Replace(Replace(DT.Rows(0)("Goals5").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit15.Text = Replace(Replace(DT.Rows(0)("Milestones5").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit15.Text = Replace(Replace(DT.Rows(0)("TargetDate5").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit16.Text = Replace(Replace(DT.Rows(0)("Goals6").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit16.Text = Replace(Replace(DT.Rows(0)("Milestones6").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit16.Text = Replace(Replace(DT.Rows(0)("TargetDate6").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit17.Text = Replace(Replace(DT.Rows(0)("Goals7").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit17.Text = Replace(Replace(DT.Rows(0)("Milestones7").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit17.Text = Replace(Replace(DT.Rows(0)("TargetDate7").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit18.Text = Replace(Replace(DT.Rows(0)("Goals8").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit18.Text = Replace(Replace(DT.Rows(0)("Milestones8").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit18.Text = Replace(Replace(DT.Rows(0)("TargetDate8").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit19.Text = Replace(Replace(DT.Rows(0)("Goals9").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit19.Text = Replace(Replace(DT.Rows(0)("Milestones9").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit19.Text = Replace(Replace(DT.Rows(0)("TargetDate9").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit20.Text = Replace(Replace(DT.Rows(0)("Goals10").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit20.Text = Replace(Replace(DT.Rows(0)("Milestones10").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit20.Text = Replace(Replace(DT.Rows(0)("TargetDate10").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        SQL1 = "select first+' '+ last Name from id_tbl where emplid=" & Trim(Left(DT.Rows(0)("DiscussionComments").ToString, 4))
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        If lblLogin_EMPLID.Text = lblFirst_SUP_EMPLID.Text Then
            If Len(DT.Rows(0)("DiscussionComments").ToString) > 5 Then
                ReviseComments.Visible = True
                lblComments.Text = "<b>" & DT1.Rows(0)("Name").ToString & " Comments:</b> " & Replace(Replace(Mid(DT.Rows(0)("DiscussionComments").ToString, 6), Chr(13), "<span>"), Chr(10), "<br>")
            Else
                ReviseComments.Visible = False
            End If
        End If


        '--Dispaly Index2--
        If Len(DT.Rows(0)("Goals2").ToString) + Len(DT.Rows(0)("Milestones2").ToString) + Len(DT.Rows(0)("TargetDate2").ToString) > 2 Then
            Panel_FutureGoal_Review2.Visible = True
        Else
            Panel_FutureGoal_Review2.Visible = False
        End If
        '--Dispaly Index3--
        If Len(DT.Rows(0)("Goals3").ToString) + Len(DT.Rows(0)("Milestones3").ToString) + Len(DT.Rows(0)("TargetDate3").ToString) > 2 Then
            Panel_FutureGoal_Review3.Visible = True
        Else
            Panel_FutureGoal_Review3.Visible = False
        End If
        '--Dispaly Index4--
        If Len(DT.Rows(0)("Goals4").ToString) + Len(DT.Rows(0)("Milestones4").ToString) + Len(DT.Rows(0)("TargetDate4").ToString) > 2 Then
            Panel_FutureGoal_Review4.Visible = True
        Else
            Panel_FutureGoal_Review4.Visible = False
        End If
        '--Dispaly Index5--
        If Len(DT.Rows(0)("Goals5").ToString) + Len(DT.Rows(0)("Milestones5").ToString) + Len(DT.Rows(0)("TargetDate5").ToString) > 2 Then
            Panel_FutureGoal_Review5.Visible = True
        Else
            Panel_FutureGoal_Review5.Visible = False
        End If
        '--Dispaly Index6--
        If Len(DT.Rows(0)("Goals6").ToString) + Len(DT.Rows(0)("Milestones6").ToString) + Len(DT.Rows(0)("TargetDate6").ToString) > 2 Then
            Panel_FutureGoal_Review6.Visible = True
        Else
            Panel_FutureGoal_Review6.Visible = False
        End If
        '--Dispaly Index7--
        If Len(DT.Rows(0)("Goals7").ToString) + Len(DT.Rows(0)("Milestones7").ToString) + Len(DT.Rows(0)("TargetDate7").ToString) > 2 Then
            Panel_FutureGoal_Review7.Visible = True
        Else
            Panel_FutureGoal_Review7.Visible = False
        End If
        '--Dispaly Index8--
        If Len(DT.Rows(0)("Goals8").ToString) + Len(DT.Rows(0)("Milestones8").ToString) + Len(DT.Rows(0)("TargetDate8").ToString) > 2 Then
            Panel_FutureGoal_Review8.Visible = True
        Else
            Panel_FutureGoal_Review8.Visible = False
        End If
        '--Dispaly Index9--
        If Len(DT.Rows(0)("Goals9").ToString) + Len(DT.Rows(0)("Milestones9").ToString) + Len(DT.Rows(0)("TargetDate9").ToString) > 2 Then
            Panel_FutureGoal_Review9.Visible = True
        Else
            Panel_FutureGoal_Review9.Visible = False
        End If
        '--Dispaly Index10--
        If Len(DT.Rows(0)("Goals10").ToString) + Len(DT.Rows(0)("Milestones10").ToString) + Len(DT.Rows(0)("TargetDate10").ToString) > 2 Then
            Panel_FutureGoal_Review10.Visible = True
        Else
            Panel_FutureGoal_Review10.Visible = False
        End If

        '--Dispaly Task Index2--
        If Len(DT.Rows(0)("Task2").ToString) > 2 Then Panel_Tasks_Review2.Visible = True Else Panel_Tasks_Review2.Visible = False
        If Len(DT.Rows(0)("Task3").ToString) > 2 Then Panel_Tasks_Review3.Visible = True Else Panel_Tasks_Review3.Visible = False
        If Len(DT.Rows(0)("Task4").ToString) > 2 Then Panel_Tasks_Review4.Visible = True Else Panel_Tasks_Review4.Visible = False
        If Len(DT.Rows(0)("Task5").ToString) > 2 Then Panel_Tasks_Review5.Visible = True Else Panel_Tasks_Review5.Visible = False
        If Len(DT.Rows(0)("Task6").ToString) > 2 Then Panel_Tasks_Review6.Visible = True Else Panel_Tasks_Review6.Visible = False
        If Len(DT.Rows(0)("Task7").ToString) > 2 Then Panel_Tasks_Review7.Visible = True Else Panel_Tasks_Review7.Visible = False
        If Len(DT.Rows(0)("Task8").ToString) > 2 Then Panel_Tasks_Review8.Visible = True Else Panel_Tasks_Review8.Visible = False
        If Len(DT.Rows(0)("Task9").ToString) > 2 Then Panel_Tasks_Review9.Visible = True Else Panel_Tasks_Review9.Visible = False
        If Len(DT.Rows(0)("Task10").ToString) > 2 Then Panel_Tasks_Review10.Visible = True Else Panel_Tasks_Review10.Visible = False
        If Len(DT.Rows(0)("Task11").ToString) > 2 Then Panel_Tasks_Review11.Visible = True Else Panel_Tasks_Review11.Visible = False
        If Len(DT.Rows(0)("Task12").ToString) > 2 Then Panel_Tasks_Review12.Visible = True Else Panel_Tasks_Review12.Visible = False
        If Len(DT.Rows(0)("Task13").ToString) > 2 Then Panel_Tasks_Review13.Visible = True Else Panel_Tasks_Review13.Visible = False
        If Len(DT.Rows(0)("Task14").ToString) > 2 Then Panel_Tasks_Review14.Visible = True Else Panel_Tasks_Review14.Visible = False
        If Len(DT.Rows(0)("Task15").ToString) > 2 Then Panel_Tasks_Review15.Visible = True Else Panel_Tasks_Review15.Visible = False
        If Len(DT.Rows(0)("Task16").ToString) > 2 Then Panel_Tasks_Review16.Visible = True Else Panel_Tasks_Review16.Visible = False
        If Len(DT.Rows(0)("Task17").ToString) > 2 Then Panel_Tasks_Review17.Visible = True Else Panel_Tasks_Review17.Visible = False
        If Len(DT.Rows(0)("Task18").ToString) > 2 Then Panel_Tasks_Review18.Visible = True Else Panel_Tasks_Review18.Visible = False
        If Len(DT.Rows(0)("Task19").ToString) > 2 Then Panel_Tasks_Review19.Visible = True Else Panel_Tasks_Review19.Visible = False
        If Len(DT.Rows(0)("Task20").ToString) > 2 Then Panel_Tasks_Review20.Visible = True Else Panel_Tasks_Review20.Visible = False


        Fut_KeyTask_Review1.Text = Replace(Replace(DT.Rows(0)("Task1").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review2.Text = Replace(Replace(DT.Rows(0)("Task2").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review3.Text = Replace(Replace(DT.Rows(0)("Task3").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review4.Text = Replace(Replace(DT.Rows(0)("Task4").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review5.Text = Replace(Replace(DT.Rows(0)("Task5").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review6.Text = Replace(Replace(DT.Rows(0)("Task6").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review7.Text = Replace(Replace(DT.Rows(0)("Task7").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review8.Text = Replace(Replace(DT.Rows(0)("Task8").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review9.Text = Replace(Replace(DT.Rows(0)("Task9").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review10.Text = Replace(Replace(DT.Rows(0)("Task10").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review11.Text = Replace(Replace(DT.Rows(0)("Task11").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review12.Text = Replace(Replace(DT.Rows(0)("Task12").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review13.Text = Replace(Replace(DT.Rows(0)("Task13").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review14.Text = Replace(Replace(DT.Rows(0)("Task14").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review15.Text = Replace(Replace(DT.Rows(0)("Task15").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review16.Text = Replace(Replace(DT.Rows(0)("Task16").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review17.Text = Replace(Replace(DT.Rows(0)("Task17").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review18.Text = Replace(Replace(DT.Rows(0)("Task18").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review19.Text = Replace(Replace(DT.Rows(0)("Task19").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        Fut_KeyTask_Review20.Text = Replace(Replace(DT.Rows(0)("Task20").ToString, Chr(13), "<span>"), Chr(10), "<br>")




        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub SendToUpperManager()
        SQL1 = "update GUILD_Appraisal_FUTUREGOAL_MASTER_tbl Set Process_Flag=1,Date_Submitted_To_UP_MGT='" & Now & "', DiscussionComments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblEMPLOYEE_NAME.Text & "'s Goals have been sent to you for review by " & LblMgr_NAME.Text & "<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details."

        '--Production Email
        'LocalClass.SendMail(lblUP_MGT_EMAIL.Text, "Guild Performance Appraisal was sent to you for Approval by " & Session("FIRST") & " " & Session("Last"), Msg)

        Response.Redirect("..\..\Default_Manager.aspx")


    End Sub
    Protected Sub Discuss_Click(sender As Object, e As EventArgs) Handles Discuss.Click


        If Len(DiscussionComments.Text) < 5 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please type discussion comments'); </script>")
        Else

            SQL = " update GUILD_Appraisal_FUTUREGOAL_MASTER_tbl Set Process_Flag=0,Date_Submitted_To_UP_MGT=NULL,Date_Submitted_To_HR=NULL, DiscussionComments ='" & lblLogin_EMPLID.Text + "-" + Replace(DiscussionComments.Text, "'", "`") & "' "
            SQL &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
            'Response.Write(SQL) ': Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            SQL1 = "select first+' '+ last Name from id_tbl where emplid=" & lblLogin_EMPLID.Text
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            Msg = DT1.Rows(0)("Name").ToString & " has sent back " & lblEMPLOYEE_NAME.Text & "'s Goals for the following reason:<br><br>"
            Msg &= DiscussionComments.Text & " <br><br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details."
            'Msg &= "<br><br>email to " & lblFirst_SUP_EMAIL.Text

            Msg1 = DT1.Rows(0)("Name").ToString & " has sent back " & lblEMPLOYEE_NAME.Text & "'s Goals for the following reason:<br><br>"
            Msg1 &= DiscussionComments.Text & ""

            '--Production email
            If (lblLogin_EMPLID.Text = lblUP_MGT_EMPLID.Text) Then
                'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Guild Future Goal(s) was Rejected by " & DT1.Rows(0)("Name").ToString, Msg)
            ElseIf (lblLogin_EMPLID.Text = lblHR_EMPLID.Text) Then
                'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Guild Future Goal(s) was Rejected by " & DT1.Rows(0)("Name").ToString, Msg1)
                'LocalClass.SendMail(lblUP_MGT_EMAIL.Text, "Guild Future Goal(s) was Rejected by " & DT1.Rows(0)("Name").ToString, Msg)
            End If

            Response.Redirect("..\..\Default_Manager.aspx")

        End If

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Generalist_Click(sender As Object, e As EventArgs) Handles Generalist.Click

        SQL1 = "update GUILD_Appraisal_FUTUREGOAL_MASTER_tbl Set Process_Flag=2,Date_Submitted_To_HR='" & Now & "', DiscussionComments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblEMPLOYEE_NAME.Text & "'s Goals have been sent to you for review by " & lblMGR_UP_NAME.Text & "<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details. "
        '--Production Email
        'LocalClass.SendMail(lblHR_EMAIL.Text, "Guild Performance Appraisal was sent to you for Approval by " & Session("FIRST") & " " & Session("Last"), Msg)

        Response.Redirect("..\..\Default_Manager.aspx")
    End Sub
    Protected Sub Approve_Click(sender As Object, e As EventArgs) Handles Approve.Click

        SQL1 = "update GUILD_Appraisal_FUTUREGOAL_MASTER_tbl Set Process_Flag=3,Date_HR_Approved='" & Now & "', DiscussionComments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblEMPLOYEE_NAME.Text & "'s Goals have been approved by " & lblGENERALIST_NAME.Text & "<br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details. "

        '--Production Email
        'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Guild Performance Appraisal was sent to you for Approval by " & Session("FIRST") & " " & Session("Last"), Msg)

        Response.Redirect("..\..\Default_Manager.aspx")
    End Sub
    Protected Sub Sub_Empl_Click(sender As Object, e As EventArgs) Handles Sub_Empl.Click
        SQL1 = "update GUILD_Appraisal_FUTUREGOAL_MASTER_tbl Set Process_Flag=4,Date_Submitted_To_Guild='" & Now & "', DiscussionComments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        Msg = "Your Future Goal(s) have been sent for your review.<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details. "
        LocalClass.CloseSQLServerConnection()
        Response.Redirect("..\..\Default_Appaisal.aspx")

    End Sub
    Protected Sub Empl_Review_Click(sender As Object, e As EventArgs) Handles Empl_Review.Click

        SQL1 = "update GUILD_Appraisal_FUTUREGOAL_MASTER_tbl Set Process_Flag=5,Date_Guild_Reviewed='" & Now & "', DiscussionComments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
        'SQL1 &= " delete GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
        SQL1 &= " insert into GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL  select  '" & lblFirst_SUP_EMPLID.Text & "' SUP_EMPLID,*,'" & Now & "' Guild_Approved from GUILD_Appraisal_FUTUREGOAL_TBL where emplid =" & lblEMPLID.Text & " and Perf_Year=2016"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)


        Msg = lblEMPLOYEE_NAME.Text & " confirmed review of goals set. <br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details."

        'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Guild Future Goal(s) Reviewed by " & lblEMPLOYEE_NAME.Text, Msg)

        LocalClass.CloseSQLServerConnection()

        Response.Redirect("..\..\Default_Appraisal.aspx")

    End Sub
    Protected Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnSave1.Click

        Dim Flag As Integer = 0

        SQL = " select * from (select * from"
        SQL &= "(select count(*)Fut1 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=1)F,"
        SQL &= "(select count(*)Fut2 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=2)G,"
        SQL &= "(select count(*)Fut3 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=3)H,"
        SQL &= "(select count(*)Fut4 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=4)J,"
        SQL &= "(select count(*)Fut5 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=5)K,"
        SQL &= "(select count(*)Fut6 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=6)K2,"
        SQL &= "(select count(*)Fut7 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=7)K3,"
        SQL &= "(select count(*)Fut8 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=8)K4,"
        SQL &= "(select count(*)Fut9 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=9)K5,"
        SQL &= "(select count(*)Fut10 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=10)K6)AA,"
        SQL &= "(select * from "
        SQL &= "(select count(*)FutTask1 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=1)A1,"
        SQL &= "(select count(*)FutTask2 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=2)A2,"
        SQL &= "(select count(*)FutTask3 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=3)A3,"
        SQL &= "(select count(*)FutTask4 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=4)A4,"
        SQL &= "(select count(*)FutTask5 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=5)A5,"
        SQL &= "(select count(*)FutTask6 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=6)A6,"
        SQL &= "(select count(*)FutTask7 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=7)A7,"
        SQL &= "(select count(*)FutTask8 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=8)A8,"
        SQL &= "(select count(*)FutTask9 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=9)A9,"
        SQL &= "(select count(*)FutTask10 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=10)A10,"
        SQL &= "(select count(*)FutTask11 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=11)A11,"
        SQL &= "(select count(*)FutTask12 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=12)A12,"
        SQL &= "(select count(*)FutTask13 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=13)A13,"
        SQL &= "(select count(*)FutTask14 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=14)A14,"
        SQL &= "(select count(*)FutTask15 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=15)A15,"
        SQL &= "(select count(*)FutTask16 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=16)A16,"
        SQL &= "(select count(*)FutTask17 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=17)A17,"
        SQL &= "(select count(*)FutTask18 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=18)A18,"
        SQL &= "(select count(*)FutTask19 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=19)A19,"
        SQL &= "(select count(*)FutTask20 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=20)A20)BB"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '---1. check FUTURE GOALS table Index=1---
        If CDbl(DT.Rows(0)("Fut1").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals1,len(Milestones)Milestones1,len(TargetDate)TargetDate1 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals1").ToString) < 2 Then
                FUT_Goal_Edit1.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit1.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones1").ToString) < 2 Then
                FUT_Succ_Edit1.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit1.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate1").ToString) < 2 Then
                FUT_Date_Edit1.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit1.BackColor = Drawing.Color.White
            End If
        End If
        '---2. check FUTURE GOALS table Index=2---
        If CDbl(DT.Rows(0)("Fut2").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals2,len(Milestones)Milestones2,len(TargetDate)TargetDate2 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals2").ToString) < 2 Then
                FUT_Goal_Edit2.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit2.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones2").ToString) < 2 Then
                FUT_Succ_Edit2.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit2.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate2").ToString) < 2 Then
                FUT_Date_Edit2.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit2.BackColor = Drawing.Color.White
            End If
        End If
        '---3. check FUTURE GOALS table Index=3---
        If CDbl(DT.Rows(0)("Fut3").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals3,len(Milestones)Milestones3,len(TargetDate)TargetDate3 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals3").ToString) < 2 Then
                FUT_Goal_Edit3.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit3.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones3").ToString) < 2 Then
                FUT_Succ_Edit3.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit3.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate3").ToString) < 2 Then
                FUT_Date_Edit3.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit3.BackColor = Drawing.Color.White
            End If
        End If
        '---4. check FUTURE GOALS table Index=4---
        If CDbl(DT.Rows(0)("Fut4").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals4,len(Milestones)Milestones4,len(TargetDate)TargetDate4 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals4").ToString) < 2 Then
                FUT_Goal_Edit4.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit4.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones4").ToString) < 2 Then
                FUT_Succ_Edit4.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit4.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate4").ToString) < 2 Then
                FUT_Date_Edit4.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit4.BackColor = Drawing.Color.White
            End If
        End If
        '---5. check FUTURE GOALS table Index=5---
        If CDbl(DT.Rows(0)("Fut5").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals5,len(Milestones)Milestones5,len(TargetDate)TargetDate5 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals5").ToString) < 2 Then
                FUT_Goal_Edit5.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit5.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones5").ToString) < 2 Then
                FUT_Succ_Edit5.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit5.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate5").ToString) < 2 Then
                FUT_Date_Edit5.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit5.BackColor = Drawing.Color.White
            End If
        End If
        '---6. check FUTURE GOALS table Index=6---
        If CDbl(DT.Rows(0)("Fut6").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals6,len(Milestones)Milestones6,len(TargetDate)TargetDate6 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals6").ToString) < 2 Then
                FUT_Goal_Edit6.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit6.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones6").ToString) < 2 Then
                FUT_Succ_Edit6.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit6.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate6").ToString) < 2 Then
                FUT_Date_Edit6.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit6.BackColor = Drawing.Color.White
            End If
        End If
        '---7. check FUTURE GOALS table Index=7---
        If CDbl(DT.Rows(0)("Fut7").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals7,len(Milestones)Milestones7,len(TargetDate)TargetDate7 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals7").ToString) < 2 Then
                FUT_Goal_Edit7.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit7.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones7").ToString) < 2 Then
                FUT_Succ_Edit7.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit7.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate7").ToString) < 2 Then
                FUT_Date_Edit7.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit7.BackColor = Drawing.Color.White
            End If
        End If
        '---8. check FUTURE GOALS table Index=8---
        If CDbl(DT.Rows(0)("Fut8").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals8,len(Milestones)Milestones8,len(TargetDate)TargetDate8 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals8").ToString) < 2 Then
                FUT_Goal_Edit8.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit8.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones8").ToString) < 2 Then
                FUT_Succ_Edit8.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit8.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate8").ToString) < 2 Then
                FUT_Date_Edit8.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit8.BackColor = Drawing.Color.White
            End If
        End If
        '---9. check FUTURE GOALS table Index=9---
        If CDbl(DT.Rows(0)("Fut9").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals9,len(Milestones)Milestones9,len(TargetDate)TargetDate9 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals9").ToString) < 2 Then
                FUT_Goal_Edit9.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit9.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones9").ToString) < 2 Then
                FUT_Succ_Edit9.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit9.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate9").ToString) < 2 Then
                FUT_Date_Edit9.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit9.BackColor = Drawing.Color.White
            End If
        End If
        '---10. check FUTURE GOALS table Index=10---
        If CDbl(DT.Rows(0)("Fut10").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals10,len(Milestones)Milestones10,len(TargetDate)TargetDate10 from GUILD_Appraisal_FUTUREGOAL_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=10"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals10").ToString) < 2 Then
                FUT_Goal_Edit10.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Goal_Edit10.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("Milestones10").ToString) < 2 Then
                FUT_Succ_Edit10.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Succ_Edit10.BackColor = Drawing.Color.White
            End If

            If CDbl(DT5.Rows(0)("TargetDate10").ToString) < 2 Then
                FUT_Date_Edit10.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                FUT_Date_Edit10.BackColor = Drawing.Color.White
            End If
        End If

        '---1. check FUTURE Task Index=1---
        If CDbl(DT.Rows(0)("FutTask1").ToString) = 1 Then
            SQL5 = "select len(Task)Task1 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task1").ToString) < 2 Then
                Fut_KeyTask1.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask1.BackColor = Drawing.Color.White
            End If
        End If

        '---2. check FUTURE Task Index=2---
        If CDbl(DT.Rows(0)("FutTask2").ToString) = 1 Then
            SQL5 = "select len(Task)Task2 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task2").ToString) < 2 Then
                Fut_KeyTask2.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask2.BackColor = Drawing.Color.White
            End If
        End If

        '---3. check FUTURE Task Index=3---
        If CDbl(DT.Rows(0)("FutTask3").ToString) = 1 Then
            SQL5 = "select len(Task)Task3 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task3").ToString) < 2 Then
                Fut_KeyTask3.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask3.BackColor = Drawing.Color.White
            End If
        End If

        '---4. check FUTURE Task Index=4---
        If CDbl(DT.Rows(0)("FutTask4").ToString) = 1 Then
            SQL5 = "select len(Task)Task4 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task4").ToString) < 2 Then
                Fut_KeyTask4.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask4.BackColor = Drawing.Color.White
            End If
        End If

        '---5. check FUTURE Task Index=5---
        If CDbl(DT.Rows(0)("FutTask5").ToString) = 1 Then
            SQL5 = "select len(Task)Task5 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task5").ToString) < 2 Then
                Fut_KeyTask5.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask5.BackColor = Drawing.Color.White
            End If
        End If

        '---6. check FUTURE Task Index=6---
        If CDbl(DT.Rows(0)("FutTask6").ToString) = 1 Then
            SQL5 = "select len(Task)Task6 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task6").ToString) < 2 Then
                Fut_KeyTask6.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask6.BackColor = Drawing.Color.White
            End If
        End If

        '---7. check FUTURE Task Index=7---
        If CDbl(DT.Rows(0)("FutTask7").ToString) = 1 Then
            SQL5 = "select len(Task)Task7 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task7").ToString) < 2 Then
                Fut_KeyTask7.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask7.BackColor = Drawing.Color.White
            End If
        End If

        '---8. check FUTURE Task Index=8---
        If CDbl(DT.Rows(0)("FutTask8").ToString) = 1 Then
            SQL5 = "select len(Task)Task8 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task8").ToString) < 2 Then
                Fut_KeyTask8.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask8.BackColor = Drawing.Color.White
            End If
        End If

        '---9. check FUTURE Task Index=9---
        If CDbl(DT.Rows(0)("FutTask9").ToString) = 1 Then
            SQL5 = "select len(Task)Task9 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task9").ToString) < 2 Then
                Fut_KeyTask9.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask9.BackColor = Drawing.Color.White
            End If
        End If

        '---10. check FUTURE Task Index=10---
        If CDbl(DT.Rows(0)("FutTask10").ToString) = 1 Then
            SQL5 = "select len(Task)Task10 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=10"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task10").ToString) < 2 Then
                Fut_KeyTask10.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask10.BackColor = Drawing.Color.White
            End If
        End If

        '---11. check FUTURE Task Index=11---
        If CDbl(DT.Rows(0)("FutTask11").ToString) = 1 Then
            SQL5 = "select len(Task)Task11 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=11"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task11").ToString) < 2 Then
                Fut_KeyTask11.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask11.BackColor = Drawing.Color.White
            End If
        End If

        '---12. check FUTURE Task Index=12---
        If CDbl(DT.Rows(0)("FutTask12").ToString) = 1 Then
            SQL5 = "select len(Task)Task12 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=12"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task12").ToString) < 2 Then
                Fut_KeyTask12.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask12.BackColor = Drawing.Color.White
            End If
        End If

        '---13. check FUTURE Task Index=13---
        If CDbl(DT.Rows(0)("FutTask13").ToString) = 1 Then
            SQL5 = "select len(Task)Task13 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=13"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task13").ToString) < 2 Then
                Fut_KeyTask13.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask13.BackColor = Drawing.Color.White
            End If
        End If

        '---14. check FUTURE Task Index=14---
        If CDbl(DT.Rows(0)("FutTask14").ToString) = 1 Then
            SQL5 = "select len(Task)Task14 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=14"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task14").ToString) < 2 Then
                Fut_KeyTask14.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask14.BackColor = Drawing.Color.White
            End If
        End If

        '---15. check FUTURE Task Index=15---
        If CDbl(DT.Rows(0)("FutTask15").ToString) = 1 Then
            SQL5 = "select len(Task)Task15 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=15"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task15").ToString) < 2 Then
                Fut_KeyTask15.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask15.BackColor = Drawing.Color.White
            End If
        End If

        '---16. check FUTURE Task Index=16---
        If CDbl(DT.Rows(0)("FutTask16").ToString) = 1 Then
            SQL5 = "select len(Task)Task16 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=16"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task16").ToString) < 2 Then
                Fut_KeyTask16.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask16.BackColor = Drawing.Color.White
            End If
        End If

        '---17. check FUTURE Task Index=17---
        If CDbl(DT.Rows(0)("FutTask17").ToString) = 1 Then
            SQL5 = "select len(Task)Task17 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=17"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task17").ToString) < 2 Then
                Fut_KeyTask17.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask17.BackColor = Drawing.Color.White
            End If
        End If

        '---18. check FUTURE Task Index=18---
        If CDbl(DT.Rows(0)("FutTask18").ToString) = 1 Then
            SQL5 = "select len(Task)Task18 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=18"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task18").ToString) < 2 Then
                Fut_KeyTask18.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask18.BackColor = Drawing.Color.White
            End If
        End If

        '---19. check FUTURE Task Index=19---
        If CDbl(DT.Rows(0)("FutTask19").ToString) = 1 Then
            SQL5 = "select len(Task)Task19 from GUILD_Appraisal_FUTURETASK_TBL where Perf_Year=2016 and emplid=" & lblEMPLID.Text & " and IndexID=19"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Task19").ToString) < 2 Then
                Fut_KeyTask19.BackColor = Drawing.Color.Yellow
                Flag = 1
            Else
                Fut_KeyTask19.BackColor = Drawing.Color.White
            End If
        End If


        If Flag = 0 Then
            SQL1 = "update GUILD_Appraisal_FUTUREGOAL_MASTER_tbl Set Process_Flag=5,Date_Submitted_To_Guild='" & Now & "', DiscussionComments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            Msg = "Your Goals have been edited and sent to you for your review and confirm.<br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details."

            'LocalClass.SendMail(lblGUILD_EMAIL.Text, "Guild Future Goal(s) was updated by " & LblMgr_NAME.Text, Msg)

            LocalClass.CloseSQLServerConnection()

            Response.Redirect("..\..\Default_Manager.aspx")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('Please fill yellow highlighted fields');</script>")
            Return
        End If

        LocalClass.CloseSQLServerConnection()

    End Sub
    Sub Goals_Log()
        SQL = "select * from(select count(*)CNT_FUTGoals from GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL where emplid=" & lblEMPLID.Text
        SQL &= " and Guild_Approved not in (select distinct Date_Guild_Reviewed from GUILD_Appraisal_FutureGoal_Master_TBL where emplid=" & lblEMPLID.Text & "))A,"
        SQL &= " (select count(*)Cycle_Done from Guild_Appraisal_FutureGoal_MASTER_tbl where emplid = " & lblEMPLID.Text & " and process_flag=5)B"
        'Response.Write(SQL)

        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If CDbl(DT.Rows(0)("CNT_FUTGoals").ToString) > 0 And CDbl(DT.Rows(0)("Cycle_Done").ToString) = 1 Then
            SqlDataSource1.SelectCommand = "select (select last+' '+first from id_tbl where emplid=A.sup_emplid)Created,* from(select distinct sup_emplid,emplid,Goals,Milestones,TargetDate,Max(Guild_Approved)Guild_Approved "
            SqlDataSource1.SelectCommand &= " from GUILD_Appraisal_FUTUREGOAL_PRESERVE_TBL A where emplid=" & lblEMPLID.Text & " group by sup_emplid,emplid,Goals,Milestones,TargetDate)A "
            SqlDataSource1.SelectCommand &= " where Guild_Approved not in (select distinct Date_Guild_Reviewed from GUILD_Appraisal_FutureGoal_Master_TBL where emplid=" & lblEMPLID.Text & ")"
            'Response.Write(SqlDataSource1.SelectCommand)
            Panel_Goals_Log.Visible = True
        Else
            Panel_Goals_Log.Visible = False
        End If

        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub ShowHideFutureTaskPanel()
        'DisplayData()
        SQL6 = "select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10,"
        SQL6 &= " Max(F_IND11)F_IND11,Max(F_IND12)F_IND12,Max(F_IND13)F_IND13,Max(F_IND14)F_IND14,Max(F_IND15)F_IND15,Max(F_IND16)F_IND16,Max(F_IND17)F_IND17,Max(F_IND18)F_IND18,Max(F_IND19)F_IND19,Max(F_IND20)F_IND20"
        SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,(case when"
        SQL6 &= " IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*)"
        SQL6 &= " else 0 end)F_IND7,(case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10,"
        SQL6 &= " (case when IndexID=11 then count(*) else 0 end)F_IND11,(case when IndexID=12 then count(*) else 0 end)F_IND12,(case when IndexID=13 then count(*) else 0 end)F_IND13,"
        SQL6 &= " (case when IndexID=14 then count(*) else 0 end)F_IND14,(case when IndexID=15 then count(*) else 0 end)F_IND15,(case when IndexID=16 then count(*) else 0 end)F_IND16,"
        SQL6 &= " (case when IndexID=17 then count(*) else 0 end)F_IND17,(case when IndexID=18 then count(*) else 0 end)F_IND18,(case when IndexID=19 then count(*) else 0 end)F_IND19,"
        SQL6 &= " (case when IndexID=20 then count(*) else 0 end)F_IND20 from GUILD_Appraisal_FUTURETASK_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=2016 group by IndexID)A"
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel2.Visible = True Else Panel2.Visible = False
        If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel3.Visible = True Else Panel3.Visible = False
        If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel4.Visible = True Else Panel4.Visible = False
        If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel5.Visible = True Else Panel5.Visible = False
        If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel6.Visible = True Else Panel6.Visible = False
        If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel7.Visible = True Else Panel7.Visible = False
        If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel8.Visible = True Else Panel8.Visible = False
        If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel9.Visible = True Else Panel9.Visible = False
        If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel10.Visible = True Else Panel10.Visible = False
        If CDbl(DT6.Rows(0)("F_IND11").ToString) > 0 Then Panel11.Visible = True Else Panel11.Visible = False
        If CDbl(DT6.Rows(0)("F_IND12").ToString) > 0 Then Panel12.Visible = True Else Panel12.Visible = False
        If CDbl(DT6.Rows(0)("F_IND13").ToString) > 0 Then Panel13.Visible = True Else Panel13.Visible = False
        If CDbl(DT6.Rows(0)("F_IND14").ToString) > 0 Then Panel14.Visible = True Else Panel14.Visible = False
        If CDbl(DT6.Rows(0)("F_IND15").ToString) > 0 Then Panel15.Visible = True Else Panel15.Visible = False
        If CDbl(DT6.Rows(0)("F_IND16").ToString) > 0 Then Panel16.Visible = True Else Panel16.Visible = False
        If CDbl(DT6.Rows(0)("F_IND17").ToString) > 0 Then Panel17.Visible = True Else Panel17.Visible = False
        If CDbl(DT6.Rows(0)("F_IND18").ToString) > 0 Then Panel18.Visible = True Else Panel18.Visible = False
        If CDbl(DT6.Rows(0)("F_IND19").ToString) > 0 Then Panel19.Visible = True Else Panel19.Visible = False
        If CDbl(DT6.Rows(0)("F_IND20").ToString) > 0 Then Panel20.Visible = True Else Panel20.Visible = False

        LocalClass.CloseSQLServerConnection()
        DisplayData()
    End Sub
    Protected Sub BtnFuture_Task_Click(sender As Object, e As EventArgs) Handles BtnFuture_Task.Click
        SQL = "select count(*)CNT_FUT from GUILD_Appraisal_FUTURETASK_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        'EMPLID,Perf_Year,Future_Year,IndexID,Goals,Milestones,TargetDate

        If CDbl(DT.Rows(0)("CNT_FUT").ToString) = 0 Then
            SQL3 = " insert into GUILD_Appraisal_FUTURETASK_TBL (EMPLID,Perf_Year,IndexID,Task) values(" & lblEMPLID.Text & ",'2016',1,'')"
            'Response.Write("Zero " & SQL3) : Response.End()
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()
        Else
            SQL2 = "select Max(IndexId)+1 NewIndexID from GUILD_Appraisal_FUTURETASK_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
            SQL4 &= " insert into GUILD_Appraisal_FUTURETASK_TBL (EMPLID,Perf_Year,IndexID,Task) values(" & lblEMPLID.Text & ", '2016'," & DT2.Rows(0)("NewIndexID").ToString & ",'')"
            'Response.Write(SQL2 & "<br>" & SQL4) : Response.End()
            DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
            LocalClass.CloseSQLServerConnection()

            SQL3 = "select Max(IndexId) NextIndexID from GUILD_Appraisal_FUTURETASK_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=2016"
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()

            If CDbl(DT3.Rows(0)("NextIndexID").ToString) > 20 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('A maximum of 20 task has been exceeded.'); </script>")
                SQL2 &= "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID>20"
                DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
                LocalClass.CloseSQLServerConnection()
            Else
                ShowHideFutureTaskPanel()
            End If
        End If
    End Sub
    Protected Sub Fut_Del2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del2.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=2 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>2 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del3.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=3 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>3 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del4.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=4 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>4 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del5.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=5 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>5 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del6.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=6 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>6 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del7.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=7 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>7 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del8.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=8 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>8 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del9.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=9 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>9 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del10.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=10 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>10 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del11_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del11.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=11 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>11 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del12.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=12 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>12 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del13_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del13.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=13 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>13 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del14_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del14.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=14 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>14 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del15_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del15.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=15 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>15 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del16_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del16.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=16 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>16 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del17_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del17.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=17 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>17 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del18_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del18.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=18 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>18 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del19_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del19.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=19 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>19 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub
    Protected Sub Fut_Del20_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Fut_Del20.Click
        SQL = "delete GUILD_Appraisal_FUTURETASK_TBL where IndexID=20 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        SQL &= " update GUILD_Appraisal_FUTURETASK_TBL Set IndexID=IndexID-1 where IndexID>20 and Perf_Year=2016 and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        ShowHideFutureTaskPanel()
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        x = (Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblLogin_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q"))

        If Session("SAP") = "GLD" Then
            Response.Redirect("..\..\Default_Appaisal.aspx?Token=" & x)
        Else
            Response.Redirect("..\..\Default_Manager.aspx?Token=" & x)
        End If
    End Sub
End Class