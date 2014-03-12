<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrasjonAvTeamBrukere.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvTeamBrukere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 id="Headline_endre_team">Endre team</h1>

    <div class="col-md-0">

        <asp:Label ID="lb_navn_paa_team" runat="server" Text="Team-Navn"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" Height="16px" Width="146px"></asp:TextBox>

    </div>
    
    <div class="col-md-6">
        <asp:CheckBoxList ID="cbl_TeamBrukere" runat="server">
        </asp:CheckBoxList>
    </div>

    <div class="col-md-6">
        <asp:CheckBoxList ID="cbl_brukere" runat="server">
        </asp:CheckBoxList>
    </div>

    <div class="col-md-0">
    </div>

    <div class="col-md-6">
         <asp:Button ID="bt_fjerneBruker" runat="server" Text="Fjern bruker" OnClick="bt_fjerneBruker_Click" />     
    </div>

    <div class="col-md-6">
         <asp:Button ID="bt_leggeTilBruker" runat="server" Text="Legg til bruker" OnClick="bt_leggeTilBruker_Click" />  
    </div>
</asp:Content>
