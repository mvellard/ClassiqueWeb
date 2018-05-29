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
    public class InstrumentationsController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Instrumentations
        public ActionResult Index()
        {
            var instrumentation = db.Instrumentation.Include(i => i.Instrument).Include(i => i.Oeuvre);
            return View(instrumentation.ToList());
        }

        // GET: Instrumentations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrumentation instrumentation = db.Instrumentation.Find(id);
            if (instrumentation == null)
            {
                return HttpNotFound();
            }
            return View(instrumentation);
        }

        // GET: Instrumentations/Create
        public ActionResult Create()
        {
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument");
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre");
            return View();
        }

        // POST: Instrumentations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Instrumentation,Code_Oeuvre,Code_Instrument")] Instrumentation instrumentation)
        {
            if (ModelState.IsValid)
            {
                db.Instrumentation.Add(instrumentation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", instrumentation.Code_Instrument);
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre", instrumentation.Code_Oeuvre);
            return View(instrumentation);
        }

        // GET: Instrumentations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrumentation instrumentation = db.Instrumentation.Find(id);
            if (instrumentation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", instrumentation.Code_Instrument);
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre", instrumentation.Code_Oeuvre);
            return View(instrumentation);
        }

        // POST: Instrumentations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Instrumentation,Code_Oeuvre,Code_Instrument")] Instrumentation instrumentation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instrumentation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Instrument = new SelectList(db.Instrument, "Code_Instrument", "Nom_Instrument", instrumentation.Code_Instrument);
            ViewBag.Code_Oeuvre = new SelectList(db.Oeuvre, "Code_Oeuvre", "Titre_Oeuvre", instrumentation.Code_Oeuvre);
            return View(instrumentation);
        }

        // GET: Instrumentations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrumentation instrumentation = db.Instrumentation.Find(id);
            if (instrumentation == null)
            {
                return HttpNotFound();
            }
            return View(instrumentation);
        }

        // POST: Instrumentations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instrumentation instrumentation = db.Instrumentation.Find(id);
            db.Instrumentation.Remove(instrumentation);
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
