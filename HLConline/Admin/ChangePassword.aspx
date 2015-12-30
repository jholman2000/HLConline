<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="HLConline.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" CssClass="ContentHeading" Text="Change Your Password"></asp:Label><br />
    <br />
    <asp:Panel ID="pnlError" runat="server" Height="50px" Width="580px" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" Visible="False" BackColor="Cornsilk">
        <table cellpadding="3"><tr><td valign="top">
        <asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/images/error_big.gif" />&nbsp;<strong><br />
        </strong>
        </td>
            <td valign="top">
                Unable to update your password because of the following:<br />
        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label></td>
        </tr></table>
    </asp:Panel>
    <asp:Label ID="lblMandatory" runat="server" CssClass="ContentText" Font-Bold="True"
        Text="You are required to change your password before you can continue using the system." Visible="False"></asp:Label>
    <br />
        <br />
    <asp:Label ID="lblMessage" runat="server" Text="Please type your current password below and then choose a new password.  You must enter your new password twice to verify it is correct." CssClass="ContentText"></asp:Label><br />
    <br />
    <table>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="Label6" runat="server" CssClass="LabelDataEntry" EnableViewState="False"
                    Text="Current Password:" Width="110px"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtCurrent" runat="server" CssClass="TextBoxDataEntry" MaxLength="12"
                    TabIndex="1" TextMode="Password" Width="100px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="Label2" runat="server" CssClass="LabelDataEntry" EnableViewState="False"
                    Text="New Password:" Width="110px"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtNew" runat="server" CssClass="TextBoxDataEntry" MaxLength="12"
                    TabIndex="1" TextMode="Password" Width="100px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="Label3" runat="server" CssClass="LabelDataEntry" EnableViewState="False"
                    Text="Retype:" Width="110px"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtRetype" runat="server" CssClass="TextBoxDataEntry" MaxLength="12"
                    TabIndex="1" TextMode="Password" Width="100px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
                <asp:Button ID="btnAction" runat="server" CssClass="ContentButton" Text="Change Password"
                    Width="110px" /></td>
        </tr>
    </table>
</asp:Content>
