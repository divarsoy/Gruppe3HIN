<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="ManuellTimeregistrering.aspx.cs" Inherits="SysUt14Gr03.ManuellTimeregistrering" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
      <asp:Label ID="lblTittel" runat="server"></asp:Label>
    </h1>
    <asp:Label ID="Label1" runat="server" Text="Registrer timer for..." Visible="false"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlDag" runat="server" Visible="false"></asp:DropDownList>

    <br />
    <asp:Label ID="Label2" runat="server" Text="Starttidspunkt:" Visible="false"></asp:Label>
    <asp:TextBox ID="txtStart" runat="server" TextMode="Time" Visible="false"></asp:TextBox>
    <asp:Label ID="Label3" runat="server" Text="Sluttidspunkt:" Visible="false"></asp:Label>
    <asp:TextBox ID="txtSlutt" runat="server" TextMode="Time" Visible="false"></asp:TextBox>
    <br />
    <asp:Button ID="btnAddPause" runat="server" Text="Legg til pause" OnClick="btnAddPause_Click" Visible="false"/>
    <br />
    <asp:Panel ID="pnlPauser" runat="server">
    </asp:Panel>

    <br />
    <asp:Button ID="btnLagre" runat="server" OnClick="btnLagre_Click" Text="Lagre timer" Visible="false"/>


    <asp:Label ID="lblTest" runat="server" Visible="false"></asp:Label>


    <asp:Button ID="btnFullfor" runat="server" OnClick="btnFullfor_Click" Text="Godta og lagre" Visible="false"/>


</asp:Content>
