using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccountingApp.Entities
{
    public class Genre
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Genre> Subgenres { get; set; }
    }
}
