<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Appraisal.aspx.vb" Inherits="Appraisal.Appraisal1" MaintainScrollPositionOnPostback="true" smartNavigation="true" ValidateRequest="false"%>

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


<link href="../../StyleSheet1.css" rel="stylesheet" />

<style type="text/css">
    .style0 {    
       width:100%;  
          }
  .style1 {
       text-align:center;
       font-size:x-large;
       font-weight:bold;
       color: #00ae4d;
       width:96%;
          }
 .style2 {
       width: 25%;
       font-family: Calibri;
          }
  .style3 {
       width: 8%;
       text-align: left;
       font-family: Calibri;
       text-wrap:none;
          }
  .style4 {
       width: 14%;
       text-align: left;
       font-family: Calibri;
       text-wrap:none;
           }
  .style5 {
       width: 8%;
       text-align: right;
       font-family: Calibri;
       text-wrap:none;
           }
  .style6 {
       width: 1%;
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
       table.Fixed {
          table-layout: fixed;
          width: 100%;    
          border-collapse: collapse;
          border-color : lightgray;
          }
        TD.WrapStyle {
          word-break: break-all;
          font-family:Calibri;
          font-size:small;
          }
        div{
         word-wrap:normal; 
         word-break:normal;
           }
 </style>

<style media=print>
 .hide_print {
     display: none;
       }
 </style>

<script type="text/javascript">

    function SetButtonStatus(sender, target) {
        if (sender.value.length >= 2)
            document.getElementById(target).disabled = true;
        else
            document.getElementById(target).disabled = false;
    }
//========================================================================
  
</script>

</head>
<body>
    
<form id="form1" runat="server">
    
<asp:Label ID="lblEMPLID" runat="server" Visible="false"/>
<asp:label id="lblLogin_EMPLID" runat="server" Visible="false"/><!--ForeColor="white" BackColor="White"-->
<asp:Label ID="lblFIRST_MGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblSECOND_MGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblHR_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblMGT_EMPLID" runat="server" Visible="false"/>
<asp:Label ID="lblSAP" runat="server" Visible="false"/>
<asp:Label ID="lblName" runat="server" Visible="false"/>   
<asp:Label ID="lblYEAR" runat="server" Visible="false"/>
<asp:Label ID="lblEmpl_Email" runat="server" Visible="false"/>
<asp:Label ID="lblMGT_Email" runat="server" Visible="false"/>
<asp:Label ID="lblUP_MGT_Email" runat="server" Visible="false"/>
<asp:Label ID="lblHR_Email" runat="server" Visible="false"/>
<asp:Label ID="lblProcess_Flag" runat="server" Visible="true" style="display:none"/>
<asp:label id="lblWindowBatch" runat="server" Visible="true" style="display:none"/>
<asp:label id="lblDataBaseBatch" runat="server" Visible="true" style="display:none"/>

<table border="0">
    <tr><td class="style6">&nbsp;&nbsp;</td>
        <td class="style1"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td>
        <td class="style6">&nbsp;&nbsp;</td></tr>
    <tr><td class="style6">&nbsp;&nbsp;</td>
        <td class="style1"><u>Performance Appraisal (FY<asp:Label ID="FY_Year" runat="server" Text="" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td>
        <td class="style6">
             &nbsp;</td></tr>
    <tr><td class="style6">&nbsp;&nbsp;</td>
        <td class="style1"></td>
        <td class="style6">&nbsp;&nbsp;</td></tr>
    <tr><td class="style6">&nbsp;&nbsp;</td>
        <td>
            <table border="0">
               <tr><td class="style3"><b>Name:</b></td>
                   <td class="style2"><asp:label id="lblEmpl_NAME" runat="server" CssClass="Label_StyleSheet" />
                   <td class="style4">&nbsp;&nbsp;</td>
                   <td class="style2">&nbsp;&nbsp;</td>
                   <td class="style5"><b>E-Signed:</b>&nbsp;&nbsp;</td>
                   <td><asp:Label ID="LblEMP_Appr" runat="server" Font-Names="Calibri"/></td></tr>
               <tr><td class="style3"><b>Title:</b></td>
                   <td class="style2"><asp:label id="lblEmpl_TITLE" runat="server" Font-Names="Calibri"/></td>
                   <td class="style4"><b>Manager Name:&nbsp;</b></td>
                   <td class="style2"><asp:Label ID="LblMGT_NAME" runat="server" Font-Names="Calibri" /></td>
                   <td class="style5"><b>Approved:</b>&nbsp;&nbsp;</td>
                   <td><asp:Label ID="LblMGT_Appr" runat="server" Font-Names="Calibri" /></td></tr>
               <tr><td class="style3"><b>Department:</b></td>
                   <td class="style2"><asp:label id="lblEmpl_DEPT" runat="server" Font-Names="Calibri"/></td>
                   <td class="style4"><b><span id="COLL_MGT" runat="server" visible="false">Former Manager:</span>&nbsp;</b></td>
                   <td class="style2"><asp:Label ID="lblCOLL_MGT_NAME" runat="server" Font-Names="Calibri"/></td>
                   <td class="style5">&nbsp;&nbsp;</td>
                   <td><asp:Label ID="LblUP_MGT_Appr" runat="server" Font-Names="Calibri" Visible="false" /></td></tr>                       
               <tr><td class="style3"><b>Hire Date:</b></td>
                   <td class="style2"><asp:label id="lblEmpl_HIRE" runat="server" Font-Names="Calibri"/></td>
                   <td class="style4"><b>Human Resources Generalist:</b></td>
                   <td class="style2"><asp:label id="lblHR_NAME" runat="server" Font-Names="Calibri"/></td>
                   <td class="style5"><b>Reviewed:</b>&nbsp;&nbsp;</td>
                   <td><asp:Label ID="LblHR_Appr" runat="server" Font-Names="Calibri"/></td></tr></table></td>
      <td class="style6">&nbsp;&nbsp;</td></tr>
  <tr><td class="style6">&nbsp;&nbsp;</td>
      <td>

<!--All Chapters are Going here-->

<table border="0" class="Fixed">

<!--Accomplishments -->
    <tr><td class="style8"><div>&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class="style7">(Summarize accomplishments for the rating criteria established)</span></div></td></tr>
    <tr><td><div><asp:Label ID="lblAccomplishment" runat="server" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small"/></div></td></tr>
    <tr><td><hr /></td></tr>

<!--Competencies -->     
    
<tr><td class="style8"><div>&nbsp;&nbsp;&nbsp;<u>Competencies:</u>&nbsp;<span class="style7" style="word-wrap: normal; word-break: normal;">(Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Competencies.)</span></div></td></tr>    
    <tr><td>
<table border="1" style="border-spacing:0; border-collapse:collapse; width:100%;" runat="server">
   <tr><td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri;">&nbsp;</td>
        <td style="width:15%; height: 30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; word-wrap: normal; word-break: normal;">Needs Development/Improvement</td>
        <td style="width:15%; height: 30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; word-wrap: normal; word-break: normal;">Proficient</td>
        <td style="width:15%; height: 30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; word-wrap: normal; word-break: normal;">Excels</td></tr>
   <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white; word-wrap: normal; word-break: normal;">Leading Oneself</span></td></tr>
   <tr><td style="height:33px; font-size:large; word-wrap: normal; word-break: normal;">Make Balanced Decisions</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbMake_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbMake_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbMake_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large; word-wrap: normal; word-break: normal;">Build Trust</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbBuild2_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbBuild2_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbBuild2_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large; word-wrap: normal; word-break: normal;">Learn Continuously</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLearn_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLearn_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLearn_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading Other</span></td></tr>
   <tr><td style="height:33px; font-size:large; word-wrap: normal; word-break: normal;">Lead with Urgency & Purpose</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead2_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead2_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead2_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large; word-wrap: normal; word-break: normal; word-wrap: normal; word-break: normal;">Promote Collaboration & Accountability</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbProm_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbProm_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbProm_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td  style="height:33px; font-size:large; word-wrap: normal; word-break: normal; word-wrap: normal; word-break: normal;">Confront Challenges</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbConf_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbConf_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbConf_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white; word-wrap: normal; word-break: normal;">Leading the Organization</span></td></tr>
   <tr><td style="height:33px; font-size:large; word-wrap: normal; word-break: normal;">Lead Change</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large; word-wrap: normal; word-break: normal;">Inspire Risk Taking & Innovation</td>
        <td style="text-align:center;"><asp:RadioButton ID="rbInsp_Need" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbInsp_Prof" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbInsp_Exce" runat="server" Enabled="false"/></td></tr>
    <tr><td style="height:33px; font-size:large; word-wrap: normal; word-break: normal;">Leverage External Perspective</td>
        <td style="text-align:center;"><asp:RadioButton ID="rbLeve_Need" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbLeve_Prof" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbLeve_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:33px; font-size:large; word-wrap: normal; word-break: normal;">Communicate for Impact</td>
       <td style="text-align:center;"><asp:RadioButton ID="rbComm_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbComm_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbComm_Exce" runat="server" Enabled="false"/></td></tr>
 </table>    

    <tr><td><hr /></td></tr>
<!--Strengths--> 
    <tr><td class="style8"><div>&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class="style7">(Comment on key strengths and highlight areas performed well)</span></div></td></tr>
    <tr><td><div><asp:Label ID="lblStrengths" runat="server" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small"/></div>
        </td></tr>
    <tr><td><hr /></td></tr>

<!--Development_Area--> 
    <tr><td class="style8"><div>&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp; <span class="style7" style="word-wrap: normal; word-break: normal;">(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></div></td></tr>
    <tr><td><div><asp:Label ID="lblDevelopment" runat="server" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" /></div></td></tr>
    <tr><td><hr /></td></tr>

<!--Overall Performance Rating-->
    <tr><td class="style8" style="word-wrap: normal; word-break: normal;"><div>&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class="style7"> (Check the box that most appropriately describes the individual&#39;s overall performance)</span></div></td></tr>
    <tr><td>
<table border="1" style="border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0; width:100%">
          <tr><td class="style9" style="word-wrap: normal; word-break: normal;"><b>Unsatisfactory</b></td>
              <td class="style9" style="word-wrap: normal; word-break: normal;"><b>Developing/Improving Contributor</b></td>        
              <td class="style9" style="word-wrap: normal; word-break: normal;"><b>Solid Contributor</b></td>
              <td class="style9" style="word-wrap: normal; word-break: normal;"><b>Very Strong Contributor</b></td>
              <td class="style9" style="word-wrap: normal; word-break: normal;"><b>Distinguished Contributor</b></td></tr>
          <tr><td style="width:20%;text-align:center;"><asp:RadioButton ID="rbBelow1" runat="server" Enabled="false"/></td>
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbNeed1" runat="server" Enabled="false"/></td>     
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbMeet1" runat="server" Enabled="false"/></td>
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbExceed1" runat="server" Enabled="false"/></td>
              <td style="width:20%; text-align:center;"><asp:RadioButton ID="rbDisting1" runat="server" Enabled="false"/></td> </tr>
      </table>
        </td></tr>
    <tr><td><hr /></td></tr>
<!--5. Overall Summary-->
    <tr><td class="style8"><div>&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class="style7">(Comment on overall performance)</span></div></td></tr>
    <tr><td><div><asp:Label ID="lblOverall_Summary" runat="server" Width="100%" BorderStyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" /></div></td></tr>
    <tr><td><hr /></td></tr>
</td></tr>
</table>

        </td><td class="style6">&nbsp;&nbsp;</td></tr>
    
    <tr><td class="style6">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td class="style6">&nbsp;&nbsp;</td></tr>
    <tr><td class="style6">&nbsp;&nbsp;</td><td>        

<asp:Panel ID="Panel_Employee_Review" runat="server">
  <tr><td class="style6">&nbsp;</td><td colspan="6">
        <table style="width:100%">
            <tr id="trPressing" runat="server"><td style="text-align:center; font-family:Calibri; font-size:medium; font-weight:bold; word-wrap: normal; word-break: normal;">Pressing the "E-Sign" button means that I have reviewed and discussed this appraisal with my immediate supervisor.</td></tr>
            <tr id="trAgree" runat="server"><td style="text-align:center; font-family:Calibri; font-size:medium; font-weight:bold; word-wrap: normal; word-break: normal;">It does not necessarily imply that I agree with this evaluation.</td></tr>
            <tr id="trComments" runat="server">
                <td style="text-align:left; font-family:Calibri; font-size:medium; font-weight:bold;">Employee's Comments: <font style="font-size:small; color:lightgray">(optional)</font></td></tr>
            <tr><td><asp:TextBox ID="txbEmployeeComments" runat="server" Width="99%" TextMode="MultiLine" Borderstyle="Solid" BorderColor="LightGray" Rows="8" CssClass="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled"/>
                <div><asp:Label ID="lblEmployeeComments" runat="server" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" ></asp:Label></div>
    </td></tr>
            <tr><td style="text-align:center;"><span class="hide_print"><asp:Button ID="btnSubmit" runat="server" Text="E-Sign" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" width="120px" CssClass="Label_StyleSheet" /></span></td></tr>
            <tr><td>&nbsp;&nbsp;&nbsp;</td></tr>
            <tr><td style="text-align:center;">
                    <span class="hide_print"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
                                                         CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;" /></span></td></tr>
            <tr><td>&nbsp;&nbsp;&nbsp;</td></tr>
            <tr><td style="text-align:center;"><span class="hide_print"><asp:ImageButton ID="Img_Print1" runat="server" ImageUrl="../../Images/print.png" Style="width:70px;" />
        </span></td></tr>
        </table>
      </td><td class="style6">&nbsp;</td></tr>   


</asp:Panel>

</td><td class="style6">&nbsp;&nbsp;</td></tr>

</td></tr>

<tr><td class="style6">&nbsp;&nbsp;</td>
    <td class="style1">&nbsp;&nbsp;</td>
    <td class="style6">&nbsp;&nbsp;</td></tr>

</table>


    </form>
</body>
</html>
