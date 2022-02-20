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
            //Getting current user profile object
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            var currentRegisteredUser = prRepo.GetLoggedInUser(userEmail);
            //Seeking book review for the registered/signed-in user by book id passed from the link in html (ViewBooks)
            BookRatingRepo brRepo = new BookRatingRepo(_context);
            var allBookRatings = brRepo.SingleBookRating(currentRegisteredUser, bookId);
            

            return View(currentRegisteredUser);
        }
    }
}
