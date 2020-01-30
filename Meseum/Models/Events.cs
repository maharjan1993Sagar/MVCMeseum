using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class Events
    {
        public Events()
        {
            Files = new List<ImageFile>();
        }
        public int Id { get; set; }
        public string Organizer { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; } = DateTime.Now;
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public string UploadedBy { get; set; } = "Admin";
        public string Title { get; set; }
        public ICollection<ImageFile> Files { get; set; }
    }
}