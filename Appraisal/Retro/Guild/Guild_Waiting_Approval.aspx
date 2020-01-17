<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Guild_Waiting_Approval.aspx.vb" Inherits="Appraisal.Guild_Waiting_Approval" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Guild Performance</title>

<link href="StyleSheet1.css" rel="stylesheet" />

<style type="text/css">
       .Style0 {
   width:100%;
        }
        .Style1 {
  text-align:center;
  font-size:large;
  font-weight:bold;
        }
        .Style2 {
  text-align:center;
  font-size: small;
  font-weight:bold;
        }
        .Style3 {
            text-align: center;
            width: 10%;
        }
        .Style4 {
        height:2px; 
        font-size:2px;
        }
        .Style5
        {
            width: 230px;
        }
        .Style6
        {
            width: 435px;
        }
        .Style7
        {
            width: 333px;
        }
        .style33
       {
           width: 175px;
       }
       .Style11 {
 text-align:center;
  font-size:large;
   color: #00ae4d;      
  height:30px;
        }
       .style34
       {
           width: 237px;
       }
       .style35
       {
          
           width: 30px;
       }
      .style12
        {
        font-weight : bold;
        width: 18%;
        }
     @media print
          {    
          .no-print, .no-print *
          {
        display: none !important;
           }
           }

        </style>

<script language="javascript" type="text/javascript">
function winopen() {
        window.open("cal.aspx", "mywindow", "left=900,top=400,width=250,height=250,toolbar=0,resizable=0,").focus();
    }
    //-------------------------------------------------------
function OnClick() {
        if (divCalendar.style.display == "none")
            divCalendar.style.display = "";
        else
            divCalendar.style.display = "none";
    }
    //------------------------------------------------------
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
<asp:label id="lblGUILD_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblFirst_SUP_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblFirst_SUP_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblHR_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblHR_EMAIL" runat="server" Text="" Visible="false"/>


<!--PAGE 1-->
<!--<div id="Div1" runat="server" visible="true">-->
<table border="0">
  <tr><td class="Style3">&nbsp; &nbsp;</td>
      <td class="Style1"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td>
      <td class="Style3">&nbsp; &nbsp;</td></tr>
  <tr><td>&nbsp; &nbsp;</td><td class="Style1" style="color: #00ae4d;"><u>Performance Appraisal</u></td>
    <td>&nbsp; &nbsp;</td></tr>
  <tr><td>&nbsp; &nbsp;</td><td class="Style11">
    <div runat="server" id="FullAppraisal">October 1, <asp:Label ID="LblPrev_Year" runat="server" Text="" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/>
      &nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;September 30, <asp:Label ID="LblCur_Year" runat="server" Text="" ForeColor="#00ae4d" CssClass="Label_StyleSheet"/></div></td><td>&nbsp; &nbsp;</td></tr>
  <tr><td>&nbsp; &nbsp;</td>
    <td>
      <table class="Style0" border="0">
        <tr><td width="3%">&nbsp;&nbsp;</td>
            <td width="8%">Name:</td>
            <td width="30%"><asp:label id="lblGUILD_NAME" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/></td>
            <td width="15%">Manager Name:</td><td><asp:label id="lblFirst_SUP_NAME" runat="server" CssClass="Label_StyleSheet"/></td>
            <td nowrap="nowrap"><asp:label id="lblApproved_First_SUP" runat="server" Text="" CssClass="Label_StyleSheet"/>&nbsp;&nbsp;<asp:label id="lblDate_First_SUP_Approved" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/></td></tr>
        <tr><td width="3%">&nbsp;&nbsp;</td><td>Title:</td>
            <td><asp:label id="lblGUILD_Title" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/></td>
            <td nowrap="nowrap">2nd Level Manager Name:</td>
            <td width="20%"><asp:label id="lblUP_MGT_NAME" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/></td><td nowrap="nowrap"><asp:label id="lblApproved_UP_MGR" runat="server" Text="" CssClass="Label_StyleSheet"/>&nbsp;&nbsp;<asp:label id="lblDate_UP_Mgr_Approved" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/></td></tr>
        <tr><td width="3%">&nbsp;&nbsp;</td><td>Department:</td>
            <td><asp:label id="lblGUILD_Dept" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/></td>
            <td>HR Generalist:</td>
            <td><asp:label id="lblHR_NAME" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/></td>
            <td nowrap="nowrap"><asp:label id="lblApproved_HR" runat="server" Text="" CssClass="Label_StyleSheet" />&nbsp;&nbsp;<asp:label id="lblDate_HR_Approved" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/></td></tr>
        <tr><td width="3%">&nbsp;&nbsp;</td>
            <td nowrap="nowrap">Hire Date:</td>
            <td><asp:label id="lblGUILD_Hired" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/></td>
            <td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="6">&nbsp;</td></tr>
        </table></td>
    <td>&nbsp;&nbsp;</td></tr>

