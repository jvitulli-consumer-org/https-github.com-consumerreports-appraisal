<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Appraisal.aspx.vb" Inherits="Appraisal.Exempt_Appraisal" MaintainScrollPositionOnPostback="true" smartNavigation="true" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=lblEmpl_NAME.Text%>`s Appraisal</title>

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
  .style11 {
       width: 10%;
       text-align: right;
       font-family: Calibri;
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
    function ChangeColor(i) {
        // Retrieve the element by its id 'i'
        document.getElementById(i).style.backgroundColor = "";
    }
    //========================================================================
    function SetButtonStatus(sender, target) {
        if (sender.value.length >= 2)
            document.getElementById(target).disabled = true;
        else
            document.getElementById(target).disabled = false;
    }
    //========================================================================
    window.onload = function () {
        var seconds = 3;
        setTimeout(function () {
            document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            document.getElementById("<%=lblMessage1.ClientID %>").style.display = "none";
        }, seconds * 1000);

};
</script>

</head>
<body>

<form id="form1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:Timer ID="Timer1" runat="server" Interval="30000" ontick="Timer1_Tick"></asp:Timer>  
<asp:Timer ID="Timer2" runat="server" Interval="5000" ontick="Timer2_Tick"></asp:Timer> 
    

<asp:Label ID="lblEMPLID" runat="server" Visible="false"/>
<asp:label id="lblLogin_EMPLID" runat="server" Text="" Visible="true" ForeColor="white" BackColor="White" />
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
<asp:Label ID="lblProcess_Flag" runat="server" Visible="false"/>
<asp:label id="lblWindowBatch" runat="server" Text="" Visible="true" style="display:none"/><!--style="display:none"-->
<asp:label id="lblDataBaseBatch" runat="server" Text="" Visible="true" style="display:none"/>

<table border="0" class="style0">
 <tr><td class="style6">&nbsp;&nbsp;</td>
     <td class="style1" colspan="6"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td>
     <td class="style6">&nbsp;&nbsp;</td></tr>
 <tr><td class="style6">
         <asp:Button ID="lblMessage1" runat="server" ForeColor="Green" BackColor="White" BorderStyle="None" Font-Bold="true" Font-Size="12px" Width="100px" Text=""  CssClass="Button_StyleSheet;" style="position:fixed;left:70px;"/><br />
         <asp:Button ID="SaveRecords1" runat="server" Text="Save Records" BackColor="Wheat" BorderStyle="None" Width="100px" Font-Size="11pt" CssClass="Button_StyleSheet" style="position:fixed;left:70px;"/></td>
     <td class="style1" colspan="6"><u>Performance Appraisal (FY<asp:Label ID="FY_Year" runat="server" Text="" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td>
     <td class="style6"><asp:ImageButton ID="Img_Print1" runat="server" ImageUrl="../../Images/print.png" Style="width:70px"/></td></tr>
 <tr><td class="style6">&nbsp;&nbsp;</td><td class="style1" colspan="6">&nbsp;&nbsp;</td><td class="style6">&nbsp;&nbsp;</td></tr>   

 <tr><td class="style6">&nbsp;&nbsp;</td>
     <td class="style3"><b>Name:</b></td>
     <td class="style2"><asp:label id="lblEmpl_NAME" runat="server" CssClass="Label_StyleSheet" /></td>
     <td class="style4">&nbsp;&nbsp;</td>
     <td class="style2">&nbsp;&nbsp;</td>
     <td class="style5">E-Signed:&nbsp;&nbsp;</td>
     <td><asp:Label ID="LblEMP_Appr" runat="server" Font-Names="Calibri"/></td>
     <td class="style6">&nbsp;&nbsp;</td></tr>
 
 <tr><td class="style6">&nbsp; &nbsp;</td>
     <td class="style3"><b>Title:</b></td>
     <td class="style2"><asp:label id="lblEmpl_TITLE" runat="server" Font-Names="Calibri"/></td>
     <td class="style4"><b>Manager Name:&nbsp;</b></td>
     <td class="style2"><asp:Label ID="LblMGT_NAME" runat="server" Font-Names="Calibri" /></td>
     <td class="style5">Approved:&nbsp;&nbsp;</td>
     <td><asp:Label ID="LblMGT_Appr" runat="server" Font-Names="Calibri" /></td>
     <td class="style6">&nbsp; &nbsp;</td></tr>
 <tr><td class="style6">&nbsp; &nbsp;</td>
     <td class="style3"><b>Department:</b></td>
     <td class="style2"><asp:label id="lblEmpl_DEPT" runat="server" Font-Names="Calibri"/></td>
     
     <td class="style4"><b>Former Manager:&nbsp;</b></td>
     <td class="style2"><asp:Label ID="lblCOLL_MGT_NAME" runat="server" Font-Names="Calibri"/></td>
     
     <td class="style5">&nbsp;&nbsp;</td><!--Approved:-->
     <td><asp:Label ID="LblUP_MGT_Appr" runat="server" Font-Names="Calibri" Visible="false" /></td>
     <td class="style6">&nbsp; &nbsp;</td></tr>
 
 <tr><td class="style6">&nbsp; &nbsp;</td>
     <td class="style3"><b>Hire Date:</b></td>
     <td class="style2"><asp:label id="lblEmpl_HIRE" runat="server" Font-Names="Calibri"/></td>
     <td class="style4"><b>Human Resources Generalist:</b></td>
     <td class="style2"><asp:label id="lblHR_NAME" runat="server" Font-Names="Calibri"/></td>
     <td class="style5">Approved:&nbsp;&nbsp;</td>
     <td><asp:Label ID="LblHR_Appr" runat="server" Font-Names="Calibri" /></td>
     <td class="style6">&nbsp;&nbsp;</td></tr>

<!--<tr><td class="style6">&nbsp;</td><td colspan="6">&nbsp;</td><td class="style6">&nbsp;</td></tr>-->
 
<tr><td class="style6">&nbsp;</td>

     <td colspan="4"><asp:Label ID="lblDiscuss" runat="server" CssClass="Label_StyleSheet"/></td>
     
    <td colspan="2" style="text-align:right;">
</td>
    <td class="style6">&nbsp;&nbsp;&nbsp;</td></tr>     

<asp:Panel ID="Panel_Create_Appraisal" Visible="false" runat="server">

<asp:UpdatePanel ID="UpdatePanel_AllData" runat="server" UpdateMode="Conditional">
<ContentTemplate>
</ContentTemplate>
    <triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/>
    </triggers>
    </asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
  </ContentTemplate>
    <triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick"/>
    </triggers>
</asp:UpdatePanel>

<tr><td class="style6">&nbsp;</td>
     <td colspan="6">
<!--1. Accomplishments -->
<table border="0" style="width:100%; vertical-align:top;">
  <tr><td class="style8" colspan="3">&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class="style7">(Summarize accomplishments for the rating criteria established)</span></td></tr>
<!--<tr><td colspan="3"><asp:Button ID="BtnNew_Accomp" runat="server" BackColor="Wheat" BorderStyle="None" Font-Names="Calibri" Font-Size="11pt" Text="Add New Record" width="100px" OnClick="BtnNew_Accomp_Click"/></td></tr>--></table>
 
<table border="0" style="width:100%; vertical-align:top;">        
  <tr><!--<td style="width:20px; vertical-align:top;"><asp:Button ID="Button2" Text="X" ForeColor="white" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #2"/></td>
      <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">1)</td>-->
      <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp1" runat="server" UpdateMode="Conditional"><ContentTemplate>
          <asp:TextBox ID="txbAccomp1" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" 
              Style="overflow:auto;" TabIndex="1" ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments1_TextChanged" onkeyup="ChangeColor(id);" />
</ContentTemplate></asp:UpdatePanel>
      </td></tr></table>

<asp:Panel ID="Panel_Accomp2" Visible="false" runat="server">
<table border="0" style="width:100%; vertical-align:top;">
  <tr><td style="width:20px; vertical-align:top;"><asp:Button ID="Delete_Accom2" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #2"/></td>
      <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">2)</td>
      <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp2" runat="server" UpdateMode="Conditional"><ContentTemplate>
          <asp:TextBox ID="txbAccomp2" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="2"
               ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments2_TextChanged" onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel>

<asp:Panel ID="Panel_Accomp3" Visible="false" runat="server">
<table border="0" style="width:100%; vertical-align:top;">
  <tr><td style="width:20px; vertical-align:top;"><asp:Button ID="Delete_Accom3" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #3"/></td>
      <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">3)</td>
      <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp3" runat="server" UpdateMode="Conditional"><ContentTemplate>
          <asp:TextBox ID="txbAccomp3" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="3"
               ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments3_TextChanged"  onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel>

<asp:Panel ID="Panel_Accomp4" Visible="false" runat="server">
<table border="0" style="width:100%; vertical-align:top;">
  <tr><td style="width:20px; vertical-align:top;"><asp:Button ID="Delete_Accom4" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #4"/></td>
      <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">4)</td>
      <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp4" runat="server" UpdateMode="Conditional"><ContentTemplate>
         <asp:TextBox ID="txbAccomp4" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="4"
               ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments4_TextChanged" onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel>

<asp:Panel ID="Panel_Accomp5" Visible="false" runat="server">
<table border="0" style="width:100%; vertical-align:top;">
  <tr><td style="width:20px; vertical-align:top;"><asp:Button ID="Delete_Accom5" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #5"/></td>
      <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">5)</td>
      <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp5" runat="server" UpdateMode="Conditional"><ContentTemplate>
          <asp:TextBox ID="txbAccomp5" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="5"
                ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments5_TextChanged" onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel>

<asp:Panel ID="Panel_Accomp6" Visible="false" runat="server">      
<table border="0" style="width:100%; vertical-align:top;">
  <tr><td style="width:20px; vertical-align:top;"><asp:Button ID="Delete_Accom6" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #6"/></td>
      <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">6)</td>
      <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp6" runat="server" UpdateMode="Conditional"><ContentTemplate>
          <asp:TextBox ID="txbAccomp6" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="6" 
               ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments6_TextChanged" onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel>

<asp:Panel ID="Panel_Accomp7" Visible="false" runat="server">
<table border="0" style="width:100%; vertical-align:top;">
  <tr><td style="width:20px; vertical-align:top;"><asp:Button ID="Delete_Accom7" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #7"/></td>
      <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">7)</td>
      <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp7" runat="server" UpdateMode="Conditional"><ContentTemplate>
          <asp:TextBox ID="txbAccomp7" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="7"
               ValidateRequestMode="Disabled"  AutoPostBack="true" OnTextChanged="Accomplishments7_TextChanged" onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel>

<asp:Panel ID="Panel_Accomp8" Visible="false" runat="server">
<table border="0" style="width:100%; vertical-align:top;">
    <tr><td style="width:20px; vertical-align:top;"><asp:Button ID="Delete_Accom8" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #8"/></td>
        <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">8)</td>
        <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp8" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:TextBox ID="txbAccomp8" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="8"
                 ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments8_TextChanged" onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel>

<asp:Panel ID="Panel_Accomp9" Visible="false" runat="server">
<table border="0" style="width:100%; vertical-align:top;">
    <tr><td style="width:20px; vertical-align:top;"><asp:Button ID="Delete_Accom9" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #9"/></td>
        <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">9)</td>
        <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp9" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:TextBox ID="txbAccomp9" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="9"
                 ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments9_TextChanged" onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel>

<asp:Panel ID="Panel_Accomp10" Visible="false" runat="server">
<table border="0" style="width:100%; vertical-align:top;">
    <tr><td style="width:20px; vertical-align:top;"><asp:Button ID="Delete_Accom10" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" ToolTip="Delete Accomplishment #10"/></td>
        <td style="width:20px; vertical-align:top; text-align:right; font-weight:bold;">10)</td>
        <td>
<asp:UpdatePanel ID="UpdatePanel_Accomp10" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:TextBox ID="txbAccomp10" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="10"
                 ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments10_TextChanged" onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel></td></tr></table></asp:Panel>

     </td>
     <td class="style6">&nbsp;</td></tr>
<!--1. END Accomplishments -->

<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->
<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->

<!--2. Strengths--> 
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
<table border="0" style="width:100%">
    <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class="style7">(Comment on key strengths and highlight areas performed well)</span></td></tr>
       <tr><td>
<asp:UpdatePanel ID="UpdatePanel_Strengths" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:TextBox ID="txbStrengths" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="11"
                    ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Strengths_TextChanged" onkeyup="ChangeColor(id);"/>
</ContentTemplate></asp:UpdatePanel>
      </td></tr>
</table>
    </td>
    <td class="style6">&nbsp;</td></tr>     
<!--2. END Strengths-->

<!--3. Development Areas-->    
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
          <table border="0" style="width:100%">
    <tr><td class="style8" >&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp; 
        <span class="style7">(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>
       <tr><td>
<asp:UpdatePanel ID="UpdatePanel_Development_Area" runat="server" UpdateMode="Conditional">
<ContentTemplate>
           <asp:TextBox ID="txbDevelopment_Areas" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="12"
                    ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Development_Areas_TextChanged" onkeyup="ChangeColor(id);" />
</ContentTemplate>
    </asp:UpdatePanel>
      </td></tr>
</table>

  </td>
  <td class="style6">&nbsp;</td></tr>   
<!--3. END Development Areas--> 
            
<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr> -->

<!--4. Overall Performance Rating -->
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
<table border="0" style="width:99%">
  <tr><td colspan="5" class="style8">&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class="style7"> (Check the box that most appropriately describes the individual&#39;s overall performance)</span></td></tr>
  <tr><td>
<table border="1" style="width:100%; border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0;" runat="server">
  <tr><td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
          <u><b>Unsatisfactory</b></u><br/><br/>This employee is not performing to the requirements of the position. Performance must improve significantly within a reasonable time period if the 
          individual is to remain in the position.<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;Note: Employees receiving this rating should be on a performance improvement plan.</span></a>
           &nbsp;<b>Unsatisfactory</b>&nbsp;&nbsp;</td>
        <td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Developing/Improving Contributor</b></u><br /><br />This employee is still learning the essential functions of the position, or is improving toward effective performance of all essential functions. 
            Continued development or improvement is required.<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;Note: Employees new to their positions should receive this rating if they are still developing and growing into the 
            role, as should those who are struggling with new responsibilities. Employees should not receive this rating in consecutive years. Employees continuing to require significant development a year after 
            receiving this rating should  be rated as “underperforming”.</span></a>
            &nbsp;&nbsp;<b>Developing/Improving Contributor</b>&nbsp;&nbsp;</td>        
       
         <td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
             <u><b>Solid Contributor</b></u><br /><br />
             Performance is considered solid, although there may be areas in which the employee should further develop.</span></a>
             &nbsp;&nbsp;<b>Solid Contributor</b>&nbsp;&nbsp</td>
       
         <td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
             <u><b>Very Strong Contributor</b></u><br /><br />This employee sustains a consistently high level of performance, frequently exceeds the requirements and expectations of the position, 
             and produces high quality work on a consistent basis. S/he makes an important contribution to enhancing organizational performance and impact.</span></a>
             &nbsp;&nbsp;<b>Very Strong Contributor</b>&nbsp;&nbsp</td>
        
        <td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Distinguished Contributor</b></u><br /><br />This employee had an extraordinary year of exceptional performance and accomplishments. S/he stands out by demonstrating remarkable 
            leadership and effecting measurable, lasting improvements in organizational performance and impact.<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;Note: Employees should generally not expect to be 
            rated as a distinguished contributor in consecutive years.</span></a>
            &nbsp;&nbsp;<b>Distinguished Contributor</b>&nbsp;&nbsp</td>
    </tr>
    
    <tr id="trOverall_Performance">
        <td id="tdBelow" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Rating_Below" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:RadioButton ID="rbBelow" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/>
</ContentTemplate></asp:UpdatePanel>
        </td>
        <td id="tdNeed" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Rating_Need" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:RadioButton ID="rbNeed" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/>
</ContentTemplate></asp:UpdatePanel>
        </td>     
        <td id="tdMeet" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Rating_Meet" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:RadioButton ID="rbMeet" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/>
</ContentTemplate></asp:UpdatePanel>
        </td>
        <td id="tdExceed" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Rating_Exceed" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:RadioButton ID="rbExceed" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/>
</ContentTemplate></asp:UpdatePanel>
         </td>
         <td id="tdDisting"  style="text-align:center;">  
<asp:UpdatePanel ID="UpdatePanel_Rating_Disting" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:RadioButton ID="rbDisting" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/>
</ContentTemplate></asp:UpdatePanel>
         </td> 
    </tr>
</table>

</td></tr></table>
    
      </td>
      <td class="style6">&nbsp;</td></tr>     
<!--4. END Overall Performance Rating -->  

<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->

<!--5. Overall Summary-->
<tr><td class="style6">&nbsp;</td>
    <td colspan="6">
<table border="0" style="width:100%">
    <tr><td class="style8" >&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp; 
        <span class="style7">(Comment on overall performance)</span></td></tr>
       <tr><td>
<asp:UpdatePanel ID="UpdatePanel_Overall_Summary" runat="server" UpdateMode="Conditional">
<ContentTemplate>
           <asp:TextBox ID="txbOverall_Sum" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="13"
                    ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Overall_Sum_TextChanged" onkeyup="ChangeColor(id);" />
</ContentTemplate>
    </asp:UpdatePanel>           
      </td></tr>
</table>

    </td>
    <td class="style6">&nbsp;</td></tr>     
<!--5. END Overall Summary-->
<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->

<!--6. Development Plan-->
<asp:Panel id="Panel_Development_Plan" runat="server" Visible="false">
<tr><td class="style6">&nbsp;</td>
    <td colspan="6">
<table border="0" style="width:100%">
    <tr><td class="style8" >&nbsp;&nbsp;&nbsp;<u>Development Plan:</u>&nbsp;<span class="style7">(Based on development areas, summarize a plan for professional and performance development)</span></td></tr>
       <tr><td>
<asp:UpdatePanel ID="UpdatePanel_Development_Objective" runat="server" UpdateMode="Conditional">
<ContentTemplate>
           <asp:TextBox ID="txbDevelopment_Objective" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="14"
                    ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Development_Objective_TextChanged"  onkeyup="ChangeColor(id);" />
</ContentTemplate>
    </asp:UpdatePanel>           
      </td></tr>
</table>
    </td>
    <td class="style6">&nbsp;</td></tr>     
</asp:Panel>
<!--6. END Development Plan-->

<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->

<!--Divider-->
    <tr><td class="style6">&nbsp;</td><td colspan="6"><table style="width:100%; border:solid 1px lightgray; font-size:0px; line-height:0px; height:1px; background-color: gray;"><tr><td></td></tr></table></td><td class="style6">&nbsp;</td></tr>
<!--END Divider-->
<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->

<!--Addendum -->  
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
 <table border="0" style="width:99%" tabindex="15">
    <tr><td class="style1"><u>Addendum:&nbsp;Leadership Competencies</u></td></tr>
    <tr><td></td></tr>
    <tr><td class="style7">Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies. 
        Use this as a guide to provide feedback and guide your direct report on areas to focus on for further growth and development.

<table border="1" style="width:99%; border-collapse:collapse; border-spacing:0;" runat="server">
    
    <tr><td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri;">&nbsp;</td>
        <td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; width:20%;">Needs Development/Improvement</td>
        <td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; width:20%;">Proficient</td>
        <td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; width:20%;">Excels</td></tr>
    <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading Oneself</span></td></tr>

    <tr id="trMake_Balanced" style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Make Balanced Decisions</b></u><br /><br />
            o Ensures decisions are data-driven, fact-based, and consumer-focused<br /><br />
            o Is comfortable making decisions with incomplete data and adjusting as new information comes to light<br /><br />
            o Synthesizes relevant data, understands interdependencies, and anticipates the impact on individuals, teams, the enterprise, and consumers <br /><br />
            o Encourages open debate and expects support and commitment once discussions are concluded</span></a>
            &nbsp;&nbsp;Make Balanced Decisions&nbsp;&nbsp;</td></tr></table></td>

       <td id="tdMake_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Make_Need" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <asp:RadioButton ID="rbMake_Need1" runat="server" AutoPostBack="true" GroupName="Make_Balance"/>
</ContentTemplate>
   </asp:UpdatePanel></td>

       <td id="tdMake_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Make_Prof" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <asp:RadioButton ID="rbMake_Prof1" runat="server" AutoPostBack="true" GroupName="Make_Balance"/>
</ContentTemplate>
    </asp:UpdatePanel></td>

       <td id="tdMake_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Make_Exce" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <asp:RadioButton ID="rbMake_Exce1" runat="server" AutoPostBack="true" GroupName="Make_Balance"/>
</ContentTemplate>
    </asp:UpdatePanel></td></tr>

    <tr id="trBuild_Trust" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Build Trust</b></u><br /><br />
            o Embodies CR’s values and cultural attributes<br /><br />
            o Acts with integrity and follows through on commitments<br /><br />
            o Creates an organization characterized by trust and integrity where everyone is accountable for CR’s success<br /><br />
            o Demonstrates personal conviction in a way that inspires others to do the same<br /><br />
            o Takes ownership when things go wrong and does not place blame on others<br /><br />
            o Demonstrates humility and authenticity</span></a>
            &nbsp;&nbsp;Build Trust&nbsp;&nbsp;
             </td></tr></table></td>

       <td id="tdBuild2_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Build_Need" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <asp:RadioButton ID="rbBuild2_Need1" runat="server" AutoPostBack="true" GroupName="Build_Trust"/>
</ContentTemplate>
    </asp:UpdatePanel></td>

        <td id="tdBuild2_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Build_Prof" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <asp:RadioButton ID="rbBuild2_Prof1" runat="server" AutoPostBack="true" GroupName="Build_Trust"/>
</ContentTemplate>
    </asp:UpdatePanel></td>

       <td id="tdBuild2_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Build_Exce" runat="server" UpdateMode="Conditional">
<ContentTemplate>       
    <asp:RadioButton ID="rbBuild2_Exce1" runat="server" AutoPostBack="true" GroupName="Build_Trust"/>
</ContentTemplate>
    </asp:UpdatePanel></td></tr>

    <tr id="trLearn_Continuously" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Learn Continuously</b></u><br /><br />
            o Is self-aware and understands his/her impact on others<br /><br />
            o Shows commitment to continuous learning and development<br /><br />
            o Seeks out and is responsive to feedback<br /><br /> 
            o Listens to and learns from others<br /><br /> 
            o Experiments appropriately and learns from experiences<br /><br />
            o Shows intellectual curiosity</span></a>
            &nbsp;&nbsp;Learn Continuously&nbsp;&nbsp;
             </td></tr></table></td>

       <td id="tdLearn_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Learn_Need" runat="server" UpdateMode="Conditional"><ContentTemplate>
    <asp:RadioButton ID="rbLearn_Need1" runat="server" AutoPostBack="true" GroupName="Learn_Continuously" />
</ContentTemplate></asp:UpdatePanel></td>
       
        <td id="tdLearn_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Learn_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbLearn_Prof1" runat="server" AutoPostBack="true" GroupName="Learn_Continuously"/>
</ContentTemplate></asp:UpdatePanel></td>

        <td id="tdLearn_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Learn_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate>            
            <asp:RadioButton ID="rbLearn_Exce1" runat="server" AutoPostBack="true" GroupName="Learn_Continuously"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

    <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading Other</span></td></tr>

    <tr id="trLead_Urgency" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Lead with Urgency & Purpose</b></u><br /><br />
            o Effectively focuses self and others toward action and results<br /><br />
            o Keeps a focus on Key Performance Indicators (KPI’s) and the impact on consumers<br /><br />
            o Effectively stewards financial and people resources while managing to budget and driving towards the finish line</span></a>
            &nbsp;&nbsp;Lead with Urgency & Purpose&nbsp;&nbsp;
             </td></tr></table></td>

       <td  id="tdLead2_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead2_Need" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbLead2_Need1" runat="server" AutoPostBack="true" GroupName="Lead_Urgency"/>
</ContentTemplate></asp:UpdatePanel></td>

       <td  id="tdLead2_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead2_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbLead2_Prof1" runat="server" AutoPostBack="true" GroupName="Lead_Urgency"/>
</ContentTemplate></asp:UpdatePanel></td>

       <td  id="tdLead2_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead2_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbLead2_Exce1" runat="server" AutoPostBack="true" GroupName="Lead_Urgency"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

    <tr id="trPromote_Collaboration" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Promote Collaboration & Accountability</b></u><br /><br />
            o Sees and leverages the connections and interdependencies and works to break down silos<br /><br />
            o Leads others to better outcomes by demonstrating the value of collaboration, inclusion and diverse perspectives<br /><br />
            o Encourages and inspires staff to collaborate, partner, and assume ownership and accountability</span></a>
            &nbsp;&nbsp;Promote Collaboration & Accountability&nbsp;&nbsp;
           </td></tr></table></td>

       <td  id="tdProm_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Prom_Need" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbProm_Need1" runat="server" AutoPostBack="true" GroupName="Promote_Collab"/>
</ContentTemplate></asp:UpdatePanel></td>

       <td  id="tdProm_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Prom_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbProm_Prof1" runat="server" AutoPostBack="true" GroupName="Promote_Collab"/>
</ContentTemplate></asp:UpdatePanel></td>

       <td id="tdProm_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Prom_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbProm_Exce1" runat="server" AutoPostBack="true" GroupName="Promote_Collab"/>
</ContentTemplate></asp:UpdatePanel></td></tr>
               
    <tr id="trConfront_Challenges" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Confront Challenges</b></u><br /><br />
            o Does not ignore issues and challenges<br /><br />
            o Confronts disagreement and conflict through candid discussion, building common understanding, and gaining agreement on what’s in CR’s best interest<br /><br />
            o Recognizes when course corrections are necessary and takes swift action<br /><br />
            o Willing to take an unpopular stand for the good of the enterprise</span></a>
            &nbsp;&nbsp;Confront Challenges&nbsp;&nbsp;
            </td></tr></table></td>
           
       <td  id="tdConf_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Conf_Need" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbConf_Need1" runat="server" AutoPostBack="true" GroupName="Confront_Challenge"/>
</ContentTemplate></asp:UpdatePanel></td>

       <td  id="tdConf_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Conf_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbConf_Prof1" runat="server" AutoPostBack="true" GroupName="Confront_Challenge"/>
</ContentTemplate></asp:UpdatePanel></td>

       <td  id="tdConf_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Conf_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbConf_Exce1" runat="server" AutoPostBack="true" GroupName="Confront_Challenge"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

    <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading the Organization</span></td></tr>

    <tr id="trLead_Change" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
           <u><b>Lead Change</b></u><br/><br/>
            o Inspires others to embrace and lead change<br/><br/> o Removes barriers to change<br/><br/> o Clearly communicates the desired future state and gains commitment to the change vision<br/><br/>
            o Knows how to leverage key influencers to champion change and build cross-functional support for change</span></a> 
           &nbsp;&nbsp;Lead Change&nbsp;&nbsp;
         </td></tr></table></td>

       <td id="tdLead_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead_Need" runat="server" UpdateMode="Conditional"><ContentTemplate>           
           <asp:RadioButton ID="rbLead_Need1" runat="server" AutoPostBack="true" GroupName="Lead_Change" />
</ContentTemplate></asp:UpdatePanel></td>
       
        <td id="tdLead_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate>           
           <asp:RadioButton ID="rbLead_Prof1" runat="server" AutoPostBack="true" GroupName="Lead_Change" />
</ContentTemplate></asp:UpdatePanel></td>

       <td id="tdLead_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate>
           <asp:RadioButton ID="rbLead_Exce1" runat="server" AutoPostBack="true" GroupName="Lead_Change" />
</ContentTemplate></asp:UpdatePanel></td></tr>

    <tr id="trInspire_Risk" style="height:33px; font-size:large">
       <td class="style11"><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
           <u><b>Inspire Risk Taking & Innovation</b></u><br/><br/>
           o Creates an environment where people feel safe in taking appropriate risks and are not afraid to fail<br/><br/>
           o Sees the possibilities and effectively leads people and process to realize innovation, revenue growth, and social impact<br /><br/>
           o Has the courage and takes action to cease unfruitful initiatives</span></a>
           &nbsp;&nbsp;Inspire Risk Taking & Innovation&nbsp;&nbsp;
           </td></tr></table></td>

        <td  id="tdInsp_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Insp_Need" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:RadioButton ID="rbInsp_Need1" runat="server" AutoPostBack="true" GroupName="Inspire_Risk"/>
</ContentTemplate></asp:UpdatePanel></td>

        <td  id="tdInsp_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Insp_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate>            
            <asp:RadioButton ID="rbInsp_Prof1" runat="server" AutoPostBack="true" GroupName="Inspire_Risk"/>
</ContentTemplate></asp:UpdatePanel></td>
        
        <td  id="tdInsp_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Insp_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:RadioButton ID="rbInsp_Exce1" runat="server" AutoPostBack="true" GroupName="Inspire_Risk"/>
</ContentTemplate></asp:UpdatePanel></td></tr>
    
    <tr id="trLeverage_External" style="height:33px; font-size:large">
       <td class="style11"><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
          <u><b>Leverage External Perspective</b></u><br /><br />
            o Leverages knowledge of the competitive landscape, marketplace trends, best practices, and technology to improve CR’s ability to compete<br/><br/>
            o Holds self and others accountable for staying abreast of external trends and developments</span></a>
            &nbsp;&nbsp;Leverage External Perspective&nbsp;&nbsp;
             </td></tr></table></td>

        <td id="tdLeve_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Leve_Need" runat="server" UpdateMode="Conditional"><ContentTemplate>            
            <asp:RadioButton ID="rbLeve_Need1" runat="server" AutoPostBack="true" GroupName="Leverage_External"/>
</ContentTemplate></asp:UpdatePanel></td>
        
        <td id="tdLeve_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Leve_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:RadioButton ID="rbLeve_Prof1" runat="server" AutoPostBack="true" GroupName="Leverage_External"/>
</ContentTemplate></asp:UpdatePanel></td>
        
        <td id="tdLeve_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Leve_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:RadioButton ID="rbLeve_Exce1" runat="server" AutoPostBack="true" GroupName="Leverage_External"/>
</ContentTemplate></asp:UpdatePanel></td></tr>
    
    <tr id="trCommunicate_Impact" style="height:33px; font-size:large">
       <td class="style11"><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
           <u><b>Communicate for Impact</b></u><br /><br />
            o Translates vision, strategy, and complex information into simple impactful messages tailored to meet the needs of the audience<br/><br/>
            o Communicates consistently in a clear and confident way<br/><br/>
            o Persuades and influences others through compelling communications and clear rationale<br/><br/>
            o Encourages others to express their views openly</span></a>
           &nbsp;&nbsp;Communicate for Impact&nbsp;&nbsp;
           </td></tr></table></td>

        <td  id="tdComm_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Comm_Need" runat="server" UpdateMode="Conditional"><ContentTemplate>            
            <asp:RadioButton ID="rbComm_Need1" runat="server" AutoPostBack="true" GroupName="Communic_Impact"/>
</ContentTemplate></asp:UpdatePanel></td>

        <td  id="tdComm_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Comm_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate>            
            <asp:RadioButton ID="rbComm_Prof1" runat="server" AutoPostBack="true" GroupName="Communic_Impact"/>
</ContentTemplate></asp:UpdatePanel></td>

        <td  id="tdComm_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Comm_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate>            
            <asp:RadioButton ID="rbComm_Exce1" runat="server" AutoPostBack="true" GroupName="Communic_Impact"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

 </table>
        
</td></tr>

 </table>
 
      </td>
<!--END Addendum -->    
     <td class="style6">&nbsp;</td></tr>     

<asp:Panel ID="Panel_Goal_Setting_Edit" Visible="true" runat="server">
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
<!--Divider-->
    <tr><td class="style6">&nbsp;</td><td colspan="6"><table style="width:100%; border:solid 1px lightgray; font-size:0px; line-height:0px; height:1px; background-color: gray;"><tr><td></td></tr></table>
                                      </td><td class="style6">&nbsp;</td></tr>
<!--END Divider-->

    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
    <tr><td class="style6">&nbsp;&nbsp;</td><td class="style1" colspan="6"><u>FY<asp:Label ID="Goal_Year" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> 
        Goal-Setting Form (06/01/<asp:Label ID="Goal_Year1" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> - 05/31/<asp:Label ID="Goal_Year2" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td>
        <td class="style6"></td></tr>
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
    <tr><td class="style6">&nbsp;</td><td colspan="6" style="font-family:Calibri; font-size:medium;">Enter SMART Goals 
            (<strong>S</strong>pecific, <strong>M</strong>easurable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to 
            focus employees on achieving important, tangible outcomes that contribute to the achievement of department and strategic goals. Summarize the goals agreed to with the employee.
        <div id="divNewtext" runat="server" style="font-size:small; font-style:italic">Please click 
            <a href="https://docs.google.com/presentation/d/1LIm9LOScdiizJcXARapml7-gj2IkJfq-MtQgkBFNHho/edit#slide=id.g1f6e4888fe_0_0" target="_blank">here</a> for a SMART goal example.</div>
                                      </td><td class="style6">&nbsp;</td></tr>     
    <tr><td class="style6">&nbsp;</td>
        <td colspan="6"><asp:Button ID="btnNew_Goal" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Text="Add New Goal" CssClass="Button_StyleSheet"/>
<table style="width:100%" border="0">
<tr><td colspan="2" style="width:4%;"></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:45%; font-family:Calibri; background-color:lightgray;">Goals
        <div id="divGoalText" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">What do I need to accomplish in order to support my department’s goals and CR’s goals?</div>
    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">
        <div id="divOldTitle" runat="server" >Success Measures or Milestones</div>
        <div id="divNewTitle" runat="server" >Key Results</div>
        <div id="divNewKey" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">How will I know that I’ve accomplished each goal?</div>
    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:10%; font-family:Calibri; background-color:lightgray;">Target<br />Completion Date
        <div id="divNewtarget" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">By when do I need to accomplish each goal?</div>
    </td></tr>

<tr><td style="width:2%; vertical-align:top;">&nbsp;</td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">1)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal1" runat="server" UpdateMode="Conditional"><ContentTemplate>
<asp:TextBox ID="txbGoal1" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray"  TextMode="MultiLine" Rows="6" Style="overflow:auto;" 
ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="16" AutoPostBack="true" OnTextChanged="Goal1_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success1" runat="server" UpdateMode="Conditional"><ContentTemplate>        
        <asp:TextBox ID="txbSuccess1" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="17" AutoPostBack="true" OnTextChanged="Success1_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date1" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate1" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="18" AutoPostBack="true" OnTextChanged="Date1_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

<asp:Panel ID="Panel_Goal2" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal2" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">2)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal2" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal2" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="19" AutoPostBack="true" OnTextChanged="Goal2_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success2" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess2" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="20" AutoPostBack="true" OnTextChanged="Success2_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date2" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate2" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="21" AutoPostBack="true" OnTextChanged="Date2_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal3" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal3" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">3)</td>

    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal3" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal3" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="22" AutoPostBack="true" OnTextChanged="Goal3_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success3" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess3" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="23" AutoPostBack="true" OnTextChanged="Success3_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date3" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate3" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="24" AutoPostBack="true" OnTextChanged="Date3_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

</asp:Panel>

<asp:Panel ID="Panel_Goal4" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal4" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">4)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal4" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal4" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="25" AutoPostBack="true" OnTextChanged="Goal4_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success4" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess4" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="26" AutoPostBack="true" OnTextChanged="Success4_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date4" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate4" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="27" AutoPostBack="true" OnTextChanged="Date4_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

</asp:Panel>
<asp:Panel ID="Panel_Goal5" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal5" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">5)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal5" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal5" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="28" AutoPostBack="true" OnTextChanged="Goal5_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success5" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess5" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="29" AutoPostBack="true" OnTextChanged="Success5_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date5" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate5" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="30" AutoPostBack="true" OnTextChanged="Date5_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

</asp:Panel>
<asp:Panel ID="Panel_Goal6" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal6" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">6)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal6" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal6" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="31" AutoPostBack="true" OnTextChanged="Goal6_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success6" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess6" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="32" AutoPostBack="true" OnTextChanged="Success6_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date6" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate6" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="33" AutoPostBack="true" OnTextChanged="Date6_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

