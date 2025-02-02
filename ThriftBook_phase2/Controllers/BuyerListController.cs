﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Repositories;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class BuyerListController : Controller
    {
        private readonly ILogger<BuyerListController> _logger;
        private readonly ApplicationDbContext _context;
        public BuyerListController(ILogger<BuyerListController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            InvoiceRepo iRepo = new InvoiceRepo(_context);
            IQueryable<InvoiceVM> iVM = iRepo.GetAll();

            ViewBag.TotalAmount = iVM.Sum(x => (double)x.TotalPrice);
            return View(iVM);
        }

        public ActionResult Details(string paymentId)
        {
            InvoiceRepo iRepo = new InvoiceRepo(_context);
            IQueryable<InvoiceDetailVM> iVM = iRepo.Get(paymentId);
            ViewBag.TotalAmount = iVM.Sum(x => (double)x.Price);
            return View(iVM);
        }

        public IActionResult ExportToCSV()
        {
            InvoiceRepo iRepo = new InvoiceRepo(_context);
            IQueryable<InvoiceVM> iVM = iRepo.GetAll();
            var builder = new StringBuilder();
            builder.AppendLine("Buyer ID,Total Price,Transaction Date,First Name,Last Name,Phone Number,Email,Postal Code");
            foreach (var item in iVM)
            {
                builder.AppendLine($"{item.BuyerId},{item.TotalPrice}, {item.DateOfTransaction}, {item.FirstName}, {item.LastName}, {item.PhoneNumber}, {item.Email}, {item.PostalCode}");

            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "invoice.csv");
        }

        public IActionResult ExportToCSVDetails(string paymentId)
        {
            InvoiceRepo iRepo = new InvoiceRepo(_context);
            IQueryable<InvoiceDetailVM> iVM = iRepo.Get(paymentId);
            var builder = new StringBuilder();
            builder.AppendLine("Buyer ID, Price,Transaction Date, Book Title, Book Genre, Book ID");
            foreach (var item in iVM)
            {
                builder.AppendLine($"{item.BuyerId},{item.Price}, {item.DateOfTransaction}, {item.Title}, {item.Genre}, {item.BookId}");

            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "invoice.csv");
        }
    }
}