using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassiqueWeb.Models;

namespace ClassiqueWeb.Controllers
{
    //pour restreindre l’accès
    //https://docs.microsoft.com/fr-fr/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api

    [Authorize]
    public class AchatsController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Achats
        public ActionResult Index()
        {
            var achat = db.Achat.Include(a => a.Abonne).Include(a => a.Enregistrement);
            return View(achat.ToList());
        }

        // GET: Achats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achat achat = db.Achat.Find(id);
            if (achat == null)
            {
                return HttpNotFound();
            }
            return View(achat);
        }

        // GET: Achats/Create
        public ActionResult Create()
        {
            ViewBag.Code_Abonne = new SelectList(db.Abonne, "Code_Abonne", "Nom_Abonne");
            ViewBag.Code_Enregistrement = new SelectList(db.Enregistrement, "Code_Morceau", "Titre");
            return View();
        }

        // POST: Achats/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Achat,Code_Enregistrement,Code_Abonne,Achat_Confirme")] Achat achat)
        {
            if (ModelState.IsValid)
            {
                db.Achat.Add(achat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Abonne = new SelectList(db.Abonne, "Code_Abonne", "Nom_Abonne", achat.Code_Abonne);
            ViewBag.Code_Enregistrement = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", achat.Code_Enregistrement);
            return View(achat);
        }

        // GET: Achats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achat achat = db.Achat.Find(id);
            if (achat == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Abonne = new SelectList(db.Abonne, "Code_Abonne", "Nom_Abonne", achat.Code_Abonne);
            ViewBag.Code_Enregistrement = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", achat.Code_Enregistrement);
            return View(achat);
        }

        // POST: Achats/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Achat,Code_Enregistrement,Code_Abonne,Achat_Confirme")] Achat achat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(achat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Abonne = new SelectList(db.Abonne, "Code_Abonne", "Nom_Abonne", achat.Code_Abonne);
            ViewBag.Code_Enregistrement = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", achat.Code_Enregistrement);
            return View(achat);
        }

        // GET: Achats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achat achat = db.Achat.Find(id);
            if (achat == null)
            {
                return HttpNotFound();
            }
            return View(achat);
        }

        // POST: Achats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Achat achat = db.Achat.Find(id);
            db.Achat.Remove(achat);
            db.SaveChanges();
            return RedirectToAction("Index");
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
