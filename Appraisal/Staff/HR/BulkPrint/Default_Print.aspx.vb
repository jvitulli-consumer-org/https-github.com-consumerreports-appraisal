Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Imports System.Drawing




Public Class Default_Print
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim Msg, Msg1, Msg2, Msg3, Msg4 As String
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView

    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer

    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("NETID") = "" Then Response.Redirect("default.aspx")
        lblYEAR_PRINT.Text = Trim(Request.QueryString("Token").ToString)
        LblYear.Text = Trim(Request.QueryString("Token").ToString)

        SQL1 = "select emplid from id_tbl where netid='" & Trim(Session("NETID")) & "' "
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LblMGT_EMPLID.Text = DT1.Rows(0)("emplid").ToString

        If IsPostBack Then
            'Response.Write("1)-Last<br>")
        Else
            LoadGeneralist_GUILD()
            LoadManager_GLD()
            LoadDepartment_GUILD()
            LoadAll_GUILD()
            LoadGeneralist_MGT()
            LoadDepartment_MGT()
            LoadManager_MGT()
            LoadAll_MGT()
            LoadMidPoint_GLD()
            LoadGoals_GLD()
            Load_Terminted()
            Load_Individual()
        End If

        ShowButtonCursor()


        If LblYear.Text = 2014 Then
            Ln.Visible = False : Ln1.Visible = False : Ln2.Visible = False : Ln3.Visible = False : Ln4.Visible = False : Ln5.Visible = False : Ln6.Visible = False : Ln7.Visible = False : Ln8.Visible = False : Ln9.Visible = False : Ln10.Visible = False
            Ln11.Visible = False : Ln12.Visible = False : Ln13.Visible = False : Ln14.Visible = False : Ln15.Visible = False : Ln16.Visible = False : Ln17.Visible = False : Ln18.Visible = False : Ln19.Visible = False : Ln20.Visible = False
        Else
            Ln.Visible = True : Ln1.Visible = True : Ln2.Visible = True : Ln3.Visible = True : Ln4.Visible = True : Ln5.Visible = True : Ln6.Visible = True : Ln7.Visible = True : Ln8.Visible = True : Ln9.Visible = True : Ln10.Visible = True
            Ln11.Visible = True : Ln12.Visible = True : Ln13.Visible = True : Ln14.Visible = True : Ln15.Visible = True : Ln16.Visible = True : Ln17.Visible = True : Ln18.Visible = True : Ln19.Visible = True : Ln20.Visible = True
        End If

        'SQL = "select Count(*)CNT_TERM from Appraisal_master_tbl A JOIN id_tbl B ON A.emplid=B.emplid where Status='I' and Perf_Year=" & LblYear.Text & ""
        'If LblMGT_EMPLID.Text = 6785 Or LblMGT_EMPLID.Text = 6250 Or LblMGT_EMPLID.Text = 6671 Then
        'Else
        'SQL &= " and deptid not in (9009120)"
        'End If
        'Response.Write(LblMGT_EMPLID.Text & "<br>" & SQL)
        'DT = LocalClass.ExecuteSQLDataSet(SQL)
        'If LblYear.Text > 2015 And CDbl(DT.Rows(0)("CNT_TERM").ToString) > 0 Then
        'Response.Write(DT.Rows(0)("CNT_TERM").ToString)
        'Panel_Terminated.Visible = True
        'Else
        'Panel_Terminated.Visible = False
        'End If

    End Sub

    Protected Sub ShowButtonCursor()
        Submit_Generalist.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Manager_GLD.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Department.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Report.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Generalist_MGT.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Manager_MGT.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Department_MGT.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Report_MGT.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_MidPoint.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Report_IND.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Goals.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Report_TER.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
    End Sub
    '--GUILD--
    Sub LoadGeneralist_GUILD()
        Generalist_Name.Items.Clear()
        If Trim(Request.QueryString("Token").ToString) >= 2016 Then
            SQL = "select distinct hr_emplid emplid,(select First+' '+Last from id_tbl where emplid=A.HR_EMPLID)Name from appraisal_master_tbl A "
            SQL &= "where SAP=14  and perf_year=" & LblYear.Text & " order by name"
        Else
            SQL = "select distinct HR_Generalist emplid,(select First_Name+' '+Last_Name from hr_pds_data_tbl where emplid=A.HR_Generalist)Name from hr_pds_data_tbl A where sal_admin_plan=14 order by name"
        End If
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Generalist_Name.Items.Add(New ListItem("  Choose Generalist  ", "0"))
        For i = 0 To DT.Rows.Count - 1
            Generalist_Name.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
        Next
        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub Submit_Generalist_Click(sender As Object, e As EventArgs) Handles Submit_Generalist.Click
        'Response.Write(Generalist_Name.SelectedValue)
        If Generalist_Name.SelectedValue.ToString > 0 Then
            Session("YEAR") = LblYear.Text
            Response.Redirect("Print_Appraisal.aspx?Token=GLD" & Generalist_Name.SelectedValue)
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose generalist from list and click submit button');</script>")
        End If
    End Sub
    Protected Sub DropDownList_Change(sender As Object, e As EventArgs) Handles Generalist_Name.SelectedIndexChanged
        Manager_Name_GLD.SelectedValue = 0
        Department_Name.SelectedValue = 0
        'All.SelectedValue = 0
        Generalist_Name_MGT.SelectedValue = 0
        Manager_Name_MGT.SelectedValue = 0
        Department_Name_MGT.SelectedValue = 0
        'All_MGT.SelectedValue = 0
    End Sub

    Protected Sub LoadManager_GLD()
        Manager_Name_GLD.Items.Clear()
        If Trim(Request.QueryString("Token").ToString) >= 2016 Then
            SQL = "select distinct MGT_emplid sup_emplid,(select Last+','+First from id_tbl where emplid=A.MGT_EMPLID)Name from appraisal_master_tbl A "
            SQL &= "where SAP=14 and perf_year=" & LblYear.Text & " order by name"
        Else
            SQL = "select * from (select distinct sup_emplid,(select Last+','+First from id_tbl where emplid=sup_emplid)Name from guild_appraisal_master_tbl "
            SQL = SQL & " where Len(Date_Guild_Reviewed)>4 and Perf_Year=2015 and New_employee=0)A order by name"

        End If
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Manager_Name_GLD.Items.Add(New ListItem("  Choose Manager  ", "0"))
        For i = 0 To DT.Rows.Count - 1
            Manager_Name_GLD.Items.Add(New ListItem(DT.Rows(i)("Name").ToString, DT.Rows(i)("sup_emplid").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub DropDownList_Manager_GLD(sender As Object, e As EventArgs) Handles Manager_Name_GLD.SelectedIndexChanged
        Generalist_Name_MGT.SelectedValue = 0
        Manager_Name_MGT.SelectedValue = 0
        Department_Name_MGT.SelectedValue = 0
        'All_MGT.SelectedValue = 0

        Generalist_Name.SelectedValue = 0
        'Manager_Name_GLD.SelectedValue = 0
        Department_Name.SelectedValue = 0
        'All.SelectedValue = 0
        Mid_Point.SelectedValue = 0
    End Sub
    Protected Sub Submit_Manager_GLD_Click(sender As Object, e As EventArgs) Handles Submit_Manager_GLD.Click
        If Manager_Name_GLD.SelectedValue.ToString > 0 Then
            'Response.Write(Manager_Name_GLD.SelectedValue) : Response.End()
            Response.Redirect("Print_Appraisal.aspx?Token=GLDMANAGER" & Manager_Name_GLD.SelectedValue)
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose all appraisals from list and click submit button');</script>")
        End If
    End Sub

    Protected Sub LoadDepartment_GUILD()
        If Trim(Request.QueryString("Token").ToString) >= 2016 Then
            SQL = "select distinct deptid,department deptname from appraisal_master_tbl A where sap=14 and perf_year=" & LblYear.Text & " order by deptname"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
        Else
            Department_Name.Items.Clear()
            SQL = "select distinct deptid,deptname from hr_pds_data_tbl A where sal_admin_plan=14 order by deptname"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
        End If
        Department_Name.Items.Add(New ListItem("  Choose Department  ", "0"))
        For i = 0 To DT.Rows.Count - 1
            Department_Name.Items.Add(New ListItem(DT.Rows(i)("deptname").ToString, DT.Rows(i)("deptid").ToString))
        Next
        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub DropDownList_Change_Waiting(sender As Object, e As EventArgs) Handles Department_Name.SelectedIndexChanged
        Generalist_Name.SelectedValue = 0
        Manager_Name_GLD.SelectedValue = 0
        'All.SelectedValue = 0
        Generalist_Name_MGT.SelectedValue = 0
        Manager_Name_MGT.SelectedValue = 0
        Department_Name_MGT.SelectedValue = 0
        Mid_Point.SelectedValue = 0
        'All_MGT.SelectedValue = 0
    End Sub
    Protected Sub Submit_Department_Click(sender As Object, e As EventArgs) Handles Submit_Department.Click
        'Response.Write(Department_Name.SelectedValue)
        If Department_Name.SelectedValue.ToString > 0 Then

            Response.Redirect("Print_Appraisal.aspx?Token=GLD" & Department_Name.SelectedValue)
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose department from list and click submit button');</script>")
        End If

    End Sub

    Protected Sub LoadAll_GUILD()
        'All.Items.Add(New ListItem("  Choose All Appraisals ", "0"))
        All.Items.Add(New ListItem("  All Appraisals ", "1"))
    End Sub
    Protected Sub DropDownList_Reports(sender As Object, e As EventArgs) Handles All.SelectedIndexChanged
        Department_Name.SelectedValue = 0
        Manager_Name_GLD.SelectedValue = 0
        Generalist_Name.SelectedValue = 0
        Generalist_Name_MGT.SelectedValue = 0
        Manager_Name_MGT.SelectedValue = 0
        Department_Name_MGT.SelectedValue = 0
        'All_MGT.SelectedValue = 0
        Mid_Point.SelectedValue = 0
    End Sub
    Protected Sub Submit_Report_Click(sender As Object, e As EventArgs) Handles Submit_Report.Click
        If All.SelectedValue.ToString > 0 Then
            Response.Redirect("Print_Appraisal.aspx?Token=GLD")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose all appraisals from list and click submit button');</script>")
        End If
    End Sub

    '---MANAGERS---    
    Protected Sub LoadGeneralist_MGT()

        If LblMGT_EMPLID.Text = 6785 Or LblMGT_EMPLID.Text = 6250 Or LblMGT_EMPLID.Text = 6671 Then
            If Trim(Request.QueryString("Token").ToString) >= 2016 Then
                SQL = "select distinct hr_emplid emplid,(select First+' '+Last from id_tbl where emplid=A.HR_EMPLID)Name from appraisal_master_tbl A "
                SQL &= "where SAP<>14  and perf_year=" & LblYear.Text & " order by name"
                'Response.Write("1   " & SQL)
            Else
                SQL = "select distinct HR_Generalist emplid,(select First_Name+' '+Last_Name from hr_pds_data_tbl where emplid=A.HR_Generalist)Name "
                SQL &= " from hr_pds_data_tbl A where sal_admin_plan<>14 order by name"
            End If
        Else
            If Trim(Request.QueryString("Token").ToString) >= 2016 Then
                SQL = "select distinct hr_emplid emplid,(select First+' '+Last from id_tbl where emplid=A.HR_EMPLID)Name from appraisal_master_tbl A "
                SQL &= "where SAP<>14 and hr_emplid not in (6785) and perf_year=" & LblYear.Text & " order by name"
                'Response.Write("2   " & SQL)
            Else
                SQL = "select distinct HR_Generalist emplid,(select First_Name+' '+Last_Name from hr_pds_data_tbl where emplid=A.HR_Generalist)Name "
                SQL &= " from hr_pds_data_tbl A where sal_admin_plan<>14 and HR_Generalist not in (6785) order by name"
            End If
        End If
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Generalist_Name_MGT.Items.Clear()



        Generalist_Name_MGT.Items.Add(New ListItem("  Choose Generalist  ", "0"))
        For i = 0 To DT.Rows.Count - 1
            Generalist_Name_MGT.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
        Next
        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub DropDownList_Generalist_MGT(sender As Object, e As EventArgs) Handles Generalist_Name_MGT.SelectedIndexChanged
        Department_Name_MGT.SelectedValue = 0
        Manager_Name_MGT.SelectedValue = 0
        'All_MGT.SelectedValue = 0
        Generalist_Name.SelectedValue = 0
        Manager_Name_GLD.SelectedValue = 0
        Department_Name.SelectedValue = 0
        'All.SelectedValue = 0
        Mid_Point.SelectedValue = 0
    End Sub


    Protected Sub Submit_Generalist_MGT_Click(sender As Object, e As EventArgs) Handles Submit_Generalist_MGT.Click

        If Generalist_Name_MGT.SelectedValue.ToString > 0 Then
            Response.Redirect("Print_Appraisal.aspx?Token=MGT" & Generalist_Name_MGT.SelectedValue)
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose generalist from list and click submit button');</script>")
        End If
    End Sub

    Protected Sub LoadManager_MGT()
        Manager_Name_MGT.Items.Clear()

        If LblMGT_EMPLID.Text = 6785 Or LblMGT_EMPLID.Text = 6250 Or LblMGT_EMPLID.Text = 6671 Then
            If Trim(Request.QueryString("Token").ToString) >= 2016 Then
                SQL = "select distinct MGT_emplid sup_emplid,(select First+' '+Last from id_tbl where emplid=A.MGT_EMPLID)Name from appraisal_master_tbl A "
                SQL &= " where SAP<>14 and perf_year=" & LblYear.Text & " order by name"
            Else
                SQL = "select * from(select distinct sup_emplid,(select Last+','+First from id_tbl where emplid=sup_emplid)Name from me_appraisal_master_tbl "
                SQL = SQL & " where Len(Date_Employee_Esign)>4 and New_employee=0)A order by name"
            End If
        Else
            If Trim(Request.QueryString("Token").ToString) >= 2016 Then
                SQL = "select distinct MGT_emplid sup_emplid,(select First+' '+Last from id_tbl where emplid=A.MGT_EMPLID)Name from appraisal_master_tbl A "
                SQL &= " where SAP<>14 and perf_year=" & LblYear.Text & " and MGT_emplid not in (select distinct supervisor_id from ps_employees where deptid=9009120)  order by name"
            Else
                SQL = "select * from(select distinct sup_emplid,(select Last+','+First from id_tbl where emplid=sup_emplid)Name from me_appraisal_master_tbl "
                SQL = SQL & " where Len(Date_Employee_Esign)>4 and New_employee=0)A where sup_emplid not in (select distinct supervisor_id from ps_employees where deptid=9009120) order by name"
            End If
        End If
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Manager_Name_MGT.Items.Add(New ListItem("  Choose Manager  ", "0"))
        For i = 0 To DT.Rows.Count - 1
            Manager_Name_MGT.Items.Add(New ListItem(DT.Rows(i)("Name").ToString, DT.Rows(i)("sup_emplid").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub DropDownList_Manager_MGT(sender As Object, e As EventArgs) Handles Manager_Name_MGT.SelectedIndexChanged
        Generalist_Name_MGT.SelectedValue = 0
        'Manager_Name_MGT.SelectedValue = 0
        Department_Name_MGT.SelectedValue = 0
        'All_MGT.SelectedValue = 0
        Generalist_Name.SelectedValue = 0
        Manager_Name_GLD.SelectedValue = 0
        Department_Name.SelectedValue = 0
        'All.SelectedValue = 0
        Mid_Point.SelectedValue = 0
    End Sub
    Protected Sub Submit_Manager_MGT_Click(sender As Object, e As EventArgs) Handles Submit_Manager_MGT.Click
        If Manager_Name_MGT.SelectedValue.ToString > 0 Then
            Response.Redirect("Print_Appraisal.aspx?Token=MGTMANAGER" & Manager_Name_MGT.SelectedValue)
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose all appraisals from list and click submit button');</script>")
        End If
    End Sub

    Protected Sub LoadDepartment_MGT()
        Department_Name_MGT.Items.Clear()

        If Trim(Request.QueryString("Token").ToString) >= 2016 Then
            If LblMGT_EMPLID.Text = 6785 Or LblMGT_EMPLID.Text = 6250 Or LblMGT_EMPLID.Text = 6671 Then
                SQL = "select distinct deptid,department deptname from appraisal_master_tbl A where sap<>14 and perf_year=" & LblYear.Text & " order by deptname"
            Else
                SQL = "select distinct deptid,department deptname from appraisal_master_tbl A where sap<>14 and deptid not in (9009120)  and perf_year=" & LblYear.Text & " order by deptname"
            End If
        Else
            SQL = "select distinct deptid,deptname from hr_pds_data_tbl A where sal_admin_plan<>14 and deptid not in (9009120) order by deptname"
        End If

        DT = LocalClass.ExecuteSQLDataSet(SQL)
        Department_Name_MGT.Items.Add(New ListItem("  Choose Department  ", "0"))
        For i = 0 To DT.Rows.Count - 1
            Department_Name_MGT.Items.Add(New ListItem(DT.Rows(i)("deptname").ToString, DT.Rows(i)("deptid").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub DropDownList_Department_MGT(sender As Object, e As EventArgs) Handles Department_Name_MGT.SelectedIndexChanged
        Generalist_Name_MGT.SelectedValue = 0
        'All_MGT.SelectedValue = 0
        Department_Name.SelectedValue = 0
        Generalist_Name.SelectedValue = 0
        'All.SelectedValue = 0
        Manager_Name_MGT.SelectedValue = 0
        Manager_Name_GLD.SelectedValue = 0
        Mid_Point.SelectedValue = 0
    End Sub
    Protected Sub Submit_Department_MGT_Click(sender As Object, e As EventArgs) Handles Submit_Department_MGT.Click
        If Department_Name_MGT.SelectedValue.ToString > 0 Then
            Response.Redirect("Print_Appraisal.aspx?Token=MGT" & Department_Name_MGT.SelectedValue)
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose department from list and click submit button');</script>")
        End If
    End Sub

    Protected Sub LoadAll_MGT()
        'All_MGT.Items.Add(New ListItem("  Choose All Appraisals ", "0"))
        All_MGT.Items.Add(New ListItem("  All Appraisals ", "1"))
    End Sub
    Protected Sub DropDownList_Reports_MGT(sender As Object, e As EventArgs) Handles All_MGT.SelectedIndexChanged
        Generalist_Name_MGT.SelectedValue = 0
        Department_Name_MGT.SelectedValue = 0
        Department_Name.SelectedValue = 0
        Generalist_Name.SelectedValue = 0
        'All.SelectedValue = 0
        Manager_Name_MGT.SelectedValue = 0
        Manager_Name_GLD.SelectedValue = 0
        Mid_Point.SelectedValue = 0

    End Sub
    Protected Sub Submit_Report_MGT_Click(sender As Object, e As EventArgs) Handles Submit_Report_MGT.Click
        If All_MGT.SelectedValue.ToString > 0 Then
            Response.Redirect("Print_Appraisal.aspx?Token=MGT")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose all appraisals from list and click submit button');</script>")
        End If
    End Sub

    '--Mid-Point for Guild--
    Protected Sub LoadMidPoint_GLD()
        Mid_Point.Items.Clear()
        If Trim(Request.QueryString("Token").ToString) = 2016 Then
            SQL = "select distinct Perf_Year from Guild_MidPoint_MASTER_tbl"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            Mid_Point.Items.Add(New ListItem("  Choose Year  ", "0"))
            For i = 0 To DT.Rows.Count - 1
                Mid_Point.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
            Next
        ElseIf Trim(Request.QueryString("Token").ToString) > 2016 Then

        Else
            Panel_MidPoint_Print.Visible = False
        End If
        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub DropDownList_MidPoint(sender As Object, e As EventArgs) Handles Mid_Point.SelectedIndexChanged, Goals.SelectedIndexChanged
        Generalist_Name_MGT.SelectedValue = 0
        Department_Name_MGT.SelectedValue = 0
        Department_Name.SelectedValue = 0
        Generalist_Name.SelectedValue = 0
        'All.SelectedValue = 0
        Manager_Name_MGT.SelectedValue = 0
        Manager_Name_GLD.SelectedValue = 0
        'All_MGT.SelectedValue = 0
    End Sub
    Protected Sub Submit_MidPoint_Click(sender As Object, e As EventArgs) Handles Submit_MidPoint.Click

        If Mid_Point.SelectedValue.ToString > 0 Then
            Response.Redirect("Print_MidPoint.aspx?Token=" & Mid_Point.SelectedValue)
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose Year from list and click submit button');</script>")
        End If
    End Sub

    '--Goals--
    Protected Sub LoadGoals_GLD()

        Goals.Items.Clear()
        If Trim(Request.QueryString("Token").ToString) < 2016 Then
            SQL = "select distinct Perf_Year from Guild_Appraisal_FUTUREGOAL_MASTER_tbl"
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            Goals.Items.Add(New ListItem("  Choose Year  ", "0"))
            For i = 0 To DT.Rows.Count - 1
                Goals.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
            Next
        ElseIf Trim(Request.QueryString("Token").ToString) >= 2016 Then
            'Response.Write("goals for 2017 and up")
            SQL = "select distinct Perf_Year from Appraisal_futuregoals_master_tbl where Perf_Year= " & Trim(Request.QueryString("Token").ToString) + 1

            DT = LocalClass.ExecuteSQLDataSet(SQL)
            Goals.Items.Add(New ListItem("  Choose Year  ", "0"))
            For i = 0 To DT.Rows.Count - 1
                Goals.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
            Next

        End If
        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub DropDownList_Goals(sender As Object, e As EventArgs) Handles Goals.SelectedIndexChanged
        Generalist_Name_MGT.SelectedValue = 0
        Department_Name_MGT.SelectedValue = 0
        Department_Name.SelectedValue = 0
        Generalist_Name.SelectedValue = 0
        'All.SelectedValue = 0
        Manager_Name_MGT.SelectedValue = 0
        Manager_Name_GLD.SelectedValue = 0
        'All_MGT.SelectedValue = 0
    End Sub

    Protected Sub Submit_Goals_Click(sender As Object, e As EventArgs) Handles Submit_Goals.Click

        If Goals.SelectedValue.ToString > 0 Then

            If Trim(Request.QueryString("Token").ToString) < 2016 Then
                Response.Redirect("Print_Goals.aspx?Token=" & Goals.SelectedValue)
            Else
                Response.Redirect("Print_Goals.aspx?Token=" & Goals.SelectedValue)
            End If

        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose Year from list and click submit button');</script>")
        End If

    End Sub

    Protected Sub Load_Individual()
        'Response.Write(Session("NETID")) : Response.End()
        Individual.Items.Clear()

        If Trim(Request.QueryString("Token").ToString) >= 2016 Then
            If LblMGT_EMPLID.Text = 6785 Or LblMGT_EMPLID.Text = 6250 Or LblMGT_EMPLID.Text = 6671 Then
                SQL = "select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID )Name from appraisal_master_tbl A where emplid in (select emplid from ps_employees) and perf_year=" & LblYear.Text & " order by name"
            Else
                SQL = "select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID)Name from appraisal_master_tbl A where  emplid in (select emplid from ps_employees where deptid not in (9009120)) and perf_year=" & LblYear.Text & " order by name"
            End If
        Else
            If LblMGT_EMPLID.Text = 6785 Or LblMGT_EMPLID.Text = 6250 Or LblMGT_EMPLID.Text = 6671 Then
                SQL = "select * from(select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID)Name from Guild_Appraisal_MASTER_tbl A where Perf_Year=" & LblYear.Text & ""
                SQL &= " UNION select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID)Name  from ME_Appraisal_Master_TBL A where  emplid in (select emplid from ps_employees) and Perf_Year=" & LblYear.Text & ")B order by name"
            Else
                SQL = "select * from(select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID)Name from Guild_Appraisal_MASTER_tbl A where Perf_Year=" & LblYear.Text & ""
                SQL &= " UNION select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID)Name  from ME_Appraisal_Master_TBL A where Perf_Year=" & LblYear.Text & ")B where "
                SQL &= "emplid not in (select emplid from ps_employees where deptid1=9009120) order by name"
            End If

        End If

        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Individual.Items.Add(New ListItem("  Choose Name  ", "0"))
        For i = 0 To DT.Rows.Count - 1
            Individual.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub DropDownList_Reports_IND(sender As Object, e As EventArgs) Handles Individual.SelectedIndexChanged

    End Sub
    Protected Sub Submit_Report_IND_Click(sender As Object, e As EventArgs) Handles Submit_Report_IND.Click

        If Individual.SelectedValue.ToString > 0 Then

            If Trim(Request.QueryString("Token").ToString) >= 2016 Then
                'Response.Write("2016 and up Appraisal" & Individual.SelectedValue)
                Session("YEAR") = LblYear.Text
                Response.Redirect("Print_Appraisal.aspx?Token=IND" & Individual.SelectedValue)
            Else
                SQL = "select * from(select count(*)CNT_GLD from Guild_Appraisal_MASTER_tbl A where Perf_Year=" & LblYear.Text & " and emplid=" & Individual.SelectedValue.ToString & ")A,"
                SQL &= " (select count(*)CNT_MGT  from ME_Appraisal_Master_TBL A where Perf_Year=" & LblYear.Text & " and emplid=" & Individual.SelectedValue.ToString & ")B"
                DT = LocalClass.ExecuteSQLDataSet(SQL)
                'Response.Write(SQL)
                If DT.Rows(0)("CNT_GLD").ToString = 1 Then
                    'Response.Write("redirect to guild's appraisal")
                    Session("YEAR") = LblYear.Text
                    Response.Redirect("Print_Appraisal.aspx?Token=INDG" & Individual.SelectedValue)
                Else
                    'Response.Write("redirect to manager's appraisal")
                    Session("YEAR") = LblYear.Text
                    Response.Redirect("Print_Appraisal.aspx?Token=INDM" & Individual.SelectedValue)
                End If
            End If

        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose Name from list and click submit button');</script>")
        End If
        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub DropDownList_Reports_TER(sender As Object, e As EventArgs) Handles Terminated.SelectedIndexChanged
    
    End Sub


    Protected Sub Load_Terminted()
        Terminated.Items.Add(New ListItem(" Choose Terminated ", "0"))
        Terminated.Items.Add(New ListItem(" All Terminated ", "1"))

        'Response.Write(Session("NETID")) : Response.End()
        'Terminated.Items.Clear()
        'If Trim(Request.QueryString("Token").ToString) >= 2016 Then
        'If LblMGT_EMPLID.Text = 6785 Or LblMGT_EMPLID.Text = 6250 Or LblMGT_EMPLID.Text = 6671 Then
        'SQL = "select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID)Name from appraisal_master_tbl A where perf_year=" & LblYear.Text & " "
        'SQL &= " and emplid in (select emplid from id_tbl where status='I' and Year(termination_date)>=" & LblYear.Text & ") order by name"
        'Else
        'SQL = "select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID)Name from appraisal_master_tbl A where deptid not in (9009120) and perf_year=" & LblYear.Text & " "
        'SQL &= " and  emplid in (select emplid from id_tbl where status='I' and Year(termination_date)>=" & LblYear.Text & ") order by name"
        'End If
        'Else
        'SQL = "select * from(select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID)Name from Guild_Appraisal_MASTER_tbl A where Perf_Year=" & LblYear.Text & ""
        'SQL &= " UNION select distinct emplid,(select Last+','+First from id_tbl where emplid=A.EMPLID)Name  from ME_Appraisal_Master_TBL A where Perf_Year=" & LblYear.Text & ")B "
        'SQL &= " where emplid in (select emplid from id_tbl where status='I' and Year(termination_date)>=" & LblYear.Text & ") order by name"
        'End If
        'Response.Write(SQL) : Response.End()
        'DT = LocalClass.ExecuteSQLDataSet(SQL)
        'Terminated.Items.Add(New ListItem("  Choose Terminated  ", "0"))
        'Terminated.Items.Add(New ListItem("All Terminated", "1"))
        'For i = 0 To DT.Rows.Count - 1
        'Terminated.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
        'Next
        'LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub Submit_Report_TER_Click(sender As Object, e As EventArgs) Handles Submit_Report_TER.Click
        If Terminated.SelectedValue.ToString > 0 Then
            Response.Redirect("Print_Appraisal.aspx?Token=TER")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose all terminated from list and click submit button');</script>")
        End If

        'If Terminated.SelectedValue.ToString > 0 Then
        'If Trim(Request.QueryString("Token").ToString) >= 2016 Then
        'Response.Write("2016 and up Appraisal" & Terminated.SelectedValue)
        'Session("YEAR") = LblYear.Text
        'Response.Redirect("Print_Appraisal.aspx?Token=IND" & Terminated.SelectedValue)
        'Else
        'SQL = "select * from(select count(*)CNT_GLD from Guild_Appraisal_MASTER_tbl A where Perf_Year=" & LblYear.Text & " and emplid=" & Terminated.SelectedValue.ToString & ")A,"
        'SQL &= " (select count(*)CNT_MGT  from ME_Appraisal_Master_TBL A where Perf_Year=" & LblYear.Text & " and emplid=" & Terminated.SelectedValue.ToString & ")B"
        'DT = LocalClass.ExecuteSQLDataSet(SQL)
        'Response.Write(SQL)
        'If DT.Rows(0)("CNT_GLD").ToString = 1 Then
        'Response.Write("redirect to guild's appraisal")
        'Session("YEAR") = LblYear.Text
        'Response.Redirect("Print_Appraisal.aspx?Token=INDG" & Terminated.SelectedValue)
        'Else
        'Response.Write("redirect to manager's appraisal")
        'Session("YEAR") = LblYear.Text
        'Response.Redirect("Print_Appraisal.aspx?Token=INDM" & Terminated.SelectedValue)
        'End If
        'End If
        'Else
        'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose Name from list and click submit button');</script>")
        'End If
        'LocalClass.CloseSQLServerConnection()
    End Sub
End Class