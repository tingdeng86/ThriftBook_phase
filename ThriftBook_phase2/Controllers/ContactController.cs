using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;

namespace ThriftBook_phase2.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ApplicationDbContext _context;
        public ContactController(ILogger<ContactController> logger,
                              ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult ViewContact()
        {
            var contact = _context.Store.First();
            return View(contact);
        }
    }
}
