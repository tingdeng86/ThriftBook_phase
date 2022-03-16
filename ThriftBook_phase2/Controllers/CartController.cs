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
        public IActionResult Index()
        {
            //sessionId changes all the time, after setting, it would not change
            HttpContext.Session.SetString("DUMB", "DUMB");

            string sessionId = HttpContext.Session.Id;
            CartRepo cartRepo = new CartRepo(_context);
            var query = cartRepo.GetLists(sessionId);
            var totalItems = cartRepo.GetTotalItems(sessionId);
            var subTotals = cartRepo.GetSubTotal(sessionId);
            HttpContext.Session.SetInt32(CARTITEMS, totalItems);
            
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

<<<<<<< HEAD

        //// Home page shows list of items. Item price is set through ViewBag.
        //public IActionResult Payment()
        //{
        //    ViewBag.TotalPrice = "3.55";
        //    var items = _context.IPNs;
        //    return View(items);
        //}


        [Authorize]
        public IActionResult Checkout(string sessionId, decimal totalPayment)
        {
            ViewData["TotalPrice"] = totalPayment;
=======
>>>>>>> master

        //[Authorize]
        // update book test
        public ActionResult CheckoutTest(int transactionId)
        {
            string sessionId = HttpContext.Session.Id;
            CartRepo cartRepo = new CartRepo(_context);
            var books = cartRepo.UpdateBooks(transactionId);
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
            string sessionId = HttpContext.Session.Id;
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            int buyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;
<<<<<<< HEAD
            ViewData["BuyerID"] = buyerId;

            PaymentRepo pmRepo = new PaymentRepo(_context);
            var cartObject = pmRepo.GetOrderData(sessionId, totalPayment, buyerId);
            return View(cartObject);
=======
            DateTime date = DateTime.Now;
            CartRepo cartRepo = new CartRepo(_context);
            int transactionId = cartRepo.CreateTransaction(totalPrice, buyerId, date);
            var query = cartRepo.CreateBookInvoice(sessionId, transactionId);
            return View(query);
>>>>>>> master
        }


        //// This method receives and stores the Paypal transaction details.
        //[HttpPost]
        //public JsonResult PaySuccess([FromBody] IPN ipn)
        //{
        //    try
        //    {
        //        _context.IPNs.Add(ipn);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //    return Json(ipn);
        //}

        //// Show transaction detail. 
        //public IActionResult FinishShopping(string paymentID)
        //{
        //    IPN transaction = _context.IPNs.Where(t => t.TransactionId == paymentID).FirstOrDefault();
        //    return View(transaction);
        //}

    }
}
