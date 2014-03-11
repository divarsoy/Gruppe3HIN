<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KommentarPaOppgave.aspx.cs" Inherits="SysUt14Gr03.KommentarPaOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> Kommenter på oppgave</h1>
    <p> 
        <asp:DropDownList ID="ddlOppgave" runat="server">
        </asp:DropDownList>
    </p>
    <p> 
        <asp:TextBox ID="txtKommentar" runat="server" MaxLength="128" TextMode="MultiLine"></asp:TextBox>
    </p>
    <p> 
        <asp:Label ID="lblFeil" runat="server" Visible="false"></asp:Label>
    </p>
    <p> 
        <asp:Button ID="btnKommentar" runat="server" OnClick="btnKommentar_Click" Text="Legg inn kommentar" />
    </p>
</asp:Content>
