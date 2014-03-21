<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArkiveringAvKommentarer.aspx.cs" Inherits="SysUt14Gr03.ArkiveringAvKommentarer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Arkiver Kommentarer</h1>
    <asp:Label ID="lblBruker" runat="server"></asp:Label>
    <p><asp:ListBox ID="lbKommentarer" runat="server" Height="56px" Width="149px"></asp:ListBox></p>
    <asp:Button ID="btnSlett" runat="server" OnClick="btnSlett_Click" Text="Slett Kommentar"/>
</asp:Content>
