using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionnairePaquet.Models;
using Microsoft.AspNet.Identity.Owin;

namespace GestionnairePaquet.Controllers
{
    public class AdminController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Societes.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Societe societe = db.Societes.Find(id);
            if (societe == null)
            {
                return HttpNotFound();
            }
            return View(societe);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nom,Adresse,CodePostal,Ville")] Societe societe)
        {
            if (ModelState.IsValid)
            {
                db.Societes.Add(societe);
                db.SaveChanges();

                Success(String.Format("La société {0} a été créée avec succès.", societe.Nom), true);
                return RedirectToAction("Index");
            }

            return View(societe);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Societe societe = db.Societes.Find(id);
            if (societe == null)
            {
                return HttpNotFound();
            }
            return View(societe);
        }

        // POST: Admin/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nom,Adresse,CodePostal,Ville")] Societe societe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(societe).State = EntityState.Modified;
                db.SaveChanges();

                Success(string.Format("La société {0} a été modifiée avec succès.", societe.Nom), true);

                return RedirectToAction("Index");
            }
            return View(societe);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Societe societe = db.Societes.Find(id);
            if (societe == null)
            {
                return HttpNotFound();
            }
            return View(societe);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Societe societe = db.Societes.Find(id);
            db.Societes.Remove(societe);
            db.SaveChanges();

            Success(string.Format("La société {0} a été supprimée de la base.", societe.Nom), true);

            return RedirectToAction("Index");
        }

        // GET: Admin/Comptes/5
        public ActionResult Comptes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var utilisateurs = db.Users.Where(u => u.SocieteId == id).ToList();
            if (utilisateurs == null)
            {
                return HttpNotFound();
            }
            return View(utilisateurs);
        }

        // GET: Admin/CompteDelete/5
        public ActionResult CompteDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var utilisateur = db.Users.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Admin/CompteDelete/5
        [HttpPost, ActionName("CompteDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CompteDeleteConfirmed(string id)
        {
            var utilisateur = db.Users.Find(id);
            var nom = utilisateur.UserName;

            db.Users.Remove(utilisateur);
            db.SaveChanges();

            Success(string.Format("Le compte {0} a été supprimée de la base.", nom), true);

            return RedirectToAction("Comptes", new { Id = utilisateur.SocieteId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
