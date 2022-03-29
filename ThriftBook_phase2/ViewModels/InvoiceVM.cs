using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel;

using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.ViewModels
{
    public class InvoiceVM
    {

        [Key]

        [DisplayName("Payment ID")]
        public string PaymentId { get; set; }
        [DisplayName("Buyer ID")]
        public int BuyerId { get; set; }
        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime? DateOfTransaction { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }       

    } 
}