</asp:Panel>
<asp:Panel ID="Panel_Goal7" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal7" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">7)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal7" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal7" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="34" AutoPostBack="true" OnTextChanged="Goal7_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success7" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess7" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="35" AutoPostBack="true" OnTextChanged="Success7_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date7" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate7" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="36" AutoPostBack="true" OnTextChanged="Date7_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

</asp:Panel>
<asp:Panel ID="Panel_Goal8" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal8" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">8)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal8" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal8" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="37" AutoPostBack="true" OnTextChanged="Goal8_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success8" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess8" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="38" AutoPostBack="true" OnTextChanged="Success8_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date8" runat="server" UpdateMode="Conditional"><ContentTemplate>        
        <asp:TextBox ID="txbDate8" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="39" AutoPostBack="true" OnTextChanged="Date8_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>

</asp:Panel>
<asp:Panel ID="Panel_Goal9" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal9" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">9)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal9" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal9" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="40" AutoPostBack="true" OnTextChanged="Goal9_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success9" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess9" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="41" AutoPostBack="true" OnTextChanged="Success9_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date9" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate9" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="42" AutoPostBack="true" OnTextChanged="Date9_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>
<asp:Panel ID="Panel_Goal10" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal10" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:2%; vertical-align:top; text-align:right; font-weight:bold;">10)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal10" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal10" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="43" AutoPostBack="true" OnTextChanged="Goal10_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success10" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess10" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="44" AutoPostBack="true" OnTextChanged="Success10_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date10" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate10" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray"  TextMode="MultiLine" Rows="6" Style="overflow:auto;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="45" AutoPostBack="true" OnTextChanged="Date10_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

