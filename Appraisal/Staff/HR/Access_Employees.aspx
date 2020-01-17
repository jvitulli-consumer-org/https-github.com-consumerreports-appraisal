<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Access_Employees.aspx.vb" Inherits="Appraisal.Access_Employees" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>

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

<script type="text/javascript">
    function dim_div() {
        document.getElementById("toggle_div").style.display = "block";
    }

</script>

</head>
<body>
<form id="form1" runat="server">

<div id="toggle_div"></div>

<asp:Label ID="lblLogin" runat="server" text="" Visible="false"/>
<asp:Label ID="lblDepartment" runat="server" text="" Visible="false" />
<asp:Label ID="lblRecord_Updated" runat="server" text="" Visible="false"/>
<asp:Label ID="lblName_Updated" runat="server" text="" Visible="false"/>

<table style="width:100%" border="0">
    <tr><td style="width:10%"></td><td style="text-align:center"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td><td style="width:10%"></td></tr>
    <tr><td colspan="3" style="text-align:center">
<asp:Button ID="Button2" Text="Close" runat="server" style="border-style:none; color:Blue; width:100px; font-weight:bold; cursor: pointer;" 
    OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/>
    </td></tr>

    <tr><td style="width:10%"></td><td style="text-align:center; font-size:large; font-weight:bold;">Employee Adjustment for <font color="#00ae4d">Appraisal only</font> in  <%= lblDepartment.Text%> department</td><td style="width:10%"></td></tr>
    <tr><td style="width:10%"></td> 
        <td>

<asp:GridView Runat="server" ID="GridView1"  AllowSorting="true" AutoGenerateColumns="False"  Width="100%"
    DataKeyNames="EMPLID" DataSourceID="SqlDataSource1" OnRowDataBound="OnRowDataBound" OnRowCreated="GridView1_RowCreated" 
    HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Names="Calibri" HeaderStyle-Font-Bold="true">
<rowstyle backcolor="LightCyan"/>
<alternatingrowstyle backcolor="White"/>


<Columns>
  <asp:BoundField DataField="EMPLID" ItemStyle-CssClass ="Hide" ControlStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"/>
  
  <asp:HyperLinkField DataTextField="Name_Adjust_Term" HeaderText="NAME" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Height="30px" SortExpression="Employee_Name"
        DataNavigateUrlFields="EMPLID" DataNavigateUrlFormatString="~/Staff/HR/Access_Employees_History.aspx?Token={0}" Target="_blank" />
  
    <asp:BoundField DataField="Jobtitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Height="30px" />
     
<asp:TemplateField HeaderText="Manager Name" ItemStyle-HorizontalAlign="Center" SortExpression="Manager_Name" Visible="false">
 <ItemTemplate>
    <asp:Label ID="lblManager" runat="server" Text='<%# Eval("Manager_Name")%>' Visible = "false"/>
     <asp:Label ID="lblManager_EMPLID" runat="server" Text='<%# Eval("MGT_EMPLID")%>' Visible = "false"/>
     <asp:Label ID="lblEMPLID" runat="server" Text='<%# Eval("EMPLID")%>' Visible = "false"/>
     <asp:DropDownList ID="ddlManager_Name" runat="server"></asp:DropDownList>
 </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Second Manager Name" ItemStyle-HorizontalAlign="Center" SortExpression="UP_Manager_Name" Visible="true">
 <ItemTemplate>
    <asp:Label ID="lblUP_Manager" runat="server" Text='<%# Eval("UP_Manager_Name")%>' Visible = "false"/>
     <asp:Label ID="lblUP_Manager_EMPLID" runat="server" Text='<%# Eval("UP_MGT_EMPLID")%>' Visible = "false"/>
     <asp:Label ID="lblEMPLID1" runat="server" Text='<%# Eval("EMPLID")%>' Visible = "false"/>
     <asp:DropDownList ID="ddlUP_Manager_Name" runat="server"></asp:DropDownList>
 </ItemTemplate>
</asp:TemplateField>     
    
<asp:TemplateField HeaderText="Generalist" ItemStyle-HorizontalAlign="Center" SortExpression="HR_Name">
 <ItemTemplate>
    <asp:Label ID="lblHR_Name" runat="server" Text='<%# Eval("HR_Name")%>' Visible = "false"/>
     <asp:Label ID="lblHR_EMPLID" runat="server" Text='<%# Eval("HR_EMPLID")%>' Visible = "false"/>
     <asp:Label ID="lblEMPLID2" runat="server" Text='<%# Eval("EMPLID")%>' Visible = "false"/>
     <asp:DropDownList ID="ddlHR_Name" runat="server"></asp:DropDownList>
 </ItemTemplate>
</asp:TemplateField>     

<asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="center">
  <ItemTemplate>
     <asp:Button ID="btnUpdate_Manager" runat="server" Text="Update" CommandArgument='<%# Eval("EMPLID")%>' OnClick="Update_Manager"  OnClientClick="dim_div()" 
          CssClass="Button_StyleSheet" BackColor="#00ae4d" BorderStyle="None"/>
    </ItemTemplate>
</asp:TemplateField>

 <asp:BoundField DataField="Updated" HeaderText="Last Update" HeaderStyle-HorizontalAlign="center"  ItemStyle-HorizontalAlign="Left" SortExpression="Updated"/>
</Columns>
</asp:GridView>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>">
<UpdateParameters><asp:Parameter Name="EMPLID" /></UpdateParameters>
</asp:SqlDataSource>

</td><td style="width:10%"></td></tr>

<tr><td></td><td></td><td></td></tr>

<tr><td colspan="3" style="text-align:center">
<asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:100px; font-weight:bold; cursor: pointer;" 
    OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/>
    </td></tr>


</table>


 </form>
</body>
</html>
