Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Public Class Default_Manager
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, x, y, z, ReturnValue As String
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
    Dim PCODE


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", "840")

        If Session("MGT_EMPLID") = "" Then Response.Redirect("default.aspx")

        ' lblMGT_EMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("MGT_EMPLID"), "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")

        lblMGT_EMPLID.Text = Session("MGT_EMPLID")
        lblNAME.Text = "Welcome " & Session("First") & " " & Session("Last")

        If IsPostBack Then

        Else
            LoadYear()
            LoadYear_Goal()
            LoadYear_MidPoint()
            LoadYear_My()
            LoadYear_My_Goal()
            LoadYear_Status_Report()
            LoadYear_Goals_Report()
        End If

        ShowButtonCursor()

        SQL = "select * from(select count(*)CNT_My_Goal from(select distinct Perf_Year from Appraisal_FutureGoals_Master_tbl where process_flag in (4,5) and emplid=" & lblMGT_EMPLID.Text & " and perf_year<2019 "
        SQL &= " UNION select distinct Perf_Year from Guild_Appraisal_FutureGoal_Master_TBL where EMPLID=" & lblMGT_EMPLID.Text & "  UNION select distinct Perf_Year from Appraisal_FutureGoals_Master_tbl where "
        SQL &= "emplid=" & lblMGT_EMPLID.Text & " and perf_year>=2019 )A)B, "
        SQL &= " (select Count(*)CNT_Emp_Goal from(select distinct Perf_Year from Appraisal_FutureGoals_Master_TBL where (MGT_EMPLID=" & lblMGT_EMPLID.Text & ")"
        SQL &= " UNION select distinct Perf_Year from Guild_Appraisal_FutureGoal_Master_TBL where (SUP_EMPLID=" & lblMGT_EMPLID.Text & ") and Process_Flag=5)C)D, "
        SQL &= " (select Count(*)CNT_MidPoint from(select Count(*)CNT_MidPoint from Guild_MidPoint_MASTER_tbl where SUP_EMPLID=" & lblMGT_EMPLID.Text & " UNION"
        SQL &= " select Count(*)CNT_MidPoint from appraisal_midpoint_master_tbl where SUP_EMPLID=" & lblMGT_EMPLID.Text & ")E)F"
        'Response.Write(SQL) : Response.End()

        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If DT.Rows(0)("CNT_My_Goal").ToString > 0 Then Panel_My_Goal.Visible = True Else Panel_My_Goal.Visible = False
        If DT.Rows(0)("CNT_Emp_Goal").ToString > 0 Then Panel_Employee_Goal.Visible = True Else Panel_Employee_Goal.Visible = False
        If DT.Rows(0)("CNT_MidPoint").ToString > 0 Then Panel_MidPoint.Visible = True Else Panel_MidPoint.Visible = False

    End Sub
    Protected Sub ShowButtonCursor()
        Submit_Appraisal_My.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Appraisal.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Goal.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        SubmitMidPoint.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Goal1.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Report.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
    End Sub

    Protected Sub LoadEmployees()


        Dim i As Integer

        'Response.Write("4)-Open " & DDLYears.SelectedValue.ToString & " Year" & Year(Now)) : Response.End()

        If Len(DDLYears.SelectedValue.ToString) = 0 Then

        Else
            DDLEmployees.Items.Clear()
            SQL = "select distinct emplid,name,perf_year from("
            SQL &= " select A.emplid,name,Perf_Year,sup_emplid SUPID from Guild_Appraisal_MASTER_tbl A,ps_employees B where A.emplid=B.emplid and (Supervisor_id=" & lblMGT_EMPLID.Text & ") and Perf_Year=" & DDLYears.SelectedValue.ToString
            SQL &= " UNION"
            SQL &= " select A.emplid,name,Perf_Year,SUP_EMPLID SUPID from ME_Appraisal_MASTER_tbl A,ps_employees B where A.emplid=B.emplid and (Supervisor_id=" & lblMGT_EMPLID.Text & ") and Perf_Year=" & DDLYears.SelectedValue.ToString
            SQL &= " UNION "
            SQL &= " select A.emplid,name,Perf_Year,MGT_EMPLID SUPID from Appraisal_Master_tbl A, ps_employees B where A.emplid=B.emplid and (Supervisor_id=" & lblMGT_EMPLID.Text & ") and Perf_Year=" & DDLYears.SelectedValue.ToString
            SQL &= " UNION "
            SQL &= " select AA.emplid,name,Perf_Year,Collab_EMPLID SUPID from(select A.Perf_Year,A.EMPLID,A.MGT_EMPLID Collab_EMPLID,B.MGT_EMPLID,Max(DateTime)DateTime from Appraisal_MasterHistory_tbl A JOIN Appraisal_Master_tbl B "
            SQL &= " ON A.emplid=B.emplid and A.Perf_Year=B.Perf_Year where (B.MGT_EMPLID=" & lblMGT_EMPLID.Text & " or A.MGT_EMPLID=" & lblMGT_EMPLID.Text & ") and LOGIN_EMPLID=0  and A.Perf_Year=" & DDLYears.SelectedValue.ToString
            SQL &= " and Process_Flag<5 group by A.Perf_Year,A.EMPLID,A.MGT_EMPLID,B.MGT_EMPLID)AA JOIN ps_employees BB ON AA.Emplid=BB.emplid)A order by name"
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            DDLEmployees.Items.Clear()
            DDLEmployees.Items.Add(New ListItem("  Choose Employee  ", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLEmployees.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If


    End Sub
    Protected Sub LoadYear()
        '---First and Second manager--- 
        SQL2 = "Select Perf_Year from(select * from("
        SQL2 &= " select distinct Perf_Year,'05/20/'+convert(char(4),Perf_Year)Today,Getdate()TodaySQL  from ME_Appraisal_MASTER_tbl A, ps_employees B  "
        SQL2 &= " where a.emplid=b.emplid and (SUP_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") "
            SQL2 &= " UNION "
            SQL2 &= " select distinct Perf_Year,'05/20/'+convert(char(4),Perf_Year)Today,Getdate()TodaySQL from GUILD_Appraisal_MASTER_tbl A, ps_employees B "
            SQL2 &= " where a.emplid = b.emplid And (SUP_EMPLID = " & lblMGT_EMPLID.Text & " Or supervisor_id = " & lblMGT_EMPLID.Text & ") "
            SQL2 &= " UNION "
            SQL2 &= " select distinct Perf_Year,'05/20/'+convert(char(4),Perf_Year)Today,Getdate()TodaySQL from Appraisal_Master_tbl A, ps_employees B "
            SQL2 &= " where a.emplid=b.emplid and (Mgt_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") )A where Today < TodaySQL)B order by Perf_Year desc"
            'Response.Write(SQL2) ': Response.End()
            DT2 = LocalClass.ExecuteSQLDataSet(SQL2)

            DDLYears.Items.Clear()

            DDLYears.Items.Add(New ListItem(" Choose Year ", "0"))
            For i = 0 To DT2.Rows.Count - 1
                DDLYears.Items.Add(New ListItem(DT2.Rows(i)("Perf_Year").ToString, DT2.Rows(i)("Perf_Year").ToString))
            Next
            'End If

            DDLEmployees.Items.Add(New ListItem("  Choose Employee  ", "0"))

            LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Year_Change(sender As Object, e As EventArgs) Handles DDLYears.SelectedIndexChanged
        Session("YEAR") = DDLYears.SelectedValue.ToString

        If DDLYears.SelectedValue.ToString > 0 Then
            DDLEmployees.Enabled = True
            DDLEmployees.Visible = True
            LoadEmployees()
        Else
            DDLEmployees.Items.Clear()
            DDLEmployees.Items.Add(New ListItem("  Choose Employee  ", "0"))
            DDLEmployees.Enabled = False
        End If

        SQL = "select count(*)CNT_Note from Appraisal_Master_tbl A, id_tbl B where A.emplid=B.emplid and "
        SQL &= " UP_MGT_EMPLID=" & lblMGT_EMPLID.Text & " and Perf_Year=" & DDLYears.SelectedValue.ToString & " and Process_Flag=1"
        DT = LocalClass.ExecuteSQLDataSet(SQL)

    End Sub
    Protected Sub DropDownList_Change(sender As Object, e As EventArgs) Handles DDLEmployees.SelectedIndexChanged
        lblEMPLID.Text = DDLEmployees.SelectedValue.ToString

        If DDLEmployees.SelectedValue.ToString = 0 Then

        Else
            If Session("YEAR") >= 2016 Then
                Session("EMPLID") = DDLEmployees.SelectedValue.ToString

                SQL2 = "Select Process_Flag from appraisal_master_tbl where emplid = " & DDLEmployees.SelectedValue.ToString & " and Perf_Year=" & DDLYears.SelectedValue
                DT2 = LocalClass.ExecuteSQLDataSet(SQL2)

                SQL = "select Max(Window_Batch)Window_Batch from appraisal_master_tbl where emplid = " & DDLEmployees.SelectedValue.ToString & " and Perf_Year=" & DDLYears.SelectedValue
                'Response.Write(SQL) ': Response.End()
                DT = LocalClass.ExecuteSQLDataSet(SQL)

                If DT2.Rows(0)("Process_Flag").ToString = 0 Or DT2.Rows(0)("Process_Flag").ToString = 1 Then
                    SQL1 = "Update appraisal_master_tbl Set Window_Batch=" & CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                    SQL1 &= " where emplid = " & DDLEmployees.SelectedValue.ToString & " and Perf_Year=" & DDLYears.SelectedValue & " and Process_flag in (0,1)"
                    'Response.Write(SQL1) ': Response.End()
                    DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                Else
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString)
                End If

                LocalClass.CloseSQLServerConnection()
            End If

        End If

    End Sub
    Protected Sub Submit_Appraisal_Click(sender As Object, e As EventArgs) Handles Submit_Appraisal.Click
        'Response.Write(Session("MGT_EMPLID") & "<br>" & lblEMPLID.Text) : Response.End()

        If Len(lblEMPLID.Text) = 4 Then
            x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblEMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        Else
            x = "aa" + Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblEMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        End If
        'Response.Write(x) : Response.End()

        y = DDLYears.SelectedValue.ToString
        z = Session("Window_Batch")

        If DDLEmployees.SelectedValue.ToString > 0 Then
            '--Year Choosen---           
            If DDLYears.SelectedValue.ToString = 2016 Or DDLYears.SelectedValue.ToString = 2017 Then
                SQL1 = "select Empl_Type,Mgt_emplid,Process_Flag from Appraisal_Master_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & y
                'Response.Write(SQL1) : Response.End()
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                LocalClass.CloseSQLServerConnection()

                If Trim(DT1.Rows(0)("Empl_Type").ToString) = "GLD" Then
                    'Response.Write("Mgt old " & DT1.Rows(0)("Mgt_emplid").ToString & "<br>Mgt new " & Session("MGT_EMPLID")) : Response.End()
                    If Trim(DT1.Rows(0)("Mgt_emplid").ToString) <> Trim(Session("MGT_EMPLID")) Then
                        Session("YEAR") = y
                        Response.Write("<script>window.open('Staff/Guild/Appraisal_print.aspx?Token=" + y + x + "','_blank' );</script>")
                    Else

                        If Today <= "01/01/2018" Then

                            If CDbl(DT1.Rows(0)("Process_Flag").ToString) = 0 Then
                                Session("YEAR") = y
                                Response.Write("<script>window.open('Staff/Guild/Appraisal.aspx?Token=" + y + x + z + "','_blank' );</script>")
                            Else
                                Session("YEAR") = y
                                Response.Write("<script>window.open('Staff/Guild/Appraisal.aspx?Token=" + y + x + "','_blank' );</script>")
                            End If
                        Else
                            Session("YEAR") = y
                            Response.Write("<script>window.open('Staff/Guild/Appraisal_print.aspx?Token=" + y + x + "','_blank' );</script>")

                        End If
                    End If

                Else

                    If CDbl(Trim(DT1.Rows(0)("Mgt_emplid").ToString)) <> CDbl(Trim(Session("MGT_EMPLID"))) Then
                        'Response.Write("1") : Response.End()
                        Session("YEAR") = y
                        Response.Write("<script>window.open('Staff/Exempt/Appraisal_print.aspx?Token=" + y + x + "','_blank' );</script>")
                        'Response.Redirect("Staff/Exempt/Appraisal.aspx?Token=" & x)
                    Else
                        'Response.Write("2") : Response.End()

                        If Today <= "01/01/2018" Then

                            If CDbl(DT1.Rows(0)("Process_Flag").ToString) = 0 Then
                                'Response.Write("3") : Response.End()
                                Session("YEAR") = y
                                Response.Write("<script>window.open('Staff/Exempt/Appraisal.aspx?Token=" + y + x + z + "','_blank' );</script>")
                            Else
                                'Response.Write("4") : Response.End()
                                Session("YEAR") = y
                                Response.Write("<script>window.open('Staff/Exempt/Appraisal.aspx?Token=" + y + x + "','_blank' );</script>")
                            End If

                        Else
                            Session("YEAR") = y
                            Response.Write("<script>window.open('Staff/Exempt/Appraisal_print.aspx?Token=" + y + x + "','_blank' );</script>")
                        End If


                    End If
                    '--reset Year & Employee's droplists
                    DDLEmployees.SelectedIndex = 0
                    DDLEmployees.Enabled = False
                    DDLYears.SelectedValue = 0
                End If

            ElseIf DDLYears.SelectedValue.ToString < 2016 Then

                SQL = "select 'GLD' SAP from Guild_Appraisal_MASTER_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & y
                SQL &= " UNION select 'MGT' SAP from ME_Appraisal_MASTER_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & y
                'Response.Write(SQL) : Response.End()
                DT = LocalClass.ExecuteSQLDataSet(SQL)
                LocalClass.CloseSQLServerConnection()

                If Trim(DT.Rows(0)("SAP").ToString) = "GLD" Then
                    'Response.Write("Redirect to guild`s form") : Response.End()
                    SQL4 = "select Process_Flag FLAG,Perf_Year,(select Max(Perf_Year) from GUILD_Appraisal_Master_tbl where emplid=" & lblEMPLID.Text & ")"
                    SQL4 &= " Max_Perf_Year from GUILD_Appraisal_Master_tbl where emplid=" & lblEMPLID.Text & " and perf_year=" & y
                    'Response.Write(SQL4) : Response.End()
                    DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
                    LocalClass.CloseSQLServerConnection()
                    Session("YEAR") = y
                    'Response.Redirect("Retro/Guild/Guild_Waiting_Approval.aspx?Token=" & x)
                    Response.Write("<script>window.open('Retro/Guild/Guild_Waiting_Approval.aspx?Token=" + x + "','_blank' );</script>")
                Else
                    Session("YEAR") = y
                    SQL4 = "select Process_Flag FLAG,Perf_Year from ME_Appraisal_Master_tbl where emplid=" & lblEMPLID.Text & " and perf_year=" & y
                    'Response.Write(SQL4) : Response.End()
                    DT4 = LocalClass.ExecuteSQLDataSet(SQL4)
                    LocalClass.CloseSQLServerConnection()
                    Session("YEAR") = y
                    Response.Redirect("Retro/Exempt/Manager_Waiting_Approval.aspx?Token=" & x)
                End If

            Else '2018
                SQL1 = "select Empl_Type,Mgt_emplid,Process_Flag from Appraisal_Master_tbl where emplid=" & lblEMPLID.Text & " and Perf_Year=" & y
                'Response.Write(SQL1) : Response.End()
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                LocalClass.CloseSQLServerConnection()

                If DT1.Rows(0)("Process_Flag").ToString < 5 Then
                    Session("YEAR") = y
                    Response.Write("<script>window.open('Staff/Appraisal/Appraisal_Edit.aspx?Token=" + y + x + z + "','_blank' );</script>")

                ElseIf DT1.Rows(0)("Process_Flag").ToString = 5 Then
                    Session("YEAR") = y
                    Response.Write("<script>window.open('Staff/Appraisal/Appraisal.aspx?Token=" + y + x + "','_blank' );</script>")
                End If
            End If

        Else
        If DDLYears.SelectedValue.ToString = 0 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee from list and click submit button');</script>")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose employee from list and click submit button');</script>")
        End If
        End If

        DDLEmployees.SelectedIndex = 0
        DDLEmployees.Enabled = False
        DDLYears.SelectedValue = 0
        '--Reset Dropdown lists--
        '1.Self Appraisal
        DDLYears_My.SelectedIndex = 0
        '2.Employees Appraisal
        DDLEmployees.SelectedIndex = 0 : DDLEmployees.Enabled = False : DDLYears.SelectedValue = 0
        '3.Employees Goals
        DDLYears_Goal.SelectedIndex = 0 : DDLEmployees_Goal.SelectedIndex = 0 : DDLEmployees_Goal.Enabled = False
        '4 Employees Goal
        DDLYears_MidPoint.SelectedIndex = 0 : DDLEmployees_MidPoint.SelectedIndex = 0 : DDLEmployees_MidPoint.Enabled = False

    End Sub

    Protected Sub LoadEmployees_Goal()
        Dim i As Integer
        'Response.Write("4)-Open " & DDLYears.SelectedValue.ToString & " Year" & Year(Now)) : Response.End()

        If Len(DDLYears_Goal.SelectedValue.ToString) = 0 Then

        Else
            DDLEmployees_Goal.Items.Clear()
            SQL = "select * from(select * from("
            SQL &= " select a.emplid,Name,Perf_Year,sup_emplid SUPID,process_flag,'01/20/'+convert(char(4),Perf_Year)Today,Getdate()TodaySQL from GUILD_Appraisal_FUTUREGOAL_MASTER_tbl A,ps_employees B "
            SQL &= " where a.emplid=b.emplid and process_flag=5 and (SUP_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") UNION "
            SQL &= " select a.emplid,Name,Perf_Year,MGT_EMPLID SUPID,process_flag,'01/20/'+convert(char(4),Perf_Year-1)Today,Getdate()TodaySQL from Appraisal_FutureGoals_Master_tbl A,ps_employees B "
            SQL &= " where a.emplid=b.emplid and  (MGT_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") )A where today<TodaySQL and Perf_Year=" & DDLYears_Goal.SelectedValue & ")B order by name"
            'Response.Write(SQL) ': Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            DDLEmployees_Goal.Items.Clear()
            DDLEmployees_Goal.Items.Add(New ListItem("  Choose Employee  ", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLEmployees_Goal.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If

    End Sub
    Protected Sub LoadYear_Goal()
        SQL = "Select Perf_Year from(select * from("
        SQL &= " select distinct Perf_Year,'01/20/'+convert(char(4),Perf_Year)Today,Getdate()TodaySQL from Guild_Appraisal_FutureGoal_Master_TBL A, ps_employees B "
        SQL &= " where a.emplid=b.emplid and (SUP_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") UNION "
        SQL &= " select distinct Perf_Year,'01/20/'+convert(char(4),Perf_Year-1)Today,Getdate()TodaySQL from Appraisal_FutureGoals_Master_TBL A, ps_employees B "
        SQL &= " where a.emplid=b.emplid and (Mgt_EMPLID=" & lblMGT_EMPLID.Text & " or supervisor_id=" & lblMGT_EMPLID.Text & ") )A where today < TodaySQL)B order by Perf_Year desc"
        'Response.Write(SQL) ': Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYears_Goal.Items.Clear()

        DDLYears_Goal.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_Goal.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next

        LocalClass.CloseSQLServerConnection()

        DDLEmployees_Goal.Items.Add(New ListItem("  Choose Employee  ", "0"))
    End Sub
    Protected Sub Year_Change_Goal(sender As Object, e As EventArgs) Handles DDLYears_Goal.SelectedIndexChanged
        Session("YEAR") = DDLYears_Goal.SelectedValue.ToString

        If DDLYears_Goal.SelectedValue.ToString > 0 Then
            DDLEmployees_Goal.Enabled = True
            LoadEmployees_Goal()
        Else
            DDLEmployees_Goal.Items.Clear()
            DDLEmployees_Goal.Items.Add(New ListItem("  Choose Employee  ", "0"))
            DDLEmployees_Goal.Enabled = False
        End If
    End Sub
    Protected Sub DropDownList_Change_Goal(sender As Object, e As EventArgs) Handles DDLEmployees_Goal.SelectedIndexChanged

        If DDLEmployees_Goal.SelectedValue.ToString = 0 Then

        Else
            If Session("YEAR") > 2016 Then
                Session("EMPLID") = DDLEmployees_Goal.SelectedValue.ToString
                lblEMPLID.Text = DDLEmployees_Goal.SelectedValue.ToString
                'Response.Write("EMPLID" & DDLEmployees_Goal.SelectedValue.ToString)
                SQL = "select Process_Flag, Max(Window_Batch)Window_Batch from appraisal_futuregoals_master_tbl where emplid = " & DDLEmployees_Goal.SelectedValue.ToString & " and Perf_Year=" & DDLYears_Goal.SelectedValue & " group by Process_Flag"
                DT = LocalClass.ExecuteSQLDataSet(SQL)

                If DT.Rows(0)("Process_Flag").ToString = 0 Or DT.Rows(0)("Process_Flag").ToString = 2 Then
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString)
                Else
                    SQL1 = "Update appraisal_futuregoals_master_tbl Set Window_Batch=" & CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                    SQL1 &= " where emplid = " & DDLEmployees_Goal.SelectedValue.ToString & " and Perf_Year=" & DDLYears_Goal.SelectedValue
                    'Response.Write(SQL1)
                    DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                End If
                LocalClass.CloseSQLServerConnection()
            End If
        End If

    End Sub
    Protected Sub Submit_Goal_Click(sender As Object, e As EventArgs) Handles Submit_Goal.Click
        '--Open Employee Goals

        PCODE = System.Configuration.ConfigurationManager.AppSettings("ApplKey").ToString()

        If DDLEmployees_Goal.SelectedValue.ToString > 0 Then
            Dim x, y, z As String
            x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(DDLEmployees_Goal.SelectedValue, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
            y = DDLYears_Goal.SelectedValue.ToString
            z = Session("Window_Batch")

            '--Year Choosen
            If DDLYears_Goal.SelectedValue.ToString = 2017 Or DDLYears_Goal.SelectedValue.ToString = 2018 Then
                SQL = "select Max(Perf_Year)Max_Year,convert(char(10),GetDate(),101)Date_Now from Appraisal_FutureGoals_master_tbl where emplid=" & DDLEmployees_Goal.SelectedValue & " "
                DT = LocalClass.ExecuteSQLDataSet(SQL)

                'Response.Write(DateTime.Parse(Today) & "<br>" & DateTime.Parse(DT.Rows(0)("Date_Now").ToString) & "<br>" & CDbl(DT.Rows(0)("Max_Year").ToString) - Year(Today) & "<br>" & CDbl(DateDiff("d", "04/15/2017", Today))) : Response.End()

                '--Lock previous goal apps
                If CDbl(DateDiff("d", PCODE, Today)) > 0 And y < CDbl(DT.Rows(0)("Max_Year").ToString) Then
                    'Response.Write("Today " & Today & "<br>Year " & DateTime.Now.Year.ToString() & "<br>PCODE " & PCODE & "<br>DayDiff " & CDbl(DateDiff("d", PCODE, Today))) : Response.End()
                    'Response.Write(CDbl(DT.Rows(0)("Max_Year").ToString)) : Response.End()
                    Session("YEAR") = y
                    Response.Write("<script>window.open('Staff/FutureGoals/FutureGoal_print.aspx?Token=" + y + x + "','_blank' );</script>")
                Else
                    'Response.Write("2 " & Today): Response.End()
                    Session("YEAR") = y
                    'Response.Write("<script>window.open('Staff/FutureGoals/FutureGoal.aspx?Token=" + y + x + z + "','_blank' );</script>")
                    '--Lock Previous FY Gola edit
                    Response.Write("<script>window.open('Staff/FutureGoals/FutureGoal_print.aspx?Token=" + y + x + "','_blank' );</script>")
                End If

            ElseIf DDLYears_Goal.SelectedValue.ToString < 2017 Then 'Previous years
                Session("YEAR") = y
                'Response.Write(x) : Response.End()
                Response.Write("<script>window.open('Retro/Guild/Guild_FutureGoal.aspx?Token=" + x + "','_blank' );</script>")

            ElseIf DDLYears_Goal.SelectedValue.ToString = 2019 Then '---2019--- 
                Session("YEAR") = y
                Response.Write("<script>window.open('Staff/FutureGoals/FutureGoals_Print.aspx?Token=" + y + x + z + "','_blank' );</script>")
            Else '2020
                Session("YEAR") = y
                Response.Write("<script>window.open('Staff/FutureGoals/myEmpGoals.aspx?Token=" + y + x + z + "','_blank' );</script>")
            End If
        Else

            If DDLYears_Goal.SelectedValue.ToString = 0 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee from list and click submit button');</script>")
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose employee from list and click submit button');</script>")
            End If
        End If

        LocalClass.CloseSQLServerConnection()

        '--Reset Dropdown lists--
        '1.Self Appraisal
        DDLYears_My.SelectedIndex = 0
        '2.Employees Appraisal
        DDLEmployees.SelectedIndex = 0 : DDLEmployees.Enabled = False : DDLYears.SelectedValue = 0
        '3.Employees Goals
        DDLYears_Goal.SelectedIndex = 0 : DDLEmployees_Goal.SelectedIndex = 0 : DDLEmployees_Goal.Enabled = False
        '4 Employees Goal
        DDLYears_MidPoint.SelectedIndex = 0 : DDLEmployees_MidPoint.SelectedIndex = 0 : DDLEmployees_MidPoint.Enabled = False

    End Sub

    Protected Sub LoadEmployees_MidPoint()
        Dim i As Integer
        'Response.Write("4)-Open " & DDLYears.SelectedValue.ToString & " Year" & Year(Now)) : Response.End()
        If Len(DDLYears_MidPoint.SelectedValue.ToString) = 0 Then
        Else
            DDLEmployees_MidPoint.Items.Clear()
            'SQL = "select (select last+','+first from id_tbl where emplid=A.emplid)Name,* from Guild_MidPoint_MASTER_tbl A where sup_emplid=" & lblMGT_EMPLID.Text & " order by Name"
            SQL = "select * from(select (select last+','+first from id_tbl where emplid=A.emplid)Name,* from Guild_MidPoint_MASTER_tbl A UNION"
            SQL &= " select (select last+','+first from id_tbl where emplid=A.emplid)Name,* from Appraisal_MidPoint_MASTER_tbl A )A"
            SQL &= " where sup_emplid=" & lblMGT_EMPLID.Text & " and Perf_Year=" & DDLYears_MidPoint.SelectedValue & " order by name"
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            DDLEmployees_MidPoint.Items.Clear()
            DDLEmployees_MidPoint.Items.Add(New ListItem("  Choose Employee  ", "0"))
            For i = 0 To DT.Rows.Count - 1
                DDLEmployees_MidPoint.Items.Add(New ListItem(DT.Rows(i)("name").ToString, DT.Rows(i)("emplid").ToString))
            Next
            LocalClass.CloseSQLServerConnection()
        End If
    End Sub
    Protected Sub LoadYear_MidPoint()
        SQL = "select * from(select distinct Perf_Year from Appraisal_MidPoint_MASTER_tbl where sup_emplid =" & lblMGT_EMPLID.Text & " UNION "
        SQL &= " select distinct Perf_Year from Guild_MidPoint_MASTER_tbl where sup_emplid =" & lblMGT_EMPLID.Text & ")A order by Perf_Year desc"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYears_MidPoint.Items.Clear()
        DDLYears_MidPoint.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_MidPoint.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

        DDLEmployees_MidPoint.Items.Add(New ListItem("  Choose Employee  ", "0"))

    End Sub
    Protected Sub Year_Change_MidPoint(sender As Object, e As EventArgs) Handles DDLYears_MidPoint.SelectedIndexChanged
        Session("YEAR") = DDLYears_MidPoint.SelectedValue.ToString
        If DDLYears_MidPoint.SelectedValue.ToString > 0 Then
            DDLEmployees_MidPoint.Enabled = True
            LoadEmployees_MidPoint()
        Else
            DDLEmployees_MidPoint.Items.Clear()
            DDLEmployees_MidPoint.Items.Add(New ListItem("  Choose Employee  ", "0"))
            DDLEmployees_MidPoint.Enabled = False
        End If
        
    End Sub
    Protected Sub DropDownList_Change_MidPoint(sender As Object, e As EventArgs) Handles DDLEmployees_MidPoint.SelectedIndexChanged
        Session("EMPLID") = DDLEmployees_MidPoint.SelectedValue.ToString
        lblEMPLID.Text = DDLEmployees_MidPoint.SelectedValue.ToString
    End Sub
    Protected Sub SubmitMidPoint_Click(sender As Object, e As EventArgs) Handles SubmitMidPoint.Click
        Dim x, y As String
        y = DDLYears_MidPoint.SelectedValue.ToString
        x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblEMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        Session("YEAR_MidPoint") = y

        Session("MGT_LOGON") = Session("MGT_EMPLID")

        If y = 2016 And Len(x) > 0 Then

            'x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblEMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
            'Response.Redirect("Retro/Guild/Guild_MidPoint.aspx?Token=" & x)
            Response.Write("<script>window.open('Retro/Guild/Guild_MidPoint.aspx?Token=" + x + "','_blank' );</script>")
        ElseIf y > 2016 And Len(x) > 0 Then
            'x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblEMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
            Response.Write("<script>window.open('Staff/Guild/MidPoint.aspx?Token=" + x + "','_blank' );</script>")
        Else
            If y = 0 Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR and Employee from list and click submit button');</script>")
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose employee from list and click submit button');</script>")
            End If
        End If

        '--Reset Dropdown lists--
        '1.Self Appraisal
        DDLYears_My.SelectedIndex = 0
        '2.Employees Appraisal
        DDLEmployees.SelectedIndex = 0 : DDLEmployees.Enabled = False : DDLYears.SelectedValue = 0
        '3.Employees Goals
        DDLYears_Goal.SelectedIndex = 0 : DDLEmployees_Goal.SelectedIndex = 0 : DDLEmployees_Goal.Enabled = False
        '4 Employees Goal
        DDLYears_MidPoint.SelectedIndex = 0 : DDLEmployees_MidPoint.SelectedIndex = 0 : DDLEmployees_MidPoint.Enabled = False
    End Sub
    Protected Sub LoadYear_My()
        '---First and Second manager--- 
        SQL2 = "select * from(select distinct Perf_Year from ME_Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and Process_Flag in(4,5) UNION "
        SQL2 &= " select distinct Perf_Year from Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and Process_Flag in(4,5) UNION "
        SQL2 &= " select distinct Perf_Year  from Guild_Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and process_Flag in (4,5))A order by Perf_Year desc"
        'Response.Write(SQL2) ': Response.End()
        DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
        DDLYears_My.Items.Clear()

        DDLYears_My.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT2.Rows.Count - 1
            DDLYears_My.Items.Add(New ListItem(DT2.Rows(i)("Perf_Year").ToString, DT2.Rows(i)("Perf_Year").ToString))
        Next
        'End If

        SQL3 = "select sum(cnt)CNT_Apr from(select Count(*)cnt from ME_Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and process_Flag in (4,5)"
        SQL3 &= " UNION select Count(*)cnt from Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and process_Flag in (4,5) UNION "
        SQL3 &= " select Count(*)cnt from Guild_Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and process_Flag in (4,5) )A "
        'Response.Write(SQL3)
        DT3 = LocalClass.ExecuteSQLDataSet(SQL3)

        If CDbl(DT3.Rows(0)("CNT_Apr").ToString) = 0 Then
            Panel_Me_Appraisal.Visible = False : Panel_My_Staff.Visible = False
        Else
            Panel_Me_Appraisal.Visible = True : Panel_My_Staff.Visible = True
        End If


        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Year_Change_My(sender As Object, e As EventArgs) Handles DDLYears_My.SelectedIndexChanged

    End Sub
    Protected Sub Submit_Appraisal_My_Click(sender As Object, e As EventArgs) Handles Submit_Appraisal_My.Click
        x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblMGT_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        y = DDLYears_My.SelectedValue.ToString

        If DDLYears_My.SelectedValue.ToString = 0 Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose YEAR from list and click submit button');</script>")

        ElseIf DDLYears_My.SelectedValue.ToString = 2016 And DDLYears_My.SelectedValue.ToString = 2017 Then
            'Response.Redirect("Staff/Exempt/Appraisal.aspx?Token=" & x)
            'Response.Write("<script>window.open('Staff/Exempt/Appraisal.aspx?Token=" + x + "','_blank' );</script>")
            SQL = "select Process_Flag from Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and perf_year=" & CDbl(DDLYears_My.SelectedValue.ToString)
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            'Session("YEAR") = Session("YEAR_Appraisal")
            If CDbl(DT.Rows(0)("Process_Flag").ToString) = 4 Then
                Session("YEAR") = y
                Response.Redirect("Staff/Exempt/Appraisal.aspx?Token=" & y + x)
            Else
                Session("YEAR") = y
                Response.Write("<script>window.open('Staff/Exempt/Appraisal.aspx?Token=" + y + x + "','_blank' );</script>")
            End If

        ElseIf DDLYears_My.SelectedValue.ToString < 2016 Then
            SQL1 = "select * from (select Count(*)cnt_mgt from ME_Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and process_Flag in (4,5) and perf_year=" & y & ")A,"
            SQL1 &= "(select Count(*)cnt_gld from Guild_Appraisal_MASTER_tbl where EMPLID=" & lblMGT_EMPLID.Text & " and process_Flag in (4,5)  and perf_year=" & y & ")B"
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            If CDbl(DT1.Rows(0)("cnt_gld").ToString) > 0 Then
                Session("YEAR") = y
                Response.Write("<script>window.open('Retro/Guild/Guild_Waiting_Approval.aspx?Token=" + x + "','_blank' );</script>")
            Else
                Session("YEAR") = y
                Response.Write("<script>window.open('Retro/Exempt/My_Appraisal.aspx?Token=" + x + "','_blank' );</script>")
            End If

        ElseIf DDLYears_My.SelectedValue.ToString = 2016 Or DDLYears_My.SelectedValue.ToString = 2017 Then
            Session("YEAR") = y
            Response.Write("<script>window.open('Staff/Exempt/Appraisal.aspx?Token=" + y + x + "','_blank' );</script>")
        Else '--2018
            Session("YEAR") = y
            Response.Write("<script>window.open('Staff/Appraisal/Appraisal.aspx?Token=" + y + x + "','_blank' );</script>")
        End If


        '--Reset Dropdown lists--
        '1.Self Appraisal
        DDLYears_My.SelectedIndex = 0
        '2.Employees Appraisal
        DDLEmployees.SelectedIndex = 0 : DDLEmployees.Enabled = False : DDLYears.SelectedValue = 0
        '3.Employees Goals
        DDLYears_Goal.SelectedIndex = 0 : DDLEmployees_Goal.SelectedIndex = 0 : DDLEmployees_Goal.Enabled = False
        '4 Employees Goal
        DDLYears_MidPoint.SelectedIndex = 0 : DDLEmployees_MidPoint.SelectedIndex = 0 : DDLEmployees_MidPoint.Enabled = False

        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("Default.aspx")
    End Sub
    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblMGT_EMPLID.Text = Session("MGT_EMPLID")
    End Sub

    Protected Sub Submit_Goal1_Click(sender As Object, e As EventArgs) Handles Submit_Goal1.Click

        '-- Open Self Goals Form-- 
        'Response.Write("My Goals") : Response.End()
        x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblMGT_EMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        y = DDLYears_My_Goal.SelectedValue
        z = Session("Window_Batch")

        'Response.Write(y & "<br>" & x) : Response.End()
        If DDLYears_My_Goal.SelectedValue > 0 Then

            If DDLYears_My_Goal.SelectedValue <= 2016 Then
                Response.Write("<script>window.open('Retro/Guild/Guild_FutureGoal.aspx?Token=" + x + "','_blank' );</script>")
            ElseIf DDLYears_My_Goal.SelectedValue = 2017 Or DDLYears_My_Goal.SelectedValue = 2018 Then
                'Session("YEAR") = DDLYears_My_Goal.SelectedValue
                Response.Write("<script>window.open('Staff/FutureGoals/FutureGoal_print.aspx?Token=" + y + x + "','_blank' );</script>")
            ElseIf DDLYears_My_Goal.SelectedValue = 2019 Then '--2019
                Response.Write("<script>window.open('Staff/FutureGoals/FutureGoals_Print.aspx?Token=" + y + x + z + "','_blank' );</script>")
            Else '2020
                Response.Write("<script>window.open('Staff/FutureGoals/myGoals.aspx?Token=" + y + x + z + "','_blank' );</script>")
            End If

        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Redirect", "<script language='JavaScript'> alert('Please choose Goal year from list and click submit button');</script>")

        End If

        DDLYears_My_Goal.SelectedIndex = 0

    End Sub
    Protected Sub Year_My_Goal(sender As Object, e As EventArgs) Handles DDLYears_My_Goal.SelectedIndexChanged
        'Response.Write(DDLYears_My_Goal.SelectedValue.ToString & "<br>" & Session("MGT_EMPLID") ) : Response.End()
        If DDLYears_My_Goal.SelectedValue.ToString > 0 Then
            Session("YEAR_Goal") = DDLYears_My_Goal.SelectedValue.ToString
            If DDLYears_My_Goal.SelectedValue.ToString > 2018 Then
                SQL = "select Process_Flag, Max(Window_Batch)Window_Batch from appraisal_futuregoals_master_tbl where emplid = " & Session("MGT_EMPLID") & " and Perf_Year=" & DDLYears_My_Goal.SelectedValue & " group by Process_Flag"
                'Response.Write(SQL) : Response.End()
                DT = LocalClass.ExecuteSQLDataSet(SQL)
                'If CDbl(DT.Rows(0)("Process_Flag").ToString) = 0 Then
                SQL1 = "Update appraisal_futuregoals_master_tbl Set Window_Batch=" & CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                SQL1 &= " where emplid = " & Session("MGT_EMPLID") & " and Perf_Year=" & DDLYears_My_Goal.SelectedValue
                'Response.Write(SQL1) : Response.End()
                DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                LocalClass.CloseSQLServerConnection()
                'Else
                'Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString)
                'End If
            End If
        Else
            Session("YEAR_Goal") = ""
        End If

    End Sub
    Protected Sub LoadYear_My_Goal()

        'SQL = "select Perf_Year from(select distinct Perf_Year from Appraisal_FutureGoals_Master_tbl where /*process_flag in (4,5) and*/ emplid=" & lblMGT_EMPLID.Text & ""
        'SQL &= " UNION select distinct Perf_Year from Guild_Appraisal_FutureGoal_Master_TBL where EMPLID=" & lblMGT_EMPLID.Text & ")A  order by Perf_Year desc"
        SQL = "select Perf_Year from(select distinct Perf_Year from Appraisal_FutureGoals_Master_tbl where process_flag in (4,5) and emplid=" & lblMGT_EMPLID.Text & " and perf_year<2019"
        SQL &= " UNION select distinct Perf_Year from Guild_Appraisal_FutureGoal_Master_TBL where EMPLID=" & lblMGT_EMPLID.Text & " UNION select distinct Perf_Year "
        SQL &= " from Appraisal_FutureGoals_Master_tbl where emplid=" & lblMGT_EMPLID.Text & " and perf_year>=2019 )A order by Perf_Year desc "
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYears_My_Goal.Items.Clear()
        DDLYears_My_Goal.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_My_Goal.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub LoadYear_Status_Report()
        SQL = "select distinct Perf_Year from Appraisal_Master_TBL where MGT_EMPLID=" & lblMGT_EMPLID.Text & "  or UP_MGT_EMPLID=" & lblMGT_EMPLID.Text & "  order by Perf_Year desc"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLStatus_Reports.Items.Clear()
        DDLStatus_Reports.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLStatus_Reports.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Year_Status_Reports(sender As Object, e As EventArgs) Handles DDLStatus_Reports.SelectedIndexChanged

    End Sub
    Protected Sub Submit_Report_Click(sender As Object, e As EventArgs) Handles Submit_Report.Click
        y = DDLStatus_Reports.SelectedValue.ToString
        If DDLStatus_Reports.SelectedValue > 0 Then
            Session("YEAR") = y
            Session("Year_Appr") = y
            Response.Write("<script>window.open('Staff/StatusReport/Status_Report.aspx?Token=1" + y + "','_blank' );</script>")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Redirect", "<script language='JavaScript'> alert('Please choose year from list and click submit button');</script>")
        End If
    End Sub

    Protected Sub LoadYear_Goals_Report()
        SQL = "select distinct Perf_Year from Appraisal_FutureGoals_Master_tbl where MGT_EMPLID=" & lblMGT_EMPLID.Text & " or UP_MGT_EMPLID=" & lblMGT_EMPLID.Text & "  order by Perf_Year desc"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLGoals_Reports.Items.Clear()
        DDLGoals_Reports.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLGoals_Reports.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub Year_Goals_Reports(sender As Object, e As EventArgs) Handles DDLGoals_Reports.SelectedIndexChanged

    End Sub

    Protected Sub Submit_Goals_Report_Click(sender As Object, e As EventArgs) Handles Submit_Goals_Report.Click
        y = DDLGoals_Reports.SelectedValue.ToString
        If DDLGoals_Reports.SelectedValue > 0 Then
            Session("YEAR") = y
            Session("Year_Appr") = y
            Response.Write("<script>window.open('Staff/StatusReport/Status_Report.aspx?Token=2" + y + "','_blank' );</script>")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Redirect", "<script language='JavaScript'> alert('Please choose year from list and click submit button');</script>")
        End If
    End Sub
End Class