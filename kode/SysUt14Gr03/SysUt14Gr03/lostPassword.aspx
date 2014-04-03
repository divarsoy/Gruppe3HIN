<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="lostPassword.aspx.cs" Inherits="SysUt14Gr03.lostPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentTemplate" Runat="Server">
    <h1>
        Få tilsendt et nytt passord</h1>
    <p>
        E-postadresse:
        <asp:TextBox ID="Email" runat="server"></asp:TextBox></p>
    <p>
        <asp:Button ID="sendPassword" runat="server" Text="Send Passord" OnClick="sendPasswordButton_Click" /> </p>
    <p>
        <asp:Label ID="InvalidEmailMessage" runat="server" ForeColor="Red" Text="Feil e-post"
            Visible="False"></asp:Label> </p>
</asp:Content>