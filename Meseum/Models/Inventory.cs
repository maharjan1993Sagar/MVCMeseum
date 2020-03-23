using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Material { get; set; }
        public string ObjectCode { get; set; }
        public string size { get; set; }
        public string OriginOf { get; set; }
        public string MadeBy { get; set; }
        public string ShortDetail { get; set; }
        public string LongDetail { get; set; }
        public bool DetailStatus { get; set; } = false;
        public decimal Long { get; set; }
        public decimal Latit { get; set; }
        public decimal Altitude { get; set; }
        //public bool AudioType { get; set; }
        //public bool VideoType { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
        public bool PhotoStatus { get; set; } = false;
        public bool IsPublic { get; set; } = false;
        public ICollection<Files> Files { get; set; }
    }
}