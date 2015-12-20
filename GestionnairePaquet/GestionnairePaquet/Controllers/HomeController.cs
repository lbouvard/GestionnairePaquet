using GestionnairePaquet.Migrations;
using GestionnairePaquet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionnairePaquet.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            /*using (var context = new ApplicationDbContext())
            {
                var test = context.Produits.FirstOrDefault();

                Produit produit1 = new Produit
                {
                    Nom = "PlastProdApp",
                    Categorie = "Commercial",
                    Description = "Gestion commercial mobile",
                    Actif = true,
                    ListeVersion = null
                };

                context.Produits.Add(produit1);
                //applique les changements
                context.SaveChanges();
            }*/

            return View();
        }

        public ActionResult APropos()
        {
            ViewBag.Message = "A propos de nous...";

            return View();
        }

    }
}