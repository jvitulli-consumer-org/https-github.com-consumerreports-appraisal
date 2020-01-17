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
Imports System.Drawing
Imports System.Drawing.Printing


Public Class FutureGoals_Print
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8, SQL9, SQL10, SQL11, SQL14, SQL1_1, SQL2_1, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6, DT7, DT8, DT9, DT10, DT11, DT14, DT1_1 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim Msg, Msg1, Subj, x, x1 As String
    Dim EMPLID As String
    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", "840")

        lblEMPLID.Text = Mid(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9"), 5, 4)
        lblLogin_EMPLID.Text = Trim(Session("MGT_EMPLID"))
        lblYear.Text = Left(Request.QueryString("Token"), 4)
        lblWindowBatch.Text = Mid(Request.QueryString("Token"), 9)
        lblDataBaseBatch.Text = Session("Window_batch")
        'Response.Write("Empl logon : " & Session("EMPLID_LOGON") & "<br>Mgt Logon :  " & lblLogin_EMPLID.Text & "<br>Emp Emplid: " & lblEMPLID.Text & "<>" & CDbl(Session("Window_batch")) & "<>" & CDbl(lblWindowBatch.Text)) ': Response.End()

        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display") ': Response.End()
        Else
            'Response.Write("<br>2. else Save Data before Display") ': Response.End()
            DisplayData()
        End If

        SQL1 = "select count(*)CNT from Appraisal_FutureGoal_Recall_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and Len(DateEmpl_Appr)>5 and Len(Recall_Date)>5"
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
        If CDbl(DT1.Rows(0)("CNT").ToString) > 0 Then
            Panel_Goals_Log.Visible = True
            Goals_Log()
        Else
            Panel_Goals_Log.Visible = False
        End If

        SetLevel_Approval()

    End Sub

    Protected Sub SetLevel_Approval()
        SQL1 = "select * from(select Goals,Milestones,TargetDate from Appraisal_FutureGoals_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & " and IndexID=1)A,"
        SQL1 &= " (select (select count(*) from Appraisal_FutureGoals_Master_tbl A where mgt_emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & ")Manager,*,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.emplid)Empl_Name,(select first+' '+last from id_tbl where emplid=a.MGT_EMPLID)MGT_Name,"
        SQL1 &= " (select first+' '+last from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Name,(select first+' '+last from id_tbl where emplid=a.HR_EMPLID)HR_Name,"
        SQL1 &= " (select convert(char(10),hire_Date,101) from id_tbl where emplid=a.emplid)Empl_Hired,(select email from id_tbl where emplid=a.emplid)Empl_Email,"
        SQL1 &= " (select email from id_tbl where emplid=a.MGT_EMPLID)MGT_Email,(select email from id_tbl where emplid=a.UP_MGT_EMPLID)UP_MGT_Email,"
        SQL1 &= " (select email from id_tbl where emplid=a.HR_EMPLID)HR_Email "
        SQL1 &= " from Appraisal_FutureGoals_Master_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & lblYear.Text & ")B,"
        If Len(lblLogin_EMPLID.Text) = 0 Then
            SQL1 &= " (select 0 Login_Mgt_Emp)C"
        Else
            SQL1 &= " (select " & Trim(lblEMPLID.Text) & " Login_Mgt_Emp)C"
        End If
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        lblEmpl_NAME.Text = DT1.Rows(0)("Empl_Name").ToString : lblEmpl_TITLE.Text = DT1.Rows(0)("jobtitle").ToString : lblEmpl_DEPT.Text = DT1.Rows(0)("Department").ToString : lblEmpl_HIRE.Text = DT1.Rows(0)("Empl_Hired").ToString
        lblEmpl_EMAIL.Text = DT1.Rows(0)("empl_email").ToString : lblFlag.Text = DT1.Rows(0)("Process_Flag").ToString
        '--First Supervisor--
        lblFirst_SUP_EMPLID.Text = DT1.Rows(0)("MGT_emplid").ToString : LblMGT_NAME.Text = DT1.Rows(0)("MGT_Name").ToString : lblFirst_SUP_EMAIL.Text = DT1.Rows(0)("MGT_Email").ToString
        '--Second Supervisor--
        lblUP_MGT_EMPLID.Text = DT1.Rows(0)("UP_MGT_emplid").ToString : lblUP_MGT_NAME.Text = DT1.Rows(0)("UP_MGT_Name").ToString : lblUP_MGT_EMAIL.Text = DT1.Rows(0)("UP_MGT_email").ToString
        '--HR Generalist--
        lblHR_NAME.Text = DT1.Rows(0)("HR_Name").ToString : lblHR_EMPLID.Text = DT1.Rows(0)("HR_EMPLID").ToString : lblHR_EMAIL.Text = DT1.Rows(0)("HR_email").ToString
        '--Goal Years--
        Goal_Year.Text = Trim(Right(lblYear.Text, 2)) : Goal_Year1.Text = Trim(Right(lblYear.Text - 1, 2)) : Goal_Year2.Text = Trim(Right(lblYear.Text, 2))

        'Response.Write("1.Empl " & lblEMPLID.Text & "<br>2.Emp_Logon  " & Session("EMPLID_LOGON") & "<br>3.Mgr  " & lblFirst_SUP_EMPLID.Text & "<br>4.Logon  " & lblLogin_EMPLID.Text) ': Response.End()

        If lblFlag.Text = 1 Then
            If lblEMPLID.Text = Session("EMPLID_LOGON") Then 'Employee waiting for approval
                Sub_Empl_Review.Visible = True : Sub_Empl_Review.ForeColor = Drawing.Color.Red
                Sub_Empl_Review.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString & " " & lblEmpl_NAME.Text & "  submitted goal(s) to " & LblMGT_NAME.Text & " for review, edit and approved."
            ElseIf lblEMPLID.Text <> Session("EMPLID_LOGON") Then ' Manager review, edit and approval
                Sub_Empl_Review.Visible = True : Sub_Empl_Review.ForeColor = Drawing.Color.Red
                Sub_Empl_Review.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString & " " & lblEmpl_NAME.Text & "  submitted goal(s) to " & LblMGT_NAME.Text & " for review, edit or approved."
            End If
        End If

        If lblFlag.Text = 2 Then
            If lblEMPLID.Text = Session("EMPLID_LOGON") Then 'Employee waiting for approval
                Sub_Empl_Review.Visible = True : Sub_Empl_Review.ForeColor = Drawing.Color.Red
                Sub_Empl_Review.Text = DT1.Rows(0)("DateEmpl_Appr").ToString & " " & lblEmpl_NAME.Text & "  submitted goal(s) to " & LblMGT_NAME.Text & " for review, edit and approved."
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
            ElseIf lblFirst_SUP_EMPLID.Text = lblLogin_EMPLID.Text Then ' Manager review, edit and approval
                Sub_Empl_Review.Visible = True : Sub_Empl_Review.ForeColor = Drawing.Color.Red
                Sub_Empl_Review.Text = DT1.Rows(0)("DateEmpl_Appr").ToString & " " & lblEmpl_NAME.Text & "  submitted goal(s) to " & LblMGT_NAME.Text & " for review, edit and approved."
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
            ElseIf lblEMPLID.Text = lblLogin_EMPLID.Text Then 'Employee waiting for approval (mgr)
                Sub_Empl_Review.Visible = True : Sub_Empl_Review.ForeColor = Drawing.Color.Red
                Sub_Empl_Review.Text = DT1.Rows(0)("DateEmpl_Appr").ToString & " " & lblEmpl_NAME.Text & "  submitted goal(s) to " & LblMGT_NAME.Text & " for review, edit and approved."
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
            End If
        End If

        If lblFlag.Text = 4 Then
            If Len(lblLogin_EMPLID.Text) <> 0 Then '--Manager waiting for approval
                'Response.Write("1. No submit button" & DT1.Rows(0)("CNT_InAppr").ToString & "<br>" & DT1.Rows(0)("MGT_emplid").ToString & "<br>" & lblEMPLID.Text)
                LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString
                Sub_Empl_Review.Visible = True : Sub_Empl_Review.Text = "Submitted for Review on " & DT1.Rows(0)("DateSUB_Empl").ToString
                LblEMP_Appr.Text = "Waiting Approval" : LblEMP_Appr.ForeColor = Drawing.Color.Blue
            ElseIf Len(lblLogin_EMPLID.Text) = 0 Then
                'Response.Write("2. No submit button")
                LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString
                LblEMP_Appr.Text = "Waiting Approval" : LblEMP_Appr.ForeColor = Drawing.Color.Blue
            End If
        End If

        If lblFlag.Text = 5 Then
            '--Employee review and confirm--
            If Len(lblLogin_EMPLID.Text) = 0 Then ' Employee's view
                LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
                If Session("GoalUpdated") = 1 Then
                Else
                    Sub_Empl_Review.Text = "Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString
                    LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
                End If
                '--Manager after Employee Approved--
            ElseIf Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) <> 0) Then ' Manager's view
                LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
                Sub_Empl_Review.Visible = True
                If Session("GoalUpdated") = 1 Then
                    Sub_Empl_Review.Text = "Waiting Confirmation from " & lblEmpl_NAME.Text : Sub_Empl_Review.Font.Size = 14 : Panel_Goals_Log.Visible = True
                Else
                    LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
                End If
            ElseIf Len(lblLogin_EMPLID.Text) <> 0 And (CDbl(lblLogin_EMPLID.Text) - CDbl(DT1.Rows(0)("emplid").ToString) = 0) Then ' manager's view his goals
                LblMGT_Appr.Text = DT1.Rows(0)("DateSUB_UP_MGT").ToString : Sub_Empl_Review.Visible = True
                LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
                If Session("GoalUpdated") = 1 Then
                Else
                    Sub_Empl_Review.Text = "Reviewed and Confirmed on " & DT1.Rows(0)("DateEmpl_Appr").ToString
                    LblEMP_Appr.Text = DT1.Rows(0)("DateEmpl_Appr").ToString
                End If
            End If
        End If

        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub DisplayData()

        ShowHideGoalPanel()

        SQL = "select A.*,"
        SQL &= " IsNull(Goals1,'')Goals1,IsNull(Milestones1,'')Milestones1,IsNull(TargetDate1,'')TargetDate1,IsNull(Goals2,'')Goals2,IsNull(Milestones2,'')Milestones2,IsNull(TargetDate2,'')TargetDate2,"
        SQL &= " IsNull(Goals3,'')Goals3,IsNull(Milestones3,'')Milestones3,IsNull(TargetDate3,'')TargetDate3,IsNull(Goals4,'')Goals4,IsNull(Milestones4,'')Milestones4,IsNull(TargetDate4,'')TargetDate4,"
        SQL &= " IsNull(Goals5,'')Goals5,IsNull(Milestones5,'')Milestones5,IsNull(TargetDate5,'')TargetDate5,IsNull(Goals6,'')Goals6,IsNull(Milestones6,'')Milestones6,IsNull(TargetDate6,'')TargetDate6,"
        SQL &= " IsNull(Goals7,'')Goals7,IsNull(Milestones7,'')Milestones7,IsNull(TargetDate7,'')TargetDate7,IsNull(Goals8,'')Goals8,IsNull(Milestones8,'')Milestones8,IsNull(TargetDate8,'')TargetDate8,"
        SQL &= " IsNull(Goals9,'')Goals9,IsNull(Milestones9,'')Milestones9,IsNull(TargetDate9,'')TargetDate9,IsNull(Goals10,'')Goals10,IsNull(Milestones10,'')Milestones10,IsNull(TargetDate10,'')TargetDate10"
        SQL &= " from (select emplid,mgt_emplid,(select first+' '+last from id_tbl where emplid=hr_emplid)HR_Name,hr_emplid,"
        SQL &= " IsNull(Comments,'')Comments from Appraisal_FutureGoals_Master_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & ")A "
        SQL &= " Left Join (select MGT_Emplid,emplid,Goals Goals1,Milestones Milestones1,TargetDate TargetDate1 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=1)A1 on a.emplid=a1.emplid"
        SQL &= " Left Join (select emplid,Goals Goals2,Milestones Milestones2,TargetDate TargetDate2 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=2)B on a.emplid=b.emplid"
        SQL &= " Left Join (select emplid,Goals Goals3,Milestones Milestones3,TargetDate TargetDate3 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=3)C on a.emplid=c.emplid"
        SQL &= " Left Join (select emplid,Goals Goals4,Milestones Milestones4,TargetDate TargetDate4 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=4)D on a.emplid=d.emplid"
        SQL &= " Left Join (select emplid,Goals Goals5,Milestones Milestones5,TargetDate TargetDate5 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=5)E on a.emplid=e.emplid"
        SQL &= " Left Join (select emplid,Goals Goals6,Milestones Milestones6,TargetDate TargetDate6 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=6)F on a.emplid=f.emplid"
        SQL &= " Left Join (select emplid,Goals Goals7,Milestones Milestones7,TargetDate TargetDate7 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=7)G on a.emplid=g.emplid"
        SQL &= " Left Join (select emplid,Goals Goals8,Milestones Milestones8,TargetDate TargetDate8 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=8)H on a.emplid=h.emplid"
        SQL &= " Left Join (select emplid,Goals Goals9,Milestones Milestones9,TargetDate TargetDate9 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & " and IndexID=9)N on a.emplid=n.emplid"
        SQL &= " Left Join (select emplid,Goals Goals10,Milestones Milestones10,TargetDate TargetDate10 "
        SQL &= " from Appraisal_FutureGoals_tbl where Perf_Year=" & lblYear.Text & " and emplid=" & lblEMPLID.Text & "  and IndexID =10)K on A.emplid=K.emplid"
        'Response.Write(SQL) : Response.End()

        DT = LocalClass.ExecuteSQLDataSet(SQL)

        '--Dispaly Data on Labels
        FUT_Goal_Edit11.Text = DT.Rows(0)("Goals1").ToString
        FUT_Succ_Edit11.Text = Replace(Replace(DT.Rows(0)("Milestones1").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit11.Text = Replace(Replace(DT.Rows(0)("TargetDate1").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        FUT_Goal_Edit12.Text = Replace(Replace(DT.Rows(0)("Goals2").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit12.Text = Replace(Replace(DT.Rows(0)("Milestones2").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit12.Text = Replace(Replace(DT.Rows(0)("TargetDate2").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Goal_Edit13.Text = Replace(Replace(DT.Rows(0)("Goals3").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit13.Text = Replace(Replace(DT.Rows(0)("Milestones3").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit13.Text = Replace(Replace(DT.Rows(0)("TargetDate3").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Goal_Edit14.Text = Replace(Replace(DT.Rows(0)("Goals4").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit14.Text = Replace(Replace(DT.Rows(0)("Milestones4").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit14.Text = Replace(Replace(DT.Rows(0)("TargetDate4").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Goal_Edit15.Text = Replace(Replace(DT.Rows(0)("Goals5").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit15.Text = Replace(Replace(DT.Rows(0)("Milestones5").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit15.Text = Replace(Replace(DT.Rows(0)("TargetDate5").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Goal_Edit16.Text = Replace(Replace(DT.Rows(0)("Goals6").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit16.Text = Replace(Replace(DT.Rows(0)("Milestones6").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit16.Text = Replace(Replace(DT.Rows(0)("TargetDate6").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Goal_Edit17.Text = Replace(Replace(DT.Rows(0)("Goals7").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit17.Text = Replace(Replace(DT.Rows(0)("Milestones7").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit17.Text = Replace(Replace(DT.Rows(0)("TargetDate7").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Goal_Edit18.Text = Replace(Replace(DT.Rows(0)("Goals8").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit18.Text = Replace(Replace(DT.Rows(0)("Milestones8").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit18.Text = Replace(Replace(DT.Rows(0)("TargetDate8").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Goal_Edit19.Text = Replace(Replace(DT.Rows(0)("Goals9").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit19.Text = Replace(Replace(DT.Rows(0)("Milestones9").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit19.Text = Replace(Replace(DT.Rows(0)("TargetDate9").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Goal_Edit20.Text = Replace(Replace(DT.Rows(0)("Goals10").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Succ_Edit20.Text = Replace(Replace(DT.Rows(0)("Milestones10").ToString, Chr(13), "<span>"), Chr(10), "<br>")
        FUT_Date_Edit20.Text = Replace(Replace(DT.Rows(0)("TargetDate10").ToString, Chr(13), "<span>"), Chr(10), "<br>")

        '--Dispaly Index2--
        If Len(DT.Rows(0)("Goals2").ToString) + Len(DT.Rows(0)("Milestones2").ToString) + Len(DT.Rows(0)("TargetDate2").ToString) > 2 Then
            Panel_FutureGoal_Review2.Visible = True
        Else
            Panel_FutureGoal_Review2.Visible = False
        End If
        '--Dispaly Index3--
        If Len(DT.Rows(0)("Goals3").ToString) + Len(DT.Rows(0)("Milestones3").ToString) + Len(DT.Rows(0)("TargetDate3").ToString) > 2 Then
            Panel_FutureGoal_Review3.Visible = True
        Else
            Panel_FutureGoal_Review3.Visible = False
        End If
        '--Dispaly Index4--
        If Len(DT.Rows(0)("Goals4").ToString) + Len(DT.Rows(0)("Milestones4").ToString) + Len(DT.Rows(0)("TargetDate4").ToString) > 2 Then
            Panel_FutureGoal_Review4.Visible = True
        Else
            Panel_FutureGoal_Review4.Visible = False
        End If
        '--Dispaly Index5--
        If Len(DT.Rows(0)("Goals5").ToString) + Len(DT.Rows(0)("Milestones5").ToString) + Len(DT.Rows(0)("TargetDate5").ToString) > 2 Then
            Panel_FutureGoal_Review5.Visible = True
        Else
            Panel_FutureGoal_Review5.Visible = False
        End If
        '--Dispaly Index6--
        If Len(DT.Rows(0)("Goals6").ToString) + Len(DT.Rows(0)("Milestones6").ToString) + Len(DT.Rows(0)("TargetDate6").ToString) > 2 Then
            Panel_FutureGoal_Review6.Visible = True
        Else
            Panel_FutureGoal_Review6.Visible = False
        End If
        '--Dispaly Index7--
        If Len(DT.Rows(0)("Goals7").ToString) + Len(DT.Rows(0)("Milestones7").ToString) + Len(DT.Rows(0)("TargetDate7").ToString) > 2 Then
            Panel_FutureGoal_Review7.Visible = True
        Else
            Panel_FutureGoal_Review7.Visible = False
        End If
        '--Dispaly Index8--
        If Len(DT.Rows(0)("Goals8").ToString) + Len(DT.Rows(0)("Milestones8").ToString) + Len(DT.Rows(0)("TargetDate8").ToString) > 2 Then
            Panel_FutureGoal_Review8.Visible = True
        Else
            Panel_FutureGoal_Review8.Visible = False
        End If
        '--Dispaly Index9--
        If Len(DT.Rows(0)("Goals9").ToString) + Len(DT.Rows(0)("Milestones9").ToString) + Len(DT.Rows(0)("TargetDate9").ToString) > 2 Then
            Panel_FutureGoal_Review9.Visible = True
        Else
            Panel_FutureGoal_Review9.Visible = False
        End If
        '--Dispaly Index10--
        If Len(DT.Rows(0)("Goals10").ToString) + Len(DT.Rows(0)("Milestones10").ToString) + Len(DT.Rows(0)("TargetDate10").ToString) > 2 Then
            Panel_FutureGoal_Review10.Visible = True
        Else
            Panel_FutureGoal_Review10.Visible = False
        End If

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
    Sub Goals_Log()

        SqlDataSource1.SelectCommand = "select distinct (select last+' '+first from id_tbl where emplid=Recall_EMPLID)Created,Recall_Emplid,emplid,"
        SqlDataSource1.SelectCommand &= "Replace(Replace(Goals,Char(13),'<span>'),Char(10),'<br>')Goals,"
        SqlDataSource1.SelectCommand &= "Replace(Replace(Milestones,Char(13),'<span>'),Char(10),'<br>')Milestones,"
        SqlDataSource1.SelectCommand &= "Replace(Replace(TargetDate,Char(13),'<span>'),Char(10),'<br>')TargetDate,"
        SqlDataSource1.SelectCommand &= "(select Max(DateEmpl_Appr)Recall_Date from Appraisal_FutureGoal_Recall_tbl where Rtrim(Ltrim(Goals))=Rtrim(Ltrim(A.Goals)) and "
        SqlDataSource1.SelectCommand &= "Rtrim(Ltrim(Milestones))=Rtrim(Ltrim(A.Milestones)) and  Rtrim(Ltrim(TargetDate))=Rtrim(Ltrim(A.TargetDate)))Recall_Date "
        SqlDataSource1.SelectCommand &= "from Appraisal_FutureGoal_Recall_tbl A where emplid=" & lblEMPLID.Text & " and Perf_Year=" & lblYear.Text & " and Len(DateEmpl_Appr)>5 and Len(Recall_Date)>5  order by Recall_Date desc"
        'Response.Write(SqlDataSource1.SelectCommand) : Response.End()
        LocalClass.CloseSQLServerConnection()

    End Sub

End Class