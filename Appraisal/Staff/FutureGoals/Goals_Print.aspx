<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Goals_Print.aspx.vb" Inherits="Appraisal.Goals_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Goal-Setting Form</title>

<script src="/offline/offline.min.js"></script>
<link rel="stylesheet" href="/offline/themes/offline-theme-dark.css"/>
<link rel="stylesheet" href="/offline/themes/offline-language-english.css"/>

<script>
    var run = function ()
    {
    var req = new XMLHttpRequest();
                    //req.timeout = 5000;
    req.open('GET', 'http://localhost:8888/walter/0', true);
    req.send();
    }

    setInterval(run, 3000);
</script>   



<link href="../../StyleSheet1.css" rel="stylesheet" />

<style type="text/css">
   .Style0 {
         width:100%; 
         }
   .Style1 {
          text-align:center;
          font-size:x-large;
          font-weight:bold;
          color: #00ae4d;
          font-family:Calibri;
          }
   .Style2 {
           width: 350px;
           font-family: Calibri;
               }
   .Style3 {
           width: 95px;
           text-align: left;
           font-family: Calibri;          
               }
   .style4 {
          width: 200px;
          text-align:left;
          font-family:Calibri;
          }
   .style5 {
          width: 90px;
          text-align:right;
          font-family:Calibri;
          }
   .style6 {
          width: 10%;
          text-align:left;
          font-family:Calibri;
          }
   .style7 {
          width: 500px;
          font-family:Calibri;
          }
   .style8 {
          width: 300px;
          font-family:Calibri;
          }
   .style9 {
          width: 20%;
          background-color:#E7E8E3;
          font-family:Calibri;
          }
   .style10 {
          width: 5%;
          background-color:#E7E8E3;
          font-family:Calibri;
          }
   .style11 {
          font-weight:bold; 
          background-color:#E7E8E3;
          font-family:Calibri;
          }
   .style12 {
          width: 240px;
          text-align: left;
          font-family:Calibri;
          }
   .style13 {
          font-weight: normal;
          font-size:medium;
          color:black;
          font-family:Calibri;
          }
   .Style14 {
          text-align:center;
          font-size:large;
          color: #00ae4d;      
          height:30px;
          font-family:Calibri;
           }
   .style15   {
        font-weight: bold;
        width: 14%;
        vertical-align:top;
        background-color:lightgray;
        border-collapse:collapse; 
        font-family:Calibri;
        }
   .style16  {
        width:2%;
        vertical-align:top;
        font-family:Calibri;
        }
   .StyleBreak  {
        page-break-after: always;
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
            width: 200px;
            text-align:left;
	      	}
	.tooltip:hover img {
			border: 0; margin: -10px 0 0 -55px;
			float: left; position: absolute;
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
            border-color:black;
            font-family:Calibri;
            }
     .ButtonAsLink{
             background-color:transparent;
             border:none;
             color:blue;
             cursor:pointer;
             text-decoration:underline;
             padding: 0px;
             font-family:Calibri;
             font-size:medium;
         }
     .topnavigation {
         width: 100px;
         position:static
          }
     .topnavigation.scrolling {
         position:fixed;
         top:0px;
          }  
       div{
         word-wrap:normal; 
         word-break:normal;
          }
    .auto-style3 {
        height: 22px;
    }
    </style>


</head>
<body onload="window.print()">

    <form id="form1" runat="server">

<asp:label id="LblEligible_Full_Review" runat="server" Text="" Visible="false"/>
<asp:label id="lblEMPLID" runat="server" Text="" Visible="true" ForeColor="white" BackColor="White" />
<asp:label id="lblLogin_EMPLID" runat="server" Text="" Visible="true" ForeColor="white" BackColor="White" />
<asp:label id="lblEmpl_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblFirst_SUP_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblFirst_SUP_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblHR_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblHR_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblFlag" runat="server" Text="" Visible="false"/>
<asp:label id="lblComments" runat="server" Text="" Visible="false"/>
<asp:label id="lblYear" runat="server" Text="" Visible="false"/>
<asp:label id="lblWindowBatch" runat="server" Text="" Visible="true" style="display:none"/><!--style="display:none"-->
<asp:label id="lblDataBaseBatch" runat="server" Text="" Visible="true" style="display:none"/>
<asp:label id="LblEmpl_Type" runat="server" Text="" Visible="true" style="display:none"/>    
        
