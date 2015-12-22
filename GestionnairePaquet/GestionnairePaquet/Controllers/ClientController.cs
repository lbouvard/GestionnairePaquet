using GestionnairePaquet.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionnairePaquet.Controllers
{
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
            int SocieteId = 0;

            using (var db = new ApplicationDbContext())
            {

                SocieteId = db.Users.Find(UserId).SocieteId;

                var query = from p in db.Produits
                            where p.SocieteID == SocieteId
                            select p;

                liste = query.ToList();
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
                var query = from v in db.Versions.Include("Produit")
                            where v.ProduitID == id
                            select v;

                liste = query.ToList();
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
                ViewBag.ChangeLog = (from v in db.Versions
                                     where v.ID == id
                                     select v.ChangeLog).First();

                var query = from f in db.Fichiers.Include("Version")
                            where f.VersionID == id
                            select f;

                liste = query.ToList();
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
            string path = AppDomain.CurrentDomain.BaseDirectory + "Content/Fichiers/";
            return File(path + fichier, "text/plain", fichier);
            //return File("~/Content/Fichiers/" + fichier, System.Net.Mime.MediaTypeNames.Application.Octet);
        }

        // GET: Telechargement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Telechargement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Telechargement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Telechargement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Telechargement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Telechargement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Telechargement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
