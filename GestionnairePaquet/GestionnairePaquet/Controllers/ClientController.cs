using GestionnairePaquet.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionnairePaquet.Controllers
{
    [Authorize]
    public class ClientController : BaseController
    {
        /// <summary>
        /// Permet de choisir le produit disponible pour la société cliente
        /// </summary>
        /// <returns></returns>
        // GET: Telechargement
        public ActionResult Index()
        {
            List<Produit> liste = null;
            string UserId = User.Identity.GetUserId();

            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.Find(UserId);
                var societe = (from s in db.Societes where s.ID == user.SocieteId select s).FirstOrDefault();

                if( societe != null)
                {
                    System.Web.HttpContext.Current.Session["clientNomSociete"] = societe.Nom;

                    var query = from p in db.Produits
                                where p.SocieteID == user.SocieteId
                                select p;

                    liste = query.ToList();
                }
            }

            return View(liste);
        }

        /// <summary>
        /// Permet de récupérer les données des différentes versions
        /// </summary>
        /// <returns></returns>
        // GET: Version/1
        public ActionResult Version(int id)
        {
            List<Models.Version> liste = null;

            using (var db = new ApplicationDbContext())
            {
                var produit = db.Produits.Find(id);
                if (produit != null)
                {
                    System.Web.HttpContext.Current.Session["clientNomProduit"] = produit.Nom;
                }

                liste = (from v in db.Versions.Include("Produit")
                         where v.ProduitID == id
                         select v).ToList();
            }

            return View(liste);
        }

        /// <summary>
        /// Permet d'afficher les documents d'une version
        /// </summary>
        /// <returns></returns>
        // GET: AfficheFichiers/1
        public ActionResult AfficheFichiers(int id)
        {
            List<Fichier> liste = null;

            using (var db = new ApplicationDbContext())
            {
                var version = (from v in db.Versions where v.ID == id select v).FirstOrDefault();

                if( version != null)
                {
                    ViewBag.ChangeLog = version.ChangeLog;
                    System.Web.HttpContext.Current.Session["clientNomVersion"] = version.Numero;

                    var query = from f in db.Fichiers.Include("Version")
                                where f.VersionID == id
                                select f;

                    liste = query.ToList();
                }
            }

            return PartialView(liste);
        }

        /// <summary>
        /// Permet de télécharger un fichier
        /// </summary>
        /// <returns></returns>
        // GET: AfficheFichiers/1
        public ActionResult TelechargeFichier(string fichier)
        {
            string path = string.Format("{0}Content/Fichiers/{1}/{2}/{3}/", AppDomain.CurrentDomain.BaseDirectory,
                                        System.Web.HttpContext.Current.Session["clientNomSociete"].ToString(),
                                        System.Web.HttpContext.Current.Session["clientNomProduit"].ToString(),
                                        System.Web.HttpContext.Current.Session["clientNomVersion"].ToString());

            return File(path + fichier, "text/plain", fichier);
            //return File("~/Content/Fichiers/" + fichier, System.Net.Mime.MediaTypeNames.Application.Octet);
        }
    }
}