<tr><td>&nbsp;&nbsp;</td><td><asp:Panel ID="Panel11" runat="server" Visible="true" Width="100%"><font color="#00ae4d"><b><u>Key Tasks:</u></b></font>&nbsp;<i>Enter in the Key Task and description information in the space provided below. 
    Key Task information should be extracted from employee&#39;s job description. Rate each key task using the effectiveness factor that best relates to the duties of each individual key task.
The effectiveness factors may be rated by entering a check mark.</i></asp:Panel></td>
    <td>&nbsp;&nbsp;</td></tr>

<tr><td valign="top">&nbsp;&nbsp;</td>
    <td>
<!--Panel1 & GridView1 -->
<asp:Panel ID="Panel1" runat="server" Visible="true" Width="100%">

<table style="width:100%;" border="0">
   <tr><td class="Style4">&nbsp;&nbsp;</td><td class="Style4">&nbsp;&nbsp;</td></tr>
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>1) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask1" runat="server" Visible="false"/><%=KeyTask1.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource1" Width="100%" CssClass="Grid_StyleSheet"
        BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
    <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
    <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
    <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" runat="server"
            checked='<%# Eval("Exceptional")%>' Enabled="false"/></ItemTemplate></asp:TemplateField>
    <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" runat="server" 
            checked='<%# Eval("High")%>' Enabled="false"/></ItemTemplate></asp:TemplateField>
    <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" runat="server" 
            checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
    <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" runat="server" 
            checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
    <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" runat="server" 
            checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource>
   </td></tr>
   <tr><td>&nbsp;&nbsp;</td><td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating1" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet"/></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments1" runat="server" Visible="false" /><%=TaskComments1.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
  <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 

 </asp:Panel>
<!--</div>-->

<!--PAGE 2-->
<div id="Div2" runat="server" visible="false">
<!--Panel2 & GridView2 -->
<asp:Panel ID="Panel2" runat="server" Visible="false">
<table style="width:100%;" border="0">
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>2) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask2" runat="server" Visible="false"/><%=KeyTask2.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource2" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
    <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate>
       <asp:CheckBox ID="chkGW1" runat="server" checked='<%# Eval("Exceptional")%>' Enabled="false"/></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate>
       <asp:CheckBox ID="chkGW2" runat="server" checked='<%# Eval("High")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate>
       <asp:CheckBox ID="chkGW3" runat="server" checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate>
       <asp:CheckBox ID="chkGW4" runat="server" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate>
       <asp:CheckBox ID="chkGW5" runat="server" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>" ></asp:SqlDataSource></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating2" runat="server" Text="" Font-Bold="true" ForeColor="Blue"  CssClass="Label_StyleSheet" /></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments2" runat="server" Visible="false"/><%=TaskComments2.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
</asp:Panel> 
</div>


