Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Public Class Appraisal_Reports_Excel
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
        lblGENERALIST_EMPLID.Text = Session("HR_EMPLID")
        'Response.Write("Generalist  " & Session("HR_EMPLID"))
        Response.Write("<table width=100% border=0><tr><td width=20%></td><td align=center><img src=../../images/CR_logo.png width=380px height=60px /></td><td width=20%></td></tr>")

        '1 - OLD Guild Report
        '2 - OLD Manager/Exempt Report
        '3 - New Guild Report
        '4 - New Manager/Exempt Report
        '5 - All Employees report

        If CDbl(lblSAP.Text) = 1 Then
            Guild_OLD_Report()
        ElseIf CDbl(lblSAP.Text) = 2 Then
            'Response.Write("Run OLD  Manager/Exempt Report for " & lblYEAR.Text & " year")
            'Response.Write("<table><tr><td width=20%></td><td align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Manager Performance Appraisal details</b></font></td><td width=20%></td></tr></table>")
            MGT_Report_OLD()
        ElseIf CDbl(lblSAP.Text) = 3 Then
            Guild_NEW_Report()
        ElseIf CDbl(lblSAP.Text) = 4 Then
            MGT_Report_NEW()
        ElseIf CDbl(lblSAP.Text) = 5 Then
            ALL_Employees_Report()
        End If
    End Sub
    Protected Sub Guild_OLD_Report()
        SQL = "select Name 'Employee Name',GoalFormStatus 'Goals Status',MidPointStatus 'MidPoint Status',First_MGT_Name 'Manager name',Second_MGT_Name 'Second manager name',HR_Name 'Generalist',Deptname 'Department',Status1 'Appraisal Status',Final_Rate 'Overall Rating',GLD_COMM 'Guild Comments',Refuse_Sign "
        SQL &= " 'Guild Refuse to Sign' from(select (select First+' '+Last from id_tbl where emplid in ((case when len(HR_EMPLID)<>4 then (select top 1 HR_Generalist from HR_PDS_DATA_tbl where deptid=d.deptid1) else HR_EMPLID end))) HR_NAME, "
        SQL &= " (case when (select count(*) from Guild_MidPoint_MASTER_tbl where Len(Guild_Comments)>0 and Len(TimeStamp)>8  and emplid=d.emplid)>0 then 'E-Signed With Comments' when (select count(*) from Guild_MidPoint_MASTER_tbl where Len(Guild_Comments)=0 and Len(TimeStamp)>8  and emplid=d.emplid)>0 then 'E-Signed NO Comments' when"
        SQL &= " (select count(*)Employees from Guild_MidPoint_MASTER_tbl where Timestamp is NULL and Len(Date_NotMEt_Mgt)>8 and emplid=d.emplid)>0 then 'Not Met-Mgt' else '' end)MidPointStatus,* from("
        SQL &= " /*6*/select (case when GoalForm=0 and Len_FutGoal=0 then 'Incomplete' when GoalForm=0 and Len_FutGoal>0 then 'Goal Incomplete' when GoalForm=1 then 'Sent to Second manager' when GoalForm=2 then 'Sent to HR' "
        SQL &= " when GoalForm=3 then 'Reviewed by HR' when GoalForm=4 then 'Sent to Employee' when GoalForm=5 then 'E-Signed by Employee' end)GoalFormStatus,* from("
        SQL &= " /*5*/select (select FutGoalTask from(select a.emplid,Sum(GoalLen+TaskLen)FutGoalTask from(select emplid,len(Goals+Milestones+TargetDate)GoalLen from GUILD_Appraisal_FUTUREGOAL_TBL)A,(select emplid,"
        SQL &= " len(Task)TaskLen from GUILD_Appraisal_FUTURETASK_TBL)B where a.emplid=b.emplid group by a.emplid)A where emplid=F.emplid)Len_FutGoal,* from("
        SQL &= " /*4*/select (select Process_Flag from GUILD_Appraisal_FUTUREGOAL_MASTER_tbl where emplid=E.emplid)GoalForm,(case when Status='A' and Sal_Admin_Plan1='GLD' then Last+','+First when Status='A' and "
        SQL &= " Sal_Admin_Plan1 <>'GLD' then Last+','+First + ' (Status Change)' else Last+','+First+' (term '+convert(char(9),termination_date,1)+')' end)Name,deptid1,departname deptname,jobtitle1,sup_emplid "
        SQL &= " FIRST_MGT_EMPLID,(select First+' '+Last from id_tbl where emplid=sup_emplid)First_Mgt_Name,(select First+' '+Last from id_tbl where emplid=up_mgt_emplid)Second_MGT_Name,(case when IsNull(Len(Refuse_Date),0)>0 "
        SQL &= " then 'YES' else '' end)Refuse_Sign,(case when IsNull(len(Date_Guild_Reviewed),0)>0 and IsNull(Len(Refuse_Date),0)=0 and IsNull(len(Guild_Comments),0)>0 then 'YES' when IsNull(len(Date_Guild_Reviewed),0)>0 and "
        SQL &= " IsNull(Len(Refuse_Date),0)=0 and IsNull(len(Guild_Comments),0)=0 then 'NO' else '' end)GLD_COMM,D.emplid Token,D.* from(/*3*/select (case when process_flag=0 and Len_TaskDesc=0 then 'Incomplete' when "
        SQL &= " Process_Flag=0 and Len_TaskDesc>0 then 'Appraisal Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=3 then 'Sent to HR' when Process_Flag=4 then 'Reviewed by HR' when "
        SQL &= " Process_Flag=5 and Date_Guild_Reviewed is Null then 'Sent to Employee' when Process_Flag=5 and Date_Guild_Reviewed is not Null then 'E-Signed by Employee' end)Status1, (case when OverAll_Rating=0 then '' "
        SQL &= " when OverAll_Rating=1 then 'Exceptional' when OverAll_Rating=2 then 'High' when OverAll_Rating=3 then 'Meets' when OverAll_Rating=4 then 'Needs Improvement' when OverAll_Rating=5 then 'Unsatisfactory' end) "
        SQL &= " Final_Rate,* from(/*2*/select *,Len_Overall_Commens+Len_TaskComments+Len_Factor Len_TaskDesc from(/*1*/select *,IsNull(Len(MGR_Comments),0)Len_Overall_Commens,IsNull(Len((select distinct Comments from "
        SQL &= " Guild_Appraisal_FACTORSummary_tbl where emplid=A.emplid and IndexId=1 and perf_year=" & lblYEAR.Text & ")),0)Len_TaskComments,"
        SQL &= " IsNull((select sum(Exceptional)+sum(High)+sum(Meets)+sum(NeedsImprovement)+sum(Unsatisfactory) from Guild_Appraisal_FACTORS_tbl where emplid=A.emplid and IndexID=1 and "
        SQL &= " perf_year=" & lblYEAR.Text & " group by emplid),0)Len_Factor from guild_appraisal_master_tbl A/*1END*/where emplid in (select emplid from id_tbl) and perf_year=" & lblYEAR.Text & " )B/*2END*/)"
        SQL &= " C/*3END*/)D,id_tbl E where D.emplid=e.emplid/*4END*/)F/*5END*/)G/*6END*/ UNION select (case when GoalForm=0 and Len_FutGoal=0 then 'Incomplete' when GoalForm=0 and Len_FutGoal>0 then 'Goal Incomplete' when "
        SQL &= " GoalForm=1 then 'Sent to Second manager' when GoalForm=2 then 'Sent to HR' when GoalForm=3 then 'Reviewed by HR' when GoalForm=4 then 'Sent to Employee' when GoalForm=5 then 'E-Signed by Employee' end) "
        SQL &= " GoalFormStatus,* from(select (select distinct FutGoalTask from(select a.emplid,Sum(GoalLen+TaskLen)FutGoalTask from(select emplid,len(Goals+Milestones+TargetDate)GoalLen from GUILD_Appraisal_FUTUREGOAL_TBL)A,"
        SQL &= " (select emplid,len(Task)TaskLen from GUILD_Appraisal_FUTURETASK_TBL)B where a.emplid=b.emplid group by a.emplid)A where emplid=B.emplid)Len_FutGoal,Process_Flag GoalForm,(select Last+','+First + ' "
        SQL &= " (New Employee)' from id_tbl where emplid=B.emplid)Name,deptid,department deptname,title jobtitle1,sup_emplid FIRST_MGT_EMPLID,(select First+' '+Last from id_tbl where emplid=sup_emplid)First_Mgt_Name,"
        SQL &= " (select First+' '+Last from id_tbl where emplid=up_mgt_emplid)Second_MGT_Name,''Refuse_Sign,''GLD_COMM,0 Token,''Status1, "
        SQL &= " ''Final_Rate,EMPLID,''Perf_Year,SUP_EMPLID,UP_MGT_EMPLID,HR_EMPLID,''Date_Sent,''Date_Submitted_To_HR,''Date_HR_Approved,''Date_Guild_Reviewed,''MGR_Comments,''MGR_Improvement_Comments1, "
        SQL &= " ''MGR_Improvement_Comments2,''MGR_Improvement_Comments3,''Process_Flag,''Overall_Rating,''Guild_Comments,Null Refuse_Date,''New_Employee,''Len_Overall_Commens,''Len_TaskComments,''Len_Factor,''Len_TaskDesc "
        SQL &= " from GUILD_Appraisal_FUTUREGOAL_MASTER_tbl B where emplid not in (select emplid from guild_appraisal_master_tbl where perf_year=" & lblYEAR.Text & ") )C)D)E order by Name"
        'Response.Write(SQL) : Response.End()

        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Dim attachment As String = "attachment; filename=Guild_Appraisal_Report.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/vnd.ms-excel"
        Dim tab1 As String = ""

        Response.Write("<table border=1><tr><td colspan=11 align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Guild Performance Appraisal details</b></font></td></tr><tr>")

        For Each dc As DataColumn In DT.Columns
            Response.Write("<td>")
            Response.Write("<b>" + tab1 + dc.ColumnName)
            tab1 = vbTab
        Next
        Response.Write(vbLf)

        Response.Write("</td></tr><tr>")

        'Dim i As Integer
        For Each dr As DataRow In DT.Rows
            tab1 = ""
            For i = 0 To DT.Columns.Count - 1
                Response.Write("<td>")
                Response.Write(tab1 & dr(i).ToString())
                tab1 = vbTab
            Next
            Response.Write(vbLf)

            Response.Write("</td></tr>")

        Next

        Response.[End]()

        Response.Write("</table>")

        'export to excel
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub MGT_Report_OLD()
        If lblGENERALIST_EMPLID.Text = 6785 Or lblGENERALIST_EMPLID.Text = 6250 Then
            SQL = "Select Name 'Employee Name',Name#1 'Manager name',Name#2 'Second manager name',VP_Name 'VP Name',HR_Name 'Generalist',Deptname 'Department',Status1 'Appraisal Status',IsNull(Final_Rate,'')'Overall Rating' from("
            SQL &= "select *,emplid Token,(select First+' '+Last from id_tbl where emplid in (select HR_Generalist from HR_PDS_DATA_tbl where emplid=dd.emplid))HR_NAME from(/*10*/select *,"
            SQL &= " (case when Process_Flag is NULL or (Process_Flag=0 and Len_Str_Devel=0) and New_Employee=0 then 'Incomplete' when Process_Flag=0 and Len_Str_Devel=0  and New_Employee=0"
            SQL &= " then 'Incomplete' when Process_Flag is NULL or (Process_Flag=0 and Len_Str_Devel=0) and New_Employee=1 then 'Incomplete-New Employee' when Process_Flag=0 and Len_Str_Devel=0"
            SQL &= " and New_Employee=1 then 'Incomplete-New Employee' when Process_Flag=0 and Len_Str_Devel>0 then 'Appraisal Incomplete' when Process_Flag=1 then 'Sent to Second manager'"
            SQL &= " when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)"
            SQL &= " STATUS1, (case when OverAll_Rating=1 then 'Underperforming' when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when"
            SQL &= " OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then 'Distinguished Contributor' end)Final_Rate from(/*9*/select AA.*,New_Employee,Len_Str_Devel,Overall_Rating,"
            SQL &= " (case when New_Employee=1 then 'SHORT' else 'FULL' end)Elig_Review,Perf_Year,Date_Sent,Date_Submitted_To_HR,Date_HR_Approved,Date_Employee_Esign,Process_Flag from("
            SQL &= " /*8 VP*/select (case when SAP#1 in (1,2,3,4) then NAME#1 when SAP#2 in (1,2,3,4) then NAME#2 when SAP#3 in (1,2,3,4) then NAME#3 when SAP#4 in (1,2,3,4) then NAME#4"
            SQL &= " when SAP#5 in (1,2,3,4) then NAME#5 when SAP#6 in (1,2,3,4) then NAME#6 end)VP_NAME,* from("
            SQL &= " /*7 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#6)SAP#6,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#6)SEVEN_MGT_EMPLID,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#6)NAME#7  from("
            SQL &= " /*6 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#5)SAP#5,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#5)EMPLID#6,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#5)NAME#6  from("
            SQL &= " /*5 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#4)SAP#4,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#4)EMPLID#5,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#4)NAME#5  from("
            SQL &= " /*4 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#3)SAP#3,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#3)EMPLID#4,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#3)NAME#4  from("
            SQL &= " /*3 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#2)SAP#2,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#2)EMPLID#3,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#2)NAME#3 from("
            SQL &= " /*2 Manager*/select *,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#1)EMPLID#2,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','') from HR_PDS_DATA_tbl where emplid=EMPLID#1) NAME#2 from("
            SQL &= " /*1 Manager*/select EMPLID,last_name+','+first_name NAME,DEPTID,DEPTNAME,JOBTITLE,SUPERVISOR_EMPLID EMPLID#1,Replace(SUPERVISOR_NAME,' ','')NAME#1,"
            SQL &= " (select sal_admin_plan from HR_PDS_DATA_tbl where emplid=A.SUPERVISOR_EMPLID)SAP#1,HR_Generalist,ben_id "
            SQL &= " from HR_PDS_DATA_tbl A where sal_admin_plan not in (14,15,16,20) and ben_id not in ('NULL') "
            SQL &= " /*1*/)A/*2END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)AA LEFT JOIN"
            SQL &= " (select *,Len(Strenghts)+Len(DevelopmentAreas) Len_Str_Devel from ME_Appraisal_MASTER_tbl)BB ON AA.emplid=BB.emplid and Perf_Year=" & lblYEAR.Text & " /*9END*/"
            SQL &= " )CC/*10END*/)DD)EE order by process_Flag desc,Status1"
        Else
            SQL = "Select Name 'Employee Name',Name#1 'Manager name',Name#2 'Second manager name',VP_Name 'VP Name',HR_Name 'Generalist',Deptname 'Department',Status1 'Appraisal Status',Final_Rate 'Overall Rating' from("
            SQL &= "select *,emplid Token,(select First+' '+Last from id_tbl where emplid in (select HR_Generalist from HR_PDS_DATA_tbl where emplid=dd.emplid))HR_NAME  from(/*10*/select *,"
            SQL &= " (case when Process_Flag is NULL or (Process_Flag=0 and Len_Str_Devel=0) and New_Employee=0 then 'Incomplete' when Process_Flag=0 and Len_Str_Devel=0  and New_Employee=0  then 'Incomplete' "
            SQL &= " when Process_Flag is NULL or (Process_Flag=0 and Len_Str_Devel=0) and New_Employee=1 then 'Incomplete-New Employee' when Process_Flag=0 and Len_Str_Devel=0 and New_Employee=1 then 'Incomplete-New Employee' "
            SQL &= " when Process_Flag=0 and Len_Str_Devel>0 then 'Appraisal Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then 'Reviewed by HR' when"
            SQL &= " Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)STATUS1, (case when OverAll_Rating=1 then 'Underperforming' when OverAll_Rating=2 then 'Developing/Improving Contributor' "
            SQL &= " when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then 'Distinguished Contributor' end)Final_Rate from("
            SQL &= " /*9*/select AA.*,New_Employee,Len_Str_Devel,Overall_Rating,(case when New_Employee=1 then 'SHORT' else 'FULL' end)Elig_Review,Perf_Year,Date_Sent,Date_Submitted_To_HR,Date_HR_Approved,Date_Employee_Esign,Process_Flag from("
            SQL &= " /*8 VP*/select (case when SAP#1 in (1,2,3,4) then NAME#1 when SAP#2 in (1,2,3,4) then NAME#2 when SAP#3 in (1,2,3,4) then NAME#3 when SAP#4 in (1,2,3,4) then NAME#4"
            SQL &= " when SAP#5 in (1,2,3,4) then NAME#5 when SAP#6 in (1,2,3,4) then NAME#6 end)VP_NAME,* from("
            SQL &= " /*7 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#6)SAP#6,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#6)SEVEN_MGT_EMPLID,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#6)NAME#7  from("
            SQL &= " /*6 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#5)SAP#5,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#5)EMPLID#6,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#5)NAME#6  from("
            SQL &= " /*5 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#4)SAP#4,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#4)EMPLID#5,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#4)NAME#5  from("
            SQL &= " /*4 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#3)SAP#3,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#3)EMPLID#4,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#3)NAME#4  from("
            SQL &= " /*3 Manager*/select *,(select sal_admin_plan from HR_PDS_DATA_tbl where emplid=EMPLID#2)SAP#2,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#2)EMPLID#3,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','')  from HR_PDS_DATA_tbl where emplid=EMPLID#2)NAME#3 from("
            SQL &= " /*2 Manager*/select *,(select SUPERVISOR_EMPLID from HR_PDS_DATA_tbl where emplid=EMPLID#1)EMPLID#2,"
            SQL &= " (select Replace(SUPERVISOR_NAME,' ','') from HR_PDS_DATA_tbl where emplid=EMPLID#1) NAME#2 from("
            SQL &= " /*1 Manager*/select EMPLID,last_name+','+first_name NAME,DEPTID,DEPTNAME,JOBTITLE,SUPERVISOR_EMPLID EMPLID#1,Replace(SUPERVISOR_NAME,' ','')NAME#1,"
            SQL &= " (select sal_admin_plan from HR_PDS_DATA_tbl where emplid=A.SUPERVISOR_EMPLID)SAP#1,HR_Generalist,ben_id "
            SQL &= " from HR_PDS_DATA_tbl A where sal_admin_plan not in (14,15,16,20) and ben_id not in ('NULL') "
            SQL &= " /*1*/)A/*2END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)AA LEFT JOIN"
            SQL &= " (select *,Len(Strenghts)+Len(DevelopmentAreas) Len_Str_Devel from ME_Appraisal_MASTER_tbl)BB ON AA.emplid=BB.emplid and Perf_Year=" & lblYEAR.Text & " /*9END*/"
            SQL &= " )CC/*10END*/)DD where deptid not in (9009120) and EMPLID#1 not in (6193) and len(Date_Sent)>0)EE order by process_Flag desc,Status1"
        End If
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Dim attachment As String = "attachment; filename=Manager_Appraisal_Report.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/vnd.ms-excel"
        Dim tab1 As String = ""

        Response.Write("<table border=1><tr><td colspan=8 align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Manager Performance Appraisal details</b></font></td></tr><tr>")

        For Each dc As DataColumn In DT.Columns
            Response.Write("<td>")
            Response.Write("<b>" + tab1 + dc.ColumnName)
            tab1 = vbTab
        Next
        Response.Write(vbLf)

        Response.Write("</td></tr><tr>")

        Dim i As Integer
        For Each dr As DataRow In DT.Rows
            tab1 = ""
            For i = 0 To DT.Columns.Count - 1
                Response.Write("<td>")
                Response.Write(tab1 & dr(i).ToString())
                tab1 = vbTab
            Next
            Response.Write(vbLf)

            Response.Write("</td></tr>")

        Next

        Response.[End]()

        Response.Write("</table>")

        'export to excel
        LocalClass.CloseSQLServerConnection()


    End Sub

    Protected Sub Guild_NEW_Report()
        SQL = "select  Name 'Employee Name',Status1 'Appraisal Status',/*GoalFormStatus 'Goals Status',MidPointStatus 'MidPoint Status',*/Name#1 'Manager name',Name#2 'Second manager name', VP_Name 'VP Name',"
        SQL &= " HR_Name 'Generalist',Deptname 'Department',Final_Rate 'Overall Rating',Comments 'Employee Comments',DateEmpl_Refused1 'Employee Refuse to Sign'"

        SQL &= " from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
        SQL &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
        SQL &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
        SQL &= " select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then "
        SQL &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
        SQL &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,IsNull(convert(char(10),DateEmpl_Refused,101),'')DateEmpl_Refused1 from("

        SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
        SQL &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
        SQL &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

        SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
        SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

        SQL &= " /*8 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

        SQL &= " /*7 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

        SQL &= " /*6 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

        SQL &= " /*5 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

        SQL &= " /*4 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

        SQL &= " /*3 Manager*/select *,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

        SQL &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
        SQL &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
        SQL &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
        SQL &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
        SQL &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"

        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

        SQL &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

        SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
        SQL &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
        SQL &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
        SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
        SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

        SQL &= " /*8 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

        SQL &= " /*7 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

        SQL &= " /*6 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

        SQL &= " /*5 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

        SQL &= " /*4 Manager*/select *,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

        SQL &= " /*3 Manager*/select *,"
        SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

        SQL &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,"
        SQL &= " NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
        SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

        SQL &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ") "
        SQL &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA where SAP=14)BB  where emplid in (select distinct emplid from ps_employees) )CC)DD order by name"
        'Response.Write(SQL) : Response.End()

        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Dim attachment As String = "attachment; filename=Guild_Appraisal_Report.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/vnd.ms-excel"
        Dim tab1 As String = ""

        Response.Write("<table border=1><tr><td colspan=10 align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Guild Performance Appraisal details</b></font></td></tr><tr>")

        For Each dc As DataColumn In DT.Columns
            Response.Write("<td>")
            Response.Write("<b>" + tab1 + dc.ColumnName)
            tab1 = vbTab
        Next
        Response.Write(vbLf)

        Response.Write("</td></tr><tr>")

        'Dim i As Integer
        For Each dr As DataRow In DT.Rows
            tab1 = ""
            For i = 0 To DT.Columns.Count - 1
                Response.Write("<td>")
                Response.Write(tab1 & dr(i).ToString())
                tab1 = vbTab
            Next
            Response.Write(vbLf)

            Response.Write("</td></tr>")

        Next

        Response.[End]()

        Response.Write("</table>")
        'export to excel

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub ALL_Employees_Report()

        If lblGENERALIST_EMPLID.Text = 6785 Or lblGENERALIST_EMPLID.Text = 6250 Or lblGENERALIST_EMPLID.Text = 6088 Or lblGENERALIST_EMPLID.Text = 5294 Then
            SQL = "select  Name 'Employee Name',Status1 'Appraisal Status',/*GoalFormStatus 'Goals Status',MidPointStatus 'MidPoint Status',*/Name#1 'Manager name',Name#2 'Second manager name', VP_Name 'VP Name',"
            SQL &= " HR_Name 'Generalist',Deptname 'Department',Final_Rate 'Overall Rating',Comments 'Employee Comments',DateEmpl_Refused1 'Employee Refuse to Sign'"

            SQL &= " from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
            SQL &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
            SQL &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
            SQL &= " select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then "
            SQL &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
            SQL &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,IsNull(convert(char(10),DateEmpl_Refused,101),'')DateEmpl_Refused1 from("

            SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
            SQL &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
            SQL &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

            SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SQL &= " /*8 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SQL &= " /*7 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SQL &= " /*6 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SQL &= " /*5 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SQL &= " /*4 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SQL &= " /*3 Manager*/select *,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

            SQL &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
            SQL &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
            SQL &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
            SQL &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
            SQL &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"

            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SQL &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

            SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
            SQL &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
            SQL &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
            SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SQL &= " /*8 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SQL &= " /*7 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SQL &= " /*6 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SQL &= " /*5 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SQL &= " /*4 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SQL &= " /*3 Manager*/select *,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

            SQL &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,"
            SQL &= " NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SQL &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ") "
            SQL &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA)BB  "
            SQL &= " where emplid in (select distinct emplid from ps_employees) )CC)DD where emplid not in (1764,1802,1969,2082,2232,2339,2321,2435,3456,1926,3139,2556,1345,2566,2116,1927,1758,2123,2536,3627,3292)  order by name"
        Else
            SQL = "select  Name 'Employee Name',Status1 'Appraisal Status',/*GoalFormStatus 'Goals Status',MidPointStatus 'MidPoint Status',*/ Name#1 'Manager name',Name#2 'Second manager name', VP_Name 'VP Name',"
            SQL &= " HR_Name 'Generalist',Deptname 'Department',Final_Rate 'Overall Rating',Comments 'Employee Comments',DateEmpl_Refused1 'Employee Refuse to Sign'"

            SQL &= " from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
            SQL &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
            SQL &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
            SQL &= " select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then "
            SQL &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
            SQL &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,IsNull(convert(char(10),DateEmpl_Refused,101),'')DateEmpl_Refused1 from("

            SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
            SQL &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
            SQL &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

            SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SQL &= " /*8 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SQL &= " /*7 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SQL &= " /*6 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SQL &= " /*5 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SQL &= " /*4 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SQL &= " /*3 Manager*/select *,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

            SQL &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
            SQL &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
            SQL &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
            SQL &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
            SQL &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"

            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SQL &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

            SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
            SQL &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
            SQL &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
            SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SQL &= " /*8 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SQL &= " /*7 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SQL &= " /*6 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SQL &= " /*5 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SQL &= " /*4 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SQL &= " /*3 Manager*/select *,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

            SQL &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,"
            SQL &= " NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SQL &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ") "
            SQL &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA where (deptid not in (9009120) or Deptname not in ('Human Resources')) )BB  "
            SQL &= " where emplid in (select distinct emplid from ps_employees) )CC)DD where emplid not in (1764,1802,1969,2082,2232,2339,2321,2435,3456,1926,3139,2556,1345,2566,2116,1927,1758,2123,2536,3627,3292)  order by name"

        End If
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Dim attachment As String = "attachment; filename=All_Employees_Appraisal_Report.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/vnd.ms-excel"
        Dim tab1 As String = ""

        Response.Write("<table border=1><tr><td colspan=10 align=center><font size=5 color=blue><b>" & lblYEAR.Text & " All Employees Performance Appraisal details</b></font></td></tr><tr>")

        For Each dc As DataColumn In DT.Columns
            Response.Write("<td>")
            Response.Write("<b>" + tab1 + dc.ColumnName)
            tab1 = vbTab
        Next
        Response.Write(vbLf)

        Response.Write("</td></tr><tr>")

        'Dim i As Integer
        For Each dr As DataRow In DT.Rows
            tab1 = ""
            For i = 0 To DT.Columns.Count - 1
                Response.Write("<td>")
                Response.Write(tab1 & dr(i).ToString())
                tab1 = vbTab
            Next
            Response.Write(vbLf)

            Response.Write("</td></tr>")

        Next

        Response.[End]()

        Response.Write("</table>")
        'export to excel

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub MGT_Report_NEW()

        If lblGENERALIST_EMPLID.Text = 6785 Or lblGENERALIST_EMPLID.Text = 6250 Or lblGENERALIST_EMPLID.Text = 6088 Or lblGENERALIST_EMPLID.Text = 5294 Then
            SQL = "select  Name 'Employee Name',Status1 'Appraisal Status', /*GoalFormStatus 'Goals Status',MidPointStatus 'MidPoint Status',*/ Name#1 'Manager name',Name#2 'Second manager name', VP_Name 'VP Name',"
            SQL &= " HR_Name 'Generalist',Deptname 'Department',Final_Rate 'Overall Rating',Comments 'Employee Comments',DateEmpl_Refused1 'Employee Refuse to Sign'"

            SQL &= " from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
            SQL &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
            SQL &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
            SQL &= " select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then "
            SQL &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
            SQL &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,IsNull(convert(char(10),DateEmpl_Refused,101),'')DateEmpl_Refused1 from("

            SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
            SQL &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
            SQL &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

            SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SQL &= " /*8 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SQL &= " /*7 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SQL &= " /*6 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SQL &= " /*5 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SQL &= " /*4 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SQL &= " /*3 Manager*/select *,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

            SQL &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
            SQL &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
            SQL &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
            SQL &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
            SQL &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"

            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SQL &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

            SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
            SQL &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
            SQL &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
            SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SQL &= " /*8 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SQL &= " /*7 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SQL &= " /*6 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SQL &= " /*5 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SQL &= " /*4 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SQL &= " /*3 Manager*/select *,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

            SQL &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,"
            SQL &= " NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SQL &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ") "
            SQL &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA where SAP<>14)BB  "
            SQL &= " where emplid in (select distinct emplid from ps_employees) )CC)DD where emplid not in (1764,1802,1969,2082,2232,2339,2321,2435,3456,1926,3139,2556,1345,2566,2116,1927,1758,2123,2536,3627,3292)  order by name"

        Else
            SQL = "select  Name 'Employee Name',Status1 'Appraisal Status', /*GoalFormStatus 'Goals Status',MidPointStatus 'MidPoint Status',*/ Name#1 'Manager name',Name#2 'Second manager name', VP_Name 'VP Name',"
            SQL &= " HR_Name 'Generalist',Deptname 'Department',Final_Rate 'Overall Rating',Comments 'Employee Comments',DateEmpl_Refused1 'Employee Refuse to Sign'"
            SQL &= " from(select *, (case when Status1='Incomplete' and Process_flag=0 and StartFile>0 then 'Incomplete' else status1 end)Status2 from("
            SQL &= " select *,(select Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+ "
            SQL &= " Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact) from appraisal_master_tbl where emplid=bb.emplid and perf_year=" & lblYEAR.Text & ")StartFile from("
            SQL &= " select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then "
            SQL &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
            SQL &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1, ''MidPointStatus,IsNull(convert(char(10),DateEmpl_Refused,101),'')DateEmpl_Refused1 from("

            SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
            SQL &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
            SQL &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("

            SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SQL &= " /*8 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SQL &= " /*7 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SQL &= " /*6 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SQL &= " /*5 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SQL &= " /*4 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SQL &= " /*3 Manager*/select *,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 from("

            SQL &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,"
            SQL &= " DateEmpl_Refused,(case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR'"
            SQL &= " when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,(case when OverAll_Rating=1 then 'Unsatisfactory'"
            SQL &= " when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
            SQL &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"

            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SQL &= " from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"

            SQL &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
            SQL &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
            SQL &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from("
            SQL &= " /*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
            SQL &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("

            SQL &= " /*8 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#6))SAP#7,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#7))EMPLID#8 from("

            SQL &= " /*7 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#5))SAP#6,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#6))EMPLID#7 from("

            SQL &= " /*6 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#4))SAP#5,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#5))EMPLID#6 from("

            SQL &= " /*5 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#3))SAP#4,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#4))EMPLID#5 from("

            SQL &= " /*4 Manager*/select *,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=EMPLID#2))SAP#3,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#3))EMPLID#4 from("

            SQL &= " /*3 Manager*/select *,"
            SQL &= " IsNull((select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & "),(select distinct SUPERVISOR_emplid from hr_pds_data_tbl where emplid=EMPLID#2))EMPLID#3 "

            SQL &= " from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,"
            SQL &= " NULL DateEmpl_Refused,'N/A'Status1,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,UP_MGT_EMPLID EMPLID#2,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.MGT_EMPLID))SAP#1,"
            SQL &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),(select distinct sal_admin_plan from hr_pds_data_tbl where emplid=A.UP_MGT_EMPLID))SAP#2 "

            SQL &= " from appraisal_FutureGoals_master_tbl A  where Perf_Year=" & lblYEAR.Text + 1 & " and emplid not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ") "
            SQL &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA where (deptid not in (9009120) or Deptname not in ('Human Resources')) and SAP<>14)BB  "
            SQL &= " where emplid in (select distinct emplid from ps_employees) )CC)DD where emplid not in (1764,1802,1969,2082,2232,2339,2321,2435,3456,1926,3139,2556,1345,2566,2116,1927,1758,2123,2536,3627,3292)  order by name"

        End If
        'Response.Write(SQL) : Response.End()

        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Dim attachment As String = "attachment; filename=Manager_Appraisal_Report.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/vnd.ms-excel"
        Dim tab1 As String = ""

        Response.Write("<table border=1><tr><td colspan=10 align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Manager Performance Appraisal details</b></font></td></tr><tr>")

        For Each dc As DataColumn In DT.Columns
            Response.Write("<td>")
            Response.Write("<b>" + tab1 + dc.ColumnName)
            tab1 = vbTab
        Next
        Response.Write(vbLf)

        Response.Write("</td></tr><tr>")

        'Dim i As Integer
        For Each dr As DataRow In DT.Rows
            tab1 = ""
            For i = 0 To DT.Columns.Count - 1
                Response.Write("<td>")
                Response.Write(tab1 & dr(i).ToString())
                tab1 = vbTab
            Next
            Response.Write(vbLf)

            Response.Write("</td></tr>")

        Next

        Response.[End]()

        Response.Write("</table>")
        'export to excel

        LocalClass.CloseSQLServerConnection()
    End Sub

End Class