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
            var query = from v in db.Invoice                       

                        select new InvoiceVM()
                        {                                                        
                            TransactionId = v.TransactionId,
                            BuyerId = v.BuyerId,
                            TotalPrice = v.TotalPrice,
                            DateOfTransaction = v.DateOfTransaction                          
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
