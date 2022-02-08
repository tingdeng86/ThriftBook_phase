using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Models;

namespace rolesDemoSSD.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public int? BuyerId { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? DateOfTransaction { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual ICollection<BookInvoice> BookInvoices { get; set; }
    }
}
