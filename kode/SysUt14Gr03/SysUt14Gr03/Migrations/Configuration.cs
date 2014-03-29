namespace SysUt14Gr03.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SysUt14Gr03.Models;
    using SysUt14Gr03.Classes;

    internal sealed class Configuration : DbMigrationsConfiguration<SysUt14Gr03.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var rettigheter = new List<Rettighet> {
                new Rettighet {
                    RettighetNavn = Konstanter.rettighet.Administrator.ToString(),
                    Brukere = new List<Bruker>()
                },
                new Rettighet {
                    RettighetNavn = Konstanter.rettighet.Prosjektleder.ToString(),
                    Brukere = new List<Bruker>()
                },
                new Rettighet {
                    RettighetNavn = Konstanter.rettighet.Teamleder.ToString(),
                    Brukere = new List<Bruker>()
                },
                new Rettighet {
                    RettighetNavn = Konstanter.rettighet.Utvikler.ToString(),
                    Brukere = new List<Bruker>()
                }
            };

            rettigheter.ForEach(element => context.Rettigheter.AddOrUpdate(rettighet => rettighet.RettighetNavn, element));
            context.SaveChanges();

            var notifikasjonsTyper = new List<NotifikasjonsType> {
                new NotifikasjonsType {
                    Type = "alert-success"
                },
                new NotifikasjonsType {
                    Type = "alert-info"
                },
                new NotifikasjonsType {
                    Type = "alert-warning"
                },
                new NotifikasjonsType {
                    Type = "alert-danger"
                }
            };

            notifikasjonsTyper.ForEach(element => context.NotifikasjonsType.AddOrUpdate(notifikasjonstype => notifikasjonstype.Type, element));
            context.SaveChanges();

            var brukere = new List<Bruker> {
               new Bruker {
                    Etternavn = "�sg�rd",
                    Fornavn = "Jane",
                    Brukernavn = "jaasgaard",
                    Epost = "janeaasgaard@gmail.com",
                    Passord = "Passord m� krypteres!",
                    IM = "jaasgaard",
                    Token = "M� generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    BrukerPreferanser = new List<BrukerPreferanse>(),
                    Rettigheter = new List<Rettighet>(),
                    Moeter = new List<Moete>(),
                    Kommentarer = new List<Kommentar>(),
                    Logger = new List<Logg>(),
                    Oppgaver = new List<Oppgave>(),
                    Prosjekter = new List<Prosjekt>(),
                    Teams = new List<Team>()
                },
                new Bruker {
                    Etternavn = "Martinsen",
                    Fornavn = "Lars",
                    Brukernavn = "lmartinsen",
                    Epost = "lmartinsen@gmail.com",
                    Passord = "Passord m� krypteres!",
                    IM = "lmartinsen",
                    Token = "M� generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    BrukerPreferanser = new List<BrukerPreferanse>(),
                    Rettigheter = new List<Rettighet>(),
                    Moeter = new List<Moete>(),
                    Kommentarer = new List<Kommentar>(),
                    Logger = new List<Logg>(),
                    Oppgaver = new List<Oppgave>(),
                    Prosjekter = new List<Prosjekt>(),
                    Teams = new List<Team>()
                },
                new Bruker {
                    Etternavn = "Larsen",
                    Fornavn = "Martin",
                    Brukernavn = "mlarsen",
                    Epost = "mlarsen@gmail.com",
                    Passord = "Passord m� krypteres!",
                    IM = "mlarsen",
                    Token = "M� generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    BrukerPreferanser = new List<BrukerPreferanse>(),
                    Rettigheter = new List<Rettighet>(),
                    Moeter = new List<Moete>(),
                    Kommentarer = new List<Kommentar>(),
                    Logger = new List<Logg>(),
                    Oppgaver = new List<Oppgave>(),
                    Prosjekter = new List<Prosjekt>(),
                    Teams = new List<Team>()
                },
                new Bruker {
                    Etternavn = "Hansen",
                    Fornavn = "Heidi",
                    Brukernavn = "hhansen",
                    Epost = "hhansen@gmail.com",
                    Passord = "Passord m� krypteres!",
                    IM = "hhansen",
                    Token = "M� generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    BrukerPreferanser = new List<BrukerPreferanse>(),
                    Rettigheter = new List<Rettighet>(),
                    Moeter = new List<Moete>(),
                    Kommentarer = new List<Kommentar>(),
                    Logger = new List<Logg>(),
                    Oppgaver = new List<Oppgave>(),
                    Prosjekter = new List<Prosjekt>(),
                    Teams = new List<Team>()
                },
                new Bruker {
                    Etternavn = "Ask�y",
                    Fornavn = "Anette",
                    Brukernavn = "aaskoy",
                    Epost = "aaskoy@gmail.com",
                    Passord = "Passord m� krypteres!",
                    IM = "aaskoy",
                    Token = "M� generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    BrukerPreferanser = new List<BrukerPreferanse>(),
                    Rettigheter = new List<Rettighet>(),
                    Moeter = new List<Moete>(),
                    Kommentarer = new List<Kommentar>(),
                    Logger = new List<Logg>(),
                    Oppgaver = new List<Oppgave>(),
                    Prosjekter = new List<Prosjekt>(),
                    Teams = new List<Team>()
                },
                new Bruker {
                    Etternavn = "Jan",
                    Fornavn = "Polden",
                    Brukernavn = "jpolden",
                    Epost = "jpolden@gmail.com",
                    Passord = "Passord m� krypteres!",
                    IM = "jpolden",
                    Token = "M� generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    BrukerPreferanser = new List<BrukerPreferanse>(),
                    Rettigheter = new List<Rettighet>(),
                    Moeter = new List<Moete>(),
                    Kommentarer = new List<Kommentar>(),
                    Logger = new List<Logg>(),
                    Oppgaver = new List<Oppgave>(),
                    Prosjekter = new List<Prosjekt>(),
                    Teams = new List<Team>()
                },
                new Bruker {
                    Etternavn = "Holm",
                    Fornavn = "Pernille",
                    Brukernavn = "pholm",
                    Epost = "pholm@gmail.com",
                    Passord = "Passord m� krypteres!",
                    IM = "pholm",
                    Token = "M� generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    BrukerPreferanser = new List<BrukerPreferanse>(),
                    Rettigheter = new List<Rettighet>(),
                    Moeter = new List<Moete>(),
                    Kommentarer = new List<Kommentar>(),
                    Logger = new List<Logg>(),
                    Oppgaver = new List<Oppgave>(),
                    Prosjekter = new List<Prosjekt>(),
                    Teams = new List<Team>()
                }
            };

            brukere.ForEach(element => context.Brukere.AddOrUpdate(bruker => bruker.Etternavn, element));
            context.SaveChanges();

            var brukerPreferanser = new List<BrukerPreferanse> {                
                new BrukerPreferanse {
                    EpostTeam = true,
                    EpostProsjekt = true,
                    EpostOppgave = true,
                    EpostKommentar = true,
                    EpostTidsfrist = true,
                    EpostRapport = true,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "jaasgaard" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = true,
                    EpostProsjekt = true,
                    EpostOppgave = true,
                    EpostKommentar = true,
                    EpostTidsfrist = true,
                    EpostRapport = true,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "lmartinsen" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = true,
                    EpostProsjekt = true,
                    EpostOppgave = true,
                    EpostKommentar = true,
                    EpostTidsfrist = true,
                    EpostRapport = true,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "mlarsen" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = true,
                    EpostProsjekt = true,
                    EpostOppgave = true,
                    EpostKommentar = true,
                    EpostTidsfrist = true,
                    EpostRapport = true,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "hhansen" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = true,
                    EpostProsjekt = true,
                    EpostOppgave = true,
                    EpostKommentar = true,
                    EpostTidsfrist = true,
                    EpostRapport = true,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "aaskoy" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = true,
                    EpostProsjekt = true,
                    EpostOppgave = true,
                    EpostKommentar = true,
                    EpostTidsfrist = true,
                    EpostRapport = true,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "jpolden" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = true,
                    EpostProsjekt = true,
                    EpostOppgave = true,
                    EpostKommentar = true,
                    EpostTidsfrist = true,
                    EpostRapport = true,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "pholm" ).Bruker_id
                }
            };

            brukerPreferanser.ForEach(element => context.BrukerPreferanser.AddOrUpdate(brukerpreferanse => brukerpreferanse.Bruker_id, element));
            context.SaveChanges();

            Rettighet BrukerRettighetAdministrator = context.Rettigheter.FirstOrDefault(rettighet => rettighet.RettighetNavn == Konstanter.rettighet.Administrator.ToString());
            Rettighet BrukerRettighetProsjektleder = context.Rettigheter.FirstOrDefault(rettighet => rettighet.RettighetNavn == Konstanter.rettighet.Prosjektleder.ToString());
            Rettighet BrukerRettighetTeamleder = context.Rettigheter.FirstOrDefault(rettighet => rettighet.RettighetNavn == Konstanter.rettighet.Teamleder.ToString());
            Rettighet BrukerRettighetUtvikler = context.Rettigheter.FirstOrDefault(rettighet => rettighet.RettighetNavn == Konstanter.rettighet.Utvikler.ToString());


            Bruker jaasgaard = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jaasgaard");
            jaasgaard.Rettigheter.Add(BrukerRettighetAdministrator);

            Bruker lmartinsen = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen");
            lmartinsen.Rettigheter.Add(BrukerRettighetUtvikler);

            Bruker mlarsen = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen");
            mlarsen.Rettigheter.Add(BrukerRettighetProsjektleder);

            Bruker hhansen = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen");
            lmartinsen.Rettigheter.Add(BrukerRettighetUtvikler);

            Bruker aaskoy = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy");
            aaskoy.Rettigheter.Add(BrukerRettighetUtvikler);

            Bruker jpolden = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden");
            jpolden.Rettigheter.Add(BrukerRettighetUtvikler);

            Bruker pholm = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm");
            pholm.Rettigheter.Add(BrukerRettighetUtvikler);

            context.SaveChanges();

            var teams = new List<Team> {
                new Team {
                    Navn = "Alpha",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Brukere = new List<Bruker>(),
                    Prosjekter = new List<Prosjekt>(),
                    Grupper = new List<Gruppe>()
                },
                 new Team {
                    Navn = "Bravo",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Brukere = new List<Bruker>(),
                    Prosjekter = new List<Prosjekt>(),
                    Grupper = new List<Gruppe>()
                },
                 new Team {
                    Navn = "Charlie",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Brukere = new List<Bruker>(),
                    Prosjekter = new List<Prosjekt>(),
                    Grupper = new List<Gruppe>()
                }
            };

            teams.ForEach(element => context.Teams.AddOrUpdate(team => team.Navn, element));
            context.SaveChanges();

            Team alpha = context.Teams.FirstOrDefault(Team => Team.Navn == "Alpha");
            lmartinsen.Teams.Add(alpha);
            mlarsen.Teams.Add(alpha);

            Team bravo = context.Teams.FirstOrDefault(Team => Team.Navn == "Bravo");
            hhansen.Teams.Add(bravo);
            aaskoy.Teams.Add(bravo);

            Team charlie = context.Teams.FirstOrDefault(Team => Team.Navn == "Charlie");
            jpolden.Teams.Add(charlie);
            pholm.Teams.Add(charlie);
            context.SaveChanges();


            var prosjekter = new List<Prosjekt> {
                new Prosjekt {
                    Navn = "R�d Elv",
                    Aktiv = true,
                    StartDato = DateTime.Now,
                    SluttDato = DateTime.Now.AddMonths(3),
                    Opprettet = DateTime.Now,
                    Team_id = context.Teams.FirstOrDefault(team => team.Navn == "Alpha").Team_id,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    Oppgaver = new List<Oppgave>()
                },
                new Prosjekt {
                    Navn = "Bl� spurv",
                    Aktiv = true,
                    StartDato = DateTime.Now,
                    SluttDato = DateTime.Now.AddMonths(4),
                    Opprettet = DateTime.Now,
                    Team_id = context.Teams.FirstOrDefault(team => team.Navn == "Bravo").Team_id,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    Oppgaver = new List<Oppgave>()
                },
                new Prosjekt {
                    Navn = "Gr� ulv",
                    Aktiv = true,
                    StartDato = DateTime.Now,
                    SluttDato = DateTime.Now.AddMonths(5),
                    Opprettet = DateTime.Now,
                    Team_id = context.Teams.FirstOrDefault(team => team.Navn == "Charlie").Team_id,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    Oppgaver = new List<Oppgave>()
                }
            };
            prosjekter.ForEach(element => context.Prosjekter.AddOrUpdate(prosjekt => prosjekt.Navn, element));
            context.SaveChanges();

            var statuser = new List <Status> {
                new Status {
                    Navn = "Klar",
                    Oppgaver = new List<Oppgave>()
                },
                new Status {
                    Navn = "Under arbeid",
                    Oppgaver = new List<Oppgave>()
                },
                new Status {
                    Navn = "Ferdig",
                    Oppgaver = new List<Oppgave>()
                }
            };
            statuser.ForEach(element => context.Status.AddOrUpdate(status => status.Navn, element));
            context.SaveChanges();

            var prioriteringer = new List <Prioritering> {
                new Prioritering {
                    Navn = "1",
                    Oppgaver = new List<Oppgave>()
                },
                                new Prioritering {
                    Navn = "2",
                    Oppgaver = new List<Oppgave>()
                },
                                new Prioritering {
                    Navn = "3",
                    Oppgaver = new List<Oppgave>()
                },
                                new Prioritering {
                    Navn = "4",
                    Oppgaver = new List<Oppgave>()
                },
                                new Prioritering {
                    Navn = "5",
                    Oppgaver = new List<Oppgave>()
                },
                                new Prioritering {
                    Navn = "6",
                    Oppgaver = new List<Oppgave>()
                },
                                new Prioritering {
                    Navn = "7",
                    Oppgaver = new List<Oppgave>()
                },
                                new Prioritering {
                    Navn = "8",
                    Oppgaver = new List<Oppgave>()
                },
                                new Prioritering {
                    Navn = "9",
                    Oppgaver = new List<Oppgave>()
                },
                                new Prioritering {
                    Navn = "10",
                    Oppgaver = new List<Oppgave>()
                }
            };

            prioriteringer.ForEach(element => context.Prioriteringer.AddOrUpdate(prioritering => prioritering.Navn, element));
            context.SaveChanges();

            var grupper = new List<Gruppe> {
                new Gruppe {
                    Navn = "Gruppe 1",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Prosjekter = new List<Prosjekt>(),
                    Teams = new List<Team>()
                },
                new Gruppe {
                    Navn = "Gruppe 2",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Prosjekter = new List<Prosjekt>(),
                    Teams = new List<Team>()
                }
            };
            grupper.ForEach(element => context.Grupper.AddOrUpdate(gruppe => gruppe.Navn, element));
            context.SaveChanges();

            Gruppe gruppe1 = context.Grupper.FirstOrDefault(Gruppe => Gruppe.Navn == "Gruppe 1");
            Gruppe gruppe2 = context.Grupper.FirstOrDefault(Gruppe => Gruppe.Navn == "Gruppe 2");

            gruppe1.Teams.Add(alpha);
            gruppe1.Teams.Add(bravo);
            gruppe2.Teams.Add(charlie);
            context.SaveChanges();

            var oppgavegrupper = new List <OppgaveGruppe> {
                new OppgaveGruppe {
                    Navn = "Administrative Oppgaver",
                    Oppgaver = new List<Oppgave>()
                },
                new OppgaveGruppe {
                    Navn = "Funksjonelle oppgaver",
                    Oppgaver = new List<Oppgave>()
                }
            };
            oppgavegrupper.ForEach(element => context.OppgaveGrupper.AddOrUpdate(oppgavegruppe => oppgavegruppe.Navn, element));
            context.SaveChanges();
          
            var oppgaver = new List <Oppgave> {
                new Oppgave {
                    Tittel = "Opprette notat",
                    UserStory = "Som bruker �nsker jeg � kunne opprette et notat",
                    Krav = "Notatet skal ikke overg� en side",
                    Estimat = 3,
                    BruktTid = 3,
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Oppdatert = DateTime.Now.AddDays(2),
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "R�d Elv").Prosjekt_id,
                    OppgaveGruppe_id = context.OppgaveGrupper.FirstOrDefault(oppgaveGruppe => oppgaveGruppe.Navn == "Administrative Oppgaver").OppgaveGruppe_id,
                    Prioritering_id = context.Prioriteringer.FirstOrDefault(prioritering => prioritering.Navn == "1").Prioritering_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Ferdig").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "Redigere notat",
                    UserStory = "Som bruker �nsker jeg � kunne redigere et notat",
                    Krav = "Notatet skal ikke overg� en side",
                    Estimat = 6,
                    BruktTid = 3,
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Oppdatert = DateTime.Now.AddDays(2),
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "R�d Elv").Prosjekt_id,
                    OppgaveGruppe_id = context.OppgaveGrupper.FirstOrDefault(oppgaveGruppe => oppgaveGruppe.Navn == "Administrative Oppgaver").OppgaveGruppe_id,
                    Prioritering_id = context.Prioriteringer.FirstOrDefault(prioritering => prioritering.Navn == "2").Prioritering_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Under Arbeid").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "Slette notat",
                    UserStory = "Som bruker �nsker jeg � kunne slette et notat",
                    Krav = "Notatet skal ikke overg� en side",
                    Estimat = 2,
                    BruktTid = 0,
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "R�d Elv").Prosjekt_id,
                    OppgaveGruppe_id = context.OppgaveGrupper.FirstOrDefault(oppgaveGruppe => oppgaveGruppe.Navn == "Administrative Oppgaver").OppgaveGruppe_id,
                    Prioritering_id = context.Prioriteringer.FirstOrDefault(prioritering => prioritering.Navn == "3").Prioritering_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Klar").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "Opprette bruker",
                    UserStory = "Som administrator �nsker jeg � kunne opprette en ny bruker",
                    Krav = "Brukeren m� ikke finnes i databasen fra f�r av",
                    Estimat = 4,
                    BruktTid = 0,
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "R�d Elv").Prosjekt_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Klar").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "Opprette Prosjekt",
                    UserStory = "Som bruker �nsker jeg � kunne opprette et prosjekt",
                    Krav = "Prosjektet m� v�re unikt",
                    Estimat = 4,
                    BruktTid = 0,
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "Bl� spurv").Prosjekt_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Klar").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "Administrere prosjekt",
                    UserStory = "Som bruker �nsker jeg � kunne administrere et prosjekt",
                    Krav = "Prosjektet m� v�re aktivt",
                    Estimat = 6,
                    BruktTid = 0,
                    Tidsfrist = DateTime.Now.AddDays(7),
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "Bl� spurv").Prosjekt_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Klar").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "Arkivere prosjekt",
                    UserStory = "Som bruker �nsker jeg � kunne arkivere et prosjekt",
                    Krav = "Prosjektet m� v�re aktivt",
                    Estimat = 2,
                    BruktTid = 0,
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "Bl� spurv").Prosjekt_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Klar").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "Opprette m�te",
                    UserStory = "Som bruker �nsker jeg � kunne opprette et m�te",
                    Krav = "M� spesifisere minst en deltaker",
                    Estimat = 4,
                    BruktTid = 4,
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Oppdatert = DateTime.Now.AddDays(2),
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "Gr� ulv").Prosjekt_id,
                    OppgaveGruppe_id = context.OppgaveGrupper.FirstOrDefault(oppgaveGruppe => oppgaveGruppe.Navn == "Funksjonelle oppgaver").OppgaveGruppe_id,
                    Prioritering_id = context.Prioriteringer.FirstOrDefault(prioritering => prioritering.Navn == "1").Prioritering_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Ferdig").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "Redigere m�te",
                    UserStory = "Som bruker �nsker jeg � kunne redigere et m�te",
                    Krav = "M�tetidspunkt m� ikke ha v�rt",
                    Estimat = 3,
                    BruktTid = 1,
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Oppdatert = DateTime.Now.AddDays(2),
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "Gr� ulv").Prosjekt_id,
                    OppgaveGruppe_id = context.OppgaveGrupper.FirstOrDefault(oppgaveGruppe => oppgaveGruppe.Navn == "Funksjonelle oppgaver").OppgaveGruppe_id,
                    Prioritering_id = context.Prioriteringer.FirstOrDefault(prioritering => prioritering.Navn == "2").Prioritering_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Under Arbeid").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "Slette et m�te",
                    UserStory = "Som bruker �nsker jeg � kunne slette et m�te",
                    Krav = "M�tetidspunkt m� ikke ha v�rt",
                    Estimat = 2,
                    BruktTid = 0,
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "Gr� ulv").Prosjekt_id,
                    OppgaveGruppe_id = context.OppgaveGrupper.FirstOrDefault(oppgaveGruppe => oppgaveGruppe.Navn == "Funksjonelle oppgaver").OppgaveGruppe_id,
                    Prioritering_id = context.Prioriteringer.FirstOrDefault(prioritering => prioritering.Navn == "3").Prioritering_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Klar").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },
                new Oppgave {
                    Tittel = "P�melding til m�te",
                    UserStory = "Som bruker �nsker jeg � kunne melde meg p� et m�te",
                    Krav = "Brukeren m� ikke allede v�re p�meldt m�tet",
                    Estimat = 2,
                    BruktTid = 0,
                    Tidsfrist = DateTime.Now.AddDays(11),
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "R�d Elv").Prosjekt_id,
                    OppgaveGruppe_id = context.OppgaveGrupper.FirstOrDefault(oppgaveGruppe => oppgaveGruppe.Navn == "Administrative Oppgaver").OppgaveGruppe_id,
                    Prioritering_id = context.Prioriteringer.FirstOrDefault(prioritering => prioritering.Navn == "1").Prioritering_id,
                    Status_id = context.Status.FirstOrDefault(status => status.Navn == "Klar").Status_id,
                    Kommentarer = new List<Kommentar>(),
                    Brukere = new List<Bruker>()
                },             
            };

            oppgaver.ForEach(element => context.Oppgaver.AddOrUpdate(oppgave => oppgave.Tittel, element));
            context.SaveChanges();

            Oppgave oppgave1 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette notat");
            lmartinsen.Oppgaver.Add(oppgave1);
            mlarsen.Oppgaver.Add(oppgave1);

            Oppgave oppgave2 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Redigere notat");
            lmartinsen.Oppgaver.Add(oppgave2);

            Oppgave oppgave3 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Slette notat");
            mlarsen.Oppgaver.Add(oppgave3);

            Oppgave oppgave4 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette bruker");
            lmartinsen.Oppgaver.Add(oppgave4);

            Oppgave oppgave5 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette Prosjekt");
            hhansen.Oppgaver.Add(oppgave5);
            aaskoy.Oppgaver.Add(oppgave5);

            Oppgave oppgave6 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Administrere prosjekt");
            hhansen.Oppgaver.Add(oppgave6);

            Oppgave oppgave7 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Arkivere prosjekt");
            aaskoy.Oppgaver.Add(oppgave7);

            Oppgave oppgave8 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette m�te");
            jpolden.Oppgaver.Add(oppgave8);
            pholm.Oppgaver.Add(oppgave8);

            Oppgave oppgave9 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Redigere m�te");
            jpolden.Oppgaver.Add(oppgave9);

            Oppgave oppgave10 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Slette et m�te");
            pholm.Oppgaver.Add(oppgave10);

            Oppgave oppgave11 = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "P�melding til m�te");
            jpolden.Oppgaver.Add(oppgave11);

            context.SaveChanges();

            var kommentarer = new List<Kommentar> {
                new Kommentar {
                    Tekst = "@mlarsen Har du f�tt startet p� oppgaven?",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette notat").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Ja, jeg fikk startet p� den i g�r",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette notat").Oppgave_id
                },
                new Kommentar {
                    Tekst = "St�r fast p� linje 3",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Redigere notat").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Pr�v med et array istedet",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Redigere notat").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Woohoooo... Det virker",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Slette notat").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Bra jobba Martinsen!",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Slette notat").Oppgave_id
                },
                new Kommentar {
                    Tekst = "@mlarsen Har du f�tt startet p� oppgaven?",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette bruker").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Ja, jeg fikk startet p� den i g�r",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette bruker").Oppgave_id
                },
                new Kommentar {
                    Tekst = "@aaskoy Fant du noen l�sning?",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette Prosjekt").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Ja, vi kan bruke brukerklassen",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette Prosjekt").Oppgave_id
                },
                                new Kommentar {
                    Tekst = "Vet du hvordan vi kan beregne estimert tid?",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Administrere prosjekt").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Ja bare legg oppgavene i en liste og g� igjennom de med en foreach loop",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Administrere prosjekt").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Aaskoy, du er genial!",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Arkivere prosjekt").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Takk :-)",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Arkivere prosjekt").Oppgave_id
                },
                new Kommentar {
                    Tekst = "@pholm Rekker vi � bli ferdig innen fristen?",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette m�te").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Ja, vi har god margin",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Opprette m�te").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Vi har kundem�te i morgen",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Redigere m�te").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Ja, jeg s� det",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Redigere m�te").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Kanskje vi burde arkivere m�tene istedet for � slette de?",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Slette et m�te").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Nei, m�tene b�r slettes. Det vil bli for mye rot i kalenderen om vi arkiverer de",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "Slette et m�te").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Ja! kunden godkjente oppgaven",
                    Aktiv = true,
                    Opprettet = DateTime.Now,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "P�melding til m�te").Oppgave_id
                },
                new Kommentar {
                    Tekst = "Jepp, det var en meget god presentasjon jpolden!",
                    Aktiv = true,
                    Opprettet = DateTime.Now.AddMinutes(2),
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id,
                    Oppgave_id = context.Oppgaver.FirstOrDefault(oppgave => oppgave.Tittel == "P�melding til m�te").Oppgave_id
                },
            };

            kommentarer.ForEach(element => context.Kommentarer.AddOrUpdate(kommentar => kommentar.Tekst));
            context.SaveChanges();

            var logger = new List <Logg> {
                new Logg {
                    Hendelse = "Opprettet 'mlarsen' som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jaasgaard").Bruker_id
                },           
                new Logg {
                    Hendelse = "Opprettet 'hhansen' som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jaasgaard").Bruker_id
                },
                new Logg {
                    Hendelse = "Opprettet 'aaskoy' som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jaasgaard").Bruker_id
                },
                new Logg {
                    Hendelse = "Opprettet 'jpolden' som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jaasgaard").Bruker_id
                },
                new Logg {
                    Hendelse = "Opprettet 'pholm' som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jaasgaard").Bruker_id
                },                
                new Logg {
                    Hendelse = "'jaasgaard' ble opprettet som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jaasgaard").Bruker_id,
                },
                new Logg {
                    Hendelse = "'lmartinsen' ble opprettet som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id
                },
                new Logg {
                    Hendelse = "'mlarsen' ble opprettet som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "'hhansen' ble opprettet som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id
                },
                new Logg {
                    Hendelse = "'aaskoy' ble opprettet som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id
                },
                new Logg {
                    Hendelse = "'jpolden' ble opprettet som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id
                },
                new Logg {
                    Hendelse = "'pholm' ble opprettet som ny bruker",
                    Opprettet = DateTime.Now,
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id
                },
                new Logg {
                    Hendelse = "Opprettet team 'Alpha'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "Opprettet team 'Bravo'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "Opprettet team 'Charlie'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "La til bruker 'lmartinsen' i team 'Alpha'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },              
                new Logg {
                    Hendelse = "La til bruker 'mlarsen' i team 'Alpha'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "La til bruker 'hhansen' i team 'Bravo'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "La til bruker 'aaskoy' i team 'Bravo'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "La til bruker 'jpolden' i team 'Charlie'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "La til bruker 'pholm' i team 'Charlie'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "'lmartinsen' ble lagt til i team 'Alpha'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id
                },
                new Logg {
                    Hendelse = "'mlarsen' ble lagt til i team 'Alpha'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "'hhansen' ble lagt til i team 'Bravo'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id
                },
                new Logg {
                    Hendelse = "'aaskoy' ble lagt til i team 'Bravo'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id
                },
                new Logg {
                    Hendelse = "'jpolden' ble lagt til i team 'Charlie'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id
                },
                new Logg {
                    Hendelse = "'pholm' ble lagt til i team 'Charlie'",
                    Opprettet = DateTime.Now.AddMinutes(10),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id
                },
                new Logg {
                    Hendelse = "Team 'Alpha' ble lagt til i prosjektet 'R�d Elv",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "Team 'Bravo' ble lagt til i prosjektet 'Bl� spurv",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "Team 'Charlie' ble lagt til i prosjektet 'Gr� Ulv",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "'lmartinsen' ble lagt til i prosjektet 'R�d Elv'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id
                },
                new Logg {
                    Hendelse = "'mlarsen' ble lagt til i prosjektet 'R�d Elv",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "'hhansen' ble lagt til i prosjektet 'Bl� spurv'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id
                },
                new Logg {
                    Hendelse = "'aaskoy' ble lagt til i prosjektet 'Bl� spurv'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id
                },
                new Logg {
                    Hendelse = "'jpolden' ble lagt til i prosjektet 'Gr� Ulv'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id
                },
                new Logg {
                    Hendelse = "'pholm' ble lagt til i prosjektet 'Gr� Ulv'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id
                },             
                new Logg {
                    Hendelse = "lmartinsen p�tok seg oppgaven 'Opprette notat'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id
                },
                new Logg {
                    Hendelse = "mlarsen p�tok seg oppgaven 'Opprette notat'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "lmartinsen p�tok seg oppgaven 'Redigere notat'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id
                },
                new Logg {
                    Hendelse = "mlarsen p�tok seg oppgaven 'Slette notat'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id
                },
                new Logg {
                    Hendelse = "lmartinsen p�tok seg oppgaven 'Opprette bruker'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id
                },
                new Logg {
                    Hendelse = "hhansen p�tok seg oppgaven 'Opprette Prosjekt'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id
                },
                new Logg {
                    Hendelse = "aaskoy p�tok seg oppgaven 'Opprette Prosjekt'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id
                },
                new Logg {
                    Hendelse = "hhansen p�tok seg oppgaven 'Administrere Prosjekt'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id
                },
                new Logg {
                    Hendelse = "aaskoy p�tok seg oppgaven 'Arkivere Prosjekt'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id
                },
                new Logg {
                    Hendelse = "jpolden p�tok seg oppgaven 'Opprette m�te'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id
                },
                new Logg {
                    Hendelse = "jholm p�tok seg oppgaven 'Opprette m�te'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id
                },
                new Logg {
                    Hendelse = "jpolden p�tok seg oppgaven 'Redigere m�te'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id
                },
                new Logg {
                    Hendelse = "pholm p�tok seg oppgaven 'Slette et m�te'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id
                },
                new Logg {
                    Hendelse = "jpolden p�tok seg oppgaven 'P�melding til m�te'",
                    Opprettet = DateTime.Now.AddMinutes(15),
                    bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id
                }
            };
            logger.ForEach(element => context.Logger.AddOrUpdate(logg => logg.Hendelse, element));
            context.SaveChanges();
            
            var notifikasjon = new List<Notifikasjon> {
                new Notifikasjon {
                    Melding = "Bruker b akspterte invitasjonen til � hjelpe deg med oppgave F",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id,
                    NotifikasjonsType_id = 1,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Du ble herved invitert til oppgave F hvor bruker A trenger hjelp",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id,
                    NotifikasjonsType_id = 2,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Du har blitt nevnt i kommentart d",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id,
                    NotifikasjonsType_id = 2,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Tiden for din oppgave F er i ferd med � l�pe ut",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id,
                    NotifikasjonsType_id = 3,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Bruker c ville ikke hjelpe deg med oppgave F",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id,
                    NotifikasjonsType_id = 2,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Fristen for oppgave F har dessverre g�tt ut, gj�r noe kjapt",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jpolden").Bruker_id,
                    NotifikasjonsType_id = 4,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Du har brukt for lang tid p� Oppgave F, venligst s�k hjelp viss du m�",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "pholm").Bruker_id,
                    NotifikasjonsType_id = 3,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Du har brukt 4 timer mindre enn planlagt forrige uke.",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id,
                    NotifikasjonsType_id = 3,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Du har v�rt meget flink",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "jaasgaard").Bruker_id,
                    NotifikasjonsType_id = 1,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Du har fulf�rt oppgave F, good job.",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    NotifikasjonsType_id = 1,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Din fullf�relse av oppgave ble ikke godkjent",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id,
                    NotifikasjonsType_id = 4,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Du har to m�ter p� samme tid Mr/mrs. vennligst avlys eller utsett",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "hhansen").Bruker_id,
                    NotifikasjonsType_id = 4,
                    Vist = false
                },
                new Notifikasjon {
                    Melding = "Svarfristen p� invitasjonen til oppgave F er g�tt ut p� dato",
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "aaskoy").Bruker_id,
                    NotifikasjonsType_id = 4,
                    Vist = false
                },
            };
            notifikasjon.ForEach(element => context.Notifikasjoner.AddOrUpdate(notifikasjoner => notifikasjoner.Melding, element));
            context.SaveChanges();
            
        }
    }
}
