<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Brukere.aspx.cs" Inherits="SysUt14Gr03.Brukere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Velkommen på deg</h1>
    <p><%=bruker.Etternavn%></p>
    <p><%=bruker.Navn()%></p>
</asp:Content>
