<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Guild_FutureGoal.aspx.vb" Inherits="Appraisal.Guild_FutureGoal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Goal-Setting Form</title>
    <style type="text/css">
       .Style1 {
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
          font-family:Calibri;
          height: 30px;
          }
       .Style3 {
          font-size:large;
          font-weight:bold;
          font-family:Calibri;
          color: #00ae4d;
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

</script>

</head>

<body>

<form id="form1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<asp:label id="LblEligible_Full_Review" runat="server" Text="" Visible="false"/>
<asp:label id="lblEMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblLogin_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblGUILD_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblFirst_SUP_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblFirst_SUP_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblHR_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblHR_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblFlag" runat="server" Text="" Visible="false"/>
<asp:label id="lblComments" runat="server" Text="" Visible="false"/> 


<table style="width:100%;" border="0">
    <tr><td style="width:50px;">&nbsp;</td>
        <td>
            <table border="0" style="width:100%">
                <tr><td style="text-align:center;"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td></tr>
                <tr><td class="Style1"><u>Goal-Setting Form - 10/01/15 - 05/31/16</u></td></tr>
                <tr><td>
                        <table border="0" style="width:100%">
                            <tr><td class="style4"><strong>Name:</strong></td>
                                <td class="style7"><asp:label id="lblEMPLOYEE_NAME" runat="server" Text="" Font-Names="Calibri"/></td>
                                <td class="style12"><b>Manager Name:</b></td>
                                <td class="style8"><asp:label id="LblMgr_NAME" runat="server" Text="" Font-Names="Calibri"/>&nbsp;</td>
                                <td style="text-align:right; font-family:Calibri;">&nbsp; Approved:&nbsp;</td><td><asp:Label ID="LblFirst_Mgr_Appr" runat="server" Font-Names="Calibri"/></td> </tr>
                                <tr><td class="style4"><strong>Title:</strong></td>
                                <td class="style7"><asp:label id="lblEMPLOYEE_TITLE" runat="server" Font-Names="Calibri"/></td>
                                <td class="style12"><b>2nd Level Manager Name:&nbsp;</b></td>
                                <td class="style8"><asp:Label ID="lblMGR_UP_NAME" runat="server" Text="" Font-Names="Calibri"/></td>
                                <td style="text-align:right; width:100px; font-family:Calibri;">Approved:&nbsp;</td><td><asp:Label ID="LblSec_Mgr_Appr" runat="server" Font-Names="Calibri"/>
                                    </td></tr>
                            <tr><td class="style4"><strong>Department:</strong></td>
                                <td class="style7"><asp:label id="lblEMPLOYEE_DEPT" runat="server" Text="" Font-Names="Calibri" /></td>
                                <td class="style12"><b>Human Resources Generalist:&nbsp;</b></td>
                                <td class="style8"><asp:Label ID="lblGENERALIST_NAME" runat="server" Text="" Font-Names="Calibri"/></td>
                                <td style="text-align:right; font-family:Calibri;">Approved:&nbsp;</td><td><asp:Label ID="LblHR_Appr" runat="server" Font-Names="Calibri"/>
                                </td></tr>
                            <tr><td class="style4"><strong>Hire Date:</strong></td>
                                <td class="style7"><asp:label id="lblEMPLOYEE_HIRE" runat="server" Text="" Font-Names="Calibri"/></td><td>&nbsp;</td>
                                <td class="style8">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></table>
   
                    </td></tr>
   <tr><td>
<asp:Panel ID="ReviseComments" Visible="false" runat="server">
&nbsp;
<table style="width:100%; border-color:blue; border-collapse:collapse; border-spacing:0;" border="1">    <tr><td><%=lblComments.Text%></td></tr></table>
&nbsp;
</asp:Panel>

</td></tr>
<tr><td>&nbsp;</td></tr>
    <tr><td>&nbsp;</td></tr>    
<!--Future Rating Criteria/Goals-->
<tr><td style="font-family:Calibri; font-size:medium;"><u style="color: #00ae4d; font-weight:bold; font-size:20px; font-family:Calibri;">Rating Criteria:</u><br /> 
Enter in the Key Task and description information in the space provided below. Key Task information should be extracted from employee's job description.</td></tr>

<asp:Panel ID="Future_Tasks" runat="server" Visible="false">

<tr><td><asp:Button ID="BtnFuture_Task" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Text="Add New Task" CssClass="Button_StyleSheet"/></td></tr>                 
<tr><td>

<table border="1" style="width:100%; border-color:#e5eaed; border-collapse:collapse; border-spacing:0; "><!--1) Key Future Task/Description-->
<tr><td class="style16">&nbsp;</td><td class="style15">1) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask1" runat="server" Width="99%"  Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr>

