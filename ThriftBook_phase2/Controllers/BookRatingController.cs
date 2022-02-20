using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
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

            //Seeking book review for the registered/signed-in user by book id passed from the link in html (ViewBooks).
            BookRatingRepo brRepo = new BookRatingRepo(_context);
            var singleBookRatingByUser = brRepo.SingleBookRating(currentRegisteredUser, bookId);

            return View(singleBookRatingByUser);
        }


        public IActionResult GetAllBookRatings(int bookId)
        {
            //Getting ALL BookRatings for a SPECIFIC book, id of which is passed from the HTML (ViewBooks) page. 

            BookRatingRepo brRepo = new BookRatingRepo(_context);
            var AllSingleBookRatings = brRepo.AllSingleBookRatings(bookId);
            return View(AllSingleBookRatings);
        }
    }
}
