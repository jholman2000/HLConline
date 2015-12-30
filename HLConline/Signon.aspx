<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Signon.aspx.vb" Inherits="HLConline.Signon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Hospital Liasion Committee</title>
    <style type="text/css">
        .style1
        {
            color: #C0C0C0;
        }
    </style>
</head>
<body bgcolor="Gray">
    <form id="form1" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div align="center" >
    <table cellpadding="2" style="border: thin ridge #000000; background-color: #C0C0C0">
    <tr>
    <td>
         <table  cellpadding="0" cellspacing="0" border="0" width="500" bgcolor="Silver">
            <tr>
                <td rowspan="8" width="150" valign="top">
                    <asp:Image ID="Image3" runat="server" BorderStyle="None" ImageUrl="~/images/HLC_icon_lg.jpg" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" bgcolor="black">&nbsp;<asp:Image ID="Image2" runat="server" BorderStyle="None" ImageUrl="~/images/HLC_wording.jpg" /></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text="User ID:" CssClass="ContentText" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">&nbsp;
                    <asp:TextBox ID="txtUserID" runat="server" CssClass="ContentText" MaxLength="15"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right">&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Password:" CssClass="ContentText" Font-Bold="True"></asp:Label>
                </td>
                <td align="left">&nbsp;
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="ContentText" MaxLength="12" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblErrorMessage" runat="server" Text="Error message" CssClass="LeftNavBoxError" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center"><br />
                    <asp:Button ID="btnLogon" runat="server" Text="Logon" CssClass="ContentButton" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:CheckBox ID="chkRememberMe" runat="server" Text="Remember me" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="hypForgotPwd" runat="server" NavigateUrl="~/Admin/ForgotPassword.aspx">Forgot password?</asp:HyperLink>
                </td>
            </tr>
        </table>    
    </td>
    </tr>
    </table>
    <br />
    <br />
        <span class="style1">Access to this site is restricted to authorized users only.
    
    </span>
    
    </div>
    </form>
</body>
</html>
