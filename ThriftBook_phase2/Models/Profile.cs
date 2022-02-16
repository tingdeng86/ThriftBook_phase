using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Models;

namespace rolesDemoSSD.Models
{
    public class Profile
    {
        //[Key]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuyerId { get; set; }

        [Required(ErrorMessage = "Email required.")]
        [EmailAddress(ErrorMessage = "Invalid email Address")]
        public string Email { get; set; }

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
        public string City { get; set; }
        public string Street { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<BookRating> BookRatings { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}

