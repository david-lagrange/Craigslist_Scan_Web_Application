using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanCraigslist.Models
{
    public class IndexViewModel
    {
        public Search Search { get; set; }
        public List<CraigslistListing> Listings { get; set; }
        public SelectList Categories = new SelectList(new CategoriesCollection().Categories, "Code", "Name");
        public string Error { get; set; }
    }
}
