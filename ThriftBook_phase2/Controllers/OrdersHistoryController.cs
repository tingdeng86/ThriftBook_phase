using Microsoft.AspNetCore.Mvc;
using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Repositories;

namespace ThriftBook_phase2.Controllers
{
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
            OrderDetailRepo coRepo = new OrderDetailRepo(_context);
            var query = coRepo.GetOrder(paymentID);
            return View(query);
        }
    }
}
