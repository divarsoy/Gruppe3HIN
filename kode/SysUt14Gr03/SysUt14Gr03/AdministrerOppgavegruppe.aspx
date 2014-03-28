<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrerOppgavegruppe.aspx.cs" Inherits="SysUt14Gr03.AdministrerOppgavegruppe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container">

        <h1>Administrer oppgavegrupper</h1>

        <br />

            <div class="table">

                <asp:PlaceHolder ID="plhOppgavetabell" runat="server"></asp:PlaceHolder>

            </div>

    </div>


</asp:Content>
