<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Appraisal_Edit_Mgt.aspx.vb" Inherits="Appraisal.Appraisal_Edit_Mgt" MaintainScrollPositionOnPostback="true" smartNavigation="true" ValidateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title><%=lblEmpl_NAME.Text%>'s Appraisal</title>
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
       width: 300px;
       font-family: Calibri;
          }
  .style3 {
       width: 95px;
       text-align: left;
       font-family: Calibri;
          }
  .style4 {
       width: 210px;
       text-align: left;
       font-family: Calibri;
           }
  .style5 {
       width: 100px;
       text-align: right;
       font-family: Calibri;
           }
  .style6 {
       width: 5%;
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

<style media=print>
 .hide_print {
     display: none;
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
            document.getElementById("<%=lblMessage1.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };

    //========================================================================
    function textAreaAdjust(o) {
        var evt = (evt) ? evt : ((event) ? event : null);
        var tabKey = 9;

        if (evt.keyCode == tabKey) {
            o.style.height = "1px";
            o.style.height = (5 + o.scrollHeight) + "px";
        } else {
            o.style.height = "1px";
            o.style.height = (5 + o.scrollHeight) + "px";
        }
    }
    //=======================================================================
</script>
</head>

<body id="body1" runat="server">
    
<form id="form1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:Timer ID="Timer1" runat="server" Interval="30000" ontick="Timer1_Tick"></asp:Timer>  
<asp:Timer ID="Timer2" runat="server" Interval="5000" ontick="Timer2_Tick"></asp:Timer> 

<span class="hide_print">   
<asp:Label ID="lblEMPLID" runat="server" Visible="false"/>
<asp:label id="lblLogin_EMPLID" runat="server" Visible="true" ForeColor="white" BackColor="White"/>
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
</span>

<table border="0" class="style0">
    <tr><td class="style6">&nbsp;&nbsp;</td>
        <td class="style1"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td>
        <td class="style6">&nbsp;&nbsp;</td></tr>
    <tr><td class="style6">
      <span class="hide_print">
        <asp:Button ID="lblMessage1" runat="server" ForeColor="Green" BackColor="White" BorderStyle="None" Font-Bold="true" Font-Size="12px" Width="120px" CssClass="Button_StyleSheet" style="position:fixed;left:10px; top: 80px;"/><br />
        <asp:Button ID="SaveRecords1" runat="server" Text="Save Records" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" CssClass="Button_StyleSheet" style="position:fixed;left:10px; top: 100px;"/></span></td>
        <td class="style1"><u>Performance Appraisal (FY<asp:Label ID="FY_Year" runat="server" Text="" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>)</u></td>
        <td class="style1">
             <span class="hide_print">
            <asp:ImageButton ID="Img_Print1" runat="server" ImageUrl="../../Images/print.png" Style="width:70px; position:fixed;right:20px; top:100px;" onclientclick="javascript:window.print();" />
              </span></td></tr>
  
    <tr><td class="style6">&nbsp;&nbsp;</td>
        <td class="style1"></td>
        <td class="style6">&nbsp;&nbsp;</td></tr>
    <tr><td class="style6">&nbsp;&nbsp;</td>
        <td><table border="0" class="style0">
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
                   <td class="style4"><b>Collaborating Manager:&nbsp;</b></td>
                   <td class="style2"><asp:Label ID="lblCOLL_MGT_NAME" runat="server" Font-Names="Calibri"/></td>
                   <td class="style5">&nbsp;&nbsp;</td>
                   <td><asp:Label ID="LblUP_MGT_Appr" runat="server" Font-Names="Calibri" Visible="false" /></td></tr>                       
               <tr><td class="style3"><b>Hire Date:</b></td>
                   <td class="style2"><asp:label id="lblEmpl_HIRE" runat="server" Font-Names="Calibri"/></td>
                   <td class="style4"><b>Human Resources Generalist:</b></td>
                   <td class="style2"><asp:label id="lblHR_NAME" runat="server" Font-Names="Calibri"/></td>
                   <td class="style5"><b>Approved:</b>&nbsp;&nbsp;</td>
                   <td><asp:Label ID="LblHR_Appr" runat="server" Font-Names="Calibri" /></td></tr></table></td>
  <td class="style6">&nbsp;&nbsp;</td></tr>
  <tr><td class="style6">&nbsp;&nbsp;</td>
      <td>
<!--Panel_Create_Appraisal-->
<asp:Panel ID="Panel_Create_Appraisal" runat="server">

<asp:UpdatePanel ID="UpdatePanel_AllData" runat="server" UpdateMode="Conditional">
<ContentTemplate></ContentTemplate>
    <triggers><asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"/></triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate></ContentTemplate>
    <triggers><asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick"/></triggers>
</asp:UpdatePanel>

<!--All Chapters are Going here-->
<table border="0" class="style0">

<!--Accomplishments -->
    <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Accomplishments:</u>&nbsp;<span class="style7">(Summarize accomplishments for the rating criteria established)</span></td></tr>
<tr><td>
<asp:Label ID="lblAccomplishment" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" Visible="false" />
<asp:UpdatePanel ID="UpdatePanel_Accomplishment" runat="server" UpdateMode="Conditional"><ContentTemplate>
<asp:TextBox ID="txbAccomplishment" runat="server" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="1"
     ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Accomplishments_TextChanged" onkeyup="ChangeColor(id);" /></ContentTemplate></asp:UpdatePanel></td></tr>
<tr><td><span class="hide_print"><asp:Label ID="lblAccomplishment_Comm" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" Visible="false" /></span></td></tr>

<tr><td><span class="hide_print"> 
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" DataKeyNames="EMPLID" HeaderStyle-ForeColor="Blue" 
Caption="Accomplishment History" CaptionAlign="Left" Font-Size="Smaller"><AlternatingRowStyle BackColor="#ccffff" />
 <Columns>
   <asp:BoundField DataField="DateTime" HeaderText="Date Time" HeaderStyle-BackColor="LightGray"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
   <asp:BoundField DataField="Generalist" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" ItemStyle-VerticalAlign="Top"></asp:BoundField>
<asp:TemplateField HeaderText="Accomplishment Comments" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
<asp:Label ID="Lbl_Accomp_Comm" runat="server" Text='<%# Eval("Accomp_Comm").ToString().Replace(Environment.NewLine, "<br />")%>' ></asp:Label></ItemTemplate></asp:TemplateField> 
</Columns></asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</span></td></tr>
<tr><td><hr /></td></tr>

<!--Addendum -->     

<tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Leadership Competencies</u>&nbsp;<span class="style7">(Check the box that most appropriately describes your direct report's proficiency level in demonstrating CR's Leadership Competencies. 
        Use this as a guide to provide feedback and guide your direct report on areas to focus on for further growth and development.)</span></td></tr>
    <tr><td>
<table border="1" style="width:100%; border-collapse:collapse; " runat="server">
    <tr><td style="height:30px; text-align:center; font-weight:bold; font-size:large; background-color:#E7E8E3; font-family:Calibri; width:30%">&nbsp;</td>
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
            o Encourages open debate and expects support and commitment once discussions are concluded</span></a>&nbsp;&nbsp;Make Balanced Decisions&nbsp;&nbsp;</td></tr></table></td>
       <td id="tdMake_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Make_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbMake_Need1" runat="server" AutoPostBack="true" GroupName="Make_Balance"/></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdMake_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Make_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbMake_Prof1" runat="server" AutoPostBack="true" GroupName="Make_Balance"/></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdMake_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Make_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbMake_Exce1" runat="server" AutoPostBack="true" GroupName="Make_Balance"/></ContentTemplate></asp:UpdatePanel></td></tr>

    <tr id="trBuild_Trust" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Build Trust</b></u><br /><br />
            o Embodies CR’s values and cultural attributes<br /><br />
            o Acts with integrity and follows through on commitments<br /><br />
            o Creates an organization characterized by trust and integrity where everyone is accountable for CR’s success<br /><br />
            o Demonstrates personal conviction in a way that inspires others to do the same<br /><br />
            o Takes ownership when things go wrong and does not place blame on others<br /><br />
            o Demonstrates humility and authenticity</span></a>&nbsp;&nbsp;Build Trust&nbsp;&nbsp;</td></tr></table></td>
       <td id="tdBuild2_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Build_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbBuild2_Need1" runat="server" AutoPostBack="true" GroupName="Build_Trust"/></ContentTemplate></asp:UpdatePanel></td>
        <td id="tdBuild2_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Build_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbBuild2_Prof1" runat="server" AutoPostBack="true" GroupName="Build_Trust"/></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdBuild2_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Build_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbBuild2_Exce1" runat="server" AutoPostBack="true" GroupName="Build_Trust"/></ContentTemplate></asp:UpdatePanel></td></tr>

    <tr id="trLearn_Continuously" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Learn Continuously</b></u><br /><br />
            o Is self-aware and understands his/her impact on others<br /><br />
            o Shows commitment to continuous learning and development<br /><br />
            o Seeks out and is responsive to feedback<br /><br /> 
            o Listens to and learns from others<br /><br /> 
            o Experiments appropriately and learns from experiences<br /><br />
            o Shows intellectual curiosity</span></a>&nbsp;&nbsp;Learn Continuously&nbsp;&nbsp;</td></tr></table></td>

       <td id="tdLearn_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Learn_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLearn_Need1" runat="server" AutoPostBack="true" GroupName="Learn_Continuously" /></ContentTemplate></asp:UpdatePanel></td>
        <td id="tdLearn_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Learn_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLearn_Prof1" runat="server" AutoPostBack="true" GroupName="Learn_Continuously"/></ContentTemplate></asp:UpdatePanel></td>
        <td id="tdLearn_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Learn_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLearn_Exce1" runat="server" AutoPostBack="true" GroupName="Learn_Continuously"/></ContentTemplate></asp:UpdatePanel></td></tr>

    <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading Other</span></td></tr>
    <tr id="trLead_Urgency" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left; text-wrap:none;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Lead with Urgency & Purpose</b></u><br /><br />
            o Effectively focuses self and others toward action and results<br /><br />
            o Keeps a focus on Key Performance Indicators (KPI’s) and the impact on consumers<br /><br />
            o Effectively stewards financial and people resources while managing to budget and driving towards the finish line</span></a>
            &nbsp;&nbsp;Lead with Urgency & Purpose&nbsp;&nbsp;</td></tr></table></td>
       <td  id="tdLead2_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead2_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLead2_Need1" runat="server" AutoPostBack="true" GroupName="Lead_Urgency"/></ContentTemplate></asp:UpdatePanel></td>
       <td  id="tdLead2_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead2_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLead2_Prof1" runat="server" AutoPostBack="true" GroupName="Lead_Urgency"/></ContentTemplate></asp:UpdatePanel></td>
       <td  id="tdLead2_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead2_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLead2_Exce1" runat="server" AutoPostBack="true" GroupName="Lead_Urgency"/></ContentTemplate></asp:UpdatePanel></td></tr>

    <tr id="trPromote_Collaboration" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Promote Collaboration & Accountability</b></u><br /><br />
            o Sees and leverages the connections and interdependencies and works to break down silos<br /><br />
            o Leads others to better outcomes by demonstrating the value of collaboration, inclusion and diverse perspectives<br /><br />
            o Encourages and inspires staff to collaborate, partner, and assume ownership and accountability</span></a>
            &nbsp;&nbsp;Promote Collaboration & Accountability&nbsp;&nbsp;</td></tr></table></td>
       <td  id="tdProm_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Prom_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbProm_Need1" runat="server" AutoPostBack="true" GroupName="Promote_Collab"/></ContentTemplate></asp:UpdatePanel></td>
       <td  id="tdProm_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Prom_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbProm_Prof1" runat="server" AutoPostBack="true" GroupName="Promote_Collab"/></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdProm_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Prom_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbProm_Exce1" runat="server" AutoPostBack="true" GroupName="Promote_Collab"/></ContentTemplate></asp:UpdatePanel></td></tr>
               
    <tr id="trConfront_Challenges" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
            <u><b>Confront Challenges</b></u><br /><br />
            o Does not ignore issues and challenges<br /><br />
            o Confronts disagreement and conflict through candid discussion, building common understanding, and gaining agreement on what’s in CR’s best interest<br /><br />
            o Recognizes when course corrections are necessary and takes swift action<br /><br />
            o Willing to take an unpopular stand for the good of the enterprise</span></a>&nbsp;&nbsp;Confront Challenges&nbsp;&nbsp;</td></tr></table></td>
       <td  id="tdConf_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Conf_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbConf_Need1" runat="server" AutoPostBack="true" GroupName="Confront_Challenge"/></ContentTemplate></asp:UpdatePanel></td>
       <td  id="tdConf_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Conf_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbConf_Prof1" runat="server" AutoPostBack="true" GroupName="Confront_Challenge"/></ContentTemplate></asp:UpdatePanel></td>
       <td  id="tdConf_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Conf_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbConf_Exce1" runat="server" AutoPostBack="true" GroupName="Confront_Challenge"/></ContentTemplate></asp:UpdatePanel></td></tr>

    <tr><td style="height:30px; background-color:gray;" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-weight:bold; font-size:large; font-family:Calibri; color:white;">Leading the Organization</span></td></tr>
    <tr id="trLead_Change" style="height:33px; font-size:large">
       <td ><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
           <u><b>Lead Change</b></u><br/><br/>
            o Inspires others to embrace and lead change<br/><br/> o Removes barriers to change<br/><br/> o Clearly communicates the desired future state and gains commitment to the change vision<br/><br/>
            o Knows how to leverage key influencers to champion change and build cross-functional support for change</span></a>&nbsp;&nbsp;Lead Change&nbsp;&nbsp;</td></tr></table></td>
       <td id="tdLead_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLead_Need1" runat="server" AutoPostBack="true" GroupName="Lead_Change" /></ContentTemplate></asp:UpdatePanel></td>
        <td id="tdLead_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLead_Prof1" runat="server" AutoPostBack="true" GroupName="Lead_Change" /></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdLead_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Lead_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLead_Exce1" runat="server" AutoPostBack="true" GroupName="Lead_Change" /></ContentTemplate></asp:UpdatePanel></td></tr>

    <tr id="trInspire_Risk" style="height:33px; font-size:large">
       <td class="style11"><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
           <u><b>Inspire Risk Taking & Innovation</b></u><br/><br/>
           o Creates an environment where people feel safe in taking appropriate risks and are not afraid to fail<br/><br/>
           o Sees the possibilities and effectively leads people and process to realize innovation, revenue growth, and social impact<br /><br/>
           o Has the courage and takes action to cease unfruitful initiatives</span></a>&nbsp;&nbsp;Inspire Risk Taking & Innovation&nbsp;&nbsp;</td></tr></table></td>
        <td  id="tdInsp_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Insp_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbInsp_Need1" runat="server" AutoPostBack="true" GroupName="Inspire_Risk"/></ContentTemplate></asp:UpdatePanel></td>
        <td  id="tdInsp_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Insp_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbInsp_Prof1" runat="server" AutoPostBack="true" GroupName="Inspire_Risk"/></ContentTemplate></asp:UpdatePanel></td>
        <td  id="tdInsp_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Insp_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbInsp_Exce1" runat="server" AutoPostBack="true" GroupName="Inspire_Risk"/></ContentTemplate></asp:UpdatePanel></td></tr>
    
    <tr id="trLeverage_External" style="height:33px; font-size:large">
       <td class="style11"><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
          <u><b>Leverage External Perspective</b></u><br /><br />
            o Leverages knowledge of the competitive landscape, marketplace trends, best practices, and technology to improve CR’s ability to compete<br/><br/>
            o Holds self and others accountable for staying abreast of external trends and developments</span></a>&nbsp;&nbsp;Leverage External Perspective&nbsp;&nbsp;</td></tr></table></td>
       <td id="tdLeve_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Leve_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLeve_Need1" runat="server" AutoPostBack="true" GroupName="Leverage_External"/></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdLeve_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Leve_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLeve_Prof1" runat="server" AutoPostBack="true" GroupName="Leverage_External"/></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdLeve_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Leve_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbLeve_Exce1" runat="server" AutoPostBack="true" GroupName="Leverage_External"/></ContentTemplate></asp:UpdatePanel></td></tr>    
    <tr id="trCommunicate_Impact" style="height:33px; font-size:large">
       <td class="style11"><table class="style11" style="height:33px; font-size:large; width:100%"><tr><td style="width:15%">&nbsp;</td>
       <td style="text-align:left;"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
           <u><b>Communicate for Impact</b></u><br /><br />
            o Translates vision, strategy, and complex information into simple impactful messages tailored to meet the needs of the audience<br/><br/>
            o Communicates consistently in a clear and confident way<br/><br/>
            o Persuades and influences others through compelling communications and clear rationale<br/><br/>
            o Encourages others to express their views openly</span></a>&nbsp;&nbsp;Communicate for Impact&nbsp;&nbsp;</td></tr></table></td>
       <td  id="tdComm_Need1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Comm_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbComm_Need1" runat="server" AutoPostBack="true" GroupName="Communic_Impact"/></ContentTemplate></asp:UpdatePanel></td>
       <td  id="tdComm_Prof1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Comm_Prof" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbComm_Prof1" runat="server" AutoPostBack="true" GroupName="Communic_Impact"/></ContentTemplate></asp:UpdatePanel></td>
       <td  id="tdComm_Exce1" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Comm_Exce" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbComm_Exce1" runat="server" AutoPostBack="true" GroupName="Communic_Impact"/></ContentTemplate></asp:UpdatePanel></td></tr>   
</table>
        </td></tr>
    <tr><td><span class="hide_print"><asp:Label ID="lblLeaderShip_Comm" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" Visible="false" /></span></td></tr>
<tr><td><span class="hide_print">
<asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource6" Width="100%" DataKeyNames="EMPLID" HeaderStyle-ForeColor="Blue" 
    Caption="Leadership Competencies History" CaptionAlign="Left" Font-Size="Smaller">
 <AlternatingRowStyle BackColor="#ccffff" />
 <Columns>
   <asp:BoundField DataField="DateTime" HeaderText="Date Time" HeaderStyle-BackColor="LightGray"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
   <asp:BoundField DataField="Generalist" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" ItemStyle-VerticalAlign="Top"></asp:BoundField>
<asp:TemplateField HeaderText="Leadership Competencies Comments" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
<asp:Label ID="LblLead_Comm" runat="server" Text='<%# Eval("Lead_Comm").ToString().Replace(Environment.NewLine, "<br />")%>' ></asp:Label></ItemTemplate></asp:TemplateField>    
</Columns></asp:GridView>
<asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</span></td></tr>    
<!--Adendum -->

<tr><td><hr /></td></tr>
<!--Strengths--> 
    <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Strengths:</u>&nbsp;<span class="style7">(Comment on key strengths and highlight areas performed well)</span></td></tr>
    <tr><td><asp:Label ID="lblStrengths" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" Visible="false"/>
<asp:UpdatePanel ID="UpdatePanel_Strengths" runat="server" UpdateMode="Conditional"><ContentTemplate>
<asp:TextBox ID="txbStrengths"  Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="2"
     ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Strengths_TextChanged" onkeyup="ChangeColor(id);" /></ContentTemplate></asp:UpdatePanel></td></tr>
    <tr><td><span class="hide_print"><asp:Label ID="lblStrengths_Comm" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" Visible="false" /></span></td></tr>
<tr><td><span class="hide_print">
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="100%" DataKeyNames="EMPLID" HeaderStyle-ForeColor="Blue" Caption="Strengths History" CaptionAlign="Left" Font-Size="Smaller">
 <AlternatingRowStyle BackColor="#ccffff" />
 <Columns>
   <asp:BoundField DataField="DateTime" HeaderText="Date Time" HeaderStyle-BackColor="LightGray"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
   <asp:BoundField DataField="Generalist" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
<asp:TemplateField HeaderText="Strengths Comments" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
<asp:Label ID="LblStr_Comm" runat="server" Text='<%# Eval("Str_Comm").ToString().Replace(Environment.NewLine, "<br />")%>' ></asp:Label></ItemTemplate></asp:TemplateField> 
</Columns></asp:GridView>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</span></td></tr>
<tr><td><hr /></td></tr>
<!--Development_Area--> 
    <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Development Areas:</u>&nbsp; <span class="style7">(Comment on and provide examples of areas of performance that require further development or improvement. Identify plans to address areas of development)</span> </td></tr>
    <tr><td><asp:Label ID="lblDevelopment" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" Visible="false" />
<asp:UpdatePanel ID="UpdatePanel_Development" runat="server" UpdateMode="Conditional"><ContentTemplate>
<asp:TextBox ID="txbDevelopment"  Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="3"
     ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Development_TextChanged" onkeyup="ChangeColor(id);" /></ContentTemplate></asp:UpdatePanel></td></tr>
    <tr><td><span class="hide_print"><asp:Label ID="lblDevelopment_Comm" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" Visible="false" /></span></td></tr>
<tr><td><span class="hide_print">
<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Width="100%" DataKeyNames="EMPLID" HeaderStyle-ForeColor="Blue" 
    Caption="Development Areas History" CaptionAlign="Left" Font-Size="Smaller">
 <AlternatingRowStyle BackColor="#ccffff" />
 <Columns>
<asp:BoundField DataField="DateTime" HeaderText="Date Time" HeaderStyle-BackColor="LightGray"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-VerticalAlign="Top"></asp:BoundField>
<asp:BoundField DataField="Generalist" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" ItemStyle-VerticalAlign="Top"></asp:BoundField>
<asp:TemplateField HeaderText="Development Area Comments" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
<asp:Label ID="LblDev_Comm" runat="server" Text='<%# Eval("Dev_Comm").ToString().Replace(Environment.NewLine, "<br />")%>'></asp:Label></ItemTemplate></asp:TemplateField> 
</Columns></asp:GridView>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</span></td></tr>    
<tr><td><hr /></td></tr>

<!--Overall Performance Rating-->
    <tr><td class="style8">&nbsp;&nbsp;&nbsp;<u>Overall Performance Rating:</u><span class="style7"> (Check the box that most appropriately describes the individual&#39;s overall performance)</span></td></tr>
    <tr><td>
<table border="1" style="width:100%; border-spacing:0; border-color:black; border-collapse:collapse; border-spacing:0;" runat="server">
   <tr><td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
          <u><b>Unsatisfactory</b></u><br/><br/>This employee is not performing to the requirements of the position. Performance must improve significantly within a reasonable time period if the individual is to remain in the position.<br />
          <br />&nbsp;&nbsp;&nbsp;&nbsp;Note: Employees receiving this rating should be on a performance improvement plan.</span></a>&nbsp;<b>Unsatisfactory</b>&nbsp;&nbsp;</td>
       <td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
          <u><b>Developing/Improving Contributor</b></u><br /><br />This employee is still learning the essential functions of the position, or is improving toward effective performance of all essential functions. Continued development or improvement 
           is required.<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;Note: Employees new to their positions should receive this rating if they are still developing and growing into the role, as should those who are struggling with new responsibilities. 
           Employees should not receive this rating in consecutive years. Employees continuing to require significant development a year after receiving this rating should  be rated as “underperforming”.</span></a>&nbsp;&nbsp;<b>Developing/Improving 
           Contributor</b>&nbsp;&nbsp;</td>        
       <td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
           <u><b>Solid Contributor</b></u><br /><br />Performance is considered solid, although there may be areas in which the employee should further develop.</span></a>&nbsp;&nbsp;<b>Solid Contributor</b>&nbsp;&nbsp</td>
       <td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
           <u><b>Very Strong Contributor</b></u><br /><br />This employee sustains a consistently high level of performance, frequently exceeds the requirements and expectations of the position, and produces high quality work on a consistent basis. 
           S/he makes an important contribution to enhancing organizational performance and impact.</span></a>&nbsp;&nbsp;<b>Very Strong Contributor</b>&nbsp;&nbsp</td>
       <td class="style9"><a class="tooltip" href="#"><img src="../../images/info.png" height="15" width="15" border="0"/><span class="custom info">
           <u><b>Distinguished Contributor</b></u><br /><br />This employee had an extraordinary year of exceptional performance and accomplishments. S/he stands out by demonstrating remarkable leadership and effecting measurable, lasting 
           improvements in organizational performance and impact.<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;Note: Employees should generally not expect to be rated as a distinguished contributor in consecutive years.</span></a>&nbsp;&nbsp;
           <b>Distinguished Contributor</b>&nbsp;&nbsp</td></tr>
                
   <tr id="trOverall_Performance" runat="server">
       <td id="tdBelow" style="text-align:center;">   
<asp:UpdatePanel ID="UpdatePanel_Rating_Below" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbBelow" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdNeed" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Rating_Need" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbNeed" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/></ContentTemplate></asp:UpdatePanel></td>     
       <td id="tdMeet" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Rating_Meet" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbMeet" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdExceed" style="text-align:center;">
<asp:UpdatePanel ID="UpdatePanel_Rating_Exceed" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbExceed" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/></ContentTemplate></asp:UpdatePanel></td>
       <td id="tdDisting"  style="text-align:center;">  
<asp:UpdatePanel ID="UpdatePanel_Rating_Disting" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:RadioButton ID="rbDisting" runat="server" text="" GroupName="OverAll" AutoPostBack="True"/></ContentTemplate></asp:UpdatePanel></td></tr></table>
        </td></tr>
    <tr><td><span class="hide_print"><asp:Label ID="lblOverall_Performance_Comm" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" /></span></td></tr>
<tr><td><span class="hide_print">
<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" Width="100%" DataKeyNames="EMPLID" HeaderStyle-ForeColor="Blue" 
    Caption="Performance Rating History" CaptionAlign="Left" Font-Size="Smaller">
 <AlternatingRowStyle BackColor="#ccffff" />
 <Columns>
   <asp:BoundField DataField="DateTime" HeaderText="Date Time" HeaderStyle-BackColor="LightGray"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-VerticalAlign="Top"></asp:BoundField>
   <asp:BoundField DataField="Generalist" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" ItemStyle-VerticalAlign="Top"></asp:BoundField>
   
<asp:TemplateField HeaderText="Overall Performance Rating Comments" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
<asp:Label ID="LblRate_Comm" runat="server" Text='<%# Eval("Rate_Comm").ToString().Replace(Environment.NewLine, "<br />")%>' ></asp:Label></ItemTemplate></asp:TemplateField> 

</Columns></asp:GridView>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>    
    <tr><td><hr /></td></tr>

<!--5. Overall Summary-->
    <tr><td class="style8" >&nbsp;&nbsp;&nbsp;<u>Overall Summary:</u>&nbsp;<span class="style7">(Comment on overall performance)</span></td></tr>
    <tr><td><asp:Label ID="lblOverall_Summary" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" />
<asp:UpdatePanel ID="UpdatePanel_Overall_Summary" runat="server" UpdateMode="Conditional"><ContentTemplate>
<asp:TextBox ID="txbOverall_Summary"  Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" TextMode="MultiLine" Rows="5" Style="overflow:auto;" TabIndex="4"
     ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="Overall_Sum_TextChanged" onkeyup="ChangeColor(id);" /></ContentTemplate></asp:UpdatePanel></td></tr>
    <tr><td><span class="hide_print"><asp:Label ID="lblOverall_Summary_Comm" Width="100%" Borderstyle="Solid" BorderColor="LightGray" BorderWidth="1px" Font-Names="Calibri" Font-Size="Small" runat="server" /></span></td></tr>
<tr><td><span class="hide_print">
<asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" Width="100%" DataKeyNames="EMPLID" HeaderStyle-ForeColor="Blue" 
    Caption="Overall Summary History" CaptionAlign="Left" Font-Size="Smaller">
 <AlternatingRowStyle BackColor="#ccffff" />
 <Columns>
   <asp:BoundField DataField="DateTime" HeaderText="Date Time" HeaderStyle-BackColor="LightGray"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%"></asp:BoundField>
   <asp:BoundField DataField="Generalist" HeaderText="Generalist" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%"></asp:BoundField>
<asp:TemplateField HeaderText="Overall Summary Comments" HeaderStyle-BackColor="LightGray" HeaderStyle-HorizontalAlign="Left"><ItemTemplate>
<asp:Label ID="LblSum_Comm" runat="server" Text='<%# Eval("Sum_Comm").ToString().Replace(Environment.NewLine, "<br />")%>' ></asp:Label></ItemTemplate></asp:TemplateField>    
</Columns></asp:GridView>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
</span></td></tr>    
    <tr><td><hr /></td></tr>



    <tr><td>&nbsp;&nbsp;</td></tr>
    <tr><td><asp:Button ID="btnSubmit_Generalist" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" CssClass="Button_StyleSheet" Visible="false"/></td></tr>
    
    <tr><td>
<table width="100%">
   <tr><td style="text-align:left; width:30%;"><asp:Button ID="btnEditForm" runat="server" BackColor="Wheat" BorderStyle="None" CssClass="Button_StyleSheet" Font-Size="11pt" Visible="false" 
           OnClientClick="return confirm(&quot;Clicking OK will allow you to make edits to the existing text and send it back to HR.&quot;);" /></td>
      <td>
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
<asp:ImageButton ID="imgCal" runat="server" ImageUrl="../../Images/calendar.png" Visible="false" style="width: 16px; height: 16px"/>
   </td></tr>
</table>
<!--END Employee Declined-->
      </td>       
      <td style="text-align:right; width:30%;"><asp:Button ID="btnSendEmployee" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Visible="false" CssClass="Button_StyleSheet"/></td></tr></table>
        
        </td></tr>
    <tr><td></td></tr>
    <tr><td></td></tr>
</table>    
</asp:Panel>
 <!--Panel_Create_Appraisal END-->
</td><td class="style6"></td></tr>
  
<tr><td class="style6">&nbsp;&nbsp;</td>
    <td class="style1"><asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
            CssClass="Button_StyleSheet" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;" /></td>
    <td class="style6">&nbsp;&nbsp;</td></tr>
<tr><td class="style6">&nbsp;&nbsp;</td>
    <td class="style1">&nbsp;&nbsp;</td>
    <td class="style6">&nbsp;&nbsp;</td></tr>
<!--<tr><td class="style6">&nbsp;&nbsp;</td><td class="style1"><hr /></td><td class="style6">&nbsp;&nbsp;</td></tr>-->
</table>
    </form>
</body>
</html>
