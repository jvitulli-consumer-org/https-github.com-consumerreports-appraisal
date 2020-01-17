<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Manager_Appraisal.aspx.vb" Inherits="Appraisal.HR_Appraisal_Review" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appraisal Review</title>

<link href="../../StyleSheet1.css" rel="stylesheet" />

<style type="text/css">
          .Style1 
        {
  text-align:center;
  font-size:x-large;
  font-weight:bold;
  color: #00ae4d;
  font-family:Calibri;
          }
        .Style2 {
            text-align: center;
            font-size: large;
            font-weight: bold;
            height: 30px;
            font-family:Calibri;
        }
        .Style3 
        {
  font-size:large;
  font-weight:bold;
  color: #00ae4d;
  font-family:Calibri;
        }
        .style4 {
            width: 100px;
            text-align:left;
            font-family:Calibri;
        }
        .style5 {
            width: 55px;
            text-align:right;
            font-family:Calibri;
        }
        .style6 {
            width: 220px;
            text-align:left;
            font-family:Calibri;
        }
 
        .style7 {
            width: 400px;
            font-family:Calibri;
        }
 
        .style8 {
            width: 350px;
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
        .style11 
        {
        text-align:center; 
        font-weight:bold; 
        background-color:#E7E8E3;
        font-family:Calibri;
        }
        .style12 {
            width: 230px;
            text-align: left;
            font-family:Calibri;
        }
        .style13 {
            font-weight: normal;
            font-size:small;
            color:black;
            font-family:Calibri;
        }

         .StyleBreak
         {
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
			position: absolute; left: 1em; top: 2em; z-index: 99;
			margin-left: 0; 
            width: 300px;
            text-align:left;
		}
		.tooltip:hover img {
			border: 0; margin: -10px 0 0 -55px;
			float: left; position: absolute;
		}
		.tooltip:hover em {
			font-family: Arial; 
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

        .BorderColor 
        {
            border-style:solid; 
            border-width:1px;
            border-color: #00ae4d;
            font-family:Calibri;
        }

        #rotate {
            -moz-transform: rotate(-90.0deg); /* FF3.5+ */
            -o-transform: rotate(-90.0deg); /* Opera 10.5 */
            -webkit-transform: rotate(-90.0deg); /* Saf3.1+, Chrome */
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=0.083); /* IE6,IE7 */
            -ms-filter: "progid:DXImageTransform.Microsoft.BasicImage(rotation=0.083)"; /* IE8 */
        }

        @media print
          {    
          .no-print, .no-print *
          {
        display: none !important;
           }
           }
</style>

</head>
<body>
    <form id="form1" runat="server">

<!--All Labels that converted from Session("")-->
<asp:label id="lblEMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_NAME" runat="server" Text="" Visible="false"/>
<asp:label id="lblFirst_SUP_NAME" runat="server" Text="" Visible="false"/>
<asp:label id="LblFirst_SUP_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="LblEligible_Full_Review" runat="server" Text="" Visible="false" />
<asp:label id="lblYEAR" runat="server" Text="" Visible="false"/>
<asp:label id="lblEMPL_Supervision" runat="server" Text="" Visible="false"/>
<asp:label id="LblDEPTID" runat="server" Text="" Visible="false"/>
<asp:label id="LblSTATUS" runat="server" Text="" Visible="false"/>



<table style="width:100%;" border="0"><tr><td style="width:20px;">&nbsp;</td><td>
<!--PAGE 1-->
<div id="Div1" runat="server" visible="true">

