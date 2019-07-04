using LibraryAccountingApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccountingApp.DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Book> Books { get; }
        IRepository<Genre> Genres { get; }
        void Save();
    }
}
