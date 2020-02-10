using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Meseum.Context;
using Meseum.Models;

namespace Meseum.Controllers
{
  
    public class MeseumApiController : ApiController
    {
        private MeseumContext db = new MeseumContext();     

        // GET: api/MeseumApi
        [HttpGet]
        public IQueryable<Location> GetLocations()
        {
            return db.Locations.Include(m=>m.Categories);
        }
        [HttpGet]
        public IQueryable<Category> GetCategories()
        {
            return db.Categories.Include(mbox=>mbox.Location);
        }
        [HttpGet]
        public IQueryable<Inventory> GetInventories()
        {
            return db.Inventories.Include(a=>a.Category).Include(m=>m.Files);
        }
        // GET: api/MeseumApi/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult GetLocation(int id)
        {
            Location location = db.Locations.Include(mbox=>mbox.Categories).FirstOrDefault(m=>m.Id==id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category location = db.Categories.Include(mbox=>mbox.Location).FirstOrDefault(m=>m.Id==id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult GetIventory(int id)
        {
            Inventory location = db.Inventories.Include(mbox=>mbox.Category).Include(m=>m.Location)
                                .Include(m=>m.Files).FirstOrDefault(m=>m.Id==id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationExists(int id)
        {
            return db.Locations.Count(e => e.Id == id) > 0;
        }
    }
}