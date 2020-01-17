<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default_HR.aspx.vb" Inherits="Appraisal.Default_HR" MaintainScrollPositionOnPostback="true" smartNavigation="true"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Appraisal</title>

<script src="/offline/offline.min.js"></script>
<link rel="stylesheet" href="/offline/themes/offline-theme-dark.css"/>
<link rel="stylesheet" href="/offline/themes/offline-language-english.css"/>

<script>
    var run = function () {
        var req = new XMLHttpRequest();
        //req.timeout = 5000;
        req.open('GET', 'http://localhost:8888/walter/0', true);
        req.send();
    }

    setInterval(run, 3000);
</script>   



<link href="StyleSheet1.css" rel="stylesheet" />

<style type="text/css">
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
       .red{
           color:red;
       }

    </style>

<script type="text/javascript">
    //Force refresh after x minutes.
    var initialTime = new Date();
    var checkSessionTimeout = function () {
        var minutes = Math.abs((initialTime - new Date()) / 1000 / 60);
        if (minutes > 20) {
            setInterval(function () { location.href = 'Default.aspx' }, 5000)
        }
    };
    setInterval(checkSessionTimeout, 1000);

</script>

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
<form id="form1" runat="server">

<asp:Label ID="lblEMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblMGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblSAP" runat="server" Visible="false"/>
<asp:Label ID="lblMyEmployee_Year" runat="server" Visible="false"/>
<asp:Label ID="lblMyEmployee_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblWaiting_Year" runat="server" Visible="false"/>       
<asp:Label ID="lblWaiting_EMPLID" runat="server" Visible="false"/>       
<asp:Label ID="Label2" runat="server" Visible="false"/>       
<asp:Label ID="Label3" runat="server" Visible="false"/>       
<asp:Label ID="Label1" runat="server" ></asp:Label>

<table border="0" style="width:100%;">
    <tr><td colspan="5" style="text-align:center;"><img  src="images/TopBanner1.png" alt="images/TopBanner1.png" style="height: 80px; width: 750px" /></td></tr>
    <tr><td colspan="5" style="font-size:3px; height:3px;">&nbsp;&nbsp;</td></tr>
    <tr><td colspan="5" style="text-align:center; font-size:larger; font-weight:bolder; color:black;"><asp:Label ID="lblNAME" runat="server" CssClass="Label_StyleSheet"/><br /></td></tr>
    <tr><td colspan="5" style="font-size:5px; height:5px;">&nbsp;&nbsp;</td></tr>
    <tr><td width="10%">&nbsp;&nbsp;</td><td colspan="3">
            
<table style="width:100%; border-collapse: collapse; border: 5px solid #00ae4d;"><tr><td style="border: 1px solid #00ae4d;">

<table border="0" style="width:100%;">
       <tr><td style="width:50%">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
<asp:Panel ID="Panel_My_Appraisal" runat="server" Visible="false">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;" width="50%">My Appraisal
                           <a class="tooltip" href="#"><img src="images/Help.png" height="15" width="15" border="0"/><span class="custom info">
                             - View my current appraisal after it's routed to me<br/>
                             - Esign my current appraisal<br/>
                             - View previous year appraisal</span></a></td>
           <td>&nbsp;&nbsp;
               <asp:DropDownList ID="DDLYears_MY" runat="server" OnSelectedIndexChanged="Year_Change_MY" AutoPostBack="true" CssClass="DropDown_StyleSheet"/>&nbsp;&nbsp;
               <asp:Button ID="SubmitPer" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
       <tr><td style="width:50%">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_My_Goal" runat="server" Visible="false">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">My Goals FY
                   <a class="tooltip" href="#"><img src="images/Help.png" height="15" width="15" border="0"/><span class="custom info">
                      - After you have created your goals and your manager has approved them you can view your goals here. You can also edit your current goals at any time<br/></span></a></td>
           <td>&nbsp;&nbsp;
               <asp:DropDownList ID="DDLYears_My_Goal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_My_Goal" CssClass="DropDown_StyleSheet" />&nbsp;&nbsp;
               <asp:Button ID="Submit_Goal1" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" ForeColor="black" BorderStyle="None"/></td></tr>
       <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_My_Staff" runat="server" Visible="false">
       <tr><td colspan="2"><hr style="border: 1px solid #00ae4d;"/></td></tr>
       <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>
                                        
<asp:Panel ID="Panel_My_Employees_Appraisal" runat="server">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">My Direct Report's Appraisal
                  <a class="tooltip" href="#"><img src="images/Help.png" height="15" width="15" border="0"/><span class="custom info">
                       - Enter and route current year<br/>
                       - View previous year</span></a></td>
           <td>&nbsp;&nbsp;
               <asp:DropDownList ID="DDLYears_Employees" runat="server" OnSelectedIndexChanged="Year_Change_Employees" AutoPostBack="true" CssClass="DropDown_StyleSheet"/>&nbsp;&nbsp;
               <asp:DropDownList ID="DDLMy_Employees" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Change_Employee" Enabled="false"  CssClass="DropDown_StyleSheet" Width="160px"/>&nbsp;&nbsp;
               <asp:Button ID="SubmitMyEmployees" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
       <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Employee_Goal" runat="server">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">Revise or Approve Goals for My Direct Reports
                  <a class="tooltip" href="#"><img src="images/Help.png" height="15" width="15" border="0"/><span class="custom info">
                      - review my direct report's goals<br/>
                      - revise and comment on goals that my direct report has already submitted</span></a></td>
           <td>&nbsp;&nbsp;
               <asp:DropDownList ID="Year_MyEmpl_Goal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Change_MyEmployee_Goal" CssClass="DropDown_StyleSheet" />&nbsp;&nbsp;
               <asp:DropDownList ID="DDLMyEmpl_Goal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_MyEmployee_Goal" Enabled="false" Width="160px" CssClass="DropDown_StyleSheet"/>&nbsp;&nbsp;
               <asp:Button ID="Submit_MyEmpl_Goal" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" ForeColor="black"  BorderStyle="None"/></td></tr>
      <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>
              
