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
        private GenreService _genreservice;
        private SignInManager<User> _singInManager;

        public HomeController(
            BookService bookService, 
            GenreService genreService,
            SignInManager<User> singInManager)
        {
            _bookService = bookService;
            _genreservice = genreService;
            _singInManager = singInManager;
        }

        public IActionResult Index()
        {
            if(_singInManager.IsSignedIn(User))
            {
                var books = _bookService.GetAll();
                var booksVM = books.Select(_ =>
                    new BookViewModel()
                    {
                        Title = _.Title,
                        AuthorName = _.AuthorName,
                        Description = _.Description,
                        GenreName = _.Genre.Name
                    });
                return View("Index", booksVM);
            }
            else
            {
                return RedirectToPage("/Account/Login",new { area = "Identity"});
            }
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