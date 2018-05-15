using BO;
using DAL;
using EniProjetMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EniProjetMvc.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var e = new Evenement();
            e.Intitule = "abc";
            e.HeureFermeture = new DateTime();
            e.HeureOuverture = new DateTime();
            e.Statut = StatutEvenement.EN_COUR;
            e.DateDebut = new DateTime();
            e.DateFin = new DateTime();
            var repo = DAOFactory<Evenement>.GetRepository<Evenement>(db);
            repo.insert(e);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}