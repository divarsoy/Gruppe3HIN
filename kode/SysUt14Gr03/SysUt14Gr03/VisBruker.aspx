<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="VisBruker.aspx.cs" Inherits="SysUt14Gr03.VisBruker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblNavn" runat="server" Text="Navn"></asp:Label>
    </h1>
    <hr />
    <br />
    <asp:Label ID="lblInfo" runat="server" Text="Info" Visible="False"></asp:Label>
    <br />
</asp:Content>
