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
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "This is not a valid city.")]
        public string City { get; set; }
        [RegularExpression(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$", ErrorMessage = "This is not a valid street address.")]
        public string Street { get; set; }

        [Display(Name = "Postal Code (eg. N1N 1N1)")]
        [RegularExpression(@"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage = "This is not a valid postal code.")]
        public string PostalCode { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "This is not a valid phone number.")]

        public string PhoneNumber { get; set; }
        public virtual ICollection<BookRating> BookRatings { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
