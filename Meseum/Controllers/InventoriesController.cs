using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Meseum.Context;
using Meseum.Mapping;
using Meseum.Models;
using Meseum.ViewModel;
using AutoMapper;
using System.IO;
using System.Text.RegularExpressions;

namespace Meseum.Controllers
{
    [Authorize]
    public class InventoriesController : Controller
    {
        string[] AudioExt = { ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA" };
        string[] ImageExt = { ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".SVG" };
        string[] VideoExt = { ".AVI", ".MP4", ".DIVX", ".WMV", "FLV", "MOV" };

        private MeseumContext db = new MeseumContext();
        //private IMapper mapper;
        //public InventoriesController(MeseumContext meseumContext, IMapper _mapper)
        //{
        //    db = meseumContext;
        //    mapper = _mapper;
        //}

        //private MappingProfile _mapper = new MappingProfile();
        // GET: Inventories
        public ActionResult Index()
        {
            var inventories = db.Inventories.Include(i => i.Category).Include(i => i.Location);
            return View(inventories.ToList());
        }

        
        [AllowAnonymous]
        public ActionResult IndexUser()
        {
            List<Inventory> inventories = db.Inventories.Include(m=>m.Files).Include(i => i.Category).Include(i => i.Location).ToList();
            return View(inventories);
        }

        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Include(m => m.Files).First(m => m.Id == id);
            IEnumerable<Files> files = inventory.Files;

            InventoryDetails invDetails = new InventoryDetails { Inventory = inventory, Files = files.ToList() };
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(invDetails);
        }
        [AllowAnonymous]
        public ActionResult DetailsUser(int? id)
        {
            Inventory inventory = new Inventory();
            if (id == null)
            {
                inventory = db.Inventories.FirstOrDefault();
            }
            inventory = db.Inventories.Include(m => m.Files).First(m => m.Id == id);
            //IEnumerable<Files> files = inventory.Files;

            //InventoryDetails invDetails = new InventoryDetails { Inventory = inventory, Files = files.ToList() };
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }
        //public ActionResult Upload()
        //{
        //    return View();
        //}
        public ActionResult Uploads()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Uploads(FormCollection col, List<HttpPostedFileBase> files)
        {
            ViewBag.message = files.Count + "Nos of files uploaded";
            return View();
        }
        [HttpPost]
        public ActionResult Upload(FormCollection col, List<HttpPostedFileBase> Image, List<HttpPostedFileBase> Audio, List<HttpPostedFileBase> Video)
        {
            string id = col["Id"].ToString();
            int Id = Convert.ToInt32(id);
            if (id != null)
            {
                if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/Inventories/" + id + "/Image"))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Inventories/" + id + "/Image"));
                }

