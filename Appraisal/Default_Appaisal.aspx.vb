Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail

Public Class Default_Appaisal
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
    Dim DT, DT1, DT2 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", "840")

        If Session("EMPLID_LOGON") = "" Then Response.Redirect("default.aspx")

        lblNAME.Text = "Welcome " & Session("First") & " " & Session("Last")

        If IsPostBack Then
            'Response.Write("1. IsPostBack")
            lblEMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("EMPLID_LOGON"), _
            "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")
        Else
            'Response.Write("2. Else")
            If Session("SAP") = "GLD" Then
                LoadYear()
                LoadYear_Goal()
                LoadYear_MidPoint()
            Else
                LoadYear()
                LoadYear_Goal()
            End If
        End If

        ShowButtonCursor()

        SQL = "select * from"
        SQL &= " (select Max(CNT_Goal)CNT_Goal from(select count(*)CNT_Goal from GUILD_Appraisal_FUTUREGOAL_MASTER_TBL where process_flag in (4,5) and emplid=" & Session("EMPLID_LOGON") & ""
        SQL &= " UNION select count(*)CNT_Goal from Appraisal_FUTUREGOALS_MASTER_TBL where /*process_flag in (4,5) and*/ emplid=" & Session("EMPLID_LOGON") & ")A)AA,"
        SQL &= " (select Max(CNT_Appr)CNT_Appr from(select count(*)CNT_Appr from Appraisal_MASTER_tbl where emplid=" & Session("EMPLID_LOGON") & " and process_flag in (4,5)"
        SQL &= " UNION select count(*)CNT_Appr from Guild_Appraisal_MASTER_tbl where emplid=" & Session("EMPLID_LOGON") & " and process_flag in (4,5)"
        SQL &= " UNION select count(*)CNT_Appr from ME_Appraisal_MASTER_tbl where emplid=" & Session("EMPLID_LOGON") & " and process_flag in (4,5))B)BB,"
        SQL &= " (select count(*)CNT_MPoint from Appraisal_MidPoint_MASTER_tbl where emplid=" & Session("EMPLID_LOGON") & ")CC"
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        If DT.Rows(0)("CNT_Goal").ToString > 0 Then
            Panel_Goal.Visible = True
        Else
            Panel_Goal.Visible = False
        End If
        If DT.Rows(0)("CNT_Appr").ToString > 0 Then
            Panel_Appraisal.Visible = True
        Else
            Panel_Appraisal.Visible = False
        End If
        If DT.Rows(0)("CNT_MPoint").ToString > 0 Then
            Panel_MidPoint.Visible = True
        Else
            Panel_MidPoint.Visible = False
        End If

    End Sub
    Protected Sub ShowButtonCursor()
        Submit_Appraisal1.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        Submit_Goal1.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        SubmitMidPoint1.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
    End Sub


    Protected Sub LoadYear()
        If Session("SAP") = "GLD" Then
            SQL = "select distinct Perf_Year  from Guild_Appraisal_MASTER_tbl where emplid=" & Session("EMPLID_LOGON") & " and Process_Flag in (3,4,5) UNION"
            SQL &= " select distinct Perf_Year from ME_Appraisal_MASTER_tbl where EMPLID=" & Session("EMPLID_LOGON") & " and Process_Flag in(3,4,5) UNION"
            SQL &= " select distinct Perf_Year from Appraisal_MASTER_tbl where EMPLID=" & Session("EMPLID_LOGON") & " and Process_Flag in (4,5) order by Perf_Year desc"
            Panel_Goal.Visible = True : Panel_MidPoint.Visible = True
        Else
            SQL = "select distinct Perf_Year  from Guild_Appraisal_MASTER_tbl where emplid=" & Session("EMPLID_LOGON") & " and Process_Flag in (3,4,5) UNION"
            SQL &= " select distinct Perf_Year from ME_Appraisal_MASTER_tbl where EMPLID=" & Session("EMPLID_LOGON") & " and Process_Flag in(3,4,5) UNION"
            SQL &= " select distinct Perf_Year from Appraisal_MASTER_tbl where EMPLID=" & Session("EMPLID_LOGON") & " and Process_Flag in (4,5) order by Perf_Year desc"
            'Response.Write(SQL) : Response.End()
            Panel_Goal.Visible = False : Panel_MidPoint.Visible = False
        End If
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        DDLYears.Items.Clear()
        DDLYears.Items.Add(New ListItem("Choose Year", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub Year_Change(sender As Object, e As EventArgs) Handles DDLYears.SelectedIndexChanged
        If DDLYears.SelectedValue.ToString > 0 Then
            DDLYears_Goal.SelectedIndex = -1 : DDLYears_MidPoint.SelectedIndex = -1
            Session("YEAR_Goal") = "" : Session("YEAR_MidPoint") = ""
            lblYear_Appraisal.Text = DDLYears.SelectedValue.ToString
            Session("YEAR_Appraisal") = lblYear_Appraisal.Text
        Else
            Session("YEAR_Appraisal") = ""
        End If

    End Sub
    Protected Sub Submit_Appraisal1_Click(sender As Object, e As EventArgs) Handles Submit_Appraisal1.Click
        y = DDLYears.SelectedValue.ToString

        SQL1 = "select * from (select Count(*)cnt_mgt from ME_Appraisal_MASTER_tbl where EMPLID=" & Session("EMPLID_LOGON") & " and process_Flag in (4,5) and perf_year=" & y & ")A,"
        SQL1 &= "(select Count(*)cnt_gld from Guild_Appraisal_MASTER_tbl where EMPLID=" & Session("EMPLID_LOGON") & " and process_Flag in (4,5)  and perf_year=" & y & ")B"
        'Response.Write(SQL1) : Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)


        If Len(Session("YEAR_Appraisal")) > 0 Then

            If Session("SAP") = "GLD" Then

                If DDLYears.SelectedValue.ToString < 2016 Then
                    If CDbl(DT1.Rows(0)("cnt_gld").ToString) > 0 Then
                        Session("YEAR") = Session("YEAR_Appraisal")
                        Response.Redirect("Retro/Guild/GuildReview.aspx?Token=" & lblEMPLID.Text)
                    Else
                        Session("YEAR") = Session("YEAR_Appraisal")
                        Response.Redirect("Retro/Exempt/My_Appraisal.aspx?Token=" & lblEMPLID.Text)
                    End If

                ElseIf DDLYears.SelectedValue.ToString = 2016 Or DDLYears.SelectedValue.ToString = 2017 Then
                    SQL = "select Process_Flag from Appraisal_MASTER_tbl where EMPLID=" & Session("EMPLID_LOGON") & " and perf_year=" & CDbl(DDLYears.SelectedValue.ToString)
                    'Response.Write(SQL) : Response.End()
                    DT = LocalClass.ExecuteSQLDataSet(SQL)
                    Session("YEAR") = Session("YEAR_Appraisal")
                    If CDbl(DT.Rows(0)("Process_Flag").ToString) = 4 Then
                        Response.Redirect("Staff/Guild/Appraisal.aspx?Token=" & y + lblEMPLID.Text)
                    Else
                        Response.Write("<script>window.open('Staff/Guild/Appraisal.aspx?Token=" + y + lblEMPLID.Text + "','_blank' );</script>")
                    End If
                Else '2018
                    Session("YEAR") = Session("YEAR_Appraisal")
                    Response.Write("<script>window.open('Staff/Appraisal/Appraisal.aspx?Token=" + y + lblEMPLID.Text + "','_blank' );</script>")
                End If

            Else '-- Manager/Exempt

                If DDLYears.SelectedValue.ToString < 2016 Then
                    'Session("YEAR") = Session("YEAR_Appraisal")
                    'Response.Redirect("Retro/Exempt/My_Appraisal.aspx?Token=" & lblEMPLID.Text)
                    If CDbl(DT1.Rows(0)("cnt_gld").ToString) > 0 Then
                        Session("YEAR") = Session("YEAR_Appraisal")
                        Response.Redirect("Retro/Guild/GuildReview.aspx?Token=" & lblEMPLID.Text)
                    Else
                        Session("YEAR") = Session("YEAR_Appraisal")
                        Response.Redirect("Retro/Exempt/My_Appraisal.aspx?Token=" & lblEMPLID.Text)
                    End If

                ElseIf DDLYears.SelectedValue.ToString = 2016 Or DDLYears.SelectedValue.ToString = 2017 Then
                    SQL = "select Process_Flag from Appraisal_MASTER_tbl where EMPLID=" & Session("EMPLID_LOGON") & " and perf_year=" & CDbl(DDLYears.SelectedValue.ToString)
                    'Response.Write(SQL) : Response.End()
                    DT = LocalClass.ExecuteSQLDataSet(SQL)
                    Session("YEAR") = Session("YEAR_Appraisal")
                    If CDbl(DT.Rows(0)("Process_Flag").ToString) = 4 Then
                        Response.Redirect("Staff/Exempt/Appraisal.aspx?Token=" & y + lblEMPLID.Text)
                    Else
                        Session("YEAR") = Session("YEAR_Appraisal")
                        Response.Write("<script>window.open('Staff/Exempt/Appraisal.aspx?Token=" + y + lblEMPLID.Text + "','_blank' );</script>")
                    End If

                Else  '2018
                    Session("YEAR") = Session("YEAR_Appraisal")
                    'Response.Write("Manager's 2018 and > Appraisal" & Session("YEAR"))
                    Response.Write("<script>window.open('Staff/Appraisal/Appraisal.aspx?Token=" + y + lblEMPLID.Text + "','_blank' );</script>")

                End If

            End If
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Redirect", "<script language='JavaScript'> alert('Please choose Appraisal year from list and click submit button');</script>")
        End If
    End Sub

    Protected Sub LoadYear_Goal()
        SQL = "select distinct Perf_Year from Appraisal_FutureGoals_Master_TBL where Process_Flag in (4,5) and emplid=" & Session("EMPLID_LOGON") & "  UNION "
        SQL &= "select distinct Perf_Year from guild_Appraisal_FutureGoal_Master_TBL where Process_Flag in (4,5) and emplid=" & Session("EMPLID_LOGON") & "   UNION "
        SQL &= "select distinct Perf_Year+1 from ME_Appraisal_Master_TBL where Process_Flag=5  and emplid=" & Session("EMPLID_LOGON") & "  UNION "
        SQL &= "select distinct Perf_Year from Appraisal_FutureGoals_Master_TBL where Perf_Year >=2019 /*and Process_Flag in (0,1,4,5)*/ and emplid=" & Session("EMPLID_LOGON") & " order by Perf_Year desc    "
        'Response.Write(SQL) ': Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYears_Goal.Items.Clear()
        DDLYears_Goal.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_Goal.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next

        LocalClass.CloseSQLServerConnection()
    End Sub
    Protected Sub Year_Change_Goal(sender As Object, e As EventArgs) Handles DDLYears_Goal.SelectedIndexChanged
        'Response.Write(DDLYears_Goal.SelectedValue.ToString & "<br>" & Session("EMPLID_LOGON") ) : Response.End()
        If DDLYears_Goal.SelectedValue.ToString > 0 Then
            DDLYears.SelectedIndex = -1 : DDLYears_MidPoint.SelectedIndex = -1
            Session("YEAR_MidPoint") = "" : Session("YEAR_Appraisal") = ""
            lblYear_Goal.Text = DDLYears_Goal.SelectedValue.ToString
            Session("YEAR_Goal") = lblYear_Goal.Text

            If DDLYears_Goal.SelectedValue.ToString > 2018 Then

                SQL = "select Process_Flag, Max(Window_Batch)Window_Batch from appraisal_futuregoals_master_tbl where emplid = " & Session("EMPLID_LOGON") & " and Perf_Year=" & DDLYears_Goal.SelectedValue & " group by Process_Flag"
                DT = LocalClass.ExecuteSQLDataSet(SQL)
                If CDbl(DT.Rows(0)("Process_Flag").ToString) = 0 Then
                    SQL1 = "Update appraisal_futuregoals_master_tbl Set Window_Batch=" & CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                    SQL1 &= " where emplid = " & Session("EMPLID_LOGON") & " and Perf_Year=" & DDLYears_Goal.SelectedValue
                    'Response.Write(SQL1) : Response.End()
                    DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                    LocalClass.CloseSQLServerConnection()
                Else
                    Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString)
                End If
            End If

        Else
            Session("YEAR_Goal") = ""
        End If
    End Sub

    Protected Sub Submit_Goal1_Click(sender As Object, e As EventArgs) Handles Submit_Goal1.Click

        y = DDLYears_Goal.SelectedValue.ToString
        z = Session("Window_Batch")

        If Len(Session("YEAR_Goal")) > 0 Then

            If DDLYears_Goal.SelectedValue.ToString <= 2018 Then

                If Session("SAP") = "GLD" Then
                    If DDLYears_Goal.SelectedValue.ToString <= 2016 Then

                        Response.Redirect("Retro/Guild/Guild_FutureGoal.aspx?Token=" & lblEMPLID.Text)
                    Else
                        Session("YEAR") = Session("YEAR_Goal")
                        'Response.Redirect("Staff/Guild/Appraisal.aspx?Token=" & lblEMPLID.Text)'
                        Response.Write("<script>window.open('Staff/FutureGoals/FutureGoal.aspx?Token=" + y + lblEMPLID.Text + "','_blank' );</script>")
                    End If

                Else
                    If DDLYears_Goal.SelectedValue.ToString <= 2016 Then

                        Response.Redirect("Retro/Exempt/My_Appraisal.aspx?Token=" & lblEMPLID.Text)
                    Else
                        Session("YEAR") = Session("YEAR_Goal")
                        'Response.Redirect("Staff/Guild/Appraisal.aspx?Token=" & lblEMPLID.Text)'
                        Response.Write("<script>window.open('Staff/FutureGoals/FutureGoal.aspx?Token=" + y + lblEMPLID.Text + "','_blank' );</script>")
                    End If
                End If

            ElseIf DDLYears_Goal.SelectedValue.ToString = 2019 Then  '2019 
                Session("YEAR") = Session("YEAR_Goal")
                'Response.Redirect("Staff/Guild/Appraisal.aspx?Token=" & lblEMPLID.Text)'
                Response.Write("<script>window.open('Staff/FutureGoals/FutureGoals.aspx?Token=" + y + lblEMPLID.Text + z + "','_blank' );</script>")

            Else '2020
                Session("YEAR") = Session("YEAR_Goal")
                'Response.Redirect("Staff/Guild/Appraisal.aspx?Token=" & lblEMPLID.Text)'
                Response.Write("<script>window.open('Staff/FutureGoals/myGoals.aspx?Token=" + y + lblEMPLID.Text + z + "','_blank' );</script>")


            End If

        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Redirect", "<script language='JavaScript'> alert('Please choose Goal year from list and click submit button');</script>")
        End If

        DDLYears_Goal.SelectedIndex = -1
        DDLYears.SelectedIndex = -1
        DDLYears_MidPoint.SelectedIndex = -1

    End Sub

    Protected Sub LoadYear_MidPoint()
        SQL = "select * from(select distinct Perf_Year from Appraisal_MidPoint_MASTER_tbl where emplid =" & Session("EMPLID_LOGON") & " UNION "
        SQL &= " select distinct Perf_Year from Guild_MidPoint_MASTER_tbl where emplid =" & Session("EMPLID_LOGON") & ")A order by Perf_Year desc"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        DDLYears_MidPoint.Items.Clear()
        DDLYears_MidPoint.Items.Add(New ListItem(" Choose Year ", "0"))
        For i = 0 To DT.Rows.Count - 1
            DDLYears_MidPoint.Items.Add(New ListItem(DT.Rows(i)("Perf_Year").ToString, DT.Rows(i)("Perf_Year").ToString))
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub
    Protected Sub Year_Change_MidPoint(sender As Object, e As EventArgs) Handles DDLYears_MidPoint.SelectedIndexChanged
        If DDLYears_MidPoint.SelectedValue.ToString > 0 Then
            DDLYears.SelectedIndex = -1 : DDLYears_Goal.SelectedIndex = -1
            Session("YEAR_Goal") = "" : Session("YEAR_Appraisal") = ""
            lblYear_MidPoint.Text = DDLYears_MidPoint.SelectedValue.ToString
            Session("YEAR_MidPoint") = lblYear_MidPoint.Text
        Else
            Session("YEAR_MidPoint") = ""
        End If




    End Sub
    Protected Sub SubmitMidPoint1_Click(sender As Object, e As EventArgs) Handles SubmitMidPoint1.Click
        Session("YEAR_MidPoint") = lblYear_MidPoint.Text

        x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(lblEMPLID.Text, "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")

        If DDLYears_MidPoint.SelectedValue.ToString = 2016 Then
            Response.Redirect("Retro/Guild/Guild_MidPoint.aspx?Token=" & lblEMPLID.Text)
        ElseIf DDLYears_MidPoint.SelectedValue.ToString > 2016 Then
            'Response.Redirect("Staff/Guild/MidPoint.aspx?Token=" & lblEMPLID.Text)
            Response.Write("<script>window.open('Staff/Guild/MidPoint.aspx?Token=" + x + "','_blank' );</script>")
        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Redirect", "<script language='JavaScript'> alert('Please choose Mid Point year from list and click submit button');</script>")
        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("Default.aspx")
    End Sub
End Class