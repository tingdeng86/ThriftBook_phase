using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using Microsoft.AspNetCore.Authorization;
namespace ThriftBook_phase2.Repositories
{
    public class ProfileRepo
    {
        ApplicationDbContext _context;

        public ProfileRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public Profile GetLoggedInUser(string userEmail)
        {
            var objects = _context.Profile;
            var registeredUser = _context.Profile.Where(pr => pr.Email == userEmail)
                                .FirstOrDefault();
            return registeredUser;
        }


        public void EditingUserInfo(Profile currentUserProfile, string userEmail)
        {
            var currentRegisteredUser = GetLoggedInUser(userEmail);
            try
            {
                currentRegisteredUser.FirstName = currentUserProfile.FirstName;
                currentRegisteredUser.LastName = currentUserProfile.LastName;
                currentRegisteredUser.Email = currentUserProfile.Email;
                currentRegisteredUser.City = currentUserProfile.City;
                currentRegisteredUser.Street = currentUserProfile.Street;
                currentRegisteredUser.PostalCode = currentUserProfile.PostalCode;
                currentRegisteredUser.PhoneNumber = currentUserProfile.PhoneNumber;
            }
            catch (Exception ex)
            {
                //Need to add exception hangling here. ViewBag, since the method returns nothing?
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //Need to add exception hangling here. ViewBag, since the method returns nothing?
            }
        }

        //Question for Pat: 
        //is there a neet for a separate method to save to db? there's only one line of text _context.SaveChanges() which is much shorter than creating a separate
        //method and passing the object there, finding the logged in user again. 
        //

    }
}
