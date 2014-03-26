﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="InvitasjonAvBruker.aspx.cs" Inherits="SysUt14Gr03.InvitasjonAvBruker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentUtvikler" runat="server">
    <h1>Invitasjon Til Bruker</h1>
    <asp:Label ID="lblbrukerInnlogget" runat="server"></asp:Label>
    <h4>Oppgaver som jeg deltar i</h4>
    <asp:DropDownList ID="ddlOppgave" runat="server"></asp:DropDownList>

    <h4>Send invitasjon til en annen bruker</h4>
    <asp:DropDownList ID="ddlBrukere" runat="server"></asp:DropDownList>
    <asp:Button ID="btnSendInvitasjon" runat="server" Text="Send Invitasjon" OnClick="btnSendInvitasjon_Click" Height="33px"/>
    <asp:Label ID="lblInvitasjon" runat="server"></asp:Label>
    
</asp:Content>
