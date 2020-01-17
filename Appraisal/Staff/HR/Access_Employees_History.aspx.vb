Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Public Class Access_Employees_History
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, x, z, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Write("<center> Employee history going here " & Request.QueryString("Token") & "<br>" & Session("Year"))

        EmployeeHistory()

    End Sub

    Protected Sub EmployeeHistory()
        SqlDataSource1.SelectCommand = "select LOGIN_EMPLID,EMPLID,Perf_Year,MGT_EMPLID,UP_MGT_EMPLID,HR_EMPLID,DateTime,"
        SqlDataSource1.SelectCommand &= "(case when Login_Emplid<9999 then (select last+','+first from id_tbl where EMPLID=A.LOGIN_EMPLID) else 'SYSTEM' end)Change_Made_by,"
        SqlDataSource1.SelectCommand &= "(select last+','+first from id_tbl where EMPLID=A.EMPLID)Employee,"
        SqlDataSource1.SelectCommand &= "(select last+','+first from id_tbl where EMPLID=Rtrim(Ltrim(A.MGT_EMPLID)))MGT#1,"
        SqlDataSource1.SelectCommand &= "(select last+','+first from id_tbl where EMPLID=Rtrim(Ltrim(A.UP_MGT_EMPLID)))MGT#2,"
        SqlDataSource1.SelectCommand &= "(select last+','+first from id_tbl where EMPLID=A.HR_EMPLID)Generalist"
        SqlDataSource1.SelectCommand &= " from Appraisal_MasterHistory_tbl A where EMPLID=" & RTrim(Request.QueryString("Token")) & " and Perf_Year=" & Session("YEAR") & " and Login_Emplid>0  order by DateTime desc"

        SQL = "select First+' '+Last Name from id_tbl where emplid=" & RTrim(Request.QueryString("Token"))
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        lblEmployee.Text = DT.Rows(0)("Name").ToString
        LocalClass.CloseSQLServerConnection()
    End Sub



End Class