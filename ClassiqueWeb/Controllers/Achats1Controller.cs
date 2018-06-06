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

        //Consultation du panier de l'abonné identifié
        [Authorize]
        public ActionResult Panier(String userId)
        {
           
            var idAbonne = db.Abonne.Single(a => a.UserId == userId);
            var achat = db.Achat.Include(a => a.Abonne).Include(a => a.Enregistrement).Where(a => a.Code_Abonne == idAbonne.Code_Abonne).Where(a => a.Achat_Confirme == null);

            return View(achat.ToList());
        }


        //Validation de tous les articles du panier
        public ActionResult ValiderPanier(String userId)
        {
            var Abonne = db.Abonne.Single(a => a.UserId == userId);
            var achat = db.Achat.Include(a => a.Abonne).Include(a => a.Enregistrement).Where(a => a.Code_Abonne == Abonne.Code_Abonne).Where(a => a.Achat_Confirme == null);
            if (Abonne.Credit.Value < achat.Sum(a => a.Enregistrement.Prix))
            {
                return RedirectToAction("CreditsInsuffisants", new { userId = userId });
            }
            else
            {
                foreach (var i in achat)
                {
                    i.Achat_Confirme = true;
                }

                db.SaveChanges();
                return RedirectToAction("Panier", new { userId = userId });
            }
        }
        public ActionResult CreditsInsuffisants(String userId)
        {
            return View();
        }

        public ActionResult Recharger(String userId, String credits)
        {
            var Abonne = db.Abonne.Single(a => a.UserId == userId);
            if (!String.IsNullOrEmpty(credits))
            { 
                Abonne.Credit = Abonne.Credit + Int32.Parse(credits);
                db.SaveChanges();
                return RedirectToAction("Panier", new { userId = userId });
            }
            return View();

        }

        //Consultation de l'historique des achats de l'abonné identifié
        [Authorize]
        public ActionResult HistoAchats(String userId)
        {

            var idAbonne = db.Abonne.Single(a => a.UserId == userId);
            var achat = db.Achat.Include(a => a.Abonne).Include(a => a.Enregistrement).Where(a => a.Code_Abonne == idAbonne.Code_Abonne).Where(a => a.Achat_Confirme == true);

            return View(achat.ToList());
        }

        //Permet de supprimer un enregistrement du panier
        [Authorize]
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

        //Confirmation de la suppression
        [Authorize]
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
