Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Public Class Status_Report
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, x, y, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Dim DT, DT1, DT2, DT3, DT4 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", "840")

        If Session("MGT_EMPLID") = "" Then Response.Redirect("default.aspx")

        lblSAP.Text = Left(Trim(Request.QueryString("Token")), 1)
        lblYEAR.Text = Right(Trim(Request.QueryString("Token")), 4)

        SQL = "Select first+' '+last MGT_NAME from id_tbl where emplid=" & Session("MGT_EMPLID")
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        lblMgt_Name.Text = DT.Rows(0)("MGT_NAME").ToString


        If CDbl(lblSAP.Text) = 1 Then
            GridView1.Visible = True
            GridView2.Visible = False

            ALL_Employees_Report()

        Else
            GridView1.Visible = False
            GridView2.Visible = True
            ALL_Employees_Goal_Report()
        End If

        


    End Sub


    Protected Sub ALL_Employees_Report()
        Dim i As Integer

        SQL1 = "select count(*)Employees, status1 from(select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Sent to Second manager' when Process_Flag=2 then 'Sent to HR' when Process_Flag=3 then "
        SQL1 &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
        SQL1 &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1,convert(char(10),DateEmpl_Refused,101)DateEmpl_Refused1 from("
        SQL1 &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
        SQL1 &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 "
        SQL1 &= " in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in "
        SQL1 &= " (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from(/*8 Manager*/select *,IsNull((select SAP "
        SQL1 &= " from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from(/*7 Manager*/select *,"
        SQL1 &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from("
        SQL1 &= " /*6 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from("
        SQL1 &= " /*5 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & ")EMPLID#5 from("
        SQL1 &= " /*4 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from("
        SQL1 &= " /*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & ")EMPLID#3 from("
        SQL1 &= " /*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,DateEmpl_Refused,"

        'SQL1 &= " (case when process_flag=0 then 'Incompleted' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR' when process_flag=4 then 'Sent to Employee' when "
        'SQL1 &= " process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,"

        SQL1 &= " (case when process_flag=0 and Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+Confront_Challenge+Lead_Change+"
        SQL1 &= " Inspire_Risk+Leverage_External+Communic_Impact)=0 then 'Incomplete' when process_flag=0 and Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+"
        SQL1 &= " Promote_Collab +Confront_Challenge + Lead_Change +Inspire_Risk+Leverage_External+Communic_Impact)>0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 "
        SQL1 &= " then 'Reviewed by HR' when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,"

        SQL1 &= " (case when OverAll_Rating=1 then 'Unsatisfactory' when OverAll_Rating=2 then 'Developing/Improving Contributor' when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' "
        SQL1 &= " when OverAll_Rating=5 then 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2,"
        SQL1 &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ""
        SQL1 &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"
        SQL1 &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
        SQL1 &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select "
        SQL1 &= " (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
        SQL1 &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from(/*8 Manager*/select *,"
        SQL1 &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from("
        SQL1 &= " /*7 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from("
        SQL1 &= " /*6 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from("
        SQL1 &= " /*5 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & ")EMPLID#5 from("
        SQL1 &= " /*4 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from("
        SQL1 &= " /*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & ")EMPLID#3 from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select "
        SQL1 &= " convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,NULL DateEmpl_Refused,'N/A'Status1,"
        SQL1 &= " 'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2,"
        SQL1 &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text + 1 & " and emplid "
        SQL1 &= " not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ") /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA)BB "
        SQL1 &= " where emplid in (select distinct emplid from ps_employees) and (EMPLID#1 in (" & Session("MGT_EMPLID") & ") or EMPLID#2 in (" & Session("MGT_EMPLID") & ") or EMPLID#3 in (" & Session("MGT_EMPLID") & ") or EMPLID#4 in (" & Session("MGT_EMPLID") & ") "
        SQL1 &= " or EMPLID#5 in (" & Session("MGT_EMPLID") & ") or EMPLID#6 in (" & Session("MGT_EMPLID") & ") or EMPLID#7 in (" & Session("MGT_EMPLID") & ") or EMPLID#8 in (" & Session("MGT_EMPLID") & ")) group by status1"
        'Response.Write(SQL1) ': Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<center><table border=0 width=30%><tr><td align=center><b><font size=4px>Welcome " & lblMgt_Name.Text & "</b></td></tr><tr><td></td></tr></table>")

        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 bordercolor=#E7E8E3 width=20%><tr><td width=30%><b>Employees</b></td><td><b>Appraisal Status</b></td></tr>")
        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr></table")
        Next

        SqlDataSource1.SelectCommand = "select * from(select *,(select (case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' "
        SqlDataSource1.SelectCommand &= "  when Process_Flag=2 and perf_year<2018 then 'Sent to HR' when Process_Flag=2 and perf_year>=2018 then 'Waiting on Manager' when Process_Flag=3 then "
        SqlDataSource1.SelectCommand &= " 'Reviewed by HR' when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed' end) from appraisal_FutureGoals_master_tbl where emplid=AA.emplid "
        SqlDataSource1.SelectCommand &= " and perf_year=" & lblYEAR.Text + 1 & ")GoalFormStatus,(case when Len(Comments)>3 then 'YES' else '' end)comments1,convert(char(10),DateEmpl_Refused,101)DateEmpl_Refused1 from("
        SqlDataSource1.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red> term '+Term+'</font>'  else '' end)Name,"
        SqlDataSource1.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,(select last+','+first "
        SqlDataSource1.SelectCommand &= " from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 "
        SqlDataSource1.SelectCommand &= " in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in "
        SqlDataSource1.SelectCommand &= " (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from(/*8 Manager*/select *,IsNull((select SAP "
        SqlDataSource1.SelectCommand &= " from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from(/*7 Manager*/select *,"
        SqlDataSource1.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6  and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from("
        SqlDataSource1.SelectCommand &= " /*6 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from("
        SqlDataSource1.SelectCommand &= " /*5 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & ")EMPLID#5 from("
        SqlDataSource1.SelectCommand &= " /*4 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from("
        SqlDataSource1.SelectCommand &= " /*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & ")EMPLID#3 from(/*1-2 Managers*/select EMPLID,SAP,DEPTID,Department Deptname,JOBTITLE,(select "
        SqlDataSource1.SelectCommand &= " convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,Overall_Rating,comments,DateEmpl_Refused,"

        'SqlDataSource1.SelectCommand &= " (case when process_flag=0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 then 'Reviewed by HR' when process_flag=4 "
        'SqlDataSource1.SelectCommand &= " then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1,"

        SqlDataSource1.SelectCommand &= " (case when process_flag=0 and Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+Lead_Urgency+Promote_Collab+Confront_Challenge+Lead_Change+"
        SqlDataSource1.SelectCommand &= " Inspire_Risk+Leverage_External+Communic_Impact)=0 then 'Incomplete' when process_flag=0 and Len(Overall_Summary+Strengths+Development_Area+Development_Objective)+(Make_Balance+Build_Trust+Learn_Continuously+"
        SqlDataSource1.SelectCommand &= " Lead_Urgency +Promote_Collab+Confront_Challenge+Lead_Change+Inspire_Risk+Leverage_External+Communic_Impact)>0 then 'Incomplete' when process_flag=1 then 'Return to Manager' when process_flag=2 then 'Awaiting HR Review' when process_flag=3 "
        SqlDataSource1.SelectCommand &= " then 'Reviewed by HR' when process_flag=4 then 'Sent to Employee' when process_flag=5 and Len(DateEmpl_Refused)>6 then 'E-Signed by Manager' else 'E-Signed by Employee' end)Status1, "

        SqlDataSource1.SelectCommand &= " (case when OverAll_Rating=1 then 'Unsatisfactory' when OverAll_Rating=2 then 'Developing/Improving Contributor'"
        SqlDataSource1.SelectCommand &= " when OverAll_Rating=3 then 'Solid Contributor' when OverAll_Rating=4 then 'Very Strong Contributor' when OverAll_Rating=5 then "
        SqlDataSource1.SelectCommand &= " 'Distinguished Contributor' else '' end)Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2,"
        SqlDataSource1.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ""
        SqlDataSource1.SelectCommand &= " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/  UNION"
        SqlDataSource1.SelectCommand &= " /*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then '<font size=2 color=red>term '+Term+'</font>'  else '<font size=2 color=red> "
        SqlDataSource1.SelectCommand &= " New Emp</font>' end)Name,(select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2,"
        SqlDataSource1.SelectCommand &= " (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,(select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select "
        SqlDataSource1.SelectCommand &= " (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 when SAP#4 in (1,2,3,4) then EMPLID#4 "
        SqlDataSource1.SelectCommand &= " when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from(/*8 Manager*/select *,"
        SqlDataSource1.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from("
        SqlDataSource1.SelectCommand &= " /*7 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from("
        SqlDataSource1.SelectCommand &= " /*6 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from("
        SqlDataSource1.SelectCommand &= " /*5 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & ")EMPLID#5 from("
        SqlDataSource1.SelectCommand &= " /*4 Manager*/select *,IsNull((select SAP from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from("
        SqlDataSource1.SelectCommand &= " /*3 Manager*/select *,(select MGT_EMPLID from appraisal_master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & ")EMPLID#3 from(/*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,(select "
        SqlDataSource1.SelectCommand &= " convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,''Overall_Rating,Comments,NULL DateEmpl_Refused,'N/A'Status1,"
        SqlDataSource1.SelectCommand &= " 'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from appraisal_master_tbl where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2,"
        SqlDataSource1.SelectCommand &= " IsNull((select SAP from appraisal_master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text + 1 & " and emplid "
        SqlDataSource1.SelectCommand &= " not in (select emplid from appraisal_master_tbl A where perf_year=" & lblYEAR.Text & ") /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/)AA)BB "
        SqlDataSource1.SelectCommand &= " where emplid in (select distinct emplid from ps_employees) and (EMPLID#1 in (" & Session("MGT_EMPLID") & ") or EMPLID#2 in (" & Session("MGT_EMPLID") & ") or EMPLID#3 in (" & Session("MGT_EMPLID") & ") or EMPLID#4 in (" & Session("MGT_EMPLID") & ") "
        SqlDataSource1.SelectCommand &= " or EMPLID#5 in (" & Session("MGT_EMPLID") & ") or EMPLID#6 in (" & Session("MGT_EMPLID") & ") or EMPLID#7 in (" & Session("MGT_EMPLID") & ") or EMPLID#8 in (" & Session("MGT_EMPLID") & ")) order by name"
        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub ALL_Employees_Goal_Report()
        Dim i As Integer
        SQL1 = "select count(*)Employees, Status1 from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
        SQL1 &= " when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then 'term '+Term+'' else ' New Emp' end)Name,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,"
        SQL1 &= " (select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 "
        SQL1 &= " when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("
        SQL1 &= " /*8 Manager*/select *, IsNull((select SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from( "
        SQL1 &= " /*7 Manager*/select *,IsNull((select  SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from( "
        SQL1 &= " /*6 Manager*/select *,IsNull((select  SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
        SQL1 &= " /*5 Manager*/select *,IsNull((select  SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & ")EMPLID#5 from( "
        SQL1 &= " /*4 Manager*/select *,IsNull((select  SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from( "
        SQL1 &= " /*3 Manager*/select *,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & ")EMPLID#3 from("
        SQL1 &= " /*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,"
        SQL1 &= " (select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from Appraisal_FutureGoals_Master_tbl "
        SQL1 &= " where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2, IsNull((select SAP from Appraisal_FutureGoals_Master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 "
        SQL1 &= " from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/"
        SQL1 &= " )AA where EMPLID#1 in (" & Session("MGT_EMPLID") & ") or EMPLID#2 in (" & Session("MGT_EMPLID") & ") or EMPLID#3 in (" & Session("MGT_EMPLID") & ") or EMPLID#4 in (" & Session("MGT_EMPLID") & ") or "
        SQL1 &= " EMPLID#5 in (" & Session("MGT_EMPLID") & ") or EMPLID#6 in (" & Session("MGT_EMPLID") & ") or EMPLID#7 in (" & Session("MGT_EMPLID") & ") or EMPLID#8 in (" & Session("MGT_EMPLID") & "))BB group by Status1"
        'Response.Write(SQL1) ': Response.End()

        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<center><table border=0 width=30%><tr><td align=center><b><font size=4px>Welcome " & lblMgt_Name.Text & "</b></td></tr><tr><td></td></tr></table>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 bordercolor=#E7E8E3 width=20%><tr><td width=30%><b>Employees</b></td><td><b>Goals Status</b></td></tr>")
        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Employees").ToString & "</b></td><td>" & DT1.Rows(i)("Status1").ToString & "</b></td></tr></table")
        Next

        SqlDataSource2.SelectCommand = "select * from(select *,(case when Process_flag=0 then 'Incomplete' when Process_Flag=1 then 'Waiting on Manager' when Process_Flag=2 then 'Waiting on Manager' when Process_Flag=3 then 'Reviewed by HR' "
        SqlDataSource2.SelectCommand &= "  when Process_Flag=4 then 'Sent to Employee' when Process_Flag=5 then 'E-Signed by Employee' end)Status1 from(/*10 NAME*/select *,(select last+','+first from id_tbl where emplid=I.emplid)+(case when len(Term)>6 then 'term '+Term+'' else '' end)Name,"
        SqlDataSource2.SelectCommand &= "  (select last+','+first from id_tbl where emplid=I.emplid#1)Name#1,(select last+','+first from id_tbl where emplid=I.emplid#2) Name#2, (select last+','+first from id_tbl where emplid=I.vp_emplid)VP_Name,"
        SqlDataSource2.SelectCommand &= "  (select last+','+first from id_tbl where emplid=I.hr_emplid)HR_Name from(/*9 VP EMPLID*/select (case when SAP#1 in (1,2,3,4) then EMPLID#1 when SAP#2 in (1,2,3,4) then EMPLID#2 when SAP#3 in (1,2,3,4) then EMPLID#3 "
        SqlDataSource2.SelectCommand &= "  when SAP#4 in (1,2,3,4) then EMPLID#4 when SAP#5 in (1,2,3,4) then EMPLID#5 when SAP#6 in (1,2,3,4) then EMPLID#6 when SAP#7 in (1,2,3,4) then EMPLID#7 end )VP_EMPLID,* from("
        SqlDataSource2.SelectCommand &= "  /*8 Manager*/select *, IsNull((select SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & "),1)SAP#7,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#7 and perf_year=" & lblYEAR.Text & ")EMPLID#8 from( "
        SqlDataSource2.SelectCommand &= "  /*7 Manager*/select *,IsNull((select  SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & "),1)SAP#6,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#6 and perf_year=" & lblYEAR.Text & ")EMPLID#7 from( "
        SqlDataSource2.SelectCommand &= "  /*6 Manager*/select *,IsNull((select  SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & "),1)SAP#5,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#5 and perf_year=" & lblYEAR.Text & ")EMPLID#6 from( "
        SqlDataSource2.SelectCommand &= "  /*5 Manager*/select *,IsNull((select  SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & "),1)SAP#4,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#4 and perf_year=" & lblYEAR.Text & ")EMPLID#5 from( "
        SqlDataSource2.SelectCommand &= "  /*4 Manager*/select *,IsNull((select  SAP from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & "),1)SAP#3,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#3 and perf_year=" & lblYEAR.Text & ")EMPLID#4 from( "
        SqlDataSource2.SelectCommand &= "  /*3 Manager*/select *,(select MGT_EMPLID from Appraisal_FutureGoals_Master_tbl where emplid=EMPLID#2 and perf_year=" & lblYEAR.Text & ")EMPLID#3 from("
        SqlDataSource2.SelectCommand &= "  /*1-2 Managers*/select emplid,SAP,deptid,Department Deptname,JOBTITLE,"
        SqlDataSource2.SelectCommand &= "  (select convert(char(8),termination_date,1) from id_tbl where emplid=a.emplid)Term,HR_EMPLID,Process_Flag,'N/A'Final_Rate,BEN_ID,MGT_EMPLID EMPLID#1,IsNull((select SAP from Appraisal_FutureGoals_Master_tbl "
        SqlDataSource2.SelectCommand &= "  where emplid=A.MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#1,UP_MGT_EMPLID EMPLID#2, IsNull((select SAP from Appraisal_FutureGoals_Master_tbl where emplid=A.UP_MGT_EMPLID and perf_year=" & lblYEAR.Text & "),1)SAP#2 "
        SqlDataSource2.SelectCommand &= "  from appraisal_FutureGoals_master_tbl A where Perf_Year=" & lblYEAR.Text & " /*1END*/)B/*3END*/)C/*4END*/)D/*5END*/)E/*6END*/)F/*7END*/)G/*8END*/)H/*9END*/)I/*10END*/"
        SqlDataSource2.SelectCommand &= "  )AA where EMPLID#1 in (" & Session("MGT_EMPLID") & ") or EMPLID#2 in (" & Session("MGT_EMPLID") & ") or EMPLID#3 in (" & Session("MGT_EMPLID") & ") or EMPLID#4 in (" & Session("MGT_EMPLID") & ") or "
        SqlDataSource2.SelectCommand &= "  EMPLID#5 in (" & Session("MGT_EMPLID") & ") or EMPLID#6 in (" & Session("MGT_EMPLID") & ") or EMPLID#7 in (" & Session("MGT_EMPLID") & ") or EMPLID#8 in (" & Session("MGT_EMPLID") & "))BB "
        LocalClass.CloseSQLServerConnection()

    End Sub



End Class