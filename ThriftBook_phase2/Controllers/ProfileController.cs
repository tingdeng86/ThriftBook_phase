using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using Microsoft.AspNetCore.Authorization;
using rolesDemoSSD.Models;

namespace ThriftBook_phase2.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly ApplicationDbContext _context;
        public ProfileController(ILogger<ProfileController> logger,
                              ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            string userName = User.Identity.Name;

            var objects = _context.Profile;
            var registeredUser = _context.Profile.Where(ru => ru.Email == userName)
                                .FirstOrDefault();
            return View(registeredUser);
        }


        [Authorize]
        public IActionResult Edit()
        {
            string userName = User.Identity.Name;
            var currentUser = _context.Profile.Where(ru => ru.Email == userName)
                                .FirstOrDefault();
            return View(currentUser);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Edit([Bind("Email, FirstName, LastName, City, Street, PostalCode, PhoneNumber")] Profile userInfoEdited)
        {
            ViewData["Message"] = "Account Edited Successfully";
            string userName = User.Identity.Name;
            var currentUser = _context.Profile.Where(ru => ru.Email == userName)
                                .FirstOrDefault();
            currentUser.FirstName = userInfoEdited.FirstName;
            currentUser.LastName = userInfoEdited.LastName;
            currentUser.Email = userInfoEdited.Email;
            currentUser.City = userInfoEdited.City;
            currentUser.Street = userInfoEdited.Street;
            currentUser.PostalCode = userInfoEdited.PostalCode;
            currentUser.PhoneNumber = userInfoEdited.PhoneNumber;
            _context.SaveChanges();

            return View(currentUser);
        }
    }
}
