﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="Innstillinger.aspx.cs" Inherits="SysUt14Gr03.Innstillinger" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Mine innstillinger</h1>
    <h2>Personlige opplysninger</h2>
    <div class="form-group">
        <div class="col-sm-3">
            <asp:TextBox ID="txtFornavn" runat="server" CssClass="form-control" PlaceHolder="Fornavn"></asp:TextBox>
            <asp:TextBox ID="txtEtternavn" runat="server" CssClass="form-control" PlaceHolder="Etternavn"></asp:TextBox>

            <asp:TextBox ID="txtBrukernavn" runat="server" CssClass="form-control" PlaceHolder="Brukernavn"></asp:TextBox>

            <asp:TextBox ID="txtIM" runat="server" CssClass="form-control" PlaceHolder="Internt kallenavn"></asp:TextBox>
       </div>
    </div>
            
    <div class="clearfix"></div>
    <h3>Endre passord</h3>
    <div class="form-group">
        <div class="col-sm-3">
            <asp:TextBox ID="txtPassord" runat="server" CssClass="form-control" TextMode="Password" PlaceHolder="Gammelt passord"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtNyPass" runat="server" CssClass="form-control" TextMode="Password" PlaceHolder="Nytt passord"></asp:TextBox>
            <asp:TextBox ID="txtNyPass1" runat="server" CssClass="form-control" TextMode="Password" PlaceHolder="Bekreft nytt passord"></asp:TextBox>
            <asp:Button ID="btnLagrePassord" runat="server" CssClass="btn btn-primary" Text="Lagre nytt passord" OnClick="btnLagrePassord_Click" />
        </div>
    </div>
    <div class="clearfix"></div>
    <h2>Varsler for e-post</h2>
    <asp:CheckBoxList ID="cblElementer" runat="server" CssClass="epost_pref_tabell">
        </asp:CheckBoxList>
    <asp:Button ID="btnLagre" runat="server" CssClass="btn btn-primary" Text="Lagre innstillinger" OnClick="btnLagre_Click" />
</asp:Content>