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
        [DisplayName("Transaction ID")]
        public int TransactionId { get; set; }
        [DisplayName("Buyer ID")]
        public int BuyerId { get; set; }
        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime? DateOfTransaction { get; set; }
        [DisplayName("Book Id")]
        public int BookId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "This is not a valid first name.")]
        [StringLength(50, ErrorMessage = "Name must be maximum of 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name required.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "This is not a valid last name.")]
        [StringLength(50, ErrorMessage = "Name must be maximum of 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required.")]
        [EmailAddress(ErrorMessage = "Invalid email Address")]

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email required.")]
        [EmailAddress(ErrorMessage = "Invalid email Address")]
        public string Email { get; set; }



    }
}
