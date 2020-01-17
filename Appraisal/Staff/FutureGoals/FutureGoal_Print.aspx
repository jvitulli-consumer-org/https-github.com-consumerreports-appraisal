<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FutureGoal_Print.aspx.vb" Inherits="Appraisal.FutureGoal_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Future Goals</title>
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
  
 </style>
<script lang="jascript" type="text/javascript" >
    //========================================================================
    function ChangeColor(i) {
        // Retrieve the element by its id 'i'
        document.getElementById(i).style.backgroundColor = "";
    }
    //========================================================================
    function setHeight(abc) {
        //abc.style.height = abc.scrollHeight  + "px";
        //var x = document.getElementsById("Goals_1");
        //x.style.height = 'auto;'
        //x.style.height = (x.scrollHeight + offset) + 'px';
        //  var charCode = (abc.which) ? abc.which : event.keyCode
        //  if (charCode == 10 || charCode == 13)
        //  {
        //      abc.style.height = abc.scrollHeight + "px";
        //}
        //alert(charCode)
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
<body onload="window.print()">

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
  <tr><!--<td class="style6">&nbsp;</td>-->
      <td class="Style1"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td></tr>
  <tr><!--<td class="style6">&nbsp;</td>--><td class="Style1"><u>FY<asp:Label ID="Goal_Year" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> 
        Goal-Setting Form (06/01/<asp:Label ID="Goal_Year1" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> - 05/31/<asp:Label ID="Goal_Year2" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td></tr>
  <tr><!--<td class="style6">&nbsp;</td>--><td>
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
                <td style="font-family: Calibri;"><b>&nbsp;</b></td>
                <td><asp:Label ID="lblUP_MGT_NAME" runat="server" Font-Names="Calibri"/></td>
                <td style="font-family: Calibri;">&nbsp;&nbsp;</td><!--Approved:-->
                <td><asp:Label ID="LblUP_MGT_Appr" runat="server" Font-Names="Calibri" Visible="false"/></td></tr>
            <tr><td style="font-family: Calibri;"><b>Hire Date:</b></td>
                <td><asp:label id="lblEmpl_HIRE" runat="server" Font-Names="Calibri"/></td>
                <td style="font-family: Calibri;"><b>Human Resources Generalist:</b></td>
                <td><asp:label id="lblHR_NAME" runat="server" Font-Names="Calibri"/></td>
                <td style="font-family: Calibri;">Approved:&nbsp;&nbsp;</td>
                <td><asp:Label ID="LblHR_Appr" runat="server" Font-Names="Calibri" /></td></tr></table>
  </td></tr>
  <tr><!--<td class="style6">&nbsp;</td>--><td>
   <asp:Panel ID="ReviseComments" Visible="false" runat="server">&nbsp;
       <table style="width:100%; border-color:blue; border-collapse:collapse; border-spacing:0;" border="1"><tr><td><%=lblComments.Text%></td></tr></table>&nbsp;</asp:Panel>
</td></tr>

<tr><!--<td class="style6">&nbsp;</td>--><td>

<table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="0">

<tr><td>
 <table border="0" style="width:100%">
<!--<tr><td>&nbsp;</td></tr>-->
     <tr><td style="font-family:Calibri; font-size:medium;">Enter SMART Goals 
            (<strong>S</strong>pecific, <strong>M</strong>easurable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to 
            focus employees on achieving important, tangible outcomes that contribute to the achievement of CR&#39;s  strategic plan. Summarize the goals agreed to with the employee.
         <div id="divNewtext" runat="server" style="font-size:small; font-style:italic">Please click 
            <a href="https://docs.google.com/presentation/d/1LIm9LOScdiizJcXARapml7-gj2IkJfq-MtQgkBFNHho/edit#slide=id.g1f6e4888fe_0_0" target="_blank">here</a> for a SMART goal example.</div>

         </td></tr></table>

<asp:Panel ID="Goal_Setting_Edit" Visible="false" runat="server">

<table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="1">
<tr><td style="background-color:lightgray;"></td>
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

<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:2%; font-family:Calibri;">1)</td>
    <td style="vertical-align:top;"><asp:Label ID="lblGoal1" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal1.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblSuccess1" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess1.Text%></td>
    <td style="vertical-align:top;"><asp:Label ID="lblDate1" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate1.Text%></td></tr>

<asp:Panel ID="Panel_Goal2" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">2)</td>
    <td><asp:Label ID="lblGoal2" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal2.Text%></td>
    <td><asp:Label ID="lblSuccess2" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess2.Text%></td>
    <td><asp:Label ID="lblDate2" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate2.Text%></td></tr>
</asp:Panel>
<asp:Panel ID="Panel_Goal3" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">3)</td>
    <td><asp:Label ID="lblGoal3" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal3.Text%></td>
    <td><asp:Label ID="lblSuccess3" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess3.Text%></td>
    <td><asp:Label ID="lblDate3" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate3.Text%></td></tr>
</asp:Panel>
<asp:Panel ID="Panel_Goal4" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">4)</td>
    <td><asp:Label ID="lblGoal4" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal4.Text%></td>
    <td><asp:Label ID="lblSuccess4" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess4.Text%></td>
    <td><asp:Label ID="lblDate4" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate4.Text%></td></tr>
</asp:Panel>
<asp:Panel ID="Panel_Goal5" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">5)</td>
    <td><asp:Label ID="lblGoal5" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal5.Text%></td>
    <td><asp:Label ID="lblSuccess5" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess5.Text%></td>
    <td><asp:Label ID="lblDate5" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate5.Text%></td></tr>
</asp:Panel>
<asp:Panel ID="Panel_Goal6" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">6)</td>
    <td><asp:Label ID="lblGoal6" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal6.Text%></td>
    <td><asp:Label ID="lblSuccess6" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess6.Text%></td>
    <td><asp:Label ID="lblDate6" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate6.Text%></td></tr>
</asp:Panel>
<asp:Panel ID="Panel_Goal7" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">7)</td>
    <td><asp:Label ID="lblGoal7" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal7.Text%></td>
    <td><asp:Label ID="lblSuccess7" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess7.Text%></td>
    <td><asp:Label ID="lblDate7" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate7.Text%></td></tr>
</asp:Panel>
<asp:Panel ID="Panel_Goal8" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">8)</td>
    <td><asp:Label ID="lblGoal8" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal8.Text%></td>
    <td><asp:Label ID="lblSuccess8" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess8.Text%></td>
    <td><asp:Label ID="lblDate8" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate8.Text%></td></tr>
</asp:Panel>
<asp:Panel ID="Panel_Goal9" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">9)</td>
    <td><asp:Label ID="lblGoal9" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal9.Text%></td>
    <td><asp:Label ID="lblSuccess9" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess9.Text%></td>
    <td><asp:Label ID="lblDate9" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate9.Text%></td></tr>
</asp:Panel>
<asp:Panel ID="Panel_Goal10" Visible="false" runat="server">
<tr><td style="vertical-align:top; text-align:center; font-weight:bold; width:3%; font-family:Calibri;">10)</td>
    <td><asp:Label ID="lblGoal10" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblGoal10.Text%></td>
    <td><asp:Label ID="lblSuccess10" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblSuccess10.Text%></td>
    <td><asp:Label ID="lblDate10" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=lblDate10.Text%></td></tr>
</asp:Panel>
</table>
</asp:Panel>  

  </td></tr>
            </table>

<asp:Panel ID="Goal_Setting_Edit1" Visible="false" runat="server">
  <table style="text-align:center; width:100%;">
    <tr><td style="width:35%;"><asp:Button ID="BtnSubmit_UpperMgr" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" Font-Bold="true" runat="server"/></td>
        <td><asp:Button ID="btnSave1" Text="Save" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" runat="server" Visible="false"/></td>
        <td style="width:35%;"><asp:Button ID="btnSave" Text="Save" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Height="20px" CssClass="Button_StyleSheet" runat="server"/></td></tr>
          </table>
</asp:Panel>

<asp:Panel ID="Goal_Setting_Review" Visible="false" runat="server">

<table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="1">
<tr><td style="background-color:lightgray;"></td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:45%; font-family:Calibri; background-color:lightgray;"">Goals</td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;"">Key Results</td>
    <td style="text-align:center; font-size:large; font-weight:bold; width:10%; font-family:Calibri; background-color:lightgray;"">Target<br />Completion Date</td></tr>

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

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
    Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet" OnRowDataBound="GridView1_RowDataBound">
 <Columns>
<asp:BoundField DataField="Recall_Date" HeaderText="Reviewed Date" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="left" ></asp:BoundField>    
<asp:BoundField DataField="Goals" HeaderText="Goals" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="35%"></asp:BoundField>
<asp:BoundField DataField="MileStones" HeaderText="Success Measures or Milestones" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="35%"></asp:BoundField>
<asp:BoundField DataField="TargetDate" HeaderText="Target Completion Date" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="7%" ></asp:BoundField>
<asp:BoundField DataField="Created" HeaderText="Manager" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" HeaderStyle-Width="10%" ></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>

</asp:Panel>


<table style="width:100%">
    <tr><td style="text-align:center;">
        &nbsp;</td></tr>
    <tr><td style="text-align:center;">
        &nbsp;</td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td style="text-align:center;">
        </td></tr>
     <tr><td style="text-align:center;">&nbsp;</td></tr>
    <tr><td>&nbsp;</td></tr>
<!--<tr><td style="text-align:center;"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" CssClass="Button_StyleSheet no-print" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/></td></tr>-->
</table>
 </td><!--<td class="style6">&nbsp;</td>--></tr>
  </table>



  </form>

</body>
</html>
