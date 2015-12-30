<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="EditNote.aspx.vb" Inherits="HLConline.EditNote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td>
    <asp:Label ID="lblHeading" runat="server" CssClass="ContentHeading" Text="Add a Note for"></asp:Label></td>
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
                Unable to update this Note because of the following:<br />
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
    <asp:Label ID="Label1" runat="server" CssClass="LabelDataEntry" Font-Bold="True" Text="Enter your free-form text below.  A maximum of 5000 characters may be added. (Note: Cut and paste of images is not supported.)"></asp:Label><br />
    <br />
    <asp:TextBox runat="server" ID="txtNote" CssClass="TextBoxDataEntry" Width="100%" textMode="MultiLine" Rows="25"></asp:TextBox>
    <asp:HiddenField runat="server" ID="hidMode"       Value="Add" />
    <asp:HiddenField runat="server" ID="hidID"         Value="" />
    <asp:HiddenField runat="server" ID="hidDoctorID"   Value="" />
    <asp:HiddenField runat="server" ID="hidDoctorName" Value="" />
    
</asp:Content>
