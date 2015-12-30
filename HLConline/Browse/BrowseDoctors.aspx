<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/HLC.Master" CodeBehind="BrowseDoctors.aspx.vb" Inherits="HLConline.BrowseDoctors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td>
        <asp:Label ID="lblHeading" runat="server" Text="Doctors at Some Hospital with SpecialtyName" CssClass="ContentHeading"></asp:Label>
        </td>
        <td align="right">
        
        </td>
    </tr>
</table>
    <asp:Panel ID="pnlAlphabet" runat="server" Visible="true" BorderStyle="None">
        <asp:Button ID="btnA" runat="server" Text="A" CssClass="ContentSmallButton" />&nbsp;    
        <asp:Button ID="btnB" runat="server" Text="B" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnC" runat="server" Text="C" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnD" runat="server" Text="D" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnE" runat="server" Text="E" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnF" runat="server" Text="F" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnG" runat="server" Text="G" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnH" runat="server" Text="H" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnI" runat="server" Text="I" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnJ" runat="server" Text="J" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnK" runat="server" Text="K" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnL" runat="server" Text="L" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnM" runat="server" Text="M" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnN" runat="server" Text="N" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnO" runat="server" Text="O" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnP" runat="server" Text="P" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnQ" runat="server" Text="Q" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnR" runat="server" Text="R" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnS" runat="server" Text="S" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnT" runat="server" Text="T" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnU" runat="server" Text="U" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnV" runat="server" Text="V" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnW" runat="server" Text="W" CssClass="ContentSmallButton" />&nbsp;
        <asp:Button ID="btnXYZ" runat="server" Text="X/Y/Z" CssClass="ContentSmallButton" />&nbsp;
    </asp:Panel>
    <br />
<asp:Label ID="lblNoResults" runat="server" Text="No results were found." CssClass="ContentText" Font-Bold="True" Visible="False"></asp:Label>
<table id="ResultsTable" runat="server" width="100%" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
<tr>
    <td bgcolor="#404040" width="180px">
        <asp:Label ID="Label10" runat="server" CssClass="TableHeading" Text="Name &amp; Practice"></asp:Label>
    </td>
    <td bgcolor="#404040" width="110px">
        <asp:Label ID="Label11" runat="server" CssClass="TableHeading" Text="Phone Numbers"></asp:Label>
    </td>
    <td bgcolor="#404040" width="120px">
        <asp:Label ID="Label13" runat="server" CssClass="TableHeading" Text="Specialties"></asp:Label>
    </td>
    <td bgcolor="#404040" width="120px">
        <asp:Label ID="Label12" runat="server" CssClass="TableHeading" Text="Hospitals"></asp:Label>
    </td>
    <td bgcolor="#404040">
        <asp:Label ID="Label1" runat="server" CssClass="TableHeading" Text="Attitude"></asp:Label>
    </td>
</tr>
</table>
</asp:Content>
