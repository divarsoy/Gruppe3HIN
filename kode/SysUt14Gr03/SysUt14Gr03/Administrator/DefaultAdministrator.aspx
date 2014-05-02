<%@ Page Title="" Language="C#" MasterPageFile="~/Site.SysAdm.Master" AutoEventWireup="true" CodeBehind="DefaultAdministrator.aspx.cs" Inherits="SysUt14Gr03.DefaultAdministrator" %>
<asp:Content ID="test" ContentPlaceHolderID="testSheep" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
      <div class="container">
        <span class="glyphicon glyphicon-plane"></span>  
        <h1>Administratorsiden</h1>
        <p>På denne websiden kan du registrere nye bruker og administrere disse. 
            <br />
           Nedenfor finner du en logg over alle registrerte hendelser på siden. Trykk på "Eksporter logg til excel" for å laste ned loggen.
        </p>
      </div>
    </div>

   <asp:HiddenField ID="SheperdBool" runat="server" />
    
    <div class="container">
      <div class="row">
        <div class="col-md-8">
          <h2>Loggføring av hendelser!</h2>
            <asp:Panel ID="Pan1" Height="500px" BackColor="#eeeeee" ScrollBars="Auto" runat="server">
             <div class="table table-condensed table-bordered table-striped">
          <asp:PlaceHolder ID="PlaceHolderTable"  runat="server"></asp:PlaceHolder>
                 </div>
                </asp:Panel>
            <br />
            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Eksporter logg til excel!" OnClick="Button1_Click" />
        </div>
        <div class="col-md-4">
          <h2>Valgt prosjekt</h2>
           
        </div>
       
      </div>
    </div>


    <div id="admDefault"></div>
</asp:Content>
