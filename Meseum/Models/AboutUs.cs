using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class AboutUs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MenuName { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        public string UploadedBy { get; set; } = "Admin";
        public ImageFile File { get; set; }
        
        public string Details { get; set; }
    }
}