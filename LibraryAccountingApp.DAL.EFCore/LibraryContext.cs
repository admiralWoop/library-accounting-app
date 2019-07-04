using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using LibraryAccountingApp.Entities;

namespace LibraryAccountingApp.DAL.EFCore
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base (options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}