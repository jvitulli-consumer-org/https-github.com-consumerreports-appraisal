Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Public Class MidPoint_Reports
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
        Session("YEAR_MidPoint") = CDbl(lblYEAR.Text)

        Session("MGT_EMPLID") = lblEmplid.Text

        Response.Write("<table width=100% border=0><tr><td width=20%></td><td align=center><img src=../../images/CR_logo.png width=380px height=60px /></td><td width=20%></td></tr>")
        Response.Write("<tr><td width=20%></td><td align=center><font size=5 color=blue><b>" & lblYEAR.Text & " Guild Employees MidPoint details</b></font></td><td width=20%></td></tr></table>")

        If CDbl(lblSAP.Text) = 1 Then
            'Response.Write(lblGENERALIST_EMPLID.Text)

            MidPoint_Report()
        End If

    End Sub


    Protected Sub MidPoint_Report()
        Dim i As Integer

        SQL1 = "select * from(select count(*)Counts,'Not File'Status1  from(select * from Appraisal_MidPoint_MASTER_tbl UNION select * from Guild_MidPoint_MASTER_tbl"
        SQL1 &= " )A JOIN ps_employees B ON a.EMPLID=b.EMPLID where class='U' and perf_Year=" & lblYEAR.Text & "  and Met_Mgt=0 UNION"
        SQL1 &= " select count(*)Counts,'Filed'Status1 from(select * from Appraisal_MidPoint_MASTER_tbl UNION select * from Guild_MidPoint_MASTER_tbl"
        SQL1 &= " )A JOIN ps_employees B ON a.EMPLID=b.EMPLID where class='U' and perf_Year=" & lblYEAR.Text & "  and Met_Mgt=1 )AA order by status1"
        'Response.Write(SQL1) ': Response.End()
        DT1 = LocalClass.ExecuteSQLDataSet(SQL1)

        Response.Write("<table border=0  width=100%><tr><td width=30%>&nbsp;&nbsp;</td><td width=40% valign=top>")
        Response.Write("<center><table border=1 cellspacing=0 cellpadding=0 style=border-color:silver><tr><td width=30%><center><b>&nbsp;Not Filed&nbsp;</b></center></td><td><b>&nbsp;&nbsp;Status</b></td></tr>")
        For i = 0 To DT1.Rows.Count - 1
            Response.Write("<tr><td align=center><b>" & DT1.Rows(i)("Counts").ToString & "</b></td><td>&nbsp;&nbsp;" & DT1.Rows(i)("Status1").ToString & "</b></td></tr>")
        Next
        Response.Write("</table></center><td width=30%>&nbsp;&nbsp;</tr></table>")

        'SqlDataSource1.SelectCommand &= " select Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(EMPLID,'0', 'a'), '1', 'z'), '2', 'x'), '3', 'd'), '4', 'v'), '5', 'g'), '6', 'n'), '7', 'k'), '8', 'i'), '9', 'q')Token,"
        'SqlDataSource1.SelectCommand &= " *,'aaa' email from(select A.emplid,name,(case when Met_Mgt+Not_Met_Mgt=1 then convert(char(10),TimeStamp,101) else 'Not Filed' end)Status,(select name from ps_employees where emplid=A.SUP_EMPLID)Manager,"
        'SqlDataSource1.SelectCommand &= " (select name from ps_employees where emplid=A.HR_EMPLID)HR_Name,Deptname,(case when Len(Guild_Comments)>2 then 'YES' else '' end)Comments from(select * from Appraisal_MidPoint_MASTER_tbl where emplid in "
        'SqlDataSource1.SelectCommand &= " (select emplid from ps_employees where class='U') UNION select * from Guild_MidPoint_MASTER_tbl  where emplid in (select emplid from ps_employees where class='U') )A JOIN ps_employees B ON A.emplid=B.emplid"
        'SqlDataSource1.SelectCommand &= " where perf_year=" & lblYEAR.Text & ")B order by HR_Name"

        SqlDataSource1.SelectCommand = " select Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(EMPLID,'0', 'a'), '1', 'z'), '2', 'x'), '3', 'd'), '4', 'v'), '5', 'g'), '6', 'n'), '7', 'k'), '8', 'i'), '9', 'q')Token,"
        SqlDataSource1.SelectCommand &= " * from(select A.emplid,name,(case when Met_Mgt+Not_Met_Mgt=1 then convert(char(10),TimeStamp,101) else 'Not Filed' end)Status,SUP_EMPLID,"
        SqlDataSource1.SelectCommand &= " (select email from id_tbl where emplid=A.SUP_EMPLID)Sup_Email,(select name from ps_employees where emplid=A.SUP_EMPLID)Manager,HR_EMPLID,"
        SqlDataSource1.SelectCommand &= " (select name from ps_employees where emplid=A.HR_EMPLID)HR_Name,Deptname,(case when Len(Guild_Comments)>2 then 'YES' else '' end)Comments from"
        SqlDataSource1.SelectCommand &= " (select * from Appraisal_MidPoint_MASTER_tbl where emplid in (select emplid from ps_employees where class='U') UNION select * from Guild_MidPoint_MASTER_tbl  "
        SqlDataSource1.SelectCommand &= " where emplid in (select emplid from ps_employees where class='U') )A JOIN ps_employees B ON A.emplid=B.emplid where perf_year=" & lblYEAR.Text & ")B  order by HR_Name"


    End Sub

    Protected Sub GridView1_ColorChange(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i As Integer = 0 To e.Row.Cells.Count - 1
                If e.Row.Cells(i).Text = "Not Filed" Then
                    'e.Row.Cells(i).BorderColor = Drawing.Color.Red
                    'e.Row.Cells(i).BorderWidth = 3
                    e.Row.Cells(i).ForeColor = Drawing.Color.Red
                    e.Row.Cells(i).Font.Bold = True
                End If
            Next
        End If
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Response.Redirect("MidPoint_Excel.aspx?Token=" & Request.QueryString("Token"))
    End Sub
End Class