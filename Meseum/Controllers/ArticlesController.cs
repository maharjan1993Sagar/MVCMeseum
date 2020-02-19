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
    public class ArticlesController : Controller
    {
        private MeseumContext db = new MeseumContext();

        // GET: Articles
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        [AllowAnonymous]
        public ActionResult DetailsUser()
        {
            return View(db.Articles.Include(m => m.File));
        }

        public FileResult Download(int? id)
        {
            if (id != null)
            {
                Article art = db.Articles.Include(a => a.File).FirstOrDefault(a => a.Id == id.Value);
                if (art.File != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(art.File.path)))
                    {
                        byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(art.File.path));
                        string fileName = art.File.Name;
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

                    }
                    return null;

                }
                return null;
            }
            else
            {
                return null;
            }
        }
        // GET: Articles/Create
        public ActionResult Create()
        {
            Article art = new Article();
            return View(art);
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Article article, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (!Directory.Exists(Server.MapPath("~/Admin/Images/Article")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Article"));
                }
                if (Image != null)
                {
                    Image.SaveAs(Server.MapPath("~/Admin/Images/Article/" + Image.FileName));
                    ImageFile file = new ImageFile
                    {
                        Name = Image.FileName,
                        Size = Image.ContentLength / 1000000,
                        path = "~/Admin/Images/Article/" + Image.FileName,
                        Type = "Image",
                        UploadedBy = "Admin",
                        UploadedDate = DateTime.Now

                    };
                    db.ImageFile.Add(file);
                    db.SaveChanges();

                    ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();
                    db.SaveChanges();
                    article.File = image;

                }
                db.Articles.Add(article);
                db.SaveChanges();
                article = db.Articles.OrderByDescending(m => m.Id).FirstOrDefault();

                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Article article, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (!Directory.Exists(Server.MapPath("~/Admin/Images/Article")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Article"));
                    }
                    if (article.File != null)
                    {
                        db.ImageFile.Remove(article.File);
                        db.SaveChanges();

                        System.IO.File.Delete(Server.MapPath(article.File.path));
                    }

                    ImageFile file = new ImageFile
                    {
                        Name = Image.FileName,
                        Size = Image.ContentLength / 10000,
                        path = "~/Admin/Images/Article/" + Image.FileName,
                        Type = "Image",
                        UploadedBy = "Admin",
                        UploadedDate = DateTime.Now

                    };

                    db.ImageFile.Add(file);
                    db.SaveChanges();
                    ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();
                    article.File = image;

                }
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            if (article.File != null)
            {
                System.IO.File.Delete(Server.MapPath(article.File.path));
            }
            db.Articles.Remove(article);
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
