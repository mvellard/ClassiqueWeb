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
    public class Composition_OeuvreController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Composition_Oeuvre
        public ActionResult Index()
        {
            var composition_Oeuvre = db.Composition_Oeuvre.Include(c => c.Composition).Include(c => c.Oeuvre);
            return View(composition_Oeuvre.ToList());
        }

        // GET: Composition_Oeuvre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composition_Oeuvre composition_Oeuvre = db.Composition_Oeuvre.Find(id);
            if (composition_Oeuvre == null)
            {
                return HttpNotFound();
            }
            return View(composition_Oeuvre);
        }

        // GET: Composition_Oeuvre/Create
        public ActionResult Create()
        {
            ViewBag.Code_Composition = new SelectList(db.Composition, "Code_Composition", "Titre_Composition");
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre");
            return View();
        }

        // POST: Composition_Oeuvre/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Composer_Oeuvre,Code_Oeuvre,Code_Composition")] Composition_Oeuvre composition_Oeuvre)
        {
            if (ModelState.IsValid)
            {
                db.Composition_Oeuvre.Add(composition_Oeuvre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Composition = new SelectList(db.Composition, "Code_Composition", "Titre_Composition", composition_Oeuvre.Code_Composition);
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre", composition_Oeuvre.Code_Oeuvre);
            return View(composition_Oeuvre);
        }

        // GET: Composition_Oeuvre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composition_Oeuvre composition_Oeuvre = db.Composition_Oeuvre.Find(id);
            if (composition_Oeuvre == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Composition = new SelectList(db.Composition, "Code_Composition", "Titre_Composition", composition_Oeuvre.Code_Composition);
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre", composition_Oeuvre.Code_Oeuvre);
            return View(composition_Oeuvre);
        }

        // POST: Composition_Oeuvre/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Composer_Oeuvre,Code_Oeuvre,Code_Composition")] Composition_Oeuvre composition_Oeuvre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(composition_Oeuvre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Composition = new SelectList(db.Composition, "Code_Composition", "Titre_Composition", composition_Oeuvre.Code_Composition);
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre", composition_Oeuvre.Code_Oeuvre);
            return View(composition_Oeuvre);
        }

        // GET: Composition_Oeuvre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composition_Oeuvre composition_Oeuvre = db.Composition_Oeuvre.Find(id);
            if (composition_Oeuvre == null)
            {
                return HttpNotFound();
            }
            return View(composition_Oeuvre);
        }

        // POST: Composition_Oeuvre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Composition_Oeuvre composition_Oeuvre = db.Composition_Oeuvre.Find(id);
            db.Composition_Oeuvre.Remove(composition_Oeuvre);
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
