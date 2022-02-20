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

        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Author")]
        public string Author { get; set; }

        [DisplayName("Genre")]
        public string Genre { get; set; }
        [DisplayName("Book Quality")]
        public string BookQuality { get; set; }

        [DisplayName("Book Quantity")]
        public int BookQuantity { get; set; }

        [DisplayName("Book Photo")]
        public string BookPhoto { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Store Name")]
        public string StoreName { get; set; }
        
    }
}
