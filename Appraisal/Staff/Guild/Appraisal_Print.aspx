<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Appraisal_Print.aspx.vb" Inherits="Appraisal.Appraisal_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Guild Appraisal Print</title>

<link href="../../StyleSheet1.css" rel="stylesheet" />

<style type="text/css">
  .style1 {
       text-align:center;
       font-size:x-large;
       font-weight:bold;
       color: #00ae4d;
          }
  .style2 {
        text-align: center;
        font-size: x-large;
        font-weight: bold;
        color: #00AE4D;
          }
  .style3 {
       font-weight: normal;
       font-size:medium;
       color:black;
       font-family:Calibri;
          }
  .style4 {
       font-size:large; 
       font-weight:bold; 
       color: #00ae4d; 
       font-family:Calibri;
          }
  .style5 {
       width:20%;
       background-color:#E7E8E3;
       font-family:Calibri;
       text-align:center; 
          }
   .StyleBreak {
        page-break-after: always;
          }
    </style>
</head>
<body onload="window.print()">

<form id="form1" runat="server">
<asp:Label ID="lblEMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblFIRST_MGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblSECOND_MGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblHR_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblMGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblSAP" runat="server" Visible="false"/>
<asp:Label ID="lblName" runat="server" Visible="false"/>
<asp:Label ID="lblYEAR" runat="server" Visible="false"/>

