<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="ViewDoctor.aspx.vb" Inherits="HLConline.ViewDoctor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
  <tr>
    <td align="right"><asp:Button ID="btnChange" runat="server" CssClass="ContentButton" Text="Change Info" /></td>
  </tr>
</table>    
<table width="100%" cellpadding="4" cellspacing="0" border="0">
  <tr>
    <td width="50%" valign="top">
    <%-- BEGIN Doctor name and address information  --%>    
      <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td colspan="4" align="center" valign="middle">
          <asp:Label ID="lblName" runat="server" CssClass="ContentText" Font-Size="14pt"></asp:Label>
          <br />&nbsp;
          </td>
        </tr>
        <tr>
          <td colspan="4">
          <asp:Label ID="lblAddress" runat="server" CssClass="ContentText" Text=""></asp:Label>
          </td>
        </tr>
        <tr>
          <td colspan="4">
          &nbsp;
          </td>
        </tr>
        <tr>
          <td style="width: 55px"><asp:Label runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Office:"></asp:Label></td>
          <td><asp:Label ID="lblOfficePhone1" runat="server" CssClass="ContentText"></asp:Label></td>
          <td style="width: 55px"><asp:Label runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Mobile:"></asp:Label></td>
          <td><asp:Label ID="lblMobilePhone" runat="server" CssClass="ContentText"></asp:Label></td>
        </tr>
        <tr>
          <td style="width: 55px"><asp:Label runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Fax:"></asp:Label></td>
          <td><asp:Label ID="lblFax" runat="server" CssClass="ContentText"></asp:Label></td>
          <td style="width: 55px"><asp:Label runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Pager:"></asp:Label></td>
          <td><asp:Label ID="lblPager" runat="server" CssClass="ContentText"></asp:Label></td>
        </tr>
        <tr>
          <td style="width: 55px"><asp:Label ID="lblOfficePhone2Label" runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Office #2:" Visible="false"></asp:Label></td>
          <td><asp:Label ID="lblOfficePhone2" runat="server" CssClass="ContentText"></asp:Label></td>
          <td style="width: 55px"><asp:Label runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Home:"></asp:Label></td>
          <td><asp:Label ID="lblHomePhone" runat="server" CssClass="ContentText"></asp:Label></td>
        </tr>
        <tr>
          <td colspan="4">
          &nbsp;
          </td>
        </tr>
        <tr>
          <td style="width: 55px"><asp:Label runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Email:"></asp:Label></td>
          <td colspan="3"><asp:Label ID="lblEmailAddress" runat="server" CssClass="ContentText"></asp:Label></td>
        </tr>
        <tr>
          <td style="width: 55px"><asp:Label ID="lblWebsiteURLLabel" runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Website:" Visible="false"></asp:Label></td>
          <td colspan="3"><asp:Label ID="lblWebsiteURL" runat="server" CssClass="ContentText"></asp:Label></td>
        </tr>
        <tr>
          <td colspan="4">
          &nbsp;
          </td>
        </tr>
        <tr>
          <td style="width: 55px"><asp:Label ID="Label3" runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Status:" Visible="true"></asp:Label></td>
          <td><asp:Label ID="lblStatus" runat="server" CssClass="ContentText"></asp:Label></td>
          <td style="width: 55px"></td>
          <td><asp:CheckBox ID="chkIsBSMP" runat="server" Text="Member of BSMP" /></td>
        </tr>
        <tr>
          <td style="width: 55px">&nbsp;</td>
          <td>&nbsp;</td>
          <td style="width: 55px">&nbsp;</td>
          <td><asp:CheckBox ID="chkIsHRP" runat="server" Text="High Risk Pregnancy doctor" /></td>
        </tr>
        <tr>
          <td style="width: 55px" valign="top"><asp:Label ID="lblPeerReviewLabel" runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Peer Review:" Visible="false"></asp:Label></td>
          <td colspan="3"><asp:Label ID="lblPeerReview" runat="server" CssClass="LabelDataEntry" Font-Bold="false" Visible="false"></asp:Label></td>
        </tr>
      </table>
    <%-- END  Doctor name and address information  --%>    
    </td>
    <td width="2%">&nbsp;</td>
    <td width="48%" valign="top">
    <%-- BEGIN Right-hand side information  --%>   
      <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td align="center">
          <asp:Image runat="server" ID="imgAttitude" ImageUrl="" />
          <br />&nbsp;
          </td>
        </tr>
        <tr>
          <td align="left" bgcolor="#404040"><asp:Label ID="Label2" runat="server" CssClass="TableHeading" Text="&nbsp;Attitude"></asp:Label></td>
        </tr>    
        <tr>
          <td>
          <asp:Label ID="lblAttitudeText" runat="server" CssClass="ContentText" Text="Specific Attitude selections for this Doctor have not yet been entered."></asp:Label>
          <br />&nbsp;
          </td>
        </tr>
        <tr>
          <td align="left" bgcolor="#404040"><asp:Label runat="server" CssClass="TableHeading" Text="&nbsp;Hospitals"></asp:Label></td>
        </tr>    
        <tr>
          <td>
          <asp:Label ID="lblHospital" runat="server" CssClass="ContentText" Text="Hospitals for this Doctor have not yet been entered."></asp:Label>
          <br />&nbsp;
          </td>
        </tr>    
        <tr>
          <td align="left" bgcolor="#404040"><asp:Label runat="server" CssClass="TableHeading" Text="&nbsp;Specialty"></asp:Label></td>
        </tr>    
        <tr>
          <td>
          <asp:Label ID="lblSpecialty" runat="server" CssClass="ContentText" Text="Specialty(s) for this Doctor have not yet been entered."></asp:Label>
          <br />&nbsp;
          </td>
        </tr>    
      </table> 
    <%-- END Right-hand side information  --%>    
    </td>
  </tr>
</table>
<br />
<table runat="server" cellspacing="0" cellpadding="1" style="width: 100%; background-color: #404040">
  <tr style="background-color:#404040">
  <td align="left" bgcolor="#404040">
  <asp:Label ID="Label1" runat="server" CssClass="TableHeading" Text="&nbsp;Notes"></asp:Label>  
  </td>
  <td align="right" bgcolor="#404040">
  <asp:Button ID="btnNotes" runat="server" Text="Add Note" CssClass="ContentSmallButton" />&nbsp;
  </td>
  </tr>
</table>
<table id="NotesTable" runat="server" cellspacing="4" cellpadding="0" style="width: 100%">
  <tr>
    <td>            
    <asp:Label ID="lblNoResults" runat="server" Text="No notes have been entered for this Doctor." CssClass="ContentText"></asp:Label>            
    </td>
  </tr>
</table>    
</asp:Content>
