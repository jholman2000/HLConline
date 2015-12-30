<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="BrowseUsers.aspx.vb" Inherits="HLConline.BrowseUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
              <asp:Label ID="lblHeading" runat="server" Text="System Users" CssClass="ContentHeading"></asp:Label>
            </td>
            <td align="right"> </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="UserID" DataSourceID="sdsDataSource" PageSize="40" 
        GridLines="Horizontal" EnableViewState="False" CaptionAlign="Top" 
        EmptyDataText="There are no Users defined.  Click the 'Add a New User' link to grant acces to a new user.">
        <Columns>
            <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" SortExpression="UserID" HeaderStyle-HorizontalAlign="Left" >
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="~/Edit/EditUser.aspx?ID={0}"
                DataTextField="FullName" HeaderText="Name" SortExpression="FullName" >
                <ItemStyle Width="180px" Font-Underline="True" ForeColor="Black" />
                <ControlStyle CssClass="TableHyperlink" /><HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="UserRole" HeaderText="Role" SortExpression="UserRole" >
                <ItemStyle Width="120px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" SortExpression="EmailAddress" >
                <ItemStyle Width="200px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="HomePhone" HeaderText="Home Phone" SortExpression="HomePhone" >
                <ItemStyle Width="180px" /><HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="DateLastOn" DataFormatString="{0:g}" HtmlEncode="False" HeaderText="Last Online" SortExpression="DateLastOn" >
                <ItemStyle Width="155px" /><HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        <AlternatingRowStyle BackColor="Cornsilk" />
        <HeaderStyle BackColor="#404040" Font-Bold="True" ForeColor="White" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" />
        <PagerSettings Mode="NumericFirstLast" Position="Top" />
        <PagerStyle HorizontalAlign="Right" />
    </asp:GridView>
    <asp:SqlDataSource ID="sdsDataSource" runat="server">
    </asp:SqlDataSource>
</asp:Content>
