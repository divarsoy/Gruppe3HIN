
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="VisRapport.aspx.cs" Inherits="SysUt14Gr03.VisRapport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Rapportsiden</h1>
    <div class="col-sm-3">
    <asp:DropDownList ID="ddlProsjekter" CssClass="form-control" runat="server" Visible="false"></asp:DropDownList>
    <asp:DropDownList ID="ddlBrukere" CssClass="form-control" runat="server" Visible="false">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlTeam" CssClass="form-control" runat="server" Visible="False">
    </asp:DropDownList>
        </div>
        <div class="clearfix"></div>
    <br /> 
    <div class="formGroup">
        <asp:Button ID="btnTeam" runat="server" Text="Vis en teamrapport" CssClass="btn btn-primary" Visible="false" OnClick="btnTeam_Click"/>
        <asp:Button ID="btnProsjekt" runat="server" Text="Vis en prosjektrapport" CssClass="btn btn-primary" Visible="false" OnClick="btnProsjekt_Click"/>
        <asp:Button ID="btnIndivid" runat="server" Text="Vis en individrapport" CssClass="btn btn-primary" Visible="false" OnClick="btnIndivid_Click"/>
        <div class="clearfix"></div>
        <asp:Button ID="btnLastNedTeam" runat="server" Text="Eksporter til excel" CssClass="btn btn-success" OnClick="btnLastNedTeam_Click" Visible="false"/>
        <asp:Button ID="btnLastNedProsjekt" runat="server" Text="Eksporter til excel" CssClass="btn btn-success" Visible="false" OnClick="btnLastNedProsjekt_Click"/>
        <asp:Button ID="btnLastNedIndivid" runat="server" Text="Eksporter til excel" CssClass="btn btn-success" Visible="false" OnClick="btnLastNedIndivid_Click"/>
        </div>
    <asp:Panel ID="pnlGrafikk" runat="server" Visible="false">
    </asp:Panel>
    
        <br />
    <asp:Label ID="lblTest" runat="server" Visible="false"></asp:Label>
</asp:Content>
