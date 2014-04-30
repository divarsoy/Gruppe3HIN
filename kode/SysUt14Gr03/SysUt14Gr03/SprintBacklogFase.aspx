<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="SprintBacklogFase.aspx.cs" Inherits="SysUt14Gr03.SprintBacklogFase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblfasenavn" runat="server"></asp:Label>
    <asp:PlaceHolder ID="phFase" runat="server"></asp:PlaceHolder>
    <asp:Button ID="btnExport" runat="server" Text="Exporter til excel dokument" />
    <asp:DropDownList ID="ddlfaser" runat="server"></asp:DropDownList>
</asp:Content>
