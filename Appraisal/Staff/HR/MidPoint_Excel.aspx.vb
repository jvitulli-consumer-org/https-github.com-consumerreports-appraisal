Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Public Class Appraisal_MidPoint_Excel
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
        lblSAP.Text = Left(Trim(Request.QueryString("Token")), 1)
        lblYEAR.Text = Right(Trim(Request.QueryString("Token")), 4)
        lblGENERALIST_EMPLID.Text = Session("HR_EMPLID")
        'Response.Write("Generalist  " & Session("HR_EMPLID"))
        Response.Write("<table width=100% border=0><tr><td width=20%></td><td align=center><img src=../../images/CR_logo.png width=380px height=60px /></td><td width=20%></td></tr>")

        '1 - OLD Guild Report
        '2 - OLD Manager/Exempt Report
        '3 - New Guild Report
        '4 - New Manager/Exempt Report
        '5 - All Employees report

        If CDbl(lblSAP.Text) = 1 Then
            GuildMidPoint()
        ElseIf CDbl(lblSAP.Text) = 2 Then
            ExemptMidPoint()
        End If

    End Sub

    Protected Sub GuildMidPoint()
        SQL = "select * from(select name 'Employee Name',(case when Met_Mgt+Not_Met_Mgt=1 then convert(char(10),TimeStamp,101) else 'Not Filed' end)Status,"
        SQL &= "(select name from ps_employees where emplid=A.SUP_EMPLID)'Manager name',(select email from id_tbl where emplid=A.SUP_EMPLID)'Manager Email',"
        SQL &= "(select name from ps_employees where emplid=A.HR_EMPLID)Generalist,Deptname 'Department',"
        SQL &= "Guild_Comments 'Employee Comments' from (select * from Appraisal_MidPoint_MASTER_tbl where emplid in (select emplid from ps_employees where class='U') "
        SQL &= "UNION select * from Guild_MidPoint_MASTER_tbl where emplid in (select emplid from ps_employees where class='U') )A JOIN ps_employees B "
        SQL &= "ON A.emplid=B.emplid where perf_year=" & lblYEAR.Text & ")A order by Generalist"
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        Dim attachment As String = "attachment; filename=Guild_Appraisal_Report.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/vnd.ms-excel"
        Dim tab1 As String = ""

        Response.Write("<table border=1><tr><td colspan=7 align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Guild MidPoint details</b></font></td></tr><tr>")

        For Each dc As DataColumn In DT.Columns
            Response.Write("<td>")
            Response.Write("<b>" + tab1 + dc.ColumnName)
            tab1 = vbTab
        Next
        Response.Write(vbLf)

        Response.Write("</td></tr><tr>")

        'Dim i As Integer
        For Each dr As DataRow In DT.Rows
            tab1 = ""
            For i = 0 To DT.Columns.Count - 1
                Response.Write("<td>")
                Response.Write(tab1 & dr(i).ToString())
                tab1 = vbTab
            Next
            Response.Write(vbLf)

            Response.Write("</td></tr>")

        Next

        Response.[End]()

        Response.Write("</table>")
        'export to excel
        LocalClass.CloseSQLServerConnection()

    End Sub

    Protected Sub ExemptMidPoint()

    End Sub
End Class