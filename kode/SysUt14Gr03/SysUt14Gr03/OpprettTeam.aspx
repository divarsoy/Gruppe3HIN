<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpprettTeam.aspx.cs" Inherits="SysUt14Gr03.OpprettTeam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Opprett team</h1>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Navn på team"></asp:Label>
        <asp:TextBox ID="txtTeamNavn" runat="server" MaxLength="128" ToolTip="Navn på team"></asp:TextBox>
    </p>
    <asp:Panel ID="pnlBrukere" runat="server" Height="40%" ScrollBars="Auto">
        <asp:CheckBoxList ID="cblBrukere" runat="server">
        </asp:CheckBoxList>
    </asp:Panel>
    <hr />

    <asp:Label ID="NoUsersSelected" runat="server" ForeColor="Red" Text="Ingen brukere valgt"
            Visible="False"></asp:Label> 
    <asp:Label ID="TeamOK" runat="server" ForeColor="Green" Text="Team opprettet!"
            Visible="False"></asp:Label><br />
    <asp:Button ID="btnOK" runat="server" Text="Opprett team" OnClick="btnOK_Click" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Avbryt" />
    </asp:Content>