</table>
        </td>
        <td class="style6">&nbsp;</td></tr>     
</asp:Panel><!--END Panel_Goal_Setting_Edit-->

<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>    

</asp:Panel> <!--END Panel_Create_Appraisal-->

<asp:Panel ID="Panel_Waiting_Approval" Visible="false" runat="server">
    <tr><td class="style6">&nbsp;</td>
        <td colspan="6">
<!--Accomplishments Waiting_Approval-->
  <table border="0" style="width:100%;">
   <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class="style7">(Summarize accomplishments for the rating criteria established)</span></td></tr>
  </table>

<table border="0" style="width:98%; border-spacing:0; border-collapse:collapse;"><tr><td colspan="2"><hr /></td></tr>
  <tr><!--<td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">1)</td>-->
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp1A" runat="server" Width="99%" /></td></tr>

<asp:Panel ID="Panel_Accomp2_View" runat="server" Visible="false"><tr><td colspan="2"><hr /></td></tr>         
  <tr><td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">2)</td>
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp2A" runat="server" Width="99%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Accomp3_View" runat="server" Visible="false"><tr><td colspan="2"><hr /></td></tr>                  
  <tr><td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">3)</td>
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp3A" runat="server" Width="99%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Accomp4_View" runat="server" Visible="false"><tr><td colspan="2"><hr /></td></tr>
  <tr><td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">4)</td>
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp4A" runat="server" Width="99%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Accomp5_View" runat="server" Visible="false"><tr><td colspan="2"><hr /></td></tr>
  <tr><td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">5)</td>
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp5A" runat="server" Width="99%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Accomp6_View" runat="server" Visible="false"><tr><td colspan="2"><hr /></td></tr>
  <tr><td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">6)</td>
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp6A" runat="server" Width="99%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Accomp7_View" runat="server" Visible="false"><tr><td colspan="2"><hr /></td></tr>
  <tr><td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">7)</td>
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp7A" runat="server" Width="99%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Accomp8_View" runat="server" Visible="false"><tr><td colspan="2"><hr /></td></tr>
  <tr><td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">8)</td>
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp8A" runat="server" Width="99%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Accomp9_View" runat="server" Visible="false"><tr><td colspan="2"><hr /></td></tr>
  <tr><td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">9)</td>
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp9A" runat="server" Width="99%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Accomp10_View" runat="server" Visible="false"><tr><td colspan="2"><hr /></td></tr>
  <tr><td style="width:30px; vertical-align:top; text-align:center; font-weight:bold;">10)</td>
      <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbAccomp10A" runat="server" Width="99%"/></td></tr></asp:Panel>
