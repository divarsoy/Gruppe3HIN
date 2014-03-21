<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrasjonAvOppgave.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvOppgave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Administrering av Oppgave</h2>
    <p>Oppgave id: <asp:TextBox ID="tbOppgave_id" runat="server" ReadOnly="true"></asp:TextBox></p>
    <p>Tittel: <asp:TextBox ID="tbTittel" runat="server" Height="25px"></asp:TextBox></p>
    <p>Beskrivelse av oppgave: <asp:TextBox ID="tbBeskrivelse" runat="server" Height="39px" Width="132px"></asp:TextBox></p>
    <p>Krav: <asp:TextBox ID="tbKrav" runat="server" Height="25px"></asp:TextBox></p>
    <p>Estimering: <asp:TextBox ID="TbEstimering" runat="server" Height="25px" Width="160px"></asp:TextBox></p>
    <p>Aktiv: <asp:CheckBox ID="cbAktiv" runat="server" /></p>
    <p>Tilgjengelige Brukere: <asp:DropDownList ID="ddlBrukere" runat="server"></asp:DropDownList></p>
    <asp:ListBox ID="lbBrukere" runat="server" Width="252px" Enabled="False"></asp:ListBox>
    <br />
    <asp:Label ID="lblFeil" runat="server" Visible="false" Text=""></asp:Label>
    <br />
    <asp:Button ID="btnLeggTilBrukere" Text="Legg Til Brukere" runat="server" OnClick="btnLeggTilBrukere_Click" />
    <asp:Button ID="btnSlettBrukere" Text="Ta bort Bruker" runat="server" OnClick="btnSlettBrukere_Click" />
       <asp:Calendar ID="cal" runat="server" Width="293px"></asp:Calendar>
    <p>Startdato:     <asp:TextBox ID="tbTidsfristStart" runat="server" Height="23px" ReadOnly="true"></asp:TextBox>
    </p>
   <p>Sluttdato:     <asp:TextBox ID="tbTidsfristSlutt" runat="server" Height="23px" Width="122px" ></asp:TextBox><asp:Button ID="btnSlutt" Text="Sett Sluttdato" OnClick="btnSlutt_Click" runat="server" Width="112px" Height="30px" />
    </p>
   <p> 
       &nbsp;</p>
    <p>Status<asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></p>
    <p>Prioritet: <asp:DropDownList ID="ddlPrioritet" runat="server"></asp:DropDownList></p>
    <p>Prosjekt: <asp:DropDownList ID="ddlProsjekt" runat="server"></asp:DropDownList></p>
    <p>Remaining time: <asp:TextBox ID="tbRemainingTime" runat="server"></asp:TextBox></p>
    <asp:label id="lblCheck" visible="false" runat="server" ></asp:label>
    <asp:Button ID="btnEndre" runat="server" OnClick="btnEndre_Click" Text="Endre Oppgave" />
</asp:Content>
