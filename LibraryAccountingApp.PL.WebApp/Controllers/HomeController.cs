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

        public IActionResult Index()
        {
            if(true || _singInManager.IsSignedIn(User))
            {
                var books = _bookService.GetAll();
                var booksVM = books.Select(_ =>
                    new BookViewModel()
                    {
                        Id = _.Id,
                        Title = _.Title,
                        AuthorName = _.AuthorName,
                        Description = _.Description,
                        GenreName = _.Genre.Name
                    });
                var genres = _genreService.GetAll();
                var genresVM = genres
                    .Where(_ => !genres.Any(c => c.Subgenres.Contains(_)))
                    .Select(_ =>
                    new GenreViewModel()
                    {
                        Name = _.Name,
                        Subgenres = _.Subgenres
                    });

                ViewBag.Books = booksVM;
                ViewBag.Genres = genresVM;
                return View("Index", booksVM);
            }
            else
            {
                return RedirectToPage("/Account/Login",new { area = "Identity"});
            }
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
                GenreName = book.Genre?.Name
            };

            return View("Book", bookVM);
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