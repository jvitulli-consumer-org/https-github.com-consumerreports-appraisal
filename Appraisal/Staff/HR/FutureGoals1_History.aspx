<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FutureGoals1_History.aspx.vb" Inherits="Appraisal.FutureGoals1_History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Goals History</title>
</head>
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
 </style>

</head>

<body>

<form id="form1" runat="server">

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
<asp:label id="lblEmpl_NAME" runat="server" CssClass="Label_StyleSheet" Visible="false"/>
<asp:Label ID="Goal_Year" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet" Visible="false"/>    
<asp:Label ID="Goal_Year1" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet" Visible="false"/>
<asp:Label ID="Goal_Year2" runat="server" ForeColor="#00ae4d" CssClass="Label_StyleSheet" Visible="false"/>
<asp:Label ID="LblEMP_Appr" runat="server" Font-Names="Calibri" Visible="false"/>
<asp:label id="lblEmpl_TITLE" runat="server" Font-Names="Calibri" Visible="false"/>
<asp:Label ID="LblMGT_NAME" runat="server" Font-Names="Calibri" Visible="false" />
<asp:Label ID="LblMGT_Appr" runat="server" Font-Names="Calibri" Visible="false" />
<asp:label id="lblEmpl_DEPT" runat="server" Font-Names="Calibri" Visible="false"/>
<asp:Label ID="lblUP_MGT_NAME" runat="server" Font-Names="Calibri" Visible="false"/>
<asp:label id="lblEmpl_HIRE" runat="server" Font-Names="Calibri" Visible="false"/>


<table width="100%" border="0"><tr><td style="text-align:center;">
    <asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
        CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/>
                                   </td></tr></table>

    </form>
</body>
</html>
