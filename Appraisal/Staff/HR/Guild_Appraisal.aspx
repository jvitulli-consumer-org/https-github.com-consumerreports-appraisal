<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Guild_Appraisal.aspx.vb" Inherits="Appraisal.Guild_Appraisal" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <title>Guild Review</title>

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
  text-align:center;
  width:15%;
        }
        .auto-style3 {
            width: 59px;
            font-weight:bold;
        }
        p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:12.0pt;
	      margin-left: 0in;
           margin-right: 0in;
           margin-top: 0in;
       }
       
        .Style4
       {
           width: 377px;
       }
       
        .Style5
       {
           width: 539px;
       }
       
        .Style6
       {
           height: 30px;
           width: 181px;
       }
       .Style7
       {
           width: 29%;
       }
       
        .style33
       {
           width: 175px;
       }
       .Style11 {
   text-align:center;
   font-size:large;
   height:30px;
   color: #00ae4d;     
        }
       .style34
       {
           width: 237px;
       }
       .style35
       {
           height: 30px;
           width: 460px;
       }
       .style36
       {
           width: 460px;
       }
 
     .style12
        {
        font-weight : bold;
        width: 18%;
         }
      .StyleBreak
         {
        page-break-after: always;
         }
        
</style>
</head>
<body>

<form id="form1" runat="server">


<asp:label id="lblEMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_EMAIL" runat="server" Text="" Visible="false"/>
<asp:label id="lblUP_MGT_NAME" runat="server" Text="" Visible="false"/>
<asp:label id="lblFirst_SUP_NAME" runat="server" Text="" Visible="false"/>
<asp:label id="LblFirst_SUP_EMPLID" runat="server" Text="" Visible="false"/>
<asp:label id="Label1" runat="server" Text="" Visible="false" />
<asp:label id="lblYEAR" runat="server" Text="" Visible="false"/>
<asp:label id="lblEMPL_Supervision" runat="server" Text="" Visible="false"/>
<asp:label id="LblDEPTID" runat="server" Text="" Visible="false"/>
<asp:label id="LblSTATUS" runat="server" Text="" Visible="false"/>

<asp:label id="LblEligible_Full_Review" runat="server" Text="" Visible="false" CssClass="Label_StyleSheet"/>

<!--PAGE 1-->
<div id="Div1" class="StyleBreak" runat="server" visible="true">

<table class="Style0" border="0">
   <tr><td class="Style3">&nbsp;</td><td colspan="4" class="Style1"><img alt="../../images/CR_logo.png" src="../../images/CR_logo.png" style="width: 380px; height:60px" /></td><td class="Style3">&nbsp;</td></tr> 
   <tr><td class="Style3">&nbsp;</td><td colspan="4" class="Style1" style="color: #00ae4d;"><u>Performance Appraisal</u></td><td class="Style3">&nbsp;</td></tr> 

   <tr><td class="Style3">&nbsp;</td><td colspan="4" class="Style11">October 1, <%=Session("Prev_Year")%>&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;September 30, <%=Session("Cur_Year")%></td><td class="Style3">&nbsp;</td></tr> 

   <tr><td class="Style3">&nbsp;</td><td colspan="4">
       <table class="Style0" border="0">
        <tr><td width="3%">&nbsp;&nbsp;</td>
            <td width="8%">Name:</td>
            <td width="30%"><%=Session("GUILD_NAME")%></td>
            <td width="15%">Manager Name:</td>
            <td><%=Session("First_SUP_NAME")%></td>
            <td nowrap="nowrap">Approved:&nbsp;&nbsp;<%=Session("Mgr_Apr")%></td></tr>
        <tr><td width="3%">&nbsp;&nbsp;</td>
            <td>Title:</td>
            <td><%=Session("GUILD_Title")%></td>
            <td nowrap="nowrap">2nd Level Manager Name:</td>
            <td width="20%"><%=Session("UP_MGT_NAME")%></td>
            <td nowrap="nowrap">Approved:&nbsp;&nbsp;<%=Session("UP_Mgt_Apr")%></td></tr>
        <tr><td width="3%">&nbsp;&nbsp;</td>
            <td>Department:</td><td><%=Session("GUILD_Dept")%></td>
            <td>HR Generalist:</td><td><%=Session("HR_NAME")%></td>
            <td nowrap="nowrap">Approved:&nbsp;&nbsp;<%=Session("HR_Apr")%></td></tr>
        <tr><td width="3%">&nbsp;&nbsp;</td>
            <td nowrap="nowrap">Hire Date:</td><td><%=Session("GUILD_Hired")%></td>
            <td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="6">&nbsp;</td></tr>
      </table></td><td class="Style3">&nbsp;</td></tr> 

  <tr><td class="Style3"></td><td colspan="4">
