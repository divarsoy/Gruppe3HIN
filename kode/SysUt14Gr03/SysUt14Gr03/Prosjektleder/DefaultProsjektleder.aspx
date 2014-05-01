<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="DefaultProsjektleder.aspx.cs" Inherits="SysUt14Gr03.DefaultProsjektleder" %>
<asp:Content ID="test" ContentPlaceHolderID="testSheep" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="jumbotron">
      <div class="container">
        <span class="glyphicon glyphicon-paperclip"></span>  
        <h1>Prosjektledersiden</h1>
        <p>På denne websiden kan du opprette prosjekter, team, brukere og oppgaver, samt administrere disse. Start med å velge et prosjekt nedenfor.</p>
      </div>
    </div>

    <div class="container">
      <div class="row">
        <div class="col-md-4">
          <h2 id="ShepherdVelgProsjekt" class="ShepherdVelgProsjekt">Velg Prosjekt</h2>
          <asp:ListBox ID="ListBoxProsjekt" runat="server" Rows="1">
                <asp:ListItem Selected = "True" Text = "Velg Prosjekt" Value = "0"></asp:ListItem>
          </asp:ListBox>
          <asp:Button ID="btnVelgProsjekt" runat="server" Text="Velg Prosjekt" OnClick="btnVelgProsjekt_Click" />
        </div>
        <div class="col-md-4">
          <h2>Valgt prosjekt</h2>
            <h4><asp:Label ID="lblValgtProsjekt" runat="server" Text=""></asp:Label></h4>
        </div>
        <div class="col-md-4">
          <h2>Meny</h2>
          <p>Velg et element fra menyen øverst på websiden</p>
        </div>
      </div>
    </div>
    <br />    
    <br />
    <br />
    <br />
    <br />
    <script  src="../Scripts/MorildShepherdProsjektleder.js"></script>
</asp:Content>
