using LibraryAccountingApp.DAL.Contracts;
using LibraryAccountingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryAccountingApp.DAL.EFCore
{
    public class GenreRepository : IRepository<Genre>
    {
        private LibraryContext _context;
        
        public GenreRepository(LibraryContext context)
        {
            this._context = context;
        }

        public void Create(Genre item)
        {   
            _context.Genres.Add(item);
        }

        public void Delete(long id)
        {
            var genre = _context.Genres.Find(id);
            if(genre != null)
            {
                _context.Genres.Remove(genre);
            }
        }

        public IEnumerable<Genre> Find(Func<Genre, bool> predicate)
        {
            return _context.Genres.Where(predicate).ToList();
        }

        public bool Any(Func<Genre, Boolean> predicate)
        {
            return _context.Genres.Any(predicate);
        }

        public Genre Get(long id)
        {
            return _context.Genres.Find(id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return _context.Genres;
        }

        public void Update(Genre item)
        {
            _context.Genres.Update(item);
        }
    }
}
