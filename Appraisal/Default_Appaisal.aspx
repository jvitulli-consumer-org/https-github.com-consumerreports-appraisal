<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default_Appaisal.aspx.vb" Inherits="Appraisal.Default_Appaisal" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Appraisal</title>

<link href="StyleSheet1.css" rel="stylesheet" />

<style type="text/css">
    .style0 {    
       width:100%;  
     }
  .style1 {
       text-align:center;
       font-size:x-large;
       font-weight:bold;
       color: #00ae4d;
          }
  .style2 {
       width: 350px;
       font-family: Calibri;
          }
  .style3 {
       width: 95px;
       text-align: left;
       font-family: Calibri;
          }
  .style4 {
       width: 200px;
       text-align: left;
       font-family: Calibri;
           }
  .style5 {
       width: 90px;
       text-align: right;
       font-family: Calibri;
           }
  .style6 {
       width: 10%;
       text-align: left;
       font-family: Calibri;
          }
  .style7 {
       font-weight: normal;
       font-size:medium;
       color:black;
       font-family:Calibri;
          }
  .style8 {
       font-size:large; 
       font-weight:bold; 
       color: #00ae4d; 
       font-family:Calibri;
          }
  .style9 {
        width: 20%;
        background-color:#E7E8E3;
        font-family:Calibri;
        text-align:center; 
          }
  .style10 {
        font-family:Calibri;
        border-style:solid;
        border-width:2px;
        border-color:lightgrey;
          }
  .tooltip {
		border-bottom: 1px dotted #000000; color: #000000; outline: none;
		cursor: help; text-decoration: none;
		position: relative;
		   }
  .tooltip span {
		margin-left: -999em;
		position: absolute;
	     	}
  .tooltip:hover span {
		border-radius: 5px 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px; 
		box-shadow: 5px 5px 5px rgba(0, 0, 0, 0.1); -webkit-box-shadow: 5px 5px rgba(0, 0, 0, 0.1); -moz-box-shadow: 5px 5px rgba(0, 0, 0, 0.1);
		font-family: Arial;
        font-size:small;
        font-weight:normal;
	    position: absolute; 
        left: 1em; 
        top: 2em; 
        z-index: 99;
		margin-left: 0; 
        width: 300px;
        text-align:left;
		  }

  .tooltip:hover img {
		/*border: 0; margin: -10px 0 0 -55px;
		float: left; position: absolute;*/
	    	}

  .tooltip:hover em {
		font-family: Calibri; 
        font-size: 1.2em; 
        display: block; 
        padding: 0.2em 0 0.6em 0;
	    	}
		.classic { padding: 0.8em 1em; }
		.custom { padding: 0.5em 0.8em 0.8em 2em; }
        * html a:hover { background: transparent; }
        .classic {background: #FFFFAA; border: 1px solid #FFAD33; }
		.critical { background: #FFCCAA; border: 1px solid #FF3334;	}
		.help { background: #9FDAEE; border: 1px solid #2BB0D7;	}
		.info { background: #9FDAEE; border: 1px solid #2BB0D7;	}
		.warning { background: #FFFFAA; border: 1px solid #FFAD33; }
        .tg   {border-collapse:collapse; border-spacing:0; border-color: #00ae4d;}
        .tg td{border-style:solid; border-width:1px;border-color: #00ae4d;}
        .tg th{border-style:solid; border-width:1px;border-color: #00ae4d;}
     .BorderColor {
            border-style:solid; 
            border-width:1px;
            border-color: #00ae4d;
            font-family:Calibri;
            }
      .StyleBreak
         {
        page-break-after: always;
         }
       @media print
          {    
          .no-print, .no-print *
          {
        display: none !important;
           }
           }
       .topnavigation {
         width: 100px;
         position:static
          }
       topnavigation.scrolling {
         position:fixed;
         top:0px;
          }
</style>



<script type="text/javascript">
//========================================================================
    //Force refresh after x minutes.
    var initialTime = new Date();
    var checkSessionTimeout = function () {
        var minutes = Math.abs((initialTime - new Date()) / 1000 / 60);
        if (minutes > 20) {
            setInterval(function () { location.href = 'Default.aspx' }, 5000)
        }
    };
    setInterval(checkSessionTimeout, 1000);
//========================================================================
</script>

</head>

<body>
<form id="form1" runat="server" >

<asp:Label ID="lblEMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblYear_Appraisal" runat="server" Visible="false"/>
<asp:Label ID="lblYear_Goal" runat="server" Visible="false"/>
<asp:Label ID="lblYear_MidPoint" runat="server" Visible="false"/>
<asp:Label ID="Label6" runat="server"/>
<asp:Label ID="Label7" runat="server"/>             

 <table border="0" style="width:100%;">
    <tr><td colspan="4" style="text-align:center;"><img  src="images/TopBanner1.png" alt="images/TopBanner1.png" style="height: 80px; width: 750px" /></td></tr>
    <tr><td style="width:25%;">&nbsp;&nbsp;</td><td style="width:25%;">&nbsp;&nbsp;</td><td style="width:25%;">&nbsp;&nbsp;</td><td style="width:25%;">&nbsp;&nbsp;</td></tr>
    <tr><td colspan="4" style="text-align:center; font-size:larger; font-weight:bolder; color:black;"><asp:Label ID="lblNAME" runat="server" CssClass="Label_StyleSheet"/><br /></td></tr>
    <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>

<asp:Panel ID="Panel_Appraisal" runat="server" Visible="false">    
    <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
    <tr><td style="width:25%;">&nbsp;&nbsp;</td>
        <td style="text-align:right; color: #00ae4d; align-content:center; font-size:larger; width:20%">My Appraisal
                  <a class="tooltip" href="#"><img src="images/Help.png" height="15" width="15" border="0"/><span class="custom info">
                      - View my current appraisal after it's routed to me<br/>
                      - Esign my current appraisal<br/>
                      - View previous year appraisal</span></a></td>
        
        <td>&nbsp;&nbsp;
            <asp:DropDownList ID="DDLYears" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Change" CssClass="DropDown_StyleSheet"/>&nbsp;&nbsp;
            <asp:Button ID="Submit_Appraisal1" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" Font-Bold="true" ForeColor="black" BorderStyle="None"/>
        </td>
        <td>&nbsp;&nbsp;</td></tr>
</asp:Panel>

       <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>

<asp:Panel ID="Panel_Goal" runat="server" Visible="false">
    <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
    <tr><td>&nbsp;&nbsp;</td>
        <td style="text-align:right; color: #00ae4d; align-content:center;  font-size:larger; width:20%">My Goals FY
                   <a class="tooltip" href="#"><img src="images/Help.png" height="15" width="15" border="0"/><span class="custom info">
                      - After you have created your goals and your manager has approved them you can view your goals here. You can also edit your current goals at any time<br/></span></a></td>
        <td>&nbsp;&nbsp;
            <asp:DropDownList ID="DDLYears_Goal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Change_Goal" CssClass="DropDown_StyleSheet" />&nbsp;&nbsp;
            <asp:Button ID="Submit_Goal1" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" Font-Bold="true" ForeColor="black" BorderStyle="None"/></td>
        <td>&nbsp;&nbsp;</td></tr>
</asp:Panel>

    <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>

<asp:Panel ID="Panel_MidPoint" runat="server" Visible="false">
    <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
    <tr><td>&nbsp;&nbsp;</td>
        <td style="text-align:right; color: #00ae4d; align-content:center; font-size:larger; width:20%">My Mid Point Conversation</td>
        <td>&nbsp;&nbsp;
            <asp:DropDownList ID="DDLYears_MidPoint" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Change_MidPoint"  CssClass="DropDown_StyleSheet"/>&nbsp;&nbsp;
            <asp:Button ID="SubmitMidPoint1" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" Height="20px" Font-Bold="true" ForeColor="black" BorderStyle="None"/></td>
        <td>&nbsp;&nbsp;</td></tr>
</asp:Panel>
    <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>


<!-- <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
     <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>-->
     <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
     <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
     <tr><td colspan="4" style="text-align:center"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Back_button.png" style="width:90px" /></td></tr>

</table>

</form>
</body>
</html>
