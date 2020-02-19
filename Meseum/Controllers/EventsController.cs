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
    public class EventsController : Controller
    {
        private MeseumContext db = new MeseumContext();
        string[] ImageExt = { ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".SVG" };
        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }
        [AllowAnonymous]
        public ActionResult DetailsUser(int? id)
        {
            EventDetails events = new EventDetails();
            events.RecentEvents = db.Events.Include(m => m.Files);
            if (id == null)
            {
                events.Event = db.Events.Include(m => m.Files).FirstOrDefault();
            }
            else
            {
                events.Event = db.Events.Include(m => m.Files).FirstOrDefault(m => m.Id == id);
            }
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            Events eve = new Events();
            return View(eve);
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Events events,List<HttpPostedFileBase> Images)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(events);
                db.SaveChanges();

                events = db.Events.OrderByDescending(m => m.Id).FirstOrDefault();
                int Id = db.Events.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                string id = Id.ToString();
                if (id != null)
                {
                    if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/Events/" + id))))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Events/" + id));
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
                                    string path = Server.MapPath("~/Admin/Images/Events/") + id + "/" + InputFileName;
                                    var ServerSavePath = Path.Combine(path);
                                    //Save file to server folder  
                                    fi.SaveAs(ServerSavePath);
                                    if (System.IO.File.Exists(path))
                                    {
                                        ImageFile file = new ImageFile
                                        {
                                            Name = InputFileName,
                                            Size = size,
                                            path = "~/Admin/Images/Events/" + id + "/" + InputFileName,
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

            return View(events);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Events events, List<HttpPostedFileBase> Images)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/Events/" + events.Id))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Events/" + events.Id));
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
                                string path = Server.MapPath("~/Admin/Images/Events/") + events.Id + "/" + InputFileName;
                                var ServerSavePath = Path.Combine(path);
                                //Save file to server folder  
                                fi.SaveAs(ServerSavePath);
                                if (System.IO.File.Exists(path))
                                {
                                    ImageFile file = new ImageFile
                                    {
                                        Name = InputFileName,
                                        Size = size,
                                        path = "~/Admin/Images/Events/" + events.Id + "/" + InputFileName,
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

                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IEnumerable<ImageFile> files = db.Events.Include(mbox => mbox.Files).FirstOrDefault(m => m.Id == id).Files;
            foreach (var item in files)
            {
                if (System.IO.File.Exists(Server.MapPath(item.path)))
                {
                    System.IO.File.Delete(Server.MapPath(item.path));
                }
                db.ImageFile.Remove(item);
                db.SaveChanges();
            }
            Events events = db.Events.Find(id);
            
            db.Events.Remove(events);
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
