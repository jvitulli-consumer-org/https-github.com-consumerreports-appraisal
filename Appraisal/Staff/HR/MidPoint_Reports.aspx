<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MidPoint_Reports.aspx.vb" Inherits="Appraisal.MidPoint_Reports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MidPoint Report</title>
</head>
<body>

<form id="form1" runat="server">
<asp:Label ID="lblSAP" runat="server" Visible="false"/>
<asp:Label ID="lblGENERALIST_EMPLID" runat="server" Visible="true"/>
<asp:Label ID="lblEmplid" runat="server" Visible="true"/>
<asp:Label ID="lblYEAR" runat="server" Visible="false"/>
<asp:Label ID="Label4" runat="server" Visible="false"/>
<asp:Label ID="Label5" runat="server" Visible="false"/>
<asp:Label ID="Label6" runat="server" Visible="false"/>

<asp:Panel ID="PanelMidPoint" runat="server">


<table style="width:100%;">
    <tr><td style="width:10%;">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td style="width:10%;">&nbsp;&nbsp;</td></tr>

    <tr><td style="width:10%;">&nbsp;&nbsp;</td>
        <td style="text-align:center; font-weight:bold;"><asp:Button ID="btnExcel" runat="server" Text="Export to Excel" BorderStyle="None" CssClass="Button_StyleSheet" BackColor="silver" Font-Size="Medium" Width="160px"/></td>
        
        <td style="width:10%;">&nbsp;&nbsp;</td></tr>
    <tr><td style="width:10%;">&nbsp;&nbsp;</td>
        <td>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True" DataKeyNames="EMPLID">
 <Columns>
     <asp:HyperLinkField DataTextField="Name" HeaderText="Employee Name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="left" ItemStyle-Wrap="false"
         DataNavigateUrlFields="Token" DataNavigateUrlFormatString="\Appraisal\Staff\Guild\MidPoint.aspx?Token={0}" SortExpression="Name" Target="_blank" />
    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightGray" SortExpression="Status" ControlStyle-Font-Size="Smaller" ItemStyle-HorizontalAlign="Center"  ></asp:BoundField>
 
   <asp:BoundField DataField="Manager" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="left" SortExpression="Manager"></asp:BoundField>

<asp:TemplateField HeaderText="Manager Email" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="left" SortExpression="Sup_Email" >
        <ItemTemplate><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("Sup_Email", "mailto:{0}?subject=Mid-Point Review")%>' Text='<%# Eval("Sup_Email")%>'></asp:HyperLink>
 </ItemTemplate>
    </asp:TemplateField>

   <asp:BoundField DataField="HR_Name" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="left" SortExpression="HR_Name"></asp:BoundField>
   <asp:BoundField DataField="Deptname" HeaderText="Department" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="LightGray" SortExpression="Deptname"></asp:BoundField>
   <asp:BoundField DataField="Comments" HeaderText="Employee Comments" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="LightGray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" SortExpression="Comments"></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</td>
    <td style="width:10%;">&nbsp;&nbsp;</td></tr></table>
</asp:Panel>

    </form>
</body>
</html>
