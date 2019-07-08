using LibraryAccountingApp.DAL.Contracts;
using LibraryAccountingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccountingApp.BLL.Core
{
    public class BookService : IDisposable
    {
        IUnitOfWork _uof;

        public BookService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public Book GetById(long id) => _uof.Books.Get(id);

        public List<Book> GetAll() => _uof.Books.GetAll().ToList();

        public List<Book> GetByGenre(Genre genre)
            => _uof.Books.GetAll().Where(book => book.Genre == genre).ToList();

        public void AddBook(Book book)
        {
            if(_uof.Books.Any(_ => _.Id == book.Id)) //в БД есть книга с таким Id
            {
                throw new ArgumentException($"Book with Id={book.Id} already exists");
            }
            else
            {
                _uof.Books.Create(book);
                _uof.Save();
            }
        }

        public void UpdateBook(Book newBook)
        {
            if (_uof.Books.Any(_ => _.Id == newBook.Id)) //в БД есть книга с таким Id
            {
                _uof.Books.Update(newBook);
                _uof.Save();
            }
            else
            {
                throw new ArgumentException($"Book with Id={newBook.Id} couldn't be found");
            }
        }

        public void DeleteBook(long id)
        {
            if (_uof.Books.Any(_ => _.Id == id)) //в БД есть книга с таким Id
            {
                _uof.Books.Delete(id);
                _uof.Save();
            }
            else
            {
                throw new ArgumentException($"Book with Id={id} couldn't be found");
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _uof.Dispose();
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
