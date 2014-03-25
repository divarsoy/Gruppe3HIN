<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="EpostPreferanser.aspx.cs" Inherits="SysUt14Gr03.EpostPreferanser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentUtvikler" runat="server">
    <h1> Innstillinger for varsling</h1>
    <h4> Varsle meg når jeg blir...</h4>
    <p> 
        <asp:CheckBoxList ID="cblElementer" runat="server">
        </asp:CheckBoxList>
    </p>
    <p> 
        <asp:Button ID="btnLagre" runat="server" OnClick="btnLagre_Click" Text="Lagre" />
        <asp:Button ID="btnAvbryt" runat="server" Text="Avbryt" OnClick="btnAvbryt_Click" />
    </p>
    </asp:Content>