<asp:Panel ID="Panel_My_Staff1" runat="server" Visible="false">
       <tr><td colspan="2"><hr style="border: 1px solid #00ae4d;"/></td></tr>
       <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Appraisal_Waiting" runat="server">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">Appraisal waiting for my approval</td>
           <td>&nbsp;&nbsp;
               <asp:DropDownList ID="DDLYears_Waiting" runat="server" OnSelectedIndexChanged="Year_Change_Waiting" AutoPostBack="true" CssClass="DropDown_StyleSheet"/>&nbsp;&nbsp;
               <asp:DropDownList ID="DDLEmployees_Waiting" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Change_Waiting" Enabled="false" CssClass="DropDown_StyleSheet" Width="160px"/>&nbsp;&nbsp;
               <asp:Button ID="SubmitWaiting" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
       <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal_Waiting" runat="server">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">Goal waiting for my approval</td>
           <td>&nbsp;&nbsp;
               <asp:DropDownList ID="DDLYears_Goal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Change_Goal" CssClass="DropDown_StyleSheet"/>&nbsp;&nbsp;
               <asp:DropDownList ID="DDLEmployees_Goal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Change_Goal" Enabled="false" CssClass="DropDown_StyleSheet" Width="160px"/>&nbsp;&nbsp;
               <asp:Button ID="Submit_Goal" text="Open" runat="server"  CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
      <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Appraisal_Report" runat="server">
      <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">Appraisal Status Report</td>
          <td>&nbsp;&nbsp;
              <asp:DropDownList ID="DDLYears_Report" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Change_Report" CssClass="DropDown_StyleSheet" />&nbsp;&nbsp;
              <asp:DropDownList ID="DDLEmployees_Report" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Change_Report" Enabled="false" CssClass="DropDown_StyleSheet" Width="160px"/>&nbsp;&nbsp;
              <asp:Button ID="SubmitReport" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
      <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>


<asp:Panel ID="Panel_Goals_Report" runat="server">
      <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">Goals Status Report</td>
          <td>&nbsp;&nbsp;
              <asp:DropDownList ID="DDLYearsGoals_Reports" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Goal_Report" CssClass="DropDown_StyleSheet" />&nbsp;&nbsp;
              <asp:DropDownList ID="DDLGoals_Reports" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Goal_Report" Enabled="false" CssClass="DropDown_StyleSheet" Width="160px"/>&nbsp;&nbsp;
              <asp:Button ID="Submit_Goals_Report" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
      <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Midpoint_Report" runat="server">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">MidPoint Status Report</td>
           <td>&nbsp;&nbsp;
              <asp:DropDownList ID="DDLYear_MidPoint" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_MidPoint" CssClass="DropDown_StyleSheet"/>&nbsp;&nbsp;
              <asp:DropDownList ID="DDLEmployees_Midpoint" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Employees_MidPoint" Enabled="false"  CssClass="DropDown_StyleSheet" Width="160px"/>&nbsp;&nbsp;
              <asp:Button ID="Submit_MidPoint" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
      <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Employee_Adjustment" runat="server">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">Employee Adjustment</td>
           <td>&nbsp;&nbsp;
               <asp:DropDownList ID="DDLYear_Change_Department" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Change_Department" CssClass="DropDown_StyleSheet" />&nbsp;&nbsp;
               <asp:DropDownList ID="DDL_Change_Department" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Update_Department" Enabled="false"  CssClass="DropDown_StyleSheet" Width="160px"/>&nbsp;&nbsp;
               <asp:Button ID="SubmitDepartment_Change" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
       <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Appraisal_Print" runat="server">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">Bulk Print</td>
           <td>&nbsp;&nbsp;
              <asp:DropDownList ID="DDLYears_Print" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Change_Print" CssClass="DropDown_StyleSheet" Width="90px"/>&nbsp;&nbsp;
              <asp:DropDownList ID="DDLBulk_Print" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Bulk_Change_Print" Enabled="false" CssClass="DropDown_StyleSheet" Width="160px" disabled/>&nbsp;&nbsp;
              <asp:Button ID="BulkPrint" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
              <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Appraisal_Term_Print" runat="server">
       <tr><td style="color: #00ae4d; font-size:larger; text-align:right;">Terminated Employee(s)</td>
           <td>&nbsp;&nbsp;
                <asp:DropDownList ID="DDEmployees_Term" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Update_Term"   CssClass="DropDown_StyleSheet" Width="160px"/>&nbsp;&nbsp;
                <asp:DropDownList ID="DDLYears_Term_Print" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Year_Change_Term_Print" Enabled="false" CssClass="DropDown_StyleSheet" />&nbsp;&nbsp;
                <asp:Button ID="BulkPrint_Term" text="Open" runat="server" CssClass="Button_StyleSheet" BackColor="#00ae4d" Font-Size="Medium" Width="80px" BorderStyle="None"/></td></tr>
       <tr><td colspan="2">&nbsp;&nbsp;</td></tr>
</asp:Panel>

</table>
      </td></tr>

</table>
     </td><td width="10%">&nbsp;&nbsp;</td></tr>

    <tr><td colspan="5">&nbsp;&nbsp;</td></tr>
    <tr><td colspan="5" style="text-align:center"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/Back_button.png" style="width:90px" /></td></tr>
 
</table>    

  </form>
 </body>
</html>
