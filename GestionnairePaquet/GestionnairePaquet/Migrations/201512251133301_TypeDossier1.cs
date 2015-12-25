namespace GestionnairePaquet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeDossier1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dossier", "TypeDossier", c => c.Int());
            DropColumn("dbo.Dossier", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dossier", "Type", c => c.Int());
            DropColumn("dbo.Dossier", "TypeDossier");
        }
    }
}
