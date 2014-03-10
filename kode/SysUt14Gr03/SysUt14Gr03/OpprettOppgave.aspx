<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpprettOppgave.aspx.cs" Inherits="SysUt14Gr03.OpprettOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Opprett Oppgave</h2>

    <p> Beskrivelse av oppgave: <asp:TextBox ID="tbBeskrivelse" runat="server" Height="25px"></asp:TextBox></p>
   <p> Estimering<asp:TextBox ID="TbEstimering" runat="server" Height="25px"></asp:TextBox></p>
   <p> Tilgjengelige Brukere<asp:DropDownList ID="ddlBrukere" runat="server"></asp:DropDownList></p>
   <p> Avhengighet<asp:ListBox ID="lbAvhengighet" runat="server" Width="175px"></asp:ListBox></p>
   <p> Prioritet<asp:DropDownList ID="ddlPrioritet" runat="server"></asp:DropDownList></p>
   <p> Prosjekt<asp:DropDownList ID="ddlProsjekt" runat="server"></asp:DropDownList></p>
    <asp:Button ID="btnOpprett" runat="server" Text="Opprett Oppgave" />
</asp:Content>