<tr><td colspan="2"><hr /></td></tr>
</table>
<!--END Accomplishments Waiting_Approval-->
        </td>
        <td class="style6">&nbsp;</td></tr>   

<!--2. Strengths Waiting_Approval--> 
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
<table border="0" style="width:100%">
  <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class="style7">(Comment on key strengths and highlight areas performed well)</span></td></tr>
  <tr><td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbStrengthsA" runat="server" Width="99%"/></td></tr>
    <tr><td><hr /></td></tr>
</table>
    </td>
    <td class="style6">&nbsp;</td></tr>     
<!--2. END Strengths Waiting_Approval-->          

<!--3. Development Areas Waiting_Approval-->  
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
<table border="0" style="width:100%">
  <tr><td class="style8" >&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp;
      <span class="style7">(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span></td></tr>
  <tr><td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDevelopment_AreasA" runat="server"/></td></tr>
  <tr><td><hr /></td></tr>
</table>
  </td>
  <td class="style6">&nbsp;</td></tr>   
<!--3. END Development Areas Waiting_Approval--> 

<!--4. Overall Performance Rating Waiting_Approval-->
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
<table border="0" style="width:99%">
  <tr><td colspan="5" class="style8">&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class="style7"> (Check the box that most appropriately describes the individual&#39;s overall performance)  </span></td></tr>
  <tr><td colspan="5">
      <table border="1" style="width:100%; border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0;">
          <tr><td class="style9"><b>Unsatisfactory</b></td>
              <td class="style9"><b>Developing/Improving Contributor</b></td>        
              <td class="style9"><b>Solid Contributor</b></td>
              <td class="style9"><b>Very Strong Contributor</b></td>
              <td class="style9"><b>Distinguished Contributor</b></td></tr>
          <tr><td style="text-align:center;"><asp:RadioButton ID="rbBelow1" runat="server" Enabled="false"/></td>
              <td style="text-align:center;"><asp:RadioButton ID="rbNeed1" runat="server" Enabled="false"/></td>     
              <td style="text-align:center;"><asp:RadioButton ID="rbMeet1" runat="server" Enabled="false"/></td>
              <td style="text-align:center;"><asp:RadioButton ID="rbExceed1" runat="server" Enabled="false"/></td>
              <td style="text-align:center;"><asp:RadioButton ID="rbDisting1" runat="server" Enabled="false"/></td> </tr>
      </table>

</td></tr></table>
      </td>
      <td class="style6">&nbsp;</td></tr>     
<!--5. END Overall Performance Rating Waiting_Approval-->  
      
<!--4. Overall Summary Waiting approval--> 
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
<table border="0" style="width:100%">
 <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class="style7">(Comment on overall performance)</span></td></tr>
 <tr><td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbOverall_SumA" runat="server" Width="99%"/></td></tr>
 <tr><td><hr /></td></tr>
</table>
    </td>
    <td class="style6">&nbsp;</td></tr>     
<!--5. END Overall Summary Waiting approval-->          

<!--6. Development Plan Waiting approval-->   
<asp:Panel id="Panel_Development_Plan1" runat="server" Visible="false">     
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
<table border="0" style="width:100%">
    <tr><td class="style8" >&nbsp;&nbsp;&nbsp;<u>Development Plan:</u>&nbsp;<span class="style7">(Based on development areas, summarize a plan for professional and performance development)</span></td></tr>
    <tr><td style="vertical-align:top; font-family:Calibri;">&nbsp<asp:Label ID="txbDevelopment_ObjectiveA" runat="server" Width="99%"/></td></tr>
    <tr><td colspan="2"><hr /></td></tr>
</table>
  </td>
  <td class="style6">&nbsp;</td></tr>   
</asp:Panel>
<!--6. END Development Plan Waiting approval--> 

<!-- <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->
<!--Divider-->
    <tr><td class="style6">&nbsp;</td><td colspan="6"><table style="width:100%; border:solid lightgray; font-size:0px; line-height:0px; height:1px; background-color:lightgray;"><tr><td></td></tr></table></td><td class="style6">&nbsp;</td></tr>
<!--END Divider-->
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->

<!--Addendum -->  
  <tr><td class="style6">&nbsp;</td>
      <td colspan="6">
 
  <table border="0" style="width:99%" tabindex="15">
    <tr><td class="style1"><u>Addendum:&nbsp;Leadership Competencies</u></td></tr>
    <tr><td></td></tr>
    <tr><td class="style7">Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies. 
        Use this as a guide to provide feedback and guide your direct report on areas to focus on for further growth and development.

<table border="1" style="width:100%; border-spacing:0; border-collapse:collapse;" runat="server">
   <tr><td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri;">&nbsp;</td>
        <td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; width:20%;">Needs Development/Improvement</td>
        <td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; width:20%;">Proficient</td>
        <td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; width:20%;">Excels</td></tr>
   <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading Oneself</span></td></tr>
   <tr style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                   <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Make Balanced Decisions&nbsp;&nbsp;</td></tr></table></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbMake_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbMake_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbMake_Exce" runat="server" Enabled="false"/></td></tr>
   <tr style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                   <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Build Trust&nbsp;&nbsp;</td></tr></table></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbBuild2_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbBuild2_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbBuild2_Exce" runat="server" Enabled="false"/></td></tr>
   <tr style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                     <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Learn Continuously&nbsp;&nbsp;</td></tr></table></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLearn_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLearn_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLearn_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading Other</span></td></tr>
   <tr style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                    <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Lead with Urgency & Purpose&nbsp;&nbsp;</td></tr></table></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead2_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead2_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead2_Exce" runat="server" Enabled="false"/></td></tr>
   <tr style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                    <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Promote Collaboration & Accountability&nbsp;&nbsp;</td></tr></table></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbProm_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbProm_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbProm_Exce" runat="server" Enabled="false"/></td></tr>
   <tr style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                      <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Confront Challenges&nbsp;&nbsp;</td></tr></table></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbConf_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbConf_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbConf_Exce" runat="server" Enabled="false"/></td></tr>
   <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading the Organization</span></td></tr>
   <tr style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                      <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Lead Change&nbsp;&nbsp;</td></tr></table></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbLead_Exce" runat="server" Enabled="false"/></td></tr>
   <tr style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                      <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Inspire Risk Taking & Innovation&nbsp;&nbsp;</td></tr></table></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbInsp_Need" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbInsp_Prof" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbInsp_Exce" runat="server" Enabled="false"/></td></tr>
    <tr style="height:33px; font-size:large">
       <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                      <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Leverage External Perspective&nbsp;&nbsp;</td></tr></table></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbLeve_Need" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbLeve_Prof" runat="server" Enabled="false"/></td>
        <td style="text-align:center;"><asp:RadioButton ID="rbLeve_Exce" runat="server" Enabled="false"/></td></tr>
   <tr style="height:33px; font-size:large">
        <td><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
                      <td style="text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;Communicate for Impact&nbsp;&nbsp;</td></tr></table></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbComm_Need" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbComm_Prof" runat="server" Enabled="false"/></td>
       <td style="text-align:center;"><asp:RadioButton ID="rbComm_Exce" runat="server" Enabled="false"/></td></tr>
 </table>
</td></tr>
 </table>
      </td>
<!--END Addendum -->
          
       <td class="style6">&nbsp;</td></tr>
   <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
<!--Divider-->
   <tr><td colspan="8"><table style="width:100%; border:solid 1px lightgray; font-size:0px; line-height:0px; height:1px; background-color: lightgray;"><tr><td></td></tr></table></td></tr>
<!--END Divider-->
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
    <tr><td class="style6">&nbsp;&nbsp;</td><td class="style1" colspan="6"><u>FY<asp:Label ID="Goal_Year3" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> Goal-Setting Form 
        (06/01/<asp:Label ID="Goal_Year4" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> - 05/31/<asp:Label ID="Goal_Year5" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td><td class="style6"></td></tr>
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     

<!--Smart Goal Waiting-->
    <tr><td class="style6">&nbsp;</td><td colspan="6" style="font-family:Calibri; font-size:medium;">Enter SMART Goals 
            (<strong>S</strong>pecific, <strong>M</strong>easurable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to 
            focus employees on achieving important, tangible outcomes that contribute to the achievement of department and strategic goals. Summarize the goals agreed to with the employee.
        <div id="divNewtext1" runat="server" style="font-size:small; font-style:italic">Please click 
            <a href="https://docs.google.com/presentation/d/1LIm9LOScdiizJcXARapml7-gj2IkJfq-MtQgkBFNHho/edit#slide=id.g1f6e4888fe_0_0" target="_blank">here</a> for a SMART goal example.</div>
                                      </td>
    <td class="style6">&nbsp;</td></tr>     
    <tr><td class="style6">&nbsp;</td><td colspan="6">

<table border="1" style="width:99%; border-collapse:collapse; border-spacing:0;">
<tr><td style="background-color:lightgray; width:3%;"></td> 
    <td style="text-align:center; font-size:large; font-weight:bold; width:45%; font-family:Calibri; background-color:lightgray;">Goals
        <div id="divGoalText1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">What do I need to accomplish in order to support my department’s goals and CR’s goals?</div>

    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">
        <div id="divOldTitle1" runat="server" >Success Measures or Milestones</div>
        <div id="divNewTitle1" runat="server" >Key Results</div>
        <div id="divNewKey1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">How will I know that I’ve accomplished each goal?</div>

    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:12%; font-family:Calibri; background-color:lightgray;">Target<br />Completion Date
        <div id="divNewtarget1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">By when do I need to accomplish each goal?</div>
    </td></tr>

<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">1)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal1A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess1A" runat="server" Width="98%"/></td>
     <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate1A" runat="server" Width="98%"/></td></tr>
<asp:Panel ID="Panel_Goal2_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">2)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal2A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess2A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate2A" runat="server" Width="98%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal3_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">3)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal3A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess3A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate3A" runat="server" Width="98%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal4_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">4)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal4A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess4A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate4A" runat="server" Width="98%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal5_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">5)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal5A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess5A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate5A" runat="server" Width="98%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal6_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">6)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal6A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess6A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate6A" runat="server" Width="98%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal7_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">7)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal7A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess7A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate7A" runat="server" Width="98%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal8_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">8)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal8A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess8A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate8A" runat="server" Width="98%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal9_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">9)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal9A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess9A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate9A" runat="server" Width="98%"/></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal10_Waiting" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold;">10)</td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbGoal10A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbSuccess10A" runat="server" Width="98%"/></td>
    <td style="vertical-align:top; font-family:Calibri;"><asp:Label ID="txbDate10A" runat="server" Width="98%"/></td></tr></asp:Panel>
