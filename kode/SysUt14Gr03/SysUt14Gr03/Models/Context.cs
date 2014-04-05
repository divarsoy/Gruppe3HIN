using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SysUt14Gr03.Models
{
    public class Context : DbContext
    {
        public Context() : base("sysUt14Gr03") 
        {
        }

        public DbSet<Bruker> Brukere { get; set; }
        public DbSet<BrukerPreferanse> BrukerPreferanser { get; set; }
        public DbSet<Kommentar> Kommentarer { get; set; }
        public DbSet<Logg> Logger { get; set; }
        public DbSet<Moete> Moeter { get; set; }
        public DbSet<Oppgave> Oppgaver { get; set; }
        public DbSet<OppgaveGruppe> OppgaveGrupper { get; set; }
        public DbSet<Prioritering> Prioriteringer { get; set; }
        public DbSet<Prosjekt> Prosjekter { get; set; }
        public DbSet<Rettighet> Rettigheter { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Notifikasjon> Notifikasjoner { get; set; }
        public DbSet<NotifikasjonsType> NotifikasjonsType { get; set; }
        public DbSet<Time> Timer { get; set; }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Mange til mange forhold Teams <=> Brukere
            modelBuilder.Entity<Bruker>()
                .HasMany(t => t.Teams)
                .WithMany(t => t.Brukere)
                .Map(m =>
                {
                    m.MapLeftKey("Bruker_id");
                    m.MapRightKey("Team_id");
                    m.ToTable("BrukerTeams");
                });


            // Mange til mange forhold Moeter <=> Brukere
            modelBuilder.Entity<Bruker>()
                .HasMany(t => t.Moeter)
                .WithMany(t => t.Brukere)
                .Map(m =>
                {
                    m.MapLeftKey("Moete_id");
                    m.MapRightKey("Bruker_id");
                    m.ToTable("BrukerMoeter");
                });

            // Mange til mange forhold Oppgaver <=> Brukere
            modelBuilder.Entity<Oppgave>()
                .HasMany(t => t.Brukere)
                .WithMany(t => t.Oppgaver)
                .Map(m =>
                {
                    m.MapLeftKey("Oppgave_id");
                    m.MapRightKey("Bruker_id");
                    m.ToTable("BrukerOppgaver");
                });
            // Mange til mange forhold Brukere <=> Rettigheter
            modelBuilder.Entity<Rettighet>()
                .HasMany(t => t.Brukere)
                .WithMany(t => t.Rettigheter)
                .Map(m =>
                {
                    m.MapLeftKey("Rettighet_id");
                    m.MapRightKey("Bruker_id");
                    m.ToTable("BrukerRettigheter");
                });

            base.OnModelCreating(modelBuilder);
        }   
    }
}