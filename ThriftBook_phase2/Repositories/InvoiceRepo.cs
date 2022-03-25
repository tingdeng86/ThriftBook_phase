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
                            PostalCode = b.PostalCode
                        };
            return query;
        }

        public InvoiceVM Get(string paymentId)
        {
            var query = GetAll()
                .Where(i => i.PaymentId == paymentId)
                .FirstOrDefault();
            return query;
        }


    }
}
