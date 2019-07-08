using LibraryAccountingApp.DAL.Contracts;
using LibraryAccountingApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccountingApp.DAL.EFCore
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private LibraryContext _context;

        private BookRepository _bookRepository;
        private GenreRepository _genreRepository;

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
        }

        public IRepository<Book> Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_context);
                return _bookRepository;
            }
        }

        public IRepository<Genre> Genres
        {
            get
            {
                if (_genreRepository == null)
                    _genreRepository = new GenreRepository(_context);
                return _genreRepository;
            }
        }

        public void Save() => _context.SaveChanges();

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
