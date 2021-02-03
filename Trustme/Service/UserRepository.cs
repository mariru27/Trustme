using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustme.Models;
using Trustme.IServices;
using Trustme.Data;
using Trustme.ViewModels;

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

        public IEnumerable<User> listAllUsers(Role _Role)
        {

            List<User> UsersList = (List<User>)_context.Role.Join(_context.User,
                role => role.IdRole,
                user => user.RoleId,
                (role, user) => new User()) ; 
            return UsersList;
        }

        public void update(User _User)
        {
            _context.User.Update(_User);
            _context.SaveChanges();
        }
    }
}
