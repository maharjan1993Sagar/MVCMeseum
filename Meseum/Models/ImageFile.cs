using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class ImageFile
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public Nullable<int> Size { get; set; }
        public string path { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        public string UploadedBy { get; set; } = "Admin";
    }
}