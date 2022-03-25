using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Models;
using ThriftBook_phase2.Repositories;
using ThriftBook_phase2.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace ThriftBook_phase2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,
                              ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
            
        //Display Index with Detail view
        public IActionResult Index(string sortOrder, string searchString)
        {

            HttpContext.Session.SetString("DUMB", "DUMB");           
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";           
            ViewBag.GenreSortParam = String.IsNullOrEmpty(sortOrder) ? "genre_desc" : "";                        
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";

            var books = from b in _context.Book
                        select b;

            if(!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.ToLower().Contains(searchString) || b.Title.Contains(searchString) || b.Author.ToLower().Contains(sortOrder) || b.Author.Contains(sortOrder));
                ViewBag.NoBooksMessage = "The book " + searchString + " is currently not in stock";
            } 

            switch(sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(s => s.Title);
                    break;               
                case "genre_desc":
                    books = books.OrderBy(s => s.Gennre);
                    break;                             
                case "Price":
                    books = books.OrderBy(s => (double)s.Price);
                    break;
                default:
                    books = books.OrderBy(s => s.Title);
                    break;
<<<<<<< HEAD
            }            
=======
            }
>>>>>>> BookReview-and-PaymentController
            return View(books.ToList());
        }
        

        public ActionResult Details(int bookID)
        {
            BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
            BookVM bVM = bdRepo.Get(bookID);            
            return View(bVM);

        }
          

        [HttpGet]
        public ActionResult Edit(int bookID)
        {
            BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
            BookVM bVM = bdRepo.Get(bookID);
            return View(bVM);
        }

        [HttpPost]
        public ActionResult Edit(BookVM bVM)
        {
            BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
            bdRepo.Update(bVM);
            return RedirectToAction(nameof(Details), new { Title = bVM.Title, Author = bVM.Author, Genre = bVM.Genre, BookQuality = bVM.BookQuality, BookQuantity = bVM.BookQuantity, Price = bVM.Price, BookID = bVM.BookID });

        }

        [HttpGet]
        public ActionResult Create()
        {
            BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind("Title, Author, Genre, BookQuality, BookQuantity, BookPhoto, Price, StoreName")] BookVM bVM)
        {
            ViewData["Message"] = "";
            Book book;
            try
            {
                BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
                book = bdRepo.Add(bVM);

                ViewData["Message"] = "Created successfully";
                return RedirectToAction("Index", "Home", new { Title = bVM.Title, Author = bVM.Author, Genre = bVM.Genre, BookQuality = bVM.BookQuality, BookQuantity = bVM.BookQuantity, Price = bVM.Price, BookID = bVM.BookID });
            }
            catch (Exception e)
            {
                ViewData["Message"] = e.Message;
                return View(bVM);
            }
        }

        public ActionResult Delete(int id)
        {
            ViewData["Message"] = "";
            try
            {
                BookDetailVMRepo bdRepo = new BookDetailVMRepo(_context);
                bdRepo.Delete(id);
                ViewData["Message"] = "Deleted successfully";
            }
            catch (Exception e)
            {
                ViewData["Message"] = e.Message;
            }
            return RedirectToAction("Index", "Home", new { message = ViewData["Message"] });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public ActionResult AddToCart(int id)
        {
            string sessionId = HttpContext.Session.Id;
            CartRepo cartRepo = new CartRepo(_context);
            var book = cartRepo.GetBook(id);
            var cartItem = cartRepo.Find(id, sessionId);
            // update the amount of total items in the session
            var totalItems = HttpContext.Session.GetInt32("CartItems");
            totalItems =  totalItems == null ? 1 : totalItems + 1;
            HttpContext.Session.SetInt32("CartItems", (int)totalItems);
            if (cartItem == null && book.BookQuantity>0)
            {
                int cartItemId = cartRepo.Add(id, sessionId);
                return RedirectToAction("Details", "Cart", new { id = cartItemId });
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.
                cartRepo.increaseQuantity(cartItem.CartItemId);
                return RedirectToAction("Details", "Cart", new { id = cartItem.CartItemId });
            }
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
