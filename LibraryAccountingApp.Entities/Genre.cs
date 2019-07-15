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
        public virtual ICollection<Genre> Subgenres { get; set; }
        public virtual Genre Parent { get; set; }

        public Genre()
        {
            Subgenres = new List<Genre>();
        }
    }
}
