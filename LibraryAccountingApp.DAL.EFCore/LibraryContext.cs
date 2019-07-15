using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using LibraryAccountingApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibraryAccountingApp.DAL.EFCore
{
    public class LibraryContext : IdentityDbContext<User>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base (options)
        {
            //Database.EnsureCreated();
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");

            modelBuilder.Entity<Genre>().ToTable("Genre");
            //modelBuilder.Entity<Genre>(entity =>
            //{
            //    entity.HasMany(genre => genre.Subgenres)
            //   .WithOne(genre => genre.Parent);
            //});

            base.OnModelCreating(modelBuilder);
        }
    }
}