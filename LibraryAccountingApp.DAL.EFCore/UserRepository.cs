using LibraryAccountingApp.DAL.Contracts;
using LibraryAccountingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryAccountingApp.DAL.EFCore
{
    public class UserRepository : IRepository<User>
    {
        private LibraryContext _context;

        public UserRepository(LibraryContext context)
        {
            this._context = context;
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
        }

        public void Delete(long id)
        {
            User user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _context.Users.Where(predicate).ToList();
        }

        public User Get(long id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public void Update(User item)
        {
            _context.Users.Update(item);
        }
    }
}
