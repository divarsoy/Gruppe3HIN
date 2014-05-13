using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Classes
{
    public class Tabeller
    {
        public static Table HentOppgaveTabell (List<Oppgave> query)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell idHeaderCell = new TableHeaderCell();
            TableHeaderCell tittelHeaderCell = new TableHeaderCell();
            TableHeaderCell statusHeaderCell = new TableHeaderCell();
            TableHeaderCell estimatHeaderCell = new TableHeaderCell();
            TableHeaderCell bruktTidHeaderCell = new TableHeaderCell();
            TableHeaderCell remainingTimeHeaderCell = new TableHeaderCell();
            TableHeaderCell brukerHeaderCell = new TableHeaderCell();
            TableHeaderCell kommentarerHeaderCell = new TableHeaderCell();
            TableHeaderCell redigerHeaderCell = new TableHeaderCell();
            TableHeaderCell sumHeaderCell = new TableHeaderCell();


            idHeaderCell.Text = "Id";
            tittelHeaderCell.Text = "Tittel";
            statusHeaderCell.Text = "Status";
            estimatHeaderCell.Text = "Estimat";
            bruktTidHeaderCell.Text = "Brukt tid";
            remainingTimeHeaderCell.Text = "Gjenstående tid";
            brukerHeaderCell.Text = "Brukere";
            kommentarerHeaderCell.Text = "Kommentarer";
            redigerHeaderCell.Text = "Rediger";

            headerRow.Cells.Add(idHeaderCell);
            headerRow.Cells.Add(tittelHeaderCell);
            headerRow.Cells.Add(statusHeaderCell);
            headerRow.Cells.Add(estimatHeaderCell);
            headerRow.Cells.Add(bruktTidHeaderCell);
            headerRow.Cells.Add(remainingTimeHeaderCell);
            headerRow.Cells.Add(brukerHeaderCell);
            headerRow.Cells.Add(kommentarerHeaderCell);
            headerRow.Cells.Add(redigerHeaderCell);
            tabell.Rows.Add(headerRow);

            foreach (Oppgave oppgave in query)
            {
                StringBuilder brukereIOppgave = new StringBuilder();
                foreach (Bruker bruker in oppgave.Brukere)
                {
                    brukereIOppgave.Append(String.Format("<a href='VisBruker?bruker_id={0}'>{1} </a>", bruker.Bruker_id, bruker.Brukernavn));
                }
                TableRow tRow = new TableRow();
                TableCell idCell = new TableCell();
                TableCell tittelCell = new TableCell();
                TableCell statusCell = new TableCell();
                TableCell estimatCell = new TableCell();
                TableCell bruktTidCell = new TableCell();
                TableCell remainingCell = new TableCell();
                TableCell brukerCell = new TableCell();
                TableCell kommentarCell = new TableCell();
                TableCell redigerCell = new TableCell();

                HttpContext http = HttpContext.Current;
                if (SessionSjekk.IsFaseleder() || Validator.SjekkRettighet(Validator.KonverterTilTall(http.Session["bruker_id"].ToString()), Konstanter.rettighet.Prosjektleder))
                {
                    string oppgaveLink = redigerCell.ResolveUrl("~/AdministrasjonAvOppgave?oppgave_id=" + oppgave.Oppgave_id.ToString());
                    redigerCell.Text = string.Format("<a href='{0}'>{1}</a>", oppgaveLink, "Rediger oppgave");
                }
                else
                {
                    redigerHeaderCell.Visible = false;
                    redigerCell.Visible = false;
                }
                string linkOppgave = idCell.ResolveUrl("~/VisOppgave?oppgave_id=" + oppgave.Oppgave_id.ToString());
                idCell.Text = string.Format("<a href='{0}'>{1}</a>", linkOppgave, oppgave.RefOppgaveId.ToString());
                tittelCell.Text = string.Format("<a href='{0}'>{1}</a>", linkOppgave, oppgave.Tittel.ToString());
                statusCell.Text = Queries.GetStatus(oppgave.Status_id).Navn;
                estimatCell.Text = oppgave.Estimat.ToString();
                bruktTidCell.Text = oppgave.BruktTid.ToString();
                remainingCell.Text = oppgave.RemainingTime.ToString();
                brukerCell.Text = brukereIOppgave.ToString();
                kommentarCell.Text = oppgave.Kommentarer.Count.ToString();

          

                tRow.Cells.Add(idCell);
                tRow.Cells.Add(tittelCell);
                tRow.Cells.Add(statusCell);
                tRow.Cells.Add(estimatCell);
                tRow.Cells.Add(bruktTidCell);
                tRow.Cells.Add(remainingCell);
                tRow.Cells.Add(brukerCell);
                tRow.Cells.Add(kommentarCell);
                tRow.Cells.Add(redigerCell);

                tabell.Rows.Add(tRow);
            }
            return tabell;
        }

        public static Table HentProsjekterTabell(List<Prosjekt> query)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell navnHeaderCell = new TableHeaderCell();
            TableHeaderCell startDatoHeaderCell = new TableHeaderCell();
            TableHeaderCell sluttDatoHeaderCell = new TableHeaderCell();
            TableHeaderCell teamHeaderCell = new TableHeaderCell();
            TableHeaderCell prosjektlederHeaderCell = new TableHeaderCell();

            navnHeaderCell.Text = "Navn";
            startDatoHeaderCell.Text = "Start Dato";
            sluttDatoHeaderCell.Text = "Slutt Dato";
            teamHeaderCell.Text = "Team";
            prosjektlederHeaderCell.Text = "Prosjektleder";

            headerRow.Cells.Add(navnHeaderCell);
            headerRow.Cells.Add(startDatoHeaderCell);
            headerRow.Cells.Add(sluttDatoHeaderCell);
            headerRow.Cells.Add(teamHeaderCell);
            headerRow.Cells.Add(prosjektlederHeaderCell);
            tabell.Rows.Add(headerRow);

            foreach (Prosjekt prosjekt in query)
            {

                TableRow tRow = new TableRow();
                TableCell navnCell = new TableCell();
                TableCell startDatoCell = new TableCell();
                TableCell sluttDatoCell = new TableCell();
                TableCell teamCell = new TableCell();
                TableCell prosjektlederCell = new TableCell();

                Team team = Queries.GetTeam((int)prosjekt.Team_id);
                Bruker bruker = Queries.GetBruker(prosjekt.Bruker_id);

                string prosjektLink = navnCell.ResolveUrl("~/VisProsjekt?prosjekt_id=" + prosjekt.Prosjekt_id.ToString());
                string teamLink = navnCell.ResolveUrl("~/VisTeam?team_id=" + team.Team_id.ToString());
                string brukerLink = navnCell.ResolveUrl("~/VisBruker?bruker_id=" + bruker.Bruker_id.ToString());

                navnCell.Text = String.Format("<a href='{0}'>{1}</a>", prosjektLink, prosjekt.Navn);
                startDatoCell.Text = String.Format("{0:dd/MM/yyyy}", prosjekt.StartDato);
                sluttDatoCell.Text = String.Format("{0:dd/MM/yyyy}", prosjekt.SluttDato);
                teamCell.Text = String.Format("<a href='{0}'>{1}</a>", teamLink, team.Navn);
                prosjektlederCell.Text = String.Format("<a href='{0}'>{1} </a>", brukerLink, bruker.ToString());

                tRow.Cells.Add(navnCell);
                tRow.Cells.Add(startDatoCell);
                tRow.Cells.Add(sluttDatoCell);
                tRow.Cells.Add(teamCell);
                tRow.Cells.Add(prosjektlederCell);

                tabell.Rows.Add(tRow);
            }
            tabell.CssClass = "table";

            return tabell;

        }

        public static Table HentBrukerTabellForTeam(List<Bruker> query, Team nesteTeam, int prosjekt_id)
        {
            Table tabell = new Table();
          
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell teamNavnCell = new TableHeaderCell();
            TableHeaderCell forNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell etterNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell brukerNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell epostHeaderCell = new TableHeaderCell();
            TableHeaderCell IMHeaderCell = new TableHeaderCell();
            TableHeaderCell rolleHeaderCell = new TableHeaderCell();

         
            
            forNavnHeaderCell.Text = "Fornavn";
            etterNavnHeaderCell.Text = "Etternavn";
            brukerNavnHeaderCell.Text = "Brukernavn";
            epostHeaderCell.Text = "Epost";
            IMHeaderCell.Text = "IM";
            rolleHeaderCell.Text = "Rolle";

            headerRow.Cells.Add(forNavnHeaderCell);
            headerRow.Cells.Add(etterNavnHeaderCell);
            headerRow.Cells.Add(brukerNavnHeaderCell);
            headerRow.Cells.Add(epostHeaderCell);
            headerRow.Cells.Add(IMHeaderCell);
            headerRow.Cells.Add(rolleHeaderCell);
            tabell.Rows.Add(headerRow);

            foreach (Bruker bruker in query)
            {
                TableRow tRow = new TableRow();
                TableCell forNavnCell = new TableCell();
                TableCell etterNavnCell = new TableCell();
                TableCell brukerNavnCell = new TableCell();
                TableCell epostCell = new TableCell();
                TableCell IMCell = new TableCell();
                TableCell rolleCell = new TableCell();

                StringBuilder brukereITeam = new StringBuilder();

                forNavnCell.Text = bruker.Fornavn;
                etterNavnCell.Text = bruker.Etternavn;
                brukerNavnCell.Text = String.Format("<a href='/VisBruker?bruker_id={0}'>{1}</a>", bruker.Bruker_id, bruker.Brukernavn);
                epostCell.Text = bruker.Epost;
                IMCell.Text = bruker.IM;

                string rettighet = Queries.GetRettighet(bruker.Bruker_id).RettighetNavn;

                if (SessionSjekk.IsFaseleder(bruker.Bruker_id, prosjekt_id))
                    rolleCell.Text = "Faseleder";
                else
                    rolleCell.Text = rettighet;

                tRow.Cells.Add(forNavnCell);
                tRow.Cells.Add(etterNavnCell);
                tRow.Cells.Add(brukerNavnCell);
                tRow.Cells.Add(epostCell);
                tRow.Cells.Add(IMCell);
                tRow.Cells.Add(rolleCell);
               
                tabell.Rows.Add(tRow);
            }

            //tabell.CssClass = "table";
            return tabell;
        }

        public static Table HentBrukerTabellIProsjektTeamUtviklere(List<Bruker> query)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell forNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell etterNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell brukerNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell epostHeaderCell = new TableHeaderCell();
            TableHeaderCell IMHeaderCell = new TableHeaderCell();
            TableHeaderCell teamHeaderCell = new TableHeaderCell();
            TableHeaderCell prosjektHeaderCell = new TableHeaderCell();

            forNavnHeaderCell.Text = "Fornavn";
            etterNavnHeaderCell.Text = "Etternavn";
            brukerNavnHeaderCell.Text = "Brukernavn";
            epostHeaderCell.Text = "Epost";
            IMHeaderCell.Text = "IM";
            teamHeaderCell.Text = "Teams";
            prosjektHeaderCell.Text = "Prosjekter";

            headerRow.Cells.Add(forNavnHeaderCell);
            headerRow.Cells.Add(etterNavnHeaderCell);
            headerRow.Cells.Add(brukerNavnHeaderCell);
            headerRow.Cells.Add(epostHeaderCell);
            headerRow.Cells.Add(IMHeaderCell);
            headerRow.Cells.Add(teamHeaderCell);
            headerRow.Cells.Add(prosjektHeaderCell);
            tabell.Rows.Add(headerRow);

            foreach (Bruker bruker in query)
            {
                TableRow tRow = new TableRow();
                TableCell forNavnCell = new TableCell();
                TableCell etterNavnCell = new TableCell();
                TableCell brukerNavnCell = new TableCell();
                TableCell epostCell = new TableCell();
                TableCell IMCell = new TableCell();
                TableCell teamsCell = new TableCell();
                TableCell prosjekterCell = new TableCell();
                
                foreach (Prosjekt prosjekt in bruker.Prosjekter)
                    prosjekterCell.Text = prosjekt.Navn;
                foreach (Team team in bruker.Teams)
                    teamsCell.Text = team.Navn;

                forNavnCell.Text = String.Format("<a href='visBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Fornavn);               
                etterNavnCell.Text = String.Format("<a href='visBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Etternavn);
                brukerNavnCell.Text = String.Format("<a href='visBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Brukernavn);
                epostCell.Text = bruker.Epost;
                IMCell.Text = bruker.IM;

                tRow.Cells.Add(forNavnCell);
                tRow.Cells.Add(etterNavnCell);
                tRow.Cells.Add(brukerNavnCell);
                tRow.Cells.Add(epostCell);
                tRow.Cells.Add(IMCell);
                tRow.Cells.Add(teamsCell);
                tRow.Cells.Add(prosjekterCell);

                tabell.Rows.Add(tRow);
            }
            return tabell;
        }
        public static Table HentBrukerTabellIProsjektTeamProsjektLeder(List<Bruker> query, List<Prosjekt> queryProsjekt, List<Team> queryTeam)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell forNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell etterNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell brukerNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell epostHeaderCell = new TableHeaderCell();
            TableHeaderCell IMHeaderCell = new TableHeaderCell();
            TableHeaderCell teamHeaderCell = new TableHeaderCell();
            TableHeaderCell prosjektHeaderCell = new TableHeaderCell();
            TableHeaderCell rolleCell = new TableHeaderCell();
            TableHeaderCell endreCell = new TableHeaderCell();

            forNavnHeaderCell.Text = "Fornavn";
            etterNavnHeaderCell.Text = " Etternavn";
            brukerNavnHeaderCell.Text = " Brukernavn";
            epostHeaderCell.Text = " Epost";
            IMHeaderCell.Text = " IM";
            teamHeaderCell.Text = " Team";
            prosjektHeaderCell.Text = " Prosjekter";
            rolleCell.Text = "Rolle";
            endreCell.Text = "Rediger bruker";
           
            headerRow.Cells.Add(forNavnHeaderCell);
            headerRow.Cells.Add(etterNavnHeaderCell);
            headerRow.Cells.Add(brukerNavnHeaderCell);
            headerRow.Cells.Add(epostHeaderCell);
            headerRow.Cells.Add(IMHeaderCell);
            headerRow.Cells.Add(teamHeaderCell);
            headerRow.Cells.Add(prosjektHeaderCell);
            headerRow.Cells.Add(rolleCell);
            headerRow.Cells.Add(endreCell);
            tabell.Rows.Add(headerRow);

            foreach (Bruker bruker in query)
            {
                TableRow tRow = new TableRow();
                TableCell forNavnCell = new TableCell();
                TableCell etterNavnCell = new TableCell();
                TableCell brukerNavnCell = new TableCell();
                TableCell epostCell = new TableCell();
                TableCell IMCell = new TableCell();
                TableCell teamsCell = new TableCell();
                TableCell prosjekterCell = new TableCell();
                TableCell rolleCelle = new TableCell();
                TableCell endreCelle = new TableCell();

                foreach (Team team in queryTeam)
                {
                    teamsCell.Text = String.Format("<a href='AdministrasjonAvTeamBrukere?team_id={0}'>{1} </a>", team.Team_id, team.Navn);
                }
                foreach (Prosjekt prosjekt in queryProsjekt)
                {
                    prosjekterCell.Text = String.Format("<a href='AdministrasjonAvProsjekt?prosjekt_id={0}'>{1} </a>", prosjekt.Prosjekt_id, prosjekt.Navn);

                    foreach (Rettighet rett in bruker.Rettigheter)
                    {

                        if (SessionSjekk.IsFaseleder(bruker.Bruker_id, prosjekt.Prosjekt_id).Equals(true))
                        {
                            rolleCelle.Text = "Faseleder";
                        }
                        else
                        {
                            rolleCelle.Text = String.Format(rett.RettighetNavn);
                        }
                    }
                }
                forNavnCell.Text = String.Format("<a href='visBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Fornavn);
                etterNavnCell.Text = String.Format("<a href='visBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Etternavn);
                brukerNavnCell.Text = String.Format("<a href='visBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Brukernavn);
                epostCell.Text = String.Format(bruker.Epost);
                IMCell.Text = String.Format(bruker.IM);
                endreCelle.Text = String.Format("<a href='EndreBrukerinformasjon?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), "Rediger bruker");
                    
                tRow.Cells.Add(forNavnCell);
                tRow.Cells.Add(etterNavnCell);
                tRow.Cells.Add(brukerNavnCell);
                tRow.Cells.Add(epostCell);
                tRow.Cells.Add(IMCell);
                tRow.Cells.Add(teamsCell);
                tRow.Cells.Add(prosjekterCell);
                tRow.Cells.Add(rolleCelle);
                tRow.Cells.Add(endreCelle);
                tabell.Rows.Add(tRow);
            }
            return tabell;
        }
        public static Table HentProsjekterTabellProsjektLeder(List<Prosjekt> query)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell navnHeaderCell = new TableHeaderCell();
            TableHeaderCell startDatoHeaderCell = new TableHeaderCell();
            TableHeaderCell sluttDatoHeaderCell = new TableHeaderCell();
            TableHeaderCell teamHeaderCell = new TableHeaderCell();
            TableHeaderCell brukereHeaderCell = new TableHeaderCell();
            TableHeaderCell opprettetHeaderCell = new TableHeaderCell();
            TableHeaderCell aktivHeaderCell = new TableHeaderCell();

            navnHeaderCell.Text = "Navn";
            startDatoHeaderCell.Text = "Start Dato";
            sluttDatoHeaderCell.Text = "Slutt Dato";
            teamHeaderCell.Text = "Team";
            opprettetHeaderCell.Text = "Opprettet";
            aktivHeaderCell.Text = "Aktiv";

            headerRow.Cells.Add(navnHeaderCell);
            headerRow.Cells.Add(startDatoHeaderCell);
            headerRow.Cells.Add(sluttDatoHeaderCell);
            headerRow.Cells.Add(teamHeaderCell);
            headerRow.Cells.Add(opprettetHeaderCell);
            headerRow.Cells.Add(aktivHeaderCell);
            tabell.Rows.Add(headerRow);
          
            foreach (Prosjekt prosjekt in query)
            {

                TableRow tRow = new TableRow();
                TableCell navnCell = new TableCell();
                TableCell startDatoCell = new TableCell();
                TableCell sluttDatoCell = new TableCell();
                TableCell teamCell = new TableCell();
                TableCell opprettetCell = new TableCell();
                TableCell aktivCell = new TableCell();

                Team team = Queries.GetTeam((int)prosjekt.Team_id);
                Bruker bruker = Queries.GetBruker(prosjekt.Bruker_id);


                navnCell.Text = String.Format("<a href='AdministrasjonAvProsjekt?Prosjekt_id={0}'>{1}</a>", prosjekt.Prosjekt_id.ToString(), prosjekt.Navn);
                startDatoCell.Text = String.Format("{0:dd/MM/yyyy}", prosjekt.StartDato);
                sluttDatoCell.Text = String.Format("{0:dd/MM/yyyy}", prosjekt.SluttDato);
                teamCell.Text = String.Format("<a href='AdministrasjonAvTeam?Team_id={0}'>{1}</a>", team.Team_id.ToString(), team.Navn);
                opprettetCell.Text = String.Format("{0:dd/MM/yyyy}", prosjekt.Opprettet);

                if (prosjekt.Aktiv == true)
                {
                    aktivCell.Text = String.Format("Ja");
                }
                else
                {
                    aktivCell.Text = String.Format("Nei");
                }

                tRow.Cells.Add(navnCell);
                tRow.Cells.Add(startDatoCell);
                tRow.Cells.Add(sluttDatoCell);
                tRow.Cells.Add(teamCell);
                tRow.Cells.Add(opprettetCell);
                tRow.Cells.Add(aktivCell);
                tabell.Rows.Add(tRow);
            }
            tabell.CssClass = "table";

            return tabell;
        }

        static public Table HentBrukereTabellForAdministrator(List<Bruker> query)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell forNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell etterNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell brukerNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell epostHeaderCell = new TableHeaderCell();
            TableHeaderCell IMHeaderCell = new TableHeaderCell();
            TableHeaderCell aktivHeaderCell = new TableHeaderCell();

            forNavnHeaderCell.Text = "Fornavn";
            etterNavnHeaderCell.Text = "Etternavn";
            brukerNavnHeaderCell.Text = "Brukernavn";
            epostHeaderCell.Text = "Epost";
            IMHeaderCell.Text = "IM";
            aktivHeaderCell.Text = "Aktiv";

            headerRow.Cells.Add(forNavnHeaderCell);
            headerRow.Cells.Add(etterNavnHeaderCell);
            headerRow.Cells.Add(brukerNavnHeaderCell);
            headerRow.Cells.Add(epostHeaderCell);
            headerRow.Cells.Add(IMHeaderCell);
            headerRow.Cells.Add(aktivHeaderCell);
            tabell.Rows.Add(headerRow);

            foreach (Bruker bruker in query)
            {
                TableRow tRow = new TableRow();
                TableCell forNavnCell = new TableCell();
                TableCell etterNavnCell = new TableCell();
                TableCell brukerNavnCell = new TableCell();
                TableCell epostCell = new TableCell();
                TableCell IMCell = new TableCell();
                TableCell aktivCell = new TableCell();
                                
                forNavnCell.Text = String.Format("<a href='EndreBrukerinformasjonSomAdministrator?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Fornavn);
                etterNavnCell.Text = String.Format("<a href='EndreBrukerinformasjonSomAdministrator?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Etternavn);
                brukerNavnCell.Text = String.Format("<a href='EndreBrukerinformasjonSomAdministrator?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Brukernavn);
                epostCell.Text = String.Format(bruker.Epost);
                IMCell.Text = String.Format(bruker.IM);
                if (bruker.Aktiv)
                    aktivCell.Text = "Ja";
                else
                    aktivCell.Text = "Nei";

                tRow.Cells.Add(forNavnCell);
                tRow.Cells.Add(etterNavnCell);
                tRow.Cells.Add(brukerNavnCell);
                tRow.Cells.Add(epostCell);
                tRow.Cells.Add(IMCell);
                tRow.Cells.Add(aktivCell);
                tabell.Rows.Add(tRow);
            }
            tabell.CssClass = "table";
            return tabell;

        }
        public static Table HentFaseTabell(Fase fase)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();


            TableHeaderCell oppgaveIDHeaderCell = new TableHeaderCell();
            TableHeaderCell oppgaveHeaderCell = new TableHeaderCell();
            TableHeaderCell estimertHeaderCell = new TableHeaderCell();
            TableHeaderCell bruktHeaderCell = new TableHeaderCell();
            TableHeaderCell gjenstaendeHeaderCell = new TableHeaderCell();
            TableHeaderCell ansvarligHeaderCell = new TableHeaderCell();
            TableHeaderCell statusHeaderCell = new TableHeaderCell();

            oppgaveIDHeaderCell.Text = "Oppgave ID";
            oppgaveHeaderCell.Text = "Oppgavenavn";
            estimertHeaderCell.Text = "Estimert tid";
            bruktHeaderCell.Text = "Brukt tid";
            gjenstaendeHeaderCell.Text = "Gjenstående tid";
            ansvarligHeaderCell.Text = "Ansvarlig Bruker";
            statusHeaderCell.Text = "Status";


            headerRow.Cells.Add(oppgaveIDHeaderCell);
            headerRow.Cells.Add(oppgaveHeaderCell);
            headerRow.Cells.Add(estimertHeaderCell);
            headerRow.Cells.Add(bruktHeaderCell);
            headerRow.Cells.Add(gjenstaendeHeaderCell);
            headerRow.Cells.Add(ansvarligHeaderCell);
            headerRow.Cells.Add(statusHeaderCell);
            tabell.Rows.Add(headerRow);

            TimeSpan sumEst = new TimeSpan(0, 0, 0);
            TimeSpan sumBrukt = new TimeSpan(0, 0, 0);
            TimeSpan sumGjenstaende = new TimeSpan(0, 0, 0);

            TableRow sumRow = new TableRow();
            TableCell sumEstimertCell = new TableCell();
            TableCell sumBruktCell = new TableCell();
            TableCell sumGjenstaendeCell = new TableCell();

            foreach (Oppgave o in fase.Oppgaver)
            {
                TableRow faseRow = new TableRow();
               
                TableCell oppgaveIdCell = new TableCell();
                TableCell oppgaveCell = new TableCell();
                TableCell statusCell = new TableCell();
                TableCell estimertCell = new TableCell();
                TableCell gjenstaendeCell = new TableCell();
                TableCell bruktCell = new TableCell();
                TableCell ansvarligCell = new TableCell();
                

               
                oppgaveIdCell.Text = o.RefOppgaveId;
                oppgaveCell.Text = o.Tittel;
                estimertCell.Text = Convert.ToString(o.Estimat);
                gjenstaendeCell.Text = Convert.ToString(o.RemainingTime);
                bruktCell.Text = Convert.ToString(o.BruktTid);
                ansvarligCell.Text = fase.Bruker.ToString();
                string status = Queries.GetStatus(o.Status_id).Navn;
                statusCell.Text = status;
                sumEst += (TimeSpan)o.Estimat;
                sumBrukt += (TimeSpan)o.BruktTid;
                sumGjenstaende += (TimeSpan)o.RemainingTime;

                faseRow.Cells.Add(oppgaveIdCell);
                faseRow.Cells.Add(oppgaveCell);
                faseRow.Cells.Add(estimertCell);
                faseRow.Cells.Add(bruktCell);         
                faseRow.Cells.Add(gjenstaendeCell);     
                faseRow.Cells.Add(ansvarligCell);
                faseRow.Cells.Add(statusCell);
                
                tabell.Rows.Add(faseRow);
                
            }
            sumEstimertCell.Text = "Sum Estimert tid: " + Convert.ToString(sumEst);
            sumBruktCell.Text = "Sum Brukt tid: " + Convert.ToString(sumBrukt);
            TimeSpan remainingTime = new TimeSpan(0);
            remainingTime = sumEst - sumBrukt;
            string sumGjen = Convert.ToString(sumGjenstaende);
            sumGjenstaendeCell.Text = "Sum Gjenstående tid: " + (sumGjenstaende == null ? sumGjen : Convert.ToString(remainingTime));
            sumRow.Cells.Add(sumEstimertCell);
            sumRow.Cells.Add(sumBruktCell);
            sumRow.Cells.Add(sumGjenstaendeCell);
            tabell.Rows.Add(sumRow);
            tabell.CssClass = "Table";
            return tabell;
        }
        public static Table HentProsjektTabell(Prosjekt prosjekt)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();


            TableHeaderCell oppgaveIDHeaderCell = new TableHeaderCell();
            TableHeaderCell oppgaveHeaderCell = new TableHeaderCell();
            TableHeaderCell faseHeaderCell = new TableHeaderCell();
            TableHeaderCell statusHeaderCell = new TableHeaderCell();

            oppgaveIDHeaderCell.Text = "Oppgave ID";
            oppgaveHeaderCell.Text = "Oppgavenavn";
            faseHeaderCell.Text = "Fase";
            statusHeaderCell.Text = "Status";


            headerRow.Cells.Add(oppgaveIDHeaderCell);
            headerRow.Cells.Add(oppgaveHeaderCell);
            headerRow.Cells.Add(faseHeaderCell);
            headerRow.Cells.Add(statusHeaderCell);
            tabell.Rows.Add(headerRow);

            TimeSpan sumEst = new TimeSpan(0, 0, 0);
            TimeSpan sumBrukt = new TimeSpan(0, 0, 0);
            TimeSpan sumGjenstaende = new TimeSpan(0, 0, 0);

            TableRow sumRow = new TableRow();
            TableCell sumEstimertCell = new TableCell();
            TableCell sumBruktCell = new TableCell();
            TableCell sumGjenstaendeCell = new TableCell();

            foreach (Oppgave o in prosjekt.Oppgaver)
            {
                TableRow faseRow = new TableRow();

                TableCell oppgaveIdCell = new TableCell();
                TableCell oppgaveCell = new TableCell();
                TableCell statusCell = new TableCell();
                TableCell faseCell = new TableCell();

                oppgaveIdCell.Text = o.RefOppgaveId;
                oppgaveCell.Text = String.Format("<a href='VisOppgave?oppgave_id={0}'>{1}</a>", o.Oppgave_id, o.Tittel);
                string status = Queries.GetStatus(o.Status_id).Navn;
                statusCell.Text = status;
                faseCell.Text = String.Format("<a href='visFase?fase_id={0}'>{1}</a>", o.Fase_id, o.Fase.Navn);



                faseRow.Cells.Add(oppgaveIdCell);
                faseRow.Cells.Add(oppgaveCell);
                faseRow.Cells.Add(faseCell);         
                faseRow.Cells.Add(statusCell);

                tabell.Rows.Add(faseRow);

            }
            return tabell;
        }
        public static Table HentTimerForBruker(List<Time> time_list, int bruker_id, Prosjekt p)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell headerCell = new TableHeaderCell();

            TableHeaderRow timeHeaderRow = new TableHeaderRow();
            TableHeaderCell oppgaveIdHeaderCell = new TableHeaderCell();
            TableHeaderCell oppgaveHeaderCell = new TableHeaderCell();
            TableHeaderCell opprettetHeaderCell = new TableHeaderCell();
            TableHeaderCell startHeaderCell = new TableHeaderCell();
            TableHeaderCell stoppHeaderCell = new TableHeaderCell();
            TableHeaderCell tidHeaderCell = new TableHeaderCell();
            TableHeaderCell manuellHeaderCell = new TableHeaderCell();

            headerCell.Text = p.Navn.ToString();
            oppgaveIdHeaderCell.Text = "Oppgave ID";
            oppgaveHeaderCell.Text = "Oppgave";
            opprettetHeaderCell.Text = "Opprettet";
            startHeaderCell.Text = "Start";
            stoppHeaderCell.Text = "Stopp";
            tidHeaderCell.Text = "Brukt tid";
            manuellHeaderCell.Text = "Registrert Manuelt";

            timeHeaderRow.Cells.Add(oppgaveIdHeaderCell);
            timeHeaderRow.Cells.Add(oppgaveHeaderCell);
            timeHeaderRow.Cells.Add(opprettetHeaderCell);
            timeHeaderRow.Cells.Add(startHeaderCell);
            timeHeaderRow.Cells.Add(stoppHeaderCell);
            timeHeaderRow.Cells.Add(tidHeaderCell);
            timeHeaderRow.Cells.Add(manuellHeaderCell);
            headerRow.Cells.Add(headerCell);
            tabell.Rows.Add(headerRow);
            tabell.Rows.Add(timeHeaderRow);

            foreach (Time t in time_list)
            {
                TableRow timeRow = new TableRow();
                TableCell oppgaveIdCell = new TableCell();
                TableCell oppgaveCell = new TableCell();
                TableCell opprettetCell = new TableCell();
                TableCell startCell = new TableCell();
                TableCell stoppCell = new TableCell();
                TableCell tidCell = new TableCell();
                TableCell manuellCell = new TableCell();

                oppgaveIdCell.Text = String.Format(t.Oppgave_id.ToString());
                //oppgaveCell.Text = String.Format("<a href='VisOppgave?oppgave_id={0}'>{1}</a>", t.Oppgave_id.ToString(), t.Oppgave.ToString());
                oppgaveCell.Text = Queries.GetOppgaveMedTimer(t.Time_id).Tittel.ToString();
                //oppgaveCell.Text = "Oppgave";
                opprettetCell.Text = String.Format(t.Opprettet.ToString());
                startCell.Text = String.Format(t.Start.ToString());
                stoppCell.Text = String.Format(t.Stopp.ToString());
                tidCell.Text = String.Format(t.Tid.ToString());
                if (t.Manuell)
                    manuellCell.Text = "Ja";
                else
                    manuellCell.Text = "Nei";

                timeRow.Cells.Add(oppgaveIdCell);
                timeRow.Cells.Add(oppgaveCell);
                timeRow.Cells.Add(opprettetCell);
                timeRow.Cells.Add(startCell);
                timeRow.Cells.Add(stoppCell);
                timeRow.Cells.Add(tidCell);
                timeRow.Cells.Add(manuellCell);
                tabell.Rows.Add(timeRow);

            }
            tabell.CssClass = "Table";
            return tabell;
        }

        public static Table HentTimerForProsjektleder(List<Time> time_list, Bruker bruker, Prosjekt p)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell headerCell = new TableHeaderCell();

            TableHeaderRow timeHeaderRow = new TableHeaderRow();
            TableHeaderCell oppgaveIdHeaderCell = new TableHeaderCell();
            TableHeaderCell oppgaveHeaderCell = new TableHeaderCell();
            TableHeaderCell opprettetHeaderCell = new TableHeaderCell();
            TableHeaderCell startHeaderCell = new TableHeaderCell();
            TableHeaderCell stoppHeaderCell = new TableHeaderCell();
            TableHeaderCell tidHeaderCell = new TableHeaderCell();
            TableHeaderCell manuellHeaderCell = new TableHeaderCell();

            headerCell.Text = bruker.Brukernavn.ToString();
            oppgaveIdHeaderCell.Text = "Oppgave ID";
            oppgaveHeaderCell.Text = "Oppgave";
            opprettetHeaderCell.Text = "Opprettet";
            startHeaderCell.Text = "Start";
            stoppHeaderCell.Text = "Stopp";
            tidHeaderCell.Text = "Brukt tid";
            manuellHeaderCell.Text = "Registrert Manuelt";

            timeHeaderRow.Cells.Add(oppgaveIdHeaderCell);
            timeHeaderRow.Cells.Add(oppgaveHeaderCell);
            timeHeaderRow.Cells.Add(opprettetHeaderCell);
            timeHeaderRow.Cells.Add(startHeaderCell);
            timeHeaderRow.Cells.Add(stoppHeaderCell);
            timeHeaderRow.Cells.Add(tidHeaderCell);
            timeHeaderRow.Cells.Add(manuellHeaderCell);
            headerRow.Cells.Add(headerCell);
            tabell.Rows.Add(headerRow);
            tabell.Rows.Add(timeHeaderRow);

            foreach (Time t in time_list)
            {
                TableRow timeRow = new TableRow();
                TableCell oppgaveIdCell = new TableCell();
                TableCell oppgaveCell = new TableCell();
                TableCell opprettetCell = new TableCell();
                TableCell startCell = new TableCell();
                TableCell stoppCell = new TableCell();
                TableCell tidCell = new TableCell();
                TableCell manuellCell = new TableCell();

                oppgaveIdCell.Text = String.Format(t.Oppgave_id.ToString());
                //oppgaveCell.Text = String.Format("<a href='VisOppgave?oppgave_id={0}'>{1}</a>", t.Oppgave_id.ToString(), t.Oppgave.ToString());
                oppgaveCell.Text = Queries.GetOppgaveMedTimer(t.Time_id).Tittel.ToString();
                //oppgaveCell.Text = "Oppgave";
                opprettetCell.Text = String.Format(t.Opprettet.ToString());
                startCell.Text = String.Format(t.Start.ToString());
                stoppCell.Text = String.Format(t.Stopp.ToString());
                tidCell.Text = String.Format(t.Tid.ToString());
                if (t.Manuell) {
                    manuellCell.Text = "Ja";
                    timeRow.BackColor = Color.DarkGray;
                }

                else
                {
                    manuellCell.Text = "Nei";
                    timeRow.BackColor = Color.PaleGreen;
                }


                timeRow.Cells.Add(oppgaveIdCell);
                timeRow.Cells.Add(oppgaveCell);
                timeRow.Cells.Add(opprettetCell);
                timeRow.Cells.Add(startCell);
                timeRow.Cells.Add(stoppCell);
                timeRow.Cells.Add(tidCell);
                timeRow.Cells.Add(manuellCell);
                tabell.Rows.Add(timeRow);

            }
            tabell.CssClass = "Table";
            return tabell;
        }

        public static Table hentLoggForAdministrator(List<Logg> loggLister)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell headerCell1 = new TableHeaderCell();
            TableHeaderRow innholdHeaderRow = new TableHeaderRow();
            TableHeaderCell loggIdHeaderCell = new TableHeaderCell();
            TableHeaderCell hendelseHeaderCell = new TableHeaderCell();
            TableHeaderCell brukerIdHeaderCell = new TableHeaderCell();
            TableHeaderCell prosjektIdHeaderCell = new TableHeaderCell();
            TableHeaderCell datoHeaderCell = new TableHeaderCell();

            headerCell1.Text = "Logg";
            loggIdHeaderCell.Text = "Logg ID";
            hendelseHeaderCell.Text = "Hendelse";
            brukerIdHeaderCell.Text = "Bruker";
            prosjektIdHeaderCell.Text = "Prosjekt";
            datoHeaderCell.Text = "Dato";

            headerRow.Cells.Add(headerCell1);
            tabell.Rows.Add(headerRow);
            innholdHeaderRow.Cells.Add(loggIdHeaderCell);
            innholdHeaderRow.Cells.Add(hendelseHeaderCell);
            innholdHeaderRow.Cells.Add(brukerIdHeaderCell);
            innholdHeaderRow.Cells.Add(prosjektIdHeaderCell);
            innholdHeaderRow.Cells.Add(datoHeaderCell);
            tabell.Rows.Add(innholdHeaderRow);

            foreach (Logg logg in loggLister)
            {
                TableRow tr = new TableRow();
                TableCell tcLID = new TableCell();
                TableCell tcHendelse = new TableCell();
                TableCell tcBID = new TableCell();
                TableCell tcPID = new TableCell();
                TableCell tcDato = new TableCell();

                string brukerLink = tcBID.ResolveUrl("~/VisBruker?bruker_id=" + logg.bruker_id.ToString());
                tcLID.Text = logg.Logg_id.ToString();
                tcHendelse.Text = logg.Hendelse.ToString();
                tcBID.Text = String.Format("<a href='{0}'>{1}</a>", brukerLink, logg.Bruker.Brukernavn.ToString());
                if (logg.Prosjekt_id != null)
                {
                    Prosjekt prosjekt = Queries.GetProsjekt((int)logg.Prosjekt_id);
                    string prosjektLink = tcPID.ResolveUrl("~/VisProsjekt?prosjekt_id=" + prosjekt.Prosjekt_id.ToString());
                    tcPID.Text = String.Format("<a href='{0}'>{1}</a>", prosjektLink, prosjekt.Navn.ToString());
                }
                tcDato.Text = logg.Opprettet.ToString();

                tr.Cells.Add(tcLID);
                tr.Cells.Add(tcHendelse);
                tr.Cells.Add(tcBID);
                tr.Cells.Add(tcPID);
                tr.Cells.Add(tcDato);

                tabell.Rows.Add(tr);
            }
            tabell.CssClass = "Table";
            return tabell;

        }

        public static Table BurndownChartForFase(int fase_id)
        {
            Fase fase = Queries.GetFase(fase_id);
            List<Oppgave> oppgaverForFase = Queries.getOppgaverIFase(fase_id);
            TimeSpan estimatForFase = new TimeSpan();
            TimeSpan totalSluttTid = new TimeSpan();
            TimeSpan totalAvvikTid = new TimeSpan();
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell headerCell = new TableHeaderCell();
            TableHeaderRow innholdHeaderRow = new TableHeaderRow();
            TableHeaderCell oppgaveRefHeaderCell = new TableHeaderCell();
            TableHeaderCell oppgaveNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell estimatHeaderCell = new TableHeaderCell();

            oppgaveRefHeaderCell.Text = "Oppgaveref.";
            innholdHeaderRow.Cells.Add(oppgaveRefHeaderCell);
            oppgaveNavnHeaderCell.Text = "Oppgavenavn";
            innholdHeaderRow.Cells.Add(oppgaveNavnHeaderCell);
            estimatHeaderCell.Text = "Estimat";
            innholdHeaderRow.Cells.Add(estimatHeaderCell);

            List<DateTime> datoOmfang = Enumerable.Range(0, (fase.Stopp - fase.Start).Days + 1)
                .Select(i => fase.Start.AddDays(i))
                .ToList();

            for (int i = 0; i < datoOmfang.Count; i++ )
            {
                TableHeaderCell tempHeaderCell = new TableHeaderCell();
                tempHeaderCell.Text = datoOmfang[i].ToShortDateString().ToString();
                innholdHeaderRow.Cells.Add(tempHeaderCell);
            }

            

            TableHeaderCell sluttHeaderCell = new TableHeaderCell();
            TableHeaderCell avvikHeaderCell = new TableHeaderCell();

            sluttHeaderCell.Text = "Slutt";
            avvikHeaderCell.Text = "Avvik";
            innholdHeaderRow.Cells.Add(sluttHeaderCell);
            innholdHeaderRow.Cells.Add(avvikHeaderCell);

            tabell.Rows.Add(innholdHeaderRow);

            List<TimeSpan> totalTider = new List<TimeSpan>();
            //Ny liste for å ta vare på totalt beregnet estimat for fase for hver dato
            List<TimeSpan> nyEstimertTidFase = new List<TimeSpan>();
            foreach (DateTime d in datoOmfang)
            {
                totalTider.Add(new TimeSpan(0));
                //Legger inn tider i listen, slik at lengden på listen tilsvarer antall datoer
                nyEstimertTidFase.Add(new TimeSpan(0));
            }


            foreach(Oppgave o in oppgaverForFase)
            {

                TimeSpan nullTimeSpan = new TimeSpan(0);
                
                totalSluttTid = totalSluttTid + (TimeSpan)o.BruktTid;
                totalAvvikTid = totalAvvikTid + (TimeSpan)(o.Estimat - o.BruktTid);
                TimeSpan resterendeTid = (TimeSpan)o.Estimat;
                estimatForFase = estimatForFase + resterendeTid;
                bool ErFerdig = false;
                
                //Legger inn den opprinnelige estimerte tiden for fasen for alle tider i listen
                for (int i = 0; i < nyEstimertTidFase.Count; i++ )
                {
                    nyEstimertTidFase[i] = nyEstimertTidFase[i] + resterendeTid;
                }
                List<Time> registrerteTimerPaaOppgaver = Queries.GetTimerForOppgave(o.Oppgave_id);
                
                TableRow oppgaveRow = new TableRow();
                TableCell oppgaveRefCell = new TableCell();
                TableCell oppgaveNavnCell = new TableCell();
                TableCell estimatCell = new TableCell();

                oppgaveRefCell.Text = o.RefOppgaveId.ToString();
                oppgaveNavnCell.Text = o.Tittel.ToString();
                estimatCell.Text = o.Estimat.ToString();

                oppgaveRow.Cells.Add(oppgaveRefCell);
                oppgaveRow.Cells.Add(oppgaveNavnCell);
                oppgaveRow.Cells.Add(estimatCell);

                for (int i = 0; i < datoOmfang.Count; i++)
                {
                    if (o.Avsluttet != null)
                    {
                        DateTime avsluttetDato = (DateTime)o.Avsluttet;
                        if (datoOmfang[i].Date.Equals(avsluttetDato.Date))
                        {
                            ErFerdig = true;
                            if (o.Estimat > o.BruktTid)
                            {
                                TimeSpan ubruktTid = (TimeSpan)o.RemainingTime;
                                for (int k = i; k < datoOmfang.Count; k++)
                                {
                                    nyEstimertTidFase[k] = nyEstimertTidFase[k] - ubruktTid;
                                }
                            }
                        }
                    } /**/
                    for (int j = 0; j < registrerteTimerPaaOppgaver.Count; j++)
                    {
                       /* if (o.Avsluttet != null)
                        {
                            DateTime avsluttetDato = (DateTime)o.Avsluttet;
                            if (datoOmfang[i].Date.Equals(avsluttetDato.Date))
                            {
                                ErFerdig = true;
                            }
                        } */
                        if (!ErFerdig)
                        {
                            DateTime stoppTid = (DateTime)registrerteTimerPaaOppgaver[j].Stopp;
                            if (datoOmfang[i].Date.Equals(stoppTid.Date))
                            {
                                resterendeTid = resterendeTid - (TimeSpan)registrerteTimerPaaOppgaver[j].Tid;
                            }
                        } else
                            resterendeTid = new TimeSpan(0);
                    }

                    if (resterendeTid > nullTimeSpan)
                    {
                        totalTider[i] = totalTider[i] + resterendeTid;
                    }
                    else // Legger til tid for estimert tid på datoen dersom det blir brukt mer tid enn beregnet på en oppgave
                    {
                        nyEstimertTidFase[i] = nyEstimertTidFase[i] - resterendeTid;
                    }

                    TableCell tempCell = new TableCell();
                    tempCell.Text = resterendeTid.ToString();
                    oppgaveRow.Cells.Add(tempCell);
                }

                TableCell sluttCell = new TableCell();
                sluttCell.Text = o.BruktTid.ToString();
                oppgaveRow.Cells.Add(sluttCell);

                TableCell avvikCell = new TableCell();
                avvikCell.Text = (o.Estimat - o.BruktTid).ToString();
                oppgaveRow.Cells.Add(avvikCell);

                tabell.Rows.Add(oppgaveRow);

            }

            TableRow totalTid = new TableRow();
            TableCell luftCell1 = new TableCell();
            TableCell totalNavn = new TableCell();
            TableCell totalEstimat = new TableCell();

            luftCell1.Text = " ";
            totalNavn.Text = "Total tid";
            totalEstimat.Text = estimatForFase.ToString();

            totalTid.Cells.Add(luftCell1);
            totalTid.Cells.Add(totalNavn);
            totalTid.Cells.Add(totalEstimat);

            for (int i = 0; i < totalTider.Count; i++)
            {
                TableCell tempCell = new TableCell();
                tempCell.Text = totalTider[i].ToString();
                totalTid.Cells.Add(tempCell);
            }

            TableCell totalSlutt = new TableCell();
            TableCell totalAvvik = new TableCell();

            totalSlutt.Text = totalSluttTid.ToString();
            totalAvvik.Text = totalAvvikTid.ToString();

            totalTid.Cells.Add(totalSlutt);
            totalTid.Cells.Add(totalAvvik);

            tabell.Rows.Add(totalTid);

            TableRow ideellTidRow = new TableRow();
            TableCell luftCell2 = new TableCell();
            TableCell ideellNavn = new TableCell();
            TableCell ideellEstimat = new TableCell();

            luftCell2.Text = "";
            ideellNavn.Text = "Ideell tidsbruk";
            ideellEstimat.Text = estimatForFase.ToString();

            ideellTidRow.Cells.Add(luftCell2);
            ideellTidRow.Cells.Add(ideellNavn);
            ideellTidRow.Cells.Add(ideellEstimat);

            double estimatSomDouble = (double) estimatForFase.TotalHours;
            double ideellTid = estimatSomDouble;
            double ideellTidRest;
            int ideelleTimer;
            int ideelleMinutter;

            for (int i = 0; i < datoOmfang.Count; i++)
            {
                TableCell tempCell = new TableCell();

                ideellTid = ideellTid - (estimatSomDouble / datoOmfang.Count);
                ideelleTimer = (int)ideellTid;
                ideellTidRest = ideellTid - (double)ideelleTimer;
                ideelleMinutter = (int)(ideellTidRest * 60);

                TimeSpan ideellTidTimeSpan = new TimeSpan(ideelleTimer, ideelleMinutter, 0);

                tempCell.Text = ideellTidTimeSpan.ToString();
                ideellTidRow.Cells.Add(tempCell);

            }

            TableCell ideellSluttCell = new TableCell();
            TableCell ideellAvvikCell = new TableCell();

            ideellSluttCell.Text = estimatForFase.ToString();
            ideellAvvikCell.Text = "00:00:00";

            ideellTidRow.Cells.Add(ideellSluttCell);
            ideellTidRow.Cells.Add(ideellAvvikCell);

            tabell.Rows.Add(ideellTidRow);

            TableRow totalBeregnetTidForFaseRow = new TableRow();
            TableCell totalBeregnetIdCell = new TableCell();
            TableCell totalBeregnetNavnCell = new TableCell();
            TableCell totalBeregnetEstimat = new TableCell();

            totalBeregnetIdCell.Text = "";
            totalBeregnetNavnCell.Text = "Beregnet estimat (fase)";
            totalBeregnetEstimat.Text = estimatForFase.ToString();

            totalBeregnetTidForFaseRow.Cells.Add(totalBeregnetIdCell);
            totalBeregnetTidForFaseRow.Cells.Add(totalBeregnetNavnCell);
            totalBeregnetTidForFaseRow.Cells.Add(totalBeregnetEstimat);

            for (int i = 0; i < datoOmfang.Count; i++)
            {
                TableCell tempCell = new TableCell();
                tempCell.Text = nyEstimertTidFase[i].ToString();
                totalBeregnetTidForFaseRow.Cells.Add(tempCell);
            }

            tabell.Rows.Add(totalBeregnetTidForFaseRow);

            return tabell;
        }
    }
}