<asp:Panel ID="Panel2" runat="server" Visible="false"><!--2) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del2" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">2) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask2" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel3" runat="server" Visible="false"><!--3) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del3" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">3) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask3" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel4" runat="server" Visible="false"><!--4) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del4" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">4) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask4" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel5" runat="server" Visible="false"><!--5) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del5" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">5) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask5" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel6" runat="server" Visible="false"><!--6) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del6" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">6) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask6" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel7" runat="server" Visible="false"><!--7) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del7" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">7) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask7" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel8" runat="server" Visible="false"><!--8) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del8" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">8) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask8" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel9" runat="server" Visible="false"><!--9) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del9" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">9) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask9" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel10" runat="server" Visible="false"><!--10) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del10" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">10) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask10" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel11" runat="server" Visible="false"><!--11) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del11" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">11) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask11" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel12" runat="server" Visible="false"><!--12) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del12" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">12) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask12" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel13" runat="server" Visible="false"><!--13) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del13" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">13) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask13" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel14" runat="server" Visible="false"><!--14) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del14" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">14) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask14" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel15" runat="server" Visible="false"><!--15) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del15" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">15) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask15" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel16" runat="server" Visible="false"><!--16) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del16" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">16) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask16" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel17" runat="server" Visible="false"><!--17) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del17" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">17) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask17" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel18" runat="server" Visible="false"><!--18) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del18" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">18) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask18" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel19" runat="server" Visible="false"><!--19) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del19" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">19) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask19" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel20" runat="server" Visible="false"><!--20) Key Future Task/Description-->
<tr><td class="style16"><asp:Button ID="Fut_Del20" Text="X" ForeColor="#00ae4d" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server" CssClass="Button_StyleSheet"/></td>
    <td class="style15">20) Key Task Description:</td>
    <td><asp:TextBox ID="Fut_KeyTask20" runat="server" Width="99%" Borderstyle="None" BorderColor="LightGray" TextMode="MultiLine" Rows="5" class="TextBox_StyleSheet" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>
</table>
</td></tr>    
</asp:Panel>

<asp:Panel ID="Future_Tasks_Review" runat="server" Visible="false">
<tr><td>
<table style="width:100%; border-color:black; border-collapse:collapse; border-spacing:0;" border="1"><!--1) Key Future Task/Description-->
<tr><td class="style15">1) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review1" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review1.Text%></td></tr>

