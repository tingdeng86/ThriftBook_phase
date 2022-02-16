using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using Microsoft.AspNetCore.Http;
using ThriftBook_phase2.Models;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Controllers
{
    
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApplicationDbContext _context;      
        public CartController(ILogger<CartController> logger,
                              ApplicationDbContext context
                           
)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            var key = HttpContext.Request.Cookies.Keys.First();
            var cookId = HttpContext.Request.Cookies[key];


            Console.WriteLine(HttpContext.Session);

            var items =  _context.Cart.Where(
                c => c.SessionId == cookId).ToList();

            return View(items);
        }
        public ActionResult Details(int id, string message)
        {
            //var query = from c in _context.Cart
            //            from b in _context.Book
            //            where c.CartItemId == id
            //            where b.BookId == c.BookId
            //            select new CartVM() { 
            //                CartItemId=c.CartItemId,
            //                SessionId =c.SessionId,
            //                BookId=c.BookId,
            //                Title =b.Title,
            //                BookPhoto=b.BookPhoto,
            //                Price=b.Price,
            //            };
            var cartItem = _context.Cart.Where(c=>c.CartItemId==id).FirstOrDefault();
            var book = _context.Book.Where(b => b.BookId == cartItem.BookId).FirstOrDefault();
            CartVM cartVM = new CartVM()
            {
                CartItemId = cartItem.CartItemId,
                SessionId = cartItem.SessionId,
                BookId = cartItem.BookId,
                Title = book.Title,
                BookPhoto = book.BookPhoto,
                Price = book.Price,
            };

            ViewData["Message"] = message;
            return View(cartVM);
        }
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            //string sessionId = HttpContext.Session.Id;
            //sessionId changes all the time.
            var key = HttpContext.Request.Cookies.Keys.First();
            var cookId = HttpContext.Request.Cookies[key];
            // Retrieve the product from the database.           

            var cartItem = _context.Cart.Where(
                c => c.SessionId == cookId
                && c.BookId == id).FirstOrDefault();
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                Cart cart = new Cart()
                {

                    BookId = id,
                    SessionId = cookId,                   
                    Quantity = 1,
                    
                };

                _context.Cart.Add(cart);
                _context.SaveChanges();
                return RedirectToAction("Details", "Cart", new {id=cart.CartItemId, message ="Message"} );
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
                _context.SaveChanges();
                return RedirectToAction("Details", "Cart", new { id=cartItem.CartItemId });
            }
            
            
        }
    }
}
