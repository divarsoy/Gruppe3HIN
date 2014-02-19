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
        }
    }
}
