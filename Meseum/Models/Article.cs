using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string UploadedBy { get; set; } = "Admin";
        public ImageFile File { get; set; }
    }
}