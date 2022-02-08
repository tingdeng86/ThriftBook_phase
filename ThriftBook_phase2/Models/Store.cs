using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Models;

namespace rolesDemoSSD.Models
{
    public class Store
    {
        [Key]
        public string StoreName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
