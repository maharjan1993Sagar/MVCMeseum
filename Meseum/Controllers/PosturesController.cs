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
    [Authorize]
    public class PosturesController : Controller
    {
        private MeseumContext db = new MeseumContext();

        // GET: Postures
        public ActionResult Index()
        {
            return View(db.Postures.ToList());
        }

        // GET: Postures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posture posture = db.Postures.Find(id);
            if (posture == null)
            {
                return HttpNotFound();
            }
            return View(posture);
        }

        // GET: Postures/Create
        public ActionResult Create()
        {
            Posture posture = new Posture();
            return View(posture);
        }

        // POST: Postures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Posture posture)
        {
            if (ModelState.IsValid)
            {
                posture.UpdatedAt = DateTime.Now;
                posture.UpdatedBy = User.Identity.Name;
                db.Postures.Add(posture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(posture);
        }

        // GET: Postures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posture posture = db.Postures.Find(id);
            if (posture == null)
            {
                return HttpNotFound();
            }
            return View(posture);
        }

        // POST: Postures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Posture posture)
        {
            if (ModelState.IsValid)
            {
                posture.UpdatedAt = DateTime.Now;
                posture.UpdatedBy = User.Identity.Name;
                db.Entry(posture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posture);
        }

        // GET: Postures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posture posture = db.Postures.Find(id);
            if (posture == null)
            {
                return HttpNotFound();
            }
            return View(posture);
        }

        // POST: Postures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posture posture = db.Postures.Find(id);
            db.Postures.Remove(posture);
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
