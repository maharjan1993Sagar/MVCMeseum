using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string UploadedBy { get; set; } = "Admin";
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        public ImageFile Image { get; set; }
    }
}