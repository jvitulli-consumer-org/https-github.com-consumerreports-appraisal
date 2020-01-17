Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Public Class Guild_Appraisal1
    Inherits System.Web.UI.Page
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

        lblEMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), _
                      "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")

        lblYEAR.Text = Session("YEAR")

        SQL7 = "select count(*)CNT_Appr_Exist from Appraisal_Master_tbl where emplid=" & lblEMPLID.Text
        'Response.Write(SQL) : Response.End()
        DT7 = LocalClass.ExecuteSQLDataSet(SQL7)
        LocalClass.CloseSQLServerConnection()

        If DT7.Rows(0)("CNT_Appr_Exist").ToString = 0 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('New Employee not eligible to file Appraisal.');{window.close();} </script>")
        Else
            'lblYEAR.Text = Session("YEAR")
            DisplayData()
            SetLevel_Approval()
        End If

        '--Hide Goal after 2017
        If Session("YEAR") >= 2018 Then Smart_Goal.Visible = False

    End Sub

    Protected Sub SetLevel_Approval()
        SQL = "select *,(case when Comments like '%and Employee declined to sign%' then 'Employee Declined to Sign' else Comments end)Comments1,"
        SQL &= " (select first+' '+last from id_tbl where emplid=a.emplid)Empl_Name,(select first+' '+last from id_tbl where emplid=a.MGT_EMPLID)MGT_Name,"
        SQL &= " (select first+' '+last from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.HR_EMPLID)HR_Name,"
        SQL &= " (select convert(char(10),hire_Date,101) from id_tbl where emplid=a.emplid)Empl_Hired,"
        SQL &= " (select email from id_tbl where emplid=a.emplid)Empl_Email,(select email from id_tbl where emplid=a.MGT_EMPLID)MGT_Email,"
        SQL &= " (select email from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Email,(select email from id_tbl where emplid=a.HR_EMPLID)HR_Email"
        SQL &= " from Appraisal_Master_tbl A where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        lblEmpl_NAME.Text = DT.Rows(0)("Empl_Name").ToString
        lblEmpl_TITLE.Text = DT.Rows(0)("JobTitle").ToString
        lblEmpl_DEPT.Text = DT.Rows(0)("Department").ToString
        lblEmpl_HIRE.Text = DT.Rows(0)("Empl_Hired").ToString

        lblFIRST_MGT_EMPLID.Text = DT.Rows(0)("MGT_EMPLID").ToString
        LblMGT_NAME.Text = DT.Rows(0)("MGT_Name").ToString
        lblSECOND_MGT_EMPLID.Text = DT.Rows(0)("UP_MGT_EMPLID").ToString
        'lblUP_MGT_NAME.Text = DT.Rows(0)("UP_MGT_Name").ToString
        lblHR_EMPLID.Text = DT.Rows(0)("HR_EMPLID").ToString
        lblHR_NAME.Text = DT.Rows(0)("HR_Name").ToString

        FY_Year.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2))
        Goal_Year3.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2) + 1)
        Goal_Year4.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2))
        Goal_Year5.Text = Trim(Right(Trim(DT.Rows(0)("Perf_Year").ToString), 2) + 1)

        SQL1 = "select top 1 * from(select emplid,(select first+' '+last from id_tbl where emplid=a.emplid)Employee,Rtrim(Ltrim(mgt_emplid))mgt_emplid,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=Rtrim(Ltrim(a.mgt_emplid)))Collab_MGT,DateTime from Appraisal_MasterHistory_tbl A where"
        SQL1 &= " Rtrim(Ltrim(emplid)) in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYEAR.Text & " and Rtrim(Ltrim(MGT_EMPLID)) not in (select Rtrim(Ltrim(mgt_emplid)) from "
        SQL1 &= " appraisal_master_tbl where emplid=a.emplid and Perf_Year=" & lblYEAR.Text & ")) AA ORDER BY DateTime desc"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        'Response.Write(SQL1) : Response.End()
        '--Get Collaboration Manager
        If DT1.Rows.Count > 0 Then lblCOLL_MGT_NAME.Text = DT1.Rows(0)("Collab_MGT").ToString

        If CDbl(Goal_Year3.Text) = "17" Then
            divOldTitle.Visible = True : divNewtext.Visible = False : divNewTitle.Visible = False
            divGoalText.Visible = False : divNewKey.Visible = False : divNewtarget.Visible = False
        Else
            divOldTitle.Visible = False : divNewtext.Visible = True : divNewTitle.Visible = True
            divGoalText.Visible = True : divNewKey.Visible = True : divNewtarget.Visible = True
        End If


        If CDbl(DT.Rows(0)("Process_Flag").ToString) = 1 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = "Waiting Approval"
            LblUP_MGT_Appr.ForeColor = Drawing.Color.Blue
            LblUP_MGT_Appr.Font.Bold = True
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 2 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = "Waiting Approval"
            LblHR_Appr.ForeColor = Drawing.Color.Blue
            LblHR_Appr.Font.Bold = True
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 3 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 4 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblEMP_Appr.Text = "Waiting Approval"
            LblEMP_Appr.ForeColor = Drawing.Color.Blue
            LblEMP_Appr.Font.Bold = True
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 5 Then
            LblMGT_Appr.Text = DT.Rows(0)("DateSUB_UP_MGT").ToString
            LblUP_MGT_Appr.Text = DT.Rows(0)("DateSUB_HR").ToString
            LblHR_Appr.Text = DT.Rows(0)("DateHR_Appr").ToString
            LblEMP_Appr.Text = DT.Rows(0)("DateEmpl_Appr").ToString
        End If

        If Len(DT.Rows(0)("Comments").ToString) > 4 And Len(DT.Rows(0)("DateEmpl_Refused").ToString) > 8 Then
            lblEmployeeComments.Visible = True
            lblEmployeeComments.Text = DT.Rows(0)("Comments").ToString
            lblEmployeeComments.Font.Bold = True
            lblEmployeeComments.ForeColor = Drawing.Color.Red
        ElseIf Len(DT.Rows(0)("Comments").ToString) > 4 And Len(DT.Rows(0)("DateEmpl_Refused").ToString) < 8 Then
            lblEmployeeComments.Visible = True
            lblEmployeeComments.Text = "<b>Employee's Comments:</b><br>" & Replace(Replace(Replace(Replace(DT.Rows(0)("Comments").ToString, Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        Else
            lblEmployeeComments.Visible = False
        End If



    End Sub

    Protected Sub DisplayData()
        '--Show/Hide Panels
        ShowHideAccomplishmentPanel()
        If Session("YEAR") < 2018 Then ShowHideGoalPanel()


        SQL1 = "select Process_Flag from Appraisal_Master_TBL where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        '--Master table
        SQL = "select A.*,Accomp1,C.* from(select * from Appraisal_Master_TBL  where Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")A JOIN"
        '--Accomplishments table 
        SQL &= "(select emplid,Accomplishment Accomp1 from Appraisal_Accomplishments_TBL where IndexID=1 and Perf_Year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text & ")B ON a.emplid=b.emplid, "

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
        '--Summary,Strengths,Development  Waiting Approval and Review---
        txbStrengthsA.Text = Replace(Replace(Replace(Replace(Replace(DT.Rows(0)("Strengths").ToString, Chr(10), "<br>"), Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDevelopment_AreasA.Text = Replace(Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Area").ToString, Chr(10), "<br>"), Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbOverall_SumA.Text = Replace(Replace(Replace(Replace(Replace(DT.Rows(0)("OverAll_Summary").ToString, Chr(10), "<br>"), Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        txbDevelopment_ObjectiveA.Text = Replace(Replace(Replace(Replace(Replace(DT.Rows(0)("Development_Objective").ToString, Chr(10), "<br>"), Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
        '--Accomplishments Waiting Approval and Review---
        txbAccomp1A.Text = Replace(Replace(Replace(Replace(Replace(DT.Rows(0)("Accomp1").ToString, Chr(10), "<br>"), Chr(13), "<br>"), "`", "'"), "{", "&lt;"), "}", "&gt;")
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

    Protected Sub ShowHideAccomplishmentPanel()

        SQL6 = "select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10 "
        SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,"
        SQL6 &= " (case when IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*) else 0 end)F_IND7,"
        SQL6 &= " (case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
        SQL6 &= " from Appraisal_Accomplishments_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " group by IndexID)A"
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        'If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_Accomp2_View.Visible = True Else Panel_Accomp2_View.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_Accomp3_View.Visible = True Else Panel_Accomp3_View.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_Accomp4_View.Visible = True Else Panel_Accomp4_View.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_Accomp5_View.Visible = True Else Panel_Accomp5_View.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel_Accomp6_View.Visible = True Else Panel_Accomp6_View.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel_Accomp7_View.Visible = True Else Panel_Accomp7_View.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel_Accomp8_View.Visible = True Else Panel_Accomp8_View.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel_Accomp9_View.Visible = True Else Panel_Accomp9_View.Visible = False
        'If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel_Accomp10_View.Visible = True Else Panel_Accomp10_View.Visible = False

        LocalClass.CloseSQLServerConnection()

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
            'Response.Write(SQL6) : Response.End()
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
        Else
            SQL6 = " select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10 "
            SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,"
            SQL6 &= " (case when IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*) else 0 end)F_IND7,"
            SQL6 &= " (case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
            SQL6 &= " from Appraisal_FutureGoal_Recall_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text + 1 & " and Appr_Goals=0 and Recall_Date in "
            SQL6 &= " (select DateEmpl_Appr from Appraisal_Master_tbl where emplid= " & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text & ") group by IndexID)A "
            'Response.Write(SQL6) : Response.End()
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

    
End Class