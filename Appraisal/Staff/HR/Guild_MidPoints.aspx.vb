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
Public Class Guild_MidPoint1
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

        lblEMPLID.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Request.QueryString("Token"), "a", "0"), "z", "1"), "x", "2"), "d", "3"), "v", "4"), "g", "5"), "n", "6"), "k", "7"), "i", "8"), "q", "9")

        'Response.Write("supervisor Len " & Len(Session("MGR_EMPLID")) & "<br>")
        lblLogin_EMPLID.Text = Session("MGR_EMPLID")
        lblMidPoint_Year.Text = Session("Year_MidPoint")

        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 1080)) + 20)

        If Session("NETID") = "" Then Response.Redirect("default.aspx")

        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display") ': Response.End()
            'DisplayData()
        Else
            'Response.Write("<br>2. else Save Data before Display") ': Response.End()
            DisplayData()
        End If

        'Response.Write("Year " & Session("Year_MidPoint") & "<> Year " & lblMidPoint_Year.Text)


        'CheckBox_Met.Attributes.Add("onclick", "if(!confirm('To leave comments, please click the ""Cancel"" button.')) {return false};")
        'Response.Write(lblSUP_EMAIL.Text & "<br>" & lblGUILD_EMAIL.Text)

    End Sub

    Protected Sub DisplayData()

        If CDbl(Session("Year_MidPoint")) = 2016 Then

            SQL = "select (select last+','+first from id_tbl where emplid=A.emplid)Name,(select First+' '+Last from id_tbl where emplid=A.emplid)FistLastName,EMPLID,Title,Department,Deptid,Hired,Perf_Year,(select First+' '+Last from id_tbl "
            SQL &= " where emplid=A.SUP_EMPLID)Sup_Name,SUP_EMPLID,(select First+' '+Last from id_tbl where emplid=A.UP_MGT_EMPLID)Up_Sup_Name,UP_MGT_EMPLID,(select First+' '+Last from id_tbl where emplid=A.HR_EMPLID)HR_Name,HR_EMPLID,"
            SQL &= " (case when len(Guild_Comments)<2 then '' else Guild_Comments end)Guild_Comments,Date_NotMet_Mgt,TimeStamp,Met_Mgt,Not_Met_Mgt,convert(char(10),Date_Met_Mgt,101)Date_Met_Mgt,(select email from id_tbl where emplid=A.EMPLID)"
            SQL &= " GLD_Email,(select email from id_tbl where emplid=A.SUP_EMPLID)Mgr_Email,(select email from id_tbl where emplid=A.UP_MGT_EMPLID)Up_Mgr_Email,(select email from id_tbl where emplid=A.HR_EMPLID)Hr_Email "
            SQL &= " from Guild_MidPoint_MASTER_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year= " & Session("YEAR_MidPoint") & ""
        Else
            SQL = "select (select last+','+first from id_tbl where emplid=A.emplid)Name,(select First+' '+Last from id_tbl where emplid=A.emplid)FistLastName,EMPLID,Title,Department,Deptid,Hired,Perf_Year,(select First+' '+Last from id_tbl "
            SQL &= " where emplid=A.SUP_EMPLID)Sup_Name,SUP_EMPLID,(select First+' '+Last from id_tbl where emplid=A.UP_MGT_EMPLID)Up_Sup_Name,UP_MGT_EMPLID,(select First+' '+Last from id_tbl where emplid=A.HR_EMPLID)HR_Name,HR_EMPLID,"
            SQL &= " (case when len(Guild_Comments)<2 then '' else Guild_Comments end)Guild_Comments,Date_NotMet_Mgt,TimeStamp,Met_Mgt,Not_Met_Mgt,convert(char(10),Date_Met_Mgt,101)Date_Met_Mgt,"
            SQL &= " (select email from id_tbl where emplid=A.EMPLID)GLD_Email,(select email from id_tbl where emplid=A.SUP_EMPLID)Mgr_Email,(select email from id_tbl where emplid=A.UP_MGT_EMPLID)Up_Mgr_Email,"
            SQL &= " (select email from id_tbl where emplid=A.HR_EMPLID)Hr_Email from Appraisal_MidPoint_MASTER_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year= " & Session("YEAR_MidPoint") & ""
        End If
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()

        lblEMPLOYEE_NAME.Text = DT.Rows(0)("FistLastName").ToString
        lblEMPLOYEE_TITLE.Text = DT.Rows(0)("title").ToString
        lblEMPLOYEE_DEPT.Text = DT.Rows(0)("Department").ToString
        lblEMPLOYEE_HIRE.Text = DT.Rows(0)("Hired").ToString
        lblGUILD_EMAIL.Text = DT.Rows(0)("GLD_Email").ToString

        Session("FirsLastName") = DT.Rows(0)("FistLastName").ToString
        lblSUP_EMPLID.Text = DT.Rows(0)("SUP_EMPLID").ToString
        LblMgr_NAME.Text = DT.Rows(0)("Sup_Name").ToString
        lblSUP_EMAIL.Text = DT.Rows(0)("Mgr_Email").ToString

        lblUP_MGT_EMPLID.Text = DT.Rows(0)("UP_MGT_EMPLID").ToString
        lblMGR_UP_NAME.Text = DT.Rows(0)("Up_Sup_Name").ToString
        lblUP_MGT_EMAIL.Text = DT.Rows(0)("Up_Mgr_Email").ToString

        lblGENERALIST_NAME.Text = DT.Rows(0)("HR_Name").ToString
        lblHR_EMPLID.Text = DT.Rows(0)("HR_EMPLID").ToString
        lblHR_EMAIL.Text = DT.Rows(0)("Hr_Email").ToString
        Session("TimeStamp") = DT.Rows(0)("TimeStamp").ToString

        lblMidPoint_Year.Text = Trim(Right(Session("YEAR_MidPoint"), 2))
        lblTimeStamp.Text = DT.Rows(0)("TimeStamp").ToString

        If CDbl(DT.Rows(0)("Met_Mgt").ToString) = 1 Then
            'Response.Write("Met manager and discuss")
            Met.Visible = True
            Not_Met.Visible = False
            lblComments.Text = Replace(DT.Rows(0)("Guild_Comments").ToString, Chr(13), "<br>")
            lblTimeStamp.Text = Session("TimeStamp")
            LblDate.Text = Session("TimeStamp")

        ElseIf CDbl(DT.Rows(0)("Not_Met_Mgt").ToString) = 1 Then
            'Response.Write("Not Met manager for the long time")
            Met.Visible = False
            Not_Met.Visible = True
            lblDate1.Text = DT.Rows(0)("Date_NotMet_Mgt").ToString
            lblComments.Text = Replace(DT.Rows(0)("Guild_Comments").ToString, Chr(13), "<br>")
            LblDate.Text = Session("TimeStamp")

        End If

    End Sub

End Class