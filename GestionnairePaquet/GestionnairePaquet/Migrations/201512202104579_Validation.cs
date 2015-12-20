namespace GestionnairePaquet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Societe", "Nom", c => c.String(nullable: false));
            AlterColumn("dbo.Societe", "Adresse", c => c.String(nullable: false));
            AlterColumn("dbo.Societe", "CodePostal", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Societe", "Ville", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Societe", "Ville", c => c.String());
            AlterColumn("dbo.Societe", "CodePostal", c => c.String());
            AlterColumn("dbo.Societe", "Adresse", c => c.String());
            AlterColumn("dbo.Societe", "Nom", c => c.String());
        }
    }
}
