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

        public static Table HentProsjektTabell(List<Prosjekt> query)
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

                navnCell.Text = String.Format("<a href='AdministrasjonAvProsjekt?Prosjekt_id={0}'>{1}</a>", prosjekt.Prosjekt_id.ToString(), prosjekt.Navn);
                startDatoCell.Text = String.Format("{0}", prosjekt.StartDato);
                sluttDatoCell.Text = String.Format("{0}", prosjekt.SluttDato);
                //teamCell.Text = Queries.GetTeam(prosjekt.Team_id).Navn;
                prosjektlederCell.Text = Queries.GetBruker(prosjekt.Bruker_id).ToString();

                tRow.Cells.Add(navnCell);
                tRow.Cells.Add(startDatoCell);
                tRow.Cells.Add(sluttDatoCell);
                //tRow.Cells.Add(teamCell);
                tRow.Cells.Add(prosjektlederCell);

                tabell.Rows.Add(tRow);
            }
            return tabell;

        }
    }
}