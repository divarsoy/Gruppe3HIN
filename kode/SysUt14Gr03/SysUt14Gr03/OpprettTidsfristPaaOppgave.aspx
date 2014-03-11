<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpprettTidsfristPaaOppgave.aspx.cs" Inherits="SysUt14Gr03.OpprettTidsfristPaaOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Sett tidsfrist på oppgaver</h1>
    <p></p>
    <asp:Panel ID="pnlOppgaver" runat="server">
        <asp:ListBox ID="lsbOppgaver" runat="server" OnSelectedIndexChanged="lsbOppgaver_SelectedIndexChanged"></asp:ListBox>
        <asp:Button ID="btnDetaljer" runat="server" OnClick="btnDetaljer_Click" Text="Vis detaljer" />
        <br />
        <asp:TextBox ID="txtInfo" runat="server" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
        <hr />
        <asp:Panel ID="pnlDato" visible="false" runat="server">
            <asp:Calendar ID="calCalendar" runat="server"></asp:Calendar>
            Klokkeslett
            <asp:DropDownList ID="ddlTime" runat="server">
            </asp:DropDownList>
            :<asp:DropDownList ID="ddlMinutt" runat="server">
            </asp:DropDownList>
            
        </asp:Panel>
        <br />
        <asp:Label ID="FristOK" runat="server" ForeColor="Green" Text="Frist satt!" Visible="False"></asp:Label>
        <asp:Label ID="Feilmelding" runat="server" ForeColor="Red" Text="Noko gjekk gale" Visible="False"></asp:Label>
    </asp:Panel>
    <asp:Button ID="btnFrist" runat="server" Text="Endre frist" OnClick="btnFrist_Click" />
</asp:Content>
