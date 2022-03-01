using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class BookRatingController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly ApplicationDbContext _context;
        public BookRatingController(ILogger<ProfileController> logger,
                              ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View(); 
        }

            public IActionResult CurrentBookRating(int bookId)
        {
            //Getting SINGLE BookRating obj made by the current user for a SPECIFIC book
            ////////////////////////////////////////////////////////////////////////
            //Getting current user profile object
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            var currentRegisteredUser = prRepo.GetLoggedInUser(userEmail);

            //Seeking book review for the registered/signed-in user by book id passed from the link in --- html (ViewBooks)?.
            BookRatingRepo brRepo = new BookRatingRepo(_context);
            var singleBookRatingByUser = brRepo.SingleBookRating(currentRegisteredUser, bookId);

            return View(singleBookRatingByUser);
        }


        public IActionResult GetAllBookRatings(int bookId)
        {
            //Getting ALL BookRatings for a SPECIFIC book, id of which is passed from the --- HTML (ViewBooks) page?. 

            BookRatingRepo brRepo = new BookRatingRepo(_context);
            var AllSingleBookRatings = brRepo.AllSingleBookRatings(bookId);
            return View(AllSingleBookRatings);
        }


        [Authorize]
        // GET: Home/Create
        public IActionResult Create(int bookId)
        {
            return View();
        }


        [Authorize]
        // POST: Home/Create
        [HttpPost]
        public IActionResult Create([Bind("Rating, Comments, BookId")] BookRating newBookRating)
        {
            ViewData["Message"] = "Review Created Successfully";

            //Getting current user profile object
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            //Obtaining the BuyerID from the object of the registered in user and insert into object
            // of newly created book rating.
            newBookRating.BuyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;

            BookRatingRepo brRepo = new BookRatingRepo(_context);
            brRepo.CreateReview(newBookRating, userEmail);

            return RedirectToAction("CurrentBookRating", "BookRating", new { newBookRating.BookId });

        }


        [Authorize]
        public IActionResult Edit(int bookId)
        {
            //Getting current user profile object
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            //Obtaining the BuyerID from the object of the registered in user and insert into object
            // of newly created book rating.
            Profile currentBuyer = prRepo.GetLoggedInUser(userEmail);

            BookRatingRepo brRepo = new BookRatingRepo(_context);
            BookRating singleBookRating = brRepo.SingleBookRating(currentBuyer, bookId).FirstOrDefault();
            return View(singleBookRating);
        }

        [HttpPost]
        public IActionResult Edit([Bind("Rating, Comments")] BookRating editedBookRating)
        {
            ViewData["Message"] = "Review Edited Successfully";
            //Getting current user email
            string userEmail = User.Identity.Name;

            BookRatingRepo brRepo = new BookRatingRepo(_context);
            brRepo.EditReview(editedBookRating, userEmail);
            return RedirectToAction("CurrentBookRating", "BookRating", new { editedBookRating.BookId});
        }


    }
}
