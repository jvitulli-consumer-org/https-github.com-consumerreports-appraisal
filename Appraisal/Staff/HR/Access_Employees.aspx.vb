Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Public Class Access_Employees
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, x, z, ReturnValue As String
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

        lblLogin.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), _
               "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")

        lblDepartment.Text = Session("DEPARTMENT")
        'Response.Write(Session("YEAR") & "<br>" & Session("Deptid") & "<br>" & lblLogin.Text) : Response.End()
        Load_Employees()

    End Sub

    Protected Sub Load_Employees()
        'SqlDataSource1.SelectCommand = "select *,(case when Len(Term)>4 then Name_Adjust+'<font color=red size=2px> term on '+ Term +'</font>'  else Name_Adjust end) Name_Adjust_Term from"
        'SqlDataSource1.SelectCommand &= " (select *,(Employee_Name+' <font color=red size=2px>'+'( '+convert(char(2),Records_Count)+')</font>')Name_Adjust from "
        'SqlDataSource1.SelectCommand &= " (select EMPLID,(select Last+','+First from id_tbl where emplid=a.emplid)Employee_Name,MGT_EMPLID,"
        'SqlDataSource1.SelectCommand &= " (select Last+','+First from id_tbl where emplid=a.MGT_EMPLID)Manager_Name,UP_MGT_EMPLID,(select Last+','+First"
        'SqlDataSource1.SelectCommand &= " from id_tbl where emplid=a.UP_MGT_EMPLID)UP_Manager_Name,HR_EMPLID,(select Last+','+First from id_tbl where "
        'SqlDataSource1.SelectCommand &= " emplid=a.HR_EMPLID)HR_Name,JOBTITLE,Deptid,Department,(select Max(DateTime) from Appraisal_MasterHistory_tbl where "
        'SqlDataSource1.SelectCommand &= " emplid in (select distinct emplid from Appraisal_MasterHistory_tbl where login_emplid>0 and emplid=a.emplid and perf_year=" & Session("YEAR") & ") )Updated,"
        'SqlDataSource1.SelectCommand &= " (select count(emplid)from Appraisal_MasterHistory_tbl where emplid=a.emplid and login_emplid>0 and Perf_Year=" & Session("YEAR") & ")Records_Count,"
        'SqlDataSource1.SelectCommand &= " (select convert(char(10),termination_date,101) from id_tbl where emplid=a.EMPLID)Term"
        'SqlDataSource1.SelectCommand &= " from Appraisal_master_tbl A where Perf_Year=" & Session("YEAR") & " and deptid=" & Session("DEPTID") & " )AA )BB order by Employee_Name"
        SqlDataSource1.SelectCommand = "/*3*/select *,(case when Len(Term)>4 then Name_Adjust+'<font color=red size=2px> term on '+ Term +'</font>'  else Name_Adjust end) Name_Adjust_Term from("
        SqlDataSource1.SelectCommand &= " /*2*/select EMPLID,Employee_Name,MGT_EMPLID,Manager_Name,/*SAP_MGT,UP_SAP_MGT,*/(case when SAP_MGT=1 then MGT_EMPLID else UP_MGT_EMPLID end) UP_MGT_EMPLID,"
        SqlDataSource1.SelectCommand &= " (case when SAP_MGT=1 then Manager_Name else (select Last+','+First from id_tbl where emplid=B.UP_MGT_EMPLID) end)UP_Manager_Name,HR_EMPLID,"
        SqlDataSource1.SelectCommand &= " (select Last+','+First from id_tbl where emplid=B.HR_EMPLID)HR_Name,JOBTITLE,DEPTID,Department,Updated,Records_Count,Term,"
        SqlDataSource1.SelectCommand &= " (Employee_Name+' <font color=red size=2px>'+'( '+convert(char(2),Records_Count)+')</font>')Name_Adjust from("
        SqlDataSource1.SelectCommand &= " /*1*/select EMPLID,(select Last+','+First from id_tbl where emplid=a.emplid)Employee_Name,MGT_EMPLID,(select Last+','+First from id_tbl where emplid=a.MGT_EMPLID)Manager_Name,"
        SqlDataSource1.SelectCommand &= " (select Sal_admin_plan from HR_PDS_DATA_tbl where emplid=A.MGT_EMPLID)SAP_MGT,(select Sal_admin_plan from HR_PDS_DATA_tbl where emplid=A.UP_MGT_EMPLID)UP_SAP_MGT,UP_MGT_EMPLID,"
        SqlDataSource1.SelectCommand &= " HR_EMPLID,JOBTITLE,DEPTID,Department,(select Max(DateTime) from Appraisal_MasterHistory_tbl where emplid in (select distinct emplid "
        SqlDataSource1.SelectCommand &= " from Appraisal_MasterHistory_tbl where login_emplid>0 and emplid=a.emplid and perf_year=" & Session("YEAR") & ") )Updated,(select count(emplid)from Appraisal_MasterHistory_tbl "
        SqlDataSource1.SelectCommand &= " where emplid=a.emplid and login_emplid>0 and Perf_Year=" & Session("YEAR") & ")Records_Count,(select convert(char(10),termination_date,101) from id_tbl where emplid=a.EMPLID)Term"
        SqlDataSource1.SelectCommand &= " from Appraisal_master_tbl A where Perf_Year=" & Session("YEAR") & " and deptid=" & Session("DEPTID") & "/*1END*/)B/*2END*/)C order by Employee_Name"
        'Response.Write(SqlDataSource1.SelectCommand) : Response.End()
    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'Find First Manager DropDownList,Labels in the Row
            Dim ddlManager_Name As DropDownList = CType(e.Row.FindControl("ddlManager_Name"), DropDownList)
            Dim lblManager As String = CType(e.Row.FindControl("lblManager"), Label).Text
            Dim lblManager_EMPLID As String = CType(e.Row.FindControl("lblManager_EMPLID"), Label).Text
            Dim EMPLID As String = CType(e.Row.FindControl("lblEMPLID"), Label).Text

            'Find Second Manager DropDownList,Labels in the Row
            Dim ddlUP_Manager_Name As DropDownList = CType(e.Row.FindControl("ddlUP_Manager_Name"), DropDownList)
            Dim lblUP_Manager As String = CType(e.Row.FindControl("lblUP_Manager"), Label).Text
            Dim lblUP_Manager_EMPLID As String = CType(e.Row.FindControl("lblUP_Manager_EMPLID"), Label).Text
            Dim EMPLID1 As String = CType(e.Row.FindControl("lblEMPLID1"), Label).Text

            'Find Generalist DropDownList,Labels in the Row
            Dim ddlHR_Name As DropDownList = CType(e.Row.FindControl("ddlHR_Name"), DropDownList)
            Dim lblHR_Name As String = CType(e.Row.FindControl("lblHR_Name"), Label).Text
            Dim lblHR_EMPLID As String = CType(e.Row.FindControl("lblHR_EMPLID"), Label).Text
            Dim EMPLID2 As String = CType(e.Row.FindControl("lblEMPLID2"), Label).Text

            '--First Manage
            SQL = "select distinct emplid MGT_EMPLID,name Manager_Name from ps_employees where emplid<9000 and sal_admin_plan in ('ADR','DIR','MGT','SLT','SRD') order by Name"

            '--Second Manager
            SQL1 = "select distinct emplid UP_MGT_EMPLID,name UP_Manager_Name from ps_employees where emplid<9000 and sal_admin_plan in ('ADR','DIR','MGT','SLT','SRD') order by Name"

            '--Hr Generalist
            SQL2 = "select * from(select distinct HR_EMPLID,(select Last+','+First from id_tbl where emplid=a.HR_EMPLID)HR_Name from Appraisal_master_tbl A where Perf_Year=" & Session("YEAR")
            SQL2 &= " UNION select distinct hr_generalist HR_EMPLID,(select Last+','+First from id_tbl where emplid=a.hr_generalist)HR_Name from HR_PDS_DATA_TBL A )B order by HR_Name"
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)

            ddlManager_Name.Items.Add(New ListItem(lblManager, lblManager_EMPLID))
            For i = 0 To DT.Rows.Count - 1
                ddlManager_Name.Items.Add(New ListItem(DT.Rows(i)("Manager_Name").ToString, DT.Rows(i)("MGT_EMPLID").ToString))
            Next

            ddlUP_Manager_Name.Items.Add(New ListItem(lblUP_Manager, lblUP_Manager_EMPLID))
            For i = 0 To DT1.Rows.Count - 1
                ddlUP_Manager_Name.Items.Add(New ListItem(DT1.Rows(i)("UP_Manager_Name").ToString, DT1.Rows(i)("UP_MGT_EMPLID").ToString))
            Next
            
            ddlHR_Name.Items.Add(New ListItem(lblHR_Name, lblHR_EMPLID))
            For i = 0 To DT2.Rows.Count - 1
                ddlHR_Name.Items.Add(New ListItem(DT2.Rows(i)("HR_Name").ToString, DT2.Rows(i)("HR_EMPLID").ToString))
            Next

        End If

        LocalClass.CloseSQLServerConnection()
    End Sub

    Protected Sub Update_Manager(sender As Object, e As System.EventArgs)

        Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = gvRow.RowIndex
        Dim btn As Button = CType(sender, Button)
        Dim CommandName As String = btn.CommandName
        Dim UPDATED_EMPLID As String = btn.CommandArgument

        Dim ddlManager_EMPLID As DropDownList = CType(gvRow.FindControl("ddlManager_Name"), DropDownList)
        Dim ddlUP_Manager_EMPLID As DropDownList = CType(gvRow.FindControl("ddlUP_Manager_Name"), DropDownList)
        Dim ddlHR_EMPLID As DropDownList = CType(gvRow.FindControl("ddlHR_Name"), DropDownList)
        'Response.Write("employee " & CommandArgument & "<br>First MGT  " & ddlManager_EMPLID.SelectedValue & "<br>Second MGT  " & ddlUP_Manager_EMPLID.SelectedValue & "<br>Generalis  " & ddlHR_EMPLID.SelectedValue)

        SQL1 = "select process_flag from Appraisal_Master_tbl where EMPLID=" & UPDATED_EMPLID & " and Perf_Year=" & Session("YEAR")
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        'Response.Write("FLAG " & DT1.Rows(0)("process_flag").ToString & "<br> EMPLID" & UPDATED_EMPLID & "<br> Year" & Session("YEAR")) : Response.End()

        If DT1.Rows(0)("process_flag").ToString = 5 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('You can not make any changes because employee e-signed the form'); </script>")
        Else
            SQL5 = "select MGT_EMPLID,(case when (select Sal_admin_plan from HR_PDS_DATA_tbl where emplid=A.MGT_EMPLID)=1 then MGT_EMPLID else UP_MGT_EMPLID end)UP_MGT_EMPLID,HR_EMPLID"
            SQL5 &= " from Appraisal_Master_tbl A where EMPLID=" & UPDATED_EMPLID & " and Perf_Year=" & Session("YEAR")
            'Response.Write(SQL5) : Response.End()
            DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
            'Response.Write(CDbl(DT5.Rows(0)("MGT_EMPLID").ToString) - CDbl(ddlManager_EMPLID.SelectedValue) & "<br>" _
            '             & CDbl(DT5.Rows(0)("UP_MGT_EMPLID").ToString) - CDbl(ddlUP_Manager_EMPLID.SelectedValue) & "<br>" _
            '             & CDbl(DT5.Rows(0)("HR_EMPLID").ToString) - CDbl(ddlHR_EMPLID.SelectedValue)) : Response.End()
            If CDbl(DT5.Rows(0)("MGT_EMPLID").ToString) - CDbl(ddlManager_EMPLID.SelectedValue) = 0 And _
               CDbl(DT5.Rows(0)("UP_MGT_EMPLID").ToString) - CDbl(ddlUP_Manager_EMPLID.SelectedValue) = 0 And _
               CDbl(DT5.Rows(0)("HR_EMPLID").ToString) - CDbl(ddlHR_EMPLID.SelectedValue) = 0 Then

                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('You did not make any changes.'); </script>")
            Else
                'SQL1 = "select MGT_EMPLID from appraisal_master_tbl where Perf_Year=" & Session("YEAR") & " and emplid=" & UPDATED_EMPLID & ""
                SQL1 = "select MGT_EMPLID,(select Sal_admin_plan from HR_PDS_DATA_tbl where emplid=A.MGT_EMPLID)MGR_SAP from appraisal_master_tbl A where Perf_Year=" & Session("YEAR") & " and emplid=" & UPDATED_EMPLID & ""
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                'Response.Write(SQL1) : Response.End()
                'Response.Write(DT1.Rows(0)("MGT_EMPLID").ToString & "<br>" & RTrim(LTrim(ddlUP_Manager_EMPLID.SelectedValue))) : Response.End()

                SQL = " Insert into Appraisal_MasterHistory_tbl (LOGIN_EMPLID,EMPLID,Perf_Year,MGT_EMPLID,UP_MGT_EMPLID,HR_EMPLID,DateTime) values"
                SQL &= " ( '" & lblLogin.Text & "','" & Trim(UPDATED_EMPLID) & "','" & Session("YEAR") & "',' " & DT1.Rows(0)("MGT_EMPLID").ToString & "',"
                If DT1.Rows(0)("MGR_SAP").ToString > 1 Then
                    SQL &= " '" & Trim(ddlUP_Manager_EMPLID.SelectedValue) & "',"
                Else
                    SQL &= " NULL,"
                End If
                SQL &= " '" & Trim(ddlHR_EMPLID.SelectedValue) & "','" & Now & "')"

                SQL &= " Update Appraisal_Master_tbl Set MGT_EMPLID=" & Trim(ddlManager_EMPLID.SelectedValue) & ","

                If DT1.Rows(0)("MGR_SAP").ToString > 1 Then
                    SQL &= " UP_MGT_EMPLID=" & RTrim(LTrim(ddlUP_Manager_EMPLID.SelectedValue)) & ", "
                Else
                    SQL &= " UP_MGT_EMPLID=NULL,"
                End If

                SQL &= " HR_EMPLID = " & RTrim(LTrim(ddlHR_EMPLID.SelectedValue))
                SQL &= " where EMPLID=" & UPDATED_EMPLID & " and Perf_Year=" & Session("YEAR")
                'Response.Write(SQL) : Response.End()
                DT = LocalClass.ExecuteSQLDataSet(SQL)

                Response.Redirect("Access_Employees.aspx?Token=" & lblLogin.Text)

            End If
        End If

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowCreated
        'e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'")
        'e.Row.Attributes.Add("onMouseOut", "this.style.background='#ffffff'")
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

End Class