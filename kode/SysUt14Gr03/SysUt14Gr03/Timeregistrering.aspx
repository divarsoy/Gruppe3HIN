<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="Timeregistrering.aspx.cs" Inherits="SysUt14Gr03.Timeregistrering" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="lblTimeregistrering" runat="server" Text=""></asp:Label></h1>
    <h4>Velg Oppgave</h4>
    <asp:DropDownList ID="ddlOppgaver" runat="server"></asp:DropDownList>
    <br />
    <h4>Dine tidsregistreringer:</h4>
    <asp:TextBox ID="tbTidsregistrert" runat="server" TextMode="MultiLine" Height="100px" Width="300px" ReadOnly="true"></asp:TextBox>
    <div class="btn-Group">
        <asp:Button id="btnSnart" CssClass="btn btn-success btn-large" runat="server" Onclick="btnStart_Click" Text="Start"/>
        <asp:Button id="btnPause" CssClass="btn btn-warning btn-large" runat="server" OnClick="btnPause_Click" Text="Pause"/>
        <asp:Button id="btnStop" CssClass="btn btn-danger btn-large" runat="server" OnClick="btnStop_Click" Text="Stop"/>
    </div>
    <br />

    <h4>Kommentar til oppgaven:</h4>
    <asp:TextBox ID="tbKommentar" runat="server" TextMode="MultiLine" Height="150px" Width="300px"></asp:TextBox>
    <br/>
    <asp:Button id="btnRegistrer" CssClass="btn btn-primary btn-large" runat="server" OnClick="btnRegistrer_Click" Text="Registrer"/>
</asp:Content>