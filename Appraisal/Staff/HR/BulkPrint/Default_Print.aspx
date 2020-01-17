<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default_Print.aspx.vb" Inherits="Appraisal.Default_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Print</title>

<link href="StyleSheet1.css" rel="stylesheet" />

<style type="text/css">
        .auto-style1 {
            width: 10%;
            height: 23px;
        }
        .auto-style2 {
            width: 40%;
            height: 23px;
        }
 </style>
</head>
<body>
    <form id="form1" runat="server">
    
<asp:Label ID="LblEMPLID" runat="server" Text="" Visible="false"/>
<asp:Label ID="LblMGT_EMPLID" runat="server" Text="" Visible="false"/>
<asp:Label ID="LblYear" runat="server" Text="" Visible="false"/>
<asp:Label ID="LblDEPTID" runat="server" Text="" Visible="false"/>


<table style="width:100%; border:0px;">
  
   <tr><td>&nbsp;</td></tr>
  <tr><td style="text-align:center"><img  src="../../../images/TopBanner1.png" alt="images/TopBanner1.png" style="height: 80px; width: 750px" /></td></tr>
  <tr><td style="text-align:center; font-family:Calibri;font-size:x-large; font-weight:bold;"><asp:Label ID="lblYEAR_PRINT" runat="server"></asp:Label> Bulk Print </td></tr>

<tr><td>
   <table border="0" style="width:100%;">
      <tr id="Ln" runat="server"><td style="width:10%;">&nbsp;&nbsp;</td><td colspan="2"><hr style="height:4px; background-color:gray"/></td><td style="width:10%;">&nbsp;&nbsp;</td></tr>  

<!--1. GUILD-->
      <tr id="Ln1" runat="server"><td colspan="4" style="text-align:center; font-family:Calibri;font-size:large; font-weight:bold; color:blue;"><u>Guild Appraisal</u></td></tr>    
       
<!--<tr><td style="height:6px;"">&nbsp;&nbsp;</td><td style="height:2px; font-size:2px;">&nbsp;&nbsp;</td><td style="height:2px; font-size:2px;">&nbsp;&nbsp;</td><td style="height:2px; font-size:2px;">&nbsp;&nbsp;</td></tr>-->
        
     <tr id="Ln2" runat="server"><td style="width:10%;">&nbsp;&nbsp;</td>
         <td style="width:40%; text-align:right; font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">By Generalist&nbsp;&nbsp;</td>
         <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                    <asp:DropDownList ID="Generalist_Name" Width="160px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Change"/>&nbsp;&nbsp;
                    <asp:Button ID="Submit_Generalist" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
         <td style="width:10%;">&nbsp;&nbsp;</td></tr>
         
     <tr id="Ln3" runat="server"><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:10px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td></tr>

   <tr id="Ln4" runat="server"><td style="width:10%;">&nbsp;&nbsp;</td>
         <td style="width:40%; text-align:right; font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">By Manager&nbsp;&nbsp;</td>
         <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                    <asp:DropDownList ID="Manager_Name_GLD" Width="160px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Manager_GLD"/>&nbsp;&nbsp;
                    <asp:Button ID="Submit_Manager_GLD" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
         <td style="width:10%;">&nbsp;&nbsp;</td></tr>
         
     <tr id="Ln5" runat="server"><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:10px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td></tr>

<!--2. Waiting for My Approval-->
     <tr id="Ln6" runat="server"><td style="width:10%;">&nbsp;&nbsp;</td>
         <td style="width:40%; text-align:right; font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">By Department&nbsp;&nbsp;</td>
         <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                    <asp:DropDownList ID="Department_Name" Width="160px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Change_Waiting"/>&nbsp;&nbsp;
                    <asp:Button ID="Submit_Department" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
        
       <td style="width:10%;">&nbsp;&nbsp;</td></tr>

     <tr id="Ln7" runat="server"><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:10px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td></tr>

        <tr id="Ln20" runat="server"><td style="width:10%;">&nbsp;&nbsp;</td>
            <td style="width:40%; text-align:right;  font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">ALL&nbsp;&nbsp;</td>
            <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                <asp:DropDownList ID="All" runat="server" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Reports" disabled/>&nbsp;&nbsp;
                <asp:Button ID="Submit_Report" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
            <td style="width:10%;">&nbsp;&nbsp;</td></tr>

    <tr id="Ln8" runat="server"><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:10px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td></tr>


    <asp:Panel ID="Panel_Goals" runat="server" Visible="false">
       <tr><td style="width:10%;">&nbsp;&nbsp;</td>
            <td style="width:40%; text-align:right;  font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">Goals&nbsp;&nbsp;</td>
            <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                <asp:DropDownList ID="Goals" runat="server" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Goals"/>&nbsp;&nbsp;
                <asp:Button ID="Submit_Goals" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
            <td style="width:10%;">&nbsp;&nbsp;</td></tr>
    </asp:Panel>   

       
    <tr id="Ln9" runat="server"><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:10px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td></tr>

