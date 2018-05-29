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
    public class EnregistrementsController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Enregistrements
        public ActionResult Index()
        {
            return View(db.Enregistrement.ToList());
        }

        // GET: Enregistrements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enregistrement enregistrement = db.Enregistrement.Find(id);
            if (enregistrement == null)
            {
                return HttpNotFound();
            }
            return View(enregistrement);
        }

        // GET: Enregistrements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enregistrements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Morceau,Titre,Code_Composition,Nom_de_Fichier,Duree,Duree_Seconde,Prix,Extrait")] Enregistrement enregistrement)
        {
            if (ModelState.IsValid)
            {
                db.Enregistrement.Add(enregistrement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enregistrement);
        }

        // GET: Enregistrements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enregistrement enregistrement = db.Enregistrement.Find(id);
            if (enregistrement == null)
            {
                return HttpNotFound();
            }
            return View(enregistrement);
        }

        // POST: Enregistrements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Morceau,Titre,Code_Composition,Nom_de_Fichier,Duree,Duree_Seconde,Prix,Extrait")] Enregistrement enregistrement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enregistrement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enregistrement);
        }

        // GET: Enregistrements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enregistrement enregistrement = db.Enregistrement.Find(id);
            if (enregistrement == null)
            {
                return HttpNotFound();
            }
            return View(enregistrement);
        }

        // POST: Enregistrements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enregistrement enregistrement = db.Enregistrement.Find(id);
            db.Enregistrement.Remove(enregistrement);
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
