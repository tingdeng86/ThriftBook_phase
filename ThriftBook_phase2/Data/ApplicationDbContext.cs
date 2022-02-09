using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rolesDemoSSD.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ThriftBook_phase2.Models;

namespace ThriftBook_phase2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<BookRating> BookRating { get; set; }
        public DbSet<BookInvoice> BookInvoice { get; set; }
        public DbSet<Buyer> Buyer { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Invoice> Invoice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // When adding OnModelCreating() in .NET Core a reference t
            // to the base class is also needed at the start of the method.
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookInvoice>()
               .HasKey(bi => new { bi.BookId, bi.TransactionId });
            // Define foreign keys here. Do not use foreign key annotations.
            modelBuilder.Entity<BookInvoice>()
                .HasOne(c => c.Book)
                .WithMany(c => c.BookInvoices)
                .HasForeignKey(fk => new { fk.BookId })
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BookInvoice>()
                .HasOne(c => c.Invoice)
                .WithMany(c => c.BookInvoices)
                .HasForeignKey(fk => new { fk.TransactionId })
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookRating>()
              .HasKey(bi => new { bi.BookId, bi.BuyerId });
            modelBuilder.Entity<BookRating>()
                .HasOne(c => c.Book)
                .WithMany(c => c.BookRatings)
                .HasForeignKey(fk => new { fk.BookId })
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BookRating>()
                .HasOne(c => c.Buyer)
                .WithMany(c => c.BookRatings)
                .HasForeignKey(fk => new { fk.BuyerId })
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Your Next Five Moves",
                    Author = "Greg Dinkin",
                    Gennre = "Business & Investing",
                    BookQuality = "like new",
                    BookQuantity = 5,
                    BookPhoto = "https://images-na.ssl-images-amazon.com/images/I/41z2wSFrXbL._SX326_BO1,204,203,200_.jpg",
                    Price = 14,
                    StoreName = "ThriftBook"
                },
                new Book
                {
                    BookId = 2,
                    Title = "The Christmas Pig",
                    Author = "J.K. Rowling",
                    Gennre = "Children Books",
                    BookQuality = "good",
                    BookQuantity = 3,
                    BookPhoto = "https://images-na.ssl-images-amazon.com/images/I/51rg5EDPpDL._SX336_BO1,204,203,200_.jpg",
                    Price = 12,
                    StoreName = "ThriftBook"
                },
                new Book
                {
                    BookId = 3,
                    Title = "The Very Hungry Caterpillar",
                    Author = "Eric Carle",
                    Gennre = "Children Books",
                    BookQuality = "old",
                    BookQuantity = 2,
                    BookPhoto = "https://images-na.ssl-images-amazon.com/images/I/41tyokViuNL._SY355_BO1,204,203,200_.jpg",
                    Price = 6.25m,
                    StoreName = "ThriftBook"
                },
                new Book
                {
                    BookId = 4,
                    Title = "Will",
                    Author = "Will Smith",
                    Gennre = "Biographies & Memoirs",
                    BookQuality = "like new",
                    BookQuantity = 3,
                    BookPhoto = "https://images-na.ssl-images-amazon.com/images/I/51oDyfsqKwL._SX327_BO1,204,203,200_.jpg",
                    Price = 10,
                    StoreName = "ThriftBook"
                },
                new Book
                {
                    BookId = 5,
                    Title = "Cosmos",
                    Author = "Carl Sagan",
                    Gennre = "Science & Math",
                    BookQuality = "like new",
                    BookQuantity = 5,
                    BookPhoto = "https://images-na.ssl-images-amazon.com/images/I/51IcVjsJlDL._SX322_BO1,204,203,200_.jpg",
                    Price = 10,
                    StoreName = "ThriftBook"
                }
                );

            modelBuilder.Entity<Buyer>().HasData(
                new Buyer
                {
                    BuyerId = 1,
                    FirstName = "Keanu",
                    LastName = "Reeves",
                    Email = "keanureeves@gmail.com",
                    City = "Los Angeles",
                    Street = "Coldwater Canyon",
                    PostalCode = "90210",
                    PhoneNumber = "123-456-7890"
                },
                new Buyer
                {
                    BuyerId = 2,
                    FirstName = "Tiger",
                    LastName = "King",
                    Email = "tigerking@gmail.com",
                    City = "Miami",
                    Street = "Sunset Blvd.",
                    PostalCode = "10101",
                    PhoneNumber = "210-654-3218"
                },
                new Buyer
                {
                    BuyerId = 3,
                    FirstName = "Homer",
                    LastName = "Simpson",
                    Email = "homer.j.simpson@gmail.com",
                    City = "Springfield",
                    Street = "Evergreen Terrace",
                    PostalCode = "12121",
                    PhoneNumber = "123-321-3165"
                },
                new Buyer
                {
                    BuyerId = 4,
                    FirstName = "Daenerys",
                    LastName = "Targaryen",
                    Email = "emailia.clarke@gmail.com",
                    City = "Dragonstone",
                    Street = "Free Cities St.",
                    PostalCode = "13337",
                    PhoneNumber = "654-321-6458"
                },
                new Buyer
                {
                    BuyerId = 5,
                    FirstName = "Ting",
                    LastName = "Deng",
                    Email = "ting.the.ceo@gmail.com",
                    City = "Shanghai",
                    Street = "Movecanada",
                    PostalCode = "13ceo4",
                    PhoneNumber = "765-432-2500"
                }
            );

            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    StoreName = "ThriftBook",
                    Email = "thriftbook@thriftbook.com",
                    City = "Vancouver",
                    Street = "Pacific Boulevard",
                    PostalCode = "V2W1B5",
                    PhoneNumber = "778-689-1000"
                }
            );
        }
    }
}
