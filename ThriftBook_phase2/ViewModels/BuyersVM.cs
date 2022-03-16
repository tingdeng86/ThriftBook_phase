using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Models;

namespace ThriftBook_phase2.ViewModels
{
    public class BuyersVM
    {
        [Required]
        [Display(Name = "Buyer Id")]
        public int BuyerId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        //public virtual ICollection<BookRating> BookRatings { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
