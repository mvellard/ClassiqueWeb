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
    public class ComposersController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Composers
        public ActionResult Index(int? code)
        {
            var composer = db.Composer.Include(c => c.Musicien).Include(c => c.Oeuvre).Where(m => m.Code_Musicien.Equals(code));
            
            return View(composer.ToList());
        }

        // GET: Composers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composer composer = db.Composer.Find(id);
            if (composer == null)
            {
                return HttpNotFound();
            }
            return View(composer);
        }

        // GET: Composers/Create
        public ActionResult Create()
        {
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien");
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre");
            return View();
        }

        // POST: Composers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Composer,Code_Musicien,Code_Oeuvre")] Composer composer)
        {
            if (ModelState.IsValid)
            {
                db.Composer.Add(composer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien", composer.Code_Musicien);
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre", composer.Code_Oeuvre);
            return View(composer);
        }

        // GET: Composers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composer composer = db.Composer.Find(id);
            if (composer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien", composer.Code_Musicien);
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre", composer.Code_Oeuvre);
            return View(composer);
        }

        // POST: Composers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Composer,Code_Musicien,Code_Oeuvre")] Composer composer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(composer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien", composer.Code_Musicien);
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre", composer.Code_Oeuvre);
            return View(composer);
        }

        // GET: Composers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composer composer = db.Composer.Find(id);
            if (composer == null)
            {
                return HttpNotFound();
            }
            return View(composer);
        }

        // POST: Composers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Composer composer = db.Composer.Find(id);
            db.Composer.Remove(composer);
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
