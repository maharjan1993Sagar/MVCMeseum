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
    }
}