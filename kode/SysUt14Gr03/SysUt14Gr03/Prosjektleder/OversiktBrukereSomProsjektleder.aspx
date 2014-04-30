<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="OversiktBrukereSomProsjektleder.aspx.cs" Inherits="SysUt14Gr03.OversiktBrukereSomProsjektleder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Oversikt over brukere</h2>
    
    <h4><asp:Label ID="lblProsjekt" runat="server"></asp:Label></h4>
   
    <asp:PlaceHolder ID="PlaceHolderBrukere" runat="server"></asp:PlaceHolder>

</asp:Content>
