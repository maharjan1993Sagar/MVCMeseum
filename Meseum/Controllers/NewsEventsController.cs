using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Meseum.Context;
using Meseum.Models;
using Meseum.ViewModel;

namespace Meseum.Controllers
{
    [Authorize]
    public class NewsEventsController : Controller
    {
        private MeseumContext db = new MeseumContext();
        string[] ImageExt = { ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".SVG" };
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
        [AllowAnonymous]
        public ActionResult DetailsUser(int? id)
        {
            NewsDetails news = new NewsDetails();
            news.RecentNews = db.NewsEvents.Include(m=>m.Files);
            if (id == null)
            {
                news.News = db.NewsEvents.Include(m => m.Files).FirstOrDefault();
                return View(news);
            }
            else
            {
                news.News = db.NewsEvents.Include(m => m.Files).FirstOrDefault(m => m.Id == id);
            }
            return View(news);
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
        [ValidateInput(false)]
        public ActionResult Create( NewsEvent newsEvent,List<HttpPostedFileBase> Images)
        {
            if (ModelState.IsValid)
            {
                newsEvent.UpdatedAt = DateTime.Now;
                newsEvent.UpdatedBy = User.Identity.Name;
                db.NewsEvents.Add(newsEvent);
                db.SaveChanges();

                NewsEvent events = db.NewsEvents.Include(m=>m.Files).OrderByDescending(m => m.Id).FirstOrDefault();
                int Id = db.NewsEvents.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                string id = Id.ToString();
                if (id != null)
                {
                    if (!Directory.Exists(Server.MapPath("~/Admin/Images/News")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Admin/Images/News"));
                    }

                    if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/News/" + id))))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Admin/Images/News/" + id));
                    }

                    if (Images.Count > 0)
                    {
                        foreach (HttpPostedFileBase fi in Images)
                        {
                            //Checking file is available to save.  
                            if (fi != null)
                            {
                                var InputFileName = Path.GetFileName(fi.FileName);
                                int size = fi.ContentLength / 1000000;

                                string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                                Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                                InputFileName = r.Replace(InputFileName, "");

                                string extension = Path.GetExtension(fi.FileName);
                                if (ImageExt.Contains(extension.ToUpper()))
                                {
                                    string path = Server.MapPath("~/Admin/Images/News/") + id + "/" + InputFileName;
                                    var ServerSavePath = Path.Combine(path);
                                    //Save file to server folder  
                                    fi.SaveAs(ServerSavePath);
                                    if (System.IO.File.Exists(path))
                                    {
                                        ImageFile file = new ImageFile
                                        {
                                            Name = InputFileName,
                                            Size = size,
                                            path = "~/Admin/Images/News/" + id + "/" + InputFileName,
                                            Type = "Image",
                                            UploadedBy = "Admin",
                                            UploadedDate = DateTime.Now
                                            
                                        };
                                        
                                        db.ImageFile.Add(file);
                                        db.SaveChanges();

                                        ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();
                                        events.Files.Add(image);
                                        db.SaveChanges();
                                    }
                                }

                            }
                        }
                    }
                }


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
        public ActionResult Edit(NewsEvent newsEvent,List<HttpPostedFileBase> Images)
        {
            if (ModelState.IsValid)
            {
                newsEvent.UpdatedAt = DateTime.Now;
                newsEvent.UpdatedBy = User.Identity.Name;
                db.Entry(newsEvent).State = EntityState.Modified;
                db.SaveChanges();
                if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/News/" + newsEvent.Id))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/News/" + newsEvent.Id));
                }

                if (Images.Count > 0)
                {
                    foreach (HttpPostedFileBase fi in Images)
                    {
                        //Checking file is available to save.  
                        if (fi != null)
                        {
                            var InputFileName = Path.GetFileName(fi.FileName);
                            int size = fi.ContentLength / 1000000;

                            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                            InputFileName = r.Replace(InputFileName, "");

                            string extension = Path.GetExtension(fi.FileName);
                            if (ImageExt.Contains(extension.ToUpper()))
                            {
                                string path = Server.MapPath("~/Admin/Images/News/") + newsEvent.Id + "/" + InputFileName;
                                var ServerSavePath = Path.Combine(path);
                                //Save file to server folder  
                                fi.SaveAs(ServerSavePath);
                                if (System.IO.File.Exists(path))
                                {
                                    ImageFile file = new ImageFile
                                    {
                                        Name = InputFileName,
                                        Size = size,
                                        path = "~/Admin/Images/News/" + newsEvent.Id + "/" + InputFileName,
                                        Type = "Image",
                                        UploadedBy = "Admin",
                                        UploadedDate = DateTime.Now

                                    };
                                    db.ImageFile.Add(file);
                                    db.SaveChanges();

                                    ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();
                                    newsEvent.Files.Add(image);
                                    db.SaveChanges();

                                }
                            }

                        }
                    }
                }
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
            IEnumerable<ImageFile> files = db.NewsEvents.Include(mbox => mbox.Files).FirstOrDefault(m => m.Id == id).Files;
            foreach (var item in files)
            {
                if (System.IO.File.Exists(Server.MapPath(item.path)))
                {
                    System.IO.File.Delete(Server.MapPath(item.path));
                }
                db.ImageFile.Remove(item);
                db.SaveChanges();
            }
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
