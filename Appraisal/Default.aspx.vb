Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.DirectoryServices
Imports System.Configuration

Partial Public Class _Default
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

    Dim DT, DT1, DT2, DT3 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", "840")

        'Response.Write("1  " & Request.QueryString("Nav")) ': Response.End()

        LoginBTN.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")

        If Len(Request.QueryString("Token")) >= 4 Then

            y = Mid(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), _
                         "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9"), 5, 4)

            SQL1 = "select count(*)CNT_EMP from Appraisal_master_tbl where MGT_EMPLID=" & y & " and Perf_Year=" & Session("YEAR")
            'Response.Write(Session("DEPTID") & "<br>" & SQL1) : Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            If CDbl(DT1.Rows(0)("CNT_EMP").ToString) > 0 Then
                'Response.Write("Redirect to Manager page, because has employee") : Response.End()
                If Session("DEPTID") = 9009120 Then
                    Response.Redirect("Default_HR.aspx?Token=" & Mid(Request.QueryString("Token"), 5, 4))
                Else
                    Response.Redirect("Default_Manager.aspx?Token=" & Mid(Request.QueryString("Token"), 5, 4))
                End If
            Else
                If Session("DEPTID") = 9009120 Then
                    Response.Redirect("Default_HR.aspx?Token=" & Mid(Request.QueryString("Token"), 5, 4))
                Else
                    Response.Redirect("Default_Appaisal.aspx?Token=" & Mid(Request.QueryString("Token"), 5, 4))
                End If
                'Response.Write("Redirect to Employee page, no employee ")
            End If
            'Response.Redirect("Default_Appaisal.aspx?Token=" & Request.QueryString("Token"))
        End If

        DT = LocalClass.ExecuteSQLDataSet(SQL)
        'Response.Write("2  " & Request.QueryString("Nav"))

    End Sub
    Protected Sub LoginBTN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoginBTN.Click

        Session("EMPLID") = ""
        Session("EMPLID_LOGON") = ""
        Session("MGT_EMPLID") = ""
        Session("HR_EMPLID") = ""

        'Response.Write("3  " & Request.QueryString("Nav"))

        'SQL = "Select A.NETID,A.EMPLID,PSWRD,DEPTID from ID_tbl a, ps_employees b where a.emplid = b.emplid and A.NETID='" & Trim(IDTXT.Text) & "'"
        If Session("EMPLID") = Nothing Then
            SQL = "select netid,emplid,pswrd,deptid,DEPTNAME,name,last,first,sap,(case when SAP='GLD' then Manage_Employee else "
            SQL &= " (select count(*) from ps_employees where supervisor_id=AA.emplid) end)Manage_Employee from("
            SQL &= " Select A.NETID,A.EMPLID,A.PSWRD,DEPTID,DEPTNAME,Rights,b.NAME,Last,First,sal_admin_plan SAP,"
            SQL &= " (select count(*) from me_appraisal_master_tbl where SUP_emplid=b.EMPLID)+"
            SQL &= " (select count(*) from guild_appraisal_master_tbl where SUP_emplid=b.EMPLID)+"
            SQL &= " (select count(*) from Appraisal_Master_tbl where MGT_emplid=b.EMPLID)+"
            SQL &= " (select count(*) from Appraisal_FutureGoals_Master_tbl where MGT_emplid=b.EMPLID)Manage_Employee"
            SQL &= " from ID_tbl a,ps_employees b  where a.EMPLID = b.EMPLID and A.NETID='" & Trim(IDTXT.Text) & "')AA "
            'Response.Write(SQL) : Response.End()
        End If
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        'Response.Write(Trim(DT.Rows(0)("NETID").ToString)) : Response.End()
        'Response.Write("4  " & Request.QueryString("Nav")) ': Response.End()


        If DT.Rows.Count = 0 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Your NETID or Password is incorrect.'); </script>")
            Response.Write("<table width=100%><tr><td height=200px></td></tr><tr><td>")
            Response.Write("<p align=center><font color=blue><b>Thank you for your interest in accessing this application. However, you are not eligible to use this feature of CRNet.</b></p>")
            Response.Write("<p align=center><b>Only active employees are authorized.</b></p>")
            Response.Write("<p align=center><b>If you feel this is incorrect, please contact IT support at extension 2003.</b></p>")
            Response.Write("<p align=center><a href='https://sites.google.com/a/consumer.org/crnet/crnethome'><img src=Images/Back_button.png style=width:50px></a></p></td></tr></table>")
            Response.End()

        ElseIf DT.Rows.Count > 0 Then

            Session("NETID") = Trim(DT.Rows(0)("NETID").ToString)
            Session("DEPTID") = Trim(DT.Rows(0)("DEPTID").ToString)
            Session("DEPTNAME") = DT.Rows(0)("DEPTNAME").ToString
            Session("NAME") = DT.Rows(0)("Name").ToString
            Session("SAP") = DT.Rows(0)("SAP").ToString
            Session("LAST") = DT.Rows(0)("Last").ToString
            Session("FIRST") = DT.Rows(0)("First").ToString
            Session("Manage_Employee") = DT.Rows(0)("Manage_Employee").ToString
            x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Trim(DT.Rows(0)("EMPLID").ToString), _
              "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")

            '--Get mimic password--
            SQL1 = "select Rtrim(Ltrim(PSWRD))PSWRD from id_tbl where Rtrim(Ltrim(emplid))=1010"
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            If Trim(PSTXT.Text) = "" & DT1.Rows(0)("PSWRD").ToString & "" Then
                'Response.Write("5  " & Request.QueryString("Nav") & "<br />") ' : Response.End()
                'If Len(Request.QueryString("Nav")) > 0 Then
                'If Left(Trim(Request.QueryString("Nav")), 2) = "FG" Then
                'If CDbl(DT.Rows(0)("Manage_Employee").ToString) = 0 Then '--Employee Logon
                'Session("EMPLID_LOGON") = Trim(DT.Rows(0)("EMPLID").ToString)
                'Response.Redirect("Staff/FutureGoals/FutureGoals.aspx?Token=" & Mid(Request.QueryString("Nav"), 3, 8))
                'Else                                                     '--Manager Logon
                'Session("MGT_EMPLID") = Trim(DT.Rows(0)("EMPLID").ToString)
                'y = Mid(Request.QueryString("Nav"), 3, 4)
                'z = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Trim(Mid(Request.QueryString("Nav"), 7, 4)), _
                ' "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")
                'SQL = "select Process_Flag,Max(Window_Batch)Window_Batch from appraisal_futuregoals_master_tbl where emplid = " & z & " and Perf_Year=" & y & " group by Process_Flag"
                'Response.Write(SQL) : Response.End()
                'DT = LocalClass.ExecuteSQLDataSet(SQL)
                'If DT.Rows(0)("Process_Flag").ToString = 0 Or DT.Rows(0)("Process_Flag").ToString = 2 Then
                'Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString)
                'Else
                'SQL1 = "Update appraisal_futuregoals_master_tbl Set Window_Batch=" & CDbl(DT.Rows(0)("Window_Batch").ToString) + 1 & " where emplid = " & z & " and Perf_Year=" & y & ""
                'Response.Write(SQL1) : Response.End()
                'DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
                'Session("Window_Batch") = CDbl(DT.Rows(0)("Window_Batch").ToString) + 1
                'End If
                'SQL2 = "select Count(*)Correct_Mgt  from ps_employees where emplid=" & z & " and supervisor_id=" & Session("MGT_EMPLID") & ""
                'Response.Write(SQL2) : Response.End()
                'DT2 = LocalClass.ExecuteSQLDataSet(SQL2)
                'If DT2.Rows(0)("Correct_Mgt").ToString = 0 Then
                'Response.Redirect("Staff/FutureGoals/FutureGoals.aspx?Token=" & Mid(Request.QueryString("Nav"), 3, 8) & Session("Window_Batch"))
                'Else
                'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('This employee doesnot belong to you.'); </script>")
                'End If
                'End If
                'End If
                'End If


                If CDbl(DT.Rows(0)("Manage_Employee").ToString) = 0 And Session("DEPTID") <> 9009120 Then
                    'Response.Write("redirect to employee's application") : Response.End()
                    Session("EMPLID_LOGON") = Trim(DT.Rows(0)("EMPLID").ToString)
                    Response.Redirect("Default_Appaisal.aspx?Token=" & x)
                Else
                    If DT.Rows(0)("SAP").ToString <> "GLD" And Session("DEPTID") = 9009120 Then
                        Session("HR_EMPLID") = Trim(DT.Rows(0)("EMPLID").ToString)
                        Response.Redirect("Default_HR.aspx?Token=" & x)
                    Else
                        Session("MGT_EMPLID") = Trim(DT.Rows(0)("EMPLID").ToString)
                        Response.Redirect("Default_Manager.aspx?Token=" & x)
                    End If
                End If
        Else
            'Response.Write("6  " & Request.QueryString("Nav")) : Response.End()
            ValidateActiveDirectoryLogin(Trim(IDTXT.Text), Trim(PSTXT.Text))
        End If
        End If

        LocalClass.CloseSQLServerConnection()

        'If Session("EMPLID") = Nothing Then
        'SQL = "Select A.NETID,A.EMPLID,A.PSWRD,DEPTID,DEPTNAME,Rights,b.NAME,Last,First,sal_admin_plan SAP,"
        'SQL &= " (select count(*) from me_appraisal_master_tbl where SUP_emplid=b.EMPLID)+"
        'SQL &= " (select count(*) from guild_appraisal_master_tbl where SUP_emplid=b.EMPLID)+"
        'SQL &= " (select count(*) from Appraisal_Master_tbl where MGT_emplid=b.EMPLID)+"
        'SQL &= " (select count(*) from Appraisal_FutureGoals_Master_tbl where MGT_emplid=b.EMPLID)Manage_Employee"
        'SQL &= " from ID_tbl a,ps_employees b  where a.EMPLID = b.EMPLID and A.NETID='" & Trim(IDTXT.Text) & "' and Pswrd='" & Trim(PSTXT.Text) & "' "
        'Response.Write(SQL) : Response.End()
        'End If
        'DT = LocalClass.ExecuteSQLDataSet(SQL)

        'If DT.Rows.Count > 0 Then
        'Session("NETID") = Trim(DT.Rows(0)("NETID").ToString)
        'Session("DEPTID") = Trim(DT.Rows(0)("DEPTID").ToString)
        'Session("DEPTNAME") = DT.Rows(0)("DEPTNAME").ToString
        'Session("NAME") = DT.Rows(0)("Name").ToString
        'Session("SAP") = DT.Rows(0)("SAP").ToString
        'Session("LAST") = DT.Rows(0)("Last").ToString
        'Session("FIRST") = DT.Rows(0)("First").ToString
        'Session("Manage_Employee") = DT.Rows(0)("Manage_Employee").ToString

        'x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Trim(DT.Rows(0)("EMPLID").ToString), _
        '  "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")

        'If CDbl(DT.Rows(0)("Manage_Employee").ToString) = 0 And Session("DEPTID") <> 9009120 Then
        'Response.Write("redirect to employee's application") : Response.End()
        'Session("EMPLID_LOGON") = Trim(DT.Rows(0)("EMPLID").ToString)
        'Response.Redirect("Default_Appaisal.aspx?Token=" & x)
        'Else
        'If DT.Rows(0)("SAP").ToString <> "GLD" And Session("DEPTID") = 9009120 Then
        'Session("HR_EMPLID") = Trim(DT.Rows(0)("EMPLID").ToString)
        'Response.Redirect("Default_HR.aspx?Token=" & x)
        'Else
        'Session("MGT_EMPLID") = Trim(DT.Rows(0)("EMPLID").ToString)
        'Response.Redirect("Default_Manager.aspx?Token=" & x)
        'End If
        'End If
        'Else
        'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Your NETID or Password is incorrect.'); </script>")
        'End If
        'LocalClass.CloseSQLServerConnection()



    End Sub

    Function ValidateActiveDirectoryLogin(ByVal Username As String, ByVal Password As String) As Boolean
        Dim Success As Boolean = False
        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://consumer.org", Username, Password)
        Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)
        Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel

        Try
            Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
            Success = Not (Results Is Nothing)
        Catch
            Success = False
        End Try

        If Success = False Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Your NETID or Password is incorrect.'); </script>")
        Else
            If Session("EMPLID") = Nothing Then
                SQL = "select netid,emplid,pswrd,deptid,DEPTNAME,name,last,first,sap,(case when SAP='GLD' then Manage_Employee else "
                SQL &= " (select count(*) from ps_employees where supervisor_id=AA.emplid) end)Manage_Employee from("
                SQL &= " Select A.NETID,A.EMPLID,A.PSWRD,DEPTID,DEPTNAME,Rights,b.NAME,Last,First,sal_admin_plan SAP,"
                SQL &= " (select count(*) from me_appraisal_master_tbl where SUP_emplid=b.EMPLID)+"
                SQL &= " (select count(*) from guild_appraisal_master_tbl where SUP_emplid=b.EMPLID)+"
                SQL &= " (select count(*) from Appraisal_Master_tbl where MGT_emplid=b.EMPLID)+"
                SQL &= " (select count(*) from Appraisal_FutureGoals_Master_tbl where MGT_emplid=b.EMPLID)Manage_Employee"
                SQL &= " from ID_tbl a,ps_employees b  where a.EMPLID = b.EMPLID and A.NETID='" & Trim(IDTXT.Text) & "')AA "
                'Response.Write(SQL) : Response.End()
            End If

            DT = LocalClass.ExecuteSQLDataSet(SQL)

            If DT.Rows.Count = 0 Then
                'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Your NETID or Password is incorrect.'); </script>")
                Response.Write("<p style=color: #420BEE;text-align: center;><strong>Thank you for your interest in accessing this application. However, you are not eligible to use this feature of CRNet.</strong></p>")
                Response.Write("<p style=color: #420BEE;text-align: center;><strong>Only active employees are authorized.</strong></p>")
                Response.Write("<p style=color: #420BEE;text-align: center;><strong>If you feel this is incorrect, please contact IT support at extension 2003.</strong></p>")
            ElseIf DT.Rows.Count > 0 Then

                Session("NETID") = Trim(DT.Rows(0)("NETID").ToString)
                Session("DEPTID") = Trim(DT.Rows(0)("DEPTID").ToString)
                Session("DEPTNAME") = DT.Rows(0)("DEPTNAME").ToString
                Session("NAME") = DT.Rows(0)("Name").ToString
                Session("SAP") = DT.Rows(0)("SAP").ToString
                Session("LAST") = DT.Rows(0)("Last").ToString
                Session("FIRST") = DT.Rows(0)("First").ToString
                Session("Manage_Employee") = DT.Rows(0)("Manage_Employee").ToString
                x = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Trim(DT.Rows(0)("EMPLID").ToString), _
                  "0", "a"), "1", "z"), "2", "x"), "3", "d"), "4", "v"), "5", "g"), "6", "n"), "7", "k"), "8", "i"), "9", "q")


                If CDbl(DT.Rows(0)("Manage_Employee").ToString) = 0 And Session("DEPTID") <> 9009120 Then
                    'Response.Write("redirect to employee's application") : Response.End()
                    Session("EMPLID_LOGON") = Trim(DT.Rows(0)("EMPLID").ToString)
                    Response.Redirect("Default_Appaisal.aspx?Token=" & x)
                Else
                    If DT.Rows(0)("SAP").ToString <> "GLD" And Session("DEPTID") = 9009120 Then
                        Session("HR_EMPLID") = Trim(DT.Rows(0)("EMPLID").ToString)
                        Response.Redirect("Default_HR.aspx?Token=" & x)
                    Else
                        Session("MGT_EMPLID") = Trim(DT.Rows(0)("EMPLID").ToString)
                        Response.Redirect("Default_Manager.aspx?Token=" & x)
                    End If
                End If
                
            End If
        End If
        LocalClass.CloseSQLServerConnection()

    End Function

End Class