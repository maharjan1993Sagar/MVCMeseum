using Meseum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Meseum.ViewModel
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string  LocationName { get; set; }
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
        public string Thumbnail { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
        public IEnumerable<Files> Files { get; set; } = null;
    }
}