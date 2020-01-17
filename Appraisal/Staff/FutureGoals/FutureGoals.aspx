<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FutureGoals.aspx.vb" Inherits="Appraisal.Goals" MaintainScrollPositionOnPostback="true" smartNavigation="true"%>

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
    .auto-style2 {
        width: 105px;
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
    .auto-style4 {
        height: 3px;
    }
 </style>


<script lang="jascript" type="text/javascript">
    //========================================================================
    function ChangeColor(i) {
        // Retrieve the element by its id 'i'
        document.getElementById(i).style.backgroundColor = "";
    }
    //========================================================================
    window.onload = function () {
        var seconds = 3;
        setTimeout(function () {
            document.getElementById("<%=lblMessage1.ClientID %>").style.display = "none";
        }, seconds * 1000);
     };
   //========================================================================
   function SetButtonStatus(sender, target) {

       //alert((document.getElementById("Approve"))*length);
       //alert((document.getElementById("Generalist")) * length);

       if ((document.getElementById("Approve")) * length !== 0) {
           if (sender.value.length >= 2)
               document.getElementById("Approve").disabled = true;
           else
               document.getElementById("Approve").disabled = false;
       }

       if ((document.getElementById("Generalist")) * length !== 0) {
           if (sender.value.length >= 2)
               document.getElementById("Generalist").disabled = true;
           else
               document.getElementById("Generalist").disabled = false;
       }
   }
   //========================================================================
</script>
</head>

<body>

<form id="form1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:Timer ID="Timer1" runat="server" Interval="5000" ontick="Timer1_Tick"></asp:Timer> 
<asp:Timer ID="Timer2" runat="server" Interval="5000" ontick="Timer2_Tick"></asp:Timer> 

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
        <td class="style6" rowspan="5">
           <asp:Button ID="lblMessage1" runat="server" ForeColor="Green" BackColor="White" Width="80px" BorderStyle="None"  Font-Bold="true" Font-Size="10pt" CssClass="Button_StyleSheet" style="position:fixed; right:100px; top:280px; height: 23px; margin-bottom: 2px;"/><br />
           <asp:Button ID="btnSave2" runat="server" Text="Save Records" BackColor="Wheat" BorderStyle="None" Width="100px" Height="20px" Font-Size="11pt" Font-Names="Calibri" style="position:fixed; right:90px; top: 300px;"/>
   <tr><td class="Style1"><u>FY<asp:Label ID="Goal_Year" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> 
        Goal-Setting Form (06/01/<asp:Label ID="Goal_Year1" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> - 05/31/<asp:Label ID="Goal_Year2" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td></tr>
  <tr><td class="auto-style3">&nbsp;&nbsp;</td></tr> 
  <tr><td>
          <table style="width:100%" border="0" class="TextBox_StyleSheet">
            <tr><td class="auto-style2"><b>Name:</b></td>
                <td class="style2"><asp:label id="lblEmpl_NAME" runat="server" CssClass="Label_StyleSheet"/></td>
                <td class="style4">&nbsp;&nbsp;</td>
                <td class="style2">&nbsp;&nbsp;</td>
                <td class="style5">E-Signed:&nbsp;&nbsp;</td>
                <td><asp:Label ID="LblEMP_Appr" runat="server" Font-Names="Calibri"/></td></tr>
            <tr><td class="auto-style2"><b>Title:</b></td>
                <td><asp:label id="lblEmpl_TITLE" runat="server" Font-Names="Calibri"/></td>
                <td><b>Manager Name:&nbsp;</b></td>
                <td><asp:Label ID="LblMGT_NAME" runat="server" Font-Names="Calibri" /></td>
                <td class="style5">Approved:&nbsp;&nbsp;</td>
                <td><asp:Label ID="LblMGT_Appr" runat="server" Font-Names="Calibri" /></td></tr>
            <tr><td class="auto-style2"><b>Department:</b></td>
                <td><asp:label id="lblEmpl_DEPT" runat="server" Font-Names="Calibri"/></td>
                <td><b>2nd Level Manager Name:&nbsp;</b></td>
                <td><asp:Label ID="lblUP_MGT_NAME" runat="server" Font-Names="Calibri"/></td>
                <td class="style5">&nbsp;&nbsp;</td>
                <td>&nbsp;</td></tr>
            <tr><td class="auto-style2"><b>Hire Date:</b></td>
                <td><asp:label id="lblEmpl_HIRE" runat="server" Font-Names="Calibri"/></td>
                <td><!--<b>Human Resources Generalist:</b>-->&nbsp;&nbsp;</td>
                <td><asp:label id="lblHR_NAME" runat="server" Font-Names="Calibri" Visible="false"/></td>
                <td class="style5">&nbsp;</td>
                <td>&nbsp;</td></tr></table>
  </td></tr>
<tr><td>&nbsp;</td></tr>
<tr><td class="style6"></td><td>
<table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="0">
 <tr><td>

 <table border="0" style="width:100%">
     <tr><td style="font-family:Calibri; font-size:small;">Enter SMART Goals 
            (<strong>S</strong>pecific, <strong>M</strong>easurable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to 
            focus on achieving important, tangible outcomes that contribute to the achievement of CR&#39;s  strategic plan.<br />
         <div id="divNewtext" runat="server" style="font-size:small; font-style:italic">Please click 
            <a href="https://docs.google.com/presentation/d/1LIm9LOScdiizJcXARapml7-gj2IkJfq-MtQgkBFNHho/edit#slide=id.g1f6e4888fe_0_0" target="_blank">here</a> for a SMART goal example.</div>
         </td></tr></table>

<asp:Panel ID="Goal_Setting_Edit" Visible="false" runat="server">

<asp:UpdatePanel ID="UpdatePanel_AllData" runat="server" UpdateMode="Conditional">
<ContentTemplate></ContentTemplate>
    <triggers><asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/></triggers>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate></ContentTemplate>
    <triggers><asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick"/></triggers>
</asp:UpdatePanel>

<table style="width:100%"><tr><td><asp:Button ID="btnNew_Goal" runat="server" BackColor="Wheat" BorderStyle="None" Font-Names="Calibri" Font-Size="11pt" width="100px" Text="Add New Goal"/></td></tr></table>
<table style="width:100%">
<tr><td colspan="2" style="width:5%;">&nbsp;&nbsp;</td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">Goals
        <div id="divGoalText" runat="server" style="font-size:small; font-weight:lighter; font-style:italic; word-wrap: normal; word-break: normal;">What do I need to accomplish in order to support my department’s goals and CR’s goals?</div>
    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">
        <div id="divOldTitle" runat="server">Success Measures or Milestones</div>
        <div id="divNewTitle" runat="server">Key Results</div>
        <div id="divNewKey" runat="server" style="font-size:small; font-weight:lighter; font-style:italic; word-wrap: normal; word-break: normal;">How will I know that I’ve accomplished each goal?</div>
    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; font-family:Calibri; background-color:lightgray; word-wrap: normal; word-break: normal;">Target<br />Completion<br />Date
         <div id="divNewtarget" runat="server" style="font-size:small; font-weight:lighter; font-style:italic; word-wrap: normal; word-break: normal;">When will I <br />accomplish this goal?</div>
    </td></tr>
<tr><td style="width:2%; vertical-align:top;">&nbsp;</td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">1)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal1" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal1" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray"  TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;" 
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="16" AutoPostBack="true" OnTextChanged="Goal1_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success1" runat="server" UpdateMode="Conditional"><ContentTemplate>        
        <asp:TextBox ID="txbSuccess1" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="17" AutoPostBack="true" OnTextChanged="Success1_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date1" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate1" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="18" AutoPostBack="true" OnTextChanged="Date1_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>

<asp:Panel ID="Panel_Goal2" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal2" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">2)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal2" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal2" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="19" AutoPostBack="true" OnTextChanged="Goal2_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success2" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess2" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="20" AutoPostBack="true" OnTextChanged="Success2_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date2" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate2" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="21" AutoPostBack="true" OnTextChanged="Date2_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal3" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal3" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">3)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal3" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal3" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;" 
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="22" AutoPostBack="true" OnTextChanged="Goal3_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success3" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess3" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="23" AutoPostBack="true" OnTextChanged="Success3_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date3" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate3" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="24" AutoPostBack="true" OnTextChanged="Date3_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal4" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal4" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">4)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal4" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal4" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="25" AutoPostBack="true" OnTextChanged="Goal4_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success4" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess4" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="26" AutoPostBack="true" OnTextChanged="Success4_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date4" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate4" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="27" AutoPostBack="true" OnTextChanged="Date4_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal5" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal5" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">5)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal5" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal5" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="28" AutoPostBack="true" OnTextChanged="Goal5_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success5" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess5" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="29" AutoPostBack="true" OnTextChanged="Success5_TextChanged"/>
</ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date5" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate5" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="30" AutoPostBack="true" OnTextChanged="Date5_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal6" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal6" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">6)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal6" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal6" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="31" AutoPostBack="true" OnTextChanged="Goal6_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success6" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess6" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="32" AutoPostBack="true" OnTextChanged="Success6_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date6" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate6" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="33" AutoPostBack="true" OnTextChanged="Date6_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal7" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal7" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">7)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal7" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal7" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="34" AutoPostBack="true" OnTextChanged="Goal7_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success7" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess7" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="35" AutoPostBack="true" OnTextChanged="Success7_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date7" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate7" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="36" AutoPostBack="true" OnTextChanged="Date7_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal8" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal8" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">8)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal8" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal8" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="37" AutoPostBack="true" OnTextChanged="Goal8_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success8" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess8" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="38" AutoPostBack="true" OnTextChanged="Success8_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date8" runat="server" UpdateMode="Conditional"><ContentTemplate>        
        <asp:TextBox ID="txbDate8" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="39" AutoPostBack="true" OnTextChanged="Date8_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal9" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal9" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">9)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal9" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal9" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="40" AutoPostBack="true" OnTextChanged="Goal9_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success9" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess9" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="41" AutoPostBack="true" OnTextChanged="Success9_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date9" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate9" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="42" AutoPostBack="true" OnTextChanged="Date9_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

