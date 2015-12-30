<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="BrowsePractices.aspx.vb" Inherits="HLConline.BrowsePractices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
              <asp:Label ID="lblHeading" runat="server" Text="Practices" CssClass="ContentHeading"></asp:Label>
            </td>
            <td align="right"> </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="sdsDataSource" PageSize="40" 
        GridLines="Horizontal" EnableViewState="False" CaptionAlign="Top" 
        EmptyDataText="There are no Practices defined.  Click the 'Add a New Practice' link to create a new Practice.">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="Practice ID" ReadOnly="True" SortExpression="ID" Visible="false">
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Edit/EditPractice.aspx?ID={0}"
                DataTextField="PracticeName" HeaderText="Practice" SortExpression="PracticeName" >
                <ItemStyle Width="250px" Font-Underline="True" ForeColor="Black" />
                <ControlStyle CssClass="TableHyperlink" /><HeaderStyle HorizontalAlign="Left" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="Address1" HeaderText="Address" SortExpression="Address1" >
                <ItemStyle Width="200px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" >
                <ItemStyle Width="100px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" >
                <ItemStyle Width="50px" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="OfficePhone1" HeaderText="Phone" SortExpression="OfficePhone1" >
                <ItemStyle Width="80px" /><HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="NumDocs" HeaderText="# Doctors" SortExpression="NumDocs" DataFormatString="{0:#,###}" HtmlEncode="False" >
                <ItemStyle HorizontalAlign="Right" Width="80px" /><HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
        <AlternatingRowStyle BackColor="Cornsilk" />
        <HeaderStyle BackColor="#404040" Font-Bold="True" ForeColor="White" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" />
        <PagerSettings Mode="NumericFirstLast" Position="Top" />
        <PagerStyle HorizontalAlign="Right" />
    </asp:GridView>
    <asp:SqlDataSource ID="sdsDataSource" runat="server"></asp:SqlDataSource>
</asp:Content>
