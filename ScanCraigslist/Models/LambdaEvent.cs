using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanCraigslist.Models
{
    public class LambdaEvent
    {
        public string amount_of_lists { get; set; }
        public string category { get; set; }
        public string search_query { get; set; }
    }
}
