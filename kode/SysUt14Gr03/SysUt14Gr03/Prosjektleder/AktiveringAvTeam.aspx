﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" CodeBehind="AktiveringAvTeam.aspx.cs" Inherits="SysUt14Gr03.Prosjektleder.AktiveringAvTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container">
    <h1>Aktivering av Teams</h1>
    <br />
        <div class="checkbox">
            <asp:CheckBoxList ID="cbl_team"  runat="server" /> 
            <p><asp:Label ID="lblTilbakeMelding" ForeColor="Red" runat="server" Text="Det eksisterer ingen arkiverte team!" CssClass="alert" Visible="false"></asp:Label></p>
        </div>
        <asp:Button ID="bt_aktivereTeam" CssClass="btn btn-primary" runat="server" Text="Aktiver Team" OnClick="bt_aktiverTeam_Click" />
    </div>
</asp:Content>