<asp:Panel ID="Panel_MidPoint_Print" runat="server" Visible="false">   
       <tr><td style="width:10%;">&nbsp;&nbsp;</td>
            <td style="width:40%; text-align:right;  font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">Mid-Point&nbsp;&nbsp;</td>
            <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                <asp:DropDownList ID="Mid_Point" runat="server" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_MidPoint"/>&nbsp;&nbsp;
                <asp:Button ID="Submit_MidPoint" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
            <td style="width:10%;">&nbsp;&nbsp;</td></tr>
    </asp:Panel>

       <tr id="Ln10" runat="server"><td style="width:10%;">&nbsp;&nbsp;</td><td colspan="2"><hr style="height:4px; background-color:gray"/></td><td style="width:10%;">&nbsp;&nbsp;</td></tr>
       

       <tr id="Ln11" runat="server"><td colspan="4" style="text-align:center; font-family:Calibri;font-size:large; font-weight:bold; color:blue;"><u>Manager Appraisal</u></td></tr> 
     
   <tr id="Ln12" runat="server"><td style="width:10%;">&nbsp;&nbsp;</td>
          <td style="width:40%; text-align:right; font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">By Generalist&nbsp;&nbsp;</td>
          <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                    <asp:DropDownList ID="Generalist_Name_MGT" Width="160px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Generalist_MGT" style="height: 22px"/>&nbsp;&nbsp;
                    <asp:Button ID="Submit_Generalist_MGT" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
         <td style="width:10%;">&nbsp;&nbsp;</td></tr>
         
     <tr id="Ln13" runat="server"><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:10px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td></tr>

<tr id="Ln14" runat="server"><td style="width:10%;">&nbsp;&nbsp;</td>
          <td style="width:40%; text-align:right; font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">By Manager&nbsp;&nbsp;</td>
          <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                    <asp:DropDownList ID="Manager_Name_MGT" Width="160px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Manager_MGT" style="height: 22px"/>&nbsp;&nbsp;
                    <asp:Button ID="Submit_Manager_MGT" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
         <td style="width:10%;">&nbsp;&nbsp;</td></tr>
         
     <tr id="Ln15" runat="server"><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:10px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td></tr>

     <tr id="Ln16" runat="server">
         <td style="width:10%;">&nbsp;&nbsp;</td>
         <td style="width:40%; text-align:right; font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">By Department&nbsp;&nbsp;</td>
         <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                    <asp:DropDownList ID="Department_Name_MGT" Width="160px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Department_MGT"/>&nbsp;&nbsp;
                    <asp:Button ID="Submit_Department_MGT" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
        
       <td style="width:10%;">&nbsp;&nbsp;</td></tr>

     <tr id="Ln17" runat="server"><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:10px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td><td style="height:1px; font-size:1px;">&nbsp;&nbsp;</td></tr>

        <tr id="Ln18" runat="server"><td style="width:10%;">&nbsp;&nbsp;</td>
            <td style="width:40%; text-align:right; font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">ALL&nbsp;&nbsp;</td>
            <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                <asp:DropDownList ID="All_MGT" runat="server" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Reports_MGT" disabled/>&nbsp;&nbsp;
                <asp:Button ID="Submit_Report_MGT" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
            <td style="width:10%;">&nbsp;&nbsp;</td></tr>
       <tr id="Ln19" runat="server"><td class="auto-style1">&nbsp;&nbsp;</td>
            <td style="text-align:right" class="auto-style2">&nbsp;&nbsp;</td>
            <td style="text-align:left" class="auto-style2">&nbsp;&nbsp;</td>
            <td class="auto-style1">&nbsp;&nbsp;</td></tr>
       <tr><td style="width:10%;">&nbsp;&nbsp;</td><td colspan="2"><hr style="height:4px; background-color:gray"/></td><td style="width:10%;">&nbsp;&nbsp;</td></tr>

       <tr><td style="width:10%;">&nbsp;&nbsp;</td><td style="width:40%; text-align:right">&nbsp;&nbsp;</td><td style="width:40%; text-align:left">&nbsp;&nbsp;</td><td style="width:10%;">&nbsp;&nbsp;</td></tr>

<asp:Panel ID="Panel_Terminated" runat="server" Visible="false">
       <tr><td colspan="4" style="text-align:center; font-family:Calibri;font-size:large; font-weight:bold; color:blue;"><u>Terminated Employee(s)</u></td></tr> 
       <tr><td style="width:10%;">&nbsp;&nbsp;</td><td style="width:40%; text-align:right; font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">ALL&nbsp;&nbsp;</td>
           <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
               <asp:DropDownList ID="Terminated" runat="server" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Reports_TER"/>&nbsp;&nbsp;
               <asp:Button ID="Submit_Report_TER" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
           <td style="width:10%;">&nbsp;&nbsp;</td></tr>
       <tr><td style="width:10%;">&nbsp;&nbsp;</td><td colspan="2"><hr style="height:4px; background-color:gray"/></td><td style="width:10%;">&nbsp;&nbsp;</td></tr>
</asp:Panel>      

       <tr><td colspan="4" style="text-align:center; font-family:Calibri;font-size:large; font-weight:bold; color:blue;"><u>Individual Appraisal</u></td></tr> 
       <tr><td style="width:10%;">&nbsp;&nbsp;</td><td style="width:40%; text-align:right; font-weight:bold; color:Blue; white-space:nowrap; font-family:Calibri;">By Last Name&nbsp;&nbsp;</td>
       <td style="width:40%; text-align:left; white-space:nowrap;">&nbsp;&nbsp;
                <asp:DropDownList ID="Individual" runat="server" Width="160px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Reports_IND"/>&nbsp;&nbsp;
                <asp:Button ID="Submit_Report_IND" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" ForeColor="black" BorderStyle="None"/></td>
                <td style="width:10%;">&nbsp;&nbsp;</td></tr>
       <tr><td style="width:10%;">&nbsp;&nbsp;</td><td colspan="2"><hr style="height:4px; background-color:gray"/></td><td style="width:10%;">&nbsp;&nbsp;</td></tr>

    </table>

</td></tr>

<tr><td>&nbsp;</td></tr>
<tr><td>&nbsp;</td></tr>
<tr><td style="text-align:center"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
                CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/></td></tr>

</table>    
</form>
</body>
</html>
