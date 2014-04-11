<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="RapportTestForm.aspx.cs" Inherits="SysUt14Gr03.RapportTestForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblTest" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Vis en teamrapport" OnClick="Button1_Click" />
    <asp:Button ID="Button2" runat="server" Text="Vis en prosjektrapport" OnClick="Button2_Click" />
    <asp:Button ID="Button3" runat="server" Text="Vis en individrapport" OnClick="Button3_Click" />
</asp:Content>
