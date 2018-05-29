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
    public class CompositionsController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Compositions
        public ActionResult Index()
        {
            return View(db.Composition.ToList());
        }

        // GET: Compositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composition composition = db.Composition.Find(id);
            if (composition == null)
            {
                return HttpNotFound();
            }
            return View(composition);
        }

        // GET: Compositions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Compositions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Composition,Titre_Composition,Annee,Composante_Composition")] Composition composition)
        {
            if (ModelState.IsValid)
            {
                db.Composition.Add(composition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(composition);
        }

        // GET: Compositions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composition composition = db.Composition.Find(id);
            if (composition == null)
            {
                return HttpNotFound();
            }
            return View(composition);
        }

        // POST: Compositions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Composition,Titre_Composition,Annee,Composante_Composition")] Composition composition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(composition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(composition);
        }

        // GET: Compositions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composition composition = db.Composition.Find(id);
            if (composition == null)
            {
                return HttpNotFound();
            }
            return View(composition);
        }

        // POST: Compositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Composition composition = db.Composition.Find(id);
            db.Composition.Remove(composition);
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
