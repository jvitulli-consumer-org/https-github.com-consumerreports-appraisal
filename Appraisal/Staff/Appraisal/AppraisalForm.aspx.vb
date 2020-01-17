Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Runtime.CompilerServices



Public Class AppraisalForm
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
        'btnDiscuss.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        'btnGeneralist.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        'btnSubmit_UpperMgr.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        'btnEditForm.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        'btnSendEmployee.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        'btnSubmit.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        'RefuseSign.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
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

        '--Accomplishment--
        'UpdatePanel_Accomplishment
        'UpdatePanel_Accomplishment_Comm

        '--Strengths
        'UpdatePanel_Strengths
        'UpdatePanel_Strengths_Comm

        '--Development
        'UpdatePanel_Development
        'UpdatePanel_Development_Comm

        '--Overall Performance Rating
        'rbBelow // rbNeed // rbMeet // rbExceed // rbDisting
        'UpdatePanel_Overall_Performance_Comm

        '-- Overall Summary
        'UpdatePanel_Overall_Summary
        'UpdatePanel_Overall_Summary_Comm

        '--Addendum
        'rbMake_Need1>>rbMake_Prof1>>rbMake_Exce1
        'rbBuild2_Need1>>rbBuild2_Prof1>>rbBuild2_Exce1
        'rbLearn_Need1>>rbLearn_Prof1>>rbLearn_Exce1
        'rbLead2_Need1>>rbLead2_Prof1>>rbLead2_Exce1
        'rbProm_Need1>>rbProm_Prof1>>rbProm_Exce1
        'rbConf_Need1>>rbConf_Prof1>>rbConf_Exce1
        'rbLead_Need1>>rbLead_Prof1>>rbLead_Exce1
        'rbInsp_Need1>>rbInsp_Prof1>>rbInsp_Exce1
        'rbLeve_Need1>>rbLeve_Prof1>>rbLeve_Exce1
        'rbComm_Need1>>rbComm_Prof1>>rbComm_Exce1

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

        If DT.Rows(0)("Process_flag").ToString = 0 Then
            '--Manager created/edited Appraisal 
            btnSubmit_Generalist.Text = "Send Appraisal to " & DT.Rows(0)("HR_Name").ToString  '--Send to HR 
            btnSubmit_Generalist.Visible = True
        ElseIf DT.Rows(0)("Process_flag").ToString = 1 Then
            LblHR_Appr.Text = "Rejected for Edit"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True

        ElseIf DT.Rows(0)("Process_flag").ToString = 2 Then
            '--send to HR for review 
            LblHR_Appr.Text = "Waiting Approval"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True
            LblMGT_Appr.Text = DT.Rows(0)("DateSub_HR").ToString

            SaveRecords1.Visible = False
            Timer1.Enabled = False
            Timer2.Enabled = False

            'ElseIf DT.Rows(0)("Process_flag").ToString = 3 Then
            '--HR Approved 
            'ElseIf DT.Rows(0)("Process_flag").ToString = 4 Then
            '--
            'ElseIf DT.Rows(0)("Process_flag").ToString = 5 Then
            '--employee e-singned 
        End If

    End Sub

    Protected Sub DisplayData()
        '--Process_flag=0  manager create form
        '--Process_flag=1  return from HR to edit
        '--Process_flag=2  HR review 
        '--Process_flag=3  HR approve
        '--Process_flag=4  send to employee
        '--Process_flag=5  employee e-sign

        SQL = "select AA.*,IsNull(BB.emplid,'0')Emplid_Comm,DateTime,IsNull(Accomplishment_Comm,'')Accomplishment_Comm,IsNull(Strengths_Comm,'')Strengths_Comm,"
        SQL &= " IsNull(Development_Comm,'')Development_Comm,IsNull(Rating_Comm,'')Rating_Comm,IsNull(Summary_Comm,'')Summary_Comm,Datetime,Rej_Generalist from"
        SQL &= " (select A.*,Accomplishment from(select * from Appraisal_Master_TBL  where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A JOIN "
        SQL &= " (select * from Appraisal_Accomplishments_TBL  where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")B ON A.emplid=B.emplid )AA LEFT JOIN "
        SQL &= " (select *,(select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Rej_Generalist from Appraisal_Discussion_tbl A where emplid=" & lblEMPLID.Text & " "
        SQL &= " and DateTime in(select Max(DateTime) from Appraisal_Discussion_tbl where emplid=" & lblEMPLID.Text & ")) BB ON AA.emplid=BB.Emplid"
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

        If DT.Rows(0)("Process_flag").ToString = 0 Then
            '--Manager create Appraisal 
            '---Accomplishment---
            txbAccomplishment.Visible = True : lblAccomplishment.Visible = False : lblAccomplishment_Comm.Visible = False
            txbAccomplishment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            lblAccomplishment_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

            'Datetime,Rej_Generalist

            '---Strengths---
            txbStrengths.Visible = True : lblStrengths.Visible = False : lblStrengths_Comm.Visible = False
            txbStrengths.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            lblStrengths_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            '---Development---
            txbDevelopment.Visible = True : lblDevelopment.Visible = True : lblDevelopment_Comm.Visible = False
            txbDevelopment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            lblDevelopment_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            '---Overall Performance---
            lblOverall_Performance_Comm.Visible = False
            '---Overall Summary---
            txbOverall_Summary.Visible = True : lblOverall_Summary.Visible = False : lblOverall_Summary_Comm.Visible = False


            txbOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            lblOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblOverall_Summary_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Summary_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

        ElseIf DT.Rows(0)("Process_flag").ToString = 1 Then
            '--HR rejected to edit Appraisal 
            txbAccomplishment.Visible = True : lblAccomplishment.Visible = False : lblAccomplishment_Comm.Visible = True : lblAccomplishment_Comm.BackColor = Drawing.Color.LightYellow
            txbAccomplishment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            lblAccomplishment_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

            '---Strengths---
            txbStrengths.Visible = True : lblStrengths.Visible = False : lblStrengths_Comm.Visible = True : lblStrengths_Comm.BackColor = Drawing.Color.LightYellow
            txbStrengths.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            lblStrengths_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

            '---Development---
            txbDevelopment.Visible = True : lblDevelopment.Visible = True : lblDevelopment_Comm.Visible = True : lblDevelopment_Comm.BackColor = Drawing.Color.LightYellow
            txbDevelopment.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            lblDevelopment_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")

            '---Overall Performance---
            lblOverall_Performance_Comm.Visible = True : lblOverall_Performance_Comm.BackColor = Drawing.Color.LightYellow
            lblOverall_Performance_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                Replace(Replace(Replace(Replace(DT.Rows(0)("Rating_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")



            '---Overall Summary---
            txbOverall_Summary.Visible = True : lblOverall_Summary.Visible = False : lblOverall_Summary_Comm.Visible = True : lblOverall_Summary_Comm.BackColor = Drawing.Color.LightYellow
            txbOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            lblOverall_Summary.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")
            lblOverall_Summary_Comm.Text = "<b>On " & DT.Rows(0)("Datetime").ToString & " " & DT.Rows(0)("Rej_Generalist").ToString & " rejected  with comments:</b><br> " & _
                Replace(Replace(Replace(Replace(DT.Rows(0)("Summary_Comm").ToString, Chr(13), "<br>"), "{", "&lt;"), "}", "&gt;"), "`", "'")




        ElseIf DT.Rows(0)("Process_flag").ToString = 2 Then
            '--send to HR for review 
            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False
            txbAccomplishment.Visible = False : lblAccomplishment.Visible = True : lblAccomplishment_Comm.Visible = False
            txbStrengths.Visible = False : lblStrengths.Visible = True : lblStrengths_Comm.Visible = False
            txbDevelopment.Visible = False : lblDevelopment.Visible = True : lblDevelopment_Comm.Visible = False
            txbOverall_Summary.Visible = False : lblOverall_Summary.Visible = True : lblOverall_Summary_Comm.Visible = False

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

        End If




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

        'Response.Write(lblEMPLID.Text & " <br>" & lblYEAR.Text) : Response.End()
        If CDbl(Trim(lblFIRST_MGT_EMPLID.Text)) - CDbl(Trim(lblSECOND_MGT_EMPLID.Text)) = 0 Then
            'Response.Write("1. First Manager =  Second Manager") : Response.End()
            If CDbl(Trim(lblSECOND_MGT_EMPLID.Text)) - CDbl(Trim(lblHR_EMPLID.Text)) = 0 Then
                Response.Write("2. Second Manger equal Generalist") : Response.End()
                '--Send to Employee -  bypass fist/second managers and generalist if they are the same
                SQL1 = "Update Appraisal_Master_tbl Set DateSUB_UP_MGT='" & Now & "',DateSUB_HR='" & Now & "',DateHR_Appr='" & Now & "',DateSUB_Empl='" & Now & "',"
                SQL1 &= "Process_Flag=4 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

                '--Production email 
                Msg = "Your performance appraisal has been completed by your manager and is ready for your review.<br><br>"
                Msg &= "<b>NOTE: Please do not electronically sign and submit the review until you have had the Appraisal/Ratings Criteria discussion with your manager."
                Msg &= " Your electronic signature only confirms that you have had the discussion, not that you necessarily agree with the contents of the appraisal.</b><br>"
                Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

                'LocalClass.SendMail(lblEmpl_Email.Text, "Your Performance Appraisal is ready for your review", Msg)


            ElseIf (CDbl(Trim(lblEMPLID.Text)) - CDbl(Trim(lblHR_EMPLID.Text)) = 0) Then
                'Response.Write("3. HR Employee equal Generalist") : Response.End()
                '--Send to Employee -  bypass first/second managers and generalist if they are the same
                SQL1 = "Update Appraisal_Master_tbl Set DateSUB_UP_MGT='" & Now & "',DateSUB_HR='" & Now & "',DateHR_Appr='" & Now & "',DateSUB_Empl='" & Now & "',"
                SQL1 &= "Process_Flag=4 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
                'Response.Write(SQL1) : Response.End()

                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                '--Production email 
                Msg = "Your performance appraisal has been completed by your manager and is ready for your review.<br><br>"
                Msg &= "<b>NOTE: Please do not electronically sign and submit the review until you have had the Appraisal/Ratings Criteria discussion with your manager."
                Msg &= "Your electronic signature only confirms that you have had the discussion, not that you necessarily agree with the contents of the appraisal.</b><br>"
                Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

                '--Production email--
                'LocalClass.SendMail(lblEmpl_Email.Text, "Your Performance Appraisal is ready for your review", Msg)

            Else
                'Response.Write("4. Else") : Response.End()
                '--Send to Generalist 
                'btnSubmit_UpperMgr.Text = "Submit for Review to " & DT.Rows(0)("HR_Name").ToString
                SQL1 = "Update Appraisal_Master_tbl Set DateSUB_UP_MGT='" & Now & "',DateSUB_HR='" & Now & "',Process_Flag=2 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
                'Response.Write(SQL1) : Response.End()
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                Msg = lblEmpl_NAME.Text & "'s Performance Appraisal has been completed by " & LblMGT_NAME.Text & "  <br>"
                Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

                Msg1 = lblEmpl_NAME.Text & "'s performance appraisal and goals have been completed by " & LblMGT_NAME.Text & " <br><br>"
                Msg1 &= "To view the form, please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx "

                Msg1 &= "<br><br>Note: As a 2nd level manager, you are not able to directly edit, but you can view all appraisals for your 2nd level direct reports. "
                Msg1 &= "If there are any edits you recommend, please speak with the 1st level manager as he/she can make those edits."

                '--Production email--
                'LocalClass.SendMail(lblHR_Email.Text, "Performance Appraisal was sent to you for Approval by " & Session("NAME"), Msg)
                'LocalClass.SendMail(lblUP_MGT_Email.Text, "Performance Appraisal For You to View", Msg1)

            End If
        Else

            If CDbl(Trim(lblFIRST_MGT_EMPLID.Text)) = 6210 Then
                'Response.Write("5 ---------    " & CDbl(Trim(lblFIRST_MGT_EMPLID.Text))) : Response.End()

                '--Send to Employee -  bypass first/second managers and generalist if they are the same
                SQL1 = "Update Appraisal_Master_tbl Set DateSUB_UP_MGT='" & Now & "',DateSUB_HR='" & Now & "',DateHR_Appr='" & Now & "',DateSUB_Empl='" & Now & "',"
                SQL1 &= "Process_Flag=4 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
                'Response.Write(SQL1) : Response.End()

                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                '--Production email 
                Msg = "Your performance appraisal has been completed by your manager and is ready for your review.<br><br>"
                Msg &= "<b>NOTE: Please do not electronically sign and submit the review until you have had the Appraisal/Ratings Criteria discussion with your manager."
                Msg &= "Your electronic signature only confirms that you have had the discussion, not that you necessarily agree with the contents of the appraisal.</b><br>"
                Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

                'LocalClass.SendMail(lblEmpl_Email.Text, "Your Performance Appraisal is ready for your review", Msg)

            Else
                'Response.Write("6 ---------    " & CDbl(Trim(lblFIRST_MGT_EMPLID.Text))) : Response.End()
                '--bypass second manager
                SQL1 = "Update Appraisal_Master_tbl Set DateSUB_UP_MGT='" & Now & "',DateSUB_HR='" & Now & "',Process_Flag=2 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text

                'Response.Write(SQL1) : Response.End()
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                '--Production email 
                Msg = lblEmpl_NAME.Text & "'s performance appraisal and goals have been completed by " & LblMGT_NAME.Text & " <br><br>"
                Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

                Session("UP_MGT_VIEW") = "MGT" & lblYEAR.Text & "" & lblEMPLID.Text & "" & lblMGT_EMPLID.Text

                Msg1 = lblEmpl_NAME.Text & "'s performance appraisal and goals have been submitted for HR approval by " & LblMGT_NAME.Text & " <br>"
                Msg1 &= "To view the form, please click on the <a href=http://" & Request.Url.Host & "/Appraisal/Defaults.aspx?Token=" & Session("UP_MGT_VIEW") & ">link.</a><br><br>"
                Msg1 &= "Note: As a 2nd level manager, you are not able to directly edit, but you can view all appraisals for your 2nd level direct reports."
                Msg1 &= "If there are any edits you recommend, please speak with the 1st level manager as he/she can make those edits."

                '--Production email--
                'LocalClass.SendMail(lblHR_Email.Text, "Performance Appraisal was sent to you for Approval by " & Session("NAME"), Msg)
                'LocalClass.SendMail(lblUP_MGT_Email.Text, "Performance Appraisal For You to View", Msg1)

            End If

        End If

        Response.Redirect("AppraisalForm.aspx?Token=" & Request.QueryString("Token"))

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
    
    Protected Sub rbBelow_CheckedChanged(sender As Object, e As EventArgs) Handles rbBelow.CheckedChanged

    End Sub
    Protected Sub rbNeed_CheckedChanged(sender As Object, e As EventArgs) Handles rbNeed.CheckedChanged

    End Sub
    Protected Sub rbMeet_CheckedChanged(sender As Object, e As EventArgs) Handles rbMeet.CheckedChanged

    End Sub
    Protected Sub rbExceed_CheckedChanged(sender As Object, e As EventArgs) Handles rbExceed.CheckedChanged

    End Sub
    Protected Sub rbDisting_CheckedChanged(sender As Object, e As EventArgs) Handles rbDisting.CheckedChanged

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

    Protected Sub Accomplishments_OnLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txbAccomplishment.Load

        'Dim scriptKey As String = "UniqueKeyForThisScript"
        'Dim javaScript As String = "<script type='text/javascript'>textAreaAdjust(o);</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, "<script type='text/javascript'>textAreaAdjust(this); true</script>")
        'Response.Write("<script language='javascript'> textAreaAdjust(this); </script>")

        'Dim charRows As Integer = 0
        'Dim tbCOntent As String
        'Dim chars As Integer = 0

        'If charRows.RowType = DataControlRowType.DataRow Then
        'For i As Integer = 0 To e.Row.Cells.Count - 1
        'If e.Row.Cells(i).Text = "&nbsp;" Then
        'e.Row.Cells(i).BorderColor = Drawing.Color.Red
        'e.Row.Cells(i).BorderWidth = 3
        'End If
        'Next
        'End If


        'tbCOntent = lblAccomplishment.Text
        'txbAccomplishment.Columns = 4
        'chars = tbCOntent.Length
        'charRows = chars / txbAccomplishment.Columns
        'Dim remaining As Integer = chars - charRows * txbAccomplishment.Columns
        'If remaining = 0 Then
        'txbAccomplishment.Rows = charRows
        'txbAccomplishment.TextMode = TextBoxMode.MultiLine
        'Else
        'txbAccomplishment.Rows = charRows + 1
        'txbAccomplishment.TextMode = TextBoxMode.MultiLine
        'End If
    End Sub


End Class