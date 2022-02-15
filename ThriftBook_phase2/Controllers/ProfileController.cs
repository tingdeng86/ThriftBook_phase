using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // Usually this section would be in a repository.
            var objects = _context.Profile;
            var registeredUser = _context.Profile.Where(ru => ru.Email == userName)
                                .FirstOrDefault();// Use FirstOrDefault() when getting one item
            return View(registeredUser);
        }
    }
}
