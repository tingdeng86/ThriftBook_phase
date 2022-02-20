using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using Microsoft.AspNetCore.Authorization;
using rolesDemoSSD.Models;
using ThriftBook_phase2.Repositories;

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
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            var currentRegisteredUser = prRepo.GetLoggedInUser(userEmail);
            return View(currentRegisteredUser);
        }


        [Authorize]
        public IActionResult Edit()
        {
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            var currentRegisteredUser = prRepo.GetLoggedInUser(userEmail);
            return View(currentRegisteredUser);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Edit([Bind("Email, FirstName, LastName, City, Street, PostalCode, PhoneNumber")] Profile userInfoEdited)
        {
            ViewData["Message"] = "Account Edited Successfully";
            string userEmail = User.Identity.Name;
            ProfileRepo prRepo = new ProfileRepo(_context);
            prRepo.EditingUserInfo(userInfoEdited, userEmail);
            return RedirectToAction(nameof(Index), new { ViewBag.Message });
        }
    }
}
