namespace SysUt14Gr03.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedBrukerPrefs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Brukers", "BrukerPreferanse_Preferanse_id", "dbo.BrukerPreferanses");
            DropIndex("dbo.Brukers", new[] { "BrukerPreferanse_Preferanse_id" });
            AddColumn("dbo.BrukerPreferanses", "Bruker_id", c => c.Int(nullable: false));
            CreateIndex("dbo.BrukerPreferanses", "Bruker_id");
            AddForeignKey("dbo.BrukerPreferanses", "Bruker_id", "dbo.Brukers", "Bruker_id");
            DropColumn("dbo.Brukers", "brukerPreferanse_id");
            DropColumn("dbo.Brukers", "BrukerPreferanse_Preferanse_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Brukers", "BrukerPreferanse_Preferanse_id", c => c.Int());
            AddColumn("dbo.Brukers", "brukerPreferanse_id", c => c.Int(nullable: false));
            DropForeignKey("dbo.BrukerPreferanses", "Bruker_id", "dbo.Brukers");
            DropIndex("dbo.BrukerPreferanses", new[] { "Bruker_id" });
            DropColumn("dbo.BrukerPreferanses", "Bruker_id");
            CreateIndex("dbo.Brukers", "BrukerPreferanse_Preferanse_id");
            AddForeignKey("dbo.Brukers", "BrukerPreferanse_Preferanse_id", "dbo.BrukerPreferanses", "Preferanse_id");
        }
    }
}
