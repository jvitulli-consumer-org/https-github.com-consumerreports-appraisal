Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Public Class Print_MidPoint
    Inherits System.Web.UI.Page
    Dim SQL, SQL1, SQL2, SQL3, SQL4, SQL5, SQL6, SQL7, SQL8, SQL9, SQL10, SQL1_1, SQL2_1, ReturnValue As String
    Dim LocalClass As New CUSharedLocalClass
    Dim LogonClass As New LogonClass
    Dim ServerName As String
    Dim EmailMsg As String
    Dim SendTo As String
    Dim TempList As DropDownList
    Dim TempValue As String
    Dim DT, DT1, DT2, DT3, DT4, DT5, DT6, DT7, DT8, DT9, DT10, DT1_1 As New DataTable
    Dim DR As DataRow
    Dim DS As New DataSet
    Dim Msg, Msg1, Subj, x, x1 As String
    Dim EMPLID As String

    Dim CountRows1, CountRows2, CountRows3, CountRows4, CountRows5, CountRows6, CountRows7, CountRows8, CountRows9, CountRows10, CountRows11, CountRows12, CountRows13, CountRows14 As Integer
    Dim KeyTaskRows1, KeyTaskRows2, KeyTaskRows3, KeyTaskRows4, KeyTaskRows5, KeyTaskRows6, KeyTaskRows7, KeyTaskRows8, KeyTaskRows9, KeyTaskRows10, KeyTaskRows11, KeyTaskRows12, KeyTaskRows13, KeyTaskRows14 As Integer

    Protected TempDataView As DataView = New DataView
    Protected TempDataView2 As DataView = New DataView
    Public sqlConn As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("NETID") = "" Then Response.Redirect("default.aspx")

        DisplayData()

    End Sub

    Protected Sub DisplayData()

        SQL = "select (select last+','+first from id_tbl where emplid=A.emplid)Name,(select First+' '+Last from id_tbl where emplid=A.emplid)FistLastName,EMPLID,Title,Department,Deptid,Hired,Perf_Year,(select First+' '+Last from id_tbl "
        SQL &= " where emplid=A.SUP_EMPLID)Sup_Name,SUP_EMPLID,(select First+' '+Last from id_tbl where emplid=A.UP_MGT_EMPLID)Up_Sup_Name,UP_MGT_EMPLID,(select First+' '+Last from id_tbl where emplid=A.HR_EMPLID)HR_Name,HR_EMPLID,"
        SQL &= " (case when len(Guild_Comments)<2 then '' else Guild_Comments end)Guild_Comments,Date_NotMet_Mgt,TimeStamp,Met_Mgt,Not_Met_Mgt,convert(char(10),Date_Met_Mgt,101)Date_Met_Mgt,(select email from id_tbl where emplid=A.EMPLID)"
        SQL &= " GLD_Email,(select email from id_tbl where emplid=A.SUP_EMPLID)Mgr_Email,(select email from id_tbl where emplid=A.UP_MGT_EMPLID)Up_Mgr_Email,(select email from id_tbl where emplid=A.HR_EMPLID)Hr_Email "
        SQL &= " from Guild_MidPoint_MASTER_tbl A where Perf_Year=" & Request.QueryString("Token") & " order by name"
        'Response.Write(SQL) : Response.End()
        DT = LocalClass.ExecuteSQLDataSet(SQL)

        For i = 0 To DT.Rows.Count - 1
            Response.Write("<table class=StyleBreak style=width:100%>")
            Response.Write("<tr><td colspan=6 style=text-align:center;><img alt=../../../images/CR_logo.png src=../../../images/CR_logo.png style=width: 380px; height:60px /></td></tr>")
            Response.Write(" <tr><td colspan=6 class=Style1><u>Mid Point Conversation Acknowledgement</u></td></tr>")
            Response.Write("<tr><td colspan=6 style=height:40px;>&nbsp;&nbsp;</td></tr>")
            Response.Write("<tr><td style=width:15%>&nbsp;</td>")

            Response.Write("<td class=style4><b>Name</b></td><td class=style7>" & DT.Rows(i)("FistLastName").ToString & "</td>")
            Response.Write("<td class=style12><b>Manager Name:</b></td><td class=style8>" & DT.Rows(i)("Sup_Name").ToString & "</td><td style=width:15%></td></tr>")
            Response.Write("<tr><td style=width:15%></td>")
            Response.Write("<td class=style4><b>Title:</b></td><td class=style7>" & DT.Rows(i)("title").ToString & "</td>")
            Response.Write("<td class=style12><b>2nd Level Manager Name:</b></td><td class=style8>" & DT.Rows(i)("Up_Sup_Name").ToString & "</td><td style=width:15%></td></tr>")
            Response.Write("<tr><td style=width:15%></td>")
            Response.Write("<td class=style4><b>Department:</b></td><td class=style7>" & DT.Rows(i)("Department").ToString & "</td>")
            Response.Write("<td class=style12><b>Human Resources Generalist:</b></td><td class=style8>" & DT.Rows(i)("HR_Name").ToString & "</td><td style=width:15%></td></tr>")
            Response.Write("<tr><td style=width:15%></td>")
            Response.Write("<td class=style4>Hire Date:</td><td>" & DT.Rows(i)("Hired").ToString & "</td>")
            Response.Write("<td class=style12><b>Time Stamp(when signed):</b></td><td class=style8>" & Session("TimeStamp") & " </td><td style=width:15%></td></tr>")
            Response.Write("<tr><td colspan=6 style=height:25px;></td></tr>")

            Response.Write("<tr><td>&nbsp;&nbsp;</td><td colspan=4 class=Style3><u>Acknowledgement:</u></td><td>&nbsp;&nbsp;</td></tr>")

            Response.Write("<tr><td>&nbsp;&nbsp;</td><td colspan=4>")
            Response.Write("<table width=100% style=height:70px>")
            Response.Write("<tr><td colspan=6>&nbsp;&nbsp;</td></tr>")
            If DT.Rows(i)("Met_Mgt").ToString = 1 Then
                Response.Write("<tr><td colspan=2 style=text-align:center;><img alt=../../../images/CheckGreen.png src=../../../images/CheckGreen.png /></td>")
                Response.Write("<td colspan=5 style=font-size:large; font-family:Calibri; vertical-align:buttom;>I confirm that <b>I have met</b> with my manager to have a mid point performance conversation on " & DT.Rows(i)("Date_Met_Mgt").ToString & "</td></tr>")
                Response.Write("<tr><td colspan=6>&nbsp;&nbsp;</td></tr>")

                Response.Write("<tr><td colspan=6 style=font-size:small; font-family:Calibri;>Comments <i>(optional)</i>:</td></tr>")
                If Len(DT.Rows(i)("Guild_Comments").ToString) = 0 Then
                    Response.Write("<tr><td colspan=6 style=border-color:gray; font-family:Calibri;>No Comments</td></tr>")
                Else
                    Response.Write("<tr><td colspan=6 style=border-color:gray; font-family:Calibri;>" & DT.Rows(i)("Guild_Comments").ToString & "</td></tr>")
                End If

            ElseIf DT.Rows(i)("Not_Met_Mgt").ToString = 1 Then
                Response.Write("<tr><td colspan=2 style=text-align:center;><img alt=../../../images/CheckGreen.png src=../../../images/CheckGreen.png /></td>")
                Response.Write("<td colspan=5 style=font-size:large; font-family:Calibri; vertical-align:buttom;><b>I have not met </b> with my manager to have a mid point performance conversation. " & DT.Rows(i)("Date_NotMet_Mgt").ToString & "</td></tr>")
                Response.Write("<tr><td colspan=6>&nbsp;&nbsp;</td></tr>")

                Response.Write("<tr><td colspan=6 style=font-size:small; font-family:Calibri;>Comments <i>(optional)</i>:</td></tr>")
                If Len(DT.Rows(i)("Guild_Comments").ToString) = 0 Then
                    Response.Write("<tr><td colspan=6 style=border-color:gray; font-family:Calibri;>No Comments</td></tr>")
                Else
                    Response.Write("<tr><td colspan=6 style=border-color:gray; font-family:Calibri;>" & DT.Rows(i)("Guild_Comments").ToString & "</td></tr>")
                End If

            ElseIf CDbl(DT.Rows(i)("Not_Met_Mgt").ToString) + CDbl(DT.Rows(i)("Met_Mgt").ToString) = 0 Then
                Response.Write("<tr><td colspan=2 style=text-align:center;><img alt=../../../images/CheckBlank.png src=../../../images/CheckBlank.png /></td>")
                Response.Write("<td colspan=5 style=font-size:large; font-family:Calibri; vertical-align:buttom;><b>I have not submitted a mid point performance conversation. </td></tr>")
                Response.Write("<tr><td colspan=6>&nbsp;&nbsp;</td></tr>")
                Response.Write("<tr><td colspan=6 style=font-size:small; font-family:Calibri;>Comments <i>(optional)</i>:</td></tr>")
            End If
            Response.Write("</table></td><tr/>")
            Response.Write("<tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr></table>")
        Next
        LocalClass.CloseSQLServerConnection()

    End Sub

End Class