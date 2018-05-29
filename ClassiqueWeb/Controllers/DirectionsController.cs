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
    public class DirectionsController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Directions
        public ActionResult Index()
        {
            var direction = db.Direction.Include(d => d.Enregistrement).Include(d => d.Musicien).Include(d => d.Orchestres);
            return View(direction.ToList());
        }

        // GET: Directions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Direction.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }

        // GET: Directions/Create
        public ActionResult Create()
        {
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre");
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien");
            ViewBag.Code_Orchestre = new SelectList(db.Orchestres, "Code_Orchestre", "Nom_Orchestre");
            return View();
        }

        // POST: Directions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Direction,Code_Musicien,Code_Morceau,Code_Orchestre")] Direction direction)
        {
            if (ModelState.IsValid)
            {
                db.Direction.Add(direction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", direction.Code_Morceau);
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien", direction.Code_Musicien);
            ViewBag.Code_Orchestre = new SelectList(db.Orchestres, "Code_Orchestre", "Nom_Orchestre", direction.Code_Orchestre);
            return View(direction);
        }

        // GET: Directions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Direction.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", direction.Code_Morceau);
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien", direction.Code_Musicien);
            ViewBag.Code_Orchestre = new SelectList(db.Orchestres, "Code_Orchestre", "Nom_Orchestre", direction.Code_Orchestre);
            return View(direction);
        }

        // POST: Directions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Direction,Code_Musicien,Code_Morceau,Code_Orchestre")] Direction direction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", direction.Code_Morceau);
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien", direction.Code_Musicien);
            ViewBag.Code_Orchestre = new SelectList(db.Orchestres, "Code_Orchestre", "Nom_Orchestre", direction.Code_Orchestre);
            return View(direction);
        }

        // GET: Directions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direction direction = db.Direction.Find(id);
            if (direction == null)
            {
                return HttpNotFound();
            }
            return View(direction);
        }

        // POST: Directions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Direction direction = db.Direction.Find(id);
            db.Direction.Remove(direction);
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
