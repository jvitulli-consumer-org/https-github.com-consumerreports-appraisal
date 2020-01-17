Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Public Class Appraisal11
    Inherits System.Web.UI.Page
    Dim Msg, Msg1, Msq2, Msg3 As String
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8, SQL9, SQL10, SQL11, SQL12, SQL13, z, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6, DT7, DT8, DT9, DT10, DT11, DT12, DT13 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Trim(Request.QueryString("Token")) & "<br>" & Session("Year_Appr") & "<br>") ': Response.End()

        lblEMPLID.Text = Trim(Request.QueryString("Token"))
        lblYEAR.Text = Session("Year_Appr")

        ' Response.Write("HR  " & Session("HR_EMPLID") & "<br>MGT  " & Session("MGT_EMPLID"))

        'Response.Write(lblEMPLID.Text & "<br>" & lblYEAR.Text) : Response.End()

        DisplayData()

        SetLevel_Approval()

        'Response.Write("HR  " & lblHR_EMPLID.Text & "<br>MGT " & lblMGT_EMPLID.Text)

        SQL = "select Emplid,MGT_EMPLID,Process_Flag,FiscaLYear,Perf_Year,FiscaLYear-Perf_Year Current_FY from(/*2*/select A.EMPLID,MGT_EMPLID,Process_Flag,Perf_year,(/*1*/select (case when Months in (6,7,8,9,10,11,12) then Years"
        SQL &= " when Months in (1,2,3,4,5) then Years-1 end) from(select GetDate() Today,Year(GetDate())Years,Month(GetDate())Months,Day(GetDate())Day1)A/*1END*/)FiscaLYear from "
        SQL &= " Appraisal_Master_tbl A JOIN ps_employees B ON A.EMPLID=B.EMPLID and Process_Flag in(1,3)/*2END*/)C where FiscaLYear-Perf_Year=0 and emplid=" & lblEMPLID.Text
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If DT.Rows.Count > 0 And Len(Session("MGT_EMPLID")) = 0 Then
            Panel_ResetAppraisal.Visible = True
        Else
            Panel_ResetAppraisal.Visible = False
        End If


    End Sub

    Protected Sub SetLevel_Approval()
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
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        lblEmpl_NAME.Text = DT.Rows(0)("Empl_Name").ToString
        lblEmpl_TITLE.Text = DT.Rows(0)("JobTitle").ToString
        lblEmpl_DEPT.Text = DT.Rows(0)("Department").ToString
        lblEmpl_HIRE.Text = DT.Rows(0)("Empl_Hired").ToString

        lblFIRST_MGT_EMPLID.Text = DT.Rows(0)("MGT_EMPLID").ToString
        LblMGT_NAME.Text = DT.Rows(0)("MGT_Name").ToString

        If CDbl(DT.Rows(0)("Process_Flag").ToString) < 5 Then
            lblFIRST_MGT_EMPLID.Text = DT.Rows(0)("MGT_EMPLID").ToString
            LblMGT_NAME.Text = DT.Rows(0)("MGT_Name").ToString
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 5 Then
            SQL1 = "select B.Emplid MGT_EMPLID, B.First+' '+B.Last MGT_Name, email MGT_Email from Appraisal_Master_Esign_tbl A JOIN id_tbl B "
            SQL1 &= " ON A.MGT_Emplid=B.emplid where A.emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            lblFIRST_MGT_EMPLID.Text = DT1.Rows(0)("MGT_EMPLID").ToString
            LblMGT_NAME.Text = DT1.Rows(0)("MGT_Name").ToString
        End If


        lblSECOND_MGT_EMPLID.Text = DT.Rows(0)("UP_MGT_EMPLID").ToString
        'lblUP_MGT_NAME.Text = DT.Rows(0)("UP_MGT_Name").ToString
        lblHR_EMPLID.Text = DT.Rows(0)("HR_EMPLID").ToString
        lblHR_NAME.Text = DT.Rows(0)("HR_Name").ToString


        FY_Year.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2))

        If CDbl(DT.Rows(0)("Process_Flag").ToString) = 1 Then
            LblHR_Appr.Text = "Revision requested"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 2 Then
            LblHR_Appr.Text = "Waiting Approval"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True
            LblMGT_Appr.Text = DT.Rows(0)("DateSub_HR").ToString
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 3 Then
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 4 Then
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblEMP_Appr.Text = "Waiting Approval"
            LblEMP_Appr.ForeColor = Drawing.Color.Blue
            LblEMP_Appr.Font.Bold = True

        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 5 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSub_HR").ToString
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblEMP_Appr.Text = DT.Rows(0)("DateEmpl_Appr").ToString
        End If

        If CDbl(DT.Rows(0)("Process_Flag").ToString) = 5 Then
            If Len(DT.Rows(0)("Comments").ToString) > 4 And Len(DT.Rows(0)("DateEmpl_Refused").ToString) > 8 Then
                lblEmployeeComments.Visible = True
                lblEmployeeComments.Text = "<center>" + DT.Rows(0)("Comments").ToString
                lblEmployeeComments.Font.Bold = True
                lblEmployeeComments.BorderWidth = 0
                lblEmployeeComments.ForeColor = Drawing.Color.Red
            ElseIf Len(DT.Rows(0)("Comments").ToString) > 4 And Len(DT.Rows(0)("DateEmpl_Refused").ToString) < 8 Then
                lblEmployeeComments.Visible = True
                lblEmployeeComments.Text = "<b>Employee's Comments:</b><br>" & Replace(Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<span>"), Chr(10), "<br>")
            Else
                lblEmployeeComments.Visible = False
            End If
        End If

        'SQL1 = "select top 1 * from(select emplid,(select first+' '+last from id_tbl where emplid=a.emplid)Employee,Rtrim(Ltrim(mgt_emplid))mgt_emplid,"
        'SQL1 &= " (select first+' '+last from id_tbl where emplid=Rtrim(Ltrim(a.mgt_emplid)))Collab_MGT,DateTime from Appraisal_MasterHistory_tbl A where"
        'SQL1 &= " Rtrim(Ltrim(emplid)) in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYEAR.Text & " and Rtrim(Ltrim(MGT_EMPLID)) not in (select Rtrim(Ltrim(mgt_emplid)) from "
        'SQL1 &= " appraisal_master_tbl where emplid=a.emplid and Perf_Year=" & lblYEAR.Text & ")) AA ORDER BY DateTime desc"
        SQL1 = "select * from(select A.EMPLID,Employee,Current_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=A.Current_MGT_EMPLID)Current_MGT_Name,"
        SQL1 &= " B.MGT_EMPLID Collab_MGT_EMPLID,(select first+' '+last from id_tbl where emplid=B.MGT_EMPLID)Collab_MGT from(select emplid,(First_name+' '+Last_name)Employee,"
        SQL1 &= " SUPERVISOR_EMPLID Current_MGT_EMPLID from HR_PDS_DATA_tbl)A JOIN Appraisal_MasterHistory_tbl B ON  A.emplid=B.emplid "
        SQL1 &= " where Perf_Year=" & lblYEAR.Text & " and LOGIN_EMPLID=0 and A.emplid =" & lblEMPLID.Text & ")C where Collab_MGT_EMPLID<>Current_MGT_EMPLID"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        '--Get Collaboration Manager
        If DT1.Rows.Count > 0 Then lblCOLL_MGT_NAME.Text = DT1.Rows(0)("Collab_MGT").ToString : COLL_MGT.Visible = True

    End Sub

    Protected Sub DisplayData()

        SQL = "select A.*,Accomplishment from(select * from Appraisal_Master_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A JOIN "
        SQL &= " (select * from Appraisal_Accomplishments_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")B ON A.emplid=B.emplid "
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '---Oveall Performance Rating
        If DT.Rows(0)("Overall_rating").ToString = 1 Then rbBelow1.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 2 Then rbNeed1.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 3 Then rbMeet1.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 4 Then rbExceed1.Checked = True
        If DT.Rows(0)("Overall_rating").ToString = 5 Then rbDisting1.Checked = True
        '1. Make Balanced Decisions
        If DT.Rows(0)("Make_Balance").ToString = 1 Then rbMake_Need.Checked = True
        If DT.Rows(0)("Make_Balance").ToString = 2 Then rbMake_Prof.Checked = True
        If DT.Rows(0)("Make_Balance").ToString = 3 Then rbMake_Exce.Checked = True
        '2. Build Trust
        If DT.Rows(0)("Build_Trust").ToString = 1 Then rbBuild2_Need.Checked = True
        If DT.Rows(0)("Build_Trust").ToString = 2 Then rbBuild2_Prof.Checked = True
        If DT.Rows(0)("Build_Trust").ToString = 3 Then rbBuild2_Exce.Checked = True
        '3. Learn Continuously
        If DT.Rows(0)("Learn_Continuously").ToString = 1 Then rbLearn_Need.Checked = True
        If DT.Rows(0)("Learn_Continuously").ToString = 2 Then rbLearn_Prof.Checked = True
        If DT.Rows(0)("Learn_Continuously").ToString = 3 Then rbLearn_Exce.Checked = True
        '4. Lead with Urgency & Purpose
        If DT.Rows(0)("Lead_Urgency").ToString = 1 Then rbLead2_Need.Checked = True
        If DT.Rows(0)("Lead_Urgency").ToString = 2 Then rbLead2_Prof.Checked = True
        If DT.Rows(0)("Lead_Urgency").ToString = 3 Then rbLead2_Exce.Checked = True
        '5. Promote Collaboration & Accountability
        If DT.Rows(0)("Promote_Collab").ToString = 1 Then rbProm_Need.Checked = True
        If DT.Rows(0)("Promote_Collab").ToString = 2 Then rbProm_Prof.Checked = True
        If DT.Rows(0)("Promote_Collab").ToString = 3 Then rbProm_Exce.Checked = True
        '6. Confront Challenges
        If DT.Rows(0)("Confront_Challenge").ToString = 1 Then rbConf_Need.Checked = True
        If DT.Rows(0)("Confront_Challenge").ToString = 2 Then rbConf_Prof.Checked = True
        If DT.Rows(0)("Confront_Challenge").ToString = 3 Then rbConf_Exce.Checked = True
        '7. Lead Change
        If DT.Rows(0)("Lead_Change").ToString = 1 Then rbLead_Need.Checked = True
        If DT.Rows(0)("Lead_Change").ToString = 2 Then rbLead_Prof.Checked = True
        If DT.Rows(0)("Lead_Change").ToString = 3 Then rbLead_Exce.Checked = True
        '8. Inspire Risk Taking & innovation
        If DT.Rows(0)("Inspire_Risk").ToString = 1 Then rbInsp_Need.Checked = True
        If DT.Rows(0)("Inspire_Risk").ToString = 2 Then rbInsp_Prof.Checked = True
        If DT.Rows(0)("Inspire_Risk").ToString = 3 Then rbInsp_Exce.Checked = True
        '9. Leverage External Perspective
        If DT.Rows(0)("Leverage_External").ToString = 1 Then rbLeve_Need.Checked = True
        If DT.Rows(0)("Leverage_External").ToString = 2 Then rbLeve_Prof.Checked = True
        If DT.Rows(0)("Leverage_External").ToString = 3 Then rbLeve_Exce.Checked = True
        '10. Communicate for Impact
        If DT.Rows(0)("Communic_Impact").ToString = 1 Then rbComm_Need.Checked = True
        If DT.Rows(0)("Communic_Impact").ToString = 2 Then rbComm_Prof.Checked = True
        If DT.Rows(0)("Communic_Impact").ToString = 3 Then rbComm_Exce.Checked = True

        lblAccomplishment.Text = Replace(Replace(DT.Rows(0)("Accomplishment").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblStrengths.Text = Replace(Replace(DT.Rows(0)("Strengths").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblDevelopment.Text = Replace(Replace(DT.Rows(0)("Development_Area").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        lblOverall_Summary.Text = Replace(Replace(DT.Rows(0)("Overall_Summary").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        LocalClass.CloseSQLServerConnection()
    End Sub


    Protected Sub ResetAppraisal_Click(sender As Object, e As EventArgs) Handles ResetAppraisal.Click
        SQL = "Update Appraisal_Master_tbl Set Process_Flag=2 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & ""
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        LocalClass.CloseSQLServerConnection()
        Response.Redirect("Appraisal1.aspx?Token=" & lblEMPLID.Text)

    End Sub
End Class