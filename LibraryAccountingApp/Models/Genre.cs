using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccountingApp.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Genre> Subgenres { get; set; }
    }
}
