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
    public class BookRatingVM
    {
        [Key]
        public int BookId { get; set; }

        public int BuyerId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public decimal Rating { get; set; }
        public string Comments { get; set; }      

    }
}
