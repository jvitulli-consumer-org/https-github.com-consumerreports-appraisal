Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page

Partial Public Class Guild_Waiting_Approval
    Inherits System.Web.UI.Page

    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim Msg, Msg1, Msq2, Msg3 As String
    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer
    Dim KeyTaskRows1, KeyTaskRows2, KeyTaskRows3, KeyTaskRows4, KeyTaskRows5, KeyTaskRows6, KeyTaskRows7, KeyTaskRows8, KeyTaskRows9, KeyTaskRows10, KeyTaskRows11, KeyTaskRows12, KeyTaskRows13, KeyTaskRows14 As Integer

    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Write(Request.QueryString("Token") & "   " & Session("NETID") & "   " & Session("YEAR")) : Response.End()

        lblEMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")

        'Response.Write(lblEMPLID.Text) : Response.End()

        Response.AddHeader("Refresh", "840")

        If Session("NETID") = "" Then Response.Redirect("default.aspx")

        If IsPostBack Then
            SetLevel_Approval()
        Else
            DisplayData()
            GridViewDisplay()
            Generalist_Name()
        End If

        SetDefault()
        SetLevel_Approval()

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub SetDefault()
        '--Hide gridview's borders--
        GridView1.GridLines = GridLines.None : GridView2.GridLines = GridLines.None : GridView3.GridLines = GridLines.None : GridView4.GridLines = GridLines.None
        GridView5.GridLines = GridLines.None : GridView6.GridLines = GridLines.None : GridView7.GridLines = GridLines.None : GridView8.GridLines = GridLines.None
        GridView9.GridLines = GridLines.None : GridView10.GridLines = GridLines.None

        SQL = "select emplid,process_flag from guild_appraisal_master_tbl where emplid=" & lblEMPLID.Text & " and perf_year =" & Session("YEAR")
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        'If DT.Rows(0)("process_flag").ToString = 0 Then
        'Button1.Visible = False : ImageButton1.Visible = True
        'Else
        'Button1.Visible = False : ImageButton1.Visible = True
        'End If

    End Sub

    Protected Sub SetLevel_Approval()

        '--Guild information---
        SQL1 = "select (case when New_Employee=1 then 'SHORT' else 'FULL' end)Elig_Review,* from (Select Datediff(dd, convert(datetime,Hire_date),convert(datetime,'09/30/'+convert(char(4),Perf_Year)))Elig_Perf,Date_Sent Mgr_Apr,"
        SQL1 &= " Date_Submitted_To_HR UP_Mgt_Apr,Date_HR_Approved HR_Apr,Year(IsNull(Date_Guild_Reviewed,0))Guild_Review,Year(IsNull(Refuse_Date,0))Refuse_Date,Date_Guild_Reviewed,Guild_Comments,a.emplid,First+' '+Last Guild_Name, "
        SQL1 &= " email,jobtitle1,Departname,convert(char(10),Hire_date,101)Hired,Left(convert(decimal,datediff(day,SERVICE_DATE,GetDate()))/365.25,5)YearsCU,Perf_Year CY,Perf_Year-1 PY,Perf_Year+1 NY,SUP_EMPLID, "
        SQL1 &= " (select First+' '+last from id_tbl where emplid=SUP_EMPLID)SUP_NAME,(select email from id_tbl where emplid=SUP_EMPLID)SUP_EMAIL,(case when Len(UP_MGT_EMPLID)=0  or UP_MGT_EMPLID is null then 0 else UP_MGT_EMPLID end)UP_MGT_EMPLID,"
        'SQL1 &= " (select First+' '+last from id_tbl where emplid=UP_MGT_EMPLID)UP_MGT_NAME,"
        SQL1 &= " (case when Len(UP_MGT_EMPLID)=0 then (select Substring(Replace(supervisor_name1,' ',''),CharIndex(',',supervisor_name1)+1,100) +' '+ Left(Replace(supervisor_name1,' ',''),CharIndex(',',supervisor_name1)-1)"
        SQL1 &= " from id_tbl where emplid=sup_emplid) else (select First+' '+last from id_tbl where emplid=UP_MGT_EMPLID) end)UP_MGT_NAME,"
        SQL1 &= " IsNull((select hr_generalist from hr_pds_data_tbl where emplid=a.emplid),0)HR_EMPLID, (select email from id_tbl where emplid=UP_MGT_EMPLID)UP_MGT_Email,"
        SQL1 &= " (select First+' '+last from id_tbl where emplid in (select hr_generalist from hr_pds_data_tbl where emplid=a.emplid))HR_NAME, "
        SQL1 &= " (select email from id_tbl where emplid in (select hr_generalist from hr_pds_data_tbl where emplid=a.emplid))HR_email,Len(IsNull(HR_EMPLID,''))SEND_HR,Process_Flag,New_Employee from id_tbl a,Guild_Appraisal_MASTER_tbl b "
        SQL1 &= " where a.emplid=b.emplid and a.emplid=" & lblEMPLID.Text & ")A where CY= " & Session("YEAR") & " "
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        '--Guild--
        lblGUILD_NAME.Text = DT1.Rows(0)("Guild_Name").ToString
        lblGUILD_Title.Text = DT1.Rows(0)("jobtitle1").ToString
        lblGUILD_Dept.Text = DT1.Rows(0)("Departname").ToString
        lblGUILD_Hired.Text = DT1.Rows(0)("Hired").ToString
        'lblYearsCU.Text = DT1.Rows(0)("YearsCU").ToString
        lblGUILD_EMAIL.Text = DT1.Rows(0)("Email").ToString
        '--Years--
        LblCur_Year.Text = DT1.Rows(0)("CY").ToString
        lblCur_Year1.Text = DT1.Rows(0)("CY").ToString
        LblPrev_Year.Text = DT1.Rows(0)("PY").ToString
        lblNext_Year.Text = DT1.Rows(0)("NY").ToString
        '--First supervisor--
        lblFirst_SUP_EMPLID.Text = DT1.Rows(0)("SUP_EMPLID").ToString
        lblFirst_SUP_NAME.Text = DT1.Rows(0)("SUP_NAME").ToString
        lblDate_First_SUP_Approved.Text = DT1.Rows(0)("Mgr_Apr").ToString
        lblFirst_SUP_EMAIL.Text = DT1.Rows(0)("SUP_EMAIL").ToString

        '--Second supevisor--
        lblUP_MGT_EMPLID.Text = DT1.Rows(0)("UP_MGT_EMPLID").ToString
        lblUP_MGT_NAME.Text = DT1.Rows(0)("UP_MGT_NAME").ToString
        lblUP_MGT_EMAIL.Text = DT1.Rows(0)("UP_MGT_Email").ToString
        Session("UP_MGR_EMPLID") = DT1.Rows(0)("UP_MGT_EMPLID").ToString

        '--HR Generalist--
        lblHR_NAME.Text = DT1.Rows(0)("HR_NAME").ToString
        lblHR_EMPLID.Text = DT1.Rows(0)("HR_EMPLID").ToString
        lblHR_EMAIL.Text = DT1.Rows(0)("HR_email").ToString
        Session("HR_EMPLID") = DT1.Rows(0)("HR_EMPLID").ToString

        If Len(DT1.Rows(0)("Date_Guild_Reviewed").ToString) < 5 Then
            Never_Completed.Text = "Never Completed"
        End If

        '--Eligible full review--
        LblEligible_Full_Review.Text = DT1.Rows(0)("Elig_Review").ToString


        '--Dynamic change UP_MGR label
        If DT1.Rows(0)("Process_Flag").ToString = 1 Then
            lblApproved_UP_MGR.Text = "Waiting Approval"
            lblApproved_UP_MGR.ForeColor = Drawing.Color.Blue
            lblApproved_First_SUP.Text = "Approved:"
        ElseIf DT1.Rows(0)("Process_Flag").ToString = 0 Then
            lblApproved_UP_MGR.Text = ""
            lblDate_First_SUP_Approved.Text = ""
            lblApproved_First_SUP.Text = ""
        Else
            lblApproved_UP_MGR.Text = "Approved:"
            lblApproved_First_SUP.Text = "Approved:"
        End If
        '--Dynamic change HR label 
        If DT1.Rows(0)("Process_Flag").ToString = 3 Then
            lblApproved_HR.Text = "Waiting Approval"
            lblApproved_HR.ForeColor = Drawing.Color.Blue
        ElseIf DT1.Rows(0)("Process_Flag").ToString = 4 Or DT1.Rows(0)("Process_Flag").ToString = 5 Then
            lblApproved_HR.Text = "Approved:"
            lblApproved_First_SUP.Text = "Approved:"
        End If


        If Len(DT1.Rows(0)("HR_Apr").ToString) < 6 Then lblDate_HR_Approved.Text = "" Else lblDate_HR_Approved.Text = DT1.Rows(0)("HR_Apr").ToString
        If Len(DT1.Rows(0)("UP_Mgt_Apr").ToString) < 6 Then lblDate_UP_Mgr_Approved.Text = "" Else lblDate_UP_Mgr_Approved.Text = DT1.Rows(0)("UP_Mgt_Apr").ToString

        Session("Guild_Review") = DT1.Rows(0)("Guild_Review").ToString
        Session("GuildComments") = DT1.Rows(0)("Guild_Comments").ToString


        If LblEligible_Full_Review.Text = "SHORT" Then
            Panel1.Visible = False : Panel11.Visible = False : Panel21.Visible = False : Panel22.Visible = False : Panel23.Visible = False
        End If

        If LblPrev_Year.Text > 2013 Then
            Panel23.Visible = False : FutureTask.Visible = False
        End If


        'Response.Write(CDbl(Session("MGT_EMPLID"))) : Response.End()

        If CDbl(Session("MGT_EMPLID")) = CDbl(lblUP_MGT_EMPLID.Text) Then
            Generalist.Visible = True : DDLGeneralist.Visible = True : Approve.Visible = False
            Generalist.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Generalist.Attributes.Add("onmouseout", "this.style.color='#000000'")
            Generalist.Text = "Submit for review to " & lblHR_NAME.Text & " "

        ElseIf CDbl(Session("MGT_EMPLID")) = CDbl(lblHR_EMPLID.Text) Then
            Approve.Visible = True : Generalist.Visible = False : DDLGeneralist.Visible = False
            Approve.Text = "Approve " & lblGUILD_NAME.Text & "'s Appraisal"

        ElseIf DT1.Rows(0)("Process_Flag").ToString = 4 Then
            SendToEmp.Visible = True
            'EditRecords.Visible = True
            EditRecords.Visible = False
            Generalist.Visible = False : DDLGeneralist.Visible = False : Approve.Visible = False
            Discuss.Visible = False : Disc_Com.Visible = False : DiscussionComments.Visible = False

        ElseIf DT1.Rows(0)("Process_Flag").ToString = 5 And DT1.Rows(0)("Guild_Review").ToString = 1900 Then
            'txtCal.Visible = True
            'imgCal.Visible = True
            'RefuseSign.Visible = True
            'EditRecords.Visible = True
            'lblRefuseText.Text = "If the Employee declined to esign their appraisal, please select the date you conducted the appraisal discussion. Then press the ""Employee Declined to Sign"" button to complete the process."
            EditRecords.Visible = False
            txtCal.Visible = False
            imgCal.Visible = False
            RefuseSign.Visible = False
            Generalist.Visible = False : DDLGeneralist.Visible = False : Approve.Visible = False
            Discuss.Visible = False : Disc_Com.Visible = False : DiscussionComments.Visible = False
        Else
            Generalist.Visible = False : DDLGeneralist.Visible = False : Approve.Visible = False
            Discuss.Visible = False : Disc_Com.Visible = False : DiscussionComments.Visible = False
        End If

        KeyTask2.BorderStyle = BorderStyle.None : TaskComments2.BorderStyle = BorderStyle.None
        KeyTask3.BorderStyle = BorderStyle.None : TaskComments3.BorderStyle = BorderStyle.None
        KeyTask4.BorderStyle = BorderStyle.None : TaskComments4.BorderStyle = BorderStyle.None
        KeyTask5.BorderStyle = BorderStyle.None : TaskComments5.BorderStyle = BorderStyle.None
        KeyTask6.BorderStyle = BorderStyle.None : TaskComments6.BorderStyle = BorderStyle.None
        KeyTask7.BorderStyle = BorderStyle.None : TaskComments7.BorderStyle = BorderStyle.None
        KeyTask8.BorderStyle = BorderStyle.None : TaskComments8.BorderStyle = BorderStyle.None
        KeyTask9.BorderStyle = BorderStyle.None : TaskComments9.BorderStyle = BorderStyle.None
        KeyTask10.BorderStyle = BorderStyle.None : TaskComments10.BorderStyle = BorderStyle.None

        'Mgr_Apr_Date.Text = DT1.Rows(0)("Mgr_Apr").ToString

        If DT1.Rows(0)("Guild_Review").ToString = 1900 Then

            LblGuild_Submitted.Visible = False : LblGuildComments.Visible = False

        ElseIf DT1.Rows(0)("Refuse_Date").ToString > 1900 Then
            LblGuild_Submitted.Text = "<font color=red><b>Employee declined to sign.</b></font>"
            If Len(DT1.Rows(0)("Guild_Comments").ToString) > 4 Then LblGuildComments.Text = "<b>Comments: </b>" & DT1.Rows(0)("Guild_Comments").ToString

        Else
            LblGuild_Submitted.Text = "<font color=red><b>" & lblGUILD_NAME.Text & " has submitted appraisal on " & DT1.Rows(0)("Date_Guild_Reviewed").ToString
            If Len(DT1.Rows(0)("Guild_Comments").ToString) > 4 Then LblGuildComments.Text = "<b>Comments: </b>" & DT1.Rows(0)("Guild_Comments").ToString
        End If
        Discuss.Attributes.Add("onMouseOver", "this.style.color='blue';this.style.cursor='pointer';") : Discuss.Attributes.Add("onmouseout", "this.style.color='#000000'")
        Disc_Com.Text = "Send suggested revisions to " & lblFirst_SUP_NAME.Text & ""
        Discuss.Text = "Send back to " & lblFirst_SUP_NAME.Text & " for revision"

    End Sub

    Protected Sub Generalist_Name()
        SQL = "SELECT * FROM Guild_Appraisal_GENERALIST_TBL order by name"
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        '2.Put them into Drop List
        DDLGeneralist.Items.Clear()

        DDLGeneralist.Items.Add(New ListItem("--Choose Generalist--", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLGeneralist.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
        Next

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub GridViewDisplay()
        SQL = "select IsNull(Max((case when indexid=1 then 1 else 0 end)),0)Index1, IsNull(Max((case when indexid=2 then 1 else 0 end)),0)Index2, "
        SQL &= " IsNull(Max((case when indexid=3 then 1 else 0 end)),0)Index3,IsNull(Max((case when indexid=4 then 1 else 0 end)),0)Index4, "
        SQL &= " IsNull(Max((case when indexid=5 then 1 else 0 end)),0)Index5,IsNull(Max((case when indexid=6 then 1 else 0 end)),0)Index6, "
        SQL &= " IsNull(Max((case when indexid=7 then 1 else 0 end)),0)Index7,IsNull(Max((case when indexid=8 then 1 else 0 end)),0)Index8, "
        SQL &= " IsNull(Max((case when indexid=9 then 1 else 0 end)),0)Index9,IsNull(Max((case when indexid=10 then 1 else 0 end)),0)Index10 "
        SQL &= " from Guild_Appraisal_FACTORS_tbl where Perf_year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If DT.Rows(0)("Index1").ToString = 1 Then
            Panel1.Visible = True
            SqlDataSource1.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=1 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            'Div1.Visible = True
        End If

        If CDbl(DT.Rows(0)("Index2").ToString) = 1 Then
            Panel2.Visible = True
            SqlDataSource3.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=2 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            Div2.Visible = True
        End If

        If CDbl(DT.Rows(0)("Index3").ToString) = 1 Then
            Panel3.Visible = True
            SqlDataSource3.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=3 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            Div3.Visible = True
        End If

        If DT.Rows(0)("Index4").ToString = 1 Then
            Panel4.Visible = True
            SqlDataSource4.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=4 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            Div4.Visible = True
        End If

        If DT.Rows(0)("Index5").ToString = 1 Then
            Panel5.Visible = True
            SqlDataSource5.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=5 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            Div5.Visible = True
        End If

        If DT.Rows(0)("Index6").ToString = 1 Then
            Panel6.Visible = True
            SqlDataSource6.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=6 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            Div6.Visible = True
        End If
        If DT.Rows(0)("Index7").ToString = 1 Then
            Panel7.Visible = True
            SqlDataSource7.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=7 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            Div7.Visible = True
        End If

        If DT.Rows(0)("Index8").ToString = 1 Then
            Panel8.Visible = True
            SqlDataSource8.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=8 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            Div8.Visible = True
        End If
        If DT.Rows(0)("Index9").ToString = 1 Then
            Panel9.Visible = True
            SqlDataSource9.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=9 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            Div9.Visible = True
        End If

        If DT.Rows(0)("Index10").ToString = 1 Then
            Panel10.Visible = True
            SqlDataSource10.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=10 and Perf_Year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
            Div10.Visible = True
        End If

    End Sub

    Protected Sub DisplayData()
        ' Save data and display 
        SQL5 = "select * from(select emplid,Perf_Year,Max(TaskDesc1)TaskDesc1,Max(TaskDesc2)TaskDesc2,Max(TaskDesc3)TaskDesc3,Max(TaskDesc4)TaskDesc4,Max(TaskDesc5)TaskDesc5,Max(TaskDesc6)TaskDesc6,"
        SQL5 &= "Max(TaskDesc7)TaskDesc7,Max(TaskDesc8)TaskDesc8,Max(TaskDesc9)TaskDesc9,Max(TaskDesc10)TaskDesc10,Max(Comments1)Comments1,Max(Comments2)Comments2,Max(Comments3)Comments3,Max(Comments4)Comments4,"
        SQL5 &= "Max(Comments5)Comments5,Max(Comments6)Comments6,Max(Comments7)Comments7,Max(Comments8)Comments8,Max(Comments9)Comments9,Max(Comments10)Comments10,"
        SQL5 &= "(case when Max(Rating1)=1 then 'Exceptional' when Max(Rating1)=2 then 'High' when Max(Rating1)=3 then 'Meets'  when Max(Rating1)=4 then 'Needs Improvement' when Max(Rating1)=5 then 'Unsatisfactory' else '' end)Rating1,"
        SQL5 &= "(case when Max(Rating2)=1 then 'Exceptional' when Max(Rating2)=2 then 'High' when Max(Rating2)=3 then 'Meets'  when Max(Rating2)=4 then 'Needs Improvement' when Max(Rating2)=5 then 'Unsatisfactory' else '' end)Rating2,"
        SQL5 &= "(case when Max(Rating3)=1 then 'Exceptional' when Max(Rating3)=2 then 'High' when Max(Rating3)=3 then 'Meets'  when Max(Rating3)=4 then 'Needs Improvement' when Max(Rating3)=5 then 'Unsatisfactory' else '' end)Rating3,"
        SQL5 &= "(case when Max(Rating4)=1 then 'Exceptional' when Max(Rating4)=2 then 'High' when Max(Rating4)=3 then 'Meets'  when Max(Rating4)=4 then 'Needs Improvement' when Max(Rating4)=5 then 'Unsatisfactory' else '' end)Rating4,"
        SQL5 &= "(case when Max(Rating5)=1 then 'Exceptional' when Max(Rating5)=2 then 'High' when Max(Rating5)=3 then 'Meets'  when Max(Rating5)=4 then 'Needs Improvement' when Max(Rating5)=5 then 'Unsatisfactory' else '' end)Rating5,"
        SQL5 &= "(case when Max(Rating6)=1 then 'Exceptional' when Max(Rating6)=2 then 'High' when Max(Rating6)=3 then 'Meets'  when Max(Rating6)=4 then 'Needs Improvement' when Max(Rating6)=5 then 'Unsatisfactory' else '' end)Rating6,"
        SQL5 &= "(case when Max(Rating7)=1 then 'Exceptional' when Max(Rating7)=2 then 'High' when Max(Rating7)=3 then 'Meets'  when Max(Rating7)=4 then 'Needs Improvement' when Max(Rating7)=5 then 'Unsatisfactory' else '' end)Rating7,"
        SQL5 &= "(case when Max(Rating8)=1 then 'Exceptional' when Max(Rating8)=2 then 'High' when Max(Rating8)=3 then 'Meets' when Max(Rating8)=4 then 'Needs Improvement' when Max(Rating8)=5 then 'Unsatisfactory' else '' end)Rating8,"
        SQL5 &= "(case when Max(Rating9)=1 then 'Exceptional' when Max(Rating9)=2 then 'High' when Max(Rating9)=3 then 'Meets'  when Max(Rating9)=4 then 'Needs Improvement' when Max(Rating9)=5 then 'Unsatisfactory' else '' end)Rating9,"
        SQL5 &= "(case when Max(Rating10)=1 then 'Exceptional' when Max(Rating10)=2 then 'High' when Max(Rating10)=3 then 'Meets'  when Max(Rating10)=4 then 'Needs Improvement' when Max(Rating10)=5 then 'Unsatisfactory' else '' end)Rating10"
        SQL5 &= " from(select emplid,Perf_Year,"
        SQL5 &= "(case when IndexID=1 then LTrim(TaskDesc) else '' end)TaskDesc1,(case when IndexID=2 then LTrim(TaskDesc) else '' end)TaskDesc2, (case when IndexID=3 then LTrim(TaskDesc) else '' end)TaskDesc3,"
        SQL5 &= "(case when IndexID=4 then LTrim(TaskDesc) else '' end)TaskDesc4,(case when IndexID=5 then LTrim(TaskDesc) else '' end)TaskDesc5,(case when IndexID=6 then LTrim(TaskDesc) else '' end)TaskDesc6,"
        SQL5 &= "(case when IndexID=7 then LTrim(TaskDesc) else '' end)TaskDesc7,(case when IndexID=8 then LTrim(TaskDesc) else '' end)TaskDesc8,(case when IndexID=9 then LTrim(TaskDesc) else '' end)TaskDesc9,"
        SQL5 &= "(case when IndexID=10 then LTrim(TaskDesc) else '' end)TaskDesc10,(case when IndexID=1 then LTrim(Comments) else '' end)Comments1,(case when IndexID=2 then LTrim(Comments) else '' end)Comments2,"
        SQL5 &= "(case when IndexID=3 then LTrim(Comments) else '' end)Comments3,(case when IndexID=4 then LTrim(Comments) else '' end)Comments4,(case when IndexID=5 then LTrim(Comments) else '' end)Comments5,"
        SQL5 &= "(case when IndexID=6 then LTrim(Comments) else '' end)Comments6,(case when IndexID=7 then LTrim(Comments) else '' end)Comments7,(case when IndexID=8 then LTrim(Comments) else '' end)Comments8,"
        SQL5 &= "(case when IndexID=9 then LTrim(Comments) else '' end)Comments9, (case when IndexID=10 then LTrim(Comments) else '' end)Comments10,"
        SQL5 &= "(case when IndexID=1 then LTrim(TaskRatings) else '' end)Rating1,(case when IndexID=2 then LTrim(TaskRatings) else '' end)Rating2,(case when IndexID=3 then LTrim(TaskRatings) else '' end)Rating3,"
        SQL5 &= "(case when IndexID=4 then LTrim(TaskRatings) else '' end)Rating4,(case when IndexID=5 then LTrim(TaskRatings) else '' end)Rating5,(case when IndexID=6 then LTrim(TaskRatings) else '' end)Rating6,"
        SQL5 &= "(case when IndexID=7 then LTrim(TaskRatings) else '' end)Rating7,(case when IndexID=8 then LTrim(TaskRatings) else '' end)Rating8,(case when IndexID=9 then LTrim(TaskRatings) else '' end)Rating9,"
        SQL5 &= "(case when IndexID=10 then LTrim(TaskRatings) else '' end)Rating10"
        SQL5 &= " from Guild_Appraisal_FACTORSUMMARY_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & Session("YEAR") & " )A group by emplid,Perf_Year)A,(select IsNull(MGR_Comments,'')MGR_Comments,"
        SQL5 &= "IsNull(MGR_Improvement_Comments1,'')MGR_Improvement_Comments1,IsNull(MGR_Improvement_Comments2,'')MGR_Improvement_Comments2,IsNull(MGR_Improvement_Comments3,'')MGR_Improvement_Comments3,"
        SQL5 &= "(case when Overall_Rating=1 then 'Exceptional' when Overall_Rating=2 then 'High' when Overall_Rating=3 then 'Meets' when Overall_Rating=4 then 'Needs Improvement' when Overall_Rating=5 then"
        SQL5 &= " 'Unsatisfactory' else '' end)Rating11 from Guild_Appraisal_MASTER_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & Session("YEAR") & ")B, "
        SQL5 &= " (select Max(F_TaskDesc1)F_TaskDesc1,Max(F_TaskDesc2)F_TaskDesc2,Max(F_TaskDesc3)F_TaskDesc3,Max(F_TaskDesc4)F_TaskDesc4,Max(F_TaskDesc5)F_TaskDesc5,Max(F_TaskDesc6)F_TaskDesc6,Max(F_TaskDesc7)"
        SQL5 &= " F_TaskDesc7,Max(F_TaskDesc8)F_TaskDesc8,Max(F_TaskDesc9)F_TaskDesc9,Max(F_TaskDesc10)F_TaskDesc10 from(select emplid,Perf_Year,(case when IndexID=1 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc1,"
        SQL5 &= " (case when IndexID=2 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc2,(case when IndexID=3 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc3,(case when IndexID=4 then LTrim(Future_TaskDesc)"
        SQL5 &= " else '' end)F_TaskDesc4,(case when IndexID=5 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc5,(case when IndexID=6 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc6,(case when IndexID=7 then"
        SQL5 &= " LTrim(Future_TaskDesc) else '' end)F_TaskDesc7,(case when IndexID=8 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc8,(case when IndexID=9 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc9,"
        SQL5 &= " (case when IndexID=10 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc10 from Guild_FUTURE_FACTORS_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & Session("YEAR") & " )C )C"
        'Response.Write(SQL5)
        DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
        LocalClass.CloseSQLServerConnection()

        KeyTask1.Text = Replace(Replace(DT5.Rows(0)("TaskDesc1").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        KeyTask2.Text = Replace(Replace(DT5.Rows(0)("TaskDesc2").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        KeyTask3.Text = Replace(Replace(DT5.Rows(0)("TaskDesc3").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        KeyTask4.Text = Replace(Replace(DT5.Rows(0)("TaskDesc4").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        KeyTask5.Text = Replace(Replace(DT5.Rows(0)("TaskDesc5").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        KeyTask6.Text = Replace(Replace(DT5.Rows(0)("TaskDesc6").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        KeyTask7.Text = Replace(Replace(DT5.Rows(0)("TaskDesc7").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        KeyTask8.Text = Replace(Replace(DT5.Rows(0)("TaskDesc8").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        KeyTask9.Text = Replace(Replace(DT5.Rows(0)("TaskDesc9").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        KeyTask10.Text = Replace(Replace(DT5.Rows(0)("TaskDesc10").ToString, Chr(13), "<p>"), Chr(10), "<p>")

        TaskComments1.Text = Replace(Replace(DT5.Rows(0)("Comments1").ToString, Chr(13), "<p>"), Chr(10), "<p>")

        TaskComments2.Text = Replace(Replace(DT5.Rows(0)("Comments2").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        TaskComments3.Text = Replace(Replace(DT5.Rows(0)("Comments3").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        TaskComments4.Text = Replace(Replace(DT5.Rows(0)("Comments4").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        TaskComments5.Text = Replace(Replace(DT5.Rows(0)("Comments5").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        TaskComments6.Text = Replace(Replace(DT5.Rows(0)("Comments6").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        TaskComments7.Text = Replace(Replace(DT5.Rows(0)("Comments7").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        TaskComments8.Text = Replace(Replace(DT5.Rows(0)("Comments8").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        TaskComments9.Text = Replace(Replace(DT5.Rows(0)("Comments9").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        TaskComments10.Text = Replace(Replace(DT5.Rows(0)("Comments10").ToString, Chr(13), "<p>"), Chr(10), "<p>")

        SuperComments.Text = Replace(Replace(DT5.Rows(0)("MGR_Comments").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        SuperComments1.Text = Replace(Replace(DT5.Rows(0)("MGR_Improvement_Comments1").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        SuperComments2.Text = Replace(Replace(DT5.Rows(0)("MGR_Improvement_Comments2").ToString, Chr(13), "<p>"), Chr(10), "<p>")
        SuperComments3.Text = Replace(Replace(DT5.Rows(0)("MGR_Improvement_Comments3").ToString, Chr(13), "<p>"), Chr(10), "<p>")

        '=====================================================================================================================================================================

        TaskRating1.Text = DT5.Rows(0)("Rating1").ToString : TaskRating2.Text = DT5.Rows(0)("Rating2").ToString : TaskRating3.Text = DT5.Rows(0)("Rating3").ToString : TaskRating4.Text = DT5.Rows(0)("Rating4").ToString
        TaskRating5.Text = DT5.Rows(0)("Rating5").ToString : TaskRating6.Text = DT5.Rows(0)("Rating6").ToString : TaskRating7.Text = DT5.Rows(0)("Rating7").ToString : TaskRating8.Text = DT5.Rows(0)("Rating8").ToString
        TaskRating9.Text = DT5.Rows(0)("Rating9").ToString : TaskRating10.Text = DT5.Rows(0)("Rating10").ToString : TaskRating11.Text = DT5.Rows(0)("Rating11").ToString

        SQL6 = "select IsNull(Max((case when indexid=1 then 1 else 0 end)),0)F_Index1, IsNull(Max((case when indexid=2 then 1 else 0 end)),0)F_Index2,"
        SQL6 &= "IsNull(Max((case when indexid=3 then 1 else 0 end)),0)F_Index3,IsNull(Max((case when indexid=4 then 1 else 0 end)),0)F_Index4,"
        SQL6 &= "IsNull(Max((case when indexid=5 then 1 else 0 end)),0)F_Index5,IsNull(Max((case when indexid=6 then 1 else 0 end)),0)F_Index6, "
        SQL6 &= "IsNull(Max((case when indexid=7 then 1 else 0 end)),0)F_Index7,IsNull(Max((case when indexid=8 then 1 else 0 end)),0)F_Index8, "
        SQL6 &= "IsNull(Max((case when indexid=9 then 1 else 0 end)),0)F_Index9,IsNull(Max((case when indexid=10 then 1 else 0 end)),0)F_Index10 "
        SQL6 &= " from Guild_FUTURE_FACTORS_TBL where Perf_year=" & Session("YEAR") & " and emplid=" & lblEMPLID.Text
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)

        FutKeyTask1.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc1").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask1.BorderStyle = BorderStyle.None : FutKeyTask1.BackColor = Drawing.Color.White

        If CDbl(DT6.Rows(0)("F_Index2").ToString) > 0 Then Panel12.Visible = True : FutKeyTask2.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc2").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask2.BorderStyle = BorderStyle.None : FutKeyTask2.BackColor = Drawing.Color.White Else Panel12.Visible = False
        If CDbl(DT6.Rows(0)("F_Index3").ToString) > 0 Then Panel13.Visible = True : FutKeyTask3.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc3").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask3.BorderStyle = BorderStyle.None : FutKeyTask3.BackColor = Drawing.Color.White Else Panel13.Visible = False
        If CDbl(DT6.Rows(0)("F_Index4").ToString) > 0 Then Panel14.Visible = True : FutKeyTask4.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc4").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask4.BorderStyle = BorderStyle.None : FutKeyTask4.BackColor = Drawing.Color.White Else Panel14.Visible = False
        If CDbl(DT6.Rows(0)("F_Index5").ToString) > 0 Then Panel15.Visible = True : FutKeyTask5.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc5").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask5.BorderStyle = BorderStyle.None : FutKeyTask5.BackColor = Drawing.Color.White Else Panel15.Visible = False
        If CDbl(DT6.Rows(0)("F_Index6").ToString) > 0 Then Panel16.Visible = True : FutKeyTask6.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc6").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask6.BorderStyle = BorderStyle.None : FutKeyTask6.BackColor = Drawing.Color.White Else Panel16.Visible = False
        If CDbl(DT6.Rows(0)("F_Index7").ToString) > 0 Then Panel17.Visible = True : FutKeyTask7.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc7").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask7.BorderStyle = BorderStyle.None : FutKeyTask7.BackColor = Drawing.Color.White Else Panel17.Visible = False
        If CDbl(DT6.Rows(0)("F_Index8").ToString) > 0 Then Panel18.Visible = True : FutKeyTask8.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc8").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask8.BorderStyle = BorderStyle.None : FutKeyTask8.BackColor = Drawing.Color.White Else Panel18.Visible = False
        If CDbl(DT6.Rows(0)("F_Index9").ToString) > 0 Then Panel19.Visible = True : FutKeyTask9.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc9").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask9.BorderStyle = BorderStyle.None : FutKeyTask9.BackColor = Drawing.Color.White Else Panel19.Visible = False
        If CDbl(DT6.Rows(0)("F_Index10").ToString) > 0 Then Panel20.Visible = True : FutKeyTask10.Text = Replace(Replace(DT5.Rows(0)("F_TaskDesc10").ToString, Chr(13), "<p>"), Chr(10), "<p>") : FutKeyTask10.BorderStyle = BorderStyle.None : FutKeyTask10.BackColor = Drawing.Color.White Else Panel20.Visible = False


        LocalClass.CloseSQLServerConnection()

    End Sub


    Protected Sub Generalist_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Generalist.Click
        'Response.Write(lblHR_EMAIL.Text) : Response.End()

        SQL1 = "update GUILD_Appraisal_Master_tbl Set Date_Submitted_To_HR='" & Now & "',Process_Flag=3,HR_EMPLID=" & lblHR_EMPLID.Text & ""
        SQL1 &= " where emplid=" & lblEMPLID.Text & " and Perf_Year=" & Session("YEAR")
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        LocalClass.CloseSQLServerConnection()

        Msg = lblGUILD_NAME.Text & "'s Performance Appraisal has been completed by " & lblFirst_SUP_NAME.Text & " and approved by " & lblUP_MGT_NAME.Text & "<br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details."


            '--Production email
        '(lblHR_EMAIL.Text, "Guild Performance Appraisal was sent to you for Approval by " & Session("NAME"), Msg)

        Response.Redirect("Guild.aspx")


    End Sub

    Protected Sub Discuss_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Discuss.Click

        If Len(DiscussionComments.Text) < 5 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please type discussion comments'); </script>")
        Else

            SQL = "update Guild_Appraisal_MASTER_tbl set Process_Flag=0,UP_MGT_EMPLID='',HR_EMPLID='',Date_Submitted_To_HR=NULL,Date_HR_Approved=NULL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & Session("YEAR")
            SQL &= " insert into Guild_Appraisal_Discussion_tbl (EMPLID,Perf_Year,MGR_EMPLID,REJ_EMPLID,DateTime,Comments)"
            SQL &= " values(" & lblEMPLID.Text & ",'" & Session("YEAR") & "'," & lblFirst_SUP_EMPLID.Text & "," & Session("MGT_EMPLID") & ",'" & Now & "','" & Replace(DiscussionComments.Text, "'", "`") & "')"

            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()

            Msg = Session("NAME") & " has rejected " & lblGUILD_NAME.Text & "'s Performance Appraisal for the following reason:<br><br>"
            Msg &= Replace(DiscussionComments.Text, "'", "`") & " <br><br>"
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details"

            Msg1 = "The Guild Performance Appraisal you had sent to " & lblFirst_SUP_NAME.Text & " regarding employee " & lblGUILD_NAME.Text & " reads as follows:<br>"
            Msg1 &= "<b> " & Replace(DiscussionComments.Text, "'", "`") & " "

            '--Production email
            Try

                '(lblFirst_SUP_EMAIL.Text, "Guild Performance Appraisal was Rejected by " & Session("NAME"), Msg)

                If CDbl(lblHR_EMPLID.Text) > 0 Then
                    '(lblHR_EMAIL.Text, "Guild Performance Appraisal - Revise comments", Msg1)
                End If

            Catch ex As Exception

            End Try

            Response.Redirect("Guild.aspx")

        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles ImageButton1.Click

        If Session("SAP") = "GLD" Then
            Response.Redirect("..\..\Default_Appaisal.aspx")
        Else
            Response.Redirect("..\..\Default_Manager.aspx")
        End If

    End Sub

    Protected Sub Approve_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Approve.Click

        SQL = "update Guild_Appraisal_MASTER_tbl set Process_Flag=4,Date_HR_Approved='" & Now & "' where emplid=" & lblEMPLID.Text & " and Perf_Year=" & Session("YEAR")

        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        Msg = lblGUILD_NAME.Text & "'s Performance Appraisal has been Approved by " & Session("NAME") & "<br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details<br>"

        '--Production email
        '(lblFirst_SUP_EMAIL.Text, "Guild Performance Appraisal was Reviewed by HR", Msg)

        Response.Redirect("Guild.aspx")

    End Sub


    Protected Sub SendToEmp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SendToEmp.Click


        SQL = "update Guild_Appraisal_MASTER_tbl set Process_Flag=5 where emplid=" & lblEMPLID.Text & " and Perf_Year=" & Session("YEAR")
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        Msg = "Your performance appraisal has been completed by your manager and is ready for your review.<br><br>"
        Msg &= "<b>NOTE: Please do not electronically sign and submit the review until you have had the Appraisal/Ratings Criteria discussion with your manager."
        Msg &= "Your electronic signature only confirms that you have had the discussion, not that you necessarily agree with the contents of the appraisal.</b><br><br>"
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Guild_Performance_Appraisal/Default.aspx for full details<br>"

        '--Production email 
        '(lblGUILD_EMAIL.Text, "Your Performance Appraisal is ready for your review", Msg)

        Response.Redirect("Guild.aspx")

    End Sub

    Protected Sub EditRecords_Click(ByVal sender As Object, ByVal e As EventArgs) Handles EditRecords.Click

        SQL = " update Guild_Appraisal_MASTER_tbl set Process_Flag=0,UP_MGT_EMPLID='',HR_EMPLID='',Date_Submitted_To_HR=NULL,Date_HR_Approved=NULL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & Session("YEAR")
        SQL &= " Insert into Guild_Appraisal_Discussion_tbl values('" & lblEMPLID.Text & "','" & Session("YEAR") & "','" & lblFirst_SUP_EMPLID.Text & "','" & lblFirst_SUP_EMPLID.Text & "','" & Now & "','Edit')"
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        'Response.Redirect("Guild_Performance.aspx?Token=" & Request.QueryString("Token"))
    End Sub

    Protected Sub RefuseSign_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RefuseSign.Click

        If IsDate(txtCal.Text) = False Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose a valid calendar date.'); </script>")
            txtCal.Text = ""
        ElseIf Len(txtCal.Text) < 9 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose a valid calendar date'); </script>")
            txtCal.Text = ""
        ElseIf Len(txtCal.Text) > 10 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose a valid calendar date.'); </script>")
            txtCal.Text = ""
        Else
            SQL = "update guild_appraisal_master_tbl Set Date_Guild_Reviewed='" & txtCal.Text & "', Refuse_Date='" & txtCal.Text & "',"
            SQL &= "Guild_Comments='Meeting was held on " & txtCal.Text & "' where emplid=" & lblEMPLID.Text & "  and Perf_Year=" & Session("YEAR")
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()
            Response.Redirect("Guild_Waiting_Approval.aspx?Token=" & Request.QueryString("Token"))
            'Response.Write("Date " & txtCal.Text & "<br>Len " & Len(txtCal.Text) & "<br> IsDate " & IsDate(txtCal.Text)) : Response.End()
        End If

    End Sub

    Private Sub Calendar1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged

        'Response.Write(Today > = Calendar1.SelectedDate)
        If Today < Calendar1.SelectedDate Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Future date not permitted.'); </script>")
            txtCal.Text = ""
        Else
            Calendar1.Visible = False
            txtCal.Text = Calendar1.SelectedDate.ToShortDateString()

            Dim div As System.Web.UI.Control = Page.FindControl("divCalendar")
            If TypeOf div Is HtmlGenericControl Then
                CType(div, HtmlGenericControl).Style.Add("display", "none")


            End If
        End If

    End Sub

    Protected Sub imgCal_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles imgCal.Click
        Calendar1.Visible = True
    End Sub

    Protected Sub txtCal_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCal.TextChanged
        Calendar1.Visible = True
    End Sub

End Class