                if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/Inventories/" + id + "/Audio"))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Inventories/" + id + "/Audio"));
                }

                if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/Inventories/" + id + "/Video"))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Inventories/" + id + "/Video"));
                }

                if (Image.Count > 0)
                {
                    foreach (HttpPostedFileBase fi in Image)
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
                                string path = Server.MapPath("~/Admin/Images/Inventories/") + id + "/Image/" + InputFileName;
                                var ServerSavePath = Path.Combine(path);
                                //Save file to server folder  
                                fi.SaveAs(ServerSavePath);
                                if (System.IO.File.Exists(path))
                                {
                                    Files file = new Files
                                    {
                                        Name = InputFileName,
                                        Size = size,
                                        path = "~/Admin/Images/Inventories/" + id + "/Image/" + InputFileName,
                                        Type = "Image",
                                        InventoryId = Id,
                                        UploadedBy = "Admin",
                                        UploadedDate = DateTime.Now
                                    };
                                    db.Files.Add(file);
                                    db.SaveChanges();

                                }
                            }

                        }
                    }
                }
                if (Audio.Count > 0)
                {
                    foreach (HttpPostedFileBase fi in Audio)
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
                            if (AudioExt.Contains(extension.ToUpper()))
                            {
                                string path = Server.MapPath("~/Admin/Images/Inventories/") + id + "/Audio/" + InputFileName;
                                var ServerSavePath = Path.Combine(path);
                                //Save file to server folder  
                                fi.SaveAs(ServerSavePath);
                                if (System.IO.File.Exists(path))
                                {
                                    Files file = new Files
                                    {
                                        Name = InputFileName,
                                        Size = size,
                                        path = "~/Admin/Images/Inventories/" + id + "/Audio/" + InputFileName,
                                        Type = "Audio",
                                        InventoryId = Id,
                                        UploadedBy = "Admin",
                                        UploadedDate = DateTime.Now
                                    };
                                    db.Files.Add(file);
                                    db.SaveChanges();

                                }
                            }

                        }
                    }
                }
                if (Video.Count > 0)
                {
                    foreach (HttpPostedFileBase fi in Video)
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
                            if (VideoExt.Contains(extension.ToUpper()))
                            {
                                string path = Server.MapPath("~/Admin/Images/Inventories/") + id + "/Video/" + InputFileName;
                                var ServerSavePath = Path.Combine(path);
                                //Save file to server folder  
                                fi.SaveAs(ServerSavePath);
                                if (System.IO.File.Exists(path))
                                {
                                    Files file = new Files
                                    {
                                        Name = InputFileName,
                                        Size = size,
                                        path = "~/Admin/Images/Inventories/" + id + "/Video/" + InputFileName,
                                        Type = "Video",
                                        InventoryId = Id,
                                        UploadedBy = "Admin",
                                        UploadedDate = DateTime.Now
                                    };
                                    db.Files.Add(file);
                                    db.SaveChanges();

                                }
                            }
                        }

                    }

                }



            }

            return RedirectToAction("Index");
        }
        // GET: Inventories/Create
        public ActionResult Create()
        {
            InventoryVM inventoryVM = new InventoryVM();
            inventoryVM.Categories = new SelectList(db.Categories, "Id", "Name");
            inventoryVM.Locations = new SelectList(db.Locations, "Id", "Name");
            inventoryVM.UpdatedAt = DateTime.Now;
            inventoryVM.UpdatedBy = User.Identity.Name;
            return View(inventoryVM);
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryVM inventoryVM)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase[] files = inventoryVM.Files;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<InventoryVM, Inventory>();
                });

                IMapper mapper = config.CreateMapper();
                inventoryVM.UpdatedAt = DateTime.Now;
                inventoryVM.UpdatedBy = User.Identity.Name;
                Inventory inventory = mapper.Map<InventoryVM, Inventory>(inventoryVM);// Mapper.Map<InventoryVM, Inventory>(inventoryVM);
                db.Inventories.Add(inventory);
                db.SaveChanges();
                int id = db.Inventories.OrderByDescending(m => m.Id).First().Id;

                if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/Inventories/Thumb"))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Inventories/Thumb"));
                }
                if (inventoryVM.File != null)
                {
                    inventoryVM.File.SaveAs(Server.MapPath("~/Admin/Images/Inventories/Thumb/") + id.ToString() + ".jpg");
                }

                return RedirectToAction("Index");
            }

            inventoryVM.Categories = new SelectList(db.Categories, "Id", "Name", inventoryVM.CategoryId);
            inventoryVM.Locations = new SelectList(db.Locations, "Id", "Name", inventoryVM.LocationId);
            return View(inventoryVM);
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Inventory, InventoryVM>();
            });

            IMapper mapper = config.CreateMapper();
            inventory.Date = DateTime.Now;
            InventoryVM inventoryVM = mapper.Map<Inventory, InventoryVM>(inventory);
            if (Directory.Exists(Server.MapPath("~/Admin/Images/Inventories/" + id.ToString())))
            {
                string[] files = Directory.GetFiles(Server.MapPath("~/Admin/Images/Inventories/" + id.ToString()));

                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        inventoryVM.Images = files;
                    }
                }
            }
            if (inventory == null)
            {
                return HttpNotFound();
            }
            inventoryVM.Categories = new SelectList(db.Categories, "Id", "Name", inventory.CategoryId);
            inventoryVM.Locations = new SelectList(db.Locations, "Id", "Name", inventory.LocationId);
            return View(inventoryVM);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, InventoryVM inventoryVM)
        {
            if (ModelState.IsValid)
            {
                Inventory inventory = db.Inventories.Find(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<InventoryVM, Inventory>();
                });
                HttpPostedFileBase[] files = inventoryVM.Files;

                IMapper mapper = config.CreateMapper();

                inventoryVM.UpdatedAt = DateTime.Now;

                inventoryVM.UpdatedBy = User.Identity.Name;
                inventory = mapper.Map<InventoryVM, Inventory>(inventoryVM);
                if (TryUpdateModel(inventory))
                {
                    db.SaveChanges();
                }
                //db.Entry(inventory).State = EntityState.Modified;
                if (!Directory.Exists(Path.Combine(Server.MapPath("~/Admin/Images/Inventories/Thumb"))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Admin/Images/Inventories/Thumb"));
                }


                if (inventoryVM.File != null)
                {
                    inventoryVM.File.SaveAs(Server.MapPath("~/Admin/Images/Inventories/Thumb/") + id.ToString() + ".jpg");
                }


                return RedirectToAction("Index");
            }
            inventoryVM.Categories = new SelectList(db.Categories, "Id", "Name", inventoryVM.CategoryId);
            inventoryVM.Locations = new SelectList(db.Locations, "Id", "Name", inventoryVM.LocationId);
            return View(inventoryVM);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }
        [HttpPost]
        public JsonResult DeleteFile(string path)
        {
            if (Directory.Exists(Server.MapPath(path)))
            {
                Directory.Delete(Server.MapPath(path));
                return Json("true");
            }
            else
            {
                return Json("false");
            }
        }
        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IEnumerable<Files> files = db.Inventories.Include(mbox => mbox.Files).FirstOrDefault(m => m.Id == id).Files;
            foreach (var item in files)
            {
                if (System.IO.File.Exists(Server.MapPath(item.path)))
                {
                    System.IO.File.Delete(Server.MapPath(item.path));
                }
                db.Files.Remove(item);
                db.SaveChanges();
            }
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
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
