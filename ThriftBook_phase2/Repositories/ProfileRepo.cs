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

            //var currentUser = _context.Profile.Where(ru => ru.Email == userName)
            //        .FirstOrDefault();
            //ProfileRepo prRepo = new ProfileRepo(_context);
            var currentRegisteredUser = GetLoggedInUser(userEmail);

            currentRegisteredUser.FirstName = currentUserProfile.FirstName;
            currentRegisteredUser.LastName = currentUserProfile.LastName;
            currentRegisteredUser.Email = currentUserProfile.Email;
            currentRegisteredUser.City = currentUserProfile.City;
            currentRegisteredUser.Street = currentUserProfile.Street;
            currentRegisteredUser.PostalCode = currentUserProfile.PostalCode;
            currentRegisteredUser.PhoneNumber = currentUserProfile.PhoneNumber;
            _context.SaveChanges();

        }
    }
}
