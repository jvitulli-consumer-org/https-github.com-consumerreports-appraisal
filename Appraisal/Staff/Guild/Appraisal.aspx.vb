Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Web.Services
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Collections.Generic


Public Class Appraisal
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8, SQL9, SQL10, SQL11, SQL12, SQL13, SQL14, z, ReturnValue As String
    Dim Msg, Msg1, Msq2, Msg3 As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6, DT7, DT8, DT9, DT10, DT11, DT12, DT13, DT14 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", "840")

        '--Guild Appraisal---
        z = Request.QueryString("Token")
        lblEMPLID.Text = Mid(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9"), 5, 4)

        lblYEAR.Text = Left(Request.QueryString("Token"), 4)
        lblWindowBatch.Text = Mid(Request.QueryString("Token"), 9)
        lblDataBaseBatch.Text = Session("Window_batch")


        If Len(Session("MGT_EMPLID")) > 0 Then
            lblMGT_EMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("MGT_EMPLID"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")

            lblLogin_EMPLID.Text = lblMGT_EMPLID.Text
        Else
            lblMGT_EMPLID.Text = 99999
        End If

        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display run SaveData()  and DisplayData()") ': Response.End()
        Else
            'Response.Write("<br>2. else Save Data before Display DisplayData(),ShowHideAccomplishmentPanel(),ShowHideGoalPanel() ': Response.End()")
            DisplayData()
        End If

        SetLevel_Approval()
        ShowButtonCursor()
        Discussion()

        If Trim(lblMGT_EMPLID.Text) = Trim(lblHR_EMPLID.Text) Then
            Panel_HR_Comments.Visible = True
            DisplayComments()
        Else
            Panel_HR_Comments.Visible = False
        End If

        'Tab              char(9) 
        'Line feed        char(10) 
        'Carriage return  char(13) 
        '--Convert data --
        'Replace(Replace(Replace(Trim(txbAccomp1.Text), "'", "`"), "<", "{"), ">", "}") 
        'Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp1").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")

    End Sub
    Protected Sub WindowBatch()
        SQL11 = "select Window_batch,process_flag from Appraisal_Master_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year= " & lblYEAR.Text
        'Response.Write(SQL11)
        DT11 = LocalClass.ExecuteSQLDataSet(SQL11)
        Session("Window_batch") = CDbl(DT11.Rows(0)("Window_batch").ToString)
        LocalClass.CloseSQLServerConnection()
        If CDbl(DT11.Rows(0)("process_flag").ToString) = 0 And Session("Window_batch") - CDbl(lblWindowBatch.Text) = 0 Then SaveData()
    End Sub

    Protected Sub WindowClosed()
        '--Close window on button click
        btnGeneralist.Attributes.Add("onclick", "window.close();")
        btnDiscuss.Attributes.Add("onclick", "window.close();")
    End Sub
    Protected Sub SetLevel_Approval()
        SQL = "select *,(case when Comments like '%and Employee declined to sign%' then 'Employee Declined to Sign' else Comments end)Comments1,"
        SQL &= " (select first+' '+last from id_tbl where emplid=a.emplid)Empl_Name,(select first+' '+last from id_tbl where emplid=a.MGT_EMPLID)MGT_Name,"
        SQL &= " (select first+' '+last from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.HR_EMPLID)HR_Name,"
        SQL &= " (select convert(char(10),hire_Date,101) from id_tbl where emplid=a.emplid)Empl_Hired,"
        SQL &= " (select email from id_tbl where emplid=a.emplid)Empl_Email,(select email from id_tbl where emplid=a.MGT_EMPLID)MGT_Email,"
        SQL &= " (select email from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Email,(select email from id_tbl where emplid=a.HR_EMPLID)HR_Email"
        SQL &= " from Appraisal_Master_tbl A where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        lblEmpl_NAME.Text = DT.Rows(0)("Empl_Name").ToString
        lblEmpl_TITLE.Text = DT.Rows(0)("JobTitle").ToString
        lblEmpl_DEPT.Text = DT.Rows(0)("Department").ToString
        lblEmpl_HIRE.Text = DT.Rows(0)("Empl_Hired").ToString

        lblFIRST_MGT_EMPLID.Text = DT.Rows(0)("MGT_EMPLID").ToString
        LblMGT_NAME.Text = DT.Rows(0)("MGT_Name").ToString
        lblSECOND_MGT_EMPLID.Text = DT.Rows(0)("UP_MGT_EMPLID").ToString
        lblHR_EMPLID.Text = DT.Rows(0)("HR_EMPLID").ToString
        lblHR_NAME.Text = DT.Rows(0)("HR_Name").ToString

        lblEmpl_Email.Text = DT.Rows(0)("Empl_Email").ToString
        lblMGT_Email.Text = DT.Rows(0)("MGT_Email").ToString
        lblUP_MGT_Email.Text = DT.Rows(0)("UP_MGT_Email").ToString
        lblHR_Email.Text = DT.Rows(0)("HR_Email").ToString

        FY_Year.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2))
        Goal_Year.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2) + 1)
        Goal_Year1.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2))
        Goal_Year2.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2) + 1)
        Goal_Year3.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2) + 1)
        Goal_Year4.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2))
        Goal_Year5.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2) + 1)
        'LblEMP_Appr.Text = DT.Rows(0)("DateEmpl_Appr").ToString
        'LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
        'LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
        'LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
        'lblProcess_Flag.Text = CDbl(DT.Rows(0)("Process_Flag").ToString)
        If CDbl(Goal_Year.Text) = "17" Then
            divOldTitle.Visible = True : divNewtext.Visible = False : divNewTitle.Visible = False
            divOldTitle1.Visible = True : divNewtext1.Visible = False : divNewTitle1.Visible = False
            divGoalText.Visible = False : divGoalText1.Visible = False : divNewKey1.Visible = False
            divNewKey.Visible = False : divNewtarget.Visible = False : divNewtarget1.Visible = False
        Else
            divOldTitle.Visible = False : divNewtext.Visible = True : divNewTitle.Visible = True
            divOldTitle1.Visible = False : divNewtext1.Visible = True : divNewTitle1.Visible = True
            divGoalText.Visible = True : divGoalText1.Visible = True : divNewKey1.Visible = True
            divNewKey.Visible = True : divNewtarget.Visible = True : divNewtarget1.Visible = True
        End If
        Session("Process_Flag") = CDbl(DT.Rows(0)("Process_Flag").ToString)

        SQL1 = "select top 1 * from(select emplid,(select first+' '+last from id_tbl where emplid=a.emplid)Employee,Rtrim(Ltrim(mgt_emplid))mgt_emplid,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=Rtrim(Ltrim(a.mgt_emplid)))Collab_MGT,DateTime from Appraisal_MasterHistory_tbl A where"
        SQL1 &= " Rtrim(Ltrim(emplid)) in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYEAR.Text & " and Rtrim(Ltrim(MGT_EMPLID)) not in (select Rtrim(Ltrim(mgt_emplid)) from "
        SQL1 &= " appraisal_master_tbl where emplid=a.emplid and Perf_Year=" & lblYEAR.Text & ")) AA ORDER BY DateTime desc"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        '--Get Collaboration Manager
        If DT1.Rows.Count > 0 Then lblCOLL_MGT_NAME.Text = DT1.Rows(0)("Collab_MGT").ToString

        'Response.Write(CDbl(lblMGT_EMPLID.Text) & "<br>" & CDbl(DT.Rows(0)("MGT_EMPLID").ToString)) : Response.End()

        If lblMGT_EMPLID.Text = 99999 Then
            Session("FIRST_MGT") = 2
        Else

            If CDbl(lblMGT_EMPLID.Text) - CDbl(DT.Rows(0)("MGT_EMPLID").ToString) = 0 Then
                Session("FIRST_MGT") = 1 '--First Manager Login
            Else
                Session("FIRST_MGT") = 0 '--Second Manager / HR Login
            End If
        End If

        '--Create Appraisal--
        If CDbl(DT.Rows(0)("Process_Flag").ToString) = 0 Then

            If CDbl(Trim(lblFIRST_MGT_EMPLID.Text)) - CDbl(Trim(lblSECOND_MGT_EMPLID.Text)) = 0 Then

                btnSubmit_UpperMgr.Text = "Submit for Review to " & DT.Rows(0)("HR_Name").ToString '--Bypass Second Manager if the same as First  
            Else
                'btnSubmit_UpperMgr.Text = "Submit for Review to " & DT.Rows(0)("UP_MGT_Name").ToString
                btnSubmit_UpperMgr.Text = "Submit for Review to " & DT.Rows(0)("HR_Name").ToString '--Exclude Second Manager-- 
            End If


            Panel_Create_Appraisal.Visible = True
            btnDiscuss.Visible = False
            btnGeneralist.Visible = False
            Panel_Waiting_Approval.Visible = False
            Panel_Employee_Review.Visible = False
            Panel_Comments.Visible = False
            tblEdit_form.Visible = False

            '--Second Manager Appraisal View --
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 1 And Session("FIRST_MGT") = 0 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = "Waiting Approval"
            LblUP_MGT_Appr.ForeColor = Drawing.Color.Blue
            LblUP_MGT_Appr.Font.Bold = True
            Panel_Waiting_Approval.Visible = True
            Panel_Comments.Visible = True
            btnDiscuss.Text = "Send back to " & DT.Rows(0)("MGT_Name").ToString & " for revision"
            lblDiscussionTitle.Text = "Send suggested revisions to " & DT.Rows(0)("MGT_Name").ToString & ""
            btnGeneralist.Text = "Submit for review to " & DT.Rows(0)("HR_Name").ToString
            Panel_Employee_Review.Visible = False
            Panel_Create_Appraisal.Visible = False
            SaveRecords1.Visible = False
            SaveRecords.Visible = False
            btnSubmit_UpperMgr.Visible = False
            lblDiscuss.Visible = False
            tblEdit_form.Visible = False

            '--First Manager Appraisal View while Waiting for Second Manager Approval 
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 1 And Session("FIRST_MGT") = 1 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = "Waiting Approval"
            LblUP_MGT_Appr.ForeColor = Drawing.Color.Blue
            LblUP_MGT_Appr.Font.Bold = True

            Panel_Waiting_Approval.Visible = True
            Panel_Create_Appraisal.Visible = False
            Panel_Employee_Review.Visible = False
            Panel_Comments.Visible = False
            btnGeneralist.Visible = False
            btnSubmit_UpperMgr.Visible = False
            btnDiscuss.Visible = False
            SaveRecords1.Visible = False
            SaveRecords.Visible = False
            lblDiscuss.Visible = False
            tblEdit_form.Visible = False

            '--First Manager Appraisal View while Waiting for HR Generalist Approval 
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 2 And Session("FIRST_MGT") = 1 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = "Waiting Approval"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True
            Panel_Waiting_Approval.Visible = True

            Panel_Create_Appraisal.Visible = False
            Panel_Employee_Review.Visible = False
            Panel_Comments.Visible = False
            btnGeneralist.Visible = False
            btnSubmit_UpperMgr.Visible = False
            btnDiscuss.Visible = False
            SaveRecords1.Visible = False
            SaveRecords.Visible = False
            lblDiscuss.Visible = False
            tblEdit_form.Visible = False

            '--HR Generalist Approval View--
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 2 And Session("FIRST_MGT") = 0 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = "Waiting Approval"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True

            Panel_Waiting_Approval.Visible = True
            Panel_Comments.Visible = True
            btnDiscuss.Text = "Send back to " & DT.Rows(0)("MGT_Name").ToString & " for revision"
            lblDiscussionTitle.Text = "Send suggested revisions to " & DT.Rows(0)("MGT_Name").ToString & ""
            btnGeneralist.Text = "Approve " & lblEmpl_NAME.Text & "'s Appraisal"

            Panel_Employee_Review.Visible = False
            Panel_Create_Appraisal.Visible = False
            SaveRecords1.Visible = False
            SaveRecords.Visible = False
            btnSubmit_UpperMgr.Visible = False
            lblDiscuss.Visible = False
            tblEdit_form.Visible = False

            '--Back to the first manager after HR approved and waiting Send to Employee --
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 3 And Session("FIRST_MGT") = 1 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            btnEditForm.Text = "Edit " & lblEmpl_NAME.Text & "'s Appraisal"
            btnSendEmployee.Text = "Send Appraisal to " & lblEmpl_NAME.Text

            Panel_Waiting_Approval.Visible = True
            Panel_Create_Appraisal.Visible = False
            Panel_Employee_Review.Visible = False
            Panel_Comments.Visible = False
            btnGeneralist.Visible = False
            btnSubmit_UpperMgr.Visible = False
            btnDiscuss.Visible = False
            SaveRecords1.Visible = False
            SaveRecords.Visible = False
            lblDiscuss.Visible = False
            tblEdit_form.Visible = True

            '--Send to Employee and waiting Employee Approval
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 4 And Session("FIRST_MGT") = 1 Then
            txtCal.Visible = True
            imgCal.Visible = True
            RefuseSign.Visible = True
            lblRefuseText.Text = "If the Employee declined to esign their appraisal, please select the date you conducted the appraisal discussion. Then press the ""Employee Declined to Sign"" button to complete the process."

            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            btnEditForm.Text = "Edit " & lblEmpl_NAME.Text & "'s Appraisal"
            LblEMP_Appr.Text = "Waiting Approval"
            LblEMP_Appr.ForeColor = Drawing.Color.Blue
            LblEMP_Appr.Font.Bold = True

            Panel_Waiting_Approval.Visible = True
            Panel_Create_Appraisal.Visible = False
            Panel_Employee_Review.Visible = False
            Panel_Comments.Visible = False
            btnGeneralist.Visible = False
            btnSubmit_UpperMgr.Visible = False
            btnDiscuss.Visible = False
            SaveRecords1.Visible = False
            SaveRecords.Visible = False
            lblDiscuss.Visible = False
            tblEdit_form.Visible = True
            btnSendEmployee.Visible = False

            '---EMPLOYEE LOGON---

            '--Employee Rewiev---
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 4 And Session("FIRST_MGT") = 2 Then
            Response.Write("<center><b>Waiting Employees approval</b></center>")

            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblEMP_Appr.Text = "Waiting Approval"
            LblEMP_Appr.ForeColor = Drawing.Color.Blue
            LblEMP_Appr.Font.Bold = True
            Panel_Employee_Review.Visible = True
            Panel_Waiting_Approval.Visible = True
            'If Len(DT.Rows(0)("Comments").ToString) > 3 Then
            txbEmployeeComments.Visible = True
            'txbEmployeeComments.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
            'Else
            'txbEmployeeComments.Visible = False
            'txbEmployeeComments.Text = ""
            'trComments.Visible = False
            'End If

            Panel_Create_Appraisal.Visible = False
            Panel_Comments.Visible = False
            btnGeneralist.Visible = False
            btnSubmit_UpperMgr.Visible = False
            btnDiscuss.Visible = False
            SaveRecords1.Visible = False
            SaveRecords.Visible = False
            lblDiscuss.Visible = False
            btnSendEmployee.Visible = False
            btnEditForm.Visible = False

            '--Employee Approved--- 
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 5 And Session("FIRST_MGT") = 2 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblEMP_Appr.Text = DT.Rows(0)("DateEmpl_Appr").ToString
            Panel_Waiting_Approval.Visible = True
            Panel_Employee_Review.Visible = True

            If Len(DT.Rows(0)("Comments").ToString) > 4 Then
                If Len(DT.Rows(0)("DateEmpl_Refused").ToString) > 8 Then
                    trComments.Visible = False
                    lblEmployeeComments.Visible = True
                    lblEmployeeComments.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
                    lblEmployeeComments.ForeColor = Drawing.Color.Red
                    lblEmployeeComments.Font.Bold = True
                Else
                    lblEmployeeComments.Visible = True
                    lblEmployeeComments.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
                End If

            Else
                lblEmployeeComments.Visible = False
                lblEmployeeComments.Text = ""
                trComments.Visible = False
            End If

            trPressing.Visible = False
            trAgree.Visible = False
            btnSubmit.Visible = False
            txbEmployeeComments.Visible = False
            Panel_Create_Appraisal.Visible = False
            Panel_Comments.Visible = False
            btnGeneralist.Visible = False
            btnSubmit_UpperMgr.Visible = False
            btnDiscuss.Visible = False
            SaveRecords1.Visible = False
            SaveRecords.Visible = False
            lblDiscuss.Visible = False
            tblEdit_form.Visible = False
            btnSendEmployee.Visible = False

        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 5 And Session("FIRST_MGT") = 1 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblEMP_Appr.Text = DT.Rows(0)("DateEmpl_Appr").ToString
            Panel_Waiting_Approval.Visible = True
            Panel_Employee_Review.Visible = True

            If Len(DT.Rows(0)("Comments").ToString) > 4 Then
                If Len(DT.Rows(0)("DateEmpl_Refused").ToString) > 8 Then
                    trComments.Visible = False
                    lblEmployeeComments.Visible = True
                    lblEmployeeComments.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
                    lblEmployeeComments.ForeColor = Drawing.Color.Red
                    lblEmployeeComments.Font.Bold = True
                Else
                    lblEmployeeComments.Visible = True
                    lblEmployeeComments.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
                End If

            Else
                lblEmployeeComments.Visible = False
                lblEmployeeComments.Text = ""
                trComments.Visible = False
            End If

            trPressing.Visible = False
            trAgree.Visible = False
            btnSubmit.Visible = False
            txbEmployeeComments.Visible = False
            Panel_Create_Appraisal.Visible = False
            Panel_Comments.Visible = False
            btnGeneralist.Visible = False
            btnSubmit_UpperMgr.Visible = False
            btnDiscuss.Visible = False
            SaveRecords1.Visible = False
            SaveRecords.Visible = False
            lblDiscuss.Visible = False
            tblEdit_form.Visible = False
            btnSendEmployee.Visible = False

        End If

        'Panel_Create_Appraisal
        'Panel_Goal_Setting_Edit
        'Panel_Waiting_Approval
        'Panel_Employee_Review
        ' If CDbl(DT.Rows(0)("Process_Flag").ToString) = 5 Then Panel_Goal_Setting_Edit.Visible = False Else Panel_Goal_Setting_Edit.Visible = True
    End Sub

    Protected Sub ShowButtonCursor()
        Delete_Accom2.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Accom3.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Accom4.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Accom5.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Accom6.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Accom7.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Accom8.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Accom9.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Accom10.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : BtnNew_Accomp.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")

        Delete_Goal2.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal3.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal4.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal5.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal6.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal7.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal8.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : Delete_Goal9.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Delete_Goal10.Attributes.Add("onMouseOver", "this.style.cursor='pointer';") : btnNew_Goal.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")

        btnDiscuss.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnGeneralist.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnSubmit_UpperMgr.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnEditForm.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnSendEmployee.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnSubmit.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        RefuseSign.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")


    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If Session("SAP") = "GLD" Then
            Response.Redirect("..\..\Default_Appaisal.aspx")
        Else
            Response.Redirect("..\..\Default_Manager.aspx")
        End If
    End Sub
    Protected Sub BtnNew_Accomp_Click(sender As Object, e As EventArgs) Handles BtnNew_Accomp.Click
        SQL = "select count(*)CNT_FUT from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " "
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        If CDbl(DT.Rows(0)("CNT_FUT").ToString) = 0 Then
            SQL3 = " insert into Appraisal_Accomplishments_TBL (EMPLID,Perf_Year,IndexID,Accomplishment) values(" & lblEMPLID.Text & "," & lblYEAR.Text & ",1,'')"
            'Response.Write(SQL3) : Response.End()
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()
        Else
            SQL2 = "select Max(IndexId)+1 NewIndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " "
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
            SQL4 &= " insert into Appraisal_Accomplishments_TBL (EMPLID,Perf_Year,IndexID,Accomplishment) values"
            SQL4 &= " (" & lblEMPLID.Text & ", " & lblYEAR.Text & " ," & DT2.Rows(0)("NewIndexID").ToString & ",'')"
            'Response.Write(SQL2 & "<br>" & SQL4) : Response.End()
            DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
            LocalClass.CloseSQLServerConnection()

            SQL3 = "select Max(IndexId) NextIndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " "
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()

            If CDbl(DT3.Rows(0)("NextIndexID").ToString) > 10 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('A maximum of 10 Accomplishments has been exceeded.'); </script>")
                SQL2 &= "delete Appraisal_Accomplishments_TBL where IndexID>10"
                DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
                LocalClass.CloseSQLServerConnection()

            Else
                ShowHideAccomplishmentPanel()
            End If
        End If


    End Sub

    Protected Sub ShowHideAccomplishmentPanel()

        SQL6 = "select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10 "
        SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,"
        SQL6 &= " (case when IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*) else 0 end)F_IND7,"
        SQL6 &= " (case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
        SQL6 &= " from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " group by IndexID)A"
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_Accomp2.Visible = True : Panel_Accomp2_View.Visible = True Else Panel_Accomp2.Visible = False : Panel_Accomp2_View.Visible = False
        If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_Accomp3.Visible = True : Panel_Accomp3_View.Visible = True Else Panel_Accomp3.Visible = False : Panel_Accomp3_View.Visible = False
        If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_Accomp4.Visible = True : Panel_Accomp4_View.Visible = True Else Panel_Accomp4.Visible = False : Panel_Accomp4_View.Visible = False
        If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_Accomp5.Visible = True : Panel_Accomp5_View.Visible = True Else Panel_Accomp5.Visible = False : Panel_Accomp5_View.Visible = False
        If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel_Accomp6.Visible = True : Panel_Accomp6_View.Visible = True Else Panel_Accomp6.Visible = False : Panel_Accomp6_View.Visible = False
        If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel_Accomp7.Visible = True : Panel_Accomp7_View.Visible = True Else Panel_Accomp7.Visible = False : Panel_Accomp7_View.Visible = False
        If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel_Accomp8.Visible = True : Panel_Accomp8_View.Visible = True Else Panel_Accomp8.Visible = False : Panel_Accomp8_View.Visible = False
        If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel_Accomp9.Visible = True : Panel_Accomp9_View.Visible = True Else Panel_Accomp9.Visible = False : Panel_Accomp9_View.Visible = False
        If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel_Accomp10.Visible = True : Panel_Accomp10_View.Visible = True Else Panel_Accomp10.Visible = False : Panel_Accomp10_View.Visible = False

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub Delete_Accom2_Click(sender As Object, e As EventArgs) Handles Delete_Accom2.Click
        SQL = "delete Appraisal_Accomplishments_TBL where IndexID=2 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_Accomplishments_TBL Set IndexID=IndexID-1 where IndexID>2 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and IndexID=2"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideAccomplishmentPanel()
        Else
            DisplayData()
            ShowHideAccomplishmentPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Accom3_Click(sender As Object, e As EventArgs) Handles Delete_Accom3.Click
        SQL = "delete Appraisal_Accomplishments_TBL where IndexID=3 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_Accomplishments_TBL Set IndexID=IndexID-1 where IndexID>3 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and IndexID=3"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideAccomplishmentPanel()
        Else
            DisplayData()
            ShowHideAccomplishmentPanel()
        End If

        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Accom4_Click(sender As Object, e As EventArgs) Handles Delete_Accom4.Click
        SQL = "delete Appraisal_Accomplishments_TBL where IndexID=4 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_Accomplishments_TBL Set IndexID=IndexID-1 where IndexID>4 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and IndexID=4"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideAccomplishmentPanel()
        Else
            DisplayData()
            ShowHideAccomplishmentPanel()
        End If
        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Accom5_Click(sender As Object, e As EventArgs) Handles Delete_Accom5.Click
        SQL = "delete Appraisal_Accomplishments_TBL where IndexID=5 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_Accomplishments_TBL Set IndexID=IndexID-1 where IndexID>5 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and IndexID=5"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideAccomplishmentPanel()
        Else
            DisplayData()
            ShowHideAccomplishmentPanel()
        End If
        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Accom6_Click(sender As Object, e As EventArgs) Handles Delete_Accom6.Click
        SQL = "delete Appraisal_Accomplishments_TBL where IndexID=6 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_Accomplishments_TBL Set IndexID=IndexID-1 where IndexID>6 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and IndexID=6"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideAccomplishmentPanel()
        Else
            DisplayData()
            ShowHideAccomplishmentPanel()
        End If
        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Accom7_Click(sender As Object, e As EventArgs) Handles Delete_Accom7.Click
        SQL = "delete Appraisal_Accomplishments_TBL where IndexID=7 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_Accomplishments_TBL Set IndexID=IndexID-1 where IndexID>7 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and IndexID=7"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideAccomplishmentPanel()
        Else
            DisplayData()
            ShowHideAccomplishmentPanel()
        End If
        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Accom8_Click(sender As Object, e As EventArgs) Handles Delete_Accom8.Click
        SQL = "delete Appraisal_Accomplishments_TBL where IndexID=8 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_Accomplishments_TBL Set IndexID=IndexID-1 where IndexID>8 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and IndexID=8"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideAccomplishmentPanel()
        Else
            DisplayData()
            ShowHideAccomplishmentPanel()
        End If
        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Accom9_Click(sender As Object, e As EventArgs) Handles Delete_Accom9.Click
        SQL = "delete Appraisal_Accomplishments_TBL where IndexID=9 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_Accomplishments_TBL Set IndexID=IndexID-1 where IndexID>9 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and IndexID=9"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideAccomplishmentPanel()
        Else
            DisplayData()
            ShowHideAccomplishmentPanel()
        End If
        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub
    Protected Sub Delete_Accom10_Click(sender As Object, e As EventArgs) Handles Delete_Accom10.Click
        SQL = "delete Appraisal_Accomplishments_TBL where IndexID=10 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_Accomplishments_TBL Set IndexID=IndexID-1 where IndexID>10 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and IndexID=10"
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        LocalClass.CloseSQLServerConnection()

        If CDbl(DT3.Rows(0)("IndexID").ToString) > 0 Then
            DisplayData()
            ShowHideAccomplishmentPanel()
        Else
            DisplayData()
            ShowHideAccomplishmentPanel()
        End If
        'Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub

    Protected Sub SaveData()
        SQL1 = "Update Appraisal_Master_TBL Set Strengths='" & Replace(Replace(Replace(Trim(txbStrengths.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= "Development_Area='" & Replace(Replace(Replace(Trim(txbDevelopment_Areas.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= "Overall_Summary='" & Replace(Replace(Replace(Trim(txbOverall_Sum.Text), "'", "`"), "<", "{"), ">", "}") & "'"
        'SQL1 &= " ,Development_Objective='" & Replace(Replace(Replace(Trim(txbDevelopment_Objective.Text), "'", "`"), "<", "{"), ">", "}") & "'"
        '---Oveall Performance Rating
        If rbBelow.Checked = True Then SQL1 &= ",Overall_Rating=1"
        If rbNeed.Checked = True Then SQL1 &= ",Overall_Rating=2"
        If rbMeet.Checked = True Then SQL1 &= ",Overall_Rating=3"
        If rbExceed.Checked = True Then SQL1 &= ",Overall_Rating=4"
        If rbDisting.Checked = True Then SQL1 &= ",Overall_Rating=5"
        '1. Make Balanced Decisions
        If rbMake_Need1.Checked = True Then SQL1 &= ",Make_Balance=1"
        If rbMake_Prof1.Checked = True Then SQL1 &= ",Make_Balance=2"
        If rbMake_Exce1.Checked = True Then SQL1 &= ",Make_Balance=3"
        '2. Build Trust
        If rbBuild2_Need1.Checked = True Then SQL1 &= ",Build_Trust=1"
        If rbBuild2_Prof1.Checked = True Then SQL1 &= ",Build_Trust=2"
        If rbBuild2_Exce1.Checked = True Then SQL1 &= ",Build_Trust=3"
        '3. Learn Continuously
        If rbLearn_Need1.Checked = True Then SQL1 &= ",Learn_Continuously=1"
        If rbLearn_Prof1.Checked = True Then SQL1 &= ",Learn_Continuously=2"
        If rbLearn_Exce1.Checked = True Then SQL1 &= ",Learn_Continuously=3"
        '4. Lead with Urgency & Purpose
        If rbLead2_Need1.Checked = True Then SQL1 &= ",Lead_Urgency=1"
        If rbLead2_Prof1.Checked = True Then SQL1 &= ",Lead_Urgency=2"
        If rbLead2_Exce1.Checked = True Then SQL1 &= ",Lead_Urgency=3"
        '5. Promote Collaboration & Accountability
        If rbProm_Need1.Checked = True Then SQL1 &= ",Promote_Collab=1"
        If rbProm_Prof1.Checked = True Then SQL1 &= ",Promote_Collab=2"
        If rbProm_Exce1.Checked = True Then SQL1 &= ",Promote_Collab=3"
        '6. Confront Challenges
        If rbConf_Need1.Checked = True Then SQL1 &= ",Confront_Challenge=1"
        If rbConf_Prof1.Checked = True Then SQL1 &= ",Confront_Challenge=2"
        If rbConf_Exce1.Checked = True Then SQL1 &= ",Confront_Challenge=3"
        '7. Lead Change
        If rbLead_Need1.Checked = True Then SQL1 &= ",Lead_Change=1"
        If rbLead_Prof1.Checked = True Then SQL1 &= ",Lead_Change=2"
        If rbLead_Exce1.Checked = True Then SQL1 &= ",Lead_Change=3"
        '8. Inspire Risk Taking & innovation
        If rbInsp_Need1.Checked = True Then SQL1 &= ",Inspire_Risk=1"
        If rbInsp_Prof1.Checked = True Then SQL1 &= ",Inspire_Risk=2"
        If rbInsp_Exce1.Checked = True Then SQL1 &= ",Inspire_Risk=3"
        '9. Leverage External Perspective
        If rbLeve_Need1.Checked = True Then SQL1 &= ",Leverage_External=1"
        If rbLeve_Prof1.Checked = True Then SQL1 &= ",Leverage_External=2"
        If rbLeve_Exce1.Checked = True Then SQL1 &= ",Leverage_External=3"
        '10. Communicate for Impact
        If rbComm_Need1.Checked = True Then SQL1 &= ",Communic_Impact=1"
        If rbComm_Prof1.Checked = True Then SQL1 &= ",Communic_Impact=2"
        If rbComm_Exce1.Checked = True Then SQL1 &= ",Communic_Impact=3"

        SQL1 &= " where Perf_Year = " & lblYEAR.Text & " And EMPLID = " & lblEMPLID.Text & " "

        '--Update Accomplishments table
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp1.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= " where IndexId=1 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp2.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= " where IndexId=2 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp3.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= " where IndexId=3 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & ""
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp4.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= " where IndexId=4 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp5.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= "  where IndexId=5 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & ""
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp6.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= " where IndexId=6 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp7.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= " where IndexId=7 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp8.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= " where IndexId=8 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp9.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= " where IndexId=9 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp10.Text), "'", "`"), "<", "{"), ">", "}") & "' "
        SQL1 &= " where IndexId=10 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        '--Update FutureGoals table
        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal1.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess1.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate1.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=1 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal2.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess2.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate2.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=2 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal3.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess3.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate3.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=3 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal4.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess4.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate4.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=4 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal5.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess5.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate5.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=5 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal6.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess6.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate6.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=6 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal7.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess7.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate7.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=7 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal8.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess8.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate8.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=8 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal9.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess9.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate9.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=9 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_FutureGoals_tbl Set Goals='" & Replace(Replace(Replace(Trim(txbGoal10.Text), "'", "`"), "<", "{"), ">", "}") & "',Milestones='" & Replace(Replace(Replace(Trim(txbSuccess10.Text), "'", "`"), "<", "{"), ">", "}") & "',"
        SQL1 &= " TargetDate='" & Replace(Replace(Replace(Trim(txbDate10.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexId=10 and Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text

        SQL1 &= " Update Appraisal_Master_tbl Set Comments='" & Replace(Replace(Replace(txbEmployeeComments.Text, "'", "`"), "<", "{"), ">", "}") & "' where emplid=" & lblEMPLID.Text & "  and Perf_Year=" & lblYEAR.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub DisplayData()

        '--Show/Hide Panels
        ShowHideAccomplishmentPanel()
        ShowHideGoalPanel()

        SQL1 = "select Process_Flag from Appraisal_Master_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        '--Master table
        SQL = "select * from(select * from Appraisal_Master_TBL  where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A,"
        '--Accomplishments table 
        SQL &= " (select Accomp1,IsNull(Accomp2,'')Accomp2,IsNull(Accomp3,'')Accomp3,IsNull(Accomp4,'')Accomp4,IsNull(Accomp5,'')Accomp5,IsNull(Accomp6,'')Accomp6,IsNull(Accomp7,'')Accomp7,IsNull(Accomp8,'')Accomp8,IsNull(Accomp9,'')Accomp9,"
        SQL &= " IsNull(Accomp10,'')Accomp10 from(select A8.Emplid,Accomp1,Accomp2,Accomp3,Accomp4,Accomp5,Accomp6,Accomp7,Accomp8,Accomp9,Accomp10 from(select A7.Emplid,Accomp1,Accomp2,Accomp3,Accomp4,Accomp5,Accomp6,Accomp7,Accomp8,Accomp9 from("
        SQL &= " select A6.Emplid,Accomp1,Accomp2,Accomp3,Accomp4,Accomp5,Accomp6,Accomp7,Accomp8 from(select A5.Emplid,Accomp1,Accomp2,Accomp3,Accomp4,Accomp5,Accomp6,Accomp7 from(select A4.Emplid,Accomp1,Accomp2,Accomp3,Accomp4,Accomp5,Accomp6 from("
        SQL &= " select A3.Emplid,Accomp1,Accomp2,Accomp3,Accomp4,Accomp5 from(select A2.Emplid,Accomp1,Accomp2,Accomp3,Accomp4 from(select A1.Emplid,Accomp1,Accomp2,Accomp3 from(select AC1.Emplid,Accomp1,Accomp2 from "
        SQL &= " (select emplid,Accomplishment Accomp1 from Appraisal_Accomplishments_TBL where IndexID=1 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac1 Left JOIN"
        SQL &= " (select emplid,Accomplishment Accomp2 from Appraisal_Accomplishments_TBL where IndexID=2 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac2"
        SQL &= " ON Ac1.emplid=Ac2.emplid)A1 LEFT JOIN "
        SQL &= " (select emplid,Accomplishment Accomp3 from Appraisal_Accomplishments_TBL where IndexID=3 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac3"
        SQL &= " ON A1.emplid=Ac3.emplid)A2 LEFT JOIN "
        SQL &= " (select emplid,Accomplishment Accomp4 from Appraisal_Accomplishments_TBL where IndexID=4 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac4"
        SQL &= " ON A2.emplid=Ac4.emplid)A3 LEFT JOIN "
        SQL &= " (select emplid,Accomplishment Accomp5 from Appraisal_Accomplishments_TBL where IndexID=5 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac5"
        SQL &= " ON A3.emplid=Ac5.emplid)A4 LEFT JOIN "
        SQL &= " (select emplid,Accomplishment Accomp6 from Appraisal_Accomplishments_TBL where IndexID=6 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac6"
        SQL &= " ON A4.emplid=Ac6.emplid)A5 LEFT JOIN "
        SQL &= " (select emplid,Accomplishment Accomp7 from Appraisal_Accomplishments_TBL where IndexID=7 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac7"
        SQL &= " ON A5.emplid=Ac7.emplid)A6 LEFT JOIN "
        SQL &= " (select emplid,Accomplishment Accomp8 from Appraisal_Accomplishments_TBL where IndexID=8 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac8"
        SQL &= " ON A6.emplid=Ac8.emplid)A7 LEFT JOIN "
        SQL &= " (select emplid,Accomplishment Accomp9 from Appraisal_Accomplishments_TBL where IndexID=9 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac9"
        SQL &= " ON A7.emplid=Ac9.emplid)A8 LEFT JOIN "
        SQL &= " (select emplid,Accomplishment Accomp10 from Appraisal_Accomplishments_TBL where IndexID=10 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")Ac10"
        SQL &= " ON A8.emplid=Ac10.emplid)A10)B,"

        If CDbl(DT1.Rows(0)("Process_Flag").ToString) < 5 Then '--Before Employee Approve 
            '--Future Goals table 
            SQL &= " (select Goals1,Milestones1,TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2,IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,"
            SQL &= " IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4,IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,"
            SQL &= " IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6,IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,"
            SQL &= " IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10"
            SQL &= " from( select A8.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,"
            SQL &= " Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9,Goals10,Milestones10,TargetDate10 from(select A7.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,"
            SQL &= " Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9 from("
            SQL &= " select A6.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,"
            SQL &= " Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8 from(select A5.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,"
            SQL &= " Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7 from(select A4.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,"
            SQL &= " Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6 from(select A3.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,"
            SQL &= " TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5 from(select A2.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4 from("
            SQL &= " select A1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3 from(select AC1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2 from "
            SQL &= " (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoals_tbl where IndexID=1 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac1 "
            SQL &= " LEFT JOIN"
            SQL &= " (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoals_tbl where IndexID=2 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac2"
            SQL &= " ON Ac1.emplid=Ac2.emplid)A1 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoals_tbl where IndexID=3 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac3"
            SQL &= " ON A1.emplid=Ac3.emplid)A2 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoals_tbl where IndexID=4 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac4"
            SQL &= " ON A2.emplid=Ac4.emplid )A3 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoals_tbl where IndexID=5 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac5"
            SQL &= " ON A3.emplid=Ac5.emplid)A4 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoals_tbl where IndexID=6 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac6"
            SQL &= " ON A4.emplid=Ac6.emplid)A5 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoals_tbl where IndexID=7 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac7"
            SQL &= " ON A5.emplid=Ac7.emplid )A6 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoals_tbl where IndexID=8 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac8"
            SQL &= " ON A6.emplid=Ac8.emplid)A7 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoals_tbl where IndexID=9 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac9"
            SQL &= " ON A7.emplid=Ac9.emplid)A8 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoals_tbl where IndexID=10 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0)Ac10"
            SQL &= " ON A8.emplid=Ac10.emplid)A10)c"
        Else '--After employee approved
            '--Future Goals table 
            SQL &= " (select Goals1,Milestones1,TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2,IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,"
            SQL &= " IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4,IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,"
            SQL &= " IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6,IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,"
            SQL &= " IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10"
            SQL &= " from( select A8.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,"
            SQL &= " Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9,Goals10,Milestones10,TargetDate10 from(select A7.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,"
            SQL &= " Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8,Goals9,Milestones9,TargetDate9 from("
            SQL &= " select A6.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,"
            SQL &= " Goals7,Milestones7,TargetDate7,Goals8,Milestones8,TargetDate8 from(select A5.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,"
            SQL &= " Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6,Goals7,Milestones7,TargetDate7 from(select A4.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,"
            SQL &= " Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5,Goals6,Milestones6,TargetDate6 from(select A3.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,"
            SQL &= " TargetDate3,Goals4,Milestones4,TargetDate4,Goals5,Milestones5,TargetDate5 from(select A2.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3,Goals4,Milestones4,TargetDate4 from("
            SQL &= " select A1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2,Goals3,Milestones3,TargetDate3 from(select AC1.Emplid,Goals1,Milestones1,TargetDate1,Goals2,Milestones2,TargetDate2 from "
            SQL &= " (select emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoal_Recall_tbl where IndexID=1 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac1  LEFT JOIN "
            SQL &= " (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoal_Recall_tbl where IndexID=2 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac2  ON Ac1.emplid=Ac2.emplid)A1 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoal_Recall_tbl where IndexID=3 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac3  ON A1.emplid=Ac3.emplid)A2 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoal_Recall_tbl where IndexID=4 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac4  ON A2.emplid=Ac4.emplid )A3 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoal_Recall_tbl where IndexID=5 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac5  ON A3.emplid=Ac5.emplid)A4 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoal_Recall_tbl where IndexID=6 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac6  ON A4.emplid=Ac6.emplid)A5 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoal_Recall_tbl where IndexID=7 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac7  ON A5.emplid=Ac7.emplid )A6 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoal_Recall_tbl where IndexID=8 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac8  ON A6.emplid=Ac8.emplid)A7 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoal_Recall_tbl where IndexID=9 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac9  ON A7.emplid=Ac9.emplid)A8 LEFT JOIN "
            SQL &= " (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoal_Recall_tbl where IndexID=10 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid= " & lblEMPLID.Text & " and Appr_Goals=0"
            SQL &= " and Recall_Date in (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") )Ac10  ON A8.emplid=Ac10.emplid)A10)c"
        End If
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '---Oveall Performance Rating
        If DT.Rows(0)("Overall_rating").ToString = 1 Then rbBelow.Checked = True : rbBelow1.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 2 Then rbNeed.Checked = True : rbNeed1.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 3 Then rbMeet.Checked = True : rbMeet1.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 4 Then rbExceed.Checked = True : rbExceed1.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 5 Then rbDisting.Checked = True : rbDisting1.Checked = True

        '1. Make Balanced Decisions
        If DT.Rows(0)("Make_Balance").ToString = 1 Then rbMake_Need1.Checked = True : rbMake_Need.Checked = True
        If DT.Rows(0)("Make_Balance").ToString = 2 Then rbMake_Prof1.Checked = True : rbMake_Prof.Checked = True
        If DT.Rows(0)("Make_Balance").ToString = 3 Then rbMake_Exce1.Checked = True : rbMake_Exce.Checked = True
        '2. Build Trust
        If DT.Rows(0)("Build_Trust").ToString = 1 Then rbBuild2_Need1.Checked = True : rbBuild2_Need.Checked = True
        If DT.Rows(0)("Build_Trust").ToString = 2 Then rbBuild2_Prof1.Checked = True : rbBuild2_Prof.Checked = True
        If DT.Rows(0)("Build_Trust").ToString = 3 Then rbBuild2_Exce1.Checked = True : rbBuild2_Exce.Checked = True
        '3. Learn Continuously
        If DT.Rows(0)("Learn_Continuously").ToString = 1 Then rbLearn_Need1.Checked = True : rbLearn_Need.Checked = True
        If DT.Rows(0)("Learn_Continuously").ToString = 2 Then rbLearn_Prof1.Checked = True : rbLearn_Prof.Checked = True
        If DT.Rows(0)("Learn_Continuously").ToString = 3 Then rbLearn_Exce1.Checked = True : rbLearn_Exce.Checked = True
        '4. Lead with Urgency & Purpose
        If DT.Rows(0)("Lead_Urgency").ToString = 1 Then rbLead2_Need1.Checked = True : rbLead2_Need.Checked = True
        If DT.Rows(0)("Lead_Urgency").ToString = 2 Then rbLead2_Prof1.Checked = True : rbLead2_Prof.Checked = True
        If DT.Rows(0)("Lead_Urgency").ToString = 3 Then rbLead2_Exce1.Checked = True : rbLead2_Exce.Checked = True
        '5. Promote Collaboration & Accountability
        If DT.Rows(0)("Promote_Collab").ToString = 1 Then rbProm_Need1.Checked = True : rbProm_Need.Checked = True
        If DT.Rows(0)("Promote_Collab").ToString = 2 Then rbProm_Prof1.Checked = True : rbProm_Prof.Checked = True
        If DT.Rows(0)("Promote_Collab").ToString = 3 Then rbProm_Exce1.Checked = True : rbProm_Exce.Checked = True
        '6. Confront Challenges
        If DT.Rows(0)("Confront_Challenge").ToString = 1 Then rbConf_Need1.Checked = True : rbConf_Need.Checked = True
        If DT.Rows(0)("Confront_Challenge").ToString = 2 Then rbConf_Prof1.Checked = True : rbConf_Prof.Checked = True
        If DT.Rows(0)("Confront_Challenge").ToString = 3 Then rbConf_Exce1.Checked = True : rbConf_Exce.Checked = True
        '7. Lead Change
        If DT.Rows(0)("Lead_Change").ToString = 1 Then rbLead_Need1.Checked = True : rbLead_Need.Checked = True
        If DT.Rows(0)("Lead_Change").ToString = 2 Then rbLead_Prof1.Checked = True : rbLead_Prof.Checked = True
        If DT.Rows(0)("Lead_Change").ToString = 3 Then rbLead_Exce1.Checked = True : rbLead_Exce.Checked = True
        '8. Inspire Risk Taking & innovation
        If DT.Rows(0)("Inspire_Risk").ToString = 1 Then rbInsp_Need1.Checked = True : rbInsp_Need.Checked = True
        If DT.Rows(0)("Inspire_Risk").ToString = 2 Then rbInsp_Prof1.Checked = True : rbInsp_Prof.Checked = True
        If DT.Rows(0)("Inspire_Risk").ToString = 3 Then rbInsp_Exce1.Checked = True : rbInsp_Exce.Checked = True
        '9. Leverage External Perspective
        If DT.Rows(0)("Leverage_External").ToString = 1 Then rbLeve_Need1.Checked = True : rbLeve_Need.Checked = True
        If DT.Rows(0)("Leverage_External").ToString = 2 Then rbLeve_Prof1.Checked = True : rbLeve_Prof.Checked = True
        If DT.Rows(0)("Leverage_External").ToString = 3 Then rbLeve_Exce1.Checked = True : rbLeve_Exce.Checked = True
        '10. Communicate for Impact
        If DT.Rows(0)("Communic_Impact").ToString = 1 Then rbComm_Need1.Checked = True : rbComm_Need.Checked = True
        If DT.Rows(0)("Communic_Impact").ToString = 2 Then rbComm_Prof1.Checked = True : rbComm_Prof.Checked = True
        If DT.Rows(0)("Communic_Impact").ToString = 3 Then rbComm_Exce1.Checked = True : rbComm_Exce.Checked = True

        '--Summary,Strengths,Development First Manager--
        txbStrengths.Text = Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, "{", "<"), "}", ">"), "`", "'")
        txbDevelopment_Areas.Text = Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, "{", "<"), "}", ">"), "`", "'")
        txbOverall_Sum.Text = Replace(Replace(Replace(DT.Rows(0)("OverAll_Summary").ToString, "{", "<"), "}", ">"), "`", "'")
        'txbDevelopment_Objective.Text = Replace(Replace(Replace(DT.Rows(0)("Development_Objective").ToString, "{", "<"), "}", ">"), "`", "'")

        '--Summary,Strengths,Development  Waiting Approval and Review---
        txbStrengthsA.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDevelopment_AreasA.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbOverall_SumA.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("OverAll_Summary").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        'txbDevelopment_ObjectiveA.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Objective").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")

        '--Accomplishments First Manager---
        txbAccomp1.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp1").ToString, "{", "<"), "}", ">"), "`", "'")
        txbAccomp2.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp2").ToString, "{", "<"), "}", ">"), "`", "'")
        txbAccomp3.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp3").ToString, "{", "<"), "}", ">"), "`", "'")
        txbAccomp4.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp4").ToString, "{", "<"), "}", ">"), "`", "'")
        txbAccomp5.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp5").ToString, "{", "<"), "}", ">"), "`", "'")
        txbAccomp6.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp6").ToString, "{", "<"), "}", ">"), "`", "'")
        txbAccomp7.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp7").ToString, "{", "<"), "}", ">"), "`", "'")
        txbAccomp8.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp8").ToString, "{", "<"), "}", ">"), "`", "'")
        txbAccomp9.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp9").ToString, "{", "<"), "}", ">"), "`", "'")
        txbAccomp10.Text = Replace(Replace(Replace(DT.Rows(0)("Accomp10").ToString, "{", "<"), "}", ">"), "`", "'")

        '--Accomplishments Waiting Approval and Review---
        txbAccomp1A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp1").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbAccomp2A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp2").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbAccomp3A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp3").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbAccomp4A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp4").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbAccomp5A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp5").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbAccomp6A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp6").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbAccomp7A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp7").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbAccomp8A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp8").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbAccomp9A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp9").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbAccomp10A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp10").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")

        '--Goals First Manager--
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

        '--Goals Waiting Approval and Review--
        txbGoal1A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals1").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess1A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones1").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate1A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate1").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbGoal2A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals2").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess2A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones2").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate2A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate2").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbGoal3A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals3").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess3A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones3").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate3A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate3").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbGoal4A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals4").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess4A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones4").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate4A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate4").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbGoal5A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals5").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess5A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones5").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate5A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate5").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbGoal6A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals6").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess6A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones6").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate6A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate6").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbGoal7A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals7").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess7A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones7").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate7A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate7").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbGoal8A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals8").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess8A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones8").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate8A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate8").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbGoal9A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals9").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess9A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones9").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate9A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate9").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbGoal10A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Goals10").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbSuccess10A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Milestones10").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDate10A.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("TargetDate10").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub Img_Print1_Click(sender As Object, e As ImageClickEventArgs) Handles Img_Print1.Click
        Response.Write("<script>window.open('Appraisal_Print.aspx?Token=" + Request.QueryString("Token") + "','_blank' );</script>")
    End Sub
    Protected Sub Img_Print2_Click(sender As Object, e As ImageClickEventArgs) Handles Img_Print2.Click
        Response.Write("<script>window.open('Appraisal_Print.aspx?Token=" + Request.QueryString("Token") + "','_blank' );</script>")
    End Sub

    Protected Sub Accomplishments1_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp1.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL13 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment='" & Replace(Replace(Replace(RTrim(txbAccomp1.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=1 and Perf_Year=" & lblYEAR.Text & " and EMPLID=" & lblEMPLID.Text & ""
            'Response.Write(SQL13) : Response.End()
            DT13 = LocalClass.ExecuteSQLDataSet(SQL13)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Accomplishments2_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp2.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp2.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=2 and Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Accomplishments3_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp3.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp3.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=3 and Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Accomplishments4_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp4.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp4.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=4 and Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Accomplishments5_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp5.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp5.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=5 and Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Accomplishments6_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp6.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp6.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=6 and Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Accomplishments7_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp7.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp7.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=7 and Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Accomplishments8_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp8.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp8.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=8 and Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Accomplishments9_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp9.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp9.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=9 and Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Accomplishments10_TextChanged(sender As Object, e As EventArgs) Handles txbAccomp10.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Trim(txbAccomp10.Text), "'", "`"), "<", "{"), ">", "}") & "' where IndexId=10 and Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Strengths_TextChanged(sender As Object, e As EventArgs) Handles txbStrengths.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Master_TBL Set Strengths='" & Replace(Replace(Replace(Trim(txbStrengths.Text), "'", "`"), "<", "{"), ">", "}") & "' where Perf_Year=" & lblYEAR.Text & " and EMPLID=" & lblEMPLID.Text & " "
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Development_Areas_TextChanged(sender As Object, e As EventArgs) Handles txbDevelopment_Areas.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Master_TBL Set Development_Area= '" & Replace(Replace(Replace(Trim(txbDevelopment_Areas.Text), "'", "`"), "<", "{"), ">", "}") & "' where Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Overall_Sum_TextChanged(sender As Object, e As EventArgs) Handles txbOverall_Sum.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Master_TBL Set Overall_Summary= '" & Replace(Replace(Replace(Trim(txbOverall_Sum.Text), "'", "`"), "<", "{"), ">", "}") & "' where Perf_Year =" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Development_Objective_TextChanged(sender As Object, e As EventArgs) Handles txbDevelopment_Objective.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_Master_TBL Set Development_Objective= '" & Replace(Replace(Replace(Trim(txbDevelopment_Objective.Text), "'", "`"), "<", "{"), ">", "}") & "' where Perf_Year=" & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub

    Protected Sub rbBelow_CheckedChanged(sender As Object, e As EventArgs) Handles rbBelow.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Overall_Rating=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbNeed_CheckedChanged(sender As Object, e As EventArgs) Handles rbNeed.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Overall_Rating=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbMeet_CheckedChanged(sender As Object, e As EventArgs) Handles rbMeet.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Overall_Rating=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbExceed_CheckedChanged(sender As Object, e As EventArgs) Handles rbExceed.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Overall_Rating=4 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbDisting_CheckedChanged(sender As Object, e As EventArgs) Handles rbDisting.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Overall_Rating=5 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub

    Protected Sub rbMake_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbMake_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Make_Balance=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbMake_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbMake_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Make_Balance=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbMake_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbMake_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Make_Balance=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbBuild2_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbBuild2_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Build_Trust=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbBuild2_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbBuild2_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Build_Trust=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbBuild2_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbBuild2_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Build_Trust=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLearn_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLearn_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Learn_Continuously=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLearn_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLearn_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Learn_Continuously=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLearn_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLearn_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Learn_Continuously=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLead2_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLead2_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Lead_Urgency=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLead2_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLead2_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Lead_Urgency=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLead2_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLead2_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Lead_Urgency=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbProm_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbProm_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Promote_Collab=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbProm_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbProm_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Promote_Collab=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbProm_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbProm_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Promote_Collab=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbConf_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbConf_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Confront_Challenge=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbConf_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbConf_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Confront_Challenge=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbConf_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbConf_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Confront_Challenge=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLead_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLead_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Lead_Change=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLead_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLead_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Lead_Change=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLead_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLead_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Lead_Change=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbInsp_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbInsp_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Inspire_Risk=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbInsp_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbInsp_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Inspire_Risk=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbInsp_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbInsp_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Inspire_Risk=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLeve_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLeve_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Leverage_External=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLeve_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLeve_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Leverage_External=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbLeve_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbLeve_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Leverage_External=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbComm_Need1_CheckedChanged(sender As Object, e As EventArgs) Handles rbComm_Need1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Communic_Impact=1 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbComm_Prof1_CheckedChanged(sender As Object, e As EventArgs) Handles rbComm_Prof1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Communic_Impact=2 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub rbComm_Exce1_CheckedChanged(sender As Object, e As EventArgs) Handles rbComm_Exce1.CheckedChanged
        Dim x As CheckBox = sender
        If x.Checked = True Then
            SQL &= " Update Appraisal_Master_TBL Set Communic_Impact=3 where Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub

    Protected Sub btnNew_Goal_Click(sender As Object, e As EventArgs) Handles btnNew_Goal.Click
        SQL = "select count(*)CNT_FUT from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " "
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        If CDbl(DT.Rows(0)("CNT_FUT").ToString) = 0 Then
            SQL3 = " insert into Appraisal_FutureGoals_tbl (EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate,DateEmpl_Appr,MGT_EMPLID) values(" & lblEMPLID.Text & "," & lblYEAR.Text + 1 & ",1,0,'','','',NULL,'')"
            'Response.Write(SQL3) : Response.End()
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            LocalClass.CloseSQLServerConnection()
        Else
            SQL2 = "select Max(IndexId)+1 NewIndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " "
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
            SQL4 &= " insert into Appraisal_FutureGoals_tbl (EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate,DateEmpl_Appr,MGT_EMPLID) values"
            SQL4 &= " (" & lblEMPLID.Text & ", " & lblYEAR.Text + 1 & " ," & DT2.Rows(0)("NewIndexID").ToString & ",0,'','','',NULL,'')"
            'Response.Write(SQL2 & "<br>" & SQL4) : Response.End()
            DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
            LocalClass.CloseSQLServerConnection()

            SQL3 = "select Max(IndexId) NextIndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " "
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

    Protected Sub ShowHideGoalPanel()
        SQL1 = "select Process_Flag from Appraisal_Master_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        If CDbl(DT1.Rows(0)("Process_Flag").ToString) < 5 Then '--Before Employee Approve 
            SQL6 = " select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10 "
            SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,"
            SQL6 &= " (case when IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*) else 0 end)F_IND7,"
            SQL6 &= " (case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
            SQL6 &= " from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and Appr_Goals=0 group by IndexID)A "
            'Response.Write("1<br>" & SQL6) : Response.End()
            DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
            If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_Goal2.Visible = True : Panel_Goal2_Waiting.Visible = True Else Panel_Goal2.Visible = False : Panel_Goal2_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_Goal3.Visible = True : Panel_Goal3_Waiting.Visible = True Else Panel_Goal3.Visible = False : Panel_Goal3_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_Goal4.Visible = True : Panel_Goal4_Waiting.Visible = True Else Panel_Goal4.Visible = False : Panel_Goal4_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_Goal5.Visible = True : Panel_Goal5_Waiting.Visible = True Else Panel_Goal5.Visible = False : Panel_Goal5_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel_Goal6.Visible = True : Panel_Goal6_Waiting.Visible = True Else Panel_Goal6.Visible = False : Panel_Goal6_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel_Goal7.Visible = True : Panel_Goal7_Waiting.Visible = True Else Panel_Goal7.Visible = False : Panel_Goal7_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel_Goal8.Visible = True : Panel_Goal8_Waiting.Visible = True Else Panel_Goal8.Visible = False : Panel_Goal8_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel_Goal9.Visible = True : Panel_Goal9_Waiting.Visible = True Else Panel_Goal9.Visible = False : Panel_Goal9_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel_Goal10.Visible = True : Panel_Goal10_Waiting.Visible = True Else Panel_Goal10.Visible = False : Panel_Goal10_Waiting.Visible = False
        Else
            SQL6 = " select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10 "
            SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,"
            SQL6 &= " (case when IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*) else 0 end)F_IND7,"
            SQL6 &= " (case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
            SQL6 &= " from Appraisal_FutureGoal_Recall_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and Appr_Goals=0 and Recall_Date in "
            SQL6 &= " (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") group by IndexID)A "
            'Response.Write("2<br>" & SQL6) : Response.End()
            DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
            If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_Goal2_Waiting.Visible = True Else Panel_Goal2_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_Goal3_Waiting.Visible = True Else Panel_Goal3_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_Goal4_Waiting.Visible = True Else Panel_Goal4_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_Goal5_Waiting.Visible = True Else Panel_Goal5_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel_Goal6_Waiting.Visible = True Else Panel_Goal6_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel_Goal7_Waiting.Visible = True Else Panel_Goal7_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel_Goal8_Waiting.Visible = True Else Panel_Goal8_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel_Goal9_Waiting.Visible = True Else Panel_Goal9_Waiting.Visible = False
            If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel_Goal10_Waiting.Visible = True Else Panel_Goal10_Waiting.Visible = False
        End If

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub Delete_Goal2_Click(sender As Object, e As EventArgs) Handles Delete_Goal2.Click
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=2 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexID>2 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and IndexId=2"
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
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=3 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexID>3 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and IndexId=3"
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
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=4 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexID>4 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and IndexId=4"
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
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=5 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexID>5 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and IndexId=5"
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
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=6 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexID>6 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and IndexId=6"
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
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=7 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexID>7 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and IndexId=7"
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
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=8 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexID>8 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and IndexId=8"
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
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=9 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexID>9 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and IndexId=9"
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
        SQL = "delete Appraisal_FutureGoals_tbl where IndexID=10 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        SQL &= " update Appraisal_FutureGoals_tbl Set IndexID=IndexID-1,MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where IndexID>10 and Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & ""
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        SQL3 = "select count(*)IndexID from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and IndexId=10"
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

    Protected Sub Goal1_TextChanged(sender As Object, e As EventArgs) Handles txbGoal1.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal1.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID=" & lblEMPLID.Text & " and IndexID=1"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success1_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess1.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess1.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID=" & lblEMPLID.Text & " and IndexID=1"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date1_TextChanged(sender As Object, e As EventArgs) Handles txbDate1.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate1.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=1"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Goal2_TextChanged(sender As Object, e As EventArgs) Handles txbGoal2.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal2.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=2"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success2_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess2.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess2.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=2"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date2_TextChanged(sender As Object, e As EventArgs) Handles txbDate2.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate2.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=2"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Goal3_TextChanged(sender As Object, e As EventArgs) Handles txbGoal3.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal3.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=3"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success3_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess3.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess3.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=3 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date3_TextChanged(sender As Object, e As EventArgs) Handles txbDate3.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate3.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=3 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Goal4_TextChanged(sender As Object, e As EventArgs) Handles txbGoal4.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal4.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year=" & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=4"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success4_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess4.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess4.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=4 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date4_TextChanged(sender As Object, e As EventArgs) Handles txbDate4.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate4.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=4 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Goal5_TextChanged(sender As Object, e As EventArgs) Handles txbGoal5.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal5.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=5"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success5_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess5.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess5.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=5 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date5_TextChanged(sender As Object, e As EventArgs) Handles txbDate5.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate5.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=5 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Goal6_TextChanged(sender As Object, e As EventArgs) Handles txbGoal6.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal6.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=6"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success6_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess6.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess6.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=6 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date6_TextChanged(sender As Object, e As EventArgs) Handles txbDate6.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate6.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=6 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Goal7_TextChanged(sender As Object, e As EventArgs) Handles txbGoal7.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal7.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=7"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success7_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess7.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess7.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=7 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date7_TextChanged(sender As Object, e As EventArgs) Handles txbDate7.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate7.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=7 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Goal8_TextChanged(sender As Object, e As EventArgs) Handles txbGoal8.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal8.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=8"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success8_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess8.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess8.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=8 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date8_TextChanged(sender As Object, e As EventArgs) Handles txbDate8.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate8.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=8 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Goal9_TextChanged(sender As Object, e As EventArgs) Handles txbGoal9.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal9.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=9"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success9_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess9.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess9.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=9 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date9_TextChanged(sender As Object, e As EventArgs) Handles txbDate9.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate9.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=9 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Goal10_TextChanged(sender As Object, e As EventArgs) Handles txbGoal10.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Goals= '" & Replace(Replace(Replace(Trim(txbGoal10.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=10"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Success10_TextChanged(sender As Object, e As EventArgs) Handles txbSuccess10.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set Milestones= '" & Replace(Replace(Replace(Trim(txbSuccess10.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=10 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
    End Sub
    Protected Sub Date10_TextChanged(sender As Object, e As EventArgs) Handles txbDate10.TextChanged
        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SQL &= " Update Appraisal_FutureGoals_tbl Set TargetDate= '" & Replace(Replace(Replace(Trim(txbDate10.Text), "'", "`"), "<", "{"), ">", "}") & "',MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " where Perf_Year = " & lblYEAR.Text + 1 & " and EMPLID = " & lblEMPLID.Text & " and IndexID=10 "
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            ResetYellowColor()
        End If
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
                SQL11 = "select Window_batch from Appraisal_Master_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year= " & lblYEAR.Text
                DT11 = LocalClass.ExecuteSQLDataSet(SQL11)
                If lblWindowBatch.Text <> CDbl(DT11.Rows(0)("Window_batch").ToString) And Session("Process_Flag") = 0 Then
                    Response.Redirect("..\..\Default.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub SaveRecords_Click(sender As Object, e As EventArgs) Handles SaveRecords.Click
        'If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
        'SaveData()
        'lblMessage.Visible = True
        'lblMessage.Text = "Data Saved"
        'lblMessage1.Visible = True
        'lblMessage1.Text = "Data Saved"
        'ResetYellowColor()
        'Else
        'ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('You have the identical form opened in another browser window.\n\nTo avoid confusion, you are not be permitted to enter information\n in this older version of the form.');</script>")
        'End If

    End Sub

    Protected Sub SaveRecords1_Click(sender As Object, e As EventArgs) Handles SaveRecords1.Click
        WindowBatch()

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SaveData()
            lblMessage.Visible = True
            lblMessage.Text = "Data Saved"
            lblMessage1.Visible = True
            lblMessage1.Text = "Data Saved"
            ResetYellowColor()
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('You have the identical form opened in another browser window.\n\nTo avoid confusion, you are not be permitted to enter information\n in this older version of the form.');</script>")
        End If

    End Sub


    Protected Sub AllRules()

        Dim Flag As Integer = 0

        SQL = "select * from"
        SQL &= " (select count(*)Acom1 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1)A1,"
        SQL &= " (select count(*)Acom2 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2)A2,"
        SQL &= " (select count(*)Acom3 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3)A3,"
        SQL &= " (select count(*)Acom4 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4)A4,"
        SQL &= " (select count(*)Acom5 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5)A5,"
        SQL &= " (select count(*)Acom6 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6)A6,"
        SQL &= " (select count(*)Acom7 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7)A7,"
        SQL &= " (select count(*)Acom8 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8)A8,"
        SQL &= " (select count(*)Acom9 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9)A9,"
        SQL &= " (select count(*)Acom10 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10)A10,"
        SQL &= " (select count(*)Fut1 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=1)B1,"
        SQL &= " (select count(*)Fut2 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=2)B2,"
        SQL &= " (select count(*)Fut3 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=3)B3,"
        SQL &= " (select count(*)Fut4 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=4)B4,"
        SQL &= " (select count(*)Fut5 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=5)B5,"
        SQL &= " (select count(*)Fut6 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=6)B6,"
        SQL &= " (select count(*)Fut7 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=7)B7,"
        SQL &= " (select count(*)Fut8 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=8)B8,"
        SQL &= " (select count(*)Fut9 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=9)B9,"
        SQL &= " (select count(*)Fut10 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=10)B10,"
        SQL &= " (select len(Strengths)Len_Str,len(Development_Area)Len_DevArea,Len(Overall_Summary)Len_OverSum,Len(Development_Objective)Len_DevObj,"
        SQL &= " Overall_Rating,Make_Balance,Build_Trust,Learn_Continuously,Lead_Urgency,Promote_Collab,Confront_Challenge,"
        SQL &= " Lead_Change,Inspire_Risk,Leverage_External,Communic_Impact from Appraisal_Master_TBL where "
        SQL &= " Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")L"
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '---1. ACCOMPLISHMENT TABLE---
        '----------A. IndexID=1----------
        If CDbl(DT.Rows(0)("Acom1").ToString) = 1 Then
            SQL1 = "select len(Accomplishment)Accomplishment1 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            If CDbl(DT1.Rows(0)("Accomplishment1").ToString) < 5 Then txbAccomp1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp1.BackColor = Drawing.Color.White
        End If
        '----------B. IndexID=2----------
        If CDbl(DT.Rows(0)("Acom2").ToString) = 1 Then
            SQL2 = "select len(Accomplishment)Accomplishment2 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
            If CDbl(DT2.Rows(0)("Accomplishment2").ToString) < 5 Then txbAccomp2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp2.BackColor = Drawing.Color.White
        End If
        '----------C. IndexID=3----------
        If CDbl(DT.Rows(0)("Acom3").ToString) = 1 Then
            SQL3 = "select len(Accomplishment)Accomplishment3 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
            If CDbl(DT3.Rows(0)("Accomplishment3").ToString) < 5 Then txbAccomp3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp3.BackColor = Drawing.Color.White
        End If
        '----------D. IndexID=4----------
        If CDbl(DT.Rows(0)("Acom4").ToString) = 1 Then
            SQL4 = "select len(Accomplishment)Accomplishment4 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
            If CDbl(DT4.Rows(0)("Accomplishment4").ToString) < 5 Then txbAccomp4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp4.BackColor = Drawing.Color.White
        End If
        '----------E. IndexID=5----------
        If CDbl(DT.Rows(0)("Acom5").ToString) = 1 Then
            SQL5 = "select len(Accomplishment)Accomplishment5 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Accomplishment5").ToString) < 5 Then txbAccomp5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp5.BackColor = Drawing.Color.White
        End If
        '----------F. IndexID=6----------
        If CDbl(DT.Rows(0)("Acom6").ToString) = 1 Then
            SQL6 = "select len(Accomplishment)Accomplishment6 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
            If CDbl(DT6.Rows(0)("Accomplishment6").ToString) < 5 Then txbAccomp6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp6.BackColor = Drawing.Color.White
        End If
        '----------G. IndexID=7----------
        If CDbl(DT.Rows(0)("Acom7").ToString) = 1 Then
            SQL7 = "select len(Accomplishment)Accomplishment7 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT7 = LocalClass.ExecuteSQLDataSet(SQL7)
            If CDbl(DT7.Rows(0)("Accomplishment7").ToString) < 5 Then txbAccomp7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp7.BackColor = Drawing.Color.White
        End If
        '----------H. IndexID=8----------
        If CDbl(DT.Rows(0)("Acom8").ToString) = 1 Then
            SQL8 = "select len(Accomplishment)Accomplishment8 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT8 = LocalClass.ExecuteSQLDataSet(SQL8)
            If CDbl(DT8.Rows(0)("Accomplishment8").ToString) < 5 Then txbAccomp8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp8.BackColor = Drawing.Color.White
        End If
        '----------I. IndexID=9----------
        If CDbl(DT.Rows(0)("Acom9").ToString) = 1 Then
            SQL9 = "select len(Accomplishment)Accomplishment9 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT9 = LocalClass.ExecuteSQLDataSet(SQL9)
            If CDbl(DT9.Rows(0)("Accomplishment9").ToString) < 5 Then txbAccomp9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp9.BackColor = Drawing.Color.White
        End If
        '----------K. IndexID=10----------
        If CDbl(DT.Rows(0)("Acom10").ToString) = 1 Then
            SQL10 = "select len(Accomplishment)Accomplishment10 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10"
            DT10 = LocalClass.ExecuteSQLDataSet(SQL10)
            If CDbl(DT10.Rows(0)("Accomplishment10").ToString) < 5 Then txbAccomp10.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomp10.BackColor = Drawing.Color.White
        End If

        '---2. Check Strengths in Master table ---
        If CDbl(DT.Rows(0)("Len_Str").ToString) < 3 Then txbStrengths.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbStrengths.BackColor = Drawing.Color.White
        '---3. Check Development Areas in Master table ---
        If CDbl(DT.Rows(0)("Len_DevArea").ToString) < 5 Then txbDevelopment_Areas.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDevelopment_Areas.BackColor = Drawing.Color.White
        '---4. Check Overall Summary in Master table ---
        If CDbl(DT.Rows(0)("Len_OverSum").ToString) < 5 Then txbOverall_Sum.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbOverall_Sum.BackColor = Drawing.Color.White
        '---6. Check Development Objective in Master table ---

        'If CDbl(DT.Rows(0)("Len_DevObj").ToString) < 5 Then txbDevelopment_Objective.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDevelopment_Objective.BackColor = Drawing.Color.White

        '---6. Check Overall Performance Rating in Master table ---
        If CDbl(DT.Rows(0)("Overall_Rating").ToString) = 0 Then trOverall_Performance.BgColor = "Yellow" : Flag = 1 Else trOverall_Performance.BgColor = "White"

        '---7. Addendum: Leadership Competencies in Master table ---
        '----------A. Make Balanced Decisions'----------
        If CDbl(DT.Rows(0)("Make_Balance").ToString) = 0 Then trMake_Balanced.BgColor = "Yellow" : Flag = 1 Else trMake_Balanced.BgColor = "White"
        '----------B. Make Balanced Decisions'----------
        If CDbl(DT.Rows(0)("Build_Trust").ToString) = 0 Then trBuild_Trust.BgColor = "Yellow" : Flag = 1 Else trBuild_Trust.BgColor = "White"
        '----------C. Learn Continuously'----------
        If CDbl(DT.Rows(0)("Learn_Continuously").ToString) = 0 Then trLearn_Continuously.BgColor = "Yellow" : Flag = 1 Else trLearn_Continuously.BgColor = "White"
        '----------D. Lead with Urgency & Purpose'----------
        If CDbl(DT.Rows(0)("Lead_Urgency").ToString) = 0 Then trLead_Urgency.BgColor = "Yellow" : Flag = 1 Else trLead_Urgency.BgColor = "White"
        '----------E. Promote Collaboration & Accountability'----------
        If CDbl(DT.Rows(0)("Promote_Collab").ToString) = 0 Then trPromote_Collaboration.BgColor = "Yellow" : Flag = 1 Else trPromote_Collaboration.BgColor = "White"
        '----------G. Confront Challenges'----------
        If CDbl(DT.Rows(0)("Confront_Challenge").ToString) = 0 Then trConfront_Challenges.BgColor = "Yellow" : Flag = 1 Else trConfront_Challenges.BgColor = "White"
        '----------H. Lead Change'----------
        If CDbl(DT.Rows(0)("Lead_Change").ToString) = 0 Then trLead_Change.BgColor = "Yellow" : Flag = 1 Else trLead_Change.BgColor = "White"
        '----------I. Inspire Risk Taking & Innovation'----------
        If CDbl(DT.Rows(0)("Inspire_Risk").ToString) = 0 Then trInspire_Risk.BgColor = "Yellow" : Flag = 1 Else trInspire_Risk.BgColor = "White"
        '----------G. Leverage External Perspective'----------
        If CDbl(DT.Rows(0)("Leverage_External").ToString) = 0 Then trLeverage_External.BgColor = "Yellow" : Flag = 1 Else trLeverage_External.BgColor = "White"
        '----------K. Communicate for Impact'----------
        If CDbl(DT.Rows(0)("Communic_Impact").ToString) = 0 Then trCommunicate_Impact.BgColor = "Yellow" : Flag = 1 Else trCommunicate_Impact.BgColor = "White"

        '---8. FUTUREGOALS TABLE---
        '----------A. IndexID=1----------
        If CDbl(DT.Rows(0)("Fut1").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals1,len(Milestones)Milestones1,len(TargetDate)TargetDate1 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals1").ToString) < 5 Then txbGoal1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones1").ToString) < 5 Then txbSuccess1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess1.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate1").ToString) < 2 Then txbDate1.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate1.BackColor = Drawing.Color.White
        End If
        '----------B. IndexID=2----------
        If CDbl(DT.Rows(0)("Fut2").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals2,len(Milestones)Milestones2,len(TargetDate)TargetDate2 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=2"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals2").ToString) < 5 Then txbGoal2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones2").ToString) < 5 Then txbSuccess2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess2.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate2").ToString) < 2 Then txbDate2.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate2.BackColor = Drawing.Color.White
        End If
        '----------C. IndexID=3----------
        If CDbl(DT.Rows(0)("Fut3").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals3,len(Milestones)Milestones3,len(TargetDate)TargetDate3 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=3"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals3").ToString) < 5 Then txbGoal3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones3").ToString) < 5 Then txbSuccess3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess3.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate3").ToString) < 2 Then txbDate3.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate3.BackColor = Drawing.Color.White
        End If
        '----------D. IndexID=4----------
        If CDbl(DT.Rows(0)("Fut4").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals4,len(Milestones)Milestones4,len(TargetDate)TargetDate4 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=4"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals4").ToString) < 5 Then txbGoal4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones4").ToString) < 5 Then txbSuccess4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess4.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate4").ToString) < 2 Then txbDate4.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate4.BackColor = Drawing.Color.White
        End If
        '----------E. IndexID=5----------
        If CDbl(DT.Rows(0)("Fut5").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals5,len(Milestones)Milestones5,len(TargetDate)TargetDate5 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=5"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals5").ToString) < 5 Then txbGoal5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones5").ToString) < 5 Then txbSuccess5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess5.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate5").ToString) < 2 Then txbDate5.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate5.BackColor = Drawing.Color.White
        End If
        '----------F. IndexID=6----------
        If CDbl(DT.Rows(0)("Fut6").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals6,len(Milestones)Milestones6,len(TargetDate)TargetDate6 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=6"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals6").ToString) < 5 Then txbGoal6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones6").ToString) < 5 Then txbSuccess6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess6.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate6").ToString) < 2 Then txbDate6.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate6.BackColor = Drawing.Color.White
        End If
        '----------G. IndexID=7----------
        If CDbl(DT.Rows(0)("Fut7").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals7,len(Milestones)Milestones7,len(TargetDate)TargetDate7 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=7"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals7").ToString) < 5 Then txbGoal7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones7").ToString) < 5 Then txbSuccess7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess7.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate7").ToString) < 2 Then txbDate7.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate7.BackColor = Drawing.Color.White
        End If
        '----------H. IndexID=8----------
        If CDbl(DT.Rows(0)("Fut8").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals8,len(Milestones)Milestones8,len(TargetDate)TargetDate8 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=8"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals8").ToString) < 5 Then txbGoal8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones8").ToString) < 5 Then txbSuccess8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess8.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate8").ToString) < 2 Then txbDate8.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate8.BackColor = Drawing.Color.White
        End If
        '----------I. IndexID=9----------
        If CDbl(DT.Rows(0)("Fut9").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals9,len(Milestones)Milestones9,len(TargetDate)TargetDate9 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=9"
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            If CDbl(DT5.Rows(0)("Goals9").ToString) < 5 Then txbGoal9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbGoal9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("Milestones9").ToString) < 5 Then txbSuccess9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbSuccess9.BackColor = Drawing.Color.White
            If CDbl(DT5.Rows(0)("TargetDate9").ToString) < 2 Then txbDate9.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDate9.BackColor = Drawing.Color.White
        End If
        '----------K. IndexID=10----------
        If CDbl(DT.Rows(0)("Fut10").ToString) = 1 Then
            SQL5 = "select len(Goals)Goals10,len(Milestones)Milestones10,len(TargetDate)TargetDate10 from Appraisal_FutureGoals_TBL"
            SQL5 &= " where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=10"
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

    Protected Sub ResetYellowColor()

        SQL14 = "select * from"
        SQL14 &= " (select count(*)Acom1 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1)A1,"
        SQL14 &= " (select count(*)Acom2 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2)A2,"
        SQL14 &= " (select count(*)Acom3 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3)A3,"
        SQL14 &= " (select count(*)Acom4 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4)A4,"
        SQL14 &= " (select count(*)Acom5 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5)A5,"
        SQL14 &= " (select count(*)Acom6 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6)A6,"
        SQL14 &= " (select count(*)Acom7 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7)A7,"
        SQL14 &= " (select count(*)Acom8 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8)A8,"
        SQL14 &= " (select count(*)Acom9 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9)A9,"
        SQL14 &= " (select count(*)Acom10 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=10)A10,"
        SQL14 &= " (select count(*)Fut1 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=1)B1,"
        SQL14 &= " (select count(*)Fut2 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=2)B2,"
        SQL14 &= " (select count(*)Fut3 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=3)B3,"
        SQL14 &= " (select count(*)Fut4 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=4)B4,"
        SQL14 &= " (select count(*)Fut5 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=5)B5,"
        SQL14 &= " (select count(*)Fut6 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=6)B6,"
        SQL14 &= " (select count(*)Fut7 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=7)B7,"
        SQL14 &= " (select count(*)Fut8 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=8)B8,"
        SQL14 &= " (select count(*)Fut9 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=9)B9,"
        SQL14 &= " (select count(*)Fut10 from Appraisal_FutureGoals_TBL where Perf_Year=" & lblYEAR.Text + 1 & " and emplid=" & lblEMPLID.Text & " and IndexID=10)B10,"
        SQL14 &= " (select len(Strengths)Len_Str,len(Development_Area)Len_DevArea,Len(Overall_Summary)Len_OverSum,Len(Development_Objective)Len_DevObj,"
        SQL14 &= " Overall_Rating,Make_Balance,Build_Trust,Learn_Continuously,Lead_Urgency,Promote_Collab,Confront_Challenge,"
        SQL14 &= " Lead_Change,Inspire_Risk,Leverage_External,Communic_Impact from Appraisal_Master_TBL where "
        SQL14 &= " Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")L"
        DT14 = LocalClass.ExecuteSQLDataSet(SQL14)

        '---1. ACCOMPLISHMENT TABLE---
        If CDbl(DT14.Rows(0)("Acom1").ToString) = 1 Then txbAccomp1.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Acom2").ToString) = 1 Then txbAccomp2.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Acom3").ToString) = 1 Then txbAccomp3.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Acom4").ToString) = 1 Then txbAccomp4.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Acom5").ToString) = 1 Then txbAccomp5.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Acom6").ToString) = 1 Then txbAccomp6.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Acom7").ToString) = 1 Then txbAccomp7.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Acom8").ToString) = 1 Then txbAccomp8.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Acom9").ToString) = 1 Then txbAccomp9.BackColor = Drawing.Color.White
        If CDbl(DT14.Rows(0)("Acom10").ToString) = 1 Then txbAccomp10.BackColor = Drawing.Color.White

        '---2. Check Strengths, Development Areas,Overall Summary,Overall Performance   in Master table ---
        txbStrengths.BackColor = Drawing.Color.White : txbDevelopment_Areas.BackColor = Drawing.Color.White
        txbOverall_Sum.BackColor = Drawing.Color.White : trOverall_Performance.BgColor = "White"

        '---3. Addendum: Leadership Competencies in Master table ---
        trMake_Balanced.BgColor = "White" : trBuild_Trust.BgColor = "White" : trLearn_Continuously.BgColor = "White"
        trLead_Urgency.BgColor = "White" : trPromote_Collaboration.BgColor = "White" : trConfront_Challenges.BgColor = "White"
        trLead_Change.BgColor = "White" : trInspire_Risk.BgColor = "White" : trLeverage_External.BgColor = "White" : trCommunicate_Impact.BgColor = "White"

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


    Protected Sub SendToUpperManager()

        'If CDbl(Trim(lblFIRST_MGT_EMPLID.Text)) - CDbl(Trim(lblSECOND_MGT_EMPLID.Text)) = 0 Then
        'SQL1 = "Update Appraisal_Master_tbl Set DateSUB_HR='" & Now & "',Process_Flag=2 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        'SQL1 &= " Update Appraisal_FutureGoals_Master_tbl Set DateSUB_HR='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1
        'Response.Write(SQL1) : Response.End()
        'DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        'LocalClass.CloseSQLServerConnection()
        'Else
        'SQL1 = " Update Appraisal_Master_tbl Set DateSUB_UP_MGT='" & Now & "',Process_Flag=1 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        'SQL1 &= " Update Appraisal_FutureGoals_Master_tbl Set DateSUB_HR='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1
        'Response.Write(SQL1) : Response.End()
        'DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        'LocalClass.CloseSQLServerConnection()
        'End If

        SQL1 = " Update Appraisal_Master_tbl Set  DateSUB_HR='" & Now & "',DateSUB_UP_MGT='" & Now & "',Process_Flag=2 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        SQL1 &= " Update Appraisal_FutureGoals_Master_tbl Set  DateSUB_HR='" & Now & "',DateSUB_UP_MGT='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1
        'Response.Write(SQL1) : Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblEmpl_NAME.Text & "'s performance appraisal have been completed by " & LblMGT_NAME.Text & " <br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

        Session("GLD") = "GLD" & lblYEAR.Text & "" & lblEMPLID.Text & "" & lblMGT_EMPLID.Text

        Msg1 = lblEmpl_NAME.Text & "'s performance appraisal have been submitted for HR approval by " & LblMGT_NAME.Text & " <br>"
        Msg1 &= "To view the form, please click on the <a href=http://" & Request.Url.Host & "/Appraisal/Defaults.aspx?Token=" & Session("GLD") & ">link.</a><br><br>"
        Msg1 &= "Note: As a 2nd level manager, you are not able to directly edit, but you can view all appraisals for your 2nd level direct reports."
        Msg1 &= "If there are any edits you recommend, please speak with the 1st level manager as he/she can make those edits."
        'Response.Write("Send To UpperManager " & Msg1) : Response.End()

        '--Production email--
        'LocalClass.SendMail(lblHR_Email.Text, "Performance Appraisal was sent to you for Approval by " & LblMGT_NAME.Text, Msg)
        'LocalClass.SendMail(lblUP_MGT_Email.Text, "Performance Appraisal For You to View", Msg1)



        Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))

    End Sub
    Protected Sub BtnSubmit_UpperMgr_Click(sender As Object, e As EventArgs) Handles btnSubmit_UpperMgr.Click
        AllRules()
    End Sub

    Protected Sub btnDiscuss_Click(sender As Object, e As EventArgs) Handles btnDiscuss.Click

        If Len(DiscussionComments.Text) < 5 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please type discussion comments'); </script>")
        Else
            SQL = "Update Appraisal_MASTER_tbl set Process_Flag=0 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
            SQL &= " Insert into Appraisal_Discussion_tbl (EMPLID,Perf_Year,MGT_EMPLID,REJ_EMPLID,DateTime,Comments)"
            SQL &= " Values(" & lblEMPLID.Text & ",'" & lblYEAR.Text & "'," & lblFIRST_MGT_EMPLID.Text & "," & Session("MGT_EMPLID") & ",'"
            SQL &= " " & Now & " ','" & Replace(Replace(Replace(DiscussionComments.Text, "'", "`"), "<", "{"), ">", "}") & "')"
            SQL &= " Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=0 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            SQL1 = "select first+' '+last Name from id_tbl where emplid=" & lblMGT_EMPLID.Text
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            Msg = DT1.Rows(0)("Name").ToString & " has comments on " & lblEmpl_NAME.Text & "'s performance<br><br>"
            Msg &= Replace(DiscussionComments.Text, "'", "`") & " <br><br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

            'Response.Write(Msg) : Response.End()
            '--Production email--
            'LocalClass.SendMail(lblMGT_Email.Text, DT1.Rows(0)("Name").ToString & " has comments on " & lblEmpl_NAME.Text & "'s performance", Msg)
            'LocalClass.SendMail(lblHR_Email.Text, DT1.Rows(0)("Name").ToString & " has comments on " & lblEmpl_NAME.Text & "'s performance", Msg)

            LocalClass.CloseSQLServerConnection()

            Response.Write("<script language='javascript'> { window.close();}</script>")
        End If

    End Sub

    Protected Sub Discussion()
        SQL = "select top 1 REJ_EMPLID,Rtrim(Ltrim(Comments))Comments,(select First+' '+Last+''''+'s' from id_tbl where emplid=Rej_Emplid)Rej_Name from Appraisal_Discussion_tbl"
        SQL &= " where MGT_EMPLID=" & lblFIRST_MGT_EMPLID.Text & " and emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " order by Datetime desc"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        If DT.Rows.Count > 0 Then
            If CDbl(lblFIRST_MGT_EMPLID.Text) = CDbl(DT.Rows(0)("REJ_EMPLID").ToString) Then
            Else
                lblDiscuss.Text = "<font color=black><b>" & DT.Rows(0)("Rej_Name").ToString & " Discussion Comments:</b></font>   " & _
                    Replace(Replace(Replace(Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")

                lblDiscuss.ForeColor = Drawing.Color.Blue
                lblDiscuss.BorderStyle = BorderStyle.Ridge
                lblDiscuss.BorderColor = Drawing.Color.LightGray
                lblDiscuss.BorderWidth = 1
            End If
        End If

    End Sub
    Protected Sub DisplayComments()
        SqlDataSource1.SelectCommand = "select *,(select First+' '+Last from id_tbl where emplid=A.emplid)EMPL,(select First+' '+Last from id_tbl where emplid=A.MGT_emplid)MGT,"
        SqlDataSource1.SelectCommand &= "(select First+' '+Last from id_tbl where emplid=A.Rej_Emplid)REJ from Appraisal_Discussion_tbl A where "
        SqlDataSource1.SelectCommand &= "perf_year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " order by DateTime desc"
        'Response.Write(SqlDataSource1.SelectCommand) ': Response.End()
    End Sub


    Protected Sub btnGeneralist_Click(sender As Object, e As EventArgs) Handles btnGeneralist.Click

        If CDbl(Session("MGT_EMPLID")) - CDbl(lblHR_EMPLID.Text) = 0 Then
            'Response.Write("Appraisal has been Reviewed by HR  Generalist " & lblHR_NAME.Text) : Response.End()
            SQL1 = "Update Appraisal_Master_tbl Set DateHR_Appr='" & Now & "', Process_Flag=3 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
            SQL1 &= " Update Appraisal_FutureGoals_Master_tbl Set DateHR_Appr='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            LocalClass.CloseSQLServerConnection()

            Msg = lblEmpl_NAME.Text & "'s Performance Appraisal has been Approved by " & lblHR_NAME.Text & "<br><br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

            '--Production email--
            'LocalClass.SendMail(lblMGT_Email.Text, "Performance Appraisal was Reviewed by HR", Msg)

        Else
            SQL1 = "Update Appraisal_Master_tbl Set DateSUB_HR='" & Now & "',Process_Flag=2 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
            SQL1 &= " Update Appraisal_FutureGoals_Master_tbl Set DateSUB_HR='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            LocalClass.CloseSQLServerConnection()

            Msg = lblEmpl_NAME.Text & "'s performance appraisal have been completed by " & LblMGT_NAME.Text & " <br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"


            Msg1 = lblEmpl_NAME.Text & "'s performance appraisal have been submitted for HR approval by " & LblMGT_NAME.Text & " <br><br>"
            Msg1 &= "To view the form, please click on the link http://" & Request.Url.Host & "/Staff/HR/Appraisal_Reports_Redirect.aspx?Token=" & Request.QueryString("Token")
            Msg1 &= " Once you login,  you can view all appraisals and goals through the ``appraisal status report`` drop-down.<br><br>"
            Msg1 &= "Note: As a 2nd level manager, you are not able to directly edit, but you can view all appraisals for your 2nd level direct reports. "
            Msg1 &= "If there are any edits you recommend, please speak with the 1st level manager as he/she can make those edits."
            'Response.Write("send to HR1" & Msg1) : Response.End()

            '--Production email--
            'LocalClass.SendMail(lblHR_Email.Text, "Performance Appraisal was sent to you for Approval by " & Session("NAME"), Msg)
            'LocalClass.SendMail(lblUP_MGT_Email.Text, "Performance Appraisal For You to View", Msg1)



        End If

        Response.Write("<script language='javascript'> { window.close();}</script>")
    End Sub

    Protected Sub btnEditForm_Click(sender As Object, e As EventArgs) Handles btnEditForm.Click
        SQL = "Update Appraisal_MASTER_tbl set Process_Flag=0 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        SQL &= " Insert into Appraisal_Discussion_tbl values('" & lblEMPLID.Text & "','" & lblYEAR.Text & "','" & lblFIRST_MGT_EMPLID.Text & "','" & lblFIRST_MGT_EMPLID.Text & "','" & Now & "','Edit')"
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub

    Protected Sub btnSendEmployee_Click(sender As Object, e As EventArgs) Handles btnSendEmployee.Click

        SQL = "Update Appraisal_MASTER_tbl set DateSUB_Empl='" & Now & "',Process_Flag=4 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        SQL &= " Update Appraisal_FutureGoals_Master_tbl Set DateSUB_Empl='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        Msg = "Your performance appraisal has been completed by your manager and is ready for your review.<br><br>"
        Msg &= "<b>NOTE: Please do not electronically sign and submit the review until you have had the Appraisal/Ratings Criteria discussion with your manager."
        Msg &= " Your electronic signature only confirms that you have had the discussion, not that you necessarily agree with the contents of the appraisal.</b><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

        '--Production email 
        'LocalClass.SendMail(lblEmpl_Email.Text, "Your Performance Appraisal is ready for your review", Msg)


        Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub

    Protected Sub RefuseSign_Click(sender As Object, e As EventArgs) Handles RefuseSign.Click
        If IsDate(txtCal.Text) = False Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose a valid calendar date.'); </script>")
            txtCal.Text = ""
        ElseIf Len(txtCal.Text) < 8 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose a valid calendar date'); </script>")
            txtCal.Text = ""
        ElseIf Len(txtCal.Text) > 10 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose a valid calendar date.'); </script>")
            txtCal.Text = ""
        Else
            SQL = "Update  Appraisal_Master_tbl Set DateEmpl_Appr='" & txtCal.Text & "',DateEmpl_Refused='" & txtCal.Text & "',Process_Flag=5,"
            SQL &= "Comments='Meeting was held on " & txtCal.Text & " and Employee declined to sign.' where emplid=" & lblEMPLID.Text & "  and Perf_Year=" & lblYEAR.Text
            SQL &= " Update Appraisal_FutureGoals_Master_tbl Set Process_Flag=5,DateEmpl_Appr='" & Now & "'  where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1

            SQL &= " Insert into Appraisal_FutureGoal_Recall_tbl "
            SQL &= " select EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate, '" & txtCal.Text & "' DateEmpl_Appr, '" & lblFIRST_MGT_EMPLID.Text & "' Recall_EMPLID,'" & txtCal.Text & "' Recall_Date"
            SQL &= " from Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
        End If
        Response.Redirect("Appraisal.aspx?Token=" & Request.QueryString("Token"))
    End Sub


    Private Sub Calendar1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged

        'Response.Write(Today > = Calendar1.SelectedDate)
        If Today < Calendar1.SelectedDate Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Future date not permitted.'); </script>")
            txtCal.Text = ""
        Else
            Calendar1.Visible = False
            txtCal.Text = Calendar1.SelectedDate.ToShortDateString()

            Dim div As System.Web.UI.Control = Page.FindControl("divCalendar")
            If TypeOf div Is HtmlGenericControl Then
                CType(div, HtmlGenericControl).Style.Add("display", "none")

            End If
        End If

    End Sub
    Protected Sub imgCal_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles imgCal.Click
        Calendar1.Visible = True
    End Sub
    Protected Sub txtCal_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCal.TextChanged
        Calendar1.Visible = True
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SQL = "Update Appraisal_Master_tbl Set Comments='" & Replace(Replace(Replace(txbEmployeeComments.Text, "'", "`"), "<", "{"), ">", "}") & "', DateEmpl_Refused=NULL,"
        SQL &= "DateEmpl_Appr='" & Now & "',Process_Flag=5 where emplid=" & lblEMPLID.Text & "  and Perf_Year=" & lblYEAR.Text
        SQL &= " Update Appraisal_FutureGoals_Master_tbl Set DateEmpl_Appr='" & Now & "',Process_Flag=5 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1

        SQL &= " Insert into Appraisal_FutureGoal_Recall_tbl "
        SQL &= " select EMPLID,Perf_Year,IndexID,Appr_Goals,Goals,Milestones,TargetDate, '" & Now & "' DateEmpl_Appr, '" & lblFIRST_MGT_EMPLID.Text & "' Recall_EMPLID,'" & Now & "' Recall_Date"
        SQL &= " from Appraisal_FutureGoals_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Response.Redirect("../../Default.aspx?Token=" & Request.QueryString("Token"))

        LocalClass.CloseSQLServerConnection()

    End Sub

End Class

