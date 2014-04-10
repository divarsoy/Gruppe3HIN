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

            idHeaderCell.Text = "Id";
            tittelHeaderCell.Text = "Tittel";
            statusHeaderCell.Text = "Status";
            estimatHeaderCell.Text = "Estimat";
            bruktTidHeaderCell.Text = "Brukt tid";
            remainingTimeHeaderCell.Text = "Gjenstående tid";
            brukerHeaderCell.Text = "Brukere";
            kommentarerHeaderCell.Text = "Kommentarer";

            headerRow.Cells.Add(idHeaderCell);
            headerRow.Cells.Add(tittelHeaderCell);
            headerRow.Cells.Add(statusHeaderCell);
            headerRow.Cells.Add(estimatHeaderCell);
            headerRow.Cells.Add(bruktTidHeaderCell);
            headerRow.Cells.Add(remainingTimeHeaderCell);
            headerRow.Cells.Add(brukerHeaderCell);
            headerRow.Cells.Add(kommentarerHeaderCell);
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
                                
                string oppgaveLink = idCell.ResolveUrl("~/VisOppgave?oppgave_id=" + oppgave.Oppgave_id.ToString());

                idCell.Text = string.Format("<a href='{0}'>{1}</a>", oppgaveLink, oppgave.Oppgave_id.ToString());
                tittelCell.Text = string.Format("<a href='{0}'>{1}</a>", oppgaveLink, oppgave.Oppgave_id.ToString());
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

                navnCell.Text = String.Format("<a href='AdministrasjonAvProsjekt?Prosjekt_id={0}'>{1}</a>", prosjekt.Prosjekt_id.ToString(), prosjekt.Navn);
                startDatoCell.Text = String.Format("{0:dd/MM/yyyy}", prosjekt.StartDato);
                sluttDatoCell.Text = String.Format("{0:dd/MM/yyyy}", prosjekt.SluttDato);
                teamCell.Text = String.Format("<a href='AdministrasjonAvTeam?Team_id={0}'>{1}</a>", team.Team_id.ToString(), team.Navn);
                prosjektlederCell.Text = String.Format("<a href='visBruker?bruker_id={0}'>{1} </a>", bruker.Bruker_id, bruker.ToString());

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

        public static Table HentBrukerTabellForTeam(List<Bruker> query, Team nesteTeam)
        {
            Table tabell = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderRow teamNavnHeader = new TableHeaderRow();
            TableHeaderCell teamNavnCell = new TableHeaderCell();
            TableHeaderCell forNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell etterNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell brukerNavnHeaderCell = new TableHeaderCell();
            TableHeaderCell epostHeaderCell = new TableHeaderCell();
            TableHeaderCell IMHeaderCell = new TableHeaderCell();

            forNavnHeaderCell.Text = "Fornavn";
            etterNavnHeaderCell.Text = "Etternavn";
            brukerNavnHeaderCell.Text = "Brukernavn";
            epostHeaderCell.Text = "Epost";
            IMHeaderCell.Text = "IM";
            teamNavnCell.Text = String.Format("<a href='VisTeam?Team_id={0}'>{1}</a>", nesteTeam.Team_id.ToString(), nesteTeam.Navn);
               
            teamNavnHeader.Cells.Add(teamNavnCell);
            tabell.Rows.Add(teamNavnHeader);

            headerRow.Cells.Add(forNavnHeaderCell);
            headerRow.Cells.Add(etterNavnHeaderCell);
            headerRow.Cells.Add(brukerNavnHeaderCell);
            headerRow.Cells.Add(epostHeaderCell);
            headerRow.Cells.Add(IMHeaderCell);
            tabell.Rows.Add(headerRow);

            foreach (Bruker bruker in query)
            {
                TableRow tRow = new TableRow();
                TableCell forNavnCell = new TableCell();
                TableCell etterNavnCell = new TableCell();
                TableCell brukerNavnCell = new TableCell();
                TableCell epostCell = new TableCell();
                TableCell IMCell = new TableCell();

                StringBuilder brukereITeam = new StringBuilder();

                forNavnCell.Text = bruker.Fornavn;

                etterNavnCell.Text = bruker.Etternavn;
                brukerNavnCell.Text = String.Format("<a href='VisBruker?bruker_id={0}'>{1}</a>", bruker.Bruker_id, bruker.Brukernavn);
                epostCell.Text = bruker.Epost;
                IMCell.Text = bruker.IM;

                tRow.Cells.Add(forNavnCell);
                tRow.Cells.Add(etterNavnCell);
                tRow.Cells.Add(brukerNavnCell);
                tRow.Cells.Add(epostCell);
                tRow.Cells.Add(IMCell);

                tabell.Rows.Add(tRow);
            }

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
            TableHeaderCell endreBrukerCell = new TableHeaderCell();

            forNavnHeaderCell.Text = "Fornavn";
            etterNavnHeaderCell.Text = " Etternavn";
            brukerNavnHeaderCell.Text = " Brukernavn";
            epostHeaderCell.Text = " Epost";
            IMHeaderCell.Text = " IM";
            teamHeaderCell.Text = " Team";
            prosjektHeaderCell.Text = " Prosjekter";
            endreBrukerCell.Text = "Rediger Bruker";
           
            headerRow.Cells.Add(forNavnHeaderCell);
            headerRow.Cells.Add(etterNavnHeaderCell);
            headerRow.Cells.Add(brukerNavnHeaderCell);
            headerRow.Cells.Add(epostHeaderCell);
            headerRow.Cells.Add(IMHeaderCell);
            headerRow.Cells.Add(teamHeaderCell);
            headerRow.Cells.Add(prosjektHeaderCell);
            headerRow.Cells.Add(endreBrukerCell);
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
                TableCell endreCell = new TableCell();

                foreach (Team team in queryTeam)
                {
                    teamsCell.Text = String.Format("<a href='AdministrasjonAvTeamBrukere?team_id={0}'>{1} </a>", team.Team_id, team.Navn);
                }
                foreach (Prosjekt prosjekt in queryProsjekt)
                {
                    prosjekterCell.Text = String.Format("<a href='AdministrasjonAvProsjekt?prosjekt_id={0}'>{1} </a>", prosjekt.Prosjekt_id, prosjekt.Navn);
                }

                forNavnCell.Text = String.Format("<a href='visBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Fornavn);
                etterNavnCell.Text = String.Format("<a href='visBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Etternavn);
                brukerNavnCell.Text = String.Format("<a href='visBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), bruker.Brukernavn);
                epostCell.Text = String.Format(bruker.Epost);
                IMCell.Text = String.Format(bruker.IM);
                endreCell.Text = String.Format("<a href='EndreBruker?Bruker_id={0}'>{1}</a>", bruker.Bruker_id.ToString(), "Rediger Bruker");
                    
                tRow.Cells.Add(forNavnCell);
                tRow.Cells.Add(etterNavnCell);
                tRow.Cells.Add(brukerNavnCell);
                tRow.Cells.Add(epostCell);
                tRow.Cells.Add(IMCell);
                tRow.Cells.Add(teamsCell);
                tRow.Cells.Add(prosjekterCell);
                tRow.Cells.Add(endreCell);
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
            oppgaveIdHeaderCell.Text = "Oppgave";
            oppgaveHeaderCell.Text = "Oppgave ID";
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
                //oppgaveCell.Text = String.Format(t.Oppgave.ToString());
                oppgaveCell.Text = "Oppgave";
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
            oppgaveIdHeaderCell.Text = "Oppgave";
            oppgaveHeaderCell.Text = "Oppgave ID";
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
                //oppgaveCell.Text = String.Format(t.Oppgave.ToString());
                oppgaveCell.Text = "Oppgave";
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
    }
}