</table>
       </td><td class="style6">&nbsp;</td></tr>     
<!--<tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>-->
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
<!--END Smart Goal Waiting-->
</asp:Panel><!--END Panel_Waiting_Approval-->

<asp:Panel ID="Panel_Employee_Review" Visible="false" runat="server">
  <tr><td class="style6">&nbsp;</td><td colspan="6">
        <table style="width:100%">
            <tr id="trPressing" runat="server"><td style="text-align:center; font-family:Calibri; font-size:medium; font-weight:bold;">Pressing the "E-Sign" button means that I have reviewed and discussed this appraisal with my immediate supervisor.</td></tr>
            <tr id="trAgree" runat="server"><td style="text-align:center; font-family:Calibri; font-size:medium; font-weight:bold;">It does not necessarily imply that I agree with this evaluation.</td></tr>
            <tr id="trComments" runat="server">
                <td style="text-align:left; font-family:Calibri; font-size:medium; font-weight:bold;">Employee's Comments: <font style="font-size:small; color:lightgray">(optional)</font></td></tr>
            <tr><td><asp:TextBox ID="txbEmployeeComments" runat="server" Width="99%" TextMode="MultiLine" Borderstyle="Solid" BorderColor="LightGray" Rows="8" CssClass="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled"/>
                    <asp:Label ID="lblEmployeeComments" runat="server" Visible="false"></asp:Label>
    </td></tr>
            <tr><td style="text-align:center;"><asp:Button ID="btnSubmit" runat="server" Text="E-Sign" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" width="120px" CssClass="Label_StyleSheet" /></td></tr>
        </table>

      </td><td class="style6">&nbsp;</td></tr>   
    
