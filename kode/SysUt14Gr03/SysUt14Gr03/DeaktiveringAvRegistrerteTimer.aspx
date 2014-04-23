<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="DeaktiveringAvRegistrerteTimer.aspx.cs" Inherits="SysUt14Gr03.DeaktiveringAvRegistrerteTimer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="text-align: center">Endre/Deaktiver Registrerte Timer</h2>
    <div style="text-align: center">
        <div style="align-self: center" class="col-sm-3">
            <asp:DropDownList ID="ddlTimer" CssClass="form-control" runat="server"></asp:DropDownList>
        </div>
        <div class="clearfix"></div>
        <hr />
        <asp:Button ID="btnDeaktiver" OnClientClick="return confirm('Er du sikker på at du vil deaktivere registreringen?');" runat="server" Text="Deaktiver" OnClick="btnDeaktiver_Click" />
        <br />
        <asp:Button ID="btnSeOppg" runat="server" Text="Se Oppgave" OnClick="btnSeOppg_Click" />
        <br />
        <asp:Button ID="btnEndre" runat="server" Text="Endre" OnClick="btnEndre_Click" />
        <br />
        <asp:Label ID="lblStart" Text="Start" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="tbStart" runat="server" TextMode="DateTime" Visible="false"></asp:TextBox>
        <br />
        <asp:Label ID="lblSlutt" Text="Stopp" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="tbSlutt" runat="server" TextMode="DateTime" Visible="false"></asp:TextBox>
        <br />
        <asp:Label ID="lblTid" Text="Tid" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="tbTid" runat="server" TextMode="Time" Visible="false"></asp:TextBox>
        <br />
        <asp:Button ID="btnLagre" runat="server" OnClick="btnLagre_Click" OnClientClick="return confirm('Er tiden rett?');" Visible="false" Text="Lagre" />
        <asp:Label ID="lblInfo" Visible="false" runat="server"></asp:Label>
    </div>
</asp:Content>
