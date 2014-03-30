<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="OpprettProsjekt.aspx.cs" Inherits="SysUt14Gr03.OpprettProsjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
       <h2>Opprett Prosjekt</h2>
       <br />

        <div class="form-group">
            <label for="inputProsjektnavn">Prosjektnavn</label>
            <asp:TextBox ID="tbProsjektnavn" runat="server" ></asp:TextBox>
        </div>
        <div class="clearfix"></div>

        <div class="form-group">
            <label for="valgProsjektleder">Prosjektleder</label>
            <asp:DropDownList ID="ddlBrukere" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Selected = "True" Text = "Velg Prosjektleder" Value = "0"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="clearfix"></div>
   
        <div class="form-group">
            <label for="valgTeam">Team</label>
            <asp:DropDownList ID="dropTeam" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Selected = "True" Text = "Velg Team" Value = "0"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="clearfix"></div>

        <asp:Calendar ID="cal" runat="server" Width="300px"></asp:Calendar>
        <br />

        
        <div class="form-group">
            <label for="inputStartdato">Startdato</label>
            <asp:TextBox ID="tbStart" runat="server"  ></asp:TextBox>
            <asp:Button ID="btnStart" CssClass="btn btn-primary" Text="Sett Startdato" OnClick="btnStart_Click" runat="server"  />
        </div>
        
        <div class="form-group">
            <label for="inputSluttdato">Sluttdato</label>
            <asp:TextBox ID="tbSlutt" runat="server" ></asp:TextBox>
            <asp:Button ID="btnSlutt" CssClass="btn btn-primary" Text="Sett Sluttdato" OnClick="btnSlutt_Click" runat="server"  />
        </div>


    <asp:Button ID="btnLagre" CssClass="btn btn-primary" runat="server" Text="Lagre Prosjekt" OnClick="btnLagre_Click" Width="300px" />
    <asp:Label ID="lblFeil" runat="server" Visible="False"></asp:Label>

    </div>
</asp:Content>