<asp:Panel ID="Panel_Goal10" Visible="false" runat="server">
<tr><td style="width:2%; vertical-align:top; text-align:right;"><asp:Button ID="Delete_Goal10" Text="X" ForeColor="#00ae4d" Width="15px" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td style="width:3%; vertical-align:top; text-align:right; font-weight:bold;">10)</td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Goal10" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbGoal10" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="43" AutoPostBack="true" OnTextChanged="Goal10_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Success10" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbSuccess10" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray" TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="44" AutoPostBack="true" OnTextChanged="Success10_TextChanged"/></ContentTemplate></asp:UpdatePanel></td>
    <td>
<asp:UpdatePanel ID="UpdatePanel_Date10" runat="server" UpdateMode="Conditional"><ContentTemplate>
        <asp:TextBox ID="txbDate10" runat="server" Width="99%" Font-Names="Calibri" Borderstyle="Solid" BorderColor="LightGray"  TextMode="MultiLine" Rows="6" Style="overflow:auto; word-wrap:normal; word-break:normal;"
             ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="45" AutoPostBack="true" OnTextChanged="Date10_TextChanged"/></ContentTemplate></asp:UpdatePanel></td></tr>
</asp:Panel>

</table>
</asp:Panel>  


  </td></tr>
</table>
<table style="width:100%;">
  <tr><td style="width:35%; text-align:left;"><asp:Button ID="BtnSubmit_Mgr" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" runat="server" Visible="false"/></td>
      <td style="text-align:center; vertical-align:bottom;"><asp:Button ID="btnSave1" Text="Save" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" runat="server" Visible="false"/></td>
      <td style="width:35%; text-align:right;"></td></tr></table>

