<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="DeaktiveringAvRegistrerteTimer.aspx.cs" Inherits="SysUt14Gr03.DeaktiveringAvRegistrerteTimer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
       <asp:Label ID="lblRegTimer"  runat="server" Visible="false"></asp:Label>
    <hr />
    <asp:Button ID="btnDeaktiver" runat="server" Text="Deaktiver" BackColor="Red" Height="90%" ForeColor="Wheat" OnClick="btnDeaktiver_Click" Width="40%" />
    <asp:Button ID="btnEndre" runat="server" Text="Endre" OnClick="btnEndre_Click" Width="40%" BackColor="Green" ForeColor="White" Height="90%" />
</asp:Content>
