using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Meseum.ViewModel
{
    public class InventoryVM
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public SelectList Categories { get; set; }
        [Required]
        public int LocationId { get; set; }
        public SelectList  Locations { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Material { get; set; }
        public string ObjectCode { get; set; }
        public string size { get; set; }
        public string OriginOf { get; set; }
        public string MadeBy { get; set; }
        public string ShortDetail { get; set; }
        public string LongDetail { get; set; }
        public bool DetailStatus { get; set; }
        public decimal Long { get; set; }
        public decimal Latit { get; set; }
        public decimal Altitude { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
        public string[] Images { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public HttpPostedFileBase File { get; set; }
        public bool PhotoStatus { get; set; } = false;
    }
}