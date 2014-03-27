<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="TidsestimerProsjekt.aspx.cs" Inherits="SysUt14Gr03.TidsestimerProsjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Tidsestimer prosjekt</h1>
    <p></p>
    <asp:Panel ID="pnlOppgaver" runat="server">
        <asp:ListBox ID="lsbProsjekt" runat="server"></asp:ListBox>
        <asp:Button ID="btnDetaljer" runat="server" OnClick="btnDetaljer_Click" Text="Vis detaljer" />
        <br />
        <asp:TextBox ID="txtInfo" runat="server" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
        <hr />
        <asp:Panel ID="pnlDato" runat="server">
            <asp:Calendar ID="calCalendar" runat="server"></asp:Calendar>
            Arbeidsmengde:
            <asp:TextBox ID="txtArbeid" runat="server" TextMode="Number" Width="128px"></asp:TextBox>
            &nbsp;timer</asp:Panel>
        <br />
        <asp:Label ID="FristOK" runat="server" ForeColor="Green" Text="Frist satt!" Visible="False"></asp:Label>
        <asp:Label ID="Feilmelding" runat="server" ForeColor="Red" Text="Noko gjekk gale" Visible="False"></asp:Label>
    </asp:Panel>
    <asp:Button ID="btnFrist" runat="server" Text="Endre frist" OnClick="btnFrist_Click" />
</asp:Content>
