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
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace EniProjetMvc.Controllers
{
    public class EvenementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public EvenementsController()
        {
        }

        public EvenementsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public ActionResult Create(EvenementVM vm)
        {
            if (vm.Evenement.Intitule != "")
            {
                if (vm.selectedTheme.HasValue)
                {
                    var theme = DAOFactory.GetRepository<Theme>(db).getById(vm.selectedTheme.Value);
                    vm.Evenement.Theme = theme;
                }
                var evenement = DAOFactory.GetRepository<Evenement>(db).insert(vm.Evenement);
                if (vm.Images.Count > 0)
                {
                    foreach (var id in vm.Images)
                    {
                        var img = DAOFactory.GetRepository<Image>(db).getById(id);
                        img.Evenement = evenement;
                        DAOFactory.GetRepository<Image>(db).update(img);
                    }
                }
                return RedirectToAction("Index");
            }

            return Create();
        }

        // GET: Evenements/Edit/5
        [Authorize(Roles = "admin")]
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
            var vm = new EvenementVM(db);
            vm.Evenement = evenement;
            vm.selectedTheme = evenement.Theme.Id;

            return View("Create", vm);
        }

        // POST: Evenements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(EvenementVM vm)
        {
            if (vm.selectedTheme.HasValue)
            {
                vm.Evenement.Theme = DAOFactory.GetRepository<Theme>(db).getById(vm.selectedTheme.Value);
                DAOFactory.GetRepository<Evenement>(db).update(vm.Evenement);
                return RedirectToAction("Index");
            }
            return Edit(vm.Evenement.Id);
        }

        // GET: Evenements/Delete/5
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Evenement evenement = db.Evenements.Find(id);
            db.Evenements.Remove(evenement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ListInscrit(string search = null, Theme theme = null, StatutEvenement? statut = null)
        {
            var user = db.GetFullUser(User.Identity.GetUserId());
            var repo = DAOFactory.GetRepository<Evenement>(db) as EvenementDAO;
            var listTheme = DAOFactory.GetRepository<Theme>(db).listAll();
            List<Evenement> list = repo.listAll(user.Utilisateur, search, theme, statut);

            ViewBag.listTheme = listTheme;
            var view = ViewRenderer.RenderPartialView("~/Views/Evenements/ListOrganizer.cshtml", list, ControllerContext);
            view = Regex.Replace(view, @"[\r\n]+", "");
            view = view.Replace("                        ", "");
            var res = new { Html = view };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AjaxListe(string search = null, Theme theme = null, StatutEvenement? statut = null)
        {
            var repo = DAOFactory.GetRepository<Evenement>(db) as EvenementDAO;
            var list = repo.listAll(null, search, theme, statut);
            var listTheme = DAOFactory.GetRepository<Theme>(db).listAll();
            
            ViewBag.listTheme = listTheme;
            var view = ViewRenderer.RenderPartialView("~/Views/Evenements/ListOrganizer.cshtml", list, ControllerContext);
            view = Regex.Replace(view, @"[\r\n]+", "");
            view = view.Replace("                        ", "");
            var res = new { Html = view };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AjaxListeHome(StatutEvenement statut, int limit)
        {
            var repo = DAOFactory.GetRepository<Evenement>(db) as EvenementDAO;
            var list = repo.getByStatut(statut, limit);
            ViewBag.statut = statut;
            var view = ViewRenderer.RenderPartialView("~/Views/Home/ListOrganizer.cshtml", list, ControllerContext);
            var res = new { Html = view };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AjaxDetails(int id)
        {
            var evenement = DAOFactory.GetRepository<Evenement>(db).getById(id);
            var view = ViewRenderer.RenderPartialView("~/Views/Evenements/Details.cshtml", evenement, ControllerContext);
            var repo = DAOFactory.GetRepository<Utilisateur>(db) as UtilisateurDAO;

            var isInscrit = false;
            ApplicationUser user = null;
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                user = db.GetFullUser(userId);
                isInscrit = repo.HasInscription(evenement, user.Utilisateur.Id);
            }

            var res = new
            {
                Html = view,
                Adresse = evenement.Adresse,
                Inscrit = isInscrit,
                Duree = evenement.Duree,
                HeureOuverture = evenement.HeureOuverture,
                HeureFermeture = evenement.HeureFermeture,
                UserAdresse = user != null ? user.Utilisateur.Adresse : "",
                InscriptionOuverte = evenement.Statut == StatutEvenement.A_VENIR || evenement.Statut == StatutEvenement.EN_COURS
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public JsonResult AjaxDelete(int id)
        {
            var evenement = DAOFactory.GetRepository<Evenement>(db).getById(id);
            var isDeleted = DAOFactory.GetRepository<Evenement>(db).cancel(evenement);
            var res = new { error = !isDeleted };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public JsonResult AjaxUploadImage()
        {
            List<Image> created = new List<Image>();
            var repo = DAOFactory.GetRepository<Image>(db) as ImageDAO;

            if (!Directory.Exists(Server.MapPath("~/uploads/")))
                Directory.CreateDirectory(Server.MapPath("~/uploads/"));

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                var tmpFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var fullName = "~/uploads/" + tmpFileName;
                file.SaveAs(Server.MapPath(fullName));
                fullName = fullName.Replace("~", "");
                var img = repo.insertFromUrl(fullName);
                created.Add(img);

            }
            var res = new { Images = created };
            return Json(res);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AjaxInscription(int idEvenement)
        {
            var userId = User.Identity.GetUserId();
            var evenement = DAOFactory.GetRepository<Evenement>(db).getById(idEvenement);
            var user = db.GetFullUser(userId);


            user.Utilisateur.Evenements.Add(evenement);
            var isOk = DAOFactory.GetRepository<Utilisateur>(db).update(user.Utilisateur);

            return Json(new { Error = !isOk });
        }

        [HttpPost]
        [Authorize]
        public JsonResult AjaxDesinscription(int idEvenement)
        {
            var userId = User.Identity.GetUserId();
            var evenement = DAOFactory.GetRepository<Evenement>(db).getById(idEvenement);
            var user = db.GetFullUser(userId);

            user.Utilisateur.Evenements.Remove(evenement);
            var isOk = DAOFactory.GetRepository<Utilisateur>(db).update(user.Utilisateur);

            return Json(new { Error = !isOk });
        }

        [HttpGet]
        [Authorize]
        public JsonResult AjaxArchiveAuto()
        {
            var repo = DAOFactory.GetRepository<Evenement>(db) as EvenementDAO;
            var error = false;
            var errorMsg = "";

            try
            {
                repo.AutoUpdateStatut();
            }catch(Exception e)
            {
                error = true;
                errorMsg = e.Message;
            }
            
            return Json(new { Error = error, Message = errorMsg }, JsonRequestBehavior.AllowGet);
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
