<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OversiktBrukerSomUtvikler.aspx.cs" Inherits="SysUt14Gr03.OversiktBrukerSomUtvikler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Liste over oppgaver</h1>
    <asp:Label ID="lblTilbakemelding" runat="server" Text=""></asp:Label>
    <asp:PlaceHolder ID="PlaceHolderTable" runat="server"></asp:PlaceHolder>
</asp:Content>