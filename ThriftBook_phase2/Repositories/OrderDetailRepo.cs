using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Repositories
{
    public class OrderDetailRepo
    {
        ApplicationDbContext _context;

        public OrderDetailRepo(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IQueryable<OrderDetailVM> GetOrder(string paymentId)
        {
            var orderDetails = from i in _context.Invoice
                               where i.PaymentId == paymentId
                               from bi in _context.BookInvoice
                               where i.PaymentId == bi.PaymentId
                               from b in _context.Book
                               where b.BookId == bi.BookId
                               select new OrderDetailVM()
                               {
                                   PaymentId = paymentId,
                                   BuyerId = i.BuyerId,
                                   TotalPrice = i.TotalPrice,
                                   DateOfTransaction = i.DateOfTransaction,
                                   BookId = bi.BookId,
                                   Quantity = bi.Quantity,
                                   BookTitle = b.Title
                               };
            return orderDetails;
        }
    }
}
