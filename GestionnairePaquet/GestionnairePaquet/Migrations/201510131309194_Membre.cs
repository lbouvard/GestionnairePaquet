namespace GestionnairePaquet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Membre : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Societes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Adresse = c.String(),
                        CodePostal = c.String(),
                        Ville = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Categorie = c.String(),
                        Description = c.String(),
                        Icone = c.String(),
                        Actif = c.Boolean(nullable: false),
                        Societe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Societes", t => t.Societe_Id)
                .Index(t => t.Societe_Id);
            
            CreateTable(
                "dbo.Versions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(),
                        ChangeLog = c.String(),
                        Logiciel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produits", t => t.Logiciel_Id)
                .Index(t => t.Logiciel_Id);
            
            CreateTable(
                "dbo.Fichiers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Extension = c.String(),
                        DateVersion = c.DateTimeOffset(nullable: false, precision: 7),
                        Taille = c.Int(nullable: false),
                        Chemin = c.String(),
                        Version_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Versions", t => t.Version_Id)
                .Index(t => t.Version_Id);
            
            AddColumn("dbo.AspNetUsers", "Membre_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Membre_Id");
            AddForeignKey("dbo.AspNetUsers", "Membre_Id", "dbo.Societes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Membre_Id", "dbo.Societes");
            DropForeignKey("dbo.Produits", "Societe_Id", "dbo.Societes");
            DropForeignKey("dbo.Versions", "Logiciel_Id", "dbo.Produits");
            DropForeignKey("dbo.Fichiers", "Version_Id", "dbo.Versions");
            DropIndex("dbo.Fichiers", new[] { "Version_Id" });
            DropIndex("dbo.Versions", new[] { "Logiciel_Id" });
            DropIndex("dbo.Produits", new[] { "Societe_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Membre_Id" });
            DropColumn("dbo.AspNetUsers", "Membre_Id");
            DropTable("dbo.Fichiers");
            DropTable("dbo.Versions");
            DropTable("dbo.Produits");
            DropTable("dbo.Societes");
        }
    }
}
