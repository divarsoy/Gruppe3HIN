<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListboxTest.aspx.cs" Inherits="SysUt14Gr03.ListboxTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Listbox Test</h1>
    <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
    <asp:Button ID="btnRemove" runat="server" Text="Fjern Bruker" OnClick="btnRemove_Click" />
</asp:Content>
