using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.Models
{
    public class Profile
    {
        [Key, Column(Order = 0)]
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? BuyerId { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual Store Store { get; set; }
    }
}
