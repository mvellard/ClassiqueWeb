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
    public class EditeursController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Editeurs
        public ActionResult Index()
        {
            var editeur = db.Editeur.Include(e => e.Pays);
            return View(editeur.ToList());
        }

        // GET: Editeurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editeur editeur = db.Editeur.Find(id);
            if (editeur == null)
            {
                return HttpNotFound();
            }
            return View(editeur);
        }

        // GET: Editeurs/Create
        public ActionResult Create()
        {
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays");
            return View();
        }

        // POST: Editeurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Editeur,Nom_Editeur,Code_Pays")] Editeur editeur)
        {
            if (ModelState.IsValid)
            {
                db.Editeur.Add(editeur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays", editeur.Code_Pays);
            return View(editeur);
        }

        // GET: Editeurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editeur editeur = db.Editeur.Find(id);
            if (editeur == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays", editeur.Code_Pays);
            return View(editeur);
        }

        // POST: Editeurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Editeur,Nom_Editeur,Code_Pays")] Editeur editeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays", editeur.Code_Pays);
            return View(editeur);
        }

        // GET: Editeurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Editeur editeur = db.Editeur.Find(id);
            if (editeur == null)
            {
                return HttpNotFound();
            }
            return View(editeur);
        }

        // POST: Editeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Editeur editeur = db.Editeur.Find(id);
            db.Editeur.Remove(editeur);
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
