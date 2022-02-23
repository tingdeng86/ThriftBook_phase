using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.ViewModels
{
    public class InvoiceVM
    {
        [Key]
        [DisplayName("Transaction ID")]
        public int TransactionId { get; set; }
        [DisplayName("Buyer ID")]
        public int BuyerId { get; set; }
        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime? DateOfTransaction { get; set; }
       
    }
}
