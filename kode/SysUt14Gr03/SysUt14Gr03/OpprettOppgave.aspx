<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpprettOppgave.aspx.cs" Inherits="SysUt14Gr03.OpprettOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Opprett Oppgave</h2>
    <p>Tittel: <asp:TextBox ID="tbTittel" runat="server" Height="25px"></asp:TextBox></p>
    <p> Beskrivelse av oppgave: <asp:TextBox ID="tbBeskrivelse" runat="server" Height="61px" Width="132px"></asp:TextBox></p>
   <p> Estimering<asp:TextBox ID="TbEstimering" runat="server" Height="25px" Width="160px"></asp:TextBox></p>
   <p> Tilgjengelige Brukere<asp:DropDownList ID="ddlBrukere" runat="server"></asp:DropDownList></p>
    <asp:ListBox ID="lbBrukere" runat="server" Width="157px"></asp:ListBox>
    <asp:Button ID="btnBrukere" Text="Legg Til Brukere" runat="server" OnClick="btnBrukere_Click" />
   <p> Avhengighet
       <asp:ListBox ID="lbOppgaver" runat="server" Width="175px" Height="61px"></asp:ListBox>
       <asp:Button ID="btnVelg" OnClick="btnVelg_Click" runat="server" Height="33px" Text="Legg Til" />
       <asp:Button ID="btnFjern" OnClick="btnFjern_Click" runat="server" Height="35px" Text="Fjern" />
       <asp:ListBox ID="lbAvhengighet" runat="server" Width="191px"></asp:ListBox>
    </p>
   <p> Prioritet<asp:DropDownList ID="ddlPrioritet" runat="server"></asp:DropDownList></p>
   <p> Prosjekt<asp:DropDownList ID="ddlProsjekt" runat="server"></asp:DropDownList></p>
    <asp:Button ID="btnOpprett" runat="server" OnClick="btnOpprett_Click" Text="Opprett Oppgave" />
</asp:Content>
