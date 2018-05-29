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
    public class AlbumsController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Albums
        public ActionResult Index()
        {
            var album = db.Album.Include(a => a.Editeur).Include(a => a.Genre);
            return View(album.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.Code_Editeur = new SelectList(db.Editeur, "Code_Editeur", "Nom_Editeur");
            ViewBag.Code_Genre = new SelectList(db.Genre, "Code_Genre", "Libelle_Abrege");
            return View();
        }

        // POST: Albums/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code_Album,Titre_Album,Annee_Album,Code_Genre,Code_Editeur,Pochette,ASIN")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Album.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Code_Editeur = new SelectList(db.Editeur, "Code_Editeur", "Nom_Editeur", album.Code_Editeur);
            ViewBag.Code_Genre = new SelectList(db.Genre, "Code_Genre", "Libelle_Abrege", album.Code_Genre);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code_Editeur = new SelectList(db.Editeur, "Code_Editeur", "Nom_Editeur", album.Code_Editeur);
            ViewBag.Code_Genre = new SelectList(db.Genre, "Code_Genre", "Libelle_Abrege", album.Code_Genre);
            return View(album);
        }

        // POST: Albums/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code_Album,Titre_Album,Annee_Album,Code_Genre,Code_Editeur,Pochette,ASIN")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Code_Editeur = new SelectList(db.Editeur, "Code_Editeur", "Nom_Editeur", album.Code_Editeur);
            ViewBag.Code_Genre = new SelectList(db.Genre, "Code_Genre", "Libelle_Abrege", album.Code_Genre);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Album.Find(id);
            db.Album.Remove(album);
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
