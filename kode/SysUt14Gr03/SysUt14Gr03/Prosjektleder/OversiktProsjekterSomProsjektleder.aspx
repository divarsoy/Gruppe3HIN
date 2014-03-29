<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="OversiktProsjekterSomProsjektleder.aspx.cs" Inherits="SysUt14Gr03.OversiktProsjekterSomProsjektleder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Mine Prosjekter</h2>
    <asp:PlaceHolder ID="ProsjektTable" runat="server"></asp:PlaceHolder>
    <asp:Button ID="BtnOpprettProsjekt" runat="server" Text="Opprett Prosjekt" CssClass="btn btn-primary" OnClick="BtnOpprettProsjekt_Click"/>
</asp:Content>
