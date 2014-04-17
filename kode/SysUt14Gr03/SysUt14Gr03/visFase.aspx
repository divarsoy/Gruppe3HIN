<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="visFase.aspx.cs" Inherits="SysUt14Gr03.visFase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div style="text-align:center">
    <h2>
        <asp:Label ID="lblFase" runat="server"></asp:Label>
    </h2>
    <hr />
    <br />
    <h3>
    <asp:Label ID="lblInfo" runat="server"></asp:Label>       
    </h3>
        </div>
</asp:Content>
