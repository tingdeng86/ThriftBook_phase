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
        public DbSet<Profile> Profile { get; set; }
<<<<<<< HEAD
=======
        public DbSet<Buyer> Buyer { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Invoice> Invoice { get; set; }


>>>>>>> d1f6ad03a9580bda620661e95c2c4a661be1fbb5

    }
}
