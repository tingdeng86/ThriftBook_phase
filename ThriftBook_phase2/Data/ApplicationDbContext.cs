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



    }
}
