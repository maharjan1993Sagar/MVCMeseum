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
    public class NewsEventsController : Controller
    {
        private MeseumContext db = new MeseumContext();

        // GET: NewsEvents
        public ActionResult Index()
        {
            return View(db.NewsEvents.ToList());
        }
        public ActionResult IndexUser()
        {
            return View(db.NewsEvents.ToList());
        }
        // GET: NewsEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsEvent newsEvent = db.NewsEvents.Find(id);
            if (newsEvent == null)
            {
                return HttpNotFound();
            }
            return View(newsEvent);
        }

        public ActionResult DetailsUser(int? id)
        {
            if (id == null)
            {
                NewsEvent news = db.NewsEvents.LastOrDefault();
                return View(news);
               //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsEvent newsEvent = db.NewsEvents.Find(id);
            if (newsEvent == null)
            {
                return HttpNotFound();
            }
            return View(newsEvent);
        }
        // GET: NewsEvents/Create
        public ActionResult Create()
        {
            NewsEvent news = new NewsEvent();
            news.UploadDate = DateTime.Now;
            return View(news);
        }

        // POST: NewsEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( NewsEvent newsEvent)
        {
            if (ModelState.IsValid)
            {
                newsEvent.UpdatedAt = DateTime.Now;
                newsEvent.UpdatedBy = User.Identity.Name;
                db.NewsEvents.Add(newsEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsEvent);
        }

        // GET: NewsEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsEvent newsEvent = db.NewsEvents.Find(id);
            if (newsEvent == null)
            {
                return HttpNotFound();
            }
            return View(newsEvent);
        }

        // POST: NewsEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsEvent newsEvent)
        {
            if (ModelState.IsValid)
            {
                newsEvent.UpdatedAt = DateTime.Now;
                newsEvent.UpdatedBy = User.Identity.Name;
                db.Entry(newsEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsEvent);
        }

        // GET: NewsEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsEvent newsEvent = db.NewsEvents.Find(id);
            if (newsEvent == null)
            {
                return HttpNotFound();
            }
            return View(newsEvent);
        }

        // POST: NewsEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsEvent newsEvent = db.NewsEvents.Find(id);
            db.NewsEvents.Remove(newsEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public void uploadnow(HttpPostedFileWrapper upload)
        //{
        //    if (upload != null)
        //    {
        //        string ImageName = upload.FileName;
        //        string path = System.IO.Path.Combine(Server.MapPath("~/Admin/Images/NewsEvent"), ImageName);
        //        upload.SaveAs(path);
        //    }
        //}


        //public ActionResult uploadPartial()
        //{
        //    var appData = Server.MapPath("~/Images/uploads");
        //    var images = Directory.GetFiles(appData).Select(x => new imagesviewmodel
        //    {
        //        Url = Url.Content("/images/uploads/" + Path.GetFileName(x))
        //    });
        //    return View(images);
        //}

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
