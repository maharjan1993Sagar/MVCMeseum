using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.ViewModel
{
    public class LongLatitEditVM
    {
        public int InventoryId { get; set; }
        public decimal Long { get; set; }
        public decimal Latit { get; set; }
        public decimal Altitude { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}