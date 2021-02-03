using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.IServices;
using Trustme.Data;

namespace Trustme.Service
{
    public class UserRepository : IUserRepository
    {
        private UserContext _context;
        public void add(User _User)
        {
            _context.User.Add(_User);
            _context.SaveChanges();
        }

        public void delete(User _User)
        {
            _context.User.Remove(_User);
            _context.SaveChanges();
        }

        public IEnumerable<User> listAllUsers()
        {
            return _context.User.ToList();
        }

        public void update(User _User)
        {
            _context.User.Update(_User);
            _context.SaveChanges();
        }
    }
}
