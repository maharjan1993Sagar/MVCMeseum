using AutoMapper;
using Meseum.Context;
using Meseum.Models;
using Meseum.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;
//#e1cfc5
namespace Meseum.Controllers
{ 
    public class HomeController : Controller
    {
        private readonly MeseumContext db = new MeseumContext();
        private readonly IMapper mapper;
        public ActionResult IndexUser()
        {
            //    HomeVM home = new HomeVM();
            //    home.Inventories = db.Inventories;
            //    home.NewsEvents = db.NewsEvents;
            //    return View(home);
            return RedirectToAction("IndexUser");
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            HomeVM home = new HomeVM();
            home.Inventories = db.Inventories;
            home.NewsEvents = db.NewsEvents.Include(m => m.Files);
            home.Galleries = db.Gallery.Include(m => m.Files);
            home.Events = db.Events.Include(m => m.Files);
            home.AboutUs = db.AboutUs.Include(m => m.File);
            return View(home);
        }
        [Authorize]
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Contact(Queries queries)
        {
            if (ModelState.IsValid)
            {
                queries.UploadedBy = queries.Name.Length == 0 ? "User" : queries.Name;
                queries.UploadedDate = DateTime.Now;
                db.Queries.Add(queries);
                db.SaveChanges();
                return Json("TRUE");
            }
            else
            {
                return Json("FALSE");
            }


        }
        public ActionResult QRcode()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Generate(QRCodeModel qrcode)
        {
            try
            {
                qrcode.QRCodeImagePath = GenerateQRCode(qrcode);
                ViewBag.Message = "QR Code Created successfully";
            }
            catch (Exception ex)
            {
                //catch exception if there is any
            }
            return View("QrCode", qrcode);
        }

        public ActionResult QRCodeGen(string id, string type)
        {
            if (string.IsNullOrEmpty(id))
            {
                QRCodeModel qrcode = new QRCodeModel();
                qrcode.FolderPath = "~/Admin/Images/QRCode";
                if (type == "Inventory")
                {
                    qrcode.QRCodeText = "ninc.gov.np/meseum/api/meseumapi/getinventory/" + id;
                    qrcode.FilePath = "~/Admin/Images/QRCode/Inventory" + id + ".jpg";
                }
                qrcode.QRCodeImagePath = GenerateQRCode(qrcode);
                return View("QrCode", qrcode);
            }
            else
            {
                return RedirectToAction("Dashboard", "Home");
            }
        }

        private string GenerateQRCode(QRCodeModel qr)
        {
            //string folderPath = "~/Admin/Images/QRCode";
            //string imagePath = "~/Admin/Images/QRCode/QrCode.jpg";
            // If the directory doesn't exist then create it.
            if (!Directory.Exists(Server.MapPath("~/Admin")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Admin"));
            }
            if (!Directory.Exists(Server.MapPath("~/Admin/Images")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Admin/Images"));
            }
            if (!Directory.Exists(Server.MapPath(qr.FolderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(qr.FolderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qr.QRCodeText);

            string barcodePath = Server.MapPath(qr.FilePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return qr.FilePath;
        }

        public ActionResult Read()
        {
            return View(ReadQRCode());
        }

        private QRCodeModel ReadQRCode()
        {
            QRCodeModel barcodeModel = new QRCodeModel();
            string barcodeText = "";
            string imagePath = "~/Admin/Images/QRCode/QrCode.jpg";
            string barcodePath = Server.MapPath(imagePath);
            var barcodeReader = new BarcodeReader();

            var result = barcodeReader.Decode(new Bitmap(barcodePath));
            if (result != null)
            {
                barcodeText = result.Text;
            }
            return new QRCodeModel() { QRCodeText = barcodeText, QRCodeImagePath = imagePath };
        }

        [Authorize]
        public ActionResult GetFiles(int? id, string type)
        {
            IEnumerable<ImageFile> files = null;
            if (id != null)
            {
                ViewBag.Type = type;
                if (type.ToUpper().Contains("NEWS"))
                {
                    files = db.NewsEvents.Include(m => m.Files).FirstOrDefault(m => m.Id == id.Value).Files;
                }
                else if (type.ToUpper().Contains("EVENT"))
                {
                    files = db.Events.Include(m => m.Files).FirstOrDefault(m => m.Id == id.Value).Files;
                }
                else if (type.ToUpper().Contains("GALLERY"))
                {
                    files = db.Gallery.Include(m => m.Files).FirstOrDefault(m => m.Id == id.Value).Files;
                }
                else if (type.ToUpper().Contains("INVENTORY"))
                {

                    IEnumerable<Files> filess = db.Inventories.Include(m => m.Files).FirstOrDefault(m => m.Id == id.Value).Files;
                    files = (from f in filess
                             select new ImageFile
                             {
                                 Id = f.Id,
                                 path = f.path,
                                 Name = f.Name,
                                 UploadedDate = f.UploadedDate,
                                 UploadedBy = f.UploadedBy,
                                 Type = f.Type,
                                 Size = f.Size
                             }).AsEnumerable();
                    // mapper.Map<IEnumerable<Files>, IEnumerable<ImageFile>>(filess);
                }
            }
            return View(files);
        }
        [Authorize]
        [HttpPost]
        public JsonResult DeleteFile(int? id, string type)
        {
            if (id != null)
            {
                if (type == "Inventory")
                {
                    Files fil = db.Files.Find(id.Value);
                    if (System.IO.File.Exists(Server.MapPath(fil.path)))
                    {
                        System.IO.File.Delete(Server.MapPath(fil.path));

                    }
                    db.Files.Remove(fil);
                    db.SaveChanges();
                    //Delete file as well as row in table
                    return Json("TRUE");
                }
                else
                {
                    ImageFile fil = db.ImageFile.Find(id.Value);
                    if (System.IO.File.Exists(Server.MapPath(fil.path)))
                    {
                        System.IO.File.Delete(Server.MapPath(fil.path));

                    }
                    db.ImageFile.Remove(fil);
                    db.SaveChanges();

                    //Delete file as well as row in table
                    return Json("TRUE");

                }
            }
            return Json("Error");
        }


        [HttpPost]
        public JsonResult GetBanner()
        {
            IEnumerable<string> urls = (from b in db.Banners
                                        select b.Image.path).AsEnumerable();
            return Json(urls);
        }
        [HttpPost]
        public JsonResult GetAboutUs()
        {
            var AboutUs = db.AboutUs;
            //(from a in db.AboutUs
            //                    select a.MenuName).AsEnumerable();
            return Json(AboutUs);
        }
    }
}