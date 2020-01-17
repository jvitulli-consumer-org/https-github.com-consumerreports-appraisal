<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="myEmpGoals.aspx.vb" Inherits="Appraisal.myEmpGoals" MaintainScrollPositionOnPostback="true" smartNavigation="true" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
          white-space:nowrap;
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
</style>

<script lang="jascript" type="text/javascript" >
    function ChangeColor(i) {
        // Retrieve the element by its id 'i'
        document.getElementById(i).style.backgroundColor = "";
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
    //Force refresh after x minutes.
    var initialTime = new Date();
    var checkSessionTimeout = function () {
        var minutes = Math.abs((initialTime - new Date()) / 1000 / 60);
        if (minutes > 20) {
            setInterval(function () { location.href = '..\..\Default.aspx' }, 5000)
        }
    };
    setInterval(checkSessionTimeout, 1000);
    //========================================================================
</script>

</head>
<body>

<form id="form1" runat="server">

<asp:label id="LblEligible_Full_Review" runat="server" Text="" Visible="false"/>
<asp:label id="lblEMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblLogin_EMPLID" runat="server" Text="" Visible="false"/>
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

<table class="Style0" border="0">
  <tr><td class="style6">&nbsp;</td>
      <td class="Style1"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td><td class="style6">&nbsp;</td></tr>
  <tr><td class="style6">&nbsp;</td>
      <td class="Style1"><u>FY<asp:Label ID="Goal_Year" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> 
        Goal-Setting Form (06/01/<asp:Label ID="Goal_Year1" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> - 05/31/<asp:Label ID="Goal_Year2" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td>
      <td class="style6">&nbsp;</td></tr>
  <tr><td class="style6">&nbsp;</td>
      <td>
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
      
      <td class="style6">&nbsp;</td></td></tr>

<asp:Panel ID="Panel_COM" Visible="false" runat="server">  
    <tr><td class="style6">&nbsp;&nbsp;</td>
      <td style="vertical-align:top; color:blue; font-family:Calibri; border-color:green; border-style:dotted;"><asp:Label ID="Lbl_COM" runat="server" Width="100%" Visible="false"/><%=Lbl_COM.Text%></td>
      <td class="style6">&nbsp;&nbsp;</td></tr>
</asp:Panel> 

  <tr><td class="style6">&nbsp;</td><td>

<table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="0">

<tr><td style="font-family:Calibri; font-size:small;">
<!--    Enter SMART Goals (<strong>S</strong>pecific, <strong>M</strong>easurable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to 
    focus on achieving important, tangible outcomes that contribute to the achievement of CR&#39;s  strategic plan.&nbsp;&nbsp;Please click 
    <a href="https://docs.google.com/presentation/d/1LIm9LOScdiizJcXARapml7-gj2IkJfq-MtQgkBFNHho/edit#slide=id.g1f6e4888fe_0_0" target="_blank">here</a> for a SMART goal example.-->
<table width="100%">
    <tr><td width="2%">&#8226;</td><td>After discussing your fiscal year goals with your manager, please enter them below.  Once completed, you will submit them to your manager for his/her review.</td></tr>
    <tr><td width="2%">&#8226;</td><td>If approved, you’ll receive a confirmation email. </td></tr>
    <tr><td width="2%">&#8226;</td><td>If your manager would like to make changes, you will receive his/her comments via email You are encouraged to have a conversation with your manager if you have questions or concerns about the edits, and can then make the changes in the system and resubmit them.</td></tr>
    <tr><td width="2%">&#8226;</td><td>Please remember that you can modify your goals at any time throughout the year. If you make changes, your manager will be notified.</td></tr>
</table>

    </td></tr>

<asp:Panel ID="Goal_Setting_Edit" Visible="false" runat="server">

<table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="1">
<tr><td style="background-color:lightgray;"></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:45%; font-family:Calibri; background-color:lightgray;">Goals
        <div id="divGoalText" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">What do I need to accomplish in order to support my department’s goals and CR’s goals?</div></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">Key Results
        <div id="divNewKey" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">How will I know that I’ve accomplished each goal?</div></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:10%; font-family:Calibri; background-color:lightgray;">Target<br />Completion Date
        <div id="divNewtarget" runat="server" style="font-size:small; font-weight:lighter; font-style:italic">By when do I need to accomplish each goal?</div></td></tr>
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:2%; font-family:Calibri;">1)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal1" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal1.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess1" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess1.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate1" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate1.Text%></td></tr>
<asp:Panel ID="Panel_Goal2" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">2)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal2" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal2.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess2" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess2.Text%></td>
    <td><asp:Label ID="lblDate2" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate2.Text%></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal3" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">3)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal3" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal3.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess3" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess3.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate3" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate3.Text%></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal4" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">4)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal4" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal4.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess4" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess4.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate4" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate4.Text%></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal5" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">5)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal5" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal5.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess5" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess5.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate5" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate5.Text%></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal6" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">6)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal6" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal6.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess6" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess6.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate6" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate6.Text%></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal7" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">7)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal7" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal7.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess7" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess7.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate7" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate7.Text%></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal8" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">8)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal8" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal8.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess8" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess8.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate8" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate8.Text%></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal9" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">9)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal9" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal9.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess9" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess9.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate9" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate9.Text%></td></tr></asp:Panel>
