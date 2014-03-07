<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpprettOppgave.aspx.cs" Inherits="SysUt14Gr03.OpprettOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Opprett Oppgave</h2>
     Beskrivelse av oppgave: <asp:TextBox ID="tbBeskrivelse" runat="server"></asp:TextBox>
    Estimering<asp:TextBox ID="TbEstimering" runat="server"></asp:TextBox>
    Tilgjengelige Brukere<asp:DropDownList ID="ddlBrukere" runat="server"></asp:DropDownList>
    <asp:CheckBox ID="cbAvhengighet" Text="Avhengighet" runat="server" />
    Prioritet<asp:DropDownList ID="ddlPrioritet" runat="server"></asp:DropDownList>
    Prosjekt<asp:DropDownList ID="ddlProsjekt" runat="server"></asp:DropDownList>
    <asp:Button ID="btnOpprett" runat="server" Text="Opprett Oppgave" />
</asp:Content>
