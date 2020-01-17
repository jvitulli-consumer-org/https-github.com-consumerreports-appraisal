Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page

Public Class Goals_Reports
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8, SQL9, SQL10, y, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6, DT7, DT8, DT9, DT10 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim Msg, Subj As String

    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", "840")

        'If Session("EMPLID_LOGON") = "" Then Response.Redirect("default.aspx")

        lblSAP.Text = Left(Trim(Request.QueryString("Token")), 1)
        lblYEAR.Text = Right(Trim(Request.QueryString("Token")), 4)

        lblGENERALIST_EMPLID.Text = Session("HR_EMPLID")
        'Response.Write("Generalist  " & Session("HR_EMPLID"))
        Response.Write("<table width=100% border=0><tr><td width=20%></td><td align=center><img src=../../images/CR_logo.png width=380px height=60px /></td><td width=20%></td></tr>")

        '1 - Employees report
        '2 - Manager/Exempt Report
        '3 - Guild Report

        SQL = "Select first+' '+last MGT_NAME from id_tbl where emplid=" & lblGENERALIST_EMPLID.Text
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        lblGENERALIST_Name.Text = DT.Rows(0)("MGT_NAME").ToString

        If CDbl(lblSAP.Text) = 1 Then
            ALL_Employees_Goal_Report()
            Panel_ALL_EMPLOYEES.Visible = True
            Panel_MGT.Visible = False
            Panel_GLD.Visible = False
        ElseIf CDbl(lblSAP.Text) = 2 Then
            MGT_Employees_Goal_Report()
            Panel_ALL_EMPLOYEES.Visible = False
            Panel_MGT.Visible = True
            Panel_GLD.Visible = False
        ElseIf CDbl(lblSAP.Text) = 3 Then
            GLD_Employees_Goal_Report()
            Panel_ALL_EMPLOYEES.Visible = False
            Panel_MGT.Visible = False
            Panel_GLD.Visible = True
        End If

    End Sub

    Protected Sub ALL_Employees_Goal_Report()
        Dim i As Integer
        SQL1 = "select count(*)Employees, Status1 from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
        SQL1 &= " when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then ' term '+Term+'' else ' New Emp' end)Name,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 "
        SQL1 &= " when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("
        SQL1 &= " /*8 Manager*/select *, IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from( "
        SQL1 &= " /*7 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from( "
        SQL1 &= " /*6 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
        SQL1 &= " /*5 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & ")EMPLID#5 from( "
        SQL1 &= " /*4 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from( "
        SQL1 &= " /*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & ")EMPLID#3 from("
        SQL1 &= " /*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,"
        SQL1 &= " (select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl "
        SQL1 &= " where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2, IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 "
        SQL1 &= " from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/"
        SQL1 &= " )AA )BB group by Status1"
        'Response.Write(SQL1) ': Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<center><table border=0 width=100%><tr><td align=center><b><font size=4px>Future Goals Status Report</b></td></tr><tr><td></td></tr></table>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 bordercolor=#E7E8E3 width=20%><tr><td width=30%><b>Employees</b></td><td><b>Goals Status</b></td></tr>")
        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr></table")
        Next

        If lblGENERALIST_EMPLID.Text = 6785 Or lblGENERALIST_EMPLID.Text = 6250 Then
            SqlDataSource1.SelectCommand = "select * from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
            SqlDataSource1.SelectCommand &= "  when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case "
            SqlDataSource1.SelectCommand &= "  when len(Term) > 6 then ' term '+Term+'' else '' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, "
            SqlDataSource1.SelectCommand &= "  (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then "
            SqlDataSource1.SelectCommand &= "  EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 "
            SqlDataSource1.SelectCommand &= "  when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from(/*8 Manager*/select *, IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,"
            SqlDataSource1.SelectCommand &= "  (select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from(/*7 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#6 "
            SqlDataSource1.SelectCommand &= "  and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from(/*6 Manager*/select *,IsNull((select  SAP from "
            SqlDataSource1.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
            SqlDataSource1.SelectCommand &= "  /*5 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and "
            SqlDataSource1.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#5 from(/*4 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from "
            SqlDataSource1.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from(/*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and "
            SqlDataSource1.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#3 from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,"
            SqlDataSource1.SelectCommand &= "  Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2,"
            SqlDataSource1.SelectCommand &= "  IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B"
            SqlDataSource1.SelectCommand &= "  /*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA)BB order by name"
        Else
            SqlDataSource1.SelectCommand = "select * from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
            SqlDataSource1.SelectCommand &= "  when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case "
            SqlDataSource1.SelectCommand &= "  when len(Term) > 6 then ' term '+Term+'' else '' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, "
            SqlDataSource1.SelectCommand &= "  (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then "
            SqlDataSource1.SelectCommand &= "  EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 "
            SqlDataSource1.SelectCommand &= "  when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from(/*8 Manager*/select *, IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,"
            SqlDataSource1.SelectCommand &= "  (select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from(/*7 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#6 "
            SqlDataSource1.SelectCommand &= "  and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from(/*6 Manager*/select *,IsNull((select  SAP from "
            SqlDataSource1.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
            SqlDataSource1.SelectCommand &= "  /*5 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and "
            SqlDataSource1.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#5 from(/*4 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from "
            SqlDataSource1.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from(/*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and "
            SqlDataSource1.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#3 from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,"
            SqlDataSource1.SelectCommand &= "  Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2,"
            SqlDataSource1.SelectCommand &= "  IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B"
            SqlDataSource1.SelectCommand &= "  /*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA)BB where (deptid not in (9009120) or Deptname not in ('Human Resources')) order by name"
        End If
        LocalClass.CloseSQLServerConnection()

    End Sub


    Protected Sub MGT_Employees_Goal_Report()
        Dim i As Integer
        SQL1 = "select count(*)Employees, Status1 from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
        SQL1 &= " when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then ' term '+Term+'' else ' New Emp' end)Name,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 "
        SQL1 &= " when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("
        SQL1 &= " /*8 Manager*/select *, IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from( "
        SQL1 &= " /*7 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from( "
        SQL1 &= " /*6 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
        SQL1 &= " /*5 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & ")EMPLID#5 from( "
        SQL1 &= " /*4 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from( "
        SQL1 &= " /*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & ")EMPLID#3 from("
        SQL1 &= " /*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,"
        SQL1 &= " (select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl "
        SQL1 &= " where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2, IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 "
        SQL1 &= " from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/"
        SQL1 &= " )AA where SAP<>14)BB group by Status1"
        'Response.Write(SQL1) ': Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<center><table border=0 width=100%><tr><td align=center><b><font size=4px>Future Goals Status Report</b></td></tr><tr><td></td></tr></table>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 bordercolor=#E7E8E3 width=20%><tr><td width=30%><b>Employees</b></td><td><b>Goals Status</b></td></tr>")
        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr></table")
        Next

        If lblGENERALIST_EMPLID.Text = 6785 Or lblGENERALIST_EMPLID.Text = 6250 Then
            SqlDataSource2.SelectCommand = "select * from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
            SqlDataSource2.SelectCommand &= "  when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case "
            SqlDataSource2.SelectCommand &= "  when len(Term) > 6 then ' term '+Term+'' else '' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, "
            SqlDataSource2.SelectCommand &= "  (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then "
            SqlDataSource2.SelectCommand &= "  EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 "
            SqlDataSource2.SelectCommand &= "  when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from(/*8 Manager*/select *, IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,"
            SqlDataSource2.SelectCommand &= "  (select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from(/*7 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#6 "
            SqlDataSource2.SelectCommand &= "  and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from(/*6 Manager*/select *,IsNull((select  SAP from "
            SqlDataSource2.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
            SqlDataSource2.SelectCommand &= "  /*5 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and "
            SqlDataSource2.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#5 from(/*4 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from "
            SqlDataSource2.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from(/*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and "
            SqlDataSource2.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#3 from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,"
            SqlDataSource2.SelectCommand &= "  Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2,"
            SqlDataSource2.SelectCommand &= "  IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B"
            SqlDataSource2.SelectCommand &= "  /*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA)BB where SAP<>14 order by name"
        Else
            SqlDataSource2.SelectCommand = "select * from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
            SqlDataSource2.SelectCommand &= "  when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case "
            SqlDataSource2.SelectCommand &= "  when len(Term) > 6 then ' term '+Term+'' else '' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, "
            SqlDataSource2.SelectCommand &= "  (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then "
            SqlDataSource2.SelectCommand &= "  EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 "
            SqlDataSource2.SelectCommand &= "  when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from(/*8 Manager*/select *, IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,"
            SqlDataSource2.SelectCommand &= "  (select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from(/*7 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#6 "
            SqlDataSource2.SelectCommand &= "  and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from(/*6 Manager*/select *,IsNull((select  SAP from "
            SqlDataSource2.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
            SqlDataSource2.SelectCommand &= "  /*5 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and "
            SqlDataSource2.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#5 from(/*4 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from "
            SqlDataSource2.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from(/*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and "
            SqlDataSource2.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#3 from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,"
            SqlDataSource2.SelectCommand &= "  Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2,"
            SqlDataSource2.SelectCommand &= "  IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B"
            SqlDataSource2.SelectCommand &= "  /*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA)BB where SAP<>14 and (deptid not in (9009120) or Deptname not in ('Human Resources')) order by name"

        End If
        LocalClass.CloseSQLServerConnection()

    End Sub


    Protected Sub GLD_Employees_Goal_Report()
        Dim i As Integer
        SQL1 = "select count(*)Employees, Status1 from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
        SQL1 &= " when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then ' term '+Term+'' else ' New Emp' end)Name,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 "
        SQL1 &= " when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("
        SQL1 &= " /*8 Manager*/select *, IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from( "
        SQL1 &= " /*7 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from( "
        SQL1 &= " /*6 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
        SQL1 &= " /*5 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & ")EMPLID#5 from( "
        SQL1 &= " /*4 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from( "
        SQL1 &= " /*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & ")EMPLID#3 from("
        SQL1 &= " /*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,"
        SQL1 &= " (select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl "
        SQL1 &= " where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2, IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 "
        SQL1 &= " from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/"
        SQL1 &= " )AA where SAP=14)BB group by Status1"
        'Response.Write(SQL1) ': Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<center><table border=0 width=100%><tr><td align=center><b><font size=4px>Future Goals Status Report</b></td></tr><tr><td></td></tr></table>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 bordercolor=#E7E8E3 width=20%><tr><td width=30%><b>Employees</b></td><td><b>Goals Status</b></td></tr>")
        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr></table")
        Next

        SqlDataSource3.SelectCommand = "select * from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
        SqlDataSource3.SelectCommand &= "  when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case "
        SqlDataSource3.SelectCommand &= "  when len(Term) > 6 then ' term '+Term+'' else '' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, "
        SqlDataSource3.SelectCommand &= "  (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then "
        SqlDataSource3.SelectCommand &= "  EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 "
        SqlDataSource3.SelectCommand &= "  when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from(/*8 Manager*/select *, IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,"
        SqlDataSource3.SelectCommand &= "  (select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from(/*7 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#6 "
        SqlDataSource3.SelectCommand &= "  and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from(/*6 Manager*/select *,IsNull((select  SAP from "
        SqlDataSource3.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
        SqlDataSource3.SelectCommand &= "  /*5 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and "
        SqlDataSource3.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#5 from(/*4 Manager*/select *,IsNull((select  SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from "
        SqlDataSource3.SelectCommand &= "  appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from(/*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and "
        SqlDataSource3.SelectCommand &= "  perf_year=" & lblYEAR.Text & ")EMPLID#3 from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,"
        SqlDataSource3.SelectCommand &= "  Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2,"
        SqlDataSource3.SelectCommand &= "  IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B"
        SqlDataSource3.SelectCommand &= "  /*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA)BB where SAP=14 order by name"
        'Response.Write(SqlDataSource3.SelectCommand) : Response.End()

        LocalClass.CloseSQLServerConnection()

    End Sub

End Class