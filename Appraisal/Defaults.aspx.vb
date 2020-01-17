Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.DirectoryServices
Imports System.Configuration

Partial Public Class _Defaults
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

    Dim DT, DT1, DT2, DT3 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.AddHeader("Refresh", "840")

        LoginBTN.Attributes.Add("onMouseOver", "this.style.cursor='pointer';")
        'Response.Write(Session("UP_MGT_VIEW") & "<br>" & Len(Session("UP_MGT_VIEW"))) : Response.End()
        'Response.Write(Session("YEAR") & "<br>" & Session("EMPLID")) : Response.End()
        Session("TYPE") = Left(Trim(Request.QueryString("Token")), 3)
        Session("YEAR") = Mid(Trim(Request.QueryString("Token")), 4, 4)
        Session("EMPLID") = Mid(Trim(Request.QueryString("Token")), 8, 4)
        Session("MGT") = Mid(Trim(Request.QueryString("Token")), 12, 4)


    End Sub
    Protected Sub LoginBTN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoginBTN.Click

        Session("NETID") = ""

        If Session("NETID") = Nothing Then
            SQL = "select count(*)CNT_EMP from Appraisal_master_tbl where MGT_EMPLID in(" & Session("MGT") & ") and Perf_Year=" & Session("YEAR") & " and emplid=" & Session("EMPLID") & " "
            SQL &= " and UP_MGT_EMPLID in (select emplid from ps_employees where NETID='" & Trim(IDTXT.Text) & "')"
            'Response.Write(SQL) : Response.End()
            DT = LocalClass.ExecuteSQLDataSet(SQL)
        End If

        If CDbl(DT.Rows(0)("CNT_EMP").ToString) = 0 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Your NETID or Password is incorrect.'); </script>")
            Response.Write("<table width=100%><tr><td height=200px></td></tr><tr><td>")
            Response.Write("<p align=center><font color=blue><b>Thank you for your interest in accessing this application. However, you are not eligible to use this feature of CRNet.</b></p>")
            Response.Write("<p align=center><b>Only upper manager is authorized.</b></p>")
            Response.Write("<p align=center><b>If you feel this is incorrect, please contact IT support at extension 2003.</b></p>")
            Response.Write("<p align=center><a href='https://sites.google.com/a/consumer.org/crnet/crnethome'><img src=Images/Back_button.png style=width:50px></a></p></td></tr></table>")
            Response.End()

        ElseIf CDbl(DT.Rows(0)("CNT_EMP").ToString) > 0 Then
            '--Get mimic password--
            SQL1 = "select Rtrim(Ltrim(PSWRD))PSWRD from id_tbl where Rtrim(Ltrim(emplid))=1010"
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

            If Trim(PSTXT.Text) = "" & DT1.Rows(0)("PSWRD").ToString & "" Then
                'Response.Write(Trim(Session("TYPE"))) : Response.End()
                If Trim(Session("TYPE")) = "GLD" Then
                    Response.Redirect("Staff/HR/Guild_Appraisal1.aspx?Token=" & Trim(Session("EMPLID")))
                Else
                    Response.Redirect("Staff/HR/Manager_Appraisal1.aspx?Token=" & Trim(Session("EMPLID")))
                End If
            Else
                ValidateActiveDirectoryLogin(Trim(IDTXT.Text), Trim(PSTXT.Text))
            End If
        End If

        LocalClass.CloseSQLServerConnection()

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
            'If Session("NETID") = Nothing Then
            'SQL = "select count(*)CNT_EMP from Appraisal_master_tbl where MGT_EMPLID in(" & Session("MGT") & ") and Perf_Year=" & Session("YEAR") & " and emplid=" & Session("EMPLID") & " "
            'SQL &= " and UP_MGT_EMPLID in (select emplid from ps_employees where NETID='" & Trim(IDTXT.Text) & "')"
            'Response.Write(SQL) : Response.End()
            'DT = LocalClass.ExecuteSQLDataSet(SQL)
            'End If

            'If CDbl(DT.Rows(0)("CNT_EMP").ToString) = 0 Then
            'ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Your NETID or Password is incorrect.'); </script>")
            'Response.Write("<table width=100%><tr><td height=200px></td></tr><tr><td>")
            'Response.Write("<p align=center><font color=blue><b>Thank you for your interest in accessing this application. However, you are not eligible to use this feature of CRNet.</b></p>")
            'Response.Write("<p align=center><b>Only upper manager is authorized.</b></p>")
            'Response.Write("<p align=center><b>If you feel this is incorrect, please contact IT support at extension 2003.</b></p>")
            'Response.Write("<p align=center><a href='https://sites.google.com/a/consumer.org/crnet/crnethome'><img src=Images/Back_button.png style=width:50px></a></p></td></tr></table>")
            'Response.End()

            'ElseIf CDbl(DT.Rows(0)("CNT_EMP").ToString) > 0 Then
            '--Get mimic password--
            'SQL1 = "select Rtrim(Ltrim(PSWRD))PSWRD from id_tbl where Rtrim(Ltrim(emplid))=1010"
            'DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            'If Trim(PSTXT.Text) = "" & DT1.Rows(0)("PSWRD").ToString & "" Then
            'Response.Write(Trim(Session("TYPE"))) : Response.End()
            If Trim(Session("TYPE")) = "GLD" Then
                Response.Redirect("Staff/HR/Guild_Appraisal1.aspx?Token=" & Trim(Session("EMPLID")))
            Else
                Response.Redirect("Staff/HR/Manager_Appraisal1.aspx?Token=" & Trim(Session("EMPLID")))
            End If
            'Else
            'ValidateActiveDirectoryLogin(Trim(IDTXT.Text), Trim(PSTXT.Text))
            'End If
            'End If
        End If

        LocalClass.CloseSQLServerConnection()

    End Function

End Class