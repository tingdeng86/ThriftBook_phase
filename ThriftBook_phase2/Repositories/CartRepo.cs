using Microsoft.AspNetCore.Http;
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
    public class CartRepo
    {
        private readonly ApplicationDbContext _context;
        public CartRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Add(int id,string sessionId)
        {
            Cart cart = new Cart()
            {
                BookId = id,
                SessionId = sessionId,
                Quantity = 1,
            };
            _context.Cart.Add(cart);
            _context.SaveChanges();
            return cart.CartItemId;
        }

       

        public Cart Find(int id, string sessionId)
        {
            var cartItem = _context.Cart.Where(
               c => c.SessionId == sessionId
               && c.BookId == id).FirstOrDefault();
            return cartItem;
        }
        
        public CartVM GetDetail(int id)
        {
            var cartItem = _context.Cart.Where(c => c.CartItemId == id).FirstOrDefault();
            var book = _context.Book.Where(b => b.BookId == cartItem.BookId).FirstOrDefault();
            CartVM cartVM = new CartVM()
            {
                CartItemId = cartItem.CartItemId,
                SessionId = cartItem.SessionId,
                BookId = cartItem.BookId,
                Title = book.Title,
                BookPhoto = book.BookPhoto,
                Price = book.Price,
                Quantity = cartItem.Quantity,
                TotalQuantity = book.BookQuantity,
            };
            return cartVM;
        }
        public IQueryable<CartVM> GetLists(string sessionId)
        {
            var query = from c in _context.Cart
                        from b in _context.Book
                        where c.SessionId == sessionId
                        where b.BookId == c.BookId
                        select new CartVM()
                        {
                            CartItemId = c.CartItemId,
                            SessionId = c.SessionId,
                            BookId = c.BookId,
                            Title = b.Title,
                            BookPhoto = b.BookPhoto,
                            Price = b.Price,
                            TotalQuantity = b.BookQuantity,
                            Quantity = c.Quantity,
                        };
            return query;
        }
        public int GetTotalItems(string sessionId)
        {
            var query = GetLists(sessionId);
            int totalItems = 0;
            foreach (var item in query)
            {
                totalItems += item.Quantity;
            }
            return totalItems;
        }
        public decimal GetSubTotal(string sessionId)
        {
            var query = GetLists(sessionId);
            decimal subTotal = 0;
            foreach (var item in query)
            {
                subTotal += item.Quantity*item.Price;
            }
            return subTotal;
        }
        public Cart GetCartItem(int id)
        {
            Cart cart = _context.Cart
                       .Where(e => e.CartItemId == id).FirstOrDefault();
            return cart;
        }
        public bool Delete(int id)
        {
            var cart = GetCartItem(id);
            if(cart != null)
            {
                _context.Remove(cart);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void EmptyCarts(string sessionId)
        {
            var query = from c in _context.Cart
                        where c.SessionId == sessionId
                        select c;
            if (query != null)
            {
                foreach (var item in query)
                {
                    _context.Cart.Remove(item);
                }
                _context.SaveChanges();
            }


        }
        public Cart increaseQuantity(int id)
        {
            var cartItem = GetCartItem(id);
            var cart = GetDetail(id);
            if(cartItem.Quantity < cart.TotalQuantity)
            {
                cartItem.Quantity += 1;
                _context.SaveChanges();
            }
            return cartItem;
        }
        public Cart decreaseQuantity(int id)
        {
            var cartItem = GetCartItem(id);
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
                _context.SaveChanges();
            }
            return cartItem;
        }

        public IQueryable<Book> UpdateBooks(string paymentId)
        {
            var bookInvoices = from b in _context.BookInvoice
                               where b.PaymentId == paymentId
                               select b;
            foreach (var item in bookInvoices)
            {
                Book book = (from b in _context.Book
                             where b.BookId == item.BookId
                             select b).FirstOrDefault();
                book.BookQuantity = book.BookQuantity - item.Quantity;
                
            }
            _context.SaveChanges();
            var books = from b in _context.Book
                        from q in bookInvoices
                        where b.BookId == q.BookId
                        select b;
            return books;
        }

        public Book GetBook(int id)
        {
            Book book = (from b in _context.Book
                         where b.BookId == id
                         select b).FirstOrDefault();
            return book;
        }

        public void MigrateCart(string sessionId, string userName)
        {
             var shoppingCart = _context.Cart.Where(c => c.SessionId == sessionId);
            foreach (var item in shoppingCart)
            {
                //solve the problem:If the item does exist in the cart of user's cart before, new cart has the same book, it should merge this book and make sure the quantity is not greater than the book's total quantity.
                var bookItem = Find(item.BookId, userName);
                if (bookItem != null)
                {
                    var qty = GetBook(item.BookId).BookQuantity;
                    if ((bookItem.Quantity + item.Quantity) >= qty)
                    {
                        bookItem.Quantity = qty;
                    }
                    else
                    {
                        bookItem.Quantity = bookItem.Quantity + item.Quantity;
                    }
                    _context.Cart.Remove(item);
                }
                else
                {
                    item.SessionId = userName;
                }
            }
                _context.SaveChanges();
        }
        public string CreateTransaction(decimal totalPrice, int buyerId, DateTime date, string paymentId)
        {
            Invoice invoice = new Invoice()
            {
                BuyerId = buyerId,
                TotalPrice = totalPrice,
                DateOfTransaction = date,
                PaymentId = paymentId
            };
            _context.Invoice.Add(invoice);
            _context.SaveChanges();
            //return invoice.PaymentId;
            return invoice.PaymentId;
        }

        public IQueryable<BookInvoice> CreateBookInvoice(string sessionId, string paymentId)
        {
            var query = GetLists(sessionId);
            foreach (var item in query)
            {
                BookInvoice bookInvoice = new BookInvoice
                {
                    PaymentId = paymentId,
                    BookId = item.BookId,
                    Quantity = item.Quantity
                };
                _context.BookInvoice.Add(bookInvoice);
            }
            _context.SaveChanges();
            var bookInvoices = from b in _context.BookInvoice
                               where b.PaymentId == paymentId
                               select b;
            return bookInvoices;
        }
    }

}
