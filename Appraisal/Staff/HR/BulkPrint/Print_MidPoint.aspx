<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Print_MidPoint.aspx.vb" Inherits="Appraisal.Print_MidPoint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="../../StyleSheet1.css" rel="stylesheet" />
        
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
   .StyleBreak
    {
     width:100%;
     page-break-after: always;
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

    </form>
</body>
</html>
