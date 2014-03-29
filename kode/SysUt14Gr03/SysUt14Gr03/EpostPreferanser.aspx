<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="EpostPreferanser.aspx.cs" Inherits="SysUt14Gr03.EpostPreferanser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentUtvikler" runat="server">
    <div class="container">
    <h1> Innstillinger for varsling</h1>
    <h4> Varsle meg på e-post når jeg blir...</h4>

        <asp:CheckBoxList ID="cblElementer" runat="server" CssClass="epost_pref_tabell">
        </asp:CheckBoxList>
   
        <asp:Button ID="btnLagre" runat="server" OnClick="btnLagre_Click" Text="Lagre" CssClass="btn btn-primary"/>
        <asp:Button ID="btnAvbryt" runat="server" Text="Avbryt" OnClick="btnAvbryt_Click" CssClass="btn btn-default"/>
   
        </div>
    </asp:Content>
