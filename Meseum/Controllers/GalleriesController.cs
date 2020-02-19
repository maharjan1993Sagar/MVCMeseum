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

namespace Meseum.Controllers
{
    [Authorize]
    public class GalleriesController : Controller
    {
        private MeseumContext db = new MeseumContext();
        string[] ImageExt = { ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".SVG" };
        // GET: Galleries
        public ActionResult Index()
        {
            return View(db.Gallery.ToList());
        }
        [AllowAnonymous]
        public ActionResult DetailsUser()
        {
            return View(db.Gallery.Include(m=>m.Files));
        }

        // GET: Galleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }
        public ActionResult Gallery(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("DetailsUser");
            }
            Gallery gallery = db.Gallery.Include(m=>m.Files).FirstOrDefault(m=>m.Id==id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }
        // GET: Galleries/Create
        public ActionResult Create()
        {
            Gallery gal = new Gallery();
            return View(gal);
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gallery gallery, List<HttpPostedFileBase> Images)
        {
            if (ModelState.IsValid)
            {
                db.Gallery.Add(gallery);
                db.SaveChanges();



                gallery = db.Gallery.OrderByDescending(m => m.Id).FirstOrDefault();
                int Id = db.Gallery.OrderByDescending(m => m.Id).FirstOrDefault().Id;
                string id = Id.ToString();
                if (id != null)
                {
                    if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/Gallery/" + id))))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Gallery/" + id));
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
                                    string path = Server.MapPath("~/Admin/Images/Gallery/") + id + "/" + InputFileName;
                                    var ServerSavePath = Path.Combine(path);
                                    //Save file to server folder  
                                    fi.SaveAs(ServerSavePath);
                                    if (System.IO.File.Exists(path))
                                    {
                                        ImageFile file = new ImageFile
                                        {
                                            Name = InputFileName,
                                            Size = size,
                                            path = "~/Admin/Images/Gallery/" + id + "/" + InputFileName,
                                            Type = "Image",
                                            UploadedBy = "Admin",
                                            UploadedDate = DateTime.Now

                                        };
                                        db.ImageFile.Add(file);
                                        db.SaveChanges();

                                        ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();
                                        gallery.Files.Add(image);
                                        db.SaveChanges();

                                    }
                                }

                            }
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // GET: Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gallery gallery,List<HttpPostedFileBase> Images)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();

                string id = gallery.Id.ToString() ;
                if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/Gallery/" + id))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Gallery/" + id));
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
                                string path = Server.MapPath("~/Admin/Images/Gallery/") + id + "/" + InputFileName;
                                var ServerSavePath = Path.Combine(path);
                                //Save file to server folder  
                                fi.SaveAs(ServerSavePath);
                                if (System.IO.File.Exists(path))
                                {
                                    ImageFile file = new ImageFile
                                    {
                                        Name = InputFileName,
                                        Size = size,
                                        path = "~/Admin/Images/Gallery/" + id + "/" + InputFileName,
                                        Type = "Image",
                                        UploadedBy = "Admin",
                                        UploadedDate = DateTime.Now

                                    };
                                    db.ImageFile.Add(file);
                                    db.SaveChanges();

                                    ImageFile image = db.ImageFile.OrderByDescending(m => m.Id).FirstOrDefault();
                                    gallery.Files.Add(image);
                                    db.SaveChanges();

                                }
                            }

                        }
                    }
                }



                return RedirectToAction("Index");
            }
            return View(gallery);
        }

        // GET: Galleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Gallery.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IEnumerable<ImageFile> files = db.Gallery.Include(mbox => mbox.Files).FirstOrDefault(m => m.Id == id).Files;
            foreach (var item in files)
            {
                if (System.IO.File.Exists(Server.MapPath(item.path)))
                {
                    System.IO.File.Delete(Server.MapPath(item.path));
                }
                db.ImageFile.Remove(item);
                db.SaveChanges();
            }

            Gallery gallery = db.Gallery.Find(id);

            
            db.Gallery.Remove(gallery);
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
