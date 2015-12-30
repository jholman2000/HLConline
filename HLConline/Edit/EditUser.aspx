<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="EditUser.aspx.vb" Inherits="HLConline.EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td>
    <asp:Label ID="lblHeading" runat="server" CssClass="ContentHeading" Text="Add a User"></asp:Label></td>
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
                Unable to update this User because of the following:<br />
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
    <table style="width: 100%" id="TABLE1">
        <tr>
            <td style="width: 80px" valign="top">
                <asp:Label ID="Label6" runat="server" CssClass="LabelDataEntry" Text="User ID:" Width="64px" EnableViewState="False"></asp:Label></td>
            <td style="width: 250px; margin-left: 40px;" valign="top">
                <asp:TextBox ID="txtUserID" runat="server" CssClass="TextBoxDataEntry" MaxLength="20"
                    Width="80px" TabIndex="1"></asp:TextBox>&nbsp;
                <asp:Label ID="lblPassword" runat="server" CssClass="LabelDataEntry" EnableViewState="False"
                    Text="Password:" Visible="True"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="TextBoxDataEntry" MaxLength="20"
                    Visible="True" Width="64px" TabIndex="2"></asp:TextBox></td>
            <td rowspan="8" style="width: 250px; border-right: steelblue 1px solid; border-top: steelblue 1px solid; border-left: steelblue 1px solid; border-bottom: steelblue 1px solid; background-color: gainsboro;" valign="top">
                <asp:MultiView ID="MultiView1" runat="server" >
                    <asp:View ID="vwAdmin" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5px">
                                    <asp:Label ID="lblRole" runat="server" CssClass="LabelDataEntry" Text="Role:"></asp:Label></td>
                                <td style="width: 145px">
                                    <asp:DropDownList ID="ddlUserRole" runat="server" CssClass="DropDownDataEntry" AutoPostBack="True" TabIndex="11">
                                        <asp:ListItem Selected="True">HLC Member</asp:ListItem>
                                        <asp:ListItem>Admin</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkIsActive" runat="server" Text="Active (User can sign on)"
                                        ToolTip="Uncheck this to prevent user from accessing the system while preserving their data" 
                                        CssClass="CheckBoxDataEntry" TabIndex="12" /><br />
                                    <asp:CheckBox ID="chkMustChangePassword" runat="server" Text="Force password change next sign on"
                                        ToolTip="Check here if you just manually changed a user password" 
                                        CssClass="CheckBoxDataEntry" TabIndex="13" /><br />
                                    <br />
                                    <asp:Label ID="lblDateLastOn" runat="server"></asp:Label><br />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="vwUser" runat="server">
                        <b>Update your name and contact information in the spaces to the left.&nbsp;</b> </asp:View>
                    </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label1" runat="server" CssClass="LabelDataEntry" Text="Name:" EnableViewState="False"></asp:Label></td>
            <td style="width: 250px; margin-left: 40px;">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="TextBoxDataEntry" MaxLength="50" Width="110px" TabIndex="3"></asp:TextBox>&nbsp;&nbsp;
                <asp:TextBox ID="txtLastName" runat="server" CssClass="TextBoxDataEntry" MaxLength="50" Width="110px" TabIndex="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label2" runat="server" CssClass="LabelDataEntry" Text="Email:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="TextBoxDataEntry" MaxLength="50"
                    Width="220px" TabIndex="4"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label15" runat="server" CssClass="LabelDataEntry" Text="Address:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtAddress" runat="server" CssClass="TextBoxDataEntry" MaxLength="50"
                    Width="220px" TabIndex="5"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label3" runat="server" CssClass="LabelDataEntry" Text="City:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtCity" runat="server" CssClass="TextBoxDataEntry" MaxLength="50"
                    Width="220px" TabIndex="6"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label4" runat="server" CssClass="LabelDataEntry" Text="State:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtState" runat="server" CssClass="TextBoxDataEntry" MaxLength="2"
                    Width="25px" TabIndex="7"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label5" runat="server" CssClass="LabelDataEntry" Text="Zip:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtZip" runat="server" CssClass="TextBoxDataEntry" MaxLength="10"
                    Width="80px" TabIndex="8"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label7" runat="server" CssClass="LabelDataEntry" 
                    Text="Home Phone:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtHomePhone" runat="server" CssClass="TextBoxDataEntry" MaxLength="12"
                    Width="100px" TabIndex="9"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
                <asp:Label ID="Label8" runat="server" CssClass="LabelDataEntry" 
                    Text="Mobile Phone:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="TextBoxDataEntry" MaxLength="12"
                    Width="100px" TabIndex="10"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 80px">
            </td>
            <td style="width: 100px">
                <br />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlActivityLog" runat="server" Visible="False" Width="100%">
    <asp:GridView ID="grdActivityLog" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" DataSourceID="sdsActivityLog" CssClass="GridView" AlternatingRowStyle-BackColor="Azure" PageSize="15" GridLines="Horizontal" EnableViewState="False" CaptionAlign="Left" Width="100%" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="DateActivity" DataFormatString="{0:g}" HtmlEncode="False" HeaderText="Date"
                SortExpression="DateActivity" >
                <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="Activity" HeaderText="Activity" SortExpression="Activity" >
                <ItemStyle Width="75px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" >
                <ItemStyle Width="325px" />
            </asp:BoundField>
        </Columns>
        <AlternatingRowStyle BackColor="Gainsboro" />
        <HeaderStyle BackColor="#404040" Font-Bold="True" ForeColor="White" Font-Names="Arial" Font-Size="8pt" />
        <PagerSettings Mode="NumericFirstLast" Position="Top" />
        <PagerStyle HorizontalAlign="Right" />
    </asp:GridView>
    </asp:Panel>
    <br />
    <asp:SqlDataSource ID="sdsActivityLog" runat="server">
    </asp:SqlDataSource>    
    <asp:HiddenField ID="hidMode" runat="server" Value="Edit" /><asp:HiddenField ID="hidIsVerified" runat="server" Value="Edit" />
</asp:Content>
