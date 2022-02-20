using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftBook_phase2.Data;
using ThriftBook_phase2.Models;
using ThriftBook_phase2.ViewModels;

namespace ThriftBook_phase2.Repositories
{
    public class BookDetailVMRepo
    {
        private readonly ApplicationDbContext db;

        public BookDetailVMRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public IQueryable<BookVM> GetAll()
        {
            var query = from b in db.Book

                        select new BookVM()
                        {
                            BookID = b.BookId,
                            Title = b.Title,
                            Author = b.Author,
                            Genre = b.Gennre,
                            BookQuality = b.BookQuality,
                            BookQuantity = (int)b.BookQuantity,
                            BookPhoto = b.BookPhoto,
                            Price = (decimal)b.Price,
                            StoreName = b.StoreName
                        };
            return query;
        }

        public BookVM Get(int bookID)
        {
            var query = GetAll()
                .Where(b => b.BookID == bookID)
                .FirstOrDefault();
            return query;
        }

        public bool Update(BookVM bVM)
        {
            Book book = db.Book
                .Where(b => b.BookId == bVM.BookID).FirstOrDefault();

            book.Title = bVM.Title;
            book.Author = bVM.Author;
            book.Gennre = bVM.Genre;
            book.BookQuality = bVM.BookQuality;
            book.BookQuantity = bVM.BookQuantity;
            book.BookPhoto = bVM.BookPhoto;
            book.Price = bVM.Price;
            book.StoreName = bVM.StoreName;
            db.SaveChanges();
            return true;
        }

       public Book Add(BookVM bVM)
        {
            BookDetailVMRepo bdRepo = new BookDetailVMRepo(db);
            Book book = new Book()
            {
                Title = bVM.Title,
                Author = bVM.Author,
                Gennre = bVM.Genre,
                BookQuality = bVM.BookQuality,
                BookQuantity = bVM.BookQuantity,
                BookPhoto = bVM.BookPhoto,
                Price = bVM.Price,
                StoreName = bVM.StoreName
            };
            db.Book.Add(book);
            db.SaveChanges();
            return book;
        }

        public bool Delete(int id)
        {
            Book book = db.Book
                       .Where(b => b.BookId == id).FirstOrDefault();
            db.Remove(book);
            db.SaveChanges();
            return true;
        }
    }
}
