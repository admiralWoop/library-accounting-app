using LibraryAccountingApp.DAL.Contracts;
using LibraryAccountingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryAccountingApp.BLL.Core
{
    public class GenreService : IDisposable
    {
        IUnitOfWork _uof;

        public GenreService(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public Genre GetById(long id) => _uof.Genres.Get(id);

        public Genre GetByName(string name) =>
            _uof.Genres.Find(_ => _.Name == name).FirstOrDefault();

        public List<Genre> GetAll() => _uof.Genres.GetAll().ToList();

        public void AddGenre(Genre genre)
        {
            if (_uof.Genres.Any(_ => _.Id == genre.Id)) //в БД есть жанр с таким Id
            {
                throw new ArgumentException($"Genre with Id={genre.Id} already exists");
            }
            else
            {
                _uof.Genres.Create(genre);
                _uof.Save();
            }
        }

        public void UpdateGenre(Genre newGenre)
        {
            if (_uof.Genres.Any(_ => _.Id == newGenre.Id)) //в БД есть жанр с таким Id
            {
                _uof.Genres.Update(newGenre);
                _uof.Save();
            }
            else
            {
                throw new ArgumentException($"Book with Id={newGenre.Id} couldn't be found");
            }
        }

        public void DeleteGenre(long id)
        {
            if (_uof.Genres.Any(_ => _.Id == id)) //в БД есть жанр с таким Id
            {
                if(_uof.Books.Find(b => b.Genre?.Id == id).Count() > 0)
                {
                    throw new Exception($"Genre cannot be deleted if there are books related to it");
                }

                foreach (var genre in _uof.Genres.Find(g => g.Parent?.Id == id))
                    genre.Parent = null;

                _uof.Genres.Delete(id);
                _uof.Save();
            }
            else
            {
                throw new ArgumentException($"Genre with Id={id} couldn't be found");
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
