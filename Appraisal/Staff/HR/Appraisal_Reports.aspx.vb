Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page

Public Class Appraisal_Reports
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

        lblSAP.Text = Left(Trim(Request.QueryString("Token")), 1)
        lblYEAR.Text = Right(Trim(Request.QueryString("Token")), 4)

        Session("Year_Appr") = CDbl(lblYEAR.Text)
        Session("Year_MidPoint") = CDbl(lblYEAR.Text) + 1

        lblGENERALIST_EMPLID.Text = Session("HR_EMPLID")
        'Response.Write("Generalist  " & Session("HR_EMPLID"))
        Response.Write("<table width=100% border=0><tr><td width=20%></td><td align=center><img src=../../images/CR_logo.png width=380px height=60px /></td><td width=20%></td></tr>")

        '1 - OLD Guild Report
        '2 - OLD Manager/Exempt Report
        '3 - New Guild Report
        '4 - New Manager/Exempt Report
        '5 - All Employees report

        If CDbl(lblSAP.Text) = 1 Then
            'Response.Write("Run OLD Guild Report for " & lblYEAR.Text & " year")
            Response.Write("<tr><td width=20%></td><td align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Guild Performance Appraisal details</b></font></td><td width=20%></td></tr></table>")
            Panel_GUILD_NEW.Visible = False
            Panel_MGT_NEW.Visible = False
            Panel_MGT_OLD.Visible = False
            Panel_GUILD_OLD.Visible = True
            Panel_ALL_EMPLOYEES.Visible = False
            Guild_OLD_Report()
        ElseIf CDbl(lblSAP.Text) = 2 Then
            'Response.Write("Run OLD  Manager/Exempt Report for " & lblYEAR.Text & " year")
            Response.Write("<tr><td width=20%></td><td align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Manager Performance Appraisal details</b></font></td><td width=20%></td></tr></table>")
            Panel_MGT_NEW.Visible = False
            Panel_GUILD_OLD.Visible = False
            Panel_GUILD_NEW.Visible = False
            Panel_MGT_OLD.Visible = True
            Panel_ALL_EMPLOYEES.Visible = False
            MGT_Report_OLD()
        ElseIf CDbl(lblSAP.Text) = 3 Then
            'Response.Write("Run New Guild Report for " & lblYEAR.Text & " year")
            Response.Write("<tr><td width=20%></td><td align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Guild Performance Appraisal details</b></font></td><td width=20%></td></tr></table>")
            Panel_MGT_OLD.Visible = False
            Panel_MGT_NEW.Visible = False
            Panel_GUILD_OLD.Visible = False
            Panel_GUILD_NEW.Visible = True
            Panel_ALL_EMPLOYEES.Visible = False
            Guild_NEW_Report()
        ElseIf CDbl(lblSAP.Text) = 4 Then
            'Response.Write("Run New Manager/Exempt Report for " & lblYEAR.Text & " year")
            Response.Write("<tr><td width=20%></td><td align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Manager Performance Appraisal details</b></font></td><td width=20%></td></tr></table>")
            Panel_GUILD_OLD.Visible = False
            Panel_GUILD_NEW.Visible = False
            Panel_MGT_OLD.Visible = False
            Panel_MGT_NEW.Visible = True
            Panel_ALL_EMPLOYEES.Visible = False
            MGT_Report_NEW()
        ElseIf CDbl(lblSAP.Text) = 5 Then
            'Response.Write("Run New ALL Employees Report for " & lblYEAR.Text & " year") : Response.End()
            Response.Write("<tr><td width=20%></td><td align=center><font size=5 color=blue><b>" & lblYEAR.Text & " All Employees Performance Appraisal details</b></font></td><td width=20%></td></tr></table>")
            Panel_GUILD_OLD.Visible = False
            Panel_GUILD_NEW.Visible = False
            Panel_MGT_OLD.Visible = False
            Panel_MGT_NEW.Visible = False
            Panel_ALL_EMPLOYEES.Visible = True
            ALL_Employees_Report()

        End If

        btnExcel.Attributes.Add("onMouseOver", "this.style.cursor='pointer';this.style.color='blue';") : btnExcel.Attributes.Add("onMouseOut", "this.style.color='black';")

    End Sub

    Protected Sub MGT_Report_OLD()

        Dim i As Integer
        
        SQL1 = "select Status1,count(*)Employees from(select *,(case when Process_Flag is NULL or (Process_Flag=0 and Len_Str_Devel=0) then 'Incomplete' when Process_Flag=0 and Len_Str_Devel=0"
        SQL1 &= " then 'Incomplete' when Process_Flag=0 and Len_Str_Devel>0 then 'Appraisal Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=2 then 'Sent to HR' when Process_Flag=3"
        SQL1 &= " then 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)STATUS1 from(/*3*/select A.*,Len_Str_Devel,Overall_Rating,(case when"
        SQL1 &= " New_Employee=1 then 'SHORT' else 'FULL' end)Elig_Review,Perf_Year, Date_Sent,Date_Submitted_To_HR,Date_HR_Approved,Date_Employee_Esign,Process_Flag from(/*2*/select * from(/*1*/select EMPLID,"
        SQL1 &= " NAME,DEPTID,DEPTNAME,JOBTITLE,SUPERVISOR_ID FIRST_MGT_EMPLID,(select HR_Generalist from HR_PDS_DATA_tbl where emplid=A.emplid)HR_Generalist,(6088)ViewALL,(6785)HR_CEO from ps_employees A"
        SQL1 &= " where ben_id<>'G3K' and convert(datetime,hire_dt) < convert(datetime,'05/31/'+convert(char(4),Year(Getdate())))/*1END*/)A /*2END*/)A LEFT JOIN (select *,Len(Strenghts)+Len(DevelopmentAreas)"
        SQL1 &= " Len_Str_Devel from ME_Appraisal_MASTER_tbl)B ON A.emplid=B.emplid and Perf_Year=" & lblYEAR.Text & "/*3END*/)C )D group by Status1 order by Status1"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 bordercolor=#E7E8E3 width=20%><tr><td width=30%><b>Employees</b></td><td><b>Appraisal Status</b></b></td></tr>")
        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr></table")
        Next

        If lblGENERALIST_EMPLID.Text = 6785 Or lblGENERALIST_EMPLID.Text = 6250 Then

            SqlDataSource1.SelectCommand = "select *,emplid Token,(select First+' '+Last from id_tbl where emplid in (select HR_Generalist from HR_PDS_DATA_tbl where emplid=dd.emplid))HR_NAME from(/*10*/select *,"
            SqlDataSource1.SelectCommand &= " (case when Process_Flag is NULL or (Process_Flag=0 and Len_Str_Devel=0) and New_Employee=0 then 'Incomplete' when Process_Flag=0 and Len_Str_Devel=0  and New_Employee=0"
            SqlDataSource1.SelectCommand &= " then 'Incomplete' when Process_Flag is NULL or (Process_Flag=0 and Len_Str_Devel=0) and New_Employee=1 then 'Incomplete-New Employee' when Process_Flag=0 and Len_Str_Devel=0"
            SqlDataSource1.SelectCommand &= " and New_Employee=1 then 'Incomplete-New Employee' when Process_Flag=0 and Len_Str_Devel>0 then 'Appraisal Incomplete' when Process_Flag=1 then 'Sent to Second manager'"
            SqlDataSource1.SelectCommand &= " when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)"
            SqlDataSource1.SelectCommand &= " STATUS1, (case when OverAll_Rating=1 then 'Underperforming' when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when"
            SqlDataSource1.SelectCommand &= " OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then 'Distinguished Contributor' end)Final_Rate from(/*9*/select AA.*,New_Employee,Len_Str_Devel,Overall_Rating,"
            SqlDataSource1.SelectCommand &= " (case when New_Employee=1 then 'SHORT' else 'FULL' end)Elig_Review,Perf_Year,Date_Sent,Date_Submitted_To_HR,Date_HR_Approved,Date_Employee_Esign,Process_Flag from("
            SqlDataSource1.SelectCommand &= " /*8 VP*/select (case when SAP#1 in (1,2,3,4) then NAME#1 when SAP#2 in (1,2,3,4) then NAME#2 when SAP#3 in (1,2,3,4) then NAME#3 when SAP#4 in (1,2,3,4) then NAME#4"
            SqlDataSource1.SelectCommand &= " when SAP#5 in (1,2,3,4) then NAME#5 when SAP#6 in (1,2,3,4) then NAME#6 end)VP_NAME,* from("
            SqlDataSource1.SelectCommand &= " /*7 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#6)SAP#6,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#6)SEVEN_MGT_EMPLID,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#6)NAME#7  from("
            SqlDataSource1.SelectCommand &= " /*6 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#5)SAP#5,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#5)EMPLID#6,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#5)NAME#6  from("
            SqlDataSource1.SelectCommand &= " /*5 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#4)SAP#4,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#4)EMPLID#5,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#4)NAME#5  from("
            SqlDataSource1.SelectCommand &= " /*4 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#3)SAP#3,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#3)EMPLID#4,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#3)NAME#4  from("
            SqlDataSource1.SelectCommand &= " /*3 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#2)SAP#2,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#2)EMPLID#3,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#2)NAME#3 from("
            SqlDataSource1.SelectCommand &= " /*2 Manager*/select *,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#1)EMPLID#2,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','') from HR_PDS_DATA_tbl where emplid=EMPLID#1) NAME#2 from("
            SqlDataSource1.SelectCommand &= " /*1 Manager*/select EMPLID,last_name+','+first_name NAME,DEPTID,DEPTNAME,JOBTITLE,SUPERVISOR_EMPLID EMPLID#1,Replace(SUPERVISOR_NAME,' ','')NAME#1,"
            SqlDataSource1.SelectCommand &= " (select sal_admin_plan from HR_PDS_DATA_tbl where emplid=A.SUPERVISOR_EMPLID)SAP#1,HR_Generalist,ben_id "
            SqlDataSource1.SelectCommand &= " from HR_PDS_DATA_tbl A where sal_admin_plan not in (14,15,16,20) and ben_id not in ('NULL') "
            SqlDataSource1.SelectCommand &= " /*1*/)A/*2END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)AA LEFT JOIN"
            SqlDataSource1.SelectCommand &= " (select *,Len(Strenghts)+Len(DevelopmentAreas) Len_Str_Devel from ME_Appraisal_MASTER_tbl)BB ON AA.emplid=BB.emplid and Perf_Year=" & lblYEAR.Text & " /*9END*/"
            SqlDataSource1.SelectCommand &= " )CC/*10END*/)DD order by process_Flag desc,Status1"
        Else
            SqlDataSource1.SelectCommand = "select *,emplid Token,(select First+' '+Last from id_tbl where emplid in (select HR_Generalist from HR_PDS_DATA_tbl where emplid=dd.emplid))HR_NAME  from(/*10*/select *,"
            SqlDataSource1.SelectCommand &= " (case when Process_Flag is NULL or (Process_Flag=0 and Len_Str_Devel=0) and New_Employee=0 then 'Incomplete' when Process_Flag=0 and Len_Str_Devel=0  and New_Employee=0  then 'Incomplete' "
            SqlDataSource1.SelectCommand &= " when Process_Flag is NULL or (Process_Flag=0 and Len_Str_Devel=0) and New_Employee=1 then 'Incomplete-New Employee' when Process_Flag=0 and Len_Str_Devel=0 and New_Employee=1 then 'Incomplete-New Employee' "
            SqlDataSource1.SelectCommand &= " when Process_Flag=0 and Len_Str_Devel>0 then 'Appraisal Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then 'Reviewed by HR' when"
            SqlDataSource1.SelectCommand &= " Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)STATUS1, (case when OverAll_Rating=1 then 'Underperforming' when OverAll_Rating=2 then 'Developing/Improving Contributor' "
            SqlDataSource1.SelectCommand &= " when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then 'Distinguished Contributor' end)Final_Rate from("
            SqlDataSource1.SelectCommand &= " /*9*/select AA.*,New_Employee,Len_Str_Devel,Overall_Rating,(case when New_Employee=1 then 'SHORT' else 'FULL' end)Elig_Review,Perf_Year,Date_Sent,Date_Submitted_To_HR,Date_HR_Approved,Date_Employee_Esign,Process_Flag from("
            SqlDataSource1.SelectCommand &= " /*8 VP*/select (case when SAP#1 in (1,2,3,4) then NAME#1 when SAP#2 in (1,2,3,4) then NAME#2 when SAP#3 in (1,2,3,4) then NAME#3 when SAP#4 in (1,2,3,4) then NAME#4"
            SqlDataSource1.SelectCommand &= " when SAP#5 in (1,2,3,4) then NAME#5 when SAP#6 in (1,2,3,4) then NAME#6 end)VP_NAME,* from("
            SqlDataSource1.SelectCommand &= " /*7 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#6)SAP#6,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#6)SEVEN_MGT_EMPLID,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#6)NAME#7  from("
            SqlDataSource1.SelectCommand &= " /*6 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#5)SAP#5,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#5)EMPLID#6,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#5)NAME#6  from("
            SqlDataSource1.SelectCommand &= " /*5 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#4)SAP#4,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#4)EMPLID#5,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#4)NAME#5  from("
            SqlDataSource1.SelectCommand &= " /*4 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#3)SAP#3,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#3)EMPLID#4,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#3)NAME#4  from("
            SqlDataSource1.SelectCommand &= " /*3 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#2)SAP#2,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#2)EMPLID#3,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#2)NAME#3 from("
            SqlDataSource1.SelectCommand &= " /*2 Manager*/select *,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#1)EMPLID#2,"
            SqlDataSource1.SelectCommand &= " (select Replace(SUPERVISOR_NAME,' ','') from HR_PDS_DATA_tbl where emplid=EMPLID#1) NAME#2 from("
            SqlDataSource1.SelectCommand &= " /*1 Manager*/select EMPLID,last_name+','+first_name NAME,DEPTID,DEPTNAME,JOBTITLE,SUPERVISOR_EMPLID EMPLID#1,Replace(SUPERVISOR_NAME,' ','')NAME#1,"
            SqlDataSource1.SelectCommand &= " (select sal_admin_plan from HR_PDS_DATA_tbl where emplid=A.SUPERVISOR_EMPLID)SAP#1,HR_Generalist,ben_id "
            SqlDataSource1.SelectCommand &= " from HR_PDS_DATA_tbl A where sal_admin_plan not in (14,15,16,20) and ben_id not in ('NULL') "
            SqlDataSource1.SelectCommand &= " /*1*/)A/*2END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)AA LEFT JOIN"
            SqlDataSource1.SelectCommand &= " (select *,Len(Strenghts)+Len(DevelopmentAreas) Len_Str_Devel from ME_Appraisal_MASTER_tbl)BB ON AA.emplid=BB.emplid and Perf_Year=" & lblYEAR.Text & " /*9END*/"
            SqlDataSource1.SelectCommand &= " )CC/*10END*/)DD where deptid not in (9009120)  and EMPLID#1 not in (6193) and len(Date_Sent)>0 order by process_Flag desc,Status1"

        End If
        'Response.Write(SqlDataSource1.SelectCommand)
    End Sub
    Protected Sub Guild_OLD_Report()
        Dim i As Integer
        '--0 - Record created 1 - Submitted to Second manager 3 - Approved by Second Manager 4 - Reviewed by HR 5 - Submitted to Guild  5 + Date_guild_review Guild Sign--
        SQL1 = "select Status1,count(*)Employees from(select (case when process_flag=0 and Len_TaskDesc=0 then 'Incomplete' when Process_Flag=0 and Len_TaskDesc>0 then 'Appraisal Incomplete' when Process_Flag=1 "
        SQL1 &= " then 'Sent to Second manager' when Process_Flag=3 then 'Sent to HR' when Process_Flag=4 then 'Reviewed by HR' when Process_Flag=5 and Date_Guild_Reviewed is Null then 'Sent to Employee' when "
        SQL1 &= " Process_Flag=5 and Date_Guild_Reviewed is not Null then 'E-Signed by Employee' end)Status1, * from(select *, Len_Overall_Commens+Len_TaskComments+Len_Factor Len_TaskDesc from(select *,"
        SQL1 &= " IsNull(Len(MGR_Comments),0)Len_Overall_Commens,IsNull(Len((select distinct Comments from Guild_Appraisal_FACTORSummary_tbl where emplid=A.emplid and IndexId=1 and perf_year=" & lblYEAR.Text & ")),0)Len_TaskComments,"
        SQL1 &= " IsNull((select sum(Exceptional)+sum(High)+sum(Meets)+sum(NeedsImprovement)+sum(Unsatisfactory) from Guild_Appraisal_FACTORS_tbl where emplid=A.emplid and IndexID=1 and perf_year=" & lblYEAR.Text & " "
        SQL1 &= " group by emplid),0)Len_Factor from guild_appraisal_master_tbl A where emplid in (select emplid from ps_employees where ben_id='G3K')  and New_Employee=0 and perf_year=" & lblYEAR.Text & ")B)C)D group by Status1 order by Status1"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<table border=0 cellspacing=0 cellpadding=0 width=100%><tr><td width=10%></td><td width=25% valign=top>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Employees&nbsp;</b></center></td><td><b>&nbsp;Appraisal Status</b></b></td></tr>")

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr>")
        Next

        Response.Write("</table></center></td><td width=25% valign=top>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Employees&nbsp;</b></center></td><td><b>&nbsp;SMART Goals Status</b></b></td></tr>")

        SQL = "select count(*)Employees,GoalFormStatus from(select (case when GoalForm=0 and Len_FutGoal=0 then 'Incomplete' when GoalForm=0 and Len_FutGoal>0 then 'Goal Incomplete' when GoalForm=1 "
        SQL &= " then 'Sent to Second manager' when GoalForm=2 then 'Sent to HR' when GoalForm=3 then 'Reviewed by HR' when GoalForm=4 then 'Sent to Employee' when GoalForm=5 then 'E-Signed by Employee' "
        SQL &= " end)GoalFormStatus,* from(select (select distinct FutGoalTask from(select a.emplid,Sum(GoalLen+TaskLen)FutGoalTask from(select emplid,len(Goals+Milestones+TargetDate)GoalLen from "
        SQL &= " GUILD_Appraisal_FUTUREGOAL_TBL)A,(select emplid,len(Task)TaskLen from GUILD_Appraisal_FUTURETASK_TBL)B where a.emplid=b.emplid group by a.emplid)A where emplid=B.emplid)Len_FutGoal, "
        SQL &= " Process_Flag GoalForm from GUILD_Appraisal_FUTUREGOAL_MASTER_tbl B )C)D group by GoalFormStatus order by GoalFormStatus "
        'Response.Write(SQL1) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        '--loop
        For i = 0 To DT.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT.Rows(i)("Employees").ToString & "</b></td><td>" & DT.Rows(i)("GoalFormStatus").ToString & "</b></td></tr>")
        Next
        '--end loop
        Response.Write("</table></center><td width=25% valign=top>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Employees&nbsp;</b></center></td><td><b>&nbsp;Mid Point Status</b></b></td></tr>")

        SQL3 = "select * from(select count(*)Employees,'E-Signed'Status from Guild_MidPoint_MASTER_tbl where Len(Timestamp)>8 UNION"
        SQL3 &= " select count(*)Employees,'Not E-Signed'Status  from Guild_MidPoint_MASTER_tbl where Met_Mgt=0 and Not_Met_Mgt=0 UNION"
        SQL3 &= " select count(*)Employees,'Not Met-Mgt'Status  from Guild_MidPoint_MASTER_tbl where Timestamp is NULL and Len(Date_NotMEt_Mgt)>8)A"
        'Response.Write(SQL1) : Response.End()
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        '--loop
        For i = 0 To DT3.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT3.Rows(i)("Employees").ToString & "</b></td><td>" & DT3.Rows(i)("Status").ToString & "</b></td></tr>")
        Next
        '--end loop
        Response.Write("</table></center>")
        Response.Write("<td width=10%></td></tr></table></br>")

        SqlDataSource3.SelectCommand = "Select (select First+' '+Last from id_tbl where emplid in ((case when len(HR_EMPLID)<>4 then (select top 1 HR_Generalist from HR_PDS_DATA_tbl where deptid=d.deptid1) else HR_EMPLID end))) HR_NAME, "
        SqlDataSource3.SelectCommand &= " (case when (select count(*) from Guild_MidPoint_MASTER_tbl where Len(Guild_Comments)>0 and Len(TimeStamp)>8  and emplid=d.emplid)>0 then 'E-Signed With Comments' when (select count(*) from Guild_MidPoint_MASTER_tbl where Len(Guild_Comments)=0 and Len(TimeStamp)>8  and emplid=d.emplid)>0 then 'E-Signed NO Comments' when"
        SqlDataSource3.SelectCommand &= " (select count(*)Employees from Guild_MidPoint_MASTER_tbl where Timestamp is NULL and Len(Date_NotMEt_Mgt)>8 and emplid=d.emplid)>0 then 'Not Met-Mgt' else '' end)MidPointStatus,* from("
        SqlDataSource3.SelectCommand &= " /*6*/select (case when GoalForm=0 and Len_FutGoal=0 then 'Incomplete' when GoalForm=0 and Len_FutGoal>0 then 'Goal Incomplete' when GoalForm=1 then 'Sent to Second manager' when GoalForm=2 then 'Sent to HR' "
        SqlDataSource3.SelectCommand &= " when GoalForm=3 then 'Reviewed by HR' when GoalForm=4 then 'Sent to Employee' when GoalForm=5 then 'E-Signed by Employee' end)GoalFormStatus,* from("
        SqlDataSource3.SelectCommand &= " /*5*/select (select FutGoalTask from(select a.emplid,Sum(GoalLen+TaskLen)FutGoalTask from(select emplid,len(Goals+Milestones+TargetDate)GoalLen from GUILD_Appraisal_FUTUREGOAL_TBL)A,(select emplid,"
        SqlDataSource3.SelectCommand &= " len(Task)TaskLen from GUILD_Appraisal_FUTURETASK_TBL)B where a.emplid=b.emplid group by a.emplid)A where emplid=F.emplid)Len_FutGoal,* from("
        SqlDataSource3.SelectCommand &= " /*4*/select (select Process_Flag from GUILD_Appraisal_FUTUREGOAL_MASTER_tbl where emplid=E.emplid)GoalForm,(case when Status='A' and Sal_Admin_Plan1='GLD' then Last+','+First when Status='A' and "
        SqlDataSource3.SelectCommand &= " Sal_Admin_Plan1 <>'GLD' then Last+','+First + '<font color=red> (Status Change)' else Last+','+First+'<font color=red> (term '+convert(char(9),termination_date,1)+')' end)Name,deptid1,departname deptname,jobtitle1,sup_emplid "
        SqlDataSource3.SelectCommand &= " FIRST_MGT_EMPLID,(select First+' '+Last from id_tbl where emplid=sup_emplid)First_Mgt_Name,(select First+' '+Last from id_tbl where emplid=up_mgt_emplid)Second_MGT_Name,(case when IsNull(Len(Refuse_Date),0)>0 "
        SqlDataSource3.SelectCommand &= " then 'YES' else '' end)Refuse_Sign,(case when IsNull(len(Date_Guild_Reviewed),0)>0 and IsNull(Len(Refuse_Date),0)=0 and IsNull(len(Guild_Comments),0)>0 then 'YES' when IsNull(len(Date_Guild_Reviewed),0)>0 and "
        SqlDataSource3.SelectCommand &= " IsNull(Len(Refuse_Date),0)=0 and IsNull(len(Guild_Comments),0)=0 then 'NO' else '' end)GLD_COMM,D.emplid Token,D.* from(/*3*/select (case when process_flag=0 and Len_TaskDesc=0 then 'Incomplete' when "
        SqlDataSource3.SelectCommand &= " Process_Flag=0 and Len_TaskDesc>0 then 'Appraisal Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=3 then 'Sent to HR' when Process_Flag=4 then 'Reviewed by HR' when "
        SqlDataSource3.SelectCommand &= " Process_Flag=5 and Date_Guild_Reviewed is Null then 'Sent to Employee' when Process_Flag=5 and Date_Guild_Reviewed is not Null then 'E-Signed by Employee' end)Status1, (case when OverAll_Rating=0 then '' "
        SqlDataSource3.SelectCommand &= " when OverAll_Rating=1 then 'Exceptional' when OverAll_Rating=2 then 'High' when OverAll_Rating=3 then 'Meets' when OverAll_Rating=4 then 'Needs Improvement' when OverAll_Rating=5 then 'Unsatisfactory' end) "
        SqlDataSource3.SelectCommand &= " Final_Rate,* from(/*2*/select *,Len_Overall_Commens+Len_TaskComments+Len_Factor Len_TaskDesc from(/*1*/select *,IsNull(Len(MGR_Comments),0)Len_Overall_Commens,IsNull(Len((select distinct Comments from "
        SqlDataSource3.SelectCommand &= " Guild_Appraisal_FACTORSummary_tbl where emplid=A.emplid and IndexId=1 and perf_year=" & lblYEAR.Text & ")),0)Len_TaskComments,"
        SqlDataSource3.SelectCommand &= " IsNull((select sum(Exceptional)+sum(High)+sum(Meets)+sum(NeedsImprovement)+sum(Unsatisfactory) from Guild_Appraisal_FACTORS_tbl where emplid=A.emplid and IndexID=1 and "
        SqlDataSource3.SelectCommand &= " perf_year=" & lblYEAR.Text & " group by emplid),0)Len_Factor from guild_appraisal_master_tbl A/*1END*/where emplid in (select emplid from id_tbl) and perf_year=" & lblYEAR.Text & " )B/*2END*/)"
        SqlDataSource3.SelectCommand &= " C/*3END*/)D,id_tbl E where D.emplid=e.emplid/*4END*/)F/*5END*/)G/*6END*/ UNION select (case when GoalForm=0 and Len_FutGoal=0 then 'Incomplete' when GoalForm=0 and Len_FutGoal>0 then 'Goal Incomplete' when "
        SqlDataSource3.SelectCommand &= " GoalForm=1 then 'Sent to Second manager' when GoalForm=2 then 'Sent to HR' when GoalForm=3 then 'Reviewed by HR' when GoalForm=4 then 'Sent to Employee' when GoalForm=5 then 'E-Signed by Employee' end) "
        SqlDataSource3.SelectCommand &= " GoalFormStatus,* from(select (select distinct FutGoalTask from(select a.emplid,Sum(GoalLen+TaskLen)FutGoalTask from(select emplid,len(Goals+Milestones+TargetDate)GoalLen from GUILD_Appraisal_FUTUREGOAL_TBL)A,"
        SqlDataSource3.SelectCommand &= " (select emplid,len(Task)TaskLen from GUILD_Appraisal_FUTURETASK_TBL)B where a.emplid=b.emplid group by a.emplid)A where emplid=B.emplid)Len_FutGoal,Process_Flag GoalForm,(select Last+','+First + ' <font color=red>"
        SqlDataSource3.SelectCommand &= " (New Employee)' from id_tbl where emplid=B.emplid)Name,deptid,department deptname,title jobtitle1,sup_emplid FIRST_MGT_EMPLID,(select First+' '+Last from id_tbl where emplid=sup_emplid)First_Mgt_Name,"
        SqlDataSource3.SelectCommand &= " (select First+' '+Last from id_tbl where emplid=up_mgt_emplid)Second_MGT_Name,''Refuse_Sign,''GLD_COMM,0 Token,''Status1, "
        SqlDataSource3.SelectCommand &= " ''Final_Rate,EMPLID,''Perf_Year,SUP_EMPLID,UP_MGT_EMPLID,HR_EMPLID,''Date_Sent,''Date_Submitted_To_HR,''Date_HR_Approved,''Date_Guild_Reviewed,''MGR_Comments,''MGR_Improvement_Comments1, "
        SqlDataSource3.SelectCommand &= " ''MGR_Improvement_Comments2,''MGR_Improvement_Comments3,''Process_Flag,''Overall_Rating,''Guild_Comments,Null Refuse_Date,''New_Employee,''Len_Overall_Commens,''Len_TaskComments,''Len_Factor,''Len_TaskDesc "
        SqlDataSource3.SelectCommand &= " from GUILD_Appraisal_FUTUREGOAL_MASTER_tbl B where emplid not in (select emplid from guild_appraisal_master_tbl where perf_year=" & lblYEAR.Text & ") )C)D order by Process_Flag desc,Date_Guild_Reviewed desc,deptname,Name"
        'Response.Write(SqlDataSource3.SelectCommand) : Response.End()

    End Sub

    Protected Sub MGT_Report_NEW()

        'GridView2.Columns(2).HeaderText = lblYEAR.Text + 1 & "<br/> Goals <br/>Status"

        Dim i As Integer

        SQL1 = "select SortBy,count(*)Employees,Status1 from(select (case when process_flag=0 and StartFile=0 then 1 when process_flag=1 then 3 when process_flag=2 then 4 when process_flag=3 then 5 "
        SQL1 &= " when process_flag=4 then 6 when process_flag=5 then 7 else 2 end)SortBy,(case when process_flag=0 and StartFile=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' "
        SQL1 &= " when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR' when process_flag=4 then 'Sent to Employee' when process_flag=5 then 'E-Signed by Employee' "
        SQL1 &= " else 'Incomplete' end)Status1 from(select process_flag,Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+"
        SQL1 &= " Promote_Collab+Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact)StartFile from appraisal_master_tbl where emplid in (select distinct emplid from ps_employees) "
        SQL1 &= " and SAP not in (14) and Perf_Year=" & lblYEAR.Text & " and emplid not in (1167,1345,2201,2232,1976,1271,1644,2566,1758,1710,3095,3566,2280,3456,1926,3139,1802,2339,1927,1805,2556,2247,2321,"
        SQL1 &= " 2222,2579,3430,1969,2541,1809,1808,2276,2901,2435,1764,2116,2082,2536,3627,3292) )A)B group by SortBy,Status1 order by SortBy"
        'Response.Write(SQL1) ': Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<table border=0 cellspacing=0 cellpadding=0 width=100%><tr><td width=10%></td><td width=25% valign=top>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Employees&nbsp;</b></center></td><td><b>&nbsp;Appraisal Status</b></td></tr>")

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr>")
        Next

        'Response.Write("</table></center></td><td width=25% valign=top>")
        'Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Employees&nbsp;</b></center></td><td><b>&nbsp;SMART Goals Status</b></b></td></tr>")

        'SQL = " select SortBy,count(*)Employees,GoalFormStatus from(select * from(select a.emplid,(case when Process_flag=0 then 1 when Process_Flag=1 then 2 when Process_Flag=2 then 3 when Process_Flag=3 "
        'SQL &= " then 4 when Process_Flag=4 then 5 when Process_Flag=5 then 6 end)SortBy,(case when Process_flag=0 then 'Incomplete'when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then "
        'SQL &= " 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)GoalFormStatus from appraisal_FutureGoals_master_tbl a, "
        'SQL &= " ps_employees b where A.emplid=B.emplid and perf_year=" & lblYEAR.Text + 1 & " and b.BEN_ID not in ('G3K') )A where emplid not in (1167,1345,2201,2232,1976,1271,1644,2566,1758,1710,3095,3566,2280,3456,1926,3139,1802,2339,1927,"
        'SQL &= " 1805, 2556,2247,2321,2222,2579,3430,1969,2541,1809,1808,2276,2901,2435,1764,2116,2082,2536,3627,3292))B group by SortBy,GoalFormStatus order by SortBy"
        'Response.Write(SQL) : Response.End()
        'DT = LocalClass.ExecuteSQLDataSet(SQL)
        '--loop
        'For i = 0 To DT.Rows.Count - 1
        'Response.Write("<tr><td align=center><b>" & DT.Rows(i)("Employees").ToString & "</b></td><td>" & DT.Rows(i)("GoalFormStatus").ToString & "</b></td></tr>")
        'Next
        '--end loop

        Response.Write("</table></center>")
        Response.Write("<td width=10%></td></tr></table></br>")


        'HR Status Page Report HR Team (1241-Tony,6129-Keili,6081-Joanna,6235-Jennifer,and 5529-Shona) can see all appraisals except for those in HR and the VP's that report to Marta?
        '6088-Esther,6250-Peggy,5294-Cindy,and 6785-Lisa can see all appraisals including those in HR?
        If lblGENERALIST_EMPLID.Text = 6785 Or lblGENERALIST_EMPLID.Text = 6250 Or lblGENERALIST_EMPLID.Text = 6088 Then

            SqlDataSource6.SelectCommand = "select * from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
            SqlDataSource6.SelectCommand &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
            SqlDataSource6.SelectCommand &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
            SqlDataSource6.SelectCommand &= " select *,(select (case when Process_flag=0 then 'Not Incomplete' when Process_Flag=1 then 'Waiting on Manager' "
            SqlDataSource6.SelectCommand &= " when Process_Flag=2 and perf_year<2018 then 'Sent to HR' when Process_Flag=2 and perf_year>=2018 then 'Waiting on Manager' when Process_Flag=3 then "
            SqlDataSource6.SelectCommand &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
            SqlDataSource6.SelectCommand &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,convert(char(10),DateEmpl_Refused,101)DateEmpl_Refused1 from("

            SqlDataSource6.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
            SqlDataSource6.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
            SqlDataSource6.SelectCommand &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

            SqlDataSource6.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SqlDataSource6.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SqlDataSource6.SelectCommand &= " /*8 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SqlDataSource6.SelectCommand &= " /*7 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SqlDataSource6.SelectCommand &= " /*6 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SqlDataSource6.SelectCommand &= " /*5 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SqlDataSource6.SelectCommand &= " /*4 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SqlDataSource6.SelectCommand &= " /*3 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

            SqlDataSource6.SelectCommand &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
            SqlDataSource6.SelectCommand &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
            SqlDataSource6.SelectCommand &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
            SqlDataSource6.SelectCommand &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
            SqlDataSource6.SelectCommand &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,"
            'Current Manager
            SqlDataSource6.SelectCommand &= " (select supervisor_id from ps_employees where emplid=A.emplid)EMPLID#1,(select supervisor_id from ps_employees where emplid in (select supervisor_id from ps_employees where emplid=A.emplid))EMPLID#2,"

            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SqlDataSource6.SelectCommand &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

            SqlDataSource6.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
            SqlDataSource6.SelectCommand &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
            SqlDataSource6.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
            SqlDataSource6.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SqlDataSource6.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SqlDataSource6.SelectCommand &= " /*8 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SqlDataSource6.SelectCommand &= " /*7 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SqlDataSource6.SelectCommand &= " /*6 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SqlDataSource6.SelectCommand &= " /*5 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SqlDataSource6.SelectCommand &= " /*4 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SqlDataSource6.SelectCommand &= " /*3 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

            SqlDataSource6.SelectCommand &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,"
            SqlDataSource6.SelectCommand &= " ''Overall_Rating,Comments, NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"

            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SqlDataSource6.SelectCommand &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ")"
            SqlDataSource6.SelectCommand &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA where SAP<>14)BB  "
            SqlDataSource6.SelectCommand &= " where emplid in (select distinct emplid from ps_employees) )CC)DD order by name"

        Else

            SqlDataSource6.SelectCommand = "select * from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
            SqlDataSource6.SelectCommand &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
            SqlDataSource6.SelectCommand &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
            SqlDataSource6.SelectCommand &= " select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' "
            SqlDataSource6.SelectCommand &= " when Process_Flag=2 and perf_year<2018 then 'Sent to HR' when Process_Flag=2 and perf_year>=2018 then 'Waiting on Manager' when Process_Flag=3 then "
            SqlDataSource6.SelectCommand &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
            SqlDataSource6.SelectCommand &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,convert(char(10),DateEmpl_Refused,101)DateEmpl_Refused1 from("

            SqlDataSource6.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
            SqlDataSource6.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
            SqlDataSource6.SelectCommand &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

            SqlDataSource6.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SqlDataSource6.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SqlDataSource6.SelectCommand &= " /*8 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SqlDataSource6.SelectCommand &= " /*7 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SqlDataSource6.SelectCommand &= " /*6 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SqlDataSource6.SelectCommand &= " /*5 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SqlDataSource6.SelectCommand &= " /*4 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SqlDataSource6.SelectCommand &= " /*3 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

            SqlDataSource6.SelectCommand &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
            SqlDataSource6.SelectCommand &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
            SqlDataSource6.SelectCommand &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
            SqlDataSource6.SelectCommand &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
            SqlDataSource6.SelectCommand &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,"
            'Current Manager
            SqlDataSource6.SelectCommand &= " (select supervisor_id from ps_employees where emplid=A.emplid)EMPLID#1,(select supervisor_id from ps_employees where emplid in (select supervisor_id from ps_employees where emplid=A.emplid))EMPLID#2,"


            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SqlDataSource6.SelectCommand &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

            SqlDataSource6.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
            SqlDataSource6.SelectCommand &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
            SqlDataSource6.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
            SqlDataSource6.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SqlDataSource6.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SqlDataSource6.SelectCommand &= " /*8 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SqlDataSource6.SelectCommand &= " /*7 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SqlDataSource6.SelectCommand &= " /*6 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SqlDataSource6.SelectCommand &= " /*5 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SqlDataSource6.SelectCommand &= " /*4 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SqlDataSource6.SelectCommand &= " /*3 Manager*/select *,"
            SqlDataSource6.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

            SqlDataSource6.SelectCommand &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,"
            SqlDataSource6.SelectCommand &= " NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SqlDataSource6.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SqlDataSource6.SelectCommand &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ") "
            SqlDataSource6.SelectCommand &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA where (deptid not in (9009120) or Deptname not in ('Human Resources')) and SAP<>14)BB  "
            SqlDataSource6.SelectCommand &= " where emplid in (select distinct emplid from ps_employees) )CC)DD order by name"

        End If
        'Response.Write(SqlDataSource6.SelectCommand) : Response.End()

    End Sub
    Protected Sub Guild_NEW_Report()

        'GridView4.Columns(2).HeaderText = lblYEAR.Text + 1 & "<br/> Goals <br/>Status"

        Dim i As Integer

        SQL1 = "select SortBy,count(*)Employees,Status1 from(select (case when process_flag=0 and StartFile=0 then 1 when process_flag=1 then 3 when process_flag=2 then 4 when process_flag=3 then 5"
        SQL1 &= " when process_flag=4 then 6 when process_flag=5 then 7 else 2 end)SortBy,(case when process_flag=0 and StartFile=0 then 'Incomplete' when process_flag=0 and StartFile>0 then 'Incomplete'"
        SQL1 &= " when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR' when process_flag=4 then 'Sent to Employee'"
        SQL1 &= " when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1 from(select process_flag,DateEmpl_Refused,Len(Overall_Summary+Strengths+"
        SQL1 &= " Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+"
        SQL1 &= " Communic_Impact)StartFile from appraisal_master_tbl where emplid in (select distinct emplid from ps_employees) and SAP=14 and Perf_Year=" & lblYEAR.Text & ")A)B group by SortBy,Status1 order by SortBy"
        'Response.Write(SQL1) ': Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<table border=0 cellspacing=0 cellpadding=0 width=100%><tr><td width=10%></td><td width=25% valign=top>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Employees&nbsp;</b></center></td><td><b>&nbsp;Appraisal Status</b></td></tr>")

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr>")
        Next

        Response.Write("</table></center>")
        Response.Write("<td width=10%></td></tr></table></br>")

        SqlDataSource4.SelectCommand = "select * from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
        SqlDataSource4.SelectCommand &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
        SqlDataSource4.SelectCommand &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
        SqlDataSource4.SelectCommand &= " select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' "
        SqlDataSource4.SelectCommand &= " when Process_Flag=2 and perf_year<2018 then 'Sent to HR' when Process_Flag=2 and perf_year>=2018 then 'Waiting on Manager' when Process_Flag=3 then "
        SqlDataSource4.SelectCommand &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
        SqlDataSource4.SelectCommand &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,convert(char(10),DateEmpl_Refused,101)DateEmpl_Refused1 from("

        SqlDataSource4.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
        SqlDataSource4.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
        SqlDataSource4.SelectCommand &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

        SqlDataSource4.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
        SqlDataSource4.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

        SqlDataSource4.SelectCommand &= " /*8 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

        SqlDataSource4.SelectCommand &= " /*7 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

        SqlDataSource4.SelectCommand &= " /*6 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

        SqlDataSource4.SelectCommand &= " /*5 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

        SqlDataSource4.SelectCommand &= " /*4 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

        SqlDataSource4.SelectCommand &= " /*3 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

        SqlDataSource4.SelectCommand &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
        SqlDataSource4.SelectCommand &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
        SqlDataSource4.SelectCommand &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
        SqlDataSource4.SelectCommand &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
        SqlDataSource4.SelectCommand &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,"
        'Current Manager
        SqlDataSource4.SelectCommand &= " (select supervisor_id from ps_employees where emplid=A.emplid)EMPLID#1,(select supervisor_id from ps_employees where emplid in (select supervisor_id from ps_employees where emplid=A.emplid))EMPLID#2,"


        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

        SqlDataSource4.SelectCommand &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

        SqlDataSource4.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
        SqlDataSource4.SelectCommand &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
        SqlDataSource4.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
        SqlDataSource4.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
        SqlDataSource4.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

        SqlDataSource4.SelectCommand &= " /*8 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

        SqlDataSource4.SelectCommand &= " /*7 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

        SqlDataSource4.SelectCommand &= " /*6 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

        SqlDataSource4.SelectCommand &= " /*5 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

        SqlDataSource4.SelectCommand &= " /*4 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

        SqlDataSource4.SelectCommand &= " /*3 Manager*/select *,"
        SqlDataSource4.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

        SqlDataSource4.SelectCommand &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,"
        SqlDataSource4.SelectCommand &= " NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
        SqlDataSource4.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

        SqlDataSource4.SelectCommand &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ")"
        SqlDataSource4.SelectCommand &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA where SAP=14)BB where emplid in (select distinct emplid from ps_employees) )CC)DD  order by name"

        'Response.Write(SqlDataSource4.SelectCommand)



    End Sub

    Protected Sub ALL_Employees_Report()

        'GridView5.Columns(2).HeaderText = lblYEAR.Text + 1 & "<br/> Goals <br/>Status"

        Dim i As Integer

        SQL1 = "select SortBy,count(*)Employees,Status1 from(select (case when process_flag=0 and StartFile=0 then 1 when process_flag=1 then 3 when process_flag=2 then 4 when process_flag=3 then 5 "
        SQL1 &= " when process_flag=4 then 6 when process_flag=5 then 7 else 2 end)SortBy,(case when process_flag=0 and StartFile=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' "
        SQL1 &= " when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR' when process_flag=4 then 'Sent to Employee' when process_flag=5 then 'E-Signed by Employee' "
        SQL1 &= " else 'Incomplete' end)Status1 from(select process_flag,Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+"
        SQL1 &= " Promote_Collab+Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact)StartFile from appraisal_master_tbl where emplid in (select distinct emplid from ps_employees) "
        SQL1 &= " and Perf_Year=" & lblYEAR.Text & " and emplid not in (1167,1345,2201,2232,1976,1271,1644,2566,1758,1710,3095,3566,2280,3456,1926,3139,1802,2339,1927,1805,2556,2247,2321,"
        SQL1 &= " 2222,2579,3430,1969,2541,1809,1808,2276,2901,2435,1764,2116,2082,2536,3627,3292) )A)B group by SortBy,Status1 order by SortBy"
        'Response.Write(SQL1 & "<br>") ': Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<table border=0 cellspacing=0 cellpadding=0 width=100%><tr><td width=10%></td><td width=25% valign=top>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Employees&nbsp;</b></center></td><td><b>&nbsp;Appraisal Status</b></td></tr>")

        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr>")
        Next

        'Response.Write("</table></center></td><td width=25% valign=top>")
        'Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Employees&nbsp;</b></center></td><td><b>&nbsp;SMART Goals Status</b></b></td></tr>")

        'SQL = " select SortBy,count(*)Employees,GoalFormStatus from(select (case when Process_flag=0 then 1 when Process_Flag=1 then 2 when Process_Flag=2 then 3 when Process_Flag=3 then 4 when Process_Flag=4 "
        'SQL &= " then 5 when Process_Flag=5 then 6 end)SortBy,(case when Process_flag=0 then 'Incomplete'when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 "
        'SQL &= " then 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)GoalFormStatus from appraisal_FutureGoals_master_tbl a, ps_employees b "
        'SQL &= " where A.emplid=B.emplid and perf_year=" & lblYEAR.Text + 1 & " and A.emplid not in (1167,1345,2201,2232,1976,1271,1644,2566,1758,1710,3095,3566,2280,3456,1926,3139,1802,2339,1927,1805,"
        'SQL &= " 2556,2247,2321,2222,2579,3430,1969,2541,1809,1808,2276,2901,2435,1764,2116,2082,2536,3627,3292) )A group by SortBy,GoalFormStatus order by SortBy"
        'Response.Write(SQL) : Response.End()
        'DT = LocalClass.ExecuteSQLDataSet(SQL)
        '--loop
        'For i = 0 To DT.Rows.Count - 1
        'Response.Write("<tr><td align=center><b>" & DT.Rows(i)("Employees").ToString & "</b></td><td>" & DT.Rows(i)("GoalFormStatus").ToString & "</b></td></tr>")
        'Next
        '--end loop

        'Response.Write("</table></center><td width=25% valign=top>")
        'Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Employees&nbsp;</b></center></td><td><b>&nbsp;MidPoint Status</b></b></td></tr>")

        'SQL3 = "select SortBy,Employees,MidPoint_Status from(select count(*)Employees,2 SortBy,'E-Signed'MidPoint_Status from Appraisal_MidPoint_MASTER_tbl where Perf_Year=" & lblYEAR.Text + 1 & " and"
        'SQL3 &= " Met_Mgt+Not_Met_Mgt=1 UNION select count(*)Employees,1 SortBy,'Not E-Signed'MidPoint_Status from Appraisal_MidPoint_MASTER_tbl where Perf_Year=" & lblYEAR.Text + 1 & " and Met_Mgt+Not_Met_Mgt=0)A order by SortBy"
        'Response.Write(SQL1) : Response.End()
        'DT3 = LocalClass.ExecuteSQLDataSet(SQL3)
        '--loop
        'For i = 0 To DT3.Rows.Count - 1
        'Response.Write("<tr><td align=center><b>" & DT3.Rows(i)("Employees").ToString & "</b></td><td>" & DT3.Rows(i)("MidPoint_Status").ToString & "</b></td></tr>")
        'Next
        '--end loop
        Response.Write("</table></center>")
        Response.Write("<td width=10%></td></tr></table></br>")


        If lblGENERALIST_EMPLID.Text = 6785 Or lblGENERALIST_EMPLID.Text = 6250 Or lblGENERALIST_EMPLID.Text = 6088 Then
            SqlDataSource5.SelectCommand = "select * from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
            SqlDataSource5.SelectCommand &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
            SqlDataSource5.SelectCommand &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
            SqlDataSource5.SelectCommand &= " select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' "
            SqlDataSource5.SelectCommand &= " when Process_Flag=2 and perf_year<2018 then 'Sent to HR' when Process_Flag=2 and perf_year>=2018 then 'Waiting on Manager' when Process_Flag=3 then "
            SqlDataSource5.SelectCommand &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
            SqlDataSource5.SelectCommand &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,convert(char(10),DateEmpl_Refused,101)DateEmpl_Refused1 from("

            SqlDataSource5.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
            SqlDataSource5.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
            SqlDataSource5.SelectCommand &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

            SqlDataSource5.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SqlDataSource5.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SqlDataSource5.SelectCommand &= " /*8 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SqlDataSource5.SelectCommand &= " /*7 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SqlDataSource5.SelectCommand &= " /*6 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SqlDataSource5.SelectCommand &= " /*5 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SqlDataSource5.SelectCommand &= " /*4 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SqlDataSource5.SelectCommand &= " /*3 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

            SqlDataSource5.SelectCommand &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
            SqlDataSource5.SelectCommand &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
            SqlDataSource5.SelectCommand &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
            SqlDataSource5.SelectCommand &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
            SqlDataSource5.SelectCommand &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,"
            'Current Manager
            SqlDataSource5.SelectCommand &= " (select supervisor_id from ps_employees where emplid=A.emplid)EMPLID#1,(select supervisor_id from ps_employees where emplid in (select supervisor_id from ps_employees where emplid=A.emplid))EMPLID#2,"

            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SqlDataSource5.SelectCommand &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

            SqlDataSource5.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
            SqlDataSource5.SelectCommand &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
            SqlDataSource5.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
            SqlDataSource5.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SqlDataSource5.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SqlDataSource5.SelectCommand &= " /*8 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SqlDataSource5.SelectCommand &= " /*7 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SqlDataSource5.SelectCommand &= " /*6 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SqlDataSource5.SelectCommand &= " /*5 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SqlDataSource5.SelectCommand &= " /*4 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SqlDataSource5.SelectCommand &= " /*3 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

            SqlDataSource5.SelectCommand &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,"
            SqlDataSource5.SelectCommand &= " NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SqlDataSource5.SelectCommand &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ")"
            SqlDataSource5.SelectCommand &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA )BB  "
            SqlDataSource5.SelectCommand &= " where emplid in (select distinct emplid from ps_employees) )CC)DD where emplid not in (1764,1802,1969,2082,2232,2339,2321,2435,3456,1926,3139,2556,1345,2566,2116,1927,1758,2123,2536,3627,3292)  order by name"

        Else
            SqlDataSource5.SelectCommand = "select * from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
            SqlDataSource5.SelectCommand &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
            SqlDataSource5.SelectCommand &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
            SqlDataSource5.SelectCommand &= " select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' "
            SqlDataSource5.SelectCommand &= " when Process_Flag=2 and perf_year<2018 then 'Sent to HR' when Process_Flag=2 and perf_year>=2018 then 'Waiting on Manager' when Process_Flag=3 then "
            SqlDataSource5.SelectCommand &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
            SqlDataSource5.SelectCommand &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,convert(char(10),DateEmpl_Refused,101)DateEmpl_Refused1 from("

            SqlDataSource5.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
            SqlDataSource5.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
            SqlDataSource5.SelectCommand &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

            SqlDataSource5.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SqlDataSource5.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SqlDataSource5.SelectCommand &= " /*8 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SqlDataSource5.SelectCommand &= " /*7 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SqlDataSource5.SelectCommand &= " /*6 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SqlDataSource5.SelectCommand &= " /*5 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SqlDataSource5.SelectCommand &= " /*4 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SqlDataSource5.SelectCommand &= " /*3 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

            SqlDataSource5.SelectCommand &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
            SqlDataSource5.SelectCommand &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
            SqlDataSource5.SelectCommand &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
            SqlDataSource5.SelectCommand &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
            SqlDataSource5.SelectCommand &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,"
            'Current Manager
            SqlDataSource5.SelectCommand &= " (select supervisor_id from ps_employees where emplid=A.emplid)EMPLID#1,(select supervisor_id from ps_employees where emplid in (select supervisor_id from ps_employees where emplid=A.emplid))EMPLID#2,"

            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SqlDataSource5.SelectCommand &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

            SqlDataSource5.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
            SqlDataSource5.SelectCommand &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
            SqlDataSource5.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
            SqlDataSource5.SelectCommand &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SqlDataSource5.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SqlDataSource5.SelectCommand &= " /*8 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SqlDataSource5.SelectCommand &= " /*7 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SqlDataSource5.SelectCommand &= " /*6 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SqlDataSource5.SelectCommand &= " /*5 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SqlDataSource5.SelectCommand &= " /*4 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SqlDataSource5.SelectCommand &= " /*3 Manager*/select *,"
            SqlDataSource5.SelectCommand &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

            SqlDataSource5.SelectCommand &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,"
            SqlDataSource5.SelectCommand &= " NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SqlDataSource5.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SqlDataSource5.SelectCommand &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ")"
            SqlDataSource5.SelectCommand &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA where (deptid not in (9009120) or Deptname not in ('Human Resources')))BB  "
            SqlDataSource5.SelectCommand &= " where emplid in (select distinct emplid from ps_employees) )CC)DD where emplid not in (1764,1802,1969,2082,2232,2339,2321,2435,3456,1926,3139,2556,1345,2566,2116,1927,1758,2123,2536,3627,3292)  order by name"
        End If

    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Response.Redirect("Appraisal_Reports_Excel.aspx?Token=" & Request.QueryString("Token"))
    End Sub

End Class