namespace GestionnairePaquet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutDossier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dossier",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        ParentID = c.Int(nullable: false),
                        EstCree = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dossier");
        }
    }
}
