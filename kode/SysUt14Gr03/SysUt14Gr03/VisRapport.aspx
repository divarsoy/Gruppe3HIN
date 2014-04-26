<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="VisRapport.aspx.cs" Inherits="SysUt14Gr03.VisRapport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Rapportsiden</h1>
    <asp:DropDownList ID="ddlProsjekter" runat="server" Visible="false"></asp:DropDownList>
    <asp:DropDownList ID="ddlBrukere" runat="server" Visible="false">
    </asp:DropDownList>
    <br /> 
    <asp:Panel ID="pnlGrafikk" runat="server" Visible="false">
    </asp:Panel>
    <asp:chart id="crtKake" runat="server"
             Height="300px" Width="400px">
  <titles>
    <asp:Title ShadowOffset="3" Name="Title1" />
  </titles>
  <legends>
    <asp:Legend Alignment="Center" Docking="Bottom"
                IsTextAutoFit="False" Name="Default"
                LegendStyle="Row" />
  </legends>
  <series>
    <asp:Series Name="Default" />
  </series>
  <chartareas>
    <asp:ChartArea Name="ChartArea1"
                     BorderWidth="0" />
  </chartareas>
</asp:chart>
    <asp:Button ID="btnTeam" runat="server" Text="Vis en teamrapport" CssClass="btn btn-primary" Visible="false" OnClick="btnTeam_Click"/>
    <asp:Button ID="btnProsjekt" runat="server" Text="Vis en prosjektrapport" CssClass="btn btn-primary" Visible="false" OnClick="btnProsjekt_Click"/>
    <asp:Button ID="btnIndivid" runat="server" Text="Vis en individrapport" CssClass="btn btn-primary" Visible="false" OnClick="btnIndivid_Click"/>
        <br />
    <asp:Label ID="lblTest" runat="server" Text="Label"></asp:Label>
</asp:Content>
