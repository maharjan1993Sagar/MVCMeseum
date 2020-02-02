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
            HomeVM home = new HomeVM();
            home.Inventories = db.Inventories;
            home.NewsEvents = db.NewsEvents;
            home.Galleries = db.Gallery.Include(m=>m.Files);
            home.Events = db.Events.Include(m => m.Files);
            home.AboutUs = db.AboutUs.Include(m => m.File);
            return View(home);
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

        [HttpPost]
        public JsonResult GetBanner()
        {
            IEnumerable<string> urls = (from b in db.Banners
                                select b.Image.path).AsEnumerable();
            return Json(urls);
        }
        [HttpPost]
        public JsonResult GetAboutUs()
        {
            IEnumerable<string> AboutUs = (from a in db.AboutUs
                                select a.MenuName).AsEnumerable();
            return Json(AboutUs);
        }
    }
}