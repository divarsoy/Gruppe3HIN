<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrasjonAvTeamBrukere.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvTeamBrukere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12">
        <asp:CheckBoxList ID="cbl_TeamBrukere" runat="server">
        </asp:CheckBoxList>
        <asp:Button ID="bt_fjerneBruker" runat="server" Text="Fjern bruker" />
    </div>
    <div class="col-md-12">
       
        <asp:CheckBoxList ID="cbl_brukere" runat="server">
        </asp:CheckBoxList>
        <asp:Button ID="bt_leggeTilBruker" runat="server" Text="Legg til bruker" />
       
    </div>
    <div class="col-md-0">
      
    </div>
</asp:Content>
