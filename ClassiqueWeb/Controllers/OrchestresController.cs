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
    public class OrchestresController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Orchestres
        public ActionResult Index()
        {
            return View(db.Orchestres.ToList());
        }

        // GET: Orchestres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orchestres orchestres = db.Orchestres.Find(id);
            if (orchestres == null)
            {
                return HttpNotFound();
            }
            return View(orchestres);
        }

        // GET: Orchestres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orchestres/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Orchestre,Nom_Orchestre")] Orchestres orchestres)
        {
            if (ModelState.IsValid)
            {
                db.Orchestres.Add(orchestres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orchestres);
        }

        // GET: Orchestres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orchestres orchestres = db.Orchestres.Find(id);
            if (orchestres == null)
            {
                return HttpNotFound();
            }
            return View(orchestres);
        }

        // POST: Orchestres/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Orchestre,Nom_Orchestre")] Orchestres orchestres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orchestres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orchestres);
        }

        // GET: Orchestres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orchestres orchestres = db.Orchestres.Find(id);
            if (orchestres == null)
            {
                return HttpNotFound();
            }
            return View(orchestres);
        }

        // POST: Orchestres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orchestres orchestres = db.Orchestres.Find(id);
            db.Orchestres.Remove(orchestres);
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
