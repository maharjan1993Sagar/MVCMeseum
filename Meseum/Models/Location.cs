using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShortDetail { get; set; }
        public string LongDetail { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
        public ICollection<Category> Categories { get; set; }
    }
}