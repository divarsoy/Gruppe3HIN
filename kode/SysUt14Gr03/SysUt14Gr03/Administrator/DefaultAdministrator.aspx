<%@ Page Title="" Language="C#" MasterPageFile="~/Site.SysAdm.Master" AutoEventWireup="true" CodeBehind="DefaultAdministrator.aspx.cs" Inherits="SysUt14Gr03.DefaultAdministrator" %>

<asp:Content ID="test" ContentPlaceHolderID="testSheep" runat="server">
    <div class="jumbotron">
      <div class="container">
        <span class="glyphicon glyphicon-plane"></span>  
        <h1>Administratorsiden</h1>
        <p>På denne websiden kan du opprette prosjekter, team, brukere og oppgaver, samt administrere disse. Start med å velge et prosjekt nedenfor.</p>
      </div>
    </div>

    <div class="container">
      <div class="row">
        <div class="col-md-8">
          <h2>Rapport</h2>
             <div class="table">
          <asp:PlaceHolder ID="PlaceHolderTable"  runat="server"></asp:PlaceHolder>
                 </div>
            <asp:Button ID="Button1" runat="server" Text="Eksporter til excel!" OnClick="Button1_Click" />
        </div>
        <div class="col-md-4">
          <h2>Valgt prosjekt</h2>
           
        </div>
       
      </div>
    </div>


    <div id="admDefault"></div>
    <script src="/SysUt14Gr03/Scripts/MorildSheperdAdministrator.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
</asp:Content>
