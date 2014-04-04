<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="VisBruker.aspx.cs" Inherits="SysUt14Gr03.VisBruker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblNavn" runat="server" Text="Navn"></asp:Label>
    </h1>
    <hr />
    <br />
    <asp:Label ID="lblInfo" runat="server" Text="Info" Visible="False"></asp:Label>
    <br />
    <asp:Label ID="lblOppgaver" runat="server" Visible="False"></asp:Label>
    <br />
    <asp:ListBox ID="lsbOppgaver" AutoPostBack="True" runat="server" Visible="False" OnSelectedIndexChanged="lsbOppgaver_SelectedIndexChanged" ></asp:ListBox>
    <br />
    <asp:Label ID="lblKommentarer" runat="server" Visible="False"></asp:Label>
    <br />
    <asp:ListBox ID="lsbKommentarer" AutoPostBack="True" runat="server" Visible="False" OnSelectedIndexChanged="lsbKommentarer_SelectedIndexChanged"></asp:ListBox>
    <br />
    <asp:Label ID="lblLogg" runat="server" Visible="False"></asp:Label>
    <br />
    <asp:ListBox ID="lsbLogg" runat="server" Visible="False"></asp:ListBox>
    <br />
    <asp:Label ID="lblFullfort" runat="server" Visible="False"></asp:Label>
    <br />
    <asp:ListBox ID="lsbFFullfort" runat="server" Visible="False"></asp:ListBox>
    <br />
    <asp:Label ID="lblPrefs" runat="server" Visible="False"></asp:Label>
    <br />
    <asp:Button ID="btnPrefs" runat="server" OnClick="btnPrefs_Click" Text="Endre instillinger" Visible="False"/>
    <br />
</asp:Content>
