using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.ViewModels
{
    public class BookVM
    {
        [Key]
        [DisplayName("Book ID")]
        public int BookID { get; set; }
        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Author")]
        public string Author { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [DisplayName("Genre")]
        public string Genre { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [DisplayName("Book Quality")]
        public string BookQuality { get; set; }
        [Required]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Whole and positive numbers only please")]
        [DisplayName("Book Quantity")]
        public int BookQuantity { get; set; }
        [Required]
        [DisplayName("Book Photo")]
        public string BookPhoto { get; set; }
        [Required]
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Store Name")]
        public string StoreName { get; set; }
        
    }
}
