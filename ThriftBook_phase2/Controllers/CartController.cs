using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using Microsoft.AspNetCore.Http;
using ThriftBook_phase2.Models;
using ThriftBook_phase2.ViewModels;
using ThriftBook_phase2.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace ThriftBook_phase2.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApplicationDbContext _context;      
        public CartController(ILogger<CartController> logger,
                              ApplicationDbContext context )
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            //sessionId changes all the time, after setting, it would not change
            
            string sessionId = HttpContext.Session.Id;
            CartRepo cartRepo = new CartRepo(_context);
            var query = cartRepo.GetLists(sessionId);
            var totalItems = cartRepo.GetTotalItems(sessionId);
            var subTotals = cartRepo.GetSubTotal(sessionId);
            ViewData["TotalItems"] = totalItems;
            ViewData["SubTotal"] = subTotals;
            ViewData["Tax"] = subTotals*0.12m;
            ViewData["Total"] = subTotals*1.12m;
            return View(query);
        }



        public ActionResult Home()
        {
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Details(int id)
        {
            CartRepo cartRepo = new CartRepo(_context);
            var cartVM = cartRepo.GetDetail(id);           
            return View(cartVM);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                CartRepo cartRepo = new CartRepo(_context);
                cartRepo.Delete(id);
            }
            catch (Exception e)
            {
                ViewData["Message"] = e.Message;
            }
            return RedirectToAction("Index", "Cart", new { message = ViewData["Message"] });
        }
        public ActionResult IncreaseOne(int id)
        {
            CartRepo cartRepo = new CartRepo(_context);
            cartRepo.increaseQuantity(id);
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult DecreaseOne(int id)
        {
            CartRepo cartRepo = new CartRepo(_context);
            cartRepo.decreaseQuantity(id);
            return RedirectToAction("Index", "Cart");
        }

        [Authorize]
        public ActionResult Checkout()
        {
            return RedirectToAction("Index", "Cart");
        }
    }
}
