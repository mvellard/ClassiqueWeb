using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ClassiqueWeb.Models;
using System.IO;
using PagedList;//Outils/Gestionnaire de paquets NuGet/Console du Gestionnaire de package /taper : Install-Package PagedList.Mvc
using System;
using System.Collections.Generic;

namespace ClassiqueWeb.Controllers
{
    public class MusiciensController : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        // GET: Musiciens
        public ActionResult Index(int? page)
        {
            
  
            var musicien = db.Musicien.Include(m => m.Genre).Include(m => m.Instrument).Include(m => m.Pays).Include(m=>m.Composer);
            
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            
            return View(musicien.OrderBy(m=>m.Nom_Musicien).ToPagedList(pageNumber, pageSize));
        }

        // GET: Musiciens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Musicien musicien = db.Musicien.Find(id);
            if (Request.Files["files"] != null)
            {
                byte[] Img;
                using (var binaryReader = new BinaryReader(Request.Files["files"].InputStream))
                {
                    Img = binaryReader.ReadBytes(Request.Files["files"].ContentLength);
                    musicien.Photo = Img;
                }
            }

            if (musicien == null)
            {
                return HttpNotFound();
            }
            ViewBag.Composer = getCompose(id);
            return View(musicien);
        }
        public List<Oeuvre> getCompose(int? musicien)
        {

            var composer = (from o in db.Oeuvre
                            join c in db.Composer on o.Code_Oeuvre equals c.Code_Oeuvre
                            join m in db.Musicien on c.Code_Musicien equals m.Code_Musicien
                            where m.Code_Musicien == musicien
                            select o);

            return composer.ToList();
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
