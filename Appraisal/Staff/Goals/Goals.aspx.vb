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
Public Class Goals
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

        lblEMPLID.Text = Mid(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9"), 5, 4)

        lblLogin_EMPLID.Text = Trim(Session("MGT_EMPLID"))

        If Len(lblLogin_EMPLID.Text) = 0 Then
            lblLogin_EMPLID.Text = Trim(Session("HR_EMPLID"))
        End If

        lblYear.Text = Left(Request.QueryString("Token"), 4)
        lblWindowBatch.Text = Mid(Request.QueryString("Token"), 9)
        lblDataBaseBatch.Text = Session("Window_batch")

        'Response.Write("LEN Mgt Emplid : " & Len(lblLogin_EMPLID.Text) & "<br>Mgt Emplid :  " & lblLogin_EMPLID.Text & "<br>Emp Emplid: " & lblEMPLID.Text) ': Response.End()


        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display") ': Response.End()
        Else
            'Response.Write("<br>2. else Save Data before Display") ': Response.End()
            DisplayData()
            'WindowBatch()
        End If
        Goals_Log()
        SetLevel_Approval()
        ShowButtonCursor()
        '--Process Flag---
        '  0--First Manager 
        '  1--Second Manager
        '  2--HR Generalist
        '  3--First Manager - Approved by HR
        '  4--Waiting to send employee
        '  5--Employee Reviewed and confirmed

        '-------TABLES USED THIS FORM-------------
        '1. Appraisal_FutureGoals_Master_tbl
        '2. Appraisal_FutureGoals_tbl
        '4. Appraisal_FutureGoal_Recall_tbl

        'CHR(10) -- Line feed        replace to <br>
        'CHR(13) -- Carriage return  replace to <span>  

        'WindowClosed()
        'Response.Write("MGT " & lblLogin_EMPLID.Text & "<br>  EMP " & lblEMPLID.Text)
        'If Len(lblLogin_EMPLID.Text) = 4 Then
        'WindowBatch()
        'End If

    End Sub
    Protected Sub WindowBatch()
        SQL11 = "select Window_batch from Appraisal_FutureGoals_Master_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year= " & lblYear.Text
        'Response.Write(SQL11)
        DT11 = LocalClass.ExecuteSQLDataSet(SQL11)
        Session("Window_batch") = CDbl(DT11.Rows(0)("Window_batch").ToString)
        LocalClass.CloseSQLServerConnection()
        If Session("Window_batch") - CDbl(lblWindowBatch.Text) = 0 Then SaveData()

    End Sub

    Protected Sub WindowClosed()
        '--Close window on button click
        'Generalist.Attributes.Add("onclick", "window.close();")
        'Approve.Attributes.Add("onclick", "window.close();")
        'Discuss.Attributes.Add("onclick", "window.close();")
        'Empl_Review.Attributes.Add("onclick", "window.close();")
    End Sub

    Protected Sub SetLevel_Approval()
        SQL1 = "select * from(select count(*)CNT_InAppr from Appraisal_Master_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text - 1 & ")A,"
        SQL1 &= " (select (select count(*) from Appraisal_FutureGoals_Master_tbl A where mgt_emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & ")Manager,*,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.emplid)Empl_Name,(select first+' '+last from id_tbl where emplid=a.MGT_EMPLID)MGT_Name,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.HR_EMPLID)HR_Name,"
        SQL1 &= " (select convert(char(10),hire_Date,101) from id_tbl where emplid=a.emplid)Empl_Hired,"
        SQL1 &= " (select email from id_tbl where emplid=a.emplid)Empl_Email,(select email from id_tbl where emplid=a.MGT_EMPLID)MGT_Email,"
        SQL1 &= " (select email from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Email,(select email from id_tbl where emplid=a.HR_EMPLID)HR_Email "
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
        lblHR_NAME.Text = DT1.Rows(0)("HR_Name").ToString
        lblHR_EMPLID.Text = DT1.Rows(0)("HR_EMPLID").ToString
        lblHR_EMAIL.Text = DT1.Rows(0)("HR_email").ToString
        '--Goal Years--
        Goal_Year.Text = Trim(Right(lblYear.Text, 2))
        Goal_Year1.Text = Trim(Right(lblYear.Text - 1, 2))
        Goal_Year2.Text = Trim(Right(lblYear.Text, 2))

        If CDbl(Goal_Year.Text) = "17" Then
            divOldTitle.Visible = True : divNewtext.Visible = False : divNewTitle.Visible = False
            divOldTitle1.Visible = True : divNewTitle1.Visible = False
            divGoalText.Visible = False : divGoalText1.Visible = False : divNewKey1.Visible = False
            divNewKey.Visible = False : divNewtarget.Visible = False : divNewtarget1.Visible = False
            GridView1.Columns(2).HeaderText = "Success Measures or Milestones"
        Else
            divOldTitle.Visible = False : divNewtext.Visible = True : divNewTitle.Visible = True
            divOldTitle1.Visible = False : divNewTitle1.Visible = True
            divGoalText.Visible = True : divGoalText1.Visible = True : divNewKey1.Visible = True
            divNewKey.Visible = True : divNewtarget.Visible = True : divNewtarget1.Visible = True
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
            Goal_Setting_Edit.Visible = True : Goal_Setting_Edit1.Visible = True : btnSave.Visible = True
            'BtnSubmit_UpperMgr.Text = "Send to " & lblUP_MGT_NAME.Text
            'Response.Write(lblHR_EMPLID.Text & "<br>" & lblEmpl_NAME.Text)
            If CDbl(lblHR_EMPLID.Text) = 6210 Then
                BtnSubmit_UpperMgr.Text = "Send to " & lblEmpl_NAME.Text
            Else
                BtnSubmit_UpperMgr.Text = "Send to " & lblHR_NAME.Text
            End If

            Goal_Setting_Review.Visible = False : Goal_Discussion.Visible = False
            BtnSubmit_UpperMgr.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : BtnSubmit_UpperMgr.Attributes.Add("onmouseout", "this.style.color='#000000'")
            btnSave.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave.Attributes.Add("onmouseout", "this.style.color='#000000'")
            btnSave2.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave2.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 1 Then
            Generalist.Visible = True : Approve.Visible = False : Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = True : LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = "Waiting Approval" : LblUP_MGT_Appr.ForeColor = Drawing.Color.Blue : Generalist.Text = "Submit for review to " & lblHR_NAME.Text
            Generalist.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Generalist.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 2 Then
            Generalist.Visible = False : Approve.Visible = True : Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = True : LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = "Waiting Approval" : LblHR_Appr.ForeColor = Drawing.Color.Blue
            Approve.Text = "Approve" : Approve.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Approve.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 3 And Len(lblLogin_EMPLID.Text) > 0 Then
            'Response.Write(lblHR_EMPLID.Text & "<br>" & lblLogin_EMPLID.Text) : Response.End()
            Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString
            If Trim(lblHR_EMPLID.Text) <> Trim(lblLogin_EMPLID.Text) Then btnSub_Empl.Visible = True Else btnSub_Empl.Visible = False
            btnSub_Empl.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSub_Empl.Attributes.Add("onmouseout", "this.style.color='#000000'")

        ElseIf lblFlag.Text = 3 And Len(lblLogin_EMPLID.Text) = 0 Then
            'Response.Write("2  ddggdgdgdgdgdg  ") : Response.End()
            Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString
            btnSub_Empl.Visible = False



        ElseIf lblFlag.Text = 4 And Len(lblLogin_EMPLID.Text) <> 0 Then
            'Response.Write("1. No submit button" & DT1.Rows(0)("CNT_InAppr").ToString & "<br>" & DT1.Rows(0)("MGT_emplid").ToString & "<br>" & lblEMPLID.Text)

            Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString
            btnSub_Empl.Visible = False : Sub_Empl_Review.Visible = True : Sub_Empl_Review.Text = "Submitted for Review on " & DT1.Rows(0)("DateSUB_Empl").ToString
            LblEMP_Appr.Text = "Waiting Approval" : LblEMP_Appr.ForeColor = Drawing.Color.Blue

            If CDbl(DT1.Rows(0)("CNT_InAppr").ToString) = 0 And CDbl(DT1.Rows(0)("MGT_emplid").ToString) <> CDbl(DT1.Rows(0)("Login_Mgt_Emp").ToString) Then
                'Response.Write("LEN Mgt Emplid : " & Len(lblLogin_EMPLID.Text) & "<br>" & DT1.Rows(0)("CNT_InAppr").ToString & "<br>" & DT1.Rows(0)("emplid").ToString & "<br>" & lblEMPLID.Text)
                Empl_Review.Visible = True
            Else
                Empl_Review.Visible = False
            End If


        ElseIf lblFlag.Text = 4 And Len(lblLogin_EMPLID.Text) = 0 Then
            'Response.Write("2. No submit button")
            Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString
            btnSub_Empl.Visible = False : Sub_Empl_Review.Visible = True : Sub_Empl_Review.Text = "Submitted for Review on " & DT1.Rows(0)("DateSUB_Empl").ToString
            Empl_Review.Visible = True : Empl_Review.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Empl_Review.Attributes.Add("onmouseout", "this.style.color='#000000'")
            LblEMP_Appr.Text = "Waiting Approval" : LblEMP_Appr.ForeColor = Drawing.Color.Blue


            '--Employee review and confirm--
            ' ElseIf lblFlag.Text = 5 And Len(lblLogin_EMPLID.Text) = 0 Then

        ElseIf lblFlag.Text = 5 And Len(lblLogin_EMPLID.Text) = 0 Then ' Employee's view
            Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : Sub_Empl_Review.Visible = True
            LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString : LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString : btnSub_Empl.Visible = False
            If Session("GoalUpdated") = 1 Then
                Empl_Review.Visible = True
            Else
                Empl_Review.Visible = False
                Sub_Empl_Review.Text = "Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
            End If

            '--Manager after Employee Approved--
        ElseIf lblFlag.Text = 5 And Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) <> 0) Then ' Manager's view
            'ElseIf lblFlag.Text = 5 And CDbl(lblEMPLID.Text) - CDbl(DT1.Rows(0)("EMPLID").ToString) <> 0 And Len(lblLogin_EMPLID.Text) <> 0 Then

            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString
            LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString : btnSub_Empl.Visible = False : Sub_Empl_Review.Visible = True : Empl_Review.Visible = False : BtnSubmit_UpperMgr.Visible = False
            'btnSave1.Text = "Save and Submit for review to " & lblEmpl_NAME.Text 
            btnSave1.Text = "Submit for review to " & lblEmpl_NAME.Text

            If Session("GoalUpdated") = 1 Then
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = True : btnSave1.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
                Sub_Empl_Review.Text = "Waiting Confirmation from " & lblEmpl_NAME.Text : Sub_Empl_Review.Font.Size = 16 : Panel_Goals_Log.Visible = False
            Else
                Goal_Setting_Edit.Visible = True : Goal_Setting_Edit.Visible = True : Goal_Setting_Edit1.Visible = True : btnSave.Visible = True
                Goal_Setting_Review.Visible = False : Goal_Discussion.Visible = False : btnSave1.Visible = True
                Sub_Empl_Review.Text = "<br>" & lblEmpl_NAME.Text & " Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString & ". <br>If changes need to be made to the goals, please make edits on the form, and used the save and submit button above."
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString : Empl_Review.Visible = False
            End If

        ElseIf lblFlag.Text = 5 And Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) = 0) Then ' manager's view
            Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
            LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : LblUP_MGT_Appr.Text = DT1.Rows(0)("DateSUB_HR").ToString : Sub_Empl_Review.Visible = True
            LblHR_Appr.Text = DT1.Rows(0)("DateHR_Appr").ToString : LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString : btnSub_Empl.Visible = False

            If Session("GoalUpdated") = 1 Then
                Empl_Review.Visible = True
            Else
                Empl_Review.Visible = False
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

        Delete_Goal2.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal3.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal4.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal5.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal6.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal7.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal8.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal9.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal10.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : btnNew_Goal.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")

        BtnSubmit_UpperMgr.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : BtnSubmit_UpperMgr.Attributes.Add("onmouseout", "this.style.color='#000000'")
        btnSave.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave.Attributes.Add("onmouseout", "this.style.color='#000000'")
        btnSave1.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave1.Attributes.Add("onmouseout", "this.style.color='#000000'")
        btnSave2.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave2.Attributes.Add("onmouseout", "this.style.color='#000000'")

        Discuss.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Discuss.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Empl_Review.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Empl_Review.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Generalist.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Generalist.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Approve.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Approve.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Discuss.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Discuss.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Empl_Review.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Empl_Review.Attributes.Add("onmouseout", "this.style.color='#000000'")


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

    Protected Sub btnNew_Goal_Click(sender As Object, e As EventArgs) Handles btnNew_Goal.Click
        SQL = "select count(*)CNT_FUT from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " "
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        If CDbl(DT.Rows(0)("CNT_FUT").ToString) = 0 Then
            SQL3 = " insert into Appraisal_FutureGoals_tbl (EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate,DateEmpl_Appr,MGT_EMPLID) values(" & lblEMPLID.Text & "," & lblYear.Text & ",1,1,'','','',NULL,'')"
            'Response.Write(SQL3) : Response.End()
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()
        Else
            SQL2 = "select Max(IndexId)+1 NewIndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " "
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
            SQL4 &= " insert into Appraisal_FutureGoals_tbl (EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate,DateEmpl_Appr,MGT_EMPLID) values"
            SQL4 &= " (" & lblEMPLID.Text & ", " & lblYear.Text & " ," & DT2.Rows(0)("NewIndexID").ToString & ",1,'','','',NULL,'')"
            'Response.Write(SQL2 & "<br>" & SQL4) : Response.End()
            DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
            LocalClass.CloseSQLServerConnection()

            SQL3 = "select Max(IndexId) NextIndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " "
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()

            If CDbl(DT3.Rows(0)("NextIndexID").ToString) > 10 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('A maximum of 10 Goals has been exceeded.'); </script>")
                SQL2 &= "delete Appraisal_FutureGoals_tbl where IndexID>10"
                DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
                LocalClass.CloseSQLServerConnection()
            Else
                ShowHideGoalPanel()
            End If
        End If
    End Sub

    Protected Sub Delete_Goal2_Click(sender As Object, e As EventArgs) Handles Delete_Goal2.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=2 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexID>2 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and IndexId=2"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideGoalPanel()
        Else
            DisplayData()
            ShowHideGoalPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Goal3_Click(sender As Object, e As EventArgs) Handles Delete_Goal3.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=3 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexID>3 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and IndexId=3"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideGoalPanel()
        Else
            DisplayData()
            ShowHideGoalPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Goal4_Click(sender As Object, e As EventArgs) Handles Delete_Goal4.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=4 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexID>4 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and IndexId=4"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideGoalPanel()
        Else
            DisplayData()
            ShowHideGoalPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Goal5_Click(sender As Object, e As EventArgs) Handles Delete_Goal5.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=5 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexID>5 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and IndexId=5"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideGoalPanel()
        Else
            DisplayData()
            ShowHideGoalPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Goal6_Click(sender As Object, e As EventArgs) Handles Delete_Goal6.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=6 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexID>6 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and IndexId=6"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideGoalPanel()
        Else
            DisplayData()
            ShowHideGoalPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Goal7_Click(sender As Object, e As EventArgs) Handles Delete_Goal7.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=7 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexID>7 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and IndexId=7"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideGoalPanel()
        Else
            DisplayData()
            ShowHideGoalPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Goal8_Click(sender As Object, e As EventArgs) Handles Delete_Goal8.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=8 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexID>8 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and IndexId=8"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideGoalPanel()
        Else
            DisplayData()
            ShowHideGoalPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Goal9_Click(sender As Object, e As EventArgs) Handles Delete_Goal9.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=9 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexID>9 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and IndexId=9"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideGoalPanel()
        Else
            DisplayData()
            ShowHideGoalPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Goal10_Click(sender As Object, e As EventArgs) Handles Delete_Goal10.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=10 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexID>10 and Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and IndexId=10"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideGoalPanel()
        Else
            DisplayData()
            ShowHideGoalPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub

    Protected Sub BtnSubmit_UpperMgr_Click(sender As Object, e As EventArgs) Handles BtnSubmit_UpperMgr.Click
        SaveData()
        AllRules()
    End Sub

    Protected Sub AllRules()

        Dim Flag As Integer = 0

        SQL = "select * from"
        SQL &= "(select count(*)Fut1 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1)B1,"
        SQL &= "(select count(*)Fut2 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2)B2,"
        SQL &= "(select count(*)Fut3 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3)B3,"
        SQL &= "(select count(*)Fut4 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4)B4,"
        SQL &= "(select count(*)Fut5 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5)B5,"
        SQL &= "(select count(*)Fut6 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6)B6,"
        SQL &= "(select count(*)Fut7 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7)B7,"
        SQL &= "(select count(*)Fut8 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8)B8,"
        SQL &= "(select count(*)Fut9 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9)B9,"
        SQL &= "(select count(*)Fut10 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10)B10"
        DT = LocalClass.ExecuteSQLDataSet(SQL)


        '---8. FUTUREGOALS TABLE---
        '----------A. IndexID=1----------
        If CDbl(DT.Rows(0)("Fut1").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals1,len(Milestones)Milestones1,len(TargetDate)TargetDate1 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals1").ToString) < 5 Then txbGoal1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones1").ToString) < 5 Then txbSuccess1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate1").ToString) < 2 Then txbDate1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate1.BackColor = Drawing.Color.White
        End If
        '----------B. IndexID=2----------
        If CDbl(DT.Rows(0)("Fut2").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals2,len(Milestones)Milestones2,len(TargetDate)TargetDate2 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals2").ToString) < 5 Then txbGoal2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones2").ToString) < 5 Then txbSuccess2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate2").ToString) < 2 Then txbDate2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate2.BackColor = Drawing.Color.White
        End If
        '----------C. IndexID=3----------
        If CDbl(DT.Rows(0)("Fut3").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals3,len(Milestones)Milestones3,len(TargetDate)TargetDate3 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals3").ToString) < 5 Then txbGoal3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones3").ToString) < 5 Then txbSuccess3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate3").ToString) < 2 Then txbDate3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate3.BackColor = Drawing.Color.White
        End If
        '----------D. IndexID=4----------
        If CDbl(DT.Rows(0)("Fut4").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals4,len(Milestones)Milestones4,len(TargetDate)TargetDate4 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals4").ToString) < 5 Then txbGoal4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones4").ToString) < 5 Then txbSuccess4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate4").ToString) < 2 Then txbDate4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate4.BackColor = Drawing.Color.White
        End If
        '----------E. IndexID=5----------
        If CDbl(DT.Rows(0)("Fut5").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals5,len(Milestones)Milestones5,len(TargetDate)TargetDate5 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals5").ToString) < 5 Then txbGoal5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones5").ToString) < 5 Then txbSuccess5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate5").ToString) < 2 Then txbDate5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate5.BackColor = Drawing.Color.White
        End If
        '----------F. IndexID=6----------
        If CDbl(DT.Rows(0)("Fut6").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals6,len(Milestones)Milestones6,len(TargetDate)TargetDate6 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals6").ToString) < 5 Then txbGoal6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones6").ToString) < 5 Then txbSuccess6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate6").ToString) < 2 Then txbDate6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate6.BackColor = Drawing.Color.White
        End If
        '----------G. IndexID=7----------
        If CDbl(DT.Rows(0)("Fut7").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals7,len(Milestones)Milestones7,len(TargetDate)TargetDate7 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals7").ToString) < 5 Then txbGoal7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones7").ToString) < 5 Then txbSuccess7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate7").ToString) < 2 Then txbDate7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate7.BackColor = Drawing.Color.White
        End If
        '----------H. IndexID=8----------
        If CDbl(DT.Rows(0)("Fut8").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals8,len(Milestones)Milestones8,len(TargetDate)TargetDate8 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals8").ToString) < 5 Then txbGoal8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones8").ToString) < 5 Then txbSuccess8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate8").ToString) < 2 Then txbDate8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate8.BackColor = Drawing.Color.White
        End If
        '----------I. IndexID=9----------
        If CDbl(DT.Rows(0)("Fut9").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals9,len(Milestones)Milestones9,len(TargetDate)TargetDate9 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals9").ToString) < 5 Then txbGoal9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones9").ToString) < 5 Then txbSuccess9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate9").ToString) < 2 Then txbDate9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate9.BackColor = Drawing.Color.White
        End If
        '----------K. IndexID=10----------
        If CDbl(DT.Rows(0)("Fut10").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals10,len(Milestones)Milestones10,len(TargetDate)TargetDate10 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals10").ToString) < 5 Then txbGoal10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal10.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones10").ToString) < 5 Then txbSuccess10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess10.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate10").ToString) < 2 Then txbDate10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate10.BackColor = Drawing.Color.White
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
        SQL7 = " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal1.Text, "'", "`"), "<", "{"), ">", "}") & "', Milestones='" & Replace(Replace(Replace(txbSuccess1.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= "TargetDate='" & Replace(Replace(Replace(txbDate1.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=1 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal2.Text, "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(txbSuccess2.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(txbDate2.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=2 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal3.Text, "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(txbSuccess3.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(txbDate3.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=3 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal4.Text, "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(txbSuccess4.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(txbDate4.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=4 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal5.Text, "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(txbSuccess5.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(txbDate5.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=5 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal6.Text, "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(txbSuccess6.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(txbDate6.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=6 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal7.Text, "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(txbSuccess7.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(txbDate7.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=7 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal8.Text, "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(txbSuccess8.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(txbDate8.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=8 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal9.Text, "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(txbSuccess9.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(txbDate9.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=9 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(txbGoal10.Text, "'", "`"), "<", "{"), ">", "}") & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(txbSuccess10.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(txbDate10.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=10 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        'Response.Write(SQL7) ': Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL7)
        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        WindowBatch()
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SaveData()
            lblMessage1.Visible = True
            lblMessage1.Text = "Data Saved"
            lblMessage.Visible = True
            lblMessage.Text = "Data Saved"
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('You have the identical form opened in another browser window.\n\nTo avoid confusion, you are not be permitted to enter information\n in this older version of the form.');</script>")
        End If

    End Sub
    Protected Sub btnSave2_Click(sender As Object, e As EventArgs) Handles btnSave2.Click
        WindowBatch()
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SaveData()
            lblMessage1.Visible = True
            lblMessage1.Text = "Data Saved"
            lblMessage.Visible = True
            lblMessage.Text = "Data Saved"
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('You have the identical form opened in another browser window.\nThe Text will not save because you have another  form opened in somewhere else.');</script>")
        End If


    End Sub


    Protected Sub DisplayData()

        ShowHideGoalPanel()

        '--Future Goals table

        'SQL = "select (select first+' '+last from id_tbl where emplid=bb.hr_emplid)HR_NAME,* from(select (select MGT_Emplid from Appraisal_FutureGoals_Master_TBL where Perf_Year=" & lblYear.Text & " "
        'SQL &= " and emplid=" & lblEMPLID.Text & ")MGT_Emplid,(select HR_Emplid from Appraisal_FutureGoals_Master_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")HR_Emplid, * from("
        'SQL &= " select *,(select IsNull(Comments,'') from Appraisal_FutureGoals_Master_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")Comments from"
        'SQL &= " (select Max(Goals1)Goals1,Max(Milestones1)Milestones1,Max(TargetDate1)TargetDate1,Max(Goals2)Goals2,Max(Milestones2)Milestones2,Max(TargetDate2)TargetDate2,Max(Goals3)Goals3,Max(Milestones3)Milestones3,"
        'SQL &= " Max(TargetDate3)TargetDate3,Max(Goals4)Goals4,Max(Milestones4)Milestones4,Max(TargetDate4)TargetDate4,Max(Goals5)Goals5,Max(Milestones5)Milestones5,Max(TargetDate5)TargetDate5,Max(Goals6)Goals6,"
        'SQL &= " Max(Milestones6)Milestones6,Max(TargetDate6)TargetDate6,Max(Goals7)Goals7,Max(Milestones7)Milestones7,Max(TargetDate7)TargetDate7,Max(Goals8)Goals8,Max(Milestones8)Milestones8,Max(TargetDate8)TargetDate8,"
        'SQL &= " Max(Goals9)Goals9,Max(Milestones9)Milestones9,Max(TargetDate9)TargetDate9,Max(Goals10)Goals10,Max(Milestones10)Milestones10,Max(TargetDate10)TargetDate10 from(select "
        'SQL &= " (case when IndexID=1 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals1,"
        'SQL &= " (case when IndexID=1 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones1,"
        'SQL &= " (case when IndexID=1 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate1,"
        'SQL &= " (case when IndexID=2 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals2,"
        'SQL &= " (case when IndexID=2 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones2,"
        'SQL &= " (case when IndexID=2 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate2,"
        'SQL &= " (case when IndexID=3 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals3,"
        'SQL &= " (case when IndexID=3 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones3,"
        'SQL &= " (case when IndexID=3 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate3,"
        'SQL &= " (case when IndexID=4 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals4,"
        'SQL &= " (case when IndexID=4 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones4,"
        'SQL &= " (case when IndexID=4 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate4,"
        'SQL &= " (case when IndexID=5 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals5,"
        'SQL &= " (case when IndexID=5 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones5,"
        'SQL &= " (case when IndexID=5 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate5,"
        'SQL &= " (case when IndexID=6 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals6,"
        'SQL &= " (case when IndexID=6 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones6,"
        'SQL &= " (case when IndexID=6 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate6,"
        'SQL &= " (case when IndexID=7 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals7,"
        'SQL &= " (case when IndexID=7 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones7,"
        'SQL &= " (case when IndexID=7 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate7,"
        'SQL &= " (case when IndexID=8 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals8,"
        'SQL &= " (case when IndexID=8 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones8,"
        'SQL &= " (case when IndexID=8 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate8,"
        'SQL &= " (case when IndexID=9 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals9,"
        'SQL &= " (case when IndexID=9 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones9,"
        'SQL &= " (case when IndexID=9 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate9,"
        'SQL &= " (case when IndexID=10 then Replace(Replace(Rtrim(Ltrim(Goals)), Char(13), ''), Char(10), '') else '' end)Goals10,"
        'SQL &= " (case when IndexID=10 then Replace(Replace(Rtrim(Ltrim(Milestones)), Char(13), ''), Char(10), '') else '' end)Milestones10,"
        'SQL &= " (case when IndexID=10 then Replace(Replace(Rtrim(Ltrim(TargetDate)), Char(13), ''), Char(10), '') else '' end)TargetDate10"
        'SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")A )C)AA)BB"

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
        txbGoal1.Text = Replace(Replace(Replace(DT.Rows(0)("Goals1").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess1.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones1").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate1.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate1").ToString, "{", "<"), "}", ">"), "`", "'")
        txbGoal2.Text = Replace(Replace(Replace(DT.Rows(0)("Goals2").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess2.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones2").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate2.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate2").ToString, "{", "<"), "}", ">"), "`", "'")
        txbGoal3.Text = Replace(Replace(Replace(DT.Rows(0)("Goals3").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess3.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones3").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate3.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate3").ToString, "{", "<"), "}", ">"), "`", "'")
        txbGoal4.Text = Replace(Replace(Replace(DT.Rows(0)("Goals4").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess4.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones4").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate4.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate4").ToString, "{", "<"), "}", ">"), "`", "'")
        txbGoal5.Text = Replace(Replace(Replace(DT.Rows(0)("Goals5").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess5.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones5").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate5.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate5").ToString, "{", "<"), "}", ">"), "`", "'")
        txbGoal6.Text = Replace(Replace(Replace(DT.Rows(0)("Goals6").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess6.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones6").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate6.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate6").ToString, "{", "<"), "}", ">"), "`", "'")
        txbGoal7.Text = Replace(Replace(Replace(DT.Rows(0)("Goals7").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess7.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones7").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate7.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate7").ToString, "{", "<"), "}", ">"), "`", "'")
        txbGoal8.Text = Replace(Replace(Replace(DT.Rows(0)("Goals8").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess8.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones8").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate8.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate8").ToString, "{", "<"), "}", ">"), "`", "'")
        txbGoal9.Text = Replace(Replace(Replace(DT.Rows(0)("Goals9").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess9.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones9").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate9.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate9").ToString, "{", "<"), "}", ">"), "`", "'")
        txbGoal10.Text = Replace(Replace(Replace(DT.Rows(0)("Goals10").ToString, "{", "<"), "}", ">"), "`", "'")
        txbSuccess10.Text = Replace(Replace(Replace(DT.Rows(0)("Milestones10").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDate10.Text = Replace(Replace(Replace(DT.Rows(0)("TargetDate10").ToString, "{", "<"), "}", ">"), "`", "'")

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

        If lblLogin_EMPLID.Text = DT.Rows(0)("MGT_Emplid").ToString Then
            If Len(DT.Rows(0)("Comments").ToString) > 5 Then
                ReviseComments.Visible = True
                lblComments.Text = "<b>" & DT.Rows(0)("HR_NAME").ToString & " Comments:</b><font color=blue>" & Replace(Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<span>"), Chr(10), "<br>") & " </font>"
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

        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub SendToUpperManager()

        If CDbl(lblHR_EMPLID.Text) = 6210 Then

            SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=5,DateSUB_Empl='" & Now & "',Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            Msg = "Your Goals have been edited and sent to you for your review and confirm.<br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details.<br>"

            '--Production Email
            'LocalClass.SendMail(lblEmpl_EMAIL.Text, "Your Future Goal(s) was updated by " & LblMGT_NAME.Text, Msg)

            LocalClass.CloseSQLServerConnection()

        Else
            SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=2,DateSUB_UP_MGT='" & Now & "',DateSUB_HR='" & Now & "',Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            Msg = lblEmpl_NAME.Text & "'s Goals have been sent to you for review by " & LblMGT_NAME.Text & "<br><br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details."
            '--Production Email
            'LocalClass.SendMail(lblHR_EMAIL.Text, "Employee Future Goal(s) was sent to you for Approval by " & Session("FIRST") & " " & Session("Last"), Msg)

            LocalClass.CloseSQLServerConnection()

        End If

        Response.Redirect("FutureGoal.aspx?Token=" & Request.QueryString("Token"))
    End Sub

    Protected Sub Discuss_Click(sender As Object, e As EventArgs) Handles Discuss.Click

        If Len(DiscussionComments.Text) < 5 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please type discussion comments'); </script>")
        Else
            SQL = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=0,Comments='" & Replace(Replace(Replace(DiscussionComments.Text, "'", "`"), "<", "{"), ">", "}") & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            SQL1 = "select first+' '+ last Name from id_tbl where emplid=" & lblHR_EMPLID.Text
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            Msg = DT1.Rows(0)("Name").ToString & " has sent back " & lblEmpl_NAME.Text & "'s Goals for the following reason:<br><br>"
            Msg &= DiscussionComments.Text & " <br><br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details."
            Msg &= "<br><br>email to " & lblFirst_SUP_EMAIL.Text
            Msg1 = DT1.Rows(0)("Name").ToString & " has sent back " & lblEmpl_NAME.Text & "'s Goals for the following reason:<br><br>"
            Msg1 &= DiscussionComments.Text & ""

            '--Production email
            'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Employee Future Goal(s) was commented by " & DT1.Rows(0)("Name").ToString, Msg1)

            'Response.Redirect("FutureGoal.aspx?Token=" & Request.QueryString("Token"))

            Generalist.Visible = False : Approve.Visible = True : Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
            Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = False : LblHR_Appr.Text = "Rejected" : LblHR_Appr.ForeColor = Drawing.Color.Blue

            LocalClass.CloseSQLServerConnection()
        End If

    End Sub
    Protected Sub Generalist_Click(sender As Object, e As EventArgs) Handles Generalist.Click
        '--Submit for review to HR generalist
        SQL1 = "update Appraisal_FutureGoals_Master_tbl Set Process_Flag=2,DateSUB_HR='" & Now & "', Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblEmpl_NAME.Text & "'s Goals have been sent to you for review by " & lblUP_MGT_NAME.Text & "<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details. "

        '--Production Email--
        'LocalClass.SendMail(lblHR_EMAIL.Text, "Employee Future Goal(s) was sent to you for Approval by " & Session("FIRST") & " " & Session("Last"), Msg)

        Response.Redirect("FutureGoal.aspx?Token=" & Request.QueryString("Token"))

    End Sub

    Protected Sub Approve_Click(sender As Object, e As EventArgs) Handles Approve.Click
        '--Generalist approved--- 

        SQL1 = "update Appraisal_FutureGoals_Master_tbl Set Process_Flag=3,DateHR_Appr='" & Now & "', Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblEmpl_NAME.Text & "'s Goals have been approved by " & lblHR_NAME.Text & "<br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details. "
        '--Production Email
        'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Future Goal(s) was sent to you for Approval by " & Session("FIRST") & " " & Session("Last"), Msg)

        'Generalist.Visible = False : Approve.Visible = True : Goal_Setting_Edit.Visible = False : Goal_Setting_Edit1.Visible = False : btnSave.Visible = False
        'Goal_Setting_Review.Visible = True : Goal_Discussion.Visible = True : 
        'Sub_Empl_Review.Visible = False

        Response.Redirect("FutureGoal.aspx?Token=" & Request.QueryString("Token"))

    End Sub

    Protected Sub Sub_Empl_Click(sender As Object, e As EventArgs) Handles btnSub_Empl.Click
        '-- Not in Future Goals form--

        SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=4,DateSUB_Empl='" & Now & "',Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        'Response.Write(SQL) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        Msg = "Your Future Goal(s) have been sent for your review.<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details. "
        LocalClass.CloseSQLServerConnection()
        'Response.Redirect("..\..\Default_Appaisal.aspx")

        Response.Redirect("FutureGoal.aspx?Token=" & Request.QueryString("Token"))

    End Sub

    Protected Sub Empl_Review_Click(sender As Object, e As EventArgs) Handles Empl_Review.Click

        SQL1 = "  Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=5,DateEmpl_Appr='" & Now & "',Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text

        SQL1 &= " Insert into Appraisal_FutureGoal_Recall_tbl "
        SQL1 &= " select EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate, '" & Now & "' DateEmpl_Appr, '" & lblFirst_SUP_EMPLID.Text & "' Recall_EMPLID,'" & Now & "' Recall_Date"
        SQL1 &= " from Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Msg = lblEmpl_NAME.Text & " confirmed review of goals set. <br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details.<br>"

        'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Employee Future Goal(s) Reviewed by " & lblEmpl_NAME.Text, Msg)

        LocalClass.CloseSQLServerConnection()

        Response.Redirect("FutureGoal.aspx?Token=" & Request.QueryString("Token"))

    End Sub

    Protected Sub AllRules1() '--Check rules after Appraisal approved
        Dim Flag As Integer = 0

        SQL = "select * from"
        SQL &= "(select count(*)Fut1 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1)B1,"
        SQL &= "(select count(*)Fut2 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2)B2,"
        SQL &= "(select count(*)Fut3 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3)B3,"
        SQL &= "(select count(*)Fut4 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4)B4,"
        SQL &= "(select count(*)Fut5 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5)B5,"
        SQL &= "(select count(*)Fut6 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6)B6,"
        SQL &= "(select count(*)Fut7 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7)B7,"
        SQL &= "(select count(*)Fut8 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8)B8,"
        SQL &= "(select count(*)Fut9 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9)B9,"
        SQL &= "(select count(*)Fut10 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10)B10"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '---FutureGoals TABLE---
        '----------A. IndexID=1----------
        If CDbl(DT.Rows(0)("Fut1").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals1,len(Milestones)Milestones1,len(TargetDate)TargetDate1 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals1").ToString) < 5 Then txbGoal1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones1").ToString) < 5 Then txbSuccess1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate1").ToString) < 2 Then txbDate1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate1.BackColor = Drawing.Color.White
        End If
        '----------B. IndexID=2----------
        If CDbl(DT.Rows(0)("Fut2").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals2,len(Milestones)Milestones2,len(TargetDate)TargetDate2 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals2").ToString) < 5 Then txbGoal2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones2").ToString) < 5 Then txbSuccess2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate2").ToString) < 2 Then txbDate2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate2.BackColor = Drawing.Color.White
        End If
        '----------C. IndexID=3----------
        If CDbl(DT.Rows(0)("Fut3").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals3,len(Milestones)Milestones3,len(TargetDate)TargetDate3 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals3").ToString) < 5 Then txbGoal3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones3").ToString) < 5 Then txbSuccess3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate3").ToString) < 2 Then txbDate3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate3.BackColor = Drawing.Color.White
        End If
        '----------D. IndexID=4----------
        If CDbl(DT.Rows(0)("Fut4").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals4,len(Milestones)Milestones4,len(TargetDate)TargetDate4 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals4").ToString) < 5 Then txbGoal4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones4").ToString) < 5 Then txbSuccess4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate4").ToString) < 2 Then txbDate4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate4.BackColor = Drawing.Color.White
        End If
        '----------E. IndexID=5----------
        If CDbl(DT.Rows(0)("Fut5").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals5,len(Milestones)Milestones5,len(TargetDate)TargetDate5 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals5").ToString) < 5 Then txbGoal5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones5").ToString) < 5 Then txbSuccess5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate5").ToString) < 2 Then txbDate5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate5.BackColor = Drawing.Color.White
        End If
        '----------F. IndexID=6----------
        If CDbl(DT.Rows(0)("Fut6").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals6,len(Milestones)Milestones6,len(TargetDate)TargetDate6 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals6").ToString) < 5 Then txbGoal6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones6").ToString) < 5 Then txbSuccess6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate6").ToString) < 2 Then txbDate6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate6.BackColor = Drawing.Color.White
        End If
        '----------G. IndexID=7----------
        If CDbl(DT.Rows(0)("Fut7").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals7,len(Milestones)Milestones7,len(TargetDate)TargetDate7 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals7").ToString) < 5 Then txbGoal7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones7").ToString) < 5 Then txbSuccess7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate7").ToString) < 2 Then txbDate7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate7.BackColor = Drawing.Color.White
        End If
        '----------H. IndexID=8----------
        If CDbl(DT.Rows(0)("Fut8").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals8,len(Milestones)Milestones8,len(TargetDate)TargetDate8 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals8").ToString) < 5 Then txbGoal8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones8").ToString) < 5 Then txbSuccess8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate8").ToString) < 2 Then txbDate8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate8.BackColor = Drawing.Color.White
        End If
        '----------I. IndexID=9----------
        If CDbl(DT.Rows(0)("Fut9").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals9,len(Milestones)Milestones9,len(TargetDate)TargetDate9 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals9").ToString) < 5 Then txbGoal9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones9").ToString) < 5 Then txbSuccess9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate9").ToString) < 2 Then txbDate9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate9.BackColor = Drawing.Color.White
        End If
        '----------K. IndexID=10----------
        If CDbl(DT.Rows(0)("Fut10").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals10,len(Milestones)Milestones10,len(TargetDate)TargetDate10 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals10").ToString) < 5 Then txbGoal10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal10.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones10").ToString) < 5 Then txbSuccess10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess10.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate10").ToString) < 2 Then txbDate10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate10.BackColor = Drawing.Color.White
        End If

        If Flag = 0 Then
            SendToEmployee()
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('Please fill yellow highlighted fields');</script>")
            Return
        End If
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub SendToEmployee() '--Update FutureGoals table after Appraisal approved

        SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=5,DateSUB_Empl='" & Now & "',Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Msg = "Your Goals have been edited and sent to you for your review and confirm.<br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details.<br>"


    End Sub

    Protected Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnSave1.Click '--Send to employee after Appraisal approved

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            AllRules1()
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('You have the identical form opened in another browser window.\nThe Text will not save because you have another  form opened in somewhere else.');</script>")
        End If

    End Sub

    Sub Goals_Log()
        SQL = "select * from(select count(*)CNT_FUTGoals from(select * from Appraisal_FutureGoal_Recall_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        SQL &= " and DateEmpl_Appr not in (select distinct DateEmpl_Appr from Appraisal_FutureGoals_Master_tbl where emplid=" & lblEMPLID.Text & "))A)A1,"
        SQL &= "(select count(*)Cycle_Done from Appraisal_FutureGoals_Master_tbl where emplid=" & lblEMPLID.Text & "  and process_flag=5 and Perf_Year=" & lblYear.Text & ")B "
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If CDbl(DT.Rows(0)("CNT_FUTGoals").ToString) > 0 And CDbl(DT.Rows(0)("Cycle_Done").ToString) = 1 Then
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

    Protected Sub Goal1_TextChanged(sender As Object, e As EventArgs) Handles txbGoal1.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal1.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=1"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success1_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess1.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess1.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID=" & lblEMPLID.Text & " and IndexID=1"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date1_TextChanged(sender As Object, e As EventArgs) Handles txbDate1.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate1.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=1"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal2_TextChanged(sender As Object, e As EventArgs) Handles txbGoal2.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal2.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=2"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success2_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess2.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess2.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=2"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date2_TextChanged(sender As Object, e As EventArgs) Handles txbDate2.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate2.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=2"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal3_TextChanged(sender As Object, e As EventArgs) Handles txbGoal3.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal3.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=3"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success3_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess3.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess3.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=3 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date3_TextChanged(sender As Object, e As EventArgs) Handles txbDate3.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate3.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=3 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal4_TextChanged(sender As Object, e As EventArgs) Handles txbGoal4.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal4.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=4"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success4_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess4.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess4.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=4 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date4_TextChanged(sender As Object, e As EventArgs) Handles txbDate4.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate4.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=4 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal5_TextChanged(sender As Object, e As EventArgs) Handles txbGoal5.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal5.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=5"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success5_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess5.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess5.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=5 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date5_TextChanged(sender As Object, e As EventArgs) Handles txbDate5.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate5.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=5 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal6_TextChanged(sender As Object, e As EventArgs) Handles txbGoal6.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal6.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=6"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success6_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess6.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess6.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=6 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date6_TextChanged(sender As Object, e As EventArgs) Handles txbDate6.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate6.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=6 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal7_TextChanged(sender As Object, e As EventArgs) Handles txbGoal7.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal7.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=7"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success7_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess7.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess7.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=7 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date7_TextChanged(sender As Object, e As EventArgs) Handles txbDate7.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate7.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=7 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal8_TextChanged(sender As Object, e As EventArgs) Handles txbGoal8.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal8.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=8"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success8_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess8.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess8.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=8 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date8_TextChanged(sender As Object, e As EventArgs) Handles txbDate8.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate8.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=8 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal9_TextChanged(sender As Object, e As EventArgs) Handles txbGoal9.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal9.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=9"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success9_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess9.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess9.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=9 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date9_TextChanged(sender As Object, e As EventArgs) Handles txbDate9.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate9.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=9 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal10_TextChanged(sender As Object, e As EventArgs) Handles txbGoal10.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(txbGoal10.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=10"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success10_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess10.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(txbSuccess10.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=10 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date10_TextChanged(sender As Object, e As EventArgs) Handles txbDate10.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(txbDate10.Text, "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=10 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub ResetYellowColor()

        SQL14 = "select * from"
        SQL14 &= " (select count(*)Fut1 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1)B1,"
        SQL14 &= " (select count(*)Fut2 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2)B2,"
        SQL14 &= " (select count(*)Fut3 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3)B3,"
        SQL14 &= " (select count(*)Fut4 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4)B4,"
        SQL14 &= " (select count(*)Fut5 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5)B5,"
        SQL14 &= " (select count(*)Fut6 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6)B6,"
        SQL14 &= " (select count(*)Fut7 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7)B7,"
        SQL14 &= " (select count(*)Fut8 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8)B8,"
        SQL14 &= " (select count(*)Fut9 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9)B9,"
        SQL14 &= " (select count(*)Fut10 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10)B10"
        'Response.Write(SQL14) : Response.End()
        DT14 = LocalClass.ExecuteSQLDataSet(SQL14)
        '---4. FUTUREGOALS TABLE---
        If CDbl(DT14.Rows(0)("Fut1").ToString) = 1 Then txbGoal1.BackColor = Drawing.Color.White : txbSuccess1.BackColor = Drawing.Color.White : txbDate1.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Fut2").ToString) = 1 Then txbGoal2.BackColor = Drawing.Color.White : txbSuccess2.BackColor = Drawing.Color.White : txbDate2.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Fut3").ToString) = 1 Then txbGoal3.BackColor = Drawing.Color.White : txbSuccess3.BackColor = Drawing.Color.White : txbDate3.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Fut4").ToString) = 1 Then txbGoal4.BackColor = Drawing.Color.White : txbSuccess4.BackColor = Drawing.Color.White : txbDate4.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Fut5").ToString) = 1 Then txbGoal5.BackColor = Drawing.Color.White : txbSuccess5.BackColor = Drawing.Color.White : txbDate5.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Fut6").ToString) = 1 Then txbGoal6.BackColor = Drawing.Color.White : txbSuccess6.BackColor = Drawing.Color.White : txbDate6.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Fut7").ToString) = 1 Then txbGoal7.BackColor = Drawing.Color.White : txbSuccess7.BackColor = Drawing.Color.White : txbDate7.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Fut8").ToString) = 1 Then txbGoal8.BackColor = Drawing.Color.White : txbSuccess8.BackColor = Drawing.Color.White : txbDate8.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Fut9").ToString) = 1 Then txbGoal9.BackColor = Drawing.Color.White : txbSuccess9.BackColor = Drawing.Color.White : txbDate9.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Fut10").ToString) = 1 Then txbGoal10.BackColor = Drawing.Color.White : txbSuccess10.BackColor = Drawing.Color.White : txbDate10.BackColor = Drawing.Color.White

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If Len(lblLogin_EMPLID.Text) > 0 Then

            If (lblLogin_EMPLID.Text <> lblEMPLID.Text) Then
                WindowBatch()
            End If
        End If

    End Sub

    Protected Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        If Len(lblLogin_EMPLID.Text) > 0 Then

            If (lblLogin_EMPLID.Text <> lblEMPLID.Text) Then
                SQL11 = "select Window_batch from Appraisal_FutureGoals_Master_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year= " & lblYear.Text
                DT11 = LocalClass.ExecuteSQLDataSet(SQL11)
                If lblWindowBatch.Text <> CDbl(DT11.Rows(0)("Window_batch").ToString) Then
                    Response.Redirect("..\..\Default.aspx")
                End If
            End If
        End If

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Protected Sub Img_Print1_Click(sender As Object, e As ImageClickEventArgs) Handles Img_Print1.Click
        Response.Write("<script>window.open('FutureGoals_Print.aspx?Token=" + Request.QueryString("Token") + "','_blank' );</script>")
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