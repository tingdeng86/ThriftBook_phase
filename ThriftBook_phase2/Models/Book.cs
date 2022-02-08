using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.Models
{
    public class Book
    {
        [Key, Column(Order = 0)]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Gennre { get; set; }
        public string BookQuality { get; set; }
        public int? BookQuantity { get; set; }
        public string BookPhoto { get; set; }
        public decimal? Price { get; set; }
        public string StoreName { get; set; }


        public virtual Store StoreNameNavigation { get; set; }
        public virtual ICollection<BookRating> BookRatings { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
