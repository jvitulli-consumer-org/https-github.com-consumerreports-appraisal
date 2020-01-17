<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Guild_MidPoints.aspx.vb" Inherits="Appraisal.Guild_MidPoint1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Mid Point Conversation Form</title>
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
          height:30px;
          }
       .style4 {
          width: 100px;
          text-align:left;
          font-family:Calibri;
          font-weight:bold
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
      .style16  {
        width:2%;
        vertical-align:top;
        font-family:Calibri;
        }
     .mycheckBig input {
         width:30px; 
         height:30px;
     } 
    .mycheckSmall  input {
        width:10px; 
        height:10px;
    } 
     .auto-style2 {
         height: 80px;
         width: 105px;
     }
     .auto-style3 {
         width: 105px;
     }
     .auto-style4 {
         width: 105px;
         text-align: left;
         font-family: Calibri;
         font-weight: bold;
     }
 </style>

<script type="text/javascript">
   //-------------------------------------------
    function showMe(box) {

        var chboxs = document.getElementsByName("c1");
        var vis = "none";
        var vis1 = "block";

        for (var i = 0; i < chboxs.length; i++) {
            if (chboxs[i].checked) {
                vis = "block";
                break;
            }
        }
        document.getElementById(box).style.display = vis;
        //document.getElementById("CheckImg")
    }

//-----------------------------------------------
    function checkBox1OnClick(elementRef) {
        if (elementRef.checked) {
            if (window.confirm('Are you sure?') == false)
                elementRef.checked = false;
        }
    }
    // --> - See more at: http://codeverge.com/asp.net.client-side/return-confirm-on-checkbox/266439#sthash.lBLLSA0G.dpuf

//----------------------------------------------------
    function chbx(obj) {
        var that = obj;
        if (document.getElementById(that.id).checked == true) {
            document.getElementById('CheckBox_Met').checked = false;
            document.getElementById('CheckBox_NotMet').checked = false;
            document.getElementById(that.id).checked = true;
        }
    }
//----------------------------------------------------

</script>

</head>

<body>

<form id="form1" runat="server">

<asp:label id="lblEMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblLogin_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblGUILD_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblSUP_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblSUP_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_EMPLID" runat="server" Visible="false"/>
<asp:label id="lblUP_MGT_EMAIL" runat="server" Visible="false"/>
<asp:label id="lblHR_EMPLID" runat="server" Visible="false"/>
<asp:label id="lblHR_EMAIL" runat="server" Visible="false"/>
<asp:label id="lblComments" runat="server" Visible="false"/> 

<table border="0" style="width:100%">
    <tr><td colspan="6" style="text-align:center;"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td></tr>
    <tr><td colspan="6" class="Style1"><u>FY<asp:Label ID="lblMidPoint_Year" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/> Mid Point Conversation Acknowledgement</u></td></tr>
    <tr><td colspan="6" style="height:40px;">&nbsp;&nbsp;</td></tr>
    <tr><td style="width:15%">&nbsp;</td>
        <td class="auto-style4"><b>Name</b></td><td class="style7"><asp:label id="lblEMPLOYEE_NAME" runat="server" Font-Names="Calibri"/></td>
        <td class="style12"><b>Manager Name:</b></td><td class="style8"><asp:label id="LblMgr_NAME" runat="server" Font-Names="Calibri"/></td><td style="width:15%"></td></tr>
    <tr><td style="width:15%"></td>
        <td class="auto-style4"><b>Title:</b></td><td class="style7"><asp:label id="lblEMPLOYEE_TITLE" runat="server" Font-Names="Calibri"/></td>
        <td class="style12"><b>2nd Level Manager Name:</b></td><td class="style8"><asp:Label ID="lblMGR_UP_NAME" runat="server" Font-Names="Calibri"/></td><td style="width:15%"></td></tr>
    <tr><td style="width:15%"></td>
        <td class="auto-style4"><b>Department:</b></td><td class="style7"><asp:label id="lblEMPLOYEE_DEPT" runat="server" Font-Names="Calibri" /></td>
        <td class="style12"><b>Human Resources Generalist:</b></td><td class="style8"><asp:Label ID="lblGENERALIST_NAME" runat="server" Font-Names="Calibri"/></td><td style="width:15%"></td></tr>
    <tr><td style="width:15%"></td>
        <td class="auto-style4">Hire Date:</td><td><asp:label id="lblEMPLOYEE_HIRE" runat="server" Text="" Font-Names="Calibri"/></td>
        <td class="style12"><b>Time Stamp(when signed):</b></td><td class="style8"><asp:label id="lblTimeStamp" runat="server" Font-Names="Calibri" /></td><td style="width:15%"></td></tr>
    <tr><td colspan="6" style="height:25px;">   </td></tr>
    <tr><td></td><td colspan="4" class="Style3"><u>Acknowledgement:</u></td><td></td></tr>
<!--<tr><td></td><td></td><td></td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>-->
 
 
    
  <tr id="Met" runat="server"><td>&nbsp;&nbsp;</td>
       <td style="vertical-align:middle;" class="auto-style2"><img id="CheckImg" runat="server" alt="../../images/CheckRed1.png" src="../../images/CheckGreen.png" /></td>
       <td colspan="3" style="font-size:large; font-family:Calibri">I confirm that <strong>I have met</strong> with my manager to have a mid point performance conversation on <asp:Label ID="LblDate" runat="server"/>.</td></tr>

  <tr id="Not_Met" runat="server"><td>&nbsp;&nbsp;</td>
      <td style="vertical-align:middle;" class="auto-style2"><img id="CheckImg1" runat="server" alt="../../images/CheckRed1.png" src="../../images/CheckGreen.png" /></td>
      <td colspan="3" style="font-size:large; font-family:Calibri"><strong>I have not met</strong> with my manager to have a mid point performance conversation. <asp:Label ID="lblDate1" runat="server"/></td></tr>
<!-- <tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>-->

<asp:Panel ID="Com1" runat="server" >
<tr><td>&nbsp;&nbsp;</td>
    <td colspan="4" style="font-size:small; font-family:Calibri; color:gray; vertical-align:top"><div style="font-size:small; font-family:Calibri; color:gray;">Comments <i>(optional)</i>:</div>
    <table id="Com_Read" style="width:100%; border-spacing:0px; border-color:lightgray;" border="0" runat="server"><tr><td><%= lblComments.Text%></td></tr></table>
    </td>
    <td>&nbsp;&nbsp;</td></tr>

</asp:Panel>

    <tr><td>&nbsp;&nbsp;</td><td class="auto-style3">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
    <tr><td>&nbsp;&nbsp;</td><td class="auto-style3">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
    <tr><td>&nbsp;&nbsp;</td><td class="auto-style3">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
    <tr><td>&nbsp;&nbsp;</td><td class="auto-style3">&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>
    <tr><td colspan="6" style="text-align:center;"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/></td></tr>

  </table>
 
</form>
</body>
</html>
