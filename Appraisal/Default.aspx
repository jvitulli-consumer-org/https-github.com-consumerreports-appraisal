<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Appraisal._Default" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
   <title>Appraisal Application</title>

<link href="StyleSheet1.css" rel="stylesheet"/>

</head>    

<body>

<form id="form1" runat="server">
  
<asp:Panel ID="LoginPNL" runat="server"> 
  <table border="0" style="width:100%;">
   <tr><td colspan="2" style="height:20px;">&nbsp;</td></tr>
       <tr><td colspan="2" style="text-align:center;"><img  src="images/TopBanner1.png" alt="images/TopBanner1.png" style="height: 119px; width: 843px" /></td></tr>
       <tr><td colspan="2" style="height:40px;">&nbsp;</td></tr>
<!-- <tr><td colspan="2" style="font-size:xx-large; font-weight: lighter; text-align:center">&nbsp;&nbsp;</td></tr>
<tr><td colspan="2">&nbsp;&nbsp;</td></tr>-->
   <tr style="height:45px">
       <td align="right" width="45%"><asp:Label ID="Label1" runat="server" Text="NETID" CssClass="Label_StyleSheet" Font-Size="Larger"></asp:Label>&nbsp;</td>
       <td align="left">&nbsp;&nbsp;<asp:TextBox ID="IDTXT" runat="server" class="TextBox_StyleSheet" Width="150px"></asp:TextBox></td></tr>
            
   <tr style="height:45px">
       <td align="right"><asp:Label ID="Label2" runat="server" Text="Password" CssClass="Label_StyleSheet" Font-Size="Larger"></asp:Label>&nbsp;</td>
       <td align="left">&nbsp;&nbsp;<asp:TextBox ID="PSTXT" runat="server" TextMode="Password" class="TextBox_StyleSheet" Width="150px"></asp:TextBox></td></tr>
   <tr><td colspan="2">&nbsp;</td></tr>
   <tr><td colspan="2" style="text-align:center">
           <asp:Button ID="LoginBTN" runat="server" BackColor="SkyBlue" CssClass="Button_StyleSheet" Font-Size="Medium" Text="Login" Width="80px" Font-Bold="true" ForeColor="Blue"  BorderStyle="None"/></td></tr>
   <tr><td colspan="2" style="text-align:center">&nbsp;&nbsp;</td></tr>
   <tr><td colspan="2" style="text-align:center"><a style="text-decoration:none; color:blue; " onmouseover="this.style.color='red';" onmouseout="this.style.color='blue';" target="_blank" 
                                href="http://cu-ykvm-ars/QPM/User/Identification/">Forgot, Change or Unlock my Password</a></td></tr>
  </table>
</asp:Panel>

  </form>
 </body>
</html>
