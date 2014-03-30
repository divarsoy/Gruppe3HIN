<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="VisOppgave.aspx.cs" Inherits="SysUt14Gr03.VisOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblNavn" runat="server" Text="Label"></asp:Label>
    </h1>
    <hr />

    <br />
    <asp:Label ID="lblInfo" runat="server" Text="Label" Visible="False"></asp:Label>

    <hr />
    <h3><asp:Label ID="lblKommentarteller" runat="server" Text="Label" Visible ="False"></asp:Label></h3>
    <p>
        <asp:TextBox ID="txtKommentar" runat="server" MaxLength="255" TextMode="MultiLine" Visible="False"></asp:TextBox>
        <br />
        <asp:Button ID="btnKommentar" runat="server" Text="Lagre kommentar" OnClick="btnKommentar_Click" Visible="False" />
    </p>
    <asp:Label ID="lblKommentarer" runat="server" Text="Label" Visible="False"></asp:Label>

</asp:Content>