<!--PAGE 3-->
<div id="Div3" runat="server" visible="false">
<!--Panel3 & GridView3 -->
<asp:Panel ID="Panel3" runat="server" Visible="false">
<table style="width:100%;" border="0">
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>3) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask3" runat="server" Visible="false" /><%=KeyTask3.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource3" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" runat="server"  
            checked='<%# Eval("Exceptional")%>' Enabled="false"/></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" runat="server"  
            checked='<%# Eval("High")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" runat="server"  
            checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" runat="server"  
            checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" runat="server"  
            checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>" ></asp:SqlDataSource></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating3" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet"/></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments3" runat="server" Visible="false" /><%=TaskComments3.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table>
</asp:Panel> 
</div>

<!--PAGE 4-->
<div id="Div4" runat="server" visible="false">
<!--Panel4 & GridView4 -->
<asp:Panel ID="Panel4" runat="server" Visible="false">
<table style="width:100%;" border="0">
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>4) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask4" runat="server" Visible="false" /><%=KeyTask4.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource4" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" runat="server" 
            checked='<%# Eval("Exceptional")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" runat="server"  
            checked='<%# Eval("High")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" runat="server"
            checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" runat="server" 
            checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" runat="server" 
            checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>" ></asp:SqlDataSource></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating4" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments4" runat="server" Visible="false" /><%=TaskComments4.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
</asp:Panel> 
</div>

<!--PAGE 5-->
<div id="Div5" runat="server" visible="false">
<!--Panel5 & GridView5 -->
<asp:Panel ID="Panel5" runat="server" Visible="false">
<table style="width:100%;" border="0">
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>5) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask5" runat="server" Visible="false"/><%=KeyTask5.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
<asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource5" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" runat="server" 
            checked='<%# Eval("Exceptional")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" runat="server" 
            checked='<%# Eval("High")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" runat="server" 
            checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" runat="server" 
            checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" runat="server" 
            checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>" ></asp:SqlDataSource></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating5" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments5" runat="server" Visible="false"/><%=TaskComments5.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
</asp:Panel> 
</div>

<!--PAGE 6-->
<div id="Div6" runat="server" visible="false">

<!--Panel6 & GridView6 -->
<asp:Panel ID="Panel6" runat="server" Visible="false">
<table style="width:100%;" border="0">
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>6) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask6" runat="server" Visible="false"/><%=KeyTask6.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
<asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource6" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" runat="server" 
            checked='<%# Eval("Exceptional")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" runat="server" 
            checked='<%# Eval("High")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" runat="server" 
            checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" runat="server"  
            checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" runat="server"
            checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>" ></asp:SqlDataSource></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
   <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating6" runat="server" Text="" Font-Bold="true" ForeColor="Blue"  CssClass="Label_StyleSheet"/></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments6" runat="server" Visible="false"/><%=TaskComments6.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
</asp:Panel> 
</div>

<!--PAGE 7-->
<div id="Div7" runat="server" visible="false">
<!--Panel7 & GridView7 -->
<asp:Panel ID="Panel7" runat="server" Visible="false">

<table style="width:100%;" border="0">
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>7) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask7" runat="server" Visible="false"/><%=KeyTask7.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
<asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource7" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" runat="server" 
            checked='<%# Eval("Exceptional")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" runat="server" 
            checked='<%# Eval("High")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" runat="server"
            checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" runat="server" 
            checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" runat="server"
            checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating7" runat="server" Text="" Font-Bold="true" ForeColor="Blue"  CssClass="Label_StyleSheet"/></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments7" runat="server" Visible="false"/><%=TaskComments7.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
</asp:Panel> 
</div>

<!--PAGE 8-->
<div id="Div8" runat="server" visible="false">

<!--Panel8 & GridView8 -->
<asp:Panel ID="Panel8" runat="server" Visible="false">

