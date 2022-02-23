using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Repositories;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Controllers
{
    public class BuyerListController : Controller
    {
        private readonly ILogger<BuyerListController> _logger;
        private readonly ApplicationDbContext _context;
        public BuyerListController(ILogger<BuyerListController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(decimal SalesTotal)
        {
            var buyerList = from bl in _context.Invoice
                        select bl;

            ViewBag.TotalAmount = buyerList.Sum(x => (double)x.TotalPrice);
          
            return View(buyerList);
        }

        public ActionResult Details(int transactionID)
        {
            InvoiceRepo iRepo = new InvoiceRepo(_context);
            InvoiceVM bVM = iRepo.Get(transactionID);

            return View(bVM);

        }
    }
}
