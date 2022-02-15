using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;

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
        public IActionResult Profile()
        {
            string userName = User.Identity.Name;

            return View();
        }
    }
}
