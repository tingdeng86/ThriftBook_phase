﻿using System;
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
        //[Key, Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string PaymentId { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateOfTransaction { get; set; }
        //public string PaymentId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<BookInvoice> BookInvoices { get; set; }
    }
}
