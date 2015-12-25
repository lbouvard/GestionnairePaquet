namespace GestionnairePaquet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeDossier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dossier", "Type", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dossier", "Type");
        }
    }
}
