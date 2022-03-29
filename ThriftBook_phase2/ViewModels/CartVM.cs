using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.ViewModels
{
    public class CartVM
    {      
        [Key]
        public int CartItemId { get; set; }
        public string SessionId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string BookPhoto { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int TotalQuantity { get; set; }
        public bool isValid { get; set; }
    }
}
