using System.Collections.Generic;
using System.Linq;
using Trustme.Models;
using Trustme.IServices;
using Trustme.Data;

namespace Trustme.Service
{
    public class UserRepository : IUserRepository
    {
        private AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }

        public void AddUser(User _User)
        {
            _context.User.Add(_User);
            _context.SaveChanges();
        }
        public void EditUser(User _User)
        {
            _context.User.Update(_User);
            _context.SaveChanges();
        }

        public void DeleteUser(User _User)
        {
            _context.User.Remove(_User);
            _context.SaveChanges();
        }

        public User GetUserById(int idUser)
        {
            User user = _context.User.Where(a => a.UserId == idUser)?.FirstOrDefault();
            return user;
        }

        public User GetUserbyMail(string mail)
        {
            User usedMailUser = _context.User.Where(a => a.Mail == mail)?.FirstOrDefault();
            return usedMailUser;
        }

        public User GetUserbyUsername(string username)
        {
            User usedUser = _context.User.Where(a => a.Username == username)?.FirstOrDefault();
            return usedUser;
        }

        public IEnumerable<User> ListAllUsers()
        {
            return _context.User.ToList();
        }

        public IEnumerable<User> ListAllUsers(Role _Role)
        {

            List<User> UsersList = (List<User>)_context.Role.Join(_context.User,
                role => role.IdRole,
                user => user.RoleId,
                (role, user) => new User()) ; 
            return UsersList;
        }

        public void UpdateUser(User _User)
        {
            _context.User.Update(_User);
            _context.SaveChanges();
        }
    }
}
