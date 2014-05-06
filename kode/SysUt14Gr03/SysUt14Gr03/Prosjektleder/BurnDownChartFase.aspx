<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="BurnDownChartFase.aspx.cs" Inherits="SysUt14Gr03.Prosjektleder.BurnDownChartFase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="testSheep" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="table table-responsive">
     <asp:PlaceHolder ID="PlaceHolderTable"  runat="server"></asp:PlaceHolder>
        </div>       
</asp:Content>  
