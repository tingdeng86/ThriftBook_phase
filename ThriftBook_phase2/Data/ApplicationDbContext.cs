using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<Book> BookDetail { get; set; }
        public DbSet<BookRating> BookRating { get; set; }
        public DbSet<Profile> Profile { get; set; }
    }
}
