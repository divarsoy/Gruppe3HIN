<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InvitasjonAvBruker.aspx.cs" Inherits="SysUt14Gr03.InvitasjonAvBruker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Invitasjon Til Bruker</h1>
    <asp:DropDownList ID="ddlBrukere" runat="server"></asp:DropDownList>
    <asp:Button ID="btnSendInvitasjon" runat="server" Text="Send Invitasjon" OnClick="btnSendInvitasjon_Click" Height="33px"/>
     <div class="alert-message error">
    <a class="close" href="#">×</a>
    <p><strong>Oh snap!</strong> Change this and that and <a href="#">try again</a>.</p>
  </div>
</asp:Content>
