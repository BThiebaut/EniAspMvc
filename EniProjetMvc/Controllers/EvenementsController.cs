using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using EniProjetMvc.Models;
using DAL;
using EniProjetMvc.Extensions;

namespace EniProjetMvc.Controllers
{
    public class EvenementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evenements
        public ActionResult Index()
        {
            List<Evenement> list = DAOFactory.GetRepository<Evenement>(db).listAll();
            return View(list);
        }

        // GET: Evenements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = db.Evenements.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // GET: Evenements/Create
        public ActionResult Create()
        {
            var vm = new EvenementVM(db);
            return View(vm);
        }

        // POST: Evenements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvenementVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.selectedTheme.HasValue)
                {
                    var theme = DAOFactory.GetRepository<Theme>(db).getById(vm.selectedTheme.Value);
                    vm.Evenement.Theme = theme;
                }
                DAOFactory.GetRepository<Evenement>(db).insert(vm.Evenement);
                return RedirectToAction("Index");
            }

            return Create();
        }

        // GET: Evenements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = db.Evenements.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // POST: Evenements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Intitule,DateDebut,DateFin,Duree,HeureOuverture,HeureFermeture,Adresse,Statut")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evenement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evenement);
        }

        // GET: Evenements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = db.Evenements.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // POST: Evenements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evenement evenement = db.Evenements.Find(id);
            db.Evenements.Remove(evenement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult AjaxListe(string term = null, int? statut = null)
        {
            var list = DAOFactory.GetRepository<Evenement>(db).listAll();
            var view = ViewRenderer.RenderPartialView("~/Views/Evenements/ListOrganizer.cshtml", list, ControllerContext);
            var res = new { Html = view };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AjaxDetails(int id)
        {
            var evenement = DAOFactory.GetRepository<Evenement>(db).getById(id);
            var view = ViewRenderer.RenderPartialView("~/Views/Evenements/Details.cshtml", evenement, ControllerContext);
            var res = new { Html = view, Adresse = evenement.Adresse };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AjaxDelete(int id)
        {
            var evenement = DAOFactory.GetRepository<Evenement>(db).getById(id);
            var isDeleted = DAOFactory.GetRepository<Evenement>(db).cancel(evenement);
            var res = new { error = !isDeleted };
            return Json(res, JsonRequestBehavior.AllowGet);
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