<asp:Panel ID="Goal_Setting_Review" Visible="false" runat="server">


<table style="width:100%; border-collapse:collapse; border-spacing:0" border="1">

<tr><td style="background-color:lightgray;"></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:45%; font-family:Calibri; background-color:lightgray;">Goals
        <div id="divGoalText1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic; word-wrap: normal; word-break: normal;">What do I need to accomplish in order to support my department’s goals and CR’s goals?</div></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">
        <div id="divOldTitle1" runat="server">Success Measures or Milestones</div>
        <div id="divNewTitle1" runat="server">Key Results</div>
        <div id="divNewKey1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic; word-wrap: normal; word-break: normal;">How will I know that I’ve accomplished each goal?</div>
    </td>
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

</asp:Panel>

<table style="width:100%" border="0">
    <tr><td style="text-align:center;"><asp:label id="Sub_Empl_Review" runat="server" Font-Size="12pt" Font-Bold="true" ForeColor="#00ae4d" Text="" Font-Names="Calibri" visible="false"/></td></tr>
    <tr><td>
        <table style="width:100%" border="0">
            <tr><td style="text-align:center;" colspan="3">
                   <asp:Button ID="btnSub_Empl" runat="server" BackColor="Wheat" Font-Size="11pt" BorderStyle="None" Height="20px" CssClass="Button_StyleSheet" Text="Submit to Employee" Visible="false" /></td></tr>
             <tr><td style="text-align:center;" colspan="3">
                  <asp:Button ID="Empl_Review" runat="server" BackColor="Wheat" Font-Size="11pt" BorderStyle="None" Height="20px" CssClass="Button_StyleSheet" Text="Reviewed and Confirmed" Visible="false" /></td></tr>
             <tr><td style="text-align:left;"><asp:Button ID="Edit_Goals" runat="server" BackColor="Wheat" Font-Size="11pt" BorderStyle="None" Height="20px" CssClass="Button_StyleSheet" Text="" Visible="false" /></td>
                 <td style="text-align:center;"><asp:Button ID="btnRecall" runat="server" BackColor="Wheat" Font-Size="11pt" BorderStyle="None" Height="20px" CssClass="Button_StyleSheet" Text="" Visible="false" /></td>
                 <td style="text-align:right;"><asp:Button ID="Esign_Goals" runat="server" BackColor="Wheat" Font-Size="11pt" BorderStyle="None" Height="20px" CssClass="Button_StyleSheet" Text="" Visible="false" /></td></tr>
