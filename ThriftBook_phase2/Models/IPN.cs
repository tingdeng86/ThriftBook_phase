using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.Models
{
    public class IPN
    {
        // This lets you link the request to paypal with the response.
        public string custom { get; set; }

        //[Key, Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        //[Display(Name = "ID")]
        [Key] // Define primary key.
        public string PaymentId { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? DateOfTransaction { get; set; }
        public int BookId { get; set; }

        public string cart { get; set; }
        public string Create_time { get; set; }

        //Payer data.
        public string payerEmail { get; set; }

        //Payment data.
        public string amount { get; set; }
        public string currency { get; set; }
        public string paymentMethod { get; set; }
        public string paymentState { get; set; }

    }
}
