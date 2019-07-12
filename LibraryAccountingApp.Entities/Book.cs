using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccountingApp.Entities
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
