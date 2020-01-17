Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Imports System.Configuration
Imports System.Web.Configuration
Public Class Goals_Print
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8, SQL9, SQL10, SQL11, SQL14, SQL12, SQL13, SQL_Flag, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6, DT7, DT8, DT9, DT10, DT11, DT14, DT_Flag As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim Msg, Msg1, Subj, x, x1 As String
    Dim EMPLID As String
    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Len(Session("NETID")) = 0 Then Response.Redirect("..\..\default.aspx")
        'Response.Write("netid " & Session("NETID") & "netid " & Len(Session("NETID")))

        Response.AddHeader("Refresh", "840")

        lblEMPLID.Text = Mid(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9"), 5, 4)
        lblLogin_EMPLID.Text = Trim(Session("MGT_EMPLID"))

        lblYear.Text = Left(Request.QueryString("Token"), 4)
        lblWindowBatch.Text = Mid(Request.QueryString("Token"), 9)
        lblDataBaseBatch.Text = Session("Window_batch")

        'Response.Write("Empl logon : " & Session("EMPLID_LOGON") & "<br>Mgt Logon :  " & lblLogin_EMPLID.Text & "<br>Emp Emplid: " & lblEMPLID.Text & "<br>" & CDbl(Session("Window_batch")) & "<br>" & CDbl(lblWindowBatch.Text)) : Response.End()

        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display") ': Response.End()
        Else
            'Response.Write("<br>2. else Save Data before Display") ': Response.End()
            PersonalInfo()
            DisplayData()

        End If


    End Sub

    Protected Sub PersonalInfo()
        SQL1 = "select * from(select Goals,Milestones,TargetDate from Appraisal_FutureGoals_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & " and IndexID=1)A,"
        SQL1 &= " (select (select count(*) from Appraisal_FutureGoals_Master_tbl A where mgt_emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & ")Manager,*,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.emplid)Empl_Name,(select first+' '+last from id_tbl where emplid=a.MGT_EMPLID)MGT_Name,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.HR_EMPLID)HR_Name,"
        SQL1 &= " (select convert(char(10),hire_Date,101) from id_tbl where emplid=a.emplid)Empl_Hired,(select email from id_tbl where emplid=a.emplid)Empl_Email,"
        SQL1 &= " (select email from id_tbl where emplid=a.MGT_EMPLID)MGT_Email,(select email from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Email,"
        SQL1 &= " (select email from id_tbl where emplid=a.HR_EMPLID)HR_Email from Appraisal_FutureGoals_Master_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & ")B"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        lblEmpl_NAME.Text = DT1.Rows(0)("Empl_Name").ToString
        lblEmpl_TITLE.Text = DT1.Rows(0)("jobtitle").ToString
        lblEmpl_DEPT.Text = DT1.Rows(0)("Department").ToString
        lblEmpl_HIRE.Text = DT1.Rows(0)("Empl_Hired").ToString
        lblEmpl_EMAIL.Text = DT1.Rows(0)("empl_email").ToString
        lblFlag.Text = DT1.Rows(0)("Process_Flag").ToString
        '--First Supervisor--
        lblFirst_SUP_EMPLID.Text = DT1.Rows(0)("MGT_emplid").ToString
        LblMGT_NAME.Text = DT1.Rows(0)("MGT_Name").ToString
        lblFirst_SUP_EMAIL.Text = DT1.Rows(0)("MGT_Email").ToString
        '--Second Supervisor--
        lblUP_MGT_EMPLID.Text = DT1.Rows(0)("UP_MGT_emplid").ToString
        lblUP_MGT_NAME.Text = DT1.Rows(0)("UP_MGT_Name").ToString
        lblUP_MGT_EMAIL.Text = DT1.Rows(0)("UP_MGT_email").ToString
        '--HR Generalist--
        'lblHR_NAME.Text = DT1.Rows(0)("HR_Name").ToString
        lblHR_EMPLID.Text = DT1.Rows(0)("HR_EMPLID").ToString
        lblHR_EMAIL.Text = DT1.Rows(0)("HR_email").ToString
        '--Goal Years--
        Goal_Year.Text = Trim(Right(lblYear.Text, 2))
        Goal_Year1.Text = Trim(Right(lblYear.Text - 1, 2))
        Goal_Year2.Text = Trim(Right(lblYear.Text, 2))

        LocalClass.CloseSQLServerConnection()

    End Sub


    Protected Sub ShowHideGoalPanel()

        SQL6 = " select Max(F_IND1)F_IND1,Max(F_IND2)F_IND2,Max(F_IND3)F_IND3,Max(F_IND4)F_IND4,Max(F_IND5)F_IND5,Max(F_IND6)F_IND6,Max(F_IND7)F_IND7,Max(F_IND8)F_IND8,Max(F_IND9)F_IND9,Max(F_IND10)F_IND10 "
        SQL6 &= " from(select (case when IndexID=1 then count(*) else 0 end)F_IND1,(case when IndexID=2 then count(*) else 0 end)F_IND2,(case when IndexID=3 then count(*) else 0 end)F_IND3,"
        SQL6 &= " (case when IndexID=4 then count(*) else 0 end)F_IND4,(case when IndexID=5 then count(*) else 0 end)F_IND5,(case when IndexID=6 then count(*) else 0 end)F_IND6,(case when IndexID=7 then count(*) else 0 end)F_IND7,"
        SQL6 &= " (case when IndexID=8 then count(*) else 0 end)F_IND8,(case when IndexID=9 then count(*) else 0 end)F_IND9,(case when IndexID=10 then count(*) else 0 end)F_IND10"
        SQL6 &= " from Appraisal_FutureGoals_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " group by IndexID)A "
        'Response.Write(SQL6) : Response.End()
        DT6 = LocalClass.ExecuteSQLDataSet(SQL6)
        If CDbl(DT6.Rows(0)("F_IND2").ToString) > 0 Then Panel_FutureGoal_Review2.Visible = True Else Panel_FutureGoal_Review2.Visible = False
        If CDbl(DT6.Rows(0)("F_IND3").ToString) > 0 Then Panel_FutureGoal_Review3.Visible = True Else Panel_FutureGoal_Review3.Visible = False
        If CDbl(DT6.Rows(0)("F_IND4").ToString) > 0 Then Panel_FutureGoal_Review4.Visible = True Else Panel_FutureGoal_Review4.Visible = False
        If CDbl(DT6.Rows(0)("F_IND5").ToString) > 0 Then Panel_FutureGoal_Review5.Visible = True Else Panel_FutureGoal_Review5.Visible = False
        If CDbl(DT6.Rows(0)("F_IND6").ToString) > 0 Then Panel_FutureGoal_Review6.Visible = True Else Panel_FutureGoal_Review6.Visible = False
        If CDbl(DT6.Rows(0)("F_IND7").ToString) > 0 Then Panel_FutureGoal_Review7.Visible = True Else Panel_FutureGoal_Review7.Visible = False
        If CDbl(DT6.Rows(0)("F_IND8").ToString) > 0 Then Panel_FutureGoal_Review8.Visible = True Else Panel_FutureGoal_Review8.Visible = False
        If CDbl(DT6.Rows(0)("F_IND9").ToString) > 0 Then Panel_FutureGoal_Review9.Visible = True Else Panel_FutureGoal_Review9.Visible = False
        If CDbl(DT6.Rows(0)("F_IND10").ToString) > 0 Then Panel_FutureGoal_Review10.Visible = True Else Panel_FutureGoal_Review10.Visible = False

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub DisplayData()
        'Response.Write("Goal Form View " & LblEmpl_Type.Text)
        ShowHideGoalPanel()

        SQL = "select A.*,"
        SQL &= " IsNull(Goals1,'')Goals1,IsNull(Milestones1,'')Milestones1,IsNull(TargetDate1,'')TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2,IsNull(Goals3,'')Goals3,"
        SQL &= " IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4,IsNull(Goals5,'')Goals5,"
        SQL &= " IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6,IsNull(Goals7,'')Goals7,"
        SQL &= " IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,IsNull(TargetDate8,'')TargetDate8,IsNull(Goals9,'')Goals9,"
        SQL &= " IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10 from (select emplid,mgt_emplid,"
        SQL &= " Process_Flag,DateEmpl_Appr,(select first+' '+last from id_tbl where emplid=hr_emplid)HR_Name,hr_emplid,Rtrim(IsNull(Comments,''))Comments from Appraisal_FutureGoals_Master_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")A Left Join (select MGT_Emplid,emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1)A1 on a.emplid=a1.emplid Left Join "
        SQL &= " (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2)B on a.emplid=b.emplid Left Join "
        SQL &= " (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3)C on a.emplid=c.emplid Left Join "
        SQL &= " (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4)D on a.emplid=d.emplid Left Join "
        SQL &= " (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5)E on a.emplid=e.emplid Left Join "
        SQL &= " (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6)F on a.emplid=f.emplid Left Join "
        SQL &= " (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7)G on a.emplid=g.emplid Left Join "
        SQL &= " (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8)H on a.emplid=h.emplid Left Join "
        SQL &= " (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9)N on a.emplid=n.emplid Left Join "
        SQL &= " (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 from Appraisal_FutureGoals_tbl where "
        SQL &= " Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & "  and IndexID =10)K on A.emplid=K.emplid"
        'Response.Write(SQL) ': Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '--Dispaly Data on Labels
        FUT_Goal_Edit11.Text = Replace(Replace(DT.Rows(0)("Goals1").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit11.Text = Replace(Replace(DT.Rows(0)("Milestones1").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit11.Text = Replace(Replace(DT.Rows(0)("TargetDate1").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Goal_Edit12.Text = Replace(Replace(DT.Rows(0)("Goals2").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit12.Text = Replace(Replace(DT.Rows(0)("Milestones2").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit12.Text = Replace(Replace(DT.Rows(0)("TargetDate2").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Goal_Edit13.Text = Replace(Replace(DT.Rows(0)("Goals3").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit13.Text = Replace(Replace(DT.Rows(0)("Milestones3").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit13.Text = Replace(Replace(DT.Rows(0)("TargetDate3").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Goal_Edit14.Text = Replace(Replace(DT.Rows(0)("Goals4").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit14.Text = Replace(Replace(DT.Rows(0)("Milestones4").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit14.Text = Replace(Replace(DT.Rows(0)("TargetDate4").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Goal_Edit15.Text = Replace(Replace(DT.Rows(0)("Goals5").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit15.Text = Replace(Replace(DT.Rows(0)("Milestones5").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit15.Text = Replace(Replace(DT.Rows(0)("TargetDate5").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Goal_Edit16.Text = Replace(Replace(DT.Rows(0)("Goals6").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit16.Text = Replace(Replace(DT.Rows(0)("Milestones6").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit16.Text = Replace(Replace(DT.Rows(0)("TargetDate6").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Goal_Edit17.Text = Replace(Replace(DT.Rows(0)("Goals7").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit17.Text = Replace(Replace(DT.Rows(0)("Milestones7").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit17.Text = Replace(Replace(DT.Rows(0)("TargetDate7").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Goal_Edit18.Text = Replace(Replace(DT.Rows(0)("Goals8").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit18.Text = Replace(Replace(DT.Rows(0)("Milestones8").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit18.Text = Replace(Replace(DT.Rows(0)("TargetDate8").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Goal_Edit19.Text = Replace(Replace(DT.Rows(0)("Goals9").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit19.Text = Replace(Replace(DT.Rows(0)("Milestones9").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit19.Text = Replace(Replace(DT.Rows(0)("TargetDate9").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Goal_Edit20.Text = Replace(Replace(DT.Rows(0)("Goals10").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Succ_Edit20.Text = Replace(Replace(DT.Rows(0)("Milestones10").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        FUT_Date_Edit20.Text = Replace(Replace(DT.Rows(0)("TargetDate10").ToString, Chr(13), "<span>"), Chr(10), "<br/>")
        '--Dispaly Index2--
        If Len(DT.Rows(0)("Goals2").ToString) + Len(DT.Rows(0)("Milestones2").ToString) + Len(DT.Rows(0)("TargetDate2").ToString) > 2 Then Panel_FutureGoal_Review2.Visible = True Else Panel_FutureGoal_Review2.Visible = False
        '--Dispaly Index3--
        If Len(DT.Rows(0)("Goals3").ToString) + Len(DT.Rows(0)("Milestones3").ToString) + Len(DT.Rows(0)("TargetDate3").ToString) > 2 Then Panel_FutureGoal_Review3.Visible = True Else Panel_FutureGoal_Review3.Visible = False
        '--Dispaly Index4--
        If Len(DT.Rows(0)("Goals4").ToString) + Len(DT.Rows(0)("Milestones4").ToString) + Len(DT.Rows(0)("TargetDate4").ToString) > 2 Then Panel_FutureGoal_Review4.Visible = True Else Panel_FutureGoal_Review4.Visible = False
        '--Dispaly Index5--
        If Len(DT.Rows(0)("Goals5").ToString) + Len(DT.Rows(0)("Milestones5").ToString) + Len(DT.Rows(0)("TargetDate5").ToString) > 2 Then Panel_FutureGoal_Review5.Visible = True Else Panel_FutureGoal_Review5.Visible = False
        '--Dispaly Index6--
        If Len(DT.Rows(0)("Goals6").ToString) + Len(DT.Rows(0)("Milestones6").ToString) + Len(DT.Rows(0)("TargetDate6").ToString) > 2 Then Panel_FutureGoal_Review6.Visible = True Else Panel_FutureGoal_Review6.Visible = False
        '--Dispaly Index7--
        If Len(DT.Rows(0)("Goals7").ToString) + Len(DT.Rows(0)("Milestones7").ToString) + Len(DT.Rows(0)("TargetDate7").ToString) > 2 Then Panel_FutureGoal_Review7.Visible = True Else Panel_FutureGoal_Review7.Visible = False
        '--Dispaly Index8--
        If Len(DT.Rows(0)("Goals8").ToString) + Len(DT.Rows(0)("Milestones8").ToString) + Len(DT.Rows(0)("TargetDate8").ToString) > 2 Then Panel_FutureGoal_Review8.Visible = True Else Panel_FutureGoal_Review8.Visible = False
        '--Dispaly Index9--
        If Len(DT.Rows(0)("Goals9").ToString) + Len(DT.Rows(0)("Milestones9").ToString) + Len(DT.Rows(0)("TargetDate9").ToString) > 2 Then Panel_FutureGoal_Review9.Visible = True Else Panel_FutureGoal_Review9.Visible = False
        '--Dispaly Index10--
        If Len(DT.Rows(0)("Goals10").ToString) + Len(DT.Rows(0)("Milestones10").ToString) + Len(DT.Rows(0)("TargetDate10").ToString) > 2 Then Panel_FutureGoal_Review10.Visible = True Else Panel_FutureGoal_Review10.Visible = False

        
        If CDbl(DT.Rows(0)("Process_Flag").ToString) = 0 Then
            LblEMP_Appr.Text = "<font color=blue>Create Goals</font>"
            LblMGT_Appr.Text = ""
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 1 Then
            LblEMP_Appr.Text = "<font color=blue>Edit Goals</font>"
            LblMGT_Appr.Text = ""
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 2 Then
            LblEMP_Appr.Text = "<font color=blue>Send to Manager</font>"
            LblMGT_Appr.Text = ""
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 3 Then
            LblEMP_Appr.Text = "<font color=blue>Revise Goals</font>"
        ElseIf CDbl(DT.Rows(0)("Process_Flag").ToString) = 5 Then
            LblEMP_Appr.Text = "<font color=blue>Approved</font>"
            LblMGT_Appr.Text = DT.Rows(0)("DateEmpl_Appr").ToString

        End If

        LocalClass.CloseSQLServerConnection()
    End Sub

End Class