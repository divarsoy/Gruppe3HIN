namespace SysUt14Gr03.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SysUt14Gr03.Models;

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
                    RettighetNavn = "Brukeradmin",
                    Brukere = new List<Bruker>()
                },
                new Rettighet {
                    RettighetNavn = "Prosjektleder",
                    Brukere = new List<Bruker>()
                },
                new Rettighet {
                    RettighetNavn = "Utvikler",
                    Brukere = new List<Bruker>()

                }
            };

            rettigheter.ForEach(element => context.Rettigheter.AddOrUpdate(element));
            context.SaveChanges();

            var brukere = new List<Bruker> {
                new Bruker {
                    Etternavn = "Martinsen",
                    Fornavn = "Lars",
                    Brukernavn = "lmartinsen",
                    Epost = "lmartinsen@gmail.com",
                    Passord = "Passord må krypteres!",
                    IM = "lmartinsen",
                    Token = "Må generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    Rettigheter = new List<Rettighet>()
                },
                new Bruker {
                    Etternavn = "Larsen",
                    Fornavn = "Martin",
                    Brukernavn = "mlarsen",
                    Epost = "mlarsen@gmail.com",
                    Passord = "Passord må krypteres!",
                    IM = "mlarsen",
                    Token = "Må generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    Rettigheter = new List<Rettighet>()
                },
                new Bruker {
                    Etternavn = "Hansen",
                    Fornavn = "Heidi",
                    Brukernavn = "hhansen",
                    Epost = "hhansen@gmail.com",
                    Passord = "Passord må krypteres!",
                    IM = "hhansen",
                    Token = "Må generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    Rettigheter = new List<Rettighet>()
                },
                new Bruker {
                    Etternavn = "Askøy",
                    Fornavn = "Anette",
                    Brukernavn = "aaskoy",
                    Epost = "aaskoy@gmail.com",
                    Passord = "Passord må krypteres!",
                    IM = "aaskoy",
                    Token = "Må generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    Rettigheter = new List<Rettighet>()
                },
                new Bruker {
                    Etternavn = "Jan",
                    Fornavn = "Polden",
                    Brukernavn = "jpolden",
                    Epost = "jpolden@gmail.com",
                    Passord = "Passord må krypteres!",
                    IM = "jpolden",
                    Token = "Må generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    Rettigheter = new List<Rettighet>()
                },
                new Bruker {
                    Etternavn = "Holm",
                    Fornavn = "Pernille",
                    Brukernavn = "pholm",
                    Epost = "pholm@gmail.com",
                    Passord = "Passord må krypteres!",
                    IM = "pholm",
                    Token = "Må generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                    Rettigheter = new List<Rettighet>()
                }
            };

            brukere.ForEach(element => context.Brukere.AddOrUpdate(bruker => bruker.Etternavn, element));
            context.SaveChanges();

            var brukerPreferanser = new List<BrukerPreferanse> {
                new BrukerPreferanse {
                    EpostTeam = false,
                    EpostProsjekt = false,
                    EpostOppgave = false,
                    EpostKommentar = false,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "lmartinsen" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = false,
                    EpostProsjekt = false,
                    EpostOppgave = false,
                    EpostKommentar = false,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "mlarsen" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = false,
                    EpostProsjekt = false,
                    EpostOppgave = false,
                    EpostKommentar = false,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "hhansen" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = false,
                    EpostProsjekt = false,
                    EpostOppgave = false,
                    EpostKommentar = false,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "aaskoy" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = false,
                    EpostProsjekt = false,
                    EpostOppgave = false,
                    EpostKommentar = false,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "jpolden" ).Bruker_id
                },
                new BrukerPreferanse {
                    EpostTeam = false,
                    EpostProsjekt = false,
                    EpostOppgave = false,
                    EpostKommentar = false,
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "pholm" ).Bruker_id
                }
            };

            brukerPreferanser.ForEach(element => context.BrukerPreferanser.AddOrUpdate(brukerpreferanse => brukerpreferanse.Bruker_id, element));
            context.SaveChanges();

            Rettighet BrukerRettighetUtvikler = context.Rettigheter.FirstOrDefault(rettighet => rettighet.RettighetNavn == "Utvikler");
            Rettighet BrukerRettighetProsjektleder = context.Rettigheter.FirstOrDefault(rettighet => rettighet.RettighetNavn == "Prosjektleder");
            Rettighet BrukerRettighetBrukeradmin = context.Rettigheter.FirstOrDefault(rettighet => rettighet.RettighetNavn == "BrukerAdmin");


            Bruker lmartinsen = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "lmartinsen");
            lmartinsen.Rettigheter.Add(BrukerRettighetBrukeradmin);

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

            var prosjekter = new List<Prosjekt> {
                new Prosjekt {
                    Navn = "Rød Elv",
                    Aktiv = true,
                    StartDato = DateTime.Now,
                    SluttDato = DateTime.Now.AddMonths(3),
                    Opprettet = DateTime.Now,
                    Team_id = context.Teams.FirstOrDefault(team => team.Navn == "Alpha").Team_id,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    Oppgaver = new List<Oppgave>()
                },
                new Prosjekt {
                    Navn = "Blå spurv",
                    Aktiv = true,
                    StartDato = DateTime.Now,
                    SluttDato = DateTime.Now.AddMonths(4),
                    Opprettet = DateTime.Now,
                    Team_id = context.Teams.FirstOrDefault(team => team.Navn == "Bravo").Team_id,
                    Bruker_id = context.Brukere.FirstOrDefault(bruker => bruker.Brukernavn == "mlarsen").Bruker_id,
                    Oppgaver = new List<Oppgave>()
                },
                new Prosjekt {
                    Navn = "Grå ulv",
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
                    Navn = "Startet",
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

            /*
            var oppgaver = new List <Oppgave> {
            new Oppgave {
                Tittel = "Opprette notat",
                UserStory = "Som bruker ønsker jeg å kunne opprette et notat",
                Krav = "Notatet skal ikke overgå en side",
                Estimat = 3,
                BruktTid = 0,
                Aktiv = true,
                Opprettet = DateTime.Now,
                Prosjekt_id = context.Prosjekter.FirstOrDefault(prosjekt => prosjekt.Navn == "Rød Elv").Prosjekt_id,
                OppgaveGruppe_id = 

            }
             
        }
            */
            /*
             * var brukerRettigheter = new List<BrukerRettigheter>
            {
                new BrukerRettigheter {
                    Bruker_id = brukere.Single(bruker => bruker.Brukernavn == "lmartinsen").Bruker_id, 
                    Rettighet_id = rettigheter.Single(rettighet => rettighet.RettighetNavn == "Utvikler").Rettighet_id
                }
            };

*/
            /*
            //Setter opp rettigheter
            Rettighet rettighetBrukerAdmin = new Rettighet { RettighetNavn = "Brukeradmin" };
            Rettighet rettighetProsjektLeder = new Rettighet { RettighetNavn = "Prosjektleder" };
            Rettighet rettighetUtvikler = new Rettighet { RettighetNavn = "Utvikler" };

            context.Rettigheter.AddOrUpdate<Rettighet>(rettighetBrukerAdmin);
            context.Rettigheter.AddOrUpdate<Rettighet>(rettighetProsjektLeder);
            context.Rettigheter.AddOrUpdate<Rettighet>(rettighetUtvikler);
            context.SaveChanges();

            var brukere = new List<Bruker>();
            int antallBrukere = 6;

            //Genererer brukerne
            for (int i = 0; i < antallBrukere; i++)
            {

                Bruker bruker = new Bruker
                {
                    Etternavn = FakeO.Name.Last(),
                    Fornavn = FakeO.Name.First(),
                    Brukernavn = FakeO.Internet.UserName(),
                    Epost = FakeO.Internet.Email(),
                    Passord = "Passord må krypteres!",
                    IM = FakeO.Internet.UserName(),
                    Token = "Må generere Token!",
                    Aktivert = true,
                    Aktiv = true,
                    opprettet = DateTime.Now,
                };
                //bruker.Rettigheter.Add(rettighetUtvikler);
                bruker.BrukerPreferanser.Add(new BrukerPreferanse
                {
                    EpostTeam = false,
                    EpostProsjekt = false,
                    EpostOppgave = false,
                    EpostKommentar = false
                });
                context.Brukere.Add(bruker);
                //base.Seed(context);
                context.SaveChanges();
            }
 * */
        }
    }
}
