using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Models;

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

        public IQueryable<BookRating> AllSingleBookRatings(int bookId)
        {
            //Getting ALL BookRatings for a SPECIFIC book
            var allBookRatings = from br in _context.BookRating
                                 where br.BookId == bookId
                                 select new BookRating()
                                 {
                                     Rating = br.Rating,
                                     Comments = br.Comments
                                 };
            return allBookRatings;
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


        public void UpdateDb()
        {

        }

        }
}
