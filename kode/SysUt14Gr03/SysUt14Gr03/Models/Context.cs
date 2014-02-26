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
        public DbSet<Gruppe> Grupper { get; set; }
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
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Mange til mange forhold Teams <=> Brukere
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Brukere)
                .WithMany(t => t.Teams)
                .Map(m =>
                {
                    m.MapLeftKey("Bruker_id");
                    m.MapRightKey("team_id");
                    m.ToTable("BrukerTeamMapping");
                });

            // Mange til mange forhold Teams <=> Grupper
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Grupper)
                .WithMany(t => t.Teams)
                .Map(m =>
                {
                    m.MapLeftKey("gruppe_id");
                    m.MapRightKey("team_id");
                    m.ToTable("GruppeTeamMapping");
                });

            // Mange til mange forhold Moeter <=> Brukere
            modelBuilder.Entity<Moete>()
                .HasMany(t => t.Brukere)
                .WithMany(t => t.Moeter)
                .Map(m =>
                {
                    m.MapLeftKey("moete_id");
                    m.MapRightKey("bruker_id");
                    m.ToTable("BrukerMoeteMapping");
                });

            // Mange til mange forhold Oppgaver <=> Brukere
            modelBuilder.Entity<Oppgave>()
                .HasMany(t => t.Brukere)
                .WithMany(t => t.Oppgaver)
                .Map(m =>
                {
                    m.MapLeftKey("oppgave_id");
                    m.MapRightKey("bruker_id");
                    m.ToTable("BrukerOppgaveMapping");
                });

            base.OnModelCreating(modelBuilder);
        }   
    }
}