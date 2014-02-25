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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SysUt14Gr03.Models.Context context)
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
            context.Rettigheter.AddOrUpdate(
                rettighet => rettighet.RettighetNavn,
                new Rettighet { RettighetNavn = "Brukeradmin" },
                new Rettighet { RettighetNavn = "Prosjektleder" },
                new Rettighet { RettighetNavn = "Utvikler" }
                );

            SetBrukerPreferanser().ForEach(brukerPreferanse => context.BrukerPreferanser.Add(brukerPreferanse));
            SetBrukere().ForEach(bruker => context.Brukere.Add(bruker));
        }

        private static List<Bruker> SetBrukere()
        {
            var brukere = new List<Bruker>();
            int antallBrukere = 10;
            for (int i = 0; i<antallBrukere; i++) {
                brukere.Add(
                new Bruker
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
                }
                );
            }
            return brukere;
        }


        private static List<BrukerPreferanse> SetBrukerPreferanser()
        {
            var brukerPreferanser = new List<BrukerPreferanse> {
                new BrukerPreferanse 
                {
                    EpostTeam = true,
                    EpostProsjekt = true,
                    EpostOppgave = true,
                    EpostKommentar = true,
                    Bruker_id = 1,
                },
            };
            return brukerPreferanser;
        }
    }
}