<asp:Panel ID="Panel_Tasks_Review2" runat="server" Visible="false"><!--2) Key Future Task/Description-->
<tr><td class="style15">2) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review2" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review2.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review3" runat="server" Visible="false"><!--3) Key Future Task/Description-->
<tr><td class="style15">3) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review3" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review3.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review4" runat="server" Visible="false"><!--4) Key Future Task/Description-->
<tr><td class="style15">4) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review4" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review4.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review5" runat="server" Visible="false"><!--5) Key Future Task/Description-->
<tr><td class="style15">5) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review5" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review5.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review6" runat="server" Visible="false"><!--6) Key Future Task/Description-->
<tr><td class="style15">6) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review6" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review6.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review7" runat="server" Visible="false"><!--7) Key Future Task/Description-->
<tr><td class="style15">7) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review7" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review7.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review8" runat="server" Visible="false"><!--8) Key Future Task/Description-->
<tr><td class="style15">8) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review8" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review8.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review9" runat="server" Visible="false"><!--9) Key Future Task/Description-->
<tr><td class="style15">9) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review9" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review9.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review10" runat="server" Visible="false"><!--10) Key Future Task/Description-->
<tr><td class="style15">10) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review10" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review10.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review11" runat="server" Visible="false"><!--11) Key Future Task/Description-->
<tr><td class="style15">11) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review11" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review11.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review12" runat="server" Visible="false"><!--12) Key Future Task/Description-->
<tr><td class="style15">12) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review12" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review12.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review13" runat="server" Visible="false"><!--13) Key Future Task/Description-->
<tr><td class="style15">13) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review13" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review13.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review14" runat="server" Visible="false"><!--14) Key Future Task/Description-->
<tr><td class="style15">14) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review14" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review14.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review15" runat="server" Visible="false"><!--15) Key Future Task/Description-->
<tr><td class="style15">15) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review15" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review15.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review16" runat="server" Visible="false"><!--16) Key Future Task/Description-->
<tr><td class="style15">16) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review16" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review16.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review17" runat="server" Visible="false"><!--17) Key Future Task/Description-->
<tr><td class="style15">17) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review17" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review17.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review18" runat="server" Visible="false"><!--18) Key Future Task/Description-->
<tr><td class="style15">18) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review18" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review18.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review19" runat="server" Visible="false"><!--19) Key Future Task/Description-->
<tr><td class="style15">19) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review19" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review19.Text%></td></tr></asp:Panel>

<asp:Panel ID="Panel_Tasks_Review20" runat="server" Visible="false"><!--20) Key Future Task/Description-->
<tr><td class="style15">20) Key Task Description:</td><td><asp:Label ID="Fut_KeyTask_Review20" runat="server" Width="100%" Font-Names="Calibri" Visible="false"/><%=Fut_KeyTask_Review20.Text%></td></tr></asp:Panel>
</table>

</td></tr>    

</asp:Panel>

<tr><td><hr /></td></tr>
<tr><td>

 <table border="0" style="width:100%">
<!--     <tr><td>&nbsp;</td></tr>-->
     <tr><td style="font-family:Calibri; font-size:medium;"><u style="color: #00ae4d; font-weight:bold; font-size:20px; font-family:Calibri;">SMART Goals:</u><br />Enter SMART Goals
            (<strong>S</strong>pecific, <strong>M</strong>easureable, <strong>A</strong>ttainable, <strong>R</strong>elevant, and <strong>T</strong>ime-bound) to 
            focus employees on achieving important, tangible outcomes that contribute to the achievement of CR&#39;s Enterprise Goals. Summarize the goals agreed to with the employee.</td></tr></table>
    
<asp:Panel ID="Goal_Setting_Edit" Visible="false" runat="server">

<table style="width:100%; border-collapse:collapse; border-spacing:0;" border="1" >
<tr><td class="BorderColor" colspan="2" style="width:5%;"><asp:Button ID="BtnFuture_Goal" runat="server" BackColor="Wheat" CssClass="Button_StyleSheet" BorderStyle="None" Font-Size="11pt" Text="Add New Goal"/></td>
    <td class="BorderColor" style="text-align:center; font-size:large; font-weight:bold; width:45%; font-family:Calibri; background-color:lightgray;">Goals</td>
    <td class="BorderColor" style="text-align:center; font-size:large; font-weight:bold; width:40%; font-family:Calibri; background-color:lightgray;">Key Results</td>
    <td class="BorderColor" style="text-align:center; font-size:large; font-weight:bold; width:10%; font-family:Calibri; background-color:lightgray;">Target<br />Completion Date</td></tr>

