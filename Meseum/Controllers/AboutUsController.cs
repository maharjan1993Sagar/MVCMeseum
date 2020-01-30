using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Meseum.Context;
using Meseum.Models;

namespace Meseum.Controllers
{
    public class AboutUsController : Controller
    {
        private MeseumContext db = new MeseumContext();

        // GET: AboutUs
        public ActionResult Index()
        {
            return View(db.AboutUs.ToList());
        }

        // GET: AboutUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // GET: AboutUs/Create
        public ActionResult Create()
        {
            AboutUs about = new AboutUs();
            return View(about);
        }

        // POST: AboutUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AboutUs aboutUs,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
               
                if (Image != null)
                {
                    Image.SaveAs(Server.MapPath("~/Admin/Images/AboutUs/" + Image.FileName));

                    ImageFile file = new ImageFile
                    {
                        Name = Image.FileName,
                        Size = Image.ContentLength / 10000,
                        path = "~/Admin/Images/AboutUs/" + Image.FileName,
                        Type = "Image",
                        UploadedBy = "Admin",
                        UploadedDate = DateTime.Now

                    };
                    db.ImageFile.Add(file);
                    db.SaveChanges();

                    ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();

                    aboutUs.File = image;
                    db.AboutUs.Add(aboutUs);
                    db.SaveChanges();

                   
                    aboutUs.File=image;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(aboutUs);
        }

        // GET: AboutUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AboutUs aboutUs,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (aboutUs.File != null)
                    {
                        db.ImageFile.Remove(aboutUs.File);
                        db.SaveChanges();

                        System.IO.File.Delete(Server.MapPath(aboutUs.File.path));
                    }

                    ImageFile file = new ImageFile
                    {
                        Name = Image.FileName,
                        Size = Image.ContentLength / 10000,
                        path = "~/Admin/Images/AboutUs/" + Image.FileName,
                        Type = "Image",
                        UploadedBy = "Admin",
                        UploadedDate = DateTime.Now

                    };
                    db.ImageFile.Add(file);
                    db.SaveChanges();

                    ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();
                    aboutUs.File = image;
                }


                db.Entry(aboutUs).State = EntityState.Modified;
                db.SaveChanges();
                
                
                return RedirectToAction("Index");
            }
            return View(aboutUs);
        }

        // GET: AboutUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // POST: AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs.File!=null)
            {
                System.IO.File.Delete(Server.MapPath(aboutUs.File.path));
            }

            db.AboutUs.Remove(aboutUs);
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
