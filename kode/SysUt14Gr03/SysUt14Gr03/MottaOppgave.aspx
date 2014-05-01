<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="MottaOppgave.aspx.cs" Inherits="SysUt14Gr03.MottaOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Invitasjon til å hjelpe med oppgave</h1>
    <p>
        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
        <br />
    <asp:Button ID="btnAvsla" runat="server" OnClick="btnAvsla_Click" Text="Avslå" CssClass="btn btn-success" />
    <asp:Button ID="btnGodta" runat="server" OnClick="btnGodta_Click" Text="Godta" CssClass="btn btn-danger" />
    </p>
</asp:Content>
