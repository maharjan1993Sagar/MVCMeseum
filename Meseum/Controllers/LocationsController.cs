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
{[Authorize]
    public class LocationsController : Controller
    {
        private MeseumContext db = new MeseumContext();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            Location location = new Location();
            return View(location);
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Location location,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                location.UpdatedAt = DateTime.Now;
                location.UpdatedBy = User.Identity.Name;
                db.Locations.Add(location);
                db.SaveChanges();

                Location loc = db.Locations.OrderByDescending(m => m.Id).First();
                if (!Directory.Exists(Server.MapPath("~/Admin/Images/Location/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Location/"));
                }
                file.SaveAs(Server.MapPath("~/Admin/Images/Location/" + loc.Id.ToString() + ".jpg"));

                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Location location,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                location.UpdatedAt = DateTime.Now;
                location.UpdatedBy = User.Identity.Name;
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();

             
                if (!Directory.Exists(Server.MapPath("~/Admin/Images/Location/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Location/"));
                }
                file.SaveAs(Server.MapPath("~/Admin/Images/Location/" + location.Id.ToString() + ".jpg"));

                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
