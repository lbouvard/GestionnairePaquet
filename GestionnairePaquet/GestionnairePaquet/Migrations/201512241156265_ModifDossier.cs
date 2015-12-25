namespace GestionnairePaquet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifDossier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dossier", "EstDossier", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dossier", "EstDossier");
        }
    }
}
