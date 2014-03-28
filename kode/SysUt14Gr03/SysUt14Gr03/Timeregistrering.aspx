<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Timeregistrering.aspx.cs" Inherits="SysUt14Gr03.Timeregistrering" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Timeregistrering for en oppgave</h1>
    <asp:TextBox ID="tbTidsregistrert" runat="server" TextMode="MultiLine" Height="100px" Width="300px"></asp:TextBox>

    <div class="btn-Group">
        <asp:Button id="btnSnart" CssClass="btn btn-success btn-large" runat="server" onclick="btnStart_Click" Text="Start"/>
        <asp:Button id="btnPause" CssClass="btn btn-warning btn-large" runat="server" OnClick="btnPause_Click" Text="Pause"/>
        <asp:Button id="btnStop" CssClass="btn btn-danger btn-large" runat="server" OnClick="btnStop_Click" Text="Stop"/>
    </div>

    <asp:DropDownList ID="ddlOppgaver" runat="server"></asp:DropDownList>
    <br />
    <asp:TextBox ID="tbKommentar" runat="server" TextMode="MultiLine" Height="150px" Width="300px"></asp:TextBox>
</asp:Content>

