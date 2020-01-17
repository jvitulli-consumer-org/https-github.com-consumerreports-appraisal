<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FutureGoal.aspx.vb" Inherits="Appraisal.FutureGoal" MaintainScrollPositionOnPostback="true" smartNavigation="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server" >
    <title>Goal-Setting Form</title>

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
     .auto-style1 {
            width: 50%;
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
           if ((document.getElementById("<%=lblEMPLID.ClientID%>").innerHTML - document.getElementById("<%=lblLogin_EMPLID.ClientID%>").innerHTML == 0) || (document.getElementById("<%=lblLogin_EMPLID.ClientID%>") !== 'undefined')) {
               //alert("Manager goal")
           } else {
               //alert('employee '+ document.getElementById("<%'=lblEMPLID.ClientID%>").innerHTML +' manager '+ document.getElementById("<%'=lblLogin_EMPLID.ClientID%>").innerHTML !== 'undefined')
               document.getElementById("<%=lblMessage.ClientID%>").style.display = "none";
               document.getElementById("<%=lblMessage1.ClientID %>").style.display = "none";
           }
        }, seconds * 1000)
    }
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

<asp:Timer ID="Timer1" runat="server" Interval="30000" ontick="Timer1_Tick"></asp:Timer> 
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


<table class="Style0" border="0">
  <tr><td class="style6">&nbsp;</td><td class="Style1"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td>
    
        <td class="style6" rowspan="4">
            <asp:Button ID="lblMessage1" runat="server" ForeColor="Green" BackColor="White" BorderStyle="None" Font-Bold="true" Font-Size="12pt" Width="100px" Text="" CssClass="Button_StyleSheet;" style="position:fixed;"/><br />
            <asp:Button ID="btnSave" Text="Save Records" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" runat="server" style="position:fixed;"/></td></tr>


  <tr><td class="style6"></td><td class="Style1"><u>FY<asp:Label ID="Goal_Year" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> 
        Goal-Setting Form (06/01/<asp:Label ID="Goal_Year1" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> - 05/31/<asp:Label ID="Goal_Year2" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td></tr>
  <tr><td class="style6">&nbsp;</td><td>
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
                <td class="style5">&nbsp;&nbsp;</td><!--Approved:-->
                <td><asp:Label ID="LblUP_MGT_Appr" runat="server" Font-Names="Calibri" Visible="false"/></td></tr>
            <tr><td class="auto-style2"><b>Hire Date:</b></td>
                <td><asp:label id="lblEmpl_HIRE" runat="server" Font-Names="Calibri"/></td>
                <td><b>Human Resources Generalist:</b></td>
                <td><asp:label id="lblHR_NAME" runat="server" Font-Names="Calibri"/></td>
                <td class="style5">Approved:&nbsp;&nbsp;</td>
                <td><asp:Label ID="LblHR_Appr" runat="server" Font-Names="Calibri" /></td></tr></table>
  </td></tr>
  <tr><td class="style6">&nbsp;</td><td>
   <asp:Panel ID="ReviseComments" Visible="false" runat="server">&nbsp;
       <table style="width:100%; border-color:blue; border-collapse:collapse; border-spacing:0;" border="1"><tr><td><%=lblComments.Text%></td></tr></table>&nbsp;</asp:Panel>
</td></tr>

<tr><td class="style6">&nbsp;</td><td>
<table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="0">
 <tr><td>

 <table border="0" style="width:100%">
<!--     <tr><td>&nbsp;</td></tr>-->
     <tr><td style="font-family:Calibri; font-size:medium;">Enter SMART Goals 
            (<strong>S</strong>pecific, <strong>M</strong>easurable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to 
            focus employees on achieving important, tangible outcomes that contribute to the achievement of CR&#39;s  strategic plan. Summarize the goals agreed to with the employee.
         <div id="divNewtext" runat="server" style="font-size:small; font-style:italic">Please click 
            <a href="https://docs.google.com/presentation/d/1LIm9LOScdiizJcXARapml7-gj2IkJfq-MtQgkBFNHho/edit#slide=id.g1f6e4888fe_0_0" target="_blank">here</a> for a SMART goal example.</div>
         </td></tr></table>

<asp:Panel ID="Goal_Setting_Edit" Visible="false" runat="server">

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

<table style="width:100%">
    <tr><td><asp:Button ID="btnNew_Goal" runat="server" BackColor="Wheat" BorderStyle="None" Font-Names="Calibri" Font-Size="11pt" width="100px" Text="Add New Goal" CssClass="Button_StyleSheet"/></td></tr></table>

<table style="width:100%">
<tr><td colspan="2" style="width:4% "></td>
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
              ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);' TabIndex="16" AutoPostBack="true" OnTextChanged="Goal1_TextChanged" />
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
</asp:Panel>  
  </td></tr>
            </table>

<asp:Panel ID="Goal_Setting_Edit1" Visible="false" runat="server">
  <table style="width:100%;">
    <tr><td style="width:35%; text-align:left;"><asp:Button ID="BtnSubmit_UpperMgr" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" Font-Bold="true" runat="server"/></td>
        <td style="text-align:center; vertical-align:bottom;"><asp:Button ID="btnSave1" Text="Save" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" runat="server" Visible="false"/></td>
        <td style="width:35%; text-align:right;"><asp:Button ID="lblMessage" runat="server" ForeColor="Green" BackColor="White" BorderStyle="None" Font-Bold="true" Font-Size="12px" Width="100px" Text="" CssClass="Button_StyleSheet;"/><br />
            <asp:Button ID="btnSave2" Text="Save Records" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" runat="server"/></td></tr>
          </table>
</asp:Panel>

<asp:Panel ID="Goal_Setting_Review" Visible="false" runat="server">

<table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="1">
<tr><td style="background-color:lightgray;"></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:45%; font-family:Calibri; background-color:lightgray;">Goals
        <div id="divGoalText1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">What do I need to accomplish in order to support my department’s goals and CR’s goals?</div>
    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">
        <div id="divOldTitle1" runat="server" >Success Measures or Milestones</div>
        <div id="divNewTitle1" runat="server" >Key Results</div>
        <div id="divNewKey1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">How will I know that I’ve accomplished each goal?</div>
    </td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:10%; font-family:Calibri; background-color:lightgray;">Target<br />Completion Date
        <div id="divNewtarget1" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">By when do I need to accomplish each goal?</div>
    </td></tr>

<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">1)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit11" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit11.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit11" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit11.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit11" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit11.Text%></td></tr>

<asp:Panel ID="Panel_FutureGoal_Review2" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">2)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit12" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit12.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit12" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit12.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit12" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit12.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Review3" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">3)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit13" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit13.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit13" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit13.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit13" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit13.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Review4" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">4)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit14" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit14.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit14" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit14.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit14" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit14.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Review5" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">5)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit15" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit15.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit15" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit15.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit15" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit15.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Review6" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">6)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit16" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit16.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit16" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit16.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit16" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit16.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Review7" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">7)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit17" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit17.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit17" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit17.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit17" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit17.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Review8" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">8)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit18" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit18.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit18" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit18.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit18" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit18.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Review9" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">9)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit19" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit19.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit19" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit19.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit19" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit19.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Review10" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">10)</td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Goal_Edit20" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Goal_Edit20.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Succ_Edit20" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Succ_Edit20.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="FUT_Date_Edit20" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=FUT_Date_Edit20.Text%></td></tr></asp:Panel>
</table>

