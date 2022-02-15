using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.Models
{
    public class Cart
    {
        [Key]
        public string CartItemId { get; set; }

        public string SessionId { get; set; }
        public int BookId { get; set; }

        public int Quantity { get; set; }

        public virtual Book Book { get; set; }
    }
}
