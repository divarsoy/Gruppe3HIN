<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrasjonAvTeam.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvTeam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administrering av Team</h1>
        <asp:CheckBoxList ID="cbl_team" runat="server">
        </asp:CheckBoxList>
        <asp:Button ID="bt_arkivereTeam" runat="server" Text="Arkivere Team" />
        <asp:Button ID="bt_endreTeam" runat="server" Text="Endre Team" />
</asp:Content>
