using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanCraigslist.Models
{
    public class CategoriesCollection
    {
        public List<Category> Categories = new List<Category>()
        {
            new Category()
            {
                Name = "Motorcycles",
                Code = "mca"
            },
            new Category()
            {
                Name = "Instruments",
                Code = "msa"
            }
        };
    }

    public class Category
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
