using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.Models
{
    public class BookInvoice
    {
        [Key, Column(Order = 0)]
        public int TransactionId { get; set; }
        [Key, Column(Order = 1)]
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Book Book { get; set; }
    }
}
