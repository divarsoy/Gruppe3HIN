<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="lostPassword.aspx.cs" Inherits="SysUt14Gr03.lostPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>Få tilsendt et nytt passord</h1>
    <p>
        Brukernavn:
        <asp:TextBox ID="Email" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="sendPassword" runat="server" Text="Send Passord" OnClick="sendPasswordButton_Click" CssClass="btn btn-primary btn-large" />
    </p>
</asp:Content>