using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;

namespace ThriftBook_phase2.Repositories
{
    public class OrdersHistoryRepo
    {
        private readonly ApplicationDbContext _context;
        public OrdersHistoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Invoice> GetOrdersLists(int buyerId)
        {
            var query = from i in _context.Invoice
                        where i.BuyerId == buyerId
                        select i;
            return query;
        }
    }
}