<asp:Panel ID="Panel11" runat="server" Visible="true" Width="100%"><font color="#00ae4d"><b><u>Key Tasks:</u></b></font>&nbsp;
<i>Enter in the Key Task and description information in the space provided below. Key Task information should be extracted from employee&#39;s job description. 
      Rate each key task using the effectiveness factor that best relates to the duties of each individual key task. The effectiveness factors may be rated by entering a check mark.</i>
</asp:Panel>
</td><td class="Style3"></td></tr>
<!--<tr><td class="Style3">&nbsp;</td><td colspan="4">&nbsp;</td><td class="Style3">&nbsp;</td></tr>-->
<tr><td class="Style3">&nbsp;</td><td colspan="4">

<!--Panel1 & GridView1 -->
<asp:Panel ID="Panel1" runat="server" Visible="true" Width="100%">

<table style="width:100%;" border="0">
<tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>1) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask1" runat="server" Visible="false"/><%=KeyTask1.Text%></td></tr>
<!--<tr><td class="Style4">&nbsp; &nbsp;</td><td class="Style4">&nbsp; &nbsp;</td></tr>-->
<tr><td>&nbsp;&nbsp;</td>
    <td>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource1" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating1" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet"/></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments1" runat="server" Visible="false"/><%=TaskComments1.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
</asp:Panel>
</td><td class="Style3">&nbsp;</td></tr>
</table>
</div>                  

<!--PAGE 2-->
<div id="Div2" class="StyleBreak"  runat="server" visible="false">
<table class="Style0" border="0">
<tr><td class="Style3">&nbsp;&nbsp;</td>
    <td>
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
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating2" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments2" runat="server" Visible="false"/><%=TaskComments2.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 

</asp:Panel> 
</td><td class="Style3">&nbsp;&nbsp;</td></tr>
</table>
</div>

<!--PAGE 3-->
<div id="Div3" class="StyleBreak" runat="server" visible="false">

<table class="Style0" border="0">
<tr><td class="Style3">&nbsp;&nbsp;</td>
    <td>
<!--Panel3 & GridView3 -->
<asp:Panel ID="Panel3" runat="server" Visible="false">

<table style="width:100%;" border="0">
<tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>3) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask3" runat="server" Visible="false"/><%=KeyTask3.Text%></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td>
<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource3" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating3" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet"/></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments3" runat="server" Visible="false"/><%=TaskComments3.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 

</asp:Panel> 
</td><td class="Style3">&nbsp;&nbsp;</td></tr>
</table>
</div>

<!--PAGE 4-->
<div id="Div4" class="StyleBreak" runat="server" visible="false">
<table class="Style0" border="0">
<tr><td class="Style3">&nbsp;&nbsp;</td>
    <td>
<!--Panel4 & GridView4 -->
<asp:Panel ID="Panel4" runat="server" Visible="false">

<table style="width:100%;" border="0">
<tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>4) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask4" runat="server" Visible="false"/><%=KeyTask4.Text%></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td>
<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource4" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating4" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet"/></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments4" runat="server" Visible="false"/><%=TaskComments4.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 

</asp:Panel> 
</td><td class="Style3">&nbsp;&nbsp;</td></tr>
</table>
</div>


<!--PAGE 5-->
<div id="Div5" class="StyleBreak" runat="server" visible="false">
<table class="Style0" border="0">
<tr><td class="Style3">&nbsp;&nbsp;</td>
    <td>
<!--Panel5 & GridView5 -->
<asp:Panel ID="Panel5" runat="server" Visible="false">

<table style="width:100%;" border=0">
<tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>5) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask5" runat="server" Visible="false"/><%=KeyTask5.Text%></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td>
<asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource5" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating5" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments5" runat="server" Visible="false"/><%=TaskComments5.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 

</asp:Panel> 
</td><td class="Style3">&nbsp;&nbsp;</td></tr>
</table>
</div>

<!--PAGE 6-->
<div id="Div6" class="StyleBreak" runat="server" visible="false">
<table class="Style0" border="0">
<tr><td class="Style3">&nbsp;&nbsp;</td>
    <td>
