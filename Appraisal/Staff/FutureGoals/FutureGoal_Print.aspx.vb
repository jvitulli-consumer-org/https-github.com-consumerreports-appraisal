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
Public Class FutureGoal_Print
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


    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblEMPLID.Text = Mid(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), _
                        "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9"), 5, 4)
        lblYear.Text = Left(Request.QueryString("Token"), 4)

        lblLogin_EMPLID.Text = Trim(Session("MGT_EMPLID"))
        'Response.Write(lblEMPLID.Text & "<br>" & lblLogin_EMPLID.Text) : Response.End()

        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 1080)) + 20)

        If Session("NETID") = "" Then Response.Redirect("..\..\default.aspx")


        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display") ': Response.End()
        Else
            'Response.Write("<br>2. else Save Data before Display") ': Response.End()
            DisplayData()
        End If
        Goals_Log()
        SetLevel_Approval()
        ShowButtonCursor()
        '--Process Flag---
        '  0--First Manager 
        '  1--Second Manager
        '  2--HR Generalist
        '  3--First Manager - Reviewed by HR
        '  4--Waiting to send employee
        '  5--Employee Reviewed and confirmed

        '-------TABLES USED THIS FORM-------------
        '1. Appraisal_FutureGoals_Master_tbl
        '2. Appraisal_FutureGoals_tbl
        '4. Appraisal_FutureGoal_Recall_tbl

        'CHR(10) -- Line feed        replace to <br>
        'CHR(13) -- Carriage return  replace to <span>  
    End Sub


    Protected Sub SetLevel_Approval()
        SQL1 = "select (select count(*) from Appraisal_FutureGoals_Master_tbl A where mgt_emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & ")Manager,*,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.emplid)Empl_Name,(select first+' '+last from id_tbl where emplid=a.MGT_EMPLID)MGT_Name,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.HR_EMPLID)HR_Name,"
        SQL1 &= " (select convert(char(10),hire_Date,101) from id_tbl where emplid=a.emplid)Empl_Hired,"
        SQL1 &= " (select email from id_tbl where emplid=a.emplid)Empl_Email,(select email from id_tbl where emplid=a.MGT_EMPLID)MGT_Email,"
        SQL1 &= " (select email from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Email,(select email from id_tbl where emplid=a.HR_EMPLID)HR_Email "
        SQL1 &= " from Appraisal_FutureGoals_Master_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text
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
        lblHR_NAME.Text = DT1.Rows(0)("HR_Name").ToString
        lblHR_EMPLID.Text = DT1.Rows(0)("HR_EMPLID").ToString
        lblHR_EMAIL.Text = DT1.Rows(0)("HR_email").ToString
        '--Goal Years--
        Goal_Year.Text = Trim(Right(lblYear.Text, 2))
        Goal_Year1.Text = Trim(Right(lblYear.Text - 1, 2))
        Goal_Year2.Text = Trim(Right(lblYear.Text, 2))

        If CDbl(Goal_Year.Text) = "17" Then
            divOldTitle.Visible = True : divNewtext.Visible = False : divNewTitle.Visible = False
            divGoalText.Visible = False : divNewKey.Visible = False : divNewtarget.Visible = False
            GridView1.Columns(2).HeaderText = "Success Measures or Milestones"
        Else
            divOldTitle.Visible = False : divNewtext.Visible = True : divNewTitle.Visible = True
            divGoalText.Visible = True : divNewKey.Visible = True : divNewtarget.Visible = True
            GridView1.Columns(2).HeaderText = "Key Results"
        End If


        If DateTime.Parse(DT1.Rows(0)("DateSUB_Empl").ToString) > DateTime.Parse(DT1.Rows(0)("DateEmpl_Appr").ToString) Then
            Session("GoalUpdated") = 1
        Else
            Session("GoalUpdated") = 0
        End If
        'Response.Write("GoalUpdated-" & Session("GoalUpdated") & "<br>" & DateTime.Parse(DT1.Rows(0)("DateSUB_Empl").ToString) & "<br>" & DateTime.Parse(DT1.Rows(0)("DateEmpl_Appr").ToString))

        '--Find out if exempt is manager
        If CDbl(DT1.Rows(0)("Manager").ToString) = 0 Then Session("Manager") = 0 Else Session("Manager") = 1

        'Response.Write("lblLogin_ID " & lblLogin_EMPLID.Text & "<br>lblEMPLID " & lblEMPLID.Text & "<br>SUP_ID " & lblFirst_SUP_EMPLID.Text & "<br>Empl_ID  - " & DT1.Rows(0)("emplid").ToString & "<br>Manage Employee " & DT1.Rows(0)("Manager").ToString)

        If lblFlag.Text = 0 Then
            Goal_Setting_Edit.Visible = True : Goal_Setting_Edit1.Visible = False
            BtnSubmit_UpperMgr.Text = "Send to " & lblUP_MGT_NAME.Text

            Goal_Setting_Review.Visible = False : Goal_Discussion.Visible = False
            BtnSubmit_UpperMgr.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : BtnSubmit_UpperMgr.Attributes.Add("onmouseout", "this.style.color='#000000'")
            btnSave.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 1 Then
            Generalist.Visible = True : Approve.Visible = False : Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = True : LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = "Waiting Approval" : LblUP_MGT_Appr.ForeColor = Drawing.Color.Blue : Generalist.Text = "Submit for review to " & lblHR_NAME.Text
            Generalist.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Generalist.Attributes.Add("onmouseout", "this.style.color='#000000'")
        ElseIf lblFlag.Text = 2 Then
            Generalist.Visible = False : Approve.Visible = True : Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False : LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = "Waiting Approval" : LblHR_Appr.ForeColor = Drawing.Color.Blue
            Approve.Text = "Approve" : Approve.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Approve.Attributes.Add("onmouseout", "this.style.color='#000000'")
        ElseIf lblFlag.Text = 3 Then
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString

        ElseIf lblFlag.Text = 4 And Len(lblLogin_EMPLID.Text) <> 0 Then
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString
            Sub_Empl_Review.Visible = True : Sub_Empl_Review.Text = "Submitted for Review on " & DT1.Rows(0)("DateSUB_Empl").ToString
        ElseIf lblFlag.Text = 4 And Len(lblLogin_EMPLID.Text) = 0 Then
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString
            Sub_Empl_Review.Visible = True : Sub_Empl_Review.Text = "Submitted for Review on " & DT1.Rows(0)("DateSUB_Empl").ToString


            '--Employee review and confirm--
            ' ElseIf lblFlag.Text = 5 And Len(lblLogin_EMPLID.Text) = 0 Then

        ElseIf lblFlag.Text = 5 And Len(lblLogin_EMPLID.Text) = 0 Then ' Employee's view
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : Sub_Empl_Review.Visible = True
            LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString : LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
            If Session("GoalUpdated") = 1 Then
                'Empl_Review.Visible = True
            Else
                ' Empl_Review.Visible = False
                Sub_Empl_Review.Text = "Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
            End If

            '--Manager after Employee Approved--
        ElseIf lblFlag.Text = 5 And Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) <> 0) Then ' Manager's view
            'ElseIf lblFlag.Text = 5 And CDbl(lblEMPLID.Text) - CDbl(DT1.Rows(0)("EMPLID").ToString) <> 0 And Len(lblLogin_EMPLID.Text) <> 0 Then

            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString
            LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString : Sub_Empl_Review.Visible = True : BtnSubmit_UpperMgr.Visible = False
            btnSave1.Text = "Save and Submit for review to " & lblEmpl_NAME.Text : btnSave.Visible = False

            If Session("GoalUpdated") = 1 Then
                Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = True : btnSave1.Visible = False
                Sub_Empl_Review.Text = "Waiting Confirmation from " & lblEmpl_NAME.Text : Sub_Empl_Review.Font.Size = 16 : Panel_Goals_Log.Visible = False
            Else
                Goal_Setting_Edit.Visible = True : Goal_Setting_Edit1.Visible = False : Goal_Setting_Edit.Visible = True
                Goal_Setting_Review.Visible = False : Goal_Discussion.Visible = False : btnSave1.Visible = True
                Sub_Empl_Review.Text = "<br>" & lblEmpl_NAME.Text & " Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString & ". <br>If changes need to be made to the goals, please make edits on the form, and used the save and submit button above."
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
            End If


        ElseIf lblFlag.Text = 5 And Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) = 0) Then ' manager's view
            Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : Sub_Empl_Review.Visible = True
            LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString : LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString

            If Session("GoalUpdated") = 1 Then

            Else

                Sub_Empl_Review.Text = "Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
            End If

        End If


        Disc_Com.Text = "Send suggested revisions to " & LblMGT_NAME.Text & ""
        Discuss.Text = "Send back to " & LblMGT_NAME.Text & " for revision"

        If (lblLogin_EMPLID.Text = lblFirst_SUP_EMPLID.Text) Then
            Goal_Discussion.Visible = False
        End If

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub ShowButtonCursor()

        BtnSubmit_UpperMgr.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : BtnSubmit_UpperMgr.Attributes.Add("onmouseout", "this.style.color='#000000'")
        btnSave.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Discuss.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Discuss.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Generalist.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Generalist.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Approve.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Approve.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Discuss.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Discuss.Attributes.Add("onmouseout", "this.style.color='#000000'")

        btnSave1.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave1.Attributes.Add("onmouseout", "this.style.color='#000000'")


    End Sub
    Protected Sub ShowHideGoalPanel()
        SQL6 = " select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10 "
        SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,"
        SQL6 &= " (case when IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*) else 0 end)F_IND7,"
        SQL6 &= " (case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
        SQL6 &= " from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " group by IndexID)A "
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_Goal2.Visible = True : Panel_FutureGoal_Review2.Visible = True Else Panel_Goal2.Visible = False : Panel_FutureGoal_Review2.Visible = False
        If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_Goal3.Visible = True : Panel_FutureGoal_Review3.Visible = True Else Panel_Goal3.Visible = False : Panel_FutureGoal_Review3.Visible = False
        If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_Goal4.Visible = True : Panel_FutureGoal_Review4.Visible = True Else Panel_Goal4.Visible = False : Panel_FutureGoal_Review4.Visible = False
        If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_Goal5.Visible = True : Panel_FutureGoal_Review5.Visible = True Else Panel_Goal5.Visible = False : Panel_FutureGoal_Review5.Visible = False
        If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel_Goal6.Visible = True : Panel_FutureGoal_Review6.Visible = True Else Panel_Goal6.Visible = False : Panel_FutureGoal_Review6.Visible = False
        If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel_Goal7.Visible = True : Panel_FutureGoal_Review7.Visible = True Else Panel_Goal7.Visible = False : Panel_FutureGoal_Review7.Visible = False
        If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel_Goal8.Visible = True : Panel_FutureGoal_Review8.Visible = True Else Panel_Goal8.Visible = False : Panel_FutureGoal_Review8.Visible = False
        If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel_Goal9.Visible = True : Panel_FutureGoal_Review9.Visible = True Else Panel_Goal9.Visible = False : Panel_FutureGoal_Review9.Visible = False
        If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel_Goal10.Visible = True : Panel_FutureGoal_Review10.Visible = True Else Panel_Goal10.Visible = False : Panel_FutureGoal_Review10.Visible = False

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub BtnSubmit_UpperMgr_Click(sender As Object, e As EventArgs) Handles BtnSubmit_UpperMgr.Click
        'SaveData()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'SaveData()
    End Sub

    Protected Sub DisplayData()

        ShowHideGoalPanel()

        '--Future Goals table
        SQL = "select A.*,"
        SQL &= " IsNull(Goals1,'')Goals1,IsNull(Milestones1,'')Milestones1,IsNull(TargetDate1,'')TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2,"
        SQL &= " IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4,"
        SQL &= " IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6,"
        SQL &= " IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,IsNull(TargetDate8,'')TargetDate8,"
        SQL &= " IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10"
        SQL &= " from (select emplid,mgt_emplid,(select first+' '+last from id_tbl where emplid=hr_emplid)HR_Name,hr_emplid,"
        SQL &= " IsNull(Comments,'')Comments from Appraisal_FutureGoals_Master_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")A "
        SQL &= " Left Join (select MGT_Emplid,emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1)A1 on a.emplid=a1.emplid"
        SQL &= " Left Join (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2)B on a.emplid=b.emplid"
        SQL &= " Left Join (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3)C on a.emplid=c.emplid"
        SQL &= " Left Join (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4)D on a.emplid=d.emplid"
        SQL &= " Left Join (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5)E on a.emplid=e.emplid"
        SQL &= " Left Join (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6)F on a.emplid=f.emplid"
        SQL &= " Left Join (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7)G on a.emplid=g.emplid"
        SQL &= " Left Join (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8)H on a.emplid=h.emplid"
        SQL &= " Left Join (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9)N on a.emplid=n.emplid"
        SQL &= " Left Join (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & "  and IndexID =10)K on A.emplid=K.emplid"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '--Dispaly Data on TextBoxs
        lblGoal1.Text = Replace(Replace(DT.Rows(0)("Goals1").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess1.Text = Replace(Replace(DT.Rows(0)("Milestones1").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate1.Text = Replace(Replace(DT.Rows(0)("TargetDate1").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        lblGoal2.Text = Replace(Replace(DT.Rows(0)("Goals2").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess2.Text = Replace(Replace(DT.Rows(0)("Milestones2").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate2.Text = Replace(Replace(DT.Rows(0)("TargetDate2").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        lblGoal3.Text = Replace(Replace(DT.Rows(0)("Goals3").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess3.Text = Replace(Replace(DT.Rows(0)("Milestones3").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate3.Text = Replace(Replace(DT.Rows(0)("TargetDate3").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        lblGoal4.Text = Replace(Replace(DT.Rows(0)("Goals4").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess4.Text = Replace(Replace(DT.Rows(0)("Milestones4").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate4.Text = Replace(Replace(DT.Rows(0)("TargetDate4").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        lblGoal5.Text = Replace(Replace(DT.Rows(0)("Goals5").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess5.Text = Replace(Replace(DT.Rows(0)("Milestones5").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate5.Text = Replace(Replace(DT.Rows(0)("TargetDate5").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        lblGoal6.Text = Replace(Replace(DT.Rows(0)("Goals6").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess6.Text = Replace(Replace(DT.Rows(0)("Milestones6").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate6.Text = Replace(Replace(DT.Rows(0)("TargetDate6").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        lblGoal7.Text = Replace(Replace(DT.Rows(0)("Goals7").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess7.Text = Replace(Replace(DT.Rows(0)("Milestones7").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate7.Text = Replace(Replace(DT.Rows(0)("TargetDate7").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        lblGoal8.Text = Replace(Replace(DT.Rows(0)("Goals8").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess8.Text = Replace(Replace(DT.Rows(0)("Milestones8").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate8.Text = Replace(Replace(DT.Rows(0)("TargetDate8").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        lblGoal9.Text = Replace(Replace(DT.Rows(0)("Goals9").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess9.Text = Replace(Replace(DT.Rows(0)("Milestones9").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate9.Text = Replace(Replace(DT.Rows(0)("TargetDate9").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        lblGoal10.Text = Replace(Replace(DT.Rows(0)("Goals10").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblSuccess10.Text = Replace(Replace(DT.Rows(0)("Milestones10").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDate10.Text = Replace(Replace(DT.Rows(0)("TargetDate10").ToString, Chr(13), "<span>"), Chr(10), "<br>")

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

        SQL1 = "select first+' '+ last Name from id_tbl where emplid=" & Trim(Left(DT.Rows(0)("Comments").ToString, 4))
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        If lblLogin_EMPLID.Text = DT.Rows(0)("MGT_Emplid").ToString Then
            If Len(DT.Rows(0)("Comments").ToString) > 5 Then
                ReviseComments.Visible = True
                lblComments.Text = "<b>" & DT1.Rows(0)("Name").ToString & " Comments:</b> " & Replace(Replace(Mid(DT.Rows(0)("Comments").ToString, 6), Chr(13), "<span>"), Chr(10), "<br>")
            Else
                ReviseComments.Visible = False
            End If
        End If

        If Len(DT.Rows(0)("Goals2").ToString) + Len(DT.Rows(0)("Milestones2").ToString) + Len(DT.Rows(0)("TargetDate2").ToString) > 2 Then Panel_FutureGoal_Review2.Visible = True Else Panel_FutureGoal_Review2.Visible = False
        If Len(DT.Rows(0)("Goals3").ToString) + Len(DT.Rows(0)("Milestones3").ToString) + Len(DT.Rows(0)("TargetDate3").ToString) > 2 Then Panel_FutureGoal_Review3.Visible = True Else Panel_FutureGoal_Review3.Visible = False
        If Len(DT.Rows(0)("Goals4").ToString) + Len(DT.Rows(0)("Milestones4").ToString) + Len(DT.Rows(0)("TargetDate4").ToString) > 2 Then Panel_FutureGoal_Review4.Visible = True Else Panel_FutureGoal_Review4.Visible = False
        If Len(DT.Rows(0)("Goals5").ToString) + Len(DT.Rows(0)("Milestones5").ToString) + Len(DT.Rows(0)("TargetDate5").ToString) > 2 Then Panel_FutureGoal_Review5.Visible = True Else Panel_FutureGoal_Review5.Visible = False
        If Len(DT.Rows(0)("Goals6").ToString) + Len(DT.Rows(0)("Milestones6").ToString) + Len(DT.Rows(0)("TargetDate6").ToString) > 2 Then Panel_FutureGoal_Review6.Visible = True Else Panel_FutureGoal_Review6.Visible = False
        If Len(DT.Rows(0)("Goals7").ToString) + Len(DT.Rows(0)("Milestones7").ToString) + Len(DT.Rows(0)("TargetDate7").ToString) > 2 Then Panel_FutureGoal_Review7.Visible = True Else Panel_FutureGoal_Review7.Visible = False
        If Len(DT.Rows(0)("Goals8").ToString) + Len(DT.Rows(0)("Milestones8").ToString) + Len(DT.Rows(0)("TargetDate8").ToString) > 2 Then Panel_FutureGoal_Review8.Visible = True Else Panel_FutureGoal_Review8.Visible = False
        If Len(DT.Rows(0)("Goals9").ToString) + Len(DT.Rows(0)("Milestones9").ToString) + Len(DT.Rows(0)("TargetDate9").ToString) > 2 Then Panel_FutureGoal_Review9.Visible = True Else Panel_FutureGoal_Review9.Visible = False
        If Len(DT.Rows(0)("Goals10").ToString) + Len(DT.Rows(0)("Milestones10").ToString) + Len(DT.Rows(0)("TargetDate10").ToString) > 2 Then Panel_FutureGoal_Review10.Visible = True Else Panel_FutureGoal_Review10.Visible = False


        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub SendToUpperManager()
        SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=1,DateSUB_UP_MGT='" & Now & "', Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()
        Msg = lblEmpl_NAME.Text & "'s Goals have been sent to you for review by " & LblMGT_NAME.Text & "<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Staff/FutureGoals/FutureGoal.aspx for full details."
        '--Production Email
        'LocalClass.SendMail(lblUP_MGT_EMAIL.Text, "Guild Performance Appraisal was sent to you for Approval by " & Session("FIRST") & " " & Session("Last"), Msg)
        Response.Redirect("FutureGoal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Discuss_Click(sender As Object, e As EventArgs) Handles Discuss.Click
        If Len(DiscussionComments.Text) < 5 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please type discussion comments'); </script>")
        Else
            SQL = " update Appraisal_FutureGoals_Master_tbl Set Process_Flag=0,DateSUB_UP_MGT=NULL,DateSUB_HR=NULL, Comments ='" & lblLogin_EMPLID.Text + "-" + Replace(DiscussionComments.Text, "'", "`") & "' "
            SQL &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
            'Response.Write(SQL) ': Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            SQL1 = "select first+' '+ last Name from id_tbl where emplid=" & lblLogin_EMPLID.Text
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            'Msg = DT1.Rows(0)("Name").ToString & " has sent back " & lblEmpl_NAME.Text & "'s Goals for the following reason:<br><br>"
            'Msg &= DiscussionComments.Text & " <br><br>"
            'Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details."
            'Msg &= "<br><br>email to " & lblFirst_SUP_EMAIL.Text
            'Msg1 = DT1.Rows(0)("Name").ToString & " has sent back " & lblEmpl_NAME.Text & "'s Goals for the following reason:<br><br>"
            'Msg1 &= DiscussionComments.Text & ""
            '--Production email
            If (lblLogin_EMPLID.Text = lblUP_MGT_EMPLID.Text) Then
                'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Guild Future Goal(s) was Rejected by " & DT1.Rows(0)("Name").ToString, Msg)
            ElseIf (lblLogin_EMPLID.Text = lblHR_EMPLID.Text) Then
                'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Guild Future Goal(s) was Rejected by " & DT1.Rows(0)("Name").ToString, Msg1)
                'LocalClass.SendMail(lblUP_MGT_EMAIL.Text, "Guild Future Goal(s) was Rejected by " & DT1.Rows(0)("Name").ToString, Msg)
            End If
            'Response.Redirect("..\..\Default_Manager.aspx")
        End If
        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub Generalist_Click(sender As Object, e As EventArgs) Handles Generalist.Click
        SQL1 = "update Appraisal_FutureGoals_Master_tbl Set Process_Flag=2,DateSUB_HR='" & Now & "', Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()
        Msg = lblEmpl_NAME.Text & "'s Goals have been sent to you for review by " & lblUP_MGT_NAME.Text & "<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details. "
        '--Production Email
        'LocalClass.SendMail(lblHR_EMAIL.Text, "Guild Performance Appraisal was sent to you for Approval by " & Session("FIRST") & " " & Session("Last"), Msg)

    End Sub
    Protected Sub Approve_Click(sender As Object, e As EventArgs) Handles Approve.Click
        SQL1 = "update Appraisal_FutureGoals_Master_tbl Set Process_Flag=3,DateHR_Appr='" & Now & "', Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()
        Msg = lblEmpl_NAME.Text & "'s Goals have been approved by " & lblHR_NAME.Text & "<br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/FutureGoals/FutureGoal.aspx for full details. "
        '--Production Email
        'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Guild Performance Appraisal was sent to you for Approval by " & Session("FIRST") & " " & Session("Last"), Msg)
        '--Close window  Add code on Load_Page Approve.Attributes.Add("onclick", "window.close();")
        'Response.Write("<script language='javascript'> { window.close();}</script>")
    End Sub

    Protected Sub SendToEmployee() '--Update FutureGoals table after Appraisal approved
        SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=5,DateSUB_Empl='" & Now & "',Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        Msg = "Your Goals have been edited and sent to you for your review and confirm.<br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Staff/FutureGoals/FutureGoal.aspx for full details.<br>"

        'LocalClass.SendMail(lblEmpl_EMAIL.Text, "Your Future Goal(s) was updated by " & LblMGT_NAME.Text, Msg)
        'LocalClass.SendMail("vituja@consumer.org", "Your Future Goal(s) was updated by " & LblMGT_NAME.Text, Msg)

        LocalClass.CloseSQLServerConnection()

        Response.Redirect("FutureGoal.aspx?Token=" & Request.QueryString("Token"))

    End Sub
    Protected Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnSave1.Click '--Send to employee after Appraisal approved

    End Sub

    Sub Goals_Log()
        SQL = "select * from(select count(*)CNT_FUTGoals from(select * from Appraisal_FutureGoal_Recall_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        SQL &= " and DateEmpl_Appr not in (select distinct DateEmpl_Appr from Appraisal_FutureGoals_Master_tbl where emplid=" & lblEMPLID.Text & "))A)A1,"
        SQL &= "(select count(*)Cycle_Done from Appraisal_FutureGoals_Master_tbl where emplid=" & lblEMPLID.Text & "  and process_flag=5 and Perf_Year=" & lblYear.Text & ")B "
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If CDbl(DT.Rows(0)("CNT_FUTGoals").ToString) > 0 And CDbl(DT.Rows(0)("Cycle_Done").ToString) = 1 Then
            'SqlDataSource1.SelectCommand = " select (select last+' '+first from id_tbl where emplid=A.Recall_EMPLID)Created,* from(select distinct Recall_EMPLID,emplid,Goals,Milestones,TargetDate,Max(DateEmpl_Appr)Guild_Approved "
            'SqlDataSource1.SelectCommand &= " from Appraisal_FutureGoal_Recall_tbl A where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " group by Recall_EMPLID,emplid,Goals,Milestones,TargetDate)A "
            'SqlDataSource1.SelectCommand &= " where Guild_Approved not in (select distinct DateEmpl_Appr from Appraisal_FutureGoals_Master_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ") "
            SqlDataSource1.SelectCommand = " select distinct (select last+' '+first from id_tbl where emplid=A.Recall_EMPLID)Created,Recall_Emplid,A.emplid,A.Goals,A.Milestones,A.TargetDate,Recall_Date"
            SqlDataSource1.SelectCommand &= " from Appraisal_FutureGoal_Recall_tbl A LEFT JOIN Appraisal_FutureGoals_TBL B ON Recall_EMPLID=MGT_EMPLID and A.Goals=B.Goals and A.Milestones=B.Milestones"
            SqlDataSource1.SelectCommand &= " and A.TargetDate=B.TargetDate where A.emplid=" & lblEMPLID.Text & " and A.Perf_Year=" & lblYear.Text & " and B.emplid is NULL order by Recall_Date desc"
            'Response.Write(SqlDataSource1.SelectCommand) : Response.End()
            SQL1 = "select count(*)CNT from Appraisal_FutureGoal_Recall_tbl A LEFT JOIN Appraisal_FutureGoals_TBL B ON Recall_EMPLID=MGT_EMPLID and A.Goals=B.Goals and A.Milestones=B.Milestones "
            SQL1 &= " and A.TargetDate=B.TargetDate where A.emplid=" & lblEMPLID.Text & " and A.Perf_Year=" & lblYear.Text & " and B.emplid is NULL"
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            If CDbl(DT1.Rows(0)("CNT").ToString) > 0 Then
                Panel_Goals_Log.Visible = True
            Else
                Panel_Goals_Log.Visible = False
            End If
        Else
            Panel_Goals_Log.Visible = False
        End If

        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If CDbl(Goal_Year.Text) = "17" Then
            'divOldTitle.Visible = True : divNewtext.Visible = False : divNewTitle.Visible = False
            'divGoalText.Visible = False : divNewKey.Visible = False : divNewtarget.Visible = False
            'GridView1.Columns(3).HeaderText = "Success Measures or Milestones"
            'GridView1.HeaderRow.Cells(3).Text = "Success Measures or Milestones"
        Else
            'divOldTitle.Visible = False : divNewtext.Visible = True : divNewTitle.Visible = True
            'divGoalText.Visible = True : divNewKey.Visible = True : divNewtarget.Visible = True
            'GridView1.Columns(3).HeaderText = "Key Results"
            'GridView1.HeaderRow.Cells(3).Text = "Key Results"
        End If

    End Sub
End Class