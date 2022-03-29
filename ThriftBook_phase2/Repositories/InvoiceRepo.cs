using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Repositories
{
    public class InvoiceRepo
    {

        private readonly ApplicationDbContext db;

        public InvoiceRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public IQueryable<InvoiceVM> GetAll()
        {
            var query = from i in db.Invoice
                        from b in db.Profile
                        where i.BuyerId == b.BuyerId
                        select new InvoiceVM
                        {
                            PaymentId = i.PaymentId,
                            BuyerId = i.BuyerId,
                            TotalPrice = i.TotalPrice,
                            DateOfTransaction = i.DateOfTransaction,
                            FirstName = b.FirstName,
                            LastName = b.LastName,
                            PhoneNumber = b.PhoneNumber,
                            PostalCode = b.PostalCode,
                            Email = b.Email
                        };
            return query;
        }



        public IQueryable<InvoiceDetailVM> GetMore()
        {
            var query = from bi in db.BookInvoice
                        from b in db.Book
                        from i in db.Invoice
                        where bi.PaymentId == i.PaymentId && bi.BookId == b.BookId
                        select new InvoiceDetailVM
                        {
                            PaymentId = i.PaymentId,
                            BuyerId = i.BuyerId,
                            Price = b.Price,
                            DateOfTransaction = i.DateOfTransaction,
                            BookId = b.BookId,
                            Title = b.Title,
                            Genre = b.Gennre
                        };
            return query;
        }

        public IQueryable<InvoiceDetailVM> Get(int paymentID)
        {
            var query = GetMore();
            var lists = from q in query
                        where q.PaymentId == paymentID
                        select q;

            return lists;
        }

        public IQueryable<InvoiceDetailVM> GetWithBuyerID(int buyerID)
        {
            var query = GetMore();
            var lists = from q in query
                        where q.BuyerId == buyerID
                        select q;

            return lists;
        }
    }
}
