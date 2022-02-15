using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using Microsoft.AspNetCore.Http;
using ThriftBook_phase2.Models;

namespace ThriftBook_phase2.Controllers
{
    
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApplicationDbContext _context;
        public CartController(ILogger<CartController> logger,
                              ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            string sessionID = HttpContext.Session.Id;
            var cart = AddToCart(1);
            return View(cart);
        }
        public Cart AddToCart(int id)
        {
            string sessionId = HttpContext.Session.Id;
            // Retrieve the product from the database.           
            

            var cartItem = _context.Cart.SingleOrDefault(
                c => c.SessionId == sessionId
                && c.BookId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                Cart cart = new Cart()
                {

                    BookId = id,
                    SessionId = sessionId,
                    Book = _context.Book.SingleOrDefault(
                   p => p.BookId== id),
                    Quantity = 1,
                    
                };

                _context.Cart.Add(cart);
                return cart;
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            _context.SaveChanges();
            return cartItem;
        }
    }
}
