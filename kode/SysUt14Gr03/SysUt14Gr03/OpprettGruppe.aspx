<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpprettGruppe.aspx.cs" Inherits="SysUt14Gr03.OpprettGruppe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Opprett gruppe</h1>
    <p>
        <asp:TextBox ID="txtGruppeNavn" runat="server" MaxLength="128" ToolTip="Navn på team"></asp:TextBox>
    </p>
    <asp:Panel ID="pnlTeam" runat="server" Height="40%" ScrollBars="Auto">
        <asp:CheckBoxList ID="cblTeam" runat="server">
        </asp:CheckBoxList>
    </asp:Panel>
    <hr />

    <asp:Label ID="NoTeamsSelected" runat="server" ForeColor="Red" Text="Ingen team valgt"
            Visible="False"></asp:Label> 
    <asp:Label ID="GruppeOK" runat="server" ForeColor="Green" Text="Gruppe opprettet!"
            Visible="False"></asp:Label><br />
    <asp:Button ID="btnOK" runat="server" Text="Opprett gruppe" OnClick="btnOK_Click" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Avbryt" />
</asp:Content>
