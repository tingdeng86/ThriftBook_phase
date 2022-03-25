using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.Controllers
{
    public class OrdersHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
