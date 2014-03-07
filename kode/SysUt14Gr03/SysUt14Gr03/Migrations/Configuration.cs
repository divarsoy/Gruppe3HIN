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
