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
    public class BookRatingRepo
    {
        ApplicationDbContext _context;

        public BookRatingRepo(ApplicationDbContext context)
        {
            _context = context;
        }


        public IQueryable<BookRating> BookRatingsByLoggedInUser(Profile currentRegisteredUser)
        {
            //Getting ALL BookRatings made by the current user
            var allBookRatings = from br in _context.BookRating
                        where br.BuyerId == currentRegisteredUser.BuyerId
                        select new BookRating()
                        {
                            Rating = br.Rating,
                            Comments = br.Comments
                        };
            return allBookRatings;
        }


        public IQueryable<BookRating> SingleBookRating(Profile currentRegisteredUser, int bookId)
        {
            //Getting SINGLE BookRating made by the current user for a SPECIFIC book
            var allBookRatingsByUser = BookRatingsByLoggedInUser(currentRegisteredUser);
            var singleBookRating = allBookRatingsByUser.Where(book => book.BookId == bookId);
            return singleBookRating;
        }

        public IQueryable<BookRatingVM> AllSingleBookRatings(int bookId)
        {
            //Getting ALL BookRatings for a SPECIFIC book
            var allBookRatings = from br in _context.BookRating
                                 from b in _context.Profile
                                 where br.BookId == bookId
                                 where br.BuyerId == b.BuyerId
                                 select new BookRatingVM {
                                     BookId = bookId,
                                     BuyerId = b.BuyerId,
                                     Rating = br.Rating,
                                     Comments = br.Comments,
                                     FirstName = b.FirstName,                                     
                                 };
            return allBookRatings;
        }

        public decimal getBookRating(int bookId)
        {
            var allBookRatings = AllSingleBookRatings(bookId).ToList();
            decimal result = allBookRatings.Select(x => x.Rating).Average();
            return result;
        }

        public void CreateReview(BookRating newBookRating, string userEmail)
        {
            _context.BookRating.Add(newBookRating);
            _context.SaveChanges();
        }


        public void EditReview(BookRating editedBookReview, string userEmail)
        {

            ProfileRepo prRepo = new ProfileRepo(_context);
            //Obtaining the BuyerID from the object of the registered in user and insert into object
            // of newly created book rating.
            BookRating editedReview = new BookRating
            {
                BuyerId = prRepo.GetLoggedInUser(userEmail).BuyerId,
                Rating = editedBookReview.Rating,
                Comments = editedBookReview.Comments
            };

        }


        public bool FindRating(string userEmail, int bookId)
        {
            ProfileRepo prRepo = new ProfileRepo(_context);
            int buyerId = prRepo.GetLoggedInUser(userEmail).BuyerId;
            var rating = from b in _context.BookRating
                         where b.BookId == bookId && b.BuyerId == buyerId
                         select b;
            if (rating != null)
            {
                return true;
            }else
            {
                return false;
            }
        }

       }
}
