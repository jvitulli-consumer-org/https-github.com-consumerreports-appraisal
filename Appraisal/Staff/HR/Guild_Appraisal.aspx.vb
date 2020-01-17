Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Partial Public Class Guild_Appraisal
    Inherits System.Web.UI.Page

    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim Msg As String
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView

    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer


    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 1080)) + 20)

        If Session("NETID") = "" Then Response.Redirect("../../default.aspx")


        If Session("YEAR") > 2015 Then Response.Redirect("Guild_Appraisal1.aspx")


        lblEMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), _
                 "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")
        lblYEAR.Text = Session("YEAR")



        SQL4 = "select Process_Flag FLAG from GUILD_Appraisal_Master_tbl where emplid=" & lblEMPLID.Text & " and perf_year=" & lblYEAR.Text
        'Response.Write(SQL4) : Response.End()
        DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
        LocalClass.CloseSQLServerConnection()
        'Response.Write(SQL4) : Response.End()


        If DT4.Rows.Count > 0 Then

            If CDbl(DT4.Rows(0)("FLAG").ToString) = 5 Then

                If IsPostBack Then

                    MaintainScrollPositionOnPostBack = True
                    GridViewDisplay()

                    If CDbl(Len(Session("Guild_Review"))) < 6 Then
                        'Response.Write("HERE") : Response.End()
                        GLD_Submit.Visible = True
                    Else
                        GLD_Submit.Visible = False : GuildComments.Visible = False : LblGuildComments.Visible = True
                        LblGuild_Submitted.Text = "<font color=red><b>You have submitted your appraisal on " & Session("Guild_Review")
                        LblGuildComments.Text = Session("GuildComments")
                    End If

                Else
                    DisplayData()
                    GridViewDisplay()
                End If

            End If

            '--Guild information---
            SQL1 = "select (case when New_Employee=1 then 'SHORT' else 'FULL' end)Elig_Review,* from(Select Datediff(dd, convert(datetime,Hire_date),convert(datetime,'09/30/'+convert(char(4),Perf_Year)))Elig_Perf,"
            SQL1 &= " Perf_Year,Date_Sent Mgr_Apr,Date_Submitted_To_HR UP_Mgt_Apr,Date_HR_Approved HR_Apr,Date_Guild_Reviewed Guild_Review,Year(IsNull(Refuse_date,0))Refuse_date,Guild_Comments,"
            SQL1 &= " a.emplid,First+' '+Last Guild_Name,email,jobtitle1,Departname,convert(char(10),Hire_date,101)Hired,Left(convert(decimal,datediff(day,SERVICE_DATE, GetDate()))/365.25,5)"
            SQL1 &= " YearsCU," & lblYEAR.Text & "CY,SUP_EMPLID,(select First+' '+last from id_tbl where emplid=SUP_EMPLID)SUP_NAME,(select email from id_tbl where emplid=SUP_EMPLID)SUP_EMAIL,UP_MGT_EMPLID,"
            SQL1 &= " (case when len(Up_MGT_EMPLID)<>4  then (select First+' '+Last from id_tbl where emplid in (select supervisor_id from ps_employees where emplid=SUP_EMPLID))"
            SQL1 &= " else (select First+' '+last from id_tbl where sup_emplid=UP_MGT_EMPLID) end )UP_MGT_NAME, (select email from id_tbl where emplid=UP_MGT_EMPLID)UP_MGT_Email,HR_EMPLID,"
            SQL1 &= " (select First+' '+Last from id_tbl where emplid in ((case when len(HR_EMPLID)<>4 then (select top 1 HR_Generalist from HR_PDS_DATA_tbl where deptid=a.deptid1) else HR_EMPLID end)))HR_NAME,"
            SQL1 &= " (select email from id_tbl where emplid=HR_EMPLID)HR_email,Len(IsNull(HR_EMPLID,''))SEND_HR,Process_Flag,New_Employee from id_tbl a,Guild_Appraisal_MASTER_tbl b "
            SQL1 &= " where a.emplid=b.emplid and a.emplid=" & lblEMPLID.Text & ")A where Perf_Year=" & lblYEAR.Text
            'Response.Write(SQL1) ': Response.End()

            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            LocalClass.CloseSQLServerConnection()

            '--Eligible full review--
            LblEligible_Full_Review.Text = DT1.Rows(0)("Elig_Review").ToString

            If LblEligible_Full_Review.Text = "SHORT" Then
                Panel1.Visible = False : Panel11.Visible = False : Panel21.Visible = False : Panel22.Visible = False : Panel23.Visible = False
            End If

            '--Guild--
            Session("GUILD_NAME") = DT1.Rows(0)("Guild_Name").ToString
            Session("GUILD_Title") = DT1.Rows(0)("jobtitle1").ToString
            Session("GUILD_Dept") = DT1.Rows(0)("Departname").ToString
            Session("GUILD_Hired") = DT1.Rows(0)("Hired").ToString
            Session("YearsCU") = DT1.Rows(0)("YearsCU").ToString
            Session("GUILD_EMAIL") = DT1.Rows(0)("Email").ToString

            Session("Cur_Year") = DT1.Rows(0)("CY").ToString
            Session("Prev_Year") = DT1.Rows(0)("CY") - 1.ToString
            Session("Next_Year") = DT1.Rows(0)("CY") + 1.ToString

            '--First supervisor---
            Session("First_SUPID") = DT1.Rows(0)("SUP_EMPLID").ToString
            Session("First_SUP_EMAIL") = DT1.Rows(0)("SUP_EMAIL").ToString
            Session("First_SUP_NAME") = DT1.Rows(0)("SUP_NAME").ToString

            '--Second supevisor--
            Session("UP_MGT_EMPLID") = DT1.Rows(0)("UP_MGT_EMPLID").ToString
            Session("UP_MGT_NAME") = DT1.Rows(0)("UP_MGT_NAME").ToString
            Session("UP_MGT_EMAIL") = DT1.Rows(0)("UP_MGT_Email").ToString

            '--HR--
            Session("HR_EMPLID") = DT1.Rows(0)("HR_EMPLID").ToString
            Session("HR_NAME") = DT1.Rows(0)("HR_NAME").ToString
            Session("HR_EMAIL") = DT1.Rows(0)("HR_email").ToString


            'Session("Mgr_Apr") = DT1.Rows(0)("Mgr_Apr").ToString
            'Session("UP_Mgt_Apr") = DT1.Rows(0)("UP_Mgt_Apr").ToString
            'Session("Guild_Review") = DT1.Rows(0)("Guild_Review").ToString

            If Len(DT1.Rows(0)("Mgr_Apr").ToString) > 8 Then Session("Mgr_Apr") = DT1.Rows(0)("Mgr_Apr").ToString Else Session("Mgr_Apr") = ""
            If Len(DT1.Rows(0)("UP_Mgt_Apr").ToString) > 8 Then Session("UP_Mgt_Apr") = DT1.Rows(0)("UP_Mgt_Apr").ToString Else Session("UP_Mgt_Apr") = ""
            If Len(DT1.Rows(0)("HR_Apr").ToString) > 8 Then Session("HR_Apr") = DT1.Rows(0)("HR_Apr").ToString Else Session("HR_Apr") = ""
            If Len(DT1.Rows(0)("Guild_Review").ToString) > 8 Then Session("Guild_Review") = DT1.Rows(0)("Guild_Review").ToString Else Session("Guild_Review") = ""


            Session("GuildComments") = Replace(DT1.Rows(0)("Guild_Comments").ToString, Chr(13), "<br>")
            Session("Refuse_Date") = DT1.Rows(0)("Refuse_Date").ToString


            If CDbl(Len(Session("Guild_Review"))) < 6 Then
                'Response.Write("HERE") : Response.End()
                GLD_Submit.Visible = True
            ElseIf Session("Refuse_Date") > 1900 Then
                GLD_Submit.Visible = False : GuildComments.Visible = False : LblGuildComments.Visible = True
                LblGuildComments.Text = Session("GuildComments")
            Else
                GLD_Submit.Visible = False : GuildComments.Visible = False : LblGuildComments.Visible = True
                LblGuild_Submitted.Text = "<font color=red><b>You have submitted your appraisal on " & Session("Guild_Review")
                LblGuildComments.Text = Session("GuildComments")
            End If

            'Else
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('No Appraisal is waiting for Review.'); window.close(); </script>")

            'Else
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('No Appraisal is waiting for Review.'); window.close(); </script>")
        End If

        If Session("YEAR") > 2014 Then
            Panel23.Visible = False : FutureTask.Visible = False
        End If


    End Sub

    Protected Sub GridViewDisplay()

        SQL = "select IsNull(Max((case when indexid=1 then 1 else 0 end)),0)Index1,IsNull(Max((case when indexid=2 then 1 else 0 end)),0)Index2,"
        SQL &= " IsNull(Max((case when indexid=3 then 1 else 0 end)),0)Index3,IsNull(Max((case when indexid=4 then 1 else 0 end)),0)Index4,"
        SQL &= " IsNull(Max((case when indexid=5 then 1 else 0 end)),0)Index5,IsNull(Max((case when indexid=6 then 1 else 0 end)),0)Index6,"
        SQL &= " IsNull(Max((case when indexid=7 then 1 else 0 end)),0)Index7,IsNull(Max((case when indexid=8 then 1 else 0 end)),0)Index8,"
        SQL &= " IsNull(Max((case when indexid=9 then 1 else 0 end)),0)Index9,IsNull(Max((case when indexid=10 then 1 else 0 end)),0)Index10"
        SQL &= " from Guild_Appraisal_FACTORS_tbl where Perf_year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If DT.Rows(0)("Index1").ToString = 1 Then
            Panel1.Visible = True : GridView1.GridLines = GridLines.None
            SqlDataSource1.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=1 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
        End If

        If CDbl(DT.Rows(0)("Index2").ToString) = 1 Then
            Panel2.Visible = True : GridView2.GridLines = GridLines.None
            SqlDataSource3.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=2 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
            Div2.Visible = True
        End If

        If CDbl(DT.Rows(0)("Index3").ToString) = 1 Then
            Panel3.Visible = True : GridView3.GridLines = GridLines.None
            SqlDataSource3.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=3 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
            Div3.Visible = True
        End If

        If DT.Rows(0)("Index4").ToString = 1 Then
            Panel4.Visible = True : GridView4.GridLines = GridLines.None
            SqlDataSource4.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=4 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
            Div4.Visible = True
        End If

        If DT.Rows(0)("Index5").ToString = 1 Then
            Panel5.Visible = True : GridView5.GridLines = GridLines.None
            SqlDataSource5.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=5 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
            Div5.Visible = True
        End If

        If DT.Rows(0)("Index6").ToString = 1 Then
            Panel6.Visible = True : GridView6.GridLines = GridLines.None
            SqlDataSource6.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=6 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
            Div6.Visible = True
        End If

        If DT.Rows(0)("Index7").ToString = 1 Then
            Panel7.Visible = True : GridView7.GridLines = GridLines.None
            SqlDataSource7.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=7 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
            Div7.Visible = True
        End If

        If DT.Rows(0)("Index8").ToString = 1 Then
            Panel8.Visible = True : GridView8.GridLines = GridLines.None
            SqlDataSource8.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=8 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
            Div8.Visible = True
        End If
        If DT.Rows(0)("Index9").ToString = 1 Then
            Panel9.Visible = True : GridView9.GridLines = GridLines.None
            SqlDataSource9.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=9 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
            Div9.Visible = True
        End If

        If DT.Rows(0)("Index10").ToString = 1 Then
            Panel10.Visible = True : GridView10.GridLines = GridLines.None
            SqlDataSource10.SelectCommand = "select * from Guild_Appraisal_FACTORS_tbl where IndexID=10 and emplid=" & lblEMPLID.Text & " and Perf_year=" & lblYEAR.Text
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
        SQL5 &= " from Guild_Appraisal_FACTORSUMMARY_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " )A group by emplid,Perf_Year)A,(select IsNull(MGR_Comments,'')MGR_Comments,"
        SQL5 &= "IsNull(MGR_Improvement_Comments1,'')MGR_Improvement_Comments1,IsNull(MGR_Improvement_Comments2,'')MGR_Improvement_Comments2,IsNull(MGR_Improvement_Comments3,'')MGR_Improvement_Comments3,"
        SQL5 &= "(case when Overall_Rating=1 then 'Exceptional' when Overall_Rating=2 then 'High' when Overall_Rating=3 then 'Meets' when Overall_Rating=4 then 'Needs Improvement' when Overall_Rating=5 then"
        SQL5 &= "  'Unsatisfactory' else '' end)Rating11 from Guild_Appraisal_MASTER_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & ")B,"

        SQL5 &= " (select Max(F_TaskDesc1)F_TaskDesc1,Max(F_TaskDesc2)F_TaskDesc2,Max(F_TaskDesc3)F_TaskDesc3,Max(F_TaskDesc4)F_TaskDesc4,Max(F_TaskDesc5)F_TaskDesc5,Max(F_TaskDesc6)F_TaskDesc6,Max(F_TaskDesc7)"
        SQL5 &= " F_TaskDesc7,Max(F_TaskDesc8)F_TaskDesc8,Max(F_TaskDesc9)F_TaskDesc9,Max(F_TaskDesc10)F_TaskDesc10 from(select emplid,Perf_Year,(case when IndexID=1 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc1,"
        SQL5 &= " (case when IndexID=2 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc2,(case when IndexID=3 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc3,(case when IndexID=4 then LTrim(Future_TaskDesc)"
        SQL5 &= " else '' end)F_TaskDesc4,(case when IndexID=5 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc5,(case when IndexID=6 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc6,(case when IndexID=7 then"
        SQL5 &= " LTrim(Future_TaskDesc) else '' end)F_TaskDesc7,(case when IndexID=8 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc8,(case when IndexID=9 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc9,"
        SQL5 &= " (case when IndexID=10 then LTrim(Future_TaskDesc) else '' end)F_TaskDesc10 from Guild_FUTURE_FACTORS_TBL where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYEAR.Text & " )C )C"
        'Response.Write(SQL5) ': Response.End()

        DT5 = LocalClass.ExecuteSQLDataSet(SQL5)
        LocalClass.CloseSQLServerConnection()
        KeyTask1.Text = DT5.Rows(0)("TaskDesc1").ToString : KeyTask2.Text = DT5.Rows(0)("TaskDesc2").ToString : KeyTask3.Text = DT5.Rows(0)("TaskDesc3").ToString : KeyTask4.Text = DT5.Rows(0)("TaskDesc4").ToString
        KeyTask5.Text = DT5.Rows(0)("TaskDesc5").ToString : KeyTask6.Text = DT5.Rows(0)("TaskDesc6").ToString : KeyTask7.Text = DT5.Rows(0)("TaskDesc7").ToString : KeyTask8.Text = DT5.Rows(0)("TaskDesc8").ToString
        KeyTask9.Text = DT5.Rows(0)("TaskDesc9").ToString : KeyTask10.Text = DT5.Rows(0)("TaskDesc10").ToString

        'TaskComments1.Text = DT5.Rows(0)("Comments1").ToString : TaskComments2.Text = DT5.Rows(0)("Comments2").ToString : TaskComments3.Text = DT5.Rows(0)("Comments3").ToString
        'TaskComments4.Text = DT5.Rows(0)("Comments4").ToString : TaskComments5.Text = DT5.Rows(0)("Comments5").ToString : TaskComments6.Text = DT5.Rows(0)("Comments6").ToString
        'TaskComments7.Text = DT5.Rows(0)("Comments7").ToString : TaskComments8.Text = DT5.Rows(0)("Comments8").ToString : TaskComments9.Text = DT5.Rows(0)("Comments9").ToString
        'TaskComments10.Text = DT5.Rows(0)("Comments10").ToString : SuperComments.Text = DT5.Rows(0)("MGR_Comments").ToString : SuperComments1.Text = DT5.Rows(0)("MGR_Improvement_Comments1").ToString
        'SuperComments2.Text = DT5.Rows(0)("MGR_Improvement_Comments2").ToString : SuperComments3.Text = DT5.Rows(0)("MGR_Improvement_Comments3").ToString

        '---Format Label---------------------
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

        TaskRating1.Text = DT5.Rows(0)("Rating1").ToString : TaskRating2.Text = DT5.Rows(0)("Rating2").ToString : TaskRating3.Text = DT5.Rows(0)("Rating3").ToString
        TaskRating4.Text = DT5.Rows(0)("Rating4").ToString : TaskRating5.Text = DT5.Rows(0)("Rating5").ToString : TaskRating6.Text = DT5.Rows(0)("Rating6").ToString
        TaskRating7.Text = DT5.Rows(0)("Rating7").ToString : TaskRating8.Text = DT5.Rows(0)("Rating8").ToString : TaskRating9.Text = DT5.Rows(0)("Rating9").ToString
        TaskRating10.Text = DT5.Rows(0)("Rating10").ToString : TaskRating11.Text = DT5.Rows(0)("Rating11").ToString

        SQL6 = "select IsNull(Max((case when indexid=1 then 1 else 0 end)),0)F_Index1, IsNull(Max((case when indexid=2 then 1 else 0 end)),0)F_Index2,"
        SQL6 &= "IsNull(Max((case when indexid=3 then 1 else 0 end)),0)F_Index3,IsNull(Max((case when indexid=4 then 1 else 0 end)),0)F_Index4,"
        SQL6 &= "IsNull(Max((case when indexid=5 then 1 else 0 end)),0)F_Index5,IsNull(Max((case when indexid=6 then 1 else 0 end)),0)F_Index6, "
        SQL6 &= "IsNull(Max((case when indexid=7 then 1 else 0 end)),0)F_Index7,IsNull(Max((case when indexid=8 then 1 else 0 end)),0)F_Index8, "
        SQL6 &= "IsNull(Max((case when indexid=9 then 1 else 0 end)),0)F_Index9,IsNull(Max((case when indexid=10 then 1 else 0 end)),0)F_Index10 "
        SQL6 &= " from Guild_FUTURE_FACTORS_TBL where Perf_year=" & lblYEAR.Text & " and emplid=" & lblEMPLID.Text
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

    Protected Sub GLD_Submit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles GLD_Submit.Click

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class