<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="ReturAvOppgave.aspx.cs" Inherits="SysUt14Gr03.ReturAvOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentUtvikler" runat="server">
    <h1>Retur av oppgave</h1>
    <asp:Panel ID="Panel1" runat="server">
        <br />
        Begrunnelse:
        <br />
        <asp:TextBox ID="txtSvar" runat="server" MaxLength="255" TextMode="MultiLine" Height="56px" Width="233px"></asp:TextBox>
    </asp:Panel>
    <p>
        <asp:Label ID="lblFeil" runat="server" visible="false"></asp:Label>
        <br />
        <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send" />

    </p>
    <br />
    </asp:Content>
