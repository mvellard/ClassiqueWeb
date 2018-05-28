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
    public class AbonnesController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Abonnes
        public ActionResult Index()
        {
            var abonne = db.Abonne.Include(a => a.Pays);
            return View(abonne.ToList());
        }

        // GET: Abonnes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonne abonne = db.Abonne.Find(id);
            if (abonne == null)
            {
                return HttpNotFound();
            }
            return View(abonne);
        }

        // GET: Abonnes/Create
        public ActionResult Create()
        {
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays");
            return View();
        }

        // POST: Abonnes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Abonne,Nom_Abonne,Login,Password,Adresse,Ville,Code_Postal,Code_Pays,Email,UserId,Credit,Prenom_Abonne")] Abonne abonne)
        {
            if (ModelState.IsValid)
            {
                db.Abonne.Add(abonne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays", abonne.Code_Pays);
            return View(abonne);
        }

        // GET: Abonnes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonne abonne = db.Abonne.Find(id);
            if (abonne == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays", abonne.Code_Pays);
            return View(abonne);
        }

        // POST: Abonnes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Abonne,Nom_Abonne,Login,Password,Adresse,Ville,Code_Postal,Code_Pays,Email,UserId,Credit,Prenom_Abonne")] Abonne abonne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abonne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Pays = new SelectList(db.Pays, "Code_Pays", "Nom_Pays", abonne.Code_Pays);
            return View(abonne);
        }

        // GET: Abonnes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abonne abonne = db.Abonne.Find(id);
            if (abonne == null)
            {
                return HttpNotFound();
            }
            return View(abonne);
        }

        // POST: Abonnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Abonne abonne = db.Abonne.Find(id);
            db.Abonne.Remove(abonne);
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
