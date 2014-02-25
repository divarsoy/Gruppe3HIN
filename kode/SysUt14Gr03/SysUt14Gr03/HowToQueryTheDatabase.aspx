<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HowToQueryTheDatabase.aspx.cs" Inherits="SysUt14Gr03.HowToQueryTheDatabase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> Hente ut bruker med id="1" </h1>
    <p><b>Etternavn: </b><%=bruker.Etternavn%></p>
    <p><b>Fornavn: </b><%=bruker.Fornavn%></p>
    </asp:Content>
