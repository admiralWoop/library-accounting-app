using LibraryAccountingApp.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccountingApp.PL.WebApp.Models
{
    public class GenreViewModel
    {
        [HiddenInput]
        public long Id { get; set; }

        public string Name { get; set; }

        public Genre Parent { get; set; }

        public IEnumerable<Genre> Subgenres { get; set; }
    }
}
