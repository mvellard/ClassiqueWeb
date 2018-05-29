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
        public ActionResult Index()
        {
            
            var composer = db.Composer.Include(c => c.Musicien).Include(c => c.Oeuvre);
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
