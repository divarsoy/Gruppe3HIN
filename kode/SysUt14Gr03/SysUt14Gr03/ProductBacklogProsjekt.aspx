<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="ProductBacklogProsjekt.aspx.cs" Inherits="SysUt14Gr03.ProductBacklogProsjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   <h2><asp:Label ID="lblProsjektnavn" runat="server"></asp:Label></h2>
    <br />
    <asp:PlaceHolder ID="phProsjekt" runat="server"></asp:PlaceHolder>
    <asp:Button ID="btnExport" CssClass="btn btn-success" runat="server" Text="Eksportert til excel" OnClick="btnExport_Click" />
</asp:Content>
