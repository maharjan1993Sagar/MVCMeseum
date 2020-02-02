using Meseum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meseum.ViewModel
{
    public class NewsDetails
    {
        public NewsEvent News { get; set; }
        public IEnumerable<NewsEvent> RecentNews { get; set; }

    }
}