</asp:Panel>

    <tr><td class="style6"></td>
        <td class="style1" colspan="6">
<table style="width:100%" border="0">

<asp:Panel ID="Panel_Comments" Visible="false" runat="server">     
<tr><td colspan="3" style="text-align:left; color:black; font-size:medium;"><asp:Label ID="lblDiscussionTitle" runat="server"/></td></tr>
<tr><td colspan="3">
<asp:UpdatePanel ID="UpdatePanel_DiscussionComments" runat="server" UpdateMode="Conditional"> <ContentTemplate>    
   <asp:TextBox ID="DiscussionComments" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" ValidateRequestMode="Disabled" 
        Rows="5" Style="overflow:auto;" TabIndex="1" onkeyup="SetButtonStatus(this, 'btnGeneralist'); "/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>
    
    <tr><td style="width:35%; text-align:left;">
              <asp:Button ID="btnSubmit_UpperMgr" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Name="Submit for" CssClass="Button_StyleSheet"/>
              <asp:Button ID="btnDiscuss" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" CssClass="Button_StyleSheet"/></td>
        <td style="text-align:center;"><asp:ImageButton ID="Img_Print2" runat="server" ImageUrl="../../Images/print.png" Style="width:70px"/></td>
        <td style="width:35%; text-align:right;">
              <asp:Button ID="btnGeneralist" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" CssClass="Button_StyleSheet"/>
                        
                        <asp:label ID="lblMessage" runat="server"  ForeColor="White" Font-Bold="true" Font-Size="Smaller" Text="" CssClass="Label_StyleSheet;" />&nbsp;&nbsp;
                        <asp:Button ID="SaveRecords" runat="server" Text="Save Records" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" CssClass="Button_StyleSheet" Visible="false"/>
                    </td></tr></table>
        </td>
        <td class="style6">&nbsp;</td></tr>     


    <tr><td class="style6"></td>
        <td colspan="6">
