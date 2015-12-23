using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionnairePaquet.Models;
using GestionnairePaquet.Helpers;
using System.IO;

namespace GestionnairePaquet.Controllers
{

    public class Navigation
    {
        public int Id { get; set; }
        public string Nom { get; set; }
    }

    public class AdminController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GereCompte
        public ActionResult GereCompte()
        {
            return View(db.Societes.ToList());
        }

        // GET: GereFichier
        public ActionResult GereFichier(int id)
        {
            //init pour le dossier racine
            if( id == 1)
            {
                System.Web.HttpContext.Current.Session["historiqueNavigation"] = null;
            }

            var dossierEnCours = (from d in db.Dossiers where d.ID == id select d).FirstOrDefault();
            HistoriseNavigation(new Navigation { Id = dossierEnCours.ID, Nom = dossierEnCours.Nom });

            System.Web.HttpContext.Current.Session["niveauDossier"] = id.ToString();
             
            List<Dossier> liste = (from d in db.Dossiers
                                   where d.ParentID == id
                                   select d).ToList();
            return View(liste);
        }

        /// <summary>
        /// Permet de générer l'arboresence des dossiers (seulement lors de la première migration)
        /// </summary>
        /// <returns></returns>
        public ActionResult GenereArbo()
        {
            //racine
            string racine = "~/Content/";
            GenererDossier(-1, ref racine);

            return RedirectToAction("GereFichier", "Admin", new { id = 1 });
        }

        // POST: TraiteChargement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TraiteChargement(IEnumerable<HttpPostedFileBase> fichiers)
        {
            if (fichiers != null)
            {
                foreach (var fichier in fichiers)
                {
                    if (fichier != null && fichier.ContentLength > 0)
                    {
                        string pic = System.IO.Path.GetFileName(fichier.FileName);
                        string path = System.IO.Path.Combine(
                                               Server.MapPath("~/Content/Fichiers/"), pic);
                        // file is uploaded
                        fichier.SaveAs(path);
                    }
                }
            }
            // after successfully uploading redirect the user
            var id_encours = System.Web.HttpContext.Current.Session["sessionString"] as String;
            return RedirectToAction("GereFichier", "Admin", new { id = int.Parse(id_encours)});
        }

        /// <summary>
        /// Permet d'ajouter un dossier physiquement et en base
        /// </summary>
        /// <returns></returns>
        // POST: CreateDossier
        [HttpPost]
        public ActionResult CreateDossier()
        {
            int idDossierParent = int.Parse(System.Web.HttpContext.Current.Session["niveauDossier"] as string);

            //Création physique
            var path = Server.MapPath(GenererCheminPourDossier(Request.Form["nomDossier"]));
            Directory.CreateDirectory(path);

            //Enregistrement en base
            var dossier = new Dossier { Nom = Request.Form["nomDossier"].ToString(), ParentID = idDossierParent, EstCree = true };
            db.Dossiers.Add(dossier);
            db.SaveChanges();

            return RedirectToAction("GereFichier", "Admin", new { id = idDossierParent });
        }

        /// <summary>
        /// Permet de supprimer un dossier et tous ses enfants physiquement et en base.
        /// </summary>
        /// <param name="id">id du dossier ou du fichier</param>
        /// <returns></returns>
        public ActionResult DeleteFichier(int id)
        {
            int idDossierParent = int.Parse(System.Web.HttpContext.Current.Session["niveauDossier"] as string);

            //récupération du dossier ou fichier
            Dossier dossier = db.Dossiers.Find(id);

            if( dossier != null)
            {
                //suppression physique
                var path = Server.MapPath(GenererCheminPourDossier(dossier.Nom));
                Directory.Delete(path, true);

                //suppression en base
                SupprimerDossierRecursif(dossier.ID);
                db.Dossiers.Remove(dossier);
                db.SaveChanges();

                Success(string.Format("La dossier {0} a été supprimé avec succès.", dossier.Nom), true);
            }

            return RedirectToAction("GereFichier", "Admin", new { id = idDossierParent });
        }

        // GET: Admin/DetailsSociete/5
        public ActionResult DetailsSociete(int? id)
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

        // GET: Admin/CreateSociete
        public ActionResult CreateSociete()
        {
            return View();
        }

        // POST: Admin/CreateSociete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSociete([Bind(Include = "ID,Nom,Adresse,CodePostal,Ville")] Societe societe)
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

        // GET: Admin/EditSociete/5
        public ActionResult EditSociete(int? id)
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

        // POST: Admin/EditSociete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSociete([Bind(Include = "ID,Nom,Adresse,CodePostal,Ville")] Societe societe)
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

        // GET: Admin/DeleteSociete/5
        public ActionResult DeleteSociete(int? id)
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