<table border="0" style="width:100%" >
 <tr><td class="style2"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" /></td></tr>
 <tr><td class="style2"><u>Performance Appraisal (FY<asp:Label ID="FY_Year" runat="server" Text="" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td></tr>
 <tr><td class="style2">&nbsp;&nbsp;</td></tr>   
 <tr><td >

<table style="width:100%">
 <tr><td style="width:7%; font-family: Calibri;"><b>Name:</b></td>
     <td style="width:30%;"><asp:label id="lblEmpl_NAME" runat="server" CssClass="Label_StyleSheet"/></td>
     <td>&nbsp;&nbsp;</td>
     <td>&nbsp;&nbsp;</td>
     <td style="width:5%; font-family: Calibri;">E-Signed:&nbsp;&nbsp;</td>
     <td  style="width:20%;"><asp:Label ID="LblEMP_Appr" runat="server" Font-Names="Calibri"/></td></tr>
 
 <tr><td><b>Title:</b></td>
     <td><asp:label id="lblEmpl_TITLE" runat="server" Font-Names="Calibri"/></td>
     <td style="width:15%; font-family: Calibri;"><b>Manager Name:&nbsp;</b></td>
     <td><asp:Label ID="LblMGT_NAME" runat="server" Font-Names="Calibri" /></td>
     <td style="font-family: Calibri;">Approved:&nbsp;&nbsp;</td>
     <td><asp:Label ID="LblMGT_Appr" runat="server" Font-Names="Calibri" /></td></tr>
 <tr><td style="font-family: Calibri;"><b>Department:</b></td>
     <td><asp:label id="lblEmpl_DEPT" runat="server" Font-Names="Calibri"/></td>
     <td style="font-family: Calibri;"><b>Former Manager:&nbsp;</b></td>
     <td><asp:Label ID="lblCOLL_MGT_NAME" runat="server" Font-Names="Calibri"/></td>
     <td style="font-family: Calibri;">&nbsp;&nbsp;</td><!--Approved:-->
     <td><asp:Label ID="LblUP_MGT_Appr" runat="server" Font-Names="Calibri" Visible="false"/></td></tr>
 <tr><td style="font-family: Calibri;"><b>Hire Date:</b></td>
     <td><asp:label id="lblEmpl_HIRE" runat="server" Font-Names="Calibri"/></td>
     <td style="font-family: Calibri;"><b>Human Resources Generalist:</b></td>
     <td><asp:label id="lblHR_NAME" runat="server" Font-Names="Calibri"/></td>
     <td style="font-family: Calibri;">Approved:&nbsp;&nbsp;</td>
     <td><asp:Label ID="LblHR_Appr" runat="server" Font-Names="Calibri" /></td></tr>
</table>

     </td></tr>
 <tr><td >&nbsp;</td></tr>     

<tr><td>
<!--Accomplishments Waiting_Approval-->
<table border="0">
 <tr><td class="style4">&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class="style3">(Summarize accomplishments vs the goals/expectations established)</span></td></tr>
  </table>
 </td></tr>     
 <tr><td >
<table border="0" style="border-spacing:0; border-collapse:collapse; width:100%">
  <tr><td style="vertical-align:top; font-family:Calibri;" runat="server"><%=txbAccomp1A.Text%><asp:Label ID="txbAccomp1A" runat="server" Visible="false"/></td></tr>
<tr><td><hr /></td></tr>
</table>
<!--END Accomplishments Waiting_Approval-->
 </td></tr>     

<!--2. Strengths Waiting_Approval--> 
  <tr><td >
<table border="0" style="width:100%">
  <tr><td class="style4">&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class="style3">(Comment on key strengths and highlight areas performed well)</span></td></tr>
  <tr><td style="vertical-align:top; font-family:Calibri;"><%=txbStrengthsA.Text%><asp:Label ID="txbStrengthsA" runat="server" Visible="false"/></td></tr>
    <tr><td><hr /></td></tr>
</table>
  </td></tr>     
<!--2. END Strengths Waiting_Approval--> 

<!--3. Development Areas Waiting_Approval-->    
  <tr><td >
<table border="0" style="width:100%">
  <tr><td class="style4" >&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;
      <span class="style3">(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>
  <tr><td style="vertical-align:top; font-family:Calibri;"><%=txbDevelopment_AreasA.Text%><asp:Label ID="txbDevelopment_AreasA" runat="server" Visible="false"/></td></tr>
  <tr><td><hr /></td></tr>
</table>
    </td></tr>     
<!--3. END Development Areas Waiting_Approval-->  


<!--4. Overall Performance Rating Waiting_Approval-->
  <tr><td >
<table border="0" style="width:100%">
  <tr><td class="style4">&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class="style3"> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>
  <tr><td>
      <table border="1" style="border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width:100%">
          <tr><td class="style5"><b>Unsatisfactory</b></td>
              <td class="style5"><b>Developing/Improving Contributor</b></td>        
              <td class="style5"><b>Solid Contributor</b></td>
              <td class="style5"><b>Very Strong Contributor</b></td>
              <td class="style5"><b>Distinguished Contributor</b></td></tr>
          <tr><td style="width:20%;text-align:center;"><asp:RadioButton ID="rbBelow1" runat="server" Enabled="false"/></td>
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbNeed1" runat="server" Enabled="false"/></td>     
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbMeet1" runat="server" Enabled="false"/></td>
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbExceed1" runat="server" Enabled="false"/></td>
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbDisting1" runat="server" Enabled="false"/></td> </tr>
      </table>
</td></tr></table>
      </td></tr>     
<!--4. END Overall Performance Rating Waiting_Approval-->  

<!--5. Overall Summary Waiting approval--> 
  <tr><td>
<table border="0" style="width:100%;">
 <tr><td class="style4">&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class="style3">(Comment on overall performance)</span></td></tr>
 <tr><td style="vertical-align:top; font-family:Calibri;"><%=txbOverall_SumA.Text%><asp:Label ID="txbOverall_SumA" runat="server" Visible="false"/></td></tr>
 <tr><td><hr /></td></tr>
</table>
    </td></tr>     
<!--5. END Overall Summary Waiting approval-->          

<!--6. Development Plan Waiting approval-->
<asp:Panel id="Panel_Development_Plan" runat="server" Visible="false">        
  <tr><td >
<table border="0" style="width:100%;">
    <tr><td class="style4" >&nbsp;&nbsp;&nbsp;<u>Development Plan:</u>&nbsp;<span class="style3">(Based on development areas, summarize a plan for professional and performance development)</span></td></tr>
    <tr><td style="vertical-align:top; font-family:Calibri;"><%=txbDevelopment_ObjectiveA.Text%><asp:Label ID="txbDevelopment_ObjectiveA" runat="server" Visible="false"/></td></tr>
    <tr><td ><hr /></td></tr>
</table>
  </td></tr>   

</asp:Panel>

<!--6. END Development Plan Waiting approval--> 

<!--Divider-->
    <tr><td ><table style="border:gray; font-size:1px; line-height:1px; height:1px; background-color:gray; width:100%;"><tr><td></td></tr></table></td></tr>
<!--END Divider-->
    <tr><td >&nbsp;</td></tr>     
<!--<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>-->
<!--Addendum -->  
  <tr><td >
  <table border="0" style="width:100%;">
    <tr><td class="style1"><u>Addendum:&nbsp;Leadership Competencies</u></td></tr>    <tr><td></td></tr>
    <tr><td class="style3">Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies. 
        Use this as a guide to provide feedback and guide your direct report on areas to focus on for further growth and development.

<table border="1" style="border-spacing:0; border-collapse:collapse; width:100%;" runat="server">
   <tr><td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri;">&nbsp;</td>
        <td style="width:15%; height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; ">Needs Development/ Improvement</td>
        <td style="width:15%; height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; ">Proficient</td>
        <td style="width:15%; height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; ">Excels</td></tr>
   <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading Oneself</span></td></tr>
   <tr><td style="height:33px; font-size:large;">Make Balanced Decisions</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbMake_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbMake_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbMake_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large;">Build Trust</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbBuild2_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbBuild2_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbBuild2_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large">Learn Continuously</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLearn_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLearn_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLearn_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading Other</span></td></tr>
   <tr><td style="height:33px; font-size:large">Lead with Urgency & Purpose</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead2_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead2_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead2_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large">Promote Collaboration & Accountability</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbProm_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbProm_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbProm_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td  style="height:33px; font-size:large">Confront Challenges</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbConf_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbConf_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbConf_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading the Organization</span></td></tr>
   <tr><td style="height:33px; font-size:large">Lead Change</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large">Inspire Risk Taking & Innovation</td>
        <td style="text-align:center;"><asp:RadioButton ID="rbInsp_Need" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbInsp_Prof" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbInsp_Exce" runat="server" Enabled="false"/></td></tr>
    <tr><td style="height:33px; font-size:large">Leverage External Perspective</td>
        <td style="text-align:center;"><asp:RadioButton ID="rbLeve_Need" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbLeve_Prof" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbLeve_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large">Communicate for Impact</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbComm_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbComm_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbComm_Exce" runat="server" Enabled="false"/></td></tr>
 </table>
</td></tr>
 </table>
      </td></tr>
<!--END Addendum --> 
       
   <tr><td >&nbsp;</td></tr>     
<!--Divider-->
   <tr><td >
       <table style="border:gray; font-size:1px; line-height:0px; height:1px; background-color:gray; width:100%;"><tr><td></td></tr></table></td></tr>
<!--END Divider-->
    <!--<tr><td>&nbsp;</td></tr>-->
    <tr><td class="style2"><u>FY<asp:Label ID="Goal_Year3" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> Goal-Setting Form 
        (06/01/<asp:Label ID="Goal_Year4" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> - 05/31/<asp:Label ID="Goal_Year5" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td></tr>
    
    <tr><td >&nbsp;</td></tr>     

<!--Smart Goal Waiting-->
    <tr><td style="font-family:Calibri; font-size:medium;">Enter SMART Goals 
            (<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to 
            focus employees on achieving important, tangible outcomes that contribute to the achievement of department and strategic goals. Summarize the goals agreed to with the employee.
        <div id="divNewtext" runat="server" style="font-size:small; font-style:italic">Please click 
            <a href="https://docs.google.com/presentation/d/1LIm9LOScdiizJcXARapml7-gj2IkJfq-MtQgkBFNHho/edit#slide=id.g1f6e4888fe_0_0" target="_blank">here</a> for a SMART goal example.</div>

        </td></tr>     
    <tr><td>
<table border="1" style="border-collapse:collapse; border-spacing:0; width:100%;">
<tr><td style="background-color:lightgray; width:3%;"></td> 
    <td style="text-align:center; font-size:large; font-weight:bold; width:45%; font-family:Calibri; background-color:lightgray;">Goals
        <div id="divGoalText" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">What do I need to accomplish in order to support my department’s goals and CR’s goals?</div>
    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">
         <div id="divOldTitle" runat="server" >Success Measures or Milestones</div>
        <div id="divNewTitle" runat="server" >Key Results</div>
        <div id="divNewKey" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">How will I know that I’ve accomplished each goal?</div>
    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:12%; font-family:Calibri; background-color:lightgray;">Target<br />Completion Date
        <div id="divNewtarget" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">By when do I need to accomplish each goal?</div>
    </td></tr>
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">1)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal1A.Text%><asp:Label ID="txbGoal1A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess1A.Text%><asp:Label ID="txbSuccess1A" runat="server" Visible="false"/></td>
     <td style="vertical-align:top; font-family:Calibri;"><%=txbDate1A.Text%><asp:Label ID="txbDate1A" runat="server" Visible="false"/></td></tr>
<asp:Panel ID="Panel_Goal2_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">2)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal2A.Text%><asp:Label ID="txbGoal2A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess2A.Text%><asp:Label ID="txbSuccess2A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbDate2A.Text%><asp:Label ID="txbDate2A" runat="server" Visible="false"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal3_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">3)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal3A.Text%><asp:Label ID="txbGoal3A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess3A.Text%><asp:Label ID="txbSuccess3A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbDate3A.Text%><asp:Label ID="txbDate3A" runat="server" Visible="false"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal4_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">4)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal4A.Text%><asp:Label ID="txbGoal4A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess4A.Text%><asp:Label ID="txbSuccess4A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbDate4A.Text%><asp:Label ID="txbDate4A" runat="server" Visible="false"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal5_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">5)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal5A.Text%><asp:Label ID="txbGoal5A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess5A.Text%><asp:Label ID="txbSuccess5A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbDate5A.Text%><asp:Label ID="txbDate5A" runat="server" Visible="false"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal6_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">6)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal6A.Text%><asp:Label ID="txbGoal6A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess6A.Text%><asp:Label ID="txbSuccess6A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbDate6A.Text%><asp:Label ID="txbDate6A" runat="server" Visible="false"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal7_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">7)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal7A.Text%><asp:Label ID="txbGoal7A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess7A.Text%><asp:Label ID="txbSuccess7A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbDate7A.Text%><asp:Label ID="txbDate7A" runat="server" Visible="false"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal8_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">8)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal8A.Text%><asp:Label ID="txbGoal8A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess8A.Text%><asp:Label ID="txbSuccess8A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbDate8A.Text%><asp:Label ID="txbDate8A" runat="server" Visible="false"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal9_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">9)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal9A.Text%><asp:Label ID="txbGoal9A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess9A.Text%><asp:Label ID="txbSuccess9A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbDate9A.Text%><asp:Label ID="txbDate9A" runat="server" Visible="false"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal10_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">10)</td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbGoal10A.Text%><asp:Label ID="txbGoal10A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbSuccess10A.Text%><asp:Label ID="txbSuccess10A" runat="server" Visible="false"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><%=txbDate10A.Text%><asp:Label ID="txbDate10A" runat="server" Visible="false"/></td></tr></asp:Panel>
</table>
       </td></tr>     
<!--END Smart Goal Waiting-->
<tr><td>&nbsp;</td></tr>
<tr><td><asp:Label ID="lblEmployeeComments" runat="server" Visible="false" /></td></tr>

 </table>

    </form>
</body>
</html>
