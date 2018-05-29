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
    public class InterpretersController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Interpreters
        public ActionResult Index()
        {
            var interpreter = db.Interpreter.Include(i => i.Enregistrement).Include(i => i.Instrument).Include(i => i.Musicien);
            return View(interpreter.ToList());
        }

        // GET: Interpreters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interpreter interpreter = db.Interpreter.Find(id);
            if (interpreter == null)
            {
                return HttpNotFound();
            }
            return View(interpreter);
        }

        // GET: Interpreters/Create
        public ActionResult Create()
        {
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre");
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument");
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien");
            return View();
        }

        // POST: Interpreters/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Interpreter,Code_Morceau,Code_Musicien,Code_Instrument")] Interpreter interpreter)
        {
            if (ModelState.IsValid)
            {
                db.Interpreter.Add(interpreter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", interpreter.Code_Morceau);
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", interpreter.Code_Instrument);
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien", interpreter.Code_Musicien);
            return View(interpreter);
        }

        // GET: Interpreters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interpreter interpreter = db.Interpreter.Find(id);
            if (interpreter == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", interpreter.Code_Morceau);
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", interpreter.Code_Instrument);
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien", interpreter.Code_Musicien);
            return View(interpreter);
        }

        // POST: Interpreters/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Interpreter,Code_Morceau,Code_Musicien,Code_Instrument")] Interpreter interpreter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interpreter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Morceau = new SelectList(db.Enregistrement, "Code_Morceau", "Titre", interpreter.Code_Morceau);
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", interpreter.Code_Instrument);
            ViewBag.Code_Musicien = new SelectList(db.Musicien, "Code_Musicien", "Nom_Musicien", interpreter.Code_Musicien);
            return View(interpreter);
        }

        // GET: Interpreters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interpreter interpreter = db.Interpreter.Find(id);
            if (interpreter == null)
            {
                return HttpNotFound();
            }
            return View(interpreter);
        }

        // POST: Interpreters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Interpreter interpreter = db.Interpreter.Find(id);
            db.Interpreter.Remove(interpreter);
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
