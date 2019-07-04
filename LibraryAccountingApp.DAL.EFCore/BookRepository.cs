using LibraryAccountingApp.DAL.Contracts;
using LibraryAccountingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryAccountingApp.DAL.EFCore
{
    public class BookRepository : IRepository<Book>
    {
        private LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            this._context = context;
        }

        public void Create(Book item)
        {
            _context.Books.Add(item);
        }

        public void Delete(long id)
        {
            Book book = _context.Books.Find(id);
            if (book != null)
                _context.Books.Remove(book);
        }

        public IEnumerable<Book> Find(Func<Book, bool> predicate)
        {
            return _context.Books.Where(predicate).ToList();
        }

        public Book Get(long id)
        {
            return _context.Books.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books;
        }

        public void Update(Book item)
        {
            _context.Books.Update(item);
        }
    }
}
