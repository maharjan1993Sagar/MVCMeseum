using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Meseum.Models
{
    public class NewsEvent
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = "Admin";
    }
}