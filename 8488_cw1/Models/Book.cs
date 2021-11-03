using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _8488_cw1.Models
{
    public class Book
    {
        [Key]
        [Display(Name = "Book Id")]
        public int BookId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Available books")]
        public int Remaining { get; set; }
        public float Cost { get; set; }
        public string Author { get; set; }
    }
}