<tr><td class="BorderColor" style="vertical-align:top; text-align:center; width:2%;">&nbsp;</td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:3%;">1)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit1" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit1" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit1" runat="server" Width="98%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr>

<asp:Panel ID="Panel_FutureGoal_Edit2" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center;"><asp:Button ID="Delete_FutureGoal2" Text="X" ForeColor="#00ae4d" CssClass="Button_StyleSheet" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold;width:3%;">2)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit2" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit2" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit2" runat="server" Width="98%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Edit3" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center;"><asp:Button ID="Delete_FutureGoal3" Text="X" ForeColor="#00ae4d" CssClass="Button_StyleSheet" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:3%;">3)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit3" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit3" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit3" runat="server" Width="98%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Edit4" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center;"><asp:Button ID="Delete_FutureGoal4" Text="X" ForeColor="#00ae4d" CssClass="Button_StyleSheet" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:3%;">4)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit4" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit4" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit4" runat="server" Width="98%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Edit5" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center;"><asp:Button ID="Delete_FutureGoal5" Text="X" ForeColor="#00ae4d" CssClass="Button_StyleSheet" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:3%;">5)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit5" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit5" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit5" runat="server" Width="98%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Edit6" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center;"><asp:Button ID="Delete_FutureGoal6" Text="X" ForeColor="#00ae4d" CssClass="Button_StyleSheet" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:3%;">6)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit6" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit6" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit6" runat="server" Width="98%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Edit7" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center;"><asp:Button ID="Delete_FutureGoal7" Text="X" ForeColor="#00ae4d" CssClass="Button_StyleSheet" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:3%;">7)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit7" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit7" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit7" runat="server" Width="98%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Edit8" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center;"><asp:Button ID="Delete_FutureGoal8" Text="X" ForeColor="#00ae4d" CssClass="Button_StyleSheet" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:3%;">8)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit8" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit8" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit8" runat="server" Width="98%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Edit9" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center;"><asp:Button ID="Delete_FutureGoal9" Text="X" ForeColor="#00ae4d" CssClass="Button_StyleSheet" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:3%;">9)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit9" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit9" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit9" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>

<asp:Panel ID="Panel_FutureGoal_Edit10" Visible="false" runat="server">
<tr><td class="BorderColor" style="vertical-align:top; text-align:center;"><asp:Button ID="Delete_FutureGoal10" Text="X" ForeColor="#00ae4d" CssClass="Button_StyleSheet" Font-Bold="true" BackColor="white" BorderStyle="None" runat="server"/></td>
    <td class="BorderColor" style="vertical-align:top; text-align:center; font-weight:bold; width:3%;">10)</td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Goal_Edit10" runat="server" Width="99%" class="TextBox_StyleSheet" BorderStyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Succ_Edit10" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td>
    <td class="BorderColor"><asp:TextBox ID="FUT_Date_Edit10" runat="server" Width="99%" class="TextBox_StyleSheet" Borderstyle="None" TextMode="MultiLine" Rows="4" Style="overflow:auto;" ValidateRequestMode="Disabled" onkeyup='ChangeColor(id);'/></td></tr></asp:Panel>
</table>
</asp:Panel>  

  </td></tr>
            </table>

