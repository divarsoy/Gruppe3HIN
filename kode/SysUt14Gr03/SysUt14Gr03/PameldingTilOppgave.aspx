﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="PameldingTilOppgave.aspx.cs" Inherits="SysUt14Gr03.PameldingTilOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentUtvikler" runat="server">
    <h1>Brukers påmelding til oppgave</h1>
    <p>
        <asp:DropDownList ID="ddlOppgaver" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="ddlBrukere" runat="server" Visible="false">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Label ID="lblMelding" runat="server" Visible="false"></asp:Label>
    </p>
    <p>
        <asp:Button ID="btnLeggTil" runat="server" OnClick="btnLeggTil_Click" Text="Legg til bruker" />
    </p>
</asp:Content>
