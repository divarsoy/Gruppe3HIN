<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="OversiktBrukerSomUtvikler.aspx.cs" Inherits="SysUt14Gr03.OversiktBrukerSomUtvikler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentUtvikler" runat="server">
    <h1>Liste over brukere i prosjektet</h1>
    <asp:Label ID="lblTilbakemelding" runat="server" Text=""></asp:Label>
    <asp:PlaceHolder ID="PlaceHolderTableProject" runat="server"></asp:PlaceHolder>
</asp:Content>