<asp:Panel ID="Goal_Setting_Edit1" Visible="false" runat="server">
  <table style="text-align:center; width:100%;">
    <tr><td style="width:35%;"><asp:Button ID="BtnSubmit_UpperMgr" Text="Send to Up Mgr Name" BackColor="Wheat" CssClass="Button_StyleSheet" BorderColor="black" Font-Size="12pt" Font-Bold="true" runat="server"/></td>
        <td><asp:Button ID="btnSave1" Text="Save" BackColor="Wheat" CssClass="Button_StyleSheet" Font-Size="12pt" Font-Bold="true"  BorderColor="black"  runat="server" Visible="false"/></td>
        <td style="width:35%;"><asp:Button ID="btnSave" Text="Save" BackColor="Wheat" CssClass="Button_StyleSheet" Font-Size="12pt" Font-Bold="true"  BorderColor="black" runat="server"/></td></tr>
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
    <tr><td>&nbsp;&nbsp;<asp:Label ID="Disc_Com" text="Send suggested revisions to " runat="server" CssClass="Label_StyleSheet" Visible="false"/> </td></tr>
    <tr><td style="text-align:center;">
        <asp:TextBox ID="DiscussionComments" runat="server" Width="99%" TextMode="MultiLine" Rows="3" style="overflow:auto; border-color:black;" CssClass="TextBox_StyleSheet" 
            onkeyup="SetButtonStatus(this, 'target');" /></td></tr>
    <tr><td>
        <table style="width:100%" border="0">
            <tr><td style="text-align:left" class="auto-style1">&nbsp;
                    <asp:Button ID="Discuss" runat="server" BackColor="Wheat" Font-Size="11pt" Font-Bold="true" Font-Names="Calibri" BorderColor="black" text="Revise" Visible="false"/></td>
                <td style="width:50%; text-align:right">
                    <asp:Button ID="Generalist" runat="server" BackColor="Wheat" Font-Size="11pt" Font-Bold="true" Font-Names="Calibri" Text="Submit for review to" BorderColor="black" Visible="false"/>
                    <asp:Button ID="Approve" runat="server" BackColor="Wheat" Font-Size="11pt" Font-Bold="true" Font-Names="Calibri" Text="Approve" BorderColor="black" Visible="false"/>&nbsp;&nbsp;</td></tr>
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
      <tr><td style="font-size:18px; font-weight:bold; font-family:Calibri; color:#00AE4D;">Previous Goals record</td></tr>
</table>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
    Width="100%" AllowSorting="True" DataKeyNames="EMPLID" CssClass="Grid_StyleSheet">
 <Columns>
<asp:BoundField DataField="Guild_Approved" HeaderText="Reviewed Date" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="left" SortExpression="Goals"></asp:BoundField>    
<asp:BoundField DataField="Goals" HeaderText="Goals" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="Goals" HeaderStyle-Width="35%"></asp:BoundField>
<asp:BoundField DataField="MileStones" HeaderText="Key Results" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="center" SortExpression="MileStones"  HeaderStyle-Width="35%"></asp:BoundField>
<asp:BoundField DataField="TargetDate" HeaderText="Target Completion Date" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="TargetDate" HeaderStyle-Width="7%" ></asp:BoundField>
<asp:BoundField DataField="Created" HeaderText="Manager" HeaderStyle-HorizontalAlign="center" HeaderStyle-BackColor="LightGray" SortExpression="Created" HeaderStyle-Width="10%" ></asp:BoundField>
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>

</asp:Panel>


<table style="width:100%">
    <tr><td style="text-align:center;">
        <asp:Button ID="Sub_Empl" runat="server" BackColor="Wheat" Font-Size="11pt" Font-Bold="true" BorderColor="black" Font-Names="Calibri" Text="Submit to Employee" Visible="false" /></td></tr>
    <tr><td style="text-align:center;">
        <asp:Button ID="Empl_Review" runat="server" BackColor="Wheat" Font-Size="11pt" Font-Bold="true" BorderColor="black" Font-Names="Calibri" Text="Reviewed and Confirmed" Visible="false" /></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td style="text-align:center;">
       <!-- <a href="Guild_FutureGoal_Print.aspx?Token=<%=Request.QueryString("Token")%>" target="_blank" style="font-size:medium; font-family:Calibri;" title="Please save data first">Print / Preview</a>-->
        </td></tr>
     <tr><td>&nbsp;</td></tr>
    <tr><td style="text-align:center;"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
                CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/>
        <!--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Images/Back_button.png" style="width:90px" />--></td></tr>
</table>
    
 </td>
    <td style="width:50px;">&nbsp;</td></tr>

</table>
    
    </form>
</body>
</html>
