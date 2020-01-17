<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Appraisal_Reports.aspx.vb" Inherits="Appraisal.Appraisal_Reports" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appraisal Reports</title>
</head>

<script>
    function goBack() {
        window.history.back();
    }
    //onbeforeunload="window.history.back();"
</script>

<body> 

<form id="form1" runat="server">

<asp:Label ID="lblSAP" runat="server" Visible="false"/>
<asp:Label ID="lblGENERALIST_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblEmplid" runat="server" Visible="true"/>
<asp:Label ID="lblYEAR" runat="server" Visible="false"/>
<asp:Label ID="Label4" runat="server" Visible="false"/>
<asp:Label ID="Label5" runat="server" Visible="false"/>
<asp:Label ID="Label6" runat="server" Visible="false"/>


<table style="width:100%; text-align:center; font-weight:bold;"><tr><td>
    <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" BorderStyle="None" CssClass="Button_StyleSheet" BackColor="silver" Font-Size="Medium" Width="160px"/>
    </td></tr></table>

<asp:Panel ID="Panel_MGT_OLD" runat="server" Visible="false">
<table style="width:100%;"><tr><td>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True" DataKeyNames="EMPLID">
 <Columns>
   <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-Wrap="false"
       DataNavigateUrlFields="Token" DataNavigateUrlFormatString="~/Staff/HR/Manager_Appraisal.aspx?Token={0}" SortExpression="Name" Target="_blank" />
   <asp:BoundField DataField="Status1" HeaderText="Appraisal Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Status1" ControlStyle-Font-Size="Smaller" ></asp:BoundField>
   <asp:BoundField DataField="Name#1" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#1"></asp:BoundField>
   <asp:BoundField DataField="Name#2" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#2"></asp:BoundField>
   <asp:BoundField DataField="VP_Name" HeaderText="VP name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="VP_Name"></asp:BoundField>
   <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="HR_Name"></asp:BoundField>
   <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
   <asp:BoundField DataField="Final_Rate" HeaderText="Overall Rating" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Final_Rate"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</td></tr></table>
</asp:Panel>
        
<asp:Panel ID="Panel_GUILD_OLD" runat="server" Visible="false">
<table style="width:100%;"><tr><td>
<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet">
<Columns>
   <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-Wrap="false" 
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/Guild_Appraisal.aspx?Token={0}" SortExpression="Name" Target="_blank"/>
   <asp:BoundField DataField="Status1" HeaderText="Appraisal Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Status1" ControlStyle-Font-Size="Smaller" ></asp:BoundField>
   <asp:HyperLinkField DataTextField="GoalFormStatus" HeaderText="Goals Status" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"  
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/Appraisal_Reports_Redirect.aspx?Token=MID_GLD{0}"  SortExpression="GoalFormStatus" Target="_blank" ControlStyle-Font-Size="Smaller" />
   <asp:HyperLinkField DataTextField="MidPointStatus" HeaderText="MidPoint Status" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"  
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/Appraisal_Reports_Redirect.aspx?Token=MID_GLD{0}"  SortExpression="MidPointStatus" Target="_blank" ControlStyle-Font-Size="Smaller" />
   <asp:BoundField DataField="First_MGT_Name" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="First_MGT_Name" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="Second_MGT_Name" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Second_MGT_Name" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="HR_Name" ItemStyle-Wrap="false"></asp:BoundField>
    <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
   <asp:BoundField DataField="Final_Rate" HeaderText="Overall Rating" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Final_Rate"></asp:BoundField>
   <asp:BoundField DataField="GLD_COMM" HeaderText="Guild Comments" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" SortExpression="GLD_COMM"></asp:BoundField>
   <asp:BoundField DataField="Refuse_Sign" HeaderText="Guild Refuse to Sign" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"  SortExpression="Refuse_Sign"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</td></tr></table>
</asp:Panel>

