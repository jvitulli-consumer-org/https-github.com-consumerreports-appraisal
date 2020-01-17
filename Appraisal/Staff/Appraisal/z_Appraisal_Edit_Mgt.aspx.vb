Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Runtime.CompilerServices
Public Class Appraisal_Edit_Mgt
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

        SetLevel_Approval()
        ShowButtonCursor()

        If IsPostBack Then

        Else
            'Response.Write("Non-PostBack")
            'txbAccomplishment_Comm.Text = "Generalist Comments" : txbAccomplishment_Comm.ForeColor = Drawing.Color.Gray
            DisplayData()
        End If


    End Sub
    Protected Sub ShowButtonCursor()
        btnSubmit_Generalist.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Img_Print1.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnEditForm.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnSendEmployee.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        SaveRecords1.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        RefuseSign.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
    End Sub

    Protected Sub WindowBatch()
        SQL11 = "select Window_batch,process_flag from Appraisal_Master_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year= " & lblYEAR.Text
        'Response.Write(SQL11) : Response.End()
        DT11 = LocalClass.ExecuteSQLDataSet(SQL11)
        Session("Window_batch") = CDbl(DT11.Rows(0)("Window_batch").ToString)
        LocalClass.CloseSQLServerConnection()
        If CDbl(DT11.Rows(0)("process_flag").ToString) = 0 And (Session("Window_batch") - CDbl(lblWindowBatch.Text)) = 0 Then SaveData()
    End Sub

    Protected Sub SetLevel_Approval()
        '--Process_flag=0  manager create form
        '--Process_flag=1  return from HR to edit
        '--Process_flag=2  HR review 
        '--Process_flag=4  send to employee
        '--Process_flag=5  employee e-sign

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

        '--Employee information
        lblEmpl_NAME.Text = DT.Rows(0)("Empl_Name").ToString : lblEmpl_TITLE.Text = DT.Rows(0)("JobTitle").ToString : lblEmpl_DEPT.Text = DT.Rows(0)("Department").ToString
        lblEmpl_HIRE.Text = DT.Rows(0)("Empl_Hired").ToString : lblEmpl_Email.Text = DT.Rows(0)("Empl_Email").ToString
        '--Direct Manager information 
        lblFIRST_MGT_EMPLID.Text = DT.Rows(0)("MGT_EMPLID").ToString : LblMGT_NAME.Text = DT.Rows(0)("MGT_Name").ToString : lblMGT_Email.Text = DT.Rows(0)("MGT_Email").ToString
        '--Next manager information
        lblSECOND_MGT_EMPLID.Text = DT.Rows(0)("UP_MGT_EMPLID").ToString : lblUP_MGT_Email.Text = DT.Rows(0)("UP_MGT_Email").ToString
        '--HR information
        lblHR_EMPLID.Text = DT.Rows(0)("HR_EMPLID").ToString : lblHR_NAME.Text = DT.Rows(0)("HR_Name").ToString : lblHR_Email.Text = DT.Rows(0)("HR_Email").ToString
        lblProcess_Flag.Text = DT.Rows(0)("Process_Flag").ToString
        FY_Year.Text = lblYEAR.Text

        If DT.Rows(0)("Process_flag").ToString = 0 Then '--Manager create/edit
            Timer1.Enabled = True : Timer2.Enabled = True
            btnSubmit_Generalist.Text = "Send Appraisal to " & DT.Rows(0)("HR_Name").ToString  '--Send to HR 
            btnSubmit_Generalist.Visible = True

        ElseIf DT.Rows(0)("Process_flag").ToString = 1 Then '--Manager create/edit
            LblHR_Appr.Text = "Rejected for Edit"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True
            btnSubmit_Generalist.Text = "Send Appraisal to " & DT.Rows(0)("HR_Name").ToString  '--Send to HR 
            btnSubmit_Generalist.Visible = True

        ElseIf DT.Rows(0)("Process_flag").ToString = 2 Then '--Sent to HR for review 
            Timer1.Enabled = False : Timer2.Enabled = False
            LblHR_Appr.Text = "Waiting Approval"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True
            LblMGT_Appr.Text = DT.Rows(0)("DateSub_HR").ToString
            SaveRecords1.Visible = False

        ElseIf DT.Rows(0)("Process_flag").ToString = 3 Then '--HR Approved 
            Timer1.Enabled = False : Timer2.Enabled = False
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            SaveRecords1.Visible = False
            btnEditForm.Visible = True : btnEditForm.Text = "Edit " & lblEmpl_NAME.Text & "'s Appraisal"
            btnSendEmployee.Visible = True : btnSendEmployee.Text = "Send Appraisal to " & lblEmpl_NAME.Text

        ElseIf DT.Rows(0)("Process_flag").ToString = 4 Then ' Sent to Employee
            Timer1.Enabled = False : Timer2.Enabled = False
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblEMP_Appr.Text = "Waiting Approval"
            LblEMP_Appr.ForeColor = Drawing.Color.Blue
            LblEMP_Appr.Font.Bold = True
            SaveRecords1.Visible = False
            btnEditForm.Visible = True : btnEditForm.Text = "Edit " & lblEmpl_NAME.Text & "'s Appraisal"
            txtCal.Visible = True
            imgCal.Visible = True
            RefuseSign.Visible = True
            lblRefuseText.Text = "If the Employee declined to esign their appraisal, please select the date you conducted the appraisal discussion. Then press the ""Employee Declined to Sign"" button to complete the process."

        ElseIf DT.Rows(0)("Process_flag").ToString = 5 Then 'Employee e-signed
            Timer1.Enabled = False : Timer2.Enabled = False
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
        End If

        SQL1 = "select top 1 * from(select emplid,(select first+' '+last from id_tbl where emplid=a.emplid)Employee,Rtrim(Ltrim(mgt_emplid))mgt_emplid,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=Rtrim(Ltrim(a.mgt_emplid)))Collab_MGT,DateTime from Appraisal_MasterHistory_tbl A where"
        SQL1 &= " Rtrim(Ltrim(emplid)) in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYEAR.Text & " and Rtrim(Ltrim(MGT_EMPLID)) not in (select Rtrim(Ltrim(mgt_emplid)) from "
        SQL1 &= " appraisal_master_tbl where emplid=a.emplid and Perf_Year=" & lblYEAR.Text & ")) AA ORDER BY DateTime desc"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        '--Get Collaboration Manager
        If DT1.Rows.Count > 0 Then lblCOLL_MGT_NAME.Text = DT1.Rows(0)("Collab_MGT").ToString


    End Sub

    Protected Sub DisplayData()
        '--Process_flag=0  manager create form
        '--Process_flag=1  return from HR to edit
        '--Process_flag=2  HR review 
        '--Process_flag=3  HR approve
        '--Process_flag=4  send to employee
        '--Process_flag=5  employee e-sign

        SQL = "select AA.*,IsNull(BB.emplid,'0')Emplid_Comm,DateTime,IsNull(Accomplishment_Comm,'')Accomplishment_Comm,IsNull(Strengths_Comm,'')Strengths_Comm,"
        SQL &= " IsNull(Development_Comm,'')Development_Comm,IsNull(Rating_Comm,'')Rating_Comm,IsNull(Summary_Comm,'')Summary_Comm,IsNull(Leadership_Comm,'')Leadership_Comm,Version,"
        SQL &= " Datetime,Rej_Generalist from(select A.*,Accomplishment from(select * from Appraisal_Master_TBL  where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A JOIN "
        SQL &= " (select * from Appraisal_Accomplishments_TBL  where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")B ON A.emplid=B.emplid )AA LEFT JOIN "
        SQL &= " (select *,(select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Rej_Generalist from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " "
        SQL &= " and Version in (select Max(Version) from Appraisal_Discussion_tbl where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")) BB ON AA.emplid=BB.Emplid"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '---Oveall Performance Rating
        If DT.Rows(0)("Overall_rating").ToString = 1 Then rbBelow.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 2 Then rbNeed.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 3 Then rbMeet.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 4 Then rbExceed.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 5 Then rbDisting.Checked = True
        '1. Make Balanced Decisions
        If DT.Rows(0)("Make_Balance").ToString = 1 Then rbMake_Need1.Checked = True
        If DT.Rows(0)("Make_Balance").ToString = 2 Then rbMake_Prof1.Checked = True
        If DT.Rows(0)("Make_Balance").ToString = 3 Then rbMake_Exce1.Checked = True
        '2. Build Trust
        If DT.Rows(0)("Build_Trust").ToString = 1 Then rbBuild2_Need1.Checked = True
        If DT.Rows(0)("Build_Trust").ToString = 2 Then rbBuild2_Prof1.Checked = True
        If DT.Rows(0)("Build_Trust").ToString = 3 Then rbBuild2_Exce1.Checked = True
        '3. Learn Continuously
        If DT.Rows(0)("Learn_Continuously").ToString = 1 Then rbLearn_Need1.Checked = True
        If DT.Rows(0)("Learn_Continuously").ToString = 2 Then rbLearn_Prof1.Checked = True
        If DT.Rows(0)("Learn_Continuously").ToString = 3 Then rbLearn_Exce1.Checked = True
        '4. Lead with Urgency & Purpose
        If DT.Rows(0)("Lead_Urgency").ToString = 1 Then rbLead2_Need1.Checked = True
        If DT.Rows(0)("Lead_Urgency").ToString = 2 Then rbLead2_Prof1.Checked = True
        If DT.Rows(0)("Lead_Urgency").ToString = 3 Then rbLead2_Exce1.Checked = True
        '5. Promote Collaboration & Accountability
        If DT.Rows(0)("Promote_Collab").ToString = 1 Then rbProm_Need1.Checked = True
        If DT.Rows(0)("Promote_Collab").ToString = 2 Then rbProm_Prof1.Checked = True
        If DT.Rows(0)("Promote_Collab").ToString = 3 Then rbProm_Exce1.Checked = True
        '6. Confront Challenges
        If DT.Rows(0)("Confront_Challenge").ToString = 1 Then rbConf_Need1.Checked = True
        If DT.Rows(0)("Confront_Challenge").ToString = 2 Then rbConf_Prof1.Checked = True
        If DT.Rows(0)("Confront_Challenge").ToString = 3 Then rbConf_Exce1.Checked = True
        '7. Lead Change
        If DT.Rows(0)("Lead_Change").ToString = 1 Then rbLead_Need1.Checked = True
        If DT.Rows(0)("Lead_Change").ToString = 2 Then rbLead_Prof1.Checked = True
        If DT.Rows(0)("Lead_Change").ToString = 3 Then rbLead_Exce1.Checked = True
        '8. Inspire Risk Taking & innovation
        If DT.Rows(0)("Inspire_Risk").ToString = 1 Then rbInsp_Need1.Checked = True
        If DT.Rows(0)("Inspire_Risk").ToString = 2 Then rbInsp_Prof1.Checked = True
        If DT.Rows(0)("Inspire_Risk").ToString = 3 Then rbInsp_Exce1.Checked = True
        '9. Leverage External Perspective
        If DT.Rows(0)("Leverage_External").ToString = 1 Then rbLeve_Need1.Checked = True
        If DT.Rows(0)("Leverage_External").ToString = 2 Then rbLeve_Prof1.Checked = True
        If DT.Rows(0)("Leverage_External").ToString = 3 Then rbLeve_Exce1.Checked = True
        '10. Communicate for Impact
        If DT.Rows(0)("Communic_Impact").ToString = 1 Then rbComm_Need1.Checked = True
        If DT.Rows(0)("Communic_Impact").ToString = 2 Then rbComm_Prof1.Checked = True
        If DT.Rows(0)("Communic_Impact").ToString = 3 Then rbComm_Exce1.Checked = True

        If DT.Rows(0)("Process_flag").ToString = 0 Then '--Manager create Appraisal 
            '---Accomplishment---
            txbAccomplishment.Visible = True ': lblAccomplishment.Visible = False            lblAccomplishment_Comm.Visible = False
            txbAccomplishment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            '---Strengths---
            txbStrengths.Visible = True : lblStrengths.Visible = False : lblStrengths_Comm.Visible = False
            txbStrengths.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            '---Development---
            txbDevelopment.Visible = True : lblDevelopment.Visible = True : lblDevelopment_Comm.Visible = False
            txbDevelopment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            '---Overall Performance---
            '---Overall Summary---
            txbOverall_Summary.Visible = True : lblOverall_Summary.Visible = False : lblOverall_Summary_Comm.Visible = False
            txbOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            lblOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblOverall_Summary_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Summary_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

        ElseIf DT.Rows(0)("Process_flag").ToString = 1 Then
            Discussion_Comments()

            '1. Accomplishment---
            txbAccomplishment.Visible = True
            txbAccomplishment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            If Len(DT.Rows(0)("Accomplishment_Comm").ToString) > 4 Then
                lblAccomplishment_Comm.Visible = True : lblAccomplishment_Comm.BackColor = Drawing.Color.LightYellow
                lblAccomplishment_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                    Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            End If

            '2. Strengths---
            txbStrengths.Visible = True : lblStrengths.Visible = False
            txbStrengths.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            If Len(DT.Rows(0)("Strengths_Comm").ToString) > 4 Then
                lblStrengths_Comm.Visible = True : lblStrengths_Comm.BackColor = Drawing.Color.LightYellow
                lblStrengths_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                    Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            End If

            '3. Development---
            txbDevelopment.Visible = True : lblDevelopment.Visible = True : lblDevelopment_Comm.Visible = True : lblDevelopment_Comm.BackColor = Drawing.Color.LightYellow
            txbDevelopment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            If Len(DT.Rows(0)("Development_Comm").ToString) > 4 Then
                lblDevelopment_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            End If
            '4. Overall Performance---
            lblOverall_Performance_Comm.Visible = True : lblOverall_Performance_Comm.BackColor = Drawing.Color.LightYellow
            If Len(DT.Rows(0)("Rating_Comm").ToString) > 4 Then
                lblOverall_Performance_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                 Replace(Replace(Replace(Replace(DT.Rows(0)("Rating_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            End If
            '5. Overall Summary---
            txbOverall_Summary.Visible = True : lblOverall_Summary.Visible = False : lblOverall_Summary_Comm.Visible = True : lblOverall_Summary_Comm.BackColor = Drawing.Color.LightYellow
            txbOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

            If Len(DT.Rows(0)("Summary_Comm").ToString) > 4 Then
                lblOverall_Summary_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                Replace(Replace(Replace(Replace(DT.Rows(0)("Summary_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            End If
            '6. Leadership
            lblLeaderShip_Comm.Visible = True : lblLeaderShip_Comm.BackColor = Drawing.Color.LightYellow
            If Len(DT.Rows(0)("Leadership_Comm").ToString) > 4 Then
                lblLeaderShip_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                Replace(Replace(Replace(Replace(DT.Rows(0)("Leadership_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            End If

        ElseIf DT.Rows(0)("Process_flag").ToString = 2 Then
            '--send to HR for review 
            lblAccomplishment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblStrengths.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblDevelopment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False
            txbAccomplishment.Visible = False : lblAccomplishment.Visible = True
            txbStrengths.Visible = False : lblStrengths.Visible = True
            txbDevelopment.Visible = False : lblDevelopment.Visible = True
            txbOverall_Summary.Visible = False : lblOverall_Summary.Visible = True


            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False : rbMake_Need1.Enabled = False : rbMake_Prof1.Enabled = False
            rbMake_Exce1.Enabled = False : rbBuild2_Need1.Enabled = False : rbBuild2_Prof1.Enabled = False : rbBuild2_Exce1.Enabled = False : rbLearn_Need1.Enabled = False : rbLearn_Prof1.Enabled = False
            rbLearn_Exce1.Enabled = False : rbLead2_Need1.Enabled = False : rbLead2_Prof1.Enabled = False : rbLead2_Exce1.Enabled = False : rbProm_Need1.Enabled = False : rbProm_Prof1.Enabled = False
            rbProm_Exce1.Enabled = False : rbConf_Need1.Enabled = False : rbConf_Prof1.Enabled = False : rbConf_Exce1.Enabled = False : rbLead_Need1.Enabled = False : rbLead_Prof1.Enabled = False
            rbLead_Exce1.Enabled = False : rbInsp_Need1.Enabled = False : rbInsp_Prof1.Enabled = False : rbInsp_Exce1.Enabled = False : rbLeve_Need1.Enabled = False : rbLeve_Prof1.Enabled = False
            rbLeve_Exce1.Enabled = False : rbComm_Need1.Enabled = False : rbComm_Prof1.Enabled = False : rbComm_Exce1.Enabled = False

            Discussion_Comments1()

        ElseIf DT.Rows(0)("Process_flag").ToString = 3 Then '--HR Approved 

            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False
            txbAccomplishment.Visible = False : lblAccomplishment.Visible = True
            txbStrengths.Visible = False : lblStrengths.Visible = True
            txbDevelopment.Visible = False : lblDevelopment.Visible = True
            txbOverall_Summary.Visible = False : lblOverall_Summary.Visible = True

            lblAccomplishment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblStrengths.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblDevelopment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")


            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False : rbMake_Need1.Enabled = False : rbMake_Prof1.Enabled = False
            rbMake_Exce1.Enabled = False : rbBuild2_Need1.Enabled = False : rbBuild2_Prof1.Enabled = False : rbBuild2_Exce1.Enabled = False : rbLearn_Need1.Enabled = False : rbLearn_Prof1.Enabled = False
            rbLearn_Exce1.Enabled = False : rbLead2_Need1.Enabled = False : rbLead2_Prof1.Enabled = False : rbLead2_Exce1.Enabled = False : rbProm_Need1.Enabled = False : rbProm_Prof1.Enabled = False
            rbProm_Exce1.Enabled = False : rbConf_Need1.Enabled = False : rbConf_Prof1.Enabled = False : rbConf_Exce1.Enabled = False : rbLead_Need1.Enabled = False : rbLead_Prof1.Enabled = False
            rbLead_Exce1.Enabled = False : rbInsp_Need1.Enabled = False : rbInsp_Prof1.Enabled = False : rbInsp_Exce1.Enabled = False : rbLeve_Need1.Enabled = False : rbLeve_Prof1.Enabled = False
            rbLeve_Exce1.Enabled = False : rbComm_Need1.Enabled = False : rbComm_Prof1.Enabled = False : rbComm_Exce1.Enabled = False

        ElseIf DT.Rows(0)("Process_flag").ToString = 4 Then 'Sent to Employee
            lblAccomplishment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblStrengths.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblDevelopment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False
            txbAccomplishment.Visible = False : lblAccomplishment.Visible = True
            txbStrengths.Visible = False : lblStrengths.Visible = True
            txbDevelopment.Visible = False : lblDevelopment.Visible = True
            txbOverall_Summary.Visible = False : lblOverall_Summary.Visible = True


            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False : rbMake_Need1.Enabled = False : rbMake_Prof1.Enabled = False
            rbMake_Exce1.Enabled = False : rbBuild2_Need1.Enabled = False : rbBuild2_Prof1.Enabled = False : rbBuild2_Exce1.Enabled = False : rbLearn_Need1.Enabled = False : rbLearn_Prof1.Enabled = False
            rbLearn_Exce1.Enabled = False : rbLead2_Need1.Enabled = False : rbLead2_Prof1.Enabled = False : rbLead2_Exce1.Enabled = False : rbProm_Need1.Enabled = False : rbProm_Prof1.Enabled = False
            rbProm_Exce1.Enabled = False : rbConf_Need1.Enabled = False : rbConf_Prof1.Enabled = False : rbConf_Exce1.Enabled = False : rbLead_Need1.Enabled = False : rbLead_Prof1.Enabled = False
            rbLead_Exce1.Enabled = False : rbInsp_Need1.Enabled = False : rbInsp_Prof1.Enabled = False : rbInsp_Exce1.Enabled = False : rbLeve_Need1.Enabled = False : rbLeve_Prof1.Enabled = False
            rbLeve_Exce1.Enabled = False : rbComm_Need1.Enabled = False : rbComm_Prof1.Enabled = False : rbComm_Exce1.Enabled = False




        ElseIf DT.Rows(0)("Process_flag").ToString = 5 Then '--employee e-singn
            lblAccomplishment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblStrengths.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblDevelopment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False
            txbAccomplishment.Visible = False : lblAccomplishment.Visible = True
            txbStrengths.Visible = False : lblStrengths.Visible = True
            txbDevelopment.Visible = False : lblDevelopment.Visible = True
            txbOverall_Summary.Visible = False : lblOverall_Summary.Visible = True


            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False : rbMake_Need1.Enabled = False : rbMake_Prof1.Enabled = False
            rbMake_Exce1.Enabled = False : rbBuild2_Need1.Enabled = False : rbBuild2_Prof1.Enabled = False : rbBuild2_Exce1.Enabled = False : rbLearn_Need1.Enabled = False : rbLearn_Prof1.Enabled = False
            rbLearn_Exce1.Enabled = False : rbLead2_Need1.Enabled = False : rbLead2_Prof1.Enabled = False : rbLead2_Exce1.Enabled = False : rbProm_Need1.Enabled = False : rbProm_Prof1.Enabled = False
            rbProm_Exce1.Enabled = False : rbConf_Need1.Enabled = False : rbConf_Prof1.Enabled = False : rbConf_Exce1.Enabled = False : rbLead_Need1.Enabled = False : rbLead_Prof1.Enabled = False
            rbLead_Exce1.Enabled = False : rbInsp_Need1.Enabled = False : rbInsp_Prof1.Enabled = False : rbInsp_Exce1.Enabled = False : rbLeve_Need1.Enabled = False : rbLeve_Prof1.Enabled = False
            rbLeve_Exce1.Enabled = False : rbComm_Need1.Enabled = False : rbComm_Prof1.Enabled = False : rbComm_Exce1.Enabled = False

        End If




    End Sub
    Protected Sub Discussion_Comments()

        SqlDataSource1.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource1.SelectCommand &= " Replace(Replace(Replace(Replace(Accomplishment_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Accomp_Comm"
        SqlDataSource1.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource1.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and version not in (select Max(Version)Ver from Appraisal_Discussion_tbl where Perf_Year=" & lblYEAR.Text & ""
        SqlDataSource1.SelectCommand &= " and emplid=" & lblEMPLID.Text & ") and len(Accomplishment_Comm)>4 and Version>0 and DateTime>"
        SqlDataSource1.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource1.SelectCommand &= " order by version desc "

        SqlDataSource2.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource2.SelectCommand &= " Replace(Replace(Replace(Replace(Strengths_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Str_Comm"
        SqlDataSource2.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource2.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and version not in (select Max(Version)Ver  from Appraisal_Discussion_tbl where Perf_Year=" & lblYEAR.Text & ""
        SqlDataSource2.SelectCommand &= " and emplid=" & lblEMPLID.Text & ") and len(Strengths_Comm)>4 and Version>0 and DateTime>"
        SqlDataSource2.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource2.SelectCommand &= " order by version desc "

        'Development_Comm
        SqlDataSource3.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource3.SelectCommand &= " Replace(Replace(Replace(Replace(Development_Comm, '{', '&lt;'), '}', '&gt;>'), '`', ''''), Char(13), '<br>')Dev_Comm"
        SqlDataSource3.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource3.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and version not in (select Max(Version)Ver  from Appraisal_Discussion_tbl where Perf_Year=" & lblYEAR.Text & ""
        SqlDataSource3.SelectCommand &= " and emplid=" & lblEMPLID.Text & ") and len(Development_Comm)>4 and Version>0 and DateTime>"
        SqlDataSource3.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource3.SelectCommand &= " order by version desc "

        SqlDataSource4.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource4.SelectCommand &= " Replace(Replace(Replace(Replace(Rating_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Rate_Comm"
        SqlDataSource4.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource4.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and version not in (select Max(Version)Ver  from Appraisal_Discussion_tbl where Perf_Year=" & lblYEAR.Text & ""
        SqlDataSource4.SelectCommand &= " and emplid=" & lblEMPLID.Text & ") and len(Rating_Comm)>4 and Version>0 and DateTime>"
        SqlDataSource4.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource4.SelectCommand &= " order by version desc "

        SqlDataSource5.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource5.SelectCommand &= " Replace(Replace(Replace(Replace(Summary_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Sum_Comm"
        SqlDataSource5.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource5.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and version not in (select Max(Version)Ver  from Appraisal_Discussion_tbl where Perf_Year=" & lblYEAR.Text & ""
        SqlDataSource5.SelectCommand &= " and emplid=" & lblEMPLID.Text & ") and len(Summary_Comm)>4 and Version>0 and DateTime>"
        SqlDataSource5.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource5.SelectCommand &= " order by version desc "

        'Leadership_Comm
        SqlDataSource6.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource6.SelectCommand &= " Replace(Replace(Replace(Replace(Leadership_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Lead_Comm"
        SqlDataSource6.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource6.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and version not in (select Max(Version)Ver  from Appraisal_Discussion_tbl where Perf_Year=" & lblYEAR.Text & ""
        SqlDataSource6.SelectCommand &= " and emplid=" & lblEMPLID.Text & ") and len(Leadership_Comm)>4 and Version>0 and DateTime>"
        SqlDataSource6.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource6.SelectCommand &= " order by version desc "

    End Sub
    Protected Sub Discussion_Comments1()

        SqlDataSource1.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource1.SelectCommand &= " Replace(Replace(Replace(Replace(Accomplishment_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Accomp_Comm"
        SqlDataSource1.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource1.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and len(Accomplishment_Comm)>4 and Version>0 and DateTime>"
        SqlDataSource1.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource1.SelectCommand &= " order by version desc"

        SqlDataSource2.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource2.SelectCommand &= " Replace(Replace(Replace(Replace(Strengths_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Str_Comm"
        SqlDataSource2.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource2.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and len(Strengths_Comm)>4 and Version>0  and DateTime>"
        SqlDataSource2.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource2.SelectCommand &= " order by version desc"

        'Development_Comm
        SqlDataSource3.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource3.SelectCommand &= " Replace(Replace(Replace(Replace(Development_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Dev_Comm"
        SqlDataSource3.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource3.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and len(Development_Comm)>4 and Version>0  and DateTime>"
        SqlDataSource3.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource3.SelectCommand &= " order by version desc"

        SqlDataSource4.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource4.SelectCommand &= " Replace(Replace(Replace(Replace(Rating_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Rate_Comm"
        SqlDataSource4.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource4.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and len(Rating_Comm)>4 and Version>0  and DateTime>"
        SqlDataSource4.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource4.SelectCommand &= " order by version desc"

        SqlDataSource5.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource5.SelectCommand &= " Replace(Replace(Replace(Replace(Summary_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Sum_Comm"
        SqlDataSource5.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource5.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and len(Summary_Comm)>4 and Version>0  and DateTime>"
        SqlDataSource5.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource5.SelectCommand &= " order by version desc"

        'Leadership_Comm
        SqlDataSource6.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource6.SelectCommand &= " Replace(Replace(Replace(Replace(Leadership_Comm, '{', '&lt;'), '}', '&gt;'), '`', ''''), Char(13), '<br>')Lead_Comm"
        SqlDataSource6.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " "
        SqlDataSource6.SelectCommand &= " and emplid=" & lblEMPLID.Text & " and len(Leadership_Comm)>4 and Version>0  and DateTime>"
        SqlDataSource6.SelectCommand &= " (select Max(DateTime)DateTime from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') "
        SqlDataSource6.SelectCommand &= " order by version desc"

    End Sub




    Protected Sub SaveData()

        SQL1 = "Update Appraisal_Master_TBL Set Strengths='" & Replace(Replace(Replace(Replace(txbStrengths.Text, "'", "`"), "<", "{"), ">", "}"), " ", "&nbsp;") & "',"
        SQL1 &= "Development_Area='" & Replace(Replace(Replace(Replace(txbDevelopment.Text, "'", "`"), "<", "{"), ">", "}"), " ", "&nbsp;") & "',"
        SQL1 &= "Overall_Summary='" & Replace(Replace(Replace(Replace(txbOverall_Summary.Text, "'", "`"), "<", "{"), ">", "}"), " ", "&nbsp;") & "'"
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

        SQL1 &= " Update Appraisal_Accomplishments_TBL Set Accomplishment= '" & Replace(Replace(Replace(Replace(txbAccomplishment.Text, "'", "`"), "<", "{"), ">", "}"), " ", "&nbsp;") & "' "
        SQL1 &= " where IndexId=1 and Perf_Year = " & lblYEAR.Text & " and EMPLID = " & lblEMPLID.Text & " "
        'SQL1 &= " Update Appraisal_Master_tbl Set Comments='" & Replace(Replace(Replace(txbEmployeeComments.Text, "'", "`"), "<", "{"), ">", "}") & "' where emplid=" & lblEMPLID.Text & "  and Perf_Year=" & lblYEAR.Text

        'Response.Write(SQL1) : Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub AllRules()

        Dim Flag As Integer = 0

        SQL = "select * from"
        SQL &= " (select count(*)Acom1 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1)A1,"
        SQL &= " (select len(Strengths)Len_Str,len(Development_Area)Len_DevArea,Len(Overall_Summary)Len_OverSum,Len(Development_Objective)Len_DevObj,Overall_Rating,"
        SQL &= " Make_Balance,Build_Trust,Learn_Continuously,Lead_Urgency,Promote_Collab,Confront_Challenge,Lead_Change,Inspire_Risk,Leverage_External,Communic_Impact"
        SQL &= " from Appraisal_Master_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")B"
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '---1. ACCOMPLISHMENT TABLE---
        If CDbl(DT.Rows(0)("Acom1").ToString) = 1 Then
            SQL1 = "select len(Accomplishment)Accomplishment1 from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1"
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            If CDbl(DT1.Rows(0)("Accomplishment1").ToString) < 5 Then txbAccomplishment.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbAccomplishment.BackColor = Drawing.Color.White
        End If
        '---2. Check Strengths in Master table ---
        If CDbl(DT.Rows(0)("Len_Str").ToString) < 3 Then txbStrengths.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbStrengths.BackColor = Drawing.Color.White
        '---3. Check Development Areas in Master table ---
        If CDbl(DT.Rows(0)("Len_DevArea").ToString) < 5 Then txbDevelopment.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbDevelopment.BackColor = Drawing.Color.White
        '---4. Check Overall Summary in Master table ---
        If CDbl(DT.Rows(0)("Len_OverSum").ToString) < 5 Then txbOverall_Summary.BackColor = Drawing.Color.Yellow : Flag = 1 Else txbOverall_Summary.BackColor = Drawing.Color.White

        '---5. Check Overall Performance Rating in Master table ---
        If CDbl(DT.Rows(0)("Overall_Rating").ToString) = 0 Then trOverall_Performance.BgColor = "Yellow" : Flag = 1 Else trOverall_Performance.BgColor = "White"

        '---6. Addendum: Leadership Competencies in Master table ---
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


        If Flag = 0 Then
            SendToGeneralist()
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('Please fill yellow highlighted fields');</script>")
            Return
        End If

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub SendToGeneralist()

        SQL1 = "Update Appraisal_Master_tbl Set DateSUB_UP_MGT='" & Now & "',DateSUB_HR='" & Now & "',Process_Flag=2 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        Msg = lblEmpl_NAME.Text & "'s Performance Appraisal has been completed by " & LblMGT_NAME.Text & "  <br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"
        '--Production email--
        'LocalClass.SendMail(lblHR_Email.Text, "Performance Appraisal was sent to you for Approval by " & Session("NAME"), Msg)

        Response.Redirect("Appraisal_Edit_Mft.aspx?Token=" & z)

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub Accomplishments_TextChanged(sender As Object, e As EventArgs) Handles txbAccomplishment.TextChanged

    End Sub

    Protected Sub Strengths_TextChanged(sender As Object, e As EventArgs) Handles txbStrengths.TextChanged

    End Sub

    Protected Sub Development_TextChanged(sender As Object, e As EventArgs) Handles txbDevelopment.TextChanged

    End Sub

    Protected Sub Overall_Sum_TextChanged(sender As Object, e As EventArgs) Handles txbOverall_Summary.TextChanged

    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        SaveData()
        If Len(lblLogin_EMPLID.Text) > 0 Then
            If (lblLogin_EMPLID.Text <> lblEMPLID.Text) Then
                WindowBatch()
            End If
        End If
    End Sub
    Protected Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        If Len(lblLogin_EMPLID.Text) > 0 Then
            If (lblLogin_EMPLID.Text <> lblEMPLID.Text) And CDbl(lblProcess_Flag.Text) = 0 Then
                SQL11 = "select Window_batch from Appraisal_Master_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year= " & lblYEAR.Text
                DT11 = LocalClass.ExecuteSQLDataSet(SQL11)
                If lblWindowBatch.Text <> CDbl(DT11.Rows(0)("Window_batch").ToString) And Session("Process_Flag") = 0 Then
                    Response.Redirect("..\..\Default.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub SaveRecords1_Click(sender As Object, e As EventArgs) Handles SaveRecords1.Click
        WindowBatch()

        If CDbl(Session("Window_batch")) = CDbl(lblWindowBatch.Text) Then
            SaveData()
            lblMessage1.Visible = True
            lblMessage1.Text = "Data Saved"
            'ResetYellowColor()
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Check", "<script language='JavaScript'> alert('You have the identical form opened in another browser window.\n\nTo avoid confusion, you are not be permitted to enter information\n in this older version of the form.');</script>")
        End If


    End Sub
    Protected Sub btnSubmit_Generalist_Click(sender As Object, e As EventArgs) Handles btnSubmit_Generalist.Click
        AllRules()
    End Sub

    Protected Sub Img_Print1_Click(sender As Object, e As ImageClickEventArgs) Handles Img_Print1.Click
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

    Protected Sub btnEditForm_Click(sender As Object, e As EventArgs) Handles btnEditForm.Click
        'SQL = "select (case when count(*)=1 then 1 else count(*) end) Ver from Appraisal_Discussion_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        'DT = LocalClass.ExecuteSQLDataSet(SQL)

        SQL1 = "Update Appraisal_MASTER_tbl set Process_Flag=0 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        SQL1 &= " Insert into Appraisal_Discussion_tbl values('" & lblEMPLID.Text & "','" & lblYEAR.Text & "','" & lblFIRST_MGT_EMPLID.Text & "',"
        SQL1 &= " '" & lblFIRST_MGT_EMPLID.Text & "','" & Now & "','Edit','','','','','','','-1')"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Response.Redirect("Appraisal_Edit_Mgt.aspx?Token=" & z)

    End Sub

    Protected Sub btnSendEmployee_Click(sender As Object, e As EventArgs) Handles btnSendEmployee.Click
        SQL = "Update Appraisal_MASTER_tbl set DateSUB_Empl='" & Now & "',Process_Flag=4 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        Msg = "Your performance appraisal has been completed by your manager and is ready for your review.<br><br>"
        Msg &= "<b>NOTE: Please do not electronically sign and submit the review until you have had the Appraisal/Ratings Criteria discussion with your manager."
        Msg &= " Your electronic signature only confirms that you have had the discussion, not that you necessarily agree with the contents of the appraisal.</b><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"
        '--Production email 
        'LocalClass.SendMail(lblEmpl_Email.Text, "Your Performance Appraisal is ready for your review", Msg)
        Response.Redirect("Appraisal_Edit_Mgt.aspx?Token=" & z)

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

End Class