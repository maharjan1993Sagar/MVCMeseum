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
using Meseum.BasicAuthentication;
using Meseum.Context;
using Meseum.Models;
using Meseum.ViewModel;

namespace Meseum.Controllers
{
    public class MeseumApiController : ApiController
    {
        private MeseumContext db = new MeseumContext();

        // GET: api/MeseumApi
        //[Authorize]
        [HttpGet]
        public List<LocationDto> GetLocations()
        {
            List<LocationDto> loc = (from l in db.Locations.Include(m => m.Categories)
                                     select new LocationDto
                                     {
                                         Id = l.Id,
                                         Categories = l.Categories,
                                         LongDetail = l.LongDetail,
                                         Name = l.Name,
                                         ShortDetail = l.ShortDetail,
                                         Thumbnail = "ninc.gov.np/meseum/Admin/Location/" + l.Id.ToString() + ".jpg",
                                         UpdatedAt = l.UpdatedAt,
                                         UpdatedBy = l.UpdatedBy
                                     }).ToList();
                                     
            return loc;
        }
        [HttpGet]
        public IQueryable<Category> GetCategories()
        {
            return db.Categories.Include(mbox => mbox.Location);
        }
        [HttpGet]
        public IEnumerable<InventoryDto> GetInventories()
        {
            IEnumerable<Inventory> Inventories = db.Inventories.Include(a => a.Category).Include(m => m.Location).Include(m => m.Files);

            IEnumerable<InventoryDto> InvDto = (from i in Inventories
                                                select new InventoryDto
                                                {
                                                    Id = i.Id,
                                                    Name = i.Name,
                                                    CategoryName = i.Category.Name,
                                                    LocationName = i.Location.Name,
                                                    Date = i.Date,
                                                    DetailStatus = i.DetailStatus,
                                                    Latit = i.Latit,
                                                    Long = i.Long,
                                                    LongDetail = i.LongDetail,
                                                    MadeBy = i.MadeBy,
                                                    Material = i.Material,
                                                    ObjectCode = i.ObjectCode,
                                                    OriginOf = i.OriginOf,
                                                    ShortDetail = i.ShortDetail,
                                                    size = i.size,
                                                    UpdatedAt = i.UpdatedAt,
                                                    UpdatedBy = i.UpdatedBy,
                                                    Files = (from f in i.Files
                                                             select new Files
                                                             {
                                                                 Name = f.Name,
                                                                 path = f.path.Replace("~", "ninc.gov.np/meseum"),
                                                                 Id = f.Id,
                                                                 Size = f.Size,
                                                                 Type = f.Type
                                                             }).AsEnumerable()
                                                }).AsEnumerable();
            return InvDto;//.Include(a=>a.Category).Include(m=>m.Location).Include(m=>m.Files);
        }
        [HttpGet]
        public IEnumerable<InventoryDto> InventoryByLocation(int id)
        {
            IEnumerable<Inventory> Inventories = db.Inventories.Include(a => a.Category).Include(m => m.Location).Include(m => m.Files);

            IEnumerable<InventoryDto> InvDto = (from i in Inventories
                                                where i.LocationId==id
                                                select new InventoryDto
                                                {
                                                    Id = i.Id,
                                                    Name = i.Name,
                                                    CategoryName = i.Category.Name,
                                                    LocationName = i.Location.Name,
                                                    Date = i.Date,
                                                    DetailStatus = i.DetailStatus,
                                                    Latit = i.Latit,
                                                    Long = i.Long,
                                                    LongDetail = i.LongDetail,
                                                    MadeBy = i.MadeBy,
                                                    Material = i.Material,
                                                    ObjectCode = i.ObjectCode,
                                                    OriginOf = i.OriginOf,
                                                    ShortDetail = i.ShortDetail,
                                                    size = i.size,
                                                    UpdatedAt = i.UpdatedAt,
                                                    UpdatedBy = i.UpdatedBy,
                                                    Thumbnail="ninc.gov.np/meseum/Admin/Images/Inventories/Thumb/"+i.Id.ToString()+".jpg",
                                                    Files = (from f in i.Files
                                                             select new Files
                                                             {
                                                                 Name = f.Name,
                                                                 path = f.path.Replace("~", "ninc.gov.np/meseum"),
                                                                 Id = f.Id,
                                                                 Size = f.Size,
                                                                 Type = f.Type
                                                             }).AsEnumerable()
                                                }).AsEnumerable();
            return InvDto;//.Include(a=>a.Category).Include(m=>m.Location).Include(m=>m.Files);
        }
        // GET: api/MeseumApi/5
        [ResponseType(typeof(LocationDto))]
        public IHttpActionResult GetLocation(int id)
        {
            Location l = db.Locations.Include(mbox => mbox.Categories).FirstOrDefault(m => m.Id == id);
            LocationDto locationDto = new LocationDto
            {
                Id = l.Id,
                Categories = l.Categories,
                LongDetail = l.LongDetail,
                Name = l.Name,
                ShortDetail = l.ShortDetail,
                Thumbnail = "ninc.gov.np/meseum/Admin/Location/" + l.Id.ToString() + ".jpg",
                UpdatedAt = l.UpdatedAt,
                UpdatedBy = l.UpdatedBy
            };
            if (locationDto == null)
            {
                return NotFound();
            }

            return Ok(locationDto);
        }
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category location = db.Categories.Include(mbox => mbox.Location).FirstOrDefault(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }
        [ResponseType(typeof(InventoryDto))]
        public IHttpActionResult GetInventory(int id)
        {
            Inventory i = db.Inventories.Include(mbox => mbox.Category).Include(m => m.Location)
                                .Include(m => m.Files).FirstOrDefault(m => m.Id == id);
            InventoryDto InvDto = new InventoryDto
            {
                Id = i.Id,
                Name = i.Name,
                CategoryName = i.Category.Name,
                LocationName = i.Location.Name,
                Date = i.Date,
                DetailStatus = i.DetailStatus,
                Latit = i.Latit,
                Long = i.Long,
                LongDetail = i.LongDetail,
                MadeBy = i.MadeBy,
                Material = i.Material,
                ObjectCode = i.ObjectCode,
                OriginOf = i.OriginOf,
                ShortDetail = i.ShortDetail,
                size = i.size,
                UpdatedAt = i.UpdatedAt,
                UpdatedBy = i.UpdatedBy,
                Thumbnail = "ninc.gov.np/meseum/Admin/Images/Inventories/Thumb/" + i.Id.ToString() + ".jpg",
                Files = (from f in i.Files
                         select new Files
                         {
                             Name = f.Name,
                             path = f.path.Replace("~", "ninc.gov.np/meseum"),
                             Id = f.Id,
                             Size = f.Size,
                             Type = f.Type
                         }).AsEnumerable()
            };


            if (i == null)
            {
                return NotFound();
            }

            return Ok(InvDto);
        }
        [ResponseType(typeof(Files))]
        public IHttpActionResult GetFiles(int id)
        {
            Inventory i = db.Inventories.Include(mbox => mbox.Category).Include(m => m.Location)
                                .Include(m => m.Files).FirstOrDefault(m => m.Id == id);
            IEnumerable<Files> files = (from f in i.Files
                                        select new Files
                                        {
                                            Name = f.Name,
                                            path = f.path.Replace("~", "ninc.gov.np/meseum"),
                                            Id = f.Id,
                                            Size = f.Size,
                                            Type = f.Type
                                        }).AsEnumerable();
          
            return Ok(files);
        }
        //[BasicAuthentication]
        [HttpPost]
        public IHttpActionResult EditLongLati(LongLatitEditVM longi)
        {
            if(longi.Username=="spadmin" &&longi.Password=="password1@")
                       {
                Inventory Inv = db.Inventories.Find(longi.InventoryId);
                Inv.Long = longi.Long;
                Inv.Latit = longi.Latit;
                Inv.Altitude = longi.Altitude;
                db.SaveChanges();
                return Ok("Latitude, longitude and altitude updated successfully.");
            }
            else
            {
                return BadRequest("Error! Failed to update the latitude longitude and altitude of inventory.");
            }
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