<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListboxTest.aspx.cs" Inherits="SysUt14Gr03.ListboxTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1>Listbox Test</h1>
        <div class="col-md-6">
            <asp:Label ID="ListboxLabel" runat="server" Text="Label"></asp:Label>
            <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
            <asp:Button ID="btnRemove" runat="server" Text="Fjern Bruker" OnClick="btnRemove_Click" />
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
