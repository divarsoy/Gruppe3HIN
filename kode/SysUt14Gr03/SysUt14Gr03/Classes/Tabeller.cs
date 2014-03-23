using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
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
            TableHeaderCell tittelHeaderCell = new TableHeaderCell();
            TableHeaderCell statusHeaderCell = new TableHeaderCell();
            TableHeaderCell estimatHeaderCell = new TableHeaderCell();
            TableHeaderCell bruktTidHeaderCell = new TableHeaderCell();
            TableHeaderCell remainingTimeHeaderCell = new TableHeaderCell();
            TableHeaderCell brukerHeaderCell = new TableHeaderCell();
            TableHeaderCell kommentarerHeaderCell = new TableHeaderCell();

            tittelHeaderCell.Text = "Tittel";
            statusHeaderCell.Text = "Status";
            estimatHeaderCell.Text = "Estimat";
            bruktTidHeaderCell.Text = "Brukt tid";
            remainingTimeHeaderCell.Text = "Gjenstående tid";
            brukerHeaderCell.Text = "Brukere";
            kommentarerHeaderCell.Text = "Kommentarer";

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
                    brukereIOppgave.Append(String.Format("<a href='HistorikkStattestikk?bruker_id={0}'>{1} </a>", bruker.Bruker_id, bruker.Brukernavn));
                }
                TableRow tRow = new TableRow();
                TableCell tittelCell = new TableCell();
                TableCell statusCell = new TableCell();
                TableCell estimatCell = new TableCell();
                TableCell bruktTidCell = new TableCell();
                TableCell remainingCell = new TableCell();
                TableCell brukerCell = new TableCell();
                TableCell kommentarCell = new TableCell();

                tittelCell.Text = String.Format("<a href='ArkiverOppg?oppgave_id={0}'>{1}</a>", oppgave.Oppgave_id.ToString(), oppgave.Tittel);
                statusCell.Text = Queries.GetStatus(oppgave.Status_id).Navn;
                estimatCell.Text = oppgave.Estimat.ToString();
                bruktTidCell.Text = oppgave.BruktTid.ToString();
                remainingCell.Text = oppgave.RemainingTime.ToString();
                brukerCell.Text = brukereIOppgave.ToString();
                kommentarCell.Text = oppgave.Kommentarer.Count.ToString();

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
                prosjektlederCell.Text = String.Format("<a href='HistorikkStattestikk?bruker_id={0}'>{1} </a>", bruker.Bruker_id, bruker.ToString());

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

        public static Table HentBrukerTabell(List<Bruker> query)
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
                /*StringBuilder brukereIOppgave = new StringBuilder();
                foreach (Bruker bruker in bruker.Brukere)
                {
                    brukereIOppgave.Append(String.Format("<a href='HistorikkStattestikk?bruker_id={0}'>{1} </a>", bruker.Bruker_id, bruker.Brukernavn));
                }*/
                TableRow tRow = new TableRow();
                TableCell forNavnCell = new TableCell();
                TableCell etterNavnCell = new TableCell();
                TableCell brukerNavnCell = new TableCell();
                TableCell epostCell = new TableCell();
                TableCell IMCell = new TableCell();
                TableCell teamsCell = new TableCell();
                TableCell prosjekterCell = new TableCell();

                /*Team team = Queries.GetAlleTeamsEnBrukerErMedI((int)bruker.Bruker_id);
                Prosjekt prosjekt = Queries.GetProsjekt(prosjekt.Bruker_id);

                forNavnCell.Text = String.Format("<a href='ArkiverOppg?oppgave_id={0}'>{1}</a>", bruker.Oppgave_id.ToString(), bruker.Tittel);
                etterNavnCell.Text = Queries.GetStatus(bruker.Status_id).Navn;
                brukerNavnCell.Text = bruker.Estimat.ToString();
                epostCell.Text = bruker.BruktTid.ToString();
                IMCell.Text = bruker.RemainingTime.ToString();
                teamsCell.Text = brukereIOppgave.ToString();
                prosjekterCell.Text = bruker.Kommentarer.Count.ToString();*/

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
            brukereHeaderCell.Text = "Tilknyttet Oppgave";
            opprettetHeaderCell.Text = "Opprettet";
            aktivHeaderCell.Text = "Aktiv";

            headerRow.Cells.Add(navnHeaderCell);
            headerRow.Cells.Add(startDatoHeaderCell);
            headerRow.Cells.Add(sluttDatoHeaderCell);
            headerRow.Cells.Add(teamHeaderCell);
            headerRow.Cells.Add(brukereHeaderCell);
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
                TableCell oppgCell = new TableCell();
                TableCell opprettetCell = new TableCell();
                TableCell aktivCell = new TableCell();

                Team team = Queries.GetTeam((int)prosjekt.Team_id);
                Bruker bruker = Queries.GetBruker(prosjekt.Bruker_id);
                List<Oppgave> liste = Queries.GetAlleAktiveOppgaverForProsjekt(prosjekt.Prosjekt_id);


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


                for (int i = 0; i < liste.Count; i++)
                {
                    Oppgave oppg = liste[i];
                    oppgCell.Text = String.Format(oppg.Tittel);
                }

                tRow.Cells.Add(navnCell);
                tRow.Cells.Add(startDatoCell);
                tRow.Cells.Add(sluttDatoCell);
                tRow.Cells.Add(teamCell);
                tRow.Cells.Add(oppgCell);
                tRow.Cells.Add(opprettetCell);
                tRow.Cells.Add(aktivCell);
                tabell.Rows.Add(tRow);
            }
            tabell.CssClass = "table";

            return tabell;
        }
    }
}