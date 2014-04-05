<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="ManuellTimeregistrering.aspx.cs" Inherits="SysUt14Gr03.ManuellTimeregistrering" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
      <asp:Label ID="lblTittel" runat="server"></asp:Label>
    </h1>
    Registrer timer for..
    <br />
    <asp:DropDownList ID="ddlDag" runat="server"></asp:DropDownList>

    <br />
    Starttidspunkt:<asp:TextBox ID="txtStart" runat="server" TextMode="Time"></asp:TextBox>
    Sluttidspunklt:<asp:TextBox ID="txtSlutt" runat="server" TextMode="Time"></asp:TextBox>
    <br />
    <asp:Button ID="btnAddPause" runat="server" Text="Legg til pause" OnClick="btnAddPause_Click" />
    <br />
    <asp:Panel ID="pnlPauser" runat="server">
    </asp:Panel>


    <br />
    <asp:Button ID="btnLagre" runat="server" OnClick="btnLagre_Click" Text="Lagre timer" />


    <asp:Label ID="lblTest" runat="server" Text="Label"></asp:Label>


</asp:Content>
