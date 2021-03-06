﻿Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Data.SqlTypes
Imports System.Web.UI.Page
Imports System.Configuration
Imports System.Web.Configuration
Public Class MidPoint
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

        Response.AddHeader("Refresh", "840")

        'Response.Write("supervisor Len " & Len(Session("MGR_EMPLID")) & "<br>")
        'lblLogin_EMPLID.Text = Session("MGR_EMPLID") 'Session("MGT_LOGON") 

        'Response.Write("MGR  " & Session("MGR_EMPLID") & "HR  " & Session("HR_EMPLID") & "<br>EMP  " & lblEMPLID.Text)

        If Session("NETID") = "" Then Response.Redirect("default.aspx")

        If IsPostBack Then
            'Response.Write("<br>1. IsPostBack Save Data before Display") ': Response.End()
            'DisplayData()
        Else
            'Response.Write("<br>2. else Save Data before Display") ': Response.End()
            DisplayData()
        End If

        'Response.Write("Today time " & Now & "<br> Date " & Today)

        'CheckBox_Met.Attributes.Add("onclick", "if(!confirm('To leave comments, please click the ""Cancel"" button.')) {return false};")
        'Response.Write(lblSUP_EMAIL.Text & "<br>" & lblGUILD_EMAIL.Text)
        'Response.Write(Len(Session("MGR_EMPLID")) & "<br>" & lblEMPLID.Text & "<br>" & Session("MGT_LOGON"))


    End Sub

    Protected Sub DisplayData()
        SQL = "select (select last+','+first from id_tbl where emplid=A.emplid)Name,(select First+' '+Last from id_tbl where emplid=A.emplid)FistLastName,EMPLID,Title,Department,Deptid,Hired,Perf_Year,(select First+' '+Last from id_tbl "
        SQL &= " where emplid=A.SUP_EMPLID)Sup_Name,SUP_EMPLID,(select First+' '+Last from id_tbl where emplid=A.UP_MGT_EMPLID)Up_Sup_Name,UP_MGT_EMPLID,(select First+' '+Last from id_tbl where emplid=A.HR_EMPLID)HR_Name,HR_EMPLID,"
        SQL &= " (case when len(Guild_Comments)<2 then '' else Guild_Comments end)Guild_Comments,Date_NotMet_Mgt,TimeStamp,Met_Mgt,Not_Met_Mgt,convert(char(10),Date_Met_Mgt,101)Date_Met_Mgt,"
        SQL &= " (select email from id_tbl where emplid=A.EMPLID)GLD_Email,(select email from id_tbl where emplid=A.SUP_EMPLID)Mgr_Email,(select email from id_tbl where emplid=A.UP_MGT_EMPLID)Up_Mgr_Email,"
        SQL &= " (select email from id_tbl where emplid=A.HR_EMPLID)Hr_Email from Appraisal_MidPoint_MASTER_tbl A where emplid in (" & lblEMPLID.Text & ") and Perf_Year= " & Session("YEAR_MidPoint") & ""
        'Response.Write(SQL) : Response.End()

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

        '--Guild view
        If Len(Session("MGT_LOGON")) = 0 And Len(Session("HR_EMPLID")) = 0 Then
            'Response.Write("Employee Logon")
            If CDbl(DT.Rows(0)("Met_Mgt").ToString) = 1 Then
                CheckBox_Met.Visible = False : CheckBox_NotMet.Visible = False : tblSelectDate.Visible = False
                lbl_Met.Text = lblEMPLOYEE_NAME.Text & " has confirm that <strong>have met</strong> with manager to have a mid point performance conversation"
                tblConfirmDate.Visible = True : CheckImg.Visible = True : Com1.Visible = True : lblComments.Visible = False
                CheckBox_NotMet.Visible = False : Com.Visible = False
                lblDate.Text = DT.Rows(0)("Date_Met_Mgt").ToString : lblTimeStamp.Text = DT.Rows(0)("TimeStamp").ToString
                If Len(DT.Rows(0)("Guild_Comments").ToString) = 0 Then
                    lblComments.Text = "No Comments"
                Else
                    lblComments.Text = Replace(Replace(Replace(DT.Rows(0)("Guild_Comments").ToString, Chr(13), "<span>"), Chr(10), "<br>"), "&nbsp;", " ")
                End If
            Else
                lbl_Met.Visible = True
                lbl_Met.Text = "I confirm that <strong>I have met</strong> with my manager to have a mid point performance conversation on (please select date)."
            End If
        End If

        If Len(Session("MGT_LOGON")) = 4 Or Len(Session("HR_EMPLID")) = 4 Then
            'Response.Write("Manager Logon")
            If CDbl(DT.Rows(0)("Met_Mgt").ToString) = 0 Then
                lblNot_Met.Visible = True : CheckBox_Met.Visible = False : CheckBlank1.Visible = True : Comments.Enabled = False
                lblNot_Met.Text = lblEMPLOYEE_NAME.Text & " has not acknowledged the mid-point review."
                Cal_Mgr.Visible = False
            Else
                CheckBox_Met.Visible = False : CheckBox_NotMet.Visible = False : tblSelectDate.Visible = False
                tblConfirmDate.Visible = True : CheckImg.Visible = True : Com1.Visible = True : lblComments.Visible = True
                CheckBox_NotMet.Visible = False : Com.Visible = False
                lblDate.Text = DT.Rows(0)("Date_Met_Mgt").ToString : lblTimeStamp.Text = DT.Rows(0)("TimeStamp").ToString

                'tblConfirmDate1.Visible = True
                If Len(DT.Rows(0)("Guild_Comments").ToString) = 0 Then
                    lblComments.Text = "No Comments"
                Else
                    lblComments.Text = Replace(Replace(Replace(DT.Rows(0)("Guild_Comments").ToString, Chr(13), "<span>"), Chr(10), "<br>"), "&nbsp;", " ")
                End If
            End If


        End If

    End Sub


    Private Sub Calendar1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged

        'Response.Write(Today > = Calendar1.SelectedDate)
        If Today < Calendar1.SelectedDate Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Future date not permitted.'); </script>")
            txtCal.Text = ""
        Else
            Calendar1.Visible = False
            txtCal.Text = Calendar1.SelectedDate.ToShortDateString()
            CheckBlank.Visible = False
            CheckBox_Met.Visible = True


            Dim div As System.Web.UI.Control = Page.FindControl("divCalendar")
            If TypeOf div Is HtmlGenericControl Then
                CType(div, HtmlGenericControl).Style.Add("display", "none")


            End If
        End If

    End Sub

    Protected Sub imgCal_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles imgCal.Click
        Calendar1.Visible = True
    End Sub

    Protected Sub Confirm_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Met.CheckedChanged

        If Len(txtCal.Text) < 4 Then
            SQL1 = "Update Appraisal_MidPoint_MASTER_tbl Set Met_Mgt=0,Not_Met_Mgt=0,Guild_Comments='" & Replace(Replace(Replace(Comments.Text, "'", "`"), "<", "{"), ">", "}") & "'"
            SQL1 &= " where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & Session("YEAR_MidPoint") & ""
            'Response.Write(SQL1) ': Response.End()
            DT1 = LocalClass.ExecuteSQLDataSet(SQL1)
            LocalClass.CloseSQLServerConnection()
            CheckBox_Met.Checked = False
            CheckBox_NotMet.Checked = False
            ClientScript.RegisterClientScriptBlock(Me.GetType, "Login", "<script language='JavaScript'> alert('Please choose the date when you met your manager.'); </script>")
            Return
        Else
            SQL = "Update Appraisal_MidPoint_MASTER_tbl Set Guild_Comments='" & Replace(Replace(Replace(Comments.Text, "'", "`"), "<", "{"), ">", "}") & "',"
            SQL &= "[TimeStamp] ='" & Now & "',Met_Mgt=1,Not_Met_Mgt=0,Date_Met_Mgt='" & txtCal.Text & "'"
            SQL &= " where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & Session("YEAR_MidPoint") & ""
            'Response.Write(SQL)
            DT = LocalClass.ExecuteSQLDataSet(SQL)
            LocalClass.CloseSQLServerConnection()

            Msg = Session("FirsLastName") & " has submitted the Mid-Point Discussion Acknowledgement form to you on " & Now & " <br><br> "
            Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br><br>"
            If Len(Comments.Text) > 0 Then
                Msg &= "<u>Comments:</u>"
                Msg &= "<table width=90% border=0><tr><td>" & Replace(Replace(Replace(Replace(Comments.Text, "'", "`"), "<", "{"), ">", "}"), Chr(13), "<br>") & "</td></tr></table> "
            End If

            Msg1 = "You met with you manager and have submitted the Mid-Point Discussion Acknowledgement form on " & Now & " <br><br> "
            Msg1 &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br><br>"
            If Len(Comments.Text) > 0 Then
                Msg1 &= "<u>Comments:</u>"
                Msg1 &= "<table width=90% border=0><tr><td>" & Replace(Replace(Replace(Replace(Comments.Text, "'", "`"), "<", "{"), ">", "}"), Chr(13), "<br>") & "</td></tr></table> "
            End If

            'Response.Write(Msg) : Response.End()
            '--Manager
            LocalClass.SendMail(lblSUP_EMAIL.Text, "Mid-Point Discussion Acknowledgement", Msg)
            '--Guild
            LocalClass.SendMail(lblGUILD_EMAIL.Text, "Mid-Point Discussion Acknowledgement", Msg1)

            If Len(lblComments.Text) > 0 Then
                Com1.Visible = True
            End If
            Response.Redirect("MidPoint.aspx?Token=" & Request.QueryString("Token"))
        End If

    End Sub

    Protected Sub Confirm_CheckedChanged1(sender As Object, e As EventArgs) Handles CheckBox_NotMet.CheckedChanged
        SQL = "Update Appraisal_MidPoint_MASTER_tbl Set Guild_Comments='" & Replace(Replace(Replace(Comments.Text, "'", "`"), "<", "{"), ">", "}") & "',"
        SQL &= " Met_Mgt=0, Not_Met_Mgt=1, Date_NotMet_Mgt='" & Now & "', Date_Met_Mgt=NULL"
        SQL &= " where emplid in (" & lblEMPLID.Text & ") and Perf_Year=" & Session("YEAR_MidPoint") & ""
        'Response.Write(SQL)
        DT = LocalClass.ExecuteSQLDataSet(SQL)
        LocalClass.CloseSQLServerConnection()
        '--Message to manager
        Msg = Session("FirsLastName") & " has indicated that they have not met with you to have a mid point performance conversation. <br><br> "
        Msg &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br><br>"

        If Len(Comments.Text) > 0 Then
            Msg &= "<u>Comments:</u>"
            Msg &= "<table width=90% border=0><tr><td>" & Replace(Replace(Replace(Replace(Comments.Text, "'", "`"), "<", "{"), ">", "}"), Chr(13), "<br>") & "</td></tr></table> "
        End If

        '--Message to guild
        Msg1 = "You have indicated that you have not met with your manager to have a mid point performance conversation. <br><br> "
        Msg1 &= "Please click on the link http://" & Request.Url.Host & "/Appraisal/Default.aspx for full details.<br><br>"
        If Len(Comments.Text) > 0 Then
            Msg1 &= "<u>Comments:</u>"
            Msg1 &= "<table width=90% border=0><tr><td>" & Replace(Replace(Replace(Replace(Comments.Text, "'", "`"), "<", "{"), ">", "}"), Chr(13), "<br>") & "</td></tr></table> "
        End If

        'Response.Write(Msg) : Response.End()
        '-Manager
        LocalClass.SendMail(lblSUP_EMAIL.Text, "Mid-Point Discussion Acknowledgement", Msg)
        '--Guild
        LocalClass.SendMail(lblGUILD_EMAIL.Text, "Mid-Point Discussion Acknowledgement", Msg1)


        If Len(lblComments.Text) > 0 Then
            Com1.Visible = True
        End If

        Response.Redirect("MidPoint.aspx?Token=" & Request.QueryString("Token"))

    End Sub

End Class