<table border="0" style="width:100%">
   <tr><td style="text-align:center;"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td></tr>
    <tr><td class="Style1"><u>FY<asp:Label ID="LblCur_Year" runat="server" Text="" Font-Bold="true" ForeColor="#00ae4d" Font-Names="Calibri" />&nbsp;PERFORMANCE APPRAISAL</u></td></tr>

   <tr style="height:2px; font-size:3px; "><td>&nbsp;</td></tr>
   <tr><td>
        <table border="0" style="width:95%">
           <tr><td class="style4"><strong>Name:</strong></td>
               <td class="style7"><asp:label id="lblEMPLOYEE_NAME" runat="server" Text="" Font-Names="Calibri"/></td>
               <td class="style12"><b></b></td>
               <td class="style8">&nbsp;</td>
               <td style="text-align:right; font-family:Calibri;">E-Signed:&nbsp;&nbsp;</td>
               <td><asp:Label ID="LblEMP_Appr" runat="server" Text="" Font-Names="Calibri" /></td> </tr>
           <tr><td class="style4"><strong>Title:</strong></td>
               <td class="style7"><asp:label id="lblEMPLOYEE_TITLE" runat="server" Text="" Font-Names="Calibri"/></td>
               <td class="style12"><b>Manager Name:&nbsp;</b></td>
               <td class="style8"><asp:Label ID="LblMgr_NAME" runat="server" Text="" Font-Names="Calibri" /></td>
               <td style="text-align:right; font-family:Calibri; width:100px;">Approved:&nbsp;&nbsp;</td>
               <td><asp:Label ID="LblFirst_Mgr_Appr" runat="server" Text="" Font-Names="Calibri" /></td></tr>
           <tr><td class="style4"><strong>Department:</strong></td>
               <td class="style7"><asp:label id="lblEMPLOYEE_DEPT" runat="server" Text="" Font-Names="Calibri"/></td>
               <td class="style12"><b>2nd Level Manager Name:&nbsp;</b></td>
               <td class="style8"><asp:Label ID="lblMGR_UP_NAME" runat="server" Text="" Font-Names="Calibri" /></td>
               <td style="text-align:right; font-family:Calibri;">Approved:&nbsp;&nbsp;</td>
               <td><asp:Label ID="LblSec_Mgr_Appr" runat="server" Text="" Font-Names="Calibri" /></td></tr>
           <tr><td class="style4"><strong>Hire Date:</strong></td>
               <td class="style7"><asp:label id="lblEMPLOYEE_HIRE" runat="server" Text="" Font-Names="Calibri"/></td>
               <td class="style12"><b>Human Resources Generalist:</b></td>
               <td class="style8"><asp:label id="lblGENERALIST_NAME" runat="server" Text="" Font-Names="Calibri"/></td>
               <td  style="text-align:right; font-family:Calibri;">Approved:&nbsp;&nbsp;</td>
               <td><asp:Label ID="LblHR_Appr" runat="server" Text="" Font-Names="Calibri" /></td></tr>
        </table>

       </td></tr>
<tr><td>&nbsp;
    <asp:Label ID="Discuss" text="" runat="server" Visible="false" Font-Names="Calibri"/>
    </td></tr> 

<!--1. Accomplishments-->
    <tr><td>

<table border="0" style="width:100%;">
    <tr><td style="font-size:large; font-weight:bold; color: #00ae4d; font-family:Calibri;" colspan="2"><u>Accomplishments:</u>
             <span class="style13"><u>(Summarize accomplishments vs the goals/expectations established for FY<asp:Label ID="LblCur_Year1" runat="server" Text="" Font-Names="Calibri"/>)</u></span></td></tr>
    <tr><td style="font-family:Calibri;"><asp:Label ID="Goals_1" runat="server" Visible="false"/><%=Goals_1.Text%></td></tr>

</table>

</td></tr>
<tr><td>

<!--2. Strengths-->   
<table border="0" style="width:100%;">
    <tr><td style="font-size:large; font-weight:bold; color: #00ae4d; font-family:Calibri;"><u>Strengths:</u><span class="style13"><u>(Comment on key strengths and highlight areas performed well this year)</u></span></td></tr>
       <tr><td style="font-family:Calibri;"><asp:Label ID="Strengths" runat="server" Visible="false"/><%=Strengths.Text%></td></tr>
</table>
<!--2. END Strengths-->
</td></tr>
<tr><td>

<!--3. Delepopment-->   
<table border="0" style="width:100%;">
   <tr><td style="font-size:large; font-weight:bold; color: #00ae4d; font-family:Calibri;"><u>Development Areas:</u> <span class="style13"><u>(Comment on areas of performance that require futher development or improvement. Cite examples of areas of performance that could have been better this year)</u></span></td></tr>
       <tr><td style="font-family:Calibri;"><asp:Label  ID="Development" runat="server" Visible="false"/><%=Development.Text%></td></tr></table>
<!--3. END Delepopment-->   
</td></tr>
<tr><td>

