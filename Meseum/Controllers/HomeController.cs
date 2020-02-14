using AutoMapper;
using Meseum.Context;
using Meseum.Models;
using Meseum.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//#e1cfc5
namespace Meseum.Controllers
{
    public class HomeController : Controller
    {
        private readonly MeseumContext db = new MeseumContext();
        private readonly IMapper mapper;
        public ActionResult IndexUser()
        {

            //    HomeVM home = new HomeVM();
            //    home.Inventories = db.Inventories;
            //    home.NewsEvents = db.NewsEvents;
            //    return View(home);
            return RedirectToAction("IndexUser");
        }
        public ActionResult Index()
        {
            HomeVM home = new HomeVM();
            home.Inventories = db.Inventories;
            home.NewsEvents = db.NewsEvents.Include(m => m.Files);
            home.Galleries = db.Gallery.Include(m => m.Files);
            home.Events = db.Events.Include(m => m.Files);
            home.AboutUs = db.AboutUs.Include(m => m.File);
            return View(home);
        }
        public ActionResult IndexAdmin()
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
        [HttpPost]
        public JsonResult Contact(Queries queries)
        {
            if (ModelState.IsValid)
            {
                queries.UploadedBy = queries.Name.Length == 0 ? "User" : queries.Name;
                queries.UploadedDate = DateTime.Now;
                db.Queries.Add(queries);
                db.SaveChanges();
                return Json("TRUE");
            }
            else
            {
                return Json("FALSE");
            }


        }

        public ActionResult GetFiles(int? id, string type)
        {
            IEnumerable<ImageFile> files=null;
            if (id != null)
            {
                ViewBag.Type =type;
                if (type.ToUpper().Contains("NEWS"))
                {
                    files = db.NewsEvents.Include(m => m.Files).FirstOrDefault(m => m.Id == id.Value).Files;
                }
                else if (type.ToUpper().Contains("EVENT"))
                {
                    files = db.Events.Include(m => m.Files).FirstOrDefault(m => m.Id == id.Value).Files;
                }
                else if (type.ToUpper().Contains("GALLERY"))
                {
                    files = db.Gallery.Include(m => m.Files).FirstOrDefault(m => m.Id == id.Value).Files;
                }
                else if (type.ToUpper().Contains("INVENTORY"))
                {
                   
                    IEnumerable<Files> filess = db.Inventories.Include(m => m.Files).FirstOrDefault(m => m.Id == id.Value).Files;
                    files = (from f in filess
                             select new ImageFile
                             {
                                 Id = f.Id,
                                 path = f.path,
                                 Name = f.Name,
                                 UploadedDate = f.UploadedDate,
                                 UploadedBy = f.UploadedBy,
                                 Type = f.Type,
                                 Size = f.Size
                             }).AsEnumerable();
                           // mapper.Map<IEnumerable<Files>, IEnumerable<ImageFile>>(filess);
                }
            }
            return View(files);
        }
        [HttpPost]
        public JsonResult DeleteFile(int? id,string type)
        {
            if (id != null)
            {
                if (type == "Inventory")
                {
                    Files fil = db.Files.Find(id.Value);
                    if (System.IO.File.Exists(Server.MapPath(fil.path)))
                    {
                        System.IO.File.Delete(Server.MapPath(fil.path));
                      
                    }
                    db.Files.Remove(fil);
                    db.SaveChanges();
                    //Delete file as well as row in table
                    return Json("TRUE");
                }
                else
                {
                    ImageFile fil = db.ImageFile.Find(id.Value);
                    if (System.IO.File.Exists(Server.MapPath(fil.path)))
                    {
                        System.IO.File.Delete(Server.MapPath(fil.path));
                       
                    }
                    db.ImageFile.Remove(fil);
                    db.SaveChanges();

                    //Delete file as well as row in table
                    return Json("TRUE");

                }
            }
            return Json("Error");
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
            var AboutUs = db.AboutUs;
            //(from a in db.AboutUs
            //                    select a.MenuName).AsEnumerable();
            return Json(AboutUs);
        }
    }
}