﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="DeaktiveringAvRegistrerteTimer.aspx.cs" Inherits="SysUt14Gr03.DeaktiveringAvRegistrerteTimer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align:center">
        <div class="col-sm-3">
    <asp:DropDownList ID="ddlTimer" CssClass="form-control" runat="server"></asp:DropDownList>
      </div>
         <div class="clearfix"></div>
    <hr />
    <asp:Button ID="btnDeaktiver" runat="server" Text="Deaktiver" BackColor="Red" Height="90%" ForeColor="Wheat" OnClick="btnDeaktiver_Click" Width="40%" />
    <br />
    <asp:Button ID="btnEndre" runat="server" Text="Se Oppgave" OnClick="btnEndre_Click" Width="40%" BackColor="Green" ForeColor="White" Height="90%" />
    <asp:Label ID="lblInfo" Visible="false" runat="server"></asp:Label>
        <asp:Textbox ID="tbStart" runat="server" TextMode="Time" Visible="false"></asp:Textbox>
</div>
</asp:Content>