<!--Panel6 & GridView6 -->
<asp:Panel ID="Panel6" runat="server" Visible="false">

<table style="width:100%;" border="0">
<tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>6) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask6" runat="server" Visible="false"/><%=KeyTask6.Text%></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td>
<asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource6" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating6" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments6" runat="server" Visible="false"/><%=TaskComments6.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 

</asp:Panel> 
</td><td class="Style3">&nbsp;&nbsp;</td></tr>
</table>
</div> 


<!--PAGE 7-->
<div id="Div7" class="StyleBreak" runat="server" visible="false">
<table class="Style0" border="0">
<tr><td class="Style3">&nbsp;&nbsp;</td>
    <td>
<!--Panel7 & GridView7 -->
<asp:Panel ID="Panel7" runat="server" Visible="false">

<table style="width:100%;" border="0">
<tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>7) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask7" runat="server" Visible="false"/><%=KeyTask7.Text%></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td>
<asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource7" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating7" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments7" runat="server" Visible="false"/><%=TaskComments7.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 
     
</asp:Panel> 
</td><td class="Style3">&nbsp;&nbsp;</td></tr>
</table>
</div>

<!--PAGE 8-->
<div id="Div8" class="StyleBreak" runat="server" visible="false">
<table class="Style0" border="0">
<tr><td class="Style3">&nbsp;&nbsp;</td>
    <td>
<!--Panel8 & GridView8 -->
<asp:Panel ID="Panel8" runat="server" Visible="false">

<table style="width:100%;" border="0">
<tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>8) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask8" runat="server" Visible="false"/><%=KeyTask8.Text%></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td>
<asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource8" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" Font-Size="0px" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating8" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments8" runat="server" Visible="false"/><%=TaskComments8.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 

</asp:Panel> 
</td><td class="Style3">&nbsp;&nbsp;</td></tr>
</table>
</div>

<!--PAGE 9-->
<div id="Div9" class="StyleBreak" runat="server" visible="false">
<table class="Style0" border="0">
<tr><td class="Style3">&nbsp;&nbsp;</td>
    <td>
<!--Panel9 & GridView9 -->
<asp:Panel ID="Panel9" runat="server" Visible="false">

<table style="width:100%;" border="0">
<tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>9) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask9" runat="server" Visible="false"/><%=KeyTask9.Text%></td></tr>

<tr><td>&nbsp;&nbsp;</td>
    <td>
<asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource9" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating9" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments9" runat="server" Visible="false"/><%=TaskComments9.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 

</asp:Panel> 
</td><td class="Style3">&nbsp;&nbsp;</td></tr>
</table>
</div>

<!--PAGE 10-->
<div id="Div10" class="StyleBreak" runat="server" visible="false">
<table class="Style0" border="0">
<tr><td class="Style3">&nbsp;&nbsp;</td>
    <td>
<!--Panel10 & GridView10 -->
<asp:Panel ID="Panel10" runat="server" Visible="false">

<table style="width:100%;" border="0">
<tr><td width="2%">&nbsp;&nbsp;</td><td><font color="#00ae4d"><u><b>10) Key Task Description:</b></u></font>&nbsp;&nbsp;<asp:Label ID="KeyTask10" runat="server" Visible="false"/><%=KeyTask10.Text%></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td>
<asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLID" DataSourceID="SqlDataSource10" Width="100%" CssClass="Grid_StyleSheet"
    BorderStyle="Solid" HeaderStyle-BackColor="LightGray" ItemStyle-BorderStyle="Solid">
 <Columns>
   <asp:BoundField DataField="Factor_Name" HeaderText="Effectiveness Factor" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:BoundField DataField="Factor_Desc" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid"/>
   <asp:TemplateField HeaderText="Exceptional" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW1" checked='<%# Eval("Exceptional")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>   
   <asp:TemplateField HeaderText="High" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW2" checked='<%# Eval("High")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Meets" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW3" checked='<%# Eval("Meets")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Needs Improvement" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW4" checked='<%# Eval("NeedsImprovement")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField>
   <asp:TemplateField HeaderText="Unsatisfactory" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" HeaderStyle-Width="100px"><ItemTemplate><asp:CheckBox ID="chkGW5" checked='<%# Eval("Unsatisfactory")%>' Enabled="false" runat="server" /></ItemTemplate></asp:TemplateField> 
 </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:GlobalSQLDataConnection %>"></asp:SqlDataSource></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Overall Task Rating:</b>&nbsp;&nbsp;<asp:Label ID="TaskRating10" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td></tr>