<asp:Panel ID="Panel_MGT_NEW" runat="server" Visible="false">
<table style="width:100%;"><tr><td>
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource6" Width="100%" AllowSorting="True" DataKeyNames="EMPLID">
 <Columns>
   <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-Wrap="false" 
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/Appraisal_Reports_Redirect.aspx?Token=APP_MGT{0}" SortExpression="Name" Target="_blank"/>
   <asp:BoundField DataField="Status2" HeaderText="Appraisal Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Status2" ControlStyle-Font-Size="Smaller" ></asp:BoundField>
   <asp:BoundField DataField="Name#1" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#1" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="Name#2" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#2" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="VP_Name" HeaderText="VP Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="VP_Name" ItemStyle-Wrap="false"></asp:BoundField>
    <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="HR_Name" ItemStyle-Wrap="false"></asp:BoundField>
    <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
   <asp:BoundField DataField="Final_Rate" HeaderText="Overall Rating" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="7%" SortExpression="Final_Rate"></asp:BoundField>
   <asp:BoundField DataField="Comments1" HeaderText="Employee Comments" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" SortExpression="Comments"></asp:BoundField>
   <asp:BoundField DataField="DateEmpl_Refused1" HeaderText="Employee Refuse to Sign" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" SortExpression="DateEmpl_Refused1"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</td></tr></table>
</asp:Panel>

<asp:Panel ID="Panel_GUILD_NEW" runat="server" Visible="false">
<table style="width:100%;"><tr><td>
<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet">
<Columns>
   <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-Wrap="false" 
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/Appraisal_Reports_Redirect.aspx?Token=APP_GLD{0}" SortExpression="Name" Target="_blank" ControlStyle-Font-Size="Smaller"/>
   <asp:BoundField DataField="Status2" HeaderText="Appraisal Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Status2"></asp:BoundField>
   
   <asp:BoundField DataField="Name#1" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#1" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="Name#2" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#2" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="VP_Name" HeaderText="VP Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="VP_Name" ItemStyle-Wrap="false"></asp:BoundField>
    <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="HR_Name" ItemStyle-Wrap="false"></asp:BoundField>
    <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
   <asp:BoundField DataField="Final_Rate" HeaderText="Overall Rating" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="7%" SortExpression="Final_Rate"></asp:BoundField>
   <asp:BoundField DataField="Comments1" HeaderText="Employee Comments" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" SortExpression="Comments"></asp:BoundField>
   <asp:BoundField DataField="DateEmpl_Refused1" HeaderText="Employee Refuse to Sign" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" SortExpression="DateEmpl_Refused1"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</td></tr></table>
</asp:Panel>

<asp:Panel ID="Panel_ALL_EMPLOYEES" runat="server" Visible="false">
<table style="width:100%;"><tr><td>
<asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet">
<Columns>
   <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-Wrap="false" 
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/Appraisal_Reports_Redirect.aspx?Token=APP_ALL{0}" SortExpression="Name" Target="_blank"/>
    <asp:BoundField DataField="Status2" HeaderText="Appraisal Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Status2"  ItemStyle-Font-Size="Smaller"/>
   <asp:BoundField DataField="Name#1" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#1" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="Name#2" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#2" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="VP_Name" HeaderText="VP Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="VP_Name" ItemStyle-Wrap="false"></asp:BoundField>
    <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="HR_Name" ItemStyle-Wrap="false"></asp:BoundField>
    <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
   <asp:BoundField DataField="Final_Rate" HeaderText="Overall Rating" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="7%" SortExpression="Final_Rate"></asp:BoundField>
   <asp:BoundField DataField="Comments1" HeaderText="Employee Comments" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" SortExpression="Comments"></asp:BoundField>
   <asp:BoundField DataField="DateEmpl_Refused1" HeaderText="Employee Refuse to Sign" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" SortExpression="DateEmpl_Refused1"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</td></tr></table>
</asp:Panel>


  </form>
 </body>
</html>