<!--4. Overall Performance rating-->
 <table border="0" style="width:100%;">      
   <tr><td style="font-size:large; font-weight:bold; color: #00ae4d; font-family:Calibri;"><u>Overall Performance Rating:</u><span class="style13"><u>(Check the box that most appropriately describes the individual&#39;s overall performance)</u></span></td></tr>
       <tr><td><table id="OVER" border="1" style="width:95%; text-align:center; font-weight:bold; border-spacing:0;" runat="server">
              <tr><td class="style9">Underperforming</td>
                  <td class="style9">Developing/Improving Contributor</td>        
                  <td class="style9">Solid Contributor</td>
                  <td class="style9">Very Strong Contributor</td>
                  <td class="style9">Distinguished Contributor</td></tr>
              <tr><td><asp:RadioButton ID="rbBelow" runat="server" text="" GroupName="OverAll" Enabled="false"/></td>
              <td><asp:RadioButton ID="rbNeed" runat="server" text="" GroupName="OverAll"  Enabled="false"/></td>
              <td><asp:RadioButton ID="rbMeet" runat="server" text="" GroupName="OverAll"  Enabled="false"/></td>
              <td><asp:RadioButton ID="rbExceed" runat="server" text="" GroupName="OverAll" Enabled="false"/></td>
              <td><asp:RadioButton ID="rbDisting" runat="server" text="" GroupName="OverAll" Enabled="false"/></td></tr></table>

</td></tr>

<!--5. Overall Summary-->   
   <tr><td style="font-size:large; font-weight:bold; color: #00ae4d; font-family:Calibri;"><u>Overall Summary:</u><span class="style13"><u>(Comment on overall performance in FY<asp:Label ID="LblCur_Year2" runat="server" Text="" Font-Names="Calibri" />)</u></span></td></tr>
       <tr><td style="font-family:Calibri;"><asp:Label ID="OverAll_Sum" runat="server" Visible="false"/><%=OverAll_Sum.Text%>
 <!--3. OverAll    --->
           </td></tr>
</table>

<!--5. END--> 
</table>

</div>
<!--PAGE 1 END-->
<!--xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx-->

<!--PAGE 2 -->
<div id="Div2" runat="server" visible="true" >
<table border="0" style="width:100%;" >
<!--<tr><td style="text-align:center;"><img alt="images/CR_logo.png" src="images/CR_logo.png" style="width: 380px; height:60px" /></td></tr>-->
<tr><td class="Style1"><u>FY<asp:Label ID="LblNext_Year" runat="server" Text="" Font-Names="Calibri" />&nbsp;Goal-Setting</u></td></tr>
<tr><td style="font-size:3px;">&nbsp;</td></tr>
<tr><td style="font-family:Calibri;">SMART Goals (<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) focus employees on achieving important, tangible outcomes that 
    contribute to the achievement of the Enterprise Goals set for FY<asp:Label ID="LblNext_Year2" runat="server" Text="" Font-Names="Calibri" />. Summarize the goals agreed to for the upcoming fiscal year.</td></tr>
<!--    <tr><td style="font-size:3px;">&nbsp;</td></tr>
<tr><td style="font-size:3px;">&nbsp;</td></tr>-->
<tr><td>
<table style="width:95%; border-color: #00ae4d; border-collapse:collapse; border-spacing:0;" border="1">

<tr><td class="BorderColor" style="width:2%;">&nbsp;</td>
    <td class="BorderColor" style="text-align:center; font-size:large; font-weight:bold; width:45%;">Goals</td>
    <td class="BorderColor" style="text-align:center; font-size:large; font-weight:bold; width:40%">Key Results</td>
    <td class="BorderColor" style="text-align:center; font-size:large; font-weight:bold;">Target<br />Completion Date</td></tr>

<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:2%;">1)</td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_1" runat="server" Visible="false"/><%=FUT_Goal_1.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_1" runat="server" Visible="false"/><%=FUT_Succ_1.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_1" runat="server" Visible="false"/><%=FUT_Date_1.Text%></td></tr>

<asp:Panel ID="Panel_FutureGoal2" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;">2)</td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_2" runat="server" Visible="false"/><%=FUT_Goal_2.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_2" runat="server" Visible="false"/><%=FUT_Succ_2.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_2" runat="server" Visible="false"/><%=FUT_Date_2.Text%></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_FutureGoal3" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;">3)</td>
    
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_3" runat="server" Visible="false"/><%=FUT_Goal_3.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_3" runat="server" Visible="false"/><%=FUT_Succ_3.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_3" runat="server" Visible="false"/><%=FUT_Date_3.Text%></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_FutureGoal4" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;">4)</td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_4" runat="server" Visible="false"/><%=FUT_Goal_4.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_4" runat="server" Visible="false"/><%=FUT_Succ_4.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_4" runat="server" Visible="false"/><%=FUT_Date_4.Text%></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_FutureGoal5" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;">5)</td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_5" runat="server" Visible="false"/><%=FUT_Goal_5.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_5" runat="server" Visible="false"/><%=FUT_Succ_5.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_5" runat="server" Visible="false"/><%=FUT_Date_5.Text%></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_FutureGoal6" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;">6)</td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_6" runat="server" Visible="false"/><%=FUT_Goal_6.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_6" runat="server" Visible="false"/><%=FUT_Succ_6.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_6" runat="server" Visible="false"/><%=FUT_Date_6.Text%></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_FutureGoal7" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;">7)</td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_7" runat="server" Visible="false"/><%=FUT_Goal_7.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_7" runat="server" Visible="false"/><%=FUT_Succ_7.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_7" runat="server" Visible="false"/><%=FUT_Date_7.Text%></td></tr>
</asp:Panel>
    
