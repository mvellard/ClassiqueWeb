using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassiqueWeb.Models;
using Microsoft.AspNet.Identity;

namespace ClassiqueWeb.Controllers
{
    public class Achats1Controller : Controller
    {
        private Classique_Web_2017Entities db = new Classique_Web_2017Entities();

        [Authorize]
        // GET: Achats1
        public ActionResult Panier(String userId)
        {
            var idAbonne = db.Abonne.First(a => a.UserId == userId);
            var achat = db.Achat.Include(a => a.Abonne).Include(a => a.Enregistrement).Where(a => a.Code_Abonne == idAbonne.Code_Abonne).Where (a => a.Achat_Confirme == null);
            return View(achat.ToList());
        }

        [Authorize]
        // GET: Achats1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achat achat = db.Achat.Find(id);
            if (achat == null)
            {
                return HttpNotFound();
            }
            return View(achat);
        }
        [Authorize]
        // POST: Achats1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Achat achat = db.Achat.Find(id);
            db.Achat.Remove(achat);
            db.SaveChanges();
            var userID = User.Identity.GetUserId();
            return RedirectToAction("Panier",new { userId = userID });
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
