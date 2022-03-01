using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Models;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Repositories
{

    public class PaymentRepo
    {
        private readonly ApplicationDbContext _context;

        public object ViewData { get; private set; }

        public PaymentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<CartVM> GetOrderData(string sessionId, decimal totalPrice, int buyerId)
        {
            CartRepo cartRepo = new CartRepo(_context);
            IQueryable<CartVM> BooksBought = cartRepo.GetLists(sessionId);

            return BooksBought;
        }

        public List<Cart> GetBooksBySession(string sessionId)
        {
            var query = from c in _context.Cart
                        where c.SessionId == sessionId
                        select new Cart
                        {
                            BookId = c.BookId,
                            Quantity = c.Quantity
                        };
            return (List<Cart>)query;
        }


        }
}
