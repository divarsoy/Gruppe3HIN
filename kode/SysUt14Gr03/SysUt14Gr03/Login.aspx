<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SysUt14Gr03.Logon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
        Logg inn</h1>
    <p>
        E-postadresse:
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox></p>
    <p>
        Passord:
        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox></p>
    <p>
        <asp:CheckBox ID="RememberMe" runat="server" Text="Husk meg" /> </p>
    <p>
        <asp:Button ID="LoginButton" runat="server" Text="Logg inn" OnClick="LoginButton_Click" /> </p>
    <p>
        <asp:Label ID="InvalidCredentialsMessage" runat="server" ForeColor="Red" Text="Feil brukernavn eller passord"
            Visible="False"></asp:Label> </p>
</asp:Content>
