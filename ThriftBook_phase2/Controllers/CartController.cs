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
            HttpContext.Session.SetString("DUMB", "DUMB");
            string sessionId = HttpContext.Session.Id;
            CartRepo cartRepo = new CartRepo(_context);
            var query = cartRepo.GetLists(sessionId);
            var totalItems = cartRepo.GetTotalItems(sessionId);
            var subTotals = cartRepo.GetSubTotal(sessionId);
            ViewData["TotalItems"] = totalItems;
            ViewData["SubTotal"] = subTotals;
            return View(query);
        }
        public ActionResult Details(int id)
        {
            CartRepo cartRepo = new CartRepo(_context);
            var cartVM = cartRepo.GetDetail(id);           
            return View(cartVM);
        }
        public ActionResult AddToCart(int id)
        {
            string sessionId = HttpContext.Session.Id;
            CartRepo cartRepo = new CartRepo(_context);
            var cartItem = cartRepo.Find(id, sessionId);
            if(cartItem==null)
            {
                int cartItemId=cartRepo.Add(id, sessionId);
                return RedirectToAction("Details", "Cart", new { id = cartItemId });
            }else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.
                cartRepo.increaseQuantity(cartItem.CartItemId);
                return RedirectToAction("Details", "Cart", new { id=cartItem.CartItemId });
            }           
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
    }
}