<table style="width:100%;" border="0">
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>8) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask8" runat="server" Visible="false"/><%=KeyTask8.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
<asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource8" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" runat="server" 
            checked='<%# Eval("Exceptional")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" runat="server" 
            checked='<%# Eval("High")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" runat="server" 
            checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" runat="server" 
            checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" runat="server" 
            checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating8" runat="server" Text="" Font-Bold="true" ForeColor="Blue"  CssClass="Label_StyleSheet"/></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments8" runat="server" Visible="false"/><%=TaskComments8.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
</asp:Panel> 
</div>

<!--PAGE 9-->
<div id="Div9" runat="server" visible="false">

<!--Panel9 & GridView9 -->
<asp:Panel ID="Panel9" runat="server" Visible="false">
<table style="width:100%;" border="0">
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>9) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask9" runat="server" Visible="false"/><%=KeyTask9.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
<asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource9" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" runat="server"  
            checked='<%# Eval("Exceptional")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" runat="server" 
            checked='<%# Eval("High")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" runat="server" 
            checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" runat="server"  
            checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" runat="server" 
            checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating9" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments9" runat="server" Visible="false"/><%=TaskComments9.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
 </asp:Panel> 
</div>

<!--PAGE 10-->
<div id="Div10" runat="server" visible="false">

<!--Panel10 & GridView10 -->
<asp:Panel ID="Panel10" runat="server" Visible="false">

<table style="width:100%;" border="0">
   <tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>10) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask10" runat="server" Visible="false"/><%=KeyTask10.Text%></td></tr>
   <tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td>
<asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource10" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" SortExpression="Factor_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" SortExpression="Factor_Desc"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" runat="server"
            checked='<%# Eval("Exceptional")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" runat="server" 
            checked='<%# Eval("High")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" runat="server"
            checked='<%# Eval("Meets")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" runat="server"
            checked='<%# Eval("NeedsImprovement")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" runat="server"
            checked='<%# Eval("Unsatisfactory")%>' Enabled="false" /></ItemTemplate></asp:TemplateField>    
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating10" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
   <tr><td>&nbsp;&nbsp;</td>
       <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments10" runat="server" Visible="false"/><%=TaskComments10.Text%></td></tr>
   <tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
 </asp:Panel>
</div>


<table class="Style0" border="0">

<asp:Panel ID="Panel21" runat="server">
<tr><td class="Style1"><font color="#00ae4d"><u>Overall Summary</u></font></td></tr>

<tr><td style="font-weight:bold; color: #00ae4d;"><u>Overall Appraisal Rating:</u>&nbsp;&nbsp;<asp:Label ID="TaskRating11" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
    <tr><td></td>
    </tr>
<tr><td>
  <asp:Panel ID="Panel22" runat="server"> 
     <table class="Style0" border="0" cellspacing="0" cellpadding="0">
         <tr><td><font color="#00ae4d"><u><b>Manager's Overall Peformance Comments:</b></u></font>&nbsp;&nbsp;<asp:Label ID="SuperComments" runat="server" CssClass="Label_StyleSheet" Visible="false"/><%=SuperComments.Text%></td></tr></table>
  </asp:Panel>
</td></tr>

<tr><td>
<asp:Panel ID="Panel23" runat="server"> 
<table class="Style0">
   <tr><td style="font-size:1px; height:1px;"><hr /></td></tr>
   <tr><td style="font-weight:bold;">PERFORMANCE IMPROVEMENT/EMPLOYEE DEVELOPMENT OBJECTIVES</td></tr>
   <tr><td>
