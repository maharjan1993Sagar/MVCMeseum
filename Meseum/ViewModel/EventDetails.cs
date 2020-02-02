using Meseum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.ViewModel
{
    public class EventDetails
    {
        public Events Event { get; set; }
        public IEnumerable<Events> RecentEvents { get; set; }

    }
}