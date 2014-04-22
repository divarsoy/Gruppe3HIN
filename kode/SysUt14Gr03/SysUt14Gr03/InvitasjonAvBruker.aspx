<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="InvitasjonAvBruker.aspx.cs" Inherits="SysUt14Gr03.InvitasjonAvBruker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Invitasjon Til Bruker</h1>

    <h4>Oppgave som jeg trenger hjelp til</h4>
    <div class="form-group">
  
    <asp:Label ID="lblInvitasjon" runat="server"></asp:Label>
        </div>
        
    <div class="clearfix"></div>

    <h4>Send invitasjon til en annen bruker</h4>
    <div class="col-sm-3">
    <asp:DropDownList ID="ddlBrukere"  CssClass="form-control" runat="server"></asp:DropDownList>
        </div>
    <div class="clearfix"></div>
    <asp:Button ID="btnSendInvitasjon" CssClass="btn btn-primary" runat="server" Text="Send Invitasjon" OnClick="btnSendInvitasjon_Click" Height="33px"/>
    
    
</asp:Content>
