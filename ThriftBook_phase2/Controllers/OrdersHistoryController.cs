using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Models;
using ThriftBook_phase2.Repositories;

namespace ThriftBook_phase2.Controllers
{
    //[Authorize]
    public class OrdersHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrdersHistoryController( ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            ViewData["Message"] = "";
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            int buyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;
            OrdersHistoryRepo oRepo = new OrdersHistoryRepo(_context);
            IQueryable<Invoice> iVM = oRepo.GetOrdersLists(buyerId);
            if (iVM == null)
            {
                ViewData["Message"] = "No orders history.";
            }
            return View(iVM);
            
        }

        public IActionResult OrderDetail(string paymentID)
        {
            ViewData["Message"] = "";
            OrderDetailRepo coRepo = new OrderDetailRepo(_context);
            BookRatingRepo brRepo = new BookRatingRepo(_context);
            string userEmail = User.Identity.Name;
            var query = coRepo.GetOrder(paymentID);
            Dictionary<int, bool> bookIds = new Dictionary<int, bool>();

            //check the book is rated or not, if rated, the button of "rate the book" would be hidden
            foreach (var item in query)
            {
                //ViewBag[item.BookId] = "";
                var isFinded = brRepo.FindRating(userEmail, item.BookId);
                if (isFinded == true)
                {
                    bookIds.Add(item.BookId, isFinded);
                    
                }
            }
            ViewBag.Message = bookIds;
            return View(query);
        }

        [Authorize]
        // GET: Home/Create
        public IActionResult CreateRating(int bookId)
        {
            ViewData["Message"] = "";
            string userEmail = User.Identity.Name;

            BookRatingRepo brRepo = new BookRatingRepo(_context);
            var isFinded = brRepo.FindRating(userEmail, bookId);
            if(isFinded == true)
            {
                ViewData["Message"] = "You already rated this book.";
            }
            return View();
        }


        [Authorize]
        // POST: Home/Create
        [HttpPost]
        public IActionResult CreateRating([Bind("Rating, Comments, BookId")] BookRating newBookRating)
        {

            //Getting current user profile object
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            //Obtaining the BuyerID from the object of the registered in user and insert into object
            // of newly created book rating.
            newBookRating.BuyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;

            BookRatingRepo brRepo = new BookRatingRepo(_context);
            brRepo.CreateReview(newBookRating, userEmail);
            ViewData["Message"] = "Review Created Successfully. Thank you for helping to make the ThriftBook community better!";


            return RedirectToAction("ThankRating", "OrdersHistory", new { message = ViewData["Message"] });

        }

        public IActionResult ThankRating(string message)
        {
            if (message == null)
            {
                message = "";
            }
            ViewData["Message"] = message;
            return View();
        }
    }
}
