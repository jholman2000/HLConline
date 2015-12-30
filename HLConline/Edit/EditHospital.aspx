<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="EditHospital.aspx.vb" Inherits="HLConline.EditHospital" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td>
    <asp:Label ID="lblHeading" runat="server" CssClass="ContentHeading" Text="Add a Hospital"></asp:Label></td>
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
                Unable to update this Hospital because of the following:<br />
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
            <td style="width: 96px" valign="top">
                <asp:Label ID="Label6" runat="server" CssClass="LabelDataEntry" 
                    Text="Hospital Name:" Width="96px" EnableViewState="False"></asp:Label></td>
            <td style="width: 290px; margin-left: 40px;" valign="top">
                <asp:TextBox ID="txtName" runat="server" CssClass="TextBoxDataEntry" MaxLength="80" Width="280px" TabIndex="1"></asp:TextBox>&nbsp;
            </td>
            <td rowspan="3" style="width: 250px; border-right: steelblue 1px solid; border-top: steelblue 1px solid; border-left: steelblue 1px solid; border-bottom: steelblue 1px solid; background-color: gainsboro;" valign="top">
            <b>Update the information for this Hospital in the spaces to the left.&nbsp;</b>
            </td>
        </tr>
        <tr>
            <td style="width: 96px">
                <asp:Label ID="Label3" runat="server" CssClass="LabelDataEntry" Text="City:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtCity" runat="server" CssClass="TextBoxDataEntry" MaxLength="50" Width="220px" TabIndex="2"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 96px">
                <asp:Label ID="Label4" runat="server" CssClass="LabelDataEntry" Text="State:" EnableViewState="False"></asp:Label></td>
            <td style="width: 100px">
                <asp:TextBox ID="txtState" runat="server" CssClass="TextBoxDataEntry" MaxLength="2" Width="25px" TabIndex="3"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 96px">
            </td>
            <td style="width: 100px">
                <br />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:Panel ID="pnlDoctors" runat="server" Visible="True" Width="100%">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="sdsDataSource" PageSize="40" 
        GridLines="Horizontal" EnableViewState="False" CaptionAlign="Top" 
        EmptyDataText="There are no Doctors currently assigned to this Hospital.">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="Doctor ID" ReadOnly="True" SortExpression="ID" Visible="false">
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/View/ViewDoctor.aspx?ID={0}"
                DataTextField="FullName" HeaderText="Doctor" SortExpression="LastName" >
                <ItemStyle Width="250px" Font-Underline="True" ForeColor="Black" />
                <ControlStyle CssClass="TableHyperlink" /><HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="MobilePhone" HeaderText="Mobile" SortExpression="MobilePhone" >
                <ItemStyle Width="100px" /><HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Pager" HeaderText="Pager" SortExpression="Pager" >
                <ItemStyle Width="100px" /><HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="HomePhone" HeaderText="Home" SortExpression="HomePhone" >
                <ItemStyle Width="100px" /><HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="AttitudeText" HeaderText="Attitude" SortExpression="Attitude" >
                <ItemStyle Width="100px" /><HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        <AlternatingRowStyle BackColor="Cornsilk" />
        <HeaderStyle BackColor="#404040" Font-Bold="True" ForeColor="White" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" />
        <PagerSettings Mode="NumericFirstLast" Position="Top" />
        <PagerStyle HorizontalAlign="Right" />
    </asp:GridView>
    <asp:SqlDataSource ID="sdsDataSource" runat="server" >
    </asp:SqlDataSource>
    </asp:Panel>
    <br />
    <asp:HiddenField ID="hidMode" runat="server" Value="Edit" />
    <asp:HiddenField ID="hidID"   runat="server" Value="0" />
</asp:Content>
