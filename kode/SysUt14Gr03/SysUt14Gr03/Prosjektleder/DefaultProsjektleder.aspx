<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="DefaultProsjektleder.aspx.cs" Inherits="SysUt14Gr03.DefaultProsjektleder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-md-12">
            <h1>Forside for Brukere</h1>
        </div>
        <div class="col-md-4">
            <h3>Velkommen <%: Fornavn %>!</h3>
            <h4>Velg Prosjekt:</h4>
            <asp:ListBox ID="ListBoxProsjekt" runat="server" Rows="1"></asp:ListBox>
            <asp:Button ID="btnVelgProsjekt" runat="server" Text="Velg Prosjekt" OnClick="btnVelgProsjekt_Click" />
        </div>
        <div class ="col-md-4">
            <asp:Label ID="lblValgtProsjekt" runat="server" Text=""></asp:Label> 
        </div>
    </div>
</asp:Content>
