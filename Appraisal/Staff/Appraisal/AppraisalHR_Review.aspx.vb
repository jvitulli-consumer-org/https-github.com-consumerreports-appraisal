Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Public Class AppraisalHR_Review
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8, SQL9, SQL10, SQL11, SQL12, SQL13, SQL14, x, y, z, ReturnValue As String
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

        If Session("NETID") = "" Then Response.Redirect("..\..\default.aspx")

        Response.AddHeader("Refresh", "840")

        z = Request.QueryString("Token")


        lblEMPLID.Text = Mid(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9"), 5, 4)
        lblYEAR.Text = Left(Request.QueryString("Token"), 4)
        'lblWindowBatch.Text = Mid(Request.QueryString("Token"), 9)
        'lblDataBaseBatch.Text = Session("Window_batch")

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
            'Response.Write("Non-PostBack   " & lblEMPLID.Text & "  " & lblYEAR.Text)
            DisplayData()
        End If

    End Sub

    Protected Sub Discuss_Record()
        SQL3 = "select * from Appraisal_Discussion_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and Version=0"

        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        If DT3.Rows.Count = 0 Then
            SQL4 = "Insert into Appraisal_Discussion_tbl (EMPLID,Perf_Year,MGT_EMPLID,REJ_EMPLID,DateTime,Comments,Accomplishment_Comm,Strengths_Comm,Development_Comm,Rating_Comm,"
            SQL4 &= "Summary_Comm,Leadership_Comm,Version) Values(" & lblEMPLID.Text & "," & lblYEAR.Text & "," & lblFIRST_MGT_EMPLID.Text & ",'','" & Now & "','','','','','','','','0')"
            DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
        End If
        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub ShowButtonCursor()
        btnDiscuss.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        btnApproved.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        SaveRecords1.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        'RefuseSign.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
    End Sub

    Protected Sub SetLevel_Approval()
        '--Process_flag=0  manager create form
        '--Process_flag=1  return from HR to edit
        '--Process_flag=2  HR review
        '--Process_flag=4  send to employee
        '--Process_flag=5  employee e-sign

        'SQL = "select *,(case when Comments like '%and Employee declined to sign%' then 'Employee Declined to Sign' else Comments end)Comments1,"
        'SQL &= " (select first+' '+last from id_tbl where emplid=a.emplid)Empl_Name,(select first+' '+last from id_tbl where emplid=a.MGT_EMPLID)MGT_Name,"
        'SQL &= " (select first+' '+last from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.HR_EMPLID)HR_Name,"
        'SQL &= " (select convert(char(10),hire_Date,101) from id_tbl where emplid=a.emplid)Empl_Hired,"
        'SQL &= " (select email from id_tbl where emplid=a.emplid)Empl_Email,(select email from id_tbl where emplid=a.MGT_EMPLID)MGT_Email,"
        'SQL &= " (select email from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Email,(select email from id_tbl where emplid=a.HR_EMPLID)HR_Email"
        'SQL &= " from Appraisal_Master_tbl A where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        SQL = "select A.*,(case when A.Comments like '%and Employee declined to sign%' then 'Employee Declined to Sign' else A.Comments end)Comments1,first+' '+last Empl_Name,"
        SQL &= " (select first+' '+last from id_tbl where emplid in (select SUPERVISOR_ID from ps_employees where emplid=a.emplid)) MGT_Name,"
        SQL &= " (select first+' '+last from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Name,"
        SQL &= " (select first+' '+last from id_tbl where emplid=a.HR_EMPLID)HR_Name,convert(char(10),B.hire_Date,101)Empl_Hired,email Empl_Email,"
        SQL &= " (select email from id_tbl where emplid in (select SUPERVISOR_ID from ps_employees where emplid=a.emplid))MGT_Email,"
        SQL &= " (select email from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Email,(select email from id_tbl where emplid=a.HR_EMPLID)HR_Email "
        SQL &= " from Appraisal_Master_tbl A JOIN id_tbl B ON A.emplid=B.emplid where A.emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
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
        lblHR_Token.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Trim(DT.Rows(0)("HR_EMPLID").ToString), _
              "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        'Response.Write(lblHR_Token.Text & "<br>" & lblHR_EMPLID.Text) : Response.End()

        lblProcess_Flag.Text = DT.Rows(0)("Process_Flag").ToString

        FY_Year.Text = lblYEAR.Text

        If DT.Rows(0)("Process_flag").ToString = 2 Then '--Sent to HR for review 
            Discuss_Record()

            LblHR_Appr.Text = "Waiting Approval"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True
            LblMGT_Appr.Text = DT.Rows(0)("DateSub_HR").ToString
            btnApproved.Text = "Approve " & lblEmpl_NAME.Text & "'s Appraisal"
            btnDiscuss.Text = "Send back to " & DT.Rows(0)("MGT_Name").ToString & " for revision"
            'ElseIf DT.Rows(0)("Process_flag").ToString = 3 Then '--HR Approved 
            'ElseIf DT.Rows(0)("Process_flag").ToString = 4 Then '--
            'ElseIf DT.Rows(0)("Process_flag").ToString = 5 Then
            '--employee e-singned 
        End If

        'SQL1 = "select top 1 * from(select emplid,(select first+' '+last from id_tbl where emplid=a.emplid)Employee,Rtrim(Ltrim(mgt_emplid))mgt_emplid,"
        'SQL1 &= " (select first+' '+last from id_tbl where emplid=Rtrim(Ltrim(a.mgt_emplid)))Collab_MGT,DateTime from Appraisal_MasterHistory_tbl A where"
        'SQL1 &= " Rtrim(Ltrim(emplid)) in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYEAR.Text & " and LOGIN_EMPLID>0) AA "
        'SQL1 &= " where Rtrim(Ltrim(MGT_EMPLID)) not in (select Rtrim(Ltrim(mgt_emplid)) from appraisal_master_tbl where Rtrim(Ltrim(emplid)) in (" & lblEMPLID.Text & ")  and Perf_Year=" & lblYEAR.Text & ")"
        'SQL1 &= " ORDER BY DateTime desc"

        SQL1 = "select * from(select A.EMPLID,Employee,Current_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=A.Current_MGT_EMPLID)Current_MGT_Name,"
        SQL1 &= " B.MGT_EMPLID Collab_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=B.MGT_EMPLID)Collab_MGT from(select emplid,(First_name+' '+Last_name)Employee,"
        SQL1 &= " SUPERVISOR_EMPLID Current_MGT_EMPLID from HR_PDS_DATA_tbl)A JOIN Appraisal_MasterHistory_tbl B ON  A.emplid=B.emplid "
        SQL1 &= " where Perf_Year=" & lblYEAR.Text & " and LOGIN_EMPLID=0 and A.emplid =" & lblEMPLID.Text & ")C where Collab_MGT_EMPLID<>Current_MGT_EMPLID"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        '--Get Collaboration Manager
        If DT1.Rows.Count > 0 Then lblCOLL_MGT_NAME.Text = DT1.Rows(0)("Collab_MGT").ToString : COLL_MGT.Visible = True

    End Sub
    Protected Sub DisplayData()
        '--Process_flag=0  manager create form
        '--Process_flag=1  return from HR to edit
        '--Process_flag=2  HR review 
        '--Process_flag=3  HR approve
        '--Process_flag=4  send to employee
        '--Process_flag=5  employee e-sign

        SQL = "select AA.*,IsNull(BB.emplid,'0')Emplid_Comm,DateTime,IsNull(Accomplishment_Comm,'')Accomplishment_Comm,IsNull(Strengths_Comm,'')Strengths_Comm,"
        SQL &= "IsNull(Development_Comm,'')Development_Comm,IsNull(Rating_Comm,'')Rating_Comm,IsNull(Summary_Comm,'')Summary_Comm,IsNull(Leadership_Comm,'')Leadership_Comm from"
        SQL &= "(select A.*,Accomplishment from(select * from Appraisal_Master_TBL  where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A JOIN "
        SQL &= "(select * from Appraisal_Accomplishments_TBL  where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")B ON A.emplid=B.emplid )AA LEFT JOIN "
        SQL &= "(select * from Appraisal_Discussion_tbl where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and version=0) BB ON AA.emplid=BB.Emplid"
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

        If DT.Rows(0)("Process_flag").ToString = 2 Then
            Discussion_Comments()
            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False
            rbBelow.Enabled = False : rbNeed.Enabled = False : rbMeet.Enabled = False : rbExceed.Enabled = False : rbDisting.Enabled = False : rbMake_Need1.Enabled = False : rbMake_Prof1.Enabled = False
            rbMake_Exce1.Enabled = False : rbBuild2_Need1.Enabled = False : rbBuild2_Prof1.Enabled = False : rbBuild2_Exce1.Enabled = False : rbLearn_Need1.Enabled = False : rbLearn_Prof1.Enabled = False
            rbLearn_Exce1.Enabled = False : rbLead2_Need1.Enabled = False : rbLead2_Prof1.Enabled = False : rbLead2_Exce1.Enabled = False : rbProm_Need1.Enabled = False : rbProm_Prof1.Enabled = False
            rbProm_Exce1.Enabled = False : rbConf_Need1.Enabled = False : rbConf_Prof1.Enabled = False : rbConf_Exce1.Enabled = False : rbLead_Need1.Enabled = False : rbLead_Prof1.Enabled = False
            rbLead_Exce1.Enabled = False : rbInsp_Need1.Enabled = False : rbInsp_Prof1.Enabled = False : rbInsp_Exce1.Enabled = False : rbLeve_Need1.Enabled = False : rbLeve_Prof1.Enabled = False
            rbLeve_Exce1.Enabled = False : rbComm_Need1.Enabled = False : rbComm_Prof1.Enabled = False : rbComm_Exce1.Enabled = False
            '--Dispaly Manager's data
            lblAccomplishment.Visible = True : lblStrengths.Visible = True : lblDevelopment.Visible = True : lblOverall_Summary.Visible = True


            lblAccomplishment.Text = Replace(Replace(DT.Rows(0)("Accomplishment").ToString, Chr(13), "<span>"), Chr(10), "<br>")
            lblStrengths.Text = Replace(Replace(DT.Rows(0)("Strengths").ToString, Chr(13), "<span>"), Chr(10), "<br>")
            lblDevelopment.Text = Replace(Replace(DT.Rows(0)("Development_Area").ToString, Chr(13), "<span>"), Chr(10), "<br>")
            lblOverall_Summary.Text = Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<span>"), Chr(10), "<br>")



            '--Display Comments
            txbAccomplishment_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Accomplishment_Comm").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbCompetencies_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Leadership_Comm").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbStrengths_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths_Comm").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbDevelopment_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Comm").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbOverall_Performance_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Rating_Comm").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")
            txbOverall_Summary_Comm.Text = Replace(Replace(Replace(Replace(DT.Rows(0)("Summary_Comm").ToString, "{", "<"), "}", ">"), "`", "'"), "&nbsp;", " ")


        End If

    End Sub

    Protected Sub SaveData()
        SQL2 = "Update Appraisal_Discussion_tbl Set DateTime='" & Now & "',MGT_EMPLID='" & Trim(lblFIRST_MGT_EMPLID.Text) & "',REJ_EMPLID='" & Trim(lblHR_EMPLID.Text) & "',"
        SQL2 &= " Accomplishment_Comm='" & Replace(Replace(Replace(Replace(txbAccomplishment_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL2 &= " Strengths_Comm='" & Replace(Replace(Replace(Replace(txbStrengths_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL2 &= " Development_Comm='" & Replace(Replace(Replace(Replace(txbDevelopment_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL2 &= " Rating_Comm='" & Replace(Replace(Replace(Replace(txbOverall_Performance_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL2 &= " Summary_Comm='" & Replace(Replace(Replace(Replace(txbOverall_Summary_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "',"
        SQL2 &= " Leadership_Comm='" & Replace(Replace(Replace(Replace(txbCompetencies_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "'"
        SQL2 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and Version=0"
        'Response.Write(SQL2) ': Response.End()
        DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub Discussion_Comments()

        SqlDataSource1.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource1.SelectCommand &= " Replace(Replace(Accomplishment_Comm, Char(13), '<span>'), Char(10), '<br>')Accomp_Comm"
        SqlDataSource1.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SqlDataSource1.SelectCommand &= " and len(Accomplishment_Comm)>4 and Version>0 "
        SqlDataSource1.SelectCommand &= " and DateTime>(select IsNull(Max(DateTime),'01/01/'+convert(char(4),Year(Getdate()))) from Appraisal_Discussion_tbl"
        SqlDataSource1.SelectCommand &= " where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') order by version desc"

        SqlDataSource3.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource3.SelectCommand &= " Replace(Replace(Strengths_Comm, Char(13), '<span>'), Char(10), '<br>')Str_Comm"
        SqlDataSource3.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SqlDataSource3.SelectCommand &= " and len(Strengths_Comm)>4 and Version>0"
        SqlDataSource3.SelectCommand &= " and DateTime>(select IsNull(Max(DateTime),'01/01/'+convert(char(4),Year(Getdate()))) from Appraisal_Discussion_tbl"
        SqlDataSource3.SelectCommand &= " where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') order by version desc"

        'Development_Comm
        SqlDataSource3.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource3.SelectCommand &= " Replace(Replace(Development_Comm, Char(13), '<span>'), Char(10), '<br>')Dev_Comm"
        SqlDataSource3.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SqlDataSource3.SelectCommand &= " and len(Development_Comm)>4 and Version>0"
        SqlDataSource3.SelectCommand &= " and DateTime>(select IsNull(Max(DateTime),'01/01/'+convert(char(4),Year(Getdate()))) from Appraisal_Discussion_tbl"
        SqlDataSource3.SelectCommand &= " where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') order by version desc"

        SqlDataSource4.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource4.SelectCommand &= " Replace(Replace(Rating_Comm, Char(13), '<span>'), Char(10), '<br>')Rate_Comm"
        SqlDataSource4.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SqlDataSource4.SelectCommand &= " and len(Rating_Comm)>4 and Version>0"
        SqlDataSource4.SelectCommand &= " and DateTime>(select IsNull(Max(DateTime),'01/01/'+convert(char(4),Year(Getdate()))) from Appraisal_Discussion_tbl"
        SqlDataSource4.SelectCommand &= " where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') order by version desc"

        SqlDataSource5.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource5.SelectCommand &= " Replace(Replace(Summary_Comm, Char(13), '<span>'), Char(10), '<br>')Sum_Comm"
        SqlDataSource5.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SqlDataSource5.SelectCommand &= " and len(Summary_Comm)>4 and Version>0"
        SqlDataSource5.SelectCommand &= " and DateTime>(select IsNull(Max(DateTime),'01/01/'+convert(char(4),Year(Getdate()))) from Appraisal_Discussion_tbl"
        SqlDataSource5.SelectCommand &= " where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') order by version desc"

        'Leadership_Comm
        SqlDataSource6.SelectCommand = "select *, (select first+' '+ last from id_tbl where EMPLID=A.rej_emplid)Generalist,"
        SqlDataSource6.SelectCommand &= " Replace(Replace(Leadership_Comm, Char(13), '<span>'), Char(10), '<br>')Lead_Comm"
        SqlDataSource6.SelectCommand &= " from Appraisal_Discussion_tbl A where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ""
        SqlDataSource6.SelectCommand &= " and len(Leadership_Comm)>4 and Version>0"
        SqlDataSource6.SelectCommand &= " and DateTime>(select IsNull(Max(DateTime),'01/01/'+convert(char(4),Year(Getdate()))) from Appraisal_Discussion_tbl"
        SqlDataSource6.SelectCommand &= " where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & " and Comments='Edit') order by version desc"

    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        SaveData()
    End Sub

    Protected Sub SaveRecords1_Click(sender As Object, e As EventArgs) Handles SaveRecords1.Click
        SaveData()
        lblMessage1.Visible = True
        lblMessage1.Text = "Data Saved"
    End Sub

    Protected Sub btnDiscuss_Click(sender As Object, e As EventArgs) Handles btnDiscuss.Click

        If Len(txbAccomplishment_Comm.Text) + Len(txbStrengths_Comm.Text) + Len(txbDevelopment_Comm.Text) + Len(txbOverall_Performance_Comm.Text) + _
            Len(txbOverall_Summary_Comm.Text) + Len(txbCompetencies_Comm.Text) < 2 Then

            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please type Accomplishment,Strengths or other comments'); </script>")
        Else
            SaveData()

            'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> RefreshParent(); </script>")

            SQL2 = "select (case when count(*)=1 then 1 else count(*) end) Ver from Appraisal_Discussion_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
            SQL = "  Update Appraisal_MASTER_tbl set Process_Flag=1 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
            SQL &= " Update Appraisal_Discussion_tbl Set Version='" & DT2.Rows(0)("Ver").ToString & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and Version=0"
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            SQL1 = "select first+' '+last Name from id_tbl where emplid=" & lblHR_EMPLID.Text
            'Response.Write(SQL & "<br><br>" & SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            Msg = DT1.Rows(0)("Name").ToString & " has comments on " & lblEmpl_NAME.Text & "'s performance<br><br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

            'Response.Write(Msg) : Response.End()
            '--Production email--
            LocalClass.SendMail(lblMGT_Email.Text, DT1.Rows(0)("Name").ToString & " has comments on " & lblEmpl_NAME.Text & "'s performance", Msg)
            LocalClass.SendMail(lblHR_Email.Text, DT1.Rows(0)("Name").ToString & " has comments on " & lblEmpl_NAME.Text & "'s performance", Msg)

            '("vituja@consumer.org", DT1.Rows(0)("Name").ToString & " has comments on " & lblEmpl_NAME.Text & "'s performance", Msg)

            LocalClass.CloseSQLServerConnection()

            'x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Trim(lblHR_EMPLID.Text), "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
            'Response.Write("<script>window.location.href='http://" & Request.Url.Host & "/Default_HR.aspx?Token=" + x + "'; </script>")

            Response.Write("<script language='javascript'> { window.close();}</script>")

        End If

    End Sub

    Protected Sub btnApproved_Click(sender As Object, e As EventArgs) Handles btnApproved.Click

        'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> RefreshParent(); </script>")

        'Response.Write("Appraisal has been Reviewed by HR  Generalist " & lblHR_NAME.Text) : Response.End()

        SQL1 = "Update Appraisal_Master_tbl Set DateHR_Appr='" & Now & "',Process_Flag=3 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        SQL1 &= " delete Appraisal_Discussion_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and version=0"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblEmpl_NAME.Text & "'s Performance Appraisal has been Approved by " & lblHR_NAME.Text & "<br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br>"

        '--Production email--
        LocalClass.SendMail(lblMGT_Email.Text, "Performance Appraisal was Reviewed by HR", Msg)

        '("zubama@consumer.org", "Performance Appraisal was Reviewed by HR", Msg)

        Response.Write("<script language='javascript'> { window.close();}</script>")

    End Sub

    Protected Sub CommAccomplishment_TextChanged(sender As Object, e As EventArgs) Handles txbAccomplishment_Comm.TextChanged
        SQL5 = "Update Appraisal_Discussion_tbl Set DateTime='" & Now & "',MGT_EMPLID='" & Trim(lblFIRST_MGT_EMPLID.Text) & "',REJ_EMPLID='" & Trim(lblHR_EMPLID.Text) & "',"
        SQL5 &= " Accomplishment_Comm='" & Replace(Replace(Replace(Replace(txbAccomplishment_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "'"
        SQL5 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and Version=0"
        'Response.Write(SQL5) ': Response.End()
        DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub CommCompetencies_TextChanged(sender As Object, e As EventArgs) Handles txbCompetencies_Comm.TextChanged
        SQL6 = "Update Appraisal_Discussion_tbl Set DateTime='" & Now & "',MGT_EMPLID='" & Trim(lblFIRST_MGT_EMPLID.Text) & "',REJ_EMPLID='" & Trim(lblHR_EMPLID.Text) & "',"
        SQL6 &= " Leadership_Comm='" & Replace(Replace(Replace(Replace(txbCompetencies_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "'"
        SQL6 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and Version=0"
        'Response.Write(SQL6) ': Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub CommStrengths_TextChanged(sender As Object, e As EventArgs) Handles txbStrengths_Comm.TextChanged
        SQL7 = "Update Appraisal_Discussion_tbl Set DateTime='" & Now & "',MGT_EMPLID='" & Trim(lblFIRST_MGT_EMPLID.Text) & "',REJ_EMPLID='" & Trim(lblHR_EMPLID.Text) & "',"
        SQL7 &= " Strengths_Comm='" & Replace(Replace(Replace(Replace(txbStrengths_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "'"
        SQL7 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and Version=0"
        'Response.Write(SQL7) ': Response.End()
        DT7 = LocalClass.ExecuteSQLDataSet(SQL7)
        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub CommDevelopment_TextChanged(sender As Object, e As EventArgs) Handles txbDevelopment_Comm.TextChanged
        SQL8 = "Update Appraisal_Discussion_tbl Set DateTime='" & Now & "',MGT_EMPLID='" & Trim(lblFIRST_MGT_EMPLID.Text) & "',REJ_EMPLID='" & Trim(lblHR_EMPLID.Text) & "',"
        SQL8 &= " Development_Comm='" & Replace(Replace(Replace(Replace(txbDevelopment_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "'"
        SQL8 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and Version=0"
        'Response.Write(SQL8) ': Response.End()
        DT8 = LocalClass.ExecuteSQLDataSet(SQL8)
        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub CommOverall_Performance_TextChanged(sender As Object, e As EventArgs) Handles txbOverall_Performance_Comm.TextChanged
        SQL9 = "Update Appraisal_Discussion_tbl Set DateTime='" & Now & "',MGT_EMPLID='" & Trim(lblFIRST_MGT_EMPLID.Text) & "',REJ_EMPLID='" & Trim(lblHR_EMPLID.Text) & "',"
        SQL9 &= " Rating_Comm='" & Replace(Replace(Replace(Replace(txbOverall_Performance_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "'"
        SQL9 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and Version=0"
        'Response.Write(SQL9) ': Response.End()
        DT9 = LocalClass.ExecuteSQLDataSet(SQL9)
        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub CommOverall_Sum_TextChanged(sender As Object, e As EventArgs) Handles txbOverall_Summary_Comm.TextChanged
        SQL10 = "Update Appraisal_Discussion_tbl Set DateTime='" & Now & "',MGT_EMPLID='" & Trim(lblFIRST_MGT_EMPLID.Text) & "',REJ_EMPLID='" & Trim(lblHR_EMPLID.Text) & "',"
        SQL10 &= " Summary_Comm='" & Replace(Replace(Replace(Replace(txbOverall_Summary_Comm.Text, "'", "`"), "<", "{"), ">", "}"), "<br>", Chr(13)) & "'"
        SQL10 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " and Version=0"
        'Response.Write(SQL10) ': Response.End()
        DT10 = LocalClass.ExecuteSQLDataSet(SQL10)
        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub Img_Print1_Click(sender As Object, e As ImageClickEventArgs) Handles Img_Print1.Click
        Response.Write("<script>window.open('Appraisal_Print.aspx?Token=" + Request.QueryString("Token") + "','_blank' );</script>")
    End Sub

    Protected Sub RadioButton_Yes(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbtYes.CheckedChanged
        txbAccomplishment_Comm.Visible = True
        'SQL = "insert into SADForm_tbl (EMPLID,IndexID,Year,OwnerName,Submitted,Less1000) values (" & Trim(Session("EMPLID")) & ",11,'" & Year(Now()) & "','',0,0)"
        'SQL &= " delete SADForm_tbl where IndexID=40 and Year='" & Year(Now()) & "'and EMPLID=" & Trim(Session("EMPLID"))
        'DT = LocalClass.ExecuteSQLDataSet(SQL)
        'LocalClass.CloseSQLServerConnection()
        'Panel11.Visible = True : RdbtYes.Checked = True : RdbtNo.Checked = False : Panel2B.Visible = False
        'RdbtYes.ForeColor = Drawing.Color.Black : RdbtNo.ForeColor = Drawing.Color.Black

    End Sub
    Protected Sub RadioButton_No(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbtNo.CheckedChanged
        txbAccomplishment_Comm.Visible = False
        'SQL = "delete SADForm_tbl where IndexID=11 and Year='" & Year(Now()) & "'and EMPLID=" & Trim(Session("EMPLID"))
        'SQL &= " insert into SADForm_tbl(EMPLID,IndexID,Year,Less1000,Submitted) values (" & Trim(Session("EMPLID")) & ",40,'" & Year(Now()) & "',0,0)"
        'DT = LocalClass.ExecuteSQLDataSet(SQL)
        'LocalClass.CloseSQLServerConnection()
        'Panel11.Visible = False : RdbtYes.Checked = False : RdbtNo.Checked = True : tblFamReceiving.Text = ""
        'RdbtYes.ForeColor = Drawing.Color.Black : RdbtNo.ForeColor = Drawing.Color.Black : Panel2B.Visible = False
    End Sub


End Class