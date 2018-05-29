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
    public class DisquesController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Disques
        public ActionResult Index()
        {
            var disque = db.Disque.Include(d => d.Album);
            return View(disque.ToList());
        }

        // GET: Disques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disque disque = db.Disque.Find(id);
            if (disque == null)
            {
                return HttpNotFound();
            }
            return View(disque);
        }

        // GET: Disques/Create
        public ActionResult Create()
        {
            ViewBag.Code_Album = new SelectList(db.Album, "Code_Album", "Titre_Album");
            return View();
        }

        // POST: Disques/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Disque,Code_Album,Reference_Album,Reference_Disque")] Disque disque)
        {
            if (ModelState.IsValid)
            {
                db.Disque.Add(disque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Album = new SelectList(db.Album, "Code_Album", "Titre_Album", disque.Code_Album);
            return View(disque);
        }

        // GET: Disques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disque disque = db.Disque.Find(id);
            if (disque == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Album = new SelectList(db.Album, "Code_Album", "Titre_Album", disque.Code_Album);
            return View(disque);
        }

        // POST: Disques/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Disque,Code_Album,Reference_Album,Reference_Disque")] Disque disque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Album = new SelectList(db.Album, "Code_Album", "Titre_Album", disque.Code_Album);
            return View(disque);
        }

        // GET: Disques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disque disque = db.Disque.Find(id);
            if (disque == null)
            {
                return HttpNotFound();
            }
            return View(disque);
        }

        // POST: Disques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disque disque = db.Disque.Find(id);
            db.Disque.Remove(disque);
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
