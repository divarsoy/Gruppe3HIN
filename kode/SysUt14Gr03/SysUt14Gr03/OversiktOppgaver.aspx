<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Bruker.Master" AutoEventWireup="true" CodeBehind="OversiktOppgaver.aspx.cs" Inherits="SysUt14Gr03.OversiktOppgaver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Liste over oppgaver</h1>
    <asp:Label ID="lblTilbakemelding" runat="server" Text=""></asp:Label>
    <asp:PlaceHolder ID="PlaceHolderTable" runat="server"></asp:PlaceHolder>
</asp:Content>
