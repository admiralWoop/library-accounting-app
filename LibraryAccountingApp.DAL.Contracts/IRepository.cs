using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccountingApp.DAL.Contracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        bool Any(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(long id);
    }
}
