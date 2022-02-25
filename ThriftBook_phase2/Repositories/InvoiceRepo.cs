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
            var query = from bi in db.BookInvoice
                        from i in db.Invoice
                        where i.TransactionId == bi.TransactionId
                        from pr in db.Profile
                        where i.BuyerId == pr.BuyerId
                        select new InvoiceVM()
                        {                                                        
                            TransactionId = i.TransactionId,
                            BuyerId = i.BuyerId,
                            TotalPrice = i.TotalPrice,
                            DateOfTransaction = i.DateOfTransaction,
                            BookId = bi.BookId,
                            FirstName = pr.FirstName,
                            LastName = pr.LastName,
                            Email = pr.Email,
                            PhoneNumber = pr.PhoneNumber
                        };
            return query;
        }

        public InvoiceVM Get(int transactionID)
        {
            var query = GetAll()
                .Where(i => i.TransactionId == transactionID)
                .FirstOrDefault();
            return query;
        }


    }
}
