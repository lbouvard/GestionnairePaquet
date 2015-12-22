namespace GestionnairePaquet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutIconeFichier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fichier", "Icone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fichier", "Icone");
        }
    }
}
