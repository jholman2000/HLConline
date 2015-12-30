<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ForgotPassword.aspx.vb" Inherits="HLConline.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="~/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Hospital Liasion Committee</title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="2" cellspacing="0" width="100%" 
        style="border-bottom-style: solid; border-bottom-color: #000000; border-bottom-width: 1px">
        <tr>
            <td bgcolor="Black" colspan="2">
                <asp:Image ID="Image1" runat="server" BorderStyle="None" 
                    ImageUrl="~/images/HLC_logo.jpg" />
            </td>
        </tr>
        <tr>
            <td bgcolor="#ACBDDB">&nbsp;                
            </td>
            <td bgcolor="#ACBDDB" align="right">            
            </td>
        </tr>
    </table>    
    <div>
    <br />
    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" CssClass="ContentHeading" Text="Forgot Your Password?"></asp:Label><br />
    <br />
    &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text='We can send your password to the email address that we currently have on file for&#13;&#10;    your user account.&nbsp; Please enter your User ID below and click the "Send Password" button.' CssClass="ContentText"></asp:Label><br />
    <br />
    &nbsp;&nbsp;<asp:Label ID="lblUserID" runat="server" CssClass="LabelDataEntry" Text="User ID:"></asp:Label>
    <asp:TextBox ID="txtUserID" runat="server" CssClass="TextBoxDataEntry" MaxLength="10"
        Width="70px"></asp:TextBox>&nbsp;<asp:Button ID="btnAction" runat="server" CssClass="ContentButton"
            Text="Send Password" Width="100px" /><br />
    <br />
    &nbsp;&nbsp;<asp:Label ID="lblMessage" runat="server"
        Visible="False"></asp:Label>
    
    </div>
    </form>
</body>
</html>