<tr><td>&nbsp;&nbsp;</td>
    <td><b>Comments:</b>&nbsp;&nbsp;<asp:Label ID="TaskComments10" runat="server" Visible="false"/><%=TaskComments10.Text%></td></tr>
<tr><td style="font-size:1px;">&nbsp;&nbsp;</td><td style="background-color: gray; font-size:1px; height:1px;">&nbsp;</td></tr>
<!--<tr><td style="font-size:xx-small;">&nbsp;</td><td style="font-size:xx-small;">&nbsp;&nbsp;</td></tr>-->
</table> 

</asp:Panel> 
</td><td class="Style3">&nbsp;&nbsp;</td></tr>
</table>
</div>

<!--PAGE 11-->
<div id="Div11" class="StyleBreak" visible="true">
<table class="Style0" border="0">
<asp:Panel ID="Panel21" runat="server">
<tr><td class="Style1" style="color: #00ae4d;" colspan="3"><u>Overall Summary</u></td></tr>

<tr><td class="Style3">&nbsp;&nbsp;</td>    
    <td><font color="#00ae4d"><b><u>Overall Appraisal Rating:</u></b></font>&nbsp;<asp:Label ID="TaskRating11" runat="server" Text="" Font-Bold="true" ForeColor="Blue" CssClass="Label_StyleSheet" /></td>
<td class="Style3">&nbsp;&nbsp;</td></tr>   
<tr><td style="font-size:1px; height:1px;">&nbsp;</td><td style="font-size:1px; background-color:Gray; height:1px;">&nbsp;</td><td  style="font-size:1px; height:1px;">&nbsp;</td></tr>

</asp:Panel>
<tr><td></td>
<td>
<asp:Panel ID="Panel22" runat="server">
   <table class="Style0">
     <tr><td><font color="#00ae4d"><b><u>Manager's Overall Peformance Comments:</u></b></font>&nbsp;&nbsp;<asp:Label ID="SuperComments" runat="server" Visible="false"/><%=SuperComments.Text%></td></tr></table>
</asp:Panel>
</td>
<td></td></tr>

<tr><td style="font-size:1px; height:1px;">&nbsp;</td><td style="font-size:1px; background-color:Gray; height:1px;">&nbsp;</td><td  style="font-size:1px; height:1px;">&nbsp;</td></tr>
<tr><td>&nbsp;&nbsp;</td>
<td>
<asp:Panel ID="Panel23" runat="server"> 
<table class="Style0">
<tr><td style="font-weight:bold;">PERFORMANCE IMPROVEMENT/EMPLOYEE DEVELOPMENT OBJECTIVES</td></tr>
<tr><td>
<table width="100%" border="1" cellpadding="1" cellspacing="0" style="border-color:black; border-style:solid;">
<tr><td width="42%"><b>Key Task/Effectiveness Factor</b></td>
    <td width="42%"><b>Improvement/Development Objective</b></td>
    <td><b>Target Date</b></td></tr>
<tr><td valign="top"><asp:Label ID="SuperComments1" runat="server" Width="99%" CssClass="Label_StyleSheet"/></td>
    <td valign="top"><asp:Label ID="SuperComments2" runat="server" Width="99%" CssClass="Label_StyleSheet"/></td>
    <td valign="top"><asp:Label ID="SuperComments3" runat="server" Width="99%" CssClass="Label_StyleSheet"/></td></tr>
</table>
</td></tr>
</table>
</asp:Panel>
</td>
    
<td>&nbsp;&nbsp;</td></tr>

<asp:Panel ID="FutureTask" runat="server">

<tr><td style="font-size:1px; height:3px;">&nbsp;</td><td style="font-size:1px; background-color:Gray; height:1px;">&nbsp;</td><td  style="font-size:1px; height:1px;">&nbsp;</td></tr>
<tr><td style="height:30px;">&nbsp;</td>
<td><table width="100%" border="0" style="background-color:#e5eaed;">
<!--Future Rating Criteria/Goals-->
<tr><td class="Style11" colspan="2">Rating Criteria/Goals (October 1, <%=Session("Cur_Year")%>&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;September 30, <%=Session("Next_Year")%>)</td></tr>
<!--1) Key Future Task/Description-->
<tr><td class="style12" valign="top">1) Key Task Description:</td>
    <td><asp:Label ID="FutKeyTask1" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
