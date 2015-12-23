namespace GestionnairePaquet.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<GestionnairePaquet.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //Insertion des données de test
        protected override void Seed(GestionnairePaquet.Models.ApplicationDbContext context)
        {
            // Insertion de trois société
            var societes = new List<Societe>
            {
                new Societe {
                    Nom = "Societe1",
                    Adresse = "1 rue Alphonse Daudet",
                    CodePostal ="54600",
                    Ville = "Villers-Lès-Nancy",
                },
                new Societe {
                    Nom = "PlastProd",
                    Adresse = "1 rue du Bac",
                    CodePostal ="54000",
                    Ville = "Nancy",
                },
                new Societe {
                    Nom = "Arc",
                    Adresse = "1 boulevard Patton",
                    CodePostal ="75002",
                    Ville = "Paris",
                }
            };
            societes.ForEach(s => context.Societes.AddOrUpdate(p => p.Nom, s));
            context.SaveChanges();

            var produits = new List<Produit>
            {
                new Produit
                {
                    Nom = "PlastProdApp",
                    SocieteID = societes.Single(s => s.Nom == "PlastProd").ID,
                    Description = "Application de gestion commercial de la société PlastProd",
                    Actif = true,
                    Icone = "ic_plastprodapp.png",
                },
                new Produit
                {
                    Nom = "ImotepApp",
                    SocieteID = societes.Single(s => s.Nom == "Arc").ID,
                    Description = "Application de démonstration",
                    Actif = true,
                    Icone = "ic_imotepapp.png",
                }
            };
            produits.ForEach(p => context.Produits.AddOrUpdate( u => new { u.Nom, u.SocieteID } , p));
            context.SaveChanges();

            var versions = new List<Models.Version>
            {
                new Models.Version { Numero = "1.0", ChangeLog = "Version initiale de l'application", ProduitID = produits.Single(p => p.Nom == "PlastProdApp").ID },
                new Models.Version { Numero = "2.0", ChangeLog = "Graphismes améliorés", ProduitID = produits.Single(p => p.Nom == "PlastProdApp").ID }
            };
            versions.ForEach(v => context.Versions.AddOrUpdate(p => new { p.Numero, p.ProduitID }, v));
            context.SaveChanges();

            var fichiers = new List<Fichier>
            {
                new Fichier{ Nom = "PlastProdAppv1", Extension = "apk", Taille = 34500000, DateVersion = DateTimeOffset.Parse("2015-09-15 14:25:20"), VersionID = versions.Single(v => v.Numero == "1.0").ID, Icone = "ic_apk.png" },
                new Fichier{ Nom = "ChangeLogv1", Extension = "pdf", Taille = 230000, DateVersion = DateTimeOffset.Parse("2015-09-15 14:27:10"), VersionID = versions.Single(v => v.Numero == "1.0").ID, Icone = "ic_pdf.png"  },
                new Fichier{ Nom = "PlastProdAppv2", Extension = "apk", Taille = 31500000, DateVersion = DateTimeOffset.Parse("2015-12-15 08:17:20"), VersionID = versions.Single(v => v.Numero == "2.0").ID, Icone = "ic_apk.png"  },
                new Fichier{ Nom = "ChangeLogv2", Extension = "pdf", Taille = 230000, DateVersion = DateTimeOffset.Parse("2015-12-15 08:20:14"), VersionID = versions.Single(v => v.Numero == "2.0").ID, Icone = "ic_pdf.png"  }
            };
            fichiers.ForEach(f => context.Fichiers.AddOrUpdate(u => new { u.Nom, u.VersionID }, f));
            context.SaveChanges();

            //ajout de role admin et user
            if( !context.Roles.Any(r => r.Name == "Admin"))
            {
                context.Roles.Add(new IdentityRole()
                {
                    Name = "Admin"
                });
                context.Roles.Add(new IdentityRole()
                {
                    Name = "User"
                });
                context.SaveChanges();
            }


            //ajout du compte administrateur
            if( !context.Users.Any(u => u.UserName == "administrateur"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var compteadmin = new ApplicationUser
                {
                    UserName = "administrateur",
                    Email = "admin@flamingsoft.fr",
                    EmailConfirmed = true
                };

                IdentityResult resultat = manager.Create(compteadmin, "Uz28*Cesi");
                if (resultat.Succeeded == true)
                {
                    manager.AddToRole(compteadmin.Id, "Admin");
                }
                //context.SaveChanges();
            }

            //Génération d'une arborescence de test
            var dossiers = new List<Dossier>
            {
                new Dossier { Nom = "Fichiers", ParentID = -1 },
                new Dossier { Nom = "Arc", ParentID = 1 },
                new Dossier { Nom = "PlastProd", ParentID = 1 },
                new Dossier { Nom = "Societe1", ParentID = 1 },
                new Dossier { Nom = "ImotepApp", ParentID = 2 },
                new Dossier { Nom = "PlastProdApp", ParentID = 3 },
                new Dossier { Nom = "1.0", ParentID = 5 },
                new Dossier { Nom = "2.0", ParentID = 5 },
                new Dossier { Nom = "1.0", ParentID = 6 },
                new Dossier { Nom = "2.0", ParentID = 6 }
            };
            dossiers.ForEach(s => context.Dossiers.AddOrUpdate(p => new { p.Nom, p.ParentID }, s));
            context.SaveChanges();

        }
    }
}
