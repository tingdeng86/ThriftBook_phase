using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Repositories;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Controllers
{
    [Authorize(Roles = "Administrator, Manager")]
    public class BuyerController : Controller
    {
        ApplicationDbContext _context;

        public BuyerController(ApplicationDbContext context)
        {
            _context = context;

        // GET: buyer
        }
        public ActionResult Index()
        {
            BuyerRepo buyerRepo = new BuyerRepo(_context);
            return View(buyerRepo.GetAllBuyers());
        }
        public ActionResult Details(int buyerId)
        {
            BuyerRepo bRepo = new BuyerRepo(_context);
            var query = bRepo.GetBuyer(buyerId);

            return View(query);

        }



    }
}