</table></td></tr>
</table>
            
<asp:Panel ID="Panel_Goals_Log" runat="server" Visible="false">
<table style="width:100%; text-align:center" border="0">
      <tr><td>&nbsp;</td></tr>
      <tr><td style="font-size:18px; font-weight:bold; font-family:Calibri;">Previous Goals record</td></tr>
</table>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="99%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet">
 <Columns>
<asp:BoundField DataField="Recall_Date" HeaderText="Reviewed Date" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="left" HeaderStyle-Width="12%"></asp:BoundField>    
<asp:TemplateField HeaderText="Goals" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40%" ItemStyle-VerticalAlign="Top"><ItemTemplate>
<asp:Label ID="Lbl_Goals" runat="server" Text='<%# Eval("Goals").ToString().Replace(Environment.NewLine, "<br/>")%>' /></ItemTemplate></asp:TemplateField> 
<asp:TemplateField HeaderText="MileStones" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"><ItemTemplate>
<asp:Label ID="Lbl_MileStones" runat="server" Text='<%# Eval("MileStones").ToString().Replace(Environment.NewLine, "<br/>")%>' /></ItemTemplate></asp:TemplateField> 
<asp:TemplateField HeaderText="TargetDate" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"><ItemTemplate>
<asp:Label ID="Lbl_TargetDate" runat="server" Text='<%# Eval("TargetDate").ToString().Replace(Environment.NewLine, "<br/>")%>' /></ItemTemplate></asp:TemplateField> 
<asp:BoundField DataField="Created" HeaderText="Creator" HeaderStyle-HorizontalAlign="left" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="10%" ></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</asp:Panel>

<table style="width:100%">
    <tr><td>&nbsp;</td></tr>
    
<asp:Panel ID="Panel_History" runat="server" Visible="false">
    <tr><td><hr style="align-self:center; width:200px; border-bottom:solid; border-top:solid; border-bottom-color:lightgreen; border-top-color:lightgreen;"/></td></tr>
    <tr><td style="text-align:center;"><asp:Button ID="BtnHistory" Text="Show Prior Goal History" runat="server" BackColor="Wheat" Font-Size="11pt" BorderStyle="None" Height="20px" CssClass="Button_StyleSheet" /></td></tr>
</asp:Panel>

    <tr><td>&nbsp;</td></tr>
    <tr><td style="text-align:center;"><asp:ImageButton ID="Img_Print1" runat="server" ImageUrl="../../Images/print.png" Style="width:70px"/></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td style="text-align:center;"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
                CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/></td></tr>
</table>
 </td><td>&nbsp;&nbsp;&nbsp;</td></tr>
   </table>
  </form>
 </body>
</html>