</asp:Panel>


<asp:Panel ID="Goal_Discussion" Visible="false" runat="server">
<table style="width:100%">
    <tr><td>&nbsp;</td></tr>
    <tr><td><asp:Label ID="Disc_Com" text="Send suggested revisions to " runat="server" CssClass="Label_StyleSheet"/> </td></tr>
    <tr><td style="text-align:center;">
        <asp:TextBox ID="DiscussionComments" runat="server" Width="99%" TextMode="MultiLine" Rows="3" style="overflow:auto; border-color:black;" CssClass="TextBox_StyleSheet" 
            onkeyup="SetButtonStatus(this, 'target');" /></td></tr>
    <tr><td>
        <table style="width:100%" border="0">
            <tr><td style="text-align:left" class="auto-style1">&nbsp;
                    <asp:Button ID="Discuss" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" text="Revise"/></td>
                <td style="width:50%; text-align:right">
                    <asp:Button ID="Generalist" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" Text="Submit for review to" Visible="false"/>
                    <asp:Button ID="Approve" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" Text="Approve" Visible="false"/>&nbsp;&nbsp;</td></tr>
            <tr><td colspan="2" style="text-align:center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
        </table></td></tr>
    
    <tr><td>&nbsp;</td></tr>
</table>
</asp:Panel>

<table style="width:100%">
    <tr><td style="text-align:center;"><asp:label id="Sub_Empl_Review" runat="server" Font-Size="12pt" Font-Bold="true" ForeColor="#00ae4d" Text="" Font-Names="Calibri" visible="false"/></td></tr>
    <tr><td>&nbsp;</td></tr>
</table>
            
<asp:Panel ID="Panel_Goals_Log" runat="server" Visible="false">

<table style="width:100%; text-align:center" border="0">
      <tr><td>&nbsp;</td></tr>
      <tr><td style="font-size:18px; font-weight:bold; font-family:Calibri;">Previous Goals record</td></tr>
</table>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet"
    OnRowDataBound="GridView1_RowDataBound">
 <Columns>
<asp:BoundField DataField="Recall_Date" HeaderText="Reviewed Date" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="left"></asp:BoundField>    
<asp:BoundField DataField="Goals" HeaderText="Goals" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="35%"></asp:BoundField>
<asp:BoundField DataField="MileStones" HeaderText="Key Results" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="35%"></asp:BoundField>
<asp:BoundField DataField="TargetDate" HeaderText="Target Completion Date" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="7%" ></asp:BoundField>
<asp:BoundField DataField="Created" HeaderText="Manager" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="10%" ></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>

</asp:Panel>


<table style="width:100%">
    <tr><td style="text-align:center;">
        <asp:Button ID="btnSub_Empl" runat="server" BackColor="Wheat" Font-Size="11pt" BorderStyle="None" Height="20px" CssClass="Button_StyleSheet" Text="Submit to Employee" Visible="false" /></td></tr>
    <tr><td style="text-align:center;">
        <asp:Button ID="Empl_Review" runat="server" BackColor="Wheat" Font-Size="11pt" BorderStyle="None" Height="20px" CssClass="Button_StyleSheet" Text="Reviewed and Confirmed" Visible="false" /></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td style="text-align:center;">
       <!-- <a href="Guild_FutureGoal_Print.aspx?Token=<%=Request.QueryString("Token")%>" target="_blank" style="font-size:medium; font-family:Calibri;" title="Please save data first">Print / Preview</a>-->
        </td></tr>
     <tr><td style="text-align:center;"><asp:ImageButton ID="Img_Print1" runat="server" ImageUrl="../../Images/print.png" Style="width:70px"/></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td style="text-align:center;"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
                CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/></td></tr>
</table>
 </td></tr>
  </table>



  </form>
</body>
</html>