<!--Edit Panel after HR approval-->
<table id="tblEdit_form" style="width:100%" runat="server" border="0">
    <tr>
      <td style="width:35%">
           <asp:Button ID="btnEditForm" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" CssClass="Button_StyleSheet"
                OnClientClick="return confirm(&quot;Clicking OK will allow you to make edits to the existing text and send it back to HR.&quot;);"/></td>
        <td style="text-align:right;">
<!--Emploee Declined-->
<table style="width:100%;" border="0">
   <tr><td style="text-align:center;"><asp:Label ID="lblRefuseText" runat="server" ForeColor="blue" Font-Size="Smaller" Text="" CssClass="Label_StyleSheet"/></td></tr> 
   <tr><td style="text-align:center;">
<div id="divCalendar" style="position:absolute;">
  <asp:Calendar id="Calendar1" runat="server" BorderWidth="2px" BackColor="White" Width="200px" ForeColor="Black" Height="180px" Font-Size="11pt" Font-Names="Calibri"  
      Visible="false" BorderColor="#999999" BorderStyle="Outset" DayNameFormat="FirstLetter" CellPadding="4">
  <TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
  <SelectorStyle BackColor="#CCCCCC"></SelectorStyle><NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
  <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle><SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
  <TitleStyle Font-Bold="True" BorderColor="Black"  BackColor="#999999"></TitleStyle><WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle><OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
