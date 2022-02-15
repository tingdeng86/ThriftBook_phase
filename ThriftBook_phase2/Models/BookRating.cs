using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.Models
{
    public class BookRating
    {
        [Key, Column(Order = 0)]
        public int BookId { get; set; }

        [Key, Column(Order = 1)]
        public int BuyerId { get; set; }
        public decimal? Rating { get; set; }
        public string Comments { get; set; }

        public virtual Book Book { get; set; }
        public virtual Profile Profile{ get; set; }
    }
}
