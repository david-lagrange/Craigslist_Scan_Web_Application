using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanCraigslist.Models
{
    public class CraigslistListings
    {
        public CraigslistListing[] listings { get; set; }
    }

    public class CraigslistListing
    {
        public string id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string post_date { get; set; }
        public string price { get; set; }
        public string photo { get; set; }
        public bool has_image { get; set; }
    }
}
