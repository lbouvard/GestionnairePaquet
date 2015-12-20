namespace GestionnairePaquet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UtilisateurSocieteId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Membre_ID", "dbo.Societe");
            DropIndex("dbo.AspNetUsers", new[] { "Membre_ID" });
            AddColumn("dbo.AspNetUsers", "SocieteId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Membre_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Membre_ID", c => c.Int());
            DropColumn("dbo.AspNetUsers", "SocieteId");
            CreateIndex("dbo.AspNetUsers", "Membre_ID");
            AddForeignKey("dbo.AspNetUsers", "Membre_ID", "dbo.Societe", "ID");
        }
    }
}