<table class="Style0" border="0">
    <tr><td class="style6" rowspan="5"></td><td class="Style1"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td>
        <td class="style6" rowspan="5"></td>
   <tr><td class="Style1"><u>FY<asp:Label ID="Goal_Year" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> 
        Goal-Setting Form (06/01/<asp:Label ID="Goal_Year1" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> - 05/31/<asp:Label ID="Goal_Year2" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td></tr>
  <tr><td class="auto-style3">&nbsp;&nbsp;</td></tr> 
  <tr><td>
           <table style="width:100%" border="0" class="TextBox_StyleSheet">
            <tr><td style="width:7%; font-family: Calibri;"><b>Name:</b></td>
                <td style="width:30%;"><asp:label id="lblEmpl_NAME" runat="server" CssClass="Label_StyleSheet"/></td>
                <td class="style4">&nbsp;&nbsp;</td>
                <td class="style2">&nbsp;&nbsp;</td>
                <td style="width:5%; font-family: Calibri;">Status:&nbsp;&nbsp;</td>
                <td style="width:20%;"><asp:Label ID="LblEMP_Appr" runat="server" Font-Names="Calibri"/></td></tr>
            <tr><td  style="width:12%; font-family: Calibri;"><b>Title:</b></td>
                <td><asp:label id="lblEmpl_TITLE" runat="server" Font-Names="Calibri"/></td>
                <td style="font-family: Calibri;"><b>Manager Name:&nbsp;</b></td>
                <td><asp:Label ID="LblMGT_NAME" runat="server" Font-Names="Calibri" /></td>
                <td style="font-family: Calibri;">Approved:&nbsp;&nbsp;</td>
                <td><asp:Label ID="LblMGT_Appr" runat="server" Font-Names="Calibri" /></td></tr>
            <tr><td style="font-family: Calibri;"><b>Department:</b></td>
                <td><asp:label id="lblEmpl_DEPT" runat="server" Font-Names="Calibri"/></td>
                <td style="font-family: Calibri;">&nbsp;</b></td>
                <td><asp:Label ID="lblUP_MGT_NAME" runat="server" Font-Names="Calibri" Visible="false"/></td>
                <td style="font-family: Calibri;">&nbsp;&nbsp;</td><!--Approved:-->
                <td>&nbsp;</td></tr>
            <tr><td style="font-family: Calibri;"><b>Hire Date:</b></td>
                <td><asp:label id="lblEmpl_HIRE" runat="server" Font-Names="Calibri"/></td>
                <td style="font-family: Calibri;"><b>&nbsp;&nbsp;</b></td>
                <td>&nbsp;&nbsp;</td>
                <td style="font-family: Calibri;">&nbsp;&nbsp;</td>
                <td>&nbsp;</td></tr></table>
  </td></tr>


   <tr><td></td></tr>

<tr><td>&nbsp;&nbsp;</td>
    <td style="font-family:Calibri; font-size:small;">Enter SMART Goals (<strong>S</strong>pecific, <strong>M</strong>easurable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to 
        focus on achieving important, tangible outcomes that contribute to the achievement of CR&#39;s  strategic plan.&nbsp;&nbsp;Please click 
            <a href="https://docs.google.com/presentation/d/1LIm9LOScdiizJcXARapml7-gj2IkJfq-MtQgkBFNHho/edit#slide=id.g1f6e4888fe_0_0" target="_blank">here</a> for a SMART goal example.</td>
    <td>&nbsp;&nbsp;</td></tr>
<tr><td class="style6">&nbsp;</td>
    <td>

  <table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="0">
      <tr><td>
<table style="width:100%; border-collapse:collapse; border-spacing:0" border="1">
<tr><td style="background-color:lightgray; width:3%;"></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">Goals
        <div id="divGoalText1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic; word-wrap: normal; word-break: normal;">What do I need to accomplish in order to support my department’s goals and CR’s goals?</div></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">
        <div id="divOldTitle1" runat="server">Success Measures or Milestones</div>
        <div id="divNewTitle1" runat="server">Key Results</div>
        <div id="divNewKey1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic; word-wrap: normal; word-break: normal;">How will I know that I’ve accomplished each goal?</div></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:10%; font-family:Calibri; background-color:lightgray; word-wrap: normal; word-break: normal;">Target<br />Completion<br />Date
        <div id="divNewtarget1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic; word-wrap: normal; word-break: normal;">When will I <br />accomplish this goal?</div></td></tr>
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">1)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit11" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit11" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit11" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr>

<asp:Panel ID="Panel_FutureGoal_Review2" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">2)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit12" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit12" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit12" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Review3" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">3)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit13" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit13" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit13" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr></asp:Panel>
<asp:Panel ID="Panel_FutureGoal_Review4" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">4)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit14" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit14" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit14" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr></asp:Panel>
<asp:Panel ID="Panel_FutureGoal_Review5" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">5)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit15" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit15" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit15" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr></asp:Panel>
<asp:Panel ID="Panel_FutureGoal_Review6" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">6)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit16" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit16" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit16" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr></asp:Panel>
<asp:Panel ID="Panel_FutureGoal_Review7" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">7)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit17" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit17" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit17" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr></asp:Panel>
<asp:Panel ID="Panel_FutureGoal_Review8" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">8)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit18" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit18" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit18" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr></asp:Panel>
<asp:Panel ID="Panel_FutureGoal_Review9" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">9)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit19" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit19" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit19" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr></asp:Panel>
<asp:Panel ID="Panel_FutureGoal_Review10" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">10)</td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Goal_Edit20" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Succ_Edit20" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td>
    <td style="vertical-align:top;"><div><asp:Label ID="FUT_Date_Edit20" runat="server" Width="100%" Font-Names="Calibri" Font-Size="Small"/></div></td></tr></asp:Panel>
</table>


  </td></tr>

<tr><td style="text-align:center;"></td></tr>

</table>
</td><td>&nbsp;&nbsp;&nbsp;</td>


</tr>

</table>
            
</td><td>&nbsp;&nbsp;&nbsp;</td></tr>

   </table>    

  </form>
 </body>
</html>