        // POST: Admin/DeleteSociete/5
        [HttpPost, ActionName("DeleteSociete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSocieteConfirmed(int id)
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

        /// <summary>
        /// Fonction récursive qui permet de créer physiquement les dossiers
        /// </summary>
        /// <param name="id"></param>
        /// <param name="chemin_temp"></param>
        private void GenererDossier(int id, ref string chemin_temp)
        {
            //Sélection des dossiers du parent (id) 
            var liste = (from d in db.Dossiers
                        where d.ParentID == id
                        select d).ToList();

            if( liste.Count() > 0)
            {
                foreach( var el in liste)
                {
                    //Création du dossier
                    var path = Server.MapPath(chemin_temp + el.Nom);
                    Directory.CreateDirectory(path);

                    //Indique que le dossier est traité
                    var query = from d in db.Dossiers where d.ID == el.ID select d;
                    var dossier = query.FirstOrDefault();

                    if( dossier != null)
                    {
                        dossier.EstCree = true;
                        db.SaveChanges();
                    }

                    //Sauvegarde du nouveau chemin
                    chemin_temp += el.Nom + "/";

                    //on parcours en recursif les sous dossiers du dossier parent
                    GenererDossier(el.ID, ref chemin_temp);
                }

                //fin du chemin
                chemin_temp = chemin_temp.Substring(0, chemin_temp.LastIndexOf("/"));
                //dossier parent
                chemin_temp = chemin_temp.Substring(0, chemin_temp.LastIndexOf("/") + 1);
            }
            else
            {
                //fin du chemin
                chemin_temp = chemin_temp.Substring(0, chemin_temp.LastIndexOf("/"));
                //dossier parent
                chemin_temp = chemin_temp.Substring(0, chemin_temp.LastIndexOf("/") + 1);
            }
        }

        /// <summary>
        /// Permet de tracer la navigation client afin de positionner correctement le fil d'ariane et le chemin du dossier ou des fichiers
        /// </summary>
        /// <param name="element"></param>
        private void HistoriseNavigation(Navigation element)
        {
            List<Navigation> liste = (List<Navigation>)System.Web.HttpContext.Current.Session["historiqueNavigation"];

            if (liste != null)
            {
                var index = liste.FindIndex(item => item.Nom == element.Nom);
                
                if( index != -1)
                {
                    SupprimerFils(liste, index + 1);
                }
                else
                {
                    AjouterFil(liste, element);
                }
            }
            else
            {
                liste = new List<Navigation>();
                AjouterFil(liste, element);
                System.Web.HttpContext.Current.Session["historiqueNavigation"] = liste;
            }

            GenererFilAriane(liste);
        }

        /// <summary>
        /// Permet d'ajouter un niveau au fil d'arianne
        /// </summary>
        /// <param name="liste"></param>
        /// <param name="item"></param>
        private void AjouterFil(List<Navigation> liste, Navigation item)
        {
            liste.Add(item);
        }

        /// <summary>
        /// Permet de supprimer les niveaux supérieurs à la position en cours du fil d'arianne
        /// </summary>
        /// <param name="liste"></param>
        /// <param name="position"></param>
        private void SupprimerFils(List<Navigation> liste, int position)
        {
            liste.RemoveRange(position, liste.Count() - position);
        }

        /// <summary>
        /// Génére le fil d'ariane en html avec bootstrap
        /// </summary>
        /// <param name="liste"></param>
        private void GenererFilAriane(List<Navigation> liste)
        {
            FilArianeHelper fileAriane = new FilArianeHelper();

            foreach( var item in liste)
            {
                fileAriane.AddNode(Url.Action("GereFichier", "Admin", new { id = item.Id }, "http"), item.Nom);
            }

            //Génération html
            ViewBag.FilAriane = fileAriane.Output();
        }

        /// <summary>
        /// Permet de recontistuer le chemin physique du dossier ou fichier demandé
        /// </summary>
        /// <param name="nouveauDossier"></param>
        /// <returns></returns>
        private string GenererCheminPourDossier(string nouveauDossier)
        {
            string retour = "~/Content/";

            List<Navigation> liste = (List<Navigation>)System.Web.HttpContext.Current.Session["historiqueNavigation"];

            foreach(Navigation item in liste)
            {
                retour += item.Nom + "/";
            }

            retour += nouveauDossier;

            return retour;
        }

        /// <summary>
        /// Permet de supprimer en base les éléments fils d'un dossier à supprimer
        /// </summary>
        /// <param name="idOrigine"></param>
        private void SupprimerDossierRecursif(int idOrigine)
        {
            //Sélection des dossiers du parent (id) 
            var liste = (from d in db.Dossiers
                         where d.ParentID == idOrigine
                         select d).ToList();

            if (liste.Count() > 0)
            {
                foreach (var el in liste)
                {
                    //Suppression du dossier
                    var query = from d in db.Dossiers where d.ID == el.ID select d;
                    var dossier = query.FirstOrDefault();

                    if (dossier != null)
                    {
                        db.Dossiers.Remove(dossier);
                        db.SaveChanges();
                    }

                    //on parcours en recursif les sous dossiers du dossier parent
                    SupprimerDossierRecursif(el.ID);
                }
            }
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
