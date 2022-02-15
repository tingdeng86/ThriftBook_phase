using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Models;

namespace ThriftBook_phase2.Repositories
{
    public class CartRepo
    {
        private readonly ApplicationDbContext _context;
        public CartRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        //public Cart AddToCart(int id)
        //{
        //    string sessionId = HttpContext.Session.Id;
        //    // Retrieve the product from the database.           


        //    var cartItem = _context.Cart.SingleOrDefault(
        //        c => c.SessionId == sessionId
        //        && c.BookId == id);
        //    if (cartItem == null)
        //    {
        //        // Create a new cart item if no cart item exists.                 
        //        Cart cart = new Cart()
        //        {

        //            BookId = id,
        //            SessionId = sessionId,
        //            Book = _context.Book.SingleOrDefault(
        //           p => p.BookId == id),
        //            Quantity = 1,

        //        };

        //        _context.Cart.Add(cart);
        //        return cart;
        //    }
        //    else
        //    {
        //        // If the item does exist in the cart,                  
        //        // then add one to the quantity.                 
        //        cartItem.Quantity++;
        //    }
        //    _context.SaveChanges();
        //    return cartItem;
        //}
    }
}
