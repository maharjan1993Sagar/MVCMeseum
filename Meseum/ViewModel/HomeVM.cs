using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meseum.Models;

namespace Meseum.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<Inventory> Inventories { get; set; }
        public IEnumerable<NewsEvent> NewsEvents { get; set; }
        public IEnumerable<Gallery> Galleries{ get; set; }
        public IEnumerable<Events> Events{ get; set; }
        public IEnumerable<AboutUs> AboutUs { get; set; }

    }
}