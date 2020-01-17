Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Public Class Appraisal_Reports_Redirect
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

        'Response.Write(Left(Trim(Request.QueryString("Token")), 7) & "<br>" & Trim(Mid(Request.QueryString("Token"), 8, 5)) & "<br>" & _
        '              Session("Year_Appr") & "<br>" & Session("MGT_EMPLID")) : Response.End()

        'Response.Write(Len(Trim(Request.QueryString("Token")))) : Response.End()
        'If Len(Trim(Request.QueryString("Token"))) >= 14 Then
        'Session("UP_MGT_VIEW") = Trim(Request.QueryString("Token"))
        'Response.Write(Session("UP_MGT_VIEW")) : Response.End()
        'Response.Redirect("/defaults.aspx")
        'Else

        SQL = "select * from"
        SQL &= " (select count(*)CNT_Appr_Exist from Appraisal_Master_tbl where emplid=" & Trim(Mid(Request.QueryString("Token"), 8, 5)) & " and Perf_Year=" & Session("Year_Appr") & " )A,"
        SQL &= " (select count(*)CNT_FutGoal_Exist from Appraisal_FutureGoals_Master_tbl where emplid=" & Trim(Mid(Request.QueryString("Token"), 8, 5)) & " and Perf_Year=" & Session("Year_Appr") + 1 & ")B,"
        SQL &= " (select count(*)CNT_MidPoint_Exist from Appraisal_MidPoint_MASTER_tbl where emplid=" & Trim(Mid(Request.QueryString("Token"), 8, 5)) & " and Perf_Year=" & Session("Year_Appr") + 1 & ")C,"
        SQL &= " (select count(*)CNT_UP_Mgr from Appraisal_Master_tbl where emplid=" & Trim(Mid(Request.QueryString("Token"), 8, 5)) & " and Perf_Year=" & Session("Year_Appr") & ")D"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)


        If CDbl(DT.Rows(0)("CNT_Appr_Exist").ToString) = 1 Or CDbl(DT.Rows(0)("CNT_FutGoal_Exist").ToString) = 1 Or CDbl(DT.Rows(0)("CNT_MidPoint_Exist").ToString) = 1 Then
            SQL1 = "select sal_admin_plan SAP from HR_PDS_DATA_tbl where emplid=" & Trim(Mid(Request.QueryString("Token"), 8, 5)) & "  UNION "
            SQL1 &= " select (case when sal_admin_plan1='GLD' then 14 else 11 end)SAP from id_tbl where emplid=" & Trim(Mid(Request.QueryString("Token"), 8, 5)) & ""
            'Response.Write(SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            '--Redirect to Appraisal

            If Session("Year_Appr") < 2018 Then

                If Left(Trim(Request.QueryString("Token")), 7) = "APP_ALL" Then
                    If CDbl(DT1.Rows(0)("SAP").ToString) = 14 And CDbl(DT.Rows(0)("CNT_Appr_Exist").ToString) = 1 Then
                        'Response.Write("All Employees form - Guild Appraisal" & Session("YEAR")) : Response.End()
                        Response.Redirect("Guild_Appraisal1.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))

                    ElseIf CDbl(DT1.Rows(0)("SAP").ToString) <> 14 And CDbl(DT.Rows(0)("CNT_Appr_Exist").ToString) = 1 Then
                        'Response.Write("All Employees form - Manager Appraisal") : Response.End()
                        Response.Redirect("Manager_Appraisal1.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))
                    Else
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'>alert('New Employee not eligible to file Appraisal.');{window.location.href = 'https://sites.google.com/a/consumer.org/crnet/crnethome';}</script>")
                    End If

                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "APP_MGT" And CDbl(DT.Rows(0)("CNT_Appr_Exist").ToString) = 1 Then
                    'Response.Write("Manager form - Manager Appraisal") : Response.End()
                    Response.Redirect("Manager_Appraisal1.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))

                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "APP_GLD" And CDbl(DT.Rows(0)("CNT_Appr_Exist").ToString) = 1 Then
                    'Response.Write("Guild form - Guild Appraisal") : Response.End()
                    Response.Redirect("Guild_Appraisal1.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))

                    '--Redirect to Goals
                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "GOL_ALL" Then
                    'Response.Write("All Employees form - Future Goals") : Response.End()

                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "GOL_MGT" Then
                    'Response.Write("Manager form - Future Goals") : Response.End()

                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "GOL_GLD" Then
                    'Response.Write("Guild form - Future Goals") : Response.End()

                    '--Redirect to MidPoint
                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "MID_GLD" Then
                    'Response.Write("Guild form - MidPoint  " & Session("Year_MidPoint")) : Response.End()
                    'Session("MidPoint_Year") = Session("Year_MidPoint")
                    Response.Redirect("Guild_MidPoints.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))

                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "MID_ALL" Then
                    'Response.Write("All Employees form - MidPoint") : Response.End()

                Else
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'>alert('New Employee not eligible to file Appraisal.');{window.location.href = 'https://sites.google.com/a/consumer.org/crnet/crnethome';}</script>")
                End If

                '--2018 and forward---
            ElseIf Session("Year_Appr") >= 2018 Then

                If Left(Trim(Request.QueryString("Token")), 7) = "APP_ALL" Then

                    If CDbl(DT1.Rows(0)("SAP").ToString) = 14 And CDbl(DT.Rows(0)("CNT_Appr_Exist").ToString) = 1 Then
                        'Response.Write("All Employees form - Guild Appraisal" & Session("YEAR")) : Response.End()
                        Response.Redirect("Appraisal1.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))

                    ElseIf CDbl(DT1.Rows(0)("SAP").ToString) <> 14 And CDbl(DT.Rows(0)("CNT_Appr_Exist").ToString) = 1 Then
                        'Response.Write("All Employees form - Manager Appraisal") : Response.End()
                        Response.Redirect("Appraisal1.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))
                    Else
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'>alert('New Employee not eligible to file Appraisal.');{window.location.href = 'https://sites.google.com/a/consumer.org/crnet/crnethome';}</script>")
                    End If

                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "APP_MGT" And CDbl(DT.Rows(0)("CNT_Appr_Exist").ToString) = 1 Then
                    'Response.Write("Manager form - Manager Appraisal") : Response.End()
                    Response.Redirect("Appraisal1.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))

                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "APP_GLD" And CDbl(DT.Rows(0)("CNT_Appr_Exist").ToString) = 1 Then
                    'Response.Write("Guild form - Guild Appraisal") : Response.End()
                    Response.Redirect("Appraisal1.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))

                    '--Redirect to MidPoint
                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "MID_GLD" Then
                    'Response.Write("Guild form - MidPoint  " & Session("Year_MidPoint")) : Response.End()
                    'Session("MidPoint_Year") = Session("Year_MidPoint")
                    Response.Redirect("Guild_MidPoints.aspx?Token=" & Trim(Mid(Request.QueryString("Token"), 8, 5)))

                ElseIf Left(Trim(Request.QueryString("Token")), 7) = "MID_ALL" Then
                    'Response.Write("All Employees form - MidPoint") : Response.End()


                Else
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'>alert('New Employee not eligible to file Appraisal.');{window.location.href = 'https://sites.google.com/a/consumer.org/crnet/crnethome';}</script>")
                End If


            End If

        Else
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'>alert('New Employee not eligible to file Appraisal.');{window.location.href = 'https://sites.google.com/a/consumer.org/crnet/crnethome';}</script>")
        End If

            LocalClass.CloseSQLServerConnection()

            'End If
    End Sub

End Class