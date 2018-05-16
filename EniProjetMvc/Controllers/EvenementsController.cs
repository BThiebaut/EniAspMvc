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

           

            Evenement e1 = new Evenement {
                Id = 1,
                DateDebut = new DateTime(),
                DateFin = new DateTime(),
                HeureFermeture = "",
                HeureOuverture = "",
                Intitule = "L'attaque des martiens",
                Adresse = "le Monde",
                Statut = StatutEvenement.EN_COURS
            };

            List<Image> images1 = new List<Image> {
                new Image { Id = 1, Evenement = e1, Url = "http://planete-cine.com/wp-content/uploads/2016/04/mars-attacks.jpg"},
            };
            e1.Images = images1;

            Evenement e2 = new Evenement
            {
                Id = 2,
                DateDebut = new DateTime(),
                DateFin = new DateTime(),
                HeureFermeture = "",
                HeureOuverture = "",
                Intitule = "La sortie de Half-Life 3",
                Adresse = "???",
                Statut = StatutEvenement.A_VENIR
            };

            List<Image> images2 = new List<Image> {
                new Image { Id = 2, Evenement = e2, Url = "http://static.papergeek.fr/2016/08/half-life-3-logo-720x450.jpg" },
                new Image {Id = 3, Evenement = e2, Url = "https://cdn.drawception.com/images/panels/2014/8-3/8AXwxdYLEn-4.png" }
            };
            e2.Images = images2;

            Evenement e3 = new Evenement
            {
                Id = 3,
                DateDebut = new DateTime(),
                DateFin = new DateTime(),
                HeureFermeture = "",
                HeureOuverture = "",
                Intitule = "Phillipe !",
                Adresse = "la ou il se cache",
                Statut = StatutEvenement.EN_COURS
            };

            List<Image> images3 = new List<Image> {
                new Image { Id = 4, Evenement = e1, Url = "https://i.ytimg.com/vi/EOxUWLl2HFs/hqdefault.jpg"},
            };
            e3.Images = images3;

            Evenement e4 = new Evenement
            {
                Id = 4,
                DateDebut = new DateTime(),
                DateFin = new DateTime(),
                HeureFermeture = "",
                HeureOuverture = "",
                Intitule = "La mort de Kenny",
                Adresse = "Le parc du sud",
                Statut = StatutEvenement.A_VENIR
            };

            List<Image> images4 = new List<Image> {
                new Image { Id = 5, Evenement = e1, Url = "https://vignette.wikia.nocookie.net/southpark/images/4/42/Proper_condom_use_kenny%27s_death.jpg/revision/latest?cb=20100327195536"},
            };
            e4.Images = images4;

            Evenement e5 = new Evenement
            {
                Id = 5,
                DateDebut = new DateTime(),
                DateFin = new DateTime(),
                HeureFermeture = "",
                HeureOuverture = "",
                Intitule = "Une journée normale d'un joueur de League of légend",
                Adresse = "n'importe ou",
                Statut = StatutEvenement.A_VENIR
            };

            List<Image> images5 = new List<Image> {
                new Image { Id = 6, Evenement = e1, Url = "http://www2.mes-coloriages-preferes.biz/colorino/Images/Large/Personnages-celebres-Troll-face-Rage-Guy-134663.png"},
            };
            e5.Images = images5;



            List<Evenement> list = new List<Evenement> { e1, e2, e3, e4, e5};

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
                    var theme = DAOFactory<Theme>.GetRepository<Theme>(db).getById(vm.selectedTheme.Value);
                    vm.Evenement.Theme = theme;
                }
                DAOFactory<Evenement>.GetRepository<Evenement>(db).insert(vm.Evenement);
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
            var list = DAOFactory<Evenement>.GetRepository<Evenement>(db).listAll();
            ViewBag.modeAjax = true;
            //var view = View("ListOrganizer", liste);
            var view = ViewRenderer.RenderView("~/views/Evenements/ListOrganizer.csthml", list, ControllerContext);
            var res = new { Html = view };
            return Json(res);
        }

        [HttpGet]
        public JsonResult AjaxDetails(int id)
        {
            var evenement = DAOFactory<Evenement>.GetRepository<Evenement>(db).getById(id);
            ViewBag.modeAjax = true;
            var view = ViewRenderer.RenderView("~/views/Evenements/Details.csthml", evenement, ControllerContext);
            var res = new { Html = view };
            return Json(res);
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
