using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class Gallery
    {
        public Gallery()
        {
            Files = new List<ImageFile>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string  ShortDetails { get; set; }
        public string UploadedBy { get; set; } = "Admin";
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        public ICollection<ImageFile> Files { get; set; }
    }
}