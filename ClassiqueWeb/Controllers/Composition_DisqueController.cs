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
    public class Composition_DisqueController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Composition_Disque
        public ActionResult Index()
        {
            var composition_Disque = db.Composition_Disque.Include(c => c.Disque).Include(c => c.Enregistrement);
            return View(composition_Disque.ToList());
        }

        // GET: Composition_Disque/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composition_Disque composition_Disque = db.Composition_Disque.Find(id);
            if (composition_Disque == null)
            {
                return HttpNotFound();
            }
            return View(composition_Disque);
        }

        // GET: Composition_Disque/Create
        public ActionResult Create()
        {
            ViewBag.Code_Disque = new SelectList(db.Disque, "Code_Disque", "Reference_Album");
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre");
            return View();
        }

        // POST: Composition_Disque/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Contenir,Code_Disque,Code_Morceau,Position")] Composition_Disque composition_Disque)
        {
            if (ModelState.IsValid)
            {
                db.Composition_Disque.Add(composition_Disque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Disque = new SelectList(db.Disque, "Code_Disque", "Reference_Album", composition_Disque.Code_Disque);
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", composition_Disque.Code_Morceau);
            return View(composition_Disque);
        }

        // GET: Composition_Disque/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composition_Disque composition_Disque = db.Composition_Disque.Find(id);
            if (composition_Disque == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Disque = new SelectList(db.Disque, "Code_Disque", "Reference_Album", composition_Disque.Code_Disque);
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", composition_Disque.Code_Morceau);
            return View(composition_Disque);
        }

        // POST: Composition_Disque/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Contenir,Code_Disque,Code_Morceau,Position")] Composition_Disque composition_Disque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(composition_Disque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Disque = new SelectList(db.Disque, "Code_Disque", "Reference_Album", composition_Disque.Code_Disque);
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", composition_Disque.Code_Morceau);
            return View(composition_Disque);
        }

        // GET: Composition_Disque/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Composition_Disque composition_Disque = db.Composition_Disque.Find(id);
            if (composition_Disque == null)
            {
                return HttpNotFound();
            }
            return View(composition_Disque);
        }

        // POST: Composition_Disque/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Composition_Disque composition_Disque = db.Composition_Disque.Find(id);
            db.Composition_Disque.Remove(composition_Disque);
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
