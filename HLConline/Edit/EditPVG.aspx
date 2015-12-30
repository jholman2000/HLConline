<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="EditPVG.aspx.vb" Inherits="HLConline.EditPVG" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td>
    <asp:Label ID="lblHeading" runat="server" CssClass="ContentHeading" Text="Add a PVG Member"></asp:Label></td>
            <td align="right">
                <asp:Button ID="btnDelete" runat="server" CssClass="ContentButton" Text="Delete" Visible="False" />&nbsp;&nbsp;
                <asp:Button ID="btnUpdate" runat="server" CssClass="ContentButton" 
                    Text="Update" onclick="btnUpdate_Click" /></td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="pnlError" runat="server" Height="50px" Width="580px" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" Visible="False" BackColor="Cornsilk">
        <table cellpadding="3"><tr><td valign="top">
        <asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/images/error_big.gif" />&nbsp;<strong><br />
        </strong>
        </td>
            <td valign="top">
                Unable to update this PVG Member because of the following:<br />
        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label></td>
        </tr></table>
    </asp:Panel>
    <asp:Panel ID="pnlMessage" runat="server" Width="580px" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" Visible="False" BackColor="Cornsilk">
        <table cellpadding="3">
        <tr>
        <td></td>
        <td valign="top" align="center"><asp:Label ID="lblMessage" runat="server" ForeColor="Black" EnableViewState="False" CssClass="ContentText"></asp:Label></td>
        </tr></table>
    </asp:Panel>
    <br />
    <table width="100%" cellpadding="2" cellspacing="0">
    <tr>
    <td style="width: 350px" valign="top">
      <table style="width: 100%" id="TABLE1">
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label1" runat="server" CssClass="LabelDataEntry" Text="Name:" EnableViewState="False"></asp:Label></td>
            <td style="width: 250px; margin-left: 40px;">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="TextBoxDataEntry" MaxLength="50" Width="110px" TabIndex="1"></asp:TextBox>&nbsp;&nbsp;
                <asp:TextBox ID="txtLastName" runat="server" CssClass="TextBoxDataEntry" MaxLength="50" Width="110px" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label2" runat="server" CssClass="LabelDataEntry" Text="Email:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="TextBoxDataEntry" MaxLength="50"
                    Width="220px" TabIndex="5"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label15" runat="server" CssClass="LabelDataEntry" Text="Address:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtAddress" runat="server" CssClass="TextBoxDataEntry" MaxLength="50"
                    Width="220px" TabIndex="6"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label3" runat="server" CssClass="LabelDataEntry" Text="City:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtCity" runat="server" CssClass="TextBoxDataEntry" MaxLength="50"
                    Width="220px" TabIndex="7"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label4" runat="server" CssClass="LabelDataEntry" Text="State:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtState" runat="server" CssClass="TextBoxDataEntry" MaxLength="2"
                    Width="25px" TabIndex="8"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label5" runat="server" CssClass="LabelDataEntry" Text="Zip:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtZip" runat="server" CssClass="TextBoxDataEntry" MaxLength="10"
                    Width="80px" TabIndex="9"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label7" runat="server" CssClass="LabelDataEntry" 
                    Text="Home Phone:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtHomePhone" runat="server" CssClass="TextBoxDataEntry" MaxLength="12"
                    Width="100px" TabIndex="10"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label8" runat="server" CssClass="LabelDataEntry" Text="Mobile Phone:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="TextBoxDataEntry" MaxLength="12"
                    Width="100px" TabIndex="11"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label16" runat="server" CssClass="LabelDataEntry" Text="Congregation:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtCongregation" runat="server" CssClass="TextBoxDataEntry" MaxLength="50"
                    Width="220px" TabIndex="12"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
            </td>
            <td style="width: 100px">
                <br />
            </td>
        </tr>
    </table>
    </td>
    <td style="width: 20px">&nbsp;</td>
    <td style="width: 200px" valign="top">
      <table>
        <tr>
        <td colspan="4" style="border-right: steelblue 1px solid; border-top: steelblue 1px solid; border-left: steelblue 1px solid; border-bottom: steelblue 1px solid; background-color: gainsboro;">
        <b>Enter the contact information for this brother in the space to the left and then select up to three Hospital assignments below.</b>
        </td>
        </tr>
        <tr><td colspan="4">&nbsp;</td></tr>
        <tr>
          <td ><asp:Label ID="Label9" runat="server" CssClass="LabelDataEntry" 
                  Text="Assigned to:" EnableViewState="False" Width="80px" /></td>
          <td>
              <asp:DropDownList ID="ddlHospital1" runat="server" CssClass="DropDownDataEntry" 
                  Width="225px" TabIndex="12">
              </asp:DropDownList>
          </td>
          <td><asp:Label ID="Label10" runat="server" CssClass="LabelDataEntry" Text="on:" EnableViewState="False" /></td>
          <td>
              <asp:DropDownList ID="ddlDayOfWeek1" runat="server" CssClass="DropDownDataEntry" 
                  Width="125px" TabIndex="13">
                <asp:ListItem Value="0" Selected="True" Text="" />
                <asp:ListItem Value="1" Text="Sunday" />
                <asp:ListItem Value="2" Text="Monday" />
                <asp:ListItem Value="3" Text="Tuesday" />
                <asp:ListItem Value="4" Text="Wednesday" />
                <asp:ListItem Value="5" Text="Thursday" />
                <asp:ListItem Value="6" Text="Friday" />
                <asp:ListItem Value="7" Text="Saturday" />
                <asp:ListItem Value="8" Text="As Needed" />
                <asp:ListItem Value="9" Text="Alternate" />
              </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td ><asp:Label ID="Label11" runat="server" CssClass="LabelDataEntry" 
                  Text="Assigned to:" EnableViewState="False" Width="80px" /></td>
          <td>
              <asp:DropDownList ID="ddlHospital2" runat="server" CssClass="DropDownDataEntry" 
                  Width="225px" TabIndex="14">
              </asp:DropDownList>
          </td>
          <td><asp:Label ID="Label12" runat="server" CssClass="LabelDataEntry" Text="on:" EnableViewState="False" /></td>
          <td>
              <asp:DropDownList ID="ddlDayOfWeek2" runat="server" CssClass="DropDownDataEntry" 
                  Width="125px" TabIndex="15">
                <asp:ListItem Value="0" Selected="True" Text="" />
                <asp:ListItem Value="1" Text="Sunday" />
                <asp:ListItem Value="2" Text="Monday" />
                <asp:ListItem Value="3" Text="Tuesday" />
                <asp:ListItem Value="4" Text="Wednesday" />
                <asp:ListItem Value="5" Text="Thursday" />
                <asp:ListItem Value="6" Text="Friday" />
                <asp:ListItem Value="7" Text="Saturday" />
              </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td ><asp:Label ID="Label13" runat="server" CssClass="LabelDataEntry" 
                  Text="Assigned to:" EnableViewState="False" Width="80px" /></td>
          <td>
              <asp:DropDownList ID="ddlHospital3" runat="server" CssClass="DropDownDataEntry" 
                  Width="225px" TabIndex="16">
              </asp:DropDownList>
          </td>
          <td><asp:Label ID="Label14" runat="server" CssClass="LabelDataEntry" Text="on:" EnableViewState="False" /></td>
          <td>
              <asp:DropDownList ID="ddlDayOfWeek3" runat="server" CssClass="DropDownDataEntry" 
                  Width="125px" TabIndex="17">
                <asp:ListItem Value="0" Selected="True" Text="" />
                <asp:ListItem Value="1" Text="Sunday" />
                <asp:ListItem Value="2" Text="Monday" />
                <asp:ListItem Value="3" Text="Tuesday" />
                <asp:ListItem Value="4" Text="Wednesday" />
                <asp:ListItem Value="5" Text="Thursday" />
                <asp:ListItem Value="6" Text="Friday" />
                <asp:ListItem Value="7" Text="Saturday" />
              </asp:DropDownList>
          </td>
        </tr>
      </table>
    </td>
    </tr>
    <tr>
      <td colspan="3">
        <asp:Label ID="Label6" runat="server" CssClass="LabelDataEntry" Text="Notes:" EnableViewState="False" /><br />
        <asp:TextBox ID="txtNotes" runat="server" CssClass="TextBoxDataEntry" MaxLength="1500"
                    TextMode="MultiLine" Rows="8" Columns="60" TabIndex="18" 
              Width="758px"></asp:TextBox>
      </td>
    </tr>
    </table>
    <asp:HiddenField ID="hidMode" runat="server" Value="Edit" /><asp:HiddenField ID="hidIsVerified" runat="server" Value="Edit" />
    <asp:HiddenField ID="hidID" runat="server" Value="" />
</asp:Content>
