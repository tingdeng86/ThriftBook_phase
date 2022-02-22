using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Repositories
{
    public class BuyerRepo
    {
        ApplicationDbContext _context;

        public BuyerRepo(ApplicationDbContext context)
        {
            this._context = context;
            //var buyersCreated = CreateInitialBuyers();
        }
        public IQueryable<Profile> GetAllBuyers()
        {
            var buyers = _context.Profile;
            //List<BuyersVM> buyerList = new List<BuyersVM>();

            //foreach (var item in buyers)
            //{
            //    buyerList.Add(new BuyersVM() 
            //    { 
            //        BuyerId = item.BuyerId, 
            //        FirstName = item.FirstName,
            //        LastName = item.LastName,
            //        Email = item.Email,
            //        City = item.City,
            //        Street = item.Street,
            //        PostalCode = item.PostalCode,
            //        PhoneNumber = item.PhoneNumber
            //    });
            //}
            return buyers;
        }

        //public BuyersVM GetBuyer(int buyerId)
        //{
        //    var item = _context.Profile.Where(p => p.BuyerId == buyerId).FirstOrDefault();
        //    if (item != null)
        //    {
        //        return new BuyersVM() 
        //        {
        //            BuyerId = item.BuyerId,
        //            FirstName = item.FirstName,
        //            LastName = item.LastName,
        //            Email = item.Email,
        //            City = item.City,
        //            Street = item.Street,
        //            PostalCode = item.PostalCode,
        //            PhoneNumber = item.PhoneNumber
        //        };
        //    }
        //    return null;
        //}

        public Profile GetBuyer(int buyerId)
        {
            var query = GetAllBuyers()
                .Where(b => b.BuyerId == buyerId)
                .FirstOrDefault();
            return query;
        }
    }
}
