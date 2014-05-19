<%@ Page Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="OpprettTeam.aspx.cs" Inherits="SysUt14Gr03.OpprettTeam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h1>Opprett team</h1>
        <br />

        <label for="inputTeam">Navn på team</label>
        <asp:TextBox ID="txtTeamNavn" runat="server" MaxLength="128" ToolTip="Navn på team"></asp:TextBox>
    
        <asp:Panel ID="pnlBrukere" runat="server" Height="40%" ScrollBars="Auto">
            <asp:CheckBoxList ID="cblBrukere" CssClass="epost_pref_tabell" runat="server">
            </asp:CheckBoxList>
        </asp:Panel>
        <br />
        <asp:Button ID="btnOK" class="btn btn-primary" runat="server" Text="Opprett team" OnClick="btnOK_Click" />
            &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" CssClass="btn btn-warning" runat="server" OnClick="btnCancel_Click" Text="Avbryt" />

    </div>
</asp:Content>