<asp:Panel ID="Panel_FutureGoal8" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;">8)</td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_8" runat="server" Visible="false"/><%=FUT_Goal_8.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_8" runat="server" Visible="false"/><%=FUT_Succ_8.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_8" runat="server" Visible="false"/><%=FUT_Date_8.Text%></td></tr>
</asp:Panel>        

<asp:Panel ID="Panel_FutureGoal9" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;">9)</td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_9" runat="server" Visible="false"/><%=FUT_Goal_9.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_9" runat="server" Visible="false"/><%=FUT_Succ_9.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_9" runat="server" Visible="false"/><%=FUT_Date_9.Text%></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_FutureGoal10" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;">10)</td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Goal_10" runat="server" Visible="false"/><%=FUT_Goal_10.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Succ_10" runat="server" isible="false"/><%=FUT_Succ_10.Text%></td>
    <td class="BorderColor" style="vertical-align:top;"><asp:Label ID="FUT_Date_10" runat="server" Visible="false"/><%=FUT_Date_10.Text%></td></tr>
</asp:Panel>

</table>

    </td></tr>
<tr><td style="font-size:3px;">&nbsp;</td></tr>

</table>
    

</div>
<!--PAGE 2 END-->        
<!--xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx-->
<!--PAGE 3 -->
<!--Added to Master table
    Column_Name         Field Name 
1. Lead_Change         Lead Change
2. Translate_Vision    Translate Vision into Action
3. Inspire_Risk        Inspire Risk Taking & innovation
4. Leverage_External   Leverage External Perspective
5. Communicate_Impact  Communicate for Impact
6. Lead_Urgency        Lead with Urgency & Purpose
7. Promote_Collabor    Promote Collaboration & Accountability
8. Build_Manage        Build & Manage High Performing Teams
9. Confront_Chall      Confront Challenges
10.Make_Balance        Make Balanced Decisions
11.Build_Trust         Build Trust
12.Learn_Continuously  Learn Continuously    
 -->

<div id="Div3" runat="server" visible="true">

<table border="0" style="width:100%;">
<tr><td>
<!--7. Addendum-->
<asp:Panel ID="Page3" runat="server" visible="true">
<table border="0" style="width:100%;">
   <tr><td class="Style1"><u>Addendum - FY<asp:Label ID="LblNext_Year1" runat="server" Text="" Font-Names="Calibri" />&nbsp;Leadership Competencies</u></td></tr>
   <tr><td style="font-size:3px;">&nbsp;</td></tr>
    <tr><td><span class="style13">Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies.
  Use this assessment to provide feedback and guide your direct report on areas to focus on in FY<asp:Label ID="LblNext_Year3" runat="server" Text="" Font-Names="Calibri" /> for further growth and development.</span></td></tr>
</table>
</asp:Panel>
</td></tr>
<!--<tr><td style="font-size:3px;">&nbsp;</td></tr>-->
<tr><td>

