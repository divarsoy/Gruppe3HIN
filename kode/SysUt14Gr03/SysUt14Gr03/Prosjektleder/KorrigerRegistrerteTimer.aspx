<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="KorrigerRegistrerteTimer.aspx.cs" Inherits="SysUt14Gr03.Prosjektleder.KorrigerRegistrerteTimer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearfix"></div>
    <h1>
      <asp:Label ID="lblTittel" runat="server"></asp:Label>
    </h1>
    <div class="form-group">
            <div class="row">
                <div style="display: inline-block" class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Sett dato" Visible="false"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtDato" runat="server" Visible="false" CssClass="form-control" TextMode="Date"></asp:TextBox>

                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Starttidspunkt:" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtStart" runat="server" TextMode="Time" Visible="false" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text="Sluttidspunkt:" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtSlutt" CssClass="form-control" runat="server" TextMode="Time" Visible="false"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnAddPause" runat="server" CssClass="btn btn-primary" Text="Legg til pause" OnClick="btnAddPause_Click" Visible="false"/>
                    <br />
                    
                    <br />
                 </div>
            </div>
        <asp:Panel ID="pnlPauser" runat="server">
                    </asp:Panel>
        </div>
    <asp:HiddenField ID="lagreTime" runat="server" value="" />
    <asp:HiddenField ID="infoField" runat="server" value="" />
    <br />
    <asp:Button ID="btnLagre" runat="server" CssClass="btn btn-warning" Text="Lagre og gå tilbake" Visible="false" OnClick="btnLagre_Click" />
    <asp:Button ID="btnGodkjenn" runat="server" CssClass="btn btn-success" Text="Lagre og godkjenn" Visible="false" OnClick="btnGodkjenn_Click"/>
    <asp:Label ID="lblFeilmelding" runat="server" Visible="false"></asp:Label>
</asp:Content>
