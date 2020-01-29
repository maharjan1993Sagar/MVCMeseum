using Meseum.Context;
using Meseum.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Meseum.Controllers
{
    public class HomeController : Controller
    {
        private readonly MeseumContext db = new MeseumContext();
        public ActionResult Index()
        {
            HomeVM home = new HomeVM();
            home.Inventories = db.Inventories;
            home.NewsEvents = db.NewsEvents;
            return View(home);
        }
        public ActionResult IndexUser()
        {
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