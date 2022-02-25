using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Models;

namespace ThriftBook_phase2.Repositories
{

    public class PaymentRepo
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IPN GetOrderData(string sessionId, decimal totalPrice, int buyerId)
        {
            //ProfileRepo prRepo = new ProfileRepo(_context);
            //int currentUserId = prRepo.GetLoggedInUser(userEmail).BuyerId;

            List<Cart> BooksBought = GetBooksBySession(sessionId);

            foreach (var eachBookBought in BooksBought)
            {
                var variable = eachBookBought.BookId;
                var lll = eachBookBought.Quantity;
            }

            IPN currentCheckout = new IPN()
            {
                BuyerId = buyerId,
                TotalPrice = totalPrice,
                DateOfTransaction = DateTime.Now
            };
            return currentCheckout;
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
