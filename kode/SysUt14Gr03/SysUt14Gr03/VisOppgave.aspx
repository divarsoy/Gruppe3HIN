<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="VisOppgave.aspx.cs" Inherits="SysUt14Gr03.VisOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container visOppgave">
        <h1>
            <asp:Label ID="lblNavn" runat="server" Text="Navn"></asp:Label>
        </h1>
        <hr />
        <br />
        <asp:Label ID="lblInfo" runat="server" Text="Info" Visible="False"></asp:Label>
        <br />
        <asp:Button ID="btnPameld" runat="server" OnClick="btnPaMeld_Click" Text="Meld deg på" CssClass="btn btn-primary" Visible="False"/>
        <asp:Button ID="btnInviter" runat="server" OnClick="btnInviter_Click" Text="Inviter andre brukere" cssClass="btn btn-success" Visible="False"/>
        <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Returner oppgave" CssClass="btn btn-danger" Visible="False"/>   

        <hr />
        <div class="col-sm-6">
            <h2>Ny kommentar</h2>
            <asp:TextBox ID="txtKommentar" runat="server" TextMode="MultiLine" cssClass="form-control" Visibl="False"></asp:TextBox>
        </div>
        <div class="clearfix"></div>
        <hr />
        <asp:Button ID="btnKommentar" runat="server" Text="Lagre kommentar" OnClick="btnKommentar_Click" CssClass="btn btn-primary" Visible="False" />
        <h3><asp:Label ID="lblKommentarteller" runat="server" Text="Kommentarer" Visible ="False"></asp:Label></h3>
        <asp:Label ID="lblKommentarer" runat="server" Text="Kommentarer" Visible="False"></asp:Label>
    </div>
</asp:Content>
