Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.SessionState
Imports System.Reflection
Imports Microsoft.VisualBasic
Imports System.Drawing
Imports System.Drawing.Printing

Public Class LogonClass
    'Public sqlConn As New SqlConnection(ConfigurationSettings.AppSettings("GlobalSQLDataConnection"))

    Function NewWayLogin(ByVal NETID1, ByVal PSWRD1)

        Dim SQL As String
        Dim LocalClass As New CUSharedLocalClass
        Dim DS1 As SqlDataReader

        Dim LogonClass As New LogonClass

        SQL = "Select * from ID_TBL where NETID='" & NETID1 & "'"
        SQL = SQL & " and pswrd='" & PSWRD1 & "'"
        DS1 = LocalClass.ExecuteSQLDataSet(SQL)
        DS1.Read()

        If DS1.HasRows Then
            HttpContext.Current.Session("NETID") = DS1("NETID")
            HttpContext.Current.Request.Cookies("EMPLID").Value = DS1("EMPLID")

            NewWayLogin = True
        Else
            NewWayLogin = False
        End If

        LocalClass.CloseSQLServerConnection()

    End Function

    Function NewWayRights(ByVal NETID1)
        Dim SQL As String
        Dim LocalClass As New CUSharedLocalClass
        Dim DS1 As SqlDataReader

        SQL = "Select Rights from ID_TBL where NETID='" & NETID1 & "'"
        DS1 = LocalClass.ExecuteSQLDataSet(SQL)
        DS1.Read()
        If InStr(DS1("Rights"), "PMOD") Then
            NewWayRights = True
        Else
            NewWayRights = False
        End If
    End Function


    Public Class PCPrint : Inherits Printing.PrintDocument
        Private _font As Font
        Private _text As String
    End Class

End Class







