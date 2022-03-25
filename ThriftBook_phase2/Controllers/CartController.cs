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
using ThriftBook_phase2.Repositories;
using Microsoft.AspNetCore.Authorization;
using rolesDemoSSD.Models;

namespace ThriftBook_phase2.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApplicationDbContext _context;      
        public CartController(ILogger<CartController> logger,
                              ApplicationDbContext context )
        {
            _logger = logger;
            _context = context;
        }
        const string CARTITEMS= "CartItems";

        public string GetSessionId()
        {
            if (HttpContext.Session.GetString("SessionId") == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {

                    HttpContext.Session.SetString("SessionId", HttpContext.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempSessionId = Guid.NewGuid();
                    HttpContext.Session.SetString("SessionId", tempSessionId.ToString());
                }
            }

            return HttpContext.Session.GetString("SessionId");
        }

        public IActionResult Index()
        {
            string sessionId = GetSessionId();
            //string aa = sessionID..Guid;
            //sessionId changes all the time, after setting, it would not change
            //HttpContext.Session.SetString("DUMB", "DUMB");

            //string sessionId = HttpContext.Session.Id;
            CartRepo cartRepo = new CartRepo(_context);
            var query = cartRepo.GetLists(sessionId);
            var totalItems = cartRepo.GetTotalItems(sessionId);
            var subTotals = cartRepo.GetSubTotal(sessionId);
            HttpContext.Session.SetInt32(CARTITEMS, totalItems);

            //HttpContext.Session.SetString("cart", new Cart {
                
            //})
            ViewData["TotalItems"] = HttpContext.Session.GetInt32(CARTITEMS);
            ViewData["SubTotal"] = subTotals;
            ViewData["Tax"] = Math.Round(subTotals * 0.12m, 2, MidpointRounding.ToEven);
            ViewData["Total"] = Math.Round(subTotals * 1.12m, 2, MidpointRounding.ToEven);
            return View(query);
        }

        public ActionResult Home()
        {

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Details(int id)
        {
            CartRepo cartRepo = new CartRepo(_context);
            var cartVM = cartRepo.GetDetail(id);           
            return View(cartVM);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                CartRepo cartRepo = new CartRepo(_context);
                cartRepo.Delete(id);
            }
            catch (Exception e)
            {
                ViewData["Message"] = e.Message;
            }
            return RedirectToAction("Index", "Cart", new { message = ViewData["Message"] });
        }
        public ActionResult IncreaseOne(int id)
        {
            CartRepo cartRepo = new CartRepo(_context);
            cartRepo.increaseQuantity(id);
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult DecreaseOne(int id)
        {
            CartRepo cartRepo = new CartRepo(_context);
            cartRepo.decreaseQuantity(id);
            return RedirectToAction("Index", "Cart");
        }


        //[Authorize]
        // update book test
        public ActionResult CheckoutTest(int transactionId)
        {
            string sessionId = GetSessionId();
            CartRepo cartRepo = new CartRepo(_context);
            var books = cartRepo.UpdateBooks(transactionId);
            cartRepo.EmptyCarts(sessionId);
            var totalItems = cartRepo.GetTotalItems(sessionId);
            HttpContext.Session.SetInt32(CARTITEMS, totalItems);
            return View(books);
        }

            [Authorize]
        public IActionResult Checkout(string sessionId, decimal totalPayment)
            {
                ViewData["TotalPrice"] = totalPayment;

                string userEmail = User.Identity.Name;
                ProfileRepo prRepo = new ProfileRepo(_context);
                int buyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;
                ViewData["BuyerID"] = buyerId;

                PaymentRepo pmRepo = new PaymentRepo(_context);
                var cartObject = pmRepo.GetOrderData(sessionId, totalPayment, buyerId);
                return View(cartObject);
                //return RedirectToAction("Index", "Cart", new { message = ViewData["TotalPrice"] });

            }

        [Authorize]
        public IActionResult CreateTransaction(decimal totalPrice)
        {
            string sessionId = GetSessionId(); ;
            string userEmail = User.Identity.Name;
            CartRepo cartRepo = new CartRepo(_context);
            cartRepo.MigrateCart(sessionId, userEmail);
            HttpContext.Session.SetString("SessionId", userEmail);
            string newSessionId = HttpContext.Session.GetString("SessionId");
            ProfileRepo prRepo = new ProfileRepo(_context);
            int buyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;
            DateTime date = DateTime.Now;
            int transactionId = cartRepo.CreateTransaction(totalPrice, buyerId, date);
            var query = cartRepo.CreateBookInvoice(newSessionId, transactionId);
            return View(query);
        }
    }
}
