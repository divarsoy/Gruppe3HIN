<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="ReturAvOppgave.aspx.cs" Inherits="SysUt14Gr03.ReturAvOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblNavn" runat="server" Text="Retur av oppgave"></asp:Label></h1>
    <asp:Panel ID="Panel1" runat="server">
        <br />
        Begrunnelse:
        <br />
        <asp:TextBox ID="txtSvar" runat="server" cssClass="form-control" MaxLength="255" TextMode="MultiLine" Height="56px" Width="233px"></asp:TextBox>
    </asp:Panel>
    <div class="clearfix"></div>
    <p>

        <asp:Label ID="lblFeil" runat="server" visible="false"></asp:Label>
        <br />
        <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send" CssClass="btn btn-primary" />

    </p>
    <br />
    </asp:Content>
