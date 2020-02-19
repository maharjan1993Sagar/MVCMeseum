using Meseum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.ViewModel
{
    public class LocationDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string ShortDetail { get; set; }
        public string LongDetail { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
        public string Thumbnail { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}