</asp:Calendar></div>

<asp:Button ID="RefuseSign" runat="server" Text="Employee Declined to Sign" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" width="180px" Visible="false" CssClass="TextBox_StyleSheet"
             OnClientClick="return confirm(&quot;Are you sure you want to indicate that the employee declined to sign?&quot;);"/>
<asp:TextBox ID="txtCal" runat="server" Width="70px" BorderStyle="Solid" BorderColor="Silver" Visible="false" ReadOnly="true" CssClass="TextBox_StyleSheet"/>
<asp:ImageButton ID="imgCal" runat="server" ImageUrl="../../Images/calendar.png" Visible="false"/>
   </td></tr>
</table>
<!--END Employee Declined-->
    </td><td style="width:35%; text-align:right;">
            <asp:Button ID="btnSendEmployee" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" CssClass="Button_StyleSheet"/></td></tr></table>
<!--END Edit Panel  after HR approval-->                       
        </td>
        <td class="style6">&nbsp;</td></tr>     
<asp:Panel ID="Panel_HR_Comments" Visible="false" runat="server">
<tr><td class="style6">&nbsp;</td><td colspan="6">
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet" Caption="Returned for Edit">
 <Columns>
<asp:BoundField DataField="EMPL" HeaderText="Employee" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="15%" ></asp:BoundField>    
<asp:BoundField DataField="MGT" HeaderText="Manager" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"  HeaderStyle-Width="15%"></asp:BoundField>
<asp:BoundField DataField="REJ" HeaderText="Sent for Edit by" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"  HeaderStyle-Width="15%"></asp:BoundField>
<asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="DateTime" HeaderText="Return Date" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"  HeaderStyle-Width="12%" ></asp:BoundField>
</Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</td><td class="style6">&nbsp;</td></tr>   
</asp:Panel>

    <tr><td class="style6">&nbsp;</td>
        <td class="style1" colspan="6">
            <asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
                CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/>
            <!--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Images/Back_button.png" style="width:90px" />--></td>
    <td class="style6">&nbsp;</td></tr>  

    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>     
    <tr><td class="style6">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class="style6">&nbsp;</td></tr>   
</table>

  </form>

 </body>
</html>
