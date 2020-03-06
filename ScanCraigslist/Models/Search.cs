using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScanCraigslist.Models
{
    public class Search
    {
        [Display(Name = "Search")]
        [Required(ErrorMessage="Search bar must not be empty.")]
        public string KeyWords { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }
        [Display(Name = "Must Include Search Words")]
        [Required]
        public bool MustIncludeWords { get; set; }
        public int AmountOfProcesses { get; set; }
    }
}
