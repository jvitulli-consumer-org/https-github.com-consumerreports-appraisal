Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Public Class Default_HR
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, x, y, z, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", "840")

        If Session("HR_EMPLID") = "" Then Response.Redirect("default.aspx")

        lblMGT_EMPLID.Text = Session("HR_EMPLID")
        lblNAME.Text = "Welcome " & Session("First") & " " & Session("Last")

        If IsPostBack Then
            'Response.Write("1 Post Back<br>") ': Response.End()
            'Response.Write(DDLEmployees_Waiting.SelectedValue.ToString & "<br>" & Len(DDLEmployees_Waiting.SelectedValue.ToString)) ' : Response.End()
        Else
            LoadYear_Report()
            LoadYear_MY()
            LoadYear_My_Goal()
            LoadYear_Waiting()
            LoadYear_Employees()
            ViewHR_Page()
            LoadYear_DepartmentChange()
            LoadEmployee_DepartmentChange()
            LoadYear_Goal()
            LoadYear_Print()
            LoadYear_MyEmployee_Goal()
            Load_Term_Employee()
            LoadYear_MidPoint()
            LoadYear_Goals()

            'Response.Write("2 Not Post Back<br>") ': Response.End()

        End If

        ShowButtonCursor()

        'SQL = "select * from(select count(*)CNT_My_Goal from Appraisal_FUTUREGOALS_MASTER_TBL where process_flag in (5) and emplid=" & lblMGT_EMPLID.Text & ")A,"
        'SQL &= " (select Count(*)CNT_Emp_Goal from(select distinct Perf_Year from Appraisal_FutureGoals_Master_TBL where MGT_EMPLID=" & lblMGT_EMPLID.Text & " and "
        'SQL &= " Process_Flag=5 UNION select distinct Perf_Year from Guild_Appraisal_FutureGoal_Master_TBL where SUP_EMPLID=" & lblMGT_EMPLID.Text & " and Process_Flag=5)B)C,"
        'SQL &= " (select Count(*)CNT_MidPoint from Guild_MidPoint_MASTER_tbl where SUP_EMPLID=" & lblMGT_EMPLID.Text & " and Met_Mgt+Not_Met_Mgt=0)D,"
        'SQL &= " (select count(*)CNT_Appr_Wait from Appraisal_Master_tbl where process_flag in (2) and hr_emplid=" & lblMGT_EMPLID.Text & ")E"
        'Response.Write(SQL)
        'DT = LocalClass.ExecuteSQLDataSet(SQL)
        'If DT.Rows(0)("CNT_My_Goal").ToString > 0 Then Panel_My_Goal.Visible = True Else Panel_My_Goal.Visible = False
        'If DT.Rows(0)("CNT_Emp_Goal").ToString > 0 Then Panel_Employee_Goal.Visible = True Else Panel_Employee_Goal.Visible = False
        'If DT.Rows(0)("CNT_MidPoint").ToString > 0 Then Panel_MidPoint.Visible = True Else Panel_MidPoint.Visible = False
        'If DT.Rows(0)("CNT_Appr_Wait").ToString > 0 Then Panel_Appraisal_Waiting.Visible = True Else Panel_Appraisal_Waiting.Visible = False
        'Response.Write("Waiting Approval  " & DT.Rows(0)("CNT_Appr_Wait").ToString)
        'If DT.Rows(0)("CNT_Appr_Wait").ToString = 0 Then Panel_Appraisal_Waiting.Visible = False

        'Response.Write("3 Page Load<br>") ': Response.End()

    End Sub
    Protected Sub ShowHidePanel()
        SQL = " select count(*)CNT_Appr_Wait from Appraisal_Master_tbl where process_flag in (2) and hr_emplid=" & lblMGT_EMPLID.Text & ""
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        If DT.Rows(0)("CNT_Appr_Wait").ToString = 0 Then Panel_Appraisal_Waiting.Visible = False
    End Sub

    Protected Sub ShowButtonCursor()
        SubmitPer.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Goal1.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        SubmitMyEmployees.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_MyEmpl_Goal.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        SubmitWaiting.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        SubmitReport.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Goals_Report.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Goal.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        SubmitDepartment_Change.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        BulkPrint.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        BulkPrint_Term.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_MidPoint.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
    End Sub
    Protected Sub ViewHR_Page()
        SQL6 = " select Max(Report_Viewer)Report_Viewer,Max(My_Appraisal)My_Appraisal,Max(My_Goal)My_Goal,Max(Appraisal_Waiting_Approval)Appraisal_Waiting_Approval,Max(Goal_Waiting_Approval)Goal_Waiting_Approval,"
        SQL6 &= " Max(Print_Viewer)Print_Viewer,Max(My_Employees_Appraisal)My_Employees_Appraisal,Max(My_Employees_Goal)My_Employees_Goal,Max(Employee_Adjuster)Employee_Adjuster,Max(Print_Terminated)Print_Terminated,Max(MidPoint)MidPoint from "

        SQL6 &= " /*1.Report_Viewer*/(select count(*)Report_Viewer from(select distinct hr_generalist Report_Viewer from hr_pds_data_tbl UNION select 6235/*Jennifer*/ UNION select 5294/*Cindy*/ UNION select 6665/*Gwyneth*/"
        SQL6 &= " UNION select 6250/*Peggy*/UNION select 6494/*John*/UNION select 6274/*Patricia*/UNION select 6129/*Keili*/UNION select 5529/*Shona*/)A where Report_Viewer=" & lblMGT_EMPLID.Text & ")/*1END*/AA, "

        SQL6 &= " /*2.My_Appraisal*/(select count(*) My_Appraisal from me_appraisal_master_tbl where process_flag in (4,5) and emplid=" & lblMGT_EMPLID.Text & " UNION select count(*)My_Appraisal"
        SQL6 &= " from appraisal_master_tbl where process_flag in (4,5) and emplid=" & lblMGT_EMPLID.Text & " )/*2END*/BB,"

        SQL6 &= " /*3.Appraisal_Waiting_Approval*/(select count(*) Appraisal_Waiting_Approval from appraisal_master_tbl where process_flag in (2) and emplid in (select emplid from id_tbl where status='A') "
        SQL6 &= " and hr_emplid=" & lblMGT_EMPLID.Text & " and Perf_Year>2017)/*3END*/CC, "

        SQL6 &= " /*4.My_Employees_Appraisal*/(select My_Employees_Appraisal from(select count(*)My_Employees_Appraisal from me_appraisal_master_tbl where emplid in (select emplid from id_tbl where status='A') and "
        SQL6 &= " sup_emplid=" & lblMGT_EMPLID.Text & " UNION select count(*)My_Employees_Appraisal from appraisal_master_tbl where process_flag < 5 and mgt_emplid=" & lblMGT_EMPLID.Text & " "
        SQL6 &= " UNION select count(*)My_Employees_Appraisal from ps_employees where SUPERVISOR_ID=" & lblMGT_EMPLID.Text & ")D)/*4END*/DD, "


        SQL6 &= " (Select count(*)Goal_Waiting_Approval from Appraisal_FutureGoals_Master_tbl where hr_emplid=" & lblMGT_EMPLID.Text & " and process_flag=2)EE, "
        SQL6 &= " /*5.My_Employees_Goal*/(select My_Employees_Goal from(select count(*)My_Employees_Goal from appraisal_futuregoals_master_tbl where process_flag=5 and mgt_emplid=" & lblMGT_EMPLID.Text & ")F)FF/*5END*/,"

        SQL6 &= " /*6.My_Goal*/(select Max(My_Goal)My_Goal from(select count(*)My_Goal from appraisal_futuregoals_master_tbl where process_flag=5  and emplid=" & lblMGT_EMPLID.Text & " and Perf_Year<2019"
        SQL6 &= " UNION select count(*)My_Goal from appraisal_futuregoals_master_tbl where emplid=" & lblMGT_EMPLID.Text & " and Perf_Year>=2019)H)HH,/*6END*/"

        SQL6 &= " /*7.Print_Viewer*/(select count(*)Print_Viewer from(select 6250/*Peggy*/Print_Viewer UNION select 6671/*Tracy*/ UNION select 6785/*Lisa*/UNION select 6235/*Jennifer*/UNION select 6611/*Lauren*/ UNION select 6665/*Gwyneth*/"
        SQL6 &= " UNION select 5294/*Cindy*/UNION select 6494/*John*/UNION select 6274/*Patricia*/UNION select 6129/*Keili*/UNION select 5529/*Shona*/)A where Print_Viewer=" & lblMGT_EMPLID.Text & ")/*7END*/KK,"

        SQL6 &= " /*8.Employee_Adjuster*/(select count(*)Employee_Adjuster from( select distinct hr_generalist Employee_Adjuster from hr_pds_data_tbl UNION select 6250/*Peggy*/UNION select 5294/*Cindy*/ UNION select 6665/*Gwyneth*/"
        SQL6 &= " UNION select 6476/*Kelli*/UNION select 6235/*Jennifer*/UNION select 6494/*John*/UNION select 6274/*Patricia*/UNION select 6129/*Keili*/UNION select 5529/*Shona*/)A where Employee_Adjuster=" & lblMGT_EMPLID.Text & ")/*8END*/ LL,"

        SQL6 &= " /*9.Print Terminated*/(select count(*)Print_Terminated from(select distinct hr_generalist Print_Terminated from hr_pds_data_tbl UNION select 5294/*Cindy*/UNION select 6250/*Peggy*/ UNION select 6665/*Gwyneth*/"
        SQL6 &= " UNION select 6476/*Kelli*/UNION select 6235/*Jennifer*/UNION select 6494/*John*/UNION select 6274/*Patricia*/UNION select 6129/*Keili*/UNION select 5529/*Shona*/)A where Print_Terminated=" & lblMGT_EMPLID.Text & ")/*9END*/ MM,"

        SQL6 &= " /*10.MidPoint View*/(select count(*)MidPoint from(select distinct hr_generalist MidPoint from hr_pds_data_tbl UNION select 6274/*Patricia*/)A where MidPoint=" & lblMGT_EMPLID.Text & ")/*10END*/ NN "
        'Response.Write(SQL6) ': Response.End()

        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        '------------------------------------------------------------------------------
        If CDbl(DT6.Rows(0)("Report_Viewer").ToString) > 0 Then Panel_Appraisal_Report.Visible = True : Panel_Goals_Report.Visible = True Else Panel_Appraisal_Report.Visible = False : Panel_Goals_Report.Visible = False
        If CDbl(DT6.Rows(0)("Employee_Adjuster").ToString) > 0 Then Panel_Employee_Adjustment.Visible = True Else Panel_Employee_Adjustment.Visible = False
        If CDbl(DT6.Rows(0)("Print_Viewer").ToString) > 0 Then Panel_Appraisal_Print.Visible = True Else Panel_Appraisal_Print.Visible = False
        If CDbl(DT6.Rows(0)("Print_Terminated").ToString) > 0 Then Panel_Appraisal_Term_Print.Visible = True Else Panel_Appraisal_Term_Print.Visible = False

        If CDbl(DT6.Rows(0)("MidPoint").ToString) > 0 Then Panel_Midpoint_Report.Visible = True Else Panel_Midpoint_Report.Visible = False

        '------------------------------------------------------------------------------
        If CDbl(DT6.Rows(0)("My_Appraisal").ToString) > 0 Then Panel_My_Appraisal.Visible = True : Panel_My_Staff.Visible = True Else Panel_My_Appraisal.Visible = False : Panel_My_Staff.Visible = False
        If CDbl(DT6.Rows(0)("My_Goal").ToString) > 0 Then Panel_My_Goal.Visible = True : Panel_My_Staff.Visible = True Else Panel_My_Goal.Visible = False : Panel_My_Staff.Visible = False

        '-------------------------------------------------------------------------------
        If CDbl(DT6.Rows(0)("My_Employees_Appraisal").ToString) > 0 Then Panel_My_Employees_Appraisal.Visible = True : Panel_My_Staff.Visible = True Else Panel_My_Employees_Appraisal.Visible = False : Panel_My_Staff.Visible = False

        If CDbl(DT6.Rows(0)("My_Employees_Goal").ToString) > 0 Then Panel_Employee_Goal.Visible = True Else Panel_Employee_Goal.Visible = False

        '-------------------------------------------------------------------------------
        If CDbl(DT6.Rows(0)("Appraisal_Waiting_Approval").ToString) > 0 Then Panel_Appraisal_Waiting.Visible = True Else Panel_Appraisal_Waiting.Visible = False
        If CDbl(DT6.Rows(0)("Goal_Waiting_Approval").ToString) > 0 Then Panel_Goal_Waiting.Visible = False Else Panel_Goal_Waiting.Visible = False

        If CDbl(DT6.Rows(0)("My_Employees_Appraisal").ToString) + CDbl(DT6.Rows(0)("My_Employees_Goal").ToString) + CDbl(DT6.Rows(0)("MidPoint").ToString) > 0 Then '--CDbl(DT6.Rows(0)("My_Appraisal").ToString) + CDbl(DT6.Rows(0)("My_Goal").ToString) + 
            Panel_My_Staff1.Visible = True
        Else
            Panel_My_Staff1.Visible = False
        End If

        '-------------------------------------------------------------------------------
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub LoadEmployees_Report()
        Dim i As Integer
        'Response.Write("4)-Open " & DDLYears.SelectedValue.ToString & " Year" & Year(Now)) : Response.End()
        If Len(DDLYears_Report.SelectedValue.ToString) = 0 Then

        Else
            DDLEmployees_Report.Items.Clear()
            SQL = " select distinct 1 YEAR_ID,'Guild' SAP from Guild_Appraisal_master_tbl where Perf_Year=" & DDLYears_Report.SelectedValue.ToString & " UNION "
            SQL &= "select distinct 2 YEAR_ID,'Manager/Exempt' SAP from ME_Appraisal_master_tbl  where Perf_Year=" & DDLYears_Report.SelectedValue.ToString & " UNION "
            SQL &= "select distinct 3 YEAR_ID,'Guild' SAP from Appraisal_master_tbl where Perf_Year=" & DDLYears_Report.SelectedValue.ToString & " UNION "
            SQL &= "select distinct 4 YEAR_ID,'Manager/Exempt' SAP from Appraisal_master_tbl  where Perf_Year=" & DDLYears_Report.SelectedValue.ToString & " UNION "
            SQL &= "select distinct 5 YEAR_ID,'All Employees' SAP from Appraisal_master_tbl  where Perf_Year=" & DDLYears_Report.SelectedValue.ToString & " order by YEAR_ID desc"
            'Response.Write(SQL)
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            DDLEmployees_Report.Items.Clear()
            DDLEmployees_Report.Items.Add(New ListItem("Choose Employee's Type", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLEmployees_Report.Items.Add(New ListItem(DT.Rows(i)("SAP").ToString, DT.Rows(i)("YEAR_ID").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub LoadYear_Report()
        SQL = "select distinct Perf_Year from ME_Appraisal_MASTER_tbl UNION "
        SQL &= "select distinct Perf_Year from GUILD_Appraisal_MASTER_tbl where Perf_Year not in (2016) UNION "
        SQL &= "select distinct Perf_Year from Appraisal_MASTER_tbl order by Perf_Year desc"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYears_Report.Items.Clear()

        DDLYears_Report.Items.Add(New ListItem("Choose Year", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_Report.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        'End If
        LocalClass.CloseSQLServerConnection()

        DDLEmployees_Report.Items.Add(New ListItem("Choose Employee's Type", "0"))

    End Sub
    Protected Sub Year_Change_Report(sender As Object, e As EventArgs) Handles DDLYears_Report.SelectedIndexChanged
        Session("YEAR") = DDLYears_Report.SelectedValue.ToString

        If DDLYears_Report.SelectedValue.ToString > 0 Then
            DDLEmployees_Report.Enabled = True
            LoadEmployees_Report()
        Else
            DDLEmployees_Report.Items.Clear()
            DDLEmployees_Report.Items.Add(New ListItem("Choose Employee's Type", "0"))
            DDLEmployees_Report.Enabled = False
        End If
    End Sub
    Protected Sub DropDownList_Change_Report(sender As Object, e As EventArgs) Handles DDLEmployees_Report.SelectedIndexChanged
        Session("EMPLID") = DDLEmployees_Report.SelectedValue.ToString
        lblEMPLID.Text = DDLEmployees_Report.SelectedValue.ToString
    End Sub
    Protected Sub SubmitReport_Click(sender As Object, e As EventArgs) Handles SubmitReport.Click
        Dim x, y As String

        x = DDLEmployees_Report.SelectedValue.ToString
        y = DDLYears_Report.SelectedValue.ToString

        If DDLEmployees_Report.SelectedValue.ToString > 0 Then
            'Response.Redirect("Staff/HR/Appraisal_Reports.aspx?Token=" & x & y)
            Response.Write("<script>window.open('Staff/HR/Appraisal_Reports.aspx?Token=" + x + y + "','_blank' );</script>")
        Else

            If DDLYears_Report.SelectedValue.ToString = 0 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee`s Type from list and click submit button');</script>")
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose Employee`s Type from list and click submit button');</script>")
            End If
        End If

        DDLEmployees_Report.SelectedIndex = 0
        DDLEmployees_Report.Enabled = False
        DDLYears_Report.SelectedIndex = 0

    End Sub

    Protected Sub LoadYear_MY()
        'SQL = "select * from ME_Appraisal_MASTER_tbl where emplid=" & lblMGT_EMPLID.Text & " and Process_Flag in (4,5) order by Perf_Year desc"
        SQL = "select * from(select distinct Perf_Year from ME_Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and Process_Flag in(4,5) UNION "
        SQL &= " select distinct Perf_Year from Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and Process_Flag in(4,5))A order by Perf_Year desc"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If DT.Rows.Count > 0 Then
            DDLYears_MY.Items.Clear()
            DDLYears_MY.Items.Add(New ListItem("Choose Year", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLYears_MY.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If

    End Sub
    Protected Sub Year_Change_MY(sender As Object, e As EventArgs) Handles DDLYears_MY.SelectedIndexChanged

    End Sub
    Protected Sub SubmitPer_Click(sender As Object, e As EventArgs) Handles SubmitPer.Click

        z = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblMGT_EMPLID.Text, _
            "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        y = DDLYears_MY.SelectedValue.ToString

        If DDLYears_MY.SelectedValue.ToString > 0 Then

            If DDLYears_MY.SelectedValue.ToString = 2016 Or DDLYears_MY.SelectedValue.ToString = 2017 Then
                SQL = "select Process_Flag from Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and perf_year=" & CDbl(DDLYears_MY.SelectedValue.ToString)
                'Response.Write(SQL) : Response.End()
                DT = LocalClass.ExecuteSQLDataSet(SQL)
                Session("YEAR") = DDLYears_MY.SelectedValue.ToString
                Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                If CDbl(DT.Rows(0)("Process_Flag").ToString) = 4 Then
                    Response.Redirect("Staff/Exempt/Appraisal.aspx?Token=" & y + z)
                Else
                    Response.Write("<script>window.open('Staff/Exempt/Appraisal.aspx?Token=" + y + z + "','_blank' );</script>")
                End If
                DDLYears_MY.SelectedValue = 0


            ElseIf DDLYears_MY.SelectedValue.ToString < 2016 Then
                Session("YEAR") = DDLYears_MY.SelectedValue.ToString
                'Response.Write(Session("YEAR") & "<br>" & z) : Response.End()
                Response.Redirect("Retro/Exempt/My_Appraisal.aspx?Token=" & z)


            Else '2018
                Session("YEAR") = DDLYears_MY.SelectedValue.ToString
                Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                Response.Write("<script>window.open('Staff/Appraisal/Appraisal.aspx?Token=" + y + z + "','_blank' );</script>")
            End If
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR from list and click submit button');</script>")
        End If

        DDLYears_MY.SelectedIndex = 0

    End Sub
    Protected Sub Load_My_Employees()
        Dim i As Integer
        'Response.Write("4)-Open " & DDLYears.SelectedValue.ToString & " Year" & Year(Now)) : Response.End()
        If Len(DDLYears_Employees.SelectedValue.ToString) = 0 Then

        Else
            DDLMy_Employees.Items.Clear()
            SQL = "select * from("
            SQL &= " select A.emplid,name,Perf_Year,sup_emplid SUPID from Guild_Appraisal_MASTER_tbl A,ps_employees B where A.emplid=B.emplid and "
            SQL &= " (SUP_EMPLID=" & lblMGT_EMPLID.Text & " or Supervisor_id=" & lblMGT_EMPLID.Text & ") and Perf_Year=" & DDLYears_Employees.SelectedValue.ToString & ""
            SQL &= " UNION select A.emplid,name,Perf_Year,SUP_EMPLID SUPID from ME_Appraisal_MASTER_tbl A,ps_employees B where A.emplid=B.emplid and "
            SQL &= " (SUP_EMPLID=" & lblMGT_EMPLID.Text & " or Supervisor_id=" & lblMGT_EMPLID.Text & ") and Perf_Year=" & DDLYears_Employees.SelectedValue.ToString & ""
            SQL &= "  UNION select A.emplid,name,Perf_Year,MGT_EMPLID SUPID from Appraisal_Master_tbl A, ps_employees B where A.emplid=B.emplid and "
            SQL &= " (MGT_EMPLID=" & lblMGT_EMPLID.Text & " or Supervisor_id=" & lblMGT_EMPLID.Text & ") and Perf_Year=" & DDLYears_Employees.SelectedValue.ToString & ")A order by name"
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            DDLMy_Employees.Items.Clear()
            DDLMy_Employees.Items.Add(New ListItem("Choose Employee", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLMy_Employees.Items.Add(New ListItem(DT.Rows(i)("Name").ToString, DT.Rows(i)("emplid").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub LoadYear_Employees()
        SQL = "select * from(select distinct Perf_Year from ME_Appraisal_MASTER_tbl A, ps_employees B where a.emplid=b.emplid and (SUP_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ")"
        SQL &= " UNION select distinct Perf_Year from GUILD_Appraisal_MASTER_tbl A, ps_employees B where a.emplid=b.emplid and (SUP_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ")"
        SQL &= " UNION select distinct Perf_Year from Appraisal_Master_tbl A, ps_employees B where a.emplid=b.emplid and (Mgt_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ")"
        SQL &= " )A order by Perf_Year desc"
        'Response.Write(SQL) 'Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        If DT.Rows.Count > 0 Then
            DDLYears_Employees.Items.Clear()
            DDLYears_Employees.Items.Add(New ListItem("Choose Year", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLYears_Employees.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If
        DDLMy_Employees.Items.Add(New ListItem("Choose Employee", "0"))
    End Sub
    Protected Sub Year_Change_Employees(sender As Object, e As EventArgs) Handles DDLYears_Employees.SelectedIndexChanged
        Session("YEAR") = DDLYears_Employees.SelectedValue.ToString
        If DDLYears_Employees.SelectedValue.ToString > 0 Then
            DDLMy_Employees.Enabled = True
            Load_My_Employees()
        Else
            DDLMy_Employees.Items.Clear()
            DDLMy_Employees.Items.Add(New ListItem("Choose Employee's Type", "0"))
            DDLMy_Employees.Enabled = False
        End If
    End Sub
    Protected Sub DropDownList_Change_Employee(sender As Object, e As EventArgs) Handles DDLMy_Employees.SelectedIndexChanged

        If DDLMy_Employees.SelectedValue.ToString = 0 Then

        Else
            lblEMPLID.Text = DDLMy_Employees.SelectedValue.ToString

            If Session("YEAR") >= 2016 Then

                Session("EMPLID") = DDLMy_Employees.SelectedValue.ToString
                'Response.Write("EMPLID " & DDLMy_Employees.SelectedValue.ToString) : Response.End()
                SQL2 = "Select Process_Flag from appraisal_master_tbl where emplid = " & DDLMy_Employees.SelectedValue.ToString & " and Perf_Year=" & DDLYears_Employees.SelectedValue
                DT2 = LocalClass.ExecuteSQLDataSet(SQL2)

                SQL = "select Max(Window_Batch)Window_Batch from appraisal_master_tbl where emplid = " & DDLMy_Employees.SelectedValue.ToString & " and Perf_Year=" & DDLYears_Employees.SelectedValue
                'Response.Write(SQL) : Response.End()
                DT = LocalClass.ExecuteSQLDataSet(SQL)

                If DT2.Rows(0)("Process_Flag").ToString = 0 Or DT2.Rows(0)("Process_Flag").ToString = 1 Then
                    SQL1 = "Update appraisal_master_tbl Set Window_Batch=" & CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                    SQL1 &= " where emplid = " & DDLMy_Employees.SelectedValue.ToString & " and Perf_Year=" & DDLYears_Employees.SelectedValue & " and Process_flag in (0,1)"
                    'Response.Write(SQL1) : Response.End()
                    DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                Else
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString)
                End If
                
                LocalClass.CloseSQLServerConnection()
            End If
        End If

    End Sub
    Protected Sub SubmitMyEmployees_Click(sender As Object, e As EventArgs) Handles SubmitMyEmployees.Click
        'Response.Write(Session("MGT_EMPLID") & "<br>" & lblEMPLID.Text) : Response.End()

        x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblEMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        y = DDLYears_Employees.SelectedValue.ToString
        z = Session("Window_Batch")

        If DDLMy_Employees.SelectedValue.ToString > 0 Then

            If DDLYears_Employees.SelectedValue.ToString = 2016 Or DDLYears_Employees.SelectedValue.ToString = 2017 Then
                SQL1 = "select emplid,Empl_Type,Mgt_emplid,Process_Flag from Appraisal_Master_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & y
                'Response.Write(SQL1) : Response.End()
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                'Response.Write("1. " & DT1.Rows(0)("Mgt_emplid").ToString & "<br> 2." & DT1.Rows(0)("emplid").ToString) : Response.End()
                If CDbl(Trim(DT1.Rows(0)("Mgt_emplid").ToString)) <> CDbl(Trim(DT1.Rows(0)("emplid").ToString)) Then
                    Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                    Session("YEAR") = y
                    'Response.Write("1. redirect to my employee`s new Appraisal<br>" & x & "<br>" & y) : Response.End()
                    Response.Write("<script>window.open('Staff/Exempt/Appraisal_print.aspx?Token=" + y + x + z + "','_blank' );</script>")
                Else

                    If CDbl(DT1.Rows(0)("Process_Flag").ToString) = 0 Then
                        'Response.Write("3") : Response.End()
                        Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                        Session("YEAR") = y
                        Response.Write("<script>window.open('Staff/Exempt/Appraisal_print.aspx?Token=" + y + x + z + "','_blank' );</script>")
                    Else
                        'Response.Write("4") : Response.End()
                        Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                        Session("YEAR") = y
                        Response.Write("<script>window.open('Staff/Exempt/Appraisal_print.aspx?Token=" + y + x + "','_blank' );</script>")
                    End If
                End If

            ElseIf DDLYears_Employees.SelectedValue.ToString < 2016 Then
                Response.Redirect("Retro/Exempt/My_Appraisal.aspx?Token=" & x)

            Else ' 2018 and forward
                Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                SQL1 = "select emplid,Empl_Type,Mgt_emplid,Process_Flag from Appraisal_Master_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & y
                'Response.Write(SQL1) : Response.End()
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                LocalClass.CloseSQLServerConnection()
                If DT1.Rows(0)("Process_Flag").ToString < 5 Then
                    Session("YEAR") = y
                    Response.Write("<script>window.open('Staff/Appraisal/Appraisal_Edit.aspx?Token=" + y + x + z + "','_blank' );</script>")

                ElseIf DT1.Rows(0)("Process_Flag").ToString = 5 Then
                    Session("YEAR") = y
                    Response.Write("<script>window.open('Staff/Appraisal/Appraisal.aspx?Token=" + y + x + "','_blank' );</script>")
                End If

            End If

        Else

            If DDLYears_Employees.SelectedValue.ToString = 0 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee from list and click submit button');</script>")
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose employee from list and click submit button');</script>")
            End If
        End If

        DDLYears_Employees.SelectedIndex = 0
        DDLMy_Employees.Enabled = False
        DDLMy_Employees.SelectedIndex = 0
        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub LoadYear_Waiting()
        SQL = "select Perf_Year from(select * from(select distinct Perf_Year,'05/20/'+convert(char(4),Perf_Year)Today,Getdate()TodaySQL from appraisal_master_tbl "
        SQL &= " where Process_Flag=2 and HR_EMPLID=" & lblMGT_EMPLID.Text & ")A where Today < TodaySQL and Perf_Year>2017)B order by Perf_Year desc "
        'Response.Write(SQL) ': Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        If DT.Rows.Count > 0 Then
            DDLYears_Waiting.Items.Clear()
            DDLYears_Waiting.Items.Add(New ListItem("Choose Year", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLYears_Waiting.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If
        DDLEmployees_Waiting.Items.Add(New ListItem("Choose Employee", "0"))

    End Sub
    Protected Sub Load_Employees_Waiting()
        Dim i, j As Integer
        If Len(DDLYears_Waiting.SelectedValue.ToString) = 0 Then
        Else

            'Response.Write("4 Load Employees Waiting<br>") ': Response.End()

            DDLEmployees_Waiting.Items.Clear()

            DDLEmployees_Waiting.Items.Add(New ListItem("Choose Employee", "1"))

            'SQL = "select distinct B.DEPTID,B.DEPTNAME from appraisal_master_tbl A JOIN ps_employees B ON A.emplid=B.emplid where"
            'SQL &= " perf_year=" & DDLYears_Waiting.SelectedValue.ToString & " and HR_EMPLID=" & lblMGT_EMPLID.Text & " and process_flag=2 order by B.deptname"
            'Response.Write(SQL) : Response.End()
            'DT = LocalClass.ExecuteSQLDataSet(SQL)
            'For j = 0 To DT.Rows.Count - 1
            'DDLEmployees_Waiting.Items.Add(New ListItem(DT.Rows(j)("deptname").ToString, DT.Rows(j)("deptid").ToString))
            'If Len(DDLEmployees_Waiting.SelectedValue) > 6 Then
            'DDLEmployees_Waiting.Items(j).Attributes.Add("style", "background-color:Red;")
            'End If
            'SQL1 = "select B.DEPTID,Name,B.emplid,Process_Flag from appraisal_master_tbl A JOIN ps_employees B ON A.emplid=B.emplid where "
            'SQL1 &= "perf_year=" & DDLYears_Waiting.SelectedValue.ToString & " and HR_EMPLID=" & lblMGT_EMPLID.Text & " and process_flag=2 and"
            'SQL1 &= " B.deptid in (" & DT.Rows(j)("deptid").ToString & ") order by deptid,name"
            'Response.Write(SQL1) : Response.End()
            'DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            'For i = 0 To DT1.Rows.Count - 1
            'DDLEmployees_Waiting.Items.Add(New ListItem(DT1.Rows(i)("name").ToString, DT1.Rows(i)("emplid").ToString))
            'DDLEmployees_Waiting.Items(i).Attributes.Add("style", "background-color:white;")
            'Next
            'Response.Write(DDLEmployees_Waiting.SelectedValue.ToString & "<br>" & Len(DDLEmployees_Waiting.SelectedValue.ToString)) ' : Response.End()
            'Next

            SQL = " /*4*/select emplid,(case when emplid=0 then deptname else name end)Name,DEPTID,deptname,NUM,CNT from("
            SQL &= " /*3*/select *,(select count(*)CNT_EMPLID from appraisal_master_tbl A JOIN ps_employees B ON A.emplid=B.emplid "
            SQL &= " where perf_year = " & DDLYears_Waiting.SelectedValue.ToString & " And HR_EMPLID = " & lblMGT_EMPLID.Text & " "
            SQL &= " and process_flag = 2 and B.deptid=bb.DEPTID group by B.deptid)CNT from("
            SQL &= " /*1*/select 0 emplid,'' Name, * from(select /*row_number() over(order by deptname)*/ 0NUM,A.deptid,A.deptname deptname "
            SQL &= " from(select distinct deptid,deptname from ps_employees where len(deptid)>0)A)B/*1END*/ UNION"
            SQL &= " /*2*/select A.emplid,name,row_number() over(order by Name) Num,B.deptid,deptname from appraisal_master_tbl A JOIN ps_employees B ON A.emplid=B.emplid "
            SQL &= " where perf_year=" & DDLYears_Waiting.SelectedValue.ToString & " and HR_EMPLID=" & lblMGT_EMPLID.Text & " and process_flag=2/*2END*/"
            SQL &= " )BB/*3*/)CC where CNT>0 order by deptname,NUM/*4END*/"
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            For i = 0 To DT.Rows.Count - 1

                If CInt(DT.Rows(i)("emplid").ToString) = 0 Then

                    DDLEmployees_Waiting.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
                    'DDLEmployees_Waiting.Items(i).Attributes.Add("style", "color:Blue;")

                ElseIf CInt(DT.Rows(i)("emplid").ToString) > 10 Then

                    DDLEmployees_Waiting.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
                    'DDLEmployees_Waiting.Items(i).Attributes.Add("style", "color:Black;")

                End If

            Next

            ChangeColor_DropdownList()

            LocalClass.CloseSQLServerConnection()

        End If
    End Sub
    Protected Sub ChangeColor_DropdownList()
        For i = 0 To DDLEmployees_Waiting.Items.Count - 1
            If DDLEmployees_Waiting.Items(i).Value = 0 Then
                DDLEmployees_Waiting.Items(i).Attributes.Add("style", "color:#4441f4; font-weight: bolder; font-size: 15px ")
            Else
                DDLEmployees_Waiting.Items(i).Attributes.Add("style", "color:Black;")
            End If
        Next
    End Sub

    Protected Sub Year_Change_Waiting(sender As Object, e As EventArgs) Handles DDLYears_Waiting.SelectedIndexChanged
        Session("YEAR") = DDLYears_Waiting.SelectedValue.ToString
        If DDLYears_Waiting.SelectedValue.ToString > 0 Then
            DDLEmployees_Waiting.Enabled = True
            Load_Employees_Waiting()
        Else
            DDLEmployees_Waiting.Items.Clear()
            DDLEmployees_Waiting.Items.Add(New ListItem("Choose Employee", "0"))
            DDLEmployees_Waiting.Enabled = False
        End If
    End Sub
    Protected Sub DropDownList_Change_Waiting(sender As Object, e As EventArgs) Handles DDLEmployees_Waiting.SelectedIndexChanged
        'Response.Write(DDLEmployees_Waiting.SelectedValue.ToString & "<br>" & Len(DDLEmployees_Waiting.SelectedValue.ToString)) ': Response.End()
        ChangeColor_DropdownList()
    End Sub
    Protected Sub SubmitWaiting_Click(sender As Object, e As EventArgs) Handles SubmitWaiting.Click

        lblWaiting_Year.Text = DDLYears_Waiting.SelectedValue.ToString
        lblWaiting_EMPLID.Text = DDLEmployees_Waiting.SelectedValue.ToString

        If Len(DDLEmployees_Waiting.SelectedValue.ToString) > 0 Then '

            SQL = "Select sal_admin_plan1 SAP from id_tbl where emplid=" & lblWaiting_EMPLID.Text
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            If DDLEmployees_Waiting.SelectedIndex.ToString > 1 Then

                If Trim(lblWaiting_Year.Text) < 2016 Then
                    If Trim(DT.Rows(0)("SAP").ToString) = "GLD" Then
                        Session("YEAR") = lblWaiting_Year.Text
                        Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                        z = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblWaiting_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
                        Response.Redirect("Retro/Guild/Guild_Waiting_Approval.aspx?Token=" & z)
                    Else
                        Session("YEAR") = lblWaiting_Year.Text
                        Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                        z = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblWaiting_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
                        Response.Redirect("Retro/Exempt/Manager_Waiting_Approval.aspx?Token=" & z)
                    End If

                ElseIf DDLYears_Waiting.SelectedValue.ToString = 2016 Or DDLYears_Waiting.SelectedValue.ToString = 2017 Then

                    If Trim(DT.Rows(0)("SAP").ToString) = "GLD" Then
                        Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                        Session("YEAR") = lblWaiting_Year.Text
                        z = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblWaiting_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
                        Response.Write("<script>window.open('Staff/Guild/Appraisal.aspx?Token=" + lblWaiting_Year.Text + z + "','_blank' );</script>")
                        DDLYears_Waiting.SelectedValue = 0
                        DDLEmployees_Waiting.SelectedValue = 0
                        DDLEmployees_Waiting.Enabled = False
                    Else
                        Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                        Session("YEAR") = lblWaiting_Year.Text
                        z = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblWaiting_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
                        Response.Write("<script>window.open('Staff/Exempt/Appraisal.aspx?Token=" + lblWaiting_Year.Text + z + "','_blank' );</script>")
                        DDLYears_Waiting.SelectedValue = 0
                        DDLEmployees_Waiting.SelectedValue = 0
                        DDLEmployees_Waiting.Enabled = False
                    End If

                Else '2018 and forward

                    SQL1 = "select Empl_Type,Mgt_emplid,Process_Flag from Appraisal_Master_tbl where emplid=" & lblWaiting_EMPLID.Text & " and Perf_Year=" & lblWaiting_Year.Text
                    'Response.Write(SQL1) : Response.End()
                    DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                    LocalClass.CloseSQLServerConnection()

                    If DT1.Rows(0)("Empl_Type").ToString = "GLD" Then
                        'Response.Write("3") : Response.End()
                        Session("YEAR") = y
                        z = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblWaiting_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
                        Response.Write("<script>window.open('Staff/Appraisal/AppraisalHR_Review.aspx?Token=" + lblWaiting_Year.Text + z + "','_blank' );</script>")

                        DDLEmployees_Waiting.SelectedIndex = 0
                        DDLEmployees_Waiting.Enabled = False
                        DDLYears_Waiting.SelectedValue = 0

                    Else
                        'Response.Write("Manager appraisal")
                        Session("YEAR") = y
                        z = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblWaiting_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
                        Response.Write("<script>window.open('Staff/Appraisal/AppraisalHR_Review.aspx?Token=" + lblWaiting_Year.Text + z + "','_blank' );</script>")

                        DDLEmployees_Waiting.SelectedIndex = 0
                        DDLEmployees_Waiting.Enabled = False
                        DDLYears_Waiting.SelectedValue = 0
                    End If

                End If
            Else

                If DDLYears_Waiting.SelectedValue.ToString = 0 Then
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee from list and click submit button'); </script>")
                Else
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose employee from list and click submit button'); </script>")
                End If
            End If

            DDLEmployees_Waiting.SelectedIndex = 0
            DDLEmployees_Waiting.Enabled = False
            DDLYears_Waiting.SelectedValue = 0

        End If

    End Sub


    Protected Sub LoadYear_DepartmentChange()
        SQL = "select distinct Perf_Year from Appraisal_MASTER_tbl order by Perf_Year desc"
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If DT.Rows.Count > 0 Then
            DDLYear_Change_Department.Items.Clear()
            DDLYear_Change_Department.Items.Add(New ListItem("Choose Year", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLYear_Change_Department.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If


    End Sub
    Protected Sub LoadEmployee_DepartmentChange()

        'If lblMGT_EMPLID.Text = 6785 Or lblMGT_EMPLID.Text = 6250 Then
        SQL = "select distinct DEPTID,DEPARTMENT from Appraisal_MASTER_tbl where Len(deptid)>0 order by DEPARTMENT"
        'Else
        'SQL = "select distinct DEPTID,DEPARTMENT from Appraisal_MASTER_tbl where deptid not in (9009120) or DEPARTMENT not in ('Human Resources') order by DEPARTMENT"
        'End If
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If DT.Rows.Count > 0 Then
            DDL_Change_Department.Items.Clear()
            DDL_Change_Department.Items.Add(New ListItem("Choose Department", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDL_Change_Department.Items.Add(New ListItem(DT.Rows(i)("DEPARTMENT").ToString, DT.Rows(i)("DEPTID").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If

    End Sub
    Protected Sub Year_Change_Department(sender As Object, e As EventArgs) Handles DDLYear_Change_Department.SelectedIndexChanged

        Session("YEAR") = DDLYear_Change_Department.SelectedValue.ToString
        If DDLYear_Change_Department.SelectedValue.ToString > 0 Then
            DDL_Change_Department.Enabled = True
            LoadEmployee_DepartmentChange()
        Else
            DDL_Change_Department.Items.Clear()
            DDL_Change_Department.Items.Add(New ListItem("Choose Department", "0"))
            DDL_Change_Department.Enabled = False
        End If

    End Sub
    Protected Sub DropDownList_Update_Department(sender As Object, e As EventArgs) Handles DDL_Change_Department.SelectedIndexChanged

    End Sub
    Protected Sub SubmitDepartment_Change_Click(sender As Object, e As EventArgs) Handles SubmitDepartment_Change.Click

        If CDbl(DDLYear_Change_Department.SelectedValue.ToString) = 0 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Department from list and click submit button');</script>")
        ElseIf CDbl(DDL_Change_Department.SelectedValue.ToString) = 0 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please Department from list and click submit button');</script>")
        Else
            z = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblMGT_EMPLID.Text, _
                "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
            Session("Year") = DDLYear_Change_Department.SelectedValue.ToString
            Session("DEPTID") = DDL_Change_Department.SelectedValue.ToString
            Session("DEPARTMENT") = DDL_Change_Department.SelectedItem.Text
            'Response.Redirect("Staff/HR/Access_Employees.aspx?Token=" & z)
            Response.Write("<script>window.open('Staff/HR/Access_Employees.aspx?Token=" + z + "','_blank' );</script>")

        End If

    End Sub

    Protected Sub LoadEmployees_Goal()
        Dim i As Integer
        'Response.Write("4)-Open " & DDLYears.SelectedValue.ToString & " Year" & Year(Now)) : Response.End()
        If Len(DDLYears_Goal.SelectedValue.ToString) = 0 Then

        Else
            DDLEmployees_Goal.Items.Clear()
            SQL = "select a.emplid,last+','+first Name,Perf_Year,MGT_EMPLID SUPID,process_flag from Appraisal_FutureGoals_Master_tbl A,id_tbl B where a.emplid=b.emplid "
            SQL &= " and HR_EMPLID=" & lblMGT_EMPLID.Text & " and Process_Flag=2 and Perf_Year=" & DDLYears_Goal.SelectedValue & " order by Name"
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            DDLEmployees_Goal.Items.Clear()
            DDLEmployees_Goal.Items.Add(New ListItem("  Choose Employee  ", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLEmployees_Goal.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If

    End Sub
    Protected Sub LoadYear_Goal()
        SQL = "select distinct Perf_Year from Appraisal_FutureGoals_Master_TBL where Process_Flag=2 and HR_EMPLID=" & lblMGT_EMPLID.Text & " order by Perf_Year desc"
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYears_Goal.Items.Clear()

        DDLYears_Goal.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_Goal.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next

        LocalClass.CloseSQLServerConnection()

        DDLEmployees_Goal.Items.Add(New ListItem("  Choose Employee  ", "0"))
    End Sub
    Protected Sub Year_Change_Goal(sender As Object, e As EventArgs) Handles DDLYears_Goal.SelectedIndexChanged
        Session("YEAR") = DDLYears_Goal.SelectedValue.ToString

        If DDLYears_Goal.SelectedValue.ToString > 0 Then
            DDLEmployees_Goal.Enabled = True
            LoadEmployees_Goal()
        Else
            DDLEmployees_Goal.Items.Clear()
            DDLEmployees_Goal.Items.Add(New ListItem(" Choose Employee ", "0"))
            DDLEmployees_Goal.Enabled = False
        End If
    End Sub
    Protected Sub DropDownList_Change_Goal(sender As Object, e As EventArgs) Handles DDLEmployees_Goal.SelectedIndexChanged
        'Session("EMPLID") = DDLEmployees_Goal.SelectedValue.ToString
        'lblEMPLID.Text = DDLEmployees_Goal.SelectedValue.ToString
    End Sub
    Protected Sub Submit_Goal_Click(sender As Object, e As EventArgs) Handles Submit_Goal.Click
        Dim x, y As String

        x = DDLEmployees_Goal.SelectedValue.ToString
        y = DDLYears_Goal.SelectedValue.ToString
        'Response.Write(x) : Response.End()
        If DDLEmployees_Goal.SelectedValue.ToString > 0 Then
            Session("YEAR") = y
            Response.Write("<script>window.open('Staff/FutureGoals/FutureGoal.aspx?Token=" + y + x + "','_blank' );</script>")
        Else
            If DDLYears_Goal.SelectedValue.ToString = 0 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee from list and click submit button');</script>")
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose employee from list and click submit button');</script>")
            End If
        End If

        DDLYears_Goal.SelectedIndex = 0
        DDLEmployees_Goal.Enabled = False
        DDLEmployees_Goal.SelectedIndex = 0

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("Default.aspx")
    End Sub
    Protected Sub LoadYear_Print()
        SQL = "select distinct Perf_Year from ME_Appraisal_MASTER_tbl UNION "
        SQL &= "select distinct Perf_Year from GUILD_Appraisal_MASTER_tbl where Perf_Year not in (2016) UNION "
        SQL &= "select distinct Perf_Year from Appraisal_MASTER_tbl order by Perf_Year desc"
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYears_Print.Items.Clear()

        DDLYears_Print.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_Print.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

        DDLBulk_Print.Items.Add(New ListItem("  All Employees  ", "1"))

    End Sub
    Protected Sub Year_Change_Print(sender As Object, e As EventArgs) Handles DDLYears_Print.SelectedIndexChanged
        Session("YEAR") = DDLYears_Print.SelectedValue.ToString

        If DDLYears_Print.SelectedValue > 0 Then
            DDLBulk_Print.Enabled = True
            'DDLBulk_Print.Text = "  All Employees  "
        Else
            DDLBulk_Print.Enabled = False
            DDLBulk_Print.Items.Clear()
            DDLBulk_Print.Items.Add(New ListItem("  All Employees  ", "0"))
        End If


    End Sub
    Protected Sub Bulk_Change_Print(sender As Object, e As EventArgs) Handles DDLBulk_Print.SelectedIndexChanged

    End Sub
    Protected Sub BulkPrint_Click(sender As Object, e As EventArgs) Handles BulkPrint.Click
        If DDLYears_Print.SelectedValue > 0 Then
            Response.Write("<script>window.open('Staff/HR/BulkPrint/Default_Print.aspx?Token=" + DDLYears_Print.SelectedValue + "','_blank' );</script>")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Redirect", "<script language='JavaScript'> alert('Please choose year from list and click submit button');</script>")
        End If
    End Sub

    Protected Sub Year_My_Goal(sender As Object, e As EventArgs) Handles DDLYears_My_Goal.SelectedIndexChanged
        If DDLYears_My_Goal.SelectedValue.ToString > 0 Then
            Session("YEAR_Goal") = DDLYears_My_Goal.Text

            If DDLYears_My_Goal.SelectedValue.ToString > 2018 Then
                SQL = "select Process_Flag, Max(Window_Batch)Window_Batch from appraisal_futuregoals_master_tbl where emplid = " & lblMGT_EMPLID.Text & " and Perf_Year=" & DDLYears_My_Goal.SelectedValue & " group by Process_Flag"
                DT = LocalClass.ExecuteSQLDataSet(SQL)
                If CDbl(DT.Rows(0)("Process_Flag").ToString) = 0 Then
                    SQL1 = "Update appraisal_futuregoals_master_tbl Set Window_Batch=" & CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                    SQL1 &= " where emplid = " & lblMGT_EMPLID.Text & " and Perf_Year=" & DDLYears_My_Goal.SelectedValue
                    'Response.Write(SQL1) : Response.End()
                    DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                    LocalClass.CloseSQLServerConnection()
                Else
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString)
                End If
            End If

        Else
            Session("YEAR_Goal") = ""
        End If
    End Sub
    Protected Sub Submit_Goal1_Click(sender As Object, e As EventArgs) Handles Submit_Goal1.Click
        x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblMGT_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        y = DDLYears_My_Goal.SelectedValue
        z = Session("Window_Batch")

        If DDLYears_My_Goal.SelectedValue > 0 And DDLYears_My_Goal.SelectedValue < 2019 Then
            Session("YEAR") = DDLYears_My_Goal.SelectedValue
            Response.Write("<script>window.open('Staff/FutureGoals/FutureGoal_print.aspx?Token=" + y + x + "','_blank' );</script>")
            DDLYears_My_Goal.SelectedValue = 0
        ElseIf DDLYears_My_Goal.SelectedValue > 0 And DDLYears_My_Goal.SelectedValue = 2019 Then
            Session("YEAR") = DDLYears_My_Goal.SelectedValue
            Session("MGT_EMPLID") = lblMGT_EMPLID.Text
            Response.Write("<script>window.open('Staff/FutureGoals/FutureGoals_Print.aspx?Token=" + y + x + z + "','_blank' );</script>")
            DDLYears_My_Goal.SelectedValue = 0

        ElseIf DDLYears_My_Goal.SelectedValue > 0 And DDLYears_My_Goal.SelectedValue > 2019 Then '2020
            Session("YEAR") = DDLYears_My_Goal.SelectedValue
            Session("MGT_EMPLID") = lblMGT_EMPLID.Text
            Response.Write("<script>window.open('Staff/FutureGoals/myGoals.aspx?Token=" + y + x + z + "','_blank' );</script>")
            DDLYears_My_Goal.SelectedValue = 0

        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Redirect", "<script language='JavaScript'> alert('Please choose Goal year from list and click submit button');</script>")
        End If
    End Sub
    Protected Sub LoadYear_My_Goal()
        SQL = "select distinct Perf_Year from Appraisal_FutureGoals_Master_TBL where Process_Flag in(4,5) and emplid=" & lblMGT_EMPLID.Text & "  UNION "
        SQL &= " select distinct Perf_Year from Appraisal_FutureGoals_Master_TBL where Perf_Year >=2019 and emplid=" & lblMGT_EMPLID.Text & " order by Perf_Year desc"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYears_My_Goal.Items.Clear()
        DDLYears_My_Goal.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_My_Goal.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub DropDownList_MyEmployee_Goal(sender As Object, e As EventArgs) Handles DDLMyEmpl_Goal.SelectedIndexChanged

        If DDLMyEmpl_Goal.SelectedValue.ToString = 0 Then
        Else
            If Session("YEAR") > 2016 Then
                Session("EMPLID") = DDLEmployees_Goal.SelectedValue.ToString
                lblEMPLID.Text = DDLEmployees_Goal.SelectedValue.ToString
                'Response.Write("EMPLID" & DDLEmployees_Goal.SelectedValue.ToString)
                SQL = "select Max(Window_Batch)Window_Batch from appraisal_futuregoals_master_tbl where emplid = " & DDLMyEmpl_Goal.SelectedValue.ToString & " and Perf_Year=" & Year_MyEmpl_Goal.SelectedValue
                'Response.Write(SQL) : Response.End()
                DT = LocalClass.ExecuteSQLDataSet(SQL)

                SQL1 = "Update appraisal_futuregoals_master_tbl Set Window_Batch=" & CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                SQL1 &= " where emplid = " & DDLMyEmpl_Goal.SelectedValue.ToString & " and Perf_Year=" & Year_MyEmpl_Goal.SelectedValue
                'Response.Write(SQL1)
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                LocalClass.CloseSQLServerConnection()
            End If
        End If

    End Sub
    Protected Sub LoadYear_MyEmployee_Goal()
        SQL = "Select Perf_Year from(select * from("
        SQL &= " select distinct Perf_Year,'01/20/'+convert(char(4),Perf_Year)Today,Getdate()TodaySQL from Guild_Appraisal_FutureGoal_Master_TBL A, ps_employees B "
        SQL &= " where a.emplid=b.emplid and (SUP_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") UNION "
        SQL &= " select distinct Perf_Year,'01/20/'+convert(char(4),Perf_Year-1)Today,Getdate()TodaySQL from Appraisal_FutureGoals_Master_TBL A, ps_employees B "
        SQL &= " where a.emplid=b.emplid and (Mgt_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") )A where today < TodaySQL)B order by Perf_Year desc"
        'Response.Write(SQL) ': Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        Year_MyEmpl_Goal.Items.Clear()
        Year_MyEmpl_Goal.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            Year_MyEmpl_Goal.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

        DDLMyEmpl_Goal.Items.Add(New ListItem("  Choose Employee  ", "0"))

    End Sub
    Protected Sub LoadMyEmployees_Goal()
        Dim i As Integer
        'Response.Write("4)-Open " & DDLYears.SelectedValue.ToString & " Year" & Year(Now)) : Response.End()
        If Len(Year_MyEmpl_Goal.SelectedValue.ToString) = 0 Then

        Else
            DDLMyEmpl_Goal.Items.Clear()
            SQL = "select * from(select * from("
            SQL &= " select a.emplid,Name,Perf_Year,sup_emplid SUPID,process_flag,'01/20/'+convert(char(4),Perf_Year)Today,Getdate()TodaySQL from GUILD_Appraisal_FUTUREGOAL_MASTER_tbl A,ps_employees B "
            SQL &= " where a.emplid=b.emplid and process_flag=5 and (SUP_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") UNION "
            SQL &= " select a.emplid,Name,Perf_Year,MGT_EMPLID SUPID,process_flag,'01/20/'+convert(char(4),Perf_Year-1)Today,Getdate()TodaySQL from Appraisal_FutureGoals_Master_tbl A,ps_employees B "
            SQL &= " where a.emplid=b.emplid and  (MGT_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") )A where today<TodaySQL and Perf_Year=" & Year_MyEmpl_Goal.SelectedValue & ")B order by name"

            'Response.Write(SQL) ': Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            DDLMyEmpl_Goal.Items.Clear()
            DDLMyEmpl_Goal.Items.Add(New ListItem("  Choose Employee  ", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLMyEmpl_Goal.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If

    End Sub
    Protected Sub Year_Change_MyEmployee_Goal(sender As Object, e As EventArgs) Handles Year_MyEmpl_Goal.SelectedIndexChanged
        Session("YEAR") = Year_MyEmpl_Goal.SelectedValue.ToString

        If Year_MyEmpl_Goal.SelectedValue.ToString > 0 Then
            DDLMyEmpl_Goal.Enabled = True
            LoadMyEmployees_Goal()
        Else
            DDLMyEmpl_Goal.Items.Clear()
            DDLMyEmpl_Goal.Items.Add(New ListItem(" Choose Employee ", "0"))
            DDLMyEmpl_Goal.Enabled = False
        End If
    End Sub
    Protected Sub Submit_MyEmpl_Goal_Click(sender As Object, e As EventArgs) Handles Submit_MyEmpl_Goal.Click

        Dim PCODE
        Dim x, y, z As String

        PCODE = System.Configuration.ConfigurationManager.AppSettings("ApplKey").ToString()

        x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(DDLMyEmpl_Goal.SelectedValue.ToString, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        y = Year_MyEmpl_Goal.SelectedValue.ToString
        z = Session("Window_Batch")


        If Year_MyEmpl_Goal.SelectedValue.ToString > 0 Then
            SQL = "select Max(Perf_Year)Max_Year,convert(char(10),GetDate(),101)Date_Now from Appraisal_FutureGoals_master_tbl where emplid=" & DDLMyEmpl_Goal.SelectedValue & " "
            DT = LocalClass.ExecuteSQLDataSet(SQL)

            If Year_MyEmpl_Goal.SelectedValue.ToString = 2017 Or Year_MyEmpl_Goal.SelectedValue.ToString = 2018 Then
                'Response.Write("1 " & Today & "<>" & DateTime.Now.Year.ToString()) : Response.End()
                Session("YEAR") = y
                Response.Write("<script>window.open('Staff/FutureGoals/FutureGoal_print.aspx?Token=" + y + x + "','_blank' );</script>")
            ElseIf Year_MyEmpl_Goal.SelectedValue.ToString = 2019 Then ' 2019
                'Response.Write("2 " & Today): Response.End()
                Session("YEAR") = y
                Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                Response.Write("<script>window.open('Staff/FutureGoals/FutureGoals_Print.aspx?Token=" + y + x + z + "','_blank' );</script>")

            Else
                Session("YEAR") = y
                Session("MGT_EMPLID") = lblMGT_EMPLID.Text
                Response.Write("<script>window.open('Staff/FutureGoals/myEmpGoals.aspx?Token=" + y + x + z + "','_blank' );</script>")

            End If

        Else
            If Year_MyEmpl_Goal.SelectedValue.ToString = 0 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee from list and click submit button');</script>")
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose employee from list and click submit button');</script>")
            End If
        End If

        Year_MyEmpl_Goal.SelectedIndex = 0
        DDLMyEmpl_Goal.Enabled = False
        DDLMyEmpl_Goal.SelectedIndex = 0
    End Sub

    Protected Sub Load_Term_Employee()
        'Response.Write(lblMGT_EMPLID.Text) : Response.End()
        If lblMGT_EMPLID.Text = 6785 Or lblMGT_EMPLID.Text = 6250 Or lblMGT_EMPLID.Text = 6088 Then
            SQL = "select A.emplid,last+','+first Name from(select distinct emplid from guild_appraisal_master_tbl UNION"
            SQL &= " select distinct emplid from me_appraisal_master_tbl UNION select distinct emplid from appraisal_master_tbl)A "
            SQL &= " JOIN id_tbl B ON a.emplid=B.emplid where status='I' order by Name"
            'Response.Write(SQL) : Response.End()
        Else
            SQL = "select A.emplid,last+','+first Name from(select distinct emplid from guild_appraisal_master_tbl UNION"
            SQL &= " select distinct emplid from me_appraisal_master_tbl UNION select distinct emplid from appraisal_master_tbl)A "
            SQL &= " JOIN id_tbl B ON a.emplid=B.emplid where status='I'  and deptid1 not in (9009120) order by Name"
            'Response.Write(SQL) : Response.End()
        End If

        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDEmployees_Term.Items.Clear()

        DDEmployees_Term.Items.Add(New ListItem("Choose Employee ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDEmployees_Term.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
        Next
        'End If
        LocalClass.CloseSQLServerConnection()

        DDLYears_Term_Print.Items.Add(New ListItem("Choose Year", "0"))
    End Sub
    Protected Sub LoadYear_Term()
        SQL = " select distinct Perf_Year from ME_Appraisal_MASTER_tbl where emplid=" & DDEmployees_Term.SelectedValue & " UNION "
        SQL &= " select distinct Perf_Year from GUILD_Appraisal_MASTER_tbl  where emplid=" & DDEmployees_Term.SelectedValue & " UNION "
        SQL &= " select distinct Perf_Year from Appraisal_MASTER_tbl  where emplid=" & DDEmployees_Term.SelectedValue & " order by Perf_Year desc"
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        DDLYears_Term_Print.Items.Clear()

        DDLYears_Term_Print.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_Term_Print.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub Year_Change_Term_Print(sender As Object, e As EventArgs) Handles DDLYears_Term_Print.SelectedIndexChanged

    End Sub
    Protected Sub DropDownList_Update_Term(sender As Object, e As EventArgs) Handles DDEmployees_Term.SelectedIndexChanged
        DDLYears_Term_Print.Enabled = True
        LoadYear_Term()
    End Sub

    Protected Sub BulkPrint_Term_Click(sender As Object, e As EventArgs) Handles BulkPrint_Term.Click
        'Response.Write(DDEmployees_Term.SelectedValue & "<br>" & DDLYears_Term_Print.SelectedValue)
        x = DDEmployees_Term.SelectedValue
        y = DDLYears_Term_Print.SelectedValue

        SQL = "select * from(select count(*)ME_Apr  from ME_Appraisal_MASTER_tbl where emplid=" & DDEmployees_Term.SelectedValue & " and Perf_Year=" & DDLYears_Term_Print.SelectedValue & ")A, "
        SQL &= " (select count(*)Guild_Apr from GUILD_Appraisal_MASTER_tbl  where emplid=" & DDEmployees_Term.SelectedValue & " and Perf_Year=" & DDLYears_Term_Print.SelectedValue & ")B, "
        SQL &= " (select count(*)All_Apr from Appraisal_MASTER_tbl  where emplid=" & DDEmployees_Term.SelectedValue & " and Perf_Year=" & DDLYears_Term_Print.SelectedValue & ")C "
        'Response.Write(SQL) : Response.End()

        DT = LocalClass.ExecuteSQLDataSet(SQL)
        If DT.Rows(0)("ME_Apr").ToString > 0 Then
            Session("YEAR") = y
            Response.Write("<script>window.open('/Appraisal/Staff/HR/Manager_Appraisal.aspx?Token=" + x + "','_blank' );</script>") '---->/Appraisal

        ElseIf DT.Rows(0)("Guild_Apr").ToString > 0 Then
            Session("YEAR") = y
            Response.Write("<script>window.open('/Appraisal/Staff/HR/Guild_Appraisal.aspx?Token=" + x + "','_blank' );</script>") '---->/Appraisal
        ElseIf DT.Rows(0)("All_Apr").ToString > 0 Then
            Session("Year_Appr") = y
            Session("YEAR") = y
            Response.Write("<script>window.open('/Appraisal/Staff/HR/Appraisal_Reports_Redirect.aspx?Token=APP_ALL" + x + "','_blank' );</script>") '---->/Appraisal
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Redirect", "<script language='JavaScript'> alert('Please choose year from list and click submit button');</script>")
        End If


        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub LoadYear_MidPoint()
        SQL = "select * from(select distinct Perf_Year from Appraisal_MidPoint_MASTER_tbl where Len(TimeStamp)>5 and emplid in (select emplid from ps_employees where class='U') /*UNION "
        SQL &= " select distinct Perf_Year from Guild_MidPoint_MASTER_tbl  where Len(TimeStamp)>5 and emplid in (select emplid from ps_employees where class='U')*/)A order by Perf_Year desc"
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYear_MidPoint.Items.Clear()

        DDLYear_MidPoint.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYear_MidPoint.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

        DDLEmployees_Midpoint.Items.Add(New ListItem("Choose Employee's Type", "0"))
    End Sub
    Protected Sub LoadEmployees_MidPoint()
        Dim i As Integer

        If Len(DDLYears_Report.SelectedValue.ToString) = 0 Then

        Else
            DDLEmployees_Midpoint.Items.Clear()
            SQL = " select distinct 1 YEAR_ID,'Guild' SAP"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            DDLEmployees_Midpoint.Items.Clear()
            DDLEmployees_Midpoint.Items.Add(New ListItem("Choose Employee's Type", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLEmployees_Midpoint.Items.Add(New ListItem(DT.Rows(i)("SAP").ToString, DT.Rows(i)("YEAR_ID").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub Year_MidPoint(sender As Object, e As EventArgs) Handles DDLYear_MidPoint.SelectedIndexChanged
        Session("YEAR") = DDLYear_MidPoint.SelectedValue.ToString

        If DDLYear_MidPoint.SelectedValue.ToString > 0 Then
            DDLEmployees_Midpoint.Enabled = True
            LoadEmployees_MidPoint()
        Else
            DDLEmployees_Midpoint.Items.Clear()
            DDLEmployees_Midpoint.Items.Add(New ListItem("Choose Employee's Type", "0"))
            DDLEmployees_Midpoint.Enabled = False
        End If
    End Sub
    Protected Sub Employees_MidPoint(sender As Object, e As EventArgs) Handles DDLEmployees_Midpoint.SelectedIndexChanged

    End Sub
    Protected Sub Submit_MidPoint_Click(sender As Object, e As EventArgs) Handles Submit_MidPoint.Click
        Dim x, y As String

        x = DDLEmployees_Midpoint.SelectedValue.ToString
        y = DDLYear_MidPoint.SelectedValue.ToString

        If DDLEmployees_Midpoint.SelectedValue.ToString > 0 Then
            Response.Write("<script>window.open('Staff/HR/MidPoint_Reports.aspx?Token=" + x + y + "','_blank' );</script>")
        Else

            If DDLYears_Report.SelectedValue.ToString = 0 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee`s Type from list and click submit button');</script>")
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose Employee`s Type from list and click submit button');</script>")
            End If
        End If

        DDLYear_MidPoint.SelectedIndex = 0
        DDLEmployees_Midpoint.Enabled = False
        DDLEmployees_Midpoint.SelectedIndex = 0

    End Sub

    Protected Sub LoadYear_Goals()
        SQL = "select distinct Perf_Year from Appraisal_FutureGoals_Master_tbl order by Perf_Year desc"
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYearsGoals_Reports.Items.Clear()

        DDLYearsGoals_Reports.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYearsGoals_Reports.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

        DDLGoals_Reports.Items.Add(New ListItem("Choose Employee's Type", "0"))
    End Sub

    Protected Sub LoadALL_Employees_Goals()
        Dim i As Integer

        If Len(DDLYearsGoals_Reports.SelectedValue.ToString) = 0 Then

        Else
            DDLGoals_Reports.Items.Clear()
            SQL = "select * from(select 1 ID,'All Employees'SAP UNION select 2 ID,'Manager/Exempt'SAP UNION select 3 ID,'Guild' SAP)A order by ID"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            DDLGoals_Reports.Items.Clear()
            DDLGoals_Reports.Items.Add(New ListItem("Choose Employee's Type", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLGoals_Reports.Items.Add(New ListItem(DT.Rows(i)("SAP").ToString, DT.Rows(i)("ID").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub

    Protected Sub Year_Goal_Report(sender As Object, e As EventArgs) Handles DDLYearsGoals_Reports.SelectedIndexChanged
        Session("YEAR") = DDLYearsGoals_Reports.SelectedValue.ToString

        If DDLYearsGoals_Reports.SelectedValue.ToString > 0 Then
            DDLGoals_Reports.Enabled = True
            LoadALL_Employees_Goals()
        Else
            DDLGoals_Reports.Items.Clear()
            DDLGoals_Reports.Items.Add(New ListItem("Choose Employee", "0"))
            DDLGoals_Reports.Enabled = False
        End If
    End Sub

    Protected Sub DropDownList_Goal_Report(sender As Object, e As EventArgs) Handles DDLGoals_Reports.SelectedIndexChanged

    End Sub

    Protected Sub Submit_Goals_Report_Click(sender As Object, e As EventArgs) Handles Submit_Goals_Report.Click
        Dim x, y As String

        x = DDLGoals_Reports.SelectedValue.ToString
        y = DDLYearsGoals_Reports.SelectedValue.ToString

        If DDLGoals_Reports.SelectedValue.ToString > 0 Then
            'Response.Redirect("Staff/HR/Goals_Reports.aspx?Token=" & x & y)
            Response.Write("<script>window.open('Staff/HR/Goals_Reports.aspx?Token=" + x + y + "','_blank' );</script>")
        Else

            If DDLYearsGoals_Reports.SelectedValue.ToString = 0 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee`s Type from list and click submit button');</script>")
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose Employee`s Type from list and click submit button');</script>")
            End If
        End If

        DDLGoals_Reports.SelectedIndex = 0
        DDLGoals_Reports.Enabled = False
        DDLYearsGoals_Reports.SelectedIndex = 0
    End Sub

End Class