<asp:Panel ID="Panel12" runat="server">
<!--2) Key Future Task/Description-->
<tr><td class="style12" valign="top">2) Key Task Description:</td>
    <td><asp:Label ID="FutKeyTask2" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel13" runat="server" Visible="true">
<!--3) Key Future Task/Description-->
<tr><td class="style12" valign="top">3) Key Task Description:</td>
    <td><asp:Label ID="FutKeyTask3" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel14" runat="server" Visible="true">
<!--4) Key Future Task/Description-->
<tr><td class="style12" valign="top">4) Key Task Description:</td>
    <td><asp:Label ID="FutKeyTask4" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel15" runat="server" Visible="true">
<!--5) Key Future Task/Description-->
<tr><td class="style12" valign="top">5) Key Task Description:</td>
     <td><asp:Label ID="FutKeyTask5" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel16" runat="server" Visible="true">
<!--6) Key Future Task/Description-->
<tr><td class="style12" valign="top">6) Key Task Description:</td>
     <td><asp:Label ID="FutKeyTask6" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel17" runat="server" Visible="true">
<!--7) Key Future Task/Description-->
<tr><td class="style12" valign="top">7) Key Task Description:</td>
     <td><asp:Label ID="FutKeyTask7" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel18" runat="server" Visible="true">
<!--8) Key Future Task/Description-->
<tr><td class="style12" valign="top">8) Key Task Description:</td>
     <td><asp:Label ID="FutKeyTask8" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>
<asp:Panel ID="Panel19" runat="server" Visible="true">
<!--9) Key Future Task/Description-->
<tr><td class="style12" valign="top">9) Key Task Description:</td>
     <td><asp:Label ID="FutKeyTask9" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet"/></td></tr>
</asp:Panel>

<asp:Panel ID="Panel20" runat="server" Visible="true">
<!--10) Key Future Task/Description-->
<tr><td class="style12" valign="top">10) Key Task Description:</td>
     <td><asp:Label ID="FutKeyTask10" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray" CssClass="Label_StyleSheet" /></td></tr>
</asp:Panel>

</table>
</td>

<td>&nbsp;</td></tr>

<tr><td style="font-size:1px; height:3px;">&nbsp;</td><td style="font-size:1px; background-color:Gray; height:1px;">&nbsp;</td><td  style="font-size:1px; height:1px;">&nbsp;</td></tr>
</asp:Panel>

<tr><td>&nbsp;</td><td style="font-size:large; font-weight:bold; text-align:center;"><!--<br/>-->
            Pressing the submit button means that I have reviewed and discussed this appraisal with my immediate supervisor.<br/><br/>
It does not necessarily imply that I agree with this evaluation.<br/><br/>           
</td><td>&nbsp;</td></tr>
<tr><td>&nbsp;</td><td>
     <table width="100%"><tr><td valign="top" class="auto-style3"><b>Comments:&nbsp;</b></td>
               <td valign="top"><asp:Textbox ID="GuildComments" runat="server" Width="99%" TextMode="MultiLine" Borderstyle="Solid" BorderColor="LightGray" Rows="3" Style="overflow:auto;" ReadOnly="true" />
               <asp:Label ID="LblGuildComments" runat="server" Width="99%" Borderstyle="Solid" BorderColor="Gray"  BorderWidth="1" Visible="false" CssClass="Label_StyleSheet"/> </td></tr></table>

</td><td>&nbsp;</td></tr>


<tr><td>&nbsp;</td><td style="text-align:center">
<asp:button id="GLD_Submit" runat="server" Text="Submit" BackColor="Wheat" BorderStyle="None" Font-Size="11pt" width="120px" Enabled="false" />
<asp:Label ID="LblGuild_Submitted" runat="server" text="" CssClass="Label_StyleSheet"/></td><td>&nbsp;</td></tr>
<tr><td>&nbsp;&nbsp;</td><td></td><td>&nbsp;&nbsp;</td></tr>
<tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>

<tr><td class="Style1" colspan="3">
<asp:Button ID="Button1" Text="Close" runat="server" style="border-style:none; color:Blue; width:120px; font-weight:bold; cursor: pointer;" OnClientClick="javascript:window.open('','_self',''); window.close(); return false;"/></td></tr>
</table>
</div>

    </form>
</body>
</html>