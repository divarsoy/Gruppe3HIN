<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="HjelpProsjektleder.aspx.cs" Inherits="SysUt14Gr03.Prosjektleder.HjelpProsjektleder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="testSheep" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1>Hjelp</h1>
        <h4>Opprett bruker</h4>        
        <p class="col-md-10">For å opprette en bruker velges "Opprett Bruker" under "Bruker" i menyen. Fyll så ut rettighet, etternavn, fornavn og epost og trykk på "Opprett Bruker".
            Brukeren vil da bli registrert i systemet, og det vil gå ut en epost til brukeren med en aktiveringslenke for å fullføre registreringen.
        </p>
        <div class="col-md-6">
            <video controls="controls">
                <source runat="server" src="~/video/OpprettBrukerSomProsjektleder.mp4" type="video/mp4" />
            Your browser does not support the video tag.
            </video>
        </div>
        <div class="clearfix"></div>

        <h4>Opprett Prosjekt</h4>        
        <p class="col-md-10">For å opprette et prosjekt velges "Opprett Prosjekt" under "Prosjekt" i menyen. Fyll så ut navn på prosjektet, velg en prosjektleder, et team og når prosjektet skal starte og avslutes.
            Man kan også dele prosjektet opp i faser, og velge en faseleder or hver fase fra teammedlemmene.
        </p>
        <div class="col-md-6">
            <video controls="controls">
                <source runat="server" src="~/video/OpprettelseAvProsjekt.mp4" type="video/mp4" />
            Your browser does not support the video tag.
            </video>
        </div>
        <div class="clearfix"></div>

        <h4>Opprett Oppgave</h4>        
        <p class="col-md-10">For å opprette en oppgave må man først velge prosjekt på førstesiden. Deretter kan man velge "Opprett Oppgave i valgt prosjekt" under "Oppgaver" i menyen.
             Oppgave ID er det samme som oppgave IDen i kravspesifikasjonen. Tittel, beskrivelse og krav fylles også ut med hensyn til det som står i kravspesifikasjonen.
            Estimert tid og hvilken fase oppgaven skal gjøre i fylles også ut. Man kan tildele oppgaven til en eller flere brukere. 
        </p>
        <div class="col-md-6">
            <video controls="controls">
                <source runat="server" src="~/video/OpprettOppgaveSomProsjektleder.mp4" type="video/mp4" />
            Your browser does not support the video tag.
            </video>
        </div>
        <div class="clearfix"></div>

    </div>
</asp:Content>