<table width="100%" border="1" cellpadding="1" cellspacing="0" style="border-color:black; border-style:solid;">
   <tr><td width="42%"><b>Key Task/Effectiveness Factor</b></td>
       <td width="42%"><b>Improvement/Development Objective</b></td>
       <td><b>Target Date</b></td></tr>
   <tr><td valign="top">&nbsp;&nbsp;<asp:Label ID="SuperComments1" runat="server" Width="98%" CssClass="Label_StyleSheet"/> </td>
       <td valign="top">&nbsp;&nbsp;<asp:Label ID="SuperComments2" runat="server" Width="98%" CssClass="Label_StyleSheet"/>  </td>
       <td valign="top">&nbsp;&nbsp;<asp:Label ID="SuperComments3" runat="server" Width="98%" CssClass="Label_StyleSheet"/> </td></tr>
</table>
   </td></tr>
</table>
</asp:Panel>
</td></tr>

<tr><td style="font-size:1px; height:1px;"><hr /></td></tr>
<tr><td>
<asp:Panel ID="FutureTask" runat="server">
<table width="100%" border="0" style="background-color:#e5eaed;">
<!--Future Rating Criteria/Goals-->
    <tr><td class="Style11" colspan="2">Rating Criteria/Goals (October 1, <asp:label id="lblCur_Year1" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/>&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;September 30, <asp:label id="lblNext_Year" runat="server" Text="" Visible="true" CssClass="Label_StyleSheet"/>)</td></tr>
<!--1) Key Future Task/Description-->
<tr><td class="style12" valign="top">1) Key Task Description:</td><td><asp:Label ID="FutKeyTask1" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
<asp:Panel ID="Panel12" runat="server">
<!--2) Key Future Task/Description-->
<tr><td class="style12" valign="top">2) Key Task Description:</td><td><asp:Label ID="FutKeyTask2" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel13" runat="server" Visible="true" Wrap="True">
<!--3) Key Future Task/Description-->
<tr><td class="style12" valign="top">3) Key Task Description:</td><td><asp:Label ID="FutKeyTask3" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel14" runat="server" Visible="true">
<!--4) Key Future Task/Description-->
<tr><td class="style12" valign="top">4) Key Task Description:</td><td><asp:Label ID="FutKeyTask4" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel15" runat="server" Visible="true">
<!--5) Key Future Task/Description-->
<tr><td class="style12" valign="top">5) Key Task Description:</td><td><asp:Label ID="FutKeyTask5" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel16" runat="server" Visible="true">
<!--6) Key Future Task/Description-->
<tr><td class="style12" valign="top">6) Key Task Description:</td><td><asp:Label ID="FutKeyTask6" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel17" runat="server" Visible="true">
<!--7) Key Future Task/Description-->
<tr><td class="style12" valign="top">7) Key Task Description:</td><td><asp:Label ID="FutKeyTask7" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel18" runat="server" Visible="true">
<!--8) Key Future Task/Description-->
<tr><td class="style12" valign="top">8) Key Task Description:</td><td><asp:Label ID="FutKeyTask8" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel19" runat="server" Visible="true">
<!--9) Key Future Task/Description-->
<tr><td class="style12" valign="top">9) Key Task Description:</td><td><asp:Label ID="FutKeyTask9" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet"/></td></tr>
</asp:Panel>
<asp:Panel ID="Panel20" runat="server" Visible="true">
<!--10) Key Future Task/Description-->
<tr><td class="style12" valign="top">10) Key Task Description:</td><td><asp:Label ID="FutKeyTask10" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
</table>
</asp:Panel>

</td></tr>
<tr>
<td>

<table width="99%">
<tr><td style="font-weight:bold; width:13%" nowrap="nowrap" colspan="2"><asp:Label ID="Disc_Com" text="Send suggested revisions to " runat="server" CssClass="Label_StyleSheet"/></td></tr>
<tr><td colspan="2">
   
<asp:UpdatePanel ID="UpdatePanel_DiscussionComments" runat="server" UpdateMode="Conditional">
<ContentTemplate>    
    <asp:TextBox ID="DiscussionComments" runat="server" Width="99%" TextMode="MultiLine" Borderstyle="Solid" BorderColor="black" Rows="3" class="TextBox_StyleSheet" onkeyup="SetButtonStatus(this, 'target'); "/>
    </ContentTemplate>
    <triggers>
        <asp:AsyncPostBackTrigger ControlID="DiscussionComments" EventName="TextChanged" />
    </triggers>
    </asp:UpdatePanel> 

    </td></tr>
