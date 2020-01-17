<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Access_Employees_History.aspx.vb" Inherits="Appraisal.Access_Employees_History" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee History</title>

<link href="../../StyleSheet1.css" rel="stylesheet" />

<style type="text/css">
      .Hide  {
         display: none;
         }

#toggle_div {
    background-color: rgba(255, 255, 255, .6);
    display: none;
    position: absolute;
    width: 100%;
    height: 100%;
    top: 0;
    z-index: 999;
}
</style>

</head>
<body>
    <form id="form1" runat="server">



<asp:Panel ID="Panel_Empl_history" runat="server" Visible="false"></asp:Panel>


<table style="width:100%;">
    
    <tr><td style="width:25%"></td><td style="text-align:center"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td><td style="width:25%"></td></tr>
    
    <tr><td style="width:25%;">&nbsp;</td><td>&nbsp;</td><td style="width:25%;">&nbsp;</td></tr>

    <tr><td style="width:25%;"></td><td><b>Employee: <asp:Label ID="lblEmployee" runat="server" Font-Size="Large" CssClass="Label_StyleSheet"/></b></td><td style="width:25%;"></td></tr>
    
    <tr><td style="width:25%;"></td><td>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True" DataKeyNames="EMPLID">
<rowstyle backcolor="LightCyan"/>
<alternatingrowstyle backcolor="White"/>

 <Columns>
   <asp:BoundField DataField="EMPLID" ItemStyle-CssClass ="Hide" ControlStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" ControlStyle-Width="0%" ></asp:BoundField>  

   <asp:BoundField DataField="Change_Made_by" HeaderText="Change Made by" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>  
   <asp:BoundField DataField="DateTime" HeaderText="Date Changed" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray"></asp:BoundField>
   <asp:BoundField DataField="MGT#1" HeaderText="Manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center"></asp:BoundField>
   <asp:BoundField DataField="MGT#2" HeaderText="Second manager name" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center"></asp:BoundField>
   <asp:BoundField DataField="Generalist" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center"></asp:BoundField>
 </Columns>
</asp:GridView>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>

</td><td style="width:25%;"></td></tr>
    <tr><td style="width:25%;">&nbsp;</td><td>&nbsp;</td><td style="width:25%;">&nbsp;</td></tr>
    <tr><td style="width:25%;">&nbsp;</td><td>&nbsp;</td><td style="width:25%;">&nbsp;</td></tr>
    <tr><td style="width:25%;"></td><td style="text-align:center;"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
                CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/></td><td style="width:25%;"></td></tr>
    
    <tr><td style="width:25%;"></td><td></td><td style="width:25%;"></td></tr>

</table>
  </form>
 </body>
</html>