<asp:Panel ID="Manage_Employees" runat="server">
<table border="1" style="width:100%; text-align:center; border-color: #00ae4d; border-collapse:collapse; border-spacing:0;" runat="server">
    <tr style="height:33px; font-size:large">
        <td colspan="2" class="BorderColor"></td>
        <td class="BorderColor" style="text-align:center; font-weight:bold; background-color:#E7E8E3; width:15%;">Needs Development/Improvement</td>
        <td class="BorderColor" style="text-align:center; font-weight:bold; background-color:#E7E8E3; width:15%;">Proficient</td>
        <td class="BorderColor" style="text-align:center; font-weight:bold; background-color:#E7E8E3; width:15%;">Excels</td></tr>
    <tr style="height:33px; font-size:large">
        <td rowspan="5" class="style10"><img alt="images/leading.png" src="images/leading.png" /></td>
        <td class="style11 BorderColor">Lead Change</td>
        <td class="BorderColor" id="Lead_Need"><asp:RadioButton ID="rbLead_Need" runat="server" GroupName="Lead_Change" Enabled="false"/></td>
        <td class="BorderColor" id="Lead_Prof"><asp:RadioButton ID="rbLead_Prof" runat="server" GroupName="Lead_Change" Enabled="false"/></td>
        <td class="BorderColor" id="Lead_Exce"><asp:RadioButton ID="rbLead_Exce" runat="server" GroupName="Lead_Change" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td class="style11 BorderColor">Translate Vision into Action</td>
        <td class="BorderColor" id="Tran_Need"><asp:RadioButton ID="rbTran_Need" runat="server" GroupName="Trans_Vision" Enabled="false"/></td>
        <td class="BorderColor" id="Tran_Prof"><asp:RadioButton ID="rbTran_Prof" runat="server" GroupName="Trans_Vision" Enabled="false"/></td>
        <td class="BorderColor" id="Tran_Exce"><asp:RadioButton ID="rbTran_Exce" runat="server" GroupName="Trans_Vision" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td class="style11 BorderColor">Inspire Risk Taking & innovation</td>
        <td class="BorderColor" id="Insp_Need"><asp:RadioButton ID="rbInsp_Need" runat="server" GroupName="Inspire_Risk" Enabled="false"/></td>
        <td class="BorderColor" id="Insp_Prof"><asp:RadioButton ID="rbInsp_Prof" runat="server" GroupName="Inspire_Risk" Enabled="false"/></td>
        <td class="BorderColor" id="Insp_Exce"><asp:RadioButton ID="rbInsp_Exce" runat="server" GroupName="Inspire_Risk" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td class="style11 BorderColor">Leverage External Perspective</td>
        <td class="BorderColor" id="Leve_Need"><asp:RadioButton ID="rbLeve_Need" runat="server" GroupName="Leverage_External" Enabled="false"/></td>
        <td class="BorderColor" id="Leve_Prof"><asp:RadioButton ID="rbLeve_Prof" runat="server" GroupName="Leverage_External" Enabled="false"/></td>
        <td class="BorderColor" id="Leve_Exce"><asp:RadioButton ID="rbLeve_Exce" runat="server" GroupName="Leverage_External" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td class="style11 BorderColor">Communicate for Impact</td>
        <td class="BorderColor" id="Comm_Need"><asp:RadioButton ID="rbComm_Need" runat="server" GroupName="Communic_Impact" Enabled="false"/></td>
        <td class="BorderColor" id="Comm_Prof"><asp:RadioButton ID="rbComm_Prof" runat="server" GroupName="Communic_Impact" Enabled="false"/></td>
        <td class="BorderColor" id="Comm_Exce"><asp:RadioButton ID="rbComm_Exce" runat="server" GroupName="Communic_Impact" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td rowspan="4" class="style10"><img alt="images/leading2.png" src="images/leading2.png" /></td>
        <td class="style11 BorderColor">Lead with Urgency & Purpose</td>
        <td class="BorderColor" id="Lead2_Need"><asp:RadioButton ID="rbLead2_Need" runat="server" GroupName="Lead_Urgency" Enabled="false"/></td>
        <td class="BorderColor" id="Lead2_Prof"><asp:RadioButton ID="rbLead2_Prof" runat="server" GroupName="Lead_Urgency" Enabled="false"/></td>
        <td class="BorderColor" id="Lead2_Exce"><asp:RadioButton ID="rbLead2_Exce" runat="server" GroupName="Lead_Urgency" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td class="style11 BorderColor">Promote Collaboration & Accountability</td>
        <td class="BorderColor" id="Prom_Need"><asp:RadioButton ID="rbProm_Need" runat="server" GroupName="Promote_Collab" Enabled="false"/></td>
        <td class="BorderColor" id="Prom_Prof"><asp:RadioButton ID="rbProm_Prof" runat="server" GroupName="Promote_Collab" Enabled="false"/></td>
        <td class="BorderColor" id="Prom_Exce"><asp:RadioButton ID="rbProm_Exce" runat="server" GroupName="Promote_Collab" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td class="style11 BorderColor">Build & Manage High Performing Teams</td>
        <td class="BorderColor" id="Build_Need"><asp:RadioButton ID="rbBuild_Need" runat="server" GroupName="Build_Manage" Enabled="false"/></td>
        <td class="BorderColor" id="Build_Prof"><asp:RadioButton ID="rbBuild_Prof" runat="server" GroupName="Build_Manage" Enabled="false"/></td>
        <td class="BorderColor" id="Build_Exce"><asp:RadioButton ID="rbBuild_Exce" runat="server" GroupName="Build_Manage" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td class="style11 BorderColor">Confront Challenges</td>
        <td class="BorderColor" id="Conf_Need"><asp:RadioButton ID="rbConf_Need" runat="server" GroupName="Confront_Challenge" Enabled="false"/></td>
        <td class="BorderColor" id="Conf_Prof"><asp:RadioButton ID="rbConf_Prof" runat="server" GroupName="Confront_Challenge" Enabled="false"/></td>
        <td class="BorderColor" id="Conf_Exce"><asp:RadioButton ID="rbConf_Exce" runat="server" GroupName="Confront_Challenge" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large"><td rowspan="3" class="style10"><img alt="images/leading3.png" src="images/leading3.png" /></td>
        <td class="style11 BorderColor">Make Balanced Decisions</td>
        <td class="BorderColor" id="Make_Need"><asp:RadioButton ID="rbMake_Need" runat="server" GroupName="Make_Balance" Enabled="false"/></td>
        <td class="BorderColor" id="Make_Prof"><asp:RadioButton ID="rbMake_Prof" runat="server" GroupName="Make_Balance" Enabled="false"/></td>
        <td class="BorderColor" id="Make_Exce"><asp:RadioButton ID="rbMake_Exce" runat="server" GroupName="Make_Balance" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td class="style11 BorderColor">Build Trust</td>
        <td class="BorderColor" id="Build2_Need"><asp:RadioButton ID="rbBuild2_Need" runat="server" GroupName="Build_Trust" Enabled="false"/></td>
        <td class="BorderColor" id="Build2_Prof"><asp:RadioButton ID="rbBuild2_Prof" runat="server" GroupName="Build_Trust" Enabled="false"/></td>
        <td class="BorderColor" id="Build2_Exce"><asp:RadioButton ID="rbBuild2_Exce" runat="server" GroupName="Build_Trust" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        <td class="style11 BorderColor">Learn Continuously</td>
        <td class="BorderColor" id="Learn_Need"><asp:RadioButton ID="rbLearn_Need" runat="server" GroupName="Learn_Continuously" Enabled="false"/></td>
        <td class="BorderColor" id="Learn_Prof"><asp:RadioButton ID="rbLearn_Prof" runat="server" GroupName="Learn_Continuously" Enabled="false"/></td>
        <td class="BorderColor" id="Learn_Exce"><asp:RadioButton ID="rbLearn_Exce" runat="server" GroupName="Learn_Continuously" Enabled="false"/></td></tr>
</table>
</asp:Panel>

<asp:Panel ID="No_Employees" runat="server">
  
         <table border="1" style="width:100%; text-align:center; border-color: #00ae4d; border-collapse:collapse; border-spacing:0;" runat="server">
    <tr style="height:33px; font-size:large">
        <td class="BorderColor" style="text-align:center; font-weight:bold; font-family:Calibri;">LEADING ONESELF</td>
        <td class="BorderColor" style="text-align:center; font-weight:bold; background-color:#E7E8E3; width:15%;">Needs Development/Improvement</td>
        <td class="BorderColor" style="text-align:center; font-weight:bold; background-color:#E7E8E3; width:15%;">Proficient</td>
        <td class="BorderColor" style="text-align:center; font-weight:bold; background-color:#E7E8E3; width:15%;">Excels</td></tr>
    
    <tr style="height:33px; font-size:large">
        <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%">
           <tr><td style="width:45%">&nbsp;</td>
               <td style="width:55%; text-align:left; Font-Family:Calibri;">Make Balanced Decisions</td></tr></table></td>
               
        
        <td class="BorderColor" id="Make_Need1"><asp:RadioButton ID="rbMake_Need1" runat="server" GroupName="Make_Balance" Enabled="false"/></td>
        <td class="BorderColor" id="Make_Prof1"><asp:RadioButton ID="rbMake_Prof1" runat="server" GroupName="Make_Balance" Enabled="false"/></td>
        <td class="BorderColor" id="Make_Exce1"><asp:RadioButton ID="rbMake_Exce1" runat="server" GroupName="Make_Balance" Enabled="false"/></td></tr>
    
       <tr style="height:33px; font-size:large">
           <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%">
           <tr><td style="width:45%">&nbsp;</td>
               <td style="width:55%; text-align:left;">Build Trust</td></tr></table></td>

        <td class="BorderColor" id="Build2_Need1"><asp:RadioButton ID="rbBuild2_Need1" runat="server" GroupName="Build_Trust" Enabled="false"/></td>
        <td class="BorderColor" id="Build2_Prof1"><asp:RadioButton ID="rbBuild2_Prof1" runat="server" GroupName="Build_Trust" Enabled="false"/></td>
        <td class="BorderColor" id="Build2_Exce1"><asp:RadioButton ID="rbBuild2_Exce1" runat="server" GroupName="Build_Trust" Enabled="false"/></td></tr>

    <tr style="height:33px; font-size:large">
         <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%"><tr>
         <td style="width:45%">&nbsp;</td>
         <td style="width:55%; text-align:left; Font-Family:Calibri;">Learn Continuously</td></tr></table></td>
        <td class="BorderColor" id="Learn_Need1"><asp:RadioButton ID="rbLearn_Need1" runat="server" GroupName="Learn_Continuously" Enabled="false"/></td>
        <td class="BorderColor" id="Learn_Prof1"><asp:RadioButton ID="rbLearn_Prof1" runat="server" GroupName="Learn_Continuously" Enabled="false"/></td>
        <td class="BorderColor" id="Learn_Exce1"><asp:RadioButton ID="rbLearn_Exce1" runat="server" GroupName="Learn_Continuously" Enabled="false"/></td></tr>
 
<tr style="height:40px; font-size:large">
       <td class="BorderColor" style="text-align:center; font-weight:bold; font-family:Calibri;">LEADING OTHERS</td>
       <td class="auto-style2" colspan="3"></td></tr>

<tr style="height:33px; font-size:large">
       <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%">
           <tr><td style="width:45%">&nbsp;</td>
               <td style="width:55%; text-align:left; Font-Family:Calibri;">Lead with Urgency & Purpose</td></tr></table></td>

        <td class="BorderColor" id="Lead2_Need1"><asp:RadioButton ID="rbLead2_Need1" runat="server" GroupName="Lead_Urgency" Enabled="false"/></td>
        <td class="BorderColor" id="Lead2_Prof1"><asp:RadioButton ID="rbLead2_Prof1" runat="server" GroupName="Lead_Urgency" Enabled="false"/></td>
        <td class="BorderColor" id="Lead2_Exce1"><asp:RadioButton ID="rbLead2_Exce1" runat="server" GroupName="Lead_Urgency" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
        
       <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%">
         <tr><td style="width:45%">&nbsp;</td>
         <td style="width:55%; text-align:left; Font-Family:Calibri;">Promote Collaboration & Accountability</td></tr></table></td>

        <td class="BorderColor" id="Prom_Need1"><asp:RadioButton ID="rbProm_Need1" runat="server" GroupName="Promote_Collab" Enabled="false"/></td>
        <td class="BorderColor" id="Prom_Prof1"><asp:RadioButton ID="rbProm_Prof1" runat="server" GroupName="Promote_Collab" Enabled="false"/></td>
        <td class="BorderColor" id="Prom_Exce1"><asp:RadioButton ID="rbProm_Exce1" runat="server" GroupName="Promote_Collab" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
       <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%">
         <tr><td style="width:45%">&nbsp;</td>
         <td style="width:55%; text-align:left; Font-Family:Calibri;">Confront Challenges</td></tr></table></td>        
        <td class="BorderColor" id="Conf_Need1"><asp:RadioButton ID="rbConf_Need1" runat="server" GroupName="Confront_Challenge" Enabled="false"/></td>
        <td class="BorderColor" id="Conf_Prof1"><asp:RadioButton ID="rbConf_Prof1" runat="server" GroupName="Confront_Challenge" Enabled="false"/></td>
        <td class="BorderColor" id="Conf_Exce1"><asp:RadioButton ID="rbConf_Exce1" runat="server" GroupName="Confront_Challenge" Enabled="false"/></td></tr>             

