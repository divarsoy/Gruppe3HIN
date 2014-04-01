<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="VisOppgave.aspx.cs" Inherits="SysUt14Gr03.VisOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblNavn" runat="server" Text="Navn"></asp:Label>
    </h1>
    <hr />
    <br />
    <asp:Label ID="lblInfo" runat="server" Text="Info" Visible="False"></asp:Label>
    <br />
    <asp:Button ID="btnPameld" runat="server" OnClick="btnPaMeld_Click" Text="Meld deg på" Visible="False"/>
    <asp:Button ID="btnInviter" runat="server" OnClick="btnInviter_Click" Text="Inviter andre brukere" Visible="False"/>
    <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Returner oppgave" Visible="False"/>

    
    <hr />
    <h3><asp:Label ID="lblKommentarteller" runat="server" Text="Kommentarer" Visible ="False"></asp:Label></h3>
    <p>
        <asp:TextBox ID="txtKommentar" runat="server" MaxLength="255" TextMode="MultiLine" Visible="False"></asp:TextBox>
        <br />
        <asp:Button ID="btnKommentar" runat="server" Text="Lagre kommentar" OnClick="btnKommentar_Click" Visible="False" />
    </p>
    <asp:Label ID="lblKommentarer" runat="server" Text="Kommentarer" Visible="False"></asp:Label>

</asp:Content>
