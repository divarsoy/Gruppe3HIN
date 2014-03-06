<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrasjonAvGruppe.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvGruppe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administrasjon av grupper</h1>
    <hr />
    <asp:Panel ID="pnlGrupper" runat="server" Height="40%" ScrollBars="Auto">
        <asp:ListBox ID="lsbGrupper" runat="server" OnSelectedIndexChanged="lsbGrupper_SelectedIndexChanged"></asp:ListBox>
        
        <asp:ListBox ID="lsbTeam" runat="server"></asp:ListBox>
        
    </asp:Panel>
    <asp:Button ID="btnVisTeam" runat="server" Text="Vis team" OnClick="btnVisTeam_Click" />
    <hr />

    <asp:Label ID="NoUsersSelected" runat="server" ForeColor="Red" Text="Ingen brukere valgt"
            Visible="False"></asp:Label> 
    <asp:Label ID="TeamOK" runat="server" ForeColor="Green" Text="Team opprettet!"
            Visible="False"></asp:Label><br />
    &nbsp;&nbsp;&nbsp;
    </asp:Content>
