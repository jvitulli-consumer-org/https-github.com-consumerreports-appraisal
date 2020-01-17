<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Goals_Reports.aspx.vb" Inherits="Appraisal.Goals_Reports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Goals Reports</title>
</head>
<body>

<form id="form1" runat="server">

<asp:Label ID="lblSAP" runat="server" Visible="false"/>
<asp:Label ID="lblGENERALIST_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblEmplid" runat="server" Visible="true"/>
<asp:Label ID="lblYEAR" runat="server" Visible="false"/>
<asp:Label ID="lblGENERALIST_Name" runat="server" Visible="false"/>
<asp:Label ID="Label5" runat="server" Visible="false"/>
<asp:Label ID="Label6" runat="server" Visible="false"/>


<asp:Panel ID="Panel_ALL_EMPLOYEES" runat="server" Visible="false">
<table style="width:100%;"><tr><td>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet">
<Columns>
   <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="15%" ItemStyle-Wrap="false"
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/FutureGoals1.aspx?Token={0}"  SortExpression="Name" Target="_blank"/>
   <asp:BoundField DataField="Status1" HeaderText="Goals Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Status1"></asp:BoundField>
   <asp:BoundField DataField="Name#1" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#1" HeaderStyle-Width="12%" ></asp:BoundField>
   <asp:BoundField DataField="Name#2" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#2" HeaderStyle-Width="12%" ></asp:BoundField>
   <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="HR_Name" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>

</td></tr></table>
</asp:Panel>        


<asp:Panel ID="Panel_MGT" runat="server" Visible="false">
<table style="width:100%;"><tr><td>
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet">
<Columns>
   <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="15%" ItemStyle-Wrap="false"
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/FutureGoals1.aspx?Token={0}"  SortExpression="Name" Target="_blank"/>
   <asp:BoundField DataField="Status1" HeaderText="Goals Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Status1"></asp:BoundField>
   <asp:BoundField DataField="Name#1" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#1" HeaderStyle-Width="12%" ></asp:BoundField>
   <asp:BoundField DataField="Name#2" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#2" HeaderStyle-Width="12%" ></asp:BoundField>
   <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="HR_Name" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>

</td></tr></table>
</asp:Panel>        

<asp:Panel ID="Panel_GLD" runat="server" Visible="false">
<table style="width:100%;"><tr><td>
<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet">
<Columns>
   <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="15%" ItemStyle-Wrap="false"
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/FutureGoals1.aspx?Token={0}"  SortExpression="Name" Target="_blank"/>
   <asp:BoundField DataField="Status1" HeaderText="Goals Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Status1"></asp:BoundField>
   <asp:BoundField DataField="Name#1" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#1" HeaderStyle-Width="12%" ></asp:BoundField>
   <asp:BoundField DataField="Name#2" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#2" HeaderStyle-Width="12%" ></asp:BoundField>
   <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="HR_Name" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>


</td></tr></table>
</asp:Panel>        

  </form>
 </body>
</html>
