<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FerdigstillelseAvOppgave.aspx.cs" Inherits="SysUt14Gr03.FerdigstillelseAvOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Ferdigstillelse av oppgave</h1>
    <p>Mine oppgaver</p>
    <p>
        <asp:ListBox ID="lsbOppgaver" runat="server"></asp:ListBox>
        <asp:Button ID="btnFerdig" runat="server" OnClick="btnFerdig_Click" Text="Merk som ferdig" />
    </p>
</asp:Content>
