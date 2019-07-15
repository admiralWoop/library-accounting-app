using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccountingApp.BLL.Core;
using LibraryAccountingApp.Entities;
using LibraryAccountingApp.PL.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAccountingApp.PL.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private BookService  _bookService;
        private GenreService _genreService;
        private SignInManager<User> _singInManager;

        public HomeController(
            BookService bookService,
            GenreService genreService,
            SignInManager<User> singInManager)
        {
            _bookService = bookService;
            _genreService = genreService;
            _singInManager = singInManager;
        }

        public IActionResult Index(int? pageNumber)
        {
            int page;
            if (pageNumber.HasValue)
                page = pageNumber.Value;
            else
                page = 1;
            var books = _bookService.GetAll();
            var booksVM = books
                .Skip((page - 1) * 5)
                .Take(5)
                .Select(_ =>
                new BookViewModel()
                {
                    Id = _.Id,
                    Title = _.Title,
                    AuthorName = _.AuthorName,
                    Description = _.Description,
                    Genre = new GenreViewModel()
                    {
                        Id = _.Genre.Id,
                        Name = _.Genre.Name,
                        Subgenres = _.Genre.Subgenres
                    }
                });
            var genres = _genreService.GetAll();
            var genresVM = genres
                .Where(g => !genres.Any(c => c.Subgenres.Contains(g)))
                .Select(g =>
                new GenreViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    Subgenres = g.Subgenres
                });

            ViewBag.Books = booksVM;
            ViewBag.Genres = genresVM;
            ViewBag.Page = page;
            ViewBag.PagesCount = Math.Ceiling((double)books.Count / 5);

            return View("Index", booksVM);
        }

        #region Books

        [HttpGet]
        public IActionResult AddBook()
        {
            ViewBag.Genres = _genreService.GetAll().Select(g => g.Name);
            return View("AddBook");
        }

        [HttpPost]
        public IActionResult AddBook(BookViewModel model)
        {
            var book = new Book()
            {
                Title = model.Title,
                AuthorName = model.AuthorName,
                Description = model.Description,
                Genre = _genreService.GetByName(model.Genre.Name)
            };
            if (ModelState.IsValid)
            {
                _bookService.AddBook(book);
                return RedirectToAction("Index");
            }

            ViewBag.Genres = _genreService.GetAll().Select(g => g.Name);
            return View("AddBook", model);
        }

        [HttpGet]
        public IActionResult EditBook(long? id)
        {
            if (!id.HasValue) return Error();
            var book = _bookService.GetById(id.Value);
            if (book == null) return Error();

            var bookVM = new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.AuthorName,
                Description = book.Description,
                Genre = new GenreViewModel()
                {
                    Id = book.Genre.Id,
                    Name = book.Genre.Name,
                    Subgenres = book.Genre.Subgenres
                }
            };

            ViewBag.Genres = _genreService.GetAll().Select(g => g.Name);

            return View("EditBook", bookVM);
        }

        [HttpPost]
        public IActionResult EditBook(BookViewModel model)
        {
            var book = new Book()
            {
                Id = model.Id,
                Title = model.Title,
                AuthorName = model.AuthorName,
                Description = model.Description,
                Genre = _genreService.GetByName(model.Genre.Name)
            };
            if (ModelState.IsValid)
            {
                _bookService.UpdateBook(book);
                return RedirectToAction("Index");
            }

            ViewBag.Genres = _genreService.GetAll().Select(g => g.Name);
            return View("EditBook", model);
        }

        public IActionResult DeleteBook(long? id)
        {
            if (!id.HasValue) return Error();

            try
            {
                _bookService.DeleteBook(id.Value);
            }
            catch
            {
                return Error();
            }

            return RedirectToAction("Index");
        }

        public IActionResult OpenBook(long? id)
        {

            if (!id.HasValue) return Error();
            var book = _bookService.GetById(id.Value);
            if (book == null) return Error();

            var bookVM = new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.AuthorName,
                Description = book.Description,
                Genre = new GenreViewModel()
                {
                    Id = book.Genre.Id,
                    Name = book.Genre.Name,
                    Subgenres = book.Genre.Subgenres
                }
            };

            return View("Book", bookVM);
        }

        #endregion

        public IActionResult OpenGenre(long? id)
        {
            if (!id.HasValue) return Error();
            var genre = _genreService.GetById(id.Value);
            if (genre == null) return Error();

            var subgenres = genre.Subgenres
                    .Where(_ => !genre.Subgenres.Any(c => c.Subgenres.Contains(_)))
                    .Select(_ => _).ToList();

            var genreVM = new GenreViewModel()
            {
                Id = genre.Id,
                Name = genre.Name,
                Subgenres = genre.Subgenres
            };

            return View("Genre", genreVM);
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        //Error View
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}