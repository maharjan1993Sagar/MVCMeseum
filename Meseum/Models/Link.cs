using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LinkUrl { get; set; } = "http://";
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        public string UploadedBy { get; set; } = "Admin";
    }
}