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

        If Len(Session("NETID")) = 0 Then Response.Redirect("..\..\default.aspx")
        'Response.Write("netid " & Session("NETID") & "netid " & Len(Session("NETID")))

        Response.AddHeader("Refresh", "840")

        lblEMPLID.Text = Mid(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9"), 5, 4)
        lblLogin_EMPLID.Text = Trim(Session("MGT_EMPLID"))

        lblYear.Text = Left(Request.QueryString("Token"), 4)
        lblWindowBatch.Text = Mid(Request.QueryString("Token"), 9)
        lblDataBaseBatch.Text = Session("Window_batch")

        'Response.Write("Empl logon : " & Session("EMPLID_LOGON") & "<br>Mgt Logon :  " & lblLogin_EMPLID.Text & "<br>Emp Emplid: " & lblEMPLID.Text & "<br>" & CDbl(Session("Window_batch")) & "<br>" & CDbl(lblWindowBatch.Text)) : Response.End()

        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display") ': Response.End()
        Else
            'Response.Write("<br>2. else Save Data before Display") ': Response.End()
            SetLevel_Approval()
            DisplayData()
            'WindowBatch()
        End If

        SQL1 = "select count(*)CNT from Appraisal_FutureGoal_Recall_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and Len(DateEmpl_Appr)>5 and Len(Recall_Date)>5"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        If CDbl(DT1.Rows(0)("CNT").ToString) > 0 Then
            Panel_History.Visible = True
            Goals_Log()
        Else
            Panel_History.Visible = False
        End If

        SetLevel_Approval()
        ShowButtonCursor()

    End Sub
    Protected Sub WindowBatch()
        SQL11 = "select Window_batch from Appraisal_FutureGoals_Master_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year= " & lblYear.Text
        'Response.Write(SQL11)
        DT11 = LocalClass.ExecuteSQLDataSet(SQL11)
        Session("Window_batch") = CDbl(DT11.Rows(0)("Window_batch").ToString)
        LocalClass.CloseSQLServerConnection()
        If Session("Window_batch") - CDbl(lblWindowBatch.Text) = 0 Then SaveData()

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
        lblHR_NAME.Text = DT1.Rows(0)("HR_Name").ToString
        lblHR_EMPLID.Text = DT1.Rows(0)("HR_EMPLID").ToString
        lblHR_EMAIL.Text = DT1.Rows(0)("HR_email").ToString
        '--Goal Years--
        Goal_Year.Text = Trim(Right(lblYear.Text, 2))
        Goal_Year1.Text = Trim(Right(lblYear.Text - 1, 2))
        Goal_Year2.Text = Trim(Right(lblYear.Text, 2))

        If Len(Session("EMPLID_LOGON")) = 0 Then
            If lblEMPLID.Text = lblLogin_EMPLID.Text Then
                'Response.Write("Manager views his Goal") : Response.End()
                LblEmpl_Type.Text = 2
            End If

            If lblFirst_SUP_EMPLID.Text = lblLogin_EMPLID.Text Then
                'Response.Write("Manager views employee Goal") : Response.End()
                LblEmpl_Type.Text = 1
            End If
        End If

        If Len(Session("EMPLID_LOGON")) > 0 Then
            If lblEMPLID.Text = Session("EMPLID_LOGON") Then
                'Response.Write("Employee views his Goal") : Response.End()
                LblEmpl_Type.Text = 2
            End If
        End If

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
        '---Check if manager approved Goals that ctreated by employee

        If DateTime.Parse(DT1.Rows(0)("DateSUB_Empl").ToString) > DateTime.Parse(DT1.Rows(0)("DateEmpl_Appr").ToString) Then
            Session("GoalUpdated") = 1
        Else
            Session("GoalUpdated") = 0
        End If

        '--Find Creator
        SQL2 = "select top 1 convert(int,emplid)-convert(int,Recall_EMPLID) Creator,emplid,Max(DateEmpl_Appr)DateEmpl_Appr,Recall_EMPLID,Max(Recall_Date)Recall_Date from "
        SQL2 &= " Appraisal_FutureGoal_Recall_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and Len(DateEmpl_Appr)>5 and Len(Recall_Date)>5 "
        SQL2 &= " group by emplid,Recall_EMPLID order by DateEmpl_Appr desc,Recall_Date desc"
        DT2 = LocalClass.ExecuteSQLDataSet(SQL2)

        If DT2.Rows.Count > 0 Then
            If CDbl(DT2.Rows(0)("Creator").ToString) > 0 Then
                Session("Creator") = 1  'employee created
            Else
                Session("Creator") = 0  'manager created  
            End If
        End If

        '--Find out if exempt is manager
        If CDbl(DT1.Rows(0)("Manager").ToString) = 0 Then Session("Manager") = 0 Else Session("Manager") = 1

        'Response.Write("1.Employee " & lblEMPLID.Text & "<br>2.Emp_Logon  " & Session("EMPLID_LOGON") & "<br>3.Mgr  " & lblFirst_SUP_EMPLID.Text & "<br>4.Logon  " & lblLogin_EMPLID.Text) ': Response.End()

        If lblFlag.Text = 0 Then
            If Len(Session("EMPLID_LOGON")) > 0 And lblEMPLID.Text = Session("EMPLID_LOGON") Then '--Employee Create Goals
                'Response.Write("Employee logon by itself") : Response.End()
                Timer1.Enabled = True : Timer2.Enabled = True : BtnSubmit_Mgr.Visible = True
                Goal_Setting_Edit.Visible = True : BtnSubmit_Mgr.Text = "Send to " & LblMGT_NAME.Text
                Goal_Setting_Review.Visible = False : btnSave2.Visible = True
                BtnSubmit_Mgr.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : BtnSubmit_Mgr.Attributes.Add("onmouseout", "this.style.color='#000000'")
            ElseIf Len(Session("EMPLID_LOGON")) = 0 And CDbl(lblEMPLID.Text) = CDbl(lblLogin_EMPLID.Text) Then '--Employee(mgr) Create Goals
                'Response.Write("manager logon by itself who has employee") : Response.End()
                Timer1.Enabled = True : Timer2.Enabled = True : BtnSubmit_Mgr.Visible = True
                Goal_Setting_Edit.Visible = True : BtnSubmit_Mgr.Text = "Send to " & LblMGT_NAME.Text
                Goal_Setting_Review.Visible = False : btnSave2.Visible = True
                BtnSubmit_Mgr.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : BtnSubmit_Mgr.Attributes.Add("onmouseout", "this.style.color='#000000'")
            ElseIf Len(lblEMPLID.Text) > 1 And lblEMPLID.Text <> Session("EMPLID_LOGON") Then 'Manager Logon
                'Response.Write("manager review employee's goals") : Response.End()
                Timer1.Enabled = False : Timer2.Enabled = False
                btnRecall.Visible = True : btnRecall.Text = "Create goals on behalf of employee"
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False
                btnSub_Empl.Visible = False : Sub_Empl_Review.Visible = True
                If Len(DT1.Rows(0)("Goals").ToString) > 0 Then
                    Sub_Empl_Review.Text = lblEmpl_NAME.Text & " not submitted goals for your review yet."
                Else
                    Sub_Empl_Review.Text = lblEmpl_NAME.Text & " not started to create goal(s) yet."
                End If
            End If
        End If

        If lblFlag.Text = 1 Then
            If lblEMPLID.Text = Session("EMPLID_LOGON") Then 'Employee waiting for approval
                Timer1.Enabled = False : Timer2.Enabled = False
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False
                Sub_Empl_Review.Visible = True : BtnSubmit_Mgr.Visible = False
                Sub_Empl_Review.Text = LblMGT_NAME.Text & " is working on your initial goals."

                'ElseIf lblEMPLID.Text <> Session("EMPLID_LOGON") Then ' Manager review, edit and approval
            ElseIf lblFirst_SUP_EMPLID.Text = lblLogin_EMPLID.Text Then ' Manager review, edit and approval
                Timer1.Enabled = True : Timer2.Enabled = True : btnSub_Empl.Text = "Submit to " & lblEmpl_NAME.Text
                Goal_Setting_Edit.Visible = True : Goal_Setting_Review.Visible = False
                btnSub_Empl.Visible = True : btnSub_Empl.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSub_Empl.Attributes.Add("onmouseout", "this.style.color='#000000'")
                Sub_Empl_Review.Visible = True : BtnSubmit_Mgr.Visible = False
                'Sub_Empl_Review.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString & " " & lblEmpl_NAME.Text & "  submitted goal(s) to " & LblMGT_NAME.Text & " for review, edit or approved."
            ElseIf lblEMPLID.Text = lblLogin_EMPLID.Text Then 'Employee waiting for approval (mgr)
                Timer1.Enabled = False : Timer2.Enabled = False
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False
                Sub_Empl_Review.Visible = True : BtnSubmit_Mgr.Visible = False
                Sub_Empl_Review.Text = LblMGT_NAME.Text & " is working on your initial goals."
            End If

        End If


        If lblFlag.Text = 2 Then
            If lblEMPLID.Text = Session("EMPLID_LOGON") Then 'Employee waiting for approval
                Timer1.Enabled = False : Timer2.Enabled = False
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False
                Sub_Empl_Review.Visible = True : BtnSubmit_Mgr.Visible = False
                Sub_Empl_Review.Text = DT1.Rows(0)("DateEmpl_Appr").ToString & " " & lblEmpl_NAME.Text & "  submitted goal(s) to " & LblMGT_NAME.Text & " for review, edit and approved."
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString

            ElseIf lblFirst_SUP_EMPLID.Text = lblLogin_EMPLID.Text Then ' Manager review, edit and approval
                Timer1.Enabled = False : Timer2.Enabled = False
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False
                Sub_Empl_Review.Visible = True : BtnSubmit_Mgr.Visible = False
                Sub_Empl_Review.Text = DT1.Rows(0)("DateEmpl_Appr").ToString & " " & lblEmpl_NAME.Text & "  submitted goal(s) to " & LblMGT_NAME.Text & " for review, edit and approved."
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
                Edit_Goals.Visible = True : Edit_Goals.Text = "Edit & Approve " & lblEmpl_NAME.Text & "'s Goals"
                Edit_Goals.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Edit_Goals.Attributes.Add("onmouseout", "this.style.color='#000000'")
                Esign_Goals.Visible = True : Esign_Goals.Text = "Approve " & lblEmpl_NAME.Text & "'s Goals"
                Esign_Goals.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Esign_Goals.Attributes.Add("onmouseout", "this.style.color='#000000'")

            ElseIf lblEMPLID.Text = lblLogin_EMPLID.Text Then 'Employee waiting for approval (mgr)
                Timer1.Enabled = False : Timer2.Enabled = False
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False
                Sub_Empl_Review.Visible = True : BtnSubmit_Mgr.Visible = False
                Sub_Empl_Review.Text = DT1.Rows(0)("DateEmpl_Appr").ToString & " " & lblEmpl_NAME.Text & "  submitted goal(s) to " & LblMGT_NAME.Text & " for review, edit and approved."
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
            End If

        End If

        If lblFlag.Text = 4 Then
            If Len(lblLogin_EMPLID.Text) = 0 Then
                Timer1.Enabled = False : Timer2.Enabled = False
                'Response.Write("Guild Employee View and Confirm") : Response.End()
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False : LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_Empl").ToString
                btnSub_Empl.Visible = False : Sub_Empl_Review.Visible = True : Sub_Empl_Review.Text = DT1.Rows(0)("MGT_Name").ToString & " Submitted your goal(s) for review and e-sign on " & DT1.Rows(0)("DateSUB_Empl").ToString
                Empl_Review.Visible = True : Empl_Review.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Empl_Review.Attributes.Add("onmouseout", "this.style.color='#000000'")
                LblEMP_Appr.Text = "Waiting Approval" : LblEMP_Appr.ForeColor = Drawing.Color.Blue
            ElseIf Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) <> 0) Then
                Timer1.Enabled = False : Timer2.Enabled = False
                'Response.Write("Manager waiting") : Response.End()
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False : LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_Empl").ToString
                btnSub_Empl.Visible = False : Sub_Empl_Review.Visible = True : Sub_Empl_Review.Text = "Submitted for Review to " & DT1.Rows(0)("Empl_Name").ToString & " on " & DT1.Rows(0)("DateSUB_Empl").ToString
                LblEMP_Appr.Text = "Waiting Approval" : LblEMP_Appr.ForeColor = Drawing.Color.Blue
                btnRecall.Visible = True : btnRecall.Text = "Create/Edit goals"
            ElseIf Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) = 0) Then
                Timer1.Enabled = False : Timer2.Enabled = False
                'Response.Write("Mgr Employee View and Confirm") : Response.End()
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False : LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_Empl").ToString
                btnSub_Empl.Visible = False : Sub_Empl_Review.Visible = True : Sub_Empl_Review.Text = DT1.Rows(0)("MGT_Name").ToString & " Submitted your goal(s) for review and e-sign on " & DT1.Rows(0)("DateSUB_Empl").ToString
                Empl_Review.Visible = True : Empl_Review.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Empl_Review.Attributes.Add("onmouseout", "this.style.color='#000000'")
                LblEMP_Appr.Text = "Waiting Approval" : LblEMP_Appr.ForeColor = Drawing.Color.Blue
            End If

        End If

        If lblFlag.Text = 5 Then
            '--Employee review and confirm--
            If Len(lblLogin_EMPLID.Text) = 0 Then ' Employee's view
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : BtnSubmit_Mgr.Visible = False : btnSave2.Visible = False
                LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_Empl").ToString
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString : btnSub_Empl.Visible = False
                If Session("GoalUpdated") = 1 And Session("GoalUpdated") = 0 Then
                    Empl_Review.Visible = True
                Else
                    Empl_Review.Visible = False
                    Sub_Empl_Review.Text = "Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString
                    LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
                End If

                '--Manager after Employee Approved--
            ElseIf Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) <> 0) Then ' Manager's view
                Timer1.Enabled = True : Timer2.Enabled = True
                LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_Empl").ToString
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString : btnSub_Empl.Visible = True : Sub_Empl_Review.Visible = True : Empl_Review.Visible = False : BtnSubmit_Mgr.Visible = False
                btnSub_Empl.Text = "Submit for review to " & lblEmpl_NAME.Text

                If Session("GoalUpdated") = 1 And Session("GoalUpdated") = 0 Then
                    Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False
                    Sub_Empl_Review.Text = "Waiting Confirmation from " & lblEmpl_NAME.Text : Sub_Empl_Review.Font.Size = 14 : Panel_Goals_Log.Visible = False
                    btnRecall.Visible = True : btnRecall.Text = "Create/Edit goals"
                Else
                    Goal_Setting_Edit.Visible = True : Goal_Setting_Edit.Visible = True
                    Goal_Setting_Review.Visible = False
                    Sub_Empl_Review.Text = "<br>" & lblEmpl_NAME.Text & " Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString & ". <br>If changes need to be made to the goals, please make edits on the form, and used the save and submit button above."
                    LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString : Empl_Review.Visible = False
                End If

            ElseIf Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) = 0) Then ' manager's view his goals
                Goal_Setting_Edit.Visible = False : Goal_Setting_Review.Visible = True : btnSave2.Visible = False
                LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_Empl").ToString : Sub_Empl_Review.Visible = True
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString : btnSub_Empl.Visible = False

                If Session("GoalUpdated") = 1 And Session("GoalUpdated") = 0 Then
                    Empl_Review.Visible = True
                Else
                    Empl_Review.Visible = False
                    Sub_Empl_Review.Text = "Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString
                    LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
                End If
            End If
        End If
        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub ShowButtonCursor()

        Delete_Goal2.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal3.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal4.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal5.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal6.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal7.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal8.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal9.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal10.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : btnNew_Goal.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        BtnSubmit_Mgr.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : BtnSubmit_Mgr.Attributes.Add("onmouseout", "this.style.color='#000000'")
        btnSave1.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave1.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Empl_Review.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Empl_Review.Attributes.Add("onmouseout", "this.style.color='#000000'")
        btnSave2.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnSave2.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSave2.Attributes.Add("onmouseout", "this.style.color='#000000'")
        btnNew_Goal.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnNew_Goal.Attributes.Add("onmouseout", "this.style.color='#000000'")
        btnRecall.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnRecall.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnRecall.Attributes.Add("onmouseout", "this.style.color='#000000'")
        btnSub_Empl.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : btnSub_Empl.Attributes.Add("onmouseout", "this.style.color='#000000'")


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
    Protected Sub BtnSubmit_Mgr_Click(sender As Object, e As EventArgs) Handles BtnSubmit_Mgr.Click
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

        '----------A. IndexID=1----------
        If CDbl(DT.Rows(0)("Fut1").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals1,len(Milestones)Milestones1,len(TargetDate)TargetDate1 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals1").ToString) < 2 Then txbGoal1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones1").ToString) < 2 Then txbSuccess1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate1").ToString) < 2 Then txbDate1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate1.BackColor = Drawing.Color.White
        End If
        '----------B. IndexID=2----------
        If CDbl(DT.Rows(0)("Fut2").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals2,len(Milestones)Milestones2,len(TargetDate)TargetDate2 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals2").ToString) < 2 Then txbGoal2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones2").ToString) < 2 Then txbSuccess2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate2").ToString) < 2 Then txbDate2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate2.BackColor = Drawing.Color.White
        End If
        '----------C. IndexID=3----------
        If CDbl(DT.Rows(0)("Fut3").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals3,len(Milestones)Milestones3,len(TargetDate)TargetDate3 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals3").ToString) < 2 Then txbGoal3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones3").ToString) < 2 Then txbSuccess3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate3").ToString) < 2 Then txbDate3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate3.BackColor = Drawing.Color.White
        End If
        '----------D. IndexID=4----------
        If CDbl(DT.Rows(0)("Fut4").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals4,len(Milestones)Milestones4,len(TargetDate)TargetDate4 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals4").ToString) < 2 Then txbGoal4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones4").ToString) < 2 Then txbSuccess4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate4").ToString) < 2 Then txbDate4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate4.BackColor = Drawing.Color.White
        End If
        '----------E. IndexID=5----------
        If CDbl(DT.Rows(0)("Fut5").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals5,len(Milestones)Milestones5,len(TargetDate)TargetDate5 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals5").ToString) < 2 Then txbGoal5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones5").ToString) < 2 Then txbSuccess5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate5").ToString) < 2 Then txbDate5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate5.BackColor = Drawing.Color.White
        End If
        '----------F. IndexID=6----------
        If CDbl(DT.Rows(0)("Fut6").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals6,len(Milestones)Milestones6,len(TargetDate)TargetDate6 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals6").ToString) < 2 Then txbGoal6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones6").ToString) < 2 Then txbSuccess6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate6").ToString) < 2 Then txbDate6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate6.BackColor = Drawing.Color.White
        End If
        '----------G. IndexID=7----------
        If CDbl(DT.Rows(0)("Fut7").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals7,len(Milestones)Milestones7,len(TargetDate)TargetDate7 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals7").ToString) < 2 Then txbGoal7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones7").ToString) < 2 Then txbSuccess7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate7").ToString) < 2 Then txbDate7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate7.BackColor = Drawing.Color.White
        End If
        '----------H. IndexID=8----------
        If CDbl(DT.Rows(0)("Fut8").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals8,len(Milestones)Milestones8,len(TargetDate)TargetDate8 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals8").ToString) < 2 Then txbGoal8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones8").ToString) < 2 Then txbSuccess8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate8").ToString) < 2 Then txbDate8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate8.BackColor = Drawing.Color.White
        End If
        '----------I. IndexID=9----------
        If CDbl(DT.Rows(0)("Fut9").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals9,len(Milestones)Milestones9,len(TargetDate)TargetDate9 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals9").ToString) < 2 Then txbGoal9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones9").ToString) < 2 Then txbSuccess9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate9").ToString) < 2 Then txbDate9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate9.BackColor = Drawing.Color.White
        End If
        '----------K. IndexID=10----------
        If CDbl(DT.Rows(0)("Fut10").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals10,len(Milestones)Milestones10,len(TargetDate)TargetDate10 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals10").ToString) < 2 Then txbGoal10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal10.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones10").ToString) < 2 Then txbSuccess10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess10.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate10").ToString) < 2 Then txbDate10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate10.BackColor = Drawing.Color.White
        End If

        If Flag = 0 Then
            SendToManager()
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('Please fill yellow highlighted fields');</script>")
            Return
        End If

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub SaveData()
        '--Update data from Goals table
        SQL7 = " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal1.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess1.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= "TargetDate='" & Replace(Replace(Replace(Replace(txbDate1.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=1 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal2.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess2.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Replace(txbDate2.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=2 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal3.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess3.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Replace(txbDate3.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=3 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal4.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess4.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Replace(txbDate4.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=4 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal5.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess5.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Replace(txbDate5.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=5 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal6.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess6.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Replace(txbDate6.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=6 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal7.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess7.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Replace(txbDate7.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=7 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal8.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess8.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Replace(txbDate8.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=8 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal9.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess9.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Replace(txbDate9.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=9 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL7 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Replace(txbGoal10.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "', "
        SQL7 &= " Milestones='" & Replace(Replace(Replace(Replace(txbSuccess10.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL7 &= " TargetDate='" & Replace(Replace(Replace(Replace(txbDate10.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where IndexId=10 and Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " "
        'Response.Write(SQL7) ': Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL7)
        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub btnSave2_Click(sender As Object, e As EventArgs) Handles btnSave2.Click
        WindowBatch()
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SaveData()
            lblMessage1.Visible = True
            lblMessage1.Text = "Data Saved"
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('You have the identical form opened in another browser window.\nThe Text will not save because you have another  form opened in somewhere else.');</script>")
        End If

    End Sub
    Protected Sub DisplayData()
        'Response.Write("Goal Form View " & LblEmpl_Type.Text)
        ShowHideGoalPanel()
        If CDbl(LblEmpl_Type.Text) = 1 Then '--Manager View
            SQL = "select A.*,"
            SQL &= " IsNull(Goals1,'')Goals1,IsNull(Milestones1,'')Milestones1,IsNull(TargetDate1,'')TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2,"
            SQL &= " IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4,"
            SQL &= " IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6,"
            SQL &= " IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,IsNull(TargetDate8,'')TargetDate8,"
            SQL &= " IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10"
            SQL &= " from (select emplid,mgt_emplid,(select first+' '+last from id_tbl where emplid=hr_emplid)HR_Name,hr_emplid,"
            SQL &= " IsNull(Comments,'')Comments from Appraisal_FutureGoals_Master_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")A Left Join "
            SQL &= " (select MGT_Emplid,emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )A1 on a.emplid=a1.emplid Left Join "
            SQL &= " (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )B on a.emplid=b.emplid Left Join "
            SQL &= " (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )C on a.emplid=c.emplid Left Join "
            SQL &= " (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )D on a.emplid=d.emplid Left Join "
            SQL &= " (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )E on a.emplid=e.emplid Left Join "
            SQL &= " (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )F on a.emplid=f.emplid Left Join "
            SQL &= " (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )G on a.emplid=g.emplid Left Join "
            SQL &= " (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )H on a.emplid=h.emplid Left Join "
            SQL &= " (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )N on a.emplid=n.emplid Left Join "
            SQL &= " (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & "  and IndexID =10"
            If CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) > 0 Then SQL &= " and Len(DateEmpl_Appr)>6"
            SQL &= " )K on A.emplid=K.emplid"
            'Response.Write("1.Manager View <br>" & SQL) ': Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            '--Dispaly Data on TextBoxs
            txbGoal1.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals1").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess1.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones1").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate1.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate1").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal2.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals2").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess2.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones2").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate2.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate2").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal3.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals3").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess3.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones3").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate3.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate3").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal4.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals4").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess4.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones4").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate4.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate4").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal5.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals5").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess5.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones5").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate5.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate5").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal6.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals6").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess6.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones6").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate6.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate6").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal7.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals7").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess7.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones7").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate7.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate7").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal8.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals8").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess8.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones8").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate8.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate8").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal9.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals9").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess9.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones9").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate9.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate9").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal10.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals10").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess10.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones10").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate10.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate10").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")

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


            '--Dispaly Index2--
            If Len(DT.Rows(0)("Goals2").ToString) + Len(DT.Rows(0)("Milestones2").ToString) + Len(DT.Rows(0)("TargetDate2").ToString) > 2 Then Panel_FutureGoal_Review2.Visible = True Else Panel_FutureGoal_Review2.Visible = False
            '--Dispaly Index3--
            If Len(DT.Rows(0)("Goals3").ToString) + Len(DT.Rows(0)("Milestones3").ToString) + Len(DT.Rows(0)("TargetDate3").ToString) > 2 Then Panel_FutureGoal_Review3.Visible = True Else Panel_FutureGoal_Review3.Visible = False
            '--Dispaly Index4--
            If Len(DT.Rows(0)("Goals4").ToString) + Len(DT.Rows(0)("Milestones4").ToString) + Len(DT.Rows(0)("TargetDate4").ToString) > 2 Then Panel_FutureGoal_Review4.Visible = True Else Panel_FutureGoal_Review4.Visible = False
            '--Dispaly Index5--
            If Len(DT.Rows(0)("Goals5").ToString) + Len(DT.Rows(0)("Milestones5").ToString) + Len(DT.Rows(0)("TargetDate5").ToString) > 2 Then Panel_FutureGoal_Review5.Visible = True Else Panel_FutureGoal_Review5.Visible = False
            '--Dispaly Index6--
            If Len(DT.Rows(0)("Goals6").ToString) + Len(DT.Rows(0)("Milestones6").ToString) + Len(DT.Rows(0)("TargetDate6").ToString) > 2 Then Panel_FutureGoal_Review6.Visible = True Else Panel_FutureGoal_Review6.Visible = False
            '--Dispaly Index7--
            If Len(DT.Rows(0)("Goals7").ToString) + Len(DT.Rows(0)("Milestones7").ToString) + Len(DT.Rows(0)("TargetDate7").ToString) > 2 Then Panel_FutureGoal_Review7.Visible = True Else Panel_FutureGoal_Review7.Visible = False
            '--Dispaly Index8--
            If Len(DT.Rows(0)("Goals8").ToString) + Len(DT.Rows(0)("Milestones8").ToString) + Len(DT.Rows(0)("TargetDate8").ToString) > 2 Then Panel_FutureGoal_Review8.Visible = True Else Panel_FutureGoal_Review8.Visible = False
            '--Dispaly Index9--
            If Len(DT.Rows(0)("Goals9").ToString) + Len(DT.Rows(0)("Milestones9").ToString) + Len(DT.Rows(0)("TargetDate9").ToString) > 2 Then Panel_FutureGoal_Review9.Visible = True Else Panel_FutureGoal_Review9.Visible = False
            '--Dispaly Index10--
            If Len(DT.Rows(0)("Goals10").ToString) + Len(DT.Rows(0)("Milestones10").ToString) + Len(DT.Rows(0)("TargetDate10").ToString) > 2 Then Panel_FutureGoal_Review10.Visible = True Else Panel_FutureGoal_Review10.Visible = False

        ElseIf CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) = 4 Then '-- Waiting Employee's Approval

            SQL = " select A.*, IsNull(Goals1,'')Goals1,IsNull(Milestones1,'')Milestones1,IsNull(TargetDate1,'')TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2, IsNull(Goals3,'')Goals3,"
            SQL &= "IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4, IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,"
            SQL &= "IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6, IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,"
            SQL &= "IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,IsNull(TargetDate8,'')TargetDate8, IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,"
            SQL &= "IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10,DateApp1,DateApp2,DateApp3,DateApp4,DateApp5,DateApp6,DateApp7,DateApp8,DateApp9,DateApp10 from "
            SQL &= " (select emplid,mgt_emplid,(select first+' '+last from id_tbl where emplid=hr_emplid)HR_Name,hr_emplid, IsNull(Comments,'')Comments from Appraisal_FutureGoals_Master_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")A "
            SQL &= " Left Join (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1,DateEmpl_Appr DateApp1 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1 and DateEmpl_Appr is NULL)A1 on a.emplid=a1.emplid "
            SQL &= " Left Join (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2,DateEmpl_Appr DateApp2 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2 and DateEmpl_Appr is NULL)B on a.emplid=b.emplid "
            SQL &= " Left Join (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3,DateEmpl_Appr DateApp3 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3 and DateEmpl_Appr is NULL)C on a.emplid=c.emplid "
            SQL &= " Left Join (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4,DateEmpl_Appr DateApp4 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4 and DateEmpl_Appr is NULL)D on a.emplid=d.emplid "
            SQL &= " Left Join (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5,DateEmpl_Appr DateApp5 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5 and DateEmpl_Appr is NULL)E on a.emplid=e.emplid "
            SQL &= " Left Join (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6,DateEmpl_Appr DateApp6 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6 and DateEmpl_Appr is NULL)F on a.emplid=f.emplid "
            SQL &= " Left Join (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7,DateEmpl_Appr DateApp7 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7 and DateEmpl_Appr is NULL)G on a.emplid=g.emplid "
            SQL &= " Left Join (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8,DateEmpl_Appr DateApp8 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8 and DateEmpl_Appr is NULL)H on a.emplid=h.emplid "
            SQL &= " Left Join (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9,DateEmpl_Appr DateApp9 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9 and DateEmpl_Appr is NULL)N on a.emplid=n.emplid "
            SQL &= " Left Join (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10,DateEmpl_Appr DateApp10 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10 and DateEmpl_Appr is NULL)K on A.emplid=K.emplid    "
            'Response.Write("2.Waiting Employee's Approval <br>" & SQL) ':Response.End
            DT = LocalClass.ExecuteSQLDataSet(SQL)

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

            '--Dispaly Index2--
            If Len(DT.Rows(0)("Goals2").ToString) + Len(DT.Rows(0)("Milestones2").ToString) + Len(DT.Rows(0)("TargetDate2").ToString) > 2 Then Panel_FutureGoal_Review2.Visible = True Else Panel_FutureGoal_Review2.Visible = False
            '--Dispaly Index3--
            If Len(DT.Rows(0)("Goals3").ToString) + Len(DT.Rows(0)("Milestones3").ToString) + Len(DT.Rows(0)("TargetDate3").ToString) > 2 Then Panel_FutureGoal_Review3.Visible = True Else Panel_FutureGoal_Review3.Visible = False
            '--Dispaly Index4--
            If Len(DT.Rows(0)("Goals4").ToString) + Len(DT.Rows(0)("Milestones4").ToString) + Len(DT.Rows(0)("TargetDate4").ToString) > 2 Then Panel_FutureGoal_Review4.Visible = True Else Panel_FutureGoal_Review4.Visible = False
            '--Dispaly Index5--
            If Len(DT.Rows(0)("Goals5").ToString) + Len(DT.Rows(0)("Milestones5").ToString) + Len(DT.Rows(0)("TargetDate5").ToString) > 2 Then Panel_FutureGoal_Review5.Visible = True Else Panel_FutureGoal_Review5.Visible = False
            '--Dispaly Index6--
            If Len(DT.Rows(0)("Goals6").ToString) + Len(DT.Rows(0)("Milestones6").ToString) + Len(DT.Rows(0)("TargetDate6").ToString) > 2 Then Panel_FutureGoal_Review6.Visible = True Else Panel_FutureGoal_Review6.Visible = False
            '--Dispaly Index7--
            If Len(DT.Rows(0)("Goals7").ToString) + Len(DT.Rows(0)("Milestones7").ToString) + Len(DT.Rows(0)("TargetDate7").ToString) > 2 Then Panel_FutureGoal_Review7.Visible = True Else Panel_FutureGoal_Review7.Visible = False
            '--Dispaly Index8--
            If Len(DT.Rows(0)("Goals8").ToString) + Len(DT.Rows(0)("Milestones8").ToString) + Len(DT.Rows(0)("TargetDate8").ToString) > 2 Then Panel_FutureGoal_Review8.Visible = True Else Panel_FutureGoal_Review8.Visible = False
            '--Dispaly Index9--
            If Len(DT.Rows(0)("Goals9").ToString) + Len(DT.Rows(0)("Milestones9").ToString) + Len(DT.Rows(0)("TargetDate9").ToString) > 2 Then Panel_FutureGoal_Review9.Visible = True Else Panel_FutureGoal_Review9.Visible = False
            '--Dispaly Index10--
            If Len(DT.Rows(0)("Goals10").ToString) + Len(DT.Rows(0)("Milestones10").ToString) + Len(DT.Rows(0)("TargetDate10").ToString) > 2 Then Panel_FutureGoal_Review10.Visible = True Else Panel_FutureGoal_Review10.Visible = False


        ElseIf CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) = 5 Then '--Employee Create and View
            'Response.Write("Flag 5")
            SQL = " select A.*, IsNull(Goals1,'')Goals1,IsNull(Milestones1,'')Milestones1,IsNull(TargetDate1,'')TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2, IsNull(Goals3,'')Goals3,"
            SQL &= " IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4, IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,"
            SQL &= " IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6, IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,"
            SQL &= " IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,"
            SQL &= " IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10 from (select emplid,mgt_emplid,(select first+' '+last from id_tbl where emplid=hr_emplid)HR_Name,hr_emplid, IsNull(Comments,'')Comments "
            SQL &= " from Appraisal_FutureGoals_Master_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")A "
            SQL &= " Left Join (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1"
            SQL &= " )A1 on a.emplid=a1.emplid "
            SQL &= " Left Join (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2 "
            SQL &= " )B on a.emplid=b.emplid "
            SQL &= " Left Join (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3 "
            SQL &= " )C on a.emplid=c.emplid "
            SQL &= " Left Join (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4 "
            SQL &= " )D on a.emplid=d.emplid "
            SQL &= " Left Join (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5 "
            SQL &= " )E on a.emplid=e.emplid "
            SQL &= " Left Join (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6 "
            SQL &= " )F on a.emplid=f.emplid "
            SQL &= " Left Join (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7 "
            SQL &= " )G on a.emplid=g.emplid "
            SQL &= " Left Join (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8 "
            SQL &= " )H on a.emplid=h.emplid "
            SQL &= " Left Join (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9 "
            SQL &= " )N on a.emplid=n.emplid "
            SQL &= " Left Join (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10,DateEmpl_Appr from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10 "
            SQL &= " )K on A.emplid=K.emplid    "
            'Response.Write("3.Employee Create and View <br>" & SQL) ':Response.End
            DT = LocalClass.ExecuteSQLDataSet(SQL)

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

            '--Dispaly Index2--
            If Len(DT.Rows(0)("Goals2").ToString) + Len(DT.Rows(0)("Milestones2").ToString) + Len(DT.Rows(0)("TargetDate2").ToString) > 2 Then Panel_FutureGoal_Review2.Visible = True Else Panel_FutureGoal_Review2.Visible = False
            '--Dispaly Index3--
            If Len(DT.Rows(0)("Goals3").ToString) + Len(DT.Rows(0)("Milestones3").ToString) + Len(DT.Rows(0)("TargetDate3").ToString) > 2 Then Panel_FutureGoal_Review3.Visible = True Else Panel_FutureGoal_Review3.Visible = False
            '--Dispaly Index4--
            If Len(DT.Rows(0)("Goals4").ToString) + Len(DT.Rows(0)("Milestones4").ToString) + Len(DT.Rows(0)("TargetDate4").ToString) > 2 Then Panel_FutureGoal_Review4.Visible = True Else Panel_FutureGoal_Review4.Visible = False
            '--Dispaly Index5--
            If Len(DT.Rows(0)("Goals5").ToString) + Len(DT.Rows(0)("Milestones5").ToString) + Len(DT.Rows(0)("TargetDate5").ToString) > 2 Then Panel_FutureGoal_Review5.Visible = True Else Panel_FutureGoal_Review5.Visible = False
            '--Dispaly Index6--
            If Len(DT.Rows(0)("Goals6").ToString) + Len(DT.Rows(0)("Milestones6").ToString) + Len(DT.Rows(0)("TargetDate6").ToString) > 2 Then Panel_FutureGoal_Review6.Visible = True Else Panel_FutureGoal_Review6.Visible = False
            '--Dispaly Index7--
            If Len(DT.Rows(0)("Goals7").ToString) + Len(DT.Rows(0)("Milestones7").ToString) + Len(DT.Rows(0)("TargetDate7").ToString) > 2 Then Panel_FutureGoal_Review7.Visible = True Else Panel_FutureGoal_Review7.Visible = False
            '--Dispaly Index8--
            If Len(DT.Rows(0)("Goals8").ToString) + Len(DT.Rows(0)("Milestones8").ToString) + Len(DT.Rows(0)("TargetDate8").ToString) > 2 Then Panel_FutureGoal_Review8.Visible = True Else Panel_FutureGoal_Review8.Visible = False
            '--Dispaly Index9--
            If Len(DT.Rows(0)("Goals9").ToString) + Len(DT.Rows(0)("Milestones9").ToString) + Len(DT.Rows(0)("TargetDate9").ToString) > 2 Then Panel_FutureGoal_Review9.Visible = True Else Panel_FutureGoal_Review9.Visible = False
            '--Dispaly Index10--
            If Len(DT.Rows(0)("Goals10").ToString) + Len(DT.Rows(0)("Milestones10").ToString) + Len(DT.Rows(0)("TargetDate10").ToString) > 2 Then Panel_FutureGoal_Review10.Visible = True Else Panel_FutureGoal_Review10.Visible = False


        ElseIf CDbl(LblEmpl_Type.Text) = 2 And (CDbl(lblFlag.Text) = 1 Or CDbl(lblFlag.Text) = 2) Then '--Employee Send to Manager and View
            'Response.Write("Employees View")
            SQL = " select A.*, IsNull(Goals1,'')Goals1,IsNull(Milestones1,'')Milestones1,IsNull(TargetDate1,'')TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2, IsNull(Goals3,'')Goals3,"
            SQL &= "IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4, IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,"
            SQL &= "IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6, IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,"
            SQL &= "IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,IsNull(TargetDate8,'')TargetDate8, IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,"
            SQL &= "IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10,DateApp1,DateApp2,DateApp3,DateApp4,DateApp5,DateApp6,DateApp7,DateApp8,DateApp9,DateApp10 from "
            SQL &= " (select emplid,mgt_emplid,(select first+' '+last from id_tbl where emplid=hr_emplid)HR_Name,hr_emplid, IsNull(Comments,'')Comments from Appraisal_FutureGoals_Master_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")A "
            SQL &= " Left Join (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1,DateEmpl_Appr DateApp1 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1 )A1 on a.emplid=a1.emplid "
            SQL &= " Left Join (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2,DateEmpl_Appr DateApp2 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2 )B on a.emplid=b.emplid "
            SQL &= " Left Join (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3,DateEmpl_Appr DateApp3 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3 )C on a.emplid=c.emplid "
            SQL &= " Left Join (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4,DateEmpl_Appr DateApp4 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4 )D on a.emplid=d.emplid "
            SQL &= " Left Join (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5,DateEmpl_Appr DateApp5 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5 )E on a.emplid=e.emplid "
            SQL &= " Left Join (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6,DateEmpl_Appr DateApp6 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6 )F on a.emplid=f.emplid "
            SQL &= " Left Join (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7,DateEmpl_Appr DateApp7 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7 )G on a.emplid=g.emplid "
            SQL &= " Left Join (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8,DateEmpl_Appr DateApp8 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8 )H on a.emplid=h.emplid "
            SQL &= " Left Join (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9,DateEmpl_Appr DateApp9 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9 )N on a.emplid=n.emplid "
            SQL &= " Left Join (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10,DateEmpl_Appr DateApp10 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10 )K on A.emplid=K.emplid    "
            'Response.Write("4.Employee Send to Manager and View <br>" & SQL)
            DT = LocalClass.ExecuteSQLDataSet(SQL)

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


            '--Dispaly Index2--
            If Len(DT.Rows(0)("Goals2").ToString) + Len(DT.Rows(0)("Milestones2").ToString) + Len(DT.Rows(0)("TargetDate2").ToString) > 2 Then Panel_FutureGoal_Review2.Visible = True Else Panel_FutureGoal_Review2.Visible = False
            '--Dispaly Index3--
            If Len(DT.Rows(0)("Goals3").ToString) + Len(DT.Rows(0)("Milestones3").ToString) + Len(DT.Rows(0)("TargetDate3").ToString) > 2 Then Panel_FutureGoal_Review3.Visible = True Else Panel_FutureGoal_Review3.Visible = False
            '--Dispaly Index4--
            If Len(DT.Rows(0)("Goals4").ToString) + Len(DT.Rows(0)("Milestones4").ToString) + Len(DT.Rows(0)("TargetDate4").ToString) > 2 Then Panel_FutureGoal_Review4.Visible = True Else Panel_FutureGoal_Review4.Visible = False
            '--Dispaly Index5--
            If Len(DT.Rows(0)("Goals5").ToString) + Len(DT.Rows(0)("Milestones5").ToString) + Len(DT.Rows(0)("TargetDate5").ToString) > 2 Then Panel_FutureGoal_Review5.Visible = True Else Panel_FutureGoal_Review5.Visible = False
            '--Dispaly Index6--
            If Len(DT.Rows(0)("Goals6").ToString) + Len(DT.Rows(0)("Milestones6").ToString) + Len(DT.Rows(0)("TargetDate6").ToString) > 2 Then Panel_FutureGoal_Review6.Visible = True Else Panel_FutureGoal_Review6.Visible = False
            '--Dispaly Index7--
            If Len(DT.Rows(0)("Goals7").ToString) + Len(DT.Rows(0)("Milestones7").ToString) + Len(DT.Rows(0)("TargetDate7").ToString) > 2 Then Panel_FutureGoal_Review7.Visible = True Else Panel_FutureGoal_Review7.Visible = False
            '--Dispaly Index8--
            If Len(DT.Rows(0)("Goals8").ToString) + Len(DT.Rows(0)("Milestones8").ToString) + Len(DT.Rows(0)("TargetDate8").ToString) > 2 Then Panel_FutureGoal_Review8.Visible = True Else Panel_FutureGoal_Review8.Visible = False
            '--Dispaly Index9--
            If Len(DT.Rows(0)("Goals9").ToString) + Len(DT.Rows(0)("Milestones9").ToString) + Len(DT.Rows(0)("TargetDate9").ToString) > 2 Then Panel_FutureGoal_Review9.Visible = True Else Panel_FutureGoal_Review9.Visible = False
            '--Dispaly Index10--
            If Len(DT.Rows(0)("Goals10").ToString) + Len(DT.Rows(0)("Milestones10").ToString) + Len(DT.Rows(0)("TargetDate10").ToString) > 2 Then Panel_FutureGoal_Review10.Visible = True Else Panel_FutureGoal_Review10.Visible = False

        ElseIf CDbl(LblEmpl_Type.Text) = 2 And CDbl(lblFlag.Text) = 0 Then '--Employee Send to Manager and View
            'Response.Write("Employees Created and View before sent to Manager")
            SQL = " select A.*, IsNull(Goals1,'')Goals1,IsNull(Milestones1,'')Milestones1,IsNull(TargetDate1,'')TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2, IsNull(Goals3,'')Goals3,"
            SQL &= "IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4, IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,"
            SQL &= "IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6, IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,"
            SQL &= "IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,IsNull(TargetDate8,'')TargetDate8, IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,"
            SQL &= "IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10,DateApp1,DateApp2,DateApp3,DateApp4,DateApp5,DateApp6,DateApp7,DateApp8,DateApp9,DateApp10 from "
            SQL &= " (select emplid,mgt_emplid,(select first+' '+last from id_tbl where emplid=hr_emplid)HR_Name,hr_emplid, IsNull(Comments,'')Comments from Appraisal_FutureGoals_Master_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")A "
            SQL &= " Left Join (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1,DateEmpl_Appr DateApp1 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1 and DateEmpl_Appr is NULL)A1 on a.emplid=a1.emplid "
            SQL &= " Left Join (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2,DateEmpl_Appr DateApp2 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2 and DateEmpl_Appr is NULL)B on a.emplid=b.emplid "
            SQL &= " Left Join (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3,DateEmpl_Appr DateApp3 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3 and DateEmpl_Appr is NULL)C on a.emplid=c.emplid "
            SQL &= " Left Join (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4,DateEmpl_Appr DateApp4 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4 and DateEmpl_Appr is NULL)D on a.emplid=d.emplid "
            SQL &= " Left Join (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5,DateEmpl_Appr DateApp5 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5 and DateEmpl_Appr is NULL)E on a.emplid=e.emplid "
            SQL &= " Left Join (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6,DateEmpl_Appr DateApp6 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6 and DateEmpl_Appr is NULL)F on a.emplid=f.emplid "
            SQL &= " Left Join (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7,DateEmpl_Appr DateApp7 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7 and DateEmpl_Appr is NULL)G on a.emplid=g.emplid "
            SQL &= " Left Join (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8,DateEmpl_Appr DateApp8 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8 and DateEmpl_Appr is NULL)H on a.emplid=h.emplid "
            SQL &= " Left Join (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9,DateEmpl_Appr DateApp9 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9 and DateEmpl_Appr is NULL)N on a.emplid=n.emplid "
            SQL &= " Left Join (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10,DateEmpl_Appr DateApp10 from Appraisal_FutureGoals_tbl where "
            SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10 and DateEmpl_Appr is NULL)K on A.emplid=K.emplid    "
            'Response.Write("5.Employee Send to Manager and View <br>" & SQL) ':Response.End
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            '--Dispaly Data on TextBoxs
            txbGoal1.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals1").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess1.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones1").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate1.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate1").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal2.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals2").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess2.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones2").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate2.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate2").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal3.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals3").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess3.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones3").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate3.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate3").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal4.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals4").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess4.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones4").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate4.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate4").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal5.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals5").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess5.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones5").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate5.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate5").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal6.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals6").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess6.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones6").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate6.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate6").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal7.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals7").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess7.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones7").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate7.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate7").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal8.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals8").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess8.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones8").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate8.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate8").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal9.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals9").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess9.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones9").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate9.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate9").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbGoal10.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals10").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbSuccess10.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones10").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDate10.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate10").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")

            If Len(DT.Rows(0)("Goals2").ToString) + Len(DT.Rows(0)("Milestones2").ToString) + Len(DT.Rows(0)("TargetDate2").ToString) > 2 Then Panel_FutureGoal_Review2.Visible = True Else Panel_FutureGoal_Review2.Visible = False
            '--Dispaly Index3--
            If Len(DT.Rows(0)("Goals3").ToString) + Len(DT.Rows(0)("Milestones3").ToString) + Len(DT.Rows(0)("TargetDate3").ToString) > 2 Then Panel_FutureGoal_Review3.Visible = True Else Panel_FutureGoal_Review3.Visible = False
            '--Dispaly Index4--
            If Len(DT.Rows(0)("Goals4").ToString) + Len(DT.Rows(0)("Milestones4").ToString) + Len(DT.Rows(0)("TargetDate4").ToString) > 2 Then Panel_FutureGoal_Review4.Visible = True Else Panel_FutureGoal_Review4.Visible = False
            '--Dispaly Index5--
            If Len(DT.Rows(0)("Goals5").ToString) + Len(DT.Rows(0)("Milestones5").ToString) + Len(DT.Rows(0)("TargetDate5").ToString) > 2 Then Panel_FutureGoal_Review5.Visible = True Else Panel_FutureGoal_Review5.Visible = False
            '--Dispaly Index6--
            If Len(DT.Rows(0)("Goals6").ToString) + Len(DT.Rows(0)("Milestones6").ToString) + Len(DT.Rows(0)("TargetDate6").ToString) > 2 Then Panel_FutureGoal_Review6.Visible = True Else Panel_FutureGoal_Review6.Visible = False
            '--Dispaly Index7--
            If Len(DT.Rows(0)("Goals7").ToString) + Len(DT.Rows(0)("Milestones7").ToString) + Len(DT.Rows(0)("TargetDate7").ToString) > 2 Then Panel_FutureGoal_Review7.Visible = True Else Panel_FutureGoal_Review7.Visible = False
            '--Dispaly Index8--
            If Len(DT.Rows(0)("Goals8").ToString) + Len(DT.Rows(0)("Milestones8").ToString) + Len(DT.Rows(0)("TargetDate8").ToString) > 2 Then Panel_FutureGoal_Review8.Visible = True Else Panel_FutureGoal_Review8.Visible = False
            '--Dispaly Index9--
            If Len(DT.Rows(0)("Goals9").ToString) + Len(DT.Rows(0)("Milestones9").ToString) + Len(DT.Rows(0)("TargetDate9").ToString) > 2 Then Panel_FutureGoal_Review9.Visible = True Else Panel_FutureGoal_Review9.Visible = False
            '--Dispaly Index10--
            If Len(DT.Rows(0)("Goals10").ToString) + Len(DT.Rows(0)("Milestones10").ToString) + Len(DT.Rows(0)("TargetDate10").ToString) > 2 Then Panel_FutureGoal_Review10.Visible = True Else Panel_FutureGoal_Review10.Visible = False

        End If

        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub SendToManager()

        SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=2,/*DateSUB_Empl='" & Now & "',*/ DateEmpl_Appr='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text

        SQL1 &= " Insert into Appraisal_FutureGoal_Recall_tbl "
        SQL1 &= " select EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate, '" & Now & "' DateEmpl_Appr, '" & lblEMPLID.Text & "' Recall_EMPLID, NULL Recall_Date"
        SQL1 &= " from Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
        'Response.Write(SQL1) : Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Msg = lblEmpl_NAME.Text & " created Goals and have been sent to you for review.<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details."

        '--Production Email
        'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Employee Created Goals", Msg)

        '("vituja@consumer.org", lblEmpl_NAME.Text & " created Goals", Msg)

        LocalClass.CloseSQLServerConnection()
        Response.Redirect("FutureGoals.aspx?Token=" & Request.QueryString("Token"))

    End Sub
    Protected Sub Sub_Empl_Click(sender As Object, e As EventArgs) Handles btnSub_Empl.Click
        SaveData()
        AllRules1()
    End Sub
    Protected Sub Empl_Review_Click(sender As Object, e As EventArgs) Handles Empl_Review.Click

        SQL1 = " Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=5,DateEmpl_Appr='" & Now & "',Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
        SQL1 &= " Update Appraisal_FutureGoal_Recall_tbl Set DateEmpl_Appr='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and DateEmpl_Appr is NULL"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Msg = lblEmpl_NAME.Text & " confirmed review of goals set. <br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details.<br>"


        'LocalClass.SendMail(lblFirst_SUP_EMAIL.Text, "Employee Goals Reviewed by " & lblEmpl_NAME.Text, Msg)

        '("vituja@consumer.org", "Employee Goals Reviewed by " & lblEmpl_NAME.Text, Msg)

        LocalClass.CloseSQLServerConnection()

        Response.Redirect("FutureGoals.aspx?Token=" & Request.QueryString("Token"))

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
            If CDbl(DT5.Rows(0)("Goals1").ToString) < 2 Then txbGoal1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones1").ToString) < 2 Then txbSuccess1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate1").ToString) < 2 Then txbDate1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate1.BackColor = Drawing.Color.White
        End If
        '----------B. IndexID=2----------
        If CDbl(DT.Rows(0)("Fut2").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals2,len(Milestones)Milestones2,len(TargetDate)TargetDate2 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals2").ToString) < 2 Then txbGoal2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones2").ToString) < 2 Then txbSuccess2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate2").ToString) < 2 Then txbDate2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate2.BackColor = Drawing.Color.White
        End If
        '----------C. IndexID=3----------
        If CDbl(DT.Rows(0)("Fut3").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals3,len(Milestones)Milestones3,len(TargetDate)TargetDate3 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals3").ToString) < 2 Then txbGoal3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones3").ToString) < 2 Then txbSuccess3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate3").ToString) < 2 Then txbDate3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate3.BackColor = Drawing.Color.White
        End If
        '----------D. IndexID=4----------
        If CDbl(DT.Rows(0)("Fut4").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals4,len(Milestones)Milestones4,len(TargetDate)TargetDate4 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals4").ToString) < 2 Then txbGoal4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones4").ToString) < 2 Then txbSuccess4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate4").ToString) < 2 Then txbDate4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate4.BackColor = Drawing.Color.White
        End If
        '----------E. IndexID=5----------
        If CDbl(DT.Rows(0)("Fut5").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals5,len(Milestones)Milestones5,len(TargetDate)TargetDate5 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals5").ToString) < 2 Then txbGoal5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones5").ToString) < 2 Then txbSuccess5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate5").ToString) < 2 Then txbDate5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate5.BackColor = Drawing.Color.White
        End If
        '----------F. IndexID=6----------
        If CDbl(DT.Rows(0)("Fut6").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals6,len(Milestones)Milestones6,len(TargetDate)TargetDate6 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals6").ToString) < 2 Then txbGoal6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones6").ToString) < 2 Then txbSuccess6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate6").ToString) < 2 Then txbDate6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate6.BackColor = Drawing.Color.White
        End If
        '----------G. IndexID=7----------
        If CDbl(DT.Rows(0)("Fut7").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals7,len(Milestones)Milestones7,len(TargetDate)TargetDate7 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals7").ToString) < 2 Then txbGoal7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones7").ToString) < 2 Then txbSuccess7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate7").ToString) < 2 Then txbDate7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate7.BackColor = Drawing.Color.White
        End If
        '----------H. IndexID=8----------
        If CDbl(DT.Rows(0)("Fut8").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals8,len(Milestones)Milestones8,len(TargetDate)TargetDate8 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals8").ToString) < 2 Then txbGoal8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones8").ToString) < 2 Then txbSuccess8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate8").ToString) < 2 Then txbDate8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate8.BackColor = Drawing.Color.White
        End If
        '----------I. IndexID=9----------
        If CDbl(DT.Rows(0)("Fut9").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals9,len(Milestones)Milestones9,len(TargetDate)TargetDate9 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals9").ToString) < 2 Then txbGoal9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones9").ToString) < 2 Then txbSuccess9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate9").ToString) < 2 Then txbDate9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate9.BackColor = Drawing.Color.White
        End If
        '----------K. IndexID=10----------
        If CDbl(DT.Rows(0)("Fut10").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals10,len(Milestones)Milestones10,len(TargetDate)TargetDate10 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals10").ToString) < 2 Then txbGoal10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal10.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones10").ToString) < 2 Then txbSuccess10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess10.BackColor = Drawing.Color.White
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
    Protected Sub SendToEmployee() '--Update FutureGoals table 

        '-- Sent to Employee for Review
        SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=4, DateSUB_Empl='" & Now & "',Comments='' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""
        SQL1 &= " Insert into Appraisal_FutureGoal_Recall_tbl "
        SQL1 &= " select EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate,NULL, '" & lblFirst_SUP_EMPLID.Text & "' Recall_EMPLID,'" & Now & "' "
        SQL1 &= " from Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Msg = "Your Goals have been sent for your review.<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/default.aspx for full details. "

        '--Production Email
        'LocalClass.SendMail(lblEmpl_EMAIL.Text, lblEmpl_NAME.Text & " Created Goals", Msg)


        '("vituja@consumer.org", lblEmpl_NAME.Text & " created Goals", Msg)


        Response.Redirect("FutureGoals.aspx?Token=" & Request.QueryString("Token"))
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnSave1.Click '--Send to employee after Appraisal approved
        'If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
        AllRules1()
        'Else
        'ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('You have the identical form opened in another browser window.\nThe Text will not save because you have another  form opened in somewhere else.');</script>")
        'End If
    End Sub
    Sub Goals_Log()
        
        SqlDataSource1.SelectCommand = "select distinct IndexID,(select last+' '+first from id_tbl where emplid=Recall_EMPLID)Created,Recall_Emplid,emplid,"
        SqlDataSource1.SelectCommand &= "Replace(Replace(Goals,Char(13),'<span>'), Char(10),'<br>')Goals,"
        SqlDataSource1.SelectCommand &= "Replace(Replace(Milestones,Char(13),'<span>'),Char(10),'<br>')Milestones,"
        SqlDataSource1.SelectCommand &= "Replace(Replace(TargetDate,Char(13), '<span>'), Char(10), '<br>')TargetDate,"
        SqlDataSource1.SelectCommand &= "(select Max(DateEmpl_Appr)Recall_Date from Appraisal_FutureGoal_Recall_tbl where Rtrim(Ltrim(Goals))=Rtrim(Ltrim(A.Goals)) and "
        SqlDataSource1.SelectCommand &= "Rtrim(Ltrim(Milestones))=Rtrim(Ltrim(A.Milestones)) and  Rtrim(Ltrim(TargetDate))=Rtrim(Ltrim(A.TargetDate)))Recall_Date "
        SqlDataSource1.SelectCommand &= "from Appraisal_FutureGoal_Recall_tbl A where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and Len(DateEmpl_Appr)>5 and Len(Recall_Date)>5  order by Recall_Date desc,IndexID"
        'Response.Write(SqlDataSource1.SelectCommand) : Response.End()
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Goal1_TextChanged(sender As Object, e As EventArgs) Handles txbGoal1.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL = " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal1.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=1"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success1_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess1.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess1.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID=" & lblEMPLID.Text & " and IndexID=1"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date1_TextChanged(sender As Object, e As EventArgs) Handles txbDate1.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate1.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=1"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal2_TextChanged(sender As Object, e As EventArgs) Handles txbGoal2.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal2.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=2"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success2_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess2.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess2.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=2"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date2_TextChanged(sender As Object, e As EventArgs) Handles txbDate2.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate2.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=2"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal3_TextChanged(sender As Object, e As EventArgs) Handles txbGoal3.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal3.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=3"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success3_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess3.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess3.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=3 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date3_TextChanged(sender As Object, e As EventArgs) Handles txbDate3.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate3.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=3 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal4_TextChanged(sender As Object, e As EventArgs) Handles txbGoal4.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal4.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year=" & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=4"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success4_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess4.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess4.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=4 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date4_TextChanged(sender As Object, e As EventArgs) Handles txbDate4.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate4.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=4 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal5_TextChanged(sender As Object, e As EventArgs) Handles txbGoal5.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal5.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=5"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success5_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess5.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess5.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=5 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date5_TextChanged(sender As Object, e As EventArgs) Handles txbDate5.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate5.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=5 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal6_TextChanged(sender As Object, e As EventArgs) Handles txbGoal6.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal6.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=6"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success6_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess6.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess6.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=6 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date6_TextChanged(sender As Object, e As EventArgs) Handles txbDate6.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate6.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=6 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal7_TextChanged(sender As Object, e As EventArgs) Handles txbGoal7.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal7.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=7"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success7_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess7.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess7.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=7 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date7_TextChanged(sender As Object, e As EventArgs) Handles txbDate7.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate7.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=7 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal8_TextChanged(sender As Object, e As EventArgs) Handles txbGoal8.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal8.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=8"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success8_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess8.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess8.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=8 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date8_TextChanged(sender As Object, e As EventArgs) Handles txbDate8.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate8.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=8 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal9_TextChanged(sender As Object, e As EventArgs) Handles txbGoal9.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal9.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=9"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success9_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess9.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess9.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=9 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date9_TextChanged(sender As Object, e As EventArgs) Handles txbDate9.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate9.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=9 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Goal10_TextChanged(sender As Object, e As EventArgs) Handles txbGoal10.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Replace(txbGoal10.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=10"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Success10_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess10.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Replace(txbSuccess10.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=10 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If

    End Sub
    Protected Sub Date10_TextChanged(sender As Object, e As EventArgs) Handles txbDate10.TextChanged

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Replace(txbDate10.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',MGT_EMPLID=" & lblFirst_SUP_EMPLID.Text & " where Perf_Year = " & lblYear.Text & " and EMPLID = " & lblEMPLID.Text & " and IndexID=10 "
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

            If Len(lblEMPLID.Text) > 0 And CDbl(lblFlag.Text) = 0 Then
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

    Protected Sub Edit_Goals_Click(sender As Object, e As EventArgs) Handles Edit_Goals.Click
        SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=1 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        LocalClass.CloseSQLServerConnection()
        Response.Redirect("FutureGoals.aspx?Token=" & Request.QueryString("Token"))
    End Sub

    Protected Sub Esign_Goals_Click(sender As Object, e As EventArgs) Handles Esign_Goals.Click
        SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=5,DateSUB_Empl='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
        SQL1 &= " Update Appraisal_FutureGoal_Recall_tbl Set Recall_Date='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & ""

        'SQL1 &= " Insert into Appraisal_FutureGoal_Recall_tbl "
        'SQL1 &= " select EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate, '" & Now & "' DateEmpl_Appr, '" & lblFirst_SUP_EMPLID.Text & "' Recall_EMPLID,'" & Now & "' Recall_Date"
        'SQL1 &= " from Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        LocalClass.CloseSQLServerConnection()
        Response.Redirect("FutureGoals.aspx?Token=" & Request.QueryString("Token"))
    End Sub

    Protected Sub btnRecall_Click(sender As Object, e As EventArgs) Handles btnRecall.Click
        SQL1 = "Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=1 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        LocalClass.CloseSQLServerConnection()
        Response.Redirect("FutureGoals.aspx?Token=" & Request.QueryString("Token"))
    End Sub

    Protected Sub BtnHistory_Click(sender As Object, e As EventArgs) Handles BtnHistory.Click
        Response.Write("<script>window.open('FutureGoals_History.aspx?Token=" + Request.QueryString("Token") + "','_blank' );</script>")
    End Sub
End Class