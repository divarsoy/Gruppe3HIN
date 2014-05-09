<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="DeaktiveringAvRegistrerteTimer.aspx.cs" Inherits="SysUt14Gr03.DeaktiveringAvRegistrerteTimer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="lblHeader" Visible="false" runat="server"></asp:Label>
    </h2>
    <div class="col-md-3">

        <asp:DropDownList ID="ddlTimer" CssClass="form-control" runat="server"></asp:DropDownList>
    </div>
    <div class="clearfix"></div>
    <asp:Button ID="btnDeaktiver" CssClass="btn btn-danger" OnClientClick="return confirm('Er du sikker på at du vil deaktivere registreringen?');" runat="server" Text="Deaktiver" OnClick="btnDeaktiver_Click" />
    <asp:Button ID="btnSeOppg" CssClass="btn btn-primary" runat="server" Text="Se Oppgave" OnClick="btnSeOppg_Click" />
    <asp:Button ID="btnEndre" CssClass="btn btn-warning" runat="server" Text="Endre" OnClick="btnEndre_Click" />
    <div class="clearfix"></div>
    <br />
    <div class="form-group">
        <div class="text-center">
            <div style="display: inline-block" class="col-md-2">
                <asp:Label ID="lblStart" Text="Starttidspunkt" runat="server" Visible="false"></asp:Label>
                <asp:TextBox ID="tbStart" CssClass="form-control" runat="server" TextMode="Time" Visible="false"></asp:TextBox>
                <br />
                <asp:Label ID="lblSlutt" Text="Sluttidspunkt" runat="server" Visible="false"></asp:Label>
                <asp:TextBox ID="tbSlutt" CssClass="form-control" runat="server" TextMode="Time" Visible="false"></asp:TextBox>
                <br />
                <asp:Label ID="lblTid" Text="Antall timer" runat="server" Visible="false"></asp:Label>
                <asp:TextBox ID="tbTid" runat="server" CssClass="form-control" TextMode="Time" Visible="false"></asp:TextBox>
                <br />

                <asp:Button ID="btnLagre" CssClass="btn btn-success" runat="server" OnClick="btnLagre_Click" OnClientClick="return confirm('Er tiden rett?');" Visible="false" Text="Lagre" />
            </div>
        </div>
    </div>
    <div class="clearfix"></div>

</asp:Content>
