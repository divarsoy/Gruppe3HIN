<%@ Page Title="" Language="C#" MasterPageFile="~/Site.SysAdm.master" AutoEventWireup="true" CodeBehind="HjelpAdmin.aspx.cs" Inherits="SysUt14Gr03.Administrator.adminHjelp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="testSheep" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1>Hjelp</h1>
        <h4>Opprett bruker</h4>
        
        <p class="col-md-10">For å opprette en bruker velges "Opprett Bruker" i menyen. Fyll så ut rettighet, etternavn, fornavn og epost og trykk på "Opprett Bruker".
            Brukeren vil da bli registrert i systemet, og det vil gå ut en epost til brukeren med en aktiveringslenke for å fullføre registreringen.
        </p>
        <div class="col-md-6">
            <video controls="controls">
                <source runat="server" src="~/video/OpprettBrukerSomAdministrator.mp4" type="video/mp4" />
            Your browser does not support the video tag.
            </video>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
