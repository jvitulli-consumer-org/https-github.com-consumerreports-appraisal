<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Status_Report.aspx.vb" Inherits="Appraisal.Status_Report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appraisal Report</title>
</head>
<body>
    <form id="form1" runat="server">
    
<asp:Label ID="lblSAP" runat="server" Visible="false"/>
<asp:Label ID="lblEmplid" runat="server" Visible="true"/>
<asp:Label ID="lblYEAR" runat="server" Visible="false"/>
<asp:Label ID="lblMgt_Name" runat="server" Visible="false"/>
<asp:Label ID="Label5" runat="server" Visible="false"/>
<asp:Label ID="Label6" runat="server" Visible="false"/>

<!--<table style="width:100%; text-align:center; font-weight:bold;"><tr><td>
    <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" BorderStyle="None" CssClass="Button_StyleSheet" BackColor="silver" Font-Size="Medium" Width="160px"/>
    </td></tr></table>-->        


<table style="width:100%;"><tr><td>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet">
<Columns>
   <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="15%" 
       DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/Appraisal_Reports_Redirect.aspx?Token=APP_ALL{0}" SortExpression="Name" Target="_blank"/>
   <asp:BoundField DataField="Name#1" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#1" HeaderStyle-Width="12%" ></asp:BoundField>
   <asp:BoundField DataField="Name#2" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Name#2" HeaderStyle-Width="12%" ></asp:BoundField>
   <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="HR_Name" ItemStyle-Wrap="false"></asp:BoundField>
   <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
   <asp:BoundField DataField="Status1" HeaderText="Appraisal Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Status1"></asp:BoundField>
   <asp:BoundField DataField="Final_Rate" HeaderText="Overall Rating" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="12%" SortExpression="Final_Rate"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>


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


</td></tr>

<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
<tr><td style="text-align:center"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
        CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/></td></tr>
<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>

</table>

    </form>
</body>
</html>
