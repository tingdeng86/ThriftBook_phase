using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Models;

namespace ThriftBook_phase2.ViewModels
{
    public class OrderDetailVM
    {
        [Key]
        //public int TransactionId { get; set; }
        public string PaymentId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public decimal Price { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public int Quantity { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Book Book { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<BookInvoice> BookInvoices { get; set; }
    }
}
