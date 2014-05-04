<%@ Page Title="changeUser" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="changeUser.aspx.cs" Inherits="SysUt14Gr03.changeUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
        Endre Brukerinformasjon</h1>
    <p>
        Fornavn:
        <asp:TextBox ID="FirstName" runat="server"></asp:TextBox></p>
    <p>
        Etternavn:
        <asp:TextBox ID="SurName" runat="server"></asp:TextBox></p>
    <p>
        Brukernavn:
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox></p>
    <p>
        Epost:
        <asp:TextBox ID="Email" runat="server"></asp:TextBox></p>
    <p>
        Passord:
        <asp:Button ID="changePassword" runat="server" Text="Endre Passord" OnClick="btnPasswordChange_Click" />
    <asp:Panel ID="HiddenPanel" runat="server" ScrollBars="Auto" Width="100%" Height="395px" Visible="false">
        Gammelt passord:
        <asp:TextBox ID="OldPassord" runat="server"></asp:TextBox>

        Nytt passord:
        <asp:TextBox ID="NewPassword" runat="server"></asp:TextBox>

        Bekreft passord:
        <asp:TextBox ID="ConfirmPassword" runat="server"></asp:TextBox>
    </asp:Panel>
    <p>
        IM:
        <asp:TextBox ID="IM" runat="server"></asp:TextBox></p>    
    <p>
        <asp:Button ID="btnUserChange" runat="server" Text="Lagre endringer" OnClick="btnUserChange_Click" /> </p>
</asp:Content>
