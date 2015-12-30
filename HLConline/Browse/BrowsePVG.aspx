<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="BrowsePVG.aspx.vb" Inherits="HLConline.BrowsePVG" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
              <asp:Label ID="lblHeading" runat="server" Text="PVG Members" CssClass="ContentHeading"></asp:Label>
            </td>
            <td align="right"> </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="sdsDataSource" PageSize="40" 
        GridLines="Horizontal" EnableViewState="False" CaptionAlign="Top" 
        EmptyDataText="There are no PVG Members defined.  Click the 'Add a New PVG Member' link to create a new member.">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" HeaderStyle-HorizontalAlign="Left" >
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="HospitalName" HeaderText="Hospital" SortExpression="HospitalName" >
                <ItemStyle Width="180px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Edit/EditPVG.aspx?ID={0}"
                DataTextField="FullName" HeaderText="Name" SortExpression="LastName" >
                <ItemStyle Width="120px" Font-Underline="True" ForeColor="Black" />
                <ControlStyle CssClass="TableHyperlink" /><HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="MobilePhone" HeaderText="MobilePhone" SortExpression="MobilePhone" >
                <ItemStyle Width="100px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" SortExpression="EmailAddress" >
                <ItemStyle Width="50px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Congregation" HeaderText="Congregation" SortExpression="Congregation" >
                <ItemStyle Width="80px" /><HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Weekday" HeaderText="Weekday" SortExpression="Weekday" >
                <ItemStyle Width="80px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
        <AlternatingRowStyle BackColor="Cornsilk" />
        <HeaderStyle BackColor="#404040" Font-Bold="True" ForeColor="White" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" />
        <PagerSettings Mode="NumericFirstLast" Position="Top" />
        <PagerStyle HorizontalAlign="Right" />
    </asp:GridView>
    <asp:SqlDataSource ID="sdsDataSource" runat="server"></asp:SqlDataSource>
</asp:Content>