<tr style="height:40px; font-size:large">
       <td class="BorderColor" style="text-align:center; font-weight:bold; font-family:Calibri;">LEADING THE ORGANIZATION</td>
       <td class="auto-style2" colspan="3"></td></tr>       

<tr style="height:33px; font-size:large">
               <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%">
         <tr><td style="width:45%">&nbsp;</td>
         <td style="width:55%; text-align:left; Font-Family:Calibri;">Lead Change</td></tr></table></td>
        <td class="BorderColor" id="Lead_Need1"><asp:RadioButton ID="rbLead_Need1" runat="server" GroupName="Lead_Change" Enabled="false"/></td>
        <td class="BorderColor" id="Lead_Prof1"><asp:RadioButton ID="rbLead_Prof1" runat="server" GroupName="Lead_Change" Enabled="false"/></td>
        <td class="BorderColor" id="Lead_Exce1"><asp:RadioButton ID="rbLead_Exce1" runat="server" GroupName="Lead_Change" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
       <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%">
         <tr><td style="width:45%">&nbsp;</td>
         <td style="width:55%; text-align:left; Font-Family:Calibri;">Inspire Risk Taking & innovation</td></tr></table></td>
        <td class="BorderColor" id="Insp_Need1"><asp:RadioButton ID="rbInsp_Need1" runat="server" GroupName="Inspire_Risk" Enabled="false"/></td>
        <td class="BorderColor" id="Insp_Prof1"><asp:RadioButton ID="rbInsp_Prof1" runat="server" GroupName="Inspire_Risk" Enabled="false"/></td>
        <td class="BorderColor" id="Insp_Exce1"><asp:RadioButton ID="rbInsp_Exce1" runat="server" GroupName="Inspire_Risk" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
       <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%">
         <tr><td style="width:45%">&nbsp;</td>
         <td style="width:55%; text-align:left; Font-Family:Calibri;">Leverage External Perspective</td></tr></table></td>
        <td class="BorderColor" id="Leve_Need1"><asp:RadioButton ID="rbLeve_Need1" runat="server" GroupName="Leverage_External" Enabled="false"/></td>
        <td class="BorderColor" id="Leve_Prof1"><asp:RadioButton ID="rbLeve_Prof1" runat="server" GroupName="Leverage_External" Enabled="false"/></td>
        <td class="BorderColor" id="Leve_Exce1"><asp:RadioButton ID="rbLeve_Exce1" runat="server" GroupName="Leverage_External" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
       <td class="BorderColor"><table class="style11" style="height:33px; font-size:large; width:100%">
         <tr><td style="width:45%">&nbsp;</td>
         <td style="width:55%; text-align:left; Font-Family:Calibri;">Communicate for Impact</td></tr></table></td>
        <td class="BorderColor" id="Comm_Need1"><asp:RadioButton ID="rbComm_Need1" runat="server" GroupName="Communic_Impact" Enabled="false"/></td>
        <td class="BorderColor" id="Comm_Prof1"><asp:RadioButton ID="rbComm_Prof1" runat="server" GroupName="Communic_Impact" Enabled="false"/></td>
        <td class="BorderColor" id="Comm_Exce1"><asp:RadioButton ID="rbComm_Exce1" runat="server" GroupName="Communic_Impact" Enabled="false"/></td></tr>


</table>

</asp:Panel>
    </td></tr>
<tr><td style="text-align:center;" colspan="3">&nbsp;</td></tr>
<tr><td style="text-align:center;"" colspan="3"> 
        <asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-family:Calibri; font-weight:bold; cursor: pointer;"
            OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/>
        </td></tr>
     </table> 
    <!--PAGE 3 END-->        
</div>
    </td><td style="width:20px;">&nbsp;</td></tr></table>
 </form>
</body>
</html>
