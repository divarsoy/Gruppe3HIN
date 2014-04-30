<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="Innstillinger.aspx.cs" Inherits="SysUt14Gr03.Innstillinger" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Mine innstillinger</h1>
    <h2>Personlige opplysninger</h2>
    <asp:TextBox ID="txtFornavn" runat="server"></asp:TextBox>
    <br />
    <asp:TextBox ID="txtEtternavn" runat="server"></asp:TextBox>
        <br />
    <asp:TextBox ID="txtBrukernavn" runat="server"></asp:TextBox>
        <br />
    <asp:TextBox ID="txtIM" runat="server"></asp:TextBox>
    <br />
    Endre passord
    <asp:TextBox ID="txtPassord" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    <asp:TextBox ID="txtNyPass" runat="server" TextMode="Password"></asp:TextBox>
    <asp:TextBox ID="txtNyPass1" runat="server" TextMode="Password"></asp:TextBox>
    <asp:Button ID="btnLagrePassord" runat="server" Text="Lagre nytt passord" OnClick="btnLagrePassord_Click" />

    <hr />
    <h2>Varsler for e-post</h2>
    <asp:CheckBoxList ID="cblElementer" runat="server" CssClass="epost_pref_tabell">
        </asp:CheckBoxList>
    <asp:Button ID="btnLagre" runat="server" Text="Lagre innstillinger" OnClick="btnLagre_Click" />
</asp:Content>
