<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Appraisal1.aspx.vb" Inherits="Appraisal.Appraisal11" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appraisal Print</title>
</head>
<body>
<link href="../../StyleSheet1.css" rel="stylesheet" />
<style type="text/css">
  .style1 {
       text-align:center;
       font-size:x-large;
       font-weight:bold;
       color: #00ae4d;
     }
  .style2 {
       font-family: Calibri;
      
     }
  .style3 {
       text-align: left;
       font-family: Calibri;
      
     }
  .style4 {
       text-align: left;
       font-family: Calibri;
      
     }
  .style5 {
       text-align: right;
       font-family: Calibri;
       
     }
  .style6 {
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
        width:20%;
        background-color:#E7E8E3;
        font-family:Calibri;
        text-align:center; 
          }
    .style10 {
        text-align: center;
        font-size: x-large;
        font-weight: bold;
        color: #00AE4D;
        }
  
        .StyleBreak
         {
        page-break-after: always;
         }
 
     div{
         word-wrap:normal; 
         word-break:normal;
          }
</style>
</head>

<body>

<form id="form2" runat="server">

<asp:Label ID="lblEMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblFIRST_MGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblSECOND_MGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblHR_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblMGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblSAP" runat="server" Visible="false"/>
<asp:Label ID="lblName" runat="server" Visible="false"/>
<asp:Label ID="lblYEAR" runat="server" Visible="false"/>

<table border="0" style="width:100%" >
 <tr><td class="style10"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" /></td></tr>
 <tr><td class="style10"><u>Performance Appraisal (FY<asp:Label ID="FY_Year" runat="server" Text="" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td></tr>
 <tr><td class="style10">&nbsp;&nbsp;</td></tr>   
 <tr><td >

<table style="width:100%">
<tr><td style="width:10%; font-family: Calibri;"><b>Name:</b></td>
     <td style="width:25%;"><asp:label id="lblEmpl_NAME" runat="server" CssClass="Label_StyleSheet"/></td>
     <td style="width:15%;">&nbsp;&nbsp;</td>
     <td style="width:20%;">&nbsp;&nbsp;</td>
     <td style="width:7%; font-family: Calibri;">E-Signed:&nbsp;&nbsp;</td>
     <td><asp:Label ID="LblEMP_Appr" runat="server" Font-Names="Calibri"/></td></tr>
 
    <tr><td><b>Title:</b></td>
     <td><asp:label id="lblEmpl_TITLE" runat="server" Font-Names="Calibri"/></td>
     <td style="width:15%; font-family: Calibri;"><b>Manager Name:&nbsp;</b></td>
     <td><asp:Label ID="LblMGT_NAME" runat="server" Font-Names="Calibri" /></td>
     <td style="font-family: Calibri;">Approved:&nbsp;&nbsp;</td>
     <td><asp:Label ID="LblMGT_Appr" runat="server" Font-Names="Calibri" /></td></tr>
 <tr><td style="font-family: Calibri;"><b>Department:</b></td>
     <td><asp:label id="lblEmpl_DEPT" runat="server" Font-Names="Calibri"/></td>

     <td style="font-family: Calibri;"><b><span id="COLL_MGT" runat="server" visible="false">Former Manager:</span>&nbsp;</b></td>
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

<tr><td >
<!--Accomplishments Waiting_Approval-->
<table border="0">
 <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class="style7">(Summarize accomplishments for the goals established)</span></td></tr>
  </table>
 </td></tr>     
 <tr><td >
<table border="0" style="border-spacing:0; border-collapse:collapse; width:100%">
  <tr><td><div><asp:Label ID="lblAccomplishment"  runat="server" Width="99%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" /></div></td></tr>
<tr><td><hr /></td></tr>
</table></td></tr>
<!--END Accomplishments Waiting_Approval-->
      
<!--Addendum -->  
<tr><td>
  <table border="0" style="width:100%;">
      <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Competencies:</u>&nbsp;<span class="style7">(Check the box that most appropriately describes your direct report's 
            proficiency level in demonstrating CR's Competencies.)</span></td></tr>
<tr><td>
<table border="1" style="border-spacing:0; border-collapse:collapse; width:99%;" runat="server">
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


<!--2. Strengths Waiting_Approval--> 
  <tr><td>
<table border="0" style="width:100%">
  <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class="style7">(Comment on key strengths and highlight areas performed well)</span></td></tr>
  <tr><td><div><asp:Label ID="lblStrengths" runat="server" Width="99%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" /></div></td></tr>
    <tr><td><hr /></td></tr>
</table>
  </td></tr>     
<!--2. END Strengths Waiting_Approval--> 

<!--3. Development Areas Waiting_Approval-->    
  <tr><td >
<table border="0" style="width:100%">
  <tr><td class="style8" >&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;
      <span class="style7">(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>
  <tr><td><div><asp:Label ID="lblDevelopment" runat="server" Width="99%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" /></div></td></tr>
  <tr><td><hr /></td></tr>
</table>
  </td>
    </tr>   
<!--3. END Development Areas Waiting_Approval-->  
    
<!--4. Overall Performance Rating Waiting_Approval-->
  <tr>
      <td >
<table border="0" style="width:99%">
  <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class="style7"> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>
  <tr><td>
      <table border="1" style="border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width:100%">
          <tr><td class="style9"><b>Unsatisfactory</b></td>
              <td class="style9"><b>Developing/Improving Contributor</b></td>        
              <td class="style9"><b>Solid Contributor</b></td>
              <td class="style9"><b>Very Strong Contributor</b></td>
              <td class="style9"><b>Distinguished Contributor</b></td></tr>
          <tr><td style="width:20%;text-align:center;"><asp:RadioButton ID="rbBelow1" runat="server" Enabled="false"/></td>
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbNeed1" runat="server" Enabled="false"/></td>     
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbMeet1" runat="server" Enabled="false"/></td>
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbExceed1" runat="server" Enabled="false"/></td>
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbDisting1" runat="server" Enabled="false"/></td> </tr>
      </table>
</td></tr></table>
      </td>
      </tr>     
<!--5. END Overall Performance Rating Waiting_Approval-->  
<!--4. Overall Summary Waiting approval--> 
  <tr><td>
<table border="0" style="width:100%;">
 <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class="style7">(Comment on overall performance)</span></td></tr>
 <tr><td><div><asp:Label ID="lblOverall_Summary"  runat="server" Width="99%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" /></div></td></tr>
 <tr><td><hr /></td></tr>
</table>
    </td></tr>     
<!--5. END Overall Summary Waiting approval-->          
<tr><td>&nbsp;</td></tr>
<tr><td><asp:Label ID="lblEmployeeComments" runat="server" Width="99%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" Visible="false" /></td></tr>
<tr><td>&nbsp;</td></tr>

<asp:Panel ID="Panel_ResetAppraisal" runat="server" Visible="false">     
    <tr><td style="text-align:center;"><asp:Button ID="ResetAppraisal" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" Text="Recall Appraisal back to Generalist" />
        </td></tr>
</asp:Panel>    

  </table>

    </form>
</body>
</html>
