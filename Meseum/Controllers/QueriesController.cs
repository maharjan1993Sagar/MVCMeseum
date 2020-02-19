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
    public class QueriesController : Controller
    {
        private MeseumContext db = new MeseumContext();

        // GET: Queries
        public ActionResult Index()
        {
            return View(db.Queries.ToList());
        }

        // GET: Queries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queries queries = db.Queries.Find(id);
            if (queries == null)
            {
                return HttpNotFound();
            }
            return View(queries);
        }

        // GET: Queries/Create
        public ActionResult Create()
        {
            Queries que = new Queries();
            return View(que);
        }

        // POST: Queries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Queries queries)
        {
            if (ModelState.IsValid)
            {
                db.Queries.Add(queries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(queries);
        }

        // GET: Queries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queries queries = db.Queries.Find(id);
            if (queries == null)
            {
                return HttpNotFound();
            }
            return View(queries);
        }

        // POST: Queries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Queries queries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(queries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(queries);
        }

        // GET: Queries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queries queries = db.Queries.Find(id);
            if (queries == null)
            {
                return HttpNotFound();
            }
            return View(queries);
        }

        // POST: Queries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Queries queries = db.Queries.Find(id);
            db.Queries.Remove(queries);
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
