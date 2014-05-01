<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="SprintBacklogFase.aspx.cs" Inherits="SysUt14Gr03.SprintBacklogFase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="lblfasenavn" runat="server"></asp:Label></h2>
    <div class="col-sm-3">
        <asp:DropDownList ID="ddlfaser" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>
    </div>
    <br />
    <asp:PlaceHolder ID="phFase" runat="server"></asp:PlaceHolder>
    <asp:Button ID="btnExport" CssClass="btn btn-success" runat="server" OnClick="btnExport_Click" Text="Exporter til excel dokument" />

</asp:Content>
