using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Meseum.Context;
using Meseum.Models;

namespace Meseum.Controllers
{
    [Authorize]
    public class BannersController : Controller
    {
        private MeseumContext db = new MeseumContext();

        // GET: Banners
        public ActionResult Index()
        {
            return View(db.Banners.ToList());
        }

        // GET: Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Banners/Create
        public ActionResult Create()
        {
            Banner bann = new Banner();
            return View(bann);
        }

        // POST: Banners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Banner banner,HttpPostedFileBase Images)
        {
            if (ModelState.IsValid)
            {
                if (!Directory.Exists(Server.MapPath("~/Admin/Images/Banner")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Banner"));
                }
                if (Images != null)
                {
                    Images.SaveAs(Server.MapPath("~/Admin/Images/Banner/" + Images.FileName));
                    ImageFile file = new ImageFile
                    {
                        Name = Images.FileName,
                        Size = Images.ContentLength / 10000,
                        path = "~/Admin/Images/Banner/" + Images.FileName,
                        Type = "Image",
                        UploadedBy = "Admin",
                        UploadedDate = DateTime.Now

                    };
                    db.ImageFile.Add(file);
                    db.SaveChanges();

                    ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();
                    banner.Image = image;
                   
                }

                db.Banners.Add(banner);
                db.SaveChanges();
              

                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Banner banner,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (!Directory.Exists(Server.MapPath("~/Admin/Images/Banner")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Banner"));
                    }
                    if (banner.Image != null)
                    {
                        db.ImageFile.Remove(banner.Image);
                        db.SaveChanges();

                        System.IO.File.Delete(Server.MapPath(banner.Image.path));
                    }
                    Image.SaveAs(Server.MapPath("~/Admin/Images/Banner/" + Image.FileName));
                    ImageFile file = new ImageFile
                    {
                        Name = Image.FileName,
                        Size = Image.ContentLength / 10000,
                        path = "~/Admin/Images/Banner/" + Image.FileName,
                        Type = "Image",
                        UploadedBy = "Admin",
                        UploadedDate = DateTime.Now

                    };
                    db.ImageFile.Add(file);
                    db.SaveChanges();

                    ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();
                    banner.Image = image;

                }


                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Banner banner = db.Banners.Find(id);
            if (banner.Image != null)
            {
                System.IO.File.Delete(Server.MapPath(banner.Image.path));
            }
            db.Banners.Remove(banner);
            db.SaveChanges();
            return RedirectToAction("Index");
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