<tr><td width="62%"></td><td ><asp:Label ID="lblRefuseText" runat="server" ForeColor="blue" Font-Size="Smaller" Text="" CssClass="Label_StyleSheet"/>
</td></tr>
</table></td></tr>

<tr><td>

<table width="99%" border="0">
    <tr><td align="left"><asp:Button ID="Discuss" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" text="Revise"/></td>
        <td align="right"><asp:Button ID="Approve" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" text="Approve"/></td>
        <td nowrap="nowrap" align="right">
           &nbsp;<asp:Button ID="Generalist" runat="server" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Text="Submit for review to" />
                <!--<asp:DropDownList ID="DDLGeneralist" runat="server" />-->
            &nbsp;&nbsp;</td>

   </tr>            
</table>

</td></tr>

<tr><td>

<table width="100%" border="0">
    <tr><td align="center" width="50%">
            <asp:button id="EditRecords" runat="server" Text="Edit" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Width="100px" Visible="false" OnClientClick="return confirm(&quot;If you want to start the approval process again, please press OK. &quot;);"/></td>
       <td align="center">
<div id="divCalendar" style="position:absolute;">
<asp:Calendar id="Calendar1" runat="server" BorderWidth="2px" BackColor="White" Width="200px" ForeColor="Black" Height="180px" Font-Size="11pt" Font-Names="Calibri"  Visible="false"
BorderColor="#999999" BorderStyle="Outset" DayNameFormat="FirstLetter" CellPadding="4" >
<TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle><SelectorStyle BackColor="#CCCCCC"></SelectorStyle><NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle><SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
<TitleStyle Font-Bold="True" BorderColor="Black"  BackColor="#999999"></TitleStyle><WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle><OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
</asp:Calendar></div>

<asp:button id="SendToEmp" runat="server" Text="Send To Employee" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" Visible="false" />
<asp:button id="RefuseSign" runat="server" Text="Employee Declined to Sign" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" width="180px" Visible="false" 
OnClientClick="return confirm(&quot;Are you sure you want to indicate that the employee declined to sign?&quot;);"/>

<asp:TextBox ID="txtCal" runat="server" Width="70px" BorderStyle="Solid" BorderColor="Silver" Visible="false" ReadOnly="true" class="TextBox_StyleSheet"/>
<asp:ImageButton ID="imgCal" runat="server" ImageUrl="../../Images/calendar.png" Visible="false" />
</td></tr>
<tr><td align="center" colspan="2"><asp:Label ID="LblGuild_Submitted" runat="server" text="" CssClass="Label_StyleSheet"/></td></tr>
</table>
</td></tr>
<tr><td><asp:Label ID="LblGuildComments" runat="server" Width="99%" BorderWidth="1" BorderColor="Gray" text="" CssClass="Label_StyleSheet"/></td></tr>

<tr><td align="center">
<table width="100%">
<tr><td align="center"><asp:Label ID="Never_Completed" runat="server" Text="" Font-Bold="true" CssClass="Label_StyleSheet" ForeColor="#00ae4d"></asp:Label> </td></tr>
        
<tr><td align="center">
<input id="Button2" type="button" value="Print" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;visibility:hidden; " onclick="window.print();"/>
&nbsp;&nbsp;&nbsp;&nbsp;</td></tr></table>
</td></tr>
<tr><td align="center">&nbsp;&nbsp;</td></tr>

<tr><td align="center">
    
    <asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" 
        OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/>
    <!--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Images/Back_button.png" style="width:90px" />-->
    </td></tr>

</table>

</td><td valign="top">&nbsp;</td></tr></table>

    </form>
</body>
</html>