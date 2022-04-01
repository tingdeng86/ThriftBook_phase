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
        decimal TOTAL_PRICE = 0m;


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

        [Authorize]
       

            [Authorize]
        public IActionResult Checkout(decimal totalPayment)
            {
            string sessionId = HttpContext.Session.Id;

            ViewData["TotalPrice"] = totalPayment;

                string userEmail = User.Identity.Name;
                ProfileRepo prRepo = new ProfileRepo(_context);
                int buyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;
                ViewData["BuyerID"] = buyerId;

                PaymentRepo pmRepo = new PaymentRepo(_context);
                var cartObjects = pmRepo.GetOrderData(sessionId, totalPayment, buyerId);

            //perform a check of books (by Id) in db to make sure there's enough:
            CartRepo cartRepo = new CartRepo(_context);
            foreach (var booksObj in cartObjects.ToList())
            {
                booksObj.isValid = cartRepo.GetBooks(booksObj);
            }
            return View(cartObjects);
                //return RedirectToAction("Index", "Cart", new { message = ViewData["TotalPrice"] });
            }

        // This method receives and stores the Paypal transaction details.
        [HttpPost]
        public JsonResult PaySuccess([FromBody] IPN ipn)
        {
            CartRepo cartRepo = new CartRepo(_context);

            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            int buyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;

            string paymentId = ipn.PaymentId;
            string sessionId = HttpContext.Session.Id;
            try
            {
                _context.IPNs.Add(ipn);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            try
            {
                decimal totalAmount = Convert.ToDecimal(ipn.amount);
                var nnn = cartRepo.CreateTransaction(totalAmount, buyerId, DateTime.Parse(ipn.Create_time), paymentId);
                var bookInv = cartRepo.CreateBookInvoice(sessionId, paymentId);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            return Json(ipn);

        }

        // Show transaction detail. 
        private CartVM ValidateOrder(CartVM bookObj)
        {
            bookObj.isValid = true;

            return bookObj;
        }

        // Show transaction detail. 
        public IActionResult FinishShopping(string paymentID)
        {
            //obtaining the object of the current order being placed:
            OrderDetailRepo coRepo = new OrderDetailRepo(_context);
            var currentOrder = coRepo.GetOrder(paymentID);
            //updating books:
            CartRepo cartRepo = new CartRepo(_context);
            var books = cartRepo.UpdateBooks(paymentID);

            return View(currentOrder);
        }
    }
}


//[Authorize]
//public IActionResult Checkout(decimal totalPayment)
//{
//    int currentIndex = 0;
//    string sessionId = HttpContext.Session.Id;
//    bool indicator = false;

//    ViewData["TotalPrice"] = totalPayment;

//    string userEmail = User.Identity.Name;
//    ProfileRepo prRepo = new ProfileRepo(_context);
//    int buyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;
//    ViewData["BuyerID"] = buyerId;

//    PaymentRepo pmRepo = new PaymentRepo(_context);
//    var cartObjects = pmRepo.GetOrderData(sessionId, totalPayment, buyerId);
//    //var newCartObj = cartObjects;

//    //perform a check of books (by Id) in db to make sure there's enough:
//    CartRepo cartRepo = new CartRepo(_context);

//    //cartObjects.ToList()[0].isValid = true;
//    //cartObjects.ToList()[0].Price = 13m;
//    //var price = cartObjects.ToList()[0].Price;
//    //bool value = cartObjects.ToList()[0].isValid;

//    foreach (var booksObj in cartObjects.ToList())
//    {
//        booksObj.isValid = cartRepo.GetBooks(booksObj);
//        //if (booksObj.isValid == true) {
//        //    indicator = cartRepo.GetBooks(booksObj);
//        //    ValidateOrder(booksObj);
//        //    cartObjects.ToList()[currentIndex] = ValidateOrder(booksObj);
//        //cartObjects.ToList()[currentIndex].isValid = true;
//        //currentIndex = currentIndex + 1;
//        //}
//        //if (purchasePossible == false)
//        //{
//        //    return StatusCode(StatusCodes.Status500InternalServerError);
//        //}
//    }
//    return View(cartObjects);
//    //return RedirectToAction("Index", "Cart", new { message = ViewData["TotalPrice"] });
//}