<asp:Panel ID="Panel_Goal10" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">10)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal10" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal10.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess10" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess10.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate10" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate10.Text%></td></tr></asp:Panel>



<table style="width:100%" border="0">
    <tr><td colspan="4" style="text-align:center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>

<asp:Panel ID="Panel_Revise" runat="server" Visible="false">        
    <tr><td colspan="4" style="text-align:left; font-weight:bolder; font-family:Calibri;">Comments <font style="font-weight:normal; font-style:italic;">(only provide comments when requesting edit)</font></td></tr>
    <tr><td colspan="4" style="text-align:center">
        <asp:TextBox ID="TxtComments" runat="server" Width="100%" Borderstyle="Solid" BorderWidth="2px" Font-Names="Calibri" runat="server" TextMode="MultiLine" Rows="4" 
            Style="overflow:auto;" ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Comments_TextChanged" placeholder="Please type comments"/></td></tr>
</asp:Panel>
    <tr><td style="width:20%;">&nbsp;&nbsp;</td>
        <td style="text-align:center; width:30%;"><asp:Button ID="Discuss" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="12pt" Height="20px" Width="100px" CssClass="Button_StyleSheet" Text="Revise"/></td>
        <td style="text-align:center; width:30%;"><asp:Button ID="Approve" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="12pt" Height="20px" Width="100px" CssClass="Button_StyleSheet" Text="Approve"/></td>
        <td style="width:20%;">&nbsp;&nbsp;</td></tr>


    <tr><td colspan="4" style="text-align:center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>

<asp:Panel ID="Panel_History" runat="server" Visible="false">    
    <tr><td colspan="4" style="text-align:center"><hr style="align-self:center; width:600px; border-bottom:solid; border-top:solid; border-bottom-color:lightgreen; border-top-color:lightgreen;"/></td></tr>
    <tr><td colspan="4" style="text-align:center"><asp:Button ID="btnHistory" Text="Show Prior Goal History" runat="server" BackColor="Wheat" Font-Size="11pt" BorderStyle="None" Height="20px" CssClass="Button_StyleSheet" /></td></tr>
</asp:Panel>

    <tr><td colspan="4" style="text-align:center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
    <tr><td colspan="4" style="text-align:center"><asp:ImageButton ID="Img_Print1" runat="server" ImageUrl="../../Images/print.png" Style="width:70px"/></td></tr>
    <tr><td colspan="4" style="text-align:center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
    <tr><td colspan="4" style="text-align:center"><asp:Button ID="Button2" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
                CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/></td></tr>
    <tr><td colspan="4" style="text-align:center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
</table>

</table>

</asp:Panel>  


  </td></tr>
</table>

</td></tr>

</table>


  </form>
</body>
</html>
