<%@ Page Title="" Language="C#" MasterPageFile="~/Site.SysAdm.Master" AutoEventWireup="true" CodeBehind="OversiktBrukereSomAdministrator.aspx.cs" Inherits="SysUt14Gr03.OversiktBrukereSomAdministrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Oversikt over brukere</h2>
    
    <asp:Label ID="lblProsjekt" runat="server"></asp:Label>
   
    <asp:PlaceHolder ID="PlaceHolderBrukere" runat="server"></asp:PlaceHolder>